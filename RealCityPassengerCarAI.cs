using ColossalFramework;
using System;
using System.Reflection;
using UnityEngine;

namespace RealCity
{
    public class RealCityPassengerCarAI
    {
        public bool CustomArriveAtDestination(ushort vehicleID, ref Vehicle vehicleData)
        {
            GetVehicleRunningTiming(vehicleID, ref vehicleData);

            BuildingManager instance = Singleton<BuildingManager>.instance;
            Building building = instance.m_buildings.m_buffer[(int)vehicleData.m_sourceBuilding];
            Building building1 = instance.m_buildings.m_buffer[(int)vehicleData.m_targetBuilding];
            BuildingInfo info = instance.m_buildings.m_buffer[(int)vehicleData.m_targetBuilding].Info;
            MainDataStore.vehical_flag[vehicleID] = false;
            var inst = Singleton<PassengerCarAI>.instance;
            var Method = typeof(PassengerCarAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance , null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType()}, null);
            //if(Method == null)
            //{
            //    DebugLog.LogToFileOnly("call PassengerCarAI.ArriveAtTarget failed, please check");
            //    return false;
            //}
            Vehicle A = vehicleData;
            ushort B = vehicleID;
            object[] parameters = new object[] { B,A };
            bool return_value = (bool)Method.Invoke(inst, parameters);
            vehicleData = (Vehicle)parameters[1];
            return return_value;
            //return false;
        }

        public void EnterTollRoad(ushort vehicle, ref Vehicle vehicleData, ushort buildingID, ushort segmentID, int basePrice)
        {
            if (buildingID != 0)
            {
                int num = basePrice;
                BuildingManager instance = Singleton<BuildingManager>.instance;
                DistrictManager instance2 = Singleton<DistrictManager>.instance;
                byte district = instance2.GetDistrict(instance.m_buildings.m_buffer[(int)buildingID].m_position);
                DistrictPolicies.CityPlanning cityPlanningPolicies = instance2.m_districts.m_buffer[(int)district].m_cityPlanningPolicies;
                if ((cityPlanningPolicies & DistrictPolicies.CityPlanning.AutomatedToll) != DistrictPolicies.CityPlanning.None)
                {
                    num = (num * 70 + Singleton<SimulationManager>.instance.m_randomizer.Int32(100u)) / 100;
                    District[] expr_82_cp_0 = instance2.m_districts.m_buffer;
                    byte expr_82_cp_1 = district;
                    expr_82_cp_0[(int)expr_82_cp_1].m_cityPlanningPoliciesEffect = (expr_82_cp_0[(int)expr_82_cp_1].m_cityPlanningPoliciesEffect | DistrictPolicies.CityPlanning.AutomatedToll);
                }
                else
                {
                    vehicleData.m_flags2 |= Vehicle.Flags2.EndStop;
                }
                //if (MainDataStore.vehical_flag[vehicle] == false && vehicleData.m_flags.IsFlagSet(Vehicle.Flags.DummyTraffic))
                //{
                //    MainDataStore.vehical_flag[vehicle] = true;
                    Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, num, ItemClass.Service.Vehicles, ItemClass.SubService.None, ItemClass.Level.Level1);
                //}
                instance.m_buildings.m_buffer[(int)buildingID].m_customBuffer1 = (ushort)Mathf.Min((int)(instance.m_buildings.m_buffer[(int)buildingID].m_customBuffer1 + 1), 65535);
            }
        }


        public void GetVehicleRunningTiming(ushort vehicleID, ref Vehicle vehicleData)
        {
            if (vehicleID > 16384)
            {
                DebugLog.LogToFileOnly("Error: vehicle ID greater than 16384");
            }
            
            CitizenManager instance2 = Singleton<CitizenManager>.instance;

            bool is_dummy = false;
            if (instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen0 != 0)
            {
                is_dummy = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen0].m_flags & Citizen.Flags.DummyTraffic) != Citizen.Flags.None);
            }
            if (instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen1 != 0)
            {
                is_dummy = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen1].m_flags & Citizen.Flags.DummyTraffic) != Citizen.Flags.None);
            }
            if (instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen2 != 0)
            {
                is_dummy = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen2].m_flags & Citizen.Flags.DummyTraffic) != Citizen.Flags.None);
            }
            if (instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen3 != 0)
            {
                is_dummy = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen3].m_flags & Citizen.Flags.DummyTraffic) != Citizen.Flags.None);
            }
            if (instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen4 != 0)
            {
                is_dummy = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen4].m_flags & Citizen.Flags.DummyTraffic) != Citizen.Flags.None);
            }

            if (is_dummy)
            {
                //DebugLog.LogToFileOnly("feedthough traffic");
            }
            else
            {
                //DebugLog.LogToFileOnly(vehicleData.m_transferType.ToString() + vehicleData.m_sourceBuilding.ToString() + vehicleData.m_targetBuilding.ToString());
                //DebugLog.LogToFileOnly("finding a car, time " + comm_data.vehical_transfer_time[vehicleID].ToString());
                MainDataStore.temp_total_citizen_vehical_time = MainDataStore.temp_total_citizen_vehical_time + MainDataStore.vehical_transfer_time[vehicleID];
                if (vehicleData.m_citizenUnits != 0)
                {
                    bool is_tourist = false;
                    if (instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen0 != 0)
                    {
                        is_tourist = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen0].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None);
                    }
                    if (instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen1 != 0)
                    {
                        is_tourist = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen1].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None);
                    }
                    if (instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen2 != 0)
                    {
                        is_tourist = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen2].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None);
                    }
                    if (instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen3 != 0)
                    {
                        is_tourist = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen3].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None);
                    }
                    if (instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen4 != 0)
                    {
                        is_tourist = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen4].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None);
                    }
                    if (is_tourist == false)
                    {
                        //assume that 1 time will cost 5fen car oil money
                        MainDataStore.family_money[vehicleData.m_citizenUnits] = (float)(MainDataStore.family_money[vehicleData.m_citizenUnits] - MainDataStore.vehical_transfer_time[vehicleID]);
                        if (instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen0 != 0)
                        {
                            MainDataStore.citizen_money[instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen0] -= MainDataStore.vehical_transfer_time[vehicleID];
                        }
                        else if (instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen1 != 0)
                        {
                            MainDataStore.citizen_money[instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen1] -= MainDataStore.vehical_transfer_time[vehicleID];
                        }
                        else if (instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen2 != 0)
                        {
                            MainDataStore.citizen_money[instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen2] -= MainDataStore.vehical_transfer_time[vehicleID];
                        }
                        else if (instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen3 != 0)
                        {
                            MainDataStore.citizen_money[instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen3] -= MainDataStore.vehical_transfer_time[vehicleID];
                        }
                        else if (instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen4 != 0)
                        {
                            MainDataStore.citizen_money[instance2.m_units.m_buffer[vehicleData.m_citizenUnits].m_citizen4] -= MainDataStore.vehical_transfer_time[vehicleID];
                        }
                    }
                }
            }
        }
    }
}
