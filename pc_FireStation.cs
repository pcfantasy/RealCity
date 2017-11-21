
using ColossalFramework;
using ColossalFramework.DataBinding;
using System;
using UnityEngine;

namespace RealCity
{
    public class FireStationAI : PlayerBuildingAI
    {
        public override void StartTransfer(ushort buildingID, ref Building data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (material == TransferManager.TransferReason.Fire)
            {
                VehicleInfo randomVehicleInfo = Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref Singleton<SimulationManager>.instance.m_randomizer, this.m_info.m_class.m_service, this.m_info.m_class.m_subService, this.m_info.m_class.m_level, VehicleInfo.VehicleType.Car);
                if (randomVehicleInfo != null)
                {
                    Array16<Vehicle> vehicles = Singleton<VehicleManager>.instance.m_vehicles;
                    ushort num;
                    if (Singleton<VehicleManager>.instance.CreateVehicle(out num, ref Singleton<SimulationManager>.instance.m_randomizer, randomVehicleInfo, data.m_position, material, true, false))
                    {
                        randomVehicleInfo.m_vehicleAI.SetSource(num, ref vehicles.m_buffer[(int)num], buildingID);
                        randomVehicleInfo.m_vehicleAI.StartTransfer(num, ref vehicles.m_buffer[(int)num], material, offer);
                    }
                }
            }
            else
            {
                base.StartTransfer(buildingID, ref data, material, offer);
            }
        }
    }
}
