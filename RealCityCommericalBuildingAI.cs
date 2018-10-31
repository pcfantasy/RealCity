using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.Math;
using UnityEngine;


namespace RealCity
{
    public class RealCityCommercialBuildingAI : PrivateBuildingAI
    {

        public override int CalculateVisitplaceCount(ItemClass.Level level, Randomizer r, int width, int length)
        {
            ItemClass @class = this.m_info.m_class;
            int num = 0;
            ItemClass.SubService subService = @class.m_subService;
            if (subService != ItemClass.SubService.CommercialLow)
            {
                if (subService != ItemClass.SubService.CommercialHigh)
                {
                    if (subService != ItemClass.SubService.CommercialLeisure)
                    {
                        if (subService != ItemClass.SubService.CommercialTourist)
                        {
                            if (subService == ItemClass.SubService.CommercialEco)
                            {
                                num = 100;
                            }
                        }
                        else
                        {
                            num = 250;
                        }
                    }
                    else
                    {
                        num = 250;
                    }
                }
                else if (level == ItemClass.Level.Level1)
                {
                    num = 200;
                }
                else if (level == ItemClass.Level.Level2)
                {
                    num = 300;
                }
                else
                {
                    num = 400;
                }
            }
            else if (level == ItemClass.Level.Level1)
            {
                num = 90;
            }
            else if (level == ItemClass.Level.Level2)
            {
                num = 100;
            }
            else
            {
                num = 110;
            }
            if (num != 0)
            {
                num = Mathf.Max(200, width * length * num + r.Int32(100u)) / 100;
            }
            return num;
        }

        public override void CreateBuilding(ushort buildingID, ref Building data)
        {
            base.CreateBuilding(buildingID, ref data);
            int width = data.Width;
            int length = data.Length;
            int num = 4000;
            int num2 = this.CalculateVisitplaceCount((ItemClass.Level)data.m_level,new Randomizer((int)buildingID), width, length);
            int num3 = Mathf.Max(num2 * 500, num * 4);
            data.m_customBuffer1 = 8000;
            MainDataStore.building_money[buildingID] -= 8000 * RealCityPrivateBuildingAI.GetPrice(false, buildingID, data);
            DistrictPolicies.Specialization specialization = this.SpecialPolicyNeeded();
            if (specialization != DistrictPolicies.Specialization.None)
            {
                DistrictManager instance = Singleton<DistrictManager>.instance;
                byte district = instance.GetDistrict(data.m_position);
                District[] expr_91_cp_0 = instance.m_districts.m_buffer;
                byte expr_91_cp_1 = district;
                expr_91_cp_0[(int)expr_91_cp_1].m_specializationPoliciesEffect = (expr_91_cp_0[(int)expr_91_cp_1].m_specializationPoliciesEffect | specialization);
            }
        }

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
                        if (material == TransferManager.TransferReason.Goods || material == TransferManager.TransferReason.Petrol || material == TransferManager.TransferReason.Food || material == TransferManager.TransferReason.LuxuryProducts)
                        {
                            int width = data.Width;
                            int length = data.Length;
                            int num = 16000;
                            int num2 = this.CalculateVisitplaceCount((ItemClass.Level)data.m_level, new Randomizer((int)buildingID), width, length);
                            //DebugLog.LogToFileOnly("commerical visitplacecount is =  " + num2.ToString());
                            int num3 = Mathf.Max(num2 * 500, num * 4);
                            num3 = 64000;
                            int customBuffer = (int)data.m_customBuffer1;
                            amountDelta = Mathf.Clamp(amountDelta, 0, num3 - customBuffer);
                            process_incoming(buildingID, ref data, material, ref amountDelta);

                            if (material == TransferManager.TransferReason.LuxuryProducts)
                            {
                                if ((customBuffer + amountDelta * MainDataStore.commericalPriceAdjust) > 64000)
                                {
                                    data.m_customBuffer1 = 64000;
                                    MainDataStore.building_buffer1[buildingID] = customBuffer + amountDelta * MainDataStore.commericalPriceAdjust;
                                }
                                else
                                {
                                    data.m_customBuffer1 = (ushort)(customBuffer + amountDelta);
                                    MainDataStore.building_buffer1[buildingID] = data.m_customBuffer1;
                                }
                            }
                            else
                            {
                                if ((customBuffer + amountDelta) > 64000)
                                {
                                    data.m_customBuffer1 = 64000;
                                    MainDataStore.building_buffer1[buildingID] = customBuffer + amountDelta;
                                }
                                else
                                {
                                    data.m_customBuffer1 = (ushort)(customBuffer + amountDelta);
                                    MainDataStore.building_buffer1[buildingID] = data.m_customBuffer1;
                                }
                            }
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
            MainDataStore.building_buffer2[buildingID] = data.m_customBuffer2;
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
