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
    public class CommercialBuildingAIModifyMaterialBufferPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(CommercialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
        }
        public static void Postfix(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            switch (material)
            {
                case TransferManager.TransferReason.ShoppingB:
                case TransferManager.TransferReason.ShoppingC:
                case TransferManager.TransferReason.ShoppingD:
                case TransferManager.TransferReason.ShoppingE:
                case TransferManager.TransferReason.ShoppingF:
                case TransferManager.TransferReason.ShoppingG:
                case TransferManager.TransferReason.ShoppingH:
                    break;
                default:
                    if (material != TransferManager.TransferReason.Shopping)
                    {
                        if (material == TransferManager.TransferReason.Goods || material == TransferManager.TransferReason.Petrol || material == TransferManager.TransferReason.Food || material == TransferManager.TransferReason.LuxuryProducts)
                        {
                            processIncoming(buildingID, ref data, material, ref amountDelta);
                        }
                        else
                        {
                            if (material == TransferManager.TransferReason.Entertainment)
                            {
                                caculateTradeIncome(buildingID, ref data, material, ref amountDelta);
                                return;
                            }
                        }
                        return;
                    }
                    break;
            }
            caculateTradeIncome(buildingID, ref data, material, ref amountDelta);
            if (RealCity.reduceVehicle)
            {
                data.m_customBuffer2 -= (ushort)(amountDelta >> MainDataStore.reduceCargoDivShift);
            }
        }

        public static void processIncoming(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            float trade_income1 = amountDelta * RealCityIndustryBuildingAI.GetResourcePrice(material);
            BuildingData.buildingMoney[buildingID] = BuildingData.buildingMoney[buildingID] - trade_income1;
        }

        public static void caculateTradeIncome(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            float trade_tax = 0;
            float trade_income = amountDelta * RealCityIndustryBuildingAI.GetResourcePrice(material);
            trade_tax = -trade_income * RealCityPrivateBuildingAI.GetTaxRate(data, buildingID) / 100f;
            Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Commercial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
            BuildingData.buildingMoney[buildingID] = (BuildingData.buildingMoney[buildingID] - (trade_income + trade_tax));
        }
    }
}
