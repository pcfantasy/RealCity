using Harmony;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public static class EconomyManagerFetchResourcePatch
    {
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
        public static float Disaster = 0f;
        public static float PlayerIndustry = 0f;
        public static float PlayerEducation = 0f;
        public static float Museums = 0f;
        public static float VarsitySports = 0f;
        public static MethodBase TargetMethod()
        {
            return typeof(EconomyManager).GetMethod("FetchResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass.Service), typeof(ItemClass.SubService), typeof(ItemClass.Level) }, null);
        }
        public static void OnFetchResourceMaintenance(EconomyManager.Resource resource, ref int amount, ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level)
        {
            int temp = 0;
            switch (service)
            {
                case ItemClass.Service.Road:
                    Road += (float)amount / MainDataStore.gameExpenseDivide;
                    if (Road > 1)
                    {
                        temp = (int)Road;
                        Road = Road - (int)Road;
                    }
                    amount = temp;
                    break;
                case ItemClass.Service.Garbage:
                    Garbage += (float)amount / MainDataStore.gameExpenseDivide;
                    if (Garbage > 1)
                    {
                        temp = (int)Garbage;
                        Garbage = Garbage - (int)Garbage;
                    }
                    amount = temp;
                    break;
                case ItemClass.Service.PoliceDepartment:
                    PoliceDepartment += (float)amount / MainDataStore.gameExpenseDivide;
                    if (PoliceDepartment > 1)
                    {
                        temp = (int)PoliceDepartment;
                        PoliceDepartment = PoliceDepartment - (int)PoliceDepartment;
                    }
                    amount = temp;
                    break;
                case ItemClass.Service.Beautification:
                    Beautification += (float)amount / MainDataStore.gameExpenseDivide;
                    if (Beautification > 1)
                    {
                        temp = (int)Beautification;
                        Beautification = Beautification - (int)Beautification;
                    }
                    amount = temp;
                    break;
                case ItemClass.Service.Water:
                    Water += (float)amount / MainDataStore.gameExpenseDivide;
                    if (Water > 1)
                    {
                        temp = (int)Water;
                        Water = Water - (int)Water;
                    }
                    amount = temp;
                    break;
                case ItemClass.Service.Education:
                    Education += (float)amount / MainDataStore.gameExpenseDivide;
                    if (Education > 1)
                    {
                        temp = (int)Education;
                        Education = Education - (int)Education;
                    }
                    amount = temp;
                    break;
                case ItemClass.Service.Electricity:
                    Electricity += (float)amount / MainDataStore.gameExpenseDivide;
                    if (Electricity > 1)
                    {
                        temp = (int)Electricity;
                        Electricity = Electricity - (int)Electricity;
                    }
                    amount = temp;
                    break;
                case ItemClass.Service.FireDepartment:
                    FireDepartment += (float)amount / MainDataStore.gameExpenseDivide;
                    if (FireDepartment > 1)
                    {
                        temp = (int)FireDepartment;
                        FireDepartment = FireDepartment - (int)FireDepartment;
                    }
                    amount = temp;
                    break;
                case ItemClass.Service.Monument:
                    Monument += amount / MainDataStore.gameExpenseDivide;
                    if (Monument > 1)
                    {
                        temp = (int)Monument;
                        Monument = Monument - (int)Monument;
                    }
                    amount = temp;
                    break;
                case ItemClass.Service.HealthCare:
                    HealthCare += (float)amount / MainDataStore.gameExpenseDivide;
                    if (HealthCare > 1)
                    {
                        temp = (int)HealthCare;
                        HealthCare = HealthCare - (int)HealthCare;
                    }
                    amount = temp;
                    break;
                case ItemClass.Service.PublicTransport:
                    PublicTransport += (float)amount / MainDataStore.gameExpenseDivide;
                    if (PublicTransport > 1)
                    {
                        temp = (int)PublicTransport;
                        PublicTransport = PublicTransport - (int)PublicTransport;
                    }
                    amount = temp;
                    break;
                case ItemClass.Service.Disaster:
                    Disaster += (float)amount / MainDataStore.gameExpenseDivide;
                    if (Disaster > 1)
                    {
                        temp = (int)Disaster;
                        Disaster = Disaster - (int)Disaster;
                    }
                    amount = temp;
                    break;
                case ItemClass.Service.PlayerIndustry:
                    PlayerIndustry += (float)amount / MainDataStore.gameExpenseDivide;
                    if (PlayerIndustry > 1)
                    {
                        temp = (int)PlayerIndustry;
                        PlayerIndustry = PlayerIndustry - (int)PlayerIndustry;
                    }
                    amount = temp;
                    break;
                case ItemClass.Service.PlayerEducation:
                    PlayerEducation += (float)amount / MainDataStore.gameExpenseDivide;
                    if (PlayerEducation > 1)
                    {
                        temp = (int)PlayerEducation;
                        PlayerEducation = PlayerEducation - (int)PlayerEducation;
                    }
                    amount = temp;
                    break;
                case ItemClass.Service.Museums:
                    Museums += (float)amount / MainDataStore.gameExpenseDivide;
                    if (Museums > 1)
                    {
                        temp = (int)Museums;
                        Museums = Museums - (int)Museums;
                    }
                    amount = temp;
                    break;
                case ItemClass.Service.VarsitySports:
                    VarsitySports += (float)amount / MainDataStore.gameExpenseDivide;
                    if (VarsitySports > 1)
                    {
                        temp = (int)VarsitySports;
                        VarsitySports = VarsitySports - (int)VarsitySports;
                    }
                    amount = temp;
                    break;
                default: break;
            }
        }

        public static void OnFetchResourcePolicy(EconomyManager.Resource resource, ref int amount, ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level)
        {
            int temp = 0;
            Policy_cost += (float)amount / MainDataStore.gameExpenseDivide;
            if (Policy_cost > 1)
            {
                temp = (int)Policy_cost;
                Policy_cost = Policy_cost - (int)Policy_cost;
            }
            amount = temp;
        }

        public static void Prefix(ref EconomyManager.Resource resource, ref int amount, ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level)
        {
            if (resource == EconomyManager.Resource.PolicyCost)
            {
                OnFetchResourcePolicy(resource, ref amount, service, subService, level);
            }
            if (resource == EconomyManager.Resource.Maintenance)
            {
                OnFetchResourceMaintenance(resource, ref amount, service, subService, level);
            }
            else if (resource == (EconomyManager.Resource)16)
            {
                resource = EconomyManager.Resource.Maintenance;
            }
            else if (resource == (EconomyManager.Resource)17)
            {
                resource = EconomyManager.Resource.PolicyCost;
            }
        }
    }
}
