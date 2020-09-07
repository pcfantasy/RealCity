using ColossalFramework;
using HarmonyLib;
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
		public static void Prefix(ushort vehicleID, ref Vehicle data)
		{
			GetVehicleRunningTiming(vehicleID, ref data);
		}
		public static void GetVehicleRunningTiming(ushort vehicleID, ref Vehicle vehicleData)
		{
			CitizenManager citizenManager = Singleton<CitizenManager>.instance;
			ushort instanceID = GetDriverInstance(vehicleID, ref vehicleData);

			if (instanceID != 0)
			{
				uint citizenID = citizenManager.m_instances.m_buffer[instanceID].m_citizen;
				if (citizenID != 0)
				{
					if (!(citizenManager.m_citizens.m_buffer[citizenID].m_flags.IsFlagSet(Citizen.Flags.DummyTraffic)))
					{
						if (!(citizenManager.m_citizens.m_buffer[citizenID].m_flags.IsFlagSet(Citizen.Flags.Tourist)))
						{
							if (!IsOutSide(citizenManager.m_instances.m_buffer[instanceID].GetLastFramePosition()))
							{
								MainDataStore.totalCitizenDrivingTime += VehicleData.vehicleTransferTime[vehicleID];
								if (vehicleData.m_citizenUnits != 0)
								{
									CitizenData.citizenMoney[citizenID] -= VehicleData.vehicleTransferTime[vehicleID];
								}
							}
							else
							{
								MainDataStore.outsideTouristMoney -= VehicleData.vehicleTransferTime[vehicleID];
								if (RealCity.noPassengerCar)
								{
									if (vehicleData.m_citizenUnits != 0)
									{
										if (citizenManager.m_citizens.m_buffer[citizenID].m_vehicle == vehicleID)
										{
											Singleton<VehicleManager>.instance.ReleaseVehicle(vehicleID);
											citizenManager.m_citizens.m_buffer[citizenID].m_vehicle = 0;
										}
										else if (citizenManager.m_citizens.m_buffer[citizenID].m_vehicle != 0)
										{
											DebugLog.LogToFileOnly($"Warning: citizen vehicleID = {citizenManager.m_citizens.m_buffer[citizenID].m_vehicle}, but vehicleID = {vehicleID}");
											Singleton<VehicleManager>.instance.ReleaseVehicle(citizenManager.m_citizens.m_buffer[citizenID].m_vehicle);
											Singleton<VehicleManager>.instance.ReleaseVehicle(vehicleID);
											citizenManager.m_citizens.m_buffer[citizenID].m_vehicle = 0;
										}

										if (citizenManager.m_citizens.m_buffer[citizenID].m_parkedVehicle != 0)
										{
											Singleton<VehicleManager>.instance.ReleaseParkedVehicle(citizenManager.m_citizens.m_buffer[citizenID].m_parkedVehicle);
											citizenManager.m_citizens.m_buffer[citizenID].m_parkedVehicle = 0;
										}
									}
								}
							}
						}
						else
						{
							MainDataStore.outsideTouristMoney -= VehicleData.vehicleTransferTime[vehicleID];
						}
					}
				}
			}
			VehicleData.vehicleTransferTime[vehicleID] = 0;
		}

		public static bool IsOutSide(Vector3 pos)
		{
			if (pos.x > 8400 || pos.z > 8400 || pos.x < -8400 || pos.z < -8400)
				return true;
			return false;
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
