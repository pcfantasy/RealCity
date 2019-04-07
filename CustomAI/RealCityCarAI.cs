using ColossalFramework;
using RealCity.Util;
using System;

namespace RealCity.CustomAI
{
    public class RealCityCarAI
    {
        public static byte[] watingPathTime = new byte[16384];
        public static ushort[] stuckTime = new ushort[16384];
        //For detour AdvanceJuctionRules
        public static void CarAICustomSimulationStepPreFix(ushort vehicleID, ref Vehicle vehicleData)
        {
            VehicleStatus(vehicleID);
            DetectStuckVehicle(vehicleID, ref vehicleData);
        }

        public static void CarAISimulationStepPreFix(ushort vehicleID, ref Vehicle vehicleData, ref Vehicle.Frame frameData, ushort leaderID, ref Vehicle leaderData, int lodPhysics)
        {
            VehicleStatus(vehicleID);
            DetectStuckVehicle(vehicleID, ref vehicleData);
        }

        public static void DetectStuckVehicle(ushort vehicleID, ref Vehicle vehicleData)
        {
            if (vehicleData.m_flags.IsFlagSet(Vehicle.Flags.WaitingPath))
            {
                stuckTime[vehicleID] = 0;
                if (vehicleData.m_path != 0)
                {
                    watingPathTime[vehicleID]++;
                }
                if (watingPathTime[vehicleID] > 192)
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
                    watingPathTime[vehicleID] = 0;
                    Singleton<PathManager>.instance.ReleasePath(vehicleData.m_path);
                    vehicleData.m_path = 0u;
                    vehicleData.m_flags = (vehicleData.m_flags & ~Vehicle.Flags.WaitingPath);
                }
            }
            else
            {
                watingPathTime[vehicleID] = 0;
                if (!vehicleData.m_flags.IsFlagSet(Vehicle.Flags.WaitingCargo) && !vehicleData.m_flags.IsFlagSet(Vehicle.Flags.WaitingSpace) && !vehicleData.m_flags.IsFlagSet(Vehicle.Flags.WaitingLoading) && !vehicleData.m_flags.IsFlagSet(Vehicle.Flags.WaitingTarget) && !vehicleData.m_flags.IsFlagSet(Vehicle.Flags.WaitingSpace) && !vehicleData.m_flags.IsFlagSet(Vehicle.Flags.Stopped) && !vehicleData.m_flags.IsFlagSet(Vehicle.Flags.Congestion))
                {
                    float realSpeed = (float)Math.Sqrt(vehicleData.GetLastFrameVelocity().x * vehicleData.GetLastFrameVelocity().x + vehicleData.GetLastFrameVelocity().y * vehicleData.GetLastFrameVelocity().y + vehicleData.GetLastFrameVelocity().z * vehicleData.GetLastFrameVelocity().z);
                    if (realSpeed < 0.1f)
                    {
                        stuckTime[vehicleID]++;
                    }
                    else
                    {
                        stuckTime[vehicleID] = 0;
                    }

                    if (stuckTime[vehicleID] > 800)
                    {
                        DebugLog.LogToFileOnly("DebugInfo: Stuck vehicle transfer type is " + vehicleData.m_transferType.ToString());
                        DebugLog.LogToFileOnly("DebugInfo: Stuck vehicle flag is " + vehicleData.m_flags.ToString());
                        DebugLog.LogToFileOnly("DebugInfo: Stuck vehicle ai is " + vehicleData.Info.m_vehicleAI.ToString());
                        stuckTime[vehicleID] = 0;
                        Singleton<PathManager>.instance.ReleasePath(vehicleData.m_path);
                        vehicleData.m_path = 0u;
                        Singleton<VehicleManager>.instance.ReleaseVehicle(vehicleID);
                    }
                }
            }
        }

        public static void VehicleStatus(int i)
        {
            if (i < 16384)
            {
                uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
                int num4 = (int)(currentFrameIndex & 255u);
                if (((num4 >> 4) & 15u) == (i & 15u))
                {
                    VehicleManager instance = Singleton<VehicleManager>.instance;
                    Vehicle vehicle = instance.m_vehicles.m_buffer[i];
                    if (!vehicle.m_flags.IsFlagSet(Vehicle.Flags.WaitingPath) && vehicle.m_flags.IsFlagSet(Vehicle.Flags.Spawned))
                    {
                        if ((TransferManager.TransferReason)vehicle.m_transferType != TransferManager.TransferReason.DummyCar && (TransferManager.TransferReason)vehicle.m_transferType != TransferManager.TransferReason.DummyPlane && (TransferManager.TransferReason)vehicle.m_transferType != TransferManager.TransferReason.DummyTrain && (TransferManager.TransferReason)vehicle.m_transferType != TransferManager.TransferReason.DummyShip)
                        {
                            if (vehicle.Info.m_vehicleAI is PoliceCarAI || vehicle.Info.m_vehicleAI is DisasterResponseVehicleAI || vehicle.Info.m_vehicleAI is HearseAI)
                            {
                                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, 100 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is GarbageTruckAI || vehicle.Info.m_vehicleAI is FireTruckAI || vehicle.Info.m_vehicleAI is MaintenanceTruckAI)
                            {
                                Building building = Singleton<BuildingManager>.instance.m_buildings.m_buffer[vehicle.m_sourceBuilding];
                                if (!building.m_flags.IsFlagSet(Building.Flags.Untouchable))
                                {
                                    Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, 100 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                                }
                            }
                            else if (vehicle.Info.m_vehicleAI is BusAI || vehicle.Info.m_vehicleAI is AmbulanceAI || vehicle.Info.m_vehicleAI is SnowTruckAI || vehicle.Info.m_vehicleAI is ParkMaintenanceVehicleAI || vehicle.Info.m_vehicleAI is WaterTruckAI || vehicle.Info.m_vehicleAI is PostVanAI)
                            {
                                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, 100 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is SnowTruckAI || vehicle.Info.m_vehicleAI is ParkMaintenanceVehicleAI || vehicle.Info.m_vehicleAI is WaterTruckAI || vehicle.Info.m_vehicleAI is PostVanAI)
                            {
                                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, 100 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is PassengerShipAI || vehicle.Info.m_vehicleAI is PassengerFerryAI)
                            {
                                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, 500 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is CargoShipAI)
                            {
                                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, 250 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is PassengerPlaneAI || vehicle.Info.m_vehicleAI is PassengerBlimpAI)
                            {
                                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, 800 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is CargoPlaneAI)
                            {
                                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, 400 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is PassengerTrainAI)
                            {
                                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, 250 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is CargoTrainAI)
                            {
                                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, 50 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is MetroTrainAI)
                            {
                                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, 200 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is PoliceCopterAI || vehicle.Info.m_vehicleAI is FireCopterAI || vehicle.Info.m_vehicleAI is DisasterResponseCopterAI || vehicle.Info.m_vehicleAI is AmbulanceCopterAI)
                            {
                                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, 800 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                            }
                            else if (vehicle.Info.m_vehicleAI is CableCarAI || vehicle.Info.m_vehicleAI is TramAI)
                            {
                                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, 150 * MainDataStore.gameExpenseDivide, vehicle.Info.m_class);
                            }
                            else if ((vehicle.Info.m_vehicleType == VehicleInfo.VehicleType.Car) && (vehicle.Info.m_class.m_subService != ItemClass.SubService.PublicTransportTaxi))
                            {
                                if (!vehicle.m_flags.IsFlagSet(Vehicle.Flags.Stopped))
                                {
                                    MainDataStore.vehicleTransferTime[i] = (ushort)(MainDataStore.vehicleTransferTime[i] + 8);
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
    }
}
