using ColossalFramework;
using HarmonyLib;
using RealCity.CustomData;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
	[HarmonyPatch]
	public class TouristAISimulationStepPatch
	{
		public static ushort touristCount;
		public static MethodBase TargetMethod()
		{
			return typeof(TouristAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(Citizen).MakeByRefType() }, null);
		}
		public static void Postfix(uint citizenID, ref Citizen data)
		{
			if (!data.m_flags.IsFlagSet(Citizen.Flags.DummyTraffic))
			{
				if (CitizenData.citizenMoney[citizenID] < 100)
				{
					FindVisitPlace(citizenID, data.m_visitBuilding, GetLeavingReason(ref data));
				}
			}
		}

		public static TransferManager.TransferReason GetLeavingReason(ref Citizen data)
		{
			switch (data.WealthLevel)
			{
				case Citizen.Wealth.Low:
					return TransferManager.TransferReason.LeaveCity0;
				case Citizen.Wealth.Medium:
					return TransferManager.TransferReason.LeaveCity1;
				case Citizen.Wealth.High:
					return TransferManager.TransferReason.LeaveCity2;
				default:
					return TransferManager.TransferReason.LeaveCity0;
			}
		}

		public static void FindVisitPlace(uint citizenID, ushort sourceBuilding, TransferManager.TransferReason reason)
		{
			TransferManager.TransferOffer offer = default;
			offer.Priority = Singleton<SimulationManager>.instance.m_randomizer.Int32(7u);
			offer.Citizen = citizenID;
			offer.Position = Singleton<BuildingManager>.instance.m_buildings.m_buffer[sourceBuilding].m_position;
			offer.Amount = 1;
			offer.Active = true;
			Singleton<TransferManager>.instance.AddIncomingOffer(reason, offer);
		}
	}
}