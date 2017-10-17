using System;
using System.Reflection;
using ColossalFramework;

namespace RealCity
{
    public static class pc_TransferManager
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
            //DebugLog.Log("Init fake transfer manager");
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
                if (_incomingCount == null || _incomingOffers == null || _incomingAmount == null || _outgoingCount == null || _outgoingOffers == null || _outgoingAmount == null)
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
        private static bool _init;
        public static bool IsBuildingOutside(UnityEngine.Vector3 position)
        {
            if ((position.x < 8600) && (position.x > -8600) && (position.z < 8600) && (position.z > -8600))
            {
                return false;

            }
            return true;
        }

        public static bool IsCitizenOutside(UnityEngine.Vector3 position)
        {
            if ((position.x < 8600) && (position.x > -8600) && (position.z < 8600) && (position.z > -8600))
            {
                return false;

            }
            return true;
        }

        private static void StartTransfer(TransferManager manager, TransferManager.TransferReason material, TransferManager.TransferOffer offerOut, TransferManager.TransferOffer offerIn, int delta)
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
                    citizenInfo2.m_citizenAI.StartTransfer(citizen2, ref citizens2.m_buffer[(int)((UIntPtr)citizen2)], material, offerIn);
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
                switch (material)
                {
                    case TransferManager.TransferReason.Goods:
                        if (!IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (!IsBuildingOutside(buildings.m_buffer[(int)building1].m_position)))
                        {
                            comm_data.shop_get_goods_from_local_count++;
                            break;
                        } else if (IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (!IsBuildingOutside(buildings.m_buffer[(int)building1].m_position))) {
                            comm_data.shop_get_goods_from_outside_count++;
                            break;
                        } else if (!IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (IsBuildingOutside(buildings.m_buffer[(int)building1].m_position))) {
                            comm_data.industy_goods_to_outside_count++;
                            break;
                        }
                        break;
                    case TransferManager.TransferReason.Logs:
                        if (!IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (!IsBuildingOutside(buildings.m_buffer[(int)building1].m_position)))
                        {
                            comm_data.logs_to_industy_count++;
                            break;
                        }
                        else if (IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (!IsBuildingOutside(buildings.m_buffer[(int)building1].m_position)))
                        {
                            comm_data.logs_from_outside_count++;
                            break;
                        }
                        else if (!IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (IsBuildingOutside(buildings.m_buffer[(int)building1].m_position)))
                        {
                            comm_data.logs_to_outside_count++;
                            break;
                        }
                        break;
                    case TransferManager.TransferReason.Grain:
                        if (!IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (!IsBuildingOutside(buildings.m_buffer[(int)building1].m_position)))
                        {
                            comm_data.Grain_to_industy_count++;
                            break;
                        }
                        else if (IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (!IsBuildingOutside(buildings.m_buffer[(int)building1].m_position)))
                        {
                            comm_data.Grain_from_outside_count++;
                            break;
                        }
                        else if (!IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (IsBuildingOutside(buildings.m_buffer[(int)building1].m_position)))
                        {
                            comm_data.Grain_to_outside_count++;
                            break;
                        }
                        break;
                    case TransferManager.TransferReason.Oil:
                        if (!IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (!IsBuildingOutside(buildings.m_buffer[(int)building1].m_position)))
                        {
                            comm_data.oil_to_industy_count++;
                            break;
                        }
                        else if (IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (!IsBuildingOutside(buildings.m_buffer[(int)building1].m_position)))
                        {
                            comm_data.oil_from_outside_count++;
                            break;
                        }
                        else if (!IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (IsBuildingOutside(buildings.m_buffer[(int)building1].m_position)))
                        {
                            comm_data.oil_to_outside_count++;
                            break;
                        }
                        break;
                    case TransferManager.TransferReason.Ore:
                        if (!IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (!IsBuildingOutside(buildings.m_buffer[(int)building1].m_position)))
                        {
                            comm_data.ore_to_industy_count++;
                            break;
                        }
                        else if (IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (!IsBuildingOutside(buildings.m_buffer[(int)building1].m_position)))
                        {
                            comm_data.ore_from_outside_count++;
                            break;
                        }
                        else if (!IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (IsBuildingOutside(buildings.m_buffer[(int)building1].m_position)))
                        {
                            comm_data.ore_to_outside_count++;
                            break;
                        }
                        break;
                    case TransferManager.TransferReason.Lumber:
                        if (!IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (!IsBuildingOutside(buildings.m_buffer[(int)building1].m_position)))
                        {
                            comm_data.lumber_to_industy_count++;
                            break;
                        }
                        else if (IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (!IsBuildingOutside(buildings.m_buffer[(int)building1].m_position)))
                        {
                            comm_data.lumber_from_outside_count++;
                            break;
                        }
                        else if (!IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (IsBuildingOutside(buildings.m_buffer[(int)building1].m_position)))
                        {
                            comm_data.lumber_to_outside_count++;
                            break;
                        }
                        break;
                    case TransferManager.TransferReason.Food:
                        if (!IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (!IsBuildingOutside(buildings.m_buffer[(int)building1].m_position)))
                        {
                            comm_data.food_to_industy_count++;
                            break;
                        }
                        else if (IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (!IsBuildingOutside(buildings.m_buffer[(int)building1].m_position)))
                        {
                            comm_data.food_from_outside_count++;
                            break;
                        }
                        else if (!IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (IsBuildingOutside(buildings.m_buffer[(int)building1].m_position)))
                        {
                            comm_data.food_to_outside_count++;
                            break;
                        }
                        break;
                    case TransferManager.TransferReason.Petrol:
                        if (!IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (!IsBuildingOutside(buildings.m_buffer[(int)building1].m_position)))
                        {
                            comm_data.Petrol_to_industy_count++;
                            break;
                        }
                        else if (IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (!IsBuildingOutside(buildings.m_buffer[(int)building1].m_position)))
                        {
                            comm_data.Petrol_from_outside_count++;
                            break;
                        }
                        else if (!IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (IsBuildingOutside(buildings.m_buffer[(int)building1].m_position)))
                        {
                            comm_data.Petrol_to_outside_count++;
                            break;
                        }
                        break;
                    case TransferManager.TransferReason.Coal:
                        if (!IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (!IsBuildingOutside(buildings.m_buffer[(int)building1].m_position)))
                        {
                            comm_data.coal_to_industy_count++;
                            break;
                        }
                        else if (IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (!IsBuildingOutside(buildings.m_buffer[(int)building1].m_position)))
                        {
                            comm_data.coal_from_outside_count++;
                            break;
                        }
                        else if (!IsBuildingOutside(buildings.m_buffer[(int)building].m_position) && (IsBuildingOutside(buildings.m_buffer[(int)building1].m_position)))
                        {
                            comm_data.coal_to_outside_count++;
                            break;
                        }
                        break;
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


        public static void AddIncomingOffer(TransferManager manager, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            // note: do NOT just use 
            //   DebugOutputPanel.AddMessage
            // here. This method is called so frequently that it will actually crash the game.
            if (!_init)
            {
                _init = true;
                Init();
            }
            //CitizenManager instance = Singleton<CitizenManager>.instance;
            //Citizen[] buffer1 = instance.m_citizens.m_buffer;
            //VehicleManager instance = Singleton<VehicleManager>.instance;
            //Vehicle[] buffer = instance.m_vehicles.m_buffer;
            BuildingManager instance1 = Singleton<BuildingManager>.instance;
            Building[] buffer = instance1.m_buildings.m_buffer;
            //DebugLog.LogToFileOnly("AddIncomingOffer" + " buildID " + offer.Building + " custom1_buffer " + buffer[13618].m_customBuffer1 + " custom2_buffer " + buffer[13618].m_customBuffer2); 
            if (material == TransferManager.TransferReason.Goods)
            {
            //DebugLog.LogToFileOnly("AddIncomingOfferx" + buffer[offer.Building].m_position.x.ToString() + " z " + buffer[offer.Building].m_position.z.ToString() + "for " + material + " from ");
            //DebugLog.LogToFileOnly("AddIncomingOffer" + " buildID " + offer.Building + " custom1_buffer " + buffer[offer.Building].m_customBuffer1 + " custom2_buffer " + buffer[offer.Building].m_customBuffer2);
            }

            //if (((material == TransferManager.TransferReason.Shopping) || (material == TransferManager.TransferReason.ShoppingB) || (material == TransferManager.TransferReason.ShoppingC)))
            //{
            //DebugLog.LogToFileOnly("AddIncomingOfferx" + material + buffer1[offer.Citizen].m_homeBuilding + Environment.StackTrace);
            //}
            // + Environment.StackTrace
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

                    switch (material)
                    {
                        //shopping
                        case TransferManager.TransferReason.Shopping:
                        case TransferManager.TransferReason.ShoppingB:
                        case TransferManager.TransferReason.ShoppingC:
                        case TransferManager.TransferReason.ShoppingD:
                        case TransferManager.TransferReason.ShoppingE:
                        case TransferManager.TransferReason.ShoppingF:
                        case TransferManager.TransferReason.ShoppingG:
                        case TransferManager.TransferReason.ShoppingH:
                            //if (buffer1[offer.Citizen].m_homeBuilding == 0)
                            //{
                            //    ;
                            //}
                            //else
                            //{
                            //    ;
                            // }
                            break;
                        //entertainment
                        case TransferManager.TransferReason.Entertainment:
                        case TransferManager.TransferReason.EntertainmentB:
                        case TransferManager.TransferReason.EntertainmentC:
                        case TransferManager.TransferReason.EntertainmentD:
                            /// if (buffer1[offer.Citizen].m_homeBuilding == 0)
                            // {
                            //     ;
                            // }
                            // else
                            // {
                            //     ;
                            // }
                            break;
                        case TransferManager.TransferReason.Food:
                            // if ((buffer[offer.Building].m_position.x > 8600) || (buffer[offer.Building].m_position.x < -8600) || (buffer[offer.Building].m_position.z > 8600) || (buffer[offer.Building].m_position.z < -8600))
                            // {
                            //     ;
                            // }
                            // else
                            // {
                            //      ;
                            // }
                            break;
                        case TransferManager.TransferReason.Goods:
                            // if ((buffer[offer.Building].m_position.x > 8600) || (buffer[offer.Building].m_position.x < -8600) || (buffer[offer.Building].m_position.z > 8600) || (buffer[offer.Building].m_position.z < -8600))
                            // {
                            //     ;
                            //  }
                            // else
                            //  {
                            //DebugLog.LogToFileOnly(offer.m_object.RawData.ToString());
                            //DebugLog.LogToFileOnly("AddIncomingOfferx goods" + offer.Priority.ToString() + "amount" + offer.Amount.ToString());
                            //  }
                            break;
                    }
                    break;
                }//endif
            }//endfor
        }//end public
    }//end publi
}//end naming space 