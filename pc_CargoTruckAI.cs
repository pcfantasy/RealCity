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

            process_trade_tax_arrive_at_target(vehicleID, ref data, ref num);

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

        private void process_trade_tax_arrive_at_target(ushort vehicleID, ref Vehicle data, ref int num)
        {
            BuildingManager instance = Singleton<BuildingManager>.instance;
            Building building = instance.m_buildings.m_buffer[(int)data.m_sourceBuilding];
            Building building1 = instance.m_buildings.m_buffer[(int)data.m_targetBuilding];
            BuildingInfo info = instance.m_buildings.m_buffer[(int)data.m_targetBuilding].Info;
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
                                    import_tax = num * 0.6f * (pc_PrivateBuildingAI.grain_import_price - (1f - pc_PrivateBuildingAI.grain_import_ratio) * 0.1f * comm_data.ConsumptionDivider1 * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.food_index);
                                    //Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                                    break;
                                case TransferManager.TransferReason.Logs:
                                    import_tax = num * 0.7f * (pc_PrivateBuildingAI.log_import_price - (1f - pc_PrivateBuildingAI.log_import_ratio) * 0.1f * comm_data.ConsumptionDivider1 * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.lumber_index);
                                    //Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                                    break;
                                case TransferManager.TransferReason.Oil:
                                    import_tax = num * 0.85f * (pc_PrivateBuildingAI.oil_import_price - (1f - pc_PrivateBuildingAI.oil_import_ratio) * 0.1f * comm_data.ConsumptionDivider1 * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.petrol_index);
                                    //Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                                    break;
                                case TransferManager.TransferReason.Ore:
                                    import_tax = num * 0.8f * (pc_PrivateBuildingAI.ore_import_price - (1f - pc_PrivateBuildingAI.ore_import_ratio) * 0.1f * comm_data.ConsumptionDivider1 * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.coal_index);
                                    //Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                                    break;
                                case TransferManager.TransferReason.Lumber:
                                    import_tax = num * 0.4f * (pc_PrivateBuildingAI.lumber_import_price - (0.2f * comm_data.ConsumptionDivider / 4f) - (1f - pc_PrivateBuildingAI.lumber_import_ratio) * 0.1f * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.lumber_index);
                                    //Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                                    break;
                                case TransferManager.TransferReason.Coal:
                                    import_tax = num * 0.6f * (pc_PrivateBuildingAI.coal_import_price - (0.2f * comm_data.ConsumptionDivider / 2.5f) - (1f - pc_PrivateBuildingAI.coal_import_ratio) * 0.1f * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.coal_index);
                                    //Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                                    break;
                                case TransferManager.TransferReason.Food:
                                    import_tax = num * 0.3f * (pc_PrivateBuildingAI.food_import_price - (0.2f * comm_data.ConsumptionDivider / 5f) - (1f - pc_PrivateBuildingAI.food_import_ratio) * 0.1f * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.food_index);
                                    //Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                                    break;
                                case TransferManager.TransferReason.Petrol:
                                    import_tax = num * 0.7f * (pc_PrivateBuildingAI.petrol_import_price - (0.2f * comm_data.ConsumptionDivider / 2f) - (1f - pc_PrivateBuildingAI.petrol_import_ratio) * 0.1f * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.petrol_index);
                                    //Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                                    break;
                                case TransferManager.TransferReason.Goods:
                                    //amountDelta * (pc_PrivateBuildingAI.good_import_price - 0.2f - 0.6f * (1f - pc_PrivateBuildingAI.good_import_ratio) - 0.1f * pc_PrivateBuildingAI.good_level2_ratio - 0.2f * pc_PrivateBuildingAI.good_level3_ratio) / 4
                                    import_tax = num * 0.3f * ((pc_PrivateBuildingAI.good_import_price - 0.2f - 0.6f * (1f - pc_PrivateBuildingAI.good_import_ratio) - 0.1f * pc_PrivateBuildingAI.good_level2_ratio - 0.2f * pc_PrivateBuildingAI.good_level3_ratio) / pc_PrivateBuildingAI.goods_idex);
                                    //Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
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
                            product_value = num * (pc_PrivateBuildingAI.petrol_import_price- (0.2f * comm_data.ConsumptionDivider / 2f) - (1f - pc_PrivateBuildingAI.petrol_import_ratio) * 0.1f * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.petrol_index);
                            if ((data.m_flags & Vehicle.Flags.Importing) != (Vehicle.Flags)0)
                            {
                                import_tax = product_value * 0.7f;
                                //Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialGeneric, ItemClass.Level.Level3, 111);
                            }              
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)product_value, info.m_class);
                            break;
                        case TransferManager.TransferReason.Coal:
                            product_value = num * (pc_PrivateBuildingAI.coal_import_price - (0.2f * comm_data.ConsumptionDivider / 2.5f) - (1f - pc_PrivateBuildingAI.coal_import_ratio) * 0.1f * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.coal_index);
                            if ((data.m_flags & Vehicle.Flags.Importing) != (Vehicle.Flags)0)
                            {
                                import_tax = product_value * 0.6f;
                                //Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialGeneric, ItemClass.Level.Level3, 111);
                            }
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)product_value, info.m_class);
                            break;
                        case TransferManager.TransferReason.Goods:
                            product_value = num * ((pc_PrivateBuildingAI.good_import_price - 0.2f - 0.6f * (1f - pc_PrivateBuildingAI.good_import_ratio) - 0.1f * pc_PrivateBuildingAI.good_level2_ratio - 0.2f * pc_PrivateBuildingAI.good_level3_ratio) / pc_PrivateBuildingAI.goods_idex);
                            if ((data.m_flags & Vehicle.Flags.Importing) != (Vehicle.Flags)0)
                            {
                                import_tax = product_value * 0.3f;
                                //Singleton<EconomyManager>.instance.AddPrivateIncome((int)import_tax, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialGeneric, ItemClass.Level.Level3, 111);
                            }
                            
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)product_value, info.m_class);
                            break;
                        default: DebugLog.LogToFileOnly("find unknow play building transition" + info.m_class.ToString() + "transfer reason " + data.m_transferType.ToString()); break;
                    }
                }
                //if ((building.m_flags.IsFlagSet(Building.Flags.Untouchable)) || (building1.m_flags.IsFlagSet(Building.Flags.Untouchable)))
                //{
                    //if (pc_OutsideConnectionAI.have_maintain_road_building)
                    //{
                        //DebugLog.LogToFileOnly("add road condition");
                        //Singleton<EconomyManager>.instance.AddPrivateIncome(100, ItemClass.Service.Road, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                        //if (((building1.m_flags & Building.Flags.IncomingOutgoing) == Building.Flags.Incoming) && (building1.Info.m_class.m_service == ItemClass.Service.Road))
                        //{
                            //DebugLog.LogToFileOnly("add road condition1");
                            //building1.m_waterBuffer += 10;
                            //DebugLog.LogToFileOnly("add road condition2 building1.m_waterBuffer = " + building1.m_waterBuffer.ToString() + "building ID = " + data.m_targetBuilding.ToString());
                            //if (building1.m_waterBuffer > 65000)
                            //{
                                //building1.m_waterBuffer = 65000;
                            //}
                        //}
                    //}
                //}
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
                    process_gabarge_income(vehicleID, ref data, num);
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

        private void process_gabarge_income(ushort vehicleID, ref Vehicle data, int num)
        {
            BuildingManager instance = Singleton<BuildingManager>.instance;
            Building building = instance.m_buildings.m_buffer[(int)data.m_sourceBuilding];
            if (building.Info.m_class.m_service == ItemClass.Service.Garbage)
            {
                float product_value = 0f;
                switch ((TransferManager.TransferReason)data.m_transferType)
                {
                    case TransferManager.TransferReason.Lumber:
                        product_value = -num * (pc_PrivateBuildingAI.lumber_import_price - (1f - pc_PrivateBuildingAI.lumber_import_ratio) * 0.1f * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.lumber_index);
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)product_value, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialForestry, ItemClass.Level.Level3, 111);
                        break;
                    case TransferManager.TransferReason.Coal:
                        product_value = -num * (pc_PrivateBuildingAI.coal_import_price - (1f - pc_PrivateBuildingAI.coal_import_ratio) * 0.1f * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.coal_index);
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)product_value, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialOre, ItemClass.Level.Level3, 111);
                        break;
                    case TransferManager.TransferReason.Petrol:
                        product_value = -num * (pc_PrivateBuildingAI.petrol_import_price - (1f - pc_PrivateBuildingAI.petrol_import_ratio) * 0.1f * comm_data.ConsumptionDivider / pc_PrivateBuildingAI.petrol_index);
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)product_value, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialOil, ItemClass.Level.Level3, 111);
                        break;
                    default: DebugLog.LogToFileOnly("find unknow gabarge transition" + building.Info.m_class.ToString() + "transfer reason " + data.m_transferType.ToString()); break;
                }
            }
        }




        /*        private bool ArriveAtSource(ushort vehicleID, ref Vehicle data)
                {
                    BuildingManager instance = Singleton<BuildingManager>.instance;
                    BuildingInfo info = instance.m_buildings.m_buffer[(int)data.m_targetBuilding].Info;
                    BuildingInfo info1 = instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].Info;
                    if ((!instance.m_buildings.m_buffer[(int)data.m_targetBuilding].m_flags.IsFlagSet(Building.Flags.Untouchable)) && (!instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].m_flags.IsFlagSet(Building.Flags.Untouchable)))
                    {
                        DebugLog.LogToFileOnly("process_trade_tax_arrive_at_source, going in");
                        DebugLog.LogToFileOnly("we enter here, target building is " + info.m_class.ToString());
                        DebugLog.LogToFileOnly("we enter here, source building is " + info1.m_class.ToString());
                    }
                    if (data.m_sourceBuilding == 0)
                    {
                        Singleton<VehicleManager>.instance.ReleaseVehicle(vehicleID);
                        return true;
                    }
                    int num = 0;
                    if ((data.m_flags & Vehicle.Flags.TransferToSource) != (Vehicle.Flags)0)
                    {
                        //process_trade_tax_arrive_at_source(vehicleID, ref data, num);
                        num = (int)data.m_transferSize;
                        info = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].Info;
                        info.m_buildingAI.ModifyMaterialBuffer(data.m_sourceBuilding, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_sourceBuilding], (TransferManager.TransferReason)data.m_transferType, ref num);
                        data.m_transferSize = (ushort)Mathf.Clamp((int)data.m_transferSize - num, 0, (int)data.m_transferSize);
                    }
                    this.RemoveSource(vehicleID, ref data);
                    Singleton<VehicleManager>.instance.ReleaseVehicle(vehicleID);
                    return true;
                }

                private void process_trade_tax_arrive_at_source(ushort vehicleID, ref Vehicle data, int num)
                {
                    num = (int)data.m_transferSize;
                    BuildingInfo info = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].Info;
                    Building building = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_targetBuilding];
                    Building building1 = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_sourceBuilding];
                    info.m_buildingAI.ModifyMaterialBuffer(data.m_sourceBuilding, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_sourceBuilding], (TransferManager.TransferReason)data.m_transferType, ref num);
                    if (building.m_flags.IsFlagSet(Building.Flags.Untouchable))
                    {
                        if (!building1.m_flags.IsFlagSet(Building.Flags.Untouchable) & !(building1.Info.m_class.m_service == ItemClass.Service.Road))
                        {
                            if ((info.m_class.m_service == ItemClass.Service.Industrial) || (info.m_class.m_service == ItemClass.Service.Commercial))
                            {
                                DebugLog.LogToFileOnly("process_trade_tax_arrive_at_source, find a import trade size = " + num.ToString());
                                Singleton<EconomyManager>.instance.AddPrivateIncome((int)(num * 0.03), info.m_class.m_service, info.m_class.m_subService, info.m_class.m_level, 111);
                            }
                        }
                    }
                }*/

                private void RemoveSource(ushort vehicleID, ref Vehicle data)
                {
                    if (data.m_sourceBuilding != 0)
                    {
                        Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].RemoveOwnVehicle(vehicleID, ref data);
                        data.m_sourceBuilding = 0;
                    }
                }


        public override void SetTarget(ushort vehicleID, ref Vehicle data, ushort targetBuilding)
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
        }

        private void RemoveTarget(ushort vehicleID, ref Vehicle data)
        {
            if (data.m_targetBuilding != 0)
            {
                Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_targetBuilding].RemoveGuestVehicle(vehicleID, ref data);
                data.m_targetBuilding = 0;
            }
        }
    }
}
