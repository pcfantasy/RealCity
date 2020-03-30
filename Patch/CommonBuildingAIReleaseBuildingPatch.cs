using HarmonyLib;
using RealCity.CustomData;
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
            BuildingData.buildingMoney[buildingID] = 0;
            BuildingData.buildingWorkCount[buildingID] = 0;
            BuildingData.isBuildingWorkerUpdated[buildingID] = false;
            BuildingData.buildingMoneyThreat[buildingID] = 0;
        }
    }
}
