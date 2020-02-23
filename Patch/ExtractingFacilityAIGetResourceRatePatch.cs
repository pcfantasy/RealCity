using ColossalFramework;
using Harmony;
using RealCity.CustomAI;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class ExtractingFacilityAIGetResourceRatePatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(ExtractingFacilityAI).GetMethod("GetResourceRate", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(EconomyManager.Resource) }, null);
        }
        public static void Postfix(ushort buildingID, ref Building data, EconomyManager.Resource resource, ref int __result)
        {
            if (resource == EconomyManager.Resource.Maintenance)
            {
                int budget = Singleton<EconomyManager>.instance.GetBudget(data.Info.m_class);
                float salary = RealCityPlayerBuildingAI.CaculateEmployeeOutcome(buildingID, data);
                __result = (int)((float)__result / MainDataStore.gameExpenseDivide + salary * budget);
            }
        }
    }
}
