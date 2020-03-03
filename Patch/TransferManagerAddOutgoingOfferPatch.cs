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
		public static void Prefix(ref TransferManager.TransferReason material, ref TransferManager.TransferOffer offer)
		{
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
		}
	}
}