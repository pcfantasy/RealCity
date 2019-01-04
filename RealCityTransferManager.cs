using System;
using ColossalFramework;
using System.Reflection;

namespace RealCity
{
    public class RealCityTransferManager
    {
        /// <summary>
        /// Point of note: This is a static function whereas the original function uses __thiscall.
        /// On x64 machines only __fastcall is left, which means that the first parameter lives in
        /// RCX - which conveniently conincides with the register usually used for the this-ptr 
        /// (at least on Windows).
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="material"></param>
        /// <param name="offer"></param>
        /// 
        // TransferManager

        /*public static void Init()
        {
            DebugLog.LogToFileOnly("Init fake transfer manager");
            try
            {
                var inst = Singleton<TransferManager>.instance;
                var incomingCount = typeof(TransferManager).GetField("m_incomingCount", BindingFlags.NonPublic | BindingFlags.Instance);
                var incomingOffers = typeof(TransferManager).GetField("m_incomingOffers", BindingFlags.NonPublic | BindingFlags.Instance);
                var incomingAmount = typeof(TransferManager).GetField("m_incomingAmount", BindingFlags.NonPublic | BindingFlags.Instance);
                var outgoingCount = typeof(TransferManager).GetField("m_outgoingCount", BindingFlags.NonPublic | BindingFlags.Instance);
                var outgoingOffers = typeof(TransferManager).GetField("m_outgoingOffers", BindingFlags.NonPublic | BindingFlags.Instance);
                var outgoingAmount = typeof(TransferManager).GetField("m_outgoingAmount", BindingFlags.NonPublic | BindingFlags.Instance);
                if (inst == null)
                {
                    DebugLog.LogToFileOnly("No instance of TransferManager found!");
                    return;
                }
                _incomingCount = incomingCount.GetValue(inst) as ushort[];
                _incomingOffers = incomingOffers.GetValue(inst) as TransferManager.TransferOffer[];
                _incomingAmount = incomingAmount.GetValue(inst) as int[];
                _outgoingCount = outgoingCount.GetValue(inst) as ushort[];
                _outgoingOffers = outgoingOffers.GetValue(inst) as TransferManager.TransferOffer[];
                _outgoingAmount = outgoingAmount.GetValue(inst) as int[];
                if (_outgoingCount == null || _outgoingOffers == null || _outgoingAmount == null)
                {
                    DebugLog.LogToFileOnly("TransferManager Arrays are null");
                }
            }
            catch (Exception ex)
            {
                DebugLog.LogToFileOnly("TransferManager Exception: " + ex.Message);
            }
        }
        private static TransferManager.TransferOffer[] _outgoingOffers;
        private static ushort[] _outgoingCount;
        private static int[] _outgoingAmount;
        private static TransferManager.TransferOffer[] _incomingOffers;
        private static ushort[] _incomingCount;
        private static int[] _incomingAmount;
        //private static bool _init = false;*/


        public void StartSpecialBuildingTransfer(ushort buildingID, ref Building data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            //DebugLog.LogToFileOnly("find valid SpecialBuilding");
            VehicleInfo vehicleInfo = null;
            if (material == (TransferManager.TransferReason)110)
            {
                //DebugLog.LogToFileOnly("find valid construction resource transfer");
                vehicleInfo = Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref Singleton<SimulationManager>.instance.m_randomizer, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialForestry, ItemClass.Level.Level1);
            }
            else if (material == (TransferManager.TransferReason)111)
            {
                //DebugLog.LogToFileOnly("find valid operation resource transfer");
                vehicleInfo = Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref Singleton<SimulationManager>.instance.m_randomizer, ItemClass.Service.Industrial, ItemClass.SubService.IndustrialFarming, ItemClass.Level.Level1);
            }


            if (vehicleInfo != null)
            {
                //DebugLog.LogToFileOnly("find valid vehicleInfo");
                Array16<Vehicle> vehicles = Singleton<VehicleManager>.instance.m_vehicles;
                ushort num16;
                if (Singleton<VehicleManager>.instance.CreateVehicle(out num16, ref Singleton<SimulationManager>.instance.m_randomizer, vehicleInfo, data.m_position, material, false, true))
                {
                    //DebugLog.LogToFileOnly("find valid CreateVehicle");
                    vehicleInfo.m_vehicleAI.SetSource(num16, ref vehicles.m_buffer[(int)num16], buildingID);
                    vehicleInfo.m_vehicleAI.StartTransfer(num16, ref vehicles.m_buffer[(int)num16], material, offer);
                    ushort building4 = offer.Building;
                    if (building4 != 0)
                    {
                        int amount;
                        int num17;
                        vehicleInfo.m_vehicleAI.GetSize(num16, ref vehicles.m_buffer[(int)num16], out amount, out num17);
                    }
                }
            }
        }

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
                    //new added begin
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
                        //new added end
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
                if (RealCityEconomyExtension.IsSpecialBuilding(building) && Loader.isRealConstructionRunning)
                {
                    StartSpecialBuildingTransfer(building, ref buildings.m_buffer[(int)building], material, offerIn);
                }
                else
                {
                    info3.m_buildingAI.StartTransfer(building, ref buildings.m_buffer[(int)building], material, offerIn);
                }
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