using ColossalFramework;
using HarmonyLib;
using RealCity.CustomData;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
    //[HarmonyPatch]
    public class ResidentAIGetCarProbabilityPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(ResidentAI).GetMethod("GetCarProbability", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(Citizen.AgeGroup)}, null);
        }
        public static bool Prefix(ref CitizenInstance citizenData, ref int __result)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            var citizenID = citizenData.m_citizen;
            ushort homeBuilding = instance.m_citizens.m_buffer[citizenID].m_homeBuilding;
            uint citizenUnit = CitizenData.GetCitizenUnit(homeBuilding);
            uint containingUnit = instance.m_citizens.m_buffer[citizenID].GetContainingUnit((uint)citizenID, citizenUnit, CitizenUnit.Flags.Home);
            if ((containingUnit == 0) || (citizenID == 0))
            {
                __result = 0;
                return false;
            }
            else
            {
                if (CitizenUnitData.familyMoney[containingUnit] < MainDataStore.highWealth)
                {
                    __result = 0;
                    return false;
                }
            }
            return true;
        }
    }
}
