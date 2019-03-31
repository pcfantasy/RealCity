using Harmony;
using System.Reflection;
using System;
using RealCity.CustomAI;
using RealCity.CustomManager;
using RealCity.RebalancedIndustries;

namespace RealCity.Util
{
    public static class HarmonyDetours
    {
        private static HarmonyInstance harmony = null;
        private static void ConditionalPatch(this HarmonyInstance harmony, MethodBase method, HarmonyMethod prefix, HarmonyMethod postfix)
        {
            var fullMethodName = string.Format("{0}.{1}", method.ReflectedType?.Name ?? "(null)", method.Name);
            if (harmony.GetPatchInfo(method)?.Owners?.Contains(harmony.Id) == true)
            {
                DebugLog.LogToFileOnly("Harmony patches already present for {0}" + fullMethodName.ToString());
            }
            else
            {
                DebugLog.LogToFileOnly("Patching {0}..." + fullMethodName.ToString());
                harmony.Patch(method, prefix, postfix);
            }
        }

        private static void ConditionalUnPatch(this HarmonyInstance harmony, MethodBase method, HarmonyMethod prefix = null, HarmonyMethod postfix = null)
        {
            var fullMethodName = string.Format("{0}.{1}", method.ReflectedType?.Name ?? "(null)", method.Name);
            if (prefix != null)
            {
                DebugLog.LogToFileOnly("UnPatching Prefix{0}..." + fullMethodName.ToString());
                harmony.Unpatch(method, HarmonyPatchType.Prefix);
            }
            if (postfix != null)
            {
                DebugLog.LogToFileOnly("UnPatching Postfix{0}..." + fullMethodName.ToString());
                harmony.Unpatch(method, HarmonyPatchType.Postfix);
            }
        }

        public static void Apply()
        {
            harmony = HarmonyInstance.Create("RealCity");
            //1
            var commonBuildingAIReleaseBuilding = typeof(CommonBuildingAI).GetMethod("ReleaseBuilding");
            var commonBuildingAIReleaseBuildingPostFix = typeof(RealCityCommonBuildingAI).GetMethod("CommonBuildingAIReleaseBuildingPostfix");
            harmony.ConditionalPatch(commonBuildingAIReleaseBuilding,
                null,
                new HarmonyMethod(commonBuildingAIReleaseBuildingPostFix));
            //2
            var citizenManagerReleaseCitizenImplementation = typeof(CitizenManager).GetMethod("ReleaseCitizenImplementation", BindingFlags.NonPublic | BindingFlags.Instance);
            var citizenManagerReleaseCitizenImplementationPostFix = typeof(RealCityCitizenManager).GetMethod("CitizenManagerReleaseCitizenImplementationPostFix");
            harmony.ConditionalPatch(citizenManagerReleaseCitizenImplementation,
                null,
                new HarmonyMethod(citizenManagerReleaseCitizenImplementationPostFix));
            //3
            var citizenManagerReleaseUnitCitizen = typeof(CitizenManager).GetMethod("ReleaseUnitCitizen", BindingFlags.NonPublic | BindingFlags.Instance);
            var citizenManagerReleaseUnitCitizenPostFix = typeof(RealCityCitizenManager).GetMethod("CitizenManagerReleaseUnitCitizenPostFix");
            harmony.ConditionalPatch(citizenManagerReleaseUnitCitizenPostFix,
                null,
                new HarmonyMethod(citizenManagerReleaseUnitCitizenPostFix));
            //4
            var playerBuildingAISimulationStep = typeof(PlayerBuildingAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            var playerBuildingAISimulationStepPostFix = typeof(RealCityPlayerBuildingAI).GetMethod("PlayerBuildingAISimulationStepPostFix");
            harmony.ConditionalPatch(playerBuildingAISimulationStep,
                null,
                new HarmonyMethod(playerBuildingAISimulationStepPostFix));
            //5
            var residentAISimulationStep = typeof(ResidentAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(Citizen).MakeByRefType()}, null);
            var residentAISimulationStepPostFix = typeof(RealCityResidentAI).GetMethod("ResidentAISimulationStepPostFix");
            harmony.ConditionalPatch(residentAISimulationStep,
                null,
                new HarmonyMethod(residentAISimulationStepPostFix));
            //6
            var touristAISimulationStep = typeof(TouristAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(Citizen).MakeByRefType() }, null);
            var touristAISimulationStepPostFix = typeof(RealCityTouristAI).GetMethod("TouristAISimulationStepPostFix");
            harmony.ConditionalPatch(touristAISimulationStep,
                null,
                new HarmonyMethod(touristAISimulationStepPostFix));
            //7
            if (!Loader.isAdvancedJunctionRuleRunning)
            {
                var carSimulationStep = typeof(CarAI).GetMethod("SimulationStep", BindingFlags.Instance | BindingFlags.Public, null, new Type[] {
                typeof(ushort),
                typeof(Vehicle).MakeByRefType(),
                typeof(Vehicle.Frame).MakeByRefType(),
                typeof(ushort),
                typeof(Vehicle).MakeByRefType(),
                typeof(int)}, null);
                var customCarAISimulationStepPreFix = typeof(RealCityCarAI).GetMethod("CustomCarAISimulationStepPreFix");
                harmony.ConditionalPatch(carSimulationStep,
                    new HarmonyMethod(customCarAISimulationStepPreFix),
                    null);
            }
            //8
            var vehicleAIReleaseVehicle = typeof(VehicleAI).GetMethod("ReleaseVehicle", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var vehicleAIReleaseVehiclePostFix = typeof(RealCityVehicleAI).GetMethod("VehicleAIReleaseVehiclePostFix");
            harmony.ConditionalPatch(vehicleAIReleaseVehicle,
                null,
                new HarmonyMethod(vehicleAIReleaseVehiclePostFix));
            //9
            var buildingAIVisitorEnter = typeof(BuildingAI).GetMethod("VisitorEnter", BindingFlags.Public | BindingFlags.Instance);
            var buildingAIVisitorEnterPostFix = typeof(RealCityBuildingAI).GetMethod("BuildingAIVisitorEnterPostFix");
            harmony.ConditionalPatch(buildingAIVisitorEnter,
                null,
                new HarmonyMethod(buildingAIVisitorEnterPostFix));
            //10
            var officeBuildingAIGetOutgoingTransferReason = typeof(OfficeBuildingAI).GetMethod("GetOutgoingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            var officeBuildingAIGetOutgoingTransferReasonPreFix = typeof(RealCityBuildingAI).GetMethod("OfficeBuildingAIGetOutgoingTransferReasonPreFix");
            harmony.ConditionalPatch(officeBuildingAIGetOutgoingTransferReason,
                new HarmonyMethod(officeBuildingAIGetOutgoingTransferReasonPreFix),
                null);
            //11
            var privateBuildingAISimulationStepActive = typeof(PrivateBuildingAI).GetMethod("SimulationStepActive", BindingFlags.NonPublic | BindingFlags.Instance);
            var privateBuildingAISimulationStepActivePostFix = typeof(RealCityPrivateBuildingAI).GetMethod("PrivateBuildingAISimulationStepActivePostFix");
            harmony.ConditionalPatch(privateBuildingAISimulationStepActive,
                null,
                new HarmonyMethod(privateBuildingAISimulationStepActivePostFix));
            //12
            //Patch RI related
            var extractingFacilityAIProduceGoods = typeof(ExtractingFacilityAI).GetMethod("ProduceGoods", BindingFlags.NonPublic | BindingFlags.Instance);
            var extractingFacilityAIProduceGoodsPrefix = typeof(CustomExtractingFacilityAI).GetMethod("ExtractingFacilityProduceGoodsPrefix");
            var extractingFacilityAIProduceGoodsPostfix = typeof(CustomExtractingFacilityAI).GetMethod("ExtractingFacilityProduceGoodsPostfix");
            harmony.ConditionalPatch(extractingFacilityAIProduceGoods,
                new HarmonyMethod(extractingFacilityAIProduceGoodsPrefix),
                new HarmonyMethod(extractingFacilityAIProduceGoodsPostfix));
            //13
            //Patch RI related
            var processingFacilityAIProduceGoods = typeof(ProcessingFacilityAI).GetMethod("ProduceGoods", BindingFlags.NonPublic | BindingFlags.Instance);
            var processingFacilityAIProduceGoodsPrefix = typeof(CustomProcessingFacilityAI).GetMethod("ProcessingFacilityAIProduceGoodsPrefix");
            var processingFacilityAIProduceGoodsPostfix = typeof(CustomProcessingFacilityAI).GetMethod("ProcessingFacilityAIProduceGoodsPostfix");
            harmony.ConditionalPatch(processingFacilityAIProduceGoods,
                new HarmonyMethod(processingFacilityAIProduceGoodsPrefix),
                new HarmonyMethod(processingFacilityAIProduceGoodsPostfix));
            //14
            //Patch RI related
            var cityServiceWorldInfoPanelOnSetTarget = typeof(CityServiceWorldInfoPanel).GetMethod("OnSetTarget", BindingFlags.NonPublic | BindingFlags.Instance);
            var cityServiceWorldInfoPanelOnSetTargetPostfix = typeof(CustomCityServiceWorldInfoPanel).GetMethod("CityServiceWorldInfoPanelOnSetTargetPostfix");
            harmony.ConditionalPatch(cityServiceWorldInfoPanelOnSetTarget,
                null,
                new HarmonyMethod(cityServiceWorldInfoPanelOnSetTargetPostfix));
            //15
            //Patch RI related
            var cityServiceWorldInfoPanelUpdateBindings = typeof(CityServiceWorldInfoPanel).GetMethod("UpdateBindings", BindingFlags.NonPublic | BindingFlags.Instance);
            var cityServiceWorldInfoPanelUpdateBindingsPostfix = typeof(CustomCityServiceWorldInfoPanel).GetMethod("CityServiceWorldInfoPanelUpdateBindingsPostfix");
            harmony.ConditionalPatch(cityServiceWorldInfoPanelUpdateBindings,
                null,
                new HarmonyMethod(cityServiceWorldInfoPanelUpdateBindingsPostfix));
            //16
            //Patch RI related
            var warehouseWorldInfoPanelPanelUpdateBindings = typeof(WarehouseWorldInfoPanel).GetMethod("UpdateBindings", BindingFlags.NonPublic | BindingFlags.Instance);
            var warehouseWorldInfoPanelUpdateBindingsPostfix = typeof(CustomWarehouseWorldInfoPanel).GetMethod("WarehouseWorldInfoPanelUpdateBindingsPostfix");
            harmony.ConditionalPatch(warehouseWorldInfoPanelPanelUpdateBindings,
                null,
                new HarmonyMethod(warehouseWorldInfoPanelUpdateBindingsPostfix));
            //17
            //Patch RI related
            var uniqueFactoryWorldInfoPanelUpdateBindings = typeof(UniqueFactoryWorldInfoPanel).GetMethod("UpdateBindings", BindingFlags.NonPublic | BindingFlags.Instance);
            var uniqueFactoryWorldInfoPanelUpdateBindingsPostfix = typeof(CustomUniqueFactoryWorldInfoPanel).GetMethod("UniqueFactoryWorldInfoPanelUpdateBindingsPostfix");
            harmony.ConditionalPatch(uniqueFactoryWorldInfoPanelUpdateBindings,
                null,
                new HarmonyMethod(uniqueFactoryWorldInfoPanelUpdateBindingsPostfix));
            Loader.HarmonyDetourInited = true;
            DebugLog.LogToFileOnly("Harmony patches applied");
        }

        public static void DeApply()
        {
            //1
            var commonBuildingAIReleaseBuilding = typeof(CommonBuildingAI).GetMethod("ReleaseBuilding");
            var commonBuildingAIReleaseBuildingPostFix = typeof(RealCityCommonBuildingAI).GetMethod("CommonBuildingAIReleaseBuildingPostfix");
            harmony.ConditionalUnPatch(commonBuildingAIReleaseBuilding,
                null,
                new HarmonyMethod(commonBuildingAIReleaseBuildingPostFix));
            //2
            var citizenManagerReleaseCitizenImplementation = typeof(CitizenManager).GetMethod("ReleaseCitizenImplementation", BindingFlags.NonPublic | BindingFlags.Instance);
            var citizenManagerReleaseCitizenImplementationPostFix = typeof(RealCityCitizenManager).GetMethod("CitizenManagerReleaseCitizenImplementationPostFix");
            harmony.ConditionalUnPatch(citizenManagerReleaseCitizenImplementation,
                null,
                new HarmonyMethod(citizenManagerReleaseCitizenImplementationPostFix));
            //3
            var citizenManagerReleaseUnitCitizen = typeof(CitizenManager).GetMethod("ReleaseUnitCitizen", BindingFlags.NonPublic | BindingFlags.Instance);
            var citizenManagerReleaseUnitCitizenPostFix = typeof(RealCityCitizenManager).GetMethod("CitizenManagerReleaseUnitCitizenPostFix");
            harmony.ConditionalUnPatch(citizenManagerReleaseUnitCitizenPostFix,
                null,
                new HarmonyMethod(citizenManagerReleaseUnitCitizenPostFix));
            //4
            var playerBuildingAISimulationStep = typeof(PlayerBuildingAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            var playerBuildingAISimulationStepPostFix = typeof(RealCityPlayerBuildingAI).GetMethod("PlayerBuildingAISimulationStepPostFix");
            harmony.ConditionalUnPatch(playerBuildingAISimulationStep,
                null,
                new HarmonyMethod(playerBuildingAISimulationStepPostFix));
            //5
            var residentAISimulationStep = typeof(ResidentAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(Citizen).MakeByRefType() }, null);
            var residentAISimulationStepPostFix = typeof(RealCityResidentAI).GetMethod("ResidentAISimulationStepPostFix");
            harmony.ConditionalUnPatch(residentAISimulationStep,
                null,
                new HarmonyMethod(residentAISimulationStepPostFix));
            //6
            var touristAISimulationStep = typeof(TouristAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(Citizen).MakeByRefType() }, null);
            var touristAISimulationStepPostFix = typeof(RealCityTouristAI).GetMethod("TouristAISimulationStepPostFix");
            harmony.ConditionalUnPatch(touristAISimulationStep,
                null,
                new HarmonyMethod(touristAISimulationStepPostFix));
            //7
            if (!Loader.isAdvancedJunctionRuleRunning)
            {
                var carSimulationStep = typeof(CarAI).GetMethod("SimulationStep", BindingFlags.Instance | BindingFlags.Public, null, new Type[] {
                typeof(ushort),
                typeof(Vehicle).MakeByRefType(),
                typeof(Vehicle.Frame).MakeByRefType(),
                typeof(ushort),
                typeof(Vehicle).MakeByRefType(),
                typeof(int)}, null);
                var customCarAISimulationStepPreFix = typeof(RealCityCarAI).GetMethod("CustomCarAISimulationStepPreFix");
                harmony.ConditionalUnPatch(carSimulationStep,
                    new HarmonyMethod(customCarAISimulationStepPreFix),
                    null);
            }
            //8
            var vehicleAIReleaseVehicle = typeof(VehicleAI).GetMethod("ReleaseVehicle", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var vehicleAIReleaseVehiclePostFix = typeof(RealCityVehicleAI).GetMethod("VehicleAIReleaseVehiclePostFix");
            harmony.ConditionalUnPatch(vehicleAIReleaseVehicle,
                null,
                new HarmonyMethod(vehicleAIReleaseVehiclePostFix));
            //9
            var buildingAIVisitorEnter = typeof(BuildingAI).GetMethod("VisitorEnter", BindingFlags.Public | BindingFlags.Instance);
            var buildingAIVisitorEnterPostFix = typeof(RealCityBuildingAI).GetMethod("BuildingAIVisitorEnterPostFix");
            harmony.ConditionalUnPatch(buildingAIVisitorEnter,
                null,
                new HarmonyMethod(buildingAIVisitorEnterPostFix));
            //10
            var officeBuildingAIGetOutgoingTransferReason = typeof(OfficeBuildingAI).GetMethod("GetOutgoingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            var officeBuildingAIGetOutgoingTransferReasonPreFix = typeof(RealCityBuildingAI).GetMethod("OfficeBuildingAIGetOutgoingTransferReasonPreFix");
            harmony.ConditionalUnPatch(officeBuildingAIGetOutgoingTransferReason,
                new HarmonyMethod(officeBuildingAIGetOutgoingTransferReasonPreFix),
                null);
            //11
            var privateBuildingAISimulationStepActive = typeof(PrivateBuildingAI).GetMethod("SimulationStepActive", BindingFlags.NonPublic | BindingFlags.Instance);
            var privateBuildingAISimulationStepActivePostFix = typeof(RealCityPrivateBuildingAI).GetMethod("PrivateBuildingAISimulationStepActivePostFix");
            harmony.ConditionalUnPatch(privateBuildingAISimulationStepActive,
                null,
                new HarmonyMethod(privateBuildingAISimulationStepActivePostFix));
            //12
            //Unpatch RI related
            var extractingFacilityAIProduceGoods = typeof(ExtractingFacilityAI).GetMethod("ProduceGoods", BindingFlags.NonPublic | BindingFlags.Instance);
            var extractingFacilityAIProduceGoodsPrefix = typeof(CustomExtractingFacilityAI).GetMethod("ExtractingFacilityProduceGoodsPrefix");
            var extractingFacilityAIProduceGoodsPostfix = typeof(CustomExtractingFacilityAI).GetMethod("ExtractingFacilityProduceGoodsPostfix");
            harmony.ConditionalUnPatch(extractingFacilityAIProduceGoods,
                new HarmonyMethod(extractingFacilityAIProduceGoodsPrefix),
                new HarmonyMethod(extractingFacilityAIProduceGoodsPostfix));
            //13
            //Unpatch RI related
            var processingFacilityAIProduceGoods = typeof(ProcessingFacilityAI).GetMethod("ProduceGoods", BindingFlags.NonPublic | BindingFlags.Instance);
            var processingFacilityAIProduceGoodsPrefix = typeof(CustomProcessingFacilityAI).GetMethod("ProcessingFacilityAIProduceGoodsPrefix");
            var processingFacilityAIProduceGoodsPostfix = typeof(CustomProcessingFacilityAI).GetMethod("ProcessingFacilityAIProduceGoodsPostfix");
            harmony.ConditionalUnPatch(processingFacilityAIProduceGoods,
                new HarmonyMethod(processingFacilityAIProduceGoodsPrefix),
                new HarmonyMethod(processingFacilityAIProduceGoodsPostfix));
            //14
            //Unpatch RI related
            var cityServiceWorldInfoPanelOnSetTarget = typeof(CityServiceWorldInfoPanel).GetMethod("OnSetTarget", BindingFlags.NonPublic | BindingFlags.Instance);
            var cityServiceWorldInfoPanelOnSetTargetPostfix = typeof(CustomCityServiceWorldInfoPanel).GetMethod("CityServiceWorldInfoPanelOnSetTargetPostfix");
            harmony.ConditionalUnPatch(cityServiceWorldInfoPanelOnSetTarget,
                null,
                new HarmonyMethod(cityServiceWorldInfoPanelOnSetTargetPostfix));
            //15
            //Unpatch RI related
            var cityServiceWorldInfoPanelUpdateBindings = typeof(CityServiceWorldInfoPanel).GetMethod("UpdateBindings", BindingFlags.NonPublic | BindingFlags.Instance);
            var cityServiceWorldInfoPanelUpdateBindingsPostfix = typeof(CustomCityServiceWorldInfoPanel).GetMethod("CityServiceWorldInfoPanelUpdateBindingsPostfix");
            harmony.ConditionalUnPatch(cityServiceWorldInfoPanelUpdateBindings,
                null,
                new HarmonyMethod(cityServiceWorldInfoPanelUpdateBindingsPostfix));
            //16
            //Unpatch RI related
            var warehouseWorldInfoPanelPanelUpdateBindings = typeof(WarehouseWorldInfoPanel).GetMethod("UpdateBindings", BindingFlags.NonPublic | BindingFlags.Instance);
            var warehouseWorldInfoPanelUpdateBindingsPostfix = typeof(CustomWarehouseWorldInfoPanel).GetMethod("WarehouseWorldInfoPanelUpdateBindingsPostfix");
            harmony.ConditionalUnPatch(warehouseWorldInfoPanelPanelUpdateBindings,
                null,
                new HarmonyMethod(warehouseWorldInfoPanelUpdateBindingsPostfix));
            //17
            //Unpatch RI related
            var uniqueFactoryWorldInfoPanelUpdateBindings = typeof(UniqueFactoryWorldInfoPanel).GetMethod("UpdateBindings", BindingFlags.NonPublic | BindingFlags.Instance);
            var uniqueFactoryWorldInfoPanelUpdateBindingsPostfix = typeof(CustomUniqueFactoryWorldInfoPanel).GetMethod("UniqueFactoryWorldInfoPanelUpdateBindingsPostfix");
            harmony.ConditionalUnPatch(uniqueFactoryWorldInfoPanelUpdateBindings,
                null,
                new HarmonyMethod(uniqueFactoryWorldInfoPanelUpdateBindingsPostfix));
            Loader.HarmonyDetourInited = false;
            DebugLog.LogToFileOnly("Harmony patches DeApplied");
        }
    }
}
