using Harmony;
using RealCity.CustomData;
using RealCity.Util;
using System;
using System.Reflection;
using UnityEngine;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class ZoneManagerCalculateCommercialDemandPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(ZoneManager).GetMethod("CalculateCommercialDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
        }
        public static bool Prefix(ref District districtData, ref int __result)
        {
            float goodDemand = 20f;
            if (MainDataStore.familyCount != 0)
            {
                goodDemand = MainDataStore.totalFamilyGoodDemand / MainDataStore.familyCount;
                if (RealCity.reduceVehicle)
                    goodDemand *= MainDataStore.reduceCargoDiv;
                //DebugLog.LogToFileOnly($"average good demand is {goodDemand}");
            }

            int num = (int)(districtData.m_commercialData.m_finalHomeOrWorkCount - districtData.m_commercialData.m_finalEmptyCount);
            int num2 = (int)(districtData.m_residentialData.m_finalHomeOrWorkCount - districtData.m_residentialData.m_finalEmptyCount);
            int finalHomeOrWorkCount = (int)districtData.m_visitorData.m_finalHomeOrWorkCount;
            int finalEmptyCount = (int)districtData.m_visitorData.m_finalEmptyCount;
            int demand = Mathf.Clamp(num2, 0, 50);
            num = num * 10 * 16 / 100;
            num2 = (int)((float)num2 * goodDemand / 100f);
            demand += Mathf.Clamp((num2 * 200 - num * 200) / Mathf.Max(num, 100), -50, 50);
            demand += Mathf.Clamp((finalHomeOrWorkCount * 100 - finalEmptyCount * 300) / Mathf.Max(finalHomeOrWorkCount, 100), -50, 50);
            __result = Mathf.Clamp(demand, 0, 100);
            return false;
        }
    }
}
