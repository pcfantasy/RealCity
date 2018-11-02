using ColossalFramework;
using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RealCity
{
    public class RealCityThreading : ThreadingExtensionBase
    {
        public override void OnBeforeSimulationFrame()
        {
            base.OnBeforeSimulationFrame();
            if (Loader.CurrentLoadMode == LoadMode.LoadGame || Loader.CurrentLoadMode == LoadMode.NewGame)
            {
                uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
                int num4 = (int)(currentFrameIndex & 255u);
                int num5 = num4 * 192;
                int num6 = (num4 + 1) * 192 - 1;
                //DebugLog.LogToFileOnly("currentFrameIndex num2 = " + currentFrameIndex.ToString());
                BuildingManager instance = Singleton<BuildingManager>.instance;


                for (int i = num5; i <= num6; i = i + 1)
                {
                    if (instance.m_buildings.m_buffer[i].Info.m_buildingAI is OutsideConnectionAI)
                    {
                        ProcessOutsideDemand((ushort)i, ref instance.m_buildings.m_buffer[i]);
                        AddGarbageOffers((ushort)i, ref instance.m_buildings.m_buffer[i]);
                    }
                }
            }
        }

        public void ProcessOutsideDemand(ushort buildingID, ref Building data)
        {
            if (data.Info.m_class.m_service == ItemClass.Service.Road)
            {
                if (Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.Garbage))
                {
                    if ((data.m_flags & Building.Flags.IncomingOutgoing) == Building.Flags.Incoming)
                    {
                        data.m_garbageBuffer = (ushort)(data.m_garbageBuffer + 40);
                    }
                    else
                    {
                        data.m_garbageBuffer = (ushort)(data.m_garbageBuffer + 20);
                    }
                }

                if (data.m_garbageBuffer > 20000)
                {
                    data.m_garbageBuffer = 20000;
                }
            }
            else if (RealCityEconomyExtension.updateOnce && (data.m_garbageBuffer != 0))
            {
                data.m_garbageBuffer = 0;
                TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                offer.Building = buildingID;
                Singleton<TransferManager>.instance.RemoveOutgoingOffer(TransferManager.TransferReason.Garbage, offer);
                Singleton<TransferManager>.instance.RemoveOutgoingOffer(TransferManager.TransferReason.GarbageMove, offer);
            }
            else
            {
                data.m_garbageBuffer = 0;
            }
        }


        public void AddGarbageOffers(ushort buildingID, ref Building data)
        {
            TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);

            if (!Politics.isOutSideGarbagePermit && RealCityOutsideConnectionAI.haveGarbageBuildingFinal && Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.Garbage))
            {
                if ((data.m_flags & Building.Flags.IncomingOutgoing) == Building.Flags.Incoming)
                {
                    if (data.m_garbageBuffer > 200)
                    {
                        int car_valid_path = TickPathfindStatus(ref data.m_education3, ref data.m_adults);
                        SimulationManager instance1 = Singleton<SimulationManager>.instance;
                        if (car_valid_path + instance1.m_randomizer.Int32(256u) >> 8 == 0)
                        {
                            if (instance1.m_randomizer.Int32(16u) == 0)
                            {
                                //DebugLog.LogToFileOnly("outside connection is not good for car in for garbageoffers");
                                int num24 = (int)data.m_garbageBuffer;
                                if (Singleton<SimulationManager>.instance.m_randomizer.Int32(5u) == 0 && Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.Garbage))
                                {
                                    int num25 = 0;
                                    int num26 = 0;
                                    int num27 = 0;
                                    int num28 = 0;
                                    this.CalculateGuestVehicles(buildingID, ref data, TransferManager.TransferReason.Garbage, ref num25, ref num26, ref num27, ref num28);
                                    num24 -= num27 - num26;
                                    //DebugLog.LogToFileOnly("caculate num24  = " + num24.ToString() + "num27 = " + num27.ToString() + "num26 = " + num26.ToString());
                                    if (num24 >= 200)
                                    {
                                        offer = default(TransferManager.TransferOffer);
                                        offer.Priority = num24 / 1000;
                                        if (offer.Priority > 7)
                                        {
                                            offer.Priority = 7;
                                        }
                                        offer.Building = buildingID;
                                        offer.Position = data.m_position;
                                        offer.Amount = 1;
                                        offer.Active = false;
                                        Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Garbage, offer);
                                    }
                                }
                            }
                        }
                        else
                        {
                            int num24 = (int)data.m_garbageBuffer;
                            if (Singleton<SimulationManager>.instance.m_randomizer.Int32(5u) == 0 && Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.Garbage))
                            {
                                int num25 = 0;
                                int num26 = 0;
                                int num27 = 0;
                                int num28 = 0;
                                CalculateGuestVehicles(buildingID, ref data, TransferManager.TransferReason.Garbage, ref num25, ref num26, ref num27, ref num28);
                                num24 -= num27 - num26;
                                //DebugLog.LogToFileOnly("caculate num24  = " + num24.ToString() + "num27 = " + num27.ToString() + "num26 = " + num26.ToString());
                                if (num24 >= 200)
                                {
                                    offer = default(TransferManager.TransferOffer);
                                    offer.Priority = num24 / 1000;
                                    if (offer.Priority > 7)
                                    {
                                        offer.Priority = 7;
                                    }
                                    offer.Building = buildingID;
                                    offer.Position = data.m_position;
                                    offer.Amount = 1;
                                    offer.Active = false;
                                    Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Garbage, offer);
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (data.m_garbageBuffer > 4000)
                    {
                        int car_valid_path = TickPathfindStatus(ref data.m_teens, ref data.m_serviceProblemTimer);
                        SimulationManager instance1 = Singleton<SimulationManager>.instance;
                        if (car_valid_path + instance1.m_randomizer.Int32(256u) >> 8 == 0)
                        {
                            if (instance1.m_randomizer.Int32(16u) == 0)
                            {
                                offer = default(TransferManager.TransferOffer);
                                offer.Priority = 1 + data.m_garbageBuffer / 5000;
                                if (offer.Priority > 7)
                                {
                                    offer.Priority = 7;
                                }
                                offer.Building = buildingID;
                                offer.Position = data.m_position;
                                offer.Amount = 1;
                                offer.Active = true;
                                Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.GarbageMove, offer);
                            }
                        }
                        else
                        {
                            offer = default(TransferManager.TransferOffer);
                            offer.Priority = 1 + data.m_garbageBuffer / 5000;
                            if (offer.Priority > 7)
                            {
                                offer.Priority = 7;
                            }
                            offer.Building = buildingID;
                            offer.Position = data.m_position;
                            offer.Amount = 1;
                            offer.Active = true;
                            Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.GarbageMove, offer);
                        }
                    }
                }
            }
        }

        // OutsideConnectionAI
        private static int TickPathfindStatus(ref byte success, ref byte failure)
        {
            int result = ((int)success << 8) / Mathf.Max(1, (int)(success + failure));
            if (success > failure)
            {
                success = (byte)(success + 1 >> 1);
                failure = (byte)(failure >> 1);
            }
            else
            {
                success = (byte)(success >> 1);
                failure = (byte)(failure + 1 >> 1);
            }
            return result;
        }


        public override void OnAfterSimulationFrame()
        {
            base.OnAfterSimulationFrame();
            if (Loader.CurrentLoadMode == LoadMode.LoadGame || Loader.CurrentLoadMode == LoadMode.NewGame)
            {
                uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
                int num4 = (int)(currentFrameIndex & 255u);
                int num5 = num4 * 192;
                int num6 = (num4 + 1) * 192 - 1;
                //DebugLog.LogToFileOnly("currentFrameIndex num2 = " + currentFrameIndex.ToString());
                BuildingManager instance = Singleton<BuildingManager>.instance;


                for (int i = num5; i <= num6; i = i + 1)
                {
                    if (instance.m_buildings.m_buffer[i].m_flags.IsFlagSet(Building.Flags.Created) && (instance.m_buildings.m_buffer[i].m_productionRate != 0) && !instance.m_buildings.m_buffer[i].m_flags.IsFlagSet(Building.Flags.Deleted) && !instance.m_buildings.m_buffer[i].m_flags.IsFlagSet(Building.Flags.Untouchable))
                    {
                        if (RealCityEconomyExtension.IsSpecialBuilding((ushort)i) == 3)
                        {
                            MainDataStore.haveCityResourceDepartment = true;
                            ProcessCityResourceDepartmentBuilding((ushort)i, instance.m_buildings.m_buffer[i]);
                        }

                        if (instance.m_buildings.m_buffer[i].Info.m_class.m_service == ItemClass.Service.Garbage)
                        {
                            RealCityOutsideConnectionAI.haveGarbageBuilding = true;
                        }
                    }
                }


                int num7 = (int)(currentFrameIndex & 15u);
                int num8 = num7 * 1024;
                int num9 = (num7 + 1) * 1024 - 1;
                //DebugLog.LogToFileOnly("currentFrameIndex num2 = " + currentFrameIndex.ToString());
                VehicleManager instance1 = Singleton<VehicleManager>.instance;
                for (int i = num8; i <= num9; i = i + 1)
                {
                    VehicleStatus(i, currentFrameIndex);
                }
            }
        }


        public void VehicleStatus(int i, uint currentFrameIndex)
        {
            VehicleManager instance = Singleton<VehicleManager>.instance;
            //System.Random rand = new System.Random();
            Vehicle vehicle = instance.m_vehicles.m_buffer[i];
            int num4 = (int)(currentFrameIndex & 255u);
            if (vehicle.m_flags.IsFlagSet(Vehicle.Flags.Created) && !vehicle.m_flags.IsFlagSet(Vehicle.Flags.Deleted))
            {
                if ((vehicle.Info.m_vehicleType == VehicleInfo.VehicleType.Car) && (vehicle.Info.m_class.m_subService != ItemClass.SubService.PublicTransportTaxi))
                {
                    if (!vehicle.m_flags.IsFlagSet(Vehicle.Flags.Stopped))
                    {
                        MainDataStore.vehical_transfer_time[i] = (ushort)(MainDataStore.vehical_transfer_time[i] + 1);
                        if (num4 >= 240)
                        {
                            if ((TransferManager.TransferReason)vehicle.m_transferType != TransferManager.TransferReason.DummyCar && (TransferManager.TransferReason)vehicle.m_transferType != TransferManager.TransferReason.DummyPlane && (TransferManager.TransferReason)vehicle.m_transferType != TransferManager.TransferReason.DummyTrain && (TransferManager.TransferReason)vehicle.m_transferType != TransferManager.TransferReason.DummyShip)
                            {
                                if (vehicle.Info.m_vehicleAI is PoliceCarAI || vehicle.Info.m_vehicleAI is DisasterResponseVehicleAI || vehicle.Info.m_vehicleAI is HearseAI)
                                {
                                    MainDataStore.allVehicles++;
                                    Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)3000, vehicle.Info.m_class);
                                }
                                else if (vehicle.Info.m_vehicleAI is GarbageTruckAI || vehicle.Info.m_vehicleAI is FireTruckAI || vehicle.Info.m_vehicleAI is MaintenanceTruckAI)
                                {
                                    if (vehicle.m_flags.IsFlagSet(Vehicle.Flags.Importing))
                                    {
                                        //comm_data.allVehicles += 1;
                                        //Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)6000, vehicle.Info.m_class);
                                    }
                                    else
                                    {
                                        MainDataStore.allVehicles += 2;
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)15000, vehicle.Info.m_class);
                                    }
                                }
                                else if (vehicle.Info.m_vehicleAI is BusAI || vehicle.Info.m_vehicleAI is AmbulanceAI || vehicle.Info.m_vehicleAI is SnowTruckAI || vehicle.Info.m_vehicleAI is ParkMaintenanceVehicleAI || vehicle.Info.m_vehicleAI is WaterTruckAI || vehicle.Info.m_vehicleAI is PostVanAI)
                                {
                                    MainDataStore.allVehicles += 2;
                                    Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)10000, vehicle.Info.m_class);
                                }
                                else if (vehicle.Info.m_vehicleAI is PassengerShipAI || vehicle.Info.m_vehicleAI is PassengerFerryAI || vehicle.Info.m_vehicleAI is CargoShipAI)
                                {
                                    MainDataStore.allVehicles += 4;
                                    Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)25000, vehicle.Info.m_class);
                                }
                                else if (vehicle.Info.m_vehicleAI is PassengerPlaneAI || vehicle.Info.m_vehicleAI is PassengerBlimpAI || vehicle.Info.m_vehicleAI is CargoPlaneAI)
                                {
                                    MainDataStore.allVehicles += 8;
                                    Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)60000, vehicle.Info.m_class);
                                }
                                else if (vehicle.Info.m_vehicleAI is PassengerTrainAI || vehicle.Info.m_vehicleAI is CargoTrainAI)
                                {
                                    Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)50000, vehicle.Info.m_class);
                                }
                                else if (vehicle.Info.m_vehicleAI is MetroTrainAI)
                                {
                                    Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)40000, vehicle.Info.m_class);
                                }
                                else if (vehicle.Info.m_vehicleAI is PoliceCopterAI || vehicle.Info.m_vehicleAI is FireCopterAI || vehicle.Info.m_vehicleAI is DisasterResponseCopterAI || vehicle.Info.m_vehicleAI is AmbulanceCopterAI)
                                {
                                    MainDataStore.allVehicles += 8;
                                    Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)60000, vehicle.Info.m_class);
                                }
                                else if (vehicle.Info.m_vehicleAI is CableCarAI || vehicle.Info.m_vehicleAI is TramAI)
                                {
                                    Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)20000, vehicle.Info.m_class);
                                }
                            }
                        }
                    }
                    else
                    {
                        MainDataStore.vehical_transfer_time[i] = 0;
                    }
                }
            }
            else
            {
                MainDataStore.vehical_transfer_time[i] = 0;
            }
        }


        void ProcessCityResourceDepartmentBuilding(ushort buildingID, Building buildingData)
        {
            int num27 = 0;
            int num28 = 0;
            int num29 = 0;
            int value = 0;
            int num34 = 0;
            TransferManager.TransferReason incomingTransferReason = default(TransferManager.TransferReason);

            //Foods
            incomingTransferReason = TransferManager.TransferReason.Food;
            if (incomingTransferReason != TransferManager.TransferReason.None)
            {
                CalculateGuestVehicles(buildingID, ref buildingData, incomingTransferReason, ref num27, ref num28, ref num29, ref value);
                buildingData.m_tempImport = (byte)Mathf.Clamp(value, (int)buildingData.m_tempImport, 255);
            }

            num34 = 18000 - MainDataStore.building_buffer3[buildingID] - num29;
            if (num34 >= 0)
            {
                TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                offer.Priority = num34 / 1000;
                if (offer.Priority > 7)
                {
                    offer.Priority = 7;
                }
                offer.Building = buildingID;
                offer.Position = buildingData.m_position;
                offer.Amount = 1;
                offer.Active = false;
                Singleton<TransferManager>.instance.AddIncomingOffer(incomingTransferReason, offer);
            }

            if (MainDataStore.building_buffer3[buildingID] > 0)
            {
                if (RealCityEconomyExtension.foodStillNeeded >= 1)
                {
                    if (MainDataStore.building_buffer3[buildingID] - (RealCityEconomyExtension.foodStillNeeded) > 0)
                    {
                        MainDataStore.building_buffer3[buildingID] -= (ushort)(RealCityEconomyExtension.foodStillNeeded);
                        RealCityEconomyExtension.foodStillNeeded = 0;
                    }
                    else
                    {
                        RealCityEconomyExtension.foodStillNeeded -= MainDataStore.building_buffer3[buildingID];
                        MainDataStore.building_buffer3[buildingID] = 0;
                    }
                }
            }
            MainDataStore.allFoods += MainDataStore.building_buffer3[buildingID];

            //Petrol
            incomingTransferReason = TransferManager.TransferReason.Petrol;
            num27 = 0;
            num28 = 0;
            num29 = 0;
            value = 0;
            num34 = 0;
            if (incomingTransferReason != TransferManager.TransferReason.None)
            {
                CalculateGuestVehicles(buildingID, ref buildingData, incomingTransferReason, ref num27, ref num28, ref num29, ref value);
                buildingData.m_tempImport = (byte)Mathf.Clamp(value, (int)buildingData.m_tempImport, 255);
            }

            num34 = 18000 - MainDataStore.building_buffer2[buildingID] - num29;
            if (num34 >= 0)
            {
                TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                offer.Priority = num34 / 1000;
                if (offer.Priority > 7)
                {
                    offer.Priority = 7;
                }
                offer.Building = buildingID;
                offer.Position = buildingData.m_position;
                offer.Amount = 1;
                offer.Active = false;
                Singleton<TransferManager>.instance.AddIncomingOffer(incomingTransferReason, offer);
            }

            if (MainDataStore.building_buffer2[buildingID] > 0)
            {
                if (RealCityEconomyExtension.petrolStillNeeded >= 1)
                {
                    if (MainDataStore.building_buffer2[buildingID] - RealCityEconomyExtension.petrolStillNeeded > 0)
                    {
                        MainDataStore.building_buffer2[buildingID] -= (ushort)(RealCityEconomyExtension.petrolStillNeeded);
                        RealCityEconomyExtension.petrolStillNeeded = 0;
                    }
                    else
                    {
                        RealCityEconomyExtension.petrolStillNeeded -= MainDataStore.building_buffer2[buildingID];
                        MainDataStore.building_buffer2[buildingID] = 0;
                    }
                }
            }
            MainDataStore.allPetrols += MainDataStore.building_buffer2[buildingID];

            //Coal
            incomingTransferReason = TransferManager.TransferReason.Coal;
            num27 = 0;
            num28 = 0;
            num29 = 0;
            value = 0;
            num34 = 0;
            if (incomingTransferReason != TransferManager.TransferReason.None)
            {
                CalculateGuestVehicles(buildingID, ref buildingData, incomingTransferReason, ref num27, ref num28, ref num29, ref value);
                buildingData.m_tempImport = (byte)Mathf.Clamp(value, (int)buildingData.m_tempImport, 255);
            }

            num34 = 18000 - MainDataStore.building_buffer1[buildingID] - num29;
            if (num34 >= 0)
            {
                TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                offer.Priority = num34 / 1000;
                if (offer.Priority > 7)
                {
                    offer.Priority = 7;
                }
                offer.Building = buildingID;
                offer.Position = buildingData.m_position;
                offer.Amount = 1;
                offer.Active = false;
                Singleton<TransferManager>.instance.AddIncomingOffer(incomingTransferReason, offer);
            }

            if (MainDataStore.building_buffer1[buildingID] > 0)
            {
                if (RealCityEconomyExtension.coalStillNeeded >= 1)
                {
                    if (MainDataStore.building_buffer1[buildingID] - RealCityEconomyExtension.coalStillNeeded > 0)
                    {
                        MainDataStore.building_buffer1[buildingID] -= (ushort)(RealCityEconomyExtension.coalStillNeeded);
                        RealCityEconomyExtension.coalStillNeeded = 0;
                    }
                    else
                    {
                        RealCityEconomyExtension.coalStillNeeded -= MainDataStore.building_buffer1[buildingID];
                        MainDataStore.building_buffer1[buildingID] = 0;
                    }
                }
            }
            MainDataStore.allCoals += MainDataStore.building_buffer1[buildingID];

            //Lumber
            incomingTransferReason = TransferManager.TransferReason.Lumber;
            num27 = 0;
            num28 = 0;
            num29 = 0;
            value = 0;
            num34 = 0;
            if (incomingTransferReason != TransferManager.TransferReason.None)
            {
                CalculateGuestVehicles(buildingID, ref buildingData, incomingTransferReason, ref num27, ref num28, ref num29, ref value);
                buildingData.m_tempImport = (byte)Mathf.Clamp(value, (int)buildingData.m_tempImport, 255);
            }

            num34 = 18000 - MainDataStore.building_buffer4[buildingID] - num29;
            if (num34 >= 0)
            {
                TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                offer.Priority = num34 / 1000;
                if (offer.Priority > 7)
                {
                    offer.Priority = 7;
                }
                offer.Building = buildingID;
                offer.Position = buildingData.m_position;
                offer.Amount = 1;
                offer.Active = false;
                Singleton<TransferManager>.instance.AddIncomingOffer(incomingTransferReason, offer);
            }

            if (MainDataStore.building_buffer4[buildingID] > 0)
            {
                if (RealCityEconomyExtension.lumberStillNeeded >= 1)
                {
                    if (MainDataStore.building_buffer4[buildingID] - (RealCityEconomyExtension.lumberStillNeeded) > 0)
                    {
                        MainDataStore.building_buffer4[buildingID] -= (ushort)(RealCityEconomyExtension.lumberStillNeeded);
                        RealCityEconomyExtension.lumberStillNeeded = 0;
                    }
                    else
                    {
                        RealCityEconomyExtension.lumberStillNeeded -= MainDataStore.building_buffer4[buildingID];
                        MainDataStore.building_buffer4[buildingID] = 0;
                    }
                }
            }
            MainDataStore.allLumbers += MainDataStore.building_buffer4[buildingID];
        }

        protected void CalculateGuestVehicles(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int count, ref int cargo, ref int capacity, ref int outside)
        {
            VehicleManager instance = Singleton<VehicleManager>.instance;
            ushort num = data.m_guestVehicles;
            int num2 = 0;
            while (num != 0)
            {
                if ((TransferManager.TransferReason)instance.m_vehicles.m_buffer[(int)num].m_transferType == material)
                {
                    VehicleInfo info = instance.m_vehicles.m_buffer[(int)num].Info;
                    int a;
                    int num3;
                    info.m_vehicleAI.GetSize(num, ref instance.m_vehicles.m_buffer[(int)num], out a, out num3);
                    cargo += Mathf.Min(a, num3);
                    capacity += num3;
                    count++;
                    if ((instance.m_vehicles.m_buffer[(int)num].m_flags & (Vehicle.Flags.Importing | Vehicle.Flags.Exporting)) != (Vehicle.Flags)0)
                    {
                        outside++;
                    }
                }
                num = instance.m_vehicles.m_buffer[(int)num].m_nextGuestVehicle;
                if (++num2 > 16384)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }
        }
    }
}
