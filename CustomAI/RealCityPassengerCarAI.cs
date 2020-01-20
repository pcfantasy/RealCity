using ColossalFramework;
using RealCity.Util;
using System;
using UnityEngine;

namespace RealCity.CustomAI
{
    public class RealCityPassengerCarAI: PassengerCarAI
    {
        private bool CustomArriveAtTarget(ushort vehicleID, ref Vehicle data)
        {
            // NON-STOCK CODE START
            GetVehicleRunningTiming(vehicleID, ref data);
            // NON-STOCK CODE END
            if ((data.m_flags & Vehicle.Flags.Parking) != 0)
            {
                VehicleManager instance = Singleton<VehicleManager>.instance;
                CitizenManager instance2 = Singleton<CitizenManager>.instance;
                ushort driverInstance = GetDriverInstance(vehicleID, ref data);
                if (driverInstance != 0)
                {
                    uint citizen = instance2.m_instances.m_buffer[driverInstance].m_citizen;
                    if (citizen != 0u)
                    {
                        ushort parkedVehicle = instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_parkedVehicle;
                        if (parkedVehicle != 0)
                        {
                            Vehicle.Frame lastFrameData = data.GetLastFrameData();
                            instance.m_parkedVehicles.m_buffer[parkedVehicle].m_travelDistance = lastFrameData.m_travelDistance;
                            VehicleParked[] expr_A1_cp_0 = instance.m_parkedVehicles.m_buffer;
                            ushort expr_A1_cp_1 = parkedVehicle;
                            expr_A1_cp_0[expr_A1_cp_1].m_flags = (ushort)(expr_A1_cp_0[expr_A1_cp_1].m_flags & 65527);
                            InstanceID empty = InstanceID.Empty;
                            empty.Vehicle = vehicleID;
                            InstanceID empty2 = InstanceID.Empty;
                            empty2.ParkedVehicle = parkedVehicle;
                            Singleton<InstanceManager>.instance.ChangeInstance(empty, empty2);
                        }
                    }
                }
            }
            UnloadPassengers(vehicleID, ref data);
            if (data.m_targetBuilding == 0)
            {
                return true;
            }
            data.m_targetPos0 = Singleton<BuildingManager>.instance.m_buildings.m_buffer[data.m_targetBuilding].CalculateSidewalkPosition();
            data.m_targetPos0.w = 2f;
            data.m_targetPos1 = data.m_targetPos0;
            data.m_targetPos2 = data.m_targetPos0;
            data.m_targetPos3 = data.m_targetPos0;
            RemoveTarget(vehicleID, ref data);
            return true;
        }

        private void UnloadPassengers(ushort vehicleID, ref Vehicle data)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint num = data.m_citizenUnits;
            int num2 = 0;
            while (num != 0u)
            {
                uint nextUnit = instance.m_units.m_buffer[(int)((UIntPtr)num)].m_nextUnit;
                for (int i = 0; i < 5; i++)
                {
                    uint citizen = instance.m_units.m_buffer[(int)((UIntPtr)num)].GetCitizen(i);
                    if (citizen != 0u)
                    {
                        ushort instance2 = instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_instance;
                        if (instance2 != 0)
                        {
                            CitizenInfo info = instance.m_instances.m_buffer[instance2].Info;
                            info.m_citizenAI.SetCurrentVehicle(instance2, ref instance.m_instances.m_buffer[instance2], 0, 0u, data.m_targetPos0);
                        }
                    }
                }
                num = nextUnit;
                if (++num2 > 524288)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }
        }

        private void RemoveTarget(ushort vehicleID, ref Vehicle data)
        {
            if (data.m_targetBuilding != 0)
            {
                Singleton<BuildingManager>.instance.m_buildings.m_buffer[data.m_targetBuilding].RemoveGuestVehicle(vehicleID, ref data);
                data.m_targetBuilding = 0;
            }
        }

        public override void EnterTollRoad(ushort vehicle, ref Vehicle vehicleData, ushort buildingID, ushort segmentID, int basePrice)
        {
            if (buildingID != 0)
            {
                int num = basePrice;
                BuildingManager instance = Singleton<BuildingManager>.instance;
                DistrictManager instance2 = Singleton<DistrictManager>.instance;
                byte district = instance2.GetDistrict(instance.m_buildings.m_buffer[buildingID].m_position);
                DistrictPolicies.CityPlanning cityPlanningPolicies = instance2.m_districts.m_buffer[district].m_cityPlanningPolicies;
                if ((cityPlanningPolicies & DistrictPolicies.CityPlanning.AutomatedToll) != DistrictPolicies.CityPlanning.None)
                {
                    num = (num * 70 + Singleton<SimulationManager>.instance.m_randomizer.Int32(100u)) / 100;
                    District[] expr_82_cp_0 = instance2.m_districts.m_buffer;
                    byte expr_82_cp_1 = district;
                    expr_82_cp_0[expr_82_cp_1].m_cityPlanningPoliciesEffect = (expr_82_cp_0[expr_82_cp_1].m_cityPlanningPoliciesEffect | DistrictPolicies.CityPlanning.AutomatedToll);
                }
                else
                {
                    vehicleData.m_flags2 |= Vehicle.Flags2.EndStop;
                }
                Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, num, ItemClass.Service.Vehicles, ItemClass.SubService.None, ItemClass.Level.Level1);
                instance.m_buildings.m_buffer[buildingID].m_customBuffer1 = (ushort)Mathf.Min((instance.m_buildings.m_buffer[buildingID].m_customBuffer1 + 1), 65535);
            }
        }

        private static ushort GetDriverInstance(ushort vehicleID, ref Vehicle data)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint num = data.m_citizenUnits;
            int num2 = 0;
            while (num != 0u)
            {
                uint nextUnit = instance.m_units.m_buffer[(int)((UIntPtr)num)].m_nextUnit;
                for (int i = 0; i < 5; i++)
                {
                    uint citizen = instance.m_units.m_buffer[(int)((UIntPtr)num)].GetCitizen(i);
                    if (citizen != 0u)
                    {
                        ushort instance2 = instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_instance;
                        if (instance2 != 0)
                        {
                            return instance2;
                        }
                    }
                }
                num = nextUnit;
                if (++num2 > 524288)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }
            return 0;
        }

        public static void GetVehicleRunningTiming(ushort vehicleID, ref Vehicle vehicleData)
        {
            if (vehicleID > Singleton<VehicleManager>.instance.m_vehicles.m_size)
            {
                DebugLog.LogToFileOnly("Error: vehicle ID greater than " + Singleton<VehicleManager>.instance.m_vehicles.m_size.ToString());
            }
            
            CitizenManager citizenMan = Singleton<CitizenManager>.instance;
            ushort instanceID = GetDriverInstance(vehicleID, ref vehicleData);

            if (instanceID != 0)
            {
                uint citizenID = citizenMan.m_instances.m_buffer[instanceID].m_citizen;
                if (citizenID != 0)
                {
                    if (!(citizenMan.m_citizens.m_buffer[citizenID].m_flags.IsFlagSet(Citizen.Flags.DummyTraffic)))
                    {
                        MainDataStore.totalCitizenDrivingTime = MainDataStore.totalCitizenDrivingTime + MainDataStore.vehicleTransferTime[vehicleID];
                        if (vehicleData.m_citizenUnits != 0)
                        {
                            MainDataStore.citizenMoney[citizenID] -= MainDataStore.vehicleTransferTime[vehicleID];
                        } 
                    }
                }
            }
            MainDataStore.vehicleTransferTime[vehicleID] = 0;
        }
    }
}
