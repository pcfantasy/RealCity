using ColossalFramework;
using RealCity.Util;
using System;
using System.Reflection;
using UnityEngine;

namespace RealCity.CustomAI
{
    public class RealCityPassengerCarAI: PassengerCarAI
    {
        public void PassengerCarAIArriveAtTargetForRealGasStationPre(ushort vehicleID, ref Vehicle data)
        {
            DebugLog.LogToFileOnly("Error: Should be detour by RealGasStation @ PassengerCarAIArriveAtTargetForRealGasStationPre");
        }

        public bool CustomArriveAtDestination(ushort vehicleID, ref Vehicle vehicleData)
        {
            //RealGasStation Mod related
            if (vehicleData.m_transferType == 112 && Loader.isRealGasStationRunning)
            {
                PassengerCarAIArriveAtTargetForRealGasStationPre(vehicleID, ref vehicleData);
                return true;
            }

            GetVehicleRunningTiming(vehicleID, ref vehicleData);

            BuildingManager instance = Singleton<BuildingManager>.instance;
            Building building = instance.m_buildings.m_buffer[(int)vehicleData.m_sourceBuilding];
            Building building1 = instance.m_buildings.m_buffer[(int)vehicleData.m_targetBuilding];
            BuildingInfo info = instance.m_buildings.m_buffer[(int)vehicleData.m_targetBuilding].Info;
            MainDataStore.isVehicleCharged[vehicleID] = false;
            var inst = Singleton<PassengerCarAI>.instance;
            var Method = typeof(PassengerCarAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance , null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType()}, null);
            Vehicle A = vehicleData;
            ushort B = vehicleID;
            object[] parameters = new object[] { B,A };
            bool return_value = (bool)Method.Invoke(inst, parameters);
            vehicleData = (Vehicle)parameters[1];
            return return_value;
        }

        public override void EnterTollRoad(ushort vehicle, ref Vehicle vehicleData, ushort buildingID, ushort segmentID, int basePrice)
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
                Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, num, ItemClass.Service.Vehicles, ItemClass.SubService.None, ItemClass.Level.Level1);
                instance.m_buildings.m_buffer[(int)buildingID].m_customBuffer1 = (ushort)Mathf.Min((int)(instance.m_buildings.m_buffer[(int)buildingID].m_customBuffer1 + 1), 65535);
            }
        }

        private ushort GetDriverInstance(ushort vehicleID, ref Vehicle data)
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

        public void GetVehicleRunningTiming(ushort vehicleID, ref Vehicle vehicleData)
        {
            if (vehicleID > 16384)
            {
                DebugLog.LogToFileOnly("Error: vehicle ID greater than 16384");
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
