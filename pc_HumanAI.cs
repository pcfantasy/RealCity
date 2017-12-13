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


        public static TransferManager.TransferReason get_shopping_reason(ushort buildingID, ref float idex)
        {
            BuildingManager instance2 = Singleton<BuildingManager>.instance;
            TransferManager.TransferReason temp_transfer_reason = TransferManager.TransferReason.None;
            System.Random rand = new System.Random();
            int aliveWorkCount = 0;
            int totalWorkCount = 0;
            Citizen.BehaviourData behaviour = default(Citizen.BehaviourData);
            BuildingUI.GetWorkBehaviour(buildingID, ref instance2.m_buildings.m_buffer[(int)buildingID], ref behaviour, ref aliveWorkCount, ref totalWorkCount);
            idex = 0f;

            switch (instance2.m_buildings.m_buffer[(int)buildingID].Info.m_class.m_subService)
            {
                case ItemClass.SubService.CommercialLow:
                    temp_transfer_reason = TransferManager.TransferReason.Entertainment;
                    idex = rand.Next(10 + aliveWorkCount * 3) / 100f;
                    break;
                case ItemClass.SubService.CommercialHigh:
                    temp_transfer_reason = TransferManager.TransferReason.Entertainment;
                    idex = rand.Next(20 + aliveWorkCount * 4) / 100f;
                    break;
                case ItemClass.SubService.CommercialLeisure:
                    temp_transfer_reason = TransferManager.TransferReason.Entertainment;
                    idex = rand.Next(20 + aliveWorkCount * 5) / 100f;
                    break;
                case ItemClass.SubService.CommercialTourist:
                    temp_transfer_reason = TransferManager.TransferReason.Entertainment;
                    idex = rand.Next(80 + aliveWorkCount * 6) / 100f;
                    break;
                case ItemClass.SubService.CommercialEco:
                    temp_transfer_reason = TransferManager.TransferReason.Entertainment;
                    idex = rand.Next(20 + aliveWorkCount * 3) / 100f;
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

            float idex = 1f;
            int num = 0;


            TransferManager.TransferReason temp_transfer_reason = get_shopping_reason(citizenData.m_targetBuilding, ref idex);
            System.Random rand = new System.Random();

            if ((instance.m_citizens.m_buffer[citizenData.m_citizen].m_flags & Citizen.Flags.Tourist) == Citizen.Flags.None)
            {
                if (temp_transfer_reason == TransferManager.TransferReason.Entertainment)
                {
                    if ((info.m_class.m_subService == ItemClass.SubService.CommercialLeisure) || (info.m_class.m_subService == ItemClass.SubService.CommercialTourist))
                    {
                        num = (comm_data.citizen_money[homeid] > 2000f) ? (int)(0.2f * comm_data.citizen_money[homeid]) : 0;
                        num = (int)(num * idex);
                        if (num > 0.25f * comm_data.citizen_money[homeid])
                        {
                            num = (int)(0.25f * comm_data.citizen_money[homeid]);
                        }
                    } else
                    {
                        num = (comm_data.citizen_money[homeid] > 1000f) ? (int)(0.1f * comm_data.citizen_money[homeid]) : 0;
                        num = (int)(num * idex);
                        if (num > 0.15f * comm_data.citizen_money[homeid])
                        {
                            num = (int)(0.15f * comm_data.citizen_money[homeid]);
                        }
                    }
                }

                num = (rand.Next(3) > 1) ? (int)(0.2f * comm_data.citizen_money[homeid]) : num;

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
                    num = rand.Next(1000);
                    if (instance.m_citizens.m_buffer[citizenData.m_citizen].WealthLevel == Citizen.Wealth.High)
                    {
                        num = num * 4;
                    }
                    if (instance.m_citizens.m_buffer[citizenData.m_citizen].WealthLevel == Citizen.Wealth.Medium)
                    {
                        num = num * 2;
                    }
                }

                num = -(int)(num * idex);
                if ((num == -200 || num == -50))
                {
                    num = num + 1;
                }
                info.m_buildingAI.ModifyMaterialBuffer(citizenData.m_targetBuilding, ref instance2.m_buildings.m_buffer[(int)citizenData.m_targetBuilding], temp_transfer_reason, ref num);
                num = -100;
                info.m_buildingAI.ModifyMaterialBuffer(citizenData.m_targetBuilding, ref instance2.m_buildings.m_buffer[(int)citizenData.m_targetBuilding], TransferManager.TransferReason.Shopping, ref num);
            }

            if (info.m_class.m_service == ItemClass.Service.Beautification || info.m_class.m_service == ItemClass.Service.Monument)
            {
                int budget = Singleton<EconomyManager>.instance.GetBudget(instance2.m_buildings.m_buffer[citizenData.m_targetBuilding].Info.m_class);
                int result = (int)(instance2.m_buildings.m_buffer[citizenData.m_targetBuilding].Info.m_buildingAI.GetMaintenanceCost() / 2.5f);
                result = (int)(result *(float)(budget * (float)(instance2.m_buildings.m_buffer[citizenData.m_targetBuilding].m_productionRate / 1000f)));

                if(result > 1000)
                {
                   result = 1000 + (int)((result-1000)/100f);
                }

                int tourism_fee = rand.Next(result);

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
                    comm_data.building_money[citizenData.m_targetBuilding] += tourism_fee /100f;
                    Singleton<EconomyManager>.instance.AddPrivateIncome(tourism_fee, ItemClass.Service.Commercial, ItemClass.SubService.CommercialTourist, ItemClass.Level.Level1, 113);
                }
                else
                {
                    //tourism_fee = (int)(tourism_fee * comm_data.resident_consumption_rate);
                    int temp = (comm_data.citizen_money[homeid]> 1f) ? (int)(comm_data.citizen_money[homeid]) : 1;
                    tourism_fee = (rand.Next(temp) > 5000) ? (int)(0.2f * comm_data.citizen_money[homeid]) : (int)(0.1f * comm_data.citizen_money[homeid]);

                    if (tourism_fee < 0)
                    {
                        tourism_fee = 0;
                    }

                    if (tourism_fee != 0)
                    {
                        comm_data.citizen_money[homeid] = (short)(comm_data.citizen_money[homeid] - tourism_fee);
                        comm_data.building_money[citizenData.m_targetBuilding] += tourism_fee / 100f;
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

                        //if (instance.m_vehicles.m_buffer[(int)num].Info.m_vehicleType == VehicleInfo.VehicleType.Train)
                        //{
                        //    DebugLog.LogToFileOnly("train price before is " + ticketPrice.ToString());
                        //}

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
                        } else
                        {
                            comm_data.tourist_transport_fee_num += ticketPrice;
                            comm_data.tourist_num++;
                            if (comm_data.tourist_transport_fee_num > 1000000000000000000)
                            {
                                comm_data.tourist_transport_fee_num = 1000000000000000000;
                            }
                            if (instance.m_vehicles.m_buffer[(int)num].Info.m_vehicleType == VehicleInfo.VehicleType.Metro)
                            {
                                if (ticketPrice > 500)
                                {
                                    ticketPrice = 500;
                                }
                            }
                            else if (instance.m_vehicles.m_buffer[(int)num].Info.m_vehicleType == VehicleInfo.VehicleType.Car)
                            {
                                if (ticketPrice > 200)
                                {
                                    ticketPrice = 200;
                                }

                            }
                            else if (instance.m_vehicles.m_buffer[(int)num].Info.m_vehicleType == VehicleInfo.VehicleType.Plane)
                            {
                                if (ticketPrice > 1500)
                                {
                                    ticketPrice = 1500;
                                }
                            }
                            else if (instance.m_vehicles.m_buffer[(int)num].Info.m_vehicleType == VehicleInfo.VehicleType.Train)
                            {
                                if (ticketPrice > 600)
                                {
                                    ticketPrice = 600;
                                }
                            }
                            else if (instance.m_vehicles.m_buffer[(int)num].Info.m_vehicleType == VehicleInfo.VehicleType.Tram)
                            {
                                if (ticketPrice > 300)
                                {
                                    ticketPrice = 300;
                                }
                            }
                            else if (instance.m_vehicles.m_buffer[(int)num].Info.m_vehicleType == VehicleInfo.VehicleType.Ship)
                            {
                                if (ticketPrice > 800)
                                {
                                    ticketPrice = 800;
                                }
                            }
                            else if (instance.m_vehicles.m_buffer[(int)num].Info.m_vehicleType == VehicleInfo.VehicleType.Ferry)
                            {
                                if (ticketPrice > 300)
                                {
                                    ticketPrice = 300;
                                }
                            }
                            else if (instance.m_vehicles.m_buffer[(int)num].Info.m_vehicleType == VehicleInfo.VehicleType.CableCar)
                            {
                                if (ticketPrice > 250)
                                {
                                    ticketPrice = 250;
                                }
                            }
                            else if (instance.m_vehicles.m_buffer[(int)num].Info.m_vehicleType == VehicleInfo.VehicleType.Monorail)
                            {
                                if (ticketPrice > 400)
                                {
                                    ticketPrice = 400;
                                }
                            }
                            else
                            {
                                if (ticketPrice > 1000)
                                {
                                    ticketPrice = 1000;
                                }
                            }
                        }

                        //if (instance.m_vehicles.m_buffer[(int)num].Info.m_vehicleType == VehicleInfo.VehicleType.Train)
                        //{
                            //DebugLog.LogToFileOnly("train price after is " + ticketPrice.ToString());
                        //}
                        //DebugLog.LogToFileOnly("ticketPrice post = " + ticketPrice.ToString() + "citizen money = " + comm_data.citizen_money[homeid].ToString());
                        Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, ticketPrice/ comm_data.game_maintain_fee_decrease3, info.m_class);
                    }
                }
            }
            return false;
        }
    }
}
