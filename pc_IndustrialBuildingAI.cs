using ColossalFramework;
using ColossalFramework.Math;
using UnityEngine;
using ColossalFramework.Globalization;
using System;

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


        public TransferManager.TransferReason GetIncomingTransferReason(ushort buildingID)
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
                        System.Random rand = new System.Random();
                        switch (rand.Next(4))
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

        /*private int MaxIncomingLoadSize()
        {
            return 16000;
        }

        public override void CreateBuilding(ushort buildingID, ref Building data)
        {
            base.CreateBuilding(buildingID, ref data);
            int width = data.Width;
            int length = data.Length;
            int num = this.MaxIncomingLoadSize();
            int num2 = this.CalculateProductionCapacity(new Randomizer((int)buildingID), width, length);
            float consumptionDivider = this.GetConsumptionDivider(buildingID, data);
            int num3 = Mathf.Max((int)(num2 * 500 / consumptionDivider), num * 4);
            data.m_customBuffer1 = (ushort)Singleton<SimulationManager>.instance.m_randomizer.Int32(num3 - num, num3);
            DistrictPolicies.Specialization specialization = this.SpecialPolicyNeeded();
            if (specialization != DistrictPolicies.Specialization.None)
            {
                DistrictManager instance = Singleton<DistrictManager>.instance;
                byte district = instance.GetDistrict(data.m_position);
                District[] expr_9C_cp_0 = instance.m_districts.m_buffer;
                byte expr_9C_cp_1 = district;
                expr_9C_cp_0[(int)expr_9C_cp_1].m_specializationPoliciesEffect = (expr_9C_cp_0[(int)expr_9C_cp_1].m_specializationPoliciesEffect | specialization);
            }
        }*/

        private DistrictPolicies.Specialization SpecialPolicyNeeded()
        {
            switch (this.m_info.m_class.m_subService)
            {
                case ItemClass.SubService.IndustrialForestry:
                    return DistrictPolicies.Specialization.Forest;
                case ItemClass.SubService.IndustrialFarming:
                    return DistrictPolicies.Specialization.Farming;
                case ItemClass.SubService.IndustrialOil:
                    return DistrictPolicies.Specialization.Oil;
                case ItemClass.SubService.IndustrialOre:
                    return DistrictPolicies.Specialization.Ore;
                default:
                    return DistrictPolicies.Specialization.None;
            }
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
                        switch (randomizer.Int32(10u))
                        {
                            case 0:
                            case 1:
                            case 2:
                                return TransferManager.TransferReason.Lumber;
                            case 3:
                                return TransferManager.TransferReason.Food;
                            case 4:
                            case 5:
                            case 6:
                                return TransferManager.TransferReason.Petrol;
                            case 7:
                            case 8:
                            case 9:
                                return TransferManager.TransferReason.Coal;
                            default:
                                return TransferManager.TransferReason.None;
                        }
                    }
            }
        }

        /*public static int GetConsumptionDivider(Building data)
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
                    return comm_data.ConsumptionDivider;
            }
        }*/


        private float GetConsumptionDivider(ushort buildingID, Building data)
        {
            float ConsumptionDivider = 1;
            switch (this.m_info.m_class.m_subService)
            {
                case ItemClass.SubService.IndustrialForestry:
                    ConsumptionDivider = comm_data.ConsumptionDivider1; break;
                case ItemClass.SubService.IndustrialFarming:
                    ConsumptionDivider = comm_data.ConsumptionDivider1; break;
                case ItemClass.SubService.IndustrialOil:
                    ConsumptionDivider = comm_data.ConsumptionDivider1; break;
                case ItemClass.SubService.IndustrialOre:
                    ConsumptionDivider = comm_data.ConsumptionDivider1; break;
                default:
                    ConsumptionDivider = comm_data.ConsumptionDivider; break;
            }

            Citizen.BehaviourData behaviourData = default(Citizen.BehaviourData);
            int aliveWorkerCount = 0;
            int totalWorkerCount = 0;
            base.GetWorkBehaviour(buildingID, ref data, ref behaviourData, ref aliveWorkerCount, ref totalWorkerCount);
            int width = data.Width;
            int length = data.Length;

            float work_efficiency = (float)behaviourData.m_efficiencyAccumulation / 100f;

            if ( (aliveWorkerCount /10f) > 1f)
            {
                work_efficiency = work_efficiency / 10f;
            }
            else
            {
                work_efficiency = work_efficiency / aliveWorkerCount;
            }

            float final_idex = work_efficiency;

            if (final_idex < 1f)
            {
                final_idex = 1;
            }

            ConsumptionDivider = ConsumptionDivider * final_idex;

            if (ConsumptionDivider < 1f)
            {
                ConsumptionDivider = 1f;
            }


            return ConsumptionDivider;
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

        public override void BuildingDeactivated(ushort buildingID, ref Building data)
        {
            TransferManager.TransferReason incomingTransferReason = this.GetIncomingTransferReason(buildingID);
            if (incomingTransferReason != TransferManager.TransferReason.None)
            {
                TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                offer.Building = buildingID;
                if (data.Info.m_class.m_subService == ItemClass.SubService.IndustrialGeneric)
                {
                    Singleton<TransferManager>.instance.RemoveIncomingOffer(TransferManager.TransferReason.Lumber, offer);
                    Singleton<TransferManager>.instance.RemoveIncomingOffer(TransferManager.TransferReason.Food, offer);
                    Singleton<TransferManager>.instance.RemoveIncomingOffer(TransferManager.TransferReason.Petrol, offer);
                    Singleton<TransferManager>.instance.RemoveIncomingOffer(TransferManager.TransferReason.Coal, offer);
                } else
                {
                    Singleton<TransferManager>.instance.RemoveIncomingOffer(incomingTransferReason, offer);
                }
            }
            TransferManager.TransferReason outgoingTransferReason = GetOutgoingTransferReason(data);
            if (outgoingTransferReason != TransferManager.TransferReason.None)
            {
                TransferManager.TransferOffer offer2 = default(TransferManager.TransferOffer);
                offer2.Building = buildingID;
                Singleton<TransferManager>.instance.RemoveOutgoingOffer(outgoingTransferReason, offer2);
            }
            base.BuildingDeactivated(buildingID, ref data);
        }

        public override void ModifyMaterialBuffer(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            if (material == GetIncomingTransferReason(data ,buildingID) || pc_PrivateBuildingAI.is_general_industry(buildingID, data , material))
            {
                int width = data.Width;
                int length = data.Length;
                int num = 16000;
                int num2 = CalculateProductionCapacity(data,new Randomizer((int)buildingID), width, length);
                float consumptionDivider = GetConsumptionDivider(buildingID, data);
                int num3 = Mathf.Max((int)(num2 * 500 / consumptionDivider), num * 4);
                num3 = 64000;
                int customBuffer = (int)data.m_customBuffer1;
                amountDelta = Mathf.Clamp(amountDelta, 0, num3 - customBuffer);
                process_incoming(buildingID, ref data, material, ref amountDelta);
                data.m_customBuffer1 = (ushort)(customBuffer + amountDelta);
            }
            else if (material == GetOutgoingTransferReason(data))
            {
                int customBuffer2 = (int)data.m_customBuffer2;
                amountDelta = Mathf.Clamp(amountDelta, -customBuffer2, 0);
                if (amountDelta < -8000)
                {
                    amountDelta = -8000;
                }
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
                    comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.log_import_price - (1f - pc_PrivateBuildingAI.log_import_ratio) * 0.1f * comm_data.ConsumptionDivider1 * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.lumber_index);
                    break;
                case TransferManager.TransferReason.Grain:
                    comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.grain_import_price - (1f - pc_PrivateBuildingAI.grain_import_ratio) * 0.1f * comm_data.ConsumptionDivider1 * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.food_index);
                    break;
                case TransferManager.TransferReason.Oil:
                    comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.oil_import_price - (1f - pc_PrivateBuildingAI.oil_import_ratio) * 0.1f * comm_data.ConsumptionDivider1 * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.petrol_index);
                    break;
                case TransferManager.TransferReason.Ore:
                    comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.ore_import_price - (1f - pc_PrivateBuildingAI.ore_import_ratio) * 0.1f * comm_data.ConsumptionDivider1 * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.coal_index);
                    break;
                case TransferManager.TransferReason.Lumber:
                    switch (data.Info.m_class.m_level)
                    {
                        case ItemClass.Level.Level1:
                            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.lumber_import_price - (1f - pc_PrivateBuildingAI.lumber_import_ratio) * 0.3f * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.lumber_index);
                            break;
                        case ItemClass.Level.Level2:
                            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.lumber_import_price - (0.1f - (1f - pc_PrivateBuildingAI.lumber_import_ratio) * 0.2f) * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.lumber_index);
                            break;
                        case ItemClass.Level.Level3:
                            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.lumber_import_price - (0.2f - (1f - pc_PrivateBuildingAI.lumber_import_ratio) * 0.1f) * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.lumber_index);
                            break;
                    }
                    break;
                case TransferManager.TransferReason.Coal:
                    switch (data.Info.m_class.m_level)
                    {
                        case ItemClass.Level.Level1:
                            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.coal_import_price - (1f - pc_PrivateBuildingAI.coal_import_ratio) * 0.3f * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.coal_index);
                            break;
                        case ItemClass.Level.Level2:
                            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.coal_import_price - (0.1f - (1f - pc_PrivateBuildingAI.coal_import_ratio) * 0.2f) * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.coal_index);
                            break;
                        case ItemClass.Level.Level3:
                            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.coal_import_price - (0.2f - (1f - pc_PrivateBuildingAI.coal_import_ratio) * 0.1f) * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.coal_index);
                            break;
                    }
                    break;
                case TransferManager.TransferReason.Food:
                    switch (data.Info.m_class.m_level)
                    {
                        case ItemClass.Level.Level1:
                            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.food_import_price - (1f - pc_PrivateBuildingAI.food_import_ratio) * 0.3f * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.food_index);
                            break;
                        case ItemClass.Level.Level2:
                            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.food_import_price - (0.1f - (1f - pc_PrivateBuildingAI.food_import_ratio) * 0.2f) * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.food_index);
                            break;
                        case ItemClass.Level.Level3:
                            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.food_import_price - (0.2f - (1f - pc_PrivateBuildingAI.food_import_ratio) * 0.1f) * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.food_index);
                            break;
                    }
                    break;
                case TransferManager.TransferReason.Petrol:
                    switch (data.Info.m_class.m_level)
                    {
                        case ItemClass.Level.Level1:
                            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.petrol_import_price - ((1f - pc_PrivateBuildingAI.petrol_import_ratio) * 0.3f) * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.petrol_index);
                            break;
                        case ItemClass.Level.Level2:
                            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.petrol_import_price - (0.1f - (1f - pc_PrivateBuildingAI.petrol_import_ratio) * 0.2f) * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.petrol_index);
                            break;
                        case ItemClass.Level.Level3:
                            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)amountDelta * (pc_PrivateBuildingAI.petrol_import_price - (0.2f - (1f - pc_PrivateBuildingAI.petrol_import_ratio) * 0.1f) * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.petrol_index);
                            break;
                    }
                    break;
            }
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
            }
            else if (tax_benefit < 0.5f)
            {
                tax_benefit = 0.5f;
            }
            return tax_benefit;
        }


        public void caculate_trade_income(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            float tax_benefit = caculate_tax_benefit(buildingID, ref data);
            switch (material)
            {
                case TransferManager.TransferReason.Lumber:
                    float trade_tax = 0;
                    float trade_income = (float)amountDelta * (pc_PrivateBuildingAI.lumber_export_price + (1f - pc_PrivateBuildingAI.lumber_export_ratio) * 0.1f *comm_data.ConsumptionDivider / pc_PrivateBuildingAI.lumber_index);
                    if ((comm_data.building_money[buildingID] - trade_income)> 0)
                    {
                        trade_tax = -trade_income * 0.1f * tax_benefit;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
                    }
                    comm_data.building_money[buildingID] = (comm_data.building_money[buildingID] - (trade_income + trade_tax));
                    break;
                case TransferManager.TransferReason.Food:
                    trade_tax = 0;
                    trade_income = (float)amountDelta * (pc_PrivateBuildingAI.food_export_price + (1f - pc_PrivateBuildingAI.food_export_ratio) * 0.1f * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.food_index);
                    if ((comm_data.building_money[buildingID] - trade_income)> 0)
                    {
                        trade_tax = -trade_income * 0.1f * tax_benefit;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
                    }
                    comm_data.building_money[buildingID] = (comm_data.building_money[buildingID] - (trade_income + trade_tax));
                    break;
                case TransferManager.TransferReason.Petrol:
                    trade_tax = 0;
                    trade_income = (float)amountDelta * (pc_PrivateBuildingAI.petrol_export_price + (1f - pc_PrivateBuildingAI.petrol_export_ratio) * 0.1f * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.petrol_index);
                    if ((comm_data.building_money[buildingID] - trade_income)> 0)
                    {
                        trade_tax = -trade_income * 0.1f * tax_benefit;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
                    }
                    comm_data.building_money[buildingID] = (comm_data.building_money[buildingID] - (trade_income + trade_tax));
                    break;
                case TransferManager.TransferReason.Coal:
                    trade_tax = 0;
                    trade_income = (float)amountDelta * (pc_PrivateBuildingAI.coal_export_price + (1f - pc_PrivateBuildingAI.coal_export_ratio) * 0.1f * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.coal_index);
                    if ((comm_data.building_money[buildingID] - trade_income)> 0)
                    {
                        trade_tax = -trade_income * 0.1f * tax_benefit;
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
                            trade_tax = -trade_income * 0.15f * tax_benefit; break;
                        case ItemClass.Level.Level2:
                            trade_income = (float)amountDelta * (pc_PrivateBuildingAI.good_export_price + 0.1f + (1f - pc_PrivateBuildingAI.good_export_ratio) * 0.3f) / 4f;
                            trade_tax = -trade_income * 0.20f * tax_benefit; break;
                        case ItemClass.Level.Level3:
                            trade_income = (float)amountDelta * (pc_PrivateBuildingAI.good_export_price + 0.2f + (1f - pc_PrivateBuildingAI.good_export_ratio) * 0.1f) / 4f;
                            trade_tax = -trade_income * 0.25f * tax_benefit; break;
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


        protected override void SimulationStepActive(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
        {
            DistrictManager instance = Singleton<DistrictManager>.instance;
            byte district = instance.GetDistrict(buildingData.m_position);
            DistrictPolicies.Services servicePolicies = instance.m_districts.m_buffer[(int)district].m_servicePolicies;
            DistrictPolicies.CityPlanning cityPlanningPolicies = instance.m_districts.m_buffer[(int)district].m_cityPlanningPolicies;
            District[] expr_52_cp_0 = instance.m_districts.m_buffer;
            byte expr_52_cp_1 = district;
            expr_52_cp_0[(int)expr_52_cp_1].m_servicePoliciesEffect = (expr_52_cp_0[(int)expr_52_cp_1].m_servicePoliciesEffect | (servicePolicies & (DistrictPolicies.Services.PowerSaving | DistrictPolicies.Services.WaterSaving | DistrictPolicies.Services.SmokeDetectors | DistrictPolicies.Services.Recycling | DistrictPolicies.Services.ExtraInsulation | DistrictPolicies.Services.NoElectricity | DistrictPolicies.Services.OnlyElectricity)));
            District[] expr_76_cp_0 = instance.m_districts.m_buffer;
            byte expr_76_cp_1 = district;
            expr_76_cp_0[(int)expr_76_cp_1].m_cityPlanningPoliciesEffect = (expr_76_cp_0[(int)expr_76_cp_1].m_cityPlanningPoliciesEffect | (cityPlanningPolicies & (DistrictPolicies.CityPlanning.IndustrySpace | DistrictPolicies.CityPlanning.LightningRods)));
            Citizen.BehaviourData behaviourData = default(Citizen.BehaviourData);
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = base.HandleWorkers(buildingID, ref buildingData, ref behaviourData, ref num, ref num2, ref num3);
            if ((buildingData.m_flags & Building.Flags.Evacuating) != Building.Flags.None)
            {
                num4 = 0;
            }
            if (Singleton<SimulationManager>.instance.m_isNightTime)
            {
                num4 = num4 + 1 >> 1;
            }
            TransferManager.TransferReason incomingTransferReason = this.GetIncomingTransferReason(buildingID);
            TransferManager.TransferReason outgoingTransferReason = GetOutgoingTransferReason(buildingData);
            int width = buildingData.Width;
            int length = buildingData.Length;
            int num5 = 4000;
            int num6 = 8000;
            int num7 = this.CalculateProductionCapacity(new Randomizer((int)buildingID), width, length);
            if (num7 < 9)
            {
                num7 = 9;
            }
            float consumptionDivider = this.GetConsumptionDivider(buildingID, buildingData);
            int num8 = Mathf.Max((int)(num7 * 500 / consumptionDivider), num5 * 4);
            int num9 = num7 * 500;
            int num10 = Mathf.Max(num9, num6 * 2);
            if (num4 != 0)
            {
                int num11 = num10;
                if (incomingTransferReason != TransferManager.TransferReason.None)
                {
                    num11 = Mathf.Min(num11, (int)(buildingData.m_customBuffer1 * consumptionDivider));
                }
                if (outgoingTransferReason != TransferManager.TransferReason.None)
                {
                    num11 = Mathf.Min(num11, num10 - (int)buildingData.m_customBuffer2);
                }
                num4 = Mathf.Max(0, Mathf.Min(num4, (num11 * 200 + num10 - 1) / num10));
                int num12 = (num7 * num4 + 9) / 10;
                num12 = Mathf.Max(0, Mathf.Min(num12, num11));
                if (incomingTransferReason != TransferManager.TransferReason.None)
                {
                    buildingData.m_customBuffer1 -= (ushort)((num12 + consumptionDivider - 1) / consumptionDivider);
                }
                if (outgoingTransferReason != TransferManager.TransferReason.None)
                {
                    if ((cityPlanningPolicies & DistrictPolicies.CityPlanning.IndustrySpace) != DistrictPolicies.CityPlanning.None)
                    {
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, 38, this.m_info.m_class);
                        buildingData.m_customBuffer2 = (ushort)Mathf.Min(num10, (int)buildingData.m_customBuffer2 + num12 * 2);
                    }
                    else
                    {
                        buildingData.m_customBuffer2 += (ushort)num12;
                    }
                    IndustrialBuildingAI.ProductType productType = IndustrialBuildingAI.GetProductType(outgoingTransferReason);
                    if (productType != IndustrialBuildingAI.ProductType.None)
                    {
                        StatisticsManager instance2 = Singleton<StatisticsManager>.instance;
                        StatisticBase statisticBase = instance2.Acquire<StatisticArray>(StatisticType.GoodsProduced);
                        statisticBase.Acquire<StatisticInt32>((int)productType, 5).Add(num12);
                    }
                }
                num4 = (num12 + 9) / 10;
            }
            int num13;
            int waterConsumption;
            int sewageAccumulation;
            int num14;
            int num15;
            this.GetConsumptionRates(new Randomizer((int)buildingID), num4, out num13, out waterConsumption, out sewageAccumulation, out num14, out num15);
            int heatingConsumption = 0;
            if (num13 != 0 && instance.IsPolicyLoaded(DistrictPolicies.Policies.ExtraInsulation))
            {
                if ((servicePolicies & DistrictPolicies.Services.ExtraInsulation) != DistrictPolicies.Services.None)
                {
                    heatingConsumption = Mathf.Max(1, num13 * 3 + 8 >> 4);
                    num15 = num15 * 95 / 100;
                }
                else
                {
                    heatingConsumption = Mathf.Max(1, num13 + 2 >> 2);
                }
            }
            if (num14 != 0 && (servicePolicies & DistrictPolicies.Services.Recycling) != DistrictPolicies.Services.None)
            {
                num14 = Mathf.Max(1, num14 * 85 / 100);
                num15 = num15 * 95 / 100;
            }
            if (Singleton<SimulationManager>.instance.m_isNightTime)
            {
                num15 <<= 1;
            }
            if (num4 != 0)
            {
                int num16 = base.HandleCommonConsumption(buildingID, ref buildingData, ref frameData, ref num13, ref heatingConsumption, ref waterConsumption, ref sewageAccumulation, ref num14, servicePolicies);
                num4 = (num4 * num16 + 99) / 100;
                if (num4 != 0)
                {
                    if (num15 != 0)
                    {
                        Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PrivateIncome, num15, this.m_info.m_class);
                    }
                    int num17;
                    int num18;
                    this.GetPollutionRates(num4, cityPlanningPolicies, out num17, out num18);
                    if (num17 != 0)
                    {
                        if ((cityPlanningPolicies & DistrictPolicies.CityPlanning.FilterIndustrialWaste) != DistrictPolicies.CityPlanning.None)
                        {
                            District[] expr_3BC_cp_0 = instance.m_districts.m_buffer;
                            byte expr_3BC_cp_1 = district;
                            expr_3BC_cp_0[(int)expr_3BC_cp_1].m_cityPlanningPoliciesEffect = (expr_3BC_cp_0[(int)expr_3BC_cp_1].m_cityPlanningPoliciesEffect | DistrictPolicies.CityPlanning.FilterIndustrialWaste);
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, 13, this.m_info.m_class);
                            if (Singleton<SimulationManager>.instance.m_randomizer.Int32(9u) == 0)
                            {
                                Singleton<NaturalResourceManager>.instance.TryDumpResource(NaturalResourceManager.Resource.Pollution, num17, num17, buildingData.m_position, 60f);
                            }
                        }
                        else if (Singleton<SimulationManager>.instance.m_randomizer.Int32(3u) == 0)
                        {
                            Singleton<NaturalResourceManager>.instance.TryDumpResource(NaturalResourceManager.Resource.Pollution, num17, num17, buildingData.m_position, 60f);
                        }
                    }
                    if (num18 != 0)
                    {
                        Singleton<ImmaterialResourceManager>.instance.AddResource(ImmaterialResourceManager.Resource.NoisePollution, num18, buildingData.m_position, 60f);
                    }
                    if (num16 < 100)
                    {
                        buildingData.m_flags |= Building.Flags.RateReduced;
                    }
                    else
                    {
                        buildingData.m_flags &= ~Building.Flags.RateReduced;
                    }
                    buildingData.m_flags |= Building.Flags.Active;
                }
                else
                {
                    buildingData.m_flags &= ~(Building.Flags.RateReduced | Building.Flags.Active);
                }
            }
            else
            {
                num13 = 0;
                heatingConsumption = 0;
                waterConsumption = 0;
                sewageAccumulation = 0;
                num14 = 0;
                buildingData.m_problems = Notification.RemoveProblems(buildingData.m_problems, Notification.Problem.Electricity | Notification.Problem.Water | Notification.Problem.Sewage | Notification.Problem.Flood | Notification.Problem.Heating);
                buildingData.m_flags &= ~(Building.Flags.RateReduced | Building.Flags.Active);
            }
            int num19 = 0;
            int wellbeing = 0;
            float radius = (float)(buildingData.Width + buildingData.Length) * 2.5f;
            if (behaviourData.m_healthAccumulation != 0)
            {
                if (num != 0)
                {
                    num19 = (behaviourData.m_healthAccumulation + (num >> 1)) / num;
                }
                Singleton<ImmaterialResourceManager>.instance.AddResource(ImmaterialResourceManager.Resource.Health, behaviourData.m_healthAccumulation, buildingData.m_position, radius);
            }
            if (behaviourData.m_wellbeingAccumulation != 0)
            {
                if (num != 0)
                {
                    wellbeing = (behaviourData.m_wellbeingAccumulation + (num >> 1)) / num;
                }
                Singleton<ImmaterialResourceManager>.instance.AddResource(ImmaterialResourceManager.Resource.Wellbeing, behaviourData.m_wellbeingAccumulation, buildingData.m_position, radius);
            }
            int num20 = Citizen.GetHappiness(num19, wellbeing) * 15 / 100;
            if (num3 != 0)
            {
                num20 += num * 40 / num3;
            }
            if ((buildingData.m_problems & Notification.Problem.MajorProblem) == Notification.Problem.None)
            {
                num20 += 20;
            }
            if (buildingData.m_problems == Notification.Problem.None)
            {
                num20 += 25;
            }
            int taxRate = Singleton<EconomyManager>.instance.GetTaxRate(this.m_info.m_class);
            int num21 = (int)((ItemClass.Level)9 - this.m_info.m_class.m_level);
            int num22 = (int)((ItemClass.Level)11 - this.m_info.m_class.m_level);
            if (taxRate < num21)
            {
                num20 += num21 - taxRate;
            }
            if (taxRate > num22)
            {
                num20 -= taxRate - num22;
            }
            if (taxRate >= num22 + 4)
            {
                if (buildingData.m_taxProblemTimer != 0 || Singleton<SimulationManager>.instance.m_randomizer.Int32(32u) == 0)
                {
                    int num23 = taxRate - num22 >> 2;
                    buildingData.m_taxProblemTimer = (byte)Mathf.Min(255, (int)buildingData.m_taxProblemTimer + num23);
                    if (buildingData.m_taxProblemTimer >= 96)
                    {
                        buildingData.m_problems = Notification.AddProblems(buildingData.m_problems, Notification.Problem.TaxesTooHigh | Notification.Problem.MajorProblem);
                    }
                    else if (buildingData.m_taxProblemTimer >= 32)
                    {
                        buildingData.m_problems = Notification.AddProblems(buildingData.m_problems, Notification.Problem.TaxesTooHigh);
                    }
                }
            }
            else
            {
                buildingData.m_taxProblemTimer = (byte)Mathf.Max(0, (int)(buildingData.m_taxProblemTimer - 1));
                buildingData.m_problems = Notification.RemoveProblems(buildingData.m_problems, Notification.Problem.TaxesTooHigh);
            }
            num20 = Mathf.Clamp(num20, 0, 100);
            buildingData.m_health = (byte)num19;
            buildingData.m_happiness = (byte)num20;
            buildingData.m_citizenCount = (byte)num;
            base.HandleDead(buildingID, ref buildingData, ref behaviourData, num2);
            int num24 = behaviourData.m_crimeAccumulation / 10;
            if ((servicePolicies & DistrictPolicies.Services.RecreationalUse) != DistrictPolicies.Services.None)
            {
                num24 = num24 * 3 + 3 >> 2;
            }
            base.HandleCrime(buildingID, ref buildingData, num24, num);
            int num25 = (int)buildingData.m_crimeBuffer;
            if (num != 0)
            {
                Singleton<ImmaterialResourceManager>.instance.AddResource(ImmaterialResourceManager.Resource.Density, num, buildingData.m_position, radius);
                int num26 = behaviourData.m_educated0Count * 100 + behaviourData.m_educated1Count * 50 + behaviourData.m_educated2Count * 30;
                num26 = num26 / num + 100;
                buildingData.m_fireHazard = (byte)num26;
                num25 = (num25 + (num >> 1)) / num;
            }
            else
            {
                buildingData.m_fireHazard = 0;
                num25 = 0;
            }
            int num27 = 0;
            int num28 = 0;
            int num29 = 0;
            int value = 0;
            if (incomingTransferReason != TransferManager.TransferReason.None)
            {
                base.CalculateGuestVehicles(buildingID, ref buildingData, incomingTransferReason, ref num27, ref num28, ref num29, ref value);
                buildingData.m_tempImport = (byte)Mathf.Clamp(value, (int)buildingData.m_tempImport, 255);
            }
            int num30 = 0;
            int num31 = 0;
            int num32 = 0;
            int value2 = 0;
            if (outgoingTransferReason != TransferManager.TransferReason.None)
            {
                base.CalculateOwnVehicles(buildingID, ref buildingData, outgoingTransferReason, ref num30, ref num31, ref num32, ref value2);
                buildingData.m_tempExport = (byte)Mathf.Clamp(value2, (int)buildingData.m_tempExport, 255);
            }
            SimulationManager instance3 = Singleton<SimulationManager>.instance;
            uint num33 = (instance3.m_currentFrameIndex & 3840u) >> 8;
            if ((ulong)num33 == (ulong)((long)(buildingID & 15)) && this.m_info.m_class.m_subService == ItemClass.SubService.IndustrialGeneric && Singleton<ZoneManager>.instance.m_lastBuildIndex == instance3.m_currentBuildIndex && (buildingData.m_flags & Building.Flags.Upgrading) == Building.Flags.None)
            {
                this.CheckBuildingLevel(buildingID, ref buildingData, ref frameData, ref behaviourData, num);
            }
            if ((buildingData.m_flags & (Building.Flags.Completed | Building.Flags.Upgrading)) != Building.Flags.None)
            {
                Notification.Problem problem = Notification.RemoveProblems(buildingData.m_problems, Notification.Problem.NoResources | Notification.Problem.NoPlaceforGoods);
                if ((int)buildingData.m_customBuffer2 > num10 - (num9 >> 1))
                {
                    buildingData.m_outgoingProblemTimer = (byte)Mathf.Min(255, (int)(buildingData.m_outgoingProblemTimer + 1));
                    if (buildingData.m_outgoingProblemTimer >= 192)
                    {
                        problem = Notification.AddProblems(problem, Notification.Problem.NoPlaceforGoods | Notification.Problem.MajorProblem);
                    }
                    else if (buildingData.m_outgoingProblemTimer >= 128)
                    {
                        problem = Notification.AddProblems(problem, Notification.Problem.NoPlaceforGoods);
                    }
                }
                else
                {
                    buildingData.m_outgoingProblemTimer = 0;
                }
                if (buildingData.m_customBuffer1 == 0)
                {
                    buildingData.m_incomingProblemTimer = (byte)Mathf.Min(255, (int)(buildingData.m_incomingProblemTimer + 1));
                    if (buildingData.m_incomingProblemTimer < 64)
                    {
                        problem = Notification.AddProblems(problem, Notification.Problem.NoResources);
                    }
                    else
                    {
                        problem = Notification.AddProblems(problem, Notification.Problem.NoResources | Notification.Problem.MajorProblem);
                    }
                }
                else
                {
                    buildingData.m_incomingProblemTimer = 0;
                }
                buildingData.m_problems = problem;
                instance.m_districts.m_buffer[(int)district].AddIndustrialData(ref behaviourData, num19, num20, num25, num3, num, Mathf.Max(0, num3 - num2), (int)this.m_info.m_class.m_level, num13, heatingConsumption, waterConsumption, sewageAccumulation, num14, num15, Mathf.Min(100, (int)(buildingData.m_garbageBuffer / 50)), (int)(buildingData.m_waterPollution * 100 / 255), (int)buildingData.m_finalImport, (int)buildingData.m_finalExport, this.m_info.m_class.m_subService);
                if (buildingData.m_fireIntensity == 0 && incomingTransferReason != TransferManager.TransferReason.None)
                {
                    int num34 = num8 - (int)buildingData.m_customBuffer1 - num29;
                    num34 -= num5 >> 1;
                    System.Random rand = new System.Random();
                    if (num34 >= 0 && (comm_data.building_money[buildingID] > 0))
                    {
                        TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                        offer.Priority = num34 * 8 / num5;
                        offer.Building = buildingID;
                        offer.Position = buildingData.m_position;
                        offer.Amount = 1;
                        offer.Active = false;
                        Singleton<TransferManager>.instance.AddIncomingOffer(incomingTransferReason, offer);
                    } else if ((num34 >= 0) && rand.Next(128) == 0)
                    {
                        TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                        offer.Priority = num34 * 8 / num5;
                        offer.Building = buildingID;
                        offer.Position = buildingData.m_position;
                        offer.Amount = 1;
                        offer.Active = false;
                        Singleton<TransferManager>.instance.AddIncomingOffer(incomingTransferReason, offer);
                    }
                }
                if (buildingData.m_fireIntensity == 0 && outgoingTransferReason != TransferManager.TransferReason.None)
                {
                    int num35 = Mathf.Max(1, num7 / 6);
                    int customBuffer = (int)buildingData.m_customBuffer2;
                    if (customBuffer >= num6 && num30 < (num35 + 2) )
                    {
                        TransferManager.TransferOffer offer2 = default(TransferManager.TransferOffer);
                        offer2.Priority = customBuffer * 8 / num6;
                        offer2.Building = buildingID;
                        offer2.Position = buildingData.m_position;
                        offer2.Amount = Mathf.Min(customBuffer / num6, num35 + 2 - num30);
                        offer2.Active = true;
                        Singleton<TransferManager>.instance.AddOutgoingOffer(outgoingTransferReason, offer2);
                    }
                }
                base.SimulationStepActive(buildingID, ref buildingData, ref frameData);
                base.HandleFire(buildingID, ref buildingData, ref frameData, servicePolicies);
            }
        }

        private void CheckBuildingLevel(ushort buildingID, ref Building buildingData, ref Building.Frame frameData, ref Citizen.BehaviourData behaviour, int workerCount)
        {
            int num = behaviour.m_educated1Count + behaviour.m_educated2Count * 2 + behaviour.m_educated3Count * 3;
            int averageEducation;
            ItemClass.Level level;
            int num2;
            if (workerCount != 0)
            {
                averageEducation = (num * 100 + (workerCount >> 1)) / workerCount;
                num = (num * 20 + (workerCount >> 1)) / workerCount;
                if (num < 15)
                {
                    level = ItemClass.Level.Level1;
                    num2 = 1 + num;
                }
                else if (num < 30)
                {
                    level = ItemClass.Level.Level2;
                    num2 = 1 + (num - 15);
                }
                else
                {
                    level = ItemClass.Level.Level3;
                    num2 = 1;
                }
                if (level < this.m_info.m_class.m_level)
                {
                    num2 = 1;
                }
                else if (level > this.m_info.m_class.m_level)
                {
                    num2 = 15;
                }
            }
            else
            {
                level = ItemClass.Level.Level1;
                averageEducation = 0;
                num2 = 0;
            }
            int num3 = this.CalculateServiceValue(buildingID, ref buildingData);
            ItemClass.Level level2;
            int num4;
            if (num2 != 0)
            {
                if (num3 < 30)
                {
                    level2 = ItemClass.Level.Level1;
                    num4 = 1 + (num3 * 15 + 15) / 30;
                }
                else if (num3 < 60)
                {
                    level2 = ItemClass.Level.Level2;
                    num4 = 1 + ((num3 - 30) * 15 + 15) / 30;
                }
                else
                {
                    level2 = ItemClass.Level.Level3;
                    num4 = 1;
                }
                if (level2 < this.m_info.m_class.m_level)
                {
                    num4 = 1;
                }
                else if (level2 > this.m_info.m_class.m_level)
                {
                    num4 = 15;
                }
            }
            else
            {
                level2 = ItemClass.Level.Level1;
                num4 = 0;
            }
            bool flag = false;
            if (this.m_info.m_class.m_level == ItemClass.Level.Level2)
            {
                if (num3 < 20)
                {
                    flag = true;
                }
            }
            else if (this.m_info.m_class.m_level == ItemClass.Level.Level3 && num3 < 40)
            {
                flag = true;
            }
            ItemClass.Level level3 = (ItemClass.Level)Mathf.Min((int)level, (int)level2);
            Singleton<BuildingManager>.instance.m_LevelUpWrapper.OnCalculateIndustrialLevelUp(ref level3, ref num2, ref num4, ref flag, averageEducation, num3, buildingID, this.m_info.m_class.m_service, this.m_info.m_class.m_subService, this.m_info.m_class.m_level);
            if (flag)
            {
                buildingData.m_serviceProblemTimer = (byte)Mathf.Min(255, (int)(buildingData.m_serviceProblemTimer + 1));
                if (buildingData.m_serviceProblemTimer >= 8)
                {
                    buildingData.m_problems = Notification.AddProblems(buildingData.m_problems, Notification.Problem.TooFewServices | Notification.Problem.MajorProblem);
                }
                else if (buildingData.m_serviceProblemTimer >= 4)
                {
                    buildingData.m_problems = Notification.AddProblems(buildingData.m_problems, Notification.Problem.TooFewServices);
                }
                else
                {
                    buildingData.m_problems = Notification.RemoveProblems(buildingData.m_problems, Notification.Problem.TooFewServices);
                }
            }
            else
            {
                buildingData.m_serviceProblemTimer = 0;
                buildingData.m_problems = Notification.RemoveProblems(buildingData.m_problems, Notification.Problem.TooFewServices);
            }
            if (level3 > this.m_info.m_class.m_level)
            {
                num2 = 0;
                num4 = 0;
                if (buildingData.m_problems == Notification.Problem.None && this.GetUpgradeInfo(buildingID, ref buildingData) != null && !Singleton<DisasterManager>.instance.IsEvacuating(buildingData.m_position))
                {
                    frameData.m_constructState = 0;
                    base.StartUpgrading(buildingID, ref buildingData);
                }
            }
            buildingData.m_levelUpProgress = (byte)(num2 | num4 << 4);
        }

        private int CalculateServiceValue(ushort buildingID, ref Building data)
        {
            ushort[] array;
            int num;
            Singleton<ImmaterialResourceManager>.instance.CheckLocalResources(data.m_position, out array, out num);
            int resourceRate = (int)array[num + 7];
            int resourceRate2 = (int)array[num + 2];
            int resourceRate3 = (int)array[num];
            int resourceRate4 = (int)array[num + 6];
            int resourceRate5 = (int)array[num + 1];
            int resourceRate6 = (int)array[num + 13];
            int resourceRate7 = (int)array[num + 3];
            int resourceRate8 = (int)array[num + 4];
            int resourceRate9 = (int)array[num + 5];
            int resourceRate10 = (int)array[num + 19];
            int resourceRate11 = (int)array[num + 8];
            int resourceRate12 = (int)array[num + 18];
            int resourceRate13 = (int)array[num + 20];
            int resourceRate14 = (int)array[num + 21];
            int resourceRate15 = (int)array[num + 23];
            int num2 = 0;
            num2 += ImmaterialResourceManager.CalculateResourceEffect(resourceRate, 100, 500, 50, 100) / 3;
            num2 += ImmaterialResourceManager.CalculateResourceEffect(resourceRate2, 100, 500, 50, 100) / 5;
            num2 += ImmaterialResourceManager.CalculateResourceEffect(resourceRate3, 100, 500, 50, 100) / 5;
            num2 += ImmaterialResourceManager.CalculateResourceEffect(resourceRate4, 100, 500, 50, 100) / 5;
            num2 += ImmaterialResourceManager.CalculateResourceEffect(resourceRate5, 100, 500, 50, 100) / 2;
            num2 += ImmaterialResourceManager.CalculateResourceEffect(resourceRate6, 100, 500, 50, 100) / 8;
            num2 += ImmaterialResourceManager.CalculateResourceEffect(resourceRate7, 100, 500, 50, 100) / 8;
            num2 += ImmaterialResourceManager.CalculateResourceEffect(resourceRate8, 100, 500, 50, 100) / 8;
            num2 += ImmaterialResourceManager.CalculateResourceEffect(resourceRate9, 100, 500, 50, 100) / 8;
            num2 += ImmaterialResourceManager.CalculateResourceEffect(resourceRate10, 100, 500, 50, 100);
            num2 += ImmaterialResourceManager.CalculateResourceEffect(resourceRate13, 50, 100, 80, 100) / 5;
            num2 += ImmaterialResourceManager.CalculateResourceEffect(resourceRate14, 100, 1000, 0, 100) / 5;
            num2 += ImmaterialResourceManager.CalculateResourceEffect(resourceRate15, 50, 100, 80, 100) / 5;
            num2 -= ImmaterialResourceManager.CalculateResourceEffect(resourceRate11, 100, 500, 50, 100) / 7;
            num2 -= ImmaterialResourceManager.CalculateResourceEffect(resourceRate12, 100, 500, 50, 100) / 7;
            byte resourceRate16;
            Singleton<NaturalResourceManager>.instance.CheckPollution(data.m_position, out resourceRate16);
            return num2 - ImmaterialResourceManager.CalculateResourceEffect((int)resourceRate16, 50, 255, 50, 100) / 6;
        }

    }
}
