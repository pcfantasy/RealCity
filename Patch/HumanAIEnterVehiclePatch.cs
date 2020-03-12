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
                VehicleManager vehicleManager = Singleton<VehicleManager>.instance;
                ushort vehicleID = Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen].m_vehicle;
                if (vehicleID != 0)
                {
                    vehicleID = vehicleManager.m_vehicles.m_buffer[vehicleID].GetFirstVehicle(vehicleID);
                }
                if (vehicleID != 0)
                {
                    VehicleInfo info = vehicleManager.m_vehicles.m_buffer[vehicleID].Info;
                    int ticketPrice = info.m_vehicleAI.GetTicketPrice(vehicleID, ref vehicleManager.m_vehicles.m_buffer[vehicleID]);
                    if (ticketPrice != 0)
                    {
                        // NON-STOCK CODE START
                        CitizenManager citizenManager = Singleton<CitizenManager>.instance;
                        if ((citizenManager.m_citizens.m_buffer[citizenData.m_citizen].m_flags & Citizen.Flags.Tourist) == Citizen.Flags.None)
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
