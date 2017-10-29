using ColossalFramework;
using ColossalFramework.Math;
using UnityEngine;
using ColossalFramework.Globalization;

namespace RealCity
{
    public class pc_IndustrialBuildingAI : PrivateBuildingAI
    {
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

        private TransferManager.TransferReason GetIncomingTransferReason(ushort buildingID)
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
                    {
                        Randomizer randomizer = new Randomizer(buildingID);
                        switch (randomizer.Int32(4u))
                        {
                            case 0:
                                return TransferManager.TransferReason.Lumber;
                            case 1:
                                return TransferManager.TransferReason.Food;
                            case 2:
                                return TransferManager.TransferReason.Petrol;
                            case 3:
                                return TransferManager.TransferReason.Coal;
                            default:
                                return TransferManager.TransferReason.None;
                        }
                    }
            }
        }

        private int GetConsumptionDivider()
        {
            switch (this.m_info.m_class.m_subService)
            {
                case ItemClass.SubService.IndustrialForestry:
                    return 1;
                case ItemClass.SubService.IndustrialFarming:
                    return 1;
                case ItemClass.SubService.IndustrialOil:
                    return 1;
                case ItemClass.SubService.IndustrialOre:
                    return 1;
                default:
                    return 4;
            }
        }

        private TransferManager.TransferReason GetOutgoingTransferReason()
        {
            switch (this.m_info.m_class.m_subService)
            {
                case ItemClass.SubService.IndustrialForestry:
                    return TransferManager.TransferReason.Lumber;
                case ItemClass.SubService.IndustrialFarming:
                    return TransferManager.TransferReason.Food;
                case ItemClass.SubService.IndustrialOil:
                    return TransferManager.TransferReason.Petrol;
                case ItemClass.SubService.IndustrialOre:
                    return TransferManager.TransferReason.Coal;
                default:
                    return TransferManager.TransferReason.Goods;
            }
        }

        public override void ModifyMaterialBuffer(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            if (material == this.GetIncomingTransferReason(buildingID))
            {
                int width = data.Width;
                int length = data.Length;
                int num = 4000;
                int num2 = this.CalculateProductionCapacity(new Randomizer((int)buildingID), width, length);
                int consumptionDivider = this.GetConsumptionDivider();
                int num3 = Mathf.Max(num2 * 500 / consumptionDivider, num * 4);
                int customBuffer = (int)data.m_customBuffer1;
                amountDelta = Mathf.Clamp(amountDelta, 0, num3 - customBuffer);
                data.m_customBuffer1 = (ushort)(customBuffer + amountDelta);
            }
            else if (material == this.GetOutgoingTransferReason())
            {
                int customBuffer2 = (int)data.m_customBuffer2;
                amountDelta = Mathf.Clamp(amountDelta, -customBuffer2, 0);
                caculate_trade_income(buildingID, ref data, material, ref amountDelta);
                data.m_customBuffer2 = (ushort)(customBuffer2 + amountDelta);
            }
            else
            {
                base.ModifyMaterialBuffer(buildingID, ref data, material, ref amountDelta);
            }
        }

        public void caculate_trade_income(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            float production_value, final_profit;
            Citizen.BehaviourData behaviour = default(Citizen.BehaviourData);
            int aliveWorkerCount = 0;
            int totalWorkerCount = 0;
            base.GetWorkBehaviour(buildingID, ref data, ref behaviour, ref aliveWorkerCount, ref totalWorkerCount);
            float num = (float)aliveWorkerCount / 5f;
            if (num < 1f)
            {
                num = 1f;
            }
            switch (data.Info.m_class.m_level)
            {
                case ItemClass.Level.Level1:
                    production_value = 2f * num; break;
                case ItemClass.Level.Level2:
                    production_value = 2.5f * num; break;
                case ItemClass.Level.Level3:
                    production_value = 3f * num; break;
                default:
                    production_value = 0f; break;
            }

            switch (data.Info.m_class.m_subService)
            {
                case ItemClass.SubService.IndustrialOil:
                    production_value = 0.5f * num; break;
                case ItemClass.SubService.IndustrialOre:
                    production_value = 0.5f * num; break;
                case ItemClass.SubService.IndustrialForestry:
                    production_value = 0.7f * num; break;
                case ItemClass.SubService.IndustrialFarming:
                    production_value = 0.8f * num; break;
                default:
                    break;
            }
            switch (material)
            {
                case TransferManager.TransferReason.Lumber:
                    float trade_tax = 0;
                    final_profit = pc_PrivateBuildingAI.lumber_profit * production_value;
                    if (final_profit > 0.9f)
                    {
                        final_profit = 0.9f;
                    }
                    float trade_income = amountDelta * final_profit;
                    if ((comm_data.building_money[buildingID] - trade_income)> 0)
                    {
                        trade_tax = -trade_income * 0.2f;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
                    }
                    comm_data.building_money[buildingID] = (comm_data.building_money[buildingID] - (trade_income + trade_tax));
                    break;
                case TransferManager.TransferReason.Food:
                    trade_tax = 0;
                    final_profit = pc_PrivateBuildingAI.food_profit * production_value;
                    if (final_profit > 0.9f)
                    {
                        final_profit = 0.9f;
                    }
                    trade_income = amountDelta * final_profit;
                    if ((comm_data.building_money[buildingID] - trade_income)> 0)
                    {
                        trade_tax = -trade_income * 0.2f;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
                    }
                    comm_data.building_money[buildingID] = (comm_data.building_money[buildingID] - (trade_income + trade_tax));
                    break;
                case TransferManager.TransferReason.Petrol:
                    trade_tax = 0;
                    final_profit = pc_PrivateBuildingAI.petrol_profit * production_value;
                    if (final_profit > 0.9f)
                    {
                        final_profit = 0.9f;
                    }
                    trade_income = amountDelta * final_profit;
                    if ((comm_data.building_money[buildingID] - trade_income)> 0)
                    {
                        trade_tax = -trade_income * 0.2f;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
                    }
                    comm_data.building_money[buildingID] = (comm_data.building_money[buildingID] - (trade_income + trade_tax));
                    break;
                case TransferManager.TransferReason.Coal:
                    trade_tax = 0;
                    final_profit = pc_PrivateBuildingAI.coal_profit * production_value;
                    if (final_profit > 0.9f)
                    {
                        final_profit = 0.9f;
                    }
                    trade_income = amountDelta * final_profit;
                    if ((comm_data.building_money[buildingID] - trade_income)> 0)
                    {
                        trade_tax = -trade_income * 0.2f;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
                    }
                    comm_data.building_money[buildingID] = (comm_data.building_money[buildingID] - (trade_income + trade_tax));
                    break;
                case TransferManager.TransferReason.Goods:
                    trade_tax = 0;
                    final_profit = pc_PrivateBuildingAI.indu_profit * production_value;
                    if (final_profit > 0.9f)
                    {
                        final_profit = 0.9f;
                    }
                    trade_income = amountDelta * final_profit;
                    if ((comm_data.building_money[buildingID] - trade_income)> 0)
                    {
                        trade_tax = -trade_income * 0.2f;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
                    }
                    comm_data.building_money[buildingID] = (comm_data.building_money[buildingID] - (trade_income + trade_tax));
                    break;
            }
        }

        // IndustrialBuildingAI
        public override string GetLevelUpInfo(ushort buildingID, ref Building data, out float progress)
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
            if (this.m_info.m_class.m_level == ItemClass.Level.Level3)
            {
                progress = 0f;
                return Locale.Get("LEVELUP_WORKERS_HAPPY");
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
            int num = (int)((data.m_levelUpProgress & 15) - 1);
            int num2 = (data.m_levelUpProgress >> 4) - 1;
            if (num <= num2)
            {
                progress = (float)num * 0.06666667f;
                return Locale.Get("LEVELUP_LOWTECH");
            }
            progress = (float)num2 * 0.06666667f;
            return Locale.Get("LEVELUP_SERVICES_NEEDED");
        }
    }
}
