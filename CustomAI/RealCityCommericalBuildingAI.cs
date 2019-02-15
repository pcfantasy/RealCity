using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.Math;
using RealCity.Util;
using UnityEngine;


namespace RealCity.CustomAI
{
    public class RealCityCommercialBuildingAI : PrivateBuildingAI
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
                        if (material == TransferManager.TransferReason.Goods || material == TransferManager.TransferReason.Petrol || material == TransferManager.TransferReason.Food || material == TransferManager.TransferReason.LuxuryProducts)
                        {
                            int width = data.Width;
                            int length = data.Length;
                            int num = 16000;
                            int num2 = this.CalculateVisitplaceCount((ItemClass.Level)data.m_level, new Randomizer((int)buildingID), width, length);
                            int num3 = Mathf.Max(num2 * 500, num * 4);
                            num3 = 64000;
                            int customBuffer = (int)data.m_customBuffer1;
                            amountDelta = Mathf.Clamp(amountDelta, 0, num3 - customBuffer);
                            // NON-STOCK CODE START
                            process_incoming(buildingID, ref data, material, ref amountDelta);
                            // For industry DLC, customBuffer may more than 64000. Fix this
                            if (material == TransferManager.TransferReason.LuxuryProducts)
                            {
                                    data.m_customBuffer1 = (ushort)(customBuffer + amountDelta * MainDataStore.commericalPriceAdjust);
                                    MainDataStore.building_buffer1[buildingID] = data.m_customBuffer1;
                            }
                            else
                            {
                                    data.m_customBuffer1 = (ushort)(customBuffer + amountDelta);
                                    MainDataStore.building_buffer1[buildingID] = data.m_customBuffer1;
                            }
                        }
                        else
                        {
                            if (material == TransferManager.TransferReason.Entertainment)
                            {
                                caculate_trade_income(buildingID, ref data, material, ref amountDelta);
                                return;
                            }
                            /// NON-STOCK CODE END ///
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
            int customBuffer2 = (int)data.m_customBuffer2;
            amountDelta = Mathf.Clamp(amountDelta, -customBuffer2, 0);
            // NON-STOCK CODE START
            caculate_trade_income(buildingID, ref data, material, ref amountDelta);
            data.m_customBuffer2 = (ushort)(customBuffer2 + amountDelta);
            MainDataStore.building_buffer2[buildingID] = data.m_customBuffer2;
            /// NON-STOCK CODE END ///
            data.m_outgoingProblemTimer = 0;
        }

        public void process_incoming(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            float trade_income1 = (float)amountDelta * RealCityIndustryBuildingAI.GetResourcePrice(material);
            MainDataStore.building_money[buildingID] = MainDataStore.building_money[buildingID] - trade_income1;
        }

        public void caculate_trade_income(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            float trade_tax = 0;
            float trade_income = amountDelta * RealCityIndustryBuildingAI.GetResourcePrice(material);
            trade_tax = -trade_income * (float)RealCityPrivateBuildingAI.GetTaxRate(data, buildingID) / 100f;
            Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Commercial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
            MainDataStore.building_money[buildingID] = (MainDataStore.building_money[buildingID] - (trade_income + trade_tax));
        }
    }
}
