using ColossalFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealCity
{
    public class pc_PassengerCarAI
    {
        public bool ArriveAtDestination_1 (ushort vehicleID, ref Vehicle vehicleData)
        {
            get_vehicle_running_timing(vehicleID, ref vehicleData);
            var inst = Singleton<PassengerCarAI>.instance;
            var Method = typeof(PassengerCarAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance , null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType()}, null);
            //if(Method == null)
            //{
            //    DebugLog.LogToFileOnly("call PassengerCarAI.ArriveAtTarget failed, please check");
            //    return false;
            //}
            Vehicle A = default(Vehicle);
            ushort B = 0;
            object[] parameters = new object[] { B,A };
            bool return_value = (bool)Method.Invoke(inst, parameters);
            vehicleData = (Vehicle)parameters[1];
            return return_value;
            //return false;
        }
        public void get_vehicle_running_timing(ushort vehicleID, ref Vehicle vehicleData)
        {
            BuildingManager instance = Singleton<BuildingManager>.instance;
            bool flag1 = instance.m_buildings.m_buffer[(int)vehicleData.m_sourceBuilding].m_flags.IsFlagSet(Building.Flags.Untouchable);
			bool flag2 = instance.m_buildings.m_buffer[(int)vehicleData.m_targetBuilding].m_flags.IsFlagSet(Building.Flags.Untouchable);
            if (flag1 || flag2)
            {
                DebugLog.LogToFileOnly("Moving in or leaving car, leave it away");
            }
            else
            {
                comm_data.temp_total_citizen_vehical_time = comm_data.temp_total_citizen_vehical_time + comm_data.vehical_transfer_time[vehicleID];
            }
        }

    }
}
