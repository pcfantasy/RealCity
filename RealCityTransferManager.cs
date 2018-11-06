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

        public static void Init()
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
        //private static bool _init = false;


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

        /*public void AddOutgoingOffer(TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (!_init)
            {
                _init = true;
                Init();
            }


            if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[offer.Building].m_flags.IsFlagSet(Building.Flags.IncomingOutgoing))
            {
                //Hell mode no import
                if(comm_data.isHellMode)
                {
                    
                    if(Singleton<UnlockManager>.instance.Unlocked(ItemClass.SubService.IndustrialFarming))
                    {
                        if (material == TransferManager.TransferReason.Food || material == TransferManager.TransferReason.Grain)
                        {
                            DebugLog.LogToFileOnly("hell mode, no import");
                            return;
                        }
                    }

                    if (Singleton<UnlockManager>.instance.Unlocked(ItemClass.SubService.IndustrialForestry))
                    {
                        if (material == TransferManager.TransferReason.Lumber || material == TransferManager.TransferReason.Logs)
                        {
                            DebugLog.LogToFileOnly("hell mode, no import");
                            return;
                        }
                    }

                    if (Singleton<UnlockManager>.instance.Unlocked(ItemClass.SubService.IndustrialOil))
                    {
                        if (material == TransferManager.TransferReason.Oil || material == TransferManager.TransferReason.Petrol)
                        {
                            DebugLog.LogToFileOnly("hell mode, no import");
                            return;
                        }
                    }

                    if (Singleton<UnlockManager>.instance.Unlocked(ItemClass.SubService.IndustrialOre))
                    {
                        if (material == TransferManager.TransferReason.Coal || material == TransferManager.TransferReason.Ore)
                        {
                            DebugLog.LogToFileOnly("hell mode, no import");
                            return;
                        }
                    }
                } else
                {
                    if (Singleton<UnlockManager>.instance.Unlocked(ItemClass.SubService.IndustrialFarming))
                    {
                        if (material == TransferManager.TransferReason.Food || material == TransferManager.TransferReason.Grain)
                        {
                            DebugLog.LogToFileOnly("not hell mode, can import");
                            return;
                        }
                    }

                    if (Singleton<UnlockManager>.instance.Unlocked(ItemClass.SubService.IndustrialForestry))
                    {
                        if (material == TransferManager.TransferReason.Lumber || material == TransferManager.TransferReason.Logs)
                        {
                            DebugLog.LogToFileOnly("not hell mode, can import");
                            return;
                        }
                    }

                    if (Singleton<UnlockManager>.instance.Unlocked(ItemClass.SubService.IndustrialOil))
                    {
                        if (material == TransferManager.TransferReason.Oil || material == TransferManager.TransferReason.Petrol)
                        {
                            DebugLog.LogToFileOnly("not hell mode, can import");
                            return;
                        }
                    }

                    if (Singleton<UnlockManager>.instance.Unlocked(ItemClass.SubService.IndustrialOre))
                    {
                        if (material == TransferManager.TransferReason.Coal || material == TransferManager.TransferReason.Ore)
                        {
                            DebugLog.LogToFileOnly("not hell mode, can import");
                            return;
                        }
                    }
                }
            }

            if (!comm_data.isPetrolsGettedFinal)
            {
                if(material == TransferManager.TransferReason.Garbage || material == TransferManager.TransferReason.GarbageMove)
                {
                    return;
                }

                if (material == TransferManager.TransferReason.Fire || material == TransferManager.TransferReason.Fire2)
                {
                    return;
                }

                if (material == TransferManager.TransferReason.SickMove || material == TransferManager.TransferReason.Sick2 || material == TransferManager.TransferReason.Sick)
                {
                    return;
                }

                if (material == TransferManager.TransferReason.Dead || material == TransferManager.TransferReason.DeadMove)
                {
                    return;
                }

                if (material == TransferManager.TransferReason.RoadMaintenance || material == TransferManager.TransferReason.Taxi)
                {
                    return;
                }

                if (material == TransferManager.TransferReason.Snow || material == TransferManager.TransferReason.SnowMove)
                {
                    return;
                }

                if (material == TransferManager.TransferReason.Crime || material == TransferManager.TransferReason.CriminalMove)
                {
                    return;
                }

                if (material == TransferManager.TransferReason.Bus || material == TransferManager.TransferReason.TouristBus)
                {
                    return;
                }
            }


            for (int priority = offer.Priority; priority >= 0; --priority)
            {
                int index = (int)material * 8 + priority;
                int count = _outgoingCount[index];
                if (count < 256)
                {
                    //here we caculate needs
                    _outgoingOffers[index * 256 + count] = offer;
                    _outgoingCount[index] = (ushort)(count + 1);
                    _outgoingAmount[(int)material] += offer.Amount;
                    return;
                }
            }
        }*/

        /*public static void AddIncomingOffer(TransferManager manager, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            // note: do NOT just use 
            //   DebugOutputPanel.AddMessage
            // here. This method is called so frequently that it will actually crash the game.
            if (!_init)
            {
                _init = true;
                Init();
            }


            //DebugLog.LogToFileOnly("AddIncomingOffer");
            //if (material == TransferManager.TransferReason.DummyCar)
            //{
                //DebugLog.LogToFileOnly("AddIncomingOffer, DummyCar");
            //}

            BuildingManager instance1 = Singleton<BuildingManager>.instance;
            if (instance1.m_buildings.m_buffer[offer.Building].Info.m_class.m_service == ItemClass.Service.Commercial)
            {
                if (material == TransferManager.TransferReason.Petrol)
                {
                    MainDataStore.building_buffer3[offer.Building] = 123;  //a flag
                }
                else if (material == TransferManager.TransferReason.Food)
                {
                    MainDataStore.building_buffer3[offer.Building] = 124;
                }
                else if (material == TransferManager.TransferReason.Goods)
                {
                    MainDataStore.building_buffer3[offer.Building] = 125;
                }
                else if (material == TransferManager.TransferReason.Lumber)
                {
                    MainDataStore.building_buffer3[offer.Building] = 126;
                }
                else if (material == TransferManager.TransferReason.LuxuryProducts)
                {

                }
                else
                {
                    if (material == TransferManager.TransferReason.Oil || material == TransferManager.TransferReason.Grain || material == TransferManager.TransferReason.Logs || material == TransferManager.TransferReason.Ore)
                    {
                        //DebugLog.LogToFileOnly("find speical incoming request for comm building" + material.ToString());
                    }
                }
            }

            for (int priority = offer.Priority; priority >= 0; --priority)
            {
                int index = (int)material * 8 + priority;
                int count = _incomingCount[index];
                if (count < 256)
                {
                    //here we caculate needs
                    _incomingOffers[index * 256 + count] = offer;
                    _incomingCount[index] = (ushort)(count + 1);
                    _incomingAmount[(int)material] += offer.Amount;
                    return;
                }
            }
        }*/

    }//end publi
}//end naming space 