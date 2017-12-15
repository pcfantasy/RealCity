using ColossalFramework;
using System;
using System.Reflection;

namespace RealCity
{
    public class pc_PassengerCarAI
    {
        public bool ArriveAtDestination_1 (ushort vehicleID, ref Vehicle vehicleData)
        {
            get_vehicle_running_timing(vehicleID, ref vehicleData);

            BuildingManager instance = Singleton<BuildingManager>.instance;
            Building building = instance.m_buildings.m_buffer[(int)vehicleData.m_sourceBuilding];
            Building building1 = instance.m_buildings.m_buffer[(int)vehicleData.m_targetBuilding];
            BuildingInfo info = instance.m_buildings.m_buffer[(int)vehicleData.m_targetBuilding].Info;

            if (comm_data.crasy_task)
            {
                if (building.m_flags.IsFlagSet(Building.Flags.Untouchable))
                {
                    if (building1.m_flags.IsFlagSet(Building.Flags.Untouchable))
                    {
                        comm_data.task_num--;
                    }
                }
            }

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
        public void get_vehicle_running_timing(ushort vehicleID, ref Vehicle vehicleData)
        {
            if (vehicleID > 16384)
            {
                DebugLog.LogToFileOnly("Error: vehicle ID greater than 16384");
            }
            BuildingManager instance = Singleton<BuildingManager>.instance;
            CitizenManager instance2 = Singleton<CitizenManager>.instance;
            bool flag1 = instance.m_buildings.m_buffer[(int)vehicleData.m_sourceBuilding].m_flags.IsFlagSet(Building.Flags.Untouchable);
			bool flag2 = instance.m_buildings.m_buffer[(int)vehicleData.m_targetBuilding].m_flags.IsFlagSet(Building.Flags.Untouchable);
            if (flag1 || flag2)
            {
                DebugLog.LogToFileOnly("Moving in or leaving car, leave it away");
            }
            else
            {
                //DebugLog.LogToFileOnly("finding a car, time " + comm_data.vehical_transfer_time[vehicleID].ToString());
                comm_data.temp_total_citizen_vehical_time = comm_data.temp_total_citizen_vehical_time + comm_data.vehical_transfer_time[vehicleID];
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
                        comm_data.citizen_money[vehicleData.m_citizenUnits] = (short)(comm_data.citizen_money[vehicleData.m_citizenUnits] - comm_data.vehical_transfer_time[vehicleID] * 5);
                    }
                }
            }
        }
    }
}
