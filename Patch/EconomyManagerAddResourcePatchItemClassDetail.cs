using HarmonyLib;
using RealCity.CustomManager;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class EconomyManagerAddResourcePatchItemClassDetail
    {
        public static MethodBase TargetMethod()
        {
            return typeof(EconomyManager).GetMethod("AddResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass.Service), typeof(ItemClass.SubService), typeof(ItemClass.Level) }, null);
        }

        public static void Prefix(EconomyManager.Resource resource, ref int amount, ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level)
        {
            // NON-STOCK CODE START
            if (resource == EconomyManager.Resource.PublicIncome)
            {
                if (service == ItemClass.Service.Vehicles)
                {
                    RealCityEconomyManager.roadIncomeForUI[MainDataStore.updateMoneyCount] += amount;

                    if (subService == ItemClass.SubService.None)
                    {
                        if (level == ItemClass.Level.Level2)
                            MainDataStore.outsideGovermentMoney -= amount;
                        else
                            MainDataStore.outsideTouristMoney -= amount;
                    }
                }
            }
            else if (resource == EconomyManager.Resource.ResourcePrice)
            {
                RealCityEconomyManager.playerIndustryIncomeForUI[MainDataStore.updateMoneyCount] += amount;
            }
            /// NON-STOCK CODE END ///
        }
    }
}
