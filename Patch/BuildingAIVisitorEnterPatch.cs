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
    public class BuildingAIVisitorEnterPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(BuildingAI).GetMethod("VisitorEnter", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(uint) }, null);
        }
        public static void Postfix(ref Building data, uint citizen)
        {
            ushort eventIndex = data.m_eventIndex;
            if (eventIndex == 0)
            {
                ProcessTourismIncome(ref data, citizen);
            }
        }
        public static void ProcessTourismIncome(ref Building data, uint citizen)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            BuildingInfo info = data.Info;
            Random rand = new Random();
            if (info.m_class.m_service == ItemClass.Service.Monument)
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

            Singleton<EconomyManager>.instance.AddPrivateIncome(tourism_fee, ItemClass.Service.Commercial, ItemClass.SubService.CommercialTourist, ItemClass.Level.Level1, 113333);
        }

        public static void ProcessMonumentTourismResidentIncome(ref Building data, uint citizen)
        {
            int tourism_fee = (int)(0.2f * CitizenData.citizenMoney[citizen]);
            if (tourism_fee > 0)
            {
                CitizenData.citizenMoney[citizen] = (CitizenData.citizenMoney[citizen] - tourism_fee);
                Singleton<EconomyManager>.instance.AddPrivateIncome(tourism_fee, ItemClass.Service.Commercial, ItemClass.SubService.CommercialTourist, ItemClass.Level.Level1, 114333);
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
                int ticketPrice = instance2.m_parks.m_buffer[b].GetTicketPrice() / 10;

                //Negetive price to help identify tourist and resident.
                if (isTourist)
                    Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, -ticketPrice, data.Info.m_class);
                else
                {
                    if (CitizenData.citizenMoney[citizen] > ticketPrice)
                    {
                        CitizenData.citizenMoney[citizen] = (CitizenData.citizenMoney[citizen] - ticketPrice);
                        Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, ticketPrice, data.Info.m_class);
                    }
                }

                DistrictPark[] park = instance2.m_parks.m_buffer;
                park[b].m_tempTicketIncome = park[b].m_tempTicketIncome + (uint)ticketPrice;
            }
        }
    }
}
