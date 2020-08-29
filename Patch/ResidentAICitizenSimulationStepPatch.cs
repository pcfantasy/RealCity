using ColossalFramework;
using HarmonyLib;
using RealCity.CustomAI;
using RealCity.CustomData;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
	[HarmonyPatch]
	public class ResidentAICitizenSimulationStepPatch
	{
		public static MethodBase TargetMethod() {
			return typeof(ResidentAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(Citizen).MakeByRefType() }, null);
		}
		public static void Postfix(uint citizenID, ref Citizen data) {
			BuildingManager instance = Singleton<BuildingManager>.instance;
			ushort homeBuilding = data.m_homeBuilding;
			uint homeId = data.GetContainingUnit(citizenID, instance.m_buildings.m_buffer[homeBuilding].m_citizenUnits, CitizenUnit.Flags.Home);
			if (homeId != 0) {
				//Change wealth
				if (CitizenUnitData.familyMoney[homeId] > MainDataStore.highWealth) {
					data.WealthLevel = Citizen.Wealth.High;
				} else if (CitizenUnitData.familyMoney[homeId] < MainDataStore.lowWealth) {
					data.WealthLevel = Citizen.Wealth.Low;
				} else {
					data.WealthLevel = Citizen.Wealth.Medium;
				}
			}
		}
	}
}
