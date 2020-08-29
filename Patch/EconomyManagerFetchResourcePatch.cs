using HarmonyLib;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
	[HarmonyPatch]
	public class EconomyManagerFetchResourcePatch
	{
		public static float Road = 0f;
		public static float Electricity = 0f;
		public static float Water = 0f;
		public static float Beautification = 0f;
		public static float Garbage = 0f;
		public static float HealthCare = 0f;
		public static float PoliceDepartment = 0f;
		public static float Education = 0f;
		public static float Monument = 0f;
		public static float FireDepartment = 0f;
		public static float PublicTransport = 0f;
		public static float Policy_cost = 0f;
		public static float Disaster = 0f;
		public static float PlayerIndustry = 0f;
		public static float PlayerEducation = 0f;
		public static float Museums = 0f;
		public static float Fishing = 0f;
		public static float VarsitySports = 0f;
		public static MethodBase TargetMethod() {
			return typeof(EconomyManager).GetMethod("FetchResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass.Service), typeof(ItemClass.SubService), typeof(ItemClass.Level) }, null);
		}
		public static void OnFetchResourceMaintenance(EconomyManager.Resource resource, ref int amount, ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level) {
			switch (service) {
				case ItemClass.Service.Road:
					ProcessUnit(ref amount, ref Road);
					break;
				case ItemClass.Service.Garbage:
					ProcessUnit(ref amount, ref Garbage);
					break;
				case ItemClass.Service.PoliceDepartment:
					ProcessUnit(ref amount, ref PoliceDepartment);
					break;
				case ItemClass.Service.Beautification:
					ProcessUnit(ref amount, ref Beautification);
					break;
				case ItemClass.Service.Water:
					ProcessUnit(ref amount, ref Water);
					break;
				case ItemClass.Service.Education:
					ProcessUnit(ref amount, ref Education);
					break;
				case ItemClass.Service.Electricity:
					ProcessUnit(ref amount, ref Electricity);
					break;
				case ItemClass.Service.FireDepartment:
					ProcessUnit(ref amount, ref FireDepartment);
					break;
				case ItemClass.Service.Monument:
					ProcessUnit(ref amount, ref Monument);
					break;
				case ItemClass.Service.HealthCare:
					ProcessUnit(ref amount, ref HealthCare);
					break;
				case ItemClass.Service.PublicTransport:
					ProcessUnit(ref amount, ref PublicTransport);
					break;
				case ItemClass.Service.Disaster:
					ProcessUnit(ref amount, ref Disaster);
					break;
				case ItemClass.Service.PlayerIndustry:
					ProcessUnit(ref amount, ref PlayerIndustry);
					break;
				case ItemClass.Service.PlayerEducation:
					ProcessUnit(ref amount, ref PlayerEducation);
					break;
				case ItemClass.Service.Museums:
					ProcessUnit(ref amount, ref Museums);
					break;
				case ItemClass.Service.VarsitySports:
					ProcessUnit(ref amount, ref VarsitySports);
					break;
				case ItemClass.Service.Fishing:
					ProcessUnit(ref amount, ref Fishing);
					break;
				default: break;
			}
		}

		public static void OnFetchResourcePolicy(EconomyManager.Resource resource, ref int amount, ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level) {
			ProcessUnit(ref amount, ref Policy_cost);
		}

		public static void ProcessUnit(ref int amount, ref float container) {
			container += amount / MainDataStore.gameExpenseDivide;
			if (container > 1) {
				amount = (int)container;
				container -= (int)container;
			} else {
				amount = 0;
			}
		}

		public static void Prefix(ref EconomyManager.Resource resource, ref int amount, ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level, ref uint __state) {
			if (amount < 0) {
				DebugLog.LogToFileOnly($"Error: EconomyManagerFetchResourcePatch: amount < 0 {service} {subService} {level}");
				amount = 0;
			}

			__state = 0xdeadbeaf;

			if (resource == EconomyManager.Resource.PolicyCost) {
				OnFetchResourcePolicy(resource, ref amount, service, subService, level);
			}
			if (resource == EconomyManager.Resource.Maintenance) {
				//we must return right amount for playerbuilding to work normally.
				if (amount > 0)
					__state = (uint)amount;
				OnFetchResourceMaintenance(resource, ref amount, service, subService, level);
			} else if (resource == (EconomyManager.Resource)16) {
				resource = EconomyManager.Resource.Maintenance;
			} else if (resource == (EconomyManager.Resource)17) {
				resource = EconomyManager.Resource.PolicyCost;
			}
		}

		public static void Postfix(ref int __result, ref uint __state) {
			if (__state != 0xdeadbeaf) {
				__result = (int)__state;
			}
		}
	}
}
