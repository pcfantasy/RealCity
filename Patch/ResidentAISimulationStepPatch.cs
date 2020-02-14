using ColossalFramework;
using Harmony;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public static class ResidentAISimulationStepPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(ResidentAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(Citizen).MakeByRefType() }, null);
        }
        public static void Postfix(uint citizenID, ref Citizen data)
        {
            //change wealth
            BuildingManager instance = Singleton<BuildingManager>.instance;
            ushort homeBuilding = data.m_homeBuilding;
            uint homeId = data.GetContainingUnit(citizenID, instance.m_buildings.m_buffer[homeBuilding].m_citizenUnits, CitizenUnit.Flags.Home);
            if (MainDataStore.family_money[homeId] > 20000)
            {
                data.WealthLevel = Citizen.Wealth.High;
            }
            else if (MainDataStore.family_money[homeId] < 5000)
            {
                data.WealthLevel = Citizen.Wealth.Low;
            }
            else
            {
                data.WealthLevel = Citizen.Wealth.Medium;
            }
        }
    }
}
