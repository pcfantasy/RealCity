using ColossalFramework;
using HarmonyLib;
using RealCity.CustomData;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
	[HarmonyPatch]
	public class HumanAIEnterVehiclePatch
	{
		public static MethodBase TargetMethod() {
			return typeof(HumanAI).GetMethod("EnterVehicle", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType() }, null);
		}
		public static void Prefix(ref CitizenInstance citizenData) {
			uint citizen = citizenData.m_citizen;
			if (citizen != 0u) {
				VehicleManager vehicleManager = Singleton<VehicleManager>.instance;
				ushort vehicleID = Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen].m_vehicle;
				if (vehicleID != 0) {
					vehicleID = vehicleManager.m_vehicles.m_buffer[vehicleID].GetFirstVehicle(vehicleID);
				}
				if (vehicleID != 0) {
					VehicleInfo info = vehicleManager.m_vehicles.m_buffer[vehicleID].Info;
					int ticketPrice = info.m_vehicleAI.GetTicketPrice(vehicleID, ref vehicleManager.m_vehicles.m_buffer[vehicleID]);
					if (ticketPrice != 0) {
						// NON-STOCK CODE START
						CitizenManager citizenManager = Singleton<CitizenManager>.instance;
						if ((citizenManager.m_citizens.m_buffer[citizenData.m_citizen].m_flags & Citizen.Flags.Tourist) == Citizen.Flags.None) {
							CitizenData.Instance.citizenMoney[citizen] = (CitizenData.Instance.citizenMoney[citizen] - (ticketPrice));
						} else {
							if (CitizenData.Instance.citizenMoney[citizen] < ticketPrice) {
								ticketPrice = (CitizenData.Instance.citizenMoney[citizen] > 0) ? (int)CitizenData.Instance.citizenMoney[citizen] + 1 : 1;
								CitizenData.Instance.citizenMoney[citizen] = (CitizenData.Instance.citizenMoney[citizen] - ticketPrice);
							} else {
								CitizenData.Instance.citizenMoney[citizen] = (CitizenData.Instance.citizenMoney[citizen] - (ticketPrice));
							}
							MainDataStore.outsideTouristMoney -= ticketPrice;
						}
						/// NON-STOCK CODE END ///
					}
				}
			}
		}
	}
}
