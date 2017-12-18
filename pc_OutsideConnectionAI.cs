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

        public static int m_patientCapacity = 200;

        public static int m_healthCareAccumulation = 200;

        public static float m_healthCareRadius = 1000f;

        public static int m_curingRate = 20;

        public static int m_ambulanceCount = 20;

        public static int m_fireDepartmentAccumulation = 200;

        public static int m_fireTruckCount = 20;

        public static float m_fireDepartmentRadius = 1000f;

        public static int m_policeDepartmentAccumulation = 200;
        public static int m_jailCapacity = 40;
        public static float m_policeDepartmentRadius = 1000f;
        public static int m_sentenceWeeks = 15;
        public static int m_policeCarCount = 20;


        /*public static void Init()
        {
            //DebugLog.Log("Init fake transfer manager");
            try
            {
                var inst = Singleton<OutsideConnectionAI>.instance;
                var cargoCapacity = typeof(OutsideConnectionAI).GetField("m_cargoCapacity", BindingFlags.Public | BindingFlags.Instance);
                var residentCapacity = typeof(OutsideConnectionAI).GetField("m_residentCapacity", BindingFlags.Public | BindingFlags.Instance);
                var touristFactor0 = typeof(OutsideConnectionAI).GetField("m_touristFactor0", BindingFlags.Public | BindingFlags.Instance);
                var touristFactor1 = typeof(OutsideConnectionAI).GetField("m_touristFactor1", BindingFlags.Public | BindingFlags.Instance);
                var touristFactor2 = typeof(OutsideConnectionAI).GetField("m_touristFactor2", BindingFlags.Public | BindingFlags.Instance);
                var dummyTrafficReason = typeof(OutsideConnectionAI).GetField("m_dummyTrafficReason", BindingFlags.Public | BindingFlags.Instance);
                var dummyTrafficFactor = typeof(OutsideConnectionAI).GetField("m_dummyTrafficFactor", BindingFlags.Public | BindingFlags.Instance);
                if (inst == null)
                {
                    DebugLog.LogToFileOnly("No instance of OutsideConnectionAI found!");
                    return;
                }
                _cargoCapacity = (int)cargoCapacity.GetValue(inst);
                _residentCapacity = (int)residentCapacity.GetValue(inst);
                _touristFactor0 = (int)touristFactor0.GetValue(inst);
                _touristFactor1 = (int)touristFactor1.GetValue(inst);
                _touristFactor2 = (int)touristFactor2.GetValue(inst);
                _dummyTrafficFactor = (int)dummyTrafficFactor.GetValue(inst);
                _dummyTrafficReason = (TransferManager.TransferReason)dummyTrafficReason.GetValue(inst);
            }
            catch (Exception ex)
            {
                DebugLog.LogToFileOnly("OutsideConnectionAI Exception: " + ex.Message);
            }
        }
        private static int _cargoCapacity;
        private static int _residentCapacity;
        private static int _touristFactor0;
        private static int _touristFactor1;
        private static int _touristFactor2;
        private static int _dummyTrafficFactor;
        private static TransferManager.TransferReason _dummyTrafficReason;

        private static bool _init = false;
        */
        public static bool have_maintain_road_building = false;
        public static bool have_garbage_building = false;
        public static bool have_cemetry_building = false;
        public static bool have_police_building = false;
        public static bool have_hospital_building = false;
        public static bool have_fire_building = false;




        public override void SimulationStep(ushort buildingID, ref Building data)
        {
            /*if (!_init)
            {
                _init = true;
                Init();
            }*/
            base.SimulationStep(buildingID, ref data);
            if ((Singleton<ToolManager>.instance.m_properties.m_mode & ItemClass.Availability.Game) != ItemClass.Availability.None)
            {
                int budget = Singleton<EconomyManager>.instance.GetBudget(this.m_info.m_class);
                int productionRate = OutsideConnectionAI.GetProductionRate(100, budget);
                //Init();
                //DebugLog.LogToFileOnly(_cargoCapacity.ToString() + _residentCapacity.ToString() + _dummyTrafficFactor.ToString() + _dummyTrafficReason.ToString());
                System.Random rand = new System.Random();
                //int temp = rand.Next(4);

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
                m_dummyTrafficFactor = rand.Next(800) + 200;

                if (comm_data.crasy_task)
                {
                    if (data.Info.m_class.m_service == ItemClass.Service.Road)
                    {
                        m_dummyTrafficFactor = m_dummyTrafficFactor * 10;
                    }
                }


                if (comm_data.outside_road_num_final != 0)
                {
                    m_cargoCapacity = (int)((1f - (float)comm_data.outside_road_count / (comm_data.outside_road_num_final * 65000f)) * 40);
                    //DebugLog.LogToFileOnly("m_cargoCapacity = " + m_cargoCapacity.ToString());
                } else
                {
                    m_cargoCapacity = 20;
                }

                int family_minus_oilorebuiling = (int)(comm_data.family_count / 10) - pc_PrivateBuildingAI.all_oil_building_profit_final - pc_PrivateBuildingAI.all_ore_building_profit_final - pc_PrivateBuildingAI.all_oil_building_loss_final - pc_PrivateBuildingAI.all_ore_building_loss_final;
                if (family_minus_oilorebuiling > 0)
                {
                    family_minus_oilorebuiling = 0;
                }

                m_residentCapacity = 1000 + (family_minus_oilorebuiling*10);
                float demand_idex = 1;

                demand_idex = (float)(comm_data.family_weight_stable_high + comm_data.family_count - comm_data.family_weight_stable_low) / (float)(comm_data.family_count);
                demand_idex = (demand_idex < 0f) ? 0 : demand_idex;

                if (comm_data.family_count > 100)
                {
                    demand_idex = demand_idex * 100f / comm_data.family_count;
                }

                m_residentCapacity = (int)(m_residentCapacity * demand_idex);

                if (m_residentCapacity < 0 || (demand_idex == 0))
                {
                    m_residentCapacity = 0;
                }

                if (m_residentCapacity < 30)
                {
                    if (rand.Next(200) < 30)
                    {
                        m_residentCapacity = rand.Next(30);
                    }
                }

                int tourist_trans_fee = 0;
                if (comm_data.tourist_num_final != 0)
                {
                    tourist_trans_fee = (int)(comm_data.tourist_transport_fee_num_final / comm_data.tourist_num_final * 10f);
                }


                if (comm_data.family_count > 0)
                {
                    int temp = (comm_data.family_count > 650) ? 650 : comm_data.family_count;
                    m_touristFactor0 = rand.Next(temp) + family_minus_oilorebuiling / 4;
                    m_touristFactor1 = rand.Next(temp) /2 + family_minus_oilorebuiling / 8;
                    m_touristFactor2 = rand.Next(temp) /4 + family_minus_oilorebuiling / 16;
                } else
                {
                    m_touristFactor0 = 0;
                    m_touristFactor1 = 0;
                    m_touristFactor2 = 0;
                }


                if (m_touristFactor0 < 0)
                {
                    m_touristFactor0 = 0;
                }

                if (m_touristFactor1 < 0)
                {
                    m_touristFactor1 = 0;
                }

                if (m_touristFactor2 < 0)
                {
                    m_touristFactor2 = 0;
                }


                if (comm_data.happy_task)
                {
                    m_touristFactor0 = (int)(m_touristFactor0 * 1.5f);
                    m_touristFactor1 = (int)(m_touristFactor1 * 1.5f);
                    m_touristFactor2 = (int)(m_touristFactor2 * 1.5f);
                }

                if (data.Info.m_class.m_service == ItemClass.Service.PublicTransport)
                {
                    if (data.Info.m_class.m_subService == ItemClass.SubService.PublicTransportPlane)
                    {
                        m_touristFactor0 = m_touristFactor0 * 4;
                        m_touristFactor1 = m_touristFactor1 * 4;
                        m_touristFactor2 = m_touristFactor2 * 4;
                    } else if (data.Info.m_class.m_subService == ItemClass.SubService.PublicTransportShip)
                    {
                        m_touristFactor0 = m_touristFactor0 * 3;
                        m_touristFactor1 = m_touristFactor1 * 3;
                        m_touristFactor2 = m_touristFactor2 * 3;
                        m_cargoCapacity = 60;
                    }
                    else if (data.Info.m_class.m_subService == ItemClass.SubService.PublicTransportTrain)
                    {
                        m_touristFactor0 = m_touristFactor0 * 2;
                        m_touristFactor1 = m_touristFactor1 * 2;
                        m_touristFactor2 = m_touristFactor2 * 2;
                        m_cargoCapacity = 50;
                    }
                }



                OutsideConnectionAI.AddConnectionOffers(buildingID, ref data, productionRate, m_cargoCapacity, m_residentCapacity, m_touristFactor0, m_touristFactor1, m_touristFactor2, m_dummyTrafficReason, m_dummyTrafficFactor);
                caculate_outside_situation(buildingID, ref data);
                process_outside_demand(buildingID, ref data);
                Addotherconnectionoffers(buildingID, ref data);
                Addotherconnectionservice(buildingID, ref data);
            }
        }


        public void Addotherconnectionservice(ushort buildingID, ref Building data)
        {
            if ((data.m_flags & Building.Flags.IncomingOutgoing) == Building.Flags.Incoming)
            {
                if (data.Info.m_class.m_service == ItemClass.Service.Road)
                {
                    BuildingManager instance = Singleton<BuildingManager>.instance;
                    ushort num3 = instance.FindBuilding(instance.m_buildings.m_buffer[buildingID].m_position, 200f, data.Info.m_class.m_service, ItemClass.SubService.None, Building.Flags.Outgoing, Building.Flags.Incoming);
                    if (num3 != 0)
                    {
                        if (comm_data.building_money[buildingID] == 70000000f)
                        {
                            ProduceHospitalGoods(buildingID, num3, ref data, ref instance.m_buildings.m_buffer[num3]);
                            //ProduceFireGoods(buildingID, num3, ref data, ref instance.m_buildings.m_buffer[num3]);
                            ProducePoliceGoods(buildingID, num3, ref data, ref instance.m_buildings.m_buffer[num3]);
                            // add connection
                        }


                        if (comm_data.building_money[buildingID] != 70000000f)
                        {
                            // create unit for building
                            //Singleton<CitizenManager>.instance.CreateUnits(out data.m_citizenUnits, ref Singleton<SimulationManager>.instance.m_randomizer, num3, 0, 0, 0, m_patientCapacity, 0, 0);
                            Singleton<CitizenManager>.instance.CreateUnits(out data.m_citizenUnits, ref Singleton<SimulationManager>.instance.m_randomizer, buildingID, 0, 0, 0, m_patientCapacity + m_jailCapacity, 0, 0);
                            comm_data.building_money[buildingID] = 70000000f;
                        }
                    }
                }
            }

        }

        public void caculate_outside_situation(ushort buildingID, ref Building data)
        {
            if (comm_data.outside_pre_building < buildingID)
            {
                comm_data.outside_dead_count_temp += data.m_customBuffer1;
                comm_data.outside_crime_count_temp += data.m_crimeBuffer;
                comm_data.outside_sick_count_temp += data.m_customBuffer2;
                comm_data.outside_garbage_count_temp += data.m_garbageBuffer;
                comm_data.outside_firestation_count_temp += data.m_electricityBuffer;
                comm_data.outside_road_count_temp += data.m_waterBuffer;
                //DebugLog.LogToFileOnly("data.m_waterBuffer = " + data.m_waterBuffer.ToString());
                if (data.Info.m_class.m_service == ItemClass.Service.Road)
                {
                    if ((data.m_flags & Building.Flags.IncomingOutgoing) == Building.Flags.Incoming)
                    {
                        comm_data.outside_road_num++;
                    }
                }
            }
            else
            {
                comm_data.outside_dead_count = comm_data.outside_dead_count_temp;
                comm_data.outside_garbage_count = comm_data.outside_garbage_count_temp;
                comm_data.outside_sick_count = comm_data.outside_sick_count_temp;
                comm_data.outside_crime_count = comm_data.outside_crime_count_temp;
                comm_data.outside_firestation_count = comm_data.outside_firestation_count_temp;
                comm_data.outside_road_count = comm_data.outside_road_count_temp;
                comm_data.outside_road_num_final = comm_data.outside_road_num;
                comm_data.outside_dead_count_temp = 0;
                comm_data.outside_crime_count_temp = 0;
                comm_data.outside_sick_count_temp = 0;
                comm_data.outside_garbage_count_temp = 0;
                comm_data.outside_firestation_count_temp = 0;
                comm_data.outside_road_count_temp = 0;
                comm_data.outside_road_num = 0;
                comm_data.outside_patient = 0;
                comm_data.outside_ambulance_car = 0;
                comm_data.outside_crime = 0;
                comm_data.outside_police_car = 0;
                comm_data.firetruck = 0;
            }

            comm_data.outside_pre_building = buildingID;
        }

        public void process_outside_demand(ushort buildingID, ref Building data)
        {
            //garbage can existed on both incoming and outgoing outside building
            //dead can only existed on outgoing building
            //crime and sick can only existed on incoming building
            //garbagebuffer

            SimulationManager instance2 = Singleton<SimulationManager>.instance;
            float currentDayTimeHour = instance2.m_currentDayTimeHour;
            

            System.Random rand = new System.Random();
            if (data.Info.m_class.m_service == ItemClass.Service.Road)
            {
                if (have_garbage_building && comm_data.garbage_connection && Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.Garbage))
                {
                    if (currentDayTimeHour > 17f || currentDayTimeHour < 5f)
                    {
                        data.m_garbageBuffer = (ushort)(data.m_garbageBuffer + 150);
                    } else
                    {
                        data.m_garbageBuffer = (ushort)(data.m_garbageBuffer + 50);
                    }

                    if ((data.m_flags & Building.Flags.IncomingOutgoing) == Building.Flags.Incoming)
                    {
                        if (comm_data.garbage_task)
                        {
                            data.m_garbageBuffer = 0;
                        }
                    }

                    if ((data.m_flags & Building.Flags.IncomingOutgoing) == Building.Flags.Outgoing)
                    {
                        if (comm_data.garbage_task)
                        {
                            data.m_garbageBuffer = 60000;
                        }
                    }
                }
                else if (RealCity.update_once && (data.m_garbageBuffer != 0))
                {
                    data.m_garbageBuffer = 0;
                    TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                    offer.Building = buildingID;
                    Singleton<TransferManager>.instance.RemoveOutgoingOffer(TransferManager.TransferReason.Garbage, offer);
                    Singleton<TransferManager>.instance.RemoveOutgoingOffer(TransferManager.TransferReason.GarbageMove, offer);
                }


                if ((data.m_flags & Building.Flags.IncomingOutgoing) == Building.Flags.Incoming)
                {
                    if (have_police_building && comm_data.crime_connection && Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.PoliceDepartment) && (!comm_data.policehelp))
                    {
                        if (currentDayTimeHour > 17f || currentDayTimeHour < 5f)
                        {
                            data.m_crimeBuffer = (ushort)(data.m_crimeBuffer + 2);
                        } else
                        {
                            data.m_crimeBuffer = (ushort)(data.m_crimeBuffer + rand.Next(11) / 10);
                        }
                    }
                    else if (RealCity.update_once && (data.m_crimeBuffer != 0))
                    {
                        data.m_crimeBuffer = 0;
                        TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                        offer.Building = buildingID;
                        Singleton<TransferManager>.instance.RemoveOutgoingOffer(TransferManager.TransferReason.Crime, offer);
                    }
                    //sick
                    if (have_hospital_building && comm_data.sick_connection && Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.HealthCare) && (!comm_data.hospitalhelp))
                    {
                        if (currentDayTimeHour > 17f || currentDayTimeHour < 5f)
                        {
                            data.m_customBuffer2 = (ushort)(data.m_customBuffer2 + 2);
                        }
                        else
                        {
                            data.m_customBuffer2 = (ushort)(data.m_customBuffer2 + rand.Next(11) / 10);
                        }
                    }
                    else if (RealCity.update_once && (data.m_customBuffer2 != 0))
                    {
                        data.m_customBuffer2 = 0;
                        TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                        offer.Building = buildingID;
                        Singleton<TransferManager>.instance.RemoveOutgoingOffer(TransferManager.TransferReason.Sick, offer);
                    }
                    //fire
                    if (have_fire_building && comm_data.fire_connection && Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.FireDepartment) && (!comm_data.firehelp))
                    {
                        if (currentDayTimeHour > 17f || currentDayTimeHour < 5f)
                        {
                            data.m_electricityBuffer = (ushort)(data.m_electricityBuffer + 2);
                        } else
                        {
                            data.m_electricityBuffer = (ushort)(data.m_electricityBuffer + rand.Next(11) / 10);
                        }
                        data.m_fireIntensity = 250;
                    }
                    else if (RealCity.update_once && (data.m_electricityBuffer != 0))
                    {
                        data.m_electricityBuffer = 0;
                        data.m_fireIntensity = 0;
                        TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                        offer.Building = buildingID;
                        Singleton<TransferManager>.instance.RemoveOutgoingOffer(TransferManager.TransferReason.Fire, offer);
                    }
                    //road maintain
                    if (have_maintain_road_building && comm_data.road_connection && have_maintain_road_building)
                    {
                        if (currentDayTimeHour > 17f || currentDayTimeHour < 5f)
                        {
                            data.m_waterBuffer = (ushort)(data.m_waterBuffer + 50);
                        } else
                        {
                            data.m_waterBuffer = (ushort)(data.m_waterBuffer + 5);
                        }
                    }
                    else if (RealCity.update_once && (data.m_waterBuffer != 32500))
                    {
                        data.m_waterBuffer = 32500;
                        TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                        offer.Building = buildingID;
                        Singleton<TransferManager>.instance.RemoveIncomingOffer(TransferManager.TransferReason.RoadMaintenance, offer);
                    }
                }
                else
                {
                    //deadbuffer
                    if (have_cemetry_building && comm_data.dead_connection && Singleton<UnlockManager>.instance.Unlocked(UnlockManager.Feature.DeathCare))
                    {
                        if (currentDayTimeHour > 17f || currentDayTimeHour < 5f)
                        {
                            data.m_customBuffer1 = (ushort)(data.m_customBuffer1 + rand.Next(20) / 10);
                        } else
                        {
                            data.m_customBuffer1 = (ushort)(data.m_customBuffer1 + rand.Next(11) / 10);
                        }


                        if (comm_data.dead_task)
                        {
                            data.m_garbageBuffer = 20;
                        }
                    }
                    else if (RealCity.update_once && (data.m_customBuffer1 != 0))
                    {
                        data.m_customBuffer1 = 0;
                        TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                        offer.Building = buildingID;
                        Singleton<TransferManager>.instance.RemoveOutgoingOffer(TransferManager.TransferReason.DeadMove, offer);
                    }
                }

                if (data.m_customBuffer1 > 64)
                {
                    data.m_customBuffer1 = 64;
                }

                if (data.m_customBuffer2 > 65000)
                {
                    data.m_customBuffer2 = 65000;
                }

                if (data.m_crimeBuffer > 65000)
                {
                    data.m_crimeBuffer = 65000;
                }

                if (data.m_garbageBuffer > 65000)
                {
                    data.m_garbageBuffer = 65000;
                }

                if (data.m_electricityBuffer > 65000)
                {
                    data.m_electricityBuffer = 65000;
                }
                if (data.m_waterBuffer > 65000)
                {
                    data.m_waterBuffer = 65000;
                }
            }
            else
            {
                //DebugLog.LogToFileOnly("add road condition4 clean building " + buildingID.ToString());
                data.m_garbageBuffer = 0;
                data.m_crimeBuffer = 0;
                data.m_customBuffer1 = 0;
                data.m_customBuffer2 = 0;
                data.m_electricityBuffer = 0;
                data.m_waterBuffer = 0;
            }
        }

        public void Addgarbageoffers(ushort buildingID, ref Building data)
        {
            TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);

            if (have_garbage_building && comm_data.garbage_connection && Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.Garbage))
            {
                if ((data.m_flags & Building.Flags.IncomingOutgoing) == Building.Flags.Incoming)
                {
                    int car_valid_path = TickPathfindStatus(ref data.m_education3, ref data.m_adults);
                    SimulationManager instance1 = Singleton<SimulationManager>.instance;
                    if (car_valid_path + instance1.m_randomizer.Int32(256u) >> 8 == 0)
                    {
                        if (instance1.m_randomizer.Int32(128u) == 0)
                        {
                            DebugLog.LogToFileOnly("outside connection is not good for car in for garbageoffers");
                            int num24 = (int)data.m_garbageBuffer;
                            if (num24 >= 200 && Singleton<SimulationManager>.instance.m_randomizer.Int32(5u) == 0 && Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.Garbage))
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
                        if (num24 >= 200 && Singleton<SimulationManager>.instance.m_randomizer.Int32(5u) == 0 && Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.Garbage))
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
                    int car_valid_path = TickPathfindStatus(ref data.m_teens, ref data.m_serviceProblemTimer);
                    SimulationManager instance1 = Singleton<SimulationManager>.instance;
                    if (car_valid_path + instance1.m_randomizer.Int32(256u) >> 8 == 0)
                    {
                        if (instance1.m_randomizer.Int32(128u) == 0)
                        {
                            DebugLog.LogToFileOnly("outside connection is not good for car out for garbagemoveoffers");
                            if (instance1.m_randomizer.Int32(data.m_garbageBuffer) > 12000)
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
                    else
                    {
                        int num25 = 0;
                        int num26 = 0;
                        int num27 = 0;
                        int num28 = 0;
                        this.CalculateOwnVehicles(buildingID, ref data, TransferManager.TransferReason.GarbageMove, ref num25, ref num26, ref num27, ref num28);
                        if (num25 < 100)
                        {
                            if ((instance1.m_randomizer.Int32(20) > 12) || (!comm_data.garbage_task))
                            {
                                if (instance1.m_randomizer.Int32(data.m_garbageBuffer) > 12000)
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
            }
        }

        public void Adddeadoffers(ushort buildingID, ref Building data)
        {
            TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
            if (have_cemetry_building && comm_data.dead_connection && Singleton<UnlockManager>.instance.Unlocked(UnlockManager.Feature.DeathCare))
            {
                int car_valid_path = TickPathfindStatus(ref data.m_teens, ref data.m_serviceProblemTimer);
                SimulationManager instance1 = Singleton<SimulationManager>.instance;
                if (car_valid_path + instance1.m_randomizer.Int32(256u) >> 8 == 0)
                {
                    if (instance1.m_randomizer.Int32(128u) == 0)
                    {
                        DebugLog.LogToFileOnly("outside connection is not good for car out for deadmoveoffers");
                        if (data.m_customBuffer1 > 10)
                        {
                            offer = default(TransferManager.TransferOffer);
                            offer.Priority = 1 + data.m_customBuffer1 / 5;
                            if (offer.Priority > 7)
                            {
                                offer.Priority = 7;
                            }
                            offer.Building = buildingID;
                            offer.Position = data.m_position;
                            offer.Amount = 1;
                            offer.Active = true;
                            Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.DeadMove, offer);
                        }
                    }
                } else
                {
                    if (data.m_customBuffer1 > 10)
                    {
                        int num25 = 0;
                        int num26 = 0;
                        int num27 = 0;
                        int num28 = 0;
                        this.CalculateOwnVehicles(buildingID, ref data, TransferManager.TransferReason.GarbageMove, ref num25, ref num26, ref num27, ref num28);
                        if (num25 < 100)
                        {
                            if (instance1.m_randomizer.Int32(20) > 12 || (!comm_data.dead_task))
                            {
                                offer = default(TransferManager.TransferOffer);
                                offer.Priority = 1 + data.m_customBuffer1 / 5;
                                if (offer.Priority > 7)
                                {
                                    offer.Priority = 7;
                                }
                                offer.Building = buildingID;
                                offer.Position = data.m_position;
                                offer.Amount = 1;
                                offer.Active = true;
                                Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.DeadMove, offer);
                            }
                        }
                    }
                }
            }
        }

        public void Addpoliceoffers(ushort buildingID, ref Building data)
        {
            TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
            if (have_police_building && comm_data.crime_connection && Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.PoliceDepartment))
            {
                int num25 = 0;
                int num26 = 0;
                int num27 = 0;
                int num28 = 0;
                this.CalculateGuestVehicles(buildingID, ref data, TransferManager.TransferReason.Crime, ref num25, ref num26, ref num27, ref num28);
                //DebugLog.LogToFileOnly("Addpoliceoffers " + data.m_crimeBuffer.ToString() + " " + num27.ToString() + " " + num26.ToString());
                int car_valid_path = TickPathfindStatus(ref data.m_education3, ref data.m_adults);
                SimulationManager instance1 = Singleton<SimulationManager>.instance;
                if (car_valid_path + instance1.m_randomizer.Int32(256u) >> 8 == 0)
                {
                    if (instance1.m_randomizer.Int32(128u) == 0)
                    {
                        DebugLog.LogToFileOnly("outside connection is not good for car in for crimeoffers");
                        if ((data.m_crimeBuffer - (num27 - num26) * 100) > 200)
                        {
                            offer = default(TransferManager.TransferOffer);
                            offer.Priority = 1 + (data.m_crimeBuffer - (num27 - num26) * 100) / 5;
                            if (offer.Priority > 7)
                            {
                                offer.Priority = 7;
                            }
                            offer.Building = buildingID;
                            offer.Position = data.m_position;
                            offer.Amount = 1;
                            offer.Active = false;
                            Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Crime, offer);
                        }
                    }
                }
                else
                {
                    if ((data.m_crimeBuffer - (num27 - num26) * 100) > 200)
                    {
                        offer = default(TransferManager.TransferOffer);
                        offer.Priority = 1 + (data.m_crimeBuffer - (num27 - num26) * 100) / 5;
                        if (offer.Priority > 7)
                        {
                            offer.Priority = 7;
                        }
                        offer.Building = buildingID;
                        offer.Position = data.m_position;
                        offer.Amount = 1;
                        offer.Active = false;
                        Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Crime, offer);
                    }
                }
            }
        }

        public void Addroadoffers(ushort buildingID, ref Building data)
        {
            TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
            if (have_maintain_road_building && comm_data.road_connection)
            {
                int num25 = 0;
                int num26 = 0;
                int num27 = 0;
                int num28 = 0;
                this.CalculateGuestVehicles(buildingID, ref data, TransferManager.TransferReason.RoadMaintenance, ref num25, ref num26, ref num27, ref num28);
                //DebugLog.LogToFileOnly("Road offers " + data.m_waterBuffer.ToString() + " " + num27.ToString() + " " + num26.ToString());
                int car_valid_path = TickPathfindStatus(ref data.m_education3, ref data.m_adults);
                SimulationManager instance1 = Singleton<SimulationManager>.instance;
                if (car_valid_path + instance1.m_randomizer.Int32(256u) >> 8 == 0)
                {
                    if (instance1.m_randomizer.Int32(128u) == 0)
                    {
                        DebugLog.LogToFileOnly("outside connection is not good for car in for roadoffers");
                        if ((data.m_waterBuffer - (num27 - num26) * 100) > 200)
                        {
                            offer = default(TransferManager.TransferOffer);
                            offer.Priority = 1 + (data.m_waterBuffer - (num27 - num26) * 100) / 5;
                            if (offer.Priority > 7)
                            {
                                offer.Priority = 7;
                            }
                            offer.Building = buildingID;
                            offer.Position = data.m_position;
                            offer.Amount = 1;
                            offer.Active = false;
                            Singleton<TransferManager>.instance.AddIncomingOffer(TransferManager.TransferReason.RoadMaintenance, offer);
                        }
                    }
                } else
                {
                    if ((data.m_waterBuffer - (num27 - num26) * 100) > 200)
                    {
                        offer = default(TransferManager.TransferOffer);
                        offer.Priority = 1 + (data.m_waterBuffer - (num27 - num26) * 100) / 5;
                        if (offer.Priority > 7)
                        {
                            offer.Priority = 7;
                        }
                        offer.Building = buildingID;
                        offer.Position = data.m_position;
                        offer.Amount = 1;
                        offer.Active = false;
                        Singleton<TransferManager>.instance.AddIncomingOffer(TransferManager.TransferReason.RoadMaintenance, offer);
                    }
                }
            }
        }

        public void Addfireoffers(ushort buildingID, ref Building data)
        {
            TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
            if (have_fire_building && comm_data.fire_connection && Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.FireDepartment))
            {
                int num25 = 0;
                int num26 = 0;
                int num27 = 0;
                int num28 = 0;
                this.CalculateGuestVehicles(buildingID, ref data, TransferManager.TransferReason.Fire, ref num25, ref num26, ref num27, ref num28);
                //DebugLog.LogToFileOnly("Addfireoffers " + data.m_electricityBuffer.ToString() + " " + num27.ToString() + " " + num26.ToString());
                int car_valid_path = TickPathfindStatus(ref data.m_education3, ref data.m_adults);
                SimulationManager instance1 = Singleton<SimulationManager>.instance;
                if (car_valid_path + instance1.m_randomizer.Int32(256u) >> 8 == 0)
                {
                    if (instance1.m_randomizer.Int32(128u) == 0)
                    {
                        DebugLog.LogToFileOnly("outside connection is not good for car in for fireoffers");
                        if ((data.m_electricityBuffer - (num27 - num26) * 100) > 200)
                        {
                            offer = default(TransferManager.TransferOffer);
                            offer.Priority = 1 + (data.m_electricityBuffer - (num27 - num26) * 100) / 5;
                            if (offer.Priority > 7)
                            {
                                offer.Priority = 7;
                            }
                            offer.Building = buildingID;
                            offer.Position = data.m_position;
                            offer.Amount = 1;
                            offer.Active = false;
                            Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Fire, offer);
                        }
                    }
                } else
                {
                    if ((data.m_electricityBuffer - (num27 - num26) * 100) > 200)
                    {
                        offer = default(TransferManager.TransferOffer);
                        offer.Priority = 1 + (data.m_electricityBuffer - (num27 - num26) * 100) / 5;
                        if (offer.Priority > 7)
                        {
                            offer.Priority = 7;
                        }
                        offer.Building = buildingID;
                        offer.Position = data.m_position;
                        offer.Amount = 1;
                        offer.Active = false;
                        Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Fire, offer);
                    }
                }
            }
        }

        public void Addsickoffers(ushort buildingID, ref Building data)
        {
            TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
            if (have_hospital_building && comm_data.sick_connection && Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.HealthCare))
            {
                int num25 = 0;
                int num26 = 0;
                int num27 = 0;
                int num28 = 0;
                this.CalculateGuestVehicles(buildingID, ref data, TransferManager.TransferReason.Sick, ref num25, ref num26, ref num27, ref num28);
                //DebugLog.LogToFileOnly("Addsickoffers " + data.m_customBuffer2.ToString() + " " + num27.ToString() + " " + num26.ToString());
                int car_valid_path = TickPathfindStatus(ref data.m_education3, ref data.m_adults);
                SimulationManager instance1 = Singleton<SimulationManager>.instance;
                if (car_valid_path + instance1.m_randomizer.Int32(256u) >> 8 == 0)
                {
                    if (instance1.m_randomizer.Int32(128u) == 0)
                    {
                        DebugLog.LogToFileOnly("outside connection is not good for car in for sickoffers");
                        if ((data.m_customBuffer2 - (num27 - num26) * 100) > 200)
                        {
                            offer = default(TransferManager.TransferOffer);
                            offer.Priority = 1 + (data.m_customBuffer2 - (num27 - num26) * 100) / 5;
                            if (offer.Priority > 7)
                            {
                                offer.Priority = 7;
                            }
                            offer.Building = buildingID;
                            offer.Position = data.m_position;
                            offer.Amount = 1;
                            offer.Active = false;
                            Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Sick, offer);
                        }
                    }
                } else
                {
                    if ((data.m_customBuffer2 - (num27 - num26) * 100) > 100)
                    {
                        offer = default(TransferManager.TransferOffer);
                        offer.Priority = 1 + (data.m_customBuffer2 - (num27 - num26) * 200) / 5;
                        if (offer.Priority > 7)
                        {
                            offer.Priority = 7;
                        }
                        offer.Building = buildingID;
                        offer.Position = data.m_position;
                        offer.Amount = 1;
                        offer.Active = false;
                        Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Sick, offer);
                    }
                }
            }
        }

        public void Addotherconnectionoffers(ushort buildingID, ref Building data)
        {
            System.Random rand = new System.Random();

            //gabarge
            if (data.Info.m_class.m_service == ItemClass.Service.Road)
            {
                Addgarbageoffers(buildingID, ref data);
                if ((data.m_flags & Building.Flags.IncomingOutgoing) == Building.Flags.Incoming)
                {
                    Addpoliceoffers(buildingID, ref data);
                    Addfireoffers(buildingID, ref data);
                    Addsickoffers(buildingID, ref data);
                    Addroadoffers(buildingID, ref data);
                } else
                {
                    Adddeadoffers(buildingID, ref data);
                }
            }
        }


        public override void StartTransfer(ushort buildingID, ref Building data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (material == TransferManager.TransferReason.GarbageMove)
            {
                //DebugLog.LogToFileOnly("starttransfer gabarge from outside to city");
                VehicleInfo randomVehicleInfo2 = Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref Singleton<SimulationManager>.instance.m_randomizer, ItemClass.Service.Garbage, ItemClass.SubService.None, ItemClass.Level.Level1);
                if (randomVehicleInfo2 != null)
                {
                    Array16<Vehicle> vehicles2 = Singleton<VehicleManager>.instance.m_vehicles;
                    ushort num2;
                    if (Singleton<VehicleManager>.instance.CreateVehicle(out num2, ref Singleton<SimulationManager>.instance.m_randomizer, randomVehicleInfo2, data.m_position, TransferManager.TransferReason.GarbageMove, false, true))
                    {
                        randomVehicleInfo2.m_vehicleAI.SetSource(num2, ref vehicles2.m_buffer[(int)num2], buildingID);
                        randomVehicleInfo2.m_vehicleAI.StartTransfer(num2, ref vehicles2.m_buffer[(int)num2], TransferManager.TransferReason.GarbageMove, offer);
                        if (comm_data.garbage_task)
                        {
                            vehicles2.m_buffer[num2].m_flags &= (~Vehicle.Flags.Importing);
                        }
                        else
                        {
                            vehicles2.m_buffer[num2].m_flags |= (Vehicle.Flags.Importing);
                        }
                    }
                }
            }
            else if (material == TransferManager.TransferReason.DeadMove)
            {
                VehicleInfo randomVehicleInfo2 = Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref Singleton<SimulationManager>.instance.m_randomizer, ItemClass.Service.HealthCare, ItemClass.SubService.None, ItemClass.Level.Level2);
                if (randomVehicleInfo2 != null)
                {
                    Array16<Vehicle> vehicles2 = Singleton<VehicleManager>.instance.m_vehicles;
                    ushort num2;
                    if (Singleton<VehicleManager>.instance.CreateVehicle(out num2, ref Singleton<SimulationManager>.instance.m_randomizer, randomVehicleInfo2, data.m_position, material, false, true))
                    {
                        //DebugLog.LogToFileOnly("try transfer deadmove to city, itemclass = " + Singleton<BuildingManager>.instance.m_buildings.m_buffer[offer.Building].Info.m_class.m_service.ToString() + Singleton<BuildingManager>.instance.m_buildings.m_buffer[offer.Building].Info.m_class.m_subService.ToString() + Singleton<BuildingManager>.instance.m_buildings.m_buffer[offer.Building].Info.m_class.m_level.ToString());
                        randomVehicleInfo2.m_vehicleAI.SetSource(num2, ref vehicles2.m_buffer[(int)num2], buildingID);
                        randomVehicleInfo2.m_vehicleAI.StartTransfer(num2, ref vehicles2.m_buffer[(int)num2], material, offer);
                        if (comm_data.dead_task)
                        {
                            vehicles2.m_buffer[num2].m_flags &= (~Vehicle.Flags.Importing);
                        }
                        else
                        {
                            vehicles2.m_buffer[num2].m_flags |= (Vehicle.Flags.Importing);
                        }
                    }
                }
            }
            else if (material == TransferManager.TransferReason.Crime)
            {
                VehicleInfo randomVehicleInfo = Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref Singleton<SimulationManager>.instance.m_randomizer, ItemClass.Service.PoliceDepartment, ItemClass.SubService.None, ItemClass.Level.Level1);
                if (randomVehicleInfo != null)
                {
                    Array16<Vehicle> vehicles = Singleton<VehicleManager>.instance.m_vehicles;
                    ushort num;
                    //DebugLog.LogToFileOnly("try help fire for city");
                    if (Singleton<VehicleManager>.instance.CreateVehicle(out num, ref Singleton<SimulationManager>.instance.m_randomizer, randomVehicleInfo, data.m_position, material, true, false))
                    {
                        randomVehicleInfo.m_vehicleAI.SetSource(num, ref vehicles.m_buffer[(int)num], buildingID);
                        randomVehicleInfo.m_vehicleAI.StartTransfer(num, ref vehicles.m_buffer[(int)num], material, offer);
                    }
                }
            }
            else if (material == TransferManager.TransferReason.Fire)
            {
                VehicleInfo randomVehicleInfo = Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref Singleton<SimulationManager>.instance.m_randomizer, ItemClass.Service.FireDepartment, ItemClass.SubService.None, ItemClass.Level.Level1);
                if (randomVehicleInfo != null)
                {
                    Array16<Vehicle> vehicles = Singleton<VehicleManager>.instance.m_vehicles;
                    ushort num;
                    //DebugLog.LogToFileOnly("try help fire for city");
                    if (Singleton<VehicleManager>.instance.CreateVehicle(out num, ref Singleton<SimulationManager>.instance.m_randomizer, randomVehicleInfo, data.m_position, material, true, false))
                    {
                        randomVehicleInfo.m_vehicleAI.SetSource(num, ref vehicles.m_buffer[(int)num], buildingID);
                        randomVehicleInfo.m_vehicleAI.StartTransfer(num, ref vehicles.m_buffer[(int)num], material, offer);
                    }
                }
            }
            else if (material == TransferManager.TransferReason.Sick)
            {
                VehicleInfo randomVehicleInfo = Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref Singleton<SimulationManager>.instance.m_randomizer, ItemClass.Service.HealthCare, ItemClass.SubService.None, ItemClass.Level.Level1);
                if (randomVehicleInfo != null)
                {
                    Array16<Vehicle> vehicles = Singleton<VehicleManager>.instance.m_vehicles;
                    ushort num;
                    //DebugLog.LogToFileOnly("try transfer patient outof city");
                    if (Singleton<VehicleManager>.instance.CreateVehicle(out num, ref Singleton<SimulationManager>.instance.m_randomizer, randomVehicleInfo, data.m_position, material, true, false))
                    {
                        randomVehicleInfo.m_vehicleAI.SetSource(num, ref vehicles.m_buffer[(int)num], buildingID);
                        randomVehicleInfo.m_vehicleAI.StartTransfer(num, ref vehicles.m_buffer[(int)num], material, offer);
                    }
                }
            }
            else if (material == TransferManager.TransferReason.Family3)
            {
            }
            else if (material == TransferManager.TransferReason.Single3B)
            {
            }
            else if (material == TransferManager.TransferReason.Single3)
            {
            }
            else
            {
                if (!OutsideConnectionAI.StartConnectionTransfer(buildingID, ref data, material, offer, m_touristFactor0, m_touristFactor1, m_touristFactor2))
                {
                    base.StartTransfer(buildingID, ref data, material, offer);
                }
            }
        }


        public override void ModifyMaterialBuffer(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            if ((data.m_flags & Building.Flags.IncomingOutgoing) == Building.Flags.Incoming)
            {
                if (material == TransferManager.TransferReason.Garbage)
                {
                    //DebugLog.LogToFileOnly("starttransfer gabarge from outside to city, gather gabage");
                    if (data.m_garbageBuffer < 0)
                    {
                        DebugLog.LogToFileOnly("garbarge < 0 in outside building, should be wrong");
                        amountDelta = 0;
                    }
                    else
                    {
                        if (data.m_garbageBuffer + amountDelta <= 0)
                        {
                            amountDelta = -data.m_garbageBuffer;
                        }
                        else
                        {

                        }
                        data.m_garbageBuffer = (ushort)(data.m_garbageBuffer + amountDelta);
                        if (!comm_data.garbage_task)
                        {
                            comm_data.building_money[buildingID] += (amountDelta * -7f / 100f);
                            Singleton<EconomyManager>.instance.AddPrivateIncome((int)(amountDelta * -7f), ItemClass.Service.Garbage, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                        }
                    }
                }
                else if (material == TransferManager.TransferReason.Crime)
                {
                    if (amountDelta < 0)
                    {
                        if (data.m_crimeBuffer < 0)
                        {
                            DebugLog.LogToFileOnly("crime < 0 in outside building, should be wrong");
                            amountDelta = 0;
                        }
                        else
                        {
                            if (data.m_crimeBuffer + amountDelta * 100 <= 0)
                            {
                                amountDelta = -data.m_crimeBuffer / 100;
                            }
                            else
                            {

                            }
                            data.m_crimeBuffer = (ushort)(data.m_crimeBuffer + amountDelta * 100);
                            comm_data.building_money[buildingID] += amountDelta * -70000f / 100f;
                            Singleton<EconomyManager>.instance.AddPrivateIncome((int)(amountDelta * -70000f), ItemClass.Service.PoliceDepartment, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                        }
                    }
                }
                else if (material == TransferManager.TransferReason.Sick)
                {
                    if (amountDelta < 0)
                    {
                        if (data.m_customBuffer2 < 0)
                        {
                            DebugLog.LogToFileOnly("sick < 0 in outside building, should be wrong");
                            amountDelta = 0;
                        }
                        else
                        {
                            if (data.m_customBuffer2 + amountDelta * 100 <= 0)
                            {
                                amountDelta = -data.m_customBuffer2 / 100;
                            }
                            else
                            {

                            }
                            data.m_customBuffer2 = (ushort)(data.m_customBuffer2 + amountDelta * 100);
                            comm_data.building_money[buildingID] += amountDelta * -70000f / 100f;
                            Singleton<EconomyManager>.instance.AddPrivateIncome((int)(amountDelta * -70000f), ItemClass.Service.HealthCare, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                        }
                    }
                }
                else if (material == TransferManager.TransferReason.Fire)
                {
                    if (data.m_electricityBuffer < 0)
                    {
                        DebugLog.LogToFileOnly("fire < 0 in outside building, should be wrong");
                        amountDelta = 0;
                    }
                    else
                    {
                        if (data.m_electricityBuffer + amountDelta * 100 <= 0)
                        {
                            amountDelta = -data.m_electricityBuffer / 100;
                        }
                        else
                        {

                        }
                        data.m_electricityBuffer = (ushort)(data.m_electricityBuffer + amountDelta * 100);
                        comm_data.building_money[buildingID] += amountDelta * -70000f / 100f;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)(amountDelta * -70000f), ItemClass.Service.FireDepartment, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                    }
                }
                else if (material == TransferManager.TransferReason.RoadMaintenance)
                {
                    if (data.m_waterBuffer < 0)
                    {
                        DebugLog.LogToFileOnly("fire < 0 in outside building, should be wrong");
                        amountDelta = 0;
                    }
                    else
                    {
                        if (data.m_waterBuffer + amountDelta * 100 <= 0)
                        {
                            amountDelta = -data.m_waterBuffer / 100;
                        }
                        else
                        {

                        }
                        //DebugLog.LogToFileOnly("find outside maintain num = " + amountDelta.ToString());
                        data.m_waterBuffer = (ushort)(data.m_waterBuffer + amountDelta * 100);
                        comm_data.building_money[buildingID] += amountDelta * -7000f / 100f;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)(amountDelta * -7000f), ItemClass.Service.Road, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                    }
                }
                else
                {
                    //amountDelta = 0;
                }
            }
            else
            {
                if (material == TransferManager.TransferReason.GarbageMove)
                {
                    //DebugLog.LogToFileOnly("starttransfer gabarge from outside to city, gather gabage");
                    if (data.m_garbageBuffer < 0)
                    {
                        DebugLog.LogToFileOnly("garbarge < 0 in outside building, should be wrong");
                        amountDelta = 0;
                    }
                    else
                    {
                        if (data.m_garbageBuffer + amountDelta <= 0)
                        {
                            amountDelta = -data.m_garbageBuffer;
                        }
                        else
                        {

                        }
                        data.m_garbageBuffer = (ushort)(data.m_garbageBuffer + amountDelta);
                    }
                }
                else if (material == TransferManager.TransferReason.DeadMove)
                {
                    if (data.m_customBuffer1 <= 0)
                    {
                        DebugLog.LogToFileOnly("dead <= 0 in outside building, should be wrong");
                        amountDelta = 0;
                    }
                    else
                    {
                        if (data.m_customBuffer1 + amountDelta <= 0)
                        {
                            amountDelta = -data.m_customBuffer1;
                        }
                        else
                        {

                        }
                        data.m_customBuffer1 = (ushort)(data.m_customBuffer1 + amountDelta);
                    }
                }
                else if (material == TransferManager.TransferReason.Garbage)
                {
                    //DebugLog.LogToFileOnly("starttransfer gabarge from outside to city, gather gabage");
                    amountDelta = 0;
                    /*if (data.m_garbageBuffer < 0)
                    {
                        DebugLog.LogToFileOnly("garbarge < 0 in outside building, should be wrong");
                        amountDelta = 0;
                    }
                    else
                    {
                        if (data.m_garbageBuffer + amountDelta <= 0)
                        {
                            amountDelta = -data.m_garbageBuffer;
                        }
                        else
                        {

                        }
                        data.m_garbageBuffer = (ushort)(data.m_garbageBuffer + amountDelta);
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)(amountDelta * -0.1f), ItemClass.Service.Garbage, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                    }*/
                }
                else
                {
                    //do nothing
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

        protected void CalculateOwnVehicles(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int count, ref int cargo, ref int capacity, ref int outside)
        {
            VehicleManager instance = Singleton<VehicleManager>.instance;
            ushort num = data.m_ownVehicles;
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
                num = instance.m_vehicles.m_buffer[(int)num].m_nextOwnVehicle;
                if (++num2 > 16384)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }
        }






        protected void ProduceHospitalGoods(ushort buildingID, ushort buildingID1, ref Building buildingData, ref Building buildingData1)
        {
            int num = 100 * m_healthCareAccumulation / 200;
            if (num != 0)
            {
                Singleton<ImmaterialResourceManager>.instance.AddResource(ImmaterialResourceManager.Resource.HealthCare, num, buildingData1.m_position, m_healthCareRadius);
            }
            int curingRate = m_curingRate;
            int num2 = (curingRate * 100 * 100 + m_patientCapacity - 1) / m_patientCapacity;
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint num3 = buildingData.m_citizenUnits;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            while (num3 != 0u)
            {
                uint nextUnit = instance.m_units.m_buffer[(int)((UIntPtr)num3)].m_nextUnit;
                if ((ushort)(instance.m_units.m_buffer[(int)((UIntPtr)num3)].m_flags & CitizenUnit.Flags.Visit) != 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        uint citizen = instance.m_units.m_buffer[(int)((UIntPtr)num3)].GetCitizen(i);
                        if (citizen != 0u)
                        {
                            if (instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Dead)
                            {
                                if (instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].CurrentLocation == Citizen.Location.Visit)
                                {
                                    instance.ReleaseCitizen(citizen);
                                }
                            }
                            else if (instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Sick)
                            {
                                if (instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].CurrentLocation == Citizen.Location.Visit)
                                {
                                    if (Singleton<SimulationManager>.instance.m_randomizer.Int32(10000u) < num2 && Singleton<SimulationManager>.instance.m_randomizer.Int32(32u) == 0)
                                    {
                                        instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Sick = false;
                                        instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_visitBuilding = buildingID1;
                                        //DebugLog.LogToFileOnly("sick man is ok now, switch to buildingID1 to let him go");
                                        num6++;
                                    }
                                    else
                                    {
                                        num5++;
                                    }
                                }
                                else
                                {
                                    num5++;
                                }
                            }
                        }
                    }
                }
                num3 = nextUnit;
                if (++num4 > 524288)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }
            comm_data.outside_patient += num5;
            buildingData.m_tempExport = (byte)Mathf.Min((int)buildingData.m_tempExport + num6, 255);
            DistrictManager instance2 = Singleton<DistrictManager>.instance;
            byte district = instance2.GetDistrict(buildingData.m_position);
            District[] expr_26A_cp_0_cp_0 = instance2.m_districts.m_buffer;
            byte expr_26A_cp_0_cp_1 = district;
            expr_26A_cp_0_cp_0[(int)expr_26A_cp_0_cp_1].m_productionData.m_tempHealCapacity = expr_26A_cp_0_cp_0[(int)expr_26A_cp_0_cp_1].m_productionData.m_tempHealCapacity + (uint)m_patientCapacity;
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            int num10 = 0;
            this.CalculateOwnVehicles(buildingID, ref buildingData, TransferManager.TransferReason.Sick, ref num7, ref num8, ref num9, ref num10);

            int num17 = 0;
            int num18 = 0;
            int num19 = 0;
            int num20 = 0;
            this.CalculateOwnVehicles(buildingID1, ref buildingData1, TransferManager.TransferReason.Sick, ref num17, ref num18, ref num19, ref num20);

            comm_data.outside_ambulance_car += (ushort)(num7 + num17);

            int num11 = m_patientCapacity - num5 - num9 - num19;
            int num12 = (100 * m_ambulanceCount + 99) / 100;

            SimulationManager instance1 = Singleton<SimulationManager>.instance;
            int car_valid_path = TickPathfindStatus(ref buildingData1.m_teens, ref buildingData1.m_serviceProblemTimer);
            int human_valid_path = TickPathfindStatus(ref buildingData.m_workerProblemTimer, ref buildingData.m_taxProblemTimer);

            if (comm_data.hospitalhelp && Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.HealthCare))
            {
                if (num11 >= 1)
                {
                    TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                    offer.Position = buildingData.m_position;
                    if (car_valid_path + instance1.m_randomizer.Int32(256u) >> 8 == 0)
                    {
                        if (instance1.m_randomizer.Int32(128u) == 0)
                        {
                            DebugLog.LogToFileOnly("outside connection is not good for car out for ProducehospitalGoods");
                            if ((num7 + num17) < num12)
                            {
                                offer.Building = buildingID1;
                                offer.Priority = 7;
                                offer.Amount = Mathf.Min(num11, num12 - num7 - num17);
                                offer.Active = true;
                            }
                        }
                    }
                    else
                    {
                        if ((num7 + num17) < num12)
                        {
                            offer.Building = buildingID1;
                            offer.Priority = 7;
                            offer.Amount = Mathf.Min(num11, num12 - num7 - num17);
                            offer.Active = true;
                        }
                    }

                    if (human_valid_path + instance1.m_randomizer.Int32(256u) >> 8 == 0)
                    {
                        if (instance1.m_randomizer.Int32(128u) == 0)
                        {
                            DebugLog.LogToFileOnly("outside connection is not good for human in for ProducehospitalGoods");
                            if ((num7 + num17) >= num12)
                            {
                                offer.Building = buildingID;
                                offer.Priority = 1;
                                offer.Amount = num11;
                                offer.Active = false;
                            }
                        }
                    }
                    else
                    {
                        if ((num7 + num17) >= num12)
                        {
                            offer.Building = buildingID;
                            offer.Priority = 1;
                            offer.Amount = num11;
                            offer.Active = false;
                        }
                    }
                    Singleton<TransferManager>.instance.AddIncomingOffer(TransferManager.TransferReason.Sick, offer);
                }
            }
        }



        // FireStationAI
        /*protected void ProduceFireGoods(ushort buildingID, ushort buildingID1, ref Building buildingData, ref Building buildingData1)
        {
            int num = 100 * m_fireDepartmentAccumulation / 100;
            if (num != 0)
            {
                Singleton<ImmaterialResourceManager>.instance.AddResource(ImmaterialResourceManager.Resource.FireDepartment, num, buildingData1.m_position, m_fireDepartmentRadius);
            }
            //base.HandleDead(buildingID, ref buildingData, ref behaviour, totalWorkerCount);
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            this.CalculateOwnVehicles(buildingID, ref buildingData, TransferManager.TransferReason.Fire, ref num2, ref num3, ref num4, ref num5);

            int num12 = 0;
            int num13 = 0;
            int num14 = 0;
            int num15 = 0;
            this.CalculateOwnVehicles(buildingID1, ref buildingData1, TransferManager.TransferReason.Fire, ref num12, ref num13, ref num14, ref num15);

            int num6 = (100 * m_fireTruckCount + 99) / 100;

            //comm_data.firetruck += (ushort)(num2 + num12);
            if (comm_data.firehelp)
            {
                if ((num2 + num12) < num6)
                {
                    TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                    offer.Priority = (num6 - num2 - num12) * 3 / num6;
                    offer.Building = buildingID1;
                    offer.Position = buildingData1.m_position;
                    offer.Amount = num6 - num2 - num12;
                    offer.Active = true;
                    Singleton<TransferManager>.instance.AddIncomingOffer(TransferManager.TransferReason.Fire, offer);
                }
            }
        }*/



        protected void ProducePoliceGoods(ushort buildingID, ushort buildingID1, ref Building buildingData, ref Building buildingData1)
        {
            DistrictManager instance = Singleton<DistrictManager>.instance;
            byte district = instance.GetDistrict(buildingData.m_position);
            DistrictPolicies.Services servicePolicies = instance.m_districts.m_buffer[(int)district].m_servicePolicies;
            int num2 = 100 * m_policeDepartmentAccumulation / 100;
            if (num2 != 0)
            {
                Singleton<ImmaterialResourceManager>.instance.AddResource(ImmaterialResourceManager.Resource.PoliceDepartment, num2, buildingData1.m_position, m_policeDepartmentRadius);
            }
            int num4 = m_sentenceWeeks;
            int num5 = 1000000 / Mathf.Max(1, num4 * 16);
            CitizenManager instance2 = Singleton<CitizenManager>.instance;
            uint num6 = buildingData.m_citizenUnits;
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            while (num6 != 0u)
            {
                uint nextUnit = instance2.m_units.m_buffer[(int)((UIntPtr)num6)].m_nextUnit;
                if ((ushort)(instance2.m_units.m_buffer[(int)((UIntPtr)num6)].m_flags & CitizenUnit.Flags.Visit) != 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        uint citizen = instance2.m_units.m_buffer[(int)((UIntPtr)num6)].GetCitizen(i);
                        if (citizen != 0u)
                        {
                            //rush hour bug??
                            if (!instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Sick)
                            {
                                instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Arrested = true;
                            }
                            //DebugLog.LogToFileOnly("citizenID is in jail = " + citizen.ToString() + instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Dead.ToString() + instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Arrested.ToString() + instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].CurrentLocation.ToString());
                            if (!instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Dead && instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Arrested && instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].CurrentLocation == Citizen.Location.Visit)
                            {
                                //DebugLog.LogToFileOnly("citizenID is in jail and Arrested = " + citizen.ToString());
                                if (Singleton<SimulationManager>.instance.m_randomizer.Int32(1000000u) < num5)
                                {
                                    instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Arrested = false;
                                    //DebugLog.LogToFileOnly("crime man is ok now, Evacuating him");
                                    ushort instance3 = instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_instance;
                                    if (instance3 != 0)
                                    {
                                        instance2.ReleaseCitizenInstance(instance3);
                                    }
                                    ushort homeBuilding = instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_homeBuilding;
                                    if (homeBuilding != 0)
                                    {
                                        CitizenInfo citizenInfo = instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].GetCitizenInfo(citizen);
                                        HumanAI humanAI = citizenInfo.m_citizenAI as HumanAI;
                                        if (humanAI != null)
                                        {
                                            Citizen[] expr_310_cp_0 = instance2.m_citizens.m_buffer;
                                            UIntPtr expr_310_cp_1 = (UIntPtr)citizen;
                                            expr_310_cp_0[(int)expr_310_cp_1].m_flags = (expr_310_cp_0[(int)expr_310_cp_1].m_flags & ~Citizen.Flags.Evacuating);
                                            humanAI.StartMoving(citizen, ref instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)], buildingID1, homeBuilding);
                                            //DebugLog.LogToFileOnly("crime man is ok now, switch to buildingID1 to let him go");
                                        }
                                    }
                                    if (instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].CurrentLocation != Citizen.Location.Visit && instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_visitBuilding == buildingID)
                                    {
                                        instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].SetVisitplace(citizen, 0, 0u);
                                    }
                                }
                                num8++;
                            }
                            num7++;
                        }
                    }
                }
                num6 = nextUnit;
                if (++num9 > 524288)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }

            comm_data.outside_crime += num8;
            //comm_data.firetruck += (ushort)num7;
            //DebugLog.LogToFileOnly("comm_data.outside_crime = " + comm_data.outside_crime.ToString());
            if (m_jailCapacity != 0)
            {
                District[] expr_41D_cp_0_cp_0 = instance.m_districts.m_buffer;
                byte expr_41D_cp_0_cp_1 = district;
                expr_41D_cp_0_cp_0[(int)expr_41D_cp_0_cp_1].m_productionData.m_tempCriminalAmount = expr_41D_cp_0_cp_0[(int)expr_41D_cp_0_cp_1].m_productionData.m_tempCriminalAmount + (uint)num8;
                District[] expr_441_cp_0_cp_0 = instance.m_districts.m_buffer;
                byte expr_441_cp_0_cp_1 = district;
                expr_441_cp_0_cp_0[(int)expr_441_cp_0_cp_1].m_productionData.m_tempCriminalCapacity = expr_441_cp_0_cp_0[(int)expr_441_cp_0_cp_1].m_productionData.m_tempCriminalCapacity + (uint)m_jailCapacity;
            }
            int num10 = 0;
            int num11 = 0;
            int num12 = 0;
            int num13 = 0;
            int num14 = 0;
            int num15 = 0;
            int num16 = 0;
            int num17 = 0;

            CalculateOwnVehicles(buildingID, ref buildingData, TransferManager.TransferReason.Crime, ref num10, ref num11, ref num12, ref num13);
            CalculateOwnVehicles(buildingID1, ref buildingData1, TransferManager.TransferReason.Crime, ref num14, ref num15, ref num16, ref num17);


            int num18 = (100 * m_policeCarCount + 99) / 100;
            comm_data.outside_police_car += (ushort)(num10 + num14);


            SimulationManager instance1 = Singleton<SimulationManager>.instance;
            int car_valid_path = TickPathfindStatus(ref buildingData1.m_teens, ref buildingData1.m_serviceProblemTimer);
            if (car_valid_path + instance1.m_randomizer.Int32(256u) >> 8 == 0)
            {
                if (instance1.m_randomizer.Int32(128u) == 0)
                {
                    DebugLog.LogToFileOnly("outside connection is not good for ProducePoliceGoods");
                    if (comm_data.policehelp && Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.PoliceDepartment))
                    {
                        if ((num10 + num14) < num18)
                        {
                            TransferManager.TransferOffer offer2 = default(TransferManager.TransferOffer);
                            offer2.Priority = 2 - num10 - num14;
                            offer2.Building = buildingID1;
                            offer2.Position = buildingData1.m_position;
                            offer2.Amount = 1;
                            offer2.Active = true;
                            Singleton<TransferManager>.instance.AddIncomingOffer(TransferManager.TransferReason.Crime, offer2);
                        }
                    }
                }
            } else
            {
                if (comm_data.policehelp && Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.PoliceDepartment))
                {
                    if ((num10 + num14) < num18)
                    {
                        TransferManager.TransferOffer offer2 = default(TransferManager.TransferOffer);
                        offer2.Priority = 2 - num10 - num14;
                        offer2.Building = buildingID1;
                        offer2.Position = buildingData1.m_position;
                        offer2.Amount = 1;
                        offer2.Active = true;
                        Singleton<TransferManager>.instance.AddIncomingOffer(TransferManager.TransferReason.Crime, offer2);
                    }
                }
            }
        }


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
    }
}
