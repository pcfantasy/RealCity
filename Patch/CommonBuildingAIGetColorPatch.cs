using ColossalFramework;
using HarmonyLib;
using RealCity.CustomData;
using System;
using System.Reflection;
using UnityEngine;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class CommonBuildingAIGetColorPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(CommonBuildingAI).GetMethod(
                    "GetColor",
                    BindingFlags.Instance | BindingFlags.Public,
                    null,
                    new[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(InfoManager.InfoMode) },
                    new ParameterModifier[0]);
        }

        public static void Postfix(ushort buildingID, ref Building data, InfoManager.InfoMode infoMode, ref Color __result)
        {
            if (infoMode == InfoManager.InfoMode.LandValue)
            {
                ItemClass @class = data.Info.m_class;
                ItemClass.Service service = @class.m_service;
                switch (service)
                {
                    case ItemClass.Service.Residential:
                        long familyMoney = GetResidentialBuildingAverageMoney(data);
                        if (familyMoney < 50000)
                            BuildingData.buildingMoneyThreat[buildingID] = 1.0f - familyMoney / 100000.0f;
                        else
                            BuildingData.buildingMoneyThreat[buildingID] = (150000.0f - familyMoney) / 200000.0f;

                        if (BuildingData.buildingMoneyThreat[buildingID] < 0.5f)
                            __result = Color.Lerp(Color.green, Color.yellow, BuildingData.buildingMoneyThreat[buildingID] * 2.0f);
                        else
                            __result = Color.Lerp(Color.yellow, Color.red, (BuildingData.buildingMoneyThreat[buildingID] - 0.5f) * 2.0f);
                        break;

                    case ItemClass.Service.Office:
                    case ItemClass.Service.Industrial:
                    case ItemClass.Service.Commercial:
                        if (BuildingData.buildingMoneyThreat[buildingID] < 0.5f)
                            __result = Color.Lerp(Color.green, Color.yellow, BuildingData.buildingMoneyThreat[buildingID] * 2.0f);
                        else
                            __result = Color.Lerp(Color.yellow, Color.red, (BuildingData.buildingMoneyThreat[buildingID] - 0.5f) * 2.0f);
                        break;
                }
            }
        }
        public static long GetResidentialBuildingAverageMoney(Building buildingData)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint citzenUnit = buildingData.m_citizenUnits;
            int unitCount = 0;
            long totalMoney = 0;
            long averageMoney = 0;
            while (citzenUnit != 0u)
            {
                if ((ushort)(instance.m_units.m_buffer[citzenUnit].m_flags & CitizenUnit.Flags.Home) != 0)
                {
                    if ((instance.m_units.m_buffer[citzenUnit].m_citizen0 != 0) || (instance.m_units.m_buffer[citzenUnit].m_citizen1 != 0) || (instance.m_units.m_buffer[citzenUnit].m_citizen2 != 0) || (instance.m_units.m_buffer[citzenUnit].m_citizen3 != 0) || (instance.m_units.m_buffer[citzenUnit].m_citizen4 != 0))
                    {
                        unitCount++;
                        totalMoney += (long)CitizenUnitData.familyMoney[citzenUnit];
                    }
                }
                citzenUnit = instance.m_units.m_buffer[citzenUnit].m_nextUnit;
                if (++unitCount > 524288)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }

            if (unitCount != 0)
            {
                averageMoney = totalMoney / unitCount;
            }

            return averageMoney;
        }
    }
}
