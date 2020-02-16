using ColossalFramework;
using HarmonyLib;
using RealCity.Util;
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
                        long family_money = GetResidentialBuildingAverageMoney(buildingID, ref data);
                        if (family_money < 5000)
                            MainDataStore.building_money_threat[buildingID] = 1.0f - family_money / 10000.0f;
                        else
                            MainDataStore.building_money_threat[buildingID] = (15000.0f - family_money) / 20000.0f;

                        if (MainDataStore.building_money_threat[buildingID] < 0.5f)
                            __result = Color.Lerp(Color.green, Color.yellow, MainDataStore.building_money_threat[buildingID] * 2.0f);
                        else
                            __result = Color.Lerp(Color.yellow, Color.red, (MainDataStore.building_money_threat[buildingID] - 0.5f) * 2.0f);
                        break;

                    case ItemClass.Service.Office:
                    case ItemClass.Service.Industrial:
                    case ItemClass.Service.Commercial:
                        if (MainDataStore.building_money_threat[buildingID] < 0.5f)
                            __result = Color.Lerp(Color.green, Color.yellow, MainDataStore.building_money_threat[buildingID] * 2.0f);
                        else
                            __result = Color.Lerp(Color.yellow, Color.red, (MainDataStore.building_money_threat[buildingID] - 0.5f) * 2.0f);
                        break;
                }
            }
        }
        public static long GetResidentialBuildingAverageMoney(ushort buildingID, ref Building buildingData)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint num = buildingData.m_citizenUnits;
            int num2 = 0;
            long totalMoney = 0;
            long averageMoney = 0;
            while (num != 0u)
            {
                if ((ushort)(instance.m_units.m_buffer[(int)((UIntPtr)num)].m_flags & CitizenUnit.Flags.Home) != 0)
                {
                    if ((instance.m_units.m_buffer[(int)((UIntPtr)num)].m_citizen0 != 0) || (instance.m_units.m_buffer[(int)((UIntPtr)num)].m_citizen1 != 0) || (instance.m_units.m_buffer[(int)((UIntPtr)num)].m_citizen2 != 0) || (instance.m_units.m_buffer[(int)((UIntPtr)num)].m_citizen3 != 0) || (instance.m_units.m_buffer[(int)((UIntPtr)num)].m_citizen4 != 0))
                    {
                        num2++;
                        totalMoney += (long)MainDataStore.family_money[num];
                    }
                }
                num = instance.m_units.m_buffer[(int)((UIntPtr)num)].m_nextUnit;
                if (++num2 > 524288)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }

            if (num2 != 0)
            {
                averageMoney = totalMoney / num2;
            }

            return averageMoney;
        }
    }
}
