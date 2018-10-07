using ColossalFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UnityEngine;

namespace RealCity
{
    public class pc_OutsideConnectionAI : BuildingAI
    {
        public static int m_cargoCapacity = 40;

        public static int m_residentCapacity = 1000;

        public static int m_touristFactor0 = 325;

        public static int m_touristFactor1 = 125;

        public static int m_touristFactor2 = 50;

        public static TransferManager.TransferReason m_dummyTrafficReason = TransferManager.TransferReason.None;

        public static int m_dummyTrafficFactor = 1000;

        public override void SimulationStep(ushort buildingID, ref Building data)
        {
            base.SimulationStep(buildingID, ref data);
            if ((Singleton<ToolManager>.instance.m_properties.m_mode & ItemClass.Availability.Game) != ItemClass.Availability.None)
            {
                int budget = Singleton<EconomyManager>.instance.GetBudget(this.m_info.m_class);
                int productionRate = OutsideConnectionAI.GetProductionRate(100, budget);
                System.Random rand = new System.Random();

                if (data.Info.m_class.m_service == ItemClass.Service.Road)
                {
                    m_dummyTrafficReason = TransferManager.TransferReason.DummyCar;
                }
                else if (data.Info.m_class.m_subService == ItemClass.SubService.PublicTransportPlane)
                {
                    m_dummyTrafficReason = TransferManager.TransferReason.DummyPlane;
                }
                else if (data.Info.m_class.m_subService == ItemClass.SubService.PublicTransportShip)
                {
                    m_dummyTrafficReason = TransferManager.TransferReason.DummyShip;
                }
                else if (data.Info.m_class.m_subService == ItemClass.SubService.PublicTransportTrain)
                {
                    m_dummyTrafficReason = TransferManager.TransferReason.DummyTrain;
                }


                //DebugLog.LogToFileOnly(m_dummyTrafficReason.ToString() + " " + m_dummyTrafficFactor.ToString() + " " + data.m_outgoingProblemTimer.ToString() + " " + data.m_education1.ToString());
                m_dummyTrafficFactor = rand.Next(1600) + 200;
                if (comm_data.isFoodsGettedFinal == false)
                {
                    m_residentCapacity = 0;
                    m_touristFactor0 = 0;
                    m_touristFactor1 = 0;
                    m_touristFactor2 = 0;
                }
                else
                {
                    m_residentCapacity = 1000;
                    m_touristFactor0 = 325;
                    m_touristFactor1 = 125;
                    m_touristFactor2 = 50;
                }

                if (comm_data.isHellMode)
                {
                    data.m_teens = 0;
                    //DebugLog.LogToFileOnly("hell mode, little import");
                }

                //if (rand.Next(8) == 0)
                //{
                    //fix sometime no dummy
                 //   data.m_outgoingProblemTimer = 128;
                 //   data.m_youngs = 128;
                //}
                AddConnectionOffers(buildingID, ref data, productionRate, m_cargoCapacity, m_residentCapacity, m_touristFactor0, m_touristFactor1, m_touristFactor2, m_dummyTrafficReason, m_dummyTrafficFactor);
            }
        }


        // OutsideConnectionAI
        public static void AddConnectionOffers(ushort buildingID, ref Building data, int productionRate, int cargoCapacity, int residentCapacity, int touristFactor0, int touristFactor1, int touristFactor2, TransferManager.TransferReason dummyTrafficReason, int dummyTrafficFactor)
        {
            SimulationManager instance = Singleton<SimulationManager>.instance;
            TransferManager instance2 = Singleton<TransferManager>.instance;
            DistrictManager instance3 = Singleton<DistrictManager>.instance;
            byte district = instance3.GetDistrict(data.m_position);
            DistrictPolicies.CityPlanning cityPlanningPolicies = instance3.m_districts.m_buffer[(int)district].m_cityPlanningPolicies;
            if ((cityPlanningPolicies & DistrictPolicies.CityPlanning.BoostConnections) != DistrictPolicies.CityPlanning.None)
            {
                District[] expr_55_cp_0 = instance3.m_districts.m_buffer;
                byte expr_55_cp_1 = district;
                expr_55_cp_0[(int)expr_55_cp_1].m_cityPlanningPoliciesEffect = (expr_55_cp_0[(int)expr_55_cp_1].m_cityPlanningPoliciesEffect | DistrictPolicies.CityPlanning.BoostConnections);
                touristFactor0 += (touristFactor0 + 3) / 5;
                touristFactor1 += (touristFactor1 + 3) / 5;
                touristFactor2 += (touristFactor2 + 3) / 5;
            }
            BuildingInfo info = data.Info;
            int num;
            if (info.m_class.m_service == ItemClass.Service.Road)
            {
                num = Singleton<BuildingManager>.instance.m_finalMonumentEffect[5].m_factor;
            }
            else
            {
                ItemClass.SubService subService = info.m_class.m_subService;
                if (subService != ItemClass.SubService.PublicTransportShip)
                {
                    num = 0;
                }
                else
                {
                    num = Singleton<BuildingManager>.instance.m_finalMonumentEffect[8].m_factor;
                }
            }
            if (num != 0)
            {
                touristFactor0 += (touristFactor0 * num + 50) / 100;
                touristFactor1 += (touristFactor1 * num + 50) / 100;
                touristFactor2 += (touristFactor2 * num + 50) / 100;
            }
            cargoCapacity = (cargoCapacity * productionRate + 99) / 100;
            residentCapacity = (residentCapacity * productionRate + 99) / 100;
            touristFactor0 = (touristFactor0 * productionRate + 99) / 100;
            touristFactor1 = (touristFactor1 * productionRate + 99) / 100;
            touristFactor2 = (touristFactor2 * productionRate + 99) / 100;
            dummyTrafficFactor = (dummyTrafficFactor * productionRate + 99) / 100;
            dummyTrafficFactor = (dummyTrafficFactor * DummyTrafficProbability() + 99) / 100;
            int num2 = (cargoCapacity + instance.m_randomizer.Int32(16u)) / 16;
            int num3 = (residentCapacity + instance.m_randomizer.Int32(16u)) / 16;
            if ((data.m_flags & Building.Flags.Outgoing) != Building.Flags.None)
            {
                int num4 = TickPathfindStatus(buildingID, ref data, BuildingAI.PathFindType.LeavingHuman);
                int num5 = TickPathfindStatus(buildingID, ref data, BuildingAI.PathFindType.LeavingCargo);
                int num6 = TickPathfindStatus(buildingID, ref data, BuildingAI.PathFindType.LeavingDummy);
                TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                offer.Building = buildingID;
                offer.Position = data.m_position * ((float)instance.m_randomizer.Int32(100, 400) * 0.01f);
                offer.Active = true;
                int num7 = num2;
                if (num7 != 0)
                {
                    if (num7 * num5 + instance.m_randomizer.Int32(256u) >> 8 == 0)
                    {
                        offer.Priority = 0;
                        offer.Amount = 1;
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(TransferManager.TransferReason.Ore, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(TransferManager.TransferReason.Oil, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(TransferManager.TransferReason.Grain, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(TransferManager.TransferReason.Logs, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(TransferManager.TransferReason.Goods, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(TransferManager.TransferReason.Coal, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(TransferManager.TransferReason.Petrol, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(TransferManager.TransferReason.Food, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(TransferManager.TransferReason.Lumber, offer);
                        }
                    }
                    else
                    {
                        offer.Priority = 0;
                        offer.Amount = num2;
                        instance2.AddOutgoingOffer(TransferManager.TransferReason.Ore, offer);
                        instance2.AddOutgoingOffer(TransferManager.TransferReason.Oil, offer);
                        instance2.AddOutgoingOffer(TransferManager.TransferReason.Grain, offer);
                        instance2.AddOutgoingOffer(TransferManager.TransferReason.Logs, offer);
                        instance2.AddOutgoingOffer(TransferManager.TransferReason.Goods, offer);
                        instance2.AddOutgoingOffer(TransferManager.TransferReason.Coal, offer);
                        instance2.AddOutgoingOffer(TransferManager.TransferReason.Petrol, offer);
                        instance2.AddOutgoingOffer(TransferManager.TransferReason.Food, offer);
                        instance2.AddOutgoingOffer(TransferManager.TransferReason.Lumber, offer);
                    }
                }
                int num8 = Singleton<ZoneManager>.instance.GetIncomingResidentDemand() * num3 / 100;
                if (num8 > 0)
                {
                    num8 = num8 * num4 + instance.m_randomizer.Int32(256u) >> 8;
                    if (num8 == 0)
                    {
                        offer.Priority = 0;
                        offer.Amount = 1;
                        if (instance.m_randomizer.Int32(2u) == 0)
                        {
                            if (instance.m_randomizer.Int32(16u) == 0)
                            {
                                instance2.AddOutgoingOffer(TransferManager.TransferReason.Single0, offer);
                            }
                            if (instance.m_randomizer.Int32(16u) == 0)
                            {
                                instance2.AddOutgoingOffer(TransferManager.TransferReason.Single1, offer);
                            }
                            if (instance.m_randomizer.Int32(16u) == 0)
                            {
                                instance2.AddOutgoingOffer(TransferManager.TransferReason.Single2, offer);
                            }
                            if (instance.m_randomizer.Int32(16u) == 0)
                            {
                                instance2.AddOutgoingOffer(TransferManager.TransferReason.Single3, offer);
                            }
                        }
                        else
                        {
                            if (instance.m_randomizer.Int32(16u) == 0)
                            {
                                instance2.AddOutgoingOffer(TransferManager.TransferReason.Single0B, offer);
                            }
                            if (instance.m_randomizer.Int32(16u) == 0)
                            {
                                instance2.AddOutgoingOffer(TransferManager.TransferReason.Single1B, offer);
                            }
                            if (instance.m_randomizer.Int32(16u) == 0)
                            {
                                instance2.AddOutgoingOffer(TransferManager.TransferReason.Single2B, offer);
                            }
                            if (instance.m_randomizer.Int32(16u) == 0)
                            {
                                instance2.AddOutgoingOffer(TransferManager.TransferReason.Single3B, offer);
                            }
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(TransferManager.TransferReason.Family0, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(TransferManager.TransferReason.Family1, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(TransferManager.TransferReason.Family2, offer);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddOutgoingOffer(TransferManager.TransferReason.Family3, offer);
                        }
                    }
                    else
                    {
                        offer.Priority = 0;
                        offer.Amount = num8;
                        if (instance.m_randomizer.Int32(2u) == 0)
                        {
                            instance2.AddOutgoingOffer(TransferManager.TransferReason.Single0, offer);
                            instance2.AddOutgoingOffer(TransferManager.TransferReason.Single1, offer);
                            instance2.AddOutgoingOffer(TransferManager.TransferReason.Single2, offer);
                            instance2.AddOutgoingOffer(TransferManager.TransferReason.Single3, offer);
                        }
                        else
                        {
                            instance2.AddOutgoingOffer(TransferManager.TransferReason.Single0B, offer);
                            instance2.AddOutgoingOffer(TransferManager.TransferReason.Single1B, offer);
                            instance2.AddOutgoingOffer(TransferManager.TransferReason.Single2B, offer);
                            instance2.AddOutgoingOffer(TransferManager.TransferReason.Single3B, offer);
                        }
                        instance2.AddOutgoingOffer(TransferManager.TransferReason.Family0, offer);
                        instance2.AddOutgoingOffer(TransferManager.TransferReason.Family1, offer);
                        instance2.AddOutgoingOffer(TransferManager.TransferReason.Family2, offer);
                        instance2.AddOutgoingOffer(TransferManager.TransferReason.Family3, offer);
                    }
                }
                int num9 = (dummyTrafficFactor + instance.m_randomizer.Int32(100u)) / 100;
                if (num9 > 0 && dummyTrafficReason != TransferManager.TransferReason.None)
                {
                    num9 = num9 * num6 + instance.m_randomizer.Int32(256u) >> 8;
                    if (num9 == 0)
                    {
                        offer.Priority = 7;
                        offer.Amount = 1;
                        if (instance.m_randomizer.Int32(4u) == 0)
                        {
                            instance2.AddOutgoingOffer(dummyTrafficReason, offer);
                        }
                    }
                    else
                    {
                        offer.Priority = 7;
                        offer.Amount = num9;
                        instance2.AddOutgoingOffer(dummyTrafficReason, offer);
                    }
                }
                int num10;
                Singleton<ImmaterialResourceManager>.instance.CheckGlobalResource(ImmaterialResourceManager.Resource.Attractiveness, out num10);
                int num11;
                Singleton<ImmaterialResourceManager>.instance.CheckTotalResource(ImmaterialResourceManager.Resource.LandValue, out num11);
                num10 += num11;
                num10 = 100 * num10 / Mathf.Max(num10 + 200, 200);
                int num12 = (int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_residentialData.m_finalHomeOrWorkCount;
                num12 = (100 * num12 + 20000) / Mathf.Max(num12 + 20000, 20000);
                int num13 = num12 * num10;
                int num14 = (num13 * touristFactor0 + instance.m_randomizer.Int32(160000u)) / 160000;
                int num15 = (num13 * touristFactor1 + instance.m_randomizer.Int32(160000u)) / 160000;
                int num16 = (num13 * touristFactor2 + instance.m_randomizer.Int32(160000u)) / 160000;
                num13 = num14 + num15 + num16;
                if (num13 != 0)
                {
                    if (num13 * num4 + instance.m_randomizer.Int32(256u) >> 8 == 0)
                    {
                        offer.Priority = instance.m_randomizer.Int32(8u);
                        offer.Amount = 1;
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            switch (instance.m_randomizer.Int32(8u))
                            {
                                case 0:
                                    instance2.AddIncomingOffer(TransferManager.TransferReason.Shopping, offer);
                                    break;
                                case 1:
                                    instance2.AddIncomingOffer(TransferManager.TransferReason.ShoppingB, offer);
                                    break;
                                case 2:
                                    instance2.AddIncomingOffer(TransferManager.TransferReason.ShoppingC, offer);
                                    break;
                                case 3:
                                    instance2.AddIncomingOffer(TransferManager.TransferReason.ShoppingD, offer);
                                    break;
                                case 4:
                                    instance2.AddIncomingOffer(TransferManager.TransferReason.ShoppingE, offer);
                                    break;
                                case 5:
                                    instance2.AddIncomingOffer(TransferManager.TransferReason.ShoppingF, offer);
                                    break;
                                case 6:
                                    instance2.AddIncomingOffer(TransferManager.TransferReason.ShoppingG, offer);
                                    break;
                                case 7:
                                    instance2.AddIncomingOffer(TransferManager.TransferReason.ShoppingH, offer);
                                    break;
                            }
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            switch (instance.m_randomizer.Int32(8u))
                            {
                                case 0:
                                    instance2.AddIncomingOffer(TransferManager.TransferReason.Entertainment, offer);
                                    break;
                                case 1:
                                    instance2.AddIncomingOffer(TransferManager.TransferReason.EntertainmentB, offer);
                                    break;
                                case 2:
                                    instance2.AddIncomingOffer(TransferManager.TransferReason.EntertainmentC, offer);
                                    break;
                                case 3:
                                    instance2.AddIncomingOffer(TransferManager.TransferReason.EntertainmentD, offer);
                                    break;
                                case 4:
                                    instance2.AddIncomingOffer(TransferManager.TransferReason.TouristA, offer);
                                    break;
                                case 5:
                                    instance2.AddIncomingOffer(TransferManager.TransferReason.TouristB, offer);
                                    break;
                                case 6:
                                    instance2.AddIncomingOffer(TransferManager.TransferReason.TouristC, offer);
                                    break;
                                case 7:
                                    instance2.AddIncomingOffer(TransferManager.TransferReason.TouristD, offer);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        offer.Priority = instance.m_randomizer.Int32(8u);
                        offer.Amount = num14 + num15 + num16;
                        switch (instance.m_randomizer.Int32(8u))
                        {
                            case 0:
                                instance2.AddIncomingOffer(TransferManager.TransferReason.Shopping, offer);
                                break;
                            case 1:
                                instance2.AddIncomingOffer(TransferManager.TransferReason.ShoppingB, offer);
                                break;
                            case 2:
                                instance2.AddIncomingOffer(TransferManager.TransferReason.ShoppingC, offer);
                                break;
                            case 3:
                                instance2.AddIncomingOffer(TransferManager.TransferReason.ShoppingD, offer);
                                break;
                            case 4:
                                instance2.AddIncomingOffer(TransferManager.TransferReason.ShoppingE, offer);
                                break;
                            case 5:
                                instance2.AddIncomingOffer(TransferManager.TransferReason.ShoppingF, offer);
                                break;
                            case 6:
                                instance2.AddIncomingOffer(TransferManager.TransferReason.ShoppingG, offer);
                                break;
                            case 7:
                                instance2.AddIncomingOffer(TransferManager.TransferReason.ShoppingH, offer);
                                break;
                        }
                        switch (instance.m_randomizer.Int32(8u))
                        {
                            case 0:
                                instance2.AddIncomingOffer(TransferManager.TransferReason.Entertainment, offer);
                                break;
                            case 1:
                                instance2.AddIncomingOffer(TransferManager.TransferReason.EntertainmentB, offer);
                                break;
                            case 2:
                                instance2.AddIncomingOffer(TransferManager.TransferReason.EntertainmentC, offer);
                                break;
                            case 3:
                                instance2.AddIncomingOffer(TransferManager.TransferReason.EntertainmentD, offer);
                                break;
                            case 4:
                                instance2.AddIncomingOffer(TransferManager.TransferReason.TouristA, offer);
                                break;
                            case 5:
                                instance2.AddIncomingOffer(TransferManager.TransferReason.TouristB, offer);
                                break;
                            case 6:
                                instance2.AddIncomingOffer(TransferManager.TransferReason.TouristC, offer);
                                break;
                            case 7:
                                instance2.AddIncomingOffer(TransferManager.TransferReason.TouristD, offer);
                                break;
                        }
                    }
                }
            }
            if ((data.m_flags & Building.Flags.Incoming) != Building.Flags.None)
            {
                int num17 = TickPathfindStatus(buildingID, ref data, BuildingAI.PathFindType.EnteringHuman);
                int num18 = TickPathfindStatus(buildingID, ref data, BuildingAI.PathFindType.EnteringCargo);
                int num19 = TickPathfindStatus(buildingID, ref data, BuildingAI.PathFindType.EnteringDummy);
                TransferManager.TransferOffer offer2 = default(TransferManager.TransferOffer);
                offer2.Building = buildingID;
                offer2.Position = data.m_position * ((float)instance.m_randomizer.Int32(100, 400) * 0.01f);
                offer2.Active = false;
                int num20 = num2;
                if (num20 != 0)
                {
                    if (num20 * num18 + instance.m_randomizer.Int32(256u) >> 8 == 0)
                    {
                        offer2.Priority = 0;
                        offer2.Amount = 1;
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddIncomingOffer(TransferManager.TransferReason.Ore, offer2);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddIncomingOffer(TransferManager.TransferReason.Oil, offer2);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddIncomingOffer(TransferManager.TransferReason.Grain, offer2);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddIncomingOffer(TransferManager.TransferReason.Logs, offer2);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddIncomingOffer(TransferManager.TransferReason.Goods, offer2);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddIncomingOffer(TransferManager.TransferReason.Coal, offer2);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddIncomingOffer(TransferManager.TransferReason.Petrol, offer2);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddIncomingOffer(TransferManager.TransferReason.Food, offer2);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddIncomingOffer(TransferManager.TransferReason.Lumber, offer2);
                        }
                    }
                    else
                    {
                        offer2.Priority = 0;
                        offer2.Amount = num2;
                        instance2.AddIncomingOffer(TransferManager.TransferReason.Ore, offer2);
                        instance2.AddIncomingOffer(TransferManager.TransferReason.Oil, offer2);
                        instance2.AddIncomingOffer(TransferManager.TransferReason.Grain, offer2);
                        instance2.AddIncomingOffer(TransferManager.TransferReason.Logs, offer2);
                        instance2.AddIncomingOffer(TransferManager.TransferReason.Goods, offer2);
                        instance2.AddIncomingOffer(TransferManager.TransferReason.Coal, offer2);
                        instance2.AddIncomingOffer(TransferManager.TransferReason.Petrol, offer2);
                        instance2.AddIncomingOffer(TransferManager.TransferReason.Food, offer2);
                        instance2.AddIncomingOffer(TransferManager.TransferReason.Lumber, offer2);
                    }
                }
                int num21 = num3;
                if (num21 > 0)
                {
                    if (num21 * num17 + instance.m_randomizer.Int32(256u) >> 8 == 0)
                    {
                        offer2.Priority = 0;
                        offer2.Amount = 1;
                        if (instance.m_randomizer.Int32(2u) == 0)
                        {
                            if (instance.m_randomizer.Int32(16u) == 0)
                            {
                                instance2.AddIncomingOffer(TransferManager.TransferReason.Single0, offer2);
                            }
                            if (instance.m_randomizer.Int32(16u) == 0)
                            {
                                instance2.AddIncomingOffer(TransferManager.TransferReason.Single1, offer2);
                            }
                            if (instance.m_randomizer.Int32(16u) == 0)
                            {
                                instance2.AddIncomingOffer(TransferManager.TransferReason.Single2, offer2);
                            }
                            if (instance.m_randomizer.Int32(16u) == 0)
                            {
                                instance2.AddIncomingOffer(TransferManager.TransferReason.Single3, offer2);
                            }
                        }
                        else
                        {
                            if (instance.m_randomizer.Int32(16u) == 0)
                            {
                                instance2.AddIncomingOffer(TransferManager.TransferReason.Single0B, offer2);
                            }
                            if (instance.m_randomizer.Int32(16u) == 0)
                            {
                                instance2.AddIncomingOffer(TransferManager.TransferReason.Single1B, offer2);
                            }
                            if (instance.m_randomizer.Int32(16u) == 0)
                            {
                                instance2.AddIncomingOffer(TransferManager.TransferReason.Single2B, offer2);
                            }
                            if (instance.m_randomizer.Int32(16u) == 0)
                            {
                                instance2.AddIncomingOffer(TransferManager.TransferReason.Single3B, offer2);
                            }
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddIncomingOffer(TransferManager.TransferReason.Family0, offer2);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddIncomingOffer(TransferManager.TransferReason.Family1, offer2);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddIncomingOffer(TransferManager.TransferReason.Family2, offer2);
                        }
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            instance2.AddIncomingOffer(TransferManager.TransferReason.Family3, offer2);
                        }
                    }
                    else
                    {
                        offer2.Priority = 0;
                        offer2.Amount = num3;
                        if (instance.m_randomizer.Int32(2u) == 0)
                        {
                            instance2.AddIncomingOffer(TransferManager.TransferReason.Single0, offer2);
                            instance2.AddIncomingOffer(TransferManager.TransferReason.Single1, offer2);
                            instance2.AddIncomingOffer(TransferManager.TransferReason.Single2, offer2);
                            instance2.AddIncomingOffer(TransferManager.TransferReason.Single3, offer2);
                        }
                        else
                        {
                            instance2.AddIncomingOffer(TransferManager.TransferReason.Single0B, offer2);
                            instance2.AddIncomingOffer(TransferManager.TransferReason.Single1B, offer2);
                            instance2.AddIncomingOffer(TransferManager.TransferReason.Single2B, offer2);
                            instance2.AddIncomingOffer(TransferManager.TransferReason.Single3B, offer2);
                        }
                        instance2.AddIncomingOffer(TransferManager.TransferReason.Family0, offer2);
                        instance2.AddIncomingOffer(TransferManager.TransferReason.Family1, offer2);
                        instance2.AddIncomingOffer(TransferManager.TransferReason.Family2, offer2);
                        instance2.AddIncomingOffer(TransferManager.TransferReason.Family3, offer2);
                    }
                }
                int num22 = (dummyTrafficFactor + instance.m_randomizer.Int32(100u)) / 100;
                if (num22 > 0 && dummyTrafficReason != TransferManager.TransferReason.None)
                {
                    num22 = num22 * num19 + instance.m_randomizer.Int32(256u) >> 8;
                    if (num22 == 0)
                    {
                        offer2.Priority = 7;
                        offer2.Amount = 1;
                        if (instance.m_randomizer.Int32(4u) == 0)
                        {
                            instance2.AddIncomingOffer(dummyTrafficReason, offer2);
                        }
                    }
                    else
                    {
                        offer2.Priority = 7;
                        offer2.Amount = num22;
                        instance2.AddIncomingOffer(dummyTrafficReason, offer2);
                    }
                }
                int num23 = Mathf.Max(1, (int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_residentialData.m_finalHomeOrWorkCount);
                int num24 = (num23 + instance.m_randomizer.Int32(10u)) / 10;
                int num25 = (num24 * touristFactor0 + instance.m_randomizer.Int32(16000u)) / 16000;
                int num26 = (num24 * touristFactor1 + instance.m_randomizer.Int32(16000u)) / 16000;
                int num27 = (num24 * touristFactor2 + instance.m_randomizer.Int32(16000u)) / 16000;
                if (num25 != 0)
                {
                    num25 = num25 * num17 + instance.m_randomizer.Int32(256u) >> 8;
                    if (num25 == 0)
                    {
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            offer2.Priority = instance.m_randomizer.Int32(8u);
                            offer2.Amount = 1;
                            instance2.AddOutgoingOffer(TransferManager.TransferReason.LeaveCity0, offer2);
                        }
                    }
                    else
                    {
                        offer2.Priority = instance.m_randomizer.Int32(8u);
                        offer2.Amount = num25;
                        instance2.AddOutgoingOffer(TransferManager.TransferReason.LeaveCity0, offer2);
                    }
                }
                if (num26 != 0)
                {
                    num26 = num26 * num17 + instance.m_randomizer.Int32(256u) >> 8;
                    if (num26 == 0)
                    {
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            offer2.Priority = instance.m_randomizer.Int32(8u);
                            offer2.Amount = 1;
                            instance2.AddOutgoingOffer(TransferManager.TransferReason.LeaveCity1, offer2);
                        }
                    }
                    else
                    {
                        offer2.Priority = instance.m_randomizer.Int32(8u);
                        offer2.Amount = num26;
                        instance2.AddOutgoingOffer(TransferManager.TransferReason.LeaveCity1, offer2);
                    }
                }
                if (num27 != 0)
                {
                    num27 = num27 * num17 + instance.m_randomizer.Int32(256u) >> 8;
                    if (num27 == 0)
                    {
                        if (instance.m_randomizer.Int32(16u) == 0)
                        {
                            offer2.Priority = instance.m_randomizer.Int32(8u);
                            offer2.Amount = 1;
                            instance2.AddOutgoingOffer(TransferManager.TransferReason.LeaveCity2, offer2);
                        }
                    }
                    else
                    {
                        offer2.Priority = instance.m_randomizer.Int32(8u);
                        offer2.Amount = num27;
                        instance2.AddOutgoingOffer(TransferManager.TransferReason.LeaveCity2, offer2);
                    }
                }
            }
        }

        private static int TickPathfindStatus(ushort buildingID, ref Building data, BuildingAI.PathFindType type)
        {
            switch (type)
            {
                case BuildingAI.PathFindType.EnteringCargo:
                    return TickPathfindStatus(ref data.m_education3, ref data.m_adults);
                case BuildingAI.PathFindType.LeavingCargo:
                    return TickPathfindStatus(ref data.m_teens, ref data.m_serviceProblemTimer);
                case BuildingAI.PathFindType.EnteringHuman:
                    return TickPathfindStatus(ref data.m_workerProblemTimer, ref data.m_taxProblemTimer);
                case BuildingAI.PathFindType.LeavingHuman:
                    return TickPathfindStatus(ref data.m_incomingProblemTimer, ref data.m_seniors);
                case BuildingAI.PathFindType.EnteringDummy:
                    return TickPathfindStatus(ref data.m_outgoingProblemTimer, ref data.m_education1);
                case BuildingAI.PathFindType.LeavingDummy:
                    return TickPathfindStatus(ref data.m_youngs, ref data.m_education2);
                default:
                    return 0;
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

        private static int DummyTrafficProbability()
        {
            uint vehicleCount = (uint)Singleton<VehicleManager>.instance.m_vehicleCount;
            uint instanceCount = (uint)Singleton<CitizenManager>.instance.m_instanceCount;
            if (vehicleCount * 65536u > instanceCount * 16384u)
            {
                return (int)(2048000u / (16384u + vehicleCount * 4u) - 25u);
            }
            return (int)(8192000u / (65536u + instanceCount * 4u) - 25u);
        }

    }
}