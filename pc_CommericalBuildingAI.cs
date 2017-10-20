using System;
using System.Reflection;
using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.Math;
using UnityEngine;


namespace RealCity
{
    public class pc_CommercialBuildingAI : PrivateBuildingAI
    {
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
                            int num = 4000;
                            int num2 = this.CalculateVisitplaceCount(new Randomizer((int)buildingID), width, length);
                            int num3 = Mathf.Max(num2 * 500, num * 4);
                            int customBuffer = (int)data.m_customBuffer1;
                            amountDelta = Mathf.Clamp(amountDelta, 0, num3 - customBuffer);
                            data.m_customBuffer1 = (ushort)(customBuffer + amountDelta);
                        }
                        else
                        {
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

        public void caculate_trade_income(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            float production_value;
            switch (data.Info.m_class.m_level)
            {
                case ItemClass.Level.Level1:
                    production_value = 1f; break;
                case ItemClass.Level.Level2:
                    production_value = 1.2f; break;
                case ItemClass.Level.Level3:
                    production_value = 1.5f; break;
                default:
                    production_value = 0f; break;
            }

            switch (data.Info.m_class.m_subService)
            {
                case ItemClass.SubService.CommercialEco:
                    production_value = 0.9f; break;
                case ItemClass.SubService.CommercialLeisure:
                    production_value = 1.8f; break;
                case ItemClass.SubService.CommercialTourist:
                    production_value = 2.0f; break;
                default:
                    break;
            }

            float trade_tax = 0;
            float trade_income = amountDelta * pc_PrivateBuildingAI.comm_profit * production_value;
            if (comm_data.building_money[buildingID] > 0)
            {
                trade_tax = -trade_income * 0.1f;
                Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Commercial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
            }
            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (int)(trade_income + trade_tax);

        }
    }
}
