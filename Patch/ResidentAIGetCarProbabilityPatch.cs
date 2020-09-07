using ColossalFramework;
using HarmonyLib;
using RealCity.CustomData;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
	[HarmonyPatch]
	public class ResidentAIGetCarProbabilityPatch
	{
		public static MethodBase TargetMethod()
		{
			return typeof(ResidentAI).GetMethod("GetCarProbability", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(Citizen.AgeGroup) }, null);
		}
		[HarmonyPriority(Priority.First)]
		public static void Prefix(ref CitizenInstance citizenData, ref Citizen.AgeGroup ageGroup)
		{
			if (RealCity.noPassengerCar)
			{
				CitizenManager instance = Singleton<CitizenManager>.instance;
				var citizenID = citizenData.m_citizen;
				ushort homeBuilding = instance.m_citizens.m_buffer[citizenID].m_homeBuilding;
				uint citizenUnit = CitizenData.GetCitizenUnit(homeBuilding);
				uint containingUnit = instance.m_citizens.m_buffer[citizenID].GetContainingUnit((uint)citizenID, citizenUnit, CitizenUnit.Flags.Home);
				if ((containingUnit == 0) || (citizenID == 0))
				{
					//Change ageGroup to Child to disable car.
					ageGroup = Citizen.AgeGroup.Child;
				}
				else
				{
					if (CitizenUnitData.familyMoney[containingUnit] < MainDataStore.highWealth)
					{
						ageGroup = Citizen.AgeGroup.Child;
					}
				}
			}
		}
	}
}
