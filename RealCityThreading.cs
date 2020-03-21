using ICities;
using System.Collections.Generic;
using RealCity.Util;
using ColossalFramework.UI;
using ColossalFramework;
using System;
using System.Reflection;
using RealCity.CustomData;

namespace RealCity
{
    public class RealCityThreading : ThreadingExtensionBase
    {
        public static bool isFirstTime = true;
        public static Assembly RealGasStation = null;
        public const int HarmonyPatchNum = 54;
        public override void OnBeforeSimulationFrame()
        {
            base.OnBeforeSimulationFrame();
            if (Loader.CurrentLoadMode == LoadMode.LoadGame || Loader.CurrentLoadMode == LoadMode.NewGame)
            {
                if (RealCity.IsEnabled)
                {
                    CheckDetour();
                    int vehicleStep = (int)(Singleton<VehicleManager>.instance.m_vehicles.m_size >> 4);
                    int frameIndex = (int)(Singleton<SimulationManager>.instance.m_currentFrameIndex & 15u);
                    int currentStartVehicleID = frameIndex * vehicleStep;
                    int currentEndVehicleID = (frameIndex + 1) * vehicleStep - 1;
                    for (int k = currentStartVehicleID; k <= currentEndVehicleID; k++)
                    {
                        VehicleManager instance = Singleton<VehicleManager>.instance;
                        Vehicle vehicle = instance.m_vehicles.m_buffer[k];
                        Vehicle.Flags flags = vehicle.m_flags;
                        if ((flags & Vehicle.Flags.Created) != 0 && vehicle.m_leadingVehicle == 0)
                        {
                            VehicleStatus((ushort)k);
                        }
                    }
                }
            }
        }

        public void CheckDetour()
        {
            if (isFirstTime && Loader.HarmonyDetourInited)
            {
                isFirstTime = false;
                DebugLog.LogToFileOnly("ThreadingExtension.OnBeforeSimulationFrame: First frame detected. Checking detours.");

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

        public void VehicleStatus(ushort vehicleID)
        {
            if (vehicleID < Singleton<VehicleManager>.instance.m_vehicles.m_size)
            {
                uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
                int frameIndex = (int)(currentFrameIndex & 4095u);
                if (((frameIndex >> 4) & 255u) == (vehicleID & 255u))
                {
                    VehicleManager instance = Singleton<VehicleManager>.instance;
                    Vehicle vehicle = instance.m_vehicles.m_buffer[vehicleID];
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
                                int capacity = 0;
                                GetVehicleCapacity((ushort)vehicleID, ref vehicle, ref capacity);
                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 100 * capacity, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is PassengerShipAI || vehicle.Info.m_vehicleAI is PassengerFerryAI)
                            {
                                int capacity = 0;
                                GetVehicleCapacity((ushort)vehicleID, ref vehicle, ref capacity);
                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 50 * capacity, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is CargoShipAI)
                            {
                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 5000, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is PassengerPlaneAI || vehicle.Info.m_vehicleAI is PassengerBlimpAI)
                            {
                                int capacity = 0;
                                GetVehicleCapacity((ushort)vehicleID, ref vehicle, ref capacity);
                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, capacity * 300, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is CargoPlaneAI)
                            {
                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 15000, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is PassengerTrainAI)
                            {
                                int capacity = 0;
                                GetVehicleCapacity((ushort)vehicleID, ref vehicle, ref capacity);
                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, capacity * 250, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is CargoTrainAI)
                            {
                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 10000, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is MetroTrainAI)
                            {
                                int capacity = 0;
                                GetVehicleCapacity((ushort)vehicleID, ref vehicle, ref capacity);
                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, capacity * 200, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is PoliceCopterAI || vehicle.Info.m_vehicleAI is FireCopterAI || vehicle.Info.m_vehicleAI is DisasterResponseCopterAI || vehicle.Info.m_vehicleAI is AmbulanceCopterAI)
                            {
                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 20000, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is CableCarAI || vehicle.Info.m_vehicleAI is TramAI)
                            {
                                int capacity = 0;
                                GetVehicleCapacity((ushort)vehicleID, ref vehicle, ref capacity);
                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, (capacity * 150), vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is PassengerCarAI  && !vehicle.m_flags.IsFlagSet(Vehicle.Flags.Parking) && !vehicle.m_flags.IsFlagSet(Vehicle.Flags.Arriving))
                            {
                                if ((vehicle.Info.m_class.m_subService == ItemClass.SubService.ResidentialLow))
                                {
                                    if (RealCity.reduceVehicle)
                                        VehicleData.vehicleTransferTime[vehicleID] = (ushort)(VehicleData.vehicleTransferTime[vehicleID] + (24 << MainDataStore.reduceCargoDivShift));
                                    else
                                        VehicleData.vehicleTransferTime[vehicleID] = (ushort)(VehicleData.vehicleTransferTime[vehicleID] + 24);
                                }
                                else
                                {
                                    VehicleData.vehicleTransferTime[vehicleID] = (ushort)(VehicleData.vehicleTransferTime[vehicleID] + 12);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                DebugLog.LogToFileOnly("Error: invalid vehicleID = " + vehicleID.ToString());
            }
        }

        protected void GetVehicleCapacity(ushort vehicleID, ref Vehicle vehicleData, ref int capacity)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint num = vehicleData.m_citizenUnits;
            int num2 = 0;
            while (num != 0u)
            {
                if ((ushort)(instance.m_units.m_buffer[(int)((UIntPtr)num)].m_flags & CitizenUnit.Flags.Vehicle) != 0)
                {
                    capacity += 5;
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
