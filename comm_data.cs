using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCity
{
    public class comm_data
    {
        //1.  citizen
        //1.1 citizen salary
        public const byte comm_high_level1_education0 = 14;
        public const byte comm_high_level2_education0 = 15;
        public const byte comm_high_level3_education0 = 16;

        public const byte comm_high_level1_education1 = 16;
        public const byte comm_high_level2_education1 = 17;
        public const byte comm_high_level3_education1 = 18;

        public const byte comm_high_level1_education2 = 22;
        public const byte comm_high_level2_education2 = 23;
        public const byte comm_high_level3_education2 = 24;

        public const byte comm_high_level1_education3 = 30;
        public const byte comm_high_level2_education3 = 31;
        public const byte comm_high_level3_education3 = 33;

        //commerial low
        public const byte comm_low_level1_education0 = 13;
        public const byte comm_low_level2_education0 = 14;
        public const byte comm_low_level3_education0 = 15;

        public const byte comm_low_level1_education1 = 15;
        public const byte comm_low_level2_education1 = 16;
        public const byte comm_low_level3_education1 = 17;

        public const byte comm_low_level1_education2 = 20;
        public const byte comm_low_level2_education2 = 21;
        public const byte comm_low_level3_education2 = 22;

        public const byte comm_low_level1_education3 = 27;
        public const byte comm_low_level2_education3 = 29;
        public const byte comm_low_level3_education3 = 30;

        //commerial leisure
        public const byte comm_lei_education0 = 16;
        public const byte comm_lei_education1 = 18;
        public const byte comm_lei_education2 = 21;
        public const byte comm_lei_education3 = 29;

        //commerial Tourist
        public const byte comm_tou_education0 = 15;
        public const byte comm_tou_education1 = 17;
        public const byte comm_tou_education2 = 22;
        public const byte comm_tou_education3 = 33;

        //indus generic
        public const byte indus_gen_level1_education0 = 15;
        public const byte indus_gen_level2_education0 = 16;
        public const byte indus_gen_level3_education0 = 17;

        public const byte indus_gen_level1_education1 = 17;
        public const byte indus_gen_level2_education1 = 18;
        public const byte indus_gen_level3_education1 = 19;

        public const byte indus_gen_level1_education2 = 21;
        public const byte indus_gen_level2_education2 = 22;
        public const byte indus_gen_level3_education2 = 23;

        public const byte indus_gen_level1_education3 = 30;
        public const byte indus_gen_level2_education3 = 31;
        public const byte indus_gen_level3_education3 = 32;

        //industrial farmer
        public const byte indus_far_education0 = 10;
        public const byte indus_far_education1 = 11;
        public const byte indus_far_education2 = 13;
        public const byte indus_far_education3 = 16;

        //industrial foerest
        public const byte indus_for_education0 = 11;
        public const byte indus_for_education1 = 12;
        public const byte indus_for_education2 = 14;
        public const byte indus_for_education3 = 18;

        //industrial oil
        public const byte indus_oil_education0 = 22;
        public const byte indus_oil_education1 = 23;
        public const byte indus_oil_education2 = 25;
        public const byte indus_oil_education3 = 28;

        //industrial ore
        public const byte indus_ore_education0 = 20;
        public const byte indus_ore_education1 = 21;
        public const byte indus_ore_education2 = 25;
        public const byte indus_ore_education3 = 26;

        //office
        public const byte office_education0 = 15;
        public const byte office_education1 = 22;
        public const byte office_education2 = 30;
        public const byte office_education3 = 40;

        //Road
        public const byte road_education0 = 14;
        public const byte road_education1 = 16;
        public const byte road_education2 = 19;
        public const byte road_education3 = 25;

        //Electricity
        public const byte Electricity_education0 = 13;
        public const byte Electricity_education1 = 15;
        public const byte Electricity_education2 = 20;
        public const byte Electricity_education3 = 27;

        //Water
        public const byte Water_education0 = 12;
        public const byte Water_education1 = 14;
        public const byte Water_education2 = 18;
        public const byte Water_education3 = 24;

        //Beautification
        public const byte Beautification_education0 = 13;
        public const byte Beautification_education1 = 15;
        public const byte Beautification_education2 = 19;
        public const byte Beautification_education3 = 24;

        //Garbage
        public const byte Garbage_education0 = 15;
        public const byte Garbage_education1 = 16;
        public const byte Garbage_education2 = 18;
        public const byte Garbage_education3 = 21;

        //HealthCare
        public const byte HealthCare_education0 = 14;
        public const byte HealthCare_education1 = 16;
        public const byte HealthCare_education2 = 21;
        public const byte HealthCare_education3 = 33;

        //PoliceDepartment
        public const byte PoliceDepartment_education0 = 15;
        public const byte PoliceDepartment_education1 = 17;
        public const byte PoliceDepartment_education2 = 20;
        public const byte PoliceDepartment_education3 = 26;

        //Education
        public const byte Education_education0 = 12;
        public const byte Education_education1 = 15;
        public const byte Education_education2 = 20;
        public const byte Education_education3 = 30;

        //Monument
        public const byte Monument_education0 = 15;
        public const byte Monument_education1 = 17;
        public const byte Monument_education2 = 21;
        public const byte Monument_education3 = 28;

        //FireDepartment
        public const byte FireDepartment_education0 = 16;
        public const byte FireDepartment_education1 = 18;
        public const byte FireDepartment_education2 = 21;
        public const byte FireDepartment_education3 = 24;

        //PublicTransport bus
        public const byte PublicTransport_bus_education0 = 17;
        public const byte PublicTransport_bus_education1 = 18;
        public const byte PublicTransport_bus_education2 = 20;
        public const byte PublicTransport_bus_education3 = 22;

        //PublicTransport tram
        public const byte PublicTransport_tram_education0 = 12;
        public const byte PublicTransport_tram_education1 = 15;
        public const byte PublicTransport_tram_education2 = 20;
        public const byte PublicTransport_tram_education3 = 23;

        //PublicTransport train
        public const byte PublicTransport_train_education0 = 15;
        public const byte PublicTransport_train_education1 = 17;
        public const byte PublicTransport_train_education2 = 20;
        public const byte PublicTransport_train_education3 = 24;

        //PublicTransport taxi
        public const byte PublicTransport_taxi_education0 = 18;
        public const byte PublicTransport_taxi_education1 = 20;
        public const byte PublicTransport_taxi_education2 = 22;
        public const byte PublicTransport_taxi_education3 = 25;

        //PublicTransport ship
        public const byte PublicTransport_ship_education0 = 12;
        public const byte PublicTransport_ship_education1 = 13;
        public const byte PublicTransport_ship_education2 = 15;
        public const byte PublicTransport_ship_education3 = 18;

        //PublicTransport plane
        public const byte PublicTransport_plane_education0 = 15;
        public const byte PublicTransport_plane_education1 = 19;
        public const byte PublicTransport_plane_education2 = 25;
        public const byte PublicTransport_plane_education3 = 35;

        //PublicTransport metro
        public const byte PublicTransport_metro_education0 = 13;
        public const byte PublicTransport_metro_education1 = 16;
        public const byte PublicTransport_metro_education2 = 20;
        public const byte PublicTransport_metro_education3 = 25;

        public static int citizen_count = 0;
        public static int family_count = 0;
        public static int citizen_salary_per_family = 0;
        public static long citizen_salary_total = 0;
        public static long citizen_salary_tax_total = 0;

        //1.2 citizen outcome
        public const byte resident_low_level1_rent = 10;
        public const byte resident_low_level2_rent = 14;
        public const byte resident_low_level3_rent = 19;
        public const byte resident_low_level4_rent = 25;
        public const byte resident_low_level5_rent = 35;
        public const byte resident_high_level1_rent = 8;
        public const byte resident_high_level2_rent = 11;
        public const byte resident_high_level3_rent = 15;
        public const byte resident_high_level4_rent = 20;
        public const byte resident_high_level5_rent = 26;
        //1.2.1 citizen outcome
        public static long citizen_outcome_per_family = 0;
        public static long citizen_outcome = 0;
        //1.2.2 transport fee  position.x unit(0.9m),in game, max distance (in x) is 18000m
        public static ushort []vehical_transfer_time = new ushort[16384];
        public static bool[] vehical_last_transfer_flag = new bool[16384];
        public static uint temp_total_citizen_vehical_time = 0;//temp use
        public static uint temp_total_citizen_vehical_time_last = 0;//temp use
        public static uint total_citizen_vehical_time = 0;
        public static long public_transport_fee = 0;
        public static long all_transport_fee = 0;
        public static byte citizen_average_transport_fee = 0;

        //1.3 income-outcome
        //public static byte citizen_shopping_idex = 0;
        public static int family_profit_money_num = 0;
        public static int family_loss_money_num = 0;
        
        //2 building
        //economic_active_count
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


        //2.2 building outcome
        public const byte resident_low_level1 = 10;
        public const byte resident_low_level2 = 14;
        public const byte resident_low_level3 = 19;
        public const byte resident_low_level4 = 25;
        public const byte resident_low_level5 = 35;

        public const byte resident_high_level1 = 50;
        public const byte resident_high_level2 = 70;
        public const byte resident_high_level3 = 110;
        public const byte resident_high_level4 = 140;
        public const byte resident_high_level5 = 180;

        public const byte comm_high_level1 = 110;
        public const byte comm_high_level2 = 170;
        public const byte comm_high_level3 = 220;

        public const byte comm_low_level1 = 80;
        public const byte comm_low_level2 = 120;
        public const byte comm_low_level3 = 160;

        public const byte comm_tourist = 160;
        public const byte comm_leisure = 230;

        public const byte indu_gen_level1 = 45;
        public const byte indu_gen_level2 = 60;
        public const byte indu_gen_level3 = 80;

        public const byte indu_forest = 30;
        public const byte indu_farm = 40;
        public const byte indu_oil = 80;
        public const byte indu_ore = 90;

        public const byte office_low_levell = 180;
        public const byte office_low_level2 = 210;
        public const byte office_low_level3 = 250;


        //2.3 process building 
        public static int[] building_money = new int[49152];
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




        //3 govement outcome
        public static int Road = 0;
        public static int Electricity = 0;
        public static int Water = 0;
        public static int Beautification = 0;
        public static int Garbage = 0;
        public static int HealthCare = 0;
        public static int PoliceDepartment = 0;
        public static int Education = 0;
        public static int Monument = 0;
        public static int FireDepartment = 0;
        public static int PublicTransport_bus = 0;
        public static int PublicTransport_tram = 0;
        public static int PublicTransport_ship = 0;
        public static int PublicTransport_train = 0;
        public static int PublicTransport_plane = 0;
        public static int PublicTransport_metro = 0;
        public static int PublicTransport_taxi = 0;


        //other in-game variable

    }
}
