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
            if ((instance.m_citizens.m_buffer[citizen].m_flags & Citizen.Flags.Tourist) == Citizen.Flags.None)
            {
                float consumptionIndex = 0f;
                if (tempTransferRreason == TransferManager.TransferReason.Entertainment)
                {
                    if ((info.m_class.m_subService == ItemClass.SubService.CommercialLeisure))
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

                if (consumptionMoney < 0)
                {
                    info.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, tempTransferRreason, ref consumptionMoney);
                }
                else
                {
                    consumptionMoney = 0;
                }

                int num = (int)(-(CitizenData.citizenMoney[citizen] + consumptionMoney) / RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Shopping));

                if (num < 0)
                {
                    if (num < -100)
                    {
                        num = -100;
                    }
                    info.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, TransferManager.TransferReason.Shopping, ref num);
                }
                else
                {
                    num = 0;
                }
                CitizenData.citizenMoney[citizen] = (CitizenData.citizenMoney[citizen] + consumptionMoney + num * RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Shopping));
            }
            else
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

                consumptionMoney = -(consumptionMoney >> MainDataStore.reduceCargoDivShift);
                info.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, tempTransferRreason, ref consumptionMoney);
                if (RealCity.reduceVehicle)
                    consumptionMoney = -(100 >> MainDataStore.reduceCargoDivShift);
                else
                    consumptionMoney = -100;
                info.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, TransferManager.TransferReason.Shopping, ref consumptionMoney);
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
