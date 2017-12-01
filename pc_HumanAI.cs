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


        public static TransferManager.TransferReason get_shopping_reason(ushort buildingID)
        {
            BuildingManager instance2 = Singleton<BuildingManager>.instance;
            TransferManager.TransferReason temp_transfer_reason = TransferManager.TransferReason.None;
            System.Random rand = new System.Random();
            int aliveWorkCount = 0;
            int totalWorkCount = 0;
            Citizen.BehaviourData behaviour = default(Citizen.BehaviourData);
            BuildingUI.GetWorkBehaviour(buildingID, ref instance2.m_buildings.m_buffer[(int)buildingID], ref behaviour, ref aliveWorkCount, ref totalWorkCount);

            if (aliveWorkCount > 40)
            {
                aliveWorkCount = 40;
            }
            switch (instance2.m_buildings.m_buffer[(int)buildingID].Info.m_class.m_subService)
            {
                case ItemClass.SubService.CommercialLow:
                    if (rand.Next(100) < (aliveWorkCount))
                    {
                        temp_transfer_reason = TransferManager.TransferReason.Entertainment;
                    }
                    else
                    {
                        temp_transfer_reason = TransferManager.TransferReason.Shopping;
                    }
                    break;
                case ItemClass.SubService.CommercialHigh:
                    if (rand.Next(100) < (aliveWorkCount * 2))
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
            return temp_transfer_reason;
        }

        public void process_tourism_income(ushort instanceID, CitizenInstance citizenData)
        {
            BuildingManager instance2 = Singleton<BuildingManager>.instance;
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint citizen = citizenData.m_citizen;
            BuildingInfo info = instance2.m_buildings.m_buffer[(int)citizenData.m_targetBuilding].Info;
            ushort homeBuilding = instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_homeBuilding;
            uint homeid = instance.m_citizens.m_buffer[citizenData.m_citizen].GetContainingUnit(citizen, instance2.m_buildings.m_buffer[(int)homeBuilding].m_citizenUnits, CitizenUnit.Flags.Home);

            int num = 100;


            TransferManager.TransferReason temp_transfer_reason = get_shopping_reason(citizenData.m_targetBuilding);
            System.Random rand = new System.Random();

            if ((instance.m_citizens.m_buffer[citizenData.m_citizen].m_flags & Citizen.Flags.Tourist) == Citizen.Flags.None)
            {
                if (temp_transfer_reason == TransferManager.TransferReason.Entertainment)
                {
                    if (info.m_class.m_subService == ItemClass.SubService.CommercialLeisure)
                    {
                        num = rand.Next(900) + 100;
                    } else
                    {
                        num = rand.Next(400) + 100;
                    }
                }

                int temp = (comm_data.citizen_money[homeid] - num > 1f) ? (int)(comm_data.citizen_money[homeid] - num) : 1;
                num = (rand.Next(temp) > 1000) ? num : (int)(0.05f * comm_data.citizen_money[homeid]);

                if(num < 0)
                {
                    num = 0;
                }

                int num1 = -num;
                if((num1 == -200 || num1 == -50))
                {
                    num1 = num1 + 1;
                }
                if (num != 0)
                {
                    info.m_buildingAI.ModifyMaterialBuffer(citizenData.m_targetBuilding, ref instance2.m_buildings.m_buffer[(int)citizenData.m_targetBuilding], temp_transfer_reason, ref num1);
                    comm_data.citizen_money[homeid] = (short)(comm_data.citizen_money[homeid] + num1);
                }
            }
            else if ((instance.m_citizens.m_buffer[citizenData.m_citizen].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None)
            {
                if (temp_transfer_reason == TransferManager.TransferReason.Entertainment)
                {
                    num = 1000;
                    if (instance.m_citizens.m_buffer[citizenData.m_citizen].WealthLevel == Citizen.Wealth.High)
                    {
                        num = num * 4;
                    }
                    if (instance.m_citizens.m_buffer[citizenData.m_citizen].WealthLevel == Citizen.Wealth.Medium)
                    {
                        num = num * 2;
                    }
                }

                num = -num;
                if ((num == -200 || num == -50))
                {
                    num = num + 1;
                }
                info.m_buildingAI.ModifyMaterialBuffer(citizenData.m_targetBuilding, ref instance2.m_buildings.m_buffer[(int)citizenData.m_targetBuilding], temp_transfer_reason, ref num);
            }

            if (info.m_class.m_service == ItemClass.Service.Beautification || info.m_class.m_service == ItemClass.Service.Monument)
            {
                int size = instance2.m_buildings.m_buffer[(int)citizenData.m_targetBuilding].Width * instance2.m_buildings.m_buffer[(int)citizenData.m_targetBuilding].Length;
                int tourism_fee = rand.Next(size * 200);
                if ((instance.m_citizens.m_buffer[citizenData.m_citizen].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None)
                {
                    //DebugLog.LogToFileOnly("tourist visit! " + instance2.m_buildings.m_buffer[(int)citizenData.m_targetBuilding].Width.ToString());
                    if (instance.m_citizens.m_buffer[citizenData.m_citizen].WealthLevel == Citizen.Wealth.High)
                    {
                        tourism_fee = tourism_fee * 4;
                    }
                    if (instance.m_citizens.m_buffer[citizenData.m_citizen].WealthLevel == Citizen.Wealth.Medium)
                    {
                        tourism_fee = tourism_fee * 2;
                    }
                    tourism_fee = (int)(tourism_fee * comm_data.outside_consumption_rate);
                    Singleton<EconomyManager>.instance.AddPrivateIncome(tourism_fee, ItemClass.Service.Commercial, ItemClass.SubService.CommercialTourist, ItemClass.Level.Level1, 113);
                }
                else
                {
                    //tourism_fee = (int)(tourism_fee * comm_data.resident_consumption_rate);
                    int temp = (comm_data.citizen_money[homeid] - tourism_fee > 1f) ? (int)(comm_data.citizen_money[homeid] - tourism_fee) : 1;
                    tourism_fee = (rand.Next(temp) > 1000) ? tourism_fee : (int)(0.05f * comm_data.citizen_money[homeid]);

                    if (tourism_fee < 0)
                    {
                        tourism_fee = 0;
                    }

                    if (tourism_fee != 0)
                    {
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
                        ticketPrice = (int)((float)ticketPrice);
                        //DebugLog.LogToFileOnly("EnterVehicle_1 ticketPrice pre = " + ticketPrice.ToString());
                        CitizenManager instance3 = Singleton<CitizenManager>.instance;
                        ushort homeBuilding = instance3.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_homeBuilding;
                        BuildingManager instance2 = Singleton<BuildingManager>.instance;
                        uint homeid = instance3.m_citizens.m_buffer[citizenData.m_citizen].GetContainingUnit(citizen, instance2.m_buildings.m_buffer[(int)homeBuilding].m_citizenUnits, CitizenUnit.Flags.Home);
                        if ((Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenData.m_citizen].m_flags & Citizen.Flags.Tourist) == Citizen.Flags.None)
                        {
                            if ((comm_data.citizen_money[homeid] - (ticketPrice/comm_data.game_maintain_fee_decrease)) > 0)
                            {
                                comm_data.citizen_money[homeid] = (short)(comm_data.citizen_money[homeid] - (ticketPrice / comm_data.game_maintain_fee_decrease3));
                            }
                            else
                            {
                                ticketPrice = 0;
                            }
                        }
                        //DebugLog.LogToFileOnly("ticketPrice post = " + ticketPrice.ToString() + "citizen money = " + comm_data.citizen_money[homeid].ToString());
                        Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, ticketPrice/ comm_data.game_maintain_fee_decrease3, info.m_class);
                    }
                }
            }
            return false;
        }
    }
}
