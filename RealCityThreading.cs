using ColossalFramework;
using ColossalFramework.Globalization;
using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RealCity.CustomAI;
using RealCity.Util;
using RealCity.UI;
using RealCity.CustomManager;
using ColossalFramework.UI;

namespace RealCity
{
    public class RealCityThreading : ThreadingExtensionBase
    {
        public static bool isFirstTime = true;
        public override void OnBeforeSimulationFrame()
        {
            base.OnBeforeSimulationFrame();
            if (Loader.CurrentLoadMode == LoadMode.LoadGame || Loader.CurrentLoadMode == LoadMode.NewGame)
            {
                if (RealCity.IsEnabled)
                {
                    CheckLanguage();
                    CheckDetour();
                }
            }
        }

        public void CheckLanguage()
        {
            if (SingletonLite<LocaleManager>.instance.language.Contains("zh") && (MainDataStore.lastLanguage == 1))
            {
            }
            else if (!SingletonLite<LocaleManager>.instance.language.Contains("zh") && (MainDataStore.lastLanguage != 1))
            {
            }
            else
            {
                MainDataStore.lastLanguage = (byte)(SingletonLite<LocaleManager>.instance.language.Contains("zh") ? 1 : 0);
                Language.LanguageSwitch(MainDataStore.lastLanguage);
                PoliticsUI.refeshOnce = true;
                RealCityUI.refeshOnce = true;
                EcnomicUI.refeshOnce = true;
                PlayerBuildingUI.refeshOnce = true;
                BuildingUI.refeshOnce = true;
                HumanUI.refeshOnce = true;
                TouristUI.refeshOnce = true;
            }
        }

        public void CheckDetour()
        {
            if (isFirstTime && Loader.DetourInited)
            {
                isFirstTime = false;
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
                    string error = "RealCity detected an incompatibility with another mod! You can continue playing but it's NOT recommended. RealCity will not work as expected. See RealCity.log for technical details.";
                    DebugLog.LogToFileOnly(error);
                    string text = "The following methods were overriden by another mod:";
                    foreach (string current2 in list)
                    {
                        text += string.Format("\n\t{0}", current2);
                    }
                    DebugLog.LogToFileOnly(text);
                    UIView.library.ShowModal<ExceptionPanel>("ExceptionPanel").SetMessage("Incompatibility Issue", text, true);
                }
            }
        }

        public override void OnAfterSimulationFrame()
        {
            base.OnAfterSimulationFrame();
            if (Loader.CurrentLoadMode == LoadMode.LoadGame || Loader.CurrentLoadMode == LoadMode.NewGame)
            {
                if (RealCity.IsEnabled)
                {
                    uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
                    //process vehicle
                    int num7 = (int)(currentFrameIndex & 15u);
                    int num8 = num7 * 1024;
                    int num9 = (num7 + 1) * 1024 - 1;
                    VehicleManager instance1 = Singleton<VehicleManager>.instance;
                    for (int i = num8; i <= num9; i = i + 1)
                    {
#if FASTRUN
#else
                        VehicleStatus(i, currentFrameIndex);
#endif
                    }
                }
            }
        }



        public void VehicleStatus(int i, uint currentFrameIndex)
        {
            int num4 = (int)(currentFrameIndex & 255u);
            if (((num4 >> 4) & 15u) == (i & 15u))
            {
                VehicleManager instance = Singleton<VehicleManager>.instance;
                Vehicle vehicle = instance.m_vehicles.m_buffer[i];               
                if (vehicle.m_flags.IsFlagSet(Vehicle.Flags.Created) && !vehicle.m_flags.IsFlagSet(Vehicle.Flags.Deleted))
                {
                    if ((TransferManager.TransferReason)vehicle.m_transferType != TransferManager.TransferReason.DummyCar && (TransferManager.TransferReason)vehicle.m_transferType != TransferManager.TransferReason.DummyPlane && (TransferManager.TransferReason)vehicle.m_transferType != TransferManager.TransferReason.DummyTrain && (TransferManager.TransferReason)vehicle.m_transferType != TransferManager.TransferReason.DummyShip)
                    {
                        if (vehicle.Info.m_vehicleAI is PoliceCarAI || vehicle.Info.m_vehicleAI is DisasterResponseVehicleAI || vehicle.Info.m_vehicleAI is HearseAI)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)100 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                        }
                        else if (vehicle.Info.m_vehicleAI is GarbageTruckAI || vehicle.Info.m_vehicleAI is FireTruckAI || vehicle.Info.m_vehicleAI is MaintenanceTruckAI)
                        {
                            Building building = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)vehicle.m_sourceBuilding];
                            if (!building.m_flags.IsFlagSet(Building.Flags.Untouchable))
                            {
                                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)100 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                            }
                        }
                        else if (vehicle.Info.m_vehicleAI is BusAI || vehicle.Info.m_vehicleAI is AmbulanceAI || vehicle.Info.m_vehicleAI is SnowTruckAI || vehicle.Info.m_vehicleAI is ParkMaintenanceVehicleAI || vehicle.Info.m_vehicleAI is WaterTruckAI || vehicle.Info.m_vehicleAI is PostVanAI)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)100 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                        }
                        else if (vehicle.Info.m_vehicleAI is SnowTruckAI || vehicle.Info.m_vehicleAI is ParkMaintenanceVehicleAI || vehicle.Info.m_vehicleAI is WaterTruckAI || vehicle.Info.m_vehicleAI is PostVanAI)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)100 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                        }
                        else if (vehicle.Info.m_vehicleAI is PassengerShipAI || vehicle.Info.m_vehicleAI is PassengerFerryAI)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)500 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                        }
                        else if (vehicle.Info.m_vehicleAI is CargoShipAI)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)250 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                        }
                        else if (vehicle.Info.m_vehicleAI is PassengerPlaneAI || vehicle.Info.m_vehicleAI is PassengerBlimpAI)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)800 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                        }
                        else if (vehicle.Info.m_vehicleAI is CargoPlaneAI)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)400 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                        }
                        else if (vehicle.Info.m_vehicleAI is PassengerTrainAI)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)250 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                        }
                        else if (vehicle.Info.m_vehicleAI is CargoTrainAI)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)50 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                        }
                        else if (vehicle.Info.m_vehicleAI is MetroTrainAI)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)200 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                        }
                        else if (vehicle.Info.m_vehicleAI is PoliceCopterAI || vehicle.Info.m_vehicleAI is FireCopterAI || vehicle.Info.m_vehicleAI is DisasterResponseCopterAI || vehicle.Info.m_vehicleAI is AmbulanceCopterAI)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)800 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                        }
                        else if (vehicle.Info.m_vehicleAI is CableCarAI || vehicle.Info.m_vehicleAI is TramAI)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)150 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                        }
                        else if ((vehicle.Info.m_vehicleType == VehicleInfo.VehicleType.Car) && (vehicle.Info.m_class.m_subService != ItemClass.SubService.PublicTransportTaxi))
                        {
                            if (!vehicle.m_flags.IsFlagSet(Vehicle.Flags.Stopped))
                            {
                                MainDataStore.vehicleTransferTime[i] = (ushort)(MainDataStore.vehicleTransferTime[i] + 8);
                            }
                            else
                            {
                                MainDataStore.vehicleTransferTime[i] = 0;
                            }
                        }
                    }
                }
                else
                {
                    MainDataStore.vehicleTransferTime[i] = 0;
                    MainDataStore.isVehicleCharged[i] = false;
                }
            }
        }
    }
}
