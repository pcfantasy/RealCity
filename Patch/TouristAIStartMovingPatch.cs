﻿using System;
using ColossalFramework;
using UnityEngine;
using RealCity.Util;
using Harmony;
using System.Reflection;
using RealCity.CustomData;
using RealCity.CustomAI;
using ColossalFramework.Math;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class TouristAIStartMovingPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(TouristAI).GetMethod("StartMoving", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(Citizen).MakeByRefType(), typeof(ushort), typeof(ushort) }, null);
        }

        public static void Prefix(uint citizenID, ref Citizen data, ref ushort sourceBuilding, ref ushort targetBuilding)
        {
            if (data.m_workBuilding != targetBuilding)
            {
                var building = Singleton<BuildingManager>.instance.m_buildings.m_buffer[targetBuilding];
                if (building.Info.m_class.m_service == ItemClass.Service.Commercial)
                {
                    CitizenManager instance = Singleton<CitizenManager>.instance;
                    ushort homeBuilding = instance.m_citizens.m_buffer[citizenID].m_homeBuilding;
                    uint citizenUnit = CitizenData.GetCitizenUnit(homeBuilding);
                    uint containingUnit = instance.m_citizens.m_buffer[citizenID].GetContainingUnit((uint)citizenID, citizenUnit, CitizenUnit.Flags.Home);


                    Citizen.BehaviourData behaviour = default(Citizen.BehaviourData);
                    int alivevisitCount = 0;
                    int totalvisitCount = 0;
                    RealCityCommercialBuildingAI.GetVisitBehaviour((CommercialBuildingAI)(building.Info.m_buildingAI), targetBuilding, ref building, ref behaviour, ref alivevisitCount, ref totalvisitCount);
                    var amount = building.m_customBuffer2 / MainDataStore.maxGoodPurchase - totalvisitCount;
                    var AI = building.Info.m_buildingAI as CommercialBuildingAI;
                    var maxcount = AI.CalculateVisitplaceCount((ItemClass.Level)building.m_level, new Randomizer(targetBuilding), building.m_width, building.m_length);
                    if ((amount <= 0) || (maxcount <= totalvisitCount))
                    {
                        sourceBuilding = targetBuilding;
                        building.m_flags &= ~Building.Flags.Active;
                        return;
                    }
                }
            }
        }
    }
}