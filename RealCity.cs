using System;
using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using System.Reflection;
using System.IO;

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
            FileStream fs = File.Create("RealCity.txt");
            fs.Close();

            var srcMethod = typeof(TransferManager).GetMethod("StartTransfer", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            var destMethod = typeof(pc_TransferManager).GetMethod("StartTransfer", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            RedirectionHelper.RedirectCalls(srcMethod, destMethod);

            //var srcMethod1 = typeof(TransferManager).GetMethod("AddIncomingOffer", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            //var destMethod1 = typeof(pc_TransferManager).GetMethod("AddIncomingOffer", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            //RedirectionHelper.RedirectCalls(srcMethod1, destMethod1);

            var srcMethod2 = typeof(IndustrialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            var destMethod2 = typeof(pc_IndustrialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            RedirectionHelper.RedirectCalls(srcMethod2, destMethod2);

            //var srcMethod3 = typeof(IndustrialBuildingAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            //var destMethod3 = typeof(pc_IndustrialBuildingAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            //RedirectionHelper.RedirectCalls(srcMethod3, destMethod3);

            var srcMethod4 = typeof(IndustrialExtractorAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            var destMethod4 = typeof(pc_IndustrialExtractorAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            RedirectionHelper.RedirectCalls(srcMethod4, destMethod4);

            //var srcMethod5 = typeof(IndustrialExtractorAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            //var destMethod5 = typeof(pc_IndustrialExtractorAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            //RedirectionHelper.RedirectCalls(srcMethod5, destMethod5);

            var srcMethod6 = typeof(CommercialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason),typeof(int).MakeByRefType() }, null);
            var destMethod6 = typeof(pc_CommercialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
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

            var srcMethod10 = typeof(HumanAI).GetMethod("ArriveAtDestination", BindingFlags.NonPublic | BindingFlags.Instance);
            var destMethod10 = typeof(pc_HumanAI).GetMethod("ArriveAtDestination_1", BindingFlags.NonPublic | BindingFlags.Instance);
            RedirectionHelper.RedirectCalls(srcMethod10, destMethod10);

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

            var srcMethod19 = typeof(IndustrialExtractorAI).GetMethod("GetLevelUpInfo", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(float).MakeByRefType() }, null);
            var destMethod19 = typeof(pc_IndustrialExtractorAI).GetMethod("GetLevelUpInfo", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(float).MakeByRefType() }, null);
            RedirectionHelper.RedirectCalls(srcMethod19, destMethod19);

            var srcMethod20 = typeof(CommercialBuildingAI).GetMethod("GetLevelUpInfo", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(float).MakeByRefType() }, null);
            var destMethod20 = typeof(pc_CommercialBuildingAI).GetMethod("GetLevelUpInfo", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(float).MakeByRefType() }, null);
            RedirectionHelper.RedirectCalls(srcMethod20, destMethod20);

            var srcMethod21 = typeof(IndustrialBuildingAI).GetMethod("GetLevelUpInfo", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(float).MakeByRefType() }, null);
            var destMethod21 = typeof(pc_IndustrialBuildingAI).GetMethod("GetLevelUpInfo", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(float).MakeByRefType() }, null);
            RedirectionHelper.RedirectCalls(srcMethod21, destMethod21);

            var srcMethod22 = typeof(CargoTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var destMethod22 = typeof(pc_CargoTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            RedirectionHelper.RedirectCalls(srcMethod22, destMethod22);

            //var srcMethod23 = typeof(CargoTruckAI).GetMethod("ArriveAtSource", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            //var destMethod23 = typeof(pc_CargoTruckAI).GetMethod("ArriveAtSource", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            //RedirectionHelper.RedirectCalls(srcMethod23, destMethod23);

            //var srcMethod23 = typeof(VehicleAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(Vehicle.Frame).MakeByRefType(), typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(int) }, null);
            //var destMethod23 = typeof(pc_VehicleAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(Vehicle.Frame).MakeByRefType(), typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(int) }, null);
            //RedirectionHelper.RedirectCalls(srcMethod23, destMethod23);


            //private int GetCarProbability_1(ushort instanceID, ref CitizenInstance citizenData, Citizen.AgeGroup ageGroup)
            //var srcMethod24 = typeof(ResidentAI).GetMethod("GetCarProbability", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(Citizen.AgeGroup) }, null);
            //var destMethod24 = typeof(pc_ResidentAI).GetMethod("GetCarProbability_1", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(Citizen.AgeGroup) }, null);
            //RedirectionHelper.RedirectCalls(srcMethod24, destMethod24);

            var srcMethod25 = typeof(HumanAI).GetMethod("EnterVehicle", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType() }, null);
            var destMethod25 = typeof(pc_HumanAI).GetMethod("EnterVehicle_1", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType() }, null);
            RedirectionHelper.RedirectCalls(srcMethod25, destMethod25);

            var srcMethod26 = typeof(ResidentAI).GetMethod("StartPathFind", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType() }, null);
            var destMethod26 = typeof(pc_ResidentAI_1).GetMethod("StartPathFind", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType() }, null);
            RedirectionHelper.RedirectCalls(srcMethod26, destMethod26);

            //var srcMethod27 = typeof(CommonBuildingAI).GetMethod("GetWorkBehaviour", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Citizen.BehaviourData).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType() }, null);
            //var destMethod27 = typeof(pc_PrivateBuildingAI_1).GetMethod("GetWorkBehaviour_1", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Citizen.BehaviourData).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType() }, null);
            //RedirectionHelper.RedirectCalls(srcMethod27, destMethod27);

            var srcMethod28 = typeof(OfficeBuildingAI).GetMethod("GetOutgoingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            var destMethod28 = typeof(pc_OfficeBuildingAI).GetMethod("GetOutgoingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            RedirectionHelper.RedirectCalls(srcMethod28, destMethod28);

            var srcMethod29 = typeof(ZoneManager).GetMethod("CalculateResidentialDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] {typeof(District).MakeByRefType() }, null);
            var destMethod29 = typeof(pc_ZoneManager).GetMethod("CalculateResidentialDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] {typeof(District).MakeByRefType() }, null);
            RedirectionHelper.RedirectCalls(srcMethod29, destMethod29);

            var srcMethod30 = typeof(ZoneManager).GetMethod("CalculateIncomingResidentDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            var destMethod30 = typeof(pc_ZoneManager).GetMethod("CalculateIncomingResidentDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            RedirectionHelper.RedirectCalls(srcMethod30, destMethod30);

            var srcMethod31 = typeof(ZoneManager).GetMethod("CalculateCommercialDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            var destMethod31 = typeof(pc_ZoneManager).GetMethod("CalculateCommercialDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            RedirectionHelper.RedirectCalls(srcMethod31, destMethod31);

            var srcMethod32 = typeof(ZoneManager).GetMethod("CalculateWorkplaceDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            var destMethod32 = typeof(pc_ZoneManager).GetMethod("CalculateWorkplaceDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            RedirectionHelper.RedirectCalls(srcMethod32, destMethod32);

            var srcMethod33 = typeof(CargoTruckAI).GetMethod("SetSource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(ushort) }, null);
            var destMethod33 = typeof(pc_CargoTruckAI).GetMethod("SetSource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(ushort) }, null);
            RedirectionHelper.RedirectCalls(srcMethod33, destMethod33);

            var srcMethod34 = typeof(OutsideConnectionAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            var destMethod34 = typeof(pc_OutsideConnectionAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            RedirectionHelper.RedirectCalls(srcMethod34, destMethod34);

            var srcMethod35 = typeof(OutsideConnectionAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            var destMethod35 = typeof(pc_OutsideConnectionAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            RedirectionHelper.RedirectCalls(srcMethod35, destMethod35);

            var srcMethod36 = typeof(OutsideConnectionAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            var destMethod36 = typeof(pc_OutsideConnectionAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            RedirectionHelper.RedirectCalls(srcMethod36, destMethod36);
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
                comm_data.current_time = Singleton<SimulationManager>.instance.m_currentDayTimeHour;
                uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
                uint num2 = currentFrameIndex & 255u;
                //DebugLog.LogToFileOnly("OnUpdateMoneyAmount num2 = " + num2.ToString());
                if ((num2 == 255u) && (comm_data.current_time != comm_data.prev_time))
                {
                    //DebugLog.LogToFileOnly("process once update money");
                    citizen_status();
                    vehicle_status();
                    building_status();
                    caculate_goverment_employee_outcome();
                    caculate_profit();
                    caculate_citizen_transport_fee();
                    comm_data.is_updated = true;
                    comm_data.update_money_count++;
                    if (comm_data.update_money_count == 17)
                    {
                        comm_data.update_money_count = 0;
                    }
                    pc_EconomyManager.clean_current(comm_data.update_money_count);
                    comm_data.prev_time = comm_data.current_time;
                    //DebugLog.LogToFileOnly("update_money_count is " + comm_data.update_money_count.ToString());
                 }
                //if(num2 != 255u)
                //{
                //    comm_data.is_updated = false;
                //}
                return internalMoneyAmount;
            }

            public void caculate_goverment_employee_outcome()
            {
                if (Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_populationData.m_finalCount > 0)
                {
                    ItemClass temp = new ItemClass();
                    temp.m_service = ItemClass.Service.PublicTransport;
                    temp.m_subService = ItemClass.SubService.PublicTransportBus;
                    //use this to diff make in-game mantenance and goverment_salary_expense 
                    temp.m_layer = ItemClass.Layer.Markers;
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
                RealCityUI.bus_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2 / comm_data.game_income_outcome_multiple;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportTram;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.tram_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2 / comm_data.game_income_outcome_multiple;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportMetro;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.metro_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2 / comm_data.game_income_outcome_multiple;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportTrain;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.train_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2 / comm_data.game_income_outcome_multiple;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportTaxi;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.taxi_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2 / comm_data.game_income_outcome_multiple;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportPlane;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.plane_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2 / comm_data.game_income_outcome_multiple;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportShip;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.ship_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2 / comm_data.game_income_outcome_multiple;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportMonorail;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.monorail_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2 / comm_data.game_income_outcome_multiple;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportCableCar;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.cablecar_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2 / comm_data.game_income_outcome_multiple;

                //add vehicle transport_fee
                comm_data.temp_total_citizen_vehical_time_last = comm_data.temp_total_citizen_vehical_time;
                comm_data.temp_total_citizen_vehical_time = 0;

                //assume that 1 time will cost 5fen car oil money
                comm_data.all_transport_fee = comm_data.public_transport_fee + comm_data.temp_total_citizen_vehical_time_last * 3 * comm_data.game_income_outcome_multiple;

                if (comm_data.family_count > 0)
                {
                    if ((comm_data.all_transport_fee / comm_data.family_count) > 40)
                    {
                        comm_data.citizen_average_transport_fee = 40;
                    }
                    else
                    {
                        comm_data.citizen_average_transport_fee = (byte)(comm_data.all_transport_fee / comm_data.family_count);
                    }
                }
            }


            public void building_status()
            {
                int office_gen_num = pc_PrivateBuildingAI.all_office_level1_building_num_final + pc_PrivateBuildingAI.all_office_level2_building_num_final + pc_PrivateBuildingAI.all_office_level3_building_num_final;
                int profit_building_num = 0;
                int high_educated_data = 0;
                int medium_educated_data = 0;
                int low_educated_data = 0;
                profit_building_num += pc_PrivateBuildingAI.all_farmer_building_profit_final;
                profit_building_num += pc_PrivateBuildingAI.all_foresty_building_profit_final;
                profit_building_num += pc_PrivateBuildingAI.all_oil_building_profit_final;
                profit_building_num += pc_PrivateBuildingAI.all_ore_building_profit_final;
                profit_building_num += pc_PrivateBuildingAI.all_industry_building_profit_final;
                if (office_gen_num != 0)
                {
                    if (comm_data.citizen_count != 0)
                    {
                        high_educated_data = (int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_educated3Data.m_finalCount;
                        medium_educated_data = (int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_educated2Data.m_finalCount;
                        low_educated_data = (int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_educated1Data.m_finalCount;
                        pc_PrivateBuildingAI.office_gen_salary_index = ((2*high_educated_data + 1f * medium_educated_data + 0.5f * low_educated_data) * profit_building_num) / (comm_data.citizen_count * office_gen_num);
                    }
                }

                if (pc_PrivateBuildingAI.all_office_high_tech_building_num_final != 0)
                {
                    if (comm_data.citizen_count != 0)
                    {
                        high_educated_data = (int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_educated3Data.m_finalCount;
                        medium_educated_data = (int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_educated2Data.m_finalCount;
                        low_educated_data = (int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_educated1Data.m_finalCount;
                        pc_PrivateBuildingAI.office_high_tech_salary_index = ((2f*high_educated_data + 1f * medium_educated_data + 0.5f * low_educated_data) * pc_PrivateBuildingAI.all_office_level3_building_num_final) / (comm_data.citizen_count * pc_PrivateBuildingAI.all_office_high_tech_building_num_final);
                    }
                }

                pc_PrivateBuildingAI.office_high_tech_salary_index = (pc_PrivateBuildingAI.office_high_tech_salary_index > 1) ? 1 : pc_PrivateBuildingAI.office_high_tech_salary_index;
                pc_PrivateBuildingAI.office_high_tech_salary_index = (pc_PrivateBuildingAI.office_high_tech_salary_index < 0.1f) ? 0.1f : pc_PrivateBuildingAI.office_high_tech_salary_index;

                pc_PrivateBuildingAI.office_gen_salary_index = (pc_PrivateBuildingAI.office_gen_salary_index > 1) ? 1 : pc_PrivateBuildingAI.office_gen_salary_index;
                pc_PrivateBuildingAI.office_gen_salary_index = (pc_PrivateBuildingAI.office_gen_salary_index < 0.1f) ? 0.1f : pc_PrivateBuildingAI.office_gen_salary_index;

            }

            public void citizen_status()
            {
                comm_data.citizen_count = (int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_populationData.m_finalCount;

                if (comm_data.citizen_count > 100)
                {
                    uint medium_citizen = (uint)(comm_data.family_count - comm_data.family_weight_stable_high - comm_data.family_weight_stable_low);
                    if (medium_citizen < 0)
                    {
                        DebugLog.LogToFileOnly("should be wrong, medium_citizen < 0");
                        medium_citizen = 0;
                    }
                    if (comm_data.family_count != 0)
                    {
                        comm_data.resident_consumption_rate = (float)((float)(2 * comm_data.family_weight_stable_high + medium_citizen / 2) / (float)comm_data.family_count);
                    }
                }
                else
                {
                    //do nothing
                }


                comm_data.mantain_and_land_fee_decrease = (byte)(1000f / (Singleton<DistrictManager>.instance.m_districts.m_buffer[0].GetLandValue() + 10f));
                comm_data.salary_idex = (Singleton<DistrictManager>.instance.m_districts.m_buffer[0].GetLandValue() + 50f) / 120f;

                /*CitizenManager instance = Singleton<CitizenManager>.instance;
                for (int i = 0; i < 524288; i = i + 1)
                {
                    CitizenUnit citizenunit = instance.m_units.m_buffer[i];
                    if (citizenunit.m_flags.IsFlagSet(CitizenUnit.Flags.Created))
                    {
                        int j;
                        for (j = 0; j < 5; j++)
                        {
                            uint citizen = citizenunit.GetCitizen(j);
                            if (citizen != 0u)
                            {
                                CitizenManager instance1 = Singleton<CitizenManager>.instance;
                                CitizenInfo citizenInfo = instance1.m_citizens.m_buffer[(int)((UIntPtr)citizen)].GetCitizenInfo(citizen);
                                //DebugLog.LogToFileOnly("m_citizenAI is " + citizenInfo.m_citizenAI.GetType().ToString());
                                if (citizenInfo.m_class.m_service == ItemClass.Service.Citizen)
                                {
                                    comm_data.citizen_count = comm_data.citizen_count + 1;
                                }
                            }
                        }
                    }
                }*/
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
                            if (!vehicle.m_flags.IsFlagSet(Vehicle.Flags.Stopped))
                            {
                                comm_data.vehical_transfer_time[i] = (ushort)(comm_data.vehical_transfer_time[i] + 1);
                            }
                            else
                            {
                                comm_data.vehical_transfer_time[i] = 0;
                            }
                        }
                    }
                    else
                    {
                        comm_data.vehical_transfer_time[i] = 0;
                    }
                }
            }

            public void caculate_profit()
            {
                if(comm_data.update_outside_count > 64)
                {
                    comm_data.update_outside_count = 0;
                }
                comm_data.update_outside_count++;
                /*float lumber_export_ratio = 0;
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
                float good_import_ratio = 0;*/
                //lumber
                if ((pc_PrivateBuildingAI.lumber_from_outside_count_final + pc_PrivateBuildingAI.lumber_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.lumber_import_ratio = (float)pc_PrivateBuildingAI.lumber_from_outside_count_final / (float)(pc_PrivateBuildingAI.lumber_from_outside_count_final + pc_PrivateBuildingAI.lumber_to_industy_count_final);
                }
                else
                {
                    pc_PrivateBuildingAI.lumber_import_ratio = 1;
                }

                if ((pc_PrivateBuildingAI.lumber_to_outside_count_final + pc_PrivateBuildingAI.lumber_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.lumber_export_ratio = (float)pc_PrivateBuildingAI.lumber_to_outside_count_final / (float)(pc_PrivateBuildingAI.lumber_to_outside_count_final + pc_PrivateBuildingAI.lumber_to_industy_count_final);
                }
                else
                {
                    pc_PrivateBuildingAI.lumber_export_ratio = 1;
                }
                //food
                if ((pc_PrivateBuildingAI.food_from_outside_count_final + pc_PrivateBuildingAI.food_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.food_import_ratio = (float)pc_PrivateBuildingAI.food_from_outside_count_final / (float)(pc_PrivateBuildingAI.food_from_outside_count_final + pc_PrivateBuildingAI.food_to_industy_count_final);
                }
                else
                {
                    pc_PrivateBuildingAI.food_import_ratio = 1;
                }

                if ((pc_PrivateBuildingAI.food_to_outside_count_final + pc_PrivateBuildingAI.food_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.food_export_ratio = (float)pc_PrivateBuildingAI.food_to_outside_count_final / (float)(pc_PrivateBuildingAI.food_to_outside_count_final + pc_PrivateBuildingAI.food_to_industy_count_final);
                }
                else
                {
                    pc_PrivateBuildingAI.food_export_ratio = 1;
                }
                //petrol
                if ((pc_PrivateBuildingAI.Petrol_from_outside_count_final + pc_PrivateBuildingAI.Petrol_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.petrol_import_ratio = (float)pc_PrivateBuildingAI.Petrol_from_outside_count_final / (float)(pc_PrivateBuildingAI.Petrol_from_outside_count_final + pc_PrivateBuildingAI.Petrol_to_industy_count_final);
                }
                else
                {
                    pc_PrivateBuildingAI.petrol_import_ratio = 1;
                }

                if ((pc_PrivateBuildingAI.Petrol_to_outside_count_final + pc_PrivateBuildingAI.Petrol_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.petrol_export_ratio = (float)pc_PrivateBuildingAI.Petrol_to_outside_count_final / (float)(pc_PrivateBuildingAI.Petrol_to_outside_count_final + pc_PrivateBuildingAI.Petrol_to_industy_count_final);
                }
                else
                {
                    pc_PrivateBuildingAI.petrol_export_ratio = 1;
                }
                //coal
                if ((pc_PrivateBuildingAI.coal_from_outside_count_final + pc_PrivateBuildingAI.coal_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.coal_import_ratio = (float)pc_PrivateBuildingAI.coal_from_outside_count_final / (float)(pc_PrivateBuildingAI.coal_from_outside_count_final + pc_PrivateBuildingAI.coal_to_industy_count_final);
                }
                else
                {
                    pc_PrivateBuildingAI.coal_import_ratio = 1f;
                }

                if ((pc_PrivateBuildingAI.coal_to_outside_count_final + pc_PrivateBuildingAI.coal_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.coal_export_ratio = (float)pc_PrivateBuildingAI.coal_to_outside_count_final / (float)(pc_PrivateBuildingAI.coal_to_outside_count_final + pc_PrivateBuildingAI.coal_to_industy_count_final);
                }
                else
                {
                    pc_PrivateBuildingAI.coal_export_ratio = 1f;
                }
                //logs
                if ((pc_PrivateBuildingAI.logs_from_outside_count_final + pc_PrivateBuildingAI.logs_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.log_import_ratio = (float)pc_PrivateBuildingAI.logs_from_outside_count_final / (float)(pc_PrivateBuildingAI.logs_from_outside_count_final + pc_PrivateBuildingAI.logs_to_industy_count_final);
                }
                else
                {
                    pc_PrivateBuildingAI.log_import_ratio = 1;
                }

                if ((pc_PrivateBuildingAI.logs_to_outside_count_final + pc_PrivateBuildingAI.logs_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.log_export_ratio = (float)pc_PrivateBuildingAI.logs_to_outside_count_final / (float)(pc_PrivateBuildingAI.logs_to_outside_count_final + pc_PrivateBuildingAI.logs_to_industy_count_final);
                }
                else
                {
                    pc_PrivateBuildingAI.log_export_ratio = 1;
                }
                //grain
                if ((pc_PrivateBuildingAI.Grain_from_outside_count_final + pc_PrivateBuildingAI.Grain_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.grain_import_ratio = (float)pc_PrivateBuildingAI.Grain_from_outside_count_final / (float)(pc_PrivateBuildingAI.Grain_from_outside_count_final + pc_PrivateBuildingAI.Grain_to_industy_count_final);
                }
                else
                {
                    pc_PrivateBuildingAI.grain_import_ratio = 1;
                }

                if ((pc_PrivateBuildingAI.Grain_to_outside_count_final + pc_PrivateBuildingAI.Grain_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.grain_export_ratio = (float)pc_PrivateBuildingAI.Grain_to_outside_count_final / (float)(pc_PrivateBuildingAI.Grain_to_outside_count_final + pc_PrivateBuildingAI.Grain_to_industy_count_final);
                }
                else
                {
                    pc_PrivateBuildingAI.grain_export_ratio = 1;
                }
                //oil
                if ((pc_PrivateBuildingAI.oil_from_outside_count_final + pc_PrivateBuildingAI.oil_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.oil_import_ratio = (float)pc_PrivateBuildingAI.oil_from_outside_count_final / (float)(pc_PrivateBuildingAI.oil_from_outside_count_final + pc_PrivateBuildingAI.oil_to_industy_count_final);
                }
                else
                {
                    pc_PrivateBuildingAI.oil_import_ratio = 1;
                }

                if ((pc_PrivateBuildingAI.oil_to_outside_count_final + pc_PrivateBuildingAI.oil_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.oil_export_ratio = (float)pc_PrivateBuildingAI.oil_to_outside_count_final / (float)(pc_PrivateBuildingAI.oil_to_outside_count_final + pc_PrivateBuildingAI.oil_to_industy_count_final);
                }
                else
                {
                    pc_PrivateBuildingAI.oil_export_ratio = 1;
                }
                //ore
                if ((pc_PrivateBuildingAI.ore_from_outside_count_final + pc_PrivateBuildingAI.ore_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.ore_import_ratio = (float)pc_PrivateBuildingAI.ore_from_outside_count_final / (float)(pc_PrivateBuildingAI.ore_from_outside_count_final + pc_PrivateBuildingAI.ore_to_industy_count_final);
                }
                else
                {
                    pc_PrivateBuildingAI.ore_import_ratio = 1;
                }

                if ((pc_PrivateBuildingAI.ore_to_outside_count_final + pc_PrivateBuildingAI.ore_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.ore_export_ratio = (float)pc_PrivateBuildingAI.ore_to_outside_count_final / (float)(pc_PrivateBuildingAI.ore_to_outside_count_final + pc_PrivateBuildingAI.ore_to_industy_count_final);
                }
                else
                {
                    pc_PrivateBuildingAI.ore_export_ratio = 1;
                }
                //good
                if ((pc_PrivateBuildingAI.shop_get_goods_from_local_count_level1_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final  + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final + pc_PrivateBuildingAI.shop_get_goods_from_outside_count_final) != 0)
                {
                    pc_PrivateBuildingAI.good_import_ratio = (float)pc_PrivateBuildingAI.shop_get_goods_from_outside_count_final / (float)(pc_PrivateBuildingAI.shop_get_goods_from_local_count_level1_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final + pc_PrivateBuildingAI.shop_get_goods_from_outside_count_final);
                }
                else
                {
                    pc_PrivateBuildingAI.good_import_ratio = 1;
                }

                if ((pc_PrivateBuildingAI.shop_get_goods_from_local_count_level1_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final + pc_PrivateBuildingAI.industy_goods_to_outside_count_final) != 0)
                {
                    pc_PrivateBuildingAI.good_export_ratio = (float)pc_PrivateBuildingAI.industy_goods_to_outside_count_final / (float)(pc_PrivateBuildingAI.shop_get_goods_from_local_count_level1_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final + pc_PrivateBuildingAI.industy_goods_to_outside_count_final);
                }
                else
                {
                    pc_PrivateBuildingAI.good_export_ratio = 1;
                }

                if ((pc_PrivateBuildingAI.shop_get_goods_from_local_count_level1_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final + pc_PrivateBuildingAI.industy_goods_to_outside_count_final) != 0)
                {
                    pc_PrivateBuildingAI.good_level2_ratio = (float)(pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final) / (float)(pc_PrivateBuildingAI.shop_get_goods_from_local_count_level1_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final + pc_PrivateBuildingAI.industy_goods_to_outside_count_final);
                } else
                {
                    pc_PrivateBuildingAI.good_level2_ratio = 0f;
                }

                if ((pc_PrivateBuildingAI.shop_get_goods_from_local_count_level1_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final + pc_PrivateBuildingAI.industy_goods_to_outside_count_final) != 0)
                {
                    pc_PrivateBuildingAI.good_level3_ratio = (float)(pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final) / (float)(pc_PrivateBuildingAI.shop_get_goods_from_local_count_level1_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final + pc_PrivateBuildingAI.industy_goods_to_outside_count_final);
                }
                else
                {
                    pc_PrivateBuildingAI.good_level3_ratio = 0f;
                }
                /*pc_PrivateBuildingAI.comm_profit = 0.2f; //update later
                pc_PrivateBuildingAI.indu_profit = (float)(5f + 2f * (5f - good_export_ratio - food_import_ratio - lumber_import_ratio - petrol_import_ratio - coal_import_ratio))/100f;
                pc_PrivateBuildingAI.food_profit = (float)(5f + 5f * (2f - food_export_ratio - grain_import_ratio))/100f;
                pc_PrivateBuildingAI.lumber_profit = (float)(5f + 5f * (2f - lumber_export_ratio - logs_import_ratio))/100f;
                pc_PrivateBuildingAI.coal_profit = (float)(5f + 5f * (2f - coal_export_ratio - ore_import_ratio))/100f;
                pc_PrivateBuildingAI.petrol_profit = (float)(5f + 5f * (2f - petrol_export_ratio - oil_import_ratio))/100f;

                pc_PrivateBuildingAI.log_profit = (float)(5f + 10f * (1f - logs_export_ratio))/100f;
                pc_PrivateBuildingAI.grain_profit = (float)(5f + 10f * (1f - grain_export_ratio))/100f;
                pc_PrivateBuildingAI.oil_profit = (float)(5f + 10f * (1f - oil_export_ratio))/100f;
                pc_PrivateBuildingAI.ore_profit = (float)(5f + 10f * (1f - ore_export_ratio))/100f;*/

            }
        }
    }
}

