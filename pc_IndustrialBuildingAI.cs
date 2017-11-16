using ColossalFramework;
using ColossalFramework.Math;
using UnityEngine;
using ColossalFramework.Globalization;

namespace RealCity
{
    public class pc_IndustrialBuildingAI : PrivateBuildingAI
    {
        public static int CalculateProductionCapacity(Building data, Randomizer r, int width, int length)
        {
            ItemClass @class = data.Info.m_class;
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

        public static TransferManager.TransferReason GetIncomingTransferReason(Building data, ushort buildingID)
        {
            switch (data.Info.m_class.m_subService)
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

        public static int GetConsumptionDivider(Building data)
        {
            switch (data.Info.m_class.m_subService)
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

        public static TransferManager.TransferReason GetOutgoingTransferReason(Building data)
        {
            switch (data.Info.m_class.m_subService)
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
            if (material == GetIncomingTransferReason(data, buildingID))
            {
                int width = data.Width;
                int length = data.Length;
                int num = 4000;
                int num2 = CalculateProductionCapacity(data,new Randomizer((int)buildingID), width, length);
                int consumptionDivider = GetConsumptionDivider(data);
                int num3 = Mathf.Max(num2 * 500 / consumptionDivider, num * 4);
                int customBuffer = (int)data.m_customBuffer1;
                amountDelta = Mathf.Clamp(amountDelta, 0, num3 - customBuffer);
                process_incoming(buildingID, ref data, material, ref amountDelta);
                data.m_customBuffer1 = (ushort)(customBuffer + amountDelta);
            }
            else if (material == GetOutgoingTransferReason(data))
            {
                int customBuffer2 = (int)data.m_customBuffer2;
                amountDelta = Mathf.Clamp(amountDelta, -customBuffer2, 0);
                caculate_trade_income(buildingID, ref data, material, ref amountDelta);
                data.m_customBuffer2 = (ushort)(customBuffer2 + amountDelta);
                comm_data.building_buffer2[buildingID] = (ushort)(customBuffer2 + amountDelta);
            }
            else
            {
                base.ModifyMaterialBuffer(buildingID, ref data, material, ref amountDelta);
            }
        }

        public void process_incoming(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            switch (material)
            {
                case TransferManager.TransferReason.Logs:
                    comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.log_import_price - (1f - pc_PrivateBuildingAI.log_import_ratio) * 0.1f);
                    break;
                case TransferManager.TransferReason.Grain:
                    comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.grain_import_price - (1f - pc_PrivateBuildingAI.grain_import_ratio) * 0.1f);
                    break;
                case TransferManager.TransferReason.Oil:
                    comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.oil_import_price - (1f - pc_PrivateBuildingAI.oil_import_ratio) * 0.1f);
                    break;
                case TransferManager.TransferReason.Ore:
                    comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.ore_import_price - (1f - pc_PrivateBuildingAI.ore_import_ratio) * 0.1f);
                    break;
                case TransferManager.TransferReason.Lumber:
                    switch (data.Info.m_class.m_level)
                    {
                        case ItemClass.Level.Level1:
                            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.lumber_import_price - (1f - pc_PrivateBuildingAI.lumber_import_ratio) * 0.3f);
                            break;
                        case ItemClass.Level.Level2:
                            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.lumber_import_price - 0.1f - (1f - pc_PrivateBuildingAI.lumber_import_ratio) * 0.2f);
                            break;
                        case ItemClass.Level.Level3:
                            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.lumber_import_price - 0.2f - (1f - pc_PrivateBuildingAI.lumber_import_ratio) * 0.1f);
                            break;
                    }
                    break;
                case TransferManager.TransferReason.Coal:
                    switch (data.Info.m_class.m_level)
                    {
                        case ItemClass.Level.Level1:
                            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.coal_import_price - (1f - pc_PrivateBuildingAI.coal_import_ratio) * 0.3f);
                            break;
                        case ItemClass.Level.Level2:
                            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.coal_import_price - 0.1f - (1f - pc_PrivateBuildingAI.coal_import_ratio) * 0.2f);
                            break;
                        case ItemClass.Level.Level3:
                            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.coal_import_price - 0.2f - (1f - pc_PrivateBuildingAI.coal_import_ratio) * 0.1f);
                            break;
                    }
                    break;
                case TransferManager.TransferReason.Food:
                    switch (data.Info.m_class.m_level)
                    {
                        case ItemClass.Level.Level1:
                            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.food_import_price - (1f - pc_PrivateBuildingAI.food_import_ratio) * 0.3f);
                            break;
                        case ItemClass.Level.Level2:
                            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.food_import_price - 0.1f - (1f - pc_PrivateBuildingAI.food_import_ratio) * 0.2f);
                            break;
                        case ItemClass.Level.Level3:
                            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.food_import_price - 0.2f - (1f - pc_PrivateBuildingAI.food_import_ratio) * 0.1f);
                            break;
                    }
                    break;
                case TransferManager.TransferReason.Petrol:
                    switch (data.Info.m_class.m_level)
                    {
                        case ItemClass.Level.Level1:
                            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.petrol_import_price - (1f - pc_PrivateBuildingAI.petrol_import_ratio) * 0.3f);
                            break;
                        case ItemClass.Level.Level2:
                            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.petrol_import_price - 0.1f - (1f - pc_PrivateBuildingAI.petrol_import_ratio) * 0.2f);
                            break;
                        case ItemClass.Level.Level3:
                            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.petrol_import_price - 0.2f - (1f - pc_PrivateBuildingAI.petrol_import_ratio) * 0.1f);
                            break;
                    }
                    break;
            }
        }



        public void caculate_trade_income(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            switch (material)
            {
                case TransferManager.TransferReason.Lumber:
                    float trade_tax = 0;
                    float trade_income = (float)amountDelta * (pc_PrivateBuildingAI.lumber_export_price + (1f - pc_PrivateBuildingAI.lumber_export_ratio)*0.1f);
                    if ((comm_data.building_money[buildingID] - trade_income)> 0)
                    {
                        trade_tax = -trade_income * 0.15f;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
                    }
                    comm_data.building_money[buildingID] = (comm_data.building_money[buildingID] - (trade_income + trade_tax));
                    break;
                case TransferManager.TransferReason.Food:
                    trade_tax = 0;
                    trade_income = (float)amountDelta * (pc_PrivateBuildingAI.food_export_price + (1f - pc_PrivateBuildingAI.food_export_ratio) * 0.1f);
                    if ((comm_data.building_money[buildingID] - trade_income)> 0)
                    {
                        trade_tax = -trade_income * 0.15f;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
                    }
                    comm_data.building_money[buildingID] = (comm_data.building_money[buildingID] - (trade_income + trade_tax));
                    break;
                case TransferManager.TransferReason.Petrol:
                    trade_tax = 0;
                    trade_income = (float)amountDelta * (pc_PrivateBuildingAI.petrol_export_price + (1f - pc_PrivateBuildingAI.petrol_export_ratio) * 0.1f);
                    if ((comm_data.building_money[buildingID] - trade_income)> 0)
                    {
                        trade_tax = -trade_income * 0.15f;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
                    }
                    comm_data.building_money[buildingID] = (comm_data.building_money[buildingID] - (trade_income + trade_tax));
                    break;
                case TransferManager.TransferReason.Coal:
                    trade_tax = 0;
                    trade_income = (float)amountDelta * (pc_PrivateBuildingAI.coal_export_price + (1f - pc_PrivateBuildingAI.coal_export_ratio) * 0.1f);
                    if ((comm_data.building_money[buildingID] - trade_income)> 0)
                    {
                        trade_tax = -trade_income * 0.15f;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
                    }
                    comm_data.building_money[buildingID] = (comm_data.building_money[buildingID] - (trade_income + trade_tax));
                    break;
                case TransferManager.TransferReason.Goods:
                    trade_tax = 0;
                    switch (data.Info.m_class.m_level)
                    {
                        case ItemClass.Level.Level1:
                            trade_income = (float)amountDelta * (pc_PrivateBuildingAI.good_export_price + (1f - pc_PrivateBuildingAI.good_export_ratio) * 0.5f) / 4f;
                            trade_tax = -trade_income * 0.15f; break;
                        case ItemClass.Level.Level2:
                            trade_income = (float)amountDelta * (pc_PrivateBuildingAI.good_export_price + 0.1f + (1f - pc_PrivateBuildingAI.good_export_ratio) * 0.3f) / 4f;
                            trade_tax = -trade_income * 0.20f; break;
                        case ItemClass.Level.Level3:
                            trade_income = (float)amountDelta * (pc_PrivateBuildingAI.good_export_price + 0.2f + (1f - pc_PrivateBuildingAI.good_export_ratio) * 0.1f) / 4f;
                            trade_tax = -trade_income * 0.25f; break;
                        default:
                            trade_income = 0;break;
                    }
                    if ((comm_data.building_money[buildingID] - trade_income)> 0)
                    {
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
                    }
                    else
                    {
                        trade_tax = 0f;
                    }
                    comm_data.building_money[buildingID] = (comm_data.building_money[buildingID] - (trade_income + trade_tax));
                    break;
            }
        }

        // IndustrialBuildingAI
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
        }*/
    }
}
