using ColossalFramework;
using Harmony;
using RealCity.CustomAI;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class IndustrialBuildingAIModifyMaterialBufferPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(IndustrialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
        }
        public static void Postfix(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            if (material == RealCityIndustrialBuildingAI.GetIncomingTransferReason(buildingID) || material == RealCityIndustrialBuildingAI.GetSecondaryIncomingTransferReason(buildingID))
            {
                processIncoming(buildingID, ref data, material, ref amountDelta);
            }
            else if (material == RealCityIndustrialBuildingAI.GetOutgoingTransferReason(buildingID))
            {
                caculateTradeIncome(buildingID, ref data, material, ref amountDelta);
            }
        }

        public static void processIncoming(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            float trade_income1 = amountDelta * RealCityIndustryBuildingAI.GetResourcePrice(material);
            MainDataStore.building_money[buildingID] = MainDataStore.building_money[buildingID] - trade_income1;
        }

        public static void caculateTradeIncome(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            float trade_income1 = amountDelta * RealCityPrivateBuildingAI.GetPrice(true, buildingID, data);
            float trade_tax = 0;
            trade_tax = -trade_income1 * RealCityPrivateBuildingAI.GetTaxRate(data, buildingID) / 100f;
            Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
            MainDataStore.building_money[buildingID] = (MainDataStore.building_money[buildingID] - (trade_income1 + trade_tax));
        }
    }
}
