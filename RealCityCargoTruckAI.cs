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
    public class RealCityCargoTruckAI: CargoTruckAI
    {
        public static float incomingTax = 0f;
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
            if (RealCityEconomyExtension.IsSpecialBuilding(data.m_targetBuilding) == 3)
            {
                switch ((TransferManager.TransferReason)data.m_transferType)
                {
                    case TransferManager.TransferReason.Food:
                        importTax1 = num * (incomingTax+ Politics.importTaxOffset) * RealCityPrivateBuildingAI.foodPrice;
                        productionValue1 = num * MainDataStore.game_expense_divide * RealCityPrivateBuildingAI.foodPrice;  // policy cost, need to x100
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, (int)productionValue1, data.Info.m_class);
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)importTax1, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialFarming, ItemClass.Level.Level3, 111);
                        MainDataStore.building_buffer3[data.m_targetBuilding] += (ushort)num; break;
                    case TransferManager.TransferReason.Lumber:
                        importTax1 = num * (incomingTax + Politics.importTaxOffset) * RealCityPrivateBuildingAI.lumberPrice;
                        productionValue1 = num * MainDataStore.game_expense_divide * RealCityPrivateBuildingAI.lumberPrice;  // policy cost, need to x100
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)importTax1, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialForestry, ItemClass.Level.Level3, 111);
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, (int)productionValue1, data.Info.m_class);
                        MainDataStore.building_buffer4[data.m_targetBuilding] += (ushort)num; break;
                    case TransferManager.TransferReason.Coal:
                        importTax1 = num * (incomingTax + Politics.importTaxOffset) * RealCityPrivateBuildingAI.coalPrice;
                        productionValue1 = num * MainDataStore.game_expense_divide * RealCityPrivateBuildingAI.coalPrice;  // policy cost, need to x100
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)importTax1, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialOil, ItemClass.Level.Level3, 111);
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, (int)productionValue1, data.Info.m_class);
                        MainDataStore.building_buffer1[data.m_targetBuilding] += (ushort)num; break;
                    case TransferManager.TransferReason.Petrol:
                        importTax1 = num * (incomingTax + Politics.importTaxOffset) * RealCityPrivateBuildingAI.petrolPrice;
                        productionValue1 = num * MainDataStore.game_expense_divide * RealCityPrivateBuildingAI.petrolPrice;  // policy cost, need to x100
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)importTax1, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialOre, ItemClass.Level.Level3, 111);
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, (int)productionValue1, data.Info.m_class);
                        MainDataStore.building_buffer2[data.m_targetBuilding] += (ushort)num; break;
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
                                    import_tax = num * (incomingTax + Politics.importTaxOffset) * RealCityPrivateBuildingAI.foodPrice;
                                    //import_tax = num * 0.25f * (pc_PrivateBuildingAI.grain_import_price - (1f - pc_PrivateBuildingAI.grain_import_ratio) * 0.1f * comm_data.ConsumptionDivider1 * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.food_index);
                                    Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                                    break;
                                case TransferManager.TransferReason.Logs:
                                    import_tax = num * (incomingTax + Politics.importTaxOffset) * RealCityPrivateBuildingAI.logPrice;
                                    //import_tax = num * 0.25f * (pc_PrivateBuildingAI.log_import_price - (1f - pc_PrivateBuildingAI.log_import_ratio) * 0.1f * comm_data.ConsumptionDivider1 * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.lumber_index);
                                    Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                                    break;
                                case TransferManager.TransferReason.Oil:
                                    import_tax = num * (incomingTax + Politics.importTaxOffset) * RealCityPrivateBuildingAI.oilPrice;
                                    //import_tax = num * 0.25f * (pc_PrivateBuildingAI.oil_import_price - (1f - pc_PrivateBuildingAI.oil_import_ratio) * 0.1f * comm_data.ConsumptionDivider1 * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.petrol_index);
                                    Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                                    break;
                                case TransferManager.TransferReason.Ore:
                                    import_tax = num * (incomingTax + Politics.importTaxOffset) * RealCityPrivateBuildingAI.orePrice;
                                    //import_tax = num * 0.25f * (pc_PrivateBuildingAI.ore_import_price - (1f - pc_PrivateBuildingAI.ore_import_ratio) * 0.1f * comm_data.ConsumptionDivider1 * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.coal_index);
                                    Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                                    break;
                                case TransferManager.TransferReason.Lumber:
                                    import_tax = num * (incomingTax + Politics.importTaxOffset) * RealCityPrivateBuildingAI.lumberPrice;
                                    //import_tax = num * 0.35f * (pc_PrivateBuildingAI.lumber_import_price - (0.2f * comm_data.ConsumptionDivider / 4f) - (1f - pc_PrivateBuildingAI.lumber_import_ratio) * 0.1f * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.lumber_index);
                                    Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                                    break;
                                case TransferManager.TransferReason.Coal:
                                    import_tax = num * (incomingTax + Politics.importTaxOffset) * RealCityPrivateBuildingAI.coalPrice;
                                    //import_tax = num * 0.35f * (pc_PrivateBuildingAI.coal_import_price - (0.2f * comm_data.ConsumptionDivider / 2.5f) - (1f - pc_PrivateBuildingAI.coal_import_ratio) * 0.1f * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.coal_index);
                                    Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                                    break;
                                case TransferManager.TransferReason.Food:
                                    import_tax = num * (incomingTax + Politics.importTaxOffset) * RealCityPrivateBuildingAI.foodPrice;
                                    //import_tax = num * 0.35f * (pc_PrivateBuildingAI.food_import_price - (0.2f * comm_data.ConsumptionDivider / 5f) - (1f - pc_PrivateBuildingAI.food_import_ratio) * 0.1f * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.food_index);
                                    Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                                    break;
                                case TransferManager.TransferReason.Petrol:
                                    import_tax = num * (incomingTax + Politics.importTaxOffset) * RealCityPrivateBuildingAI.petrolPrice;
                                    //import_tax = num * 0.35f * (pc_PrivateBuildingAI.petrol_import_price - (0.2f * comm_data.ConsumptionDivider / 2f) - (1f - pc_PrivateBuildingAI.petrol_import_ratio) * 0.1f * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.petrol_index);
                                    Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                                    break;
                                case TransferManager.TransferReason.Goods:
                                    import_tax = num * (incomingTax + Politics.importTaxOffset) * RealCityPrivateBuildingAI.goodPrice;
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
                            product_value = num * RealCityPrivateBuildingAI.petrolPrice;
                            if ((data.m_flags & Vehicle.Flags.Importing) != (Vehicle.Flags)0)
                            {
                                import_tax = product_value * (incomingTax + Politics.importTaxOffset);
                                Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialGeneric, ItemClass.Level.Level3, 111);
                            }             
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)product_value * MainDataStore.game_expense_divide, info.m_class);
                            break;
                        case TransferManager.TransferReason.Coal:
                            product_value = num * RealCityPrivateBuildingAI.coalPrice;
                            if ((data.m_flags & Vehicle.Flags.Importing) != (Vehicle.Flags)0)
                            {
                                import_tax = product_value * (incomingTax + Politics.importTaxOffset);
                                Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialGeneric, ItemClass.Level.Level3, 111);
                            }
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)product_value * MainDataStore.game_expense_divide, info.m_class);
                            break;
                        case TransferManager.TransferReason.Goods:
                            product_value = num * RealCityPrivateBuildingAI.goodPrice;
                            if ((data.m_flags & Vehicle.Flags.Importing) != (Vehicle.Flags)0)
                            {
                                import_tax = product_value * (incomingTax + Politics.importTaxOffset);
                                Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialGeneric, ItemClass.Level.Level3, 111);
                            }
                            
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)product_value * MainDataStore.game_expense_divide, info.m_class);
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
                        product_value = -num * RealCityPrivateBuildingAI.lumberPrice;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)product_value, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialForestry, ItemClass.Level.Level3, 111);
                        break;
                    case TransferManager.TransferReason.Coal:
                        product_value = -num * RealCityPrivateBuildingAI.coalPrice;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)product_value, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialOre, ItemClass.Level.Level3, 111);
                        break;
                    case TransferManager.TransferReason.Petrol:
                        product_value = -num * RealCityPrivateBuildingAI.petrolPrice;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)product_value, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialOil, ItemClass.Level.Level3, 111);
                        break;
                    default: DebugLog.LogToFileOnly("find unknow gabarge transition" + building.Info.m_class.ToString() + "transfer reason " + data.m_transferType.ToString()); break;
                }

                MainDataStore.building_money[data.m_sourceBuilding] += product_value / 100f;
            }
        }




        private bool ArriveAtSource(ushort vehicleID, ref Vehicle data)
        {
            BuildingManager instance = Singleton<BuildingManager>.instance;
            BuildingInfo info = instance.m_buildings.m_buffer[(int)data.m_targetBuilding].Info;
            BuildingInfo info1 = instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].Info;
            //new add begin
            MainDataStore.vehical_flag[vehicleID] = false;
            //new add end
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

    }
}
