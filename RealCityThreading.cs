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
                    else
                    {
                        ProcessZeroWorker((ushort)i, ref instance.m_buildings.m_buffer[i]);
                    }
                }
            }
        }


        public void ProcessZeroWorker(ushort buildingID, ref Building data)
        {
            if (ItemClass.GetPublicServiceIndex(data.Info.m_class.m_service) != -1 && !data.m_flags.IsFlagSet(Building.Flags.Deleted) && data.m_flags.IsFlagSet(Building.Flags.Created) && data.m_flags.IsFlagSet(Building.Flags.Completed))
            {
                int aliveWorkCount = 0;
                int totalWorkCount = 0;
                Citizen.BehaviourData behaviour = default(Citizen.BehaviourData);
                BuildingUI.GetWorkBehaviour(buildingID, ref data, ref behaviour, ref aliveWorkCount, ref totalWorkCount);
                System.Random rand = new System.Random();
                int allWorkCount = 0;
                if (rand.Next(100) == 0)
                {
                    allWorkCount = RealCityResidentAI.TotalWorkCount(buildingID, data, true, true);
                }
                else
                {
                    allWorkCount = RealCityResidentAI.TotalWorkCount(buildingID, data, true, false);
                }

                if (totalWorkCount == 0 && allWorkCount != 0)
                {
                    int num1 = (MainDataStore.goverment_education3 / 16) * allWorkCount;
                    //DebugLog.LogToFileOnly("totalWorkCount = " + totalWorkCount.ToString() + "allWorkCount =" + allWorkCount.ToString());
                    //DebugLog.LogToFileOnly("find zero worker building = " + data.Info.m_buildingAI.ToString() + data.m_flags.ToString());
                    Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)(num1 * MainDataStore.game_expense_divide), data.Info.m_class);
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
                        data.m_garbageBuffer = (ushort)(data.m_garbageBuffer + Politics.garbageCount * 8);
                    }
                    else
                    {
                        data.m_garbageBuffer = (ushort)(data.m_garbageBuffer + Politics.garbageCount * 4);
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

            if (RealCityOutsideConnectionAI.haveGarbageBuildingFinal && Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.Garbage))
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
                        if (RealCityEconomyExtension.IsSpecialBuilding((ushort)i) == true)
                        {
                            MainDataStore.haveCityResourceDepartment = true;
                        }
                        else if (instance.m_buildings.m_buffer[i].Info.m_class.m_service == ItemClass.Service.Garbage)
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


                int num10 = (int)(currentFrameIndex & 4095u);
                int num11 = num10 * 256;
                int num12 = (num10 + 1) * 256 - 1;
                CitizenManager instance2 = Singleton<CitizenManager>.instance;
                for (int i = num11; i <= num12; i++)
                {
                    if ((instance2.m_citizens.m_buffer[i].m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None)
                    {
                        if (!MainDataStore.citizenFlag[i])
                        {
                            MainDataStore.citizenFlag[i] = true;
                            if (instance2.m_citizens.m_buffer[i].WealthLevel == Citizen.Wealth.Low)
                            {
                                MainDataStore.citizen_money[i] = 1000;
                            }
                            else if(instance2.m_citizens.m_buffer[i].WealthLevel == Citizen.Wealth.Medium)
                            {
                                MainDataStore.citizen_money[i] = 3000;
                            }
                            else
                            {
                                MainDataStore.citizen_money[i] = 9000;
                            }
                        }
                        else
                        {
                            if ((instance2.m_citizens.m_buffer[i].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None)
                            {
                                if (MainDataStore.citizen_money[i] < 0)
                                {
                                    //DebugLog.LogToFileOnly("no money now, tourist will leave" + MainDataStore.citizen_money[i].ToString());
                                    FindVisitPlace((uint)i, instance2.m_citizens.m_buffer[i].m_visitBuilding, GetLeavingReason((uint)i, ref instance2.m_citizens.m_buffer[i]));
                                }
                            }
                        }                        
                    }
                }

           }
        }

        protected TransferManager.TransferReason GetLeavingReason(uint citizenID, ref Citizen data)
        {
            switch (data.WealthLevel)
            {
                case Citizen.Wealth.Low:
                    return TransferManager.TransferReason.LeaveCity0;
                case Citizen.Wealth.Medium:
                    return TransferManager.TransferReason.LeaveCity1;
                case Citizen.Wealth.High:
                    return TransferManager.TransferReason.LeaveCity2;
                default:
                    return TransferManager.TransferReason.LeaveCity0;
            }
        }

        protected void FindVisitPlace(uint citizenID, ushort sourceBuilding, TransferManager.TransferReason reason)
        {
            TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
            offer.Priority = Singleton<SimulationManager>.instance.m_randomizer.Int32(8u);
            offer.Citizen = citizenID;
            offer.Position = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)sourceBuilding].m_position;
            offer.Amount = 1;
            offer.Active = true;
            Singleton<TransferManager>.instance.AddIncomingOffer(reason, offer);
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
                            MainDataStore.allVehicles++;
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)50 * MainDataStore.game_expense_divide, vehicle.Info.m_class);
                        }
                        else if (vehicle.Info.m_vehicleAI is GarbageTruckAI || vehicle.Info.m_vehicleAI is FireTruckAI || vehicle.Info.m_vehicleAI is MaintenanceTruckAI)
                        {
                            Building building = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)vehicle.m_sourceBuilding];
                            if (building.m_flags.IsFlagSet(Building.Flags.Untouchable))
                            {
                                if ((TransferManager.TransferReason)vehicle.m_transferType == TransferManager.TransferReason.GarbageMove)
                                {
                                    //comm_data.allVehicles += 1;
                                    //Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)6000, vehicle.Info.m_class);
                                    //Singleton<NaturalResourceManager>.instance.TryDumpResource(NaturalResourceManager.Resource.Pollution, 80, 80, vehicle.GetLastFramePosition(), 5f);
                                    //DebugLog.LogToFileOnly("vehicle.sourcebuilding = " + vehicle.m_sourceBuilding.ToString() + vehicle.m_flags.ToString());
                                }
                            }
                            else
                            {
                                MainDataStore.allVehicles += 2;
                                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)100 * MainDataStore.game_expense_divide, vehicle.Info.m_class);
                            }
                        }
                        else if (vehicle.Info.m_vehicleAI is BusAI || vehicle.Info.m_vehicleAI is AmbulanceAI || vehicle.Info.m_vehicleAI is SnowTruckAI || vehicle.Info.m_vehicleAI is ParkMaintenanceVehicleAI || vehicle.Info.m_vehicleAI is WaterTruckAI || vehicle.Info.m_vehicleAI is PostVanAI)
                        {
                            MainDataStore.allVehicles += 2;
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)100 * MainDataStore.game_expense_divide, vehicle.Info.m_class);
                        }
                        else if (vehicle.Info.m_vehicleAI is SnowTruckAI || vehicle.Info.m_vehicleAI is ParkMaintenanceVehicleAI || vehicle.Info.m_vehicleAI is WaterTruckAI || vehicle.Info.m_vehicleAI is PostVanAI)
                        {
                            MainDataStore.allVehicles += 1;
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)100 * MainDataStore.game_expense_divide, vehicle.Info.m_class);
                        }
                        else if (vehicle.Info.m_vehicleAI is PassengerShipAI || vehicle.Info.m_vehicleAI is PassengerFerryAI)
                        {
                            MainDataStore.allVehicles += 4;
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)500 * MainDataStore.game_expense_divide, vehicle.Info.m_class);
                        }
                        else if (vehicle.Info.m_vehicleAI is CargoShipAI)
                        {
                            MainDataStore.allVehicles += 4;
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)250 * MainDataStore.game_expense_divide, vehicle.Info.m_class);
                        }
                        else if (vehicle.Info.m_vehicleAI is PassengerPlaneAI || vehicle.Info.m_vehicleAI is PassengerBlimpAI)
                        {
                            MainDataStore.allVehicles += 8;
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)800 * MainDataStore.game_expense_divide, vehicle.Info.m_class);
                        }
                        else if (vehicle.Info.m_vehicleAI is CargoPlaneAI)
                        {
                            MainDataStore.allVehicles += 8;
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)400 * MainDataStore.game_expense_divide, vehicle.Info.m_class);
                        }
                        else if (vehicle.Info.m_vehicleAI is PassengerTrainAI)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)130 * MainDataStore.game_expense_divide, vehicle.Info.m_class);
                        }
                        else if (vehicle.Info.m_vehicleAI is CargoTrainAI)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)65 * MainDataStore.game_expense_divide, vehicle.Info.m_class);
                        }
                        else if (vehicle.Info.m_vehicleAI is MetroTrainAI)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)120 * MainDataStore.game_expense_divide, vehicle.Info.m_class);
                        }
                        else if (vehicle.Info.m_vehicleAI is PoliceCopterAI || vehicle.Info.m_vehicleAI is FireCopterAI || vehicle.Info.m_vehicleAI is DisasterResponseCopterAI || vehicle.Info.m_vehicleAI is AmbulanceCopterAI)
                        {
                            MainDataStore.allVehicles += 8;
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)800 * MainDataStore.game_expense_divide, vehicle.Info.m_class);
                        }
                        else if (vehicle.Info.m_vehicleAI is CableCarAI || vehicle.Info.m_vehicleAI is TramAI)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)110 * MainDataStore.game_expense_divide, vehicle.Info.m_class);
                        }
                        else if ((vehicle.Info.m_vehicleType == VehicleInfo.VehicleType.Car) && (vehicle.Info.m_class.m_subService != ItemClass.SubService.PublicTransportTaxi))
                        {
                            if (!vehicle.m_flags.IsFlagSet(Vehicle.Flags.Stopped))
                            {
                                MainDataStore.vehical_transfer_time[i] = (ushort)(MainDataStore.vehical_transfer_time[i] + 16);
                            }
                            else
                            {
                                MainDataStore.vehical_transfer_time[i] = 0;
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
