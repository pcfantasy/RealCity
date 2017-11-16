using ColossalFramework;
using ColossalFramework.Math;
using System;
using UnityEngine;
using System.Text.RegularExpressions;

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
        public const float oil_export_price = 0.25f;
        public const float ore_export_price = 0.25f;
        public const float grain_export_price = 0.25f;
        public const float log_export_price = 0.25f;

        public const float good_import_price = 2.6f;
        public const float food_import_price = 0.9f;
        public const float petrol_import_price = 0.9f;
        public const float coal_import_price = 0.9f;
        public const float lumber_import_price = 0.9f;
        public const float oil_import_price = 0.4f;
        public const float ore_import_price = 0.4f;
        public const float grain_import_price = 0.4f;
        public const float log_import_price = 0.4f;

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
                DebugLog.LogToFileOnly("Error: buildingID greater than 49152");
            }
            base.SimulationStepActive(buildingID, ref buildingData, ref frameData);
            process_land_fee(buildingData, buildingID);
            caculate_employee_expense(buildingData, buildingID);
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
            process_addition_product(buildingID, ref buildingData);

        }


        public void process_addition_demand(ushort buildingID, ref Building buildingData)
        {
            Regex r = new Regex("IndustrialBuildingAI");
            //Regex p = new Regex("IndustrialExtractorAI");

            Match m = r.Match(Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingID].Info.m_buildingAI.ToString());
            //Match n = p.Match(Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingID].Info.m_buildingAI.ToString());
            if (m.Success)
            {
                Citizen.BehaviourData behaviourData = default(Citizen.BehaviourData);
                int aliveWorkerCount = 0;
                int totalWorkerCount = 0;
                base.GetWorkBehaviour(buildingID, ref buildingData, ref behaviourData, ref aliveWorkerCount, ref totalWorkerCount);
                TransferManager.TransferReason incomingTransferReason = pc_IndustrialBuildingAI.GetIncomingTransferReason(buildingData, buildingID);
                TransferManager.TransferReason outgoingTransferReason = pc_IndustrialBuildingAI.GetOutgoingTransferReason(buildingData);
                int width = buildingData.Width;
                int length = buildingData.Length;
                int production_capacity = pc_IndustrialBuildingAI.CalculateProductionCapacity(buildingData, new Randomizer((int)buildingID), width, length);
                int num30 = 0;
                int num31 = 0;
                int num32 = 0;
                int value2 = 0;
                if (outgoingTransferReason != TransferManager.TransferReason.None)
                {
                    base.CalculateOwnVehicles(buildingID, ref buildingData, outgoingTransferReason, ref num30, ref num31, ref num32, ref value2);
                    buildingData.m_tempExport = (byte)Mathf.Clamp(value2, (int)buildingData.m_tempExport, 255);
                }

                //add 3 more cars to industrial
                if (buildingData.m_fireIntensity == 0 && outgoingTransferReason != TransferManager.TransferReason.None)
                {
                    int num35 = Mathf.Max(1, production_capacity / 6);
                    int customBuffer = (int)buildingData.m_customBuffer2;
                    if (customBuffer >= 8000 && (num30 < num35+3) && (num30 >= num35))
                    {
                        TransferManager.TransferOffer offer2 = default(TransferManager.TransferOffer);
                        offer2.Priority = customBuffer * 8 / 8000;
                        offer2.Building = buildingID;
                        offer2.Position = buildingData.m_position;
                        offer2.Amount = Mathf.Min(customBuffer / 8000, num35 + 3 - num30);
                        offer2.Active = true;
                        Singleton<TransferManager>.instance.AddOutgoingOffer(outgoingTransferReason, offer2);
                    }
                }
            }
        }

        public void process_addition_product(ushort buildingID, ref Building buildingData)
        {
            Regex r = new Regex("IndustrialBuildingAI");
            //Regex p = new Regex("IndustrialExtractorAI");

            Match m = r.Match(Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingID].Info.m_buildingAI.ToString());
            //Match n = p.Match(Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingID].Info.m_buildingAI.ToString());
            if (m.Success)
            {
                DistrictManager instance = Singleton<DistrictManager>.instance;
                byte district = instance.GetDistrict(buildingData.m_position);
                DistrictPolicies.Services servicePolicies = instance.m_districts.m_buffer[(int)district].m_servicePolicies;
                DistrictPolicies.CityPlanning cityPlanningPolicies = instance.m_districts.m_buffer[(int)district].m_cityPlanningPolicies;
                Citizen.BehaviourData behaviourData = default(Citizen.BehaviourData);
                int aliveWorkerCount = 0;
                int totalWorkerCount = 0;
                base.GetWorkBehaviour(buildingID, ref buildingData, ref behaviourData, ref aliveWorkerCount, ref totalWorkerCount);
                TransferManager.TransferReason incomingTransferReason = pc_IndustrialBuildingAI.GetIncomingTransferReason(buildingData, buildingID);
                TransferManager.TransferReason outgoingTransferReason = pc_IndustrialBuildingAI.GetOutgoingTransferReason(buildingData);
                int width = buildingData.Width;
                int length = buildingData.Length;
                int production_capacity = pc_IndustrialBuildingAI.CalculateProductionCapacity(buildingData, new Randomizer((int)buildingID), width, length);
                int num5 = 4000;
                int num6 = 8000;
                int consumptionDivider = pc_IndustrialBuildingAI.GetConsumptionDivider(buildingData);
                int num8 = Mathf.Max(production_capacity * 500 / consumptionDivider, num5 * 4);
                int num9 = production_capacity * 500;
                int num10 = Mathf.Max(num9, num6 * 2);

                int work_capacity = 1;
                if (aliveWorkerCount != 0)
                {
                    work_capacity = (int)(((float)behaviourData.m_efficiencyAccumulation * (0.5f * (float)aliveWorkerCount + 0.5f * (float)totalWorkerCount)) / (float)aliveWorkerCount);
                }
                else
                {
                    work_capacity = (int)(60 * 0.5f * (float)totalWorkerCount);
                }

                int production_actually = 0;

                float compensation_factor = 1f;
                if ((width * length) != 0)
                {
                    compensation_factor = 16f / (float)(width * length);
                    if (compensation_factor < 1f)
                    {
                        compensation_factor = 1f;
                    }
                }


                float space_factor = 1f;

                if (num10 != 0)
                {
                    space_factor  = (float)(num10 - buildingData.m_customBuffer2) / (float)num10;
                }
                production_actually = (int)((float)work_capacity * (float)production_capacity * compensation_factor * space_factor / 80f);
                

                int game_production = buildingData.m_customBuffer2 - comm_data.building_buffer2[buildingID];

                if ((game_production > 1000) || (game_production < 0))
                {
                    //DebugLog.LogToFileOnly("may be first time progress or save data not correct, game_production = " + game_production.ToString() + "work_capacity = " + work_capacity.ToString() + "production_capacity =" + production_capacity.ToString());
                }
                else if(game_production == 0)
                {
                    //DebugLog.LogToFileOnly("game_production = 0 ??? ");
                }
                else
                {
                    if (game_production >= production_actually)
                    {
                        //DebugLog.LogToFileOnly("game_production great than production_actually, game_production= " + game_production.ToString() + "production_actually = " + production_actually.ToString());
                        production_actually = 0;
                    }
                    else
                    {
                        production_actually = production_actually - game_production;
                    }

                    if (incomingTransferReason != TransferManager.TransferReason.None)
                    {
                        buildingData.m_customBuffer1 -= (ushort)((production_actually + consumptionDivider - 1) / consumptionDivider);
                    }
                    if (outgoingTransferReason != TransferManager.TransferReason.None)
                    {
                        if ((cityPlanningPolicies & DistrictPolicies.CityPlanning.IndustrySpace) != DistrictPolicies.CityPlanning.None)
                        {
                            buildingData.m_customBuffer2 = (ushort)Mathf.Min(num10, (int)buildingData.m_customBuffer2 + production_actually * 2);
                        }
                        else
                        {
                            buildingData.m_customBuffer2 += (ushort)production_actually;
                        }
                        IndustrialBuildingAI.ProductType productType = IndustrialBuildingAI.GetProductType(outgoingTransferReason);
                        if (productType != IndustrialBuildingAI.ProductType.None)
                        {
                            StatisticsManager instance2 = Singleton<StatisticsManager>.instance;
                            StatisticBase statisticBase = instance2.Acquire<StatisticArray>(StatisticType.GoodsProduced);
                            statisticBase.Acquire<StatisticInt32>((int)productType, 5).Add(production_actually);
                        }
                    }
                }
                comm_data.building_buffer2[buildingID] = buildingData.m_customBuffer2;
                process_addition_demand(buildingID, ref buildingData);
            }//m.sucess
        }

        public static int process_building_asset(ushort buildingID, ref Building buildingData)
        {
            Regex p = new Regex("IndustrialExtractorAI");
            Match n = p.Match(Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingID].Info.m_buildingAI.ToString());
            int asset = 0;
            if (n.Success)
            {
                asset = (int)(buildingData.m_customBuffer1 * grain_export_price);
            } else
            {
                if (buildingData.Info.m_class.m_service == ItemClass.Service.Commercial)
                {
                    asset = (int)((buildingData.m_customBuffer1 + buildingData.m_customBuffer2) * (good_export_price / 4f));
                }
                else if (buildingData.Info.m_class.m_subService == ItemClass.SubService.IndustrialGeneric)
                {
                    asset = (int)(buildingData.m_customBuffer1 * lumber_export_price + buildingData.m_customBuffer2 * (good_export_price/4f));
                }
                else if (buildingData.Info.m_class.m_subService == ItemClass.SubService.IndustrialForestry)
                {
                    asset = (int)(buildingData.m_customBuffer1 * log_export_price + buildingData.m_customBuffer2 * lumber_export_price);
                }
                else if (buildingData.Info.m_class.m_subService == ItemClass.SubService.IndustrialFarming)
                {
                    asset = (int)(buildingData.m_customBuffer1 * log_export_price + buildingData.m_customBuffer2 * lumber_export_price);
                }
                else if (buildingData.Info.m_class.m_subService == ItemClass.SubService.IndustrialOil)
                {
                    asset = (int)(buildingData.m_customBuffer1 * log_export_price + buildingData.m_customBuffer2 * lumber_export_price);
                }
                else if (buildingData.Info.m_class.m_subService == ItemClass.SubService.IndustrialOre)
                {
                    asset = (int)(buildingData.m_customBuffer1 * log_export_price + buildingData.m_customBuffer2 * lumber_export_price);
                }
                else
                {

                }
            }
            return asset;
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

                int asset = process_building_asset(buildingID, ref buildingData);
                Notification.Problem problem = Notification.RemoveProblems(buildingData.m_problems, Notification.Problem.NoCustomers);
                //if (buildingData.Info.m_class.m_service == ItemClass.Service.Commercial)
                //{
                System.Random rand = new System.Random();
                if ((comm_data.building_money[i] + asset) < -1000)
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
                if ((comm_data.building_money[i] + asset) < -500)
                {
                    problem = Notification.AddProblems(problem, Notification.Problem.NoCustomers | Notification.Problem.MajorProblem);
                }
                else if ((comm_data.building_money[i] + asset) < 0)
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

            int asset = process_building_asset(buildingID, ref building);

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
                    case ItemClass.SubService.CommercialEco:
                        all_comm_building_loss = (ushort)(all_comm_building_loss + 1);
                        break;
                    default: break;
                }
            }
        }


        public void caculate_employee_expense(Building building, ushort buildingID)
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

            //money < 0, salary/1.5f
            if ((building.Info.m_class.m_service == ItemClass.Service.Commercial) || (building.Info.m_class.m_service == ItemClass.Service.Industrial))
            {
                if (comm_data.building_money[buildingID] < 0)
                {
                    comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)num1 * comm_data.salary_idex / 24;
                }
                else
                {
                    comm_data.building_money[buildingID] = comm_data.building_money[buildingID] - (float)num1 * comm_data.salary_idex / 16;
                }
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

            //do this to decrase land expense in early game;
            //float idex = (comm_data.mantain_and_land_fee_decrease > 1) ? (comm_data.mantain_and_land_fee_decrease / 2) : 1f;
            if ((building.Info.m_class.m_service == ItemClass.Service.Commercial) || (building.Info.m_class.m_service == ItemClass.Service.Industrial))
            {
                comm_data.building_money[buildingID] = (comm_data.building_money[buildingID] - (float)(num * num2) / 100);
            }
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
                    incomeAccumulation = (int)(comm_data.office_high_tech * office_high_tech_salary_index);
                    all_office_high_tech_building_num++;
                    break;
                case ItemClass.SubService.OfficeGeneric:
                    if (this.m_info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = (int)(comm_data.office_gen_levell * office_gen_salary_index);
                        all_office_level1_building_num++;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = (int)(comm_data.office_gen_level2 * office_gen_salary_index);
                        all_office_level2_building_num++;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = (int)(comm_data.office_gen_level3 * office_gen_salary_index);
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
                default: break;
            }
        }
    }
}
