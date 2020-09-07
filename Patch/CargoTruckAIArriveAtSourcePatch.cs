using ColossalFramework;
using HarmonyLib;
using RealCity.CustomAI;
using RealCity.Util;
using System;
using System.Reflection;
using UnityEngine;

namespace RealCity.Patch
{
	[HarmonyPatch]
	public class CargoTruckAIArriveAtSourcePatch
	{
		public static MethodBase TargetMethod()
		{
			return typeof(CargoTruckAI).GetMethod("ArriveAtSource", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
		}
		public static bool Prefix(ref Vehicle data)
		{
			if (data.m_sourceBuilding == 0)
			{
				return true;
			}

			if ((data.m_flags & Vehicle.Flags.TransferToTarget) != (Vehicle.Flags)0)
			{
				int transferSize = (int)data.m_transferSize;
				BuildingInfo info = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].Info;
				info.m_buildingAI.ModifyMaterialBuffer(data.m_sourceBuilding, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_sourceBuilding], (TransferManager.TransferReason)data.m_transferType, ref transferSize);
			}
			return true;
		}
	}
}
