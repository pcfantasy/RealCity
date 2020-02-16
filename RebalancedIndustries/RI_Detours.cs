using UnityEngine;
using System;
using RealCity.Util;
using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.UI;
using System.Reflection;
using HarmonyLib;

namespace RealCity.RebalancedIndustries
{
    [HarmonyPatch]
    public static class ExtractingFacilityAIProduceGoodsPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(ExtractingFacilityAI).GetMethod("ProduceGoods", BindingFlags.NonPublic | BindingFlags.Instance);
        }
        public static void Prefix(ushort buildingID, ref Building buildingData, ExtractingFacilityAI __instance, out ushort __state)
        {
            if (Mod.IsIndustriesBuilding(__instance))
                __state = buildingData.m_customBuffer1;
            else
                __state = 0;
        }

        public static void Postfix(ushort buildingID, ref Building buildingData, ExtractingFacilityAI __instance, ref ushort __state)
        {
            int cargoDiff;

            if (Mod.IsIndustriesBuilding(__instance))
            {
                // Output
                cargoDiff = Convert.ToInt32((buildingData.m_customBuffer1 - __state) / RI_Data.GetFactorCargo(__instance.m_outputResource));
                //DebugLog.LogToFileOnly($"ID:{buildingID}={(ushort)Mathf.Clamp(__state + cargoDiff, 0, 64000)} ({__state + cargoDiff}), state:{__state}, buff:{buildingData.m_customBuffer1}, diff:{cargoDiff}");
                buildingData.m_customBuffer1 = (ushort)Mathf.Clamp(__state + cargoDiff, 0, 64000);
            }
            else
            {
                DebugLog.LogToFileOnly($"Unknown EF instance {__instance.name} ({__instance.GetType()})");
            }
        }
    }
    
    [HarmonyPatch]
    public static class ExtractingFacilityAIGetLocalizedStatsPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(ExtractingFacilityAI).GetMethod("GetLocalizedStats", BindingFlags.Public | BindingFlags.Instance);
        }

        public static void Prefix(ushort buildingID, ref Building data, out byte __state)
        {
            __state = data.m_education3;
            if (RealCity.reduceVehicle)
            {
                data.m_education3 = (byte)(data.m_education3 / (MainDataStore.reduceCargoDiv * MainDataStore.playerIndustryBuildingProductionSpeedDiv));
            }
        }

        public static void Postfix(ushort buildingID, ref Building data, ref byte __state)
        {
            data.m_education3 = __state;
        }
    }

    // Includes Unique Factories
    [HarmonyPatch]
    public static class ProcessingFacilityAIProduceGoodsPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(ProcessingFacilityAI).GetMethod("ProduceGoods", BindingFlags.NonPublic | BindingFlags.Instance);
        }
        public static void Prefix(ushort buildingID, ref Building buildingData, ProcessingFacilityAI __instance, out ushort[] __state)
        {
            __state = new ushort[5];

            if (Mod.IsIndustriesBuilding(__instance))
            {
                __state[0] = buildingData.m_customBuffer1; // Output (increases during production)
                __state[1] = buildingData.m_customBuffer2; // Input1 (used during prodction)
                __state[2] = Mod.CombineBytes(buildingData.m_teens, buildingData.m_youngs); // Input2
                __state[3] = Mod.CombineBytes(buildingData.m_adults, buildingData.m_seniors); // Input3
                __state[4] = Mod.CombineBytes(buildingData.m_education1, buildingData.m_education2); // Input4
            }
            else
                __state[0] = __state[1] = __state[2] = __state[3] = __state[4] = 0;
        }

        public static void Postfix(ushort buildingID, ref Building buildingData, ProcessingFacilityAI __instance, ref ushort[] __state)
        {
            int cargoDiff = 0;

            if (Mod.IsIndustriesBuilding(__instance))
            {
                // Input
                if (__instance.m_inputResource1 != TransferManager.TransferReason.None)
                {
                    cargoDiff = Convert.ToInt32((__state[1] - buildingData.m_customBuffer2) / RI_Data.GetFactorCargo(__instance.m_inputResource1));
                    buildingData.m_customBuffer2 = (ushort)Mathf.Clamp(__state[1] - cargoDiff, 0, 64000);
                }

                if (__instance.m_inputResource2 != TransferManager.TransferReason.None)
                {
                    cargoDiff = Convert.ToInt32((__state[2] - Mod.CombineBytes(buildingData.m_teens, buildingData.m_youngs)) / RI_Data.GetFactorCargo(__instance.m_inputResource2));
                    Mod.SplitBytes((ushort)Mathf.Clamp(__state[2] - cargoDiff, 0, 64000), ref buildingData.m_teens, ref buildingData.m_youngs);
                }

                if (__instance.m_inputResource3 != TransferManager.TransferReason.None)
                {
                    cargoDiff = Convert.ToInt32((__state[3] - Mod.CombineBytes(buildingData.m_adults, buildingData.m_seniors)) / RI_Data.GetFactorCargo(__instance.m_inputResource3));
                    Mod.SplitBytes((ushort)Mathf.Clamp(__state[3] - cargoDiff, 0, 64000), ref buildingData.m_adults, ref buildingData.m_seniors);
                }

                if (__instance.m_inputResource4 != TransferManager.TransferReason.None)
                {
                    cargoDiff = Convert.ToInt32((__state[4] - Mod.CombineBytes(buildingData.m_education1, buildingData.m_education2)) / RI_Data.GetFactorCargo(__instance.m_inputResource4));
                    Mod.SplitBytes((ushort)Mathf.Clamp(__state[4] - cargoDiff, 0, 64000), ref buildingData.m_education1, ref buildingData.m_education2);
                }

                // Output (materials being produced)
                if (__instance.m_outputResource != TransferManager.TransferReason.None)
                {
                    //try
                    //{
                    //} catch (OverflowException Ex)
                    //{
                    //    Debug.Log($"Output overflow caught: cargoDiff:{buildingData.m_customBuffer1}-{__state[0]}={(buildingData.m_customBuffer1 - __state[0])} factor:{RI_Data.GetFactorCargo(__instance.m_outputResource)}\n{Ex.ToString()}");
                    //    Singleton<SimulationManager>.instance.SimulationPaused = true;
                    //}

                    cargoDiff = Convert.ToInt32((buildingData.m_customBuffer1 - __state[0]) / RI_Data.GetFactorCargo(__instance.m_outputResource));
                    buildingData.m_customBuffer1 = (ushort)Mathf.Clamp(__state[0] + cargoDiff, 0, 64000);
                    //DebugLog.LogToFileOnly($"Out ID:{buildingID}, state:{__state}, buff:{buildingData.m_customBuffer1}, diff:{cargoDiff}");
                }

                //if (__instance is UniqueFactoryAI)
                //    DebugLog.LogToFileOnly($"PF:{__instance.name}, ID:{buildingID}, lastDiff:{cargoDiff} (Old:{__state[0]}-{__state[1]},{__state[2]},{__state[3]},{__state[4]} - New:{buildingData.m_customBuffer1}-{buildingData.m_customBuffer2},{Mod.CombineBytes(buildingData.m_teens, buildingData.m_youngs)},{Mod.CombineBytes(buildingData.m_adults, buildingData.m_seniors)},{Mod.CombineBytes(buildingData.m_education1, buildingData.m_education2)})");
                //else
                //    DebugLog.LogToFileOnly($"PF:{__instance.name}, ID:{buildingID}, lastDiff:{cargoDiff} (Old:{__state[0]}-{__state[1]} - New:{buildingData.m_customBuffer1}-{buildingData.m_customBuffer2})");
            }
            else
            {
                DebugLog.LogToFileOnly($"Unknown PF instance {__instance.name} ({__instance.GetType()})");
            }
        }
    }

    [HarmonyPatch]
    public static class ProcessingFacilityAIGetLocalizedStatsPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(ProcessingFacilityAI).GetMethod("GetLocalizedStats", BindingFlags.Public | BindingFlags.Instance);
        }
        public static void Prefix(ushort buildingID, ref Building data, out byte __state)
        {
            __state = data.m_education3;
            if (RealCity.reduceVehicle)
            {
                data.m_education3 = (byte)(data.m_education3 / (MainDataStore.reduceCargoDiv * MainDataStore.playerIndustryBuildingProductionSpeedDiv));
            }
        }

        public static void Postfix(ushort buildingID, ref Building data, ref byte __state)
        {
            data.m_education3 = __state;
        }
    }

    [HarmonyPatch]
    public static class LandfillSiteAIProduceGoodsPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(LandfillSiteAI).GetMethod("ProduceGoods", BindingFlags.NonPublic | BindingFlags.Instance);
        }
        public static void Prefix(ushort buildingID, ref Building buildingData, LandfillSiteAI __instance, out ushort __state)
        {
            __state = buildingData.m_customBuffer1;
        }

        public static void Postfix(ushort buildingID, ref Building buildingData, LandfillSiteAI __instance, ref ushort __state)
        {
            int cargoDiff = 0;

            // Output
            if (RealCity.reduceVehicle)
            {
                cargoDiff = Convert.ToInt32((buildingData.m_customBuffer2 - __state) >> MainDataStore.reduceCargoDivShift);
            }
            else
            {
                cargoDiff = Convert.ToInt32(buildingData.m_customBuffer2 - __state);
            }
            //Debug.Log($"ID:{buildingID}={(ushort)Mathf.Clamp(__state + cargoDiff, 0, 64000)} ({__state + cargoDiff}), state:{__state}, buff:{buildingData.m_customBuffer1}, diff:{cargoDiff}");
            buildingData.m_customBuffer2 = (ushort)Mathf.Clamp(__state + cargoDiff, 0, 64000);
        }
    }

    [HarmonyPatch]
    public static class LandfillSiteAIGetGarbageRate
    {
        public static MethodBase TargetMethod()
        {
            return typeof(LandfillSiteAI).GetMethod("GetGarbageRate", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
        }
        public static void Postfix(ref int __result)
        {
            if (RealCity.reduceVehicle)
            {
                __result >>= 1;
            }
        }
    }

    [HarmonyPatch]
    public static class UniqueFactoryWorldInfoPanelUpdateBindingsPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(UniqueFactoryWorldInfoPanel).GetMethod("UpdateBindings", BindingFlags.NonPublic | BindingFlags.Instance);
        }
        public static void Postfix(ref InstanceID ___m_InstanceID, ref UILabel ___m_expenses)
        {
            ushort id = ___m_InstanceID.Building;
            Building building = Singleton<BuildingManager>.instance.m_buildings.m_buffer[id];
            UniqueFactoryAI ai = (UniqueFactoryAI)building.Info.m_buildingAI;
            int volume;
            byte health = Singleton<BuildingManager>.instance.m_buildings.m_buffer[id].m_health;

            volume = health * ai.m_inputRate1 * 16 / 100;
            long input1 = volume * IndustryBuildingAI.GetResourcePrice(ai.m_inputResource1) / (long)RI_Data.GetFactorCargo(ai.m_inputResource1) / 10000;
            volume = health * ai.m_inputRate2 * 16 / 100;
            long input2 = volume * IndustryBuildingAI.GetResourcePrice(ai.m_inputResource2) / (long)RI_Data.GetFactorCargo(ai.m_inputResource2) / 10000;
            volume = health * ai.m_inputRate3 * 16 / 100;
            long input3 = volume * IndustryBuildingAI.GetResourcePrice(ai.m_inputResource3) / (long)RI_Data.GetFactorCargo(ai.m_inputResource3) / 10000;
            volume = health * ai.m_inputRate4 * 16 / 100;
            long input4 = volume * IndustryBuildingAI.GetResourcePrice(ai.m_inputResource4) / (long)RI_Data.GetFactorCargo(ai.m_inputResource4) / 10000;
            ___m_expenses.text = (input1 + input2 + input3 + input4).ToString(Settings.moneyFormatNoCents, LocaleManager.cultureInfo);
        }
    }
}
