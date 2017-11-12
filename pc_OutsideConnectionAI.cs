﻿using ColossalFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UnityEngine;

namespace RealCity
{
    public class pc_OutsideConnectionAI : BuildingAI
    {
        /*public int m_cargoCapacity = 20;

        public int m_residentCapacity = 1000;

        public int m_touristFactor0 = 325;

        public int m_touristFactor1 = 125;

        public int m_touristFactor2 = 50;

        public TransferManager.TransferReason m_dummyTrafficReason = TransferManager.TransferReason.None;

        public int m_dummyTrafficFactor = 1000;*/

        public static void Init()
        {
            //DebugLog.Log("Init fake transfer manager");
            try
            {
                var inst = Singleton<OutsideConnectionAI>.instance;
                var cargoCapacity = typeof(OutsideConnectionAI).GetField("m_cargoCapacity", BindingFlags.Public | BindingFlags.Instance);
                var residentCapacity = typeof(OutsideConnectionAI).GetField("m_residentCapacity", BindingFlags.Public | BindingFlags.Instance);
                var touristFactor0 = typeof(OutsideConnectionAI).GetField("m_touristFactor0", BindingFlags.Public | BindingFlags.Instance);
                var touristFactor1 = typeof(OutsideConnectionAI).GetField("m_touristFactor1", BindingFlags.Public | BindingFlags.Instance);
                var touristFactor2 = typeof(OutsideConnectionAI).GetField("m_touristFactor2", BindingFlags.Public | BindingFlags.Instance);
                var dummyTrafficReason = typeof(OutsideConnectionAI).GetField("m_dummyTrafficReason", BindingFlags.Public | BindingFlags.Instance);
                var dummyTrafficFactor = typeof(OutsideConnectionAI).GetField("m_dummyTrafficFactor", BindingFlags.Public | BindingFlags.Instance);
                if (inst == null)
                {
                    DebugLog.LogToFileOnly("No instance of OutsideConnectionAI found!");
                    return;
                }
                _cargoCapacity = (int)cargoCapacity.GetValue(inst);
                _residentCapacity = (int)residentCapacity.GetValue(inst);
                _touristFactor0 = (int)touristFactor0.GetValue(inst);
                _touristFactor1 = (int)touristFactor1.GetValue(inst);
                _touristFactor2 = (int)touristFactor2.GetValue(inst);
                _dummyTrafficFactor = (int)dummyTrafficFactor.GetValue(inst);
                _dummyTrafficReason = (TransferManager.TransferReason)dummyTrafficReason.GetValue(inst);
            }
            catch (Exception ex)
            {
                DebugLog.LogToFileOnly("OutsideConnectionAI Exception: " + ex.Message);
            }
        }
        private static int _cargoCapacity;
        private static int _residentCapacity;
        private static int _touristFactor0;
        private static int _touristFactor1;
        private static int _touristFactor2;
        private static int _dummyTrafficFactor;
        private static TransferManager.TransferReason _dummyTrafficReason;

        private static bool _init = false;

        public static bool have_maintain_road_building = false;
        public static bool have_garbage_building = false;
        public static bool have_cemetry_building = false;




        public override void SimulationStep(ushort buildingID, ref Building data)
        {
            if (!_init)
            {
                _init = true;
                Init();
            }
            base.SimulationStep(buildingID, ref data);
            if ((Singleton<ToolManager>.instance.m_properties.m_mode & ItemClass.Availability.Game) != ItemClass.Availability.None)
            {
                int budget = Singleton<EconomyManager>.instance.GetBudget(this.m_info.m_class);
                int productionRate = OutsideConnectionAI.GetProductionRate(100, budget);
                OutsideConnectionAI.AddConnectionOffers(buildingID, ref data, productionRate, _cargoCapacity, _residentCapacity, _touristFactor0, _touristFactor1, _touristFactor2, _dummyTrafficReason, _dummyTrafficFactor);
                process_outside_demand(buildingID, ref data);
                Addotherconnectionoffers(buildingID, ref data);
                caculate_outside_situation(buildingID, ref data);
            }
        }



        public void caculate_outside_situation (ushort buildingID, ref Building data)
        {
            if (comm_data.outside_pre_building < buildingID)
            {
                comm_data.outside_dead_count_temp += data.m_customBuffer1;
                comm_data.outside_crime_count_temp += data.m_crimeBuffer;
                comm_data.outside_sick_count_temp += data.m_customBuffer2;
                comm_data.outside_garbage_count_temp += data.m_garbageBuffer;
            }
            else
            {
                comm_data.outside_dead_count = comm_data.outside_dead_count_temp;
                comm_data.outside_garbage_count = comm_data.outside_garbage_count_temp;
                comm_data.outside_sick_count = comm_data.outside_sick_count_temp;
                comm_data.outside_crime_count = comm_data.outside_crime_count_temp;
                comm_data.outside_dead_count_temp = 0;
                comm_data.outside_crime_count_temp = 0;
                comm_data.outside_sick_count_temp = 0;
                comm_data.outside_garbage_count_temp = 0;
            }

            comm_data.outside_pre_building = buildingID;
        }

        public void process_outside_demand(ushort buildingID, ref Building data)
        {
            //garbage can existed on both incoming and outgoing outside building
            //dead can only existed on outgoing building
            //crime and sick can only existed on incoming building
            //garbagebuffer
            System.Random rand = new System.Random();
            if (RealCity.garbage_connection && Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.Garbage))
            {
                data.m_garbageBuffer = (ushort)(data.m_garbageBuffer + 200);
            }
            if ((data.m_flags & Building.Flags.IncomingOutgoing) == Building.Flags.Incoming)
            {
                if (RealCity.crime_connection)
                {
                    data.m_crimeBuffer = (ushort)(data.m_crimeBuffer + 2);
                }
                //sick
                if (RealCity.sick_connection)
                {
                    data.m_customBuffer2 = (ushort)(data.m_customBuffer2 + 1);
                }
            }
            else
            {
                //deadbuffer
                if (RealCity.dead_connection && Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.HealthCare))
                {
                    data.m_customBuffer1 = (ushort)(data.m_customBuffer1 + (int)(rand.Next(60) / 50));
                }
            }

            if (data.m_customBuffer1 > 65000)
            {
                data.m_customBuffer1 = 65000;
            }

            if (data.m_customBuffer2 > 65000)
            {
                data.m_customBuffer2 = 65000;
            }

            if (data.m_crimeBuffer > 65000)
            {
                data.m_crimeBuffer = 65000;
            }

            if (data.m_garbageBuffer > 65000)
            {
                data.m_garbageBuffer = 65000;
            }
        }

        public void Addgarbageoffers(ushort buildingID, ref Building data)
        {
            TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);

            if (have_garbage_building && RealCity.garbage_connection)
            {
                if ((data.m_flags & Building.Flags.IncomingOutgoing) == Building.Flags.Incoming)
                {
                    int num24 = (int)data.m_garbageBuffer;
                    if (num24 >= 200 && Singleton<SimulationManager>.instance.m_randomizer.Int32(5u) == 0 && Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.Garbage))
                    {
                        int num25 = 0;
                        int num26 = 0;
                        int num27 = 0;
                        int num28 = 0;
                        this.CalculateGuestVehicles(buildingID, ref data, TransferManager.TransferReason.Garbage, ref num25, ref num26, ref num27, ref num28);
                        num24 -= num27 - num26;
                        //DebugLog.LogToFileOnly("caculate num24  = " + num24.ToString() + "num27 = " + num27.ToString() + "num26 = " + num26.ToString());
                        if (num24 >= 200)
                        {
                            offer = default(TransferManager.TransferOffer);
                            offer.Priority = num24 / 1000;
                            if (offer.Priority > 7)
                            {
                                offer.Priority = 7;
                            }
                            offer.Building = buildingID;
                            offer.Position = data.m_position;
                            offer.Amount = 1;
                            offer.Active = false;
                            Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Garbage, offer);
                        }
                    }
                }
                else
                {
                    if (data.m_garbageBuffer > 5000)
                    {
                        offer = default(TransferManager.TransferOffer);
                        offer.Priority = 1 + data.m_garbageBuffer / 5000;
                        if (offer.Priority > 7)
                        {
                            offer.Priority = 7;
                        }
                        offer.Building = buildingID;
                        offer.Position = data.m_position;
                        offer.Amount = 1;
                        offer.Active = true;
                        Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.GarbageMove, offer);
                    }
                }
            }
        }

        public void Addotherconnectionoffers(ushort buildingID, ref Building data)
        {
            System.Random rand = new System.Random();

            //gabarge
            TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);

            Addgarbageoffers(buildingID, ref data);


            if (have_cemetry_building && RealCity.dead_connection)
            {
                if (data.m_customBuffer1 > 10)
                {
                    offer = default(TransferManager.TransferOffer);
                    offer.Priority = 1 + data.m_customBuffer1 / 5;
                    if (offer.Priority > 7)
                    {
                        offer.Priority = 7;
                    }
                    offer.Building = buildingID;
                    offer.Position = data.m_position;
                    offer.Amount = 1;
                    offer.Active = true;
                    Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.DeadMove, offer);
                }
            }
        }


        public override void StartTransfer(ushort buildingID, ref Building data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (material == TransferManager.TransferReason.GarbageMove)
            {
                //DebugLog.LogToFileOnly("starttransfer gabarge from outside to city");
                VehicleInfo randomVehicleInfo2 = Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref Singleton<SimulationManager>.instance.m_randomizer, ItemClass.Service.Garbage, ItemClass.SubService.None, ItemClass.Level.Level1);
                if (randomVehicleInfo2 != null)
                {
                    Array16<Vehicle> vehicles2 = Singleton<VehicleManager>.instance.m_vehicles;
                    ushort num2;
                    if (Singleton<VehicleManager>.instance.CreateVehicle(out num2, ref Singleton<SimulationManager>.instance.m_randomizer, randomVehicleInfo2, data.m_position, TransferManager.TransferReason.GarbageMove, false, true))
                    {
                        randomVehicleInfo2.m_vehicleAI.SetSource(num2, ref vehicles2.m_buffer[(int)num2], buildingID);
                        randomVehicleInfo2.m_vehicleAI.StartTransfer(num2, ref vehicles2.m_buffer[(int)num2], TransferManager.TransferReason.GarbageMove, offer);
                    }
                }
            }
            else if (material == TransferManager.TransferReason.DeadMove)
            {
                VehicleInfo randomVehicleInfo2 = Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref Singleton<SimulationManager>.instance.m_randomizer, ItemClass.Service.HealthCare, ItemClass.SubService.None, ItemClass.Level.Level2);
                if (randomVehicleInfo2 != null)
                {
                    Array16<Vehicle> vehicles2 = Singleton<VehicleManager>.instance.m_vehicles;
                    ushort num2;
                    if (Singleton<VehicleManager>.instance.CreateVehicle(out num2, ref Singleton<SimulationManager>.instance.m_randomizer, randomVehicleInfo2, data.m_position, material, false, true))
                    {
                        //DebugLog.LogToFileOnly("try transfer deadmove to city, itemclass = " + Singleton<BuildingManager>.instance.m_buildings.m_buffer[offer.Building].Info.m_class.m_service.ToString() + Singleton<BuildingManager>.instance.m_buildings.m_buffer[offer.Building].Info.m_class.m_subService.ToString() + Singleton<BuildingManager>.instance.m_buildings.m_buffer[offer.Building].Info.m_class.m_level.ToString());
                        randomVehicleInfo2.m_vehicleAI.SetSource(num2, ref vehicles2.m_buffer[(int)num2], buildingID);
                        randomVehicleInfo2.m_vehicleAI.StartTransfer(num2, ref vehicles2.m_buffer[(int)num2], material, offer);
                    }
                }
            }
            /*else if (material == TransferManager.TransferReason.Crime)
            {
                VehicleInfo randomVehicleInfo = Singleton<VehicleManager>.instance.GetRandomVehicleInfo(ref Singleton<SimulationManager>.instance.m_randomizer, ItemClass.Service.PoliceDepartment, ItemClass.SubService.None, ItemClass.Level.Level2);
                if (randomVehicleInfo != null)
                {
                    Array16<Vehicle> vehicles = Singleton<VehicleManager>.instance.m_vehicles;
                    ushort num;
                    DebugLog.LogToFileOnly("try transfer Crime to city, itemclass = " + Singleton<BuildingManager>.instance.m_buildings.m_buffer[offer.Building].Info.m_class.m_service.ToString() + Singleton<BuildingManager>.instance.m_buildings.m_buffer[offer.Building].Info.m_class.m_subService.ToString() + Singleton<BuildingManager>.instance.m_buildings.m_buffer[offer.Building].Info.m_class.m_level.ToString());
                    if (Singleton<VehicleManager>.instance.CreateVehicle(out num, ref Singleton<SimulationManager>.instance.m_randomizer, randomVehicleInfo, data.m_position, material, true, false))
                    {
                        randomVehicleInfo.m_vehicleAI.SetSource(num, ref vehicles.m_buffer[(int)num], buildingID);
                        randomVehicleInfo.m_vehicleAI.StartTransfer(num, ref vehicles.m_buffer[(int)num], material, offer);
                        vehicles.m_buffer[(int)num].m_transferSize = 1;
                        vehicles.m_buffer[(int)num].m_flags |= Vehicle.Flags.GoingBack;
                        vehicles.m_buffer[(int)num].m_flags |= Vehicle.Flags.Emergency2;
                    }
                }
            }*/
            else
            {
                if (!OutsideConnectionAI.StartConnectionTransfer(buildingID, ref data, material, offer, _touristFactor0, _touristFactor1, _touristFactor2))
                {
                    base.StartTransfer(buildingID, ref data, material, offer);
                }
            }
        }


        public override void ModifyMaterialBuffer(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            if ((data.m_flags & Building.Flags.IncomingOutgoing) == Building.Flags.Incoming)
            {
                if (material == TransferManager.TransferReason.Garbage)
                {
                    //DebugLog.LogToFileOnly("starttransfer gabarge from outside to city, gather gabage");
                    if (data.m_garbageBuffer < 0)
                    {
                        DebugLog.LogToFileOnly("garbarge < 0 in outside building, should be wrong");
                        amountDelta = 0;
                    }
                    else
                    {
                        if (data.m_garbageBuffer + amountDelta <= 0)
                        {
                            amountDelta = -data.m_garbageBuffer;
                        }
                        else
                        {

                        }
                        data.m_garbageBuffer = (ushort)(data.m_garbageBuffer + amountDelta);
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)(amountDelta * -0.1f), ItemClass.Service.Garbage, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                    }
                }
                else if (material == TransferManager.TransferReason.Sick)
                {

                }
                else if (material == TransferManager.TransferReason.Crime)
                {

                }
                else
                {
                    //amountDelta = 0;
                }
            }
            else
            {
                if (material == TransferManager.TransferReason.GarbageMove)
                {
                    //DebugLog.LogToFileOnly("starttransfer gabarge from outside to city, gather gabage");
                    if (data.m_garbageBuffer < 0)
                    {
                        DebugLog.LogToFileOnly("garbarge < 0 in outside building, should be wrong");
                        amountDelta = 0;
                    }
                    else
                    {
                        if (data.m_garbageBuffer + amountDelta <= 0)
                        {
                            amountDelta = -data.m_garbageBuffer;
                        }
                        else
                        {

                        }
                        data.m_garbageBuffer = (ushort)(data.m_garbageBuffer + amountDelta);
                    }
                }
                else if (material == TransferManager.TransferReason.DeadMove)
                {
                    if (data.m_customBuffer1 <= 0)
                    {
                        DebugLog.LogToFileOnly("dead <= 0 in outside building, should be wrong");
                        amountDelta = 0;
                    }
                    else
                    {
                        if (data.m_customBuffer1 + amountDelta <= 0)
                        {
                            amountDelta = -data.m_customBuffer1;
                        }
                        else
                        {

                        }
                        data.m_customBuffer1 = (ushort)(data.m_customBuffer1 + amountDelta);
                    }
                }
                else if (material == TransferManager.TransferReason.Garbage)
                {
                    //DebugLog.LogToFileOnly("starttransfer gabarge from outside to city, gather gabage");
                    amountDelta = 0;
                    /*if (data.m_garbageBuffer < 0)
                    {
                        DebugLog.LogToFileOnly("garbarge < 0 in outside building, should be wrong");
                        amountDelta = 0;
                    }
                    else
                    {
                        if (data.m_garbageBuffer + amountDelta <= 0)
                        {
                            amountDelta = -data.m_garbageBuffer;
                        }
                        else
                        {

                        }
                        data.m_garbageBuffer = (ushort)(data.m_garbageBuffer + amountDelta);
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)(amountDelta * -0.1f), ItemClass.Service.Garbage, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                    }*/
                }
                else
                {
                       //do nothing
                }
            }
        }

        protected void CalculateGuestVehicles(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int count, ref int cargo, ref int capacity, ref int outside)
        {
            VehicleManager instance = Singleton<VehicleManager>.instance;
            ushort num = data.m_guestVehicles;
            int num2 = 0;
            while (num != 0)
            {
                if ((TransferManager.TransferReason)instance.m_vehicles.m_buffer[(int)num].m_transferType == material)
                {
                    VehicleInfo info = instance.m_vehicles.m_buffer[(int)num].Info;
                    int a;
                    int num3;
                    info.m_vehicleAI.GetSize(num, ref instance.m_vehicles.m_buffer[(int)num], out a, out num3);
                    cargo += Mathf.Min(a, num3);
                    capacity += num3;
                    count++;
                    if ((instance.m_vehicles.m_buffer[(int)num].m_flags & (Vehicle.Flags.Importing | Vehicle.Flags.Exporting)) != (Vehicle.Flags)0)
                    {
                        outside++;
                    }
                }
                num = instance.m_vehicles.m_buffer[(int)num].m_nextGuestVehicle;
                if (++num2 > 16384)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }
        }

    }
}
