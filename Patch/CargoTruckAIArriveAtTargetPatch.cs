using ColossalFramework;
using Harmony;
using RealCity.CustomAI;
using RealCity.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public static class CargoTruckAIArriveAtTargetPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(CargoTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
        }
        public static void Prefix(ref CargoTruckAI __instance, ushort vehicleID, ref Vehicle data)
        {
            int num = 0;
            if ((data.m_flags & Vehicle.Flags.TransferToTarget) != 0)
            {
                num = data.m_transferSize;
            }
            if ((data.m_flags & Vehicle.Flags.TransferToSource) != 0)
            {
                num = Mathf.Min(0, data.m_transferSize - __instance.m_cargoCapacity);
            }
            // NON-STOCK CODE START
            ProcessResourceArriveAtTarget(ref data, ref num);
        }
        public static void ProcessResourceArriveAtTarget(ref Vehicle data, ref int num)
        {
            BuildingManager instance = Singleton<BuildingManager>.instance;
            BuildingInfo info = instance.m_buildings.m_buffer[data.m_targetBuilding].Info;
            if ((data.m_flags & Vehicle.Flags.TransferToTarget) != 0)
            { 
                if ((info.m_class.m_service == ItemClass.Service.Electricity) || (info.m_class.m_service == ItemClass.Service.Water) || (info.m_class.m_service == ItemClass.Service.Disaster))
                {
                    info.m_buildingAI.ModifyMaterialBuffer(data.m_targetBuilding, ref instance.m_buildings.m_buffer[data.m_targetBuilding], (TransferManager.TransferReason)data.m_transferType, ref num);
                    float product_value = 0f;
                    switch ((TransferManager.TransferReason)data.m_transferType)
                    {
                        case TransferManager.TransferReason.Petrol:
                            product_value = num * RealCityIndustryBuildingAI.GetResourcePrice((TransferManager.TransferReason)data.m_transferType);
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.ResourcePrice, (int)product_value, ItemClass.Service.PlayerIndustry, ItemClass.SubService.PlayerIndustryOil, ItemClass.Level.Level1);
                            break;
                        case TransferManager.TransferReason.Coal:
                            product_value = num * RealCityIndustryBuildingAI.GetResourcePrice((TransferManager.TransferReason)data.m_transferType);
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.ResourcePrice, (int)product_value, ItemClass.Service.PlayerIndustry, ItemClass.SubService.PlayerIndustryOre, ItemClass.Level.Level1);
                            break;
                        case TransferManager.TransferReason.Goods:
                            product_value = num * RealCityIndustryBuildingAI.GetResourcePrice((TransferManager.TransferReason)data.m_transferType);
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.ResourcePrice, (int)product_value, ItemClass.Service.PlayerIndustry, ItemClass.SubService.None, ItemClass.Level.Level1);
                            break;
                        case (TransferManager.TransferReason)124:
                        case (TransferManager.TransferReason)125: break;
                        default: DebugLog.LogToFileOnly("Error: ProcessResourceArriveAtTarget find unknow play building transition" + info.m_class.ToString() + "transfer reason " + data.m_transferType.ToString()); break;
                    }
                    if ((data.m_flags & Vehicle.Flags.TransferToTarget) != 0)
                    {
                        data.m_transferSize = (ushort)Mathf.Clamp(data.m_transferSize - num, 0, data.m_transferSize);
                    }
                }
            }
        }
    }
}
