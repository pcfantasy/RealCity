using ColossalFramework;
using HarmonyLib;
using RealCity.CustomAI;
using RealCity.CustomData;
using System;
using System.Reflection;

namespace RealCity.Patch
{
	[HarmonyPatch]
	public class CommercialBuildingAICreateBuildingPatch
	{
		public static MethodBase TargetMethod() {
			return typeof(CommercialBuildingAI).GetMethod("CreateBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
		}
		public static void Postfix(ushort buildingID, ref Building data) {
			var material = TransferManager.TransferReason.Goods;
			float initialMaterialFee = data.m_customBuffer1 * RealCityIndustryBuildingAI.GetResourcePrice(material) + 10000;
			BuildingData.buildingMoney[buildingID] = -initialMaterialFee;
		}
	}
}
