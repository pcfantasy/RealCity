using ColossalFramework;
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
                Addotherconnectionoffers(buildingID, ref data);
            }
        }

        public void Addotherconnectionoffers (ushort buildingID, ref Building data)
        {
            System.Random rand = new System.Random();

            //gabarge
            TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
            ushort num = Singleton<BuildingManager>.instance.FindBuilding(data.m_position, 1000000000f, ItemClass.Service.Garbage, ItemClass.SubService.None, Building.Flags.Created | Building.Flags.Active, Building.Flags.Deleted);
            if (num != 0)
            {
                if (rand.Next(100) < 10)
                {
                    offer = default(TransferManager.TransferOffer);
                    offer.Priority = 1 + rand.Next(6);
                    offer.Building = buildingID;
                    offer.Position = data.m_position;
                    offer.Amount = 1;
                    offer.Active = true;
                    Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.GarbageMove, offer);
                }
            }

            //num = Singleton<BuildingManager>.instance.FindBuilding(data.m_position, 1000000000f, ItemClass.Service.HealthCare, ItemClass.SubService.None, Building.Flags.Created | Building.Flags.Active, Building.Flags.Deleted);
            if (have_cemetry_building)
            {
                if (rand.Next(100) < 30)
                {
                    offer = default(TransferManager.TransferOffer);
                    offer.Priority = 1 + rand.Next(6);
                    offer.Building = buildingID;
                    offer.Position = data.m_position;
                    offer.Amount = 1;
                    offer.Active = true;
                    Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.DeadMove, offer);
                }
            }

            /*num = Singleton<BuildingManager>.instance.FindBuilding(data.m_position, 1000000000f, ItemClass.Service.HealthCare, ItemClass.SubService.None, Building.Flags.Created | Building.Flags.Active, Building.Flags.Deleted);
            if (num != 0)
            {
                if (rand.Next(100) < 70)
                {
                    offer = default(TransferManager.TransferOffer);
                    offer.Priority = 7;
                    offer.Building = buildingID;
                    offer.Position = data.m_position;
                    offer.Amount = 1;
                    offer.Active = true;
                    Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Crime, offer);
                }
            }*/

            /*num = Singleton<BuildingManager>.instance.FindBuilding(data.m_position, 1000000000f, ItemClass.Service.Road, ItemClass.SubService.None, Building.Flags.Created | Building.Flags.Active, Building.Flags.Untouchable);
            {
                if (num != 0)
                {
                    have_maintain_road_building = true;
                }
            }*/
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
                        DebugLog.LogToFileOnly("try transfer deadmove to city, itemclass = " + Singleton<BuildingManager>.instance.m_buildings.m_buffer[offer.Building].Info.m_class.m_service.ToString() + Singleton<BuildingManager>.instance.m_buildings.m_buffer[offer.Building].Info.m_class.m_subService.ToString() + Singleton<BuildingManager>.instance.m_buildings.m_buffer[offer.Building].Info.m_class.m_level.ToString());
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
            System.Random rand = new System.Random();
            if (material == TransferManager.TransferReason.GarbageMove)
            {
                //DebugLog.LogToFileOnly("starttransfer gabarge from outside to city, gather gabage");
                amountDelta = -1000 * (rand.Next(19) + 1);
                //Singleton<EconomyManager>.instance.AddPrivateIncome((int)(amountDelta * -0.05f), ItemClass.Service.Garbage, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
            } else if (material == TransferManager.TransferReason.DeadMove)
            {
                amountDelta = -1 * (rand.Next(9) + 1);
                //DebugLog.LogToFileOnly("starttransfer dead from outside to city, gather gabage");
                //Singleton<EconomyManager>.instance.AddPrivateIncome(amountDelta * -100, ItemClass.Service.HealthCare, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
            }
        }
    }
}
