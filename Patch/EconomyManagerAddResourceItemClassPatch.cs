using HarmonyLib;
using RealCity.CustomManager;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class EconomyManagerAddResourceItemClassPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(EconomyManager).GetMethod("AddResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass) }, null);
        }

        public static void Prefix(EconomyManager.Resource resource, ref int amount, ItemClass itemClass)
        {
            // NON-STOCK CODE START
            if (resource == EconomyManager.Resource.TourismIncome)
            {
                amount = 0;
            }
            else if (resource == EconomyManager.Resource.ResourcePrice)
            {
                RealCityEconomyManager.playerIndustryIncomeForUI[MainDataStore.updateMoneyCount] += amount;
            }
            else if (resource == EconomyManager.Resource.PublicIncome && itemClass.m_service == ItemClass.Service.Beautification)
            {
                if (amount > 0)
                {
                    RealCityEconomyManager.citizenIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                }
                else
                {
                    //We use negetive amount to identify tourist income
                    amount = -amount;
                    RealCityEconomyManager.touristIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                }
            }
            else if (resource == EconomyManager.Resource.PublicIncome && itemClass.m_service == ItemClass.Service.PlayerEducation)
            {
                RealCityEconomyManager.schoolIncomeForUI[MainDataStore.updateMoneyCount] += amount;
            }
        }
    }
}
