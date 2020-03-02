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
                int consumptionMoney = rand.Next(100);
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

                info.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, tempTransferRreason, ref consumptionMoney);
                if (RealCity.reduceVehicle)
                    consumptionMoney = -(100 >> MainDataStore.reduceCargoDivShift);
                else
                    consumptionMoney = -100;
                info.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, TransferManager.TransferReason.Shopping, ref consumptionMoney);
            }
            else
            { 
                float consumptionIndex = 0f;
                if (tempTransferRreason == TransferManager.TransferReason.Entertainment)
                {
                    if (info.m_class.m_subService == ItemClass.SubService.CommercialLeisure)
                    {
                        consumptionIndex = 0.5f;
                    }
                    else if ((info.m_class.m_subService == ItemClass.SubService.CommercialTourist))
                    {
                        consumptionIndex = 0.4f;
                    }
                    else if ((info.m_class.m_subService == ItemClass.SubService.CommercialEco))
                    {
                        consumptionIndex = 0.1f;
                    }
                    else if ((info.m_class.m_subService == ItemClass.SubService.CommercialHigh))
                    {
                        consumptionIndex = 0.3f;
                    }
                    else
                    {
                        consumptionIndex = 0.2f;
                    }
                }

                int consumptionMoney = -(int)(consumptionIndex * CitizenData.citizenMoney[citizen]);

                DebugLog.LogToFileOnly($"consumptionMoney = {consumptionMoney}");

                if (consumptionMoney < 0)
                {
                    DebugLog.LogToFileOnly($"ModifyMaterialBuffer consumptionMoney = {consumptionMoney}");
                    int tempMoney = consumptionMoney;
                    info.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, tempTransferRreason, ref tempMoney);
                    DebugLog.LogToFileOnly($"ModifyMaterialBuffer consumptionMoney post = {consumptionMoney}");
                }
                else
                {
                    consumptionMoney = 0;
                }

                int num = (int)(-(CitizenData.citizenMoney[citizen] + consumptionMoney) / RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Shopping));
                DebugLog.LogToFileOnly($" num = {num}");

                if (num < 0)
                {
                    if (num < -1000)
                    {
                        num = -1000;
                    }
                    info.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, TransferManager.TransferReason.Shopping, ref num);
                    DebugLog.LogToFileOnly($"ModifyMaterialBuffer num post = {num}");

                    if (num != 0)
                    {

                        ushort homeBuilding = instance.m_citizens.m_buffer[citizen].m_homeBuilding;
                        uint citizenUnit = CitizenData.GetCitizenUnit(homeBuilding);
                        uint containingUnit = instance.m_citizens.m_buffer[citizen].GetContainingUnit((uint)citizen, citizenUnit, CitizenUnit.Flags.Home);

                        if (containingUnit != 0)
                        {
                            instance.m_units.m_buffer[containingUnit].m_goods = (ushort)(instance.m_units.m_buffer[containingUnit].m_goods - num);
                            CitizenData.citizenCanUpdateGoods[citizen] = true;
                        }
                    }
                }
                else
                {
                    num = 0;
                }

                DebugLog.LogToFileOnly($"citizenMoney num pre = {CitizenData.citizenMoney[citizen]}");
                CitizenData.citizenMoney[citizen] = (CitizenData.citizenMoney[citizen] + consumptionMoney + num * RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Shopping));
                DebugLog.LogToFileOnly($"citizenMoney num post = {CitizenData.citizenMoney[citizen]}");
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
