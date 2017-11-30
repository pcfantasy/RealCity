using ColossalFramework;
using ColossalFramework.Math;
using UnityEngine;
using ColossalFramework.Globalization;

namespace RealCity
{
    public class pc_IndustrialExtractorAI : PrivateBuildingAI
    {
        // IndustrialExtractorAI
        public override int CalculateProductionCapacity(Randomizer r, int width, int length)
        {
            ItemClass @class = this.m_info.m_class;
            int num;
            if (@class.m_subService == ItemClass.SubService.IndustrialGeneric)
            {
                if (@class.m_level == ItemClass.Level.Level1)
                {
                    num = 100;
                }
                else if (@class.m_level == ItemClass.Level.Level2)
                {
                    num = 140;
                }
                else
                {
                    num = 160;
                }
            }
            else
            {
                num = 100;
            }
            if (num != 0)
            {
                num = Mathf.Max(100, width * length * num + r.Int32(100u)) / 100;
            }
            return num;
        }

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
            float trade_income = 0;
            float trade_tax = 0f;
            //DebugLog.LogToFileOnly(data.Info.m_class.m_subService.ToString() + "money = " + trade_tax.ToString());
            switch (material)
            {
                case TransferManager.TransferReason.Grain:
                    trade_income = amountDelta * (pc_PrivateBuildingAI.grain_export_price + (1f - pc_PrivateBuildingAI.grain_export_ratio) * 0.1f * comm_data.ConsumptionDivider1 * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.food_index);
                    if ((comm_data.building_money[buildingID] - trade_income) > 0)
                    {
                        trade_tax = -trade_income * 0.75f;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
                    }
                    break;
                case TransferManager.TransferReason.Logs:
                    trade_income = amountDelta * (pc_PrivateBuildingAI.log_export_price + (1f - pc_PrivateBuildingAI.log_export_ratio) * 0.1f * comm_data.ConsumptionDivider1 * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.lumber_index);
                    if ((comm_data.building_money[buildingID] - trade_income) > 0)
                    {
                        trade_tax = -trade_income * 0.80f;
                        //DebugLog.LogToFileOnly(data.Info.m_class.m_subService.ToString() + "money = " + trade_tax.ToString());
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
                    }
                    break;
                case TransferManager.TransferReason.Oil:
                    trade_income = amountDelta * (pc_PrivateBuildingAI.oil_export_price + (1f - pc_PrivateBuildingAI.oil_export_ratio) * 0.1f * comm_data.ConsumptionDivider1 * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.petrol_index);
                    if ((comm_data.building_money[buildingID] - trade_income) > 0)
                    {
                        trade_tax = -trade_income * 0.9f;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
                    }
                    break;
                case TransferManager.TransferReason.Ore:
                    trade_income = amountDelta * (pc_PrivateBuildingAI.ore_export_price + (1f - pc_PrivateBuildingAI.ore_export_ratio) * 0.1f * comm_data.ConsumptionDivider1 * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.coal_index);
                    if ((comm_data.building_money[buildingID] - trade_income) > 0)
                    {
                        trade_tax = -trade_income * 0.9f;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
                    }
                    break;
            }                  
            comm_data.building_money[buildingID] = (comm_data.building_money[buildingID] - (trade_income + trade_tax));
        }

        /*public override string GetLevelUpInfo(ushort buildingID, ref Building data, out float progress)
        {
            comm_data.current_buildingid = buildingID;
            if ((data.m_problems & Notification.Problem.FatalProblem) != Notification.Problem.None)
            {
                progress = 0f;
                return Locale.Get("LEVELUP_IMPOSSIBLE");
            }
            if (this.m_info.m_class.m_subService != ItemClass.SubService.IndustrialGeneric)
            {
                progress = 0f;
                return Locale.Get("LEVELUP_SPECIAL_INDUSTRY");
            }
            return base.GetLevelUpInfo(buildingID, ref data, out progress);
        }*/

    }
}

