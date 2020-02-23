﻿using ColossalFramework;
using Harmony;
using RealCity.CustomAI;
using RealCity.UI;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class PlayerBuildingAISimulationStepPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(PlayerBuildingAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
        }
        public static void Postfix(ushort buildingID, ref Building buildingData)
        {
            ProcessZeroWorker(buildingID, ref buildingData);
        }

        public static void ProcessZeroWorker(ushort buildingID, ref Building data)
        {
            if (data.m_flags.IsFlagSet(Building.Flags.Completed))
            {
                int aliveWorkCount = 0;
                int totalWorkCount = 0;
                Citizen.BehaviourData behaviour = default;
                BuildingUI.GetWorkBehaviour(buildingID, ref data, ref behaviour, ref aliveWorkCount, ref totalWorkCount);
                int allWorkCount;
                uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
                int num4 = (int)(currentFrameIndex & 4095u);
                if (((num4 >> 8) & 15u) == (buildingID & 15u))
                {
                    allWorkCount = RealCityResidentAI.TotalWorkCount(buildingID, data, true, true);
                }
                else
                {
                    allWorkCount = RealCityResidentAI.TotalWorkCount(buildingID, data, true, false);
                }

                if (totalWorkCount == 0 && allWorkCount != 0)
                {
                    int budget = Singleton<EconomyManager>.instance.GetBudget(data.Info.m_class);
                    int education3Salary = Math.Max((int)((budget * MainDataStore.govermentEducation3SalaryFixed * RealCityResidentAI.ProcessSalaryLandPriceAdjust(buildingID)) / 100), MainDataStore.govermentEducation3Salary);
                    float num1 = (education3Salary / 16) * allWorkCount;
                    Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, (int)num1, data.Info.m_class);
                }
            }
        }
    }
}
