using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCity
{
    public class pc_VehicleAI
    {
        // VehicleAI
        public static ushort prevehicleID = 0;
        public static byte[] save_data = new byte[2];
        public static byte[] load_data = new byte[2];

        public static void save()
        {
            int i = 0;
            saveandrestore.save_ushort(ref i, prevehicleID, ref save_data);
        }

        public static void load()
        {
            int i = 0;
            prevehicleID = saveandrestore.load_ushort(ref i, load_data);
        }

        public virtual void SimulationStep(ushort vehicleID, ref Vehicle vehicleData, ref Vehicle.Frame frameData, ushort leaderID, ref Vehicle leaderData, int lodPhysics)
        {
            vehicle_status(vehicleID, ref vehicleData);
        }

        public void vehicle_status(ushort vehicleID, ref Vehicle vehicleData)
        {

            Vehicle vehicle = vehicleData;
            if ((vehicle.Info.m_vehicleType == VehicleInfo.VehicleType.Car) && (vehicle.Info.m_class.m_subService != ItemClass.SubService.PublicTransportTaxi))
            {
                if ((vehicle.m_flags & Vehicle.Flags.Transition) != (Vehicle.Flags)0)
                {
                    comm_data.vehical_transfer_time[vehicleID] = (ushort)(comm_data.vehical_transfer_time[vehicleID] + 1);
                }
            }

            int i;
            if (prevehicleID < vehicleID)
            {
                for (i = (int)(prevehicleID + 1); i < vehicleID; i++)
                {
                    if (comm_data.vehical_transfer_time[i] != 0)
                    {
                        comm_data.vehical_transfer_time[i] = 0;
                    }
                }
            }
            prevehicleID = vehicleID;
        }
    }
}
