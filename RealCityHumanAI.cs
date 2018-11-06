using System;
using ColossalFramework;
using ColossalFramework.Math;
using UnityEngine;

namespace RealCity
{
    public class RealCityHumanAI : HumanAI
    {
        public virtual void VisitorEnter(ushort buildingID, ref Building data, uint citizen)
        {
            //DebugLog.LogToFileOnly("VisitorEnter coming");
            ProcessTourismIncome(buildingID, ref data, citizen);
            ushort eventIndex = data.m_eventIndex;
            if (eventIndex != 0)
            {
                EventManager instance = Singleton<EventManager>.instance;
                EventInfo info = instance.m_events.m_buffer[(int)eventIndex].Info;
                info.m_eventAI.VisitorEnter(eventIndex, ref instance.m_events.m_buffer[(int)eventIndex], buildingID, citizen);
            }
        }


        public override void EnterParkArea(ushort instanceID, ref CitizenInstance citizenData, byte park, ushort gateID)
        {
            if (gateID != 0)
            {
                DistrictManager instance = Singleton<DistrictManager>.instance;
                BuildingManager instance2 = Singleton<BuildingManager>.instance;
                CitizenManager instance3 = Singleton<CitizenManager>.instance;
                int ticketPrice = instance.m_parks.m_buffer[(int)park].GetTicketPrice();
                if ((instance3.m_citizens.m_buffer[citizenData.m_citizen].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None)
                {
                    if (ticketPrice != 0)
                    {
                        BuildingInfo info = instance2.m_buildings.m_buffer[(int)gateID].Info;
                        Singleton<EconomyManager>.instance.AddPrivateIncome(ticketPrice, ItemClass.Service.Commercial, ItemClass.SubService.CommercialTourist, ItemClass.Level.Level1, 113);
                        //Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, ticketPrice, info.m_class);
                        DistrictPark[] expr_6C_cp_0 = instance.m_parks.m_buffer;
                        expr_6C_cp_0[(int)park].m_tempTicketIncome = expr_6C_cp_0[(int)park].m_tempTicketIncome + (uint)ticketPrice;
                    }
                }
            }
        }


        public void ProcessTourismIncome(ushort buildingID, ref Building data, uint citizen)
        {
            BuildingManager instance2 = Singleton<BuildingManager>.instance;
            CitizenManager instance = Singleton<CitizenManager>.instance;
            //uint citizen = citizenData.m_citizen;
            BuildingInfo info = data.Info;
            ushort homeBuilding = instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_homeBuilding;
            uint homeId = instance.m_citizens.m_buffer[citizen].GetContainingUnit(citizen, instance2.m_buildings.m_buffer[(int)homeBuilding].m_citizenUnits, CitizenUnit.Flags.Home);

            TransferManager.TransferReason tempTransferRreason = TransferManager.TransferReason.Entertainment;
            System.Random rand = new System.Random();
            int num = 0;
            if ((instance.m_citizens.m_buffer[citizen].m_flags & Citizen.Flags.Tourist) == Citizen.Flags.None)
            {
                if (tempTransferRreason == TransferManager.TransferReason.Entertainment)
                {
                    if ((info.m_class.m_subService == ItemClass.SubService.CommercialLeisure))
                    {
                        num = rand.Next(800) + 1;
                    }
                    else
                    {
                        num = rand.Next(100) + 1;
                    }
                }

                if (MainDataStore.family_money[homeId] < 5000)
                {
                    
                }
                else if (MainDataStore.family_money[homeId] >= 80000)
                {
                    num = (num << 4);
                }
                else if (MainDataStore.family_money[homeId] >= 40000)
                {
                    num = (num << 3);
                }
                else if (MainDataStore.family_money[homeId] >= 20000)
                {
                    num = (num << 2);
                } 
                else
                {
                    num = (num << 1);
                }


                int num1 = -num;
                if(num1 == -50)   //for rush hour
                {
                    num1 = num1 + 1;
                }
                if (num != 0)
                {
                    info.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, tempTransferRreason, ref num1);
                    num = -100;
                    info.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, TransferManager.TransferReason.Shopping, ref num);
                    MainDataStore.family_money[homeId] = (float)(MainDataStore.family_money[homeId] + num1 + num * RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Shopping));
                }
            }
            else if ((instance.m_citizens.m_buffer[citizen].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None)
            {
                if (tempTransferRreason == TransferManager.TransferReason.Entertainment)
                {
                    num = rand.Next(400);
                    if (instance.m_citizens.m_buffer[citizen].WealthLevel == Citizen.Wealth.High)
                    {
                        num = num * 8;
                    }
                    if (instance.m_citizens.m_buffer[citizen].WealthLevel == Citizen.Wealth.Medium)
                    {
                        num = num * 3;
                    }
                }

                num = -(int)(num);
                if ((num == -200 || num == -50))
                {
                    num = num + 1;
                }
                info.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, tempTransferRreason, ref num);
                num = -100;
                info.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, TransferManager.TransferReason.Shopping, ref num);
            }

            if (info.m_class.m_service == ItemClass.Service.Beautification || info.m_class.m_service == ItemClass.Service.Monument)
            {
                if ((instance.m_citizens.m_buffer[citizen].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None)
                {
                    int tourism_fee = rand.Next(400);
                    if (instance.m_citizens.m_buffer[citizen].WealthLevel == Citizen.Wealth.High)
                    {
                        tourism_fee = tourism_fee * 8;
                    }
                    if (instance.m_citizens.m_buffer[citizen].WealthLevel == Citizen.Wealth.Medium)
                    {
                        tourism_fee = tourism_fee * 3;
                    }
                    Singleton<EconomyManager>.instance.AddPrivateIncome(tourism_fee, ItemClass.Service.Commercial, ItemClass.SubService.CommercialTourist, ItemClass.Level.Level1, 113);
                }
                else
                {
                    int tourism_fee = rand.Next(100) + 1;
                    if (MainDataStore.family_money[homeId] < 5000)
                    {

                    }
                    else if (MainDataStore.family_money[homeId] >= 80000)
                    {
                        tourism_fee = (tourism_fee << 4);
                    }
                    else if (MainDataStore.family_money[homeId] >= 40000)
                    {
                        tourism_fee = (tourism_fee << 3);
                    }
                    else if (MainDataStore.family_money[homeId] >= 20000)
                    {
                        tourism_fee = (tourism_fee << 2);
                    }
                    else
                    {
                        tourism_fee = (tourism_fee << 1);
                    }
                    if (tourism_fee != 0)
                    {
                        MainDataStore.family_money[homeId] = (float)(MainDataStore.family_money[homeId] - tourism_fee);
                        Singleton<EconomyManager>.instance.AddPrivateIncome(tourism_fee, ItemClass.Service.Commercial, ItemClass.SubService.CommercialTourist, ItemClass.Level.Level1, 114);
                    }
                }
            }
        }

        protected virtual bool CustomEnterVehicle(ushort instanceID, ref CitizenInstance citizenData)
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
                        //new added begin
                        ticketPrice = (int)((float)ticketPrice);
                        CitizenManager instance3 = Singleton<CitizenManager>.instance;
                        ushort homeBuilding = instance3.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_homeBuilding;
                        BuildingManager instance2 = Singleton<BuildingManager>.instance;
                        uint homeId = instance3.m_citizens.m_buffer[citizenData.m_citizen].GetContainingUnit(citizen, instance2.m_buildings.m_buffer[(int)homeBuilding].m_citizenUnits, CitizenUnit.Flags.Home);
                        if ((Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenData.m_citizen].m_flags & Citizen.Flags.Tourist) == Citizen.Flags.None)
                        {
                            if ((MainDataStore.family_money[homeId] - (ticketPrice)) > 0)
                            {
                                MainDataStore.family_money[homeId] = (float)(MainDataStore.family_money[homeId] - (ticketPrice));
                            }
                            else
                            {
                                ticketPrice = 0;
                            }
                        } else
                        {
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
                        //new added end
                        Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, ticketPrice, info.m_class);
                    }
                }
            }
            return false;
        }


        public override void SimulationStep(ushort instanceID, ref CitizenInstance citizenData, ref CitizenInstance.Frame frameData, bool lodPhysics)
        {
            uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
            if ((ulong)(currentFrameIndex >> 4 & 63u) == (ulong)((long)(instanceID & 63)))
            {
                CitizenManager instance = Singleton<CitizenManager>.instance;
                uint citizen = citizenData.m_citizen;
                if (citizen != 0u && (instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_flags & Citizen.Flags.NeedGoods) != Citizen.Flags.None)
                {
                    BuildingManager instance2 = Singleton<BuildingManager>.instance;
                    ushort homeBuilding = instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_homeBuilding;
                    ushort num = 0;

                    //new added begin
                    num = RealCityResidentAI.FindNotSoCloseBuilding(frameData.m_position, 128f, ItemClass.Service.Commercial, ItemClass.SubService.None, Building.Flags.Created, Building.Flags.Deleted | Building.Flags.Abandoned);
                    //new added end
                    if (homeBuilding != 0 && num != 0)
                    {
                        BuildingInfo info = instance2.m_buildings.m_buffer[(int)num].Info;
                        int num2 = -100;
                        info.m_buildingAI.ModifyMaterialBuffer(num, ref instance2.m_buildings.m_buffer[(int)num], TransferManager.TransferReason.Shopping, ref num2);
                        uint containingUnit1 = instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].GetContainingUnit(citizen, instance2.m_buildings.m_buffer[(int)homeBuilding].m_citizenUnits, CitizenUnit.Flags.Home);
                        if (containingUnit1 != 0u)
                        {
                            CitizenUnit[] expr_127_cp_0 = instance.m_units.m_buffer;
                            UIntPtr expr_127_cp_1 = (UIntPtr)containingUnit1;
                            expr_127_cp_0[(int)expr_127_cp_1].m_goods = (ushort)(expr_127_cp_0[(int)expr_127_cp_1].m_goods + (ushort)(-(ushort)num2));
                        }
                        Citizen[] expr_14A_cp_0 = instance.m_citizens.m_buffer;
                        UIntPtr expr_14A_cp_1 = (UIntPtr)citizen;
                        if (instance.m_units.m_buffer[containingUnit1].m_goods > 200)
                        {
                            expr_14A_cp_0[(int)expr_14A_cp_1].m_flags = (expr_14A_cp_0[(int)expr_14A_cp_1].m_flags & ~Citizen.Flags.NeedGoods);
                        }
                    }
                }
            }
            base.SimulationStep(instanceID, ref citizenData, ref frameData, lodPhysics);
        }
    }
}
