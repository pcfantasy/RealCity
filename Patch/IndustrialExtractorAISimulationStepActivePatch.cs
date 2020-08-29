using ColossalFramework;
using ColossalFramework.Math;
using RealCity.Util;
using RealCity.UI;
using HarmonyLib;
using System.Reflection;
using RealCity.CustomData;
using RealCity.CustomAI;
using UnityEngine;

namespace RealCity.Patch
{
	[HarmonyPatch]
	public class IndustrialExtractorAISimulationStepActivePatch
	{
		public static MethodBase TargetMethod() {
			return typeof(IndustrialExtractorAI).GetMethod("SimulationStepActive", BindingFlags.NonPublic | BindingFlags.Instance);
		}

		public static void Prefix(ref Building buildingData, ref ushort[] __state) {
			__state = new ushort[1];
			__state[0] = buildingData.m_customBuffer1;
		}

		public static void Postfix(ref Building buildingData, ref ushort[] __state) {
			RealCityPrivateBuildingAI.ProcessAdditionProduct(ref buildingData, ref __state);
		}
	}
}
