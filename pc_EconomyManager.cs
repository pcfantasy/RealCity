using System;
using System.Reflection;
using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.Math;
using UnityEngine;


namespace RealCity
{
    public class pc_EconomyManager
    {
        //because maintance and police cost is too small when use real time.
        public static float Road = 0f;
        public static float Electricity = 0f;
        public static float Water = 0f;
        public static float Beautification = 0f;
        public static float Garbage = 0f;
        public static float HealthCare = 0f;
        public static float PoliceDepartment = 0f;
        public static float Education = 0f;
        public static float Monument = 0f;
        public static float FireDepartment = 0f;
        public static float PublicTransport = 0f;
        public static float Policy_cost = 0f;
        public static float citizen_income = 0f;
        public static float tourist_income = 0f;

        public static float commerical_low_level1_income = 0f;
        public static float commerical_low_level2_income = 0f;
        public static float commerical_low_level3_income = 0f;
        public static float commerical_high_level1_income = 0f;
        public static float commerical_high_level2_income = 0f;
        public static float commerical_high_level3_income = 0f;
        public static float commerical_lei_income = 0f;
        public static float commerical_tou_income = 0f;
        public static float industy_forest_income = 0f;
        public static float industy_farm_income = 0f;
        public static float industy_oil_income = 0f;
        public static float industy_ore_income = 0f;
        public static float industy_gen_level1_income = 0f;
        public static float industy_gen_level2_income = 0f;
        public static float industy_gen_level3_income = 0f;
        public static float office_level1_income = 0f;
        public static float office_level2_income = 0f;
        public static float office_level3_income = 0f;
        public static float resident_low_level1_income = 0f;
        public static float resident_low_level2_income = 0f;
        public static float resident_low_level3_income = 0f;
        public static float resident_low_level4_income = 0f;
        public static float resident_low_level5_income = 0f;
        public static float resident_high_level1_income = 0f;
        public static float resident_high_level2_income = 0f;
        public static float resident_high_level3_income = 0f;
        public static float resident_high_level4_income = 0f;
        public static float resident_high_level5_income = 0f;



        public int FetchResource(EconomyManager.Resource resource, int amount, ItemClass itemClass)
        {
            //DebugLog.LogToFileOnly("go in FetchResource " + Policy_cost.ToString());
            // if(itemClass.m_level == ItemClass.Level.Level1) means mod added employee outcome
            int temp;
            int coefficient;
            if (resource == EconomyManager.Resource.Maintenance)
            {
                if(itemClass.m_level == ItemClass.Level.Level1)
                {
                    coefficient = 16;
                }
                else
                {
                    coefficient = 1;
                }
                switch (itemClass.m_service)
                {
                    case ItemClass.Service.Road:
                        Road += (float)amount / coefficient;
                        if (Road > 1)
                        {
                            temp = (int)Road;
                            Road = Road - (int)Road;
                            return Singleton<EconomyManager>.instance.FetchResource(resource, temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
                        }
                        return amount;
                    case ItemClass.Service.Garbage:
                        Garbage += (float)amount / coefficient;
                        if (Garbage > 1)
                        {
                            temp = (int)Garbage;
                            Garbage = Garbage - (int)Garbage;
                            return Singleton<EconomyManager>.instance.FetchResource(resource, temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
                        }
                        return amount;
                    case ItemClass.Service.PoliceDepartment:
                        PoliceDepartment += (float)amount / coefficient;
                        if (PoliceDepartment > 1)
                        {
                            temp = (int)PoliceDepartment;
                            PoliceDepartment = PoliceDepartment - (int)PoliceDepartment;
                            return Singleton<EconomyManager>.instance.FetchResource(resource, temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
                        }
                        return amount;
                    case ItemClass.Service.Beautification:
                        Beautification += (float)amount / coefficient;
                        if (Beautification > 1)
                        {
                            temp = (int)Beautification;
                            Beautification = Beautification - (int)Beautification;
                            Singleton<EconomyManager>.instance.FetchResource(resource, temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
                            return amount;
                        }
                        return amount;
                    case ItemClass.Service.Water:
                        Water += (float)amount / coefficient;
                        if (Water > 1)
                        {
                            temp = (int)Water;
                            Water = Water - (int)Water;
                            Singleton<EconomyManager>.instance.FetchResource(resource, temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
                            return amount;
                        }
                        return amount;
                    case ItemClass.Service.Education:
                        Education += (float)amount / coefficient;
                        if (Education > 1)
                        {
                            temp = (int)Education;
                            Education = Education - (int)Education;
                            return Singleton<EconomyManager>.instance.FetchResource(resource, temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
                        }
                        return amount;
                    case ItemClass.Service.Electricity:
                        Electricity += (float)amount / coefficient;
                        if (Electricity > 1)
                        {
                            temp = (int)Electricity;
                            Electricity = Electricity - (int)Electricity;
                            Singleton<EconomyManager>.instance.FetchResource(resource, temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
                            return amount;
                        }
                        return amount;
                    case ItemClass.Service.FireDepartment:
                        FireDepartment += (float)amount / coefficient;
                        if (FireDepartment > 1)
                        {
                            temp = (int)FireDepartment;
                            FireDepartment = FireDepartment - (int)FireDepartment;
                            return Singleton<EconomyManager>.instance.FetchResource(resource, temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
                        }
                        return amount;
                    case ItemClass.Service.Monument:
                        Monument += amount / coefficient;
                        if (Monument > 1)
                        {
                            temp = (int)Monument;
                            Monument = Monument - (int)Monument;
                            return Singleton<EconomyManager>.instance.FetchResource(resource, temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
                        }
                        return amount;
                    case ItemClass.Service.HealthCare:
                        HealthCare += (float)amount / coefficient;
                        if (HealthCare > 1)
                        {
                            temp = (int)HealthCare;
                            HealthCare = HealthCare - (int)HealthCare;
                            return Singleton<EconomyManager>.instance.FetchResource(resource, temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
                        }
                        return amount;
                    case ItemClass.Service.PublicTransport:
                        PublicTransport += (float)amount / coefficient;
                        if (PublicTransport > 1)
                        {
                            temp = (int)PublicTransport;
                            PublicTransport = PublicTransport - (int)PublicTransport;
                            return Singleton<EconomyManager>.instance.FetchResource(resource, temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
                        }
                        return amount;
                    default: break;
                }
            }
            if (resource == EconomyManager.Resource.PolicyCost)
            {
                Policy_cost += (float)amount / 1;
                //DebugLog.LogToFileOnly("go in FetchResource " + Policy_cost.ToString() + " " + amount.ToString());
                if (Policy_cost > 1)
                {
                    temp = (int)Policy_cost;
                    Policy_cost = Policy_cost - (int)Policy_cost;
                    return Singleton<EconomyManager>.instance.FetchResource(resource, (int)temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
                }
                return amount;
            }
                return Singleton<EconomyManager>.instance.FetchResource(resource, amount, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
        }



        public int AddResource(EconomyManager.Resource resource, int amount, ItemClass itemClass)
        {
            int temp;
            if((resource == EconomyManager.Resource.CitizenIncome) && (itemClass.m_service == ItemClass.Service.Citizen))
            {
                citizen_income += (float)amount / 16;
                if (citizen_income > 1)
                {
                    temp = (int)citizen_income;
                    citizen_income = citizen_income - (int)citizen_income;
                    Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, (int)temp, ItemClass.Service.None, ItemClass.SubService.None, ItemClass.Level.None);
                    return Singleton<EconomyManager>.instance.AddResource(resource, (int)temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level, DistrictPolicies.Taxation.None);
                }
                return 0;
            }
            else if (resource == EconomyManager.Resource.CitizenIncome)
            {
                return 0;
            }
            else if (resource == EconomyManager.Resource.TourismIncome)
            {
                Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, amount, ItemClass.Service.None, ItemClass.SubService.None, ItemClass.Level.None);
                return Singleton<EconomyManager>.instance.AddResource(resource, amount, itemClass.m_service, itemClass.m_subService, itemClass.m_level, DistrictPolicies.Taxation.None);
            }
            return Singleton<EconomyManager>.instance.AddResource(resource, amount, itemClass.m_service, itemClass.m_subService, itemClass.m_level, DistrictPolicies.Taxation.None);
        }


        public int EXAddPrivateIncome(int amount, ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level, int taxRate)
        {
            switch (subService)
            {
                case ItemClass.SubService.IndustrialFarming:
                    industy_farm_income += amount * taxRate * _taxMultiplier / 1000000L;
                    if(industy_farm_income > 1)
                    {
                        amount = (int)industy_farm_income;
                        industy_farm_income = industy_farm_income - (int)industy_farm_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                    break;
                case ItemClass.SubService.IndustrialForestry:
                    industy_forest_income += amount * taxRate * _taxMultiplier / 1000000L;
                    if (industy_forest_income > 1)
                    {
                        amount = (int)industy_forest_income;
                        industy_forest_income = industy_forest_income - (int)industy_forest_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                    break;
                case ItemClass.SubService.IndustrialOil:
                    industy_oil_income += amount * taxRate * _taxMultiplier / 1000000L;
                    if (industy_oil_income > 1)
                    {
                        amount = (int)industy_oil_income;
                        industy_oil_income = industy_oil_income - (int)industy_oil_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                    break;
                case ItemClass.SubService.IndustrialOre:
                    industy_ore_income += amount * taxRate * _taxMultiplier / 1000000L;
                    if (industy_ore_income > 1)
                    {
                        amount = (int)industy_ore_income;
                        industy_ore_income = industy_ore_income - (int)industy_ore_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                    break;
                case ItemClass.SubService.IndustrialGeneric:
                    if (level == ItemClass.Level.Level1)
                    {
                        industy_gen_level1_income += amount * taxRate * _taxMultiplier / 1000000L;
                        if (industy_gen_level1_income > 1)
                        {
                            amount = (int)industy_gen_level1_income;
                            industy_gen_level1_income = industy_gen_level1_income - (int)industy_gen_level1_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level2)
                    {
                        industy_gen_level2_income += amount * taxRate * _taxMultiplier / 1000000L;
                        if (industy_gen_level2_income > 1)
                        {
                            amount = (int)industy_gen_level2_income;
                            industy_gen_level2_income = industy_gen_level2_income - (int)industy_gen_level2_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level3)
                    {
                        industy_gen_level3_income += amount * taxRate * _taxMultiplier / 1000000L;
                        if (industy_gen_level3_income > 1)
                        {
                            amount = (int)industy_gen_level3_income;
                            industy_gen_level3_income = industy_gen_level3_income - (int)industy_gen_level3_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    break;
                case ItemClass.SubService.CommercialHigh:
                    if (level == ItemClass.Level.Level1)
                    {
                        commerical_high_level1_income += amount * taxRate * _taxMultiplier / 1000000L;
                        if (commerical_high_level1_income > 1)
                        {
                            amount = (int)commerical_high_level1_income;
                            commerical_high_level1_income = commerical_high_level1_income - (int)commerical_high_level1_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level2)
                    {
                        commerical_high_level2_income += amount * taxRate * _taxMultiplier / 1000000L;
                        if (commerical_high_level2_income > 1)
                        {
                            amount = (int)commerical_high_level2_income;
                            commerical_high_level2_income = commerical_high_level2_income - (int)commerical_high_level2_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level3)
                    {
                        commerical_high_level3_income += amount * taxRate * _taxMultiplier / 1000000L;
                        if (commerical_high_level3_income > 1)
                        {
                            amount = (int)commerical_high_level3_income;
                            commerical_high_level3_income = commerical_high_level3_income - (int)commerical_high_level3_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    break;
                case ItemClass.SubService.CommercialLow:
                    if (level == ItemClass.Level.Level1)
                    {
                        commerical_low_level1_income += amount * taxRate * _taxMultiplier / 1000000L;
                        if (commerical_low_level1_income > 1)
                        {
                            amount = (int)commerical_low_level1_income;
                            commerical_low_level1_income = commerical_low_level1_income - (int)commerical_low_level1_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level2)
                    {
                        commerical_low_level2_income += amount * taxRate * _taxMultiplier / 1000000L;
                        if (commerical_low_level2_income > 1)
                        {
                            amount = (int)commerical_low_level2_income;
                            commerical_low_level2_income = commerical_low_level2_income - (int)commerical_low_level2_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level3)
                    {
                        commerical_low_level3_income += amount * taxRate * _taxMultiplier / 1000000L;
                        if (commerical_low_level3_income > 1)
                        {
                            amount = (int)commerical_low_level3_income;
                            commerical_low_level3_income = commerical_low_level3_income - (int)commerical_low_level3_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    break;
                case ItemClass.SubService.CommercialLeisure:
                    commerical_lei_income += amount * taxRate * _taxMultiplier / 1000000L;
                    if (commerical_lei_income > 1)
                    {
                        amount = (int)commerical_lei_income;
                        commerical_lei_income = commerical_lei_income - (int)commerical_lei_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                    break;
                case ItemClass.SubService.CommercialTourist:
                    commerical_tou_income += amount * taxRate * _taxMultiplier / 1000000L;
                    if (commerical_tou_income > 1)
                    {
                        amount = (int)commerical_tou_income;
                        commerical_tou_income = commerical_tou_income - (int)commerical_tou_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                    break;
                case ItemClass.SubService.ResidentialHigh:
                    if (level == ItemClass.Level.Level1)
                    {
                        resident_high_level1_income += amount * taxRate * _taxMultiplier / 1000000L;
                        if (resident_high_level1_income > 1)
                        {
                            amount = (int)resident_high_level1_income;
                            resident_high_level1_income = resident_high_level1_income - (int)resident_high_level1_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level2)
                    {
                        resident_high_level2_income += amount * taxRate * _taxMultiplier / 1000000L;
                        if (resident_high_level2_income > 1)
                        {
                            amount = (int)resident_high_level2_income;
                            resident_high_level2_income = resident_high_level2_income - (int)resident_high_level2_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level3)
                    {
                        resident_high_level3_income += amount * taxRate * _taxMultiplier / 1000000L;
                        if (resident_high_level3_income > 1)
                        {
                            amount = (int)resident_high_level3_income;
                            resident_high_level3_income = resident_high_level3_income - (int)resident_high_level3_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level4)
                    {
                        resident_high_level4_income += amount * taxRate * _taxMultiplier / 1000000L;
                        if (resident_high_level4_income > 1)
                        {
                            amount = (int)resident_high_level4_income;
                            resident_high_level4_income = resident_high_level4_income - (int)resident_high_level4_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level5)
                    {
                        resident_high_level5_income += amount * taxRate * _taxMultiplier / 1000000L;
                        if (resident_high_level5_income > 1)
                        {
                            amount = (int)resident_high_level5_income;
                            resident_high_level5_income = resident_high_level5_income - (int)resident_high_level5_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    break;
                case ItemClass.SubService.ResidentialLow:
                    if (level == ItemClass.Level.Level1)
                    {
                        resident_low_level1_income += amount * taxRate * _taxMultiplier / 1000000L;
                        if (resident_low_level1_income > 1)
                        {
                            amount = (int)resident_low_level1_income;
                            resident_low_level1_income = resident_low_level1_income - (int)resident_low_level1_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level2)
                    {
                        resident_low_level2_income += amount * taxRate * _taxMultiplier / 1000000L;
                        if (resident_low_level2_income > 1)
                        {
                            amount = (int)resident_low_level2_income;
                            resident_low_level2_income = resident_low_level2_income - (int)resident_low_level2_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level3)
                    {
                        resident_low_level3_income += amount * taxRate * _taxMultiplier / 1000000L;
                        if (resident_low_level3_income > 1)
                        {
                            amount = (int)resident_low_level3_income;
                            resident_low_level3_income = resident_low_level3_income - (int)resident_low_level3_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level4)
                    {
                        resident_low_level4_income += amount * taxRate * _taxMultiplier / 1000000L;
                        if (resident_low_level4_income > 1)
                        {
                            amount = (int)resident_low_level4_income;
                            resident_low_level4_income = resident_low_level4_income - (int)resident_low_level4_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level5)
                    {
                        resident_low_level5_income += amount * taxRate * _taxMultiplier / 1000000L;
                        if (resident_low_level5_income > 1)
                        {
                            amount = (int)resident_low_level5_income;
                            resident_low_level5_income = resident_low_level5_income - (int)resident_low_level5_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    break;
                default: break;
            }
            if (service == ItemClass.Service.Office)
            {
                if (level == ItemClass.Level.Level1)
                {
                    office_level1_income += amount * taxRate * _taxMultiplier / 1000000L;
                    if (office_level1_income > 1)
                    {
                        amount = (int)office_level1_income;
                        office_level1_income = office_level1_income - (int)office_level1_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                }
                else if (level == ItemClass.Level.Level2)
                {
                    office_level2_income += amount * taxRate * _taxMultiplier / 1000000L;
                    if (office_level2_income > 1)
                    {
                        amount = (int)office_level2_income;
                        office_level2_income = office_level2_income - (int)office_level2_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                }
                else if (level == ItemClass.Level.Level3)
                {
                    office_level3_income += amount * taxRate * _taxMultiplier / 1000000L;
                    if (office_level1_income > 1)
                    {
                        amount = (int)office_level3_income;
                        office_level3_income = office_level3_income - (int)office_level3_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                }
            }
            return amount;
        }

        public int AddPrivateIncome(int amount, ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level, int taxRate)
        {
            if (!_init)
            {
                _init = true;
                Init();
            }
            if (taxRate >= 100)
            {
                taxRate = taxRate / 100;
                Singleton<EconomyManager>.instance.m_EconomyWrapper.OnAddResource(EconomyManager.Resource.PrivateIncome, ref amount, service, subService, level);
                amount = EXAddPrivateIncome(amount, service, subService, level, taxRate);
                int num = ClassIndex(service, subService, level);
                if (num != -1)
                {
                    _income[num * 17 + 16] += (long)amount;
                }
                _cashAmount += (long)amount;
                _cashDelta += (long)amount;
            }
            else
            {
                Singleton<EconomyManager>.instance.m_EconomyWrapper.OnAddResource(EconomyManager.Resource.PrivateIncome, ref amount, service, subService, level);
                amount = (int)(((long)amount * (long)taxRate * (long)_taxMultiplier + 999999L) / 1000000L);
                //int num = ClassIndex(service, subService, level);
                //if (num != -1)
                //{
                //    _income[num * 17 + 16] += (long)amount;
                //}
                //_cashAmount += (long)amount;
                //_cashDelta += (long)amount;
            }
            return amount;
        }

        private static int ClassIndex(ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level)
        {
            int privateServiceIndex = ItemClass.GetPrivateServiceIndex(service);
            if (privateServiceIndex != -1)
            {
                return PrivateClassIndex(service, subService, level);
            }
            return PublicClassIndex(service, subService) + 120;
        }

        // EconomyManager
        private static int PrivateClassIndex(ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level)
        {
            int privateServiceIndex = ItemClass.GetPrivateServiceIndex(service);
            int privateSubServiceIndex = ItemClass.GetPrivateSubServiceIndex(subService);
            if (privateServiceIndex == -1)
            {
                return -1;
            }
            int num;
            if (privateSubServiceIndex != -1)
            {
                num = 8 + privateSubServiceIndex;
            }
            else
            {
                num = privateServiceIndex;
            }
            num *= 5;
            if (level != ItemClass.Level.None)
            {
                num = (int)(num + level);
            }
            return num;
        }

        private static int PublicClassIndex(ItemClass.Service service, ItemClass.SubService subService)
        {
            int publicServiceIndex = ItemClass.GetPublicServiceIndex(service);
            int publicSubServiceIndex = ItemClass.GetPublicSubServiceIndex(subService);
            if (publicServiceIndex == -1)
            {
                return -1;
            }
            int result;
            if (publicSubServiceIndex != -1)
            {
                result = 12 + publicSubServiceIndex;
            }
            else
            {
                result = publicServiceIndex;
            }
            return result;
        }

        public static void Init()
        {
            //DebugLog.Log("Init fake transfer manager");
            try
            {
                var inst = Singleton<EconomyManager>.instance;
                var income = typeof(EconomyManager).GetField("m_income", BindingFlags.NonPublic | BindingFlags.Instance);
                var taxMultiplier = typeof(EconomyManager).GetField("m_taxMultiplier", BindingFlags.NonPublic | BindingFlags.Instance);
                var cashDelta = typeof(EconomyManager).GetField("m_cashDelta", BindingFlags.NonPublic | BindingFlags.Instance);
                var cashAmount = typeof(EconomyManager).GetField("m_cashAmount", BindingFlags.NonPublic | BindingFlags.Instance);
                if (inst == null)
                {
                    DebugLog.LogToFileOnly("No instance of EconomyManager found!");
                    return;
                }
                _income = income.GetValue(inst) as long[];
                _taxMultiplier = (int)taxMultiplier.GetValue(inst);
                _cashDelta = (long)cashDelta.GetValue(inst);
                _cashAmount = (long)cashAmount.GetValue(inst);
                if (_income == null)
                {
                    DebugLog.LogToFileOnly("EconomyManager Arrays are null");
                }
            }
            catch (Exception ex)
            {
                DebugLog.LogToFileOnly("EconomyManager Exception: " + ex.Message);
            }
        }

        private static int _taxMultiplier;
        private static long _cashDelta;
        private static long _cashAmount;
        private static long[] _income;
        private static bool _init;
    }
}
