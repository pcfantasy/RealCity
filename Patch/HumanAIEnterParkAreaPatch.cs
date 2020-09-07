using ColossalFramework;
using HarmonyLib;
using RealCity.CustomData;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
	[HarmonyPatch]
	public class HumanAIEnterParkAreaPatch
	{
		public static MethodBase TargetMethod()
		{
			return typeof(HumanAI).GetMethod("EnterParkArea", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(byte), typeof(ushort) }, null);
		}
		public static bool Prefix()
		{
			//do not allow ticket price
			return false;
		}
	}
}
