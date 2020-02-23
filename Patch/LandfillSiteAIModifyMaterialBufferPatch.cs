using ColossalFramework;
using Harmony;
using RealCity.CustomAI;
using RealCity.CustomData;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class IndustrialExtractorAIModifyMaterialBufferPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(IndustrialExtractorAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
        }

        public static void Prefix(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            if (material == RealCityIndustrialExtractorAI.GetOutgoingTransferReason(buildingID))
            {
                RevertGabargeIncome(buildingID, amountDelta, material);
            }
        }

        public static void RevertGabargeIncome(ushort buildingID , int num , TransferManager.TransferReason material)
        {
            BuildingManager instance = Singleton<BuildingManager>.instance;
            Building building = instance.m_buildings.m_buffer[buildingID];
            if (building.Info.m_class.m_service == ItemClass.Service.Garbage)
            {
                float product_value = 0f;
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
