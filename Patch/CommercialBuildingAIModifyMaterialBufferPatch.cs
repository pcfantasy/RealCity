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
	public class CommercialBuildingAIModifyMaterialBufferPatch
	{
		public static MethodBase TargetMethod() {
			return typeof(CommercialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
		}

		public static bool Prefix(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta) {
			if (material == TransferManager.TransferReason.Entertainment) {
				CaculateTradeIncome(buildingID, ref data, material, ref amountDelta);
			} else {
				switch (material) {
					case TransferManager.TransferReason.Shopping:
					case TransferManager.TransferReason.ShoppingB:
					case TransferManager.TransferReason.ShoppingC:
					case TransferManager.TransferReason.ShoppingD:
					case TransferManager.TransferReason.ShoppingE:
					case TransferManager.TransferReason.ShoppingF:
					case TransferManager.TransferReason.ShoppingG:
					case TransferManager.TransferReason.ShoppingH:
						if (amountDelta == -100) {
							// Disable other - 100 ModifyMaterialBuffer
							return false;
						}
						break;
				}
			}
			return true;
		}

		public static void Postfix(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta) {
			switch (material) {
				case TransferManager.TransferReason.ShoppingB:
				case TransferManager.TransferReason.ShoppingC:
				case TransferManager.TransferReason.ShoppingD:
				case TransferManager.TransferReason.ShoppingE:
				case TransferManager.TransferReason.ShoppingF:
				case TransferManager.TransferReason.ShoppingG:
				case TransferManager.TransferReason.ShoppingH:
					break;
				default:
					if (material != TransferManager.TransferReason.Shopping) {
						if (material == TransferManager.TransferReason.Goods || material == TransferManager.TransferReason.Petrol || material == TransferManager.TransferReason.Food || material == TransferManager.TransferReason.LuxuryProducts) {
							ProcessIncoming(buildingID, ref data, material, ref amountDelta);
						}
						return;
					}
					break;
			}
			CaculateTradeIncome(buildingID, ref data, material, ref amountDelta);
		}

		public static void ProcessIncoming(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta) {
			if (amountDelta > 0) {
				float tradeIncome1 = amountDelta * RealCityIndustryBuildingAI.GetResourcePrice(material);
				BuildingData.buildingMoney[buildingID] = BuildingData.buildingMoney[buildingID] - tradeIncome1;
			}
		}

		public static void CaculateTradeIncome(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta) {
			if (amountDelta < 0) {
				float tradeIncome = amountDelta * RealCityIndustryBuildingAI.GetResourcePrice(material);
				float tradeTax = -tradeIncome * RealCityPrivateBuildingAI.GetTaxRate(data) / 100f;
				Singleton<EconomyManager>.instance.AddPrivateIncome((int)tradeTax, ItemClass.Service.Commercial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111333);
				BuildingData.buildingMoney[buildingID] = (BuildingData.buildingMoney[buildingID] - (tradeIncome + tradeTax));
			}
		}
	}
}
