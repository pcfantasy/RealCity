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
        public static byte[] load_data = new byte[140];

        public static void load()
        {
            int i = 0;
            precitizenid = saveandrestore.load_uint(ref i, load_data);
            family_count = saveandrestore.load_int(ref i, load_data);
            family_very_profit_money_num = saveandrestore.load_uint(ref i, load_data);
            family_profit_money_num = saveandrestore.load_uint(ref i, load_data);
            family_loss_money_num = saveandrestore.load_uint(ref i, load_data);
            citizen_salary_count = saveandrestore.load_int(ref i, load_data);
            citizen_expense_count = saveandrestore.load_int(ref i, load_data);
            citizen_salary_tax_total = saveandrestore.load_int(ref i, load_data);
            temp_citizen_salary_tax_total = saveandrestore.load_float(ref i, load_data);

            Road = saveandrestore.load_int(ref i, load_data);
            Electricity = saveandrestore.load_int(ref i, load_data);
            Water = saveandrestore.load_int(ref i, load_data);
            Beautification = saveandrestore.load_int(ref i, load_data);
            Garbage = saveandrestore.load_int(ref i, load_data);
            HealthCare = saveandrestore.load_int(ref i, load_data);
            PoliceDepartment = saveandrestore.load_int(ref i, load_data);
            Education = saveandrestore.load_int(ref i, load_data);
            Monument = saveandrestore.load_int(ref i, load_data);
            FireDepartment = saveandrestore.load_int(ref i, load_data);
            PublicTransport_bus = saveandrestore.load_int(ref i, load_data);
            PublicTransport_tram = saveandrestore.load_int(ref i, load_data);
            PublicTransport_ship = saveandrestore.load_int(ref i, load_data);
            PublicTransport_plane = saveandrestore.load_int(ref i, load_data);
            PublicTransport_metro = saveandrestore.load_int(ref i, load_data);
            PublicTransport_train = saveandrestore.load_int(ref i, load_data);
            PublicTransport_taxi = saveandrestore.load_int(ref i, load_data);
            PublicTransport_cablecar = saveandrestore.load_int(ref i, load_data);
            PublicTransport_monorail = saveandrestore.load_int(ref i, load_data);
            Disaster = saveandrestore.load_int(ref i, load_data);

            family_weight_stable_high = saveandrestore.load_uint(ref i, load_data);
            family_weight_stable_low = saveandrestore.load_uint(ref i, load_data);

            citizen_goods = saveandrestore.load_long(ref i, load_data);
            citizen_goods_temp = saveandrestore.load_long(ref i, load_data);

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


        public int citizen_salary(uint citizen_id)
        {
            int num = 0;
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
                                    switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                                    {
                                        case Citizen.Education.Uneducated:
                                            num = num + (int)(comm_data.comm_high_level1_education0) + rand.Next(1); break;
                                        case Citizen.Education.OneSchool:
                                            num = num + (int)(comm_data.comm_high_level1_education1) + rand.Next(2); break;
                                        case Citizen.Education.TwoSchools:
                                            num = num + (int)(comm_data.comm_high_level1_education2) + rand.Next(3); break;
                                        case Citizen.Education.ThreeSchools:
                                            num = num + (int)(comm_data.comm_high_level1_education3) + rand.Next(4); break;
                                    }
                                    break;
                                case ItemClass.Level.Level2:
                                    switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                                    {
                                        case Citizen.Education.Uneducated:
                                            num = num + (int)(comm_data.comm_high_level2_education0) + rand.Next(1); break;
                                        case Citizen.Education.OneSchool:
                                            num = num + (int)(comm_data.comm_high_level2_education1) + rand.Next(2); break;
                                        case Citizen.Education.TwoSchools:
                                            num = num + (int)(comm_data.comm_high_level2_education2) + rand.Next(3); break;
                                        case Citizen.Education.ThreeSchools:
                                            num = num + (int)(comm_data.comm_high_level2_education3) + rand.Next(4); break;
                                    }
                                    break;
                                case ItemClass.Level.Level3:
                                    switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                                    {
                                        case Citizen.Education.Uneducated:
                                            num = num + (int)(comm_data.comm_high_level3_education0) + rand.Next(1); break;
                                        case Citizen.Education.OneSchool:
                                            num = num + (int)(comm_data.comm_high_level3_education1) + rand.Next(2); break;
                                        case Citizen.Education.TwoSchools:
                                            num = num + (int)(comm_data.comm_high_level3_education2) + rand.Next(3); break;
                                        case Citizen.Education.ThreeSchools:
                                            num = num + (int)(comm_data.comm_high_level3_education3) + rand.Next(4); break;
                                    }
                                    break;
                            }
                            break; //
                        case ItemClass.SubService.CommercialLow:
                            switch (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_level)
                            {
                                case ItemClass.Level.Level1:
                                    switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                                    {
                                        case Citizen.Education.Uneducated:
                                            num = num + (int)(comm_data.comm_low_level1_education0) + rand.Next(1); break;
                                        case Citizen.Education.OneSchool:
                                            num = num + (int)(comm_data.comm_low_level1_education1) + rand.Next(2); break;
                                        case Citizen.Education.TwoSchools:
                                            num = num + (int)(comm_data.comm_low_level1_education2) + rand.Next(3); break;
                                        case Citizen.Education.ThreeSchools:
                                            num = num + (int)(comm_data.comm_low_level1_education3) + rand.Next(4); break;
                                    }
                                    break;
                                case ItemClass.Level.Level2:
                                    switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                                    {
                                        case Citizen.Education.Uneducated:
                                            num = num + (int)(comm_data.comm_low_level2_education0) + rand.Next(1); break;
                                        case Citizen.Education.OneSchool:
                                            num = num + (int)(comm_data.comm_low_level2_education1) + rand.Next(2); break;
                                        case Citizen.Education.TwoSchools:
                                            num = num + (int)(comm_data.comm_low_level2_education2) + rand.Next(3); break;
                                        case Citizen.Education.ThreeSchools:
                                            num = num + (int)(comm_data.comm_low_level2_education3) + rand.Next(4); break;
                                    }
                                    break;
                                case ItemClass.Level.Level3:
                                    switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                                    {
                                        case Citizen.Education.Uneducated:
                                            num = num + (int)(comm_data.comm_low_level3_education0) + rand.Next(1); break;
                                        case Citizen.Education.OneSchool:
                                            num = num + (int)(comm_data.comm_low_level3_education1) + rand.Next(2); break;
                                        case Citizen.Education.TwoSchools:
                                            num = num + (int)(comm_data.comm_low_level3_education2) + rand.Next(3); break;
                                        case Citizen.Education.ThreeSchools:
                                            num = num + (int)(comm_data.comm_low_level3_education3) + rand.Next(4); break;
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
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.indus_far_education0) + rand.Next(1); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.indus_far_education1) + rand.Next(2); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.indus_far_education2) + rand.Next(3); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.indus_far_education3) + rand.Next(4); break;
                            }
                            break; //
                        case ItemClass.SubService.IndustrialForestry:
                            if (rand.Next(1000) < 10)
                            {
                                if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].CurrentLocation == Citizen.Location.Work)
                                {
                                    if (Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.HealthCare))
                                    {
                                        Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].Sick = true;
                                    }
                                }
                            }

                            if (rand.Next(1000) < 5)
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
                            break; //
                        case ItemClass.SubService.IndustrialOil:
                            if (rand.Next(1000) < 30)
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
                            break; //
                        case ItemClass.SubService.IndustrialOre:
                            if (rand.Next(1000) < 20)
                            {
                                if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].CurrentLocation == Citizen.Location.Work)
                                {
                                    if (Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.HealthCare))
                                    {
                                        Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].Sick = true;
                                    }
                                }
                            }

                            if (rand.Next(1000) < 5)
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
                            break; //
                        /*case ItemClass.SubService.OfficeGeneric:
                            switch (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_level)
                            {
                                case ItemClass.Level.Level1:
                                    switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                                    {
                                        case Citizen.Education.Uneducated:
                                            num = num + (int)(comm_data.office_gen_level1_education0) + rand.Next(1); break;
                                        case Citizen.Education.OneSchool:
                                            num = num + (int)(comm_data.office_gen_level1_education1) + rand.Next(2); break;
                                        case Citizen.Education.TwoSchools:
                                            num = num + (int)(comm_data.office_gen_level1_education2) + rand.Next(3);
                                            num = (int)(num); break;
                                        case Citizen.Education.ThreeSchools:
                                            num = num + (int)(comm_data.office_gen_level1_education3) + rand.Next(4);
                                            num = (int)(num); break;
                                    }
                                    break;
                                case ItemClass.Level.Level2:
                                    switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                                    {
                                        case Citizen.Education.Uneducated:
                                            num = num + (int)(comm_data.office_gen_level2_education0) + rand.Next(1); break;
                                        case Citizen.Education.OneSchool:
                                            num = num + (int)(comm_data.office_gen_level2_education1) + rand.Next(2); break;
                                        case Citizen.Education.TwoSchools:
                                            num = num + (int)(comm_data.office_gen_level2_education2) + rand.Next(3);
                                            num = (int)(num); break;
                                        case Citizen.Education.ThreeSchools:
                                            num = num + (int)(comm_data.office_gen_level2_education3) + rand.Next(4);
                                            num = (int)(num); break;
                                    }
                                    break;
                                case ItemClass.Level.Level3:
                                    switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                                    {
                                        case Citizen.Education.Uneducated:
                                            num = num + (int)(comm_data.office_gen_level3_education0) + rand.Next(1); break;
                                        case Citizen.Education.OneSchool:
                                            num = num + (int)(comm_data.office_gen_level3_education1) + rand.Next(2); break;
                                        case Citizen.Education.TwoSchools:
                                            num = num + (int)(comm_data.office_gen_level3_education2) + rand.Next(3);
                                            num = (int)(num); break;
                                        case Citizen.Education.ThreeSchools:
                                            num = num + (int)(comm_data.office_gen_level3_education3) + rand.Next(4);
                                            num = (int)(num); break;
                                    }
                                    break;
                            }
                            break; //
                        case ItemClass.SubService.OfficeHightech:
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.office_high_tech_education0) + rand.Next(1); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.office_high_tech_education1) + rand.Next(2); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.office_high_tech_education2) + rand.Next(3);
                                    num = (int)(num); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.office_high_tech_education3) + rand.Next(4);
                                    num = (int)(num); break;
                            }
                            break; //*/
                        case ItemClass.SubService.CommercialLeisure:
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.comm_lei_education0) + rand.Next(1); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.comm_lei_education1) + rand.Next(2); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.comm_lei_education2) + rand.Next(3); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.comm_lei_education3) + rand.Next(4); break;
                            }
                            break; //
                        case ItemClass.SubService.CommercialTourist:
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.comm_tou_education0) + rand.Next(1); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.comm_tou_education1) + rand.Next(2); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.comm_tou_education2) + rand.Next(3); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.comm_tou_education3) + rand.Next(4); break;
                            }
                            break; //
                        case ItemClass.SubService.CommercialEco:
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.comm_eco_education0) + rand.Next(1); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.comm_eco_education1) + rand.Next(2); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.comm_eco_education2) + rand.Next(3); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.comm_eco_education3) + rand.Next(4); break;
                            }
                            break; //
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
                                if (aliveworkcount1 != 0)
                                {
                                    num = (int)(comm_data.building_money[work_building] / aliveworkcount1);
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
                    if (num == 0)
                    {
                        DebugLog.LogToFileOnly("find unknown citizen workbuilding" + " building servise is" + Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_service + " building subservice is" + Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_subService);
                    }


                    int aliveworkcount = 0;
                    int totalworkcount = 0;
                    Citizen.BehaviourData behaviour = default(Citizen.BehaviourData);
                    BuildingUI.GetWorkBehaviour((ushort)work_building, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building], ref behaviour, ref aliveworkcount, ref totalworkcount);

                    if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_service == ItemClass.Service.Office)
                    {
                        //num = num;
                    }
                    else if (comm_data.building_money[Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_workBuilding] < 0)
                    {
                        num = (int)((float)num * comm_data.salary_idex / 3f + 0.5f);
                        num = 0;
                    }
                    else if (comm_data.building_money[Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_workBuilding] > 1000 + aliveworkcount * 200)
                    {
                        num = (int)((float)num * comm_data.salary_idex * 1.33f + 0.5f);
                    }
                    else if (budget != 0)
                    {
                        num = (int)(num * budget / 100f);
                    }
                    else
                    {
                        num = (int)((float)num * comm_data.salary_idex + 0.5f);
                    }

                    if ((Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_service == ItemClass.Service.Commercial) || (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_service == ItemClass.Service.Industrial))
                    {

                        if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Width * Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Length <= 12)
                        {
                            num = (int)(num / 1.1f);
                        }

                        if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Width * Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Length <= 9)
                        {
                            num = (int)(num / 1.1f);
                        }

                        if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Width * Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Length <= 6)
                        {
                            num = (int)(num / 1.1f);
                        }

                        if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Width * Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Length <= 4)
                        {
                            num = (int)(num / 1.1f);
                        }

                        if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Width * Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Length <= 2)
                        {
                            num = (int)(num / 1.1f);
                        }
                    }

                    if ((Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_service == ItemClass.Service.Commercial) || (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_service == ItemClass.Service.Industrial) || (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_service == ItemClass.Service.Office))
                    {
                        if (comm_data.building_money[Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_workBuilding] > 0)
                        {
                            comm_data.building_money[Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_workBuilding] -= num * 0.1f;
                            comm_data.city_insurance_account += num * 0.1f;
                            if (comm_data.is_help_resident)
                            {
                                comm_data.building_money[Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_workBuilding] -= num * 0.2f;
                                comm_data.city_insurance_account += num * 0.2f;
                            }
                        }
                    }

                }
           }//if (citizen_id != 0u)
            return num;
        }//public

        public byte process_citizen(uint homeID, ref CitizenUnit data)
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
            if (comm_data.is_help_resident)
            {
                insurance = 0.5f * citizen_salary_current;
                comm_data.city_insurance_account += 0.2f * citizen_salary_current;
                citizen_salary_current = 0.8f * citizen_salary_current;
            }
            else
            {
                insurance = 0.2f * citizen_salary_current;
                comm_data.city_insurance_account += 0.1f * citizen_salary_current;
                citizen_salary_current = 0.9f * citizen_salary_current;
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
            process_citizen_income_tax(homeID, tax);
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
            comm_data.citizen_money[homeID] = (short)(comm_data.citizen_money[homeID] + temp_num);
            //process citizen status
            System.Random rand = new System.Random();
            if (temp_num <= 0)
            {
                temp_num = rand.Next(5);
                family_loss_money_num = (uint)(family_loss_money_num + 1);
                comm_data.citizen_profit_status[homeID]--;
                //try_move_family to do here;
            }
            else if (temp_num > 80)
            {
                temp_num = rand.Next(temp_num);
                family_very_profit_money_num = (uint)(family_very_profit_money_num + 1);
                comm_data.citizen_profit_status[homeID]++;
            }
            else
            {
                temp_num = rand.Next(temp_num);
                family_profit_money_num = (uint)(family_profit_money_num + 1);
            }

            temp_num = (temp_num > 200) ? 200 : temp_num;

            if (comm_data.citizen_money[homeID] > 32000000f)
            {
                comm_data.citizen_money[homeID] = 32000000f;
            }

            if (comm_data.citizen_money[homeID] < -32000000f)
            {
                comm_data.citizen_money[homeID] = -32000000f;
            }

            if (comm_data.citizen_profit_status[homeID] > 250)
            {
                comm_data.citizen_profit_status[homeID] = 250;
            }
            if (comm_data.citizen_profit_status[homeID] < 5)
            {
                comm_data.citizen_profit_status[homeID] = 5;
            }

            ItemClass.Level home_level = Singleton<BuildingManager>.instance.m_buildings.m_buffer[Singleton<CitizenManager>.instance.m_units.m_buffer[(int)((UIntPtr)homeID)].m_building].Info.m_class.m_level;
            if ((comm_data.citizen_money[homeID] >= 10000) && (comm_data.citizen_profit_status[homeID] >= 230))
            {
                family_weight_stable_high = (ushort)(family_weight_stable_high + 1);
                if ((home_level == ItemClass.Level.Level1) || (home_level == ItemClass.Level.Level2) || (home_level == ItemClass.Level.Level3))
                {
                    if (rand.Next(100) < 2)
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
            else if ((comm_data.citizen_money[homeID] <= 0) && (comm_data.citizen_profit_status[homeID] <= 25))
            {
                if ((home_level == ItemClass.Level.Level2) || (home_level == ItemClass.Level.Level3) || (home_level == ItemClass.Level.Level4) || (home_level == ItemClass.Level.Level5))
                {
                    if (rand.Next(100) < 2)
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
            else if (comm_data.citizen_money[homeID] > 0)
            {
                if (home_level == ItemClass.Level.Level1)
                {
                    if (rand.Next(100) < 2)
                    {
                        if (num3 != 0u)
                        {
                            TryMoveFamily_1(homeID, num3, ref instance.m_citizens.m_buffer[(int)((UIntPtr)num3)], num4);
                        }
                    }
                }
                else if ((home_level == ItemClass.Level.Level4) || (home_level == ItemClass.Level.Level5))
                {
                    if (rand.Next(100) < 2)
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
            else if (comm_data.citizen_money[homeID] <= 0)
            {

            }
            else
            {
                //just keep;
            }

            if (comm_data.citizen_money[homeID] < 0)
            {
                family_weight_stable_low = (ushort)(family_weight_stable_low + 1);
            }
            //set other non-exist citizen status to 0
            uint i;
            if (precitizenid < homeID)
            {
                for (i = (precitizenid + 1); i < homeID; i++)
                {
                    if ((comm_data.citizen_money[i] != 0) || (comm_data.citizen_profit_status[i] != 128))
                    {
                        comm_data.citizen_money[i] = 0;
                        comm_data.citizen_profit_status[i] = 128;
                    }
                }
            }
            /*if (comm_data.citizen_count == 0)
            {
                comm_data.citizen_money[homeID] = 0;
                comm_data.citizen_profit_status[homeID] = 128;
            }*/
            precitizenid = homeID;

            //comm_data.citizen_shopping_idex = (byte)temp_num;
            return (byte)temp_num;
            //return to original game code.
        }


        public void Help_low_income_family(int citizen_salary_current)
        {
            int expense = 20 - citizen_salary_current;
            if (expense <= 0)
            {
                DebugLog.LogToFileOnly("we can not offer <0 money to citizen");
            } else
            {
                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, expense * comm_data.game_maintain_fee_decrease, this.m_info.m_class);
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
                    switch (data.EducationLevel)
                    {
                        case Citizen.Education.Uneducated:
                            Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single0, offer);
                            break;
                        case Citizen.Education.OneSchool:
                            Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single1, offer);
                            break;
                        case Citizen.Education.TwoSchools:
                            Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single2, offer);
                            break;
                        case Citizen.Education.ThreeSchools:
                            Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single3, offer);
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
                            Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single2B, offer);
                            break;
                        case Citizen.Education.ThreeSchools:
                            Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single3B, offer);
                            break;
                    }
                }
            }
            else
            {
                if (comm_data.citizen_profit_status[homeID] >= 230)
                {
                    Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Family3, offer);
                }
                else if (comm_data.citizen_money[homeID] < 0)
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
            ushort num = FindNotSoCloseBuilding(building.m_position, 2000f, ItemClass.Service.Commercial, ItemClass.SubService.None, Building.Flags.Created | Building.Flags.Active, Building.Flags.Deleted);
            if (num == 0 || (Singleton<SimulationManager>.instance.m_randomizer.Int32(20) < 10))
            {
                int num2 = Singleton<SimulationManager>.instance.m_randomizer.Int32(5u);
                for (int i = 0; i < 5; i++)
                {
                    uint citizen = data.GetCitizen((num2 + i) % 5);
                    if (citizen != 0u)
                    {
                        if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen].m_workBuilding != 0)
                        {
                            ushort num1;
                            num1 = FindNotSoCloseBuilding(expr_18.m_buildings.m_buffer[Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen].m_workBuilding].m_position, 500f, ItemClass.Service.Commercial, ItemClass.SubService.None, Building.Flags.Created | Building.Flags.Active, Building.Flags.Deleted);
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
                int num1 = -300;
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
            for (int i = num2; i <= num4; i++)
            {
                for (int j = num; j <= num3; j++)
                {
                    ushort num6 = building.m_buildingGrid[i * 270 + j];
                    int num7 = 0;
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
                                    if (maxDistance == 2000f)
                                    {
                                        if (building.m_buildings.m_buffer[(int)num6].m_customBuffer1 > 5000)
                                        {
                                            if (((num8 - num5) < (maxDistance * maxDistance)) || ((num8 - num5) > (maxDistance * maxDistance)) || (result == 0))
                                            {
                                                if ((instance2.m_randomizer.Int32(80u) < 4) || (result == 0))
                                                {
                                                    result = num6;
                                                    num5 = num8;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (building.m_buildings.m_buffer[(int)num6].m_customBuffer2 >= 0)
                                        {
                                            if (((num8 - num5) < (maxDistance * maxDistance)) || ((num8 - num5) > (-maxDistance * maxDistance)))
                                            {
                                                if ((instance2.m_randomizer.Int32(80u) < 40) || (result == 0))
                                                {
                                                    result = num6;
                                                    num5 = num8;
                                                }
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
            return result;
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
                this.TryMoveAwayFromHome(data.m_citizen2, ref instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen2)]);
            }
            if (data.m_citizen3 != 0u)
            {
                this.TryMoveAwayFromHome(data.m_citizen3, ref instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen3)]);
            }
            if (data.m_citizen4 != 0u)
            {
                this.TryMoveAwayFromHome(data.m_citizen4, ref instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen4)]);
            }
            int temp_num = process_citizen(homeID, ref data);

            if (data.m_goods - temp_num < 0)
            {
                //DebugLog.LogToFileOnly("very lack of good, try do viture shopping");
                if (Chancetodovitureshopping(homeID, ref data))
                {

                }
                else
                {
                    temp_num = 0; //not buy anything, so do not decrease money
                }
            } else if (data.m_goods < 20000)
            { 
                SimulationManager instance2 = Singleton<SimulationManager>.instance;
                float currentDayTimeHour = instance2.m_currentDayTimeHour;
                if (currentDayTimeHour > 20f || currentDayTimeHour < 4f)
                {
                    if (instance2.m_randomizer.Int32(data.m_goods) < 2000)
                    {
                        Chancetodovitureshopping(homeID, ref data);
                    }
                }
                else
                {
                    if (instance2.m_randomizer.Int32(data.m_goods) < 20)
                    {
                        Chancetodovitureshopping(homeID, ref data);
                    }
                }
            }


            //sick
            if (data.m_goods < 10000 || ((comm_data.citizen_money[homeID]) < 0 &&(comm_data.citizen_profit_status[homeID] < 10)))
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
                            if (instance2.m_randomizer.Int32(data.m_goods) < 200)
                            {
                                expr_2FA_cp_0[citizen].Sick = true;
                            }
                            break;
                        }
                    }
                }
            }

            data.m_goods = (ushort)Mathf.Max(0, (int)(data.m_goods - temp_num)); //here we can adjust demand
            comm_data.citizen_money[homeID] = (float)(comm_data.citizen_money[homeID] - temp_num);

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
                if (comm_data.citizen_money[homeid] > 0)
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
                if (comm_data.citizen_money[homeid] > 0 && (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_flags.IsFlagSet(Citizen.Flags.Education2)))
                {
                    temp = temp + 10;
                    Singleton<EconomyManager>.instance.AddPrivateIncome(10, ItemClass.Service.Education, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                }
            }

            if (UnlockManager.instance.Unlocked(ItemClass.Service.HealthCare))
            {
                if ((Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_flags & Citizen.Flags.Sick) != Citizen.Flags.None)
                {
                    if ((comm_data.citizen_money[homeid] > 0) && pc_OutsideConnectionAI.have_hospital_building && !comm_data.hospitalhelp)
                    {
                        temp = temp + 10;
                        // 10% is provide by citizen  90% is provide by goverment
                        Singleton<EconomyManager>.instance.AddPrivateIncome(20, ItemClass.Service.HealthCare, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, 380, ItemClass.Service.HealthCare, ItemClass.SubService.None, ItemClass.Level.Level1);
                        comm_data.city_insurance_account -= 380;
                    }
                    else if ((comm_data.citizen_money[homeid] < 0) && pc_OutsideConnectionAI.have_hospital_building && !comm_data.hospitalhelp)
                    {
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, 400, ItemClass.Service.HealthCare, ItemClass.SubService.None, ItemClass.Level.Level1);
                        comm_data.city_insurance_account -= 400;
                    }
                    else
                    {
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, 400, ItemClass.Service.HealthCare, ItemClass.SubService.None, ItemClass.Level.Level1);
                        comm_data.city_insurance_account -= 400;
                    }
                }
            }
            return temp;
        }

        private void Die(uint citizenID, ref Citizen data)
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

            canusetransport(instanceID, ref data, targetBuilding);

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
            if (!this.StartPathFind(instanceID, ref data))
            {
                data.Unspawn(instanceID);
            }
        }

        public void canusetransport (ushort instanceID, ref CitizenInstance citizenData, ushort targetBuilding)
        {
            uint citizen = citizenData.m_citizen;
            int homeBuilding1 = Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_homeBuilding;
            uint containingUnit = Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].GetContainingUnit(citizen, Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)homeBuilding1].m_citizenUnits, CitizenUnit.Flags.Home);
            if (comm_data.citizen_money[containingUnit] < 1000)
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
                //citizenData.m_flags = (citizenData.m_flags | CitizenInstance.Flags.CannotUseTaxi);
                //citizenData.m_flags = (citizenData.m_flags | CitizenInstance.Flags.CannotUseTransport);
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
        }
    }
}


namespace RealCity
{
    public class pc_ResidentAI_1 : HumanAI
    {
        /*protected override bool StartPathFind(ushort instanceID, ref CitizenInstance citizenData)
        {
            ushort temp_parked_car = 0;

            BuildingManager instance3 = Singleton<BuildingManager>.instance;
            DistrictManager instance4 = Singleton<DistrictManager>.instance;

            byte district = 0;
            if (citizenData.m_sourceBuilding != 0)
            {
                district = instance4.GetDistrict(instance3.m_buildings.m_buffer[citizenData.m_sourceBuilding].m_position);
            }

            byte district1 = 0;
            if (citizenData.m_targetBuilding != 0)
            {
                district1 = instance4.GetDistrict(instance3.m_buildings.m_buffer[citizenData.m_targetBuilding].m_position);
            }
            DistrictPolicies.Services servicePolicies = instance4.m_districts.m_buffer[(int)district].m_servicePolicies;
            DistrictPolicies.Event @event = instance4.m_districts.m_buffer[(int)district].m_eventPolicies & Singleton<EventManager>.instance.GetEventPolicyMask();
            DistrictPolicies.Services servicePolicies1 = instance4.m_districts.m_buffer[(int)district1].m_servicePolicies;
            DistrictPolicies.Event @event1 = instance4.m_districts.m_buffer[(int)district1].m_eventPolicies & Singleton<EventManager>.instance.GetEventPolicyMask();

            CitizenManager instance = Singleton<CitizenManager>.instance;
            VehicleManager instance2 = Singleton<VehicleManager>.instance;
            uint citizen1 = citizenData.m_citizen;
            ushort homeBuilding = instance.m_citizens.m_buffer[(int)((UIntPtr)citizen1)].m_homeBuilding;
            uint homeid = instance.m_citizens.m_buffer[citizen1].GetContainingUnit(citizen1, instance3.m_buildings.m_buffer[(int)homeBuilding].m_citizenUnits, CitizenUnit.Flags.Home);
            ushort vehicle = instance.m_citizens.m_buffer[(int)((UIntPtr)citizenData.m_citizen)].m_vehicle;
            Randomizer randomizer = new Randomizer(citizen1);
            if (citizenData.m_citizen != 0u)
            {
                if (vehicle != 0 && ((comm_data.citizen_money[homeid] > 0) || (instance2.m_vehicles.m_buffer[vehicle].Info.m_vehicleType != VehicleInfo.VehicleType.Car || true)))
                {
                    VehicleInfo info = instance2.m_vehicles.m_buffer[(int)vehicle].Info;
                    if (info != null)
                    {
                        uint citizen = info.m_vehicleAI.GetOwnerID(vehicle, ref instance2.m_vehicles.m_buffer[(int)vehicle]).Citizen;
                        if (citizen == citizenData.m_citizen)
                        {
                            info.m_vehicleAI.SetTarget(vehicle, ref instance2.m_vehicles.m_buffer[(int)vehicle], 0);
                            return false;
                        }
                    }
                    instance.m_citizens.m_buffer[(int)((UIntPtr)citizenData.m_citizen)].SetVehicle(citizenData.m_citizen, 0, 0u);
                    return false;
                }
                else if (vehicle != 0 && (comm_data.citizen_money[homeid] <= 0))
                {
                    VehicleInfo info1 = instance2.m_vehicles.m_buffer[(int)vehicle].Info;
                    if (info1 != null)
                    {
                        //DebugLog.LogToFileOnly("citizen too poor, and give up his car");
                        instance.m_citizens.m_buffer[(int)((UIntPtr)citizenData.m_citizen)].SetVehicle(citizenData.m_citizen, 0, 0u);
                        instance2.ReleaseVehicle(vehicle);
                    }
                    else
                    {
                        instance.m_citizens.m_buffer[(int)((UIntPtr)citizenData.m_citizen)].SetVehicle(citizenData.m_citizen, 0, 0u);
                        return false;
                    }
                }
            }
            if (citizenData.m_targetBuilding != 0)
            {
                VehicleInfo vehicleInfo;
                vehicleInfo = CustomGetVehicleInfo(instanceID, ref citizenData, false);

                if ((comm_data.citizen_money[homeid] >= 300) || ((citizenData.m_flags & CitizenInstance.Flags.BorrowCar) != CitizenInstance.Flags.None))
                {
                    if (instance.m_citizens.m_buffer[citizen1].m_parkedVehicle != 0)
                    {
                        if (vehicleInfo == Singleton<VehicleManager>.instance.m_parkedVehicles.m_buffer[(int)instance.m_citizens.m_buffer[citizen1].m_parkedVehicle].Info)
                        {
                            //instance.m_citizens.m_buffer[citizen1].SetParkedVehicle(citizenData.m_citizen, 0);
                            //DebugLog.LogToFileOnly("find his packed car");
                        }
                    }
                    //DebugLog.LogToFileOnly("GetVehicleInfo" + Environment.StackTrace);
                }
                else
                {
                    if (vehicleInfo == Singleton<VehicleManager>.instance.m_parkedVehicles.m_buffer[(int)instance.m_citizens.m_buffer[citizen1].m_parkedVehicle].Info)
                    {
                        if (instance.m_citizens.m_buffer[citizen1].CurrentLocation == Citizen.Location.Home)
                        {
                            temp_parked_car = instance.m_citizens.m_buffer[citizen1].m_parkedVehicle;
                            instance.m_citizens.m_buffer[citizen1].m_parkedVehicle = 0;
                            citizenData.m_flags = (citizenData.m_flags | CitizenInstance.Flags.CannotUseTaxi);
                            citizenData.m_flags = (citizenData.m_flags | CitizenInstance.Flags.CannotUseTransport);
                            if ((citizenData.m_sourceBuilding != 0) && (citizenData.m_targetBuilding != 0))
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
                            //DebugLog.LogToFileOnly("citizen too poor and do not want to use car and stay car near his home");
                            if (randomizer.Int32(100u) < 50)
                            {
                                vehicleInfo = Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref randomizer, ItemClass.Service.Residential, ItemClass.SubService.ResidentialHigh, (citizenData.Info.m_agePhase != Citizen.AgePhase.Child) ? ItemClass.Level.Level2 : ItemClass.Level.Level1);
                            }
                            else
                            {
                                vehicleInfo = null;
                            }
                        } else
                        {
                            //not at home, can use his parked vehicle back, but no money to use taxi and transport
                            citizenData.m_flags = (citizenData.m_flags | CitizenInstance.Flags.CannotUseTaxi);
                            citizenData.m_flags = (citizenData.m_flags | CitizenInstance.Flags.CannotUseTransport);
                            if ((citizenData.m_sourceBuilding != 0) && (citizenData.m_targetBuilding != 0))
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
                    }
                    else
                    {
                        if (randomizer.Int32(100u) < 50)
                        {
                            vehicleInfo = Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref randomizer, ItemClass.Service.Residential, ItemClass.SubService.ResidentialHigh, (citizenData.Info.m_agePhase != Citizen.AgePhase.Child) ? ItemClass.Level.Level2 : ItemClass.Level.Level1);
                        }
                        else
                        {
                            vehicleInfo = null;
                        }
                        //no money to use taxi and transport
                        citizenData.m_flags = (citizenData.m_flags | CitizenInstance.Flags.CannotUseTaxi);
                        citizenData.m_flags = (citizenData.m_flags | CitizenInstance.Flags.CannotUseTransport);
                        if ((citizenData.m_sourceBuilding != 0) && (citizenData.m_targetBuilding != 0))
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
                }
                BuildingInfo info2 = instance3.m_buildings.m_buffer[(int)citizenData.m_targetBuilding].Info;
                randomizer = new Randomizer((int)instanceID << 8 | (int)citizenData.m_targetSeed);
                Vector3 vector;
                Vector3 endPos;
                Vector2 vector2;
                CitizenInstance.Flags flags;
                info2.m_buildingAI.CalculateUnspawnPosition(citizenData.m_targetBuilding, ref instance3.m_buildings.m_buffer[(int)citizenData.m_targetBuilding], ref randomizer, this.m_info, instanceID, out vector, out endPos, out vector2, out flags);
                
                bool return_value_tmp = base.StartPathFind(instanceID, ref citizenData, citizenData.m_targetPos, endPos, vehicleInfo);
                if (temp_parked_car != 0)
                {
                    instance.m_citizens.m_buffer[citizen1].m_parkedVehicle = temp_parked_car;
                    //after caculate path, return parked_car to citizen
                }
                return return_value_tmp;
            }
            return false;
        }

        public VehicleInfo CustomGetVehicleInfo(ushort instanceID, ref CitizenInstance citizenData, bool forceCar)
        {
            if (citizenData.m_citizen == 0u)
            {
                return null;
            }
            bool flag = false;
            if (ExtCitizenInstanceManager.Instance.ExtInstances[(int)instanceID].pathMode == ExtCitizenInstance.ExtPathMode.TaxiToTarget)
            {
                flag = true;
            }
            Citizen.AgeGroup ageGroup = GetAgeGroup(citizenData.Info.m_agePhase);
            int num;
            int num2;
            int num3;
            if (flag)
            {
                num = 0;
                num2 = 0;
                num3 = 100;
            }
            else if (forceCar || (citizenData.m_flags & CitizenInstance.Flags.BorrowCar) != CitizenInstance.Flags.None)
            {
                num = 100;
                num2 = 0;
                num3 = 0;
            }
            else
            {
                num = this.GetCarProbability(instanceID, ref citizenData, ageGroup);
                num2 = this.GetBikeProbability(instanceID, ref citizenData, ageGroup);
                num3 = this.GetTaxiProbability(instanceID, ref citizenData, ageGroup);
            }
            Randomizer randomizer = new Randomizer(citizenData.m_citizen);
            bool flag2 = randomizer.Int32(100u) < num;
            bool flag3 = !flag2 && randomizer.Int32(100u) < num2;
            bool flag4 = !flag2 && !flag3 && randomizer.Int32(100u) < num3;
            bool flag5 = false;
            if (flag2)
            {
                int electricCarProbability = this.GetElectricCarProbability(instanceID, ref citizenData, this.m_info.m_agePhase);
                flag5 = (randomizer.Int32(100u) < electricCarProbability);
            }
            ItemClass.Service service = ItemClass.Service.Residential;
            ItemClass.SubService subService = flag5 ? ItemClass.SubService.ResidentialLowEco : ItemClass.SubService.ResidentialLow;
            if (flag4)
            {
                service = ItemClass.Service.PublicTransport;
                subService = ItemClass.SubService.PublicTransportTaxi;
            }
            VehicleInfo vehicleInfo = null;
            if (flag2)
            {
                ushort parkedVehicle = Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)citizenData.m_citizen].m_parkedVehicle;
                if (parkedVehicle != 0)
                {
                    vehicleInfo = Singleton<VehicleManager>.instance.m_parkedVehicles.m_buffer[(int)parkedVehicle].Info;
                }
            }
            if (vehicleInfo == null && (flag2 | flag4))
            {
                vehicleInfo = Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref randomizer, service, subService, ItemClass.Level.Level1);
            }
            if (flag3)
            {
                VehicleInfo randomVehicleInfo = Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref randomizer, ItemClass.Service.Residential, ItemClass.SubService.ResidentialHigh, (ageGroup != Citizen.AgeGroup.Child) ? ItemClass.Level.Level2 : ItemClass.Level.Level1);
                if (randomVehicleInfo != null)
                {
                    return randomVehicleInfo;
                }
            }
            if ((flag2 | flag4) && vehicleInfo != null)
            {
                return vehicleInfo;
            }
            return null;
        }

        internal static Citizen.AgeGroup GetAgeGroup(Citizen.AgePhase agePhase)
        {
            switch (agePhase)
            {
                case Citizen.AgePhase.Child:
                    return Citizen.AgeGroup.Child;
                case Citizen.AgePhase.Teen0:
                case Citizen.AgePhase.Teen1:
                    return Citizen.AgeGroup.Teen;
                case Citizen.AgePhase.Young0:
                case Citizen.AgePhase.Young1:
                case Citizen.AgePhase.Young2:
                    return Citizen.AgeGroup.Young;
                case Citizen.AgePhase.Adult0:
                case Citizen.AgePhase.Adult1:
                case Citizen.AgePhase.Adult2:
                case Citizen.AgePhase.Adult3:
                    return Citizen.AgeGroup.Adult;
                case Citizen.AgePhase.Senior0:
                case Citizen.AgePhase.Senior1:
                case Citizen.AgePhase.Senior2:
                case Citizen.AgePhase.Senior3:
                    return Citizen.AgeGroup.Senior;
                default:
                    return Citizen.AgeGroup.Adult;
            }
        }

        private int GetTaxiProbability(ushort instanceID, ref CitizenInstance citizenData, Citizen.AgeGroup ageGroup)
        {
            return 20;
        }

        private int GetBikeProbability(ushort instanceID, ref CitizenInstance citizenData, Citizen.AgeGroup ageGroup)
        {
            return 20;
        }

        private int GetCarProbability(ushort instanceID, ref CitizenInstance citizenData, Citizen.AgeGroup ageGroup)
        {
            return 20;
        }

        private int GetElectricCarProbability(ushort instanceID, ref CitizenInstance citizenData, Citizen.AgePhase agePhase)
        {
            return 20;
        }*/

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
                    ushort num = pc_ResidentAI.FindNotSoCloseBuilding(frameData.m_position, 80f, ItemClass.Service.Commercial, ItemClass.SubService.None, Building.Flags.Created | Building.Flags.Active, Building.Flags.Deleted);
                    if (homeBuilding != 0 && num != 0)
                    {
                        BuildingInfo info = instance2.m_buildings.m_buffer[(int)num].Info;
                        int num2 = -100;
                        TransferManager.TransferReason temp_reason = pc_HumanAI.get_shopping_reason(num);
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
    }
}
