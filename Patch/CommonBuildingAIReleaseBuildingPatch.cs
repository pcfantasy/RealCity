using Harmony;
using RealCity.Util;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class CommonBuildingAIReleaseBuildingPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(CommonBuildingAI).GetMethod("ReleaseBuilding");
        }
        public static void Postfix(ushort buildingID)
        {
            MainDataStore.building_money[buildingID] = 0;
            MainDataStore.building_buffer2[buildingID] = 0;
            MainDataStore.building_buffer1[buildingID] = 0;
            MainDataStore.building_buffer3[buildingID] = 0;
            MainDataStore.building_buffer4[buildingID] = 0;
            MainDataStore.isBuildingWorkerUpdated[buildingID] = false;
            MainDataStore.building_money_threat[buildingID] = 0;
        }
    }
}
