using HarmonyLib;
using System;
using System.Reflection;
using ColossalFramework;
using RealCity.Util;
using RealCity.CustomData;
using RealCity.CustomAI;
using ColossalFramework.Math;
using UnityEngine;

namespace RealCity.Patch
{
	[HarmonyPatch]
	public class TransferManagerAddIncomingOfferPatch
	{
		public static MethodBase TargetMethod()
		{
			return typeof(TransferManager).GetMethod("AddIncomingOffer", BindingFlags.Public | BindingFlags.Instance);
		}

		[HarmonyPriority(Priority.First)]
		public static bool Prefix(TransferManager.TransferReason material, ref TransferManager.TransferOffer offer)
		{
			switch (material)
			{
				case TransferManager.TransferReason.Shopping:
				case TransferManager.TransferReason.ShoppingB:
				case TransferManager.TransferReason.ShoppingC:
				case TransferManager.TransferReason.ShoppingD:
				case TransferManager.TransferReason.ShoppingE:
				case TransferManager.TransferReason.ShoppingF:
				case TransferManager.TransferReason.ShoppingG:
				case TransferManager.TransferReason.ShoppingH:
				case TransferManager.TransferReason.Entertainment:
				case TransferManager.TransferReason.EntertainmentB:
				case TransferManager.TransferReason.EntertainmentC:
				case TransferManager.TransferReason.EntertainmentD:
				case TransferManager.TransferReason.TouristA:
				case TransferManager.TransferReason.TouristB:
				case TransferManager.TransferReason.TouristC:
				case TransferManager.TransferReason.TouristD:
					if (RealCity.realCityV10)
					{
						if (MainDataStore.outsideTouristMoney < 0)
						{
							if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[offer.Building].Info.m_buildingAI is OutsideConnectionAI)
								return false;
						}
					}
					break;
				case TransferManager.TransferReason.Oil:
				case TransferManager.TransferReason.Ore:
				case TransferManager.TransferReason.Coal:
				case TransferManager.TransferReason.Petrol:
				case TransferManager.TransferReason.Food:
				case TransferManager.TransferReason.Grain:
				case TransferManager.TransferReason.Lumber:
				case TransferManager.TransferReason.Logs:
				case TransferManager.TransferReason.Goods:
				case TransferManager.TransferReason.LuxuryProducts:
				case TransferManager.TransferReason.AnimalProducts:
				case TransferManager.TransferReason.Flours:
				case TransferManager.TransferReason.Petroleum:
				case TransferManager.TransferReason.Plastics:
				case TransferManager.TransferReason.Metals:
				case TransferManager.TransferReason.Glass:
				case TransferManager.TransferReason.PlanedTimber:
				case TransferManager.TransferReason.Paper:
					break;
				default:
					return true;
			}

			if (offer.Citizen != 0)
			{
				var instance = Singleton<CitizenManager>.instance;
				ushort homeBuilding = instance.m_citizens.m_buffer[offer.Citizen].m_homeBuilding;
				uint citizenUnit = CitizenData.GetCitizenUnit(homeBuilding);
				uint containingUnit = instance.m_citizens.m_buffer[offer.Citizen].GetContainingUnit((uint)offer.Citizen, citizenUnit, CitizenUnit.Flags.Home);

				if (!instance.m_citizens.m_buffer[offer.Citizen].m_flags.IsFlagSet(Citizen.Flags.Tourist))
				{
					if (CitizenUnitData.familyMoney[containingUnit] < MainDataStore.maxGoodPurchase * RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Shopping))
					{
						//Reject poor citizen to building
						return false;
					}
				}
			}
			else if (offer.Building != 0)
			{
				var instance = Singleton<BuildingManager>.instance;
				var buildingID = offer.Building;
				var buildingData = instance.m_buildings.m_buffer[buildingID];
				if (buildingData.Info.m_class.m_service == ItemClass.Service.Industrial)
				{
					RealCityIndustrialBuildingAI.InitDelegate();
					RealCityCommonBuildingAI.InitDelegate();
					var industrialBuildingAI = buildingData.Info.m_buildingAI as IndustrialBuildingAI;
					TransferManager.TransferReason incomingTransferReason = RealCityIndustrialBuildingAI.GetIncomingTransferReason((IndustrialBuildingAI)(buildingData.Info.m_buildingAI), buildingID);
					TransferManager.TransferReason secondaryIncomingTransferReason = RealCityIndustrialBuildingAI.GetSecondaryIncomingTransferReason((IndustrialBuildingAI)(buildingData.Info.m_buildingAI), buildingID);
					int maxIncomingLoadSize = RealCityIndustrialBuildingAI.MaxIncomingLoadSize((IndustrialBuildingAI)(buildingData.Info.m_buildingAI));
					int num = 0;
					int num1 = 0;
					int incomingCargoCapacity = 0;
					int value = 0;
					if (incomingTransferReason != TransferManager.TransferReason.None)
					{
						if (secondaryIncomingTransferReason != TransferManager.TransferReason.None)
						{
							RealCityCommonBuildingAI.CalculateGuestVehicles1((IndustrialBuildingAI)(buildingData.Info.m_buildingAI), buildingID, ref buildingData, incomingTransferReason, secondaryIncomingTransferReason, ref num, ref num1, ref incomingCargoCapacity, ref value);
						}
						else
						{
							RealCityCommonBuildingAI.CalculateGuestVehicles((IndustrialBuildingAI)(buildingData.Info.m_buildingAI), buildingID, ref buildingData, incomingTransferReason, ref num, ref num1, ref incomingCargoCapacity, ref value);
						}
					}
					int productionCapacity = industrialBuildingAI.CalculateProductionCapacity((ItemClass.Level)buildingData.m_level, new Randomizer((int)buildingID), buildingData.m_width, buildingData.m_length);
					int consumptionDivider = RealCityIndustrialBuildingAI.GetConsumptionDivider((IndustrialBuildingAI)(buildingData.Info.m_buildingAI));
					int fullCapacity = Mathf.Max(productionCapacity * 500 / consumptionDivider, maxIncomingLoadSize * 4);
					int incomingAmontNeeded = fullCapacity - (int)buildingData.m_customBuffer1 - incomingCargoCapacity;
					incomingAmontNeeded -= 8000;

					if (incomingAmontNeeded < 0)
					{
						return false;
					}
					else
					{
						if (material == incomingTransferReason)
						{
							//first remove.
							//game bug, will send 2 incomingTransferReason per period
							TransferManager.TransferOffer offer1 = default;
							offer1.Building = buildingID;
							Singleton<TransferManager>.instance.RemoveIncomingOffer(incomingTransferReason, offer1);
						}
					}
				}
				else if (buildingData.Info.m_class.m_service == ItemClass.Service.Commercial)
				{
					RealCityCommercialBuildingAI.InitDelegate();
					RealCityCommonBuildingAI.InitDelegate();
					var commercialBuildingAI = buildingData.Info.m_buildingAI as CommercialBuildingAI;
					int maxIncomingLoadSize = RealCityCommercialBuildingAI.MaxIncomingLoadSize((CommercialBuildingAI)(buildingData.Info.m_buildingAI));
					int visitplaceCount = commercialBuildingAI.CalculateVisitplaceCount((ItemClass.Level)buildingData.m_level, new Randomizer((int)buildingID), buildingData.m_width, buildingData.m_length);
					int fullCapacity = Mathf.Max(visitplaceCount * 500, maxIncomingLoadSize * 4);
					int num = 0;
					int num1 = 0;
					int incomingCargoCapacity = 0;
					int value = 0;
					TransferManager.TransferReason incomingTransferReason = RealCityCommercialBuildingAI.GetIncomingTransferReason((CommercialBuildingAI)(buildingData.Info.m_buildingAI));
					if (incomingTransferReason != TransferManager.TransferReason.None)
					{
						if (incomingTransferReason == TransferManager.TransferReason.Goods || incomingTransferReason == TransferManager.TransferReason.Food)
						{
							RealCityCommonBuildingAI.CalculateGuestVehicles1((CommercialBuildingAI)(buildingData.Info.m_buildingAI), buildingID, ref buildingData, incomingTransferReason, TransferManager.TransferReason.LuxuryProducts, ref num, ref num1, ref incomingCargoCapacity, ref value);
						}
						else
						{
							RealCityCommonBuildingAI.CalculateGuestVehicles((CommercialBuildingAI)(buildingData.Info.m_buildingAI), buildingID, ref buildingData, incomingTransferReason, ref num, ref num1, ref incomingCargoCapacity, ref value);
						}
						buildingData.m_tempImport = (byte)Mathf.Clamp(value, (int)buildingData.m_tempImport, 255);
					}
					int incomingAmontNeeded = fullCapacity - (int)buildingData.m_customBuffer1 - incomingCargoCapacity;
					incomingAmontNeeded -= 6000;
					if (incomingAmontNeeded < 0)
					{
						return false;
					}
				}
			}
			return true;
		}
	}
}