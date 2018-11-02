using ColossalFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RealCity
{
    public class RealCityTollBooth: PlayerBuildingAI
    {
        public override void EnterBuildingSegment(ushort buildingID, ref Building data, ushort segmentID, byte offset, InstanceID itemID)
        {
            base.EnterBuildingSegment(buildingID, ref data, segmentID, offset, itemID);
            if ((data.m_flags & Building.Flags.Active) != Building.Flags.None)
            {
                ushort vehicle = itemID.Vehicle;
                if (vehicle != 0)
                {
                    VehicleManager instance = Singleton<VehicleManager>.instance;
                    VehicleInfo info = instance.m_vehicles.m_buffer[(int)vehicle].Info;
                    if (info.m_vehicleAI is CargoTruckAI && (instance.m_vehicles.m_buffer[(int)vehicle].m_flags.IsFlagSet(Vehicle.Flags.DummyTraffic) || instance.m_vehicles.m_buffer[(int)vehicle].m_flags.IsFlagSet(Vehicle.Flags.Importing)))
                    {
                        if (!MainDataStore.vehical_flag[vehicle])
                        {
                            //DebugLog.LogToFileOnly("cargo tickprice = " + data.m_education1.ToString());
                            MainDataStore.vehical_flag[vehicle] = true;
                            this.EnterTollRoad(vehicle, ref instance.m_vehicles.m_buffer[(int)vehicle], buildingID, segmentID, (int)(data.m_education1 * 100));
                        }
                    }
                    else if (info.m_vehicleAI is PassengerCarAI && instance.m_vehicles.m_buffer[(int)vehicle].m_flags.IsFlagSet(Vehicle.Flags.DummyTraffic))
                    {
                        if (!MainDataStore.vehical_flag[vehicle])
                        {
                            //DebugLog.LogToFileOnly("PassengerCar tickprice = " + data.m_education1.ToString());
                            MainDataStore.vehical_flag[vehicle] = true;
                            this.EnterTollRoad(vehicle, ref instance.m_vehicles.m_buffer[(int)vehicle], buildingID, segmentID, (int)(data.m_education1 * 50));
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
                if (vehicleData.Info.m_vehicleAI is CargoTruckAI)
                {
                    Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, num, ItemClass.Service.Vehicles, ItemClass.SubService.None, ItemClass.Level.Level2);
                }
                else
                {
                    Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, num, ItemClass.Service.Vehicles, ItemClass.SubService.None, ItemClass.Level.Level1);
                }
                instance.m_buildings.m_buffer[(int)buildingID].m_customBuffer1 = (ushort)Mathf.Min((int)(instance.m_buildings.m_buffer[(int)buildingID].m_customBuffer1 + 1), 65535);
            }
        }
    }
}
