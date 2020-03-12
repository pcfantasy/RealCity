using ColossalFramework;
using ColossalFramework.Math;
using Harmony;
using RealCity.CustomData;
using System;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class CitizenManagerCreateCitizenInstancePatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(CitizenManager).GetMethod("CreateCitizenInstance", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort).MakeByRefType(), typeof(Randomizer).MakeByRefType(), typeof(CitizenInfo), typeof(uint) }, null);
        }
        public static void Prefix(ref CitizenManager __instance, uint citizen)
        {
            var data = __instance.m_citizens.m_buffer[citizen];
            if (data.m_flags.IsFlagSet(Citizen.Flags.Tourist) && data.m_flags.IsFlagSet(Citizen.Flags.MovingIn))
            {
                //Add initial money
                if (data.WealthLevel == Citizen.Wealth.Low)
                {
                    CitizenData.citizenMoney[citizen] = 2048;
                }
                else if (data.WealthLevel == Citizen.Wealth.Medium)
                {
                    CitizenData.citizenMoney[citizen] = 4096;
                }
                else
                {
                    CitizenData.citizenMoney[citizen] = 8192;
                }
            }
        }
    }
}
