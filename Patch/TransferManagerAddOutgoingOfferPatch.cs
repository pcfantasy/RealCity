using Harmony;
using System;
using System.Reflection;
using ColossalFramework;
using RealCity.Util;
using RealCity.CustomAI;
using ColossalFramework.Math;
using ColossalFramework.Plugins;

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
                    Citizen.BehaviourData behaviour = default;
                    int aliveVisitCount = 0;
                    int totalVisitCount = 0;
                    RealCityCommercialBuildingAI.InitDelegate();
                    RealCityCommercialBuildingAI.GetVisitBehaviour((CommercialBuildingAI)(buildingData.Info.m_buildingAI), buildingID, ref buildingData, ref behaviour, ref aliveVisitCount, ref totalVisitCount);
                    var AI = buildingData.Info.m_buildingAI as CommercialBuildingAI;
                    var maxCount = AI.CalculateVisitplaceCount((ItemClass.Level)buildingData.m_level, new Randomizer(buildingID), buildingData.m_width, buildingData.m_length);
                    var amount = Math.Min(buildingData.m_customBuffer2 / MainDataStore.maxGoodPurchase - aliveVisitCount, maxCount - totalVisitCount);

                    if ((amount <= 0) || (maxCount <= totalVisitCount))
                    { 
                        buildingData.m_flags &= ~Building.Flags.Active;
                        //no resource
                        return false;
                    }
                    else
                    {
                        offer.Amount = amount;
                        AddIncomingOfferFixedForRealTime(material, offer);
                        return false;
                    }
                }
            }

            //Remove cotenancy
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

        //RealTime do not let commercial building add shopping offer, may be a bug?
        public static void AddIncomingOfferFixedForRealTime(TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (!_init)
            {
                Init();
                _init = true;
            }

            int num = offer.Priority;
            int num2;
            int num3;
            while (true)
            {
                if (num < 0)
                {
                    return;
                }
                num2 = (int)material * 8 + num;
                num3 = m_incomingCount[num2];
                if (num3 < 256)
                {
                    break;
                }
                num--;
            }
            int num4 = num2 * 256 + num3;
            m_incomingOffers[num4] = offer;
            m_incomingCount[num2] = (ushort)(num3 + 1);
            m_incomingAmount[(int)material] += offer.Amount;
        }

        public static bool _init = false;

        public static void Init()
        {
            var inst = Singleton <TransferManager>.instance;
            var incomingCount = typeof(TransferManager).GetField("m_incomingCount", BindingFlags.NonPublic | BindingFlags.Instance);
            var incomingOffers = typeof(TransferManager).GetField("m_incomingOffers", BindingFlags.NonPublic | BindingFlags.Instance);
            var incomingAmount = typeof(TransferManager).GetField("m_incomingAmount", BindingFlags.NonPublic | BindingFlags.Instance);
            var outgoingCount = typeof(TransferManager).GetField("m_outgoingCount", BindingFlags.NonPublic | BindingFlags.Instance);
            var outgoingOffers = typeof(TransferManager).GetField("m_outgoingOffers", BindingFlags.NonPublic | BindingFlags.Instance);
            var outgoingAmount = typeof(TransferManager).GetField("m_outgoingAmount", BindingFlags.NonPublic | BindingFlags.Instance);
            if (inst == null)
            {
                CODebugBase<LogChannel>.Error(LogChannel.Core, "No instance of TransferManager found!");
                DebugOutputPanel.AddMessage(PluginManager.MessageType.Error, "No instance of TransferManager found!");
                return;
            }
            m_incomingCount = incomingCount.GetValue(inst) as ushort[];
            m_incomingOffers = incomingOffers.GetValue(inst) as TransferManager.TransferOffer[];
            m_incomingAmount = incomingAmount.GetValue(inst) as int[];
            m_outgoingCount = outgoingCount.GetValue(inst) as ushort[];
            m_outgoingOffers = outgoingOffers.GetValue(inst) as TransferManager.TransferOffer[];
            m_outgoingAmount = outgoingAmount.GetValue(inst) as int[];
        }

        public static TransferManager.TransferOffer[] m_outgoingOffers;
        public static TransferManager.TransferOffer[] m_incomingOffers;
        public static ushort[] m_outgoingCount;
        public static ushort[] m_incomingCount;
        public static int[] m_outgoingAmount;
        public static int[] m_incomingAmount;
    }
}