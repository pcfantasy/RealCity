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
            var vehicleManagerReleaseVehicleImplementation = typeof(VehicleManager).GetMethod("ReleaseVehicleImplementation", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var vehicleManagerReleaseVehicleImplementationPostFix = typeof(RealCityVehicleManager).GetMethod("VehicleManagerReleaseVehicleImplementationPostFix");
            harmony.ConditionalPatch(vehicleManagerReleaseVehicleImplementation,
                null,
                new HarmonyMethod(vehicleManagerReleaseVehicleImplementationPostFix));
            //8
            var buildingAIVisitorEnter = typeof(BuildingAI).GetMethod("VisitorEnter", BindingFlags.Public | BindingFlags.Instance);
            var buildingAIVisitorEnterPostFix = typeof(RealCityBuildingAI).GetMethod("BuildingAIVisitorEnterPostFix");
            harmony.ConditionalPatch(buildingAIVisitorEnter,
                null,
                new HarmonyMethod(buildingAIVisitorEnterPostFix));
            //9
            var officeBuildingAIGetOutgoingTransferReason = typeof(OfficeBuildingAI).GetMethod("GetOutgoingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            var officeBuildingAIGetOutgoingTransferReasonPreFix = typeof(RealCityBuildingAI).GetMethod("OfficeBuildingAIGetOutgoingTransferReasonPreFix");
            harmony.ConditionalPatch(officeBuildingAIGetOutgoingTransferReason,
                new HarmonyMethod(officeBuildingAIGetOutgoingTransferReasonPreFix),
                null);
            //10
            var privateBuildingAISimulationStepActive = typeof(PrivateBuildingAI).GetMethod("SimulationStepActive", BindingFlags.NonPublic | BindingFlags.Instance);
            var privateBuildingAISimulationStepActivePostFix = typeof(RealCityPrivateBuildingAI).GetMethod("PrivateBuildingAISimulationStepActivePostFix");
            harmony.ConditionalPatch(privateBuildingAISimulationStepActive,
                null,
                new HarmonyMethod(privateBuildingAISimulationStepActivePostFix));
            //11
            //Patch RI related
            var extractingFacilityAIProduceGoods = typeof(ExtractingFacilityAI).GetMethod("ProduceGoods", BindingFlags.NonPublic | BindingFlags.Instance);
            var extractingFacilityAIProduceGoodsPrefix = typeof(CustomExtractingFacilityAI).GetMethod("ExtractingFacilityAIProduceGoodsPrefix");
            var extractingFacilityAIProduceGoodsPostfix = typeof(CustomExtractingFacilityAI).GetMethod("ExtractingFacilityAIProduceGoodsPostfix");
            harmony.ConditionalPatch(extractingFacilityAIProduceGoods,
                new HarmonyMethod(extractingFacilityAIProduceGoodsPrefix),
                new HarmonyMethod(extractingFacilityAIProduceGoodsPostfix));
            //12
            //Patch RI related
            var processingFacilityAIProduceGoods = typeof(ProcessingFacilityAI).GetMethod("ProduceGoods", BindingFlags.NonPublic | BindingFlags.Instance);
            var processingFacilityAIProduceGoodsPrefix = typeof(CustomProcessingFacilityAI).GetMethod("ProcessingFacilityAIProduceGoodsPrefix");
            var processingFacilityAIProduceGoodsPostfix = typeof(CustomProcessingFacilityAI).GetMethod("ProcessingFacilityAIProduceGoodsPostfix");
            harmony.ConditionalPatch(processingFacilityAIProduceGoods,
                new HarmonyMethod(processingFacilityAIProduceGoodsPrefix),
                new HarmonyMethod(processingFacilityAIProduceGoodsPostfix));
            //13
            var customLandfillSiteAIProduceGoods = typeof(LandfillSiteAI).GetMethod("ProduceGoods", BindingFlags.NonPublic | BindingFlags.Instance);
            var customLandfillSiteAIProduceGoodsPrefix = typeof(CustomLandfillSiteAI).GetMethod("CustomLandfillSiteAIProduceGoodsPrefix");
            var customLandfillSiteAIProduceGoodsPostfix = typeof(CustomLandfillSiteAI).GetMethod("CustomLandfillSiteAIProduceGoodsPostfix");
            harmony.ConditionalPatch(customLandfillSiteAIProduceGoods,
                new HarmonyMethod(customLandfillSiteAIProduceGoodsPrefix),
                new HarmonyMethod(customLandfillSiteAIProduceGoodsPostfix));
            //14
            var processingFacilityAIGetLocalizedStats = typeof(ProcessingFacilityAI).GetMethod("GetLocalizedStats", BindingFlags.Public | BindingFlags.Instance);
            var processingFacilityAIGetLocalizedStatsPrefix = typeof(CustomProcessingFacilityAI).GetMethod("ProcessingFacilityAIGetLocalizedStatsPrefix");
            var processingFacilityAIGetLocalizedStatsPostfix = typeof(CustomProcessingFacilityAI).GetMethod("ProcessingFacilityAIGetLocalizedStatsPostfix");
            harmony.ConditionalPatch(processingFacilityAIGetLocalizedStats,
                new HarmonyMethod(processingFacilityAIGetLocalizedStatsPrefix),
                new HarmonyMethod(processingFacilityAIGetLocalizedStatsPostfix));
            //15
            var extractingFacilityAIGetLocalizedStats = typeof(ExtractingFacilityAI).GetMethod("GetLocalizedStats", BindingFlags.Public | BindingFlags.Instance);
            var extractingFacilityAIGetLocalizedStatsPrefix = typeof(CustomExtractingFacilityAI).GetMethod("ExtractingFacilityAIGetLocalizedStatsPrefix");
            var extractingFacilityAIGetLocalizedStatsPostfix = typeof(CustomExtractingFacilityAI).GetMethod("ExtractingFacilityAIGetLocalizedStatsPostfix");
            harmony.ConditionalPatch(extractingFacilityAIGetLocalizedStats,
                new HarmonyMethod(extractingFacilityAIGetLocalizedStatsPrefix),
                new HarmonyMethod(extractingFacilityAIGetLocalizedStatsPostfix));
            //16
            //Patch RI related
            var uniqueFactoryWorldInfoPanelUpdateBindings = typeof(UniqueFactoryWorldInfoPanel).GetMethod("UpdateBindings", BindingFlags.NonPublic | BindingFlags.Instance);
            var uniqueFactoryWorldInfoPanelUpdateBindingsPostfix = typeof(CustomUniqueFactoryWorldInfoPanel).GetMethod("UniqueFactoryWorldInfoPanelUpdateBindingsPostfix");
            harmony.ConditionalPatch(uniqueFactoryWorldInfoPanelUpdateBindings,
                null,
                new HarmonyMethod(uniqueFactoryWorldInfoPanelUpdateBindingsPostfix));
            //17
            var citizenManagerReleaseCitizenInstance= typeof(CitizenManager).GetMethod("ReleaseCitizenInstance", BindingFlags.Public | BindingFlags.Instance);
            var citizenManagerReleaseCitizenInstancePostFix = typeof(RealCityCitizenManager).GetMethod("CitizenManagerReleaseCitizenInstancePostFix");
            harmony.ConditionalPatch(citizenManagerReleaseCitizenInstance,
                null,
                new HarmonyMethod(citizenManagerReleaseCitizenInstancePostFix));
            //18
            var humanAISimulationStep= typeof(HumanAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(CitizenInstance.Frame).MakeByRefType(), typeof(bool)}, null);
            var humanAISimulationStepPostFix = typeof(RealCityHumanAI).GetMethod("HumanAISimulationStepPostFix");
            harmony.ConditionalPatch(humanAISimulationStep,
                null,
                new HarmonyMethod(humanAISimulationStepPostFix));

            // 19
            RealCityPrivateBuildingAI.originalGetColorMethod = typeof(PrivateBuildingAI).GetMethod("GetColor", BindingFlags.NonPublic | BindingFlags.Instance);
            var privateBuildingAIGetColorPostFix = typeof(RealCityPrivateBuildingAI).GetMethod("PrivateBuildingAIGetColorPostFix");
            harmony.ConditionalPatch(RealCityPrivateBuildingAI.originalGetColorMethod,
                null,
                new HarmonyMethod(privateBuildingAIGetColorPostFix));

            Loader.HarmonyDetourFailed = false;
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
            var vehicleManagerReleaseVehicleImplementation = typeof(VehicleManager).GetMethod("ReleaseVehicleImplementation", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var vehicleManagerReleaseVehicleImplementationPostFix = typeof(RealCityVehicleManager).GetMethod("VehicleManagerReleaseVehicleImplementationPostFix");
            harmony.ConditionalUnPatch(vehicleManagerReleaseVehicleImplementation,
                null,
                new HarmonyMethod(vehicleManagerReleaseVehicleImplementationPostFix));
            //8
            var buildingAIVisitorEnter = typeof(BuildingAI).GetMethod("VisitorEnter", BindingFlags.Public | BindingFlags.Instance);
            var buildingAIVisitorEnterPostFix = typeof(RealCityBuildingAI).GetMethod("BuildingAIVisitorEnterPostFix");
            harmony.ConditionalUnPatch(buildingAIVisitorEnter,
                null,
                new HarmonyMethod(buildingAIVisitorEnterPostFix));
            //9
            var officeBuildingAIGetOutgoingTransferReason = typeof(OfficeBuildingAI).GetMethod("GetOutgoingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            var officeBuildingAIGetOutgoingTransferReasonPreFix = typeof(RealCityBuildingAI).GetMethod("OfficeBuildingAIGetOutgoingTransferReasonPreFix");
            harmony.ConditionalUnPatch(officeBuildingAIGetOutgoingTransferReason,
                new HarmonyMethod(officeBuildingAIGetOutgoingTransferReasonPreFix),
                null);
            //10
            var privateBuildingAISimulationStepActive = typeof(PrivateBuildingAI).GetMethod("SimulationStepActive", BindingFlags.NonPublic | BindingFlags.Instance);
            var privateBuildingAISimulationStepActivePostFix = typeof(RealCityPrivateBuildingAI).GetMethod("PrivateBuildingAISimulationStepActivePostFix");
            harmony.ConditionalUnPatch(privateBuildingAISimulationStepActive,
                null,
                new HarmonyMethod(privateBuildingAISimulationStepActivePostFix));
            //11
            //Unpatch RI related
            var extractingFacilityAIProduceGoods = typeof(ExtractingFacilityAI).GetMethod("ProduceGoods", BindingFlags.NonPublic | BindingFlags.Instance);
            var extractingFacilityAIProduceGoodsPrefix = typeof(CustomExtractingFacilityAI).GetMethod("ExtractingFacilityAIProduceGoodsPrefix");
            var extractingFacilityAIProduceGoodsPostfix = typeof(CustomExtractingFacilityAI).GetMethod("ExtractingFacilityAIProduceGoodsPostfix");
            harmony.ConditionalUnPatch(extractingFacilityAIProduceGoods,
                new HarmonyMethod(extractingFacilityAIProduceGoodsPrefix),
                new HarmonyMethod(extractingFacilityAIProduceGoodsPostfix));
            //12
            //Unpatch RI related
            var processingFacilityAIProduceGoods = typeof(ProcessingFacilityAI).GetMethod("ProduceGoods", BindingFlags.NonPublic | BindingFlags.Instance);
            var processingFacilityAIProduceGoodsPrefix = typeof(CustomProcessingFacilityAI).GetMethod("ProcessingFacilityAIProduceGoodsPrefix");
            var processingFacilityAIProduceGoodsPostfix = typeof(CustomProcessingFacilityAI).GetMethod("ProcessingFacilityAIProduceGoodsPostfix");
            harmony.ConditionalUnPatch(processingFacilityAIProduceGoods,
                new HarmonyMethod(processingFacilityAIProduceGoodsPrefix),
                new HarmonyMethod(processingFacilityAIProduceGoodsPostfix));
            //13
            var customLandfillSiteAIProduceGoods = typeof(LandfillSiteAI).GetMethod("ProduceGoods", BindingFlags.NonPublic | BindingFlags.Instance);
            var customLandfillSiteAIProduceGoodsPrefix = typeof(CustomLandfillSiteAI).GetMethod("CustomLandfillSiteAIProduceGoodsPrefix");
            var customLandfillSiteAIProduceGoodsPostfix = typeof(CustomLandfillSiteAI).GetMethod("CustomLandfillSiteAIProduceGoodsPostfix");
            harmony.ConditionalUnPatch(customLandfillSiteAIProduceGoods,
                new HarmonyMethod(customLandfillSiteAIProduceGoodsPrefix),
                new HarmonyMethod(customLandfillSiteAIProduceGoodsPostfix));
            //14
            var processingFacilityAIGetLocalizedStats = typeof(ProcessingFacilityAI).GetMethod("GetLocalizedStats", BindingFlags.Public | BindingFlags.Instance);
            var processingFacilityAIGetLocalizedStatsPrefix = typeof(CustomProcessingFacilityAI).GetMethod("ProcessingFacilityAIGetLocalizedStatsPrefix");
            var processingFacilityAIGetLocalizedStatsPostfix = typeof(CustomProcessingFacilityAI).GetMethod("ProcessingFacilityAIGetLocalizedStatsPostfix");
            harmony.ConditionalUnPatch(processingFacilityAIGetLocalizedStats,
                new HarmonyMethod(processingFacilityAIGetLocalizedStatsPrefix),
                new HarmonyMethod(processingFacilityAIGetLocalizedStatsPostfix));
            //15
            var extractingFacilityAIGetLocalizedStats = typeof(ExtractingFacilityAI).GetMethod("GetLocalizedStats", BindingFlags.Public | BindingFlags.Instance);
            var extractingFacilityAIGetLocalizedStatsPrefix = typeof(CustomExtractingFacilityAI).GetMethod("ExtractingFacilityAIGetLocalizedStatsPrefix");
            var extractingFacilityAIGetLocalizedStatsPostfix = typeof(CustomExtractingFacilityAI).GetMethod("ExtractingFacilityAIGetLocalizedStatsPostfix");
            harmony.ConditionalUnPatch(extractingFacilityAIGetLocalizedStats,
                new HarmonyMethod(extractingFacilityAIGetLocalizedStatsPrefix),
                new HarmonyMethod(extractingFacilityAIGetLocalizedStatsPostfix));
            //16
            //Unpatch RI related
            var uniqueFactoryWorldInfoPanelUpdateBindings = typeof(UniqueFactoryWorldInfoPanel).GetMethod("UpdateBindings", BindingFlags.NonPublic | BindingFlags.Instance);
            var uniqueFactoryWorldInfoPanelUpdateBindingsPostfix = typeof(CustomUniqueFactoryWorldInfoPanel).GetMethod("UniqueFactoryWorldInfoPanelUpdateBindingsPostfix");
            harmony.ConditionalUnPatch(uniqueFactoryWorldInfoPanelUpdateBindings,
                null,
                new HarmonyMethod(uniqueFactoryWorldInfoPanelUpdateBindingsPostfix));
            //17
            var citizenManagerReleaseCitizenInstance = typeof(CitizenManager).GetMethod("ReleaseCitizenInstance", BindingFlags.Public | BindingFlags.Instance);
            var citizenManagerReleaseCitizenInstancePostFix = typeof(RealCityCitizenManager).GetMethod("CitizenManagerReleaseCitizenInstancePostFix");
            harmony.ConditionalUnPatch(citizenManagerReleaseCitizenInstance,
                null,
                new HarmonyMethod(citizenManagerReleaseCitizenInstancePostFix));
            //18
            var humanAISimulationStep = typeof(HumanAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(CitizenInstance.Frame).MakeByRefType(), typeof(bool) }, null);
            var humanAISimulationStepPostFix = typeof(RealCityHumanAI).GetMethod("HumanAISimulationStepPostFix");
            harmony.ConditionalUnPatch(humanAISimulationStep,
                null,
                new HarmonyMethod(humanAISimulationStepPostFix));

            // 19
            var privateBuildingAIGetColor = typeof(PrivateBuildingAI).GetMethod("GetColor", BindingFlags.NonPublic | BindingFlags.Instance);
            var privateBuildingAIGetColorPostFix = typeof(RealCityPrivateBuildingAI).GetMethod("PrivateBuildingAIGetColorPostFix");
            harmony.ConditionalUnPatch(privateBuildingAIGetColor,
                null,
                new HarmonyMethod(privateBuildingAIGetColorPostFix));

            RealCityPrivateBuildingAI.originalGetColorMethod = null;

            DebugLog.LogToFileOnly("Harmony patches DeApplied");
        }
    }
}
