using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using UnityEngine;
using System.Collections;
using System.Reflection;
using ColossalFramework.Globalization;
using ColossalFramework.Math;
using ColossalFramework.Threading;

namespace RealCity
{
    public class pc_CargoTruckAI: CargoTruckAI
    {
        public static float incomingTax = 0.2f;
        // CargoTruckAI
        private bool ArriveAtTarget(ushort vehicleID, ref Vehicle data)
        {
            if (data.m_targetBuilding == 0)
            {
                return true;
            }
            int num = 0;
            if ((data.m_flags & Vehicle.Flags.TransferToTarget) != (Vehicle.Flags)0)
            {
                num = (int)data.m_transferSize;
            }
            if ((data.m_flags & Vehicle.Flags.TransferToSource) != (Vehicle.Flags)0)
            {
                num = Mathf.Min(0, (int)data.m_transferSize - this.m_cargoCapacity);
            }
            BuildingManager instance = Singleton<BuildingManager>.instance;
            BuildingInfo info = instance.m_buildings.m_buffer[(int)data.m_targetBuilding].Info;
            BuildingInfo info1 = instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].Info;

            ProcessTradeTaxArriveAtTarget(vehicleID, ref data, ref num);

            if ((data.m_flags & Vehicle.Flags.TransferToTarget) != (Vehicle.Flags)0)
            {
                data.m_transferSize = (ushort)Mathf.Clamp((int)data.m_transferSize - num, 0, (int)data.m_transferSize);
            }
            if ((data.m_flags & Vehicle.Flags.TransferToSource) != (Vehicle.Flags)0)
            {
                data.m_transferSize += (ushort)Mathf.Max(0, -num);
            }
            if (data.m_sourceBuilding != 0 && (instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].m_flags & Building.Flags.IncomingOutgoing) == Building.Flags.Outgoing)
            {
                BuildingInfo info2 = instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].Info;
                ushort num2 = instance.FindBuilding(instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].m_position, 200f, info2.m_class.m_service, ItemClass.SubService.None, Building.Flags.Incoming, Building.Flags.Outgoing);
                if (num2 != 0)
                {
                    instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].RemoveOwnVehicle(vehicleID, ref data);
                    data.m_sourceBuilding = num2;
                    instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].AddOwnVehicle(vehicleID, ref data);
                }
            }
            if ((instance.m_buildings.m_buffer[(int)data.m_targetBuilding].m_flags & Building.Flags.IncomingOutgoing) == Building.Flags.Incoming)
            {
                ushort num3 = instance.FindBuilding(instance.m_buildings.m_buffer[(int)data.m_targetBuilding].m_position, 200f, info.m_class.m_service, ItemClass.SubService.None, Building.Flags.Outgoing, Building.Flags.Incoming);
                if (num3 != 0)
                {
                    data.Unspawn(vehicleID);
                    BuildingInfo info3 = instance.m_buildings.m_buffer[(int)num3].Info;
                    Randomizer randomizer = new Randomizer((int)vehicleID);
                    Vector3 vector;
                    Vector3 vector2;
                    info3.m_buildingAI.CalculateSpawnPosition(num3, ref instance.m_buildings.m_buffer[(int)num3], ref randomizer, this.m_info, out vector, out vector2);
                    Quaternion rotation = Quaternion.identity;
                    Vector3 forward = vector2 - vector;
                    if (forward.sqrMagnitude > 0.01f)
                    {
                        rotation = Quaternion.LookRotation(forward);
                    }
                    data.m_frame0 = new Vehicle.Frame(vector, rotation);
                    data.m_frame1 = data.m_frame0;
                    data.m_frame2 = data.m_frame0;
                    data.m_frame3 = data.m_frame0;
                    data.m_targetPos0 = vector;
                    data.m_targetPos0.w = 2f;
                    data.m_targetPos1 = vector2;
                    data.m_targetPos1.w = 2f;
                    data.m_targetPos2 = data.m_targetPos1;
                    data.m_targetPos3 = data.m_targetPos1;
                    this.FrameDataUpdated(vehicleID, ref data, ref data.m_frame0);
                    this.SetTarget(vehicleID, ref data, 0);
                    return true;
                }
            }
            this.SetTarget(vehicleID, ref data, 0);
            return false;
        }

        private void ProcessTradeTaxArriveAtTarget(ushort vehicleID, ref Vehicle data, ref int num)
        {
            BuildingManager instance = Singleton<BuildingManager>.instance;
            Building building = instance.m_buildings.m_buffer[(int)data.m_sourceBuilding];
            Building building1 = instance.m_buildings.m_buffer[(int)data.m_targetBuilding];
            BuildingInfo info = instance.m_buildings.m_buffer[(int)data.m_targetBuilding].Info;
            float importTax1 = 0f;
            float productionValue1 = 0f;
            if (RealCity.EconomyExtension.is_special_building(data.m_targetBuilding) == 3)
            {
                switch ((TransferManager.TransferReason)data.m_transferType)
                {
                    case TransferManager.TransferReason.Food:
                        importTax1 = num * incomingTax * pc_PrivateBuildingAI.foodPrice;
                        productionValue1 = num * comm_data.game_expense_divide * pc_PrivateBuildingAI.foodPrice;  // policy cost, need to x100
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, (int)productionValue1, data.Info.m_class);
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)importTax1, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialFarming, ItemClass.Level.Level3, 111);
                        comm_data.building_buffer3[data.m_targetBuilding] += (ushort)num; break;
                    case TransferManager.TransferReason.Lumber:
                        importTax1 = num * incomingTax * pc_PrivateBuildingAI.lumberPrice;
                        productionValue1 = num * comm_data.game_expense_divide * pc_PrivateBuildingAI.lumberPrice;  // policy cost, need to x100
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)importTax1, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialForestry, ItemClass.Level.Level3, 111);
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, (int)productionValue1, data.Info.m_class);
                        comm_data.building_buffer4[data.m_targetBuilding] += (ushort)num; break;
                    case TransferManager.TransferReason.Coal:
                        importTax1 = num * incomingTax * pc_PrivateBuildingAI.coalPrice;
                        productionValue1 = num * comm_data.game_expense_divide * pc_PrivateBuildingAI.coalPrice;  // policy cost, need to x100
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)importTax1, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialOil, ItemClass.Level.Level3, 111);
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, (int)productionValue1, data.Info.m_class);
                        comm_data.building_buffer1[data.m_targetBuilding] += (ushort)num; break;
                    case TransferManager.TransferReason.Petrol:
                        importTax1 = num * incomingTax * pc_PrivateBuildingAI.petrolPrice;
                        productionValue1 = num * comm_data.game_expense_divide * pc_PrivateBuildingAI.petrolPrice;  // policy cost, need to x100
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)importTax1, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialOre, ItemClass.Level.Level3, 111);
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, (int)productionValue1, data.Info.m_class);
                        comm_data.building_buffer2[data.m_targetBuilding] += (ushort)num; break;
                    default:
                        importTax1 = 0f;
                        productionValue1 = 0f;
                        DebugLog.LogToFileOnly("process_trade_tax_arrive_at_target, find a import trade size error = " + data.m_transferType.ToString());break;
                }
                //DebugLog.LogToFileOnly("process_trade_tax_arrive_at_target, find a import trade size = " + num.ToString() + "productionValue1" + productionValue1.ToString() + " " +importTax1.ToString());
                return;
            }

            float import_tax = 0f;
            if ((data.m_flags & Vehicle.Flags.TransferToTarget) != (Vehicle.Flags)0)
            {
                info.m_buildingAI.ModifyMaterialBuffer(data.m_targetBuilding, ref instance.m_buildings.m_buffer[(int)data.m_targetBuilding], (TransferManager.TransferReason)data.m_transferType, ref num);
                if (building.m_flags.IsFlagSet(Building.Flags.Untouchable))
                {
                    if (!building1.m_flags.IsFlagSet(Building.Flags.Untouchable) & !(building1.Info.m_class.m_service == ItemClass.Service.Road))
                    {
                        if (((info.m_class.m_service == ItemClass.Service.Industrial) || (info.m_class.m_service == ItemClass.Service.Commercial)) && ((data.m_flags & Vehicle.Flags.Importing) != (Vehicle.Flags)0))
                        {
                            //DebugLog.LogToFileOnly("process_trade_tax_arrive_at_target, find a import trade size = " + num.ToString());
                            switch ((TransferManager.TransferReason)data.m_transferType)
                            {
                                case TransferManager.TransferReason.Grain:
                                    import_tax = num * incomingTax * pc_PrivateBuildingAI.foodPrice;
                                    //import_tax = num * 0.25f * (pc_PrivateBuildingAI.grain_import_price - (1f - pc_PrivateBuildingAI.grain_import_ratio) * 0.1f * comm_data.ConsumptionDivider1 * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.food_index);
                                    Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                                    break;
                                case TransferManager.TransferReason.Logs:
                                    import_tax = num * incomingTax * pc_PrivateBuildingAI.logPrice;
                                    //import_tax = num * 0.25f * (pc_PrivateBuildingAI.log_import_price - (1f - pc_PrivateBuildingAI.log_import_ratio) * 0.1f * comm_data.ConsumptionDivider1 * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.lumber_index);
                                    Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                                    break;
                                case TransferManager.TransferReason.Oil:
                                    import_tax = num * incomingTax * pc_PrivateBuildingAI.oilPrice;
                                    //import_tax = num * 0.25f * (pc_PrivateBuildingAI.oil_import_price - (1f - pc_PrivateBuildingAI.oil_import_ratio) * 0.1f * comm_data.ConsumptionDivider1 * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.petrol_index);
                                    Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                                    break;
                                case TransferManager.TransferReason.Ore:
                                    import_tax = num * incomingTax * pc_PrivateBuildingAI.orePrice;
                                    //import_tax = num * 0.25f * (pc_PrivateBuildingAI.ore_import_price - (1f - pc_PrivateBuildingAI.ore_import_ratio) * 0.1f * comm_data.ConsumptionDivider1 * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.coal_index);
                                    Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                                    break;
                                case TransferManager.TransferReason.Lumber:
                                    import_tax = num * incomingTax * pc_PrivateBuildingAI.lumberPrice;
                                    //import_tax = num * 0.35f * (pc_PrivateBuildingAI.lumber_import_price - (0.2f * comm_data.ConsumptionDivider / 4f) - (1f - pc_PrivateBuildingAI.lumber_import_ratio) * 0.1f * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.lumber_index);
                                    Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                                    break;
                                case TransferManager.TransferReason.Coal:
                                    import_tax = num * incomingTax * pc_PrivateBuildingAI.coalPrice;
                                    //import_tax = num * 0.35f * (pc_PrivateBuildingAI.coal_import_price - (0.2f * comm_data.ConsumptionDivider / 2.5f) - (1f - pc_PrivateBuildingAI.coal_import_ratio) * 0.1f * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.coal_index);
                                    Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                                    break;
                                case TransferManager.TransferReason.Food:
                                    import_tax = num * incomingTax * pc_PrivateBuildingAI.foodPrice;
                                    //import_tax = num * 0.35f * (pc_PrivateBuildingAI.food_import_price - (0.2f * comm_data.ConsumptionDivider / 5f) - (1f - pc_PrivateBuildingAI.food_import_ratio) * 0.1f * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.food_index);
                                    Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                                    break;
                                case TransferManager.TransferReason.Petrol:
                                    import_tax = num * incomingTax * pc_PrivateBuildingAI.petrolPrice;
                                    //import_tax = num * 0.35f * (pc_PrivateBuildingAI.petrol_import_price - (0.2f * comm_data.ConsumptionDivider / 2f) - (1f - pc_PrivateBuildingAI.petrol_import_ratio) * 0.1f * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.petrol_index);
                                    Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                                    break;
                                case TransferManager.TransferReason.Goods:
                                    import_tax = num * incomingTax * pc_PrivateBuildingAI.goodPrice;
                                    //amountDelta * (pc_PrivateBuildingAI.good_import_price - 0.2f - 0.6f * (1f - pc_PrivateBuildingAI.good_import_ratio) - 0.1f * pc_PrivateBuildingAI.good_level2_ratio - 0.2f * pc_PrivateBuildingAI.good_level3_ratio) / 4
                                    //import_tax = num * 0.2f * ((pc_PrivateBuildingAI.good_import_price - 0.2f - 0.6f * (1f - pc_PrivateBuildingAI.good_import_ratio) - 0.1f * pc_PrivateBuildingAI.good_level2_ratio - 0.2f * pc_PrivateBuildingAI.good_level3_ratio) / pc_PrivateBuildingAI.goods_idex);
                                    Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                                    break;
                                default:
                                    DebugLog.LogToFileOnly("find unknow building " + info.m_class.ToString() + "transfer reason " + data.m_transferType.ToString());
                                    break;
                            }
                        }
                    }
                }

                if ((info.m_class.m_service == ItemClass.Service.Electricity) || (info.m_class.m_service == ItemClass.Service.Water) || (info.m_class.m_service == ItemClass.Service.Disaster))
                {
                    import_tax = 0f;
                    float product_value = 0f;
                    switch ((TransferManager.TransferReason)data.m_transferType)
                    {
                        case TransferManager.TransferReason.Petrol:
                            product_value = num * pc_PrivateBuildingAI.petrolPrice;
                            if ((data.m_flags & Vehicle.Flags.Importing) != (Vehicle.Flags)0)
                            {
                                import_tax = product_value * incomingTax;
                                Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialGeneric, ItemClass.Level.Level3, 111);
                            }             
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)product_value * comm_data.game_expense_divide, info.m_class);
                            break;
                        case TransferManager.TransferReason.Coal:
                            product_value = num * pc_PrivateBuildingAI.coalPrice;
                            if ((data.m_flags & Vehicle.Flags.Importing) != (Vehicle.Flags)0)
                            {
                                import_tax = product_value * incomingTax;
                                Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialGeneric, ItemClass.Level.Level3, 111);
                            }
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)product_value * comm_data.game_expense_divide, info.m_class);
                            break;
                        case TransferManager.TransferReason.Goods:
                            product_value = num * pc_PrivateBuildingAI.goodPrice;
                            if ((data.m_flags & Vehicle.Flags.Importing) != (Vehicle.Flags)0)
                            {
                                import_tax = product_value * incomingTax;
                                Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialGeneric, ItemClass.Level.Level3, 111);
                            }
                            
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)product_value * comm_data.game_expense_divide, info.m_class);
                            break;
                        default: DebugLog.LogToFileOnly("find unknow play building transition" + info.m_class.ToString() + "transfer reason " + data.m_transferType.ToString()); break;
                    }
                }
            }
            else
            {
                info.m_buildingAI.ModifyMaterialBuffer(data.m_targetBuilding, ref instance.m_buildings.m_buffer[(int)data.m_targetBuilding], (TransferManager.TransferReason)data.m_transferType, ref num);
            }
        }



        public override void SetSource(ushort vehicleID, ref Vehicle data, ushort sourceBuilding)
        {
            this.RemoveSource(vehicleID, ref data);
            data.m_sourceBuilding = sourceBuilding;
            if (sourceBuilding != 0)
            {
                BuildingManager instance = Singleton<BuildingManager>.instance;
                BuildingInfo info = instance.m_buildings.m_buffer[(int)sourceBuilding].Info;
                data.Unspawn(vehicleID);
                Randomizer randomizer = new Randomizer((int)vehicleID);
                Vector3 vector;
                Vector3 vector2;
                info.m_buildingAI.CalculateSpawnPosition(sourceBuilding, ref instance.m_buildings.m_buffer[(int)sourceBuilding], ref randomizer, this.m_info, out vector, out vector2);
                Quaternion rotation = Quaternion.identity;
                Vector3 forward = vector2 - vector;
                if (forward.sqrMagnitude > 0.01f)
                {
                    rotation = Quaternion.LookRotation(forward);
                }
                data.m_frame0 = new Vehicle.Frame(vector, rotation);
                data.m_frame1 = data.m_frame0;
                data.m_frame2 = data.m_frame0;
                data.m_frame3 = data.m_frame0;
                data.m_targetPos0 = vector;
                data.m_targetPos0.w = 2f;
                data.m_targetPos1 = vector2;
                data.m_targetPos1.w = 2f;
                data.m_targetPos2 = data.m_targetPos1;
                data.m_targetPos3 = data.m_targetPos1;
                if ((data.m_flags & Vehicle.Flags.TransferToTarget) != (Vehicle.Flags)0)
                {
                    int num = Mathf.Min(0, (int)data.m_transferSize - this.m_cargoCapacity);
                    info.m_buildingAI.ModifyMaterialBuffer(sourceBuilding, ref instance.m_buildings.m_buffer[(int)sourceBuilding], (TransferManager.TransferReason)data.m_transferType, ref num);
                    ProcessGabargeIncome(vehicleID, ref data, num);
                    num = Mathf.Max(0, -num);
                    data.m_transferSize += (ushort)num;
                }
                this.FrameDataUpdated(vehicleID, ref data, ref data.m_frame0);
                instance.m_buildings.m_buffer[(int)sourceBuilding].AddOwnVehicle(vehicleID, ref data);
                if ((instance.m_buildings.m_buffer[(int)sourceBuilding].m_flags & Building.Flags.IncomingOutgoing) != Building.Flags.None)
                {
                    if ((data.m_flags & Vehicle.Flags.TransferToTarget) != (Vehicle.Flags)0)
                    {
                        data.m_flags |= Vehicle.Flags.Importing;
                    }
                    else if ((data.m_flags & Vehicle.Flags.TransferToSource) != (Vehicle.Flags)0)
                    {
                        data.m_flags |= Vehicle.Flags.Exporting;
                    }
                }
            }
        }

        private void ProcessGabargeIncome(ushort vehicleID, ref Vehicle data, int num)
        {
            BuildingManager instance = Singleton<BuildingManager>.instance;
            Building building = instance.m_buildings.m_buffer[(int)data.m_sourceBuilding];
            if (building.Info.m_class.m_service == ItemClass.Service.Garbage)
            {
                float product_value = 0f;
                switch ((TransferManager.TransferReason)data.m_transferType)
                {
                    case TransferManager.TransferReason.Lumber:
                        product_value = -num * pc_PrivateBuildingAI.lumberPrice;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)product_value, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialForestry, ItemClass.Level.Level3, 111);
                        break;
                    case TransferManager.TransferReason.Coal:
                        product_value = -num * pc_PrivateBuildingAI.coalPrice;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)product_value, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialOre, ItemClass.Level.Level3, 111);
                        break;
                    case TransferManager.TransferReason.Petrol:
                        product_value = -num * pc_PrivateBuildingAI.petrolPrice;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)product_value, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialOil, ItemClass.Level.Level3, 111);
                        break;
                    default: DebugLog.LogToFileOnly("find unknow gabarge transition" + building.Info.m_class.ToString() + "transfer reason " + data.m_transferType.ToString()); break;
                }

                comm_data.building_money[data.m_sourceBuilding] += product_value / 100f;
            }
        }




        private bool ArriveAtSource(ushort vehicleID, ref Vehicle data)
        {
            BuildingManager instance = Singleton<BuildingManager>.instance;
            BuildingInfo info = instance.m_buildings.m_buffer[(int)data.m_targetBuilding].Info;
            BuildingInfo info1 = instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].Info;
            comm_data.vehical_flag[vehicleID] = false;
            if (data.m_sourceBuilding == 0)
            {
                Singleton<VehicleManager>.instance.ReleaseVehicle(vehicleID);
                return true;
            }
            int num = 0;
            if ((data.m_flags & Vehicle.Flags.TransferToSource) != (Vehicle.Flags)0)
            {
                num = (int)data.m_transferSize;
                info = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].Info;
                info.m_buildingAI.ModifyMaterialBuffer(data.m_sourceBuilding, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_sourceBuilding], (TransferManager.TransferReason)data.m_transferType, ref num);
                data.m_transferSize = (ushort)Mathf.Clamp((int)data.m_transferSize - num, 0, (int)data.m_transferSize);
            }
            this.RemoveSource(vehicleID, ref data);
            Singleton<VehicleManager>.instance.ReleaseVehicle(vehicleID);
            return true;
        }

        private void RemoveSource(ushort vehicleID, ref Vehicle data)
        {
            if (data.m_sourceBuilding != 0)
            {
                Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].RemoveOwnVehicle(vehicleID, ref data);
                data.m_sourceBuilding = 0;
            }
        }


        /*public override void SetTarget(ushort vehicleID, ref Vehicle data, ushort targetBuilding)
        {
            if (targetBuilding == data.m_targetBuilding)
            {
                if (data.m_path == 0u)
                {
                    if (!this.StartPathFind(vehicleID, ref data))
                    {
                        data.Unspawn(vehicleID);
                    }
                }
                else
                {
                    DebugLog.LogToFileOnly("find data.m_path !=0  source_building adn targetbuilding is " + Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].Info.m_class.ToString() + Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)targetBuilding].Info.m_class.ToString());
                    this.TrySpawn(vehicleID, ref data);
                }
            }
            else
            {
                this.RemoveTarget(vehicleID, ref data);
                data.m_targetBuilding = targetBuilding;
                data.m_flags &= ~Vehicle.Flags.WaitingTarget;
                data.m_waitCounter = 0;
                if (targetBuilding != 0)
                {
                    Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)targetBuilding].AddGuestVehicle(vehicleID, ref data);
                    if ((Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)targetBuilding].m_flags & Building.Flags.IncomingOutgoing) != Building.Flags.None)
                    {
                        if ((data.m_flags & Vehicle.Flags.TransferToTarget) != (Vehicle.Flags)0)
                        {
                            data.m_flags |= Vehicle.Flags.Exporting;
                        }
                        else if ((data.m_flags & Vehicle.Flags.TransferToSource) != (Vehicle.Flags)0)
                        {
                            data.m_flags |= Vehicle.Flags.Importing;
                        }
                    }
                }
                else
                {
                    if ((data.m_flags & Vehicle.Flags.TransferToTarget) != (Vehicle.Flags)0)
                    {
                        if (data.m_transferSize > 0)
                        {
                            TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                            offer.Priority = 7;
                            offer.Vehicle = vehicleID;
                            if (data.m_sourceBuilding != 0)
                            {
                                offer.Position = (data.GetLastFramePosition() + Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].m_position) * 0.5f;
                            }
                            else
                            {
                                offer.Position = data.GetLastFramePosition();
                            }
                            offer.Amount = 1;
                            offer.Active = true;
                            Singleton<TransferManager>.instance.AddOutgoingOffer((TransferManager.TransferReason)data.m_transferType, offer);
                            data.m_flags |= Vehicle.Flags.WaitingTarget;
                        }
                        else
                        {
                            data.m_flags |= Vehicle.Flags.GoingBack;
                        }
                    }
                    if ((data.m_flags & Vehicle.Flags.TransferToSource) != (Vehicle.Flags)0)
                    {
                        if ((int)data.m_transferSize < this.m_cargoCapacity)
                        {
                            TransferManager.TransferOffer offer2 = default(TransferManager.TransferOffer);
                            offer2.Priority = 7;
                            offer2.Vehicle = vehicleID;
                            if (data.m_sourceBuilding != 0)
                            {
                                offer2.Position = (data.GetLastFramePosition() + Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].m_position) * 0.5f;
                            }
                            else
                            {
                                offer2.Position = data.GetLastFramePosition();
                            }
                            offer2.Amount = 1;
                            offer2.Active = true;
                            Singleton<TransferManager>.instance.AddIncomingOffer((TransferManager.TransferReason)data.m_transferType, offer2);
                            data.m_flags |= Vehicle.Flags.WaitingTarget;
                        }
                        else
                        {
                            data.m_flags |= Vehicle.Flags.GoingBack;
                        }
                    }
                }
                if (data.m_cargoParent != 0)
                {
                    if (data.m_path != 0u)
                    {
                        if (data.m_path != 0u)
                        {
                            Singleton<PathManager>.instance.ReleasePath(data.m_path);
                        }
                        data.m_path = 0u;
                    }
                }
                else if (!this.StartPathFind(vehicleID, ref data))
                {
                    data.Unspawn(vehicleID);
                }
            }
        }*/

        /*private void RemoveTarget(ushort vehicleID, ref Vehicle data)
        {
            if (data.m_targetBuilding != 0)
            {
                Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_targetBuilding].RemoveGuestVehicle(vehicleID, ref data);
                data.m_targetBuilding = 0;
            }
        }*/
    }
}
