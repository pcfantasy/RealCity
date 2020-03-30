using ColossalFramework;
using HarmonyLib;
using RealCity.CustomAI;
using RealCity.CustomData;
using RealCity.Util;
using System;
using System.Reflection;
using UnityEngine;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class IndustrialBuildingAIModifyMaterialBufferPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(IndustrialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
        }


        public static void Prefix(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            RealCityIndustrialBuildingAI.InitDelegate();
            if (material == RealCityIndustrialBuildingAI.GetOutgoingTransferReason((IndustrialBuildingAI)(data.Info.m_buildingAI)))
            {
                RevertTradeIncome(buildingID, ref data, material, ref amountDelta);
            }
        }

        public static void Postfix(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            RealCityIndustrialBuildingAI.InitDelegate();
            if (material == RealCityIndustrialBuildingAI.GetIncomingTransferReason((IndustrialBuildingAI)(data.Info.m_buildingAI), buildingID) || material == RealCityIndustrialBuildingAI.GetSecondaryIncomingTransferReason((IndustrialBuildingAI)(data.Info.m_buildingAI), buildingID))
            {
                ProcessIncoming(buildingID, material, ref amountDelta);
            }
            else if (material == RealCityIndustrialBuildingAI.GetOutgoingTransferReason((IndustrialBuildingAI)(data.Info.m_buildingAI)))
            {
                CaculateTradeIncome(buildingID, ref data, material, ref amountDelta);
            }
        }

        public static void ProcessIncoming(ushort buildingID, TransferManager.TransferReason material, ref int amountDelta)
        {
            float tradeIncome = amountDelta * RealCityIndustryBuildingAI.GetResourcePrice(material);
            BuildingData.buildingMoney[buildingID] = BuildingData.buildingMoney[buildingID] - tradeIncome;
        }

        public static void RevertTradeIncome(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            if (amountDelta > 0)
            {
                //revert
                data.m_customBuffer2 = (ushort)Mathf.Clamp(data.m_customBuffer2 + amountDelta, 0, 65535);
                float tradeIncome = -amountDelta * RealCityIndustryBuildingAI.GetResourcePrice(material);
                float tradeTax = -tradeIncome * RealCityPrivateBuildingAI.GetTaxRate(data) / 100f;
                MainDataStore.unfinishedTransitionLost += (int)(tradeTax / 100f);
                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)17, (int)tradeTax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level);
                BuildingData.buildingMoney[buildingID] = (BuildingData.buildingMoney[buildingID] + (tradeIncome + tradeTax));
            }
        }

        public static void CaculateTradeIncome(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            if (amountDelta < 0)
            {
                float tradeIncome = amountDelta * RealCityIndustryBuildingAI.GetResourcePrice(material);
                float tradeTax = -tradeIncome * RealCityPrivateBuildingAI.GetTaxRate(data) / 100f;
                Singleton<EconomyManager>.instance.AddPrivateIncome((int)tradeTax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111333);
                BuildingData.buildingMoney[buildingID] = (BuildingData.buildingMoney[buildingID] - (tradeIncome + tradeTax));
            }
        }
    }
}
