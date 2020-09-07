using HarmonyLib;
using RealCity.CustomData;
using System;
using System.Reflection;

namespace RealCity.Patch
{
	[HarmonyPatch]
	public class OfficeBuildingAIGetOutgoingTransferReasonPatch
	{
		public static MethodBase TargetMethod()
		{
			return typeof(OfficeBuildingAI).GetMethod("GetOutgoingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
		}
		public static bool Prefix(ref TransferManager.TransferReason __result)
		{
			__result = TransferManager.TransferReason.None;
			return false;
		}
	}
}
