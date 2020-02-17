using ColossalFramework;
using Harmony;
using RealCity.Util;
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
        public static void Prefix(ref TaxiAI __instance, ushort vehicleID, ref Vehicle data, ref TransportPassengerData passengerData)
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
                            int num4 = Mathf.RoundToInt(__instance.m_transportInfo.m_ticketPrice * Vector3.Distance(lastFramePosition2, lastFramePosition) * 0.001f);
                            //new added begin
                            if (num4 != 0)
                            {
                                //DebugLog.LogToFileOnly("UnloadPassengers ticketPrice pre = " + num4.ToString());
                                CitizenManager instance3 = Singleton<CitizenManager>.instance;
                                ushort homeBuilding = instance3.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_homeBuilding;
                                BuildingManager instance4 = Singleton<BuildingManager>.instance;
                                if ((Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen].m_flags & Citizen.Flags.Tourist) == Citizen.Flags.None)
                                {
                                    MainDataStore.citizenMoney[citizen] -= (num4);
                                }
                                else
                                {
                                    if (MainDataStore.citizenMoney[citizen] < num4)
                                    {
                                        num4 = (MainDataStore.citizenMoney[citizen] > 0) ? (int)MainDataStore.citizenMoney[citizen] + 1 : 1;
                                        MainDataStore.citizenMoney[citizen] = (MainDataStore.citizenMoney[citizen] - (num4) - 1);
                                    }
                                }
                                Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, num4, data.Info.m_class);
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
