using Harmony;
using System.Reflection;
using System;
using UnityEngine;
using RealCity.CustomAI;
using RealCity.CustomManager;

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
            DebugLog.LogToFileOnly("Harmony patches DeApplied");
        }
    }
}
