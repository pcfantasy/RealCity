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
                    ProcessParkTourismTouristIncome(ref data, citizen);
                }
                else
                {
                    ProcessParkTourismResidentIncome(ref data, citizen);
                }
            }
        }

        public static void ProcessMonumentTourismTouristIncome(ref Building data, uint citizen)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            Random rand = new Random();
            int tourism_fee = rand.Next(1000);
            if (instance.m_citizens.m_buffer[citizen].WealthLevel == Citizen.Wealth.High)
                tourism_fee <<= 4;
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

        public static void ProcessParkTourismTouristIncome(ref Building data, uint citizen)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            Random rand = new Random();
            int tourism_fee = rand.Next(100);
            if (instance.m_citizens.m_buffer[citizen].WealthLevel == Citizen.Wealth.High)
                tourism_fee <<= 4;
            else if (instance.m_citizens.m_buffer[citizen].WealthLevel == Citizen.Wealth.Medium)
                tourism_fee <<= 2;

            Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, -tourism_fee, data.Info.m_class);
        }

        public static void ProcessParkTourismResidentIncome(ref Building data, uint citizen)
        {
            int tourism_fee = (int)(0.1f * CitizenData.citizenMoney[citizen]);
            if (tourism_fee > 0)
            {
                CitizenData.citizenMoney[citizen] = (CitizenData.citizenMoney[citizen] - tourism_fee);
                Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, tourism_fee, data.Info.m_class);
            }
        }
    }
}
