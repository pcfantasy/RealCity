using ColossalFramework;
using Harmony;
using RealCity.CustomData;
using System;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class HumanAIEnterVehiclePatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(HumanAI).GetMethod("EnterVehicle", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType() }, null);
        }
        public static void Prefix(ref CitizenInstance citizenData)
        {
            uint citizen = citizenData.m_citizen;
            if (citizen != 0u)
            {
                VehicleManager instance = Singleton<VehicleManager>.instance;
                ushort num = Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_vehicle;
                if (num != 0)
                {
                    num = instance.m_vehicles.m_buffer[num].GetFirstVehicle(num);
                }
                if (num != 0)
                {
                    VehicleInfo info = instance.m_vehicles.m_buffer[num].Info;
                    int ticketPrice = info.m_vehicleAI.GetTicketPrice(num, ref instance.m_vehicles.m_buffer[num]);
                    if (ticketPrice != 0)
                    {
                        // NON-STOCK CODE START
                        CitizenManager instance3 = Singleton<CitizenManager>.instance;
                        ushort homeBuilding = instance3.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_homeBuilding;
                        BuildingManager instance2 = Singleton<BuildingManager>.instance;
                        uint homeId = instance3.m_citizens.m_buffer[citizenData.m_citizen].GetContainingUnit(citizen, instance2.m_buildings.m_buffer[homeBuilding].m_citizenUnits, CitizenUnit.Flags.Home);
                        if ((Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenData.m_citizen].m_flags & Citizen.Flags.Tourist) == Citizen.Flags.None)
                        {
                            CitizenData.citizenMoney[citizen] = (CitizenData.citizenMoney[citizen] - (ticketPrice));
                        }
                        else
                        {
                            if (CitizenData.citizenMoney[citizen] < ticketPrice)
                            {
                                ticketPrice = (CitizenData.citizenMoney[citizen] > 0) ? (int)CitizenData.citizenMoney[citizen] + 1 : 1;
                                CitizenData.citizenMoney[citizen] = (CitizenData.citizenMoney[citizen] - ticketPrice);
                            }
                            else
                            {
                                CitizenData.citizenMoney[citizen] = (CitizenData.citizenMoney[citizen] - (ticketPrice));
                            }
                        }
                        /// NON-STOCK CODE END ///
                    }
                }
            }
        }
    }
}
