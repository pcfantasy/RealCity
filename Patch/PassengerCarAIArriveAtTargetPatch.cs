using ColossalFramework;
using Harmony;
using RealCity.CustomAI;
using RealCity.CustomData;
using RealCity.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public static class PassengerCarAIArriveAtTargetPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(PassengerCarAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
        }
        public static void Prefix(ref CargoTruckAI __instance, ushort vehicleID, ref Vehicle data)
        {
            GetVehicleRunningTiming(vehicleID, ref data);
        }
        public static void GetVehicleRunningTiming(ushort vehicleID, ref Vehicle vehicleData)
        {
            if (vehicleID > Singleton<VehicleManager>.instance.m_vehicles.m_size)
            {
                DebugLog.LogToFileOnly("Error: vehicle ID greater than " + Singleton<VehicleManager>.instance.m_vehicles.m_size.ToString());
            }

            CitizenManager citizenMan = Singleton<CitizenManager>.instance;
            ushort instanceID = GetDriverInstance(vehicleID, ref vehicleData);

            if (instanceID != 0)
            {
                uint citizenID = citizenMan.m_instances.m_buffer[instanceID].m_citizen;
                if (citizenID != 0)
                {
                    if (!(citizenMan.m_citizens.m_buffer[citizenID].m_flags.IsFlagSet(Citizen.Flags.DummyTraffic)))
                    {
                        if (!(citizenMan.m_citizens.m_buffer[citizenID].m_flags.IsFlagSet(Citizen.Flags.Tourist)))
                        {
                            MainDataStore.totalCitizenDrivingTime = MainDataStore.totalCitizenDrivingTime + VehicleData.vehicleTransferTime[vehicleID];
                            if (vehicleData.m_citizenUnits != 0)
                            {
                                CitizenData.citizenMoney[citizenID] -= VehicleData.vehicleTransferTime[vehicleID];
                            }
                        }
                    }
                }
            }
            VehicleData.vehicleTransferTime[vehicleID] = 0;
        }

        public static ushort GetDriverInstance(ushort vehicleID, ref Vehicle data)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint num = data.m_citizenUnits;
            int num2 = 0;
            while (num != 0u)
            {
                uint nextUnit = instance.m_units.m_buffer[(int)((UIntPtr)num)].m_nextUnit;
                for (int i = 0; i < 5; i++)
                {
                    uint citizen = instance.m_units.m_buffer[(int)((UIntPtr)num)].GetCitizen(i);
                    if (citizen != 0u)
                    {
                        ushort instance2 = instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_instance;
                        if (instance2 != 0)
                        {
                            return instance2;
                        }
                    }
                }
                num = nextUnit;
                if (++num2 > 524288)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }
            return 0;
        }
    }
}
