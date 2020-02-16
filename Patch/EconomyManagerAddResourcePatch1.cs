using HarmonyLib;
using RealCity.CustomManager;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class EconomyManagerAddResourcePatch1
    {
        public static MethodBase TargetMethod()
        {
            return typeof(EconomyManager).GetMethod("AddResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass.Service), typeof(ItemClass.SubService), typeof(ItemClass.Level) }, null);
        }

        public static void Prefix(EconomyManager.Resource resource, ref int amount, ItemClass.Service service)
        {
            // NON-STOCK CODE START
            if (resource == EconomyManager.Resource.PublicIncome)
            {
                if (service == ItemClass.Service.Vehicles)
                {
                    RealCityEconomyManager.road_income_forui[MainDataStore.update_money_count] += amount;
                }
            }
            else if (resource == EconomyManager.Resource.ResourcePrice)
            {
                RealCityEconomyManager.playerIndustryIncomeForUI[MainDataStore.update_money_count] += amount;
            }
            /// NON-STOCK CODE END ///
        }
    }
}
