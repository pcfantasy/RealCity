﻿using System;
using ColossalFramework;
using UnityEngine;
using RealCity.Util;
using Harmony;
using System.Reflection;
using RealCity.CustomData;
using RealCity.CustomAI;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class ResidentAIStartMovingPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(ResidentAI).GetMethod("StartMoving", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(Citizen).MakeByRefType(), typeof(ushort), typeof(ushort) }, null);
        }

        public static void Prefix(uint citizenID, ref Citizen data, ref ushort sourceBuilding, ref ushort targetBuilding)
        {
            //reject poor citizen to building which lack of goods
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
                    RealCityPrivateBuildingAI.GetVisitBehaviour(targetBuilding, ref building, ref behaviour, ref alivevisitCount, ref totalvisitCount);
                    var amount = building.m_customBuffer2 / MainDataStore.maxGoodPurchase - alivevisitCount;
                    if (amount <= 0)
                    {
                        sourceBuilding = targetBuilding;
                        building.m_flags &= ~Building.Flags.Active;
                        return;
                    }

                    if (CitizenUnitData.familyMoney[containingUnit] < MainDataStore.maxGoodPurchase * RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Shopping))
                    {
                        sourceBuilding = targetBuilding;
                        return;
                    }
                }
            }
        }
    }
}