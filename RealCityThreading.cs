using ICities;
using RealCity.Util;
using ColossalFramework.UI;
using ColossalFramework;
using System;
using RealCity.CustomData;
using HarmonyLib;

namespace RealCity
{
    public class RealCityThreading : ThreadingExtensionBase
    {
        public static bool isFirstTime = true;
        public const int HarmonyPatchNum = 60;
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
                //Caculate goverment salary
                RealCityEconomyExtension.CaculateGovermentSalary();
                //reset playereducation fee
                RealCityEconomyExtension.RefreshPlayerEducationFee();

                if (Loader.HarmonyDetourFailed)
                {
                    string error = "RealCity HarmonyDetourInit is failed, Send RealCity.txt to Author.";
                    DebugLog.LogToFileOnly(error);
                    UIView.library.ShowModal<ExceptionPanel>("ExceptionPanel").SetMessage("Incompatibility Issue", error, true);
                }
                else
                {
                    var harmony = new Harmony(HarmonyDetours.Id);
                    var methods = harmony.GetPatchedMethods();
                    int i = 0;
                    foreach (var method in methods)
                    {
                        var info = Harmony.GetPatchInfo(method);
                        if (info.Owners?.Contains(HarmonyDetours.Id) == true)
                        {
                            DebugLog.LogToFileOnly($"Harmony patch method = {method.FullDescription()}");
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
                            switch (vehicle.Info.m_class.m_service)
                            {
                                case ItemClass.Service.PoliceDepartment:
                                case ItemClass.Service.HealthCare:
                                case ItemClass.Service.FireDepartment:
                                case ItemClass.Service.Disaster:
                                    if (vehicle.Info.m_vehicleType == VehicleInfo.VehicleType.Helicopter)
                                        Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 20000, vehicle.Info.m_class);
                                    else
                                    {
                                        if (vehicle.m_flags.IsFlagSet(Vehicle.Flags.Emergency2) || vehicle.m_flags.IsFlagSet(Vehicle.Flags.Emergency1))
                                            Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 1600, vehicle.Info.m_class);
                                        else
                                            Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 800, vehicle.Info.m_class);
                                    }
                                    break;
                                case ItemClass.Service.Road:
                                case ItemClass.Service.Garbage:
                                    if (!vehicle.m_flags.IsFlagSet(Vehicle.Flags.Importing))
                                    {
                                        Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 1600, vehicle.Info.m_class);
                                    }
                                    break;
                                case ItemClass.Service.PublicTransport:
                                    int capacity = 0;
                                    GetVehicleCapacity((ushort)vehicleID, ref vehicle, ref capacity);
                                    switch (vehicle.Info.m_class.m_subService)
                                    {
                                        case ItemClass.SubService.PublicTransportBus:
                                        case ItemClass.SubService.PublicTransportTrolleybus:
                                        case ItemClass.SubService.PublicTransportTours:
                                            Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 10 * capacity, vehicle.Info.m_class); break;
                                        case ItemClass.SubService.PublicTransportMonorail:
                                        case ItemClass.SubService.PublicTransportCableCar:
                                        case ItemClass.SubService.PublicTransportTram:
                                            Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 15 * capacity, vehicle.Info.m_class); break;
                                        case ItemClass.SubService.PublicTransportMetro:
                                            Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 200 * capacity, vehicle.Info.m_class); break;
                                        case ItemClass.SubService.PublicTransportTrain:
                                            if (vehicle.Info.m_vehicleAI is CargoTrainAI)
                                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 5000, vehicle.Info.m_class);
                                            else
                                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 300 * capacity, vehicle.Info.m_class);
                                            break;
                                        case ItemClass.SubService.PublicTransportPlane:
                                            if (vehicle.Info.m_vehicleAI is CargoPlaneAI)
                                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 10000, vehicle.Info.m_class);
                                            else
                                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 400 * capacity, vehicle.Info.m_class);
                                            break;
                                        case ItemClass.SubService.PublicTransportPost:
                                            Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 800, vehicle.Info.m_class); break;
                                        case ItemClass.SubService.PublicTransportTaxi:
                                            Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 200, vehicle.Info.m_class); break;
                                        case ItemClass.SubService.PublicTransportShip:
                                            if (vehicle.Info.m_vehicleAI is CargoShipAI)
                                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 3000, vehicle.Info.m_class);
                                            else
                                                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, 20 * capacity, vehicle.Info.m_class);
                                            break;
                                    }
                                    break;
                                case ItemClass.Service.Residential:
                                    if (!vehicle.m_flags.IsFlagSet(Vehicle.Flags.Parking) && !vehicle.m_flags.IsFlagSet(Vehicle.Flags.Arriving))
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
                                    break;
                            }
                        }
                    }
                }
            }
            else
            {
                DebugLog.LogToFileOnly($"Error: VehicleStatus invalid vehicleID = {vehicleID}");
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