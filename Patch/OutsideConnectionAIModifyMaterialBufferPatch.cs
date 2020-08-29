using HarmonyLib;
using RealCity.CustomAI;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
	[HarmonyPatch]
	[HarmonyBefore("pcfantasy.moreoutsideinteraction")]
	public static class OutsideConnectionAIModifyMaterialBufferPatch
	{
		public static MethodBase TargetMethod() {
			return typeof(OutsideConnectionAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
		}
		public static void Prefix(TransferManager.TransferReason material, ref int amountDelta) {
			switch (material) {
				case TransferManager.TransferReason.Oil:
				case TransferManager.TransferReason.Ore:
				case TransferManager.TransferReason.Coal:
				case TransferManager.TransferReason.Petrol:
				case TransferManager.TransferReason.Food:
				case TransferManager.TransferReason.Grain:
				case TransferManager.TransferReason.Lumber:
				case TransferManager.TransferReason.Fish:
				case TransferManager.TransferReason.Logs:
				case TransferManager.TransferReason.Goods:
				case TransferManager.TransferReason.LuxuryProducts:
				case TransferManager.TransferReason.AnimalProducts:
				case TransferManager.TransferReason.Flours:
				case TransferManager.TransferReason.Petroleum:
				case TransferManager.TransferReason.Plastics:
				case TransferManager.TransferReason.Metals:
				case TransferManager.TransferReason.Glass:
				case TransferManager.TransferReason.PlanedTimber:
				case TransferManager.TransferReason.Paper:
					if (amountDelta < 0) {
						int transferSize = -amountDelta;
						MainDataStore.outsideGovermentMoney += (transferSize * RealCityIndustryBuildingAI.GetResourcePrice(material) * MainDataStore.outsideGovermentProfitRatio);
						MainDataStore.outsideTouristMoney += (transferSize * RealCityIndustryBuildingAI.GetResourcePrice(material) * MainDataStore.outsideCompanyProfitRatio * MainDataStore.outsideTouristSalaryProfitRatio);
						//DebugLog.LogToFileOnly($"OutsideConnectionAIModifyMaterialBufferPatch: Find {material} amount = {amountDelta}");
					}
					break;
				default:
					break;
			}
		}
	}
}
