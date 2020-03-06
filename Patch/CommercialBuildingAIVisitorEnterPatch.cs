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
            CitizenManager instance = Singleton<CitizenManager>.instance;
            BuildingInfo info = data.Info;
            TransferManager.TransferReason tempTransferRreason = TransferManager.TransferReason.Entertainment;
            if ((instance.m_citizens.m_buffer[citizen].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None)
            {
                Random rand = new Random();
                int consumptionMoney = rand.Next(1000);
                if (tempTransferRreason == TransferManager.TransferReason.Entertainment)
                {
                    if (instance.m_citizens.m_buffer[citizen].WealthLevel == Citizen.Wealth.High)
                    {
                        consumptionMoney = consumptionMoney << 4;
                    }
                    if (instance.m_citizens.m_buffer[citizen].WealthLevel == Citizen.Wealth.Medium)
                    {
                        consumptionMoney = consumptionMoney << 2;
                    }
                }

                consumptionMoney = -consumptionMoney;
                info.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, tempTransferRreason, ref consumptionMoney);
                consumptionMoney = -MainDataStore.maxGoodPurchase;
                info.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, TransferManager.TransferReason.Shopping, ref consumptionMoney);
            }
            else
            {
                ushort homeBuilding = instance.m_citizens.m_buffer[citizen].m_homeBuilding;
                uint citizenUnit = CitizenData.GetCitizenUnit(homeBuilding);
                uint containingUnit = instance.m_citizens.m_buffer[citizen].GetContainingUnit((uint)citizen, citizenUnit, CitizenUnit.Flags.Home);

                if (containingUnit != 0)
                {
                    int num = (int)(-(CitizenUnitData.familyMoney[containingUnit]) / RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Shopping));

                    if (num < 0)
                    {
                        if (num < -MainDataStore.maxGoodPurchase)
                        {
                            num = -MainDataStore.maxGoodPurchase;
                        }
                        info.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, TransferManager.TransferReason.Shopping, ref num);

                        if (num != 0)
                        {
                            CitizenUnitData.familyGoods[containingUnit] = (ushort)(CitizenUnitData.familyGoods[containingUnit] - (num * 10));
                            if (CitizenUnitData.familyGoods[containingUnit] > 2000)
                            {
                                instance.m_citizens.m_buffer[citizen].m_flags &= ~Citizen.Flags.NeedGoods;
                            }
                        }
                    }
                    else
                    {
                        num = 0;
                    }

                    var familyMoney = (CitizenUnitData.familyMoney[containingUnit] + num * RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Shopping));

                    float consumptionIndex = 0f;
                    if (tempTransferRreason == TransferManager.TransferReason.Entertainment)
                    {
                        if (info.m_class.m_subService == ItemClass.SubService.CommercialLeisure)
                        {
                            consumptionIndex = 0.25f;
                        }
                        else if ((info.m_class.m_subService == ItemClass.SubService.CommercialTourist))
                        {
                            consumptionIndex = 0.2f;
                        }
                        else if ((info.m_class.m_subService == ItemClass.SubService.CommercialEco))
                        {
                            consumptionIndex = 0.05f;
                        }
                        else if ((info.m_class.m_subService == ItemClass.SubService.CommercialHigh))
                        {
                            consumptionIndex = 0.15f;
                        }
                        else
                        {
                            consumptionIndex = 0.1f;
                        }
                    }

                    int consumptionMoney = -(int)(consumptionIndex * familyMoney);

                    if (consumptionMoney < 0)
                    {
                        int tempMoney = consumptionMoney;
                        info.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, tempTransferRreason, ref tempMoney);
                    }
                    else
                    {
                        consumptionMoney = 0;
                    }

                    CitizenData.citizenMoney[citizen] = (CitizenData.citizenMoney[citizen] + consumptionMoney + num * RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Shopping));
                    CitizenUnitData.familyMoney[containingUnit] = CitizenUnitData.familyMoney[containingUnit] + consumptionMoney + num * RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Shopping);
                }
            }

            //base.VisitorEnter(buildingID, ref data, citizen);
            ushort eventIndex = data.m_eventIndex;
            if (eventIndex != 0)
            {
                EventManager instance1 = Singleton<EventManager>.instance;
                EventInfo info1 = instance1.m_events.m_buffer[(int)eventIndex].Info;
                info1.m_eventAI.VisitorEnter(eventIndex, ref instance1.m_events.m_buffer[(int)eventIndex], buildingID, citizen);
            }
            return false;
        }
    }
}
