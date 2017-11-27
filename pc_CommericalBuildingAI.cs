using ColossalFramework;
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

        private TransferManager.TransferReason GetIncomingTransferReason()
        {
            return  TransferManager.TransferReason.Goods;
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
                            int num = 4000;
                            int num2 = this.CalculateVisitplaceCount(new Randomizer((int)buildingID), width, length);
                            //DebugLog.LogToFileOnly("commerical visitplacecount is =  " + num2.ToString());
                            int num3 = Mathf.Max(num2 * 500, num * 4);
                            int customBuffer = (int)data.m_customBuffer1;
                            amountDelta = Mathf.Clamp(amountDelta, 0, num3 - customBuffer);
                            process_incoming(buildingID, ref data, material, ref amountDelta);
                            data.m_customBuffer1 = (ushort)(customBuffer + amountDelta);
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
            //try discount sale in customBuffer1
            if (amountDelta != -300)
            {
                int customBuffer2 = (int)data.m_customBuffer2;
                amountDelta = Mathf.Clamp(amountDelta, -customBuffer2, 0);
                caculate_trade_income(buildingID, ref data, material, ref amountDelta);
                data.m_customBuffer2 = (ushort)(customBuffer2 + amountDelta);
            } else
            {
                //DebugLog.LogToFileOnly("we do viture shopping, cost customBuffer1");
                amountDelta = -100;
                int customBuffer1 = (int)data.m_customBuffer1;
                amountDelta = Mathf.Clamp(amountDelta, -customBuffer1, 0);
                float employee_benefit = caculate_employee_benefit(buildingID, ref data);
                int temp_amount = (int)(amountDelta * (0.8f + employee_benefit));
                caculate_trade_income(buildingID, ref data, material, ref temp_amount);
                data.m_customBuffer1 = (ushort)(customBuffer1 + amountDelta);
            }
            data.m_outgoingProblemTimer = 0;
        }

        public void process_incoming(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            float trade_income = 0;
            if (data.Info.m_class.m_subService == ItemClass.SubService.CommercialLow)
            {
                switch (data.Info.m_class.m_level)
                {
                    case ItemClass.Level.Level1:
                        trade_income = amountDelta * (float)(pc_PrivateBuildingAI.good_import_price - 0.6f * (1f - pc_PrivateBuildingAI.good_import_ratio) - 0.1f* pc_PrivateBuildingAI.good_level2_ratio - 0.2f * pc_PrivateBuildingAI.good_level3_ratio) / 4; break;
                    case ItemClass.Level.Level2:
                        trade_income = amountDelta * (float)(pc_PrivateBuildingAI.good_import_price - 0.1f - 0.5f * (1f - pc_PrivateBuildingAI.good_import_ratio) - 0.1f * pc_PrivateBuildingAI.good_level2_ratio - 0.2f * pc_PrivateBuildingAI.good_level3_ratio) / 4; break;
                    case ItemClass.Level.Level3:
                        trade_income = amountDelta * (float)(pc_PrivateBuildingAI.good_import_price - 0.2f - 0.4f * (1f - pc_PrivateBuildingAI.good_import_ratio) - 0.1f * pc_PrivateBuildingAI.good_level2_ratio - 0.2f * pc_PrivateBuildingAI.good_level3_ratio) / 4; break;
                    default:
                        trade_income = 0; break;
                }
            }
            else if (data.Info.m_class.m_subService == ItemClass.SubService.CommercialHigh)
            {
                switch (data.Info.m_class.m_level)
                {
                    case ItemClass.Level.Level1:
                        trade_income = amountDelta * (float)(pc_PrivateBuildingAI.good_import_price - 0.3f - 0.3f * (1f - pc_PrivateBuildingAI.good_import_ratio) - 0.1f * pc_PrivateBuildingAI.good_level2_ratio - 0.2f * pc_PrivateBuildingAI.good_level3_ratio) / 4; break;
                    case ItemClass.Level.Level2:
                        trade_income = amountDelta * (float)(pc_PrivateBuildingAI.good_import_price - 0.4f - 0.2f * (1f - pc_PrivateBuildingAI.good_import_ratio) - 0.1f * pc_PrivateBuildingAI.good_level2_ratio - 0.2f * pc_PrivateBuildingAI.good_level3_ratio) / 4; break;
                    case ItemClass.Level.Level3:
                        trade_income = amountDelta * (float)(pc_PrivateBuildingAI.good_import_price - 0.5f - 0.1f * (1f - pc_PrivateBuildingAI.good_import_ratio) - 0.1f * pc_PrivateBuildingAI.good_level2_ratio - 0.2f * pc_PrivateBuildingAI.good_level3_ratio) / 4; break;
                    default:
                        trade_income = 0; break;
                }
            }

            switch (data.Info.m_class.m_subService)
            {
                case ItemClass.SubService.CommercialEco:
                    trade_income = amountDelta * (float)(pc_PrivateBuildingAI.good_import_price - 0.3f - 0.5f * (1f - pc_PrivateBuildingAI.good_import_ratio) - 0.1f * pc_PrivateBuildingAI.good_level2_ratio - 0.2f * pc_PrivateBuildingAI.good_level3_ratio) / (float)comm_data.ConsumptionDivider; break;
                case ItemClass.SubService.CommercialLeisure:
                    trade_income = amountDelta * (float)(pc_PrivateBuildingAI.good_import_price - 0.3f - 0.5f * (1f - pc_PrivateBuildingAI.good_import_ratio) - 0.1f * pc_PrivateBuildingAI.good_level2_ratio - 0.2f * pc_PrivateBuildingAI.good_level3_ratio) / (float)comm_data.ConsumptionDivider; break;
                case ItemClass.SubService.CommercialTourist:
                    trade_income = amountDelta * (float)(pc_PrivateBuildingAI.good_import_price - 0.3f - 0.5f * (1f - pc_PrivateBuildingAI.good_import_ratio) - 0.1f * pc_PrivateBuildingAI.good_level2_ratio - 0.2f * pc_PrivateBuildingAI.good_level3_ratio) / (float)comm_data.ConsumptionDivider; break;
                default:
                    break;
            }
            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - trade_income;
        }


        public float caculate_tax_benefit(ushort buildingID, ref Building data)
        {
            int aliveWorkCount = 0;
            int totalWorkCount = 0;
            Citizen.BehaviourData behaviour = default(Citizen.BehaviourData);
            BuildingUI.GetWorkBehaviour(buildingID, ref data, ref behaviour, ref aliveWorkCount, ref totalWorkCount);
            float tax_benefit = 0;
            tax_benefit = 30f / (aliveWorkCount + 20);
            if (tax_benefit > 1f)
            {
                tax_benefit = 1f;
            } else if (tax_benefit < 0.2f)
            {
                tax_benefit = 0.2f;
            }
            return tax_benefit;
        }

        public float caculate_employee_benefit(ushort buildingID, ref Building data)
        {
            int aliveWorkCount = 0;
            int totalWorkCount = 0;
            Citizen.BehaviourData behaviour = default(Citizen.BehaviourData);
            BuildingUI.GetWorkBehaviour(buildingID, ref data, ref behaviour, ref aliveWorkCount, ref totalWorkCount);
            float employee_benefit = aliveWorkCount / 100f;
            if (employee_benefit > 0.2f)
            {
                employee_benefit = 0.2f;
            }
            return employee_benefit;
        }

        public void caculate_trade_income(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            float trade_tax = 0;
            float trade_income = 0;
            float tax_benefit = caculate_tax_benefit(buildingID, ref data);
            if (data.Info.m_class.m_subService == ItemClass.SubService.CommercialHigh)
            {
                switch (data.Info.m_class.m_level)
                {
                    case ItemClass.Level.Level1:
                        trade_income = amountDelta * 1; trade_tax = -trade_income * 0.15f * tax_benefit; break;
                    case ItemClass.Level.Level2:
                        trade_income = amountDelta * 1; trade_tax = -trade_income * 0.17f * tax_benefit; break;
                    case ItemClass.Level.Level3:
                        trade_income = amountDelta * 1; trade_tax = -trade_income * 0.19f * tax_benefit; break;
                    default:
                        trade_income = 0; break;
                }
            }
            else if (data.Info.m_class.m_subService == ItemClass.SubService.CommercialLow)
            {
                switch (data.Info.m_class.m_level)
                {
                    case ItemClass.Level.Level1:
                        trade_income = amountDelta * 1; trade_tax = -trade_income * 0.2f * tax_benefit; break;
                    case ItemClass.Level.Level2:
                        trade_income = amountDelta * 1; trade_tax = -trade_income * 0.22f * tax_benefit; break;
                    case ItemClass.Level.Level3:
                        trade_income = amountDelta * 1; trade_tax = -trade_income * 0.24f * tax_benefit; break;
                    default:
                        trade_income = 0; break;
                }
            }

            switch (data.Info.m_class.m_subService)
            {
                case ItemClass.SubService.CommercialEco:
                    trade_income = amountDelta; trade_tax = -trade_income * 0.15f * tax_benefit; break;
                case ItemClass.SubService.CommercialLeisure:
                    trade_income = amountDelta; trade_tax = -trade_income * 0.15f * tax_benefit; break;
                case ItemClass.SubService.CommercialTourist:
                    trade_income = amountDelta; trade_tax = -trade_income * 0.15f * tax_benefit; break;
                default:
                    break;
            }
            if ((comm_data.building_money[buildingID] - trade_income) > 0)
            {
                Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Commercial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
            }
            else
            {
                trade_tax = 0f;
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
            if (this.m_info.m_class.m_subService != ItemClass.SubService.CommercialLow && this.m_info.m_class.m_subService != ItemClass.SubService.CommercialHigh)
            {
                progress = 0f;
                return Locale.Get("LEVELUP_SPECIAL_INDUSTRY");
            }
            if (this.m_info.m_class.m_level == ItemClass.Level.Level3)
            {
                progress = 0f;
                return Locale.Get("LEVELUP_COMMERCIAL_HAPPY");
            }
            if (data.m_problems != Notification.Problem.None)
            {
                progress = 0f;
                return Locale.Get("LEVELUP_DISTRESS");
            }
            if (data.m_levelUpProgress == 0)
            {
                return base.GetLevelUpInfo(buildingID, ref data, out progress);
            }
            if (data.m_levelUpProgress == 1)
            {
                progress = 0.933333337f;
                return Locale.Get("LEVELUP_HIGHRISE_BAN");
            }
            int num = (int)((data.m_levelUpProgress & 15) - 1);
            int num2 = (data.m_levelUpProgress >> 4) - 1;
            if (num <= num2)
            {
                progress = (float)num * 0.06666667f;
                return Locale.Get("LEVELUP_LOWWEALTH");
            }
            progress = (float)num2 * 0.06666667f;
            return Locale.Get("LEVELUP_LOWLANDVALUE");
        }*/
    }
}
