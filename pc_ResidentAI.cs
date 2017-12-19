using System;
using ColossalFramework;
using UnityEngine;
using ColossalFramework.Math;
using System.Reflection;
using System.Text.RegularExpressions;

namespace RealCity
{
    public class pc_ResidentAI : ResidentAI
    {
        public static uint precitizenid = 0;
        public static int family_count = 0;
        public static uint family_very_profit_money_num = 0;
        public static uint family_profit_money_num = 0;
        public static uint family_loss_money_num = 0;
        public static int citizen_salary_count = 0;
        public static int citizen_expense_count = 0;
        public static int citizen_salary_tax_total = 0;
        public static float temp_citizen_salary_tax_total = 0f;
        //public static bool citizen_process_done = false;
        //govement salary outconme
        public static int Road = 0;
        public static int Electricity = 0;
        public static int Water = 0;
        public static int Beautification = 0;
        public static int Garbage = 0;
        public static int HealthCare = 0;
        public static int PoliceDepartment = 0;
        public static int Education = 0;
        public static int Monument = 0;
        public static int FireDepartment = 0;
        public static int PublicTransport_bus = 0;
        public static int PublicTransport_tram = 0;
        public static int PublicTransport_ship = 0;
        public static int PublicTransport_plane = 0;
        public static int PublicTransport_metro = 0;
        public static int PublicTransport_train = 0;
        public static int PublicTransport_taxi = 0;
        public static int PublicTransport_cablecar = 0;
        public static int PublicTransport_monorail = 0;
        public static int Disaster = 0;

        public static uint family_weight_stable_high = 0;
        public static uint family_weight_stable_low = 0;

        public static long citizen_goods_temp = 0;
        public static long citizen_goods = 0;

        public static byte[] save_data = new byte[140];
        //public static byte[] save_data = new byte[140];

        public static void load()
        {
            int i = 0;
            precitizenid = saveandrestore.load_uint(ref i, save_data);
            family_count = saveandrestore.load_int(ref i, save_data);
            family_very_profit_money_num = saveandrestore.load_uint(ref i, save_data);
            family_profit_money_num = saveandrestore.load_uint(ref i, save_data);
            family_loss_money_num = saveandrestore.load_uint(ref i, save_data);
            citizen_salary_count = saveandrestore.load_int(ref i, save_data);
            citizen_expense_count = saveandrestore.load_int(ref i, save_data);
            citizen_salary_tax_total = saveandrestore.load_int(ref i, save_data);
            temp_citizen_salary_tax_total = saveandrestore.load_float(ref i, save_data);

            Road = saveandrestore.load_int(ref i, save_data);
            Electricity = saveandrestore.load_int(ref i, save_data);
            Water = saveandrestore.load_int(ref i, save_data);
            Beautification = saveandrestore.load_int(ref i, save_data);
            Garbage = saveandrestore.load_int(ref i, save_data);
            HealthCare = saveandrestore.load_int(ref i, save_data);
            PoliceDepartment = saveandrestore.load_int(ref i, save_data);
            Education = saveandrestore.load_int(ref i, save_data);
            Monument = saveandrestore.load_int(ref i, save_data);
            FireDepartment = saveandrestore.load_int(ref i, save_data);
            PublicTransport_bus = saveandrestore.load_int(ref i, save_data);
            PublicTransport_tram = saveandrestore.load_int(ref i, save_data);
            PublicTransport_ship = saveandrestore.load_int(ref i, save_data);
            PublicTransport_plane = saveandrestore.load_int(ref i, save_data);
            PublicTransport_metro = saveandrestore.load_int(ref i, save_data);
            PublicTransport_train = saveandrestore.load_int(ref i, save_data);
            PublicTransport_taxi = saveandrestore.load_int(ref i, save_data);
            PublicTransport_cablecar = saveandrestore.load_int(ref i, save_data);
            PublicTransport_monorail = saveandrestore.load_int(ref i, save_data);
            Disaster = saveandrestore.load_int(ref i, save_data);

            family_weight_stable_high = saveandrestore.load_uint(ref i, save_data);
            family_weight_stable_low = saveandrestore.load_uint(ref i, save_data);

            citizen_goods = saveandrestore.load_long(ref i, save_data);
            citizen_goods_temp = saveandrestore.load_long(ref i, save_data);

            DebugLog.LogToFileOnly("save_data in residentAI is " + i.ToString());
        }

        public static void save()
        {
            int i = 0;

            //2*4 + 3*4 + 4*4 = 36
            saveandrestore.save_uint(ref i, precitizenid, ref save_data);
            saveandrestore.save_int(ref i, family_count, ref save_data);
            saveandrestore.save_uint(ref i, family_very_profit_money_num, ref save_data);
            saveandrestore.save_uint(ref i, family_profit_money_num, ref save_data);
            saveandrestore.save_uint(ref i, family_loss_money_num, ref save_data);
            saveandrestore.save_int(ref i, citizen_salary_count, ref save_data);
            saveandrestore.save_int(ref i, citizen_expense_count, ref save_data);
            saveandrestore.save_int(ref i, citizen_salary_tax_total, ref save_data);
            saveandrestore.save_float(ref i, temp_citizen_salary_tax_total, ref save_data);

            //20 * 4 = 80
            saveandrestore.save_int(ref i, Road, ref save_data);
            saveandrestore.save_int(ref i, Electricity, ref save_data);
            saveandrestore.save_int(ref i, Water, ref save_data);
            saveandrestore.save_int(ref i, Beautification, ref save_data);
            saveandrestore.save_int(ref i, Garbage, ref save_data);
            saveandrestore.save_int(ref i, HealthCare, ref save_data);
            saveandrestore.save_int(ref i, PoliceDepartment, ref save_data);
            saveandrestore.save_int(ref i, Education, ref save_data);
            saveandrestore.save_int(ref i, Monument, ref save_data);
            saveandrestore.save_int(ref i, FireDepartment, ref save_data);
            saveandrestore.save_int(ref i, PublicTransport_bus, ref save_data);
            saveandrestore.save_int(ref i, PublicTransport_tram, ref save_data);
            saveandrestore.save_int(ref i, PublicTransport_ship, ref save_data);
            saveandrestore.save_int(ref i, PublicTransport_plane, ref save_data);
            saveandrestore.save_int(ref i, PublicTransport_metro, ref save_data);
            saveandrestore.save_int(ref i, PublicTransport_train, ref save_data);
            saveandrestore.save_int(ref i, PublicTransport_taxi, ref save_data);
            saveandrestore.save_int(ref i, PublicTransport_cablecar, ref save_data);
            saveandrestore.save_int(ref i, PublicTransport_monorail, ref save_data);
            saveandrestore.save_int(ref i, Disaster, ref save_data);

            //8
            saveandrestore.save_uint(ref i, family_weight_stable_high, ref save_data);
            saveandrestore.save_uint(ref i, family_weight_stable_low, ref save_data);

            //16
            saveandrestore.save_long(ref i, citizen_goods, ref save_data);
            saveandrestore.save_long(ref i, citizen_goods_temp, ref save_data);
        }


        public static int citizen_salary(uint citizen_id)
        {
            int num = 0;
            int num1 = 0;
            System.Random rand = new System.Random();
            //Array16<Building> buildings = Singleton<BuildingManager>.instance.m_buildings;
            if (citizen_id != 0u)
            {
                Citizen.Flags temp_flag = Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_flags;
                if (((temp_flag & Citizen.Flags.Arrested) != Citizen.Flags.None) || ((temp_flag & Citizen.Flags.Student) != Citizen.Flags.None) || ((temp_flag & Citizen.Flags.Sick) != Citizen.Flags.None))
                {
                    return num;
                }
                int budget = 0;
                int work_building = Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_workBuilding;
                if (work_building != 0u)
                {
                    budget = Singleton<EconomyManager>.instance.GetBudget(Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class);
                    switch (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_subService)
                    {
                        case ItemClass.SubService.CommercialHigh:
                            switch (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_level)
                            {
                                case ItemClass.Level.Level1:
                                    int aliveworkcount3 = 0;
                                    int totalworkcount3 = 0;
                                    Citizen.BehaviourData behaviour3 = default(Citizen.BehaviourData);
                                    BuildingUI.GetWorkBehaviour((ushort)work_building, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building], ref behaviour3, ref aliveworkcount3, ref totalworkcount3);
                                    if (comm_data.building_money[work_building] > (pc_PrivateBuildingAI.good_import_price * 2000))
                                    {
                                        if (totalworkcount3 != 0)
                                        {
                                            num = (int)((comm_data.building_money[work_building] - (pc_PrivateBuildingAI.good_import_price * 2000)) * 0.2f / totalworkcount3);
                                            num1 = (int)((comm_data.building_money[work_building] - (pc_PrivateBuildingAI.good_import_price * 2000)) / totalworkcount3);
                                        }
                                        comm_data.building_money[work_building] -= num1;
                                    }
                                    break;
                                case ItemClass.Level.Level2:
                                    aliveworkcount3 = 0;
                                    totalworkcount3 = 0;
                                    behaviour3 = default(Citizen.BehaviourData);
                                    BuildingUI.GetWorkBehaviour((ushort)work_building, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building], ref behaviour3, ref aliveworkcount3, ref totalworkcount3);
                                    if (comm_data.building_money[work_building] > (pc_PrivateBuildingAI.good_import_price * 2000))
                                    {
                                        if (totalworkcount3 != 0)
                                        {
                                            num = (int)((comm_data.building_money[work_building] - (pc_PrivateBuildingAI.good_import_price * 2000)) * 0.4f / totalworkcount3);
                                            num1 = (int)((comm_data.building_money[work_building] - (pc_PrivateBuildingAI.good_import_price * 2000)) / totalworkcount3);
                                        }
                                        comm_data.building_money[work_building] -= num1;
                                    }
                                    break;
                                case ItemClass.Level.Level3:
                                    aliveworkcount3 = 0;
                                    totalworkcount3 = 0;
                                    behaviour3 = default(Citizen.BehaviourData);
                                    BuildingUI.GetWorkBehaviour((ushort)work_building, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building], ref behaviour3, ref aliveworkcount3, ref totalworkcount3);
                                    if (comm_data.building_money[work_building] > (pc_PrivateBuildingAI.good_import_price * 2000))
                                    {
                                        if (totalworkcount3 != 0)
                                        {
                                            num = (int)((comm_data.building_money[work_building] - (pc_PrivateBuildingAI.good_import_price * 2000)) * 0.7f / totalworkcount3);
                                            num1 = (int)((comm_data.building_money[work_building] - (pc_PrivateBuildingAI.good_import_price * 2000)) / totalworkcount3);
                                        }
                                        comm_data.building_money[work_building] -= num1;
                                    }
                                    break;
                            }
                            break; //
                        case ItemClass.SubService.CommercialLow:
                            switch (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_level)
                            {
                                case ItemClass.Level.Level1:
                                    int aliveworkcount2 = 0;
                                    int totalworkcount2 = 0;
                                    Citizen.BehaviourData behaviour2 = default(Citizen.BehaviourData);
                                    BuildingUI.GetWorkBehaviour((ushort)work_building, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building], ref behaviour2, ref aliveworkcount2, ref totalworkcount2);
                                    if (comm_data.building_money[work_building] > (pc_PrivateBuildingAI.good_import_price * 2000))
                                    {
                                        if (totalworkcount2 != 0)
                                        {
                                            num = (int)((comm_data.building_money[work_building] - (pc_PrivateBuildingAI.good_import_price * 2000)) * 0.1f / totalworkcount2);
                                            num1 = (int)((comm_data.building_money[work_building] - (pc_PrivateBuildingAI.good_import_price * 2000))/ totalworkcount2);
                                        }
                                        comm_data.building_money[work_building] -= num1;
                                    }
                                    break;
                                case ItemClass.Level.Level2:
                                    aliveworkcount2 = 0;
                                    totalworkcount2 = 0;
                                    behaviour2 = default(Citizen.BehaviourData);
                                    BuildingUI.GetWorkBehaviour((ushort)work_building, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building], ref behaviour2, ref aliveworkcount2, ref totalworkcount2);
                                    if (comm_data.building_money[work_building] > (pc_PrivateBuildingAI.good_import_price * 2000))
                                    {
                                        if (totalworkcount2 != 0)
                                        {
                                            num = (int)((comm_data.building_money[work_building] - (pc_PrivateBuildingAI.good_import_price * 2000)) * 0.3f / totalworkcount2);
                                            num1 = (int)((comm_data.building_money[work_building] - (pc_PrivateBuildingAI.good_import_price * 2000)) / totalworkcount2);
                                        }
                                        comm_data.building_money[work_building] -= num1;
                                    }
                                    break;
                                case ItemClass.Level.Level3:
                                    aliveworkcount2 = 0;
                                    totalworkcount2 = 0;
                                    behaviour2 = default(Citizen.BehaviourData);
                                    BuildingUI.GetWorkBehaviour((ushort)work_building, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building], ref behaviour2, ref aliveworkcount2, ref totalworkcount2);
                                    if (comm_data.building_money[work_building] > (pc_PrivateBuildingAI.good_import_price * 2000))
                                    {
                                        if (totalworkcount2 != 0)
                                        {
                                            num = (int)((comm_data.building_money[work_building] - (pc_PrivateBuildingAI.good_import_price * 2000 ))* 0.6f / totalworkcount2);
                                            num1 = (int)((comm_data.building_money[work_building] - (pc_PrivateBuildingAI.good_import_price * 2000)) / totalworkcount2);
                                        }
                                        comm_data.building_money[work_building] -= num1;
                                    }
                                    break;
                            }
                            break; //
                        case ItemClass.SubService.IndustrialGeneric:
                            switch (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_level)
                            {
                                case ItemClass.Level.Level1:
                                    switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                                    {
                                        case Citizen.Education.Uneducated:
                                            num = num + (int)(comm_data.indus_gen_level1_education0) + rand.Next(1); break;
                                        case Citizen.Education.OneSchool:
                                            num = num + (int)(comm_data.indus_gen_level1_education1) + rand.Next(2); break;
                                        case Citizen.Education.TwoSchools:
                                            num = num + (int)(comm_data.indus_gen_level1_education2) + rand.Next(3); break;
                                        case Citizen.Education.ThreeSchools:
                                            num = num + (int)(comm_data.indus_gen_level1_education3) + rand.Next(4); break;
                                    }
                                    break;
                                case ItemClass.Level.Level2:
                                    switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                                    {
                                        case Citizen.Education.Uneducated:
                                            num = num + (int)(comm_data.indus_gen_level2_education0) + rand.Next(1); break;
                                        case Citizen.Education.OneSchool:
                                            num = num + (int)(comm_data.indus_gen_level2_education1) + rand.Next(2); break;
                                        case Citizen.Education.TwoSchools:
                                            num = num + (int)(comm_data.indus_gen_level2_education2) + rand.Next(3); break;
                                        case Citizen.Education.ThreeSchools:
                                            num = num + (int)(comm_data.indus_gen_level2_education3) + rand.Next(4); break;
                                    }
                                    break;
                                case ItemClass.Level.Level3:
                                    switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                                    {
                                        case Citizen.Education.Uneducated:
                                            num = num + (int)(comm_data.indus_gen_level3_education0) + rand.Next(1); break;
                                        case Citizen.Education.OneSchool:
                                            num = num + (int)(comm_data.indus_gen_level3_education1) + rand.Next(2); break;
                                        case Citizen.Education.TwoSchools:
                                            num = num + (int)(comm_data.indus_gen_level3_education2) + rand.Next(3); break;
                                        case Citizen.Education.ThreeSchools:
                                            num = num + (int)(comm_data.indus_gen_level3_education3) + rand.Next(4); break;
                                    }
                                    break;
                            }
                            break; //
                        case ItemClass.SubService.IndustrialFarming:
                            if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_buildingAI is IndustrialExtractorAI)
                            {
                                int aliveworkcount4 = 0;
                                int totalworkcount4 = 0;
                                Citizen.BehaviourData behaviour4 = default(Citizen.BehaviourData);
                                BuildingUI.GetWorkBehaviour((ushort)work_building, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building], ref behaviour4, ref aliveworkcount4, ref totalworkcount4);
                                if (comm_data.building_money[work_building] > 0)
                                {
                                    if (totalworkcount4 != 0)
                                    {
                                        num = (int)(comm_data.building_money[work_building] * 0.2f / totalworkcount4);
                                    }
                                    comm_data.building_money[work_building] -= num;
                                }
                            }
                            else
                            {
                                switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                                {
                                    case Citizen.Education.Uneducated:
                                        num = num + (int)(comm_data.indus_far_education0) + rand.Next(1); num = num >> 1; break;
                                    case Citizen.Education.OneSchool:
                                        num = num + (int)(comm_data.indus_far_education1) + rand.Next(2); num = num >> 1; break;
                                    case Citizen.Education.TwoSchools:
                                        num = num + (int)(comm_data.indus_far_education2) + rand.Next(3); num = num >> 1; break;
                                    case Citizen.Education.ThreeSchools:
                                        num = num + (int)(comm_data.indus_far_education3) + rand.Next(4); num = num >> 1; break;
                                }
                            }
                            break; //
                        case ItemClass.SubService.IndustrialForestry:
                            if (rand.Next(10000) < 4)
                            {
                                if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].CurrentLocation == Citizen.Location.Work)
                                {
                                    if (Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.HealthCare))
                                    {
                                        Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].Sick = true;
                                    }
                                }
                            }

                            if (rand.Next(10000) < 2)
                            {
                                if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].CurrentLocation == Citizen.Location.Work)
                                {
                                    if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].Sick == false)
                                    {
                                        if (Singleton<UnlockManager>.instance.Unlocked(UnlockManager.Feature.DeathCare))
                                        {
                                            Die(citizen_id, ref Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id]);
                                        }
                                    }
                                }
                            }
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.indus_for_education0) + rand.Next(1); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.indus_for_education1) + rand.Next(2); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.indus_for_education2) + rand.Next(3); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.indus_for_education3) + rand.Next(4); break;
                            }
                            if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_buildingAI is IndustrialExtractorAI)
                            {
                            } else
                            {
                                num = num / 2;
                            }
                            break; //
                        case ItemClass.SubService.IndustrialOil:
                            if (rand.Next(10000) < 10)
                            {
                                if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].CurrentLocation == Citizen.Location.Work)
                                {
                                    if (Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.HealthCare))
                                    {
                                        Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].Sick = true;
                                    }
                                }
                            }
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.indus_oil_education0) + rand.Next(1); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.indus_oil_education1) + rand.Next(2); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.indus_oil_education2) + rand.Next(3); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.indus_oil_education3) + rand.Next(4); break;
                            }
                            if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_buildingAI is IndustrialExtractorAI)
                            {
                            }
                            else
                            {
                                num = num / 2;
                            }
                            break; //
                        case ItemClass.SubService.IndustrialOre:
                            if (rand.Next(10000) < 6)
                            {
                                if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].CurrentLocation == Citizen.Location.Work)
                                {
                                    if (Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.HealthCare))
                                    {
                                        Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].Sick = true;
                                    }
                                }
                            }

                            if (rand.Next(10000) < 3)
                            {
                                if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].CurrentLocation == Citizen.Location.Work)
                                {
                                    if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].Sick == false)
                                    {
                                        if (Singleton<UnlockManager>.instance.Unlocked(UnlockManager.Feature.DeathCare))
                                        {
                                            Die(citizen_id, ref Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id]);
                                        }
                                    }
                                }
                            }

                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.indus_ore_education0) + rand.Next(1); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.indus_ore_education1) + rand.Next(2); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.indus_ore_education2) + rand.Next(3); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.indus_ore_education3) + rand.Next(4); break;
                            }
                            if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_buildingAI is IndustrialExtractorAI)
                            {
                            }
                            else
                            {
                                num = num / 2;
                            }
                            break;
                        case ItemClass.SubService.CommercialLeisure:
                            int aliveworkcount1 = 0;
                            int totalworkcount1 = 0;
                            Citizen.BehaviourData behaviour1 = default(Citizen.BehaviourData);
                            BuildingUI.GetWorkBehaviour((ushort)work_building, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building], ref behaviour1, ref aliveworkcount1, ref totalworkcount1);
                            if (comm_data.building_money[work_building] > (pc_PrivateBuildingAI.good_import_price * 2000))
                            {
                                if (totalworkcount1 != 0)
                                {
                                    num = (int)((comm_data.building_money[work_building] - (pc_PrivateBuildingAI.good_import_price * 2000)) * 0.7f / totalworkcount1);
                                    num1 = (int)((comm_data.building_money[work_building] - (pc_PrivateBuildingAI.good_import_price * 2000)) / totalworkcount1);
                                }
                                comm_data.building_money[work_building] -= num1;
                            }
                            break; 
                        case ItemClass.SubService.CommercialTourist:
                            aliveworkcount1 = 0;
                            totalworkcount1 = 0;
                            behaviour1 = default(Citizen.BehaviourData);
                            BuildingUI.GetWorkBehaviour((ushort)work_building, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building], ref behaviour1, ref aliveworkcount1, ref totalworkcount1);
                            if (comm_data.building_money[work_building] > (pc_PrivateBuildingAI.good_import_price * 2000))
                            {
                                if (totalworkcount1 != 0)
                                {
                                    num = (int)((comm_data.building_money[work_building] - (pc_PrivateBuildingAI.good_import_price * 2000)) * 0.9f / totalworkcount1);
                                    num1 = (int)((comm_data.building_money[work_building] - (pc_PrivateBuildingAI.good_import_price * 2000)) / totalworkcount1);
                                }
                                comm_data.building_money[work_building] -= num1;
                            }
                            break; 
                        case ItemClass.SubService.CommercialEco:
                            aliveworkcount1 = 0;
                            totalworkcount1 = 0;
                            behaviour1 = default(Citizen.BehaviourData);
                            BuildingUI.GetWorkBehaviour((ushort)work_building, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building], ref behaviour1, ref aliveworkcount1, ref totalworkcount1);
                            if (comm_data.building_money[work_building] > (pc_PrivateBuildingAI.good_import_price * 2000))
                            {
                                if (totalworkcount1 != 0)
                                {
                                    num = (int)((comm_data.building_money[work_building] - (pc_PrivateBuildingAI.good_import_price * 2000)) * 0.05f / totalworkcount1);
                                    num1 = (int)((comm_data.building_money[work_building] - (pc_PrivateBuildingAI.good_import_price * 2000)) / totalworkcount1);
                                }
                                comm_data.building_money[work_building] -= num1;
                            }
                            break;
                        case ItemClass.SubService.PublicTransportBus:
                            if (budget == 0)
                            {
                                DebugLog.LogToFileOnly("Error:  playerbuilding budget1 = 0");
                            }
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.PublicTransport_bus_education0) + rand.Next(1); PublicTransport_bus += (int)(num * budget / 100f); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.PublicTransport_bus_education1) + rand.Next(2); PublicTransport_bus += (int)(num * budget / 100f); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.PublicTransport_bus_education2) + rand.Next(3); PublicTransport_bus += (int)(num * budget / 100f); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.PublicTransport_bus_education3) + rand.Next(4); PublicTransport_bus += (int)(num * budget / 100f); break;
                            }
                            break; //
                        case ItemClass.SubService.PublicTransportTram:
                            if (budget == 0)
                            {
                                DebugLog.LogToFileOnly("Error:  playerbuilding budget2 = 0");
                            }
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.PublicTransport_tram_education0) + rand.Next(1); PublicTransport_tram += (int)(num * budget / 100f); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.PublicTransport_tram_education1) + rand.Next(2); PublicTransport_tram += (int)(num * budget / 100f); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.PublicTransport_tram_education2) + rand.Next(3); PublicTransport_tram += (int)(num * budget / 100f); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.PublicTransport_tram_education3) + rand.Next(4); PublicTransport_tram += (int)(num * budget / 100f); break;
                            }
                            break; //
                        case ItemClass.SubService.PublicTransportTrain:
                            if (budget == 0)
                            {
                                DebugLog.LogToFileOnly("Error:  playerbuilding budget3 = 0");
                            }
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.PublicTransport_train_education0) + rand.Next(1); PublicTransport_train += (int)(num * budget / 100f); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.PublicTransport_train_education1) + rand.Next(2); PublicTransport_train += (int)(num * budget / 100f); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.PublicTransport_train_education2) + rand.Next(3); PublicTransport_train += (int)(num * budget / 100f); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.PublicTransport_train_education3) + rand.Next(4); PublicTransport_train += (int)(num * budget / 100f); break;
                            }
                            break; //
                        case ItemClass.SubService.PublicTransportTaxi:
                            if (budget == 0)
                            {
                                DebugLog.LogToFileOnly("Error:  playerbuilding budget4 = 0");
                            }
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.PublicTransport_taxi_education0) + rand.Next(1); PublicTransport_taxi += (int)(num * budget / 100f); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.PublicTransport_taxi_education1) + rand.Next(2); PublicTransport_taxi += (int)(num * budget / 100f); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.PublicTransport_taxi_education2) + rand.Next(3); PublicTransport_taxi += (int)(num * budget / 100f); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.PublicTransport_taxi_education3) + rand.Next(4); PublicTransport_taxi += (int)(num * budget / 100f); break;
                            }
                            break; //
                        case ItemClass.SubService.PublicTransportShip:
                            if (budget == 0)
                            {
                                DebugLog.LogToFileOnly("Error:  playerbuilding budget5 = 0");
                            }
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.PublicTransport_ship_education0) + rand.Next(1); PublicTransport_ship += (int)(num * budget / 100f); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.PublicTransport_ship_education1) + rand.Next(2); PublicTransport_ship += (int)(num * budget / 100f); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.PublicTransport_ship_education2) + rand.Next(3); PublicTransport_ship += (int)(num * budget / 100f); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.PublicTransport_ship_education3) + rand.Next(4); PublicTransport_ship += (int)(num * budget / 100f); break;
                            }
                            break; //
                        case ItemClass.SubService.PublicTransportMetro:
                            if (budget == 0)
                            {
                                DebugLog.LogToFileOnly("Error:  playerbuilding budget6 = 0");
                            }
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.PublicTransport_metro_education0) + rand.Next(1); PublicTransport_metro += (int)(num * budget / 100f); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.PublicTransport_metro_education1) + rand.Next(2); PublicTransport_metro += (int)(num * budget / 100f); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.PublicTransport_metro_education2) + rand.Next(3); PublicTransport_metro += (int)(num * budget / 100f); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.PublicTransport_metro_education3) + rand.Next(4); PublicTransport_metro += (int)(num * budget / 100f); break;
                            }
                            break; //
                        case ItemClass.SubService.PublicTransportPlane:
                            if (budget == 0)
                            {
                                DebugLog.LogToFileOnly("Error:  playerbuilding budget7 = 0");
                            }
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.PublicTransport_plane_education0) + rand.Next(1); PublicTransport_plane += (int)(num * budget / 100f); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.PublicTransport_plane_education1) + rand.Next(2); PublicTransport_plane += (int)(num * budget / 100f); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.PublicTransport_plane_education2) + rand.Next(3); PublicTransport_plane += (int)(num * budget / 100f); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.PublicTransport_plane_education3) + rand.Next(4); PublicTransport_plane += (int)(num * budget / 100f); break;
                            }
                            break; //
                        case ItemClass.SubService.PublicTransportCableCar:
                            if (budget == 0)
                            {
                                DebugLog.LogToFileOnly("Error:  playerbuilding budget8 = 0");
                            }
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.PublicTransport_cablecar_education0) + rand.Next(1); PublicTransport_cablecar += (int)(num * budget / 100f); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.PublicTransport_cablecar_education1) + rand.Next(2); PublicTransport_cablecar += (int)(num * budget / 100f); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.PublicTransport_cablecar_education2) + rand.Next(3); PublicTransport_cablecar += (int)(num * budget / 100f); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.PublicTransport_cablecar_education3) + rand.Next(4); PublicTransport_cablecar += (int)(num * budget / 100f); break;
                            }
                            break; //
                        case ItemClass.SubService.PublicTransportMonorail:
                            if (budget == 0)
                            {
                                DebugLog.LogToFileOnly("Error:  playerbuilding budget9 = 0");
                            }
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.PublicTransport_monorail_education0) + rand.Next(1); PublicTransport_monorail += (int)(num * budget / 100f); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.PublicTransport_monorail_education1) + rand.Next(2); PublicTransport_monorail += (int)(num * budget / 100f); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.PublicTransport_monorail_education2) + rand.Next(3); PublicTransport_monorail += (int)(num * budget / 100f); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.PublicTransport_monorail_education3) + rand.Next(4); PublicTransport_monorail += (int)(num * budget / 100f); break;
                            }
                            break; //
                        default: break;
                    }
                    switch (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_service)
                    {
                        case ItemClass.Service.Office:
                            int aliveworkcount1 = 0;
                            int totalworkcount1 = 0;
                            Citizen.BehaviourData behaviour1 = default(Citizen.BehaviourData);
                            BuildingUI.GetWorkBehaviour((ushort)work_building, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building], ref behaviour1, ref aliveworkcount1, ref totalworkcount1);
                            if (comm_data.building_money[work_building] > 0)
                            {
                                if (totalworkcount1 != 0)
                                {
                                    num = (int)(comm_data.building_money[work_building] / totalworkcount1);
                                }
                                comm_data.building_money[work_building] -= num;
                            }
                            break;
                        case ItemClass.Service.Disaster:
                            if (budget == 0)
                            {
                                DebugLog.LogToFileOnly("Error:  playerbuilding budget10 = 0");
                            }
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.disaster_education0) + rand.Next(1); Disaster += (int)(num * budget / 100f); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.disaster_education1) + rand.Next(2); Disaster += (int)(num * budget / 100f); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.disaster_education2) + rand.Next(3); Disaster += (int)(num * budget / 100f); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.disaster_education3) + rand.Next(4); Disaster += (int)(num * budget / 100f); break;
                            }
                            break; //
                        case ItemClass.Service.PoliceDepartment:
                            if (budget == 0)
                            {
                                DebugLog.LogToFileOnly("Error:  playerbuilding budget11 = 0");
                            }
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.PoliceDepartment_education0) + rand.Next(1); PoliceDepartment += (int)(num * budget / 100f); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.PoliceDepartment_education1) + rand.Next(2); PoliceDepartment += (int)(num * budget / 100f); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.PoliceDepartment_education2) + rand.Next(3); PoliceDepartment += (int)(num * budget / 100f); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.PoliceDepartment_education3) + rand.Next(4); PoliceDepartment += (int)(num * budget / 100f); break;
                            }
                            break; //
                        case ItemClass.Service.Education:
                            if (budget == 0)
                            {
                                DebugLog.LogToFileOnly("Error:  playerbuilding budget12 = 0");
                            }
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.Education_education0) + rand.Next(1); Education += (int)(num * budget / 100f); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.Education_education1) + rand.Next(2); Education += (int)(num * budget / 100f); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.Education_education2) + rand.Next(3); Education += (int)(num * budget / 100f); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.Education_education3) + rand.Next(4); Education += (int)(num * budget / 100f); break;
                            }
                            break; //
                        case ItemClass.Service.Road:
                            if (budget == 0)
                            {
                                DebugLog.LogToFileOnly("Error:  playerbuilding budget13 = 0");
                            }
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.road_education0) + rand.Next(1); Road += (int)(num * budget / 100f); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.road_education1) + rand.Next(2); Road += (int)(num * budget / 100f); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.road_education2) + rand.Next(3); Road += (int)(num * budget / 100f); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.road_education3) + rand.Next(4); Road += (int)(num * budget / 100f); break;
                            }
                            break; //
                        case ItemClass.Service.Garbage:
                            if (budget == 0)
                            {
                                DebugLog.LogToFileOnly("Error:  playerbuilding budget14 = 0");
                            }
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.Garbage_education0) + rand.Next(1); Garbage += (int)(num * budget / 100f); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.Garbage_education1) + rand.Next(2); Garbage += (int)(num * budget / 100f); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.Garbage_education2) + rand.Next(3); Garbage += (int)(num * budget / 100f); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.Garbage_education3) + rand.Next(4); Garbage += (int)(num * budget / 100f); break;
                            }
                            break; //
                        case ItemClass.Service.HealthCare:
                            if (budget == 0)
                            {
                                DebugLog.LogToFileOnly("Error:  playerbuilding budget15 = 0");
                            }
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.HealthCare_education0) + rand.Next(1); HealthCare += (int)(num * budget / 100f); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.HealthCare_education1) + rand.Next(2); HealthCare += (int)(num * budget / 100f); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.HealthCare_education2) + rand.Next(3); HealthCare += (int)(num * budget / 100f); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.HealthCare_education3) + rand.Next(4); HealthCare += (int)(num * budget / 100f); break;
                            }
                            break; //
                        case ItemClass.Service.Beautification:
                            if (budget == 0)
                            {
                                DebugLog.LogToFileOnly("Error:  playerbuilding budget16 = 0");
                            }
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.Beautification_education0) + rand.Next(1); Beautification += (int)(num * budget / 100f); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.Beautification_education1) + rand.Next(2); Beautification += (int)(num * budget / 100f); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.Beautification_education2) + rand.Next(3); Beautification += (int)(num * budget / 100f); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.Beautification_education3) + rand.Next(4); Beautification += (int)(num * budget / 100f); break;
                            }
                            break; //
                        case ItemClass.Service.Monument:
                            if (budget == 0)
                            {
                                DebugLog.LogToFileOnly("Error:  playerbuilding budget17 = 0");
                            }
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.Monument_education0) + rand.Next(1); Monument += (int)(num * budget / 100f); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.Monument_education1) + rand.Next(2); Monument += (int)(num * budget / 100f); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.Monument_education2) + rand.Next(3); Monument += (int)(num * budget / 100f); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.Monument_education3) + rand.Next(4); Monument += (int)(num * budget / 100f); break;
                            }
                            break;
                        case ItemClass.Service.Water:
                            if (budget == 0)
                            {
                                DebugLog.LogToFileOnly("Error:  playerbuilding budget18 = 0");
                            }
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.Water_education0) + rand.Next(1); Water += (int)(num * budget / 100f); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.Water_education1) + rand.Next(2); Water += (int)(num * budget / 100f); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.Water_education2) + rand.Next(3); Water += (int)(num * budget / 100f); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.Water_education3) + rand.Next(4); Water += (int)(num * budget / 100f); break;
                            }
                            break; //
                        case ItemClass.Service.Electricity:
                            if (budget == 0)
                            {
                                DebugLog.LogToFileOnly("Error:  playerbuilding budget19 = 0");
                            }
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.Electricity_education0) + rand.Next(1); Electricity = +(int)(num * budget / 100f); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.Electricity_education1) + rand.Next(2); Electricity = +(int)(num * budget / 100f); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.Electricity_education2) + rand.Next(3); Electricity = +(int)(num * budget / 100f); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.Electricity_education3) + rand.Next(4); Electricity = +(int)(num * budget / 100f); break;
                            }
                            break; //
                        case ItemClass.Service.FireDepartment:
                            if (budget == 0)
                            {
                                DebugLog.LogToFileOnly("Error:  playerbuilding budget20 = 0");
                            }
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.FireDepartment_education0) + rand.Next(1); FireDepartment += (int)(num * budget / 100f); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.FireDepartment_education1) + rand.Next(2); FireDepartment += (int)(num * budget / 100f); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.FireDepartment_education2) + rand.Next(3); FireDepartment += (int)(num * budget / 100f); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.FireDepartment_education3) + rand.Next(4); FireDepartment += (int)(num * budget / 100f); break;
                            }
                            break; //
                        default:
                            break;
                    }
                    if (num == 0 && (!(Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_service == ItemClass.Service.Commercial)) && (!(Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_service == ItemClass.Service.Office)) && (!(Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_subService == ItemClass.SubService.IndustrialFarming)))
                    {
                        DebugLog.LogToFileOnly("find unknown citizen workbuilding" + " building servise is" + Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_service + " building subservice is" + Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_subService);
                    }


                    int aliveworkcount = 0;
                    int totalworkcount = 0;
                    Citizen.BehaviourData behaviour = default(Citizen.BehaviourData);
                    BuildingUI.GetWorkBehaviour((ushort)work_building, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building], ref behaviour, ref aliveworkcount, ref totalworkcount);

                    float local_salary_idex = 0.5f;
                    float final_salary_idex = 0.5f;
                    DistrictManager instance2 = Singleton<DistrictManager>.instance;
                    byte district = 0;
                    if ((Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_service == ItemClass.Service.Industrial))
                    {
                        district = instance2.GetDistrict(Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].m_position);
                        local_salary_idex = (Singleton<DistrictManager>.instance.m_districts.m_buffer[district].GetLandValue() + 50f) / 120f;
                        final_salary_idex = (local_salary_idex * 3f + comm_data.salary_idex) / 4f;
                    }


                    if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_service == ItemClass.Service.Office || Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_service == ItemClass.Service.Commercial)
                    {
                        //num = num;
                    }else if (budget != 0)
                    {
                        num = (int)(num * budget / 100f);
                        switch (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_service)
                        {
                            case ItemClass.Service.HealthCare:
                            case ItemClass.Service.PoliceDepartment:
                            case ItemClass.Service.Road:
                            case ItemClass.Service.Beautification:
                            case ItemClass.Service.Monument:
                            case ItemClass.Service.Garbage:
                            case ItemClass.Service.FireDepartment:
                                comm_data.building_money[Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_workBuilding] -= (num / 100f); break;
                            default: comm_data.building_money[Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_workBuilding] = 0; break;
                        }
                    }
                    else if (comm_data.building_money[Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_workBuilding] < 0)
                    {
                        num = (int)((float)num * final_salary_idex / 3f + 0.5f);
                        num = 0;
                    }
                    else
                    {
                        num = (int)((float)num * final_salary_idex + 0.5f);
                    }

                    if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_service == ItemClass.Service.Industrial)
                    {
                        num = (int)((float)num * (float)(Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].m_width * (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].m_length) / 9f));
                        comm_data.building_money[Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_workBuilding] -= num;
                    }

                    if ((Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_service == ItemClass.Service.Commercial) || (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_service == ItemClass.Service.Industrial) || (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_service == ItemClass.Service.Office))
                    {
                        if (comm_data.building_money[Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_workBuilding] > 0)
                        {
                            comm_data.building_money[Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_workBuilding] -= num * 0.1f;
                            comm_data.city_insurance_account += num * 0.1f;
                            if (comm_data.is_help_resident)
                            {
                                comm_data.building_money[Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_workBuilding] -= num * 0.3f;
                                comm_data.city_insurance_account += num * 0.3f;
                            }
                        }
                    }

                }
           }//if (citizen_id != 0u)
            return num;
        }//public


        public void process_citizen(uint homeID, ref CitizenUnit data, bool is_pre)
        {
            if (is_pre)
            {
                comm_data.family_money[homeID] = 0;
                if (data.m_citizen0 != 0)
                {
                    comm_data.family_money[homeID] += comm_data.citizen_money[data.m_citizen0];
                }
                if (data.m_citizen1 != 0)
                {
                    comm_data.family_money[homeID] += comm_data.citizen_money[data.m_citizen1];
                }
                if (data.m_citizen2 != 0)
                {
                    comm_data.family_money[homeID] += comm_data.citizen_money[data.m_citizen2];
                }
                if (data.m_citizen3 != 0)
                {
                    comm_data.family_money[homeID] += comm_data.citizen_money[data.m_citizen3];
                }
                if (data.m_citizen4 != 0)
                {
                    comm_data.family_money[homeID] += comm_data.citizen_money[data.m_citizen4];
                }
            } else
            {
                int temp = 0;
                if (data.m_citizen0 != 0)
                {
                    temp++;
                }
                if (data.m_citizen1 != 0)
                {
                    temp++;
                }
                if (data.m_citizen2 != 0)
                {
                    temp++;
                }
                if (data.m_citizen3 != 0)
                {
                    temp++;
                }
                if (data.m_citizen4 != 0)
                {
                    temp++;
                }

                if (temp!=0)
                {
                    if (data.m_citizen0 != 0)
                    {
                        comm_data.citizen_money[data.m_citizen0] = comm_data.family_money[homeID] / (float)temp;
                    }
                    if (data.m_citizen1 != 0)
                    {
                        comm_data.citizen_money[data.m_citizen1] = comm_data.family_money[homeID] / (float)temp;
                    }
                    if (data.m_citizen2 != 0)
                    {
                        comm_data.citizen_money[data.m_citizen2] = comm_data.family_money[homeID] / (float)temp;
                    }
                    if (data.m_citizen3 != 0)
                    {
                        comm_data.citizen_money[data.m_citizen3] = comm_data.family_money[homeID] / (float)temp;
                    }
                    if (data.m_citizen4 != 0)
                    {
                        comm_data.citizen_money[data.m_citizen4] = comm_data.family_money[homeID] / (float)temp;
                    }
                }
            }

        }


        public byte process_family(uint homeID, ref CitizenUnit data)
        {
            //DebugLog.LogToFileOnly("we go in now, pc_ResidentAI");
            if (precitizenid > homeID)
            {
                //DebugLog.LogToFileOnly("process once");
                //citizen_process_done = true;
                comm_data.family_count = family_count;
                comm_data.family_profit_money_num = family_profit_money_num;
                comm_data.family_very_profit_money_num = family_very_profit_money_num;
                comm_data.family_loss_money_num = family_loss_money_num;
                if (family_count != 0)
                {
                    comm_data.citizen_salary_per_family = (int)((citizen_salary_count / family_count));
                    comm_data.citizen_expense_per_family = (int)((citizen_expense_count / family_count));
                }
                comm_data.citizen_expense = citizen_expense_count;
                comm_data.citizen_salary_tax_total = citizen_salary_tax_total;
                comm_data.citizen_salary_total = citizen_salary_count;
                comm_data.Monument = (int)(Monument * comm_data.salary_idex);
                comm_data.PublicTransport_bus = (int)(PublicTransport_bus * comm_data.salary_idex);
                comm_data.PublicTransport_tram = (int)(PublicTransport_tram * comm_data.salary_idex);
                comm_data.PublicTransport_train = (int)(PublicTransport_train * comm_data.salary_idex);
                comm_data.PublicTransport_taxi = (int)(PublicTransport_taxi * comm_data.salary_idex);
                comm_data.PublicTransport_metro = (int)(PublicTransport_metro * comm_data.salary_idex);
                comm_data.PublicTransport_plane = (int)(PublicTransport_plane * comm_data.salary_idex);
                comm_data.PublicTransport_ship = (int)(PublicTransport_ship * comm_data.salary_idex);
                comm_data.PublicTransport_monorail = (int)(PublicTransport_monorail * comm_data.salary_idex);
                comm_data.PublicTransport_cablecar = (int)(PublicTransport_cablecar * comm_data.salary_idex);
                comm_data.Beautification = (int)(Beautification * comm_data.salary_idex);
                comm_data.Education = (int)(Education * comm_data.salary_idex);
                comm_data.Disaster = (int)(Disaster * comm_data.salary_idex);
                comm_data.PoliceDepartment = (int)(PoliceDepartment * comm_data.salary_idex);
                comm_data.Electricity = (int)(Electricity * comm_data.salary_idex);
                comm_data.Water = (int)(Water * comm_data.salary_idex);
                comm_data.Garbage = (int)(Garbage * comm_data.salary_idex);
                comm_data.HealthCare = (int)(HealthCare * comm_data.salary_idex);
                comm_data.Road = (int)(Road * comm_data.salary_idex);
                comm_data.FireDepartment = (int)(FireDepartment * comm_data.salary_idex);
                comm_data.family_weight_stable_high = family_weight_stable_high;
                comm_data.family_weight_stable_low = family_weight_stable_low;
                comm_data.city_insurance_account_final = comm_data.city_insurance_account;
                citizen_goods = citizen_goods_temp;
                family_very_profit_money_num = 0;
                family_profit_money_num = 0;
                family_loss_money_num = 0;
                family_count = 0;
                citizen_salary_count = 0;
                citizen_expense_count = 0;
                citizen_salary_tax_total = 0;
                temp_citizen_salary_tax_total = 0f;
                PublicTransport_bus = 0;
                PublicTransport_tram = 0;
                PublicTransport_train = 0;
                PublicTransport_ship = 0;
                PublicTransport_taxi = 0;
                PublicTransport_metro = 0;
                PublicTransport_plane = 0;
                PublicTransport_monorail = 0;
                PublicTransport_cablecar = 0;
                Road = 0;
                FireDepartment = 0;
                Education = 0;
                Disaster = 0;
                HealthCare = 0;
                PoliceDepartment = 0;
                Electricity = 0;
                Water = 0;
                Beautification = 0;
                Garbage = 0;
                Monument = 0;
                family_weight_stable_high = 0;
                family_weight_stable_low = 0;
                citizen_goods_temp = 0;
                comm_data.city_insurance_account = 0;
            }
            else if (precitizenid < homeID)
            {
                //citizen_process_done = false;
                family_count++;
                citizen_goods_temp += data.m_goods;
            }

            if (homeID > 524288)
            {
                DebugLog.LogToFileOnly("Error: citizen ID greater than 524288");
            }

            /*if (comm_data.family_money[homeID] < -39000000f)
            {
                comm_data.family_money[homeID] = 0;
            }*/

            process_citizen(homeID, ref data, true);

            //here we caculate citizen income
            int temp_num;
            temp_num = citizen_salary(data.m_citizen0);
            //DebugLog.LogToFileOnly("in ResidentAI salary = " + temp_num.ToString());
            temp_num = temp_num + citizen_salary(data.m_citizen1);
            //DebugLog.LogToFileOnly("in ResidentAI salary = " + temp_num.ToString());
            temp_num = temp_num + citizen_salary(data.m_citizen2);
            //DebugLog.LogToFileOnly("in ResidentAI salary = " + temp_num.ToString());
            temp_num = temp_num + citizen_salary(data.m_citizen3);
            //DebugLog.LogToFileOnly("in ResidentAI salary = " + temp_num.ToString());
            temp_num = temp_num + citizen_salary(data.m_citizen4);
            //DebugLog.LogToFileOnly("in ResidentAI salary = " + temp_num.ToString());
            //DebugLog.LogToFileOnly("Citzen " + homeID.ToString() + "salary is " + temp_num.ToString());
            citizen_salary_count = citizen_salary_count + temp_num;
            int citizen_salary_current = temp_num;
            temp_num = 0;

            if (data.m_citizen0 != 0u)
            {
                temp_num++;
            }
            if (data.m_citizen1 != 0u)
            {
                temp_num++;
            }

            if (data.m_citizen2 != 0u)
            {
                temp_num++;
            }

            if (data.m_citizen3 != 0u)
            {
                temp_num++;
            }

            if (data.m_citizen4 != 0u)
            {
                temp_num++;
            }
            //caculate tax
            float salary_per_family_member;
            if (temp_num != 0)
            {
                salary_per_family_member = (float)citizen_salary_current / temp_num;
            }
            else
            {
                salary_per_family_member = 0;
                DebugLog.LogToFileOnly("temp_num == 0 in ResidentAI");
            }
            float tax = 0;
            //0-10 10% 10-30 15% 30-60 20% 60-100 25% >100 30%

            if (citizen_salary_current < 0)
            {
                DebugLog.LogToFileOnly("citizen_salary_current< 0 in ResidentAI");
                citizen_salary_current = 0;
            }


            //sick insurance 10%  
            //endowment & unemployed 10%
            //some is paid by company
            float insurance = 0;
            float company_insurance = 0;
            if (comm_data.is_help_resident)
            {
                insurance = 0.2f * citizen_salary_current;
                //company_insurance = 0.3f * citizen_salary_current;
                comm_data.city_insurance_account += insurance;
                citizen_salary_current = (int)(0.8f * citizen_salary_current);
            }
            else
            {
                insurance = 0.1f * citizen_salary_current;
                //company_insurance = 0.1f * citizen_salary_current;
                comm_data.city_insurance_account += insurance;
                citizen_salary_current = (int)(0.9f * citizen_salary_current);
            }


            if (citizen_salary_current < 10)
            {
                tax = citizen_salary_current * 0.1f;
            }
            else if (citizen_salary_current >= 10 && citizen_salary_current <= 30)
            {
                tax = (citizen_salary_current - 10) * 0.2f + 1f;
            }
            else if (citizen_salary_current > 30 && citizen_salary_current <= 60)
            {
                tax = (citizen_salary_current - 30) * 0.3f + 5f;
            }
            else if (citizen_salary_current > 60 && citizen_salary_current <= 100)
            {
                tax = (citizen_salary_current - 60) * 0.4f + 14f;
            }
            else if (citizen_salary_current > 100)
            {
                tax = (citizen_salary_current - 100) * 0.5f + 30f;
            }


            tax = tax + insurance;


            if (citizen_salary_current < (comm_data.citizen_salary_per_family / 2) && comm_data.is_help_resident)
            {
                Help_low_income_family(citizen_salary_current);
                citizen_salary_current = comm_data.citizen_salary_per_family/2;
                comm_data.city_insurance_account -= (int)(comm_data.citizen_salary_per_family / 2);
            }

            temp_citizen_salary_tax_total = temp_citizen_salary_tax_total + (int)tax;
            citizen_salary_tax_total = (int)temp_citizen_salary_tax_total;
            process_citizen_income_tax(homeID, (tax + company_insurance));
            //here we caculate expense
            temp_num = 0;
            int expenserate = 0;
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint num3 = 0u;
            int num4 = 0;
            if (data.m_citizen4 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen4)].Dead)
            {
                num4++;
                num3 = data.m_citizen4;
                expenserate = 0;
                temp_num += GetexpenseRate(homeID, data.m_citizen4, out expenserate);
            }
            if (data.m_citizen3 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen3)].Dead)
            {
                num4++;
                num3 = data.m_citizen3;
                expenserate = 0;
                temp_num += GetexpenseRate(homeID, data.m_citizen3, out expenserate);
            }
            if (data.m_citizen2 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen2)].Dead)
            {
                num4++;
                num3 = data.m_citizen2;
                expenserate = 0;
                temp_num += GetexpenseRate(homeID, data.m_citizen2, out expenserate);
            }
            if (data.m_citizen1 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen1)].Dead)
            {
                num4++;
                num3 = data.m_citizen1;
                expenserate = 0;
                temp_num += GetexpenseRate(homeID, data.m_citizen1, out expenserate);
            }
            if (data.m_citizen0 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen0)].Dead)
            {
                num4++;
                num3 = data.m_citizen0;
                expenserate = 0;
                temp_num += GetexpenseRate(homeID, data.m_citizen0, out expenserate);
            }

            //temp = education&sick   expenserate = house rent(one family)
            process_citizen_house_rent(homeID, expenserate);
            citizen_expense_count = citizen_expense_count + temp_num + expenserate;

            //DebugLog.LogToFileOnly("temp_num = " + temp_num.ToString());
            //DebugLog.LogToFileOnly("expenserate = " + expenserate.ToString());

            //income - expense
            temp_num = citizen_salary_current - (int)(tax) - temp_num - expenserate;// - comm_data.citizen_average_transport_fee;
            comm_data.family_money[homeID] = (short)(comm_data.family_money[homeID] + temp_num);
            //process citizen status
            System.Random rand = new System.Random();
            if (temp_num <= 20)
            {
                temp_num = rand.Next(20);
                family_loss_money_num = (uint)(family_loss_money_num + 1);
                //try_move_family to do here;
            }
            else if (temp_num > 70)
            {
                temp_num = rand.Next(temp_num);
                family_very_profit_money_num = (uint)(family_very_profit_money_num + 1);
            }
            else
            {
                temp_num = rand.Next(temp_num);
                family_profit_money_num = (uint)(family_profit_money_num + 1);
            }


            temp_num = (temp_num > 100) ? 100 : temp_num;

            comm_data.family_money[homeID] = (float)(comm_data.family_money[homeID] - temp_num);


            if (comm_data.family_money[homeID] >= 20000)
            {
                comm_data.family_profit_status[homeID]++;
            }
            else if (comm_data.family_money[homeID] < 5000)
            {
                comm_data.family_profit_status[homeID]--;
            }
            else
            {
                if (comm_data.family_profit_status[homeID] > 128)
                {
                    comm_data.family_profit_status[homeID]--;
                } else
                {
                    comm_data.family_profit_status[homeID]++;
                }
            }



            if (comm_data.family_money[homeID] > 32000000f)
            {
                comm_data.family_money[homeID] = 32000000f;
            }

            if (comm_data.family_money[homeID] < -32000000f)
            {
                comm_data.family_money[homeID] = -32000000f;
            }

            if (comm_data.family_profit_status[homeID] > 250)
            {
                comm_data.family_profit_status[homeID] = 250;
            }
            if (comm_data.family_profit_status[homeID] < 5)
            {
                comm_data.family_profit_status[homeID] = 5;
            }


            ItemClass.Level home_level = Singleton<BuildingManager>.instance.m_buildings.m_buffer[Singleton<CitizenManager>.instance.m_units.m_buffer[(int)((UIntPtr)homeID)].m_building].Info.m_class.m_level;
            if ((comm_data.family_money[homeID] >= 20000) && (comm_data.family_profit_status[homeID] >= 230))
            {
                if ((home_level == ItemClass.Level.Level1) || (home_level == ItemClass.Level.Level2) || (home_level == ItemClass.Level.Level3))
                {
                    if (rand.Next(100) < 20)
                    {
                        if (num3 != 0u)
                        {
                            TryMoveFamily_1(homeID, num3, ref instance.m_citizens.m_buffer[(int)((UIntPtr)num3)], num4);
                        }
                    }
                }
                //change wealth to high
                //try move family here (1、2、3 level house to 4-5 level house)
            }
            else if ((comm_data.family_money[homeID] <= 5000) && (comm_data.family_profit_status[homeID] <= 25))
            {
                if ((home_level == ItemClass.Level.Level2) || (home_level == ItemClass.Level.Level3) || (home_level == ItemClass.Level.Level4) || (home_level == ItemClass.Level.Level5))
                {
                    if (rand.Next(100) < 10)
                    {
                        if (num3 != 0u)
                        {
                            TryMoveFamily_1(homeID, num3, ref instance.m_citizens.m_buffer[(int)((UIntPtr)num3)], num4);
                        }
                    }
                }
                //change wealth to low
                //try move family here try move family here (2-5 level house to 1 level house)
            }
            else
            {
                if (home_level == ItemClass.Level.Level1)
                {
                    if (rand.Next(100) < 10)
                    {
                        if (num3 != 0u)
                        {
                            TryMoveFamily_1(homeID, num3, ref instance.m_citizens.m_buffer[(int)((UIntPtr)num3)], num4);
                        }
                    }
                }
                else if ((home_level == ItemClass.Level.Level4) || (home_level == ItemClass.Level.Level5))
                {
                    if (rand.Next(100) < 10)
                    {
                        if (num3 != 0u)
                        {
                            TryMoveFamily_1(homeID, num3, ref instance.m_citizens.m_buffer[(int)((UIntPtr)num3)], num4);
                        }
                    }
                }
                //change wealth to medium if wealth is low
                //change wealth to medium if wealth is high
                //by move family;
            }

            if ((comm_data.family_money[homeID] < 5000) && (comm_data.family_profit_status[homeID] <= 25))
            {
                family_weight_stable_low = (ushort)(family_weight_stable_low + 1);
            }
            else if ((comm_data.family_money[homeID] >= 20000) && (comm_data.family_profit_status[homeID] >= 230))
            {
                family_weight_stable_high = (ushort)(family_weight_stable_high + 1);
            }

            //DebugLog.LogToFileOnly("comm_data.family_profit_status[" + homeID.ToString() +"] = " + comm_data.family_profit_status[homeID].ToString() + "money = " + comm_data.family_money[homeID].ToString());
            //set other non-exist citizen status to 0
            /*uint i;
            if (precitizenid < homeID)
            {
                for (i = (precitizenid + 1); i < homeID; i++)
                {
                    comm_data.family_money[i] = -40000000f;  // 40000000f is default value
                    if (comm_data.family_profit_status[i] != 20)
                    {
                        //comm_data.family_money[i] = rand.Next(comm_data.citizen_salary_per_family + 1) * 200 ;
                        comm_data.family_profit_status[i] = 20;
                    }
                }
            } else
            {
                for (i = (precitizenid + 1); i < 524288; i++)
                {
                    comm_data.family_money[i] = -40000000f;
                    if (comm_data.family_profit_status[i] != 20)
                    {
                        //comm_data.family_money[i] = rand.Next(comm_data.citizen_salary_per_family + 1) * 200;
                        comm_data.family_profit_status[i] = 20;
                    }
                }

                for (i = 0; i < homeID; i++)
                {
                    comm_data.family_money[i] = -40000000f; 
                    if (comm_data.family_profit_status[i] != 20)
                    {
                        comm_data.family_profit_status[i] = 20;
                    }
                }
            }*/
            /*if (comm_data.citizen_count == 0)
            {
                comm_data.family_money[homeID] = 0;
                comm_data.family_profit_status[homeID] = 128;
            }*/
            precitizenid = homeID;
            process_citizen(homeID, ref data, false);

            //comm_data.citizen_shopping_idex = (byte)temp_num;
            return (byte)temp_num;
            //return to original game code.
        }


        public void Help_low_income_family(int citizen_salary_current)
        {
            int expense = (comm_data.citizen_salary_per_family / 2) - citizen_salary_current;
            if (expense <= 0)
            {
                DebugLog.LogToFileOnly("we can not offer <0 money to citizen");
            } else
            {
                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, expense * comm_data.game_maintain_fee_decrease, this.m_info.m_class);
            }
        }

        public void TryMoveAwayFromHome_1(uint citizenID, ref Citizen data)
        {
            if (data.Dead)
            {
                return;
            }
            if (data.m_homeBuilding == 0)
            {
                return;
            }
            Citizen.AgeGroup ageGroup = Citizen.GetAgeGroup(data.Age);
            if (ageGroup == Citizen.AgeGroup.Young || ageGroup == Citizen.AgeGroup.Adult)
            {
                TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                if (ageGroup == Citizen.AgeGroup.Young)
                {
                    offer.Priority = 1;
                }
                else
                {
                    offer.Priority = Singleton<SimulationManager>.instance.m_randomizer.Int32(2, 4);
                }
                offer.Citizen = citizenID;
                offer.Position = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_homeBuilding].m_position;
                offer.Amount = 1;
                offer.Active = true;
                if (Singleton<SimulationManager>.instance.m_randomizer.Int32(2u) == 0)
                {
                    switch (data.EducationLevel)
                    {
                        case Citizen.Education.Uneducated:
                            Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single0, offer);
                            break;
                        case Citizen.Education.OneSchool:
                            Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single1, offer);
                            break;
                        case Citizen.Education.TwoSchools:
                        case Citizen.Education.ThreeSchools:
                            Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single2, offer);
                            break;
                    }
                }
                else
                {
                    switch (data.EducationLevel)
                    {
                        case Citizen.Education.Uneducated:
                            Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single0B, offer);
                            break;
                        case Citizen.Education.OneSchool:
                            Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single1B, offer);
                            break;
                        case Citizen.Education.TwoSchools:
                        case Citizen.Education.ThreeSchools:
                            Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single2B, offer);
                            break;
                    }
                }
            }
        }


        public void TryMoveFamily_1(uint homeID, uint citizenID, ref Citizen data, int familySize)
        {
            if (data.Dead)
            {
                return;
            }
            if (data.m_homeBuilding == 0)
            {
                return;
            }
            TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
            offer.Priority = Singleton<SimulationManager>.instance.m_randomizer.Int32(1, 7);
            offer.Citizen = citizenID;
            offer.Position = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_homeBuilding].m_position;
            offer.Amount = 1;
            offer.Active = true;
            if (familySize == 1)
            {
                if (Singleton<SimulationManager>.instance.m_randomizer.Int32(2u) == 0)
                {
                    if ((comm_data.family_money[homeID] > 20000) && (comm_data.family_profit_status[homeID] >= 230))
                    {
                        Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single3, offer);
                    }
                    else if ((comm_data.family_money[homeID] < 5000) && (comm_data.family_profit_status[homeID] <= 25))
                    {
                        Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single0, offer);
                    }
                    else if (Singleton<SimulationManager>.instance.m_randomizer.Int32(2u) == 0)
                    {
                        Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single2, offer);
                    }
                    else
                    {
                        Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single1, offer);
                    }
                }
                else
                {
                    if ((comm_data.family_money[homeID] > 20000) && (comm_data.family_profit_status[homeID] >= 230))
                    {
                        Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single3B, offer);
                    }
                    else if ((comm_data.family_money[homeID] < 5000) && (comm_data.family_profit_status[homeID] <= 25))
                    {
                        Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single0B, offer);
                    }
                    else if (Singleton<SimulationManager>.instance.m_randomizer.Int32(2u) == 0)
                    {
                        Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single2B, offer);
                    }
                    else
                    {
                        Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single1B, offer);
                    }
                }
            }
            else
            {
                if ((comm_data.family_money[homeID] > 50000) && (comm_data.family_profit_status[homeID] >= 230))
                {
                    Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Family3, offer);
                }
                else if ((comm_data.family_money[homeID] < 5000) && (comm_data.family_profit_status[homeID] <= 25))
                {
                    Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Family0, offer);
                }
                else if (Singleton<SimulationManager>.instance.m_randomizer.Int32(2u) == 0)
                {
                    Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Family2, offer);
                }
                else
                {
                    Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Family1, offer);
                }
            }
        }

        public void process_citizen_income_tax(uint homeID, float tax)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            ushort building = instance.m_units.m_buffer[(int)((UIntPtr)homeID)].m_building;
            Building buildingdata = Singleton<BuildingManager>.instance.m_buildings.m_buffer[building];
            Singleton<EconomyManager>.instance.AddPrivateIncome((int)(tax), buildingdata.Info.m_class.m_service, buildingdata.Info.m_class.m_subService, buildingdata.Info.m_class.m_level, 112);
        }

        public void process_citizen_house_rent(uint homeID, int expenserate)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            ushort building = instance.m_units.m_buffer[(int)((UIntPtr)homeID)].m_building;
            Building buildingdata = Singleton<BuildingManager>.instance.m_buildings.m_buffer[building];
            DistrictManager instance2 = Singleton<DistrictManager>.instance;
            byte district = instance2.GetDistrict(buildingdata.m_position);
            //DistrictPolicies.Taxation taxationPolicies = instance2.m_districts.m_buffer[(int)district].m_taxationPolicies;
            //int num2;
            //num2 = Singleton<EconomyManager>.instance.GetTaxRate(this.m_info.m_class, taxationPolicies);
            Singleton<EconomyManager>.instance.AddPrivateIncome(expenserate*100, buildingdata.Info.m_class.m_service, buildingdata.Info.m_class.m_subService, buildingdata.Info.m_class.m_level, 100);
        }


        public bool Chancetodovitureshopping(uint homeID, ref CitizenUnit data)
        {
            SimulationManager instance2 = Singleton<SimulationManager>.instance;
            BuildingManager expr_18 = Singleton<BuildingManager>.instance;
            Building building = expr_18.m_buildings.m_buffer[(int)data.m_building];
            ushort num = 0;
            
            num = FindNotSoCloseBuilding(building.m_position, 2000f, ItemClass.Service.Commercial, ItemClass.SubService.None, Building.Flags.Created, Building.Flags.Deleted | Building.Flags.Abandoned);
            

            if ((num == 0) || (Singleton<SimulationManager>.instance.m_randomizer.Int32(20) < 10))
            {
                int num2 = Singleton<SimulationManager>.instance.m_randomizer.Int32(5u);
                for (int i = 0; i < 5; i++)
                {
                    uint citizen = data.GetCitizen((num2 + i) % 5);
                    if (citizen != 0u)
                    {
                        if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen].m_workBuilding != 0)
                        {
                            ushort num1 = 0;

                            
                            num1 = FindNotSoCloseBuilding(expr_18.m_buildings.m_buffer[Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen].m_workBuilding].m_position, 2000f, ItemClass.Service.Commercial, ItemClass.SubService.None, Building.Flags.Created, Building.Flags.Deleted | Building.Flags.Abandoned);


                            if (num1 != 0)
                            {
                                num = (ushort)num1;
                                break;
                            }
                        }
                    }
                }
            }
            if (num != 0)
            {
                int num1 = -100;
                expr_18.m_buildings.m_buffer[(int)num].Info.m_buildingAI.ModifyMaterialBuffer(num, ref expr_18.m_buildings.m_buffer[(int)num], TransferManager.TransferReason.Shopping, ref num1);
                ushort temp = (ushort)(-num1);
                data.m_goods += temp;
                //DebugLog.LogToFileOnly("try viture shopping now, temp = " + temp.ToString());
                return true;
            }
            else
            {
                //DebugLog.LogToFileOnly("failed to find a building to shopping");
            }
            return false;
        }

        public static ushort FindNotSoCloseBuilding(Vector3 pos, float maxDistance, ItemClass.Service service, ItemClass.SubService subService, Building.Flags flagsRequired, Building.Flags flagsForbidden)
        {
            int num = Mathf.Max((int)((pos.x - maxDistance) / 64f + 135f), 0);
            int num2 = Mathf.Max((int)((pos.z - maxDistance) / 64f + 135f), 0);
            int num3 = Mathf.Min((int)((pos.x + maxDistance) / 64f + 135f), 269);
            int num4 = Mathf.Min((int)((pos.z + maxDistance) / 64f + 135f), 269);
            ushort result = 0;
            float num5 = maxDistance * maxDistance;
            BuildingManager building = Singleton<BuildingManager>.instance;
            SimulationManager instance2 = Singleton<SimulationManager>.instance;

            if ((num4 >= num2) && (num3 >= num))
            {
                int[] source = new int[(num4 - num2 + 1) * (num3 - num + 1)];
                int[] array = new int[(num4 - num2 + 1) * (num3 - num + 1)];
                int idex = 0;
                for (int i = num2; i <= num4; i++)
                {
                    for (int j = num; j <= num3; j++)
                    {
                        source[idex] = i * 270 + j;
                        idex++;
                    }
                }

                setNum(ref source, ref array);
                idex = 0;
                for (int i = 0; i <= num4-num2; i++)
                  {
                    if (result != 0)
                    {
                        break;
                    }
                    for (int j = 0; j <= num3-num; j++)
                    {
                        ushort num6 = building.m_buildingGrid[array[idex]];
                        idex++;
                        int num7 = 0;
                        if (result != 0)
                        {
                            break;
                        }
                        while (num6 != 0)
                        {
                            BuildingInfo info = building.m_buildings.m_buffer[(int)num6].Info;
                            if ((info.m_class.m_service == service || service == ItemClass.Service.None) && (info.m_class.m_subService == subService || subService == ItemClass.SubService.None))
                            {
                                Building.Flags flags = building.m_buildings.m_buffer[(int)num6].m_flags;
                                if ((flags & (flagsRequired | flagsForbidden)) == flagsRequired)
                                {
                                    float num8 = Vector3.SqrMagnitude(pos - building.m_buildings.m_buffer[(int)num6].m_position);
                                    //for rush hour
                                    if (info.m_class.m_service == ItemClass.Service.Commercial)
                                    {
                                        if ((info.m_class.m_subService == ItemClass.SubService.CommercialTourist) && (instance2.m_randomizer.Int32(100) < 97))
                                        {

                                        }
                                        else if ((info.m_class.m_subService == ItemClass.SubService.CommercialLeisure) && (instance2.m_randomizer.Int32(100) < 95))
                                        {

                                        }
                                        else if ((maxDistance == 2000f) || (maxDistance == 500f))
                                        {
                                            if (instance2.m_randomizer.Int32(building.m_buildings.m_buffer[(int)num6].m_customBuffer2) > 300)
                                            {
                                                result = num6;
                                                if (instance2.m_randomizer.Int32(6) == 0)
                                                {
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if ((instance2.m_randomizer.Int32(building.m_buildings.m_buffer[(int)num6].m_customBuffer2) > 200))
                                            {
                                                result = num6;
                                                if (instance2.m_randomizer.Int32(6) == 0)
                                                {
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            num6 = building.m_buildings.m_buffer[(int)num6].m_nextGridBuilding;
                            if (++num7 >= 49152)
                            {
                                CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }

        public static void setNum(ref int[] source, ref int[] array)
        {
            System.Random rd = new System.Random();
            int range = array.Length;
            for (int i = 0; i < array.Length; i++)
            {
                //随机产生一个位置  
                int pos = rd.Next(range);
                //获取该位置的值  
                array[i] = source[pos];
                //改良：将最后一个数赋给被删除的索引所对应的值  
                source[pos] = source[range - 1];
                range--;
            }
        }

        // ResidentAI
        public void SimulationStep_1(uint homeID, ref CitizenUnit data)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            ushort building = instance.m_units.m_buffer[(int)((UIntPtr)homeID)].m_building;
            if (data.m_citizen0 != 0u && data.m_citizen1 != 0u && (data.m_citizen2 == 0u || data.m_citizen3 == 0u || data.m_citizen4 == 0u))
            {
                bool flag = this.CanMakeBabies(data.m_citizen0, ref instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen0)]);
                bool flag2 = this.CanMakeBabies(data.m_citizen1, ref instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen1)]);
                if (flag && flag2 && Singleton<SimulationManager>.instance.m_randomizer.Int32(12u) == 0)
                {
                    int family = (int)instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen0)].m_family;
                    uint num;
                    if (instance.CreateCitizen(out num, 0, family, ref Singleton<SimulationManager>.instance.m_randomizer))
                    {
                        instance.m_citizens.m_buffer[(int)((UIntPtr)num)].SetHome(num, 0, homeID);
                        Citizen[] expr_126_cp_0 = instance.m_citizens.m_buffer;
                        UIntPtr expr_126_cp_1 = (UIntPtr)num;
                        expr_126_cp_0[(int)expr_126_cp_1].m_flags = (expr_126_cp_0[(int)expr_126_cp_1].m_flags | Citizen.Flags.Original);
                        if (building != 0)
                        {
                            DistrictManager instance2 = Singleton<DistrictManager>.instance;
                            Vector3 position = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)building].m_position;
                            byte district = instance2.GetDistrict(position);
                            District[] expr_183_cp_0_cp_0 = instance2.m_districts.m_buffer;
                            byte expr_183_cp_0_cp_1 = district;
                            expr_183_cp_0_cp_0[(int)expr_183_cp_0_cp_1].m_birthData.m_tempCount = expr_183_cp_0_cp_0[(int)expr_183_cp_0_cp_1].m_birthData.m_tempCount + 1u;
                        }
                    }
                }
            }
            if (data.m_citizen0 != 0u && data.m_citizen1 == 0u)
            {
                this.TryFindPartner(data.m_citizen0, ref instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen0)]);
            }
            else if (data.m_citizen1 != 0u && data.m_citizen0 == 0u)
            {
                this.TryFindPartner(data.m_citizen1, ref instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen1)]);
            }
            if (data.m_citizen2 != 0u)
            {
                this.TryMoveAwayFromHome_1(data.m_citizen2, ref instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen2)]);
            }
            if (data.m_citizen3 != 0u)
            {
                this.TryMoveAwayFromHome_1(data.m_citizen3, ref instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen3)]);
            }
            if (data.m_citizen4 != 0u)
            {
                this.TryMoveAwayFromHome_1(data.m_citizen4, ref instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen4)]);
            }
            int temp_num = process_family(homeID, ref data);


            data.m_goods = (ushort)Mathf.Max(0, (int)(data.m_goods - temp_num)); //here we can adjust demand

            if (data.m_goods < 20000)
            { 
                SimulationManager instance2 = Singleton<SimulationManager>.instance;
                float currentDayTimeHour = instance2.m_currentDayTimeHour;
                if (currentDayTimeHour > 20f || currentDayTimeHour < 5f)
                {
                    if (instance2.m_randomizer.Int32((uint)data.m_goods + 1) < temp_num * 200)
                    {
                        Chancetodovitureshopping(homeID, ref data);
                    }
                }
                else
                {
                    if (instance2.m_randomizer.Int32((uint)data.m_goods + 1) < (temp_num * 140))
                    {
                        Chancetodovitureshopping(homeID, ref data);
                    }
                }
            }


            //sick
            if (data.m_goods < 10000 || ((comm_data.family_money[homeID]) < 0 &&(comm_data.family_profit_status[homeID] < 10)))
            {
                if (Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.HealthCare))
                {
                    int num2 = Singleton<SimulationManager>.instance.m_randomizer.Int32(5u);
                    for (int i = 0; i < 5; i++)
                    {
                        uint citizen = data.GetCitizen((num2 + i) % 5);
                        if (citizen != 0u)
                        {
                            SimulationManager instance2 = Singleton<SimulationManager>.instance;
                            Citizen[] expr_2FA_cp_0 = instance.m_citizens.m_buffer;
                            if (instance2.m_randomizer.Int32(data.m_goods) < 50)
                            {
                                expr_2FA_cp_0[citizen].Sick = true;
                            }
                            break;
                        }
                    }
                }
            }

            if (data.m_goods < 20000)
            {
                int num2 = Singleton<SimulationManager>.instance.m_randomizer.Int32(5u);
                for (int i = 0; i < 5; i++)
                {
                    uint citizen = data.GetCitizen((num2 + i) % 5);
                    if (citizen != 0u)
                    {
                        Citizen[] expr_2FA_cp_0 = instance.m_citizens.m_buffer;
                        UIntPtr expr_2FA_cp_1 = (UIntPtr)citizen;
                        expr_2FA_cp_0[(int)expr_2FA_cp_1].m_flags = (expr_2FA_cp_0[(int)expr_2FA_cp_1].m_flags | Citizen.Flags.NeedGoods);
                        break;
                    }
                }
            }
            if (building != 0 && (Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)building].m_problems & (Notification.Problem.MajorProblem | Notification.Problem.FatalProblem)) != Notification.Problem.None)
            {
                uint num3 = 0u;
                int num4 = 0;
                if (data.m_citizen4 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen4)].Dead)
                {
                    num4++;
                    num3 = data.m_citizen4;
                }
                if (data.m_citizen3 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen3)].Dead)
                {
                    num4++;
                    num3 = data.m_citizen3;
                }
                if (data.m_citizen2 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen2)].Dead)
                {
                    num4++;
                    num3 = data.m_citizen2;
                }
                if (data.m_citizen1 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen1)].Dead)
                {
                    num4++;
                    num3 = data.m_citizen1;
                }
                if (data.m_citizen0 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen0)].Dead)
                {
                    num4++;
                    num3 = data.m_citizen0;
                }
                if (num3 != 0u)
                {
                    this.TryMoveFamily_1(homeID, num3, ref instance.m_citizens.m_buffer[(int)((UIntPtr)num3)], num4);
                }
            }
        }

        public int GetexpenseRate(uint homeid, uint citizen_id, out int incomeAccumulation)
        {
            BuildingManager instance1 = Singleton<BuildingManager>.instance;
            CitizenManager instance2 = Singleton<CitizenManager>.instance;
            ItemClass @class = instance1.m_buildings.m_buffer[instance2.m_citizens.m_buffer[citizen_id].m_homeBuilding].Info.m_class;
            incomeAccumulation = 0;
            DistrictManager instance = Singleton<DistrictManager>.instance;
            if (instance2.m_citizens.m_buffer[citizen_id].m_homeBuilding != 0)
            {
                byte district = instance.GetDistrict(instance1.m_buildings.m_buffer[instance2.m_citizens.m_buffer[citizen_id].m_homeBuilding].m_position);
                DistrictPolicies.Taxation taxationPolicies = instance.m_districts.m_buffer[(int)district].m_taxationPolicies;
                if (@class.m_subService == ItemClass.SubService.ResidentialLow)
                {
                    switch (@class.m_level)
                    {
                        case ItemClass.Level.Level1:
                            incomeAccumulation = comm_data.resident_low_level1_rent;
                            break;
                        case ItemClass.Level.Level2:
                            incomeAccumulation = comm_data.resident_low_level2_rent;
                            break;
                        case ItemClass.Level.Level3:
                            incomeAccumulation = comm_data.resident_low_level3_rent;
                            break;
                        case ItemClass.Level.Level4:
                            incomeAccumulation = comm_data.resident_low_level4_rent;
                            break;
                        case ItemClass.Level.Level5:
                            incomeAccumulation = comm_data.resident_low_level5_rent;
                            break;
                    }
                }
                else if (@class.m_subService == ItemClass.SubService.ResidentialLowEco)
                {
                    switch (@class.m_level)
                    {
                        case ItemClass.Level.Level1:
                            incomeAccumulation = comm_data.resident_low_eco_level1_rent;
                            break;
                        case ItemClass.Level.Level2:
                            incomeAccumulation = comm_data.resident_low_eco_level2_rent;
                            break;
                        case ItemClass.Level.Level3:
                            incomeAccumulation = comm_data.resident_low_eco_level3_rent;
                            break;
                        case ItemClass.Level.Level4:
                            incomeAccumulation = comm_data.resident_low_eco_level4_rent;
                            break;
                        case ItemClass.Level.Level5:
                            incomeAccumulation = comm_data.resident_low_eco_level5_rent;
                            break;
                    }
                }
                else if (@class.m_subService == ItemClass.SubService.ResidentialHigh)
                {
                    switch (@class.m_level)
                    {
                        case ItemClass.Level.Level1:
                            incomeAccumulation = comm_data.resident_high_level1_rent;
                            break;
                        case ItemClass.Level.Level2:
                            incomeAccumulation = comm_data.resident_high_level2_rent;
                            break;
                        case ItemClass.Level.Level3:
                            incomeAccumulation = comm_data.resident_high_level3_rent;
                            break;
                        case ItemClass.Level.Level4:
                            incomeAccumulation = comm_data.resident_high_level4_rent;
                            break;
                        case ItemClass.Level.Level5:
                            incomeAccumulation = comm_data.resident_high_level5_rent;
                            break;
                    }
                }
                else
                {
                    switch (@class.m_level)
                    {
                        case ItemClass.Level.Level1:
                            incomeAccumulation = comm_data.resident_high_eco_level1_rent;
                            break;
                        case ItemClass.Level.Level2:
                            incomeAccumulation = comm_data.resident_high_eco_level2_rent;
                            break;
                        case ItemClass.Level.Level3:
                            incomeAccumulation = comm_data.resident_high_eco_level3_rent;
                            break;
                        case ItemClass.Level.Level4:
                            incomeAccumulation = comm_data.resident_high_eco_level4_rent;
                            break;
                        case ItemClass.Level.Level5:
                            incomeAccumulation = comm_data.resident_high_eco_level5_rent;
                            break;
                    }
                }
                int num2;
                num2 = Singleton<EconomyManager>.instance.GetTaxRate(@class, taxationPolicies);
                if (comm_data.family_money[homeid] > 0)
                {
                    incomeAccumulation = (int)(num2 * incomeAccumulation * ((float)(instance.m_districts.m_buffer[(int)district].GetLandValue() + 50) / 10000));
                } else
                {
                    incomeAccumulation = 0;
                }
            }

            int temp = 0;
            if ((Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_flags & Citizen.Flags.Student) != Citizen.Flags.None)
            {
                if (comm_data.family_money[homeid] > 0 && (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_flags.IsFlagSet(Citizen.Flags.Education2)))
                {
                    temp = temp + 20;
                    Singleton<EconomyManager>.instance.AddPrivateIncome(20, ItemClass.Service.Education, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                }

                if (comm_data.family_money[homeid] > 0 && (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_flags.IsFlagSet(Citizen.Flags.Education1)))
                {
                    temp = temp + 10;
                    Singleton<EconomyManager>.instance.AddPrivateIncome(10, ItemClass.Service.Education, ItemClass.SubService.None, ItemClass.Level.Level2, 115);
                }

                if (comm_data.family_money[homeid] > 0)
                {
                    if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_flags.IsFlagSet(Citizen.Flags.Education1))
                    {

                    }
                    else if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_flags.IsFlagSet(Citizen.Flags.Education2))
                    {

                    }
                    else if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_flags.IsFlagSet(Citizen.Flags.Education3))
                    {

                    }
                    else
                    {
                        temp = temp + 5;
                        Singleton<EconomyManager>.instance.AddPrivateIncome(5, ItemClass.Service.Education, ItemClass.SubService.None, ItemClass.Level.Level2, 115);
                    }
                }

            }

            if (UnlockManager.instance.Unlocked(ItemClass.Service.HealthCare))
            {
                if ((Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_flags & Citizen.Flags.Sick) != Citizen.Flags.None)
                {
                    if ((comm_data.family_money[homeid] > 0) && pc_OutsideConnectionAI.have_hospital_building && !comm_data.hospitalhelp)
                    {
                        temp = temp + 30;
                        // 10% is provide by citizen  90% is provide by goverment
                        Singleton<EconomyManager>.instance.AddPrivateIncome(30, ItemClass.Service.HealthCare, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, 120, ItemClass.Service.HealthCare, ItemClass.SubService.None, ItemClass.Level.Level1);
                        comm_data.city_insurance_account -= 120;
                    }
                    else if ((comm_data.family_money[homeid] < 0) && pc_OutsideConnectionAI.have_hospital_building && !comm_data.hospitalhelp)
                    {
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, 150, ItemClass.Service.HealthCare, ItemClass.SubService.None, ItemClass.Level.Level1);
                        comm_data.city_insurance_account -= 150;
                    }
                    else
                    {
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, 150, ItemClass.Service.HealthCare, ItemClass.SubService.None, ItemClass.Level.Level1);
                        comm_data.city_insurance_account -= 150;
                    }
                }
            }
            return temp;
        }

        public static void Die(uint citizenID, ref Citizen data)
        {
            data.Sick = false;
            data.Dead = true;
            data.SetParkedVehicle(citizenID, 0);
            if ((data.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None)
            {
                ushort num = data.GetBuildingByLocation();
                if (num == 0)
                {
                    num = data.m_homeBuilding;
                }
                if (num != 0)
                {
                    DistrictManager instance = Singleton<DistrictManager>.instance;
                    Vector3 position = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)num].m_position;
                    byte district = instance.GetDistrict(position);
                    District[] expr_7D_cp_0_cp_0 = instance.m_districts.m_buffer;
                    byte expr_7D_cp_0_cp_1 = district;
                    expr_7D_cp_0_cp_0[(int)expr_7D_cp_0_cp_1].m_deathData.m_tempCount = expr_7D_cp_0_cp_0[(int)expr_7D_cp_0_cp_1].m_deathData.m_tempCount + 1u;
                }
            }
        }

        private bool IsRoadConnection(ushort building)
        {
            if (building != 0)
            {
                BuildingManager instance = Singleton<BuildingManager>.instance;
                if ((instance.m_buildings.m_buffer[(int)building].m_flags & Building.Flags.IncomingOutgoing) != Building.Flags.None && instance.m_buildings.m_buffer[(int)building].Info.m_class.m_service == ItemClass.Service.Road)
                {
                    return true;
                }
            }
            return false;
        }



        public void is_outside_movingin(ushort instanceID, ref CitizenInstance data, ushort targetBuilding)
        {
            System.Random rand = new System.Random();
            int homeBuilding1 = Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].m_homeBuilding;
            uint containingUnit = Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].GetContainingUnit(data.m_citizen, Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)homeBuilding1].m_citizenUnits, CitizenUnit.Flags.Home);
            if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].m_flags.IsFlagSet(Citizen.Flags.MovingIn))
            {
                if (comm_data.citizen_salary_per_family > 0)
                {
                    comm_data.citizen_money[data.m_citizen] = rand.Next(comm_data.citizen_salary_per_family + 1) * 200;
                }
            }
        }

        public override void SetTarget(ushort instanceID, ref CitizenInstance data, ushort targetBuilding)
        {
            int dayTimeFrame = (int)Singleton<SimulationManager>.instance.m_dayTimeFrame;
            int dAYTIME_FRAMES = (int)SimulationManager.DAYTIME_FRAMES;
            int num = Mathf.Max(dAYTIME_FRAMES >> 2, Mathf.Abs(dayTimeFrame - (dAYTIME_FRAMES >> 1)));
            if (Singleton<SimulationManager>.instance.m_randomizer.Int32((uint)dAYTIME_FRAMES >> 1) < num)
            {
                data.m_flags &= ~CitizenInstance.Flags.CannotUseTaxi;
            }
            else
            {
                data.m_flags |= CitizenInstance.Flags.CannotUseTaxi;
            }
            data.m_flags &= ~CitizenInstance.Flags.CannotUseTransport;



            if (targetBuilding != data.m_targetBuilding)
            {
                if (data.m_targetBuilding != 0)
                {
                    Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_targetBuilding].RemoveTargetCitizen(instanceID, ref data);
                }
                data.m_targetBuilding = targetBuilding;
                if (data.m_targetBuilding != 0)
                {
                    Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_targetBuilding].AddTargetCitizen(instanceID, ref data);
                    data.m_targetSeed = (byte)Singleton<SimulationManager>.instance.m_randomizer.Int32(256u);
                }
            }
            if (this.IsRoadConnection(targetBuilding) || this.IsRoadConnection(data.m_sourceBuilding))
            {
                data.m_flags |= CitizenInstance.Flags.BorrowCar;
                is_outside_movingin(instanceID, ref data, targetBuilding);
            }
            else
            {
                data.m_flags &= ~CitizenInstance.Flags.BorrowCar;
            }
            if (targetBuilding != 0 && (data.m_flags & CitizenInstance.Flags.Character) == CitizenInstance.Flags.None)
            {
                ushort eventIndex = 0;
                if (data.m_citizen != 0u && Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].m_workBuilding != targetBuilding)
                {
                    eventIndex = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)targetBuilding].m_eventIndex;
                }
                Color32 eventCitizenColor = Singleton<EventManager>.instance.GetEventCitizenColor(eventIndex, data.m_citizen);
                if (eventCitizenColor.a == 255)
                {
                    data.m_color = eventCitizenColor;
                    data.m_flags |= CitizenInstance.Flags.CustomColor;
                }
            }

            Citizen.AgePhase temp_agephase = data.Info.m_agePhase;
            ushort temp_parkedVehicle = 0;
            if ((Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].CurrentLocation == Citizen.Location.Home) && (!(targetBuilding == (Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].m_workBuilding))))
            { 
                data.Info.m_agePhase = canusetransport(instanceID, ref data, targetBuilding);
            }

            if (data.Info.m_agePhase == Citizen.AgePhase.Child)
            {
                if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].m_parkedVehicle != 0)
                {
                    if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].CurrentLocation == Citizen.Location.Home)
                    {
                        temp_parkedVehicle = Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].m_parkedVehicle;
                        Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].m_parkedVehicle = 0;
                    }
                }
            }


            if (!this.StartPathFind(instanceID, ref data))
            {
                data.Info.m_agePhase = temp_agephase;
                data.Unspawn(instanceID);
                if (temp_parkedVehicle != 0)
                {
                    Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].m_parkedVehicle = temp_parkedVehicle;
                }
            }
            data.Info.m_agePhase = temp_agephase;
            if (temp_parkedVehicle != 0)
            {
                Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].m_parkedVehicle = temp_parkedVehicle;
            }
        }

        public Citizen.AgePhase canusetransport (ushort instanceID, ref CitizenInstance citizenData, ushort targetBuilding)
        {
            uint citizen = citizenData.m_citizen;
            System.Random rand = new System.Random();
            Citizen.AgePhase temp_agephase = citizenData.Info.m_agePhase;
            int homeBuilding1 = Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_homeBuilding;
            uint containingUnit = Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].GetContainingUnit(citizen, Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)homeBuilding1].m_citizenUnits, CitizenUnit.Flags.Home);
            int temp_money = (comm_data.family_money[containingUnit] > 1) ? (int)comm_data.family_money[containingUnit] : 1;
            if (rand.Next(temp_money) < 4000)
            {
                BuildingManager instance3 = Singleton<BuildingManager>.instance;
                DistrictManager instance4 = Singleton<DistrictManager>.instance;

                byte district = 0;
                if (citizenData.m_sourceBuilding != 0)
                {
                    district = instance4.GetDistrict(instance3.m_buildings.m_buffer[citizenData.m_sourceBuilding].m_position);
                }

                byte district1 = 0;
                if (targetBuilding != 0)
                {
                    district1 = instance4.GetDistrict(instance3.m_buildings.m_buffer[targetBuilding].m_position);
                }
                DistrictPolicies.Services servicePolicies = instance4.m_districts.m_buffer[(int)district].m_servicePolicies;
                DistrictPolicies.Event @event = instance4.m_districts.m_buffer[(int)district].m_eventPolicies & Singleton<EventManager>.instance.GetEventPolicyMask();
                DistrictPolicies.Services servicePolicies1 = instance4.m_districts.m_buffer[(int)district1].m_servicePolicies;
                DistrictPolicies.Event @event1 = instance4.m_districts.m_buffer[(int)district1].m_eventPolicies & Singleton<EventManager>.instance.GetEventPolicyMask();
                citizenData.m_flags = (citizenData.m_flags | CitizenInstance.Flags.CannotUseTaxi);
                citizenData.m_flags = (citizenData.m_flags | CitizenInstance.Flags.CannotUseTransport);
                temp_agephase = Citizen.AgePhase.Child;
                if ((citizenData.m_sourceBuilding != 0) && (targetBuilding != 0))
                {
                    if (((servicePolicies & DistrictPolicies.Services.FreeTransport) != DistrictPolicies.Services.None) || ((@event & DistrictPolicies.Event.ComeOneComeAll) != DistrictPolicies.Event.None))
                    {
                        if (((servicePolicies1 & DistrictPolicies.Services.FreeTransport) != DistrictPolicies.Services.None) || ((@event1 & DistrictPolicies.Event.ComeOneComeAll) != DistrictPolicies.Event.None))
                        {
                            citizenData.m_flags = (citizenData.m_flags & (~CitizenInstance.Flags.CannotUseTransport));
                            //DebugLog.LogToFileOnly("public transport free, people can use transport now");
                        }
                    }
                }
            }
            else
            {
                BuildingManager instance3 = Singleton<BuildingManager>.instance;
                DistrictManager instance4 = Singleton<DistrictManager>.instance;

                byte district = 0;
                if (citizenData.m_sourceBuilding != 0)
                {
                    district = instance4.GetDistrict(instance3.m_buildings.m_buffer[citizenData.m_sourceBuilding].m_position);
                }

                byte district1 = 0;
                if (targetBuilding != 0)
                {
                    district1 = instance4.GetDistrict(instance3.m_buildings.m_buffer[targetBuilding].m_position);
                }
                DistrictPolicies.Services servicePolicies = instance4.m_districts.m_buffer[(int)district].m_servicePolicies;
                DistrictPolicies.Event @event = instance4.m_districts.m_buffer[(int)district].m_eventPolicies & Singleton<EventManager>.instance.GetEventPolicyMask();
                DistrictPolicies.Services servicePolicies1 = instance4.m_districts.m_buffer[(int)district1].m_servicePolicies;
                DistrictPolicies.Event @event1 = instance4.m_districts.m_buffer[(int)district1].m_eventPolicies & Singleton<EventManager>.instance.GetEventPolicyMask();
                if (rand.Next((int)comm_data.family_money[containingUnit]) < 6000)
                {
                    citizenData.m_flags = (citizenData.m_flags | CitizenInstance.Flags.CannotUseTaxi);
                }

                if (rand.Next((int)comm_data.family_money[containingUnit]) < 5000)
                {
                    temp_agephase = Citizen.AgePhase.Child; // do not use car
                }                    //citizenData.m_flags = (citizenData.m_flags | CitizenInstance.Flags.CannotUseTransport);
                if ((citizenData.m_sourceBuilding != 0) && (targetBuilding != 0))
                {
                    if (((servicePolicies & DistrictPolicies.Services.FreeTransport) != DistrictPolicies.Services.None) || ((@event & DistrictPolicies.Event.ComeOneComeAll) != DistrictPolicies.Event.None))
                    {
                        if (((servicePolicies1 & DistrictPolicies.Services.FreeTransport) != DistrictPolicies.Services.None) || ((@event1 & DistrictPolicies.Event.ComeOneComeAll) != DistrictPolicies.Event.None))
                        {
                            citizenData.m_flags = (citizenData.m_flags & (~CitizenInstance.Flags.CannotUseTransport));
                            //DebugLog.LogToFileOnly("public transport free, people can use transport now");
                        }
                    }
                }
            }

            return temp_agephase;
        }
    }
}


namespace RealCity
{
    public class pc_ResidentAI_1 : HumanAI
    {
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

                    num = pc_ResidentAI.FindNotSoCloseBuilding(frameData.m_position, 64f, ItemClass.Service.Commercial, ItemClass.SubService.None, Building.Flags.Created, Building.Flags.Deleted | Building.Flags.Abandoned);
                    if (homeBuilding != 0 && num != 0)
                    {
                        BuildingInfo info = instance2.m_buildings.m_buffer[(int)num].Info;
                        int num2 = -100;
                        //TransferManager.TransferReason temp_reason = pc_HumanAI.get_shopping_reason(num);
                        TransferManager.TransferReason temp_reason = TransferManager.TransferReason.Shopping;
                        info.m_buildingAI.ModifyMaterialBuffer(num, ref instance2.m_buildings.m_buffer[(int)num], temp_reason, ref num2);
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
