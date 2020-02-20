using ICities;
using System.Collections.Generic;
using RealCity.Util;
using ColossalFramework.UI;
using ColossalFramework;
using System;
using System.Reflection;
using RealCity.CustomAI;
using RealCity.CustomData;

namespace RealCity
{
    public class RealCityThreading : ThreadingExtensionBase
    {
        public static bool isFirstTime = true;
        public static Assembly RealGasStation = null;
        public const int HarmonyPatchNum = 41;
        public override void OnBeforeSimulationFrame()
        {
            base.OnBeforeSimulationFrame();
            if (Loader.CurrentLoadMode == LoadMode.LoadGame || Loader.CurrentLoadMode == LoadMode.NewGame)
            {
                if (RealCity.IsEnabled)
                {
                    CheckDetour();
                    int vehicleStep = (int)(Singleton<VehicleManager>.instance.m_vehicles.m_size >> 4);
                    int num4 = (int)(Singleton<SimulationManager>.instance.m_currentFrameIndex & 15u);
                    int num5 = num4 * vehicleStep;
                    int num6 = (num4 + 1) * vehicleStep - 1;
                    for (int k = num5; k <= num6; k++)
                    {
                        VehicleManager instance = Singleton<VehicleManager>.instance;
                        Vehicle vehicle = instance.m_vehicles.m_buffer[k];
                        Vehicle.Flags flags = vehicle.m_flags;
                        if ((flags & Vehicle.Flags.Created) != 0 && vehicle.m_leadingVehicle == 0)
                        {
                            VehicleStatus(k);
                            if (RealCity.removeStuck)
                            {
                                DetectStuckVehicle((ushort)k, ref instance.m_vehicles.m_buffer[k]);
                            }
                        }
                    }
                }
            }
        }

        public void DetourAfterLoad()
        {
            //This is for Detour Other Mod method
            DebugLog.LogToFileOnly("Init DetourAfterLoad");
            bool detourFailed = false;

            if (Loader.isRealGasStationRunning)
            {
                RealGasStation = Assembly.Load("RealGasStation");
                //1
                DebugLog.LogToFileOnly("Detour RealCityCargoTruckAI::ProcessResourceArriveAtTarget calls");
                try
                {
                    Loader.Detours.Add(new Loader.Detour(RealGasStation.GetType("RealGasStation.CustomAI.CustomCargoTruckAI").GetMethod("ProcessResourceArriveAtTargetForRealCity", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(int).MakeByRefType() }, null),
                                           typeof(RealCityCargoTruckAI).GetMethod("ProcessResourceArriveAtTarget", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(int).MakeByRefType() }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour RealCityCargoTruckAI::ProcessResourceArriveAtTarget");
                    detourFailed = true;
                }

                //2
                DebugLog.LogToFileOnly("Detour RealCityPassengerCarAI::GetVehicleRunningTiming calls");
                try
                {
                    Loader.Detours.Add(new Loader.Detour(RealGasStation.GetType("RealGasStation.CustomAI.CustomPassengerCarAI").GetMethod("GetVehicleRunningTimingForRealCity", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null),
                                           typeof(RealCityPassengerCarAI).GetMethod("GetVehicleRunningTiming", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour RealCityPassengerCarAI::GetVehicleRunningTiming");
                    detourFailed = true;
                }
            }

            if (detourFailed)
            {
                DebugLog.LogToFileOnly("DetourAfterLoad failed");
            }
            else
            {
                DebugLog.LogToFileOnly("DetourAfterLoad successful");
            }
        }

        public void CheckDetour()
        {
            if (isFirstTime && Loader.DetourInited && Loader.HarmonyDetourInited)
            {
                isFirstTime = false;
                DetourAfterLoad();
                DebugLog.LogToFileOnly("ThreadingExtension.OnBeforeSimulationFrame: First frame detected. Checking detours.");
                List<string> list = new List<string>();
                foreach (Loader.Detour current in Loader.Detours)
                {
                    if (!RedirectionHelper.IsRedirected(current.OriginalMethod, current.CustomMethod))
                    {
                        list.Add(string.Format("{0}.{1} with {2} parameters ({3})", new object[]
                        {
                    current.OriginalMethod.DeclaringType.Name,
                    current.OriginalMethod.Name,
                    current.OriginalMethod.GetParameters().Length,
                    current.OriginalMethod.DeclaringType.AssemblyQualifiedName
                        }));
                    }
                }
                DebugLog.LogToFileOnly(string.Format("ThreadingExtension.OnBeforeSimulationFrame: First frame detected. Detours checked. Result: {0} missing detours", list.Count));
                if (list.Count > 0)
                {
                    string error = "RealCity detected an incompatibility with another mod! You can continue playing but it's NOT recommended. RealCity will not work as expected. Send RealCity.txt to Author.";
                    DebugLog.LogToFileOnly(error);
                    string text = "The following methods were overriden by another mod:";
                    foreach (string current2 in list)
                    {
                        text += string.Format("\n\t{0}", current2);
                    }
                    DebugLog.LogToFileOnly(text);
                    UIView.library.ShowModal<ExceptionPanel>("ExceptionPanel").SetMessage("Incompatibility Issue", text, true);
                }

                if (Loader.HarmonyDetourFailed)
                {
                    string error = "RealCity HarmonyDetourInit is failed, Send RealCity.txt to Author.";
                    DebugLog.LogToFileOnly(error);
                    UIView.library.ShowModal<ExceptionPanel>("ExceptionPanel").SetMessage("Incompatibility Issue", error, true);
                }
                else
                {
                    var harmony = new Harmony.Harmony(HarmonyDetours.Id);
                    var methods = harmony.GetPatchedMethods();
                    int i = 0;
                    foreach (var method in methods)
                    {
                        var info = Harmony.Harmony.GetPatchInfo(method);
                        if (info.Owners?.Contains(HarmonyDetours.Id) == true)
                        {
                            DebugLog.LogToFileOnly("Harmony patch method = " + method.Name.ToString());
                            if (info.Prefixes.Count != 0)
                            {
                                DebugLog.LogToFileOnly("Harmony patch method has PreFix");
                            }
                            if (info.Postfixes.Count != 0)
                            {
                                DebugLog.LogToFileOnly("Harmony patch method has PostFix");
                            }
                            i++;
                        }
                    }

                    if (i != HarmonyPatchNum)
                    {
                        string error = $"RealCity HarmonyDetour Patch Num is {i}, Right Num is {HarmonyPatchNum} Send RealCity.txt to Author.";
                        DebugLog.LogToFileOnly(error);
                        UIView.library.ShowModal<ExceptionPanel>("ExceptionPanel").SetMessage("Incompatibility Issue", error, true);
                    }
                }
            }
        }

        public void DetectStuckVehicle(ushort vehicleID, ref Vehicle vehicleData)
        {
            uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
            int num4 = (int)(currentFrameIndex & 255u);
            if (((num4 >> 4) & 15u) == (vehicleID & 15u))
            {
                if (vehicleData.m_flags.IsFlagSet(Vehicle.Flags.WaitingPath))
                {
                    VehicleData.stuckTime[vehicleID] = 0;
                    if (vehicleData.m_path != 0)
                    {
                        VehicleData.watingPathTime[vehicleID]++;
                    }
                    if (VehicleData.watingPathTime[vehicleID] > 10)
                    {
                        ushort building = 0;
                        if (!vehicleData.m_flags.IsFlagSet(Vehicle.Flags.GoingBack))
                        {
                            building = vehicleData.m_targetBuilding;
                        }
                        else
                        {
                            building = vehicleData.m_sourceBuilding;
                        }

                        var buildingData = Singleton<BuildingManager>.instance.m_buildings.m_buffer[building];
                        DebugLog.LogToFileOnly("DebugInfo: Stuck at waitingpath vehicle target building m_class is " + buildingData.Info.m_class.ToString());
                        DebugLog.LogToFileOnly("DebugInfo: Stuck at waitingpath vehicle target name is " + buildingData.Info.name.ToString());
                        DebugLog.LogToFileOnly("DebugInfo: Stuck at waitingpath vehicle transfer type is " + vehicleData.m_transferType.ToString());
                        DebugLog.LogToFileOnly("DebugInfo: Stuck at waitingpath vehicle flag is " + vehicleData.m_flags.ToString());
                        DebugLog.LogToFileOnly("DebugInfo: Stuck at waitingpath vehicle ai is " + vehicleData.Info.m_vehicleAI.ToString());
                        DebugLog.LogToFileOnly("DebugInfo: Stuck at waitingpath vehicle pathflag is " + Singleton<PathManager>.instance.m_pathUnits.m_buffer[vehicleData.m_path].m_simulationFlags.ToString());
                        VehicleData.watingPathTime[vehicleID] = 0;
                        Singleton<PathManager>.instance.ReleasePath(vehicleData.m_path);
                        vehicleData.m_path = 0u;
                        vehicleData.m_flags = (vehicleData.m_flags & ~Vehicle.Flags.WaitingPath);
                    }
                }
                else
                {
                    VehicleData.watingPathTime[vehicleID] = 0;
                    if (!vehicleData.m_flags.IsFlagSet(Vehicle.Flags.WaitingCargo) && !vehicleData.m_flags.IsFlagSet(Vehicle.Flags.WaitingSpace) && !vehicleData.m_flags.IsFlagSet(Vehicle.Flags.WaitingLoading) && !vehicleData.m_flags.IsFlagSet(Vehicle.Flags.WaitingTarget) && !vehicleData.m_flags.IsFlagSet(Vehicle.Flags.WaitingSpace) && !vehicleData.m_flags.IsFlagSet(Vehicle.Flags.Stopped) && !vehicleData.m_flags.IsFlagSet(Vehicle.Flags.Congestion))
                    {
                        float realSpeed = (float)Math.Sqrt(vehicleData.GetLastFrameVelocity().x * vehicleData.GetLastFrameVelocity().x + vehicleData.GetLastFrameVelocity().y * vehicleData.GetLastFrameVelocity().y + vehicleData.GetLastFrameVelocity().z * vehicleData.GetLastFrameVelocity().z);
                        if (realSpeed < 0.1f)
                        {
                            VehicleData.stuckTime[vehicleID]++;
                        }
                        else
                        {
                            VehicleData.stuckTime[vehicleID] = 0;
                        }

                        if (VehicleData.stuckTime[vehicleID] > 600)
                        {
                            DebugLog.LogToFileOnly("DebugInfo: Stuck vehicle transfer type is " + vehicleData.m_transferType.ToString());
                            DebugLog.LogToFileOnly("DebugInfo: Stuck vehicle flag is " + vehicleData.m_flags.ToString());
                            DebugLog.LogToFileOnly("DebugInfo: Stuck vehicle ai is " + vehicleData.Info.m_vehicleAI.ToString());
                            VehicleData.stuckTime[vehicleID] = 0;
                            Singleton<PathManager>.instance.ReleasePath(vehicleData.m_path);
                            vehicleData.m_path = 0u;
                            Singleton<VehicleManager>.instance.ReleaseVehicle(vehicleID);
                        }
                    }
                }
            }
        }

        public void VehicleStatus(int i)
        {
            if (i < Singleton<VehicleManager>.instance.m_vehicles.m_size)
            {
                uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
                int num4 = (int)(currentFrameIndex & 4095u);
                if (((num4 >> 4) & 255u) == (i & 255u))
                {
                    VehicleManager instance = Singleton<VehicleManager>.instance;
                    Vehicle vehicle = instance.m_vehicles.m_buffer[i];
                    if (!vehicle.m_flags.IsFlagSet(Vehicle.Flags.WaitingPath) && vehicle.m_flags.IsFlagSet(Vehicle.Flags.Spawned))
                    {
                        if ((TransferManager.TransferReason)vehicle.m_transferType != TransferManager.TransferReason.DummyCar && (TransferManager.TransferReason)vehicle.m_transferType != TransferManager.TransferReason.DummyPlane && (TransferManager.TransferReason)vehicle.m_transferType != TransferManager.TransferReason.DummyTrain && (TransferManager.TransferReason)vehicle.m_transferType != TransferManager.TransferReason.DummyShip)
                        {
                            if (vehicle.Info.m_vehicleAI is PoliceCarAI || vehicle.Info.m_vehicleAI is DisasterResponseVehicleAI || vehicle.Info.m_vehicleAI is FireTruckAI || vehicle.Info.m_vehicleAI is MaintenanceTruckAI)
                            {
                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 1600, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is GarbageTruckAI || vehicle.Info.m_vehicleAI is HearseAI)
                            {
                                Building building = Singleton<BuildingManager>.instance.m_buildings.m_buffer[vehicle.m_sourceBuilding];
                                if (!building.m_flags.IsFlagSet(Building.Flags.Untouchable))
                                {
                                    Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 1600, vehicle.Info.m_class);
                                }
                            }
                            else if (vehicle.Info.m_vehicleAI is AmbulanceAI || vehicle.Info.m_vehicleAI is SnowTruckAI || vehicle.Info.m_vehicleAI is ParkMaintenanceVehicleAI || vehicle.Info.m_vehicleAI is WaterTruckAI || vehicle.Info.m_vehicleAI is PostVanAI)
                            {
                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 1600, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is TaxiAI)
                            {
                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 500, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is BusAI)
                            {
                                int num = 0;
                                GetVehicleCapacity((ushort)i, ref vehicle, ref num);
                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 100 * num, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is PassengerShipAI || vehicle.Info.m_vehicleAI is PassengerFerryAI)
                            {
                                int num = 0;
                                GetVehicleCapacity((ushort)i, ref vehicle, ref num);
                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 50 * num, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is CargoShipAI)
                            {
                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 5000, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is PassengerPlaneAI || vehicle.Info.m_vehicleAI is PassengerBlimpAI)
                            {
                                int num = 0;
                                GetVehicleCapacity((ushort)i, ref vehicle, ref num);
                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, num * 300, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is CargoPlaneAI)
                            {
                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 15000, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is PassengerTrainAI)
                            {
                                int num = 0;
                                GetVehicleCapacity((ushort)i, ref vehicle, ref num);
                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, num * 250, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is CargoTrainAI)
                            {
                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 10000, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is MetroTrainAI)
                            {
                                int num = 0;
                                GetVehicleCapacity((ushort)i, ref vehicle, ref num);
                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, num * 200, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is PoliceCopterAI || vehicle.Info.m_vehicleAI is FireCopterAI || vehicle.Info.m_vehicleAI is DisasterResponseCopterAI || vehicle.Info.m_vehicleAI is AmbulanceCopterAI)
                            {
                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 20000, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is CableCarAI || vehicle.Info.m_vehicleAI is TramAI)
                            {
                                int num = 0;
                                GetVehicleCapacity((ushort)i, ref vehicle, ref num);
                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, (num * 150), vehicle.Info.m_class);
                            }
                            else if ((vehicle.Info.m_vehicleType == VehicleInfo.VehicleType.Car))
                            {
                                if (!vehicle.m_flags.IsFlagSet(Vehicle.Flags.Stopped))
                                {
                                    VehicleData.vehicleTransferTime[i] = (ushort)(VehicleData.vehicleTransferTime[i] + 64);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                DebugLog.LogToFileOnly("Error: invalid vehicleID = " + i.ToString());
            }
        }

        protected void GetVehicleCapacity(ushort vehicleID, ref Vehicle vehicleData, ref int maxcount)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint num = vehicleData.m_citizenUnits;
            int num2 = 0;
            while (num != 0u)
            {
                if ((ushort)(instance.m_units.m_buffer[(int)((UIntPtr)num)].m_flags & CitizenUnit.Flags.Vehicle) != 0)
                {
                    maxcount += 5;
                }
                num = instance.m_units.m_buffer[(int)((UIntPtr)num)].m_nextUnit;
                if (++num2 > 524288)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }
        }
    }
}
