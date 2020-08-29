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
	public class IndustrialBuildingAISimulationStepActivePatch
	{
		public static MethodBase TargetMethod() {
			return typeof(IndustrialBuildingAI).GetMethod("SimulationStepActive", BindingFlags.NonPublic | BindingFlags.Instance);
		}

		public static void Prefix(ref Building buildingData, ref ushort[] __state) {
			__state = new ushort[2];
			__state[0] = buildingData.m_customBuffer1;
			__state[1] = buildingData.m_customBuffer2;
		}

		public static void Postfix(ushort buildingID, ref Building buildingData, ref ushort[] __state) {
			RealCityPrivateBuildingAI.ProcessAdditionProduct(buildingID, ref buildingData, ref __state);
		}

	}
}
