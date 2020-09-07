﻿using ColossalFramework;
using HarmonyLib;
using RealCity.CustomAI;
using RealCity.CustomData;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
	[HarmonyPatch]
	public class CommercialBuildingAIVisitorEnterPatch
	{
		public static MethodBase TargetMethod()
		{
			return typeof(CommercialBuildingAI).GetMethod("VisitorEnter", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(uint) }, null);
		}
		public static bool Prefix(ushort buildingID, ref Building data, uint citizen)
		{
			CitizenManager citizenManager = Singleton<CitizenManager>.instance;
			BuildingInfo buildingInfo = data.Info;
			if ((citizenManager.m_citizens.m_buffer[citizen].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None)
			{
				int consumptionMoney = MainDataStore.govermentSalary << 4;
				if (citizenManager.m_citizens.m_buffer[citizen].WealthLevel == Citizen.Wealth.High)
				{
					consumptionMoney <<= 2;
				}
				if (citizenManager.m_citizens.m_buffer[citizen].WealthLevel == Citizen.Wealth.Medium)
				{
					consumptionMoney <<= 1;
				}

				consumptionMoney = -consumptionMoney;
				if (MainDataStore.outsideTouristMoney > 0)
				{
					buildingInfo.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, TransferManager.TransferReason.Entertainment, ref consumptionMoney);
					MainDataStore.outsideTouristMoney += (consumptionMoney);
				}
				consumptionMoney = -MainDataStore.maxGoodPurchase;
				buildingInfo.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, TransferManager.TransferReason.Shopping, ref consumptionMoney);
				int priceInt = 0;
				IndustryBuildingGetResourcePricePatch.Prefix(ref priceInt, TransferManager.TransferReason.Shopping, data.Info.m_class.m_service);
				var m_goodsSellPrice = priceInt / 100;
				MainDataStore.outsideTouristMoney += (consumptionMoney * m_goodsSellPrice);
			}
			else
			{
				ushort homeBuilding = citizenManager.m_citizens.m_buffer[citizen].m_homeBuilding;
				uint citizenUnit = CitizenData.GetCitizenUnit(homeBuilding);
				uint containingUnit = citizenManager.m_citizens.m_buffer[citizen].GetContainingUnit((uint)citizen, citizenUnit, CitizenUnit.Flags.Home);

				if (containingUnit != 0)
				{
					//int goodAmount = (int)(-(CitizenUnitData.familyMoney[containingUnit]) / RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Shopping));
					int goodAmount = -MainDataStore.maxGoodPurchase;

					if (goodAmount < 0)
					{
						buildingInfo.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, TransferManager.TransferReason.Shopping, ref goodAmount);

						if (goodAmount != 0)
						{
							CitizenUnitData.familyGoods[containingUnit] = (ushort)(CitizenUnitData.familyGoods[containingUnit] - (goodAmount * 10));
							if (CitizenUnitData.familyGoods[containingUnit] > 2000)
							{
								citizenManager.m_citizens.m_buffer[citizen].m_flags &= ~Citizen.Flags.NeedGoods;
							}
						}
					}
					else
					{
						goodAmount = 0;
					}

					var familyMoney = (CitizenUnitData.familyMoney[containingUnit] + goodAmount * RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Shopping));

					float consumptionIndex;
					if (buildingInfo.m_class.m_subService == ItemClass.SubService.CommercialLeisure)
					{
						consumptionIndex = 0.25f;
					}
					else if ((buildingInfo.m_class.m_subService == ItemClass.SubService.CommercialTourist))
					{
						consumptionIndex = 0.2f;
					}
					else if ((buildingInfo.m_class.m_subService == ItemClass.SubService.CommercialEco))
					{
						consumptionIndex = 0.05f;
					}
					else if ((buildingInfo.m_class.m_subService == ItemClass.SubService.CommercialHigh))
					{
						consumptionIndex = 0.15f;
					}
					else
					{
						consumptionIndex = 0.1f;
					}

					int consumptionMoney = -(int)(consumptionIndex * familyMoney);

					if (consumptionMoney < 0)
					{
						int money = consumptionMoney;
						buildingInfo.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, TransferManager.TransferReason.Entertainment, ref money);
					}
					else
					{
						consumptionMoney = 0;
					}

					CitizenData.citizenMoney[citizen] = (CitizenData.citizenMoney[citizen] + consumptionMoney + goodAmount * RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Shopping));
					CitizenUnitData.familyMoney[containingUnit] = CitizenUnitData.familyMoney[containingUnit] + consumptionMoney + goodAmount * RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Shopping);
				}
			}

			Singleton<BuildingAI>.instance.VisitorEnter(buildingID, ref data, citizen);
			return false;
		}
	}
}
