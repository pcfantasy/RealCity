using System;
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

                    if (CitizenUnitData.familyMoney[containingUnit] < MainDataStore.maxGoodPurchase * RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Shopping))
                    {
                        sourceBuilding = targetBuilding;
                    }
                    else if (building.m_customBuffer2 < MainDataStore.maxGoodPurchase)
                    {
                        sourceBuilding = targetBuilding;
                    }
                }
            }
        }
    }
}
