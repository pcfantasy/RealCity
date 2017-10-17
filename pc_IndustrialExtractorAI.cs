using System;
using System.Reflection;
using ColossalFramework;
using ColossalFramework.Math;
using UnityEngine;

namespace RealCity
{
    public class pc_IndustrialExtractorAI : PrivateBuildingAI
    {
        private TransferManager.TransferReason GetOutgoingTransferReason()
        {
            switch (this.m_info.m_class.m_subService)
            {
                case ItemClass.SubService.IndustrialForestry:
                    return TransferManager.TransferReason.Logs;
                case ItemClass.SubService.IndustrialFarming:
                    return TransferManager.TransferReason.Grain;
                case ItemClass.SubService.IndustrialOil:
                    return TransferManager.TransferReason.Oil;
                case ItemClass.SubService.IndustrialOre:
                    return TransferManager.TransferReason.Ore;
                default:
                    return TransferManager.TransferReason.None;
            }
        }

        // IndustrialExtractorAI
        public override void ModifyMaterialBuffer(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            if (material == this.GetOutgoingTransferReason())
            {
                int customBuffer = (int)data.m_customBuffer1;
                amountDelta = Mathf.Clamp(amountDelta, -customBuffer, 0);
                caculate_trade_income(buildingID, ref data, material, ref amountDelta);
                data.m_customBuffer1 = (ushort)(customBuffer + amountDelta);
            }
            else
            {
                base.ModifyMaterialBuffer(buildingID, ref data, material, ref amountDelta);
            }
        }
        public void caculate_trade_income(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            float production_value;
            switch (data.Info.m_class.m_subService)
            {
                case ItemClass.SubService.IndustrialFarming:
                    production_value = 0.05f; break;
                case ItemClass.SubService.IndustrialForestry:
                    production_value = 0.05f; break;
                case ItemClass.SubService.IndustrialOil:
                    production_value = 0.1f; break;
                case ItemClass.SubService.IndustrialOre:
                    production_value = 0.1f; break;
                default:
                    production_value = 0f; break;
            }
            switch (material)
            {
                case TransferManager.TransferReason.Logs:
                    float trade_tax = 0;
                    float trade_income = amountDelta * comm_data.log_profit * production_value;
                    if (comm_data.building_money[buildingID] > 0)
                    {
                        trade_tax = -trade_income * 0.04f;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 100);
                    }
                    comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (int)(trade_income + trade_tax);
                    break;
                case TransferManager.TransferReason.Grain:
                    trade_tax = 0;
                    trade_income = amountDelta * comm_data.grain_profit * production_value;
                    if (comm_data.building_money[buildingID] > 0)
                    {
                        trade_tax = -trade_income * 0.04f;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 100);
                    }
                    comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (int)(trade_income + trade_tax);
                    break;
                case TransferManager.TransferReason.Oil:
                    trade_tax = 0;
                    trade_income = amountDelta * comm_data.oil_profit * production_value;
                    if (comm_data.building_money[buildingID] > 0)
                    {
                        trade_tax = -trade_income * 0.04f;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 100);
                    }
                    comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (int)(trade_income + trade_tax);
                    break;
                case TransferManager.TransferReason.Ore:
                    trade_tax = 0;
                    trade_income = amountDelta * comm_data.ore_profit * production_value;
                    if (comm_data.building_money[buildingID] > 0)
                    {
                        trade_tax = -trade_income * 0.04f;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 100);
                    }
                    comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (int)(trade_income + trade_tax);
                    break;
            }
        }
    }
}

