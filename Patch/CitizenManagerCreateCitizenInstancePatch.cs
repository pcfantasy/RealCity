using ColossalFramework;
using ColossalFramework.Math;
using HarmonyLib;
using RealCity.CustomData;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
	[HarmonyPatch]
	public class CitizenManagerCreateCitizenInstancePatch
	{
		public static MethodBase TargetMethod() {
			return typeof(CitizenManager).GetMethod("CreateCitizenInstance", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort).MakeByRefType(), typeof(Randomizer).MakeByRefType(), typeof(CitizenInfo), typeof(uint) }, null);
		}
		public static bool Prefix(ref CitizenManager __instance, uint citizen, ref bool __result) {
			var data = __instance.m_citizens.m_buffer[citizen];

			if (data.m_flags.IsFlagSet(Citizen.Flags.DummyTraffic)) {
				if (RealCity.realCityV10) {
					if (MainDataStore.outsideTouristMoney < 0) {
						__result = false;
						return false;
					}
				}
			} else if (data.m_flags.IsFlagSet(Citizen.Flags.Tourist)) {
				if (data.m_flags.IsFlagSet(Citizen.Flags.MovingIn)) {
					//Add initial money
					if (data.WealthLevel == Citizen.Wealth.Low) {
						CitizenData.Instance.citizenMoney[citizen] = 2048;
					} else if (data.WealthLevel == Citizen.Wealth.Medium) {
						CitizenData.Instance.citizenMoney[citizen] = 4096;
					} else {
						CitizenData.Instance.citizenMoney[citizen] = 8192;
					}
				}
			}
			return true;
		}
	}
}
