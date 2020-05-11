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
    public class BuildingAIVisitorEnterPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(BuildingAI).GetMethod("VisitorEnter", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(uint) }, null);
        }
        public static void Postfix(ushort buildingID, ref Building data, uint citizen)
        {
            ushort eventIndex = data.m_eventIndex;
            if (eventIndex == 0)
            {
                ProcessTourismIncome(buildingID, ref data, citizen);
            }
            else
            {
                EventManager instance = Singleton<EventManager>.instance;
                EventInfo info = instance.m_events.m_buffer[(int)eventIndex].Info;
                if ((info.m_eventAI is SportMatchAI) || (info.m_eventAI is VarsitySportsMatchAI))
                {
                    if (buildingID == instance.m_events.m_buffer[(int)eventIndex].m_building && (instance.m_events.m_buffer[(int)eventIndex].m_flags & (EventData.Flags.Preparing | EventData.Flags.Active)) != EventData.Flags.None)
                    {
                        if ((Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None)
                        {
                            MainDataStore.outsideTouristMoney -= instance.m_events.m_buffer[(int)eventIndex].m_ticketPrice;
                        }
                        else
                        {
                            CitizenData.citizenMoney[citizen] -= instance.m_events.m_buffer[(int)eventIndex].m_ticketPrice;
                        }
                    }
                }
            }
        }

        public static void ProcessTourismIncome(ushort buildingID, ref Building data, uint citizen)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            BuildingInfo info = data.Info;
            if (info.m_class.m_service == ItemClass.Service.Monument)
            {
                if (IsRealUniqueBuilding(buildingID))
                {
                    if ((instance.m_citizens.m_buffer[citizen].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None)
                    {
                        ProcessMonumentTourismTouristIncome(ref data, citizen);
                    }
                    else
                    {
                        ProcessMonumentTourismResidentIncome(ref data, citizen);
                    }
                }
            }
            else if (info.m_buildingAI is ParkBuildingAI)
            {
                if ((instance.m_citizens.m_buffer[citizen].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None)
                {
                    ProcessParkIncome(ref data, citizen, true);
                }
                else
                {
                    ProcessParkIncome(ref data, citizen, false);
                }
            }
        }

        public static void ProcessMonumentTourismTouristIncome(ref Building data, uint citizen)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            int tourism_fee = 1000;
            if (instance.m_citizens.m_buffer[citizen].WealthLevel == Citizen.Wealth.High)
                tourism_fee <<= 1;
            else if (instance.m_citizens.m_buffer[citizen].WealthLevel == Citizen.Wealth.Medium)
                tourism_fee <<= 2;

            RealCityPrivateBuildingAI.profitBuildingMoney += (tourism_fee >> 1);
            MainDataStore.outsideTouristMoney -= (tourism_fee >> 1);
            Singleton<EconomyManager>.instance.AddPrivateIncome((tourism_fee >> 1), ItemClass.Service.Commercial, ItemClass.SubService.CommercialTourist, ItemClass.Level.Level1, 113333);
        }

        public static void ProcessMonumentTourismResidentIncome(ref Building data, uint citizen)
        {
            int tourism_fee = (int)(0.2f * CitizenData.citizenMoney[citizen]);
            if (tourism_fee > 0)
            {
                CitizenData.citizenMoney[citizen] = (CitizenData.citizenMoney[citizen] - tourism_fee);
                RealCityPrivateBuildingAI.profitBuildingMoney += (tourism_fee >> 1);
                Singleton<EconomyManager>.instance.AddPrivateIncome((tourism_fee >> 1), ItemClass.Service.Commercial, ItemClass.SubService.CommercialTourist, ItemClass.Level.Level1, 114333);
            }
        }

        public static void ProcessParkIncome(ref Building data, uint citizen, bool isTourist)
        {
            DistrictManager instance2 = Singleton<DistrictManager>.instance;
            byte b = instance2.GetPark(data.m_position);
            if (b != 0 && !instance2.m_parks.m_buffer[b].IsPark)
            {
                b = 0;
            }

            if (b != 0)
            {
                int ticketPrice = instance2.m_parks.m_buffer[b].GetTicketPrice();
                var ticketPriceLeft = (ticketPrice / 100f);
                //Negetive price to help identify tourist and resident.
                if (isTourist)
                {
                    Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, -(int)ticketPriceLeft, data.Info.m_class);
                    MainDataStore.outsideTouristMoney -= ticketPrice;
                }
                else
                {
                    if (CitizenData.citizenMoney[citizen] > ticketPriceLeft)
                    {
                        CitizenData.citizenMoney[citizen] = (CitizenData.citizenMoney[citizen] - ticketPriceLeft);
                        Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, (int)ticketPriceLeft, data.Info.m_class);
                    }
                }

                RealCityPrivateBuildingAI.profitBuildingMoney += (long)(ticketPrice - ticketPriceLeft);
                DistrictPark[] park = instance2.m_parks.m_buffer;
                park[b].m_tempTicketIncome = park[b].m_tempTicketIncome + (uint)(ticketPriceLeft);
            }
        }

        public static bool IsRealUniqueBuilding(ushort buildingId)
        {
            if (buildingId == 0)
            {
                return false;
            }

            var buildingInfo = BuildingManager.instance.m_buildings.m_buffer[buildingId].Info;
            if (buildingInfo?.m_class?.m_service != ItemClass.Service.Monument)
            {
                return false;
            }

            var monumentAI = buildingInfo.m_buildingAI as MonumentAI;
            return monumentAI != null
                && (monumentAI.m_supportEvents & (EventManager.EventType.Football | EventManager.EventType.Concert)) == 0;
        }
    }
}
