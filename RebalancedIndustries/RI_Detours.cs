using UnityEngine;
using System;
using RealCity.Util;

namespace RealCity.RebalancedIndustries
{
    public class CustomExtractingFacilityAI
    {
        public static void ExtractingFacilityAIProduceGoodsPrefix(ushort buildingID, ref Building buildingData, ExtractingFacilityAI __instance, out ushort __state)
        {
            if (Mod.IsIndustriesBuilding(__instance))
                __state = buildingData.m_customBuffer1;
            else
                __state = 0;
        }

        public static void ExtractingFacilityAIProduceGoodsPostfix(ushort buildingID, ref Building buildingData, ExtractingFacilityAI __instance, ref ushort __state)
        {
            int cargoDiff;

            if (Mod.IsIndustriesBuilding(__instance))
            {
                // Output
                cargoDiff = Convert.ToInt32((buildingData.m_customBuffer1 - __state) / RI_Data.GetFactorCargo(__instance.m_outputResource));
                //Debug.Log($"ID:{buildingID}={(ushort)Mathf.Clamp(__state + cargoDiff, 0, 64000)} ({__state + cargoDiff}), state:{__state}, buff:{buildingData.m_customBuffer1}, diff:{cargoDiff}");
                buildingData.m_customBuffer1 = (ushort)Mathf.Clamp(__state + cargoDiff, 0, 64000);
            }
            else
            {
                DebugLog.LogToFileOnly($"Unknown EF instance {__instance.name} ({__instance.GetType()})");
            }
        }
    }

    // Includes Unique Factories
    public class CustomProcessingFacilityAI
    {
        public static void ProcessingFacilityAIProduceGoodsPrefix(ushort buildingID, ref Building buildingData, ProcessingFacilityAI __instance, out ushort[] __state)
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

        public static void ProcessingFacilityAIProduceGoodsPostfix(ushort buildingID, ref Building buildingData, ProcessingFacilityAI __instance, ref ushort[] __state)
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
                    //Debug.Log($"Out ID:{buildingID}, state:{__state}, buff:{buildingData.m_customBuffer1}, diff:{cargoDiff}");
                }

                //if (__instance is UniqueFactoryAI)
                //    Debug.Log($"PF:{__instance.name}, ID:{buildingID}, lastDiff:{cargoDiff} (Old:{__state[0]}-{__state[1]},{__state[2]},{__state[3]},{__state[4]} - New:{buildingData.m_customBuffer1}-{buildingData.m_customBuffer2},{Mod.CombineBytes(buildingData.m_teens, buildingData.m_youngs)},{Mod.CombineBytes(buildingData.m_adults, buildingData.m_seniors)},{Mod.CombineBytes(buildingData.m_education1, buildingData.m_education2)})");
                //else
                //    Debug.Log($"PF:{__instance.name}, ID:{buildingID}, lastDiff:{cargoDiff} (Old:{__state[0]}-{__state[1]} - New:{buildingData.m_customBuffer1}-{buildingData.m_customBuffer2})");
            }
            else
            {
                DebugLog.LogToFileOnly($"Unknown PF instance {__instance.name} ({__instance.GetType()})");
            }
        }
    }

    public class CustomLandfillSiteAI
    {
        public static void LandfillSiteAIProduceGoodsPostfix(ushort buildingID, ref Building buildingData, LandfillSiteAI __instance, ref ushort __state)
        {
            int cargoDiff = 0;

            // Output
            if (RealCity.reduceVehicle)
            {
                cargoDiff = Convert.ToInt32((buildingData.m_customBuffer2 - __state) / MainDataStore.reduceCargoDiv);
            }
            else
            {
                cargoDiff = Convert.ToInt32(buildingData.m_customBuffer2 - __state);
            }
            //Debug.Log($"ID:{buildingID}={(ushort)Mathf.Clamp(__state + cargoDiff, 0, 64000)} ({__state + cargoDiff}), state:{__state}, buff:{buildingData.m_customBuffer1}, diff:{cargoDiff}");
            buildingData.m_customBuffer2 = (ushort)Mathf.Clamp(__state + cargoDiff, 0, 64000);
        }
    }
}
