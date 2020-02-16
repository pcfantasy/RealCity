using ColossalFramework;
using Harmony;
using RealCity.CustomAI;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class CargoTruckAIReleaseVehiclePatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(CargoTruckAI).GetMethod("ReleaseVehicle", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
        }
        public static void Prefix(ushort vehicleID, ref Vehicle data)
        {
            if ((data.m_flags & Vehicle.Flags.TransferToTarget) != 0 && data.m_sourceBuilding != 0 && data.m_transferSize != 0)
            {
                BuildingManager instance = Singleton<BuildingManager>.instance;
                BuildingInfo info = instance.m_buildings.m_buffer[data.m_sourceBuilding].Info;
                if ((object)info != null)
                {
                    int amountDelta = data.m_transferSize;
                    //revert income
                    Building building = instance.m_buildings.m_buffer[data.m_sourceBuilding];
                    if (building.Info.m_class.m_service == ItemClass.Service.Garbage)
                    {
                        RevertGabargeIncome(vehicleID, ref data, amountDelta);
                    }
                    else if (building.Info.m_class.m_service == ItemClass.Service.Industrial)
                    {
                        RevertTradeIncome(data.m_sourceBuilding, ref building, (TransferManager.TransferReason)data.m_transferType, ref amountDelta);
                    }
                }
            }
        }

        public static void RevertTradeIncome(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            amountDelta = -amountDelta;
            float trade_income1 = amountDelta * RealCityPrivateBuildingAI.GetPrice(true, buildingID, data);
            float trade_tax = 0;
            trade_tax = -trade_income1 * RealCityPrivateBuildingAI.GetTaxRate(data, buildingID) / 100f;
            MainDataStore.unfinishedTransitionLost += (int)(trade_tax / 100f);
            Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)17, (int)trade_tax, ItemClass.Service.Industrial, data.Info.m_class.m_subService, data.Info.m_class.m_level);
            MainDataStore.building_money[buildingID] = (MainDataStore.building_money[buildingID] + (trade_income1 - trade_tax));
        }

        public static void RevertGabargeIncome(ushort vehicleID, ref Vehicle data, int num)
        {
            BuildingManager instance = Singleton<BuildingManager>.instance;
            Building building = instance.m_buildings.m_buffer[data.m_sourceBuilding];
            if (building.Info.m_class.m_service == ItemClass.Service.Garbage)
            {
                float product_value = 0f;
                switch ((TransferManager.TransferReason)data.m_transferType)
                {
                    case TransferManager.TransferReason.Lumber:
                        product_value = num * RealCityIndustryBuildingAI.GetResourcePrice((TransferManager.TransferReason)data.m_transferType);
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.ResourcePrice, (int)product_value, ItemClass.Service.PlayerIndustry, ItemClass.SubService.PlayerIndustryForestry, ItemClass.Level.Level1);
                        break;
                    case TransferManager.TransferReason.Coal:
                        product_value = num * RealCityIndustryBuildingAI.GetResourcePrice((TransferManager.TransferReason)data.m_transferType);
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.ResourcePrice, (int)product_value, ItemClass.Service.PlayerIndustry, ItemClass.SubService.PlayerIndustryOre, ItemClass.Level.Level1);
                        break;
                    case TransferManager.TransferReason.Petrol:
                        product_value = num * RealCityIndustryBuildingAI.GetResourcePrice((TransferManager.TransferReason)data.m_transferType);
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.ResourcePrice, (int)product_value, ItemClass.Service.PlayerIndustry, ItemClass.SubService.PlayerIndustryOil, ItemClass.Level.Level1);
                        break;
                    default: DebugLog.LogToFileOnly("Error: ProcessGabargeIncome find unknow gabarge transition" + building.Info.m_class.ToString() + "transfer reason " + data.m_transferType.ToString()); break;
                }
            }
        }
    }
}
