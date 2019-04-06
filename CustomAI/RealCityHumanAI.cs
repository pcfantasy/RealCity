using System;
using ColossalFramework;
using RealCity.Util;

namespace RealCity.CustomAI
{
    public class RealCityHumanAI : HumanAI
    {
        public override void EnterParkArea(ushort instanceID, ref CitizenInstance citizenData, byte park, ushort gateID)
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
                        MainDataStore.citizenMoney[citizenData.m_citizen] -= ticketPrice;
                        Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, ticketPrice, info.m_class);
                    }
                    else
                    {
                        MainDataStore.citizenMoney[citizenData.m_citizen] -= ticketPrice;
                        //Negetive price to help identify tourist and resident.
                        Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, -ticketPrice, info.m_class);
                    }
                    /// NON-STOCK CODE END ///
                    DistrictPark[] expr_6C_cp_0 = instance.m_parks.m_buffer;
                    expr_6C_cp_0[park].m_tempTicketIncome = expr_6C_cp_0[park].m_tempTicketIncome + (uint)ticketPrice;
                }
            }
        }

        protected virtual bool CustomEnterVehicle(ushort instanceID, ref CitizenInstance citizenData)
        {
            citizenData.m_flags &= ~CitizenInstance.Flags.EnteringVehicle;
            citizenData.Unspawn(instanceID);
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
                            MainDataStore.citizenMoney[citizen] = (MainDataStore.citizenMoney[citizen] - (ticketPrice));
                        }
                        else
                        {
                            if (MainDataStore.citizenMoney[citizen] < ticketPrice)
                            {
                                ticketPrice = (MainDataStore.citizenMoney[citizen] > 0) ? (int)MainDataStore.citizenMoney[citizen] + 1 : 1;
                                MainDataStore.citizenMoney[citizen] = (MainDataStore.citizenMoney[citizen] - ticketPrice);
                            }
                            else
                            {
                                MainDataStore.citizenMoney[citizen] = (MainDataStore.citizenMoney[citizen] - (ticketPrice));
                            }
                        }
                        /// NON-STOCK CODE END ///
                        Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, ticketPrice, info.m_class);
                    }
                }
            }
            return false;
        }

        public static byte[] watingPathTime = new byte[65536];
        public static void HumanAISimulationStepPostFix(ushort instanceID, ref CitizenInstance citizenData, ref CitizenInstance.Frame frameData, bool lodPhysics)
        {
            if (citizenData.m_flags.IsFlagSet(CitizenInstance.Flags.WaitingPath))
            {
                if (citizenData.m_path != 0)
                {
                    watingPathTime[instanceID]++;
                }
                if (watingPathTime[instanceID] > 192)
                {
                    ushort building = 0;
                    building = citizenData.m_sourceBuilding;
                    var buildingData = Singleton<BuildingManager>.instance.m_buildings.m_buffer[building];
                    DebugLog.LogToFileOnly("DebugInfo: Stuck citizen target building m_class is " + buildingData.Info.m_class.ToString());
                    DebugLog.LogToFileOnly("DebugInfo: Stuck citizen target name is " + buildingData.Info.name.ToString());
                    DebugLog.LogToFileOnly("DebugInfo: Stuck citizen flag is " + citizenData.m_flags.ToString());
                    watingPathTime[instanceID] = 0;
                    Singleton<PathManager>.instance.ReleasePath(citizenData.m_path);
                    citizenData.m_path = 0u;
                    citizenData.m_flags = (citizenData.m_flags & ~(CitizenInstance.Flags.WaitingPath | CitizenInstance.Flags.WaitingTransport | CitizenInstance.Flags.EnteringVehicle | CitizenInstance.Flags.BoredOfWaiting | CitizenInstance.Flags.WaitingTaxi));
                }
            }
            else
            {
                watingPathTime[instanceID] = 0;
            }
        }
    }
}
