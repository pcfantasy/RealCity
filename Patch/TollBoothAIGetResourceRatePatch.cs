using ColossalFramework;
using HarmonyLib;
using RealCity.CustomAI;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
	[HarmonyPatch]
	public class TollBoothAIGetResourceRatePatch
	{
		public static MethodBase TargetMethod() {
			return typeof(TollBoothAI).GetMethod("GetResourceRate", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(EconomyManager.Resource) }, null);
		}
		public static void Postfix(ushort buildingID, ref Building data, EconomyManager.Resource resource, ref int __result) {
			if (resource == EconomyManager.Resource.Maintenance) {
				float salary = RealCityPlayerBuildingAI.CaculateEmployeeOutcome(buildingID, data);
				__result = (int)((float)__result / MainDataStore.gameExpenseDivide - salary * 100f);
			}
		}
	}
}
