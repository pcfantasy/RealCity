using ColossalFramework;
using Harmony;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public static class TouristAISimulationStepPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(TouristAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(Citizen).MakeByRefType() }, null);
        }
        public static void Postfix(uint citizenID, ref Citizen data)
        {
            //Add initial money
            if (!MainDataStore.isCitizenFirstMovingIn[citizenID])
            {
                MainDataStore.isCitizenFirstMovingIn[citizenID] = true;
                if (data.WealthLevel == Citizen.Wealth.Low)
                {
                    MainDataStore.citizenMoney[citizenID] += 3000;
                }
                else if (data.WealthLevel == Citizen.Wealth.Medium)
                {
                    MainDataStore.citizenMoney[citizenID] += 6000;
                }
                else
                {
                    MainDataStore.citizenMoney[citizenID] += 9000;
                }
            }
            else
            {
                if (MainDataStore.citizenMoney[citizenID] < 100)
                {
                    FindVisitPlace(citizenID, data.m_visitBuilding, GetLeavingReason(citizenID, ref data));
                }
            }
        }

        public static TransferManager.TransferReason GetLeavingReason(uint citizenID, ref Citizen data)
        {
            switch (data.WealthLevel)
            {
                case Citizen.Wealth.Low:
                    return TransferManager.TransferReason.LeaveCity0;
                case Citizen.Wealth.Medium:
                    return TransferManager.TransferReason.LeaveCity1;
                case Citizen.Wealth.High:
                    return TransferManager.TransferReason.LeaveCity2;
                default:
                    return TransferManager.TransferReason.LeaveCity0;
            }
        }

        public static void FindVisitPlace(uint citizenID, ushort sourceBuilding, TransferManager.TransferReason reason)
        {
            TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
            offer.Priority = Singleton<SimulationManager>.instance.m_randomizer.Int32(7u);
            offer.Citizen = citizenID;
            offer.Position = Singleton<BuildingManager>.instance.m_buildings.m_buffer[sourceBuilding].m_position;
            offer.Amount = 1;
            offer.Active = true;
            Singleton<TransferManager>.instance.AddIncomingOffer(reason, offer);
        }
    }
}
