using ColossalFramework;
using Harmony;
using RealCity.CustomAI;
using RealCity.CustomData;
using RealCity.Util;
using System;
using System.Reflection;
using UnityEngine;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class LandfillSiteAIModifyMaterialBufferPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(LandfillSiteAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
        }

        public static void Prefix(ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            if ((material == TransferManager.TransferReason.Lumber) || (material == TransferManager.TransferReason.Coal) || (material == TransferManager.TransferReason.Petrol))
            {
                RevertGabargeIncome(ref data, amountDelta, material);
            }
        }

        public static void RevertGabargeIncome(ref Building building , int num , TransferManager.TransferReason material)
        {
            if (building.Info.m_class.m_service == ItemClass.Service.Garbage)
            {
                building.m_customBuffer2 = (ushort)Mathf.Clamp(building.m_customBuffer2 + num, 0, 65535);
                float product_value;
                switch (material)
                {
                    case TransferManager.TransferReason.Lumber:
                        product_value = num * RealCityIndustryBuildingAI.GetResourcePrice(material);
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.ResourcePrice, (int)product_value, ItemClass.Service.PlayerIndustry, ItemClass.SubService.PlayerIndustryForestry, ItemClass.Level.Level1);
                        break;
                    case TransferManager.TransferReason.Coal:
                        product_value = num * RealCityIndustryBuildingAI.GetResourcePrice(material);
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.ResourcePrice, (int)product_value, ItemClass.Service.PlayerIndustry, ItemClass.SubService.PlayerIndustryOre, ItemClass.Level.Level1);
                        break;
                    case TransferManager.TransferReason.Petrol:
                        product_value = num * RealCityIndustryBuildingAI.GetResourcePrice(material);
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.ResourcePrice, (int)product_value, ItemClass.Service.PlayerIndustry, ItemClass.SubService.PlayerIndustryOil, ItemClass.Level.Level1);
                        break;
                    default: DebugLog.LogToFileOnly("Error: ProcessGabargeIncome find unknow gabarge transition" + building.Info.m_class.ToString() + "transfer reason " + material.ToString()); break;
                }
            }
        }

    }
}
