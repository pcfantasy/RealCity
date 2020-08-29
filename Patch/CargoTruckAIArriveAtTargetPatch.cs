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
	public static class CargoTruckAIArriveAtTargetPatch
	{
		public static MethodBase TargetMethod() {
			return typeof(CargoTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
		}
		public static void Prefix(ref CargoTruckAI __instance, ref Vehicle data) {
			int transferSize = 0;
			if ((data.m_flags & Vehicle.Flags.TransferToTarget) != 0) {
				transferSize = data.m_transferSize;
			}
			if ((data.m_flags & Vehicle.Flags.TransferToSource) != 0) {
				transferSize = Mathf.Min(0, data.m_transferSize - __instance.m_cargoCapacity);
			}
			// NON-STOCK CODE START
			ProcessResourceArriveAtTarget(ref data, ref transferSize);
		}
		public static void ProcessResourceArriveAtTarget(ref Vehicle data, ref int transferSize) {
			BuildingManager instance = Singleton<BuildingManager>.instance;
			BuildingInfo info = instance.m_buildings.m_buffer[data.m_targetBuilding].Info;
			if ((data.m_flags & Vehicle.Flags.TransferToTarget) != 0) {
				if ((info.m_class.m_service == ItemClass.Service.Electricity) || (info.m_class.m_service == ItemClass.Service.Water) || (info.m_class.m_service == ItemClass.Service.Disaster)) {
					info.m_buildingAI.ModifyMaterialBuffer(data.m_targetBuilding, ref instance.m_buildings.m_buffer[data.m_targetBuilding], (TransferManager.TransferReason)data.m_transferType, ref transferSize);
					float productValue;
					switch ((TransferManager.TransferReason)data.m_transferType) {
						case TransferManager.TransferReason.Petrol:
							productValue = transferSize * RealCityIndustryBuildingAI.GetResourcePrice((TransferManager.TransferReason)data.m_transferType);
							Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.ResourcePrice, (int)productValue, ItemClass.Service.PlayerIndustry, ItemClass.SubService.PlayerIndustryOil, ItemClass.Level.Level1);
							break;
						case TransferManager.TransferReason.Coal:
							productValue = transferSize * RealCityIndustryBuildingAI.GetResourcePrice((TransferManager.TransferReason)data.m_transferType);
							Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.ResourcePrice, (int)productValue, ItemClass.Service.PlayerIndustry, ItemClass.SubService.PlayerIndustryOre, ItemClass.Level.Level1);
							break;
						case TransferManager.TransferReason.Goods:
							productValue = transferSize * RealCityIndustryBuildingAI.GetResourcePrice((TransferManager.TransferReason)data.m_transferType);
							Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.ResourcePrice, (int)productValue, ItemClass.Service.PlayerIndustry, ItemClass.SubService.None, ItemClass.Level.Level1);
							break;
						case (TransferManager.TransferReason)124:
						case (TransferManager.TransferReason)125: break;
						default: DebugLog.LogToFileOnly("Error: ProcessResourceArriveAtTarget find unknow play building transition" + info.m_class.ToString() + "transfer reason " + data.m_transferType.ToString()); break;
					}
					if ((data.m_flags & Vehicle.Flags.TransferToTarget) != 0) {
						data.m_transferSize = (ushort)Mathf.Clamp(data.m_transferSize - transferSize, 0, data.m_transferSize);
					}
				}
			}
		}
	}
}