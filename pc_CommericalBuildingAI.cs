﻿using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.Math;
using UnityEngine;


namespace RealCity
{
    public class pc_CommercialBuildingAI : PrivateBuildingAI
    {
        public override int CalculateProductionCapacity(Randomizer r, int width, int length)
        {
            ItemClass @class = this.m_info.m_class;
            int num = 0;
            if (@class.m_subService == ItemClass.SubService.CommercialEco)
            {
                num = 50;
            }
            if (num != 0)
            {
                num = Mathf.Max(100, width * length * num + r.Int32(100u)) / 100;
            }
            return num;
        }

        //public TransferManager.TransferReason GetIncomingTransferReason()
        //{
        //    DebugLog.LogToFileOnly("comm building GetIncomingTransferReason called");
        //    return  TransferManager.TransferReason.Goods;
        //}

        private DistrictPolicies.Specialization SpecialPolicyNeeded()
        {
            ItemClass.SubService subService = this.m_info.m_class.m_subService;
            if (subService == ItemClass.SubService.CommercialLeisure)
            {
                return DistrictPolicies.Specialization.Leisure;
            }
            if (subService == ItemClass.SubService.CommercialTourist)
            {
                return DistrictPolicies.Specialization.Tourist;
            }
            if (subService != ItemClass.SubService.CommercialEco)
            {
                return DistrictPolicies.Specialization.None;
            }
            return DistrictPolicies.Specialization.Organic;
        }

        public override void ModifyMaterialBuffer(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
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
                        if (material == TransferManager.TransferReason.Goods)
                        {
                            int width = data.Width;
                            int length = data.Length;
                            int num = 16000;
                            int num2 = this.CalculateVisitplaceCount(new Randomizer((int)buildingID), width, length);
                            //DebugLog.LogToFileOnly("commerical visitplacecount is =  " + num2.ToString());
                            int num3 = Mathf.Max(num2 * 500, num * 4);
                            num3 = 64000;
                            int customBuffer = (int)data.m_customBuffer1;
                            amountDelta = Mathf.Clamp(amountDelta, 0, num3 - customBuffer);
                            process_incoming(buildingID, ref data, material, ref amountDelta);
                            data.m_customBuffer1 = (ushort)(customBuffer + amountDelta);
                            //sync with org game.
                            comm_data.building_buffer1[buildingID] = (ushort)(customBuffer + amountDelta);
                        }
                        else
                        {
                            if (material == TransferManager.TransferReason.Entertainment)
                            {
                                caculate_trade_income(buildingID, ref data, material, ref amountDelta);
                                return;
                            }
                            base.ModifyMaterialBuffer(buildingID, ref data, material, ref amountDelta);
                        }
                        return;
                    }
                    break;
            }
            //do not allow rush hour add 200 demand during 20-4 in the night.
            if (amountDelta == -200 || amountDelta == -50)
            {
                amountDelta = 0;
            }
            //DebugLog.LogToFileOnly("we go in now, find a visit arrive in comm " + Environment.StackTrace);
            int customBuffer2 = (int)data.m_customBuffer2;
            amountDelta = Mathf.Clamp(amountDelta, -customBuffer2, 0);
            caculate_trade_income(buildingID, ref data, material, ref amountDelta);
            data.m_customBuffer2 = (ushort)(customBuffer2 + amountDelta);
            data.m_outgoingProblemTimer = 0;
        }

        public void process_incoming(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            float trade_income1 = (float)amountDelta * pc_PrivateBuildingAI.GetPrice(false, buildingID, data, material);
            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - trade_income1;
        }

        public void caculate_trade_income(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            float trade_tax = 0;
            float trade_income = amountDelta;
            trade_tax = -amountDelta * pc_PrivateBuildingAI.GetTaxRate(data, buildingID);
            Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Commercial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
            comm_data.building_money[buildingID] = (comm_data.building_money[buildingID] - (trade_income + trade_tax));
        }
    }
}
