using ColossalFramework;
using RealCity.Util;
using UnityEngine;


namespace RealCity.CustomAI
{
    public class RealCityTollBooth: PlayerBuildingAI
    {
        public override void EnterBuildingSegment(ushort buildingID, ref Building data, ushort segmentID, byte offset, InstanceID itemID)
        {
            base.EnterBuildingSegment(buildingID, ref data, segmentID, offset, itemID);
            if ((data.m_flags & Building.Flags.Active) != Building.Flags.None)
            {
                bool isForFuel = false;
                ushort vehicle1 = itemID.Vehicle;
                if (vehicle1 != 0)
                {
                    VehicleManager instance = Singleton<VehicleManager>.instance;
                    Vehicle vehicleData = instance.m_vehicles.m_buffer[vehicle1];
                    if (vehicleData.m_transferType == 112)
                    {
                        isForFuel = true;
                    }
                }

                if (!isForFuel)
                {
                    ushort vehicle = itemID.Vehicle;
                    if (vehicle != 0)
                    {
                        VehicleManager instance = Singleton<VehicleManager>.instance;
                        VehicleInfo info = instance.m_vehicles.m_buffer[vehicle].Info;
                        if (info.m_vehicleAI is CargoTruckAI && (instance.m_vehicles.m_buffer[vehicle].m_flags.IsFlagSet(Vehicle.Flags.DummyTraffic)))
                        {
                            if (!MainDataStore.isVehicleCharged[vehicle])
                            {
                                MainDataStore.isVehicleCharged[vehicle] = true;
                                EnterTollRoad(vehicle, ref instance.m_vehicles.m_buffer[vehicle], buildingID, segmentID, (data.m_education1 * 20));
                            }
                        }
                        else if (info.m_vehicleAI is PassengerCarAI)
                        {
                            bool is_tourist = false;
                            bool is_dummy = false;
                            if (instance.m_vehicles.m_buffer[vehicle].m_citizenUnits != 0)
                            {
                                CitizenManager instance2 = Singleton<CitizenManager>.instance;
                                if (instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen0 != 0)
                                {
                                    is_tourist = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen0].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None);
                                    is_dummy = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen0].m_flags & Citizen.Flags.DummyTraffic) != Citizen.Flags.None);
                                }
                                if (instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen1 != 0)
                                {
                                    is_tourist = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen1].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None);
                                    is_dummy = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen1].m_flags & Citizen.Flags.DummyTraffic) != Citizen.Flags.None);
                                }
                                if (instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen2 != 0)
                                {
                                    is_tourist = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen2].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None);
                                    is_dummy = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen2].m_flags & Citizen.Flags.DummyTraffic) != Citizen.Flags.None);

                                }
                                if (instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen3 != 0)
                                {
                                    is_tourist = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen3].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None);
                                    is_dummy = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen3].m_flags & Citizen.Flags.DummyTraffic) != Citizen.Flags.None);
                                }
                                if (instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen4 != 0)
                                {
                                    is_tourist = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen4].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None);
                                    is_dummy = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen4].m_flags & Citizen.Flags.DummyTraffic) != Citizen.Flags.None);
                                }
                            }
                            if (!MainDataStore.isVehicleCharged[vehicle] && (is_dummy || is_tourist))
                            {
                                MainDataStore.isVehicleCharged[vehicle] = true;
                                EnterTollRoad(vehicle, ref instance.m_vehicles.m_buffer[vehicle], buildingID, segmentID, (data.m_education1 * 10));
                            }
                        }
                    }
                }
            }
        }

        public void EnterTollRoad(ushort vehicle, ref Vehicle vehicleData, ushort buildingID, ushort segmentID, int basePrice)
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
                if (vehicleData.Info.m_vehicleAI is CargoTruckAI)
                {
                    Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, num, ItemClass.Service.Vehicles, ItemClass.SubService.None, ItemClass.Level.Level2);
                }
                else
                {
                    Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, num, ItemClass.Service.Vehicles, ItemClass.SubService.None, ItemClass.Level.Level1);
                }
                instance.m_buildings.m_buffer[buildingID].m_customBuffer1 = (ushort)Mathf.Min((instance.m_buildings.m_buffer[buildingID].m_customBuffer1 + 1), 65535);
            }
        }
    }
}
