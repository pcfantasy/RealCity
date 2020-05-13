using ColossalFramework;
using HarmonyLib;
using RealCity.CustomAI;
using RealCity.CustomData;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class MarketAIVisitorEnterPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(MarketAI).GetMethod("VisitorEnter", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(uint) }, null);
        }
        public static bool Prefix(ushort buildingID, ref Building data, uint citizen)
        {
            CitizenManager citizenManager = Singleton<CitizenManager>.instance;
            BuildingInfo buildingInfo = data.Info;
            if ((citizenManager.m_citizens.m_buffer[citizen].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None)
            {
                var consumptionMoney = -MainDataStore.maxGoodPurchase;
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
                int priceInt = 0;
                IndustryBuildingGetResourcePricePatch.Prefix(ref priceInt, TransferManager.TransferReason.Shopping, data.Info.m_class.m_service);
                var m_goodsSellPrice = priceInt / 100;

                if (containingUnit != 0)
                {
                    int goodAmount = (int)(-(CitizenUnitData.familyMoney[containingUnit]) / m_goodsSellPrice);

                    if (goodAmount < 0)
                    {
                        if (goodAmount < -MainDataStore.maxGoodPurchase)
                        {
                            goodAmount = -MainDataStore.maxGoodPurchase;
                        }

                        if (goodAmount == -100)
                        {
                            //Disable other -100 ModifyMaterialBuffer
                            goodAmount = -99;
                        }

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

                    CitizenData.citizenMoney[citizen] = (CitizenData.citizenMoney[citizen] + goodAmount * m_goodsSellPrice);
                    CitizenUnitData.familyMoney[containingUnit] = CitizenUnitData.familyMoney[containingUnit] + goodAmount * m_goodsSellPrice;
                }
            }

            Singleton<BuildingAI>.instance.VisitorEnter(buildingID, ref data, citizen);
            return false;
        }
    }
}
