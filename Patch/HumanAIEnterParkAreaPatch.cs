using ColossalFramework;
using Harmony;
using RealCity.CustomData;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class HumanAIEnterParkAreaPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(HumanAI).GetMethod("EnterParkArea", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(byte), typeof(ushort) }, null);
        }
        public static bool Prefix(ushort instanceID, ref CitizenInstance citizenData, byte park, ushort gateID)
        {
            if (gateID != 0)
            {
                DistrictManager instance = Singleton<DistrictManager>.instance;
                BuildingManager instance2 = Singleton<BuildingManager>.instance;
                CitizenManager instance3 = Singleton<CitizenManager>.instance;
                // NON-STOCK CODE START
                int ticketPrice = instance.m_parks.m_buffer[park].GetTicketPrice() / MainDataStore.gameExpenseDivide;
                if (ticketPrice != 0)
                {
                    BuildingInfo info = instance2.m_buildings.m_buffer[gateID].Info;
                    ushort homeBuilding = instance3.m_citizens.m_buffer[(int)((UIntPtr)citizenData.m_citizen)].m_homeBuilding;
                    uint homeId = instance3.m_citizens.m_buffer[citizenData.m_citizen].GetContainingUnit(citizenData.m_citizen, instance2.m_buildings.m_buffer[homeBuilding].m_citizenUnits, CitizenUnit.Flags.Home);
                    if ((instance3.m_citizens.m_buffer[citizenData.m_citizen].m_flags & Citizen.Flags.Tourist) == Citizen.Flags.None)
                    {
                        CitizenData.citizenMoney[citizenData.m_citizen] -= ticketPrice;
                        Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, ticketPrice, info.m_class);
                    }
                    else
                    {
                        if (CitizenData.citizenMoney[citizenData.m_citizen] < ticketPrice)
                        {
                            ticketPrice = (CitizenData.citizenMoney[citizenData.m_citizen] > 0) ? (int)CitizenData.citizenMoney[citizenData.m_citizen] : 0;
                            CitizenData.citizenMoney[citizenData.m_citizen] = 0;
                        }
                        else
                        {
                            CitizenData.citizenMoney[citizenData.m_citizen] = (CitizenData.citizenMoney[citizenData.m_citizen] - ticketPrice);
                        }
                        //Negetive price to help identify tourist and resident.
                        Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, -ticketPrice, info.m_class);
                    }
                    /// NON-STOCK CODE END ///
                    DistrictPark[] expr_6C_cp_0 = instance.m_parks.m_buffer;
                    expr_6C_cp_0[park].m_tempTicketIncome = expr_6C_cp_0[park].m_tempTicketIncome + (uint)ticketPrice;
                }
            }
            return false;
        }
    }
}
