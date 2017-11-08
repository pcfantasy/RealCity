using System;
using ColossalFramework;


namespace RealCity
{
    public class pc_HumanAI : HumanAI
    {
        protected virtual void ArriveAtDestination_1(ushort instanceID, ref CitizenInstance citizenData, bool success)
        {
            uint citizen = citizenData.m_citizen;
            if (citizen != 0u)
            {
                CitizenManager instance = Singleton<CitizenManager>.instance;
                instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].SetVehicle(citizen, 0, 0u);
                if (success)
                {
                    instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].SetLocationByBuilding(citizen, citizenData.m_targetBuilding);
                }
                if (citizenData.m_targetBuilding != 0 && instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].CurrentLocation == Citizen.Location.Visit)
                {
                    BuildingManager instance2 = Singleton<BuildingManager>.instance;
                    BuildingInfo info = instance2.m_buildings.m_buffer[(int)citizenData.m_targetBuilding].Info;
                    //int num = -100;
                    process_tourism_income(instanceID,citizenData);
                    //info.m_buildingAI.ModifyMaterialBuffer(citizenData.m_targetBuilding, ref instance2.m_buildings.m_buffer[(int)citizenData.m_targetBuilding], TransferManager.TransferReason.Shopping, ref num);
                    if (info.m_class.m_service == ItemClass.Service.Beautification)
                    {
                        StatisticsManager instance3 = Singleton<StatisticsManager>.instance;
                        StatisticBase statisticBase = instance3.Acquire<StatisticInt32>(StatisticType.ParkVisitCount);
                        statisticBase.Add(1);
                    }
                    ushort eventIndex = instance2.m_buildings.m_buffer[(int)citizenData.m_targetBuilding].m_eventIndex;
                    if (eventIndex != 0)
                    {
                        EventManager instance4 = Singleton<EventManager>.instance;
                        EventInfo info2 = instance4.m_events.m_buffer[(int)eventIndex].Info;
                        info2.m_eventAI.VisitorEnter(eventIndex, ref instance4.m_events.m_buffer[(int)eventIndex], citizenData.m_targetBuilding, citizen);
                    }
                }
            }
            if ((citizenData.m_flags & CitizenInstance.Flags.HangAround) == CitizenInstance.Flags.None || !success)
            {
                this.SetSource(instanceID, ref citizenData, 0);
                this.SetTarget(instanceID, ref citizenData, 0);
                citizenData.Unspawn(instanceID);
            }
        }

        public void process_tourism_income(ushort instanceID, CitizenInstance citizenData)
        {
            System.Random rand = new System.Random();
            BuildingManager instance2 = Singleton<BuildingManager>.instance;
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint citizen = citizenData.m_citizen;
            BuildingInfo info = instance2.m_buildings.m_buffer[(int)citizenData.m_targetBuilding].Info;
            ushort homeBuilding = instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_homeBuilding;
            uint homeid = instance.m_citizens.m_buffer[citizenData.m_citizen].GetContainingUnit(citizen, instance2.m_buildings.m_buffer[(int)homeBuilding].m_citizenUnits, CitizenUnit.Flags.Home);

            int num = 100;
            TransferManager.TransferReason temp_transfer_reason = TransferManager.TransferReason.None;

            switch (instance2.m_buildings.m_buffer[(int)citizenData.m_targetBuilding].Info.m_class.m_subService)
            {
                case ItemClass.SubService.CommercialLow:
                    if(rand.Next(100) < 2)
                    {
                        temp_transfer_reason = TransferManager.TransferReason.Entertainment;
                    } else
                    {
                        temp_transfer_reason = TransferManager.TransferReason.Shopping;
                    }
                    break;
                case ItemClass.SubService.CommercialHigh:
                    if (rand.Next(100) < 4)
                    {
                        temp_transfer_reason = TransferManager.TransferReason.Entertainment;
                    }
                    else
                    {
                        temp_transfer_reason = TransferManager.TransferReason.Shopping;
                    }
                    break;
                case ItemClass.SubService.CommercialLeisure:
                    if (rand.Next(100) < 20)
                    {
                        temp_transfer_reason = TransferManager.TransferReason.Entertainment;
                    }
                    else
                    {
                        temp_transfer_reason = TransferManager.TransferReason.Shopping;
                    }
                    break;
                case ItemClass.SubService.CommercialTourist:
                    if (rand.Next(100) < 80)
                    {
                        temp_transfer_reason = TransferManager.TransferReason.Entertainment;
                    }
                    else
                    {
                        temp_transfer_reason = TransferManager.TransferReason.Shopping;
                    }
                    break;
                default: temp_transfer_reason = TransferManager.TransferReason.Shopping; break;
            }



            if ((comm_data.citizen_money[homeid] > 0) && ((instance.m_citizens.m_buffer[citizenData.m_citizen].m_flags & Citizen.Flags.Tourist) == Citizen.Flags.None))
            {
                num = (comm_data.citizen_money[homeid] - num > 0f) ? num : (int)comm_data.citizen_money[homeid];
                int num1 = -num;
                info.m_buildingAI.ModifyMaterialBuffer(citizenData.m_targetBuilding, ref instance2.m_buildings.m_buffer[(int)citizenData.m_targetBuilding], TransferManager.TransferReason.Entertainment, ref num1);
                comm_data.citizen_money[homeid] = (short)(comm_data.citizen_money[homeid] + num1);
            }
            else if ((instance.m_citizens.m_buffer[citizenData.m_citizen].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None)
            {
                num = -100;
                info.m_buildingAI.ModifyMaterialBuffer(citizenData.m_targetBuilding, ref instance2.m_buildings.m_buffer[(int)citizenData.m_targetBuilding], temp_transfer_reason, ref num);
            }

            if (info.m_class.m_service == ItemClass.Service.Beautification || info.m_class.m_service == ItemClass.Service.Monument)
            {
                int size = instance2.m_buildings.m_buffer[(int)citizenData.m_targetBuilding].Width * instance2.m_buildings.m_buffer[(int)citizenData.m_targetBuilding].Length;
                int tourism_fee = size * 5;
                if ((instance.m_citizens.m_buffer[citizenData.m_citizen].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None)
                {
                    //DebugLog.LogToFileOnly("tourist visit! " + instance2.m_buildings.m_buffer[(int)citizenData.m_targetBuilding].Width.ToString());
                    tourism_fee = (int)(tourism_fee * comm_data.outside_consumption_rate);
                    Singleton<EconomyManager>.instance.AddPrivateIncome(tourism_fee, ItemClass.Service.Commercial, ItemClass.SubService.CommercialTourist, ItemClass.Level.Level1, 113);
                }
                else
                {
                    if (comm_data.citizen_money[homeid] > 0)
                    {
                        //tourism_fee = (int)(tourism_fee * comm_data.resident_consumption_rate);
                        tourism_fee = (comm_data.citizen_money[homeid] - tourism_fee > 0f) ? tourism_fee : (int)comm_data.citizen_money[homeid];
                        comm_data.citizen_money[homeid] = (short)(comm_data.citizen_money[homeid] - tourism_fee);
                        Singleton<EconomyManager>.instance.AddPrivateIncome(tourism_fee, ItemClass.Service.Commercial, ItemClass.SubService.CommercialTourist, ItemClass.Level.Level1, 114);
                    }
                }
                //DebugLog.LogToFileOnly("find a Beautification building width " + instance2.m_buildings.m_buffer[(int)citizenData.m_targetBuilding].Width.ToString());
            }
        }

        protected virtual bool EnterVehicle_1(ushort instanceID, ref CitizenInstance citizenData)
        {
            citizenData.m_flags &= ~CitizenInstance.Flags.EnteringVehicle;
            citizenData.Unspawn(instanceID);
            uint citizen = citizenData.m_citizen;
            if (citizen != 0u)
            {
                VehicleManager instance = Singleton<VehicleManager>.instance;
                ushort num = Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_vehicle;
                if (num != 0)
                {
                    num = instance.m_vehicles.m_buffer[(int)num].GetFirstVehicle(num);
                }
                if (num != 0)
                {
                    VehicleInfo info = instance.m_vehicles.m_buffer[(int)num].Info;
                    int ticketPrice = info.m_vehicleAI.GetTicketPrice(num, ref instance.m_vehicles.m_buffer[(int)num]);
                    if (ticketPrice != 0)
                    {
                        //DebugLog.LogToFileOnly("EnterVehicle_1 ticketPrice pre = " + ticketPrice.ToString());
                        CitizenManager instance3 = Singleton<CitizenManager>.instance;
                        ushort homeBuilding = instance3.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_homeBuilding;
                        BuildingManager instance2 = Singleton<BuildingManager>.instance;
                        uint homeid = instance3.m_citizens.m_buffer[citizenData.m_citizen].GetContainingUnit(citizen, instance2.m_buildings.m_buffer[(int)homeBuilding].m_citizenUnits, CitizenUnit.Flags.Home);
                        if ((Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenData.m_citizen].m_flags & Citizen.Flags.Tourist) == Citizen.Flags.None)
                        {
                            if ((comm_data.citizen_money[homeid] - ticketPrice) > 0)
                            {
                                comm_data.citizen_money[homeid] = (short)(comm_data.citizen_money[homeid] - ticketPrice);
                            }
                            else
                            {
                                ticketPrice = 0;
                            }
                        }
                        //DebugLog.LogToFileOnly("ticketPrice post = " + ticketPrice.ToString() + "citizen money = " + comm_data.citizen_money[homeid].ToString());
                        Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, ticketPrice * comm_data.game_income_outcome_multiple, info.m_class);
                    }
                }
            }
            return false;
        }
    }
}
