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
            float trade_tax = 0f;
            float trade_income1 = (float)amountDelta * pc_PrivateBuildingAI.GetPrice(true, buildingID, data);
            trade_tax = -trade_income1 * pc_PrivateBuildingAI.GetTaxRate(data, buildingID);
            Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
            comm_data.building_money[buildingID] = (comm_data.building_money[buildingID] - (trade_income1 + trade_tax));
        }
    }
}

