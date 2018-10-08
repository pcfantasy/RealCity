using System;
using ColossalFramework;
using ColossalFramework.Math;
using UnityEngine;

namespace RealCity
{
    public class pc_HumanAI : HumanAI
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
                    if ((info.m_class.m_subService == ItemClass.SubService.CommercialLeisure) || (info.m_class.m_subService == ItemClass.SubService.CommercialTourist))
                    {
                        num = rand.Next(100) + 1;
                    }
                    else if (info.m_class.m_subService == ItemClass.SubService.CommercialHigh)
                    {
                        num = rand.Next(50) + 1;
                    }
                    else
                    {
                        num = rand.Next(25) + 1;
                    }
                }
                int num1 = -num;
                if(num1 == -50)   //for rush hour
                {
                    num1 = num1 + 1;
                }
                if (num != 0)
                {
                    info.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, tempTransferRreason, ref num1);
                    comm_data.family_money[homeId] = (float)(comm_data.family_money[homeId] + num1);
                }
            }
            else if ((instance.m_citizens.m_buffer[citizen].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None)
            {
                if (tempTransferRreason == TransferManager.TransferReason.Entertainment)
                {
                    num = rand.Next(200);
                    if (instance.m_citizens.m_buffer[citizen].WealthLevel == Citizen.Wealth.High)
                    {
                        num = num * 4;
                    }
                    if (instance.m_citizens.m_buffer[citizen].WealthLevel == Citizen.Wealth.Medium)
                    {
                        num = num * 2;
                    }
                }

                num = -(int)(num);
                if ((num == -200 || num == -50))
                {
                    num = num + 1;
                }
                info.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, tempTransferRreason, ref num);
            }

            if (info.m_class.m_service == ItemClass.Service.Beautification || info.m_class.m_service == ItemClass.Service.Monument)
            {
                int tourism_fee = rand.Next(500);

                if ((instance.m_citizens.m_buffer[citizen].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None)
                {
                    if (instance.m_citizens.m_buffer[citizen].WealthLevel == Citizen.Wealth.High)
                    {
                        tourism_fee = tourism_fee * 4;
                    }
                    if (instance.m_citizens.m_buffer[citizen].WealthLevel == Citizen.Wealth.Medium)
                    {
                        tourism_fee = tourism_fee * 2;
                    }
                    Singleton<EconomyManager>.instance.AddPrivateIncome(tourism_fee, ItemClass.Service.Commercial, ItemClass.SubService.CommercialTourist, ItemClass.Level.Level1, 113);
                }
                else
                {
                    tourism_fee = rand.Next(100) + 1;
                    if (tourism_fee != 0)
                    {
                        comm_data.family_money[homeId] = (float)(comm_data.family_money[homeId] - tourism_fee);
                        Singleton<EconomyManager>.instance.AddPrivateIncome(tourism_fee, ItemClass.Service.Commercial, ItemClass.SubService.CommercialTourist, ItemClass.Level.Level1, 114);
                    }
                }
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
                        //new added begin
                        ticketPrice = (int)((float)ticketPrice);
                        CitizenManager instance3 = Singleton<CitizenManager>.instance;
                        ushort homeBuilding = instance3.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_homeBuilding;
                        BuildingManager instance2 = Singleton<BuildingManager>.instance;
                        uint homeId = instance3.m_citizens.m_buffer[citizenData.m_citizen].GetContainingUnit(citizen, instance2.m_buildings.m_buffer[(int)homeBuilding].m_citizenUnits, CitizenUnit.Flags.Home);
                        if ((Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenData.m_citizen].m_flags & Citizen.Flags.Tourist) == Citizen.Flags.None)
                        {
                            if ((comm_data.family_money[homeId] - (ticketPrice)) > 0)
                            {
                                comm_data.family_money[homeId] = (float)(comm_data.family_money[homeId] - (ticketPrice));
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
                    num = pc_ResidentAI.FindNotSoCloseBuilding(frameData.m_position, 64f, ItemClass.Service.Commercial, ItemClass.SubService.None, Building.Flags.Created, Building.Flags.Deleted | Building.Flags.Abandoned);
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
                        if (instance.m_units.m_buffer[containingUnit1].m_goods > 20000)
                        {
                            expr_14A_cp_0[(int)expr_14A_cp_1].m_flags = (expr_14A_cp_0[(int)expr_14A_cp_1].m_flags & ~Citizen.Flags.NeedGoods);
                        }
                    }
                }
            }
            base.SimulationStep(instanceID, ref citizenData, ref frameData, lodPhysics);
        }


        public override void StartTransfer(uint citizenID, ref Citizen data, TransferManager.TransferReason reason, TransferManager.TransferOffer offer)
        {
            if (data.m_flags == Citizen.Flags.None || (data.Dead && reason != TransferManager.TransferReason.Dead))
            {
                return;
            }
            switch (reason)
            {
                case TransferManager.TransferReason.Single0B:
                case TransferManager.TransferReason.Single1B:
                case TransferManager.TransferReason.Single2B:
                case TransferManager.TransferReason.Single3B:
                case TransferManager.TransferReason.Single0:
                case TransferManager.TransferReason.Single1:
                case TransferManager.TransferReason.Single2:
                case TransferManager.TransferReason.Single3:
                    uint num2 = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)offer.Building].GetEmptyCitizenUnit(CitizenUnit.Flags.Home);
                    if (num2 != 0)
                    {
                        data.SetHome(citizenID, 0, num2);
                    }
                    if (data.m_homeBuilding == 0 && (data.CurrentLocation != Citizen.Location.Visit || (data.m_flags & Citizen.Flags.Evacuating) == Citizen.Flags.None))
                    {
                        Singleton<CitizenManager>.instance.ReleaseCitizen(citizenID);
                    }
                    break;
                case TransferManager.TransferReason.Family0:
                case TransferManager.TransferReason.Family1:
                case TransferManager.TransferReason.Family2:
                case TransferManager.TransferReason.Family3:
                    if (data.m_homeBuilding != 0 && offer.Building != 0)
                    {
                        uint num = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_homeBuilding].FindCitizenUnit(CitizenUnit.Flags.Home, citizenID);
                        if (num != 0u)
                        {
                            this.MoveFamily(num, ref Singleton<CitizenManager>.instance.m_units.m_buffer[(int)((UIntPtr)num)], offer.Building);
                        }
                    }
                    break;
                case TransferManager.TransferReason.ShoppingB:
                case TransferManager.TransferReason.ShoppingC:
                case TransferManager.TransferReason.ShoppingD:
                case TransferManager.TransferReason.ShoppingE:
                case TransferManager.TransferReason.ShoppingF:
                case TransferManager.TransferReason.ShoppingG:
                case TransferManager.TransferReason.ShoppingH:
                case TransferManager.TransferReason.Shopping:
                    if (data.m_homeBuilding != 0 && !data.Sick)
                    {
                        data.m_flags &= ~Citizen.Flags.Evacuating;
                        if (base.StartMoving(citizenID, ref data, 0, offer.Building))
                        {
                            data.SetVisitplace(citizenID, offer.Building, 0u);
                            CitizenManager instance3 = Singleton<CitizenManager>.instance;
                            BuildingManager instance4 = Singleton<BuildingManager>.instance;
                            uint containingUnit = data.GetContainingUnit(citizenID, instance4.m_buildings.m_buffer[(int)data.m_homeBuilding].m_citizenUnits, CitizenUnit.Flags.Home);
                            if (containingUnit != 0u)
                            {
                                CitizenUnit[] expr_286_cp_0 = instance3.m_units.m_buffer;
                                UIntPtr expr_286_cp_1 = (UIntPtr)containingUnit;
                                expr_286_cp_0[(int)expr_286_cp_1].m_goods = (ushort)(expr_286_cp_0[(int)expr_286_cp_1].m_goods + 100);
                            }
                        }
                    }
                    break;
                case TransferManager.TransferReason.EntertainmentB:
                case TransferManager.TransferReason.EntertainmentC:
                case TransferManager.TransferReason.EntertainmentD:
                case TransferManager.TransferReason.Entertainment:
                    if (data.m_homeBuilding != 0 && !data.Sick)
                    {
                        data.m_flags &= ~Citizen.Flags.Evacuating;
                        if (base.StartMoving(citizenID, ref data, 0, offer.Building))
                        {
                            data.SetVisitplace(citizenID, offer.Building, 0u);
                        }
                    }
                    break;
                case TransferManager.TransferReason.Taxi:
                case TransferManager.TransferReason.CriminalMove:
                case TransferManager.TransferReason.Tram:
                case TransferManager.TransferReason.Snow:
                case TransferManager.TransferReason.SnowMove:
                case TransferManager.TransferReason.RoadMaintenance:
                case TransferManager.TransferReason.SickMove:
                case TransferManager.TransferReason.ForestFire:
                case TransferManager.TransferReason.Collapsed:
                case TransferManager.TransferReason.Collapsed2:
                case TransferManager.TransferReason.Fire2:
                case TransferManager.TransferReason.Sick2:
                case TransferManager.TransferReason.FloodWater:
                case TransferManager.TransferReason.EvacuateA:
                case TransferManager.TransferReason.EvacuateB:
                case TransferManager.TransferReason.EvacuateC:
                case TransferManager.TransferReason.EvacuateD:
                case TransferManager.TransferReason.EvacuateVipA:
                case TransferManager.TransferReason.EvacuateVipB:
                case TransferManager.TransferReason.EvacuateVipC:
                case TransferManager.TransferReason.EvacuateVipD:
                    data.m_flags |= Citizen.Flags.Evacuating;
                    if (base.StartMoving(citizenID, ref data, 0, offer.Building))
                    {
                        data.SetVisitplace(citizenID, offer.Building, 0u);
                    }
                    else
                    {
                        data.SetVisitplace(citizenID, offer.Building, 0u);
                        if (data.m_visitBuilding == offer.Building)
                        {
                            data.CurrentLocation = Citizen.Location.Visit;
                        }
                    }
                    break;
                case TransferManager.TransferReason.Sick:
                    if (data.Sick)
                    {
                        data.m_flags &= ~Citizen.Flags.Evacuating;
                        if (base.StartMoving(citizenID, ref data, 0, offer.Building))
                        {
                            data.SetVisitplace(citizenID, offer.Building, 0u);
                        }
                    }
                    break;
                case TransferManager.TransferReason.Dead:
                    if (data.Dead)
                    {
                        data.SetVisitplace(citizenID, offer.Building, 0u);
                        if (data.m_visitBuilding != 0)
                        {
                            data.CurrentLocation = Citizen.Location.Visit;
                        }
                    }
                    break;
                case TransferManager.TransferReason.Worker0:
                case TransferManager.TransferReason.Worker1:
                case TransferManager.TransferReason.Worker2:
                case TransferManager.TransferReason.Worker3:
                    if (data.m_workBuilding == 0)
                    {
                        data.SetWorkplace(citizenID, offer.Building, 0u);
                    }
                    break;
                case TransferManager.TransferReason.Student1:
                case TransferManager.TransferReason.Student2:
                case TransferManager.TransferReason.Student3:
                    if (data.m_workBuilding == 0)
                    {
                        data.SetStudentplace(citizenID, offer.Building, 0u);
                    }
                    break;
                case TransferManager.TransferReason.PartnerYoung:
                case TransferManager.TransferReason.PartnerAdult:
                    uint citizen = offer.Citizen;
                    if (citizen != 0u)
                    {
                        CitizenManager instance = Singleton<CitizenManager>.instance;
                        BuildingManager instance2 = Singleton<BuildingManager>.instance;
                        ushort homeBuilding = instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_homeBuilding;
                        if (homeBuilding != 0 && !instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Dead)
                        {
                            uint num20 = instance2.m_buildings.m_buffer[(int)homeBuilding].FindCitizenUnit(CitizenUnit.Flags.Home, citizen);
                            if (num20 != 0u)
                            {
                                data.SetHome(citizenID, 0, num20);
                                data.m_family = instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_family;
                            }
                        }
                    }
                    break;
            }
        }
        private void MoveFamily(uint homeID, ref CitizenUnit data, ushort targetBuilding)
        {
            BuildingManager instance = Singleton<BuildingManager>.instance;
            CitizenManager instance2 = Singleton<CitizenManager>.instance;
            uint unitID = 0u;
            if (targetBuilding != 0)
            {
                unitID = instance.m_buildings.m_buffer[(int)targetBuilding].GetEmptyCitizenUnit(CitizenUnit.Flags.Home);
            }
            for (int i = 0; i < 5; i++)
            {
                uint citizen = data.GetCitizen(i);
                if (citizen != 0u && !instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Dead)
                {
                    instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].SetHome(citizen, 0, unitID);
                    if (instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_homeBuilding == 0)
                    {
                        instance2.ReleaseCitizen(citizen);
                    }
                }
            }
        }
    }
}
