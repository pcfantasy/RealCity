using System;
using ColossalFramework;
using System.Reflection;

namespace RealCity.CustomManager
{
    public class RealCityTransferManager
    {
        private void StartTransfer(TransferManager.TransferReason material, TransferManager.TransferOffer offerOut, TransferManager.TransferOffer offerIn, int delta)
        {
            bool active = offerIn.Active;
            bool active2 = offerOut.Active;
            if (active && offerIn.Vehicle != 0)
            {
                Array16<Vehicle> vehicles = Singleton<VehicleManager>.instance.m_vehicles;
                ushort vehicle = offerIn.Vehicle;
                VehicleInfo info = vehicles.m_buffer[(int)vehicle].Info;
                offerOut.Amount = delta;
                info.m_vehicleAI.StartTransfer(vehicle, ref vehicles.m_buffer[(int)vehicle], material, offerOut);
            }
            else if (active2 && offerOut.Vehicle != 0)
            {
                Array16<Vehicle> vehicles2 = Singleton<VehicleManager>.instance.m_vehicles;
                ushort vehicle2 = offerOut.Vehicle;
                VehicleInfo info2 = vehicles2.m_buffer[(int)vehicle2].Info;
                offerIn.Amount = delta;
                info2.m_vehicleAI.StartTransfer(vehicle2, ref vehicles2.m_buffer[(int)vehicle2], material, offerIn);
            }
            else if (active && offerIn.Citizen != 0u)
            {
                Array32<Citizen> citizens = Singleton<CitizenManager>.instance.m_citizens;
                uint citizen = offerIn.Citizen;
                CitizenInfo citizenInfo = citizens.m_buffer[(int)((UIntPtr)citizen)].GetCitizenInfo(citizen);
                if (citizenInfo != null)
                {
                    offerOut.Amount = delta;
                    citizenInfo.m_citizenAI.StartTransfer(citizen, ref citizens.m_buffer[(int)((UIntPtr)citizen)], material, offerOut);
                }
            }
            else if (active2 && offerOut.Citizen != 0u)
            {
                Array32<Citizen> citizens2 = Singleton<CitizenManager>.instance.m_citizens;
                uint citizen2 = offerOut.Citizen;
                CitizenInfo citizenInfo2 = citizens2.m_buffer[(int)((UIntPtr)citizen2)].GetCitizenInfo(citizen2);
                if (citizenInfo2 != null)
                {
                    offerIn.Amount = delta;
                    // NON-STOCK CODE START
                    // Remove cotenancy, otherwise we can not caculate family money
                    bool flag2 = (material == TransferManager.TransferReason.Single0 || material == TransferManager.TransferReason.Single1 || material == TransferManager.TransferReason.Single2 || material == TransferManager.TransferReason.Single3 || material == TransferManager.TransferReason.Single0B || material == TransferManager.TransferReason.Single1B || material == TransferManager.TransferReason.Single2B || material == TransferManager.TransferReason.Single3B);
                    bool flag = (citizenInfo2.m_citizenAI is ResidentAI) && (Singleton<BuildingManager>.instance.m_buildings.m_buffer[offerIn.Building].Info.m_class.m_service == ItemClass.Service.Residential);
                    if (flag && flag2)
                    {
                       if (material == TransferManager.TransferReason.Single0 || material == TransferManager.TransferReason.Single0B)
                        {
                            material = TransferManager.TransferReason.Family0;
                        }
                        else if (material == TransferManager.TransferReason.Single1 || material == TransferManager.TransferReason.Single1B)
                        {
                            material = TransferManager.TransferReason.Family1;
                        }
                        else if (material == TransferManager.TransferReason.Single2 || material == TransferManager.TransferReason.Single2B)
                        {
                            material = TransferManager.TransferReason.Family2;
                        }
                        else if (material == TransferManager.TransferReason.Single3 || material == TransferManager.TransferReason.Single3B)
                        {
                            material = TransferManager.TransferReason.Family3;
                        }
                        citizenInfo2.m_citizenAI.StartTransfer(citizen2, ref citizens2.m_buffer[(int)((UIntPtr)citizen2)], material, offerIn);
                    }
                    else
                    {
                        /// NON-STOCK CODE END ///
                        citizenInfo2.m_citizenAI.StartTransfer(citizen2, ref citizens2.m_buffer[(int)((UIntPtr)citizen2)], material, offerIn);
                    }
                }
            }
            else if (active2 && offerOut.Building != 0)
            {
                Array16<Building> buildings = Singleton<BuildingManager>.instance.m_buildings;
                ushort building = offerOut.Building;
                ushort building1 = offerIn.Building;
                BuildingInfo info3 = buildings.m_buffer[(int)building].Info;
                offerIn.Amount = delta;
                info3.m_buildingAI.StartTransfer(building, ref buildings.m_buffer[(int)building], material, offerIn);
            }
            else if (active && offerIn.Building != 0)
            {
                Array16<Building> buildings2 = Singleton<BuildingManager>.instance.m_buildings;
                ushort building2 = offerIn.Building;
                BuildingInfo info4 = buildings2.m_buffer[(int)building2].Info;
                offerOut.Amount = delta;
                info4.m_buildingAI.StartTransfer(building2, ref buildings2.m_buffer[(int)building2], material, offerOut);
            }
        }

    }//end publi
}//end naming space 