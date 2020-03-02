using Harmony;
using System;
using System.Reflection;
using ColossalFramework;
using RealCity.Util;
using RealCity.CustomData;
using RealCity.CustomAI;

namespace RealCity.Patch
{
	[HarmonyPatch]
	public class TransferManagerAddIncomingOfferPatch
	{
		public static MethodBase TargetMethod()
		{
			return typeof(TransferManager).GetMethod("AddIncomingOffer", BindingFlags.Public | BindingFlags.Instance);
		}

		[HarmonyPriority(Priority.VeryHigh)]
		public static bool Prefix(TransferManager.TransferReason material, ref TransferManager.TransferOffer offer)
		{
			switch (material)
			{
				case TransferManager.TransferReason.Shopping:
				case TransferManager.TransferReason.ShoppingB:
				case TransferManager.TransferReason.ShoppingC:
				case TransferManager.TransferReason.ShoppingD:
				case TransferManager.TransferReason.ShoppingE:
				case TransferManager.TransferReason.ShoppingF:
				case TransferManager.TransferReason.ShoppingG:
				case TransferManager.TransferReason.ShoppingH:
				case TransferManager.TransferReason.Entertainment:
				case TransferManager.TransferReason.EntertainmentB:
				case TransferManager.TransferReason.EntertainmentC:
				case TransferManager.TransferReason.EntertainmentD:
					break;
				default:
					return true;
			}

			var instance = Singleton<CitizenManager>.instance;
			if (offer.Citizen != 0)
			{
				ushort homeBuilding = instance.m_citizens.m_buffer[offer.Citizen].m_homeBuilding;
				uint citizenUnit = CitizenData.GetCitizenUnit(homeBuilding);
				uint containingUnit = instance.m_citizens.m_buffer[offer.Citizen].GetContainingUnit((uint)offer.Citizen, citizenUnit, CitizenUnit.Flags.Home);

				if (CitizenUnitData.familyMoney[containingUnit] < MainDataStore.maxGoodPurchase * RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Shopping))
				{
					//DebugLog.LogToFileOnly($"Reject Citizen money = {CitizenData.citizenMoney[offer.Citizen]}");
					return false;
				}
			}
			return true;
		}
	}
}
