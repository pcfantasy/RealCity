using ColossalFramework;
using ColossalFramework.Math;
using UnityEngine;
using ColossalFramework.Globalization;
using System;

namespace RealCity
{
    public class pc_IndustrialBuildingAI : PrivateBuildingAI
    {
        public override void CreateBuilding(ushort buildingID, ref Building data)
        {
            base.CreateBuilding(buildingID, ref data);
            int width = data.Width;
            int length = data.Length;
            int num = 4000;
            int num2 = this.CalculateProductionCapacity(new Randomizer((int)buildingID), width, length);
            int consumptionDivider = GetConsumptionDivider(buildingID, data);
            int num3 = Mathf.Max(num2 * 500 / consumptionDivider, num * 4);
            data.m_customBuffer1 = 8000;
            comm_data.building_money[buildingID] -= 8000* pc_PrivateBuildingAI.GetPrice(false, buildingID, data);
            System.Random rand = new System.Random();
            if (rand.Next(100) < Politics.stateOwnedPercent)
            {
                comm_data.buildingFlag[buildingID] = true;
            }
            DistrictPolicies.Specialization specialization = this.SpecialPolicyNeeded();
            if (specialization != DistrictPolicies.Specialization.None)
            {
                DistrictManager instance = Singleton<DistrictManager>.instance;
                byte district = instance.GetDistrict(data.m_position);
                District[] expr_9C_cp_0 = instance.m_districts.m_buffer;
                byte expr_9C_cp_1 = district;
                expr_9C_cp_0[(int)expr_9C_cp_1].m_specializationPoliciesEffect = (expr_9C_cp_0[(int)expr_9C_cp_1].m_specializationPoliciesEffect | specialization);
            }
        }

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
            //DebugLog.LogToFileOnly("industrial building GetIncomingTransferReason called");
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
                        switch (comm_data.building_buffer4[buildingID])
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
                                DebugLog.LogToFileOnly("Error: should be 0-3 for industrial gen building");
                                return TransferManager.TransferReason.None;
                        }
                    }
            }
        }

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


        private int GetConsumptionDivider(ushort buildingID, Building data)
        {
            int ConsumptionDivider;
            switch (this.m_info.m_class.m_subService)
            {
                case ItemClass.SubService.IndustrialForestry:
                    ConsumptionDivider = 1; break;
                case ItemClass.SubService.IndustrialFarming:
                    ConsumptionDivider = 1; break;
                case ItemClass.SubService.IndustrialOil:
                    ConsumptionDivider = 1; break;
                case ItemClass.SubService.IndustrialOre:
                    ConsumptionDivider = 1; break;
                default:
                    ConsumptionDivider = 4; break;
            }
            return ConsumptionDivider;
        }

        public static TransferManager.TransferReason GetOutgoingTransferReason(Building data)
        {
            //DebugLog.LogToFileOnly("industrial building GetoutgoingTransferReason called");
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
            if (material == GetIncomingTransferReason(buildingID) || pc_PrivateBuildingAI.IsGeneralIndustry(buildingID, data, material))
            {
                int width = data.Width;
                int length = data.Length;
                int num = 16000;
                int num2 = CalculateProductionCapacity(data, new Randomizer((int)buildingID), width, length);
                float consumptionDivider = GetConsumptionDivider(buildingID, data);
                int num3 = Mathf.Max((int)(num2 * 500 / consumptionDivider), num * 4);
                num3 = 64000;
                int customBuffer = (int)data.m_customBuffer1;
                amountDelta = Mathf.Clamp(amountDelta, 0, num3 - customBuffer);
                process_incoming(buildingID, ref data, material, ref amountDelta);
                data.m_customBuffer1 = (ushort)(customBuffer + amountDelta);
                comm_data.building_buffer1[buildingID] = data.m_customBuffer1;

                if (data.Info.m_class.m_subService == ItemClass.SubService.IndustrialGeneric)
                {
                    comm_data.building_buffer4[buildingID]++;
                    if (comm_data.building_buffer4[buildingID] > 3)
                    {
                        comm_data.building_buffer4[buildingID] = 0;
                    }
                    if (material == TransferManager.TransferReason.Petrol)
                    {
                        //DebugLog.LogToFileOnly("find speical incoming request for comm building");
                        comm_data.building_buffer3[buildingID] = 123;  //a flag
                    }
                    else if (material == TransferManager.TransferReason.Food)
                    {
                        comm_data.building_buffer3[buildingID] = 124;
                    }
                    else if (material == TransferManager.TransferReason.Lumber)
                    {
                        comm_data.building_buffer3[buildingID] = 125;
                    }
                    else if (material == TransferManager.TransferReason.Coal)
                    {
                        comm_data.building_buffer3[buildingID] = 126;
                    }
                    else
                    {
                        DebugLog.LogToFileOnly("find speical incoming request for indus general building" + material.ToString());
                    }
                }
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
                comm_data.building_buffer2[buildingID] = data.m_customBuffer2;
            }
            else
            {
                base.ModifyMaterialBuffer(buildingID, ref data, material, ref amountDelta);
            }
        }

        public void process_incoming(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            float trade_income1 = (float)amountDelta * pc_PrivateBuildingAI.GetPrice(false, buildingID, data);
            if (!comm_data.buildingFlag[buildingID])
            {
                comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - trade_income1;
            }
            else
            {
                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, (int)(trade_income1 * comm_data.game_expense_divide), Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingID].Info.m_class);
            }
        }

        public void caculate_trade_income(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            float trade_income1 = (float)amountDelta * pc_PrivateBuildingAI.GetPrice(true, buildingID, data);
            float trade_tax = 0;
            trade_tax = -trade_income1 * pc_PrivateBuildingAI.GetTaxRate(data, buildingID);
            if (!comm_data.buildingFlag[buildingID])
            {
                Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
                comm_data.building_money[buildingID] = (comm_data.building_money[buildingID] - (trade_income1 + trade_tax));
            }
            else
            {
                Singleton<EconomyManager>.instance.AddPrivateIncome((int)-trade_income1, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
            }
         
            
        }
    }
}
