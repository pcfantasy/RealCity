using ColossalFramework;
using ColossalFramework.Math;
using System;
using UnityEngine;

namespace RealCity
{

    public class pc_PrivateBuildingAI : CommonBuildingAI
    {
        //2.1 building income
        //2.1.1 profit and tranport cost
        public static ushort resident_shopping_count = 0;
        public static ushort resident_leisure_count = 0;
        public static ushort shop_get_goods_from_local_level1_count = 0;
        public static ushort shop_get_goods_from_local_level2_count = 0;
        public static ushort shop_get_goods_from_local_level3_count = 0;
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
        //public static ushort visit_shopping_count = 0;
        //public static ushort visit_leisure_count = 0;

        public const float good_export_price = 1.5f;
        public const float food_export_price = 0.5f;
        public const float petrol_export_price = 0.5f;
        public const float coal_export_price = 0.5f;
        public const float lumber_export_price = 0.5f;
        public const float oil_export_price = 0.1f;
        public const float ore_export_price = 0.1f;
        public const float grain_export_price = 0.1f;
        public const float log_export_price = 0.1f;

        public const float good_import_price = 2.6f;
        public const float food_import_price = 0.9f;
        public const float petrol_import_price = 0.9f;
        public const float coal_import_price = 0.9f;
        public const float lumber_import_price = 0.9f;
        public const float oil_import_price = 0.3f;
        public const float ore_import_price = 0.3f;
        public const float grain_import_price = 0.3f;
        public const float log_import_price = 0.3f;

        public static float good_export_ratio = 1f;
        public static float food_export_ratio = 1f;
        public static float petrol_export_ratio = 1f;
        public static float coal_export_ratio = 1f;
        public static float lumber_export_ratio = 01f;
        public static float oil_export_ratio = 1f;
        public static float ore_export_ratio = 1f;
        public static float grain_export_ratio = 1f;
        public static float log_export_ratio = 1f;

        public static float good_import_ratio = 1f;
        public static float food_import_ratio = 1f;
        public static float petrol_import_ratio = 1f;
        public static float coal_import_ratio = 1f;
        public static float lumber_import_ratio = 1f;
        public static float oil_import_ratio = 1f;
        public static float ore_import_ratio = 1f;
        public static float grain_import_ratio = 1f;
        public static float log_import_ratio = 1f;

        public static float good_level2_ratio = 0f;
        public static float good_level3_ratio = 0f;


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
        public static ushort resident_shopping_count_final = 0;
        public static ushort resident_leisure_count_final = 0;
        public static ushort shop_get_goods_from_local_count_level1_final = 0;
        public static ushort shop_get_goods_from_local_count_level2_final = 0;
        public static ushort shop_get_goods_from_local_count_level3_final = 0;
        public static ushort shop_get_goods_from_outside_count_final = 0;
        public static ushort industy_goods_to_outside_count_final = 0;
        public static ushort Grain_to_outside_count_final = 0;
        public static ushort Grain_to_industy_count_final = 0;
        public static ushort Grain_from_outside_count_final = 0;
        public static ushort food_to_outside_count_final = 0;
        public static ushort food_to_industy_count_final = 0;
        public static ushort food_from_outside_count_final = 0;
        public static ushort oil_to_outside_count_final = 0;
        public static ushort oil_to_industy_count_final = 0;
        public static ushort oil_from_outside_count_final = 0;
        public static ushort Petrol_to_outside_count_final = 0;
        public static ushort Petrol_to_industy_count_final = 0;
        public static ushort Petrol_from_outside_count_final = 0;
        public static ushort ore_to_outside_count_final = 0;
        public static ushort ore_to_industy_count_final = 0;
        public static ushort ore_from_outside_count_final = 0;
        public static ushort coal_to_outside_count_final = 0;
        public static ushort coal_to_industy_count_final = 0;
        public static ushort coal_from_outside_count_final = 0;
        public static ushort logs_to_outside_count_final = 0;
        public static ushort logs_to_industy_count_final = 0;
        public static ushort logs_from_outside_count_final = 0;
        public static ushort lumber_to_outside_count_final = 0;
        public static ushort lumber_to_industy_count_final = 0;
        public static ushort lumber_from_outside_count_final = 0;
        public static ushort all_comm_building_profit_final = 0;
        public static ushort all_industry_building_profit_final = 0;
        public static ushort all_foresty_building_profit_final = 0;
        public static ushort all_farmer_building_profit_final = 0;
        public static ushort all_oil_building_profit_final = 0;
        public static ushort all_ore_building_profit_final = 0;
        public static ushort all_comm_building_loss_final = 0;
        public static ushort all_industry_building_loss_final = 0;
        public static ushort all_foresty_building_loss_final = 0;
        public static ushort all_farmer_building_loss_final = 0;
        public static ushort all_oil_building_loss_final = 0;
        public static ushort all_ore_building_loss_final = 0;
        public static ushort all_buildings_final = 0;

        public static ushort all_office_level1_building_num = 0;
        public static ushort all_office_level2_building_num = 0;
        public static ushort all_office_level3_building_num = 0;
        public static ushort all_office_high_tech_building_num = 0;

        public static ushort all_office_level1_building_num_final = 0;
        public static ushort all_office_level2_building_num_final = 0;
        public static ushort all_office_level3_building_num_final = 0;
        public static ushort all_office_high_tech_building_num_final = 0;
        public static float office_gen_salary_index = 0.5f;
        public static float office_high_tech_salary_index = 0.5f;
        // PrivateBuildingAI

        public static byte[] save_data = new byte[304];
        public static byte[] load_data = new byte[304];

        public static void load()
        {
            int i = 0;
            resident_shopping_count = saveandrestore.load_ushort(ref i, load_data);
            resident_leisure_count = saveandrestore.load_ushort(ref i, load_data);
            shop_get_goods_from_local_level1_count = saveandrestore.load_ushort(ref i, load_data);
            shop_get_goods_from_local_level2_count = saveandrestore.load_ushort(ref i, load_data);
            shop_get_goods_from_local_level3_count = saveandrestore.load_ushort(ref i, load_data);
            shop_get_goods_from_outside_count = saveandrestore.load_ushort(ref i, load_data);
            industy_goods_to_outside_count = saveandrestore.load_ushort(ref i, load_data);
            Grain_to_outside_count = saveandrestore.load_ushort(ref i, load_data);
            Grain_to_industy_count = saveandrestore.load_ushort(ref i, load_data);
            Grain_from_outside_count = saveandrestore.load_ushort(ref i, load_data);
            food_to_outside_count = saveandrestore.load_ushort(ref i, load_data);
            food_to_industy_count = saveandrestore.load_ushort(ref i, load_data);
            food_from_outside_count = saveandrestore.load_ushort(ref i, load_data);
            oil_to_outside_count = saveandrestore.load_ushort(ref i, load_data);
            oil_to_industy_count = saveandrestore.load_ushort(ref i, load_data);
            oil_from_outside_count = saveandrestore.load_ushort(ref i, load_data);
            Petrol_to_outside_count = saveandrestore.load_ushort(ref i, load_data);
            Petrol_to_industy_count = saveandrestore.load_ushort(ref i, load_data);
            Petrol_from_outside_count = saveandrestore.load_ushort(ref i, load_data);
            ore_to_outside_count = saveandrestore.load_ushort(ref i, load_data);
            ore_to_industy_count = saveandrestore.load_ushort(ref i, load_data);
            ore_from_outside_count = saveandrestore.load_ushort(ref i, load_data);
            coal_to_outside_count = saveandrestore.load_ushort(ref i, load_data);
            coal_to_industy_count = saveandrestore.load_ushort(ref i, load_data);
            coal_from_outside_count = saveandrestore.load_ushort(ref i, load_data);
            logs_to_outside_count = saveandrestore.load_ushort(ref i, load_data);
            logs_to_industy_count = saveandrestore.load_ushort(ref i, load_data);
            logs_from_outside_count = saveandrestore.load_ushort(ref i, load_data);
            lumber_to_outside_count = saveandrestore.load_ushort(ref i, load_data);
            lumber_to_industy_count = saveandrestore.load_ushort(ref i, load_data);
            lumber_from_outside_count = saveandrestore.load_ushort(ref i, load_data);

            good_export_ratio = saveandrestore.load_float(ref i, load_data);
            food_export_ratio = saveandrestore.load_float(ref i, load_data);
            lumber_export_ratio = saveandrestore.load_float(ref i, load_data);
            coal_export_ratio = saveandrestore.load_float(ref i, load_data);
            petrol_export_ratio = saveandrestore.load_float(ref i, load_data);
            log_export_ratio = saveandrestore.load_float(ref i, load_data);
            grain_export_ratio = saveandrestore.load_float(ref i, load_data);
            oil_export_ratio = saveandrestore.load_float(ref i, load_data);
            ore_export_ratio = saveandrestore.load_float(ref i, load_data);

            good_import_ratio = saveandrestore.load_float(ref i, load_data);
            food_import_ratio = saveandrestore.load_float(ref i, load_data);
            lumber_import_ratio = saveandrestore.load_float(ref i, load_data);
            coal_import_ratio = saveandrestore.load_float(ref i, load_data);
            petrol_import_ratio = saveandrestore.load_float(ref i, load_data);
            log_import_ratio = saveandrestore.load_float(ref i, load_data);
            grain_import_ratio = saveandrestore.load_float(ref i, load_data);
            oil_import_ratio = saveandrestore.load_float(ref i, load_data);
            ore_import_ratio = saveandrestore.load_float(ref i, load_data);

            good_level2_ratio = saveandrestore.load_float(ref i, load_data);
            good_level3_ratio = saveandrestore.load_float(ref i, load_data);

            all_comm_building_profit = saveandrestore.load_ushort(ref i, load_data);
            all_industry_building_profit = saveandrestore.load_ushort(ref i, load_data);
            all_foresty_building_profit = saveandrestore.load_ushort(ref i, load_data);
            all_farmer_building_profit = saveandrestore.load_ushort(ref i, load_data);
            all_oil_building_profit = saveandrestore.load_ushort(ref i, load_data);
            all_ore_building_profit = saveandrestore.load_ushort(ref i, load_data);
            all_comm_building_loss = saveandrestore.load_ushort(ref i, load_data);
            all_industry_building_loss = saveandrestore.load_ushort(ref i, load_data);
            all_foresty_building_loss = saveandrestore.load_ushort(ref i, load_data);
            all_farmer_building_loss = saveandrestore.load_ushort(ref i, load_data);
            all_oil_building_loss = saveandrestore.load_ushort(ref i, load_data);
            all_ore_building_loss = saveandrestore.load_ushort(ref i, load_data);
            total_cargo_vehical_time = saveandrestore.load_uint(ref i, load_data);
            temp_total_cargo_vehical_time = saveandrestore.load_uint(ref i, load_data);
            temp_total_cargo_vehical_time_last = saveandrestore.load_uint(ref i, load_data);
            total_cargo_transfer_size = saveandrestore.load_uint(ref i, load_data);
            total_train_transfer_size = saveandrestore.load_uint(ref i, load_data);
            total_ship_transfer_size = saveandrestore.load_uint(ref i, load_data);
            prebuidlingid = saveandrestore.load_uint(ref i, load_data);

            resident_shopping_count_final = saveandrestore.load_ushort(ref i, load_data);
            resident_leisure_count_final = saveandrestore.load_ushort(ref i, load_data);
            shop_get_goods_from_local_count_level1_final = saveandrestore.load_ushort(ref i, load_data);
            shop_get_goods_from_local_count_level2_final = saveandrestore.load_ushort(ref i, load_data);
            shop_get_goods_from_local_count_level3_final = saveandrestore.load_ushort(ref i, load_data);
            shop_get_goods_from_outside_count_final = saveandrestore.load_ushort(ref i, load_data);
            industy_goods_to_outside_count_final = saveandrestore.load_ushort(ref i, load_data);
            Grain_to_outside_count_final = saveandrestore.load_ushort(ref i, load_data);
            Grain_to_industy_count_final = saveandrestore.load_ushort(ref i, load_data);
            Grain_from_outside_count_final = saveandrestore.load_ushort(ref i, load_data);
            food_to_outside_count_final = saveandrestore.load_ushort(ref i, load_data);
            food_to_industy_count_final = saveandrestore.load_ushort(ref i, load_data);
            food_from_outside_count_final = saveandrestore.load_ushort(ref i, load_data);
            oil_to_outside_count_final = saveandrestore.load_ushort(ref i, load_data);
            oil_to_industy_count_final = saveandrestore.load_ushort(ref i, load_data);
            oil_from_outside_count_final = saveandrestore.load_ushort(ref i, load_data);
            Petrol_to_outside_count_final = saveandrestore.load_ushort(ref i, load_data);
            Petrol_to_industy_count_final = saveandrestore.load_ushort(ref i, load_data);
            Petrol_from_outside_count_final = saveandrestore.load_ushort(ref i, load_data);
            ore_to_outside_count_final = saveandrestore.load_ushort(ref i, load_data);
            ore_to_industy_count_final = saveandrestore.load_ushort(ref i, load_data);
            ore_from_outside_count_final = saveandrestore.load_ushort(ref i, load_data);
            coal_to_outside_count_final = saveandrestore.load_ushort(ref i, load_data);
            coal_to_industy_count_final = saveandrestore.load_ushort(ref i, load_data);
            coal_from_outside_count_final = saveandrestore.load_ushort(ref i, load_data);
            logs_to_outside_count_final = saveandrestore.load_ushort(ref i, load_data);
            logs_to_industy_count_final = saveandrestore.load_ushort(ref i, load_data);
            logs_from_outside_count_final = saveandrestore.load_ushort(ref i, load_data);
            lumber_to_outside_count_final = saveandrestore.load_ushort(ref i, load_data);
            lumber_to_industy_count_final = saveandrestore.load_ushort(ref i, load_data);
            lumber_from_outside_count_final = saveandrestore.load_ushort(ref i, load_data);

            all_comm_building_profit_final = saveandrestore.load_ushort(ref i, load_data);
            all_industry_building_profit_final = saveandrestore.load_ushort(ref i, load_data);
            all_foresty_building_profit_final = saveandrestore.load_ushort(ref i, load_data);
            all_farmer_building_profit_final = saveandrestore.load_ushort(ref i, load_data);
            all_oil_building_profit_final = saveandrestore.load_ushort(ref i, load_data);
            all_ore_building_profit_final = saveandrestore.load_ushort(ref i, load_data);
            all_comm_building_loss_final = saveandrestore.load_ushort(ref i, load_data);
            all_industry_building_loss_final = saveandrestore.load_ushort(ref i, load_data);
            all_foresty_building_loss_final = saveandrestore.load_ushort(ref i, load_data);
            all_farmer_building_loss_final = saveandrestore.load_ushort(ref i, load_data);
            all_oil_building_loss_final = saveandrestore.load_ushort(ref i, load_data);
            all_ore_building_loss_final = saveandrestore.load_ushort(ref i, load_data);

            all_office_level1_building_num = saveandrestore.load_ushort(ref i, load_data);
            all_office_level2_building_num = saveandrestore.load_ushort(ref i, load_data);
            all_office_level3_building_num = saveandrestore.load_ushort(ref i, load_data);
            all_office_high_tech_building_num = saveandrestore.load_ushort(ref i, load_data);

            all_office_level1_building_num_final = saveandrestore.load_ushort(ref i, load_data);
            all_office_level2_building_num_final = saveandrestore.load_ushort(ref i, load_data);
            all_office_level3_building_num_final = saveandrestore.load_ushort(ref i, load_data);
            all_office_high_tech_building_num_final = saveandrestore.load_ushort(ref i, load_data);

            office_gen_salary_index = saveandrestore.load_float(ref i, load_data);
            office_high_tech_salary_index = saveandrestore.load_float(ref i, load_data);


        }


        public static void save()
        {
            int i = 0;

            //31 * 2 = 62
            saveandrestore.save_ushort(ref i, resident_shopping_count, ref save_data);
            saveandrestore.save_ushort(ref i, resident_leisure_count, ref save_data);
            saveandrestore.save_ushort(ref i, shop_get_goods_from_local_level1_count, ref save_data);
            saveandrestore.save_ushort(ref i, shop_get_goods_from_local_level2_count, ref save_data);
            saveandrestore.save_ushort(ref i, shop_get_goods_from_local_level3_count, ref save_data);
            saveandrestore.save_ushort(ref i, shop_get_goods_from_outside_count, ref save_data);
            saveandrestore.save_ushort(ref i, industy_goods_to_outside_count, ref save_data);
            saveandrestore.save_ushort(ref i, Grain_to_outside_count, ref save_data);
            saveandrestore.save_ushort(ref i, Grain_to_industy_count, ref save_data);
            saveandrestore.save_ushort(ref i, Grain_from_outside_count, ref save_data);
            saveandrestore.save_ushort(ref i, food_to_outside_count, ref save_data);
            saveandrestore.save_ushort(ref i, food_to_industy_count, ref save_data);
            saveandrestore.save_ushort(ref i, food_from_outside_count, ref save_data);
            saveandrestore.save_ushort(ref i, oil_to_outside_count, ref save_data);
            saveandrestore.save_ushort(ref i, oil_to_industy_count, ref save_data);
            saveandrestore.save_ushort(ref i, oil_from_outside_count, ref save_data);
            saveandrestore.save_ushort(ref i, Petrol_to_outside_count, ref save_data);
            saveandrestore.save_ushort(ref i, Petrol_to_industy_count, ref save_data);
            saveandrestore.save_ushort(ref i, Petrol_from_outside_count, ref save_data);
            saveandrestore.save_ushort(ref i, ore_to_outside_count, ref save_data);
            saveandrestore.save_ushort(ref i, ore_to_industy_count, ref save_data);
            saveandrestore.save_ushort(ref i, ore_from_outside_count, ref save_data);
            saveandrestore.save_ushort(ref i, coal_to_outside_count, ref save_data);
            saveandrestore.save_ushort(ref i, coal_to_industy_count, ref save_data);
            saveandrestore.save_ushort(ref i, coal_from_outside_count, ref save_data);
            saveandrestore.save_ushort(ref i, logs_to_outside_count, ref save_data);
            saveandrestore.save_ushort(ref i, logs_to_industy_count, ref save_data);
            saveandrestore.save_ushort(ref i, logs_from_outside_count, ref save_data);
            saveandrestore.save_ushort(ref i, lumber_to_outside_count, ref save_data);
            saveandrestore.save_ushort(ref i, lumber_to_industy_count, ref save_data);
            saveandrestore.save_ushort(ref i, lumber_from_outside_count, ref save_data);

            //20 * 4 = 80
            saveandrestore.save_float(ref i, good_export_ratio, ref save_data);
            saveandrestore.save_float(ref i, food_export_ratio, ref save_data);
            saveandrestore.save_float(ref i, lumber_export_ratio, ref save_data);
            saveandrestore.save_float(ref i, coal_export_ratio, ref save_data);
            saveandrestore.save_float(ref i, petrol_export_ratio, ref save_data);
            saveandrestore.save_float(ref i, log_export_ratio, ref save_data);
            saveandrestore.save_float(ref i, grain_export_ratio, ref save_data);
            saveandrestore.save_float(ref i, oil_export_ratio, ref save_data);
            saveandrestore.save_float(ref i, ore_export_ratio, ref save_data);

            saveandrestore.save_float(ref i, good_import_ratio, ref save_data);
            saveandrestore.save_float(ref i, food_import_ratio, ref save_data);
            saveandrestore.save_float(ref i, lumber_import_ratio, ref save_data);
            saveandrestore.save_float(ref i, coal_import_ratio, ref save_data);
            saveandrestore.save_float(ref i, petrol_import_ratio, ref save_data);
            saveandrestore.save_float(ref i, log_import_ratio, ref save_data);
            saveandrestore.save_float(ref i, grain_import_ratio, ref save_data);
            saveandrestore.save_float(ref i, oil_import_ratio, ref save_data);
            saveandrestore.save_float(ref i, ore_import_ratio, ref save_data);
            saveandrestore.save_float(ref i, good_level2_ratio, ref save_data);
            saveandrestore.save_float(ref i, good_level3_ratio, ref save_data);

            //12*2 + 7*4 = 52
            saveandrestore.save_ushort(ref i, all_comm_building_profit, ref save_data);
            saveandrestore.save_ushort(ref i, all_industry_building_profit, ref save_data);
            saveandrestore.save_ushort(ref i, all_foresty_building_profit, ref save_data);
            saveandrestore.save_ushort(ref i, all_farmer_building_profit, ref save_data);
            saveandrestore.save_ushort(ref i, all_oil_building_profit, ref save_data);
            saveandrestore.save_ushort(ref i, all_ore_building_profit, ref save_data);
            saveandrestore.save_ushort(ref i, all_comm_building_loss, ref save_data);
            saveandrestore.save_ushort(ref i, all_industry_building_loss, ref save_data);
            saveandrestore.save_ushort(ref i, all_foresty_building_loss, ref save_data);
            saveandrestore.save_ushort(ref i, all_farmer_building_loss, ref save_data);
            saveandrestore.save_ushort(ref i, all_oil_building_loss, ref save_data);
            saveandrestore.save_ushort(ref i, all_ore_building_loss, ref save_data);
            saveandrestore.save_uint(ref i, total_cargo_vehical_time, ref save_data);
            saveandrestore.save_uint(ref i, temp_total_cargo_vehical_time, ref save_data);
            saveandrestore.save_uint(ref i, temp_total_cargo_vehical_time_last, ref save_data);
            saveandrestore.save_uint(ref i, total_cargo_transfer_size, ref save_data);
            saveandrestore.save_uint(ref i, total_train_transfer_size, ref save_data);
            saveandrestore.save_uint(ref i, total_ship_transfer_size, ref save_data);
            saveandrestore.save_uint(ref i, prebuidlingid, ref save_data);

            //58
            saveandrestore.save_ushort(ref i, resident_shopping_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, resident_leisure_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, shop_get_goods_from_local_count_level1_final, ref save_data);
            saveandrestore.save_ushort(ref i, shop_get_goods_from_local_count_level2_final, ref save_data);
            saveandrestore.save_ushort(ref i, shop_get_goods_from_local_count_level3_final, ref save_data);
            saveandrestore.save_ushort(ref i, shop_get_goods_from_outside_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, industy_goods_to_outside_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, Grain_to_outside_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, Grain_to_industy_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, Grain_from_outside_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, food_to_outside_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, food_to_industy_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, food_from_outside_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, oil_to_outside_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, oil_to_industy_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, oil_from_outside_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, Petrol_to_outside_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, Petrol_to_industy_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, Petrol_from_outside_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, ore_to_outside_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, ore_to_industy_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, ore_from_outside_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, coal_to_outside_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, coal_to_industy_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, coal_from_outside_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, logs_to_outside_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, logs_to_industy_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, logs_from_outside_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, lumber_to_outside_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, lumber_to_industy_count_final, ref save_data);
            saveandrestore.save_ushort(ref i, lumber_from_outside_count_final, ref save_data);

            //24
            saveandrestore.save_ushort(ref i, all_comm_building_profit_final, ref save_data);
            saveandrestore.save_ushort(ref i, all_industry_building_profit_final, ref save_data);
            saveandrestore.save_ushort(ref i, all_foresty_building_profit_final, ref save_data);
            saveandrestore.save_ushort(ref i, all_farmer_building_profit_final, ref save_data);
            saveandrestore.save_ushort(ref i, all_oil_building_profit_final, ref save_data);
            saveandrestore.save_ushort(ref i, all_ore_building_profit_final, ref save_data);
            saveandrestore.save_ushort(ref i, all_comm_building_loss_final, ref save_data);
            saveandrestore.save_ushort(ref i, all_industry_building_loss_final, ref save_data);
            saveandrestore.save_ushort(ref i, all_foresty_building_loss_final, ref save_data);
            saveandrestore.save_ushort(ref i, all_farmer_building_loss_final, ref save_data);
            saveandrestore.save_ushort(ref i, all_oil_building_loss_final, ref save_data);
            saveandrestore.save_ushort(ref i, all_ore_building_loss_final, ref save_data);

            //24
            saveandrestore.save_ushort(ref i, all_office_level1_building_num, ref save_data);
            saveandrestore.save_ushort(ref i, all_office_level2_building_num, ref save_data);
            saveandrestore.save_ushort(ref i, all_office_level3_building_num, ref save_data);
            saveandrestore.save_ushort(ref i, all_office_high_tech_building_num, ref save_data);
            saveandrestore.save_ushort(ref i, all_office_level1_building_num_final, ref save_data);
            saveandrestore.save_ushort(ref i, all_office_level2_building_num_final, ref save_data);
            saveandrestore.save_ushort(ref i, all_office_level3_building_num_final, ref save_data);
            saveandrestore.save_ushort(ref i, all_office_high_tech_building_num_final, ref save_data);
            saveandrestore.save_float(ref i, office_gen_salary_index, ref save_data);
            saveandrestore.save_float(ref i, office_high_tech_salary_index, ref save_data);

        }
        protected void SimulationStepActive_1(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
        {
            if (buildingID > 49152)
            {
                DebugLog.LogToFileOnly("Error: buildingIDgreater than 49152");
            }
            base.SimulationStepActive(buildingID, ref buildingData, ref frameData);
            process_land_fee(buildingData, buildingID);
            caculate_employee_outcome(buildingData, buildingID);
            limit_and_check_building_money(buildingData, buildingID);
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

            process_building_data_final(buildingID, ref buildingData);

        }

        public void process_building_data_final(ushort buildingID, ref Building buildingData)
        {
            int i;
            if (prebuidlingid < buildingID)
            {
                for (i = (int)(prebuidlingid + 1); i < buildingID; i++)
                {
                    if (comm_data.building_money[i] != 0)
                    {
                        comm_data.building_money[i] = 0;
                    }
                }

                Notification.Problem problem = Notification.RemoveProblems(buildingData.m_problems, Notification.Problem.NoCustomers);
                //if (buildingData.Info.m_class.m_service == ItemClass.Service.Commercial)
                //{
                System.Random rand = new System.Random();
                if (comm_data.building_money[i] < -7000)
                {
                    if (rand.Next(10) < 2)
                    {
                        buildingData.m_majorProblemTimer = 192;
                        buildingData.m_flags &= ~Building.Flags.Active;
                        buildingData.m_flags |= Building.Flags.Abandoned;
                        buildingData.m_problems = (Notification.Problem.FatalProblem | (buildingData.m_problems & ~Notification.Problem.MajorProblem));
                        base.RemovePeople(buildingID, ref buildingData, 100);
                        this.BuildingDeactivated(buildingID, ref buildingData);
                        Singleton<BuildingManager>.instance.UpdateBuildingRenderer(buildingID, true);
                    }
                }
                if (comm_data.building_money[i] < -5000)
                {
                    problem = Notification.AddProblems(problem, Notification.Problem.NoCustomers | Notification.Problem.MajorProblem);
                    //here we can let building down
                }
                else if (comm_data.building_money[i] < -3000)
                {
                    problem = Notification.AddProblems(problem, Notification.Problem.NoCustomers);
                }
                buildingData.m_problems = problem;
                // }
            }
            else
            {
                //DebugLog.LogToFileOnly("caculate building final status, time " + comm_data.vehical_transfer_time[vehicleID].ToString());
                if (comm_data.update_outside_count == 63)
                {
                    resident_shopping_count_final = resident_shopping_count;
                    resident_leisure_count_final = resident_leisure_count;
                    shop_get_goods_from_local_count_level1_final = shop_get_goods_from_local_level1_count;
                    shop_get_goods_from_local_count_level2_final = shop_get_goods_from_local_level2_count;
                    shop_get_goods_from_local_count_level3_final = shop_get_goods_from_local_level3_count;
                    shop_get_goods_from_outside_count_final = shop_get_goods_from_outside_count;
                    industy_goods_to_outside_count_final = industy_goods_to_outside_count;
                    Grain_to_outside_count_final = Grain_to_outside_count;
                    Grain_to_industy_count_final = Grain_to_industy_count;
                    Grain_from_outside_count_final = Grain_from_outside_count;
                    food_to_outside_count_final = food_to_outside_count;
                    food_to_industy_count_final = food_to_industy_count;
                    food_from_outside_count_final = food_from_outside_count;
                    oil_to_outside_count_final = oil_to_outside_count;
                    oil_to_industy_count_final = oil_to_industy_count;
                    oil_from_outside_count_final = oil_from_outside_count;
                    Petrol_to_outside_count_final = Petrol_to_outside_count;
                    Petrol_to_industy_count_final = Petrol_to_industy_count;
                    Petrol_from_outside_count_final = Petrol_from_outside_count;
                    ore_to_outside_count_final = ore_to_outside_count;
                    ore_to_industy_count_final = ore_to_industy_count;
                    ore_from_outside_count_final = ore_from_outside_count;
                    coal_to_outside_count_final = coal_to_outside_count;
                    coal_to_industy_count_final = coal_to_industy_count;
                    coal_from_outside_count_final = coal_from_outside_count;
                    logs_to_outside_count_final = logs_to_outside_count;
                    logs_to_industy_count_final = logs_to_industy_count;
                    logs_from_outside_count_final = logs_from_outside_count;
                    lumber_to_outside_count_final = lumber_to_outside_count;
                    lumber_to_industy_count_final = lumber_to_industy_count;
                    lumber_from_outside_count_final = lumber_from_outside_count;
                }
                all_farmer_building_profit_final = all_farmer_building_profit;
                all_foresty_building_profit_final = all_foresty_building_profit;
                all_oil_building_profit_final = all_oil_building_profit;
                all_ore_building_profit_final = all_ore_building_profit;
                all_industry_building_profit_final = all_industry_building_profit;
                all_comm_building_profit_final = all_comm_building_profit;
                all_farmer_building_loss_final = all_farmer_building_loss;
                all_foresty_building_loss_final = all_foresty_building_loss;
                all_oil_building_loss_final = all_oil_building_loss;
                all_ore_building_loss_final = all_ore_building_loss;
                all_industry_building_loss_final = all_industry_building_loss;
                all_comm_building_loss_final = all_comm_building_loss;
                all_office_high_tech_building_num_final = all_office_high_tech_building_num;
                all_office_level1_building_num_final = all_office_level1_building_num;
                all_office_level2_building_num_final = all_office_level2_building_num;
                all_office_level3_building_num_final = all_office_level3_building_num;

                all_buildings_final = 0;
                all_farmer_building_profit = 0;
                all_foresty_building_profit = 0;
                all_oil_building_profit = 0;
                all_ore_building_profit = 0;
                all_industry_building_profit = 0;
                all_comm_building_profit = 0;
                all_farmer_building_loss = 0;
                all_foresty_building_loss = 0;
                all_oil_building_loss = 0;
                all_ore_building_loss = 0;
                all_industry_building_loss = 0;
                all_comm_building_loss = 0;
                if (comm_data.update_outside_count == 63)
                {
                    resident_shopping_count = 0;
                    resident_leisure_count = 0;
                    shop_get_goods_from_local_level1_count = 0;
                    shop_get_goods_from_local_level2_count = 0;
                    shop_get_goods_from_local_level3_count = 0;
                    shop_get_goods_from_outside_count = 0;
                    industy_goods_to_outside_count = 0;
                    Grain_to_outside_count = 0;
                    Grain_to_industy_count = 0;
                    Grain_from_outside_count = 0;
                    food_to_outside_count = 0;
                    food_to_industy_count = 0;
                    food_from_outside_count = 0;
                    oil_to_outside_count = 0;
                    oil_to_industy_count = 0;
                    oil_from_outside_count = 0;
                    Petrol_to_outside_count = 0;
                    Petrol_to_industy_count = 0;
                    Petrol_from_outside_count = 0;
                    ore_to_outside_count = 0;
                    ore_to_industy_count = 0;
                    ore_from_outside_count = 0;
                    coal_to_outside_count = 0;
                    coal_to_industy_count = 0;
                    coal_from_outside_count = 0;
                    logs_to_outside_count = 0;
                    logs_to_industy_count = 0;
                    logs_from_outside_count = 0;
                    lumber_to_outside_count = 0;
                    lumber_to_industy_count = 0;
                    lumber_from_outside_count = 0;
                }

                all_office_level1_building_num = 0;
                all_office_level2_building_num = 0;
                all_office_level3_building_num = 0;
                all_office_high_tech_building_num = 0;
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
            if (comm_data.building_money[buildingID] > 60000000)
            {
                comm_data.building_money[buildingID] = 60000000;
            }
            else if (comm_data.building_money[buildingID] < -60000000)
            {
                comm_data.building_money[buildingID] = -60000000;
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
                        all_industry_building_profit = (ushort)(all_industry_building_profit + 1);
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
                        all_industry_building_loss = (ushort)(all_industry_building_loss + 1);
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

            //add random value to match citizen salary
            if (num1 != 0)
            {
                num1 += (behaviour.m_educated0Count * rand.Next(1) + behaviour.m_educated0Count * rand.Next(2) + behaviour.m_educated0Count * rand.Next(3) + behaviour.m_educated0Count * rand.Next(4));
            }

            //money < 0, salary/2
            if (comm_data.building_money[buildingID] < 0)
            {
                comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)num1 * comm_data.salary_idex / 32;
            }
            else
            {
                comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)num1 * comm_data.salary_idex / 16;
            }
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
            int num2;
            num2 = Singleton<EconomyManager>.instance.GetTaxRate(this.m_info.m_class, taxationPolicies);
            if (((taxationPolicies & DistrictPolicies.Taxation.DontTaxLeisure) != DistrictPolicies.Taxation.None) && (building.Info.m_class.m_subService == ItemClass.SubService.CommercialLeisure))
            {
                num = 0;
            }
            num = (int)(num * ((float)(instance.m_districts.m_buffer[(int)district].GetLandValue() + 50) / 100));
            //num = num / comm_data.mantain_and_land_fee_decrease;

            //do this to decrase land outcome in early game;
            //float idex = (comm_data.mantain_and_land_fee_decrease > 1) ? (comm_data.mantain_and_land_fee_decrease / 2) : 1f;
            comm_data.building_money[buildingID] = (comm_data.building_money[buildingID] - (float)(num * num2) / 100);
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

            Singleton<EconomyManager>.instance.AddPrivateIncome(num, building.Info.m_class.m_service, building.Info.m_class.m_subService, building.Info.m_class.m_level, num2 * 100);
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
                    all_office_high_tech_building_num++;
                    break;
                case ItemClass.SubService.OfficeGeneric:
                    if (this.m_info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = comm_data.office_gen_levell;
                        all_office_level1_building_num++;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = comm_data.office_gen_level2;
                        all_office_level2_building_num++;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = comm_data.office_gen_level3;
                        all_office_level3_building_num++;
                    }
                    break;
                case ItemClass.SubService.IndustrialFarming:
                    incomeAccumulation = comm_data.indu_farm;
                    break;
                case ItemClass.SubService.IndustrialForestry:
                    incomeAccumulation = comm_data.indu_forest;
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
                /*case ItemClass.SubService.ResidentialHigh:
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
                    break;*/
                default: break;
            }
        }
    }

    public class pc_PrivateBuildingAI_1 : PrivateBuildingAI
    {
        protected void GetWorkBehaviour_1(ushort buildingID, ref Building buildingData, ref Citizen.BehaviourData behaviour, ref int aliveCount, ref int totalCount)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint num = buildingData.m_citizenUnits;
            int num2 = 0;
            while (num != 0u)
            {
                if ((ushort)(instance.m_units.m_buffer[(int)((UIntPtr)num)].m_flags & CitizenUnit.Flags.Work) != 0)
                {
                    instance.m_units.m_buffer[(int)((UIntPtr)num)].GetCitizenWorkBehaviour(ref behaviour, ref aliveCount, ref totalCount);
                }
                num = instance.m_units.m_buffer[(int)((UIntPtr)num)].m_nextUnit;
                if (++num2 > 524288)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }
            //more works with more m_efficiencyAccumulation
            //if alive count =0  and total cout > 1,then let alive count = 1 to let building product goods

            if ( (buildingData.Info.m_class.m_service == ItemClass.Service.Industrial) || (buildingData.Info.m_class.m_service == ItemClass.Service.Commercial))
            {
                int num3;
                int num4;
                int num5;
                int num6;
                this.CalculateWorkplaceCount(new Randomizer((int)buildingID), buildingData.Width, buildingData.Length, out num3, out num4, out num5, out num6);
                int workPlaceCount = num3 + num4 + num5 + num6;

                if ((totalCount > 1) && (aliveCount == 0))
                {
                    aliveCount = 1;
                }

                if (workPlaceCount == 0)
                {
                    DebugLog.LogToFileOnly("find a building workplacecount == 0");
                }

                float compensation_factor = (float)(aliveCount + workPlaceCount) / (float)(aliveCount * 2f);

                compensation_factor = ((float)(aliveCount + totalCount) / (float)(2f * totalCount)) * compensation_factor;

                if ((totalCount / 5f) > 1f)
                {
                    behaviour.m_efficiencyAccumulation = (int)((float)behaviour.m_efficiencyAccumulation * compensation_factor * (float)totalCount / 5f);
                }
                else
                {
                    behaviour.m_efficiencyAccumulation = (int)((float)behaviour.m_efficiencyAccumulation * compensation_factor);
                }

            }
        }
    }
}
