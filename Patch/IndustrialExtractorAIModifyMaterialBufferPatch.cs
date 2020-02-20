using ColossalFramework;
using Harmony;
using RealCity.CustomAI;
using RealCity.CustomData;
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
        public static void Postfix(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            if (material == RealCityIndustrialExtractorAI.GetOutgoingTransferReason(buildingID))
            {
                caculateTradeIncome(buildingID, ref data, material, ref amountDelta);
            }
        }

        public static void caculateTradeIncome(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            float trade_tax = 0f;
            float trade_income1 = amountDelta * RealCityPrivateBuildingAI.GetPrice(true, buildingID, data);
            trade_tax = -trade_income1 * RealCityPrivateBuildingAI.GetTaxRate(data, buildingID) / 100f;
            Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
            BuildingData.buildingMoney[buildingID] = (BuildingData.buildingMoney[buildingID] - (trade_income1 + trade_tax));
        }
    }
}
