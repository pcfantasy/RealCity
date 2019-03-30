using ColossalFramework;
using UnityEngine;
using ColossalFramework.Math;
using RealCity.Util;

namespace RealCity.CustomAI
{
    public class RealCityCargoTruckAI: CargoTruckAI
    {
        public void CargoTruckAIArriveAtTargetForRealGasStationPre(ushort vehicleID, ref Vehicle data)
        {
            DebugLog.LogToFileOnly("Error: Should be detour by RealGasStation @ CargoTruckAIArriveAtTargetForRealGasStationPre");
        }

        public void CargoTruckAIArriveAtTargetForRealGasStationPost(ushort vehicleID, ref Vehicle data)
        {
            DebugLog.LogToFileOnly("Error: Should be detour by RealGasStation @ CargoTruckAIArriveAtTargetForRealGasStationPost");
        }

        public void CargoTruckAIArriveAtTargetForRealConstruction(ushort vehicleID, ref Vehicle data)
        {
            DebugLog.LogToFileOnly("Error: Should be detour by RealConstruction @ CargoTruckAIArriveAtTargetForRealConstruction");
        }

        public void CargoTruckAISetSourceForRealConstruction(ushort vehicleID, ref Vehicle data, ushort sourceBuilding)
        {
            DebugLog.LogToFileOnly("Error: Should be detour by RealConstruction @ CargoTruckAISetSourceForRealConstruction");
        }

        private bool ArriveAtTarget(ushort vehicleID, ref Vehicle data)
        {
            // NON-STOCK CODE START
            // 112 means fuel demand, see more in RealGasStation mod
            if (data.m_transferType == 112 && Loader.isRealGasStationRunning)
            {
                CargoTruckAIArriveAtTargetForRealGasStationPre(vehicleID, ref data);
                return true;
            }
            /// NON-STOCK CODE END ///
            if (data.m_targetBuilding == 0)
            {
                return true;
            }
            int num = 0;
            if ((data.m_flags & Vehicle.Flags.TransferToTarget) != 0)
            {
                // NON-STOCK CODE START
                // RealConstruction and RealGasStation mod
                if (Loader.isRealConstructionRunning)
                {
                    CargoTruckAIArriveAtTargetForRealConstruction(vehicleID, ref data);
                }
                if (Loader.isRealGasStationRunning)
                {
                    CargoTruckAIArriveAtTargetForRealGasStationPost(vehicleID, ref data);
                }
                /// NON-STOCK CODE END ///
                num = data.m_transferSize;
            }
            if ((data.m_flags & Vehicle.Flags.TransferToSource) != 0)
            {
                num = Mathf.Min(0, data.m_transferSize - m_cargoCapacity);
            }
            BuildingManager instance = Singleton<BuildingManager>.instance;
            BuildingInfo info = instance.m_buildings.m_buffer[data.m_targetBuilding].Info;

            // NON-STOCK CODE START
            ProcessResourceArriveAtTarget(vehicleID, ref data, ref num);
            /// NON-STOCK CODE END ///

            if ((data.m_flags & Vehicle.Flags.TransferToTarget) != 0)
            {
                data.m_transferSize = (ushort)Mathf.Clamp(data.m_transferSize - num, 0, data.m_transferSize);
                if (data.m_sourceBuilding != 0)
                {
                    IndustryBuildingAI.ExchangeResource((TransferManager.TransferReason)data.m_transferType, num, data.m_sourceBuilding, data.m_targetBuilding);
                }
            }
            if ((data.m_flags & Vehicle.Flags.TransferToSource) != 0)
            {
                data.m_transferSize += (ushort)Mathf.Max(0, -num);
            }
            if (data.m_sourceBuilding != 0 && (instance.m_buildings.m_buffer[data.m_sourceBuilding].m_flags & Building.Flags.IncomingOutgoing) == Building.Flags.Outgoing)
            {
                BuildingInfo info2 = instance.m_buildings.m_buffer[data.m_sourceBuilding].Info;
                ushort num2 = instance.FindBuilding(instance.m_buildings.m_buffer[data.m_sourceBuilding].m_position, 200f, info2.m_class.m_service, ItemClass.SubService.None, Building.Flags.Incoming, Building.Flags.Outgoing);
                if (num2 != 0)
                {
                    instance.m_buildings.m_buffer[data.m_sourceBuilding].RemoveOwnVehicle(vehicleID, ref data);
                    data.m_sourceBuilding = num2;
                    instance.m_buildings.m_buffer[data.m_sourceBuilding].AddOwnVehicle(vehicleID, ref data);
                }
            }
            if ((instance.m_buildings.m_buffer[data.m_targetBuilding].m_flags & Building.Flags.IncomingOutgoing) == Building.Flags.Incoming)
            {
                ushort num3 = instance.FindBuilding(instance.m_buildings.m_buffer[data.m_targetBuilding].m_position, 200f, info.m_class.m_service, ItemClass.SubService.None, Building.Flags.Outgoing, Building.Flags.Incoming);
                if (num3 != 0)
                {
                    data.Unspawn(vehicleID);
                    BuildingInfo info3 = instance.m_buildings.m_buffer[num3].Info;
                    Randomizer randomizer = new Randomizer(vehicleID);
                    Vector3 vector;
                    Vector3 vector2;
                    info3.m_buildingAI.CalculateSpawnPosition(num3, ref instance.m_buildings.m_buffer[num3], ref randomizer, m_info, out vector, out vector2);
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
                    FrameDataUpdated(vehicleID, ref data, ref data.m_frame0);
                    SetTarget(vehicleID, ref data, 0);
                    return true;
                }
            }
            SetTarget(vehicleID, ref data, 0);
            return false;
        }

        private void ProcessResourceArriveAtTarget(ushort vehicleID, ref Vehicle data, ref int num)
        {
            BuildingManager instance = Singleton<BuildingManager>.instance;
            Building building = instance.m_buildings.m_buffer[data.m_sourceBuilding];
            Building building1 = instance.m_buildings.m_buffer[data.m_targetBuilding];
            BuildingInfo info = instance.m_buildings.m_buffer[data.m_targetBuilding].Info;
            if ((data.m_flags & Vehicle.Flags.TransferToTarget) != 0)
            {
                info.m_buildingAI.ModifyMaterialBuffer(data.m_targetBuilding, ref instance.m_buildings.m_buffer[data.m_targetBuilding], (TransferManager.TransferReason)data.m_transferType, ref num);

                if ((info.m_class.m_service == ItemClass.Service.Electricity) || (info.m_class.m_service == ItemClass.Service.Water) || (info.m_class.m_service == ItemClass.Service.Disaster))
                {
                    float product_value = 0f;
                    switch ((TransferManager.TransferReason)data.m_transferType)
                    {
                        case TransferManager.TransferReason.Petrol:
                            product_value = num * RealCityIndustryBuildingAI.GetResourcePrice((TransferManager.TransferReason)data.m_transferType);
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.ResourcePrice, (int)product_value, ItemClass.Service.PlayerIndustry, ItemClass.SubService.PlayerIndustryOil, ItemClass.Level.Level1);
                            break;
                        case TransferManager.TransferReason.Coal:
                            product_value = num * RealCityIndustryBuildingAI.GetResourcePrice((TransferManager.TransferReason)data.m_transferType);
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.ResourcePrice, (int)product_value, ItemClass.Service.PlayerIndustry, ItemClass.SubService.PlayerIndustryOre, ItemClass.Level.Level1);
                            break;
                        case TransferManager.TransferReason.Goods:
                            product_value = num * RealCityIndustryBuildingAI.GetResourcePrice((TransferManager.TransferReason)data.m_transferType);
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.ResourcePrice, (int)product_value, ItemClass.Service.PlayerIndustry, ItemClass.SubService.PlayerIndustryFarming, ItemClass.Level.Level1);
                            break;
                        case (TransferManager.TransferReason)110:
                        case (TransferManager.TransferReason)111: break;
                        default: DebugLog.LogToFileOnly("Error: ProcessResourceArriveAtTarget find unknow play building transition" + info.m_class.ToString() + "transfer reason " + data.m_transferType.ToString()); break;
                    }
                }
            }
            else
            {
                info.m_buildingAI.ModifyMaterialBuffer(data.m_targetBuilding, ref instance.m_buildings.m_buffer[data.m_targetBuilding], (TransferManager.TransferReason)data.m_transferType, ref num);
            }
        }

        public override void SetSource(ushort vehicleID, ref Vehicle data, ushort sourceBuilding)
        {
            RemoveSource(vehicleID, ref data);
            data.m_sourceBuilding = sourceBuilding;
            if (sourceBuilding != 0)
            {
                BuildingManager instance = Singleton<BuildingManager>.instance;
                BuildingInfo info = instance.m_buildings.m_buffer[sourceBuilding].Info;
                data.Unspawn(vehicleID);
                Randomizer randomizer = new Randomizer(vehicleID);
                Vector3 vector;
                Vector3 vector2;
                info.m_buildingAI.CalculateSpawnPosition(sourceBuilding, ref instance.m_buildings.m_buffer[sourceBuilding], ref randomizer, m_info, out vector, out vector2);
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
                if ((data.m_flags & Vehicle.Flags.TransferToTarget) != 0)
                {
                    int num = Mathf.Min(0, data.m_transferSize - m_cargoCapacity);
                    info.m_buildingAI.ModifyMaterialBuffer(sourceBuilding, ref instance.m_buildings.m_buffer[sourceBuilding], (TransferManager.TransferReason)data.m_transferType, ref num);
                    // NON-STOCK CODE START
                    ProcessGabargeIncome(vehicleID, ref data, num);
                    /// NON-STOCK CODE END ///
                    num = Mathf.Max(0, -num);
                    data.m_transferSize += (ushort)num;
                }
                FrameDataUpdated(vehicleID, ref data, ref data.m_frame0);
                instance.m_buildings.m_buffer[sourceBuilding].AddOwnVehicle(vehicleID, ref data);
                if ((instance.m_buildings.m_buffer[sourceBuilding].m_flags & Building.Flags.IncomingOutgoing) != Building.Flags.None)
                {
                    if ((data.m_flags & Vehicle.Flags.TransferToTarget) != 0)
                    {
                        data.m_flags |= Vehicle.Flags.Importing;
                    }
                    else if ((data.m_flags & Vehicle.Flags.TransferToSource) != 0)
                    {
                        data.m_flags |= Vehicle.Flags.Exporting;
                    }
                }
            }
        }

        private void ProcessGabargeIncome(ushort vehicleID, ref Vehicle data, int num)
        {
            BuildingManager instance = Singleton<BuildingManager>.instance;
            Building building = instance.m_buildings.m_buffer[data.m_sourceBuilding];
            if (building.Info.m_class.m_service == ItemClass.Service.Garbage)
            {
                float product_value = 0f;
                switch ((TransferManager.TransferReason)data.m_transferType)
                {
                    case TransferManager.TransferReason.Lumber:
                        product_value = -num * RealCityIndustryBuildingAI.GetResourcePrice((TransferManager.TransferReason)data.m_transferType);
                        Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.ResourcePrice,(int)product_value, ItemClass.Service.PlayerIndustry, ItemClass.SubService.PlayerIndustryForestry, ItemClass.Level.Level1);
                        break;
                    case TransferManager.TransferReason.Coal:
                        product_value = -num * RealCityIndustryBuildingAI.GetResourcePrice((TransferManager.TransferReason)data.m_transferType);
                        Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.ResourcePrice, (int)product_value, ItemClass.Service.PlayerIndustry, ItemClass.SubService.PlayerIndustryOre, ItemClass.Level.Level1);
                        break;
                    case TransferManager.TransferReason.Petrol:
                        product_value = -num * RealCityIndustryBuildingAI.GetResourcePrice((TransferManager.TransferReason)data.m_transferType);
                        Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.ResourcePrice, (int)product_value, ItemClass.Service.PlayerIndustry, ItemClass.SubService.PlayerIndustryOil, ItemClass.Level.Level1);
                        break;
                    default: DebugLog.LogToFileOnly("Error: ProcessGabargeIncome find unknow gabarge transition" + building.Info.m_class.ToString() + "transfer reason " + data.m_transferType.ToString()); break;
                }
            }
        }

        private void RemoveSource(ushort vehicleID, ref Vehicle data)
        {
            if (data.m_sourceBuilding != 0)
            {
                Singleton<BuildingManager>.instance.m_buildings.m_buffer[data.m_sourceBuilding].RemoveOwnVehicle(vehicleID, ref data);
                data.m_sourceBuilding = 0;
            }
        }

    }
}
