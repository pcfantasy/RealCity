using ColossalFramework;
using Harmony;
using RealCity.CustomData;
using System;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class ResidentAICitizenInstanceSimulationStepPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(ResidentAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(CitizenInstance.Frame).MakeByRefType(), typeof(bool) }, null);
        }
        public static void Prefix(ref CitizenInstance citizenData)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint citizen = citizenData.m_citizen;
            if (citizen != 0 && (instance.m_citizens.m_buffer[citizen].m_flags & Citizen.Flags.NeedGoods) != 0)
            {
                instance.m_citizens.m_buffer[citizen].m_flags &= ~Citizen.Flags.NeedGoods;
            }
        }

        public static void Postfix(ref CitizenInstance citizenData)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint citizen = citizenData.m_citizen;
            if (citizen != 0)
            {
                ushort homeBuilding = instance.m_citizens.m_buffer[citizen].m_homeBuilding;
                uint citizenUnit = CitizenData.GetCitizenUnit(homeBuilding);
                uint containingUnit = instance.m_citizens.m_buffer[citizen].GetContainingUnit((uint)citizen, citizenUnit, CitizenUnit.Flags.Home);

                if (containingUnit != 0)
                {
                    if (CitizenUnitData.familyGoods[containingUnit] < 2000)
                    {
                        instance.m_citizens.m_buffer[citizen].m_flags |= Citizen.Flags.NeedGoods;
                    }
                }
            }
        }

    }
}
