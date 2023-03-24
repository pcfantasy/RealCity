using ColossalFramework;
using HarmonyLib;
using RealCity.CustomData;
using System;
using System.Reflection;
using UnityEngine;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class TaxiAIUnloadPassengersPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(TaxiAI).GetMethod("UnloadPassengers", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(TransportPassengerData).MakeByRefType() }, null);
        }
        public static void Prefix(ref TaxiAI __instance, ref Vehicle data)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            Vector3 lastFramePosition = data.GetLastFramePosition();
            uint num2 = data.m_citizenUnits;
            int num3 = 0;
            while (num2 != 0u)
            {
                uint nextUnit = instance.m_units.m_buffer[(int)((UIntPtr)num2)].m_nextUnit;
                for (int i = 0; i < 5; i++)
                {
                    uint citizen = instance.m_units.m_buffer[(int)((UIntPtr)num2)].GetCitizen(i);
                    if (citizen != 0u)
                    {
                        ushort instance2 = instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_instance;
                        if (instance2 != 0)
                        {
                            Vector3 lastFramePosition2 = instance.m_instances.m_buffer[instance2].GetLastFramePosition();
                            int expense = Mathf.RoundToInt(__instance.m_transportInfo.m_ticketPrice * Vector3.Distance(lastFramePosition2, lastFramePosition) * 0.001f);
                            //new added begin
                            if (expense != 0)
                            {
                                //DebugLog.LogToFileOnly("UnloadPassengers ticketPrice pre = " + num4.ToString());
                                if ((Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen].m_flags & Citizen.Flags.Tourist) == Citizen.Flags.None)
                                {
                                    CitizenData.citizenMoney[citizen] -= (expense);
                                }
                                else
                                {
                                    if (CitizenData.citizenMoney[citizen] < expense)
                                    {
                                        expense = (CitizenData.citizenMoney[citizen] > 0) ? (int)CitizenData.citizenMoney[citizen] + 1 : 1;
                                        CitizenData.citizenMoney[citizen] = (CitizenData.citizenMoney[citizen] - (expense) - 1);
                                    }
                                }
                                Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, expense, data.Info.m_class);
                            }
                            //new added end
                        }
                    }
                }
                num2 = nextUnit;
                if (++num3 > 524288)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }
        }
    }
}
