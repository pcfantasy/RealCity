using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using UnityEngine;
using System.Reflection;
using ColossalFramework.Math;

namespace RealCity
{

    public class RealCity : IUserMod
    {
        public static bool IsEnabled = false;

        public string Name
        {
            get { return "Real City Mod"; }
        }

        public string Description
        {
            get { return "Make your city reality"; }
        }

        public void OnEnabled()
        {
            RealCity.IsEnabled = true;
            var srcMethod = typeof(TransferManager).GetMethod("StartTransfer", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            var destMethod = typeof(pc_TransferManager).GetMethod("StartTransfer", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            RedirectionHelper.RedirectCalls(srcMethod, destMethod);

            var srcMethod1 = typeof(TransferManager).GetMethod("AddIncomingOffer", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            var destMethod1 = typeof(pc_TransferManager).GetMethod("AddIncomingOffer", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            RedirectionHelper.RedirectCalls(srcMethod1, destMethod1);

            var srcMethod2 = typeof(IndustrialBuildingAI).GetMethod("ModifyMaterialBuffer");
            var destMethod2 = typeof(pc_IndustrialBuildingAI).GetMethod("ModifyMaterialBuffer");
            RedirectionHelper.RedirectCalls(srcMethod2, destMethod2);

            //var srcMethod3 = typeof(IndustrialBuildingAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            //var destMethod3 = typeof(pc_IndustrialBuildingAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            //RedirectionHelper.RedirectCalls(srcMethod3, destMethod3);

            var srcMethod4 = typeof(IndustrialExtractorAI).GetMethod("ModifyMaterialBuffer");
            var destMethod4 = typeof(pc_IndustrialExtractorAI).GetMethod("ModifyMaterialBuffer");
            RedirectionHelper.RedirectCalls(srcMethod4, destMethod4);

            //var srcMethod5 = typeof(IndustrialExtractorAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            //var destMethod5 = typeof(pc_IndustrialExtractorAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            //RedirectionHelper.RedirectCalls(srcMethod5, destMethod5);

            var srcMethod6 = typeof(CommercialBuildingAI).GetMethod("ModifyMaterialBuffer");
            var destMethod6 = typeof(pc_CommercialBuildingAI).GetMethod("ModifyMaterialBuffer");
            RedirectionHelper.RedirectCalls(srcMethod6, destMethod6);

            //var srcMethod7 = typeof(CommercialBuildingAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            //var destMethod7 = typeof(pc_CommercialBuildingAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            //RedirectionHelper.RedirectCalls(srcMethod7, destMethod7);

            //var srcMethod8 = typeof(BuildingAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            //var destMethod8 = typeof(pc_BuildingAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            //RedirectionHelper.RedirectCalls(srcMethod8, destMethod8);

            var srcMethod9 = typeof(ResidentAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(CitizenUnit).MakeByRefType() }, null);
            var destMethod9 = typeof(pc_ResidentAI).GetMethod("SimulationStep_1", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(CitizenUnit).MakeByRefType() }, null);
            RedirectionHelper.RedirectCalls(srcMethod9, destMethod9);

            //var srcMethod10 = typeof(HumanAI).GetMethod("ArriveAtDestination", BindingFlags.NonPublic | BindingFlags.Instance);
            //var destMethod10 = typeof(pc_HumanAI).GetMethod("ArriveAtDestination_1", BindingFlags.NonPublic | BindingFlags.Instance);
            //RedirectionHelper.RedirectCalls(srcMethod10, destMethod10);

            var srcMethod11 = typeof(EconomyManager).GetMethod("FetchResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int),  typeof(ItemClass) }, null);
            var destMethod11 = typeof(pc_EconomyManager).GetMethod("FetchResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass) }, null);
            RedirectionHelper.RedirectCalls(srcMethod11, destMethod11);

            //var srcMethod12 = typeof(EconomyManager).GetMethod("AddResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass) }, null);
            //var destMethod12 = typeof(pc_EconomyManager).GetMethod("AddResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass) }, null);
            //RedirectionHelper.RedirectCalls(srcMethod12, destMethod12);

            var srcMethod13 = typeof(EconomyManager).GetMethod("AddPrivateIncome", BindingFlags.Public | BindingFlags.Instance);
            var destMethod13 = typeof(pc_EconomyManager).GetMethod("AddPrivateIncome", BindingFlags.Public | BindingFlags.Instance);
            RedirectionHelper.RedirectCalls(srcMethod13, destMethod13);

            //var srcMethod14 = typeof(ResidentialBuildingAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            //var destMethod14 = typeof(pc_ResidentialBuildingAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            //RedirectionHelper.RedirectCalls(srcMethod14, destMethod14);

            //var srcMethod15 = typeof(OfficeBuildingAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            //var destMethod15 = typeof(pc_OfficeBuildingAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            //RedirectionHelper.RedirectCalls(srcMethod15, destMethod15);

            //var srcMethod16 = typeof(IndustrialExtractorAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            //var destMethod16 = typeof(pc_IndustrialExtractorAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            //RedirectionHelper.RedirectCalls(srcMethod16, destMethod16);

            var srcMethod17 = typeof(PrivateBuildingAI).GetMethod("SimulationStepActive", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            var destMethod17 = typeof(pc_PrivateBuildingAI).GetMethod("SimulationStepActive_1", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            RedirectionHelper.RedirectCalls(srcMethod17, destMethod17);

            var srcMethod18 = typeof(PassengerCarAI).GetMethod("ArriveAtDestination", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var destMethod18 = typeof(pc_PassengerCarAI).GetMethod("ArriveAtDestination_1", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            RedirectionHelper.RedirectCalls(srcMethod18, destMethod18);

        }

        public void OnDisabled()
        {
            RealCity.IsEnabled = false;
        }

        // public void OnSettingsUI(UIHelperBase helper)
        // {
        //     UIHelperBase group = helper.AddGroup("Check to enable income from excess capacity");
        //     ExpmHolder.get().AddOptions(group);
        // }


        public class EconomyExtension : EconomyExtensionBase
        {
            public override long OnUpdateMoneyAmount(long internalMoneyAmount)
            {
                //DebugLog.LogToFileOnly(Singleton<SimulationManager>.instance.m_currentDayTimeHour.ToString());
                //here we process income_tax and goverment_salary_expense 
                //to make goverment_salary_expense the same with in game unit
                uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
                uint num2 = currentFrameIndex & 255u;
                if (num2 == 255u)
                {
                    if (Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_populationData.m_finalCount > 0)
                    {
                        ItemClass temp = new ItemClass();
                        temp.m_service = ItemClass.Service.PublicTransport;
                        temp.m_subService = ItemClass.SubService.PublicTransportBus;
                        //use this to diff make in-game mantenance and goverment_salary_expense 
                        temp.m_level = ItemClass.Level.Level1;
                        if (comm_data.PublicTransport_bus != 0)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.PublicTransport_bus, temp);
                        }

                        temp.m_service = ItemClass.Service.PublicTransport;
                        temp.m_subService = ItemClass.SubService.PublicTransportTram;
                        if (comm_data.PublicTransport_tram != 0)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.PublicTransport_tram, temp);
                        }

                        temp.m_service = ItemClass.Service.PublicTransport;
                        temp.m_subService = ItemClass.SubService.PublicTransportTrain;
                        if (comm_data.PublicTransport_train != 0)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.PublicTransport_train, temp);
                        }

                        temp.m_service = ItemClass.Service.PublicTransport;
                        temp.m_subService = ItemClass.SubService.PublicTransportMetro;
                        if (comm_data.PublicTransport_metro != 0)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.PublicTransport_metro, temp);
                        }

                        temp.m_service = ItemClass.Service.PublicTransport;
                        temp.m_subService = ItemClass.SubService.PublicTransportTaxi;
                        if (comm_data.PublicTransport_taxi != 0)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.PublicTransport_taxi, temp);
                        }

                        temp.m_service = ItemClass.Service.PublicTransport;
                        temp.m_subService = ItemClass.SubService.PublicTransportPlane;
                        if (comm_data.PublicTransport_plane != 0)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.PublicTransport_plane, temp);
                        }

                        temp.m_service = ItemClass.Service.PublicTransport;
                        temp.m_subService = ItemClass.SubService.PublicTransportShip;
                        if (comm_data.PublicTransport_ship != 0)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.PublicTransport_ship, temp);
                        }

                        temp.m_service = ItemClass.Service.PublicTransport;
                        temp.m_subService = ItemClass.SubService.PublicTransportCableCar;
                        if (comm_data.PublicTransport_cablecar != 0)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.PublicTransport_cablecar, temp);
                        }

                        temp.m_service = ItemClass.Service.PublicTransport;
                        temp.m_subService = ItemClass.SubService.PublicTransportMonorail;
                        if (comm_data.PublicTransport_monorail != 0)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.PublicTransport_monorail, temp);
                        }

                        temp.m_service = ItemClass.Service.Road;
                        temp.m_subService = ItemClass.SubService.None;
                        if (comm_data.Road != 0)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.Road, temp);
                        }

                        temp.m_service = ItemClass.Service.Water;
                        if (comm_data.Water != 0)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.Water, temp);
                        }

                        temp.m_service = ItemClass.Service.Education;
                        if (comm_data.Education != 0)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.Education, temp);
                        }

                        temp.m_service = ItemClass.Service.HealthCare;
                        if (comm_data.HealthCare != 0)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.HealthCare, temp);
                        }

                        temp.m_service = ItemClass.Service.FireDepartment;
                        if (comm_data.FireDepartment != 0)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.FireDepartment, temp);
                        }

                        temp.m_service = ItemClass.Service.Beautification;
                        if (comm_data.Beautification != 0)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.Beautification, temp);
                        }

                        temp.m_service = ItemClass.Service.Garbage;
                        if (comm_data.Garbage != 0)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.Garbage, temp);
                        }

                        temp.m_service = ItemClass.Service.Electricity;
                        if (comm_data.Electricity != 0)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.Electricity, temp);
                        }

                        temp.m_service = ItemClass.Service.Monument;
                        if (comm_data.Monument != 0)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.Monument, temp);
                        }

                        temp.m_service = ItemClass.Service.PoliceDepartment;
                        if (comm_data.PoliceDepartment != 0)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.PoliceDepartment, temp);
                        }

                        temp.m_service = ItemClass.Service.Disaster;
                        if (comm_data.Disaster != 0)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.Disaster, temp);
                        }

                        //discard this, because in 1.9.0-f5, no citizen income and tourist income showed in UI, combin them into residential building income
                        //and show them in CTRL+R UI.
                        //citizen income tax
                        //if (comm_data.citizen_salary_tax_total != 0)
                        //{
                        //    temp = new ItemClass();
                        //    temp.m_service = ItemClass.Service.Citizen;
                        //    Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.CitizenIncome, (int)(comm_data.citizen_salary_tax_total), temp);
                        //}
                    }
                    caculate_profit();
                    caculate_citizen_transport_fee();
                }
                return internalMoneyAmount;
            }

            public void caculate_citizen_transport_fee()
            {
                ItemClass temp = new ItemClass();
                long temp1 = 0L;
                long temp2 = 0L;
                comm_data.public_transport_fee = 0L;
                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportBus;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportTram;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportMetro;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportTrain;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportTaxi;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportPlane;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportShip;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportMonorail;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportCableCar;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                //add vehicle transport_fee
                if (comm_data.temp_total_citizen_vehical_time >= comm_data.temp_total_citizen_vehical_time_last)
                {
                    comm_data.total_citizen_vehical_time = comm_data.temp_total_citizen_vehical_time - comm_data.temp_total_citizen_vehical_time_last;
                } else
                {
                    comm_data.total_citizen_vehical_time = 4294967295 + comm_data.temp_total_citizen_vehical_time - comm_data.temp_total_citizen_vehical_time_last;
                }
                comm_data.temp_total_citizen_vehical_time_last = comm_data.temp_total_citizen_vehical_time;
                comm_data.all_transport_fee = comm_data.public_transport_fee + comm_data.total_citizen_vehical_time;

                if (comm_data.citizen_count > 0)
                {
                    if ((comm_data.all_transport_fee / comm_data.citizen_count) > 128)
                    {
                        comm_data.citizen_average_transport_fee = 128;
                    }
                    else
                    {
                        comm_data.citizen_average_transport_fee = (byte)(comm_data.all_transport_fee / comm_data.citizen_count);
                    }
                }
            }

            public void vehicle_status()
            {
                VehicleManager instance = Singleton<VehicleManager>.instance;
                for (int i = 0; i < 16384; i = i + 1)
                {
                    Vehicle vehicle = instance.m_vehicles.m_buffer[i];
                    if (vehicle.m_flags.IsFlagSet(Vehicle.Flags.Created) && !vehicle.m_flags.IsFlagSet(Vehicle.Flags.Deleted))
                    {
                        if ((vehicle.Info.m_vehicleType == VehicleInfo.VehicleType.Car) && (vehicle.Info.m_class.m_subService != ItemClass.SubService.PublicTransportTaxi))
                        {
                            if (vehicle.m_flags.IsFlagSet(Vehicle.Flags.Transition))
                            {
                                comm_data.vehical_transfer_time[i] = (ushort)(comm_data.vehical_transfer_time[i] + 1);
                            }
                        }
                    }
                    if ((comm_data.vehical_last_transfer_flag[i] == false) || (vehicle.m_flags.IsFlagSet(Vehicle.Flags.Transition) == true))
                    {
                        comm_data.vehical_transfer_time[i] = 1;
                    }
                    comm_data.vehical_last_transfer_flag[i] = vehicle.m_flags.IsFlagSet(Vehicle.Flags.Transition);
                }
            }


            public void citizen_status()
            {
                CitizenManager instance = Singleton<CitizenManager>.instance;
                for (int i = 0; i < 65536; i = i + 1)
                {
                    CitizenUnit citizen = instance.m_units.m_buffer[i];
                    if (citizen.m_flags.IsFlagSet(CitizenUnit.Flags.Created))
                    {
                        if(citizen.m_citizen0 != 0)
                        {
                            comm_data.citizen_count = comm_data.citizen_count + 1;
                        }
                        if (citizen.m_citizen1 != 0)
                        {
                            comm_data.citizen_count = comm_data.citizen_count + 1;
                        }
                        if (citizen.m_citizen2 != 0)
                        {
                            comm_data.citizen_count = comm_data.citizen_count + 1;
                        }
                        if (citizen.m_citizen3 != 0)
                        {
                            comm_data.citizen_count = comm_data.citizen_count + 1;
                        }
                        if (citizen.m_citizen4 != 0)
                        {
                            comm_data.citizen_count = comm_data.citizen_count + 1;
                        }
                    }
                }
            }


            public void caculate_profit()
            {
                float lumber_export_ratio = 0;
                float lumber_import_ratio = 0;
                float petrol_export_ratio = 0;
                float petrol_import_ratio = 0;
                float coal_export_ratio = 0;
                float coal_import_ratio = 0;
                float food_export_ratio = 0;
                float food_import_ratio = 0;
                float logs_export_ratio = 0;
                float logs_import_ratio = 0;
                float grain_export_ratio = 0;
                float grain_import_ratio = 0;
                float oil_export_ratio = 0;
                float oil_import_ratio = 0;
                float ore_export_ratio = 0;
                float ore_import_ratio = 0;
                float good_export_ratio = 0;
                float good_import_ratio = 0;
                //lumber
                if ((pc_PrivateBuildingAI.lumber_from_outside_count + pc_PrivateBuildingAI.lumber_to_industy_count) != 0)
                {
                    lumber_import_ratio = pc_PrivateBuildingAI.lumber_from_outside_count / (pc_PrivateBuildingAI.lumber_from_outside_count + pc_PrivateBuildingAI.lumber_to_industy_count);
                }

                if ((pc_PrivateBuildingAI.lumber_to_outside_count + pc_PrivateBuildingAI.lumber_to_industy_count) != 0)
                {
                    lumber_export_ratio = pc_PrivateBuildingAI.lumber_from_outside_count / (pc_PrivateBuildingAI.lumber_to_outside_count + pc_PrivateBuildingAI.lumber_to_industy_count);
                }
                //food
                if ((pc_PrivateBuildingAI.food_from_outside_count + pc_PrivateBuildingAI.food_to_industy_count) != 0)
                {
                    food_import_ratio = pc_PrivateBuildingAI.food_from_outside_count / (pc_PrivateBuildingAI.food_from_outside_count + pc_PrivateBuildingAI.food_to_industy_count);
                }

                if ((pc_PrivateBuildingAI.food_to_outside_count + pc_PrivateBuildingAI.food_to_industy_count) != 0)
                {
                    food_export_ratio = pc_PrivateBuildingAI.food_from_outside_count / (pc_PrivateBuildingAI.food_to_outside_count + pc_PrivateBuildingAI.food_to_industy_count);
                }
                //petrol
                if ((pc_PrivateBuildingAI.Petrol_from_outside_count + pc_PrivateBuildingAI.Petrol_to_industy_count) != 0)
                {
                    petrol_import_ratio = pc_PrivateBuildingAI.Petrol_from_outside_count / (pc_PrivateBuildingAI.Petrol_from_outside_count + pc_PrivateBuildingAI.Petrol_to_industy_count);
                }

                if ((pc_PrivateBuildingAI.Petrol_to_outside_count + pc_PrivateBuildingAI.Petrol_to_industy_count) != 0)
                {
                    petrol_export_ratio = pc_PrivateBuildingAI.Petrol_from_outside_count / (pc_PrivateBuildingAI.Petrol_to_outside_count + pc_PrivateBuildingAI.Petrol_to_industy_count);
                }
                //coal
                if ((pc_PrivateBuildingAI.coal_from_outside_count + pc_PrivateBuildingAI.coal_to_industy_count) != 0)
                {
                    coal_import_ratio = pc_PrivateBuildingAI.coal_from_outside_count / (pc_PrivateBuildingAI.coal_from_outside_count + pc_PrivateBuildingAI.coal_to_industy_count);
                }

                if ((pc_PrivateBuildingAI.coal_to_outside_count + pc_PrivateBuildingAI.coal_to_industy_count) != 0)
                {
                    coal_export_ratio = pc_PrivateBuildingAI.coal_from_outside_count / (pc_PrivateBuildingAI.coal_to_outside_count + pc_PrivateBuildingAI.coal_to_industy_count);
                }
                //logs
                if ((pc_PrivateBuildingAI.logs_from_outside_count + pc_PrivateBuildingAI.logs_to_industy_count) != 0)
                {
                    logs_import_ratio = pc_PrivateBuildingAI.logs_from_outside_count / (pc_PrivateBuildingAI.logs_from_outside_count + pc_PrivateBuildingAI.logs_to_industy_count);
                }

                if ((pc_PrivateBuildingAI.logs_to_outside_count + pc_PrivateBuildingAI.logs_to_industy_count) != 0)
                {
                    logs_export_ratio = pc_PrivateBuildingAI.logs_from_outside_count / (pc_PrivateBuildingAI.logs_to_outside_count + pc_PrivateBuildingAI.logs_to_industy_count);
                }
                //grain
                if ((pc_PrivateBuildingAI.Grain_from_outside_count + pc_PrivateBuildingAI.Grain_to_industy_count) != 0)
                {
                    grain_import_ratio = pc_PrivateBuildingAI.Grain_from_outside_count / (pc_PrivateBuildingAI.Grain_from_outside_count + pc_PrivateBuildingAI.Grain_to_industy_count);
                }

                if ((pc_PrivateBuildingAI.Grain_to_outside_count + pc_PrivateBuildingAI.Grain_to_industy_count) != 0)
                {
                    grain_export_ratio = pc_PrivateBuildingAI.Grain_from_outside_count / (pc_PrivateBuildingAI.Grain_to_outside_count + pc_PrivateBuildingAI.Grain_to_industy_count);
                }
                //oil
                if ((pc_PrivateBuildingAI.oil_from_outside_count + pc_PrivateBuildingAI.oil_to_industy_count) != 0)
                {
                    oil_import_ratio = pc_PrivateBuildingAI.oil_from_outside_count / (pc_PrivateBuildingAI.oil_from_outside_count + pc_PrivateBuildingAI.oil_to_industy_count);
                }

                if ((pc_PrivateBuildingAI.oil_to_outside_count + pc_PrivateBuildingAI.oil_to_industy_count) != 0)
                {
                    oil_export_ratio = pc_PrivateBuildingAI.oil_from_outside_count / (pc_PrivateBuildingAI.oil_to_outside_count + pc_PrivateBuildingAI.oil_to_industy_count);
                }
                //ore
                if ((pc_PrivateBuildingAI.ore_from_outside_count + pc_PrivateBuildingAI.ore_to_industy_count) != 0)
                {
                    ore_import_ratio = pc_PrivateBuildingAI.ore_from_outside_count / (pc_PrivateBuildingAI.ore_from_outside_count + pc_PrivateBuildingAI.ore_to_industy_count);
                }

                if ((pc_PrivateBuildingAI.ore_to_outside_count + pc_PrivateBuildingAI.ore_to_industy_count) != 0)
                {
                    ore_export_ratio = pc_PrivateBuildingAI.ore_from_outside_count / (pc_PrivateBuildingAI.ore_to_outside_count + pc_PrivateBuildingAI.ore_to_industy_count);
                }
                //good
                if ((pc_PrivateBuildingAI.shop_get_goods_from_local_count + pc_PrivateBuildingAI.shop_get_goods_from_outside_count) != 0)
                {
                    good_import_ratio = pc_PrivateBuildingAI.shop_get_goods_from_outside_count / (pc_PrivateBuildingAI.shop_get_goods_from_local_count + pc_PrivateBuildingAI.shop_get_goods_from_outside_count);
                }

                if ((pc_PrivateBuildingAI.shop_get_goods_from_local_count + pc_PrivateBuildingAI.industy_goods_to_outside_count) != 0)
                {
                    good_export_ratio = pc_PrivateBuildingAI.industy_goods_to_outside_count / (pc_PrivateBuildingAI.shop_get_goods_from_local_count + pc_PrivateBuildingAI.industy_goods_to_outside_count);
                }

                pc_PrivateBuildingAI.comm_profit = 0.3f; //update later
                pc_PrivateBuildingAI.indu_profit = (float)(5f + 2f * (5f - good_export_ratio - food_import_ratio - lumber_import_ratio - petrol_import_ratio - coal_import_ratio))/100f;
                pc_PrivateBuildingAI.food_profit = (float)(5f + 5f * (2f - food_export_ratio - grain_import_ratio))/100f;
                pc_PrivateBuildingAI.lumber_profit = (float)(5f + 5f * (2f - lumber_export_ratio - logs_import_ratio))/100f;
                pc_PrivateBuildingAI.coal_profit = (float)(5f + 5f * (2f - coal_export_ratio - ore_import_ratio))/100f;
                pc_PrivateBuildingAI.petrol_profit = (float)(5f + 5f * (2f - petrol_export_ratio - oil_import_ratio))/100f;

                pc_PrivateBuildingAI.log_profit = (float)(5f + 10f * (1f - logs_export_ratio))/100f;
                pc_PrivateBuildingAI.grain_profit = (float)(5f + 10f * (1f - grain_export_ratio))/100f;
                pc_PrivateBuildingAI.oil_profit = (float)(5f + 10f * (1f - oil_export_ratio))/100f;
                pc_PrivateBuildingAI.ore_profit = (float)(5f + 10f * (1f - ore_export_ratio))/100f;

            }
        }
    }
}

