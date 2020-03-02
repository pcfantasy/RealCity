using Harmony;
using System;
using System.Reflection;
using ColossalFramework;
using RealCity.Util;

namespace RealCity.Patch
{
	[HarmonyPatch]
	public class TransferManagerAddOutgoingOfferPatch
	{
		public static MethodBase TargetMethod()
		{
			return typeof(TransferManager).GetMethod("AddOutgoingOffer", BindingFlags.Public | BindingFlags.Instance);
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

			var instance = Singleton<BuildingManager>.instance;
			if (offer.Building != 0)
			{
				if (instance.m_buildings.m_buffer[offer.Building].Info.m_class.m_service == ItemClass.Service.Commercial)
				{
					var amount = instance.m_buildings.m_buffer[offer.Building].m_customBuffer2 / MainDataStore.maxGoodPurchase;
					if (amount == 0)
					{
						//no resource
						return false;
					}
					else
					{
						//DebugLog.LogToFileOnly($"Change offer amount from {offer.Amount} to {amount}");
						offer.Amount = amount;
					}
				}
			}
			return true;
		}
	}
}