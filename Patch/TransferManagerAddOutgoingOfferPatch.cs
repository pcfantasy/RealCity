using Harmony;
using System;
using System.Reflection;
using ColossalFramework;
using RealCity.Util;
using RealCity.CustomAI;
using ColossalFramework.Math;

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
		public static bool Prefix(ref TransferManager.TransferReason material, ref TransferManager.TransferOffer offer)
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
            var buildingID = offer.Building;
            if (buildingID != 0)
            {
                var buildingData = instance.m_buildings.m_buffer[buildingID];
                if (instance.m_buildings.m_buffer[buildingID].Info.m_class.m_service == ItemClass.Service.Commercial)
                {
                    Citizen.BehaviourData behaviour = default(Citizen.BehaviourData);
                    int alivevisitCount = 0;
                    int totalvisitCount = 0;
                    RealCityCommercialBuildingAI.InitDelegate();
                    RealCityCommercialBuildingAI.GetVisitBehaviour((CommercialBuildingAI)(buildingData.Info.m_buildingAI), buildingID, ref buildingData, ref behaviour, ref alivevisitCount, ref totalvisitCount);
                    var amount = buildingData.m_customBuffer2 / MainDataStore.maxGoodPurchase - totalvisitCount;
                    if (amount <= 0)
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

            if (material == TransferManager.TransferReason.Single0 || material == TransferManager.TransferReason.Single0B)
			{
				material = TransferManager.TransferReason.Family0;
			}
			else if (material == TransferManager.TransferReason.Single1 || material == TransferManager.TransferReason.Single1B)
			{
				material = TransferManager.TransferReason.Family1;
			}
			else if (material == TransferManager.TransferReason.Single2 || material == TransferManager.TransferReason.Single2B)
			{
				material = TransferManager.TransferReason.Family2;
			}
			else if (material == TransferManager.TransferReason.Single3 || material == TransferManager.TransferReason.Single3B)
			{
				material = TransferManager.TransferReason.Family3;
			}

            return true;
		}
	}
}