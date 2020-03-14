using ColossalFramework;
using Harmony;
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
                    consumptionMoney <<= 1;
                }
                if (citizenManager.m_citizens.m_buffer[citizen].WealthLevel == Citizen.Wealth.Medium)
                {
                    consumptionMoney <<= 2;
                }

                consumptionMoney = -consumptionMoney;
                buildingInfo.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, TransferManager.TransferReason.Entertainment, ref consumptionMoney);
                consumptionMoney = -MainDataStore.maxGoodPurchase;
                buildingInfo.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, TransferManager.TransferReason.Shopping, ref consumptionMoney);
            }
            else
            {
                ushort homeBuilding = citizenManager.m_citizens.m_buffer[citizen].m_homeBuilding;
                uint citizenUnit = CitizenData.GetCitizenUnit(homeBuilding);
                uint containingUnit = citizenManager.m_citizens.m_buffer[citizen].GetContainingUnit((uint)citizen, citizenUnit, CitizenUnit.Flags.Home);

                if (containingUnit != 0)
                {
                    int goodAmount = (int)(-(CitizenUnitData.familyMoney[containingUnit]) / RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Shopping));

                    if (goodAmount < 0)
                    {
                        if (goodAmount < -MainDataStore.maxGoodPurchase)
                        {
                            goodAmount = -MainDataStore.maxGoodPurchase;
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

            //base.VisitorEnter(buildingID, ref data, citizen);
            ushort eventIndex = data.m_eventIndex;
            if (eventIndex != 0)
            {
                EventManager eventManager = Singleton<EventManager>.instance;
                EventInfo eventInfo = eventManager.m_events.m_buffer[(int)eventIndex].Info;
                eventInfo.m_eventAI.VisitorEnter(eventIndex, ref eventManager.m_events.m_buffer[(int)eventIndex], buildingID, citizen);
            }
            return false;
        }
    }
}
