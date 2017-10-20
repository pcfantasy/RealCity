using ColossalFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RealCity
{

    public class pc_PrivateBuildingAI : CommonBuildingAI
    {
        //2.1 building income
        //2.1.1 profit and tranport cost
        public static ushort resident_shopping_count = 0;
        public static ushort resident_leisure_count = 0;
        public static ushort shop_get_goods_from_local_count = 0;
        public static ushort shop_get_goods_from_outside_count = 0;
        public static ushort industy_goods_to_outside_count = 0;
        public static ushort Grain_to_outside_count = 0;
        public static ushort Grain_to_industy_count = 0;
        public static ushort Grain_from_outside_count = 0;
        public static ushort food_to_outside_count = 0;
        public static ushort food_to_industy_count = 0;
        public static ushort food_from_outside_count = 0;
        public static ushort oil_to_outside_count = 0;
        public static ushort oil_to_industy_count = 0;
        public static ushort oil_from_outside_count = 0;
        public static ushort Petrol_to_outside_count = 0;
        public static ushort Petrol_to_industy_count = 0;
        public static ushort Petrol_from_outside_count = 0;
        public static ushort ore_to_outside_count = 0;
        public static ushort ore_to_industy_count = 0;
        public static ushort ore_from_outside_count = 0;
        public static ushort coal_to_outside_count = 0;
        public static ushort coal_to_industy_count = 0;
        public static ushort coal_from_outside_count = 0;
        public static ushort logs_to_outside_count = 0;
        public static ushort logs_to_industy_count = 0;
        public static ushort logs_from_outside_count = 0;
        public static ushort lumber_to_outside_count = 0;
        public static ushort lumber_to_industy_count = 0;
        public static ushort lumber_from_outside_count = 0;
        public static ushort visit_shopping_count = 0;
        public static ushort visit_leisure_count = 0;

        public static float comm_profit = 5;
        public static float indu_profit = 5;
        public static float food_profit = 5;
        public static float petrol_profit = 5;
        public static float coal_profit = 5;
        public static float lumber_profit = 5;
        public static float oil_profit = 5;
        public static float ore_profit = 5;
        public static float grain_profit = 5;
        public static float log_profit = 5;

        public static bool all_building_process_done;
        public static ushort all_comm_building_profit = 0;
        public static ushort all_industry_building_profit = 0;
        public static ushort all_foresty_building_profit = 0;
        public static ushort all_farmer_building_profit = 0;
        public static ushort all_oil_building_profit = 0;
        public static ushort all_ore_building_profit = 0;
        public static ushort all_comm_building_loss = 0;
        public static ushort all_industry_building_loss = 0;
        public static ushort all_foresty_building_loss = 0;
        public static ushort all_farmer_building_loss = 0;
        public static ushort all_oil_building_loss = 0;
        public static ushort all_ore_building_loss = 0;
        public static ushort all_buildings = 0;
        public static uint total_cargo_vehical_time = 0;
        public static uint temp_total_cargo_vehical_time = 0;//temp use
        public static uint temp_total_cargo_vehical_time_last = 0;//temp use
        public static uint total_cargo_transfer_size = 0;
        public static uint total_train_transfer_size = 0;
        public static uint total_ship_transfer_size = 0;

        public static uint prebuidlingid = 0;
        // PrivateBuildingAI
        protected void SimulationStepActive_1(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
        {
            base.SimulationStepActive(buildingID, ref buildingData, ref frameData);
            process_land_fee(buildingData, buildingID);
            caculate_employee_outcome(buildingData, buildingID);
            limit_and_check_building_money(buildingData,buildingID);
            if ((buildingData.m_problems & Notification.Problem.MajorProblem) != Notification.Problem.None)
            {
                if (buildingData.m_fireIntensity == 0)
                {
                    buildingData.m_majorProblemTimer = (byte)Mathf.Min(255, (int)(buildingData.m_majorProblemTimer + 1));
                    if (buildingData.m_majorProblemTimer >= 64 && !Singleton<BuildingManager>.instance.m_abandonmentDisabled)
                    {
                        if ((buildingData.m_flags & Building.Flags.Flooded) != Building.Flags.None)
                        {
                            InstanceID id = default(InstanceID);
                            id.Building = buildingID;
                            Singleton<InstanceManager>.instance.SetGroup(id, null);
                            buildingData.m_flags &= ~Building.Flags.Flooded;
                        }
                        buildingData.m_majorProblemTimer = 192;
                        buildingData.m_flags &= ~Building.Flags.Active;
                        buildingData.m_flags |= Building.Flags.Abandoned;
                        buildingData.m_problems = (Notification.Problem.FatalProblem | (buildingData.m_problems & ~Notification.Problem.MajorProblem));
                        base.RemovePeople(buildingID, ref buildingData, 100);
                        this.BuildingDeactivated(buildingID, ref buildingData);
                        Singleton<BuildingManager>.instance.UpdateBuildingRenderer(buildingID, true);
                    }
                }
            }
            else
            {
                buildingData.m_majorProblemTimer = 0;
            }

            int i;
            if (prebuidlingid < buildingID)
            {
                for (i = (int)(prebuidlingid + 1); i < buildingID; i++)
                {
                    comm_data.building_money[i] = 0;
                }
            }
            prebuidlingid = buildingID;
        }

        //        public void building_status()
        //        {
        //            BuildingManager instance = Singleton<BuildingManager>.instance;
        //            for (int i = 0; i < 49152; i = i + 1)
        //            {
        //                Building building = instance.m_buildings.m_buffer[i];
        //                if (building.m_flags.IsFlagSet(Building.Flags.Created) && !building.m_flags.IsFlagSet(Building.Flags.Deleted) && !building.m_flags.IsFlagSet(Building.Flags.Untouchable))
        //                {
        //                    if ((building.Info.m_class.m_service == ItemClass.Service.Commercial) || (building.Info.m_class.m_service == ItemClass.Service.Industrial) || (building.Info.m_class.m_service == ItemClass.Service.Office))
        //                    {
        //                        caculate_employee_outcome(building, i);
        //                        process_land_fee(building, i);
        //                    }
        //                    else if (building.Info.m_class.m_service == ItemClass.Service.Residential)
        //                    {
        //                        process_land_fee(building, i);
        //                    }
        //                }
        //            }
        //        }

        public void limit_and_check_building_money(Building building, ushort buildingID)
        {
            if (comm_data.building_money[buildingID] > 100000)
            {
                comm_data.building_money[buildingID] = 100000;
            }
            else if (comm_data.building_money[buildingID] < -100000)
            {
                comm_data.building_money[buildingID] = -100000;
            }

            if (comm_data.building_money[buildingID] > 0)
            {
                switch (building.Info.m_class.m_subService)
                {
                    case ItemClass.SubService.IndustrialFarming:
                        all_farmer_building_profit = (ushort)(all_farmer_building_profit + 1);
                        break;
                    case ItemClass.SubService.IndustrialForestry:
                        all_foresty_building_profit = (ushort)(all_foresty_building_profit + 1);
                        break;
                    case ItemClass.SubService.IndustrialOil:
                        all_oil_building_profit = (ushort)(all_oil_building_profit + 1);
                        break;
                    case ItemClass.SubService.IndustrialOre:
                        all_ore_building_profit = (ushort)(all_ore_building_profit + 1);
                        break;
                    case ItemClass.SubService.IndustrialGeneric:
                        all_comm_building_profit = (ushort)(all_comm_building_profit + 1);
                        break;
                    case ItemClass.SubService.CommercialHigh:
                        all_comm_building_profit = (ushort)(all_comm_building_profit + 1);
                        break;
                    case ItemClass.SubService.CommercialLow:
                        all_comm_building_profit = (ushort)(all_comm_building_profit + 1);
                        break;
                    case ItemClass.SubService.CommercialLeisure:
                        all_comm_building_profit = (ushort)(all_comm_building_profit + 1);
                        break;
                    case ItemClass.SubService.CommercialTourist:
                        all_comm_building_profit = (ushort)(all_comm_building_profit + 1);
                        break;
                    case ItemClass.SubService.CommercialEco:
                        all_comm_building_profit = (ushort)(all_comm_building_profit + 1);
                        break;
                    default: break;
                }
            }
            else
            {
                switch (building.Info.m_class.m_subService)
                {
                    case ItemClass.SubService.IndustrialFarming:
                        all_farmer_building_loss = (ushort)(all_farmer_building_loss + 1);
                        break;
                    case ItemClass.SubService.IndustrialForestry:
                        all_foresty_building_loss = (ushort)(all_foresty_building_loss + 1);
                        break;
                    case ItemClass.SubService.IndustrialOil:
                        all_oil_building_loss = (ushort)(all_oil_building_loss + 1);
                        break;
                    case ItemClass.SubService.IndustrialOre:
                        all_ore_building_loss = (ushort)(all_ore_building_loss + 1);
                        break;
                    case ItemClass.SubService.IndustrialGeneric:
                        all_comm_building_loss = (ushort)(all_comm_building_loss + 1);
                        break;
                    case ItemClass.SubService.CommercialHigh:
                        all_comm_building_loss = (ushort)(all_comm_building_loss + 1);
                        break;
                    case ItemClass.SubService.CommercialLow:
                        all_comm_building_loss = (ushort)(all_comm_building_loss + 1);
                        break;
                    case ItemClass.SubService.CommercialLeisure:
                        all_comm_building_loss = (ushort)(all_comm_building_loss + 1);
                        break;
                    case ItemClass.SubService.CommercialTourist:
                        all_comm_building_loss = (ushort)(all_comm_building_loss + 1);
                        break;
                    default: break;
                }
            }
        }


        public void caculate_employee_outcome(Building building, ushort buildingID)
        {
            int num1 = 0;
            Citizen.BehaviourData behaviour = default(Citizen.BehaviourData);
            int aliveWorkerCount = 0;
            int totalWorkerCount = 0;
            base.GetWorkBehaviour(buildingID, ref building, ref behaviour, ref aliveWorkerCount, ref totalWorkerCount);
            switch (building.Info.m_class.m_subService)
            {
                case ItemClass.SubService.IndustrialFarming:
                    num1 = (int)(behaviour.m_educated0Count * comm_data.indus_far_education0 + behaviour.m_educated1Count * comm_data.indus_far_education1 + behaviour.m_educated2Count * comm_data.indus_far_education2 + behaviour.m_educated3Count * comm_data.indus_far_education3);
                    break;
                case ItemClass.SubService.IndustrialForestry:
                    num1 = (int)(behaviour.m_educated0Count * comm_data.indus_for_education0 + behaviour.m_educated1Count * comm_data.indus_for_education1 + behaviour.m_educated2Count * comm_data.indus_for_education2 + behaviour.m_educated3Count * comm_data.indus_for_education3);
                    break;
                case ItemClass.SubService.IndustrialOil:
                    num1 = (int)(behaviour.m_educated0Count * comm_data.indus_oil_education0 + behaviour.m_educated1Count * comm_data.indus_oil_education1 + behaviour.m_educated2Count * comm_data.indus_oil_education2 + behaviour.m_educated3Count * comm_data.indus_oil_education3);
                    break;
                case ItemClass.SubService.IndustrialOre:
                    num1 = (int)(behaviour.m_educated0Count * comm_data.indus_ore_education0 + behaviour.m_educated1Count * comm_data.indus_ore_education1 + behaviour.m_educated2Count * comm_data.indus_ore_education2 + behaviour.m_educated3Count * comm_data.indus_ore_education3);
                    break;
                case ItemClass.SubService.IndustrialGeneric:
                    if (this.m_info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        num1 = (int)(behaviour.m_educated0Count * comm_data.indus_gen_level1_education0 + behaviour.m_educated1Count * comm_data.indus_gen_level1_education1 + behaviour.m_educated2Count * comm_data.indus_gen_level1_education2 + behaviour.m_educated3Count * comm_data.indus_gen_level1_education3);
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        num1 = (int)(behaviour.m_educated0Count * comm_data.indus_gen_level2_education0 + behaviour.m_educated1Count * comm_data.indus_gen_level2_education1 + behaviour.m_educated2Count * comm_data.indus_gen_level2_education2 + behaviour.m_educated3Count * comm_data.indus_gen_level2_education3);
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        num1 = (int)(behaviour.m_educated0Count * comm_data.indus_gen_level3_education0 + behaviour.m_educated1Count * comm_data.indus_gen_level3_education1 + behaviour.m_educated2Count * comm_data.indus_gen_level3_education2 + behaviour.m_educated3Count * comm_data.indus_gen_level3_education3);
                    }
                    break;
                case ItemClass.SubService.CommercialHigh:
                    if (this.m_info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        num1 = (int)(behaviour.m_educated0Count * comm_data.comm_high_level1_education0 + behaviour.m_educated1Count * comm_data.comm_high_level1_education1 + behaviour.m_educated2Count * comm_data.comm_high_level1_education2 + behaviour.m_educated3Count * comm_data.comm_high_level1_education3);
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        num1 = (int)(behaviour.m_educated0Count * comm_data.comm_high_level2_education0 + behaviour.m_educated1Count * comm_data.comm_high_level2_education1 + behaviour.m_educated2Count * comm_data.comm_high_level2_education2 + behaviour.m_educated3Count * comm_data.comm_high_level2_education3);
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        num1 = (int)(behaviour.m_educated0Count * comm_data.comm_high_level3_education0 + behaviour.m_educated1Count * comm_data.comm_high_level3_education1 + behaviour.m_educated2Count * comm_data.comm_high_level3_education2 + behaviour.m_educated3Count * comm_data.comm_high_level3_education3);
                    }
                    break;
                case ItemClass.SubService.CommercialLow:
                    if (this.m_info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        num1 = (int)(behaviour.m_educated0Count * comm_data.comm_low_level1_education0 + behaviour.m_educated1Count * comm_data.comm_low_level1_education1 + behaviour.m_educated2Count * comm_data.comm_low_level1_education2 + behaviour.m_educated3Count * comm_data.comm_low_level1_education3);
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        num1 = (int)(behaviour.m_educated0Count * comm_data.comm_low_level2_education0 + behaviour.m_educated1Count * comm_data.comm_low_level2_education1 + behaviour.m_educated2Count * comm_data.comm_low_level2_education2 + behaviour.m_educated3Count * comm_data.comm_low_level2_education3);
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        num1 = (int)(behaviour.m_educated0Count * comm_data.comm_low_level3_education0 + behaviour.m_educated1Count * comm_data.comm_low_level3_education1 + behaviour.m_educated2Count * comm_data.comm_low_level3_education2 + behaviour.m_educated3Count * comm_data.comm_low_level3_education3);
                    }
                    break;
                case ItemClass.SubService.CommercialLeisure:
                    num1 = (int)(behaviour.m_educated0Count * comm_data.comm_lei_education0 + behaviour.m_educated1Count * comm_data.comm_lei_education1 + behaviour.m_educated2Count * comm_data.comm_lei_education2 + behaviour.m_educated3Count * comm_data.comm_lei_education3);
                    break;
                case ItemClass.SubService.CommercialTourist:
                    num1 = (int)(behaviour.m_educated0Count * comm_data.comm_tou_education0 + behaviour.m_educated1Count * comm_data.comm_tou_education1 + behaviour.m_educated2Count * comm_data.comm_tou_education2 + behaviour.m_educated3Count * comm_data.comm_tou_education3);
                    break;
                case ItemClass.SubService.CommercialEco:
                    num1 = (int)(behaviour.m_educated0Count * comm_data.comm_eco_education0 + behaviour.m_educated1Count * comm_data.comm_eco_education1 + behaviour.m_educated2Count * comm_data.comm_eco_education2 + behaviour.m_educated3Count * comm_data.comm_eco_education3);
                    break;
                default: break;
            }
            System.Random rand = new System.Random();

            //add some benefit if building is profit
            if ((num1 != 0) && (comm_data.building_money[buildingID] >= 0))
            {
                num1 += (behaviour.m_educated0Count * rand.Next(1) + behaviour.m_educated0Count * rand.Next(2) + behaviour.m_educated0Count * rand.Next(3) + behaviour.m_educated0Count * rand.Next(4));
            }
            comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - num1;
        }

        public void process_land_fee(Building building, ushort buildingID)
        {
            DistrictManager instance = Singleton<DistrictManager>.instance;
            byte district = instance.GetDistrict(building.m_position);
            DistrictPolicies.Services servicePolicies = instance.m_districts.m_buffer[(int)district].m_servicePolicies;
            DistrictPolicies.Taxation taxationPolicies = instance.m_districts.m_buffer[(int)district].m_taxationPolicies;
            DistrictPolicies.CityPlanning cityPlanningPolicies = instance.m_districts.m_buffer[(int)district].m_cityPlanningPolicies;

            int num = 0;
            GetLandRent(out num);
            if (((taxationPolicies & DistrictPolicies.Taxation.DontTaxLeisure) != DistrictPolicies.Taxation.None) && (building.Info.m_class.m_subService == ItemClass.SubService.CommercialLeisure))
            {
                num = 0;
            }
                comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - num;

            if (instance.IsPolicyLoaded(DistrictPolicies.Policies.ExtraInsulation))
            {
                if ((servicePolicies & DistrictPolicies.Services.ExtraInsulation) != DistrictPolicies.Services.None)
                {
                    num = num * 95 / 100;
                }
            }
            if ((servicePolicies & DistrictPolicies.Services.Recycling) != DistrictPolicies.Services.None)
            {
                num = num * 95 / 100;
            }
            num = (int)(num * ((float)(instance.m_districts.m_buffer[(int)district].GetLandValue() + 50) / 100));
            int num2;
            num2 = Singleton<EconomyManager>.instance.GetTaxRate(this.m_info.m_class, taxationPolicies);
            Singleton<EconomyManager>.instance.AddPrivateIncome(num, building.Info.m_class.m_service, building.Info.m_class.m_subService, this.m_info.m_class.m_level, num2 * 100);
        }

        public void GetLandRent(out int incomeAccumulation)
        {
            ItemClass @class = this.m_info.m_class;
            incomeAccumulation = 0;
            ItemClass.SubService subService = @class.m_subService;
            switch (subService)
            {
                case ItemClass.SubService.OfficeHightech:
                    incomeAccumulation = comm_data.office_high_tech;
                    break;
                case ItemClass.SubService.OfficeGeneric:
                    if (this.m_info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = comm_data.office_gen_levell;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = comm_data.office_gen_level2;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = comm_data.office_gen_level3;
                    }
                    break;
                case ItemClass.SubService.IndustrialFarming:
                    incomeAccumulation = comm_data.indu_farm;
                    break;
                case ItemClass.SubService.IndustrialForestry:
                    incomeAccumulation =comm_data.indu_forest;
                    break;
                case ItemClass.SubService.IndustrialOil:
                    incomeAccumulation = comm_data.indu_oil;
                    break;
                case ItemClass.SubService.IndustrialOre:
                    incomeAccumulation = comm_data.indu_ore;
                    break;
                case ItemClass.SubService.IndustrialGeneric:
                    if (this.m_info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = comm_data.indu_gen_level1;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = comm_data.indu_gen_level2;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = comm_data.indu_gen_level3;
                    }
                    break;
                case ItemClass.SubService.CommercialHigh:
                    if (this.m_info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = comm_data.comm_high_level1;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = comm_data.comm_high_level2;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = comm_data.comm_high_level3;
                    }
                    break;
                case ItemClass.SubService.CommercialLow:
                    if (this.m_info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = comm_data.comm_low_level1;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = comm_data.comm_low_level2;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = comm_data.comm_low_level3;
                    }
                    break;
                case ItemClass.SubService.CommercialLeisure:
                    incomeAccumulation = comm_data.comm_leisure;
                    break;
                case ItemClass.SubService.CommercialTourist:
                    incomeAccumulation = comm_data.comm_tourist;
                    break;
                case ItemClass.SubService.CommercialEco:
                    incomeAccumulation = comm_data.comm_eco;
                    break;
                case ItemClass.SubService.ResidentialHigh:
                    if (this.m_info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = comm_data.resident_high_level1;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = comm_data.resident_high_level2;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = comm_data.resident_high_level3;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level4)
                    {
                        incomeAccumulation = comm_data.resident_high_level4;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level5)
                    {
                        incomeAccumulation = comm_data.resident_high_level5;
                    }
                    break;
                case ItemClass.SubService.ResidentialHighEco:
                    if (this.m_info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = comm_data.resident_high_eco_level1;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = comm_data.resident_high_eco_level2;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = comm_data.resident_high_eco_level3;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level4)
                    {
                        incomeAccumulation = comm_data.resident_high_eco_level4;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level5)
                    {
                        incomeAccumulation = comm_data.resident_high_eco_level5;
                    }
                    break;
                case ItemClass.SubService.ResidentialLow:
                    if (this.m_info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = comm_data.resident_low_level1;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = comm_data.resident_low_level2;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = comm_data.resident_low_level3;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level5)
                    {
                        incomeAccumulation = comm_data.resident_low_level4;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level5)
                    {
                        incomeAccumulation = comm_data.resident_low_level5;
                    }
                    break;
                case ItemClass.SubService.ResidentialLowEco:
                    if (this.m_info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = comm_data.resident_low_eco_level1;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = comm_data.resident_low_eco_level2;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = comm_data.resident_low_eco_level3;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level5)
                    {
                        incomeAccumulation = comm_data.resident_low_eco_level4;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level5)
                    {
                        incomeAccumulation = comm_data.resident_low_eco_level5;
                    }
                    break;
                default: break;
            }
        }
    }
}
