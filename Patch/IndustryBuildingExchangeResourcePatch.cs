using ColossalFramework;
using HarmonyLib;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class IndustryBuildingExchangeResourcePatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(IndustryBuildingAI).GetMethod("ExchangeResource", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
        }
        public static bool Prefix(ushort sourceBuilding, ushort targetBuilding)
        {
            BuildingManager instance2 = Singleton<BuildingManager>.instance;
            BuildingInfo info = instance2.m_buildings.m_buffer[sourceBuilding].Info;
            BuildingInfo info2 = instance2.m_buildings.m_buffer[targetBuilding].Info;
            if ((info.m_buildingAI is WarehouseAI) || (info2.m_buildingAI is WarehouseAI))
            {
                return false;
            }
            return true;
        }
    }
}
