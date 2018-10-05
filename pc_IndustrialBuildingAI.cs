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
            if (material == GetIncomingTransferReason(buildingID) || pc_PrivateBuildingAI.is_general_industry(buildingID, data , material))
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
                comm_data.building_buffer1[buildingID] = (ushort)(customBuffer + amountDelta);
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
            }
            else
            {
                base.ModifyMaterialBuffer(buildingID, ref data, material, ref amountDelta);
            }
        }

        public void process_incoming(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            float trade_income1 = (float)amountDelta * pc_PrivateBuildingAI.get_price(false, buildingID, data, material);
            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - trade_income1;
        }

        public void caculate_trade_income(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            float trade_income1 = (float)amountDelta * pc_PrivateBuildingAI.get_price(true, buildingID, data, material);
            float trade_tax = 0;
            if ((comm_data.building_money[buildingID] - trade_income1) > 0)
            {
                trade_tax = -trade_income1 * pc_PrivateBuildingAI.get_tax_rate(data, buildingID);            
                Singleton<EconomyManager>.instance.AddPrivateIncome((int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level, 111);
            } else
            {
                trade_tax = 0;
            }
         
            comm_data.building_money[buildingID] = (comm_data.building_money[buildingID] - (trade_income1 + trade_tax));
        }
    }
}
