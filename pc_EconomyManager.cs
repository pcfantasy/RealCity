using System;
using System.Reflection;
using ColossalFramework;


namespace RealCity
{
    public class pc_EconomyManager
    {
        //all income * 100 , other wise is too small for game
        //all maintain fee / 1, otherwise it is too high for city
        //that is , for in game maintain fee and policy cost, it is 1/100 unit compared with enonomic element of real city mode
        //because maintance and police cost is too small when use real city mod.
        public static float Road = 0f;
        public static float Electricity = 0f;
        public static float Water = 0f;
        public static float Beautification = 0f;
        public static float Garbage = 0f;
        public static float HealthCare = 0f;
        public static float PoliceDepartment = 0f;
        public static float Education = 0f;
        public static float Monument = 0f;
        public static float FireDepartment = 0f;
        public static float PublicTransport = 0f;
        public static float Policy_cost = 0f;
        public static float Disaster = 0f;
        //public static float citizen_tax_income = 0f;
        public static float citizen_income = 0f;
        public static float tourist_income = 0f;


        public static float resident_low_level1_tax_income = 0f;
        public static float resident_low_level2_tax_income = 0f;
        public static float resident_low_level3_tax_income = 0f;
        public static float resident_low_level4_tax_income = 0f;
        public static float resident_low_level5_tax_income = 0f;
        public static float resident_low_eco_level1_tax_income = 0f;
        public static float resident_low_eco_level2_tax_income = 0f;
        public static float resident_low_eco_level3_tax_income = 0f;
        public static float resident_low_eco_level4_tax_income = 0f;
        public static float resident_low_eco_level5_tax_income = 0f;
        public static float resident_high_level1_tax_income = 0f;
        public static float resident_high_level2_tax_income = 0f;
        public static float resident_high_level3_tax_income = 0f;
        public static float resident_high_level4_tax_income = 0f;
        public static float resident_high_level5_tax_income = 0f;
        public static float resident_high_eco_level1_tax_income = 0f;
        public static float resident_high_eco_level2_tax_income = 0f;
        public static float resident_high_eco_level3_tax_income = 0f;
        public static float resident_high_eco_level4_tax_income = 0f;
        public static float resident_high_eco_level5_tax_income = 0f;

        public static float commerical_low_level1_income = 0f;
        public static float commerical_low_level2_income = 0f;
        public static float commerical_low_level3_income = 0f;
        public static float commerical_high_level1_income = 0f;
        public static float commerical_high_level2_income = 0f;
        public static float commerical_high_level3_income = 0f;
        public static float commerical_lei_income = 0f;
        public static float commerical_tou_income = 0f;
        public static float commerical_eco_income = 0f;
        public static float industy_forest_income = 0f;
        public static float industy_farm_income = 0f;
        public static float industy_oil_income = 0f;
        public static float industy_ore_income = 0f;
        public static float industy_gen_level1_income = 0f;
        public static float industy_gen_level2_income = 0f;
        public static float industy_gen_level3_income = 0f;
        public static float office_gen_level1_income = 0f;
        public static float office_gen_level2_income = 0f;
        public static float office_gen_level3_income = 0f;
        public static float office_high_tech_income = 0f;
        public static float resident_low_level1_income = 0f;
        public static float resident_low_level2_income = 0f;
        public static float resident_low_level3_income = 0f;
        public static float resident_low_level4_income = 0f;
        public static float resident_low_level5_income = 0f;
        public static float resident_low_eco_level1_income = 0f;
        public static float resident_low_eco_level2_income = 0f;
        public static float resident_low_eco_level3_income = 0f;
        public static float resident_low_eco_level4_income = 0f;
        public static float resident_low_eco_level5_income = 0f;
        public static float resident_high_level1_income = 0f;
        public static float resident_high_level2_income = 0f;
        public static float resident_high_level3_income = 0f;
        public static float resident_high_level4_income = 0f;
        public static float resident_high_level5_income = 0f;
        public static float resident_high_eco_level1_income = 0f;
        public static float resident_high_eco_level2_income = 0f;
        public static float resident_high_eco_level3_income = 0f;
        public static float resident_high_eco_level4_income = 0f;
        public static float resident_high_eco_level5_income = 0f;

        public static float commerical_low_level1_trade_income = 0f;
        public static float commerical_low_level2_trade_income = 0f;
        public static float commerical_low_level3_trade_income = 0f;
        public static float commerical_high_level1_trade_income = 0f;
        public static float commerical_high_level2_trade_income = 0f;
        public static float commerical_high_level3_trade_income = 0f;
        public static float commerical_lei_trade_income = 0f;
        public static float commerical_tou_trade_income = 0f;
        public static float commerical_eco_trade_income = 0f;
        public static float industy_forest_trade_income = 0f;
        public static float industy_farm_trade_income = 0f;
        public static float industy_oil_trade_income = 0f;
        public static float industy_ore_trade_income = 0f;
        public static float industy_gen_level1_trade_income = 0f;
        public static float industy_gen_level2_trade_income = 0f;
        public static float industy_gen_level3_trade_income = 0f;

        //citizen tax income
        public static int[] citizen_tax_income_forui = new int[17];

        //tourist for both citizen and tourist
        public static int[] citizen_income_forui = new int[17];
        public static int[] tourist_income_forui = new int[17];

        //land income
        public static int[] resident_high_landincome_forui = new int[17];
        public static int[] resident_low_landincome_forui = new int[17];
        public static int[] resident_high_eco_landincome_forui = new int[17];
        public static int[] resident_low_eco_landincome_forui = new int[17];
        public static int[] comm_high_landincome_forui = new int[17];
        public static int[] comm_low_landincome_forui = new int[17];
        public static int[] comm_lei_landincome_forui = new int[17];
        public static int[] comm_tou_landincome_forui = new int[17];
        public static int[] comm_eco_landincome_forui = new int[17];
        public static int[] indu_gen_landincome_forui = new int[17];
        public static int[] indu_farmer_landincome_forui = new int[17];
        public static int[] indu_foresty_landincome_forui = new int[17];
        public static int[] indu_oil_landincome_forui = new int[17];
        public static int[] indu_ore_landincome_forui = new int[17];
        public static int[] office_gen_landincome_forui = new int[17];
        public static int[] office_high_tech_landincome_forui = new int[17];

        //trade income
        public static int[] comm_high_tradeincome_forui = new int[17];
        public static int[] comm_low_tradeincome_forui = new int[17];
        public static int[] comm_lei_tradeincome_forui = new int[17];
        public static int[] comm_tou_tradeincome_forui = new int[17];
        public static int[] comm_eco_tradeincome_forui = new int[17];
        public static int[] indu_gen_tradeincome_forui = new int[17];
        public static int[] indu_farmer_tradeincome_forui = new int[17];
        public static int[] indu_foresty_tradeincome_forui = new int[17];
        public static int[] indu_oil_tradeincome_forui = new int[17];
        public static int[] indu_ore_tradeincome_forui = new int[17];

        //govement income
        public static float garbage_income = 0f;
        public static float road_income = 0f;
        public static float cemetery_income = 0f;
        public static int[] garbage_income_forui = new int[17];
        public static int[] road_income_forui = new int[17];
        public static int[] cemetery_income_forui = new int[17];

        //public static bool cemetery_income_forui = new int[17];

        public static byte[] save_data = new byte[2606];
        public static byte[] load_data = new byte[2606];

        //public income

        public static void clean_current(int current_idex)
        {
            int i = current_idex;
            citizen_tax_income_forui[i] = 0;
            citizen_income_forui[i] = 0;
            tourist_income_forui[i] = 0;
            resident_high_landincome_forui[i] = 0;
            resident_low_landincome_forui[i] = 0;
            resident_high_eco_landincome_forui[i] = 0;
            resident_low_eco_landincome_forui[i] = 0;
            comm_high_landincome_forui[i] = 0;
            comm_low_landincome_forui[i] = 0;
            comm_lei_landincome_forui[i] = 0;
            comm_tou_landincome_forui[i] = 0;
            comm_eco_landincome_forui[i] = 0;
            indu_gen_landincome_forui[i] = 0;
            indu_farmer_landincome_forui[i] = 0;
            indu_foresty_landincome_forui[i] = 0;
            indu_oil_landincome_forui[i] = 0;
            indu_ore_landincome_forui[i] = 0;
            office_gen_landincome_forui[i] = 0;
            office_high_tech_landincome_forui[i] = 0;
            comm_high_tradeincome_forui[i] = 0;
            comm_low_tradeincome_forui[i] = 0;
            comm_lei_tradeincome_forui[i] = 0;
            comm_tou_tradeincome_forui[i] = 0;
            comm_eco_tradeincome_forui[i] = 0;
            indu_gen_tradeincome_forui[i] = 0;
            indu_farmer_tradeincome_forui[i] = 0;
            indu_foresty_tradeincome_forui[i] = 0;
            indu_oil_tradeincome_forui[i] = 0;
            indu_ore_tradeincome_forui[i] = 0;
            garbage_income_forui[i] = 0;
            cemetery_income_forui[i] = 0;
            road_income_forui[i] = 0;
        }

        public static void data_init()
        {
            int i = 0;
            for (i = 0; i < 17; i++)
            {
                citizen_tax_income_forui[i] = 0;
                citizen_income_forui[i] = 0;
                tourist_income_forui[i] = 0;
                resident_high_landincome_forui[i] = 0;
                resident_low_landincome_forui[i] = 0;
                resident_high_eco_landincome_forui[i] = 0;
                resident_low_eco_landincome_forui[i] = 0;
                comm_high_landincome_forui[i] = 0;
                comm_low_landincome_forui[i] = 0;
                comm_lei_landincome_forui[i] = 0;
                comm_tou_landincome_forui[i] = 0;
                comm_eco_landincome_forui[i] = 0;
                indu_gen_landincome_forui[i] = 0;
                indu_farmer_landincome_forui[i] = 0;
                indu_foresty_landincome_forui[i] = 0;
                indu_oil_landincome_forui[i] = 0;
                indu_ore_landincome_forui[i] = 0;
                office_gen_landincome_forui[i] = 0;
                office_high_tech_landincome_forui[i] = 0;
                comm_high_tradeincome_forui[i] = 0;
                comm_low_tradeincome_forui[i] = 0;
                comm_lei_tradeincome_forui[i] = 0;
                comm_tou_tradeincome_forui[i] = 0;
                comm_eco_tradeincome_forui[i] = 0;
                indu_gen_tradeincome_forui[i] = 0;
                indu_farmer_tradeincome_forui[i] = 0;
                indu_foresty_tradeincome_forui[i] = 0;
                indu_oil_tradeincome_forui[i] = 0;
                indu_ore_tradeincome_forui[i] = 0;
                road_income_forui[i] = 0;
                cemetery_income_forui[i] = 0;
                garbage_income_forui[i] = 0;
            }
        }

        public static void load()
        {
            int i = 0;
            Road = saveandrestore.load_float(ref i, load_data);
            Electricity = saveandrestore.load_float(ref i, load_data);
            Water = saveandrestore.load_float(ref i, load_data);
            Beautification = saveandrestore.load_float(ref i, load_data);
            Garbage = saveandrestore.load_float(ref i, load_data);
            HealthCare = saveandrestore.load_float(ref i, load_data);
            PoliceDepartment = saveandrestore.load_float(ref i, load_data);
            Education = saveandrestore.load_float(ref i, load_data);
            Monument = saveandrestore.load_float(ref i, load_data);
            FireDepartment = saveandrestore.load_float(ref i, load_data);
            PublicTransport = saveandrestore.load_float(ref i, load_data);
            Policy_cost = saveandrestore.load_float(ref i, load_data);
            Disaster = saveandrestore.load_float(ref i, load_data);
            citizen_income = saveandrestore.load_float(ref i, load_data);
            tourist_income = saveandrestore.load_float(ref i, load_data);


            resident_low_level1_tax_income = saveandrestore.load_float(ref i, load_data);
            resident_low_level2_tax_income = saveandrestore.load_float(ref i, load_data);
            resident_low_level3_tax_income = saveandrestore.load_float(ref i, load_data);
            resident_low_level4_tax_income = saveandrestore.load_float(ref i, load_data);
            resident_low_level5_tax_income = saveandrestore.load_float(ref i, load_data);
            resident_low_eco_level1_tax_income = saveandrestore.load_float(ref i, load_data);
            resident_low_eco_level2_tax_income = saveandrestore.load_float(ref i, load_data);
            resident_low_eco_level3_tax_income = saveandrestore.load_float(ref i, load_data);
            resident_low_eco_level4_tax_income = saveandrestore.load_float(ref i, load_data);
            resident_low_eco_level5_tax_income = saveandrestore.load_float(ref i, load_data);
            resident_high_level1_tax_income = saveandrestore.load_float(ref i, load_data);
            resident_high_level2_tax_income = saveandrestore.load_float(ref i, load_data);
            resident_high_level3_tax_income = saveandrestore.load_float(ref i, load_data);
            resident_high_level4_tax_income = saveandrestore.load_float(ref i, load_data);
            resident_high_level5_tax_income = saveandrestore.load_float(ref i, load_data);
            resident_high_eco_level1_tax_income = saveandrestore.load_float(ref i, load_data);
            resident_high_eco_level2_tax_income = saveandrestore.load_float(ref i, load_data);
            resident_high_eco_level3_tax_income = saveandrestore.load_float(ref i, load_data);
            resident_high_eco_level4_tax_income = saveandrestore.load_float(ref i, load_data);
            resident_high_eco_level5_tax_income = saveandrestore.load_float(ref i, load_data);

            commerical_low_level1_income = saveandrestore.load_float(ref i, load_data);
            commerical_low_level2_income = saveandrestore.load_float(ref i, load_data);
            commerical_low_level3_income = saveandrestore.load_float(ref i, load_data);
            commerical_high_level1_income = saveandrestore.load_float(ref i, load_data);
            commerical_high_level2_income = saveandrestore.load_float(ref i, load_data);
            commerical_high_level3_income = saveandrestore.load_float(ref i, load_data);
            commerical_lei_income = saveandrestore.load_float(ref i, load_data);
            commerical_tou_income = saveandrestore.load_float(ref i, load_data);
            commerical_eco_income = saveandrestore.load_float(ref i, load_data);
            industy_forest_income = saveandrestore.load_float(ref i, load_data);
            industy_farm_income = saveandrestore.load_float(ref i, load_data);
            industy_oil_income = saveandrestore.load_float(ref i, load_data);
            industy_ore_income = saveandrestore.load_float(ref i, load_data);
            industy_gen_level1_income = saveandrestore.load_float(ref i, load_data);
            industy_gen_level2_income = saveandrestore.load_float(ref i, load_data);
            industy_gen_level3_income = saveandrestore.load_float(ref i, load_data);
            office_gen_level1_income = saveandrestore.load_float(ref i, load_data);
            office_gen_level2_income = saveandrestore.load_float(ref i, load_data);
            office_gen_level3_income = saveandrestore.load_float(ref i, load_data);
            office_high_tech_income = saveandrestore.load_float(ref i, load_data);
            resident_low_level1_income = saveandrestore.load_float(ref i, load_data);
            resident_low_level2_income = saveandrestore.load_float(ref i, load_data);
            resident_low_level3_income = saveandrestore.load_float(ref i, load_data);
            resident_low_level4_income = saveandrestore.load_float(ref i, load_data);
            resident_low_level5_income = saveandrestore.load_float(ref i, load_data);
            resident_low_eco_level1_income = saveandrestore.load_float(ref i, load_data);
            resident_low_eco_level2_income = saveandrestore.load_float(ref i, load_data);
            resident_low_eco_level3_income = saveandrestore.load_float(ref i, load_data);
            resident_low_eco_level4_income = saveandrestore.load_float(ref i, load_data);
            resident_low_eco_level5_income = saveandrestore.load_float(ref i, load_data);
            resident_high_level1_income = saveandrestore.load_float(ref i, load_data);
            resident_high_level2_income = saveandrestore.load_float(ref i, load_data);
            resident_high_level3_income = saveandrestore.load_float(ref i, load_data);
            resident_high_level4_income = saveandrestore.load_float(ref i, load_data);
            resident_high_level5_income = saveandrestore.load_float(ref i, load_data);
            resident_high_eco_level1_income = saveandrestore.load_float(ref i, load_data);
            resident_high_eco_level2_income = saveandrestore.load_float(ref i, load_data);
            resident_high_eco_level3_income = saveandrestore.load_float(ref i, load_data);
            resident_high_eco_level4_income = saveandrestore.load_float(ref i, load_data);
            resident_high_eco_level5_income = saveandrestore.load_float(ref i, load_data);

            commerical_low_level1_trade_income = saveandrestore.load_float(ref i, load_data);
            commerical_low_level2_trade_income = saveandrestore.load_float(ref i, load_data);
            commerical_low_level3_trade_income = saveandrestore.load_float(ref i, load_data);
            commerical_high_level1_trade_income = saveandrestore.load_float(ref i, load_data);
            commerical_high_level2_trade_income = saveandrestore.load_float(ref i, load_data);
            commerical_high_level3_trade_income = saveandrestore.load_float(ref i, load_data);
            commerical_lei_trade_income = saveandrestore.load_float(ref i, load_data);
            commerical_tou_trade_income = saveandrestore.load_float(ref i, load_data);
            commerical_eco_trade_income = saveandrestore.load_float(ref i, load_data);
            industy_forest_trade_income = saveandrestore.load_float(ref i, load_data);
            industy_farm_trade_income = saveandrestore.load_float(ref i, load_data);
            industy_oil_trade_income = saveandrestore.load_float(ref i, load_data);
            industy_ore_trade_income = saveandrestore.load_float(ref i, load_data);
            industy_gen_level1_trade_income = saveandrestore.load_float(ref i, load_data);
            industy_gen_level2_trade_income = saveandrestore.load_float(ref i, load_data);
            industy_gen_level3_trade_income = saveandrestore.load_float(ref i, load_data);

            citizen_tax_income_forui = saveandrestore.load_ints(ref i, load_data, 17);
            citizen_income_forui = saveandrestore.load_ints(ref i, load_data, 17);
            tourist_income_forui = saveandrestore.load_ints(ref i, load_data, 17);
            resident_high_landincome_forui = saveandrestore.load_ints(ref i, load_data, 17);
            resident_low_landincome_forui = saveandrestore.load_ints(ref i, load_data, 17);
            resident_high_eco_landincome_forui = saveandrestore.load_ints(ref i, load_data, 17);
            resident_low_eco_landincome_forui = saveandrestore.load_ints(ref i, load_data, 17);
            comm_high_landincome_forui = saveandrestore.load_ints(ref i, load_data, 17);
            comm_low_landincome_forui = saveandrestore.load_ints(ref i, load_data, 17);
            comm_lei_landincome_forui = saveandrestore.load_ints(ref i, load_data, 17);
            comm_tou_landincome_forui = saveandrestore.load_ints(ref i, load_data, 17);
            comm_eco_landincome_forui = saveandrestore.load_ints(ref i, load_data, 17);
            indu_gen_landincome_forui = saveandrestore.load_ints(ref i, load_data, 17);
            indu_farmer_landincome_forui = saveandrestore.load_ints(ref i, load_data, 17);
            indu_foresty_landincome_forui = saveandrestore.load_ints(ref i, load_data, 17);
            indu_oil_landincome_forui = saveandrestore.load_ints(ref i, load_data, 17);
            indu_ore_landincome_forui = saveandrestore.load_ints(ref i, load_data, 17);
            office_gen_landincome_forui = saveandrestore.load_ints(ref i, load_data, 17);
            office_high_tech_landincome_forui = saveandrestore.load_ints(ref i, load_data, 17);

            comm_high_tradeincome_forui = saveandrestore.load_ints(ref i, load_data, 17);
            comm_low_tradeincome_forui = saveandrestore.load_ints(ref i, load_data, 17);
            comm_lei_tradeincome_forui = saveandrestore.load_ints(ref i, load_data, 17);
            comm_tou_tradeincome_forui = saveandrestore.load_ints(ref i, load_data, 17);
            comm_eco_tradeincome_forui = saveandrestore.load_ints(ref i, load_data, 17);
            indu_gen_tradeincome_forui = saveandrestore.load_ints(ref i, load_data, 17);
            indu_farmer_tradeincome_forui = saveandrestore.load_ints(ref i, load_data, 17);
            indu_foresty_tradeincome_forui = saveandrestore.load_ints(ref i, load_data, 17);
            indu_oil_tradeincome_forui = saveandrestore.load_ints(ref i, load_data, 17);
            indu_ore_tradeincome_forui = saveandrestore.load_ints(ref i, load_data, 17);

            road_income_forui = saveandrestore.load_ints(ref i, load_data, 17);
            cemetery_income_forui = saveandrestore.load_ints(ref i, load_data, 17);
            garbage_income_forui = saveandrestore.load_ints(ref i, load_data, 17);

            road_income = saveandrestore.load_float(ref i, load_data);
            cemetery_income = saveandrestore.load_float(ref i, load_data);
            garbage_income = saveandrestore.load_float(ref i, load_data);
        }

        public static void save()
        {
            int i = 0;

            //680+1292+64+160+80+52 = 2382
            //13*4 = 52
            saveandrestore.save_float(ref i, Road, ref save_data);
            saveandrestore.save_float(ref i, Electricity, ref save_data);
            saveandrestore.save_float(ref i, Water, ref save_data);
            saveandrestore.save_float(ref i, Beautification, ref save_data);
            saveandrestore.save_float(ref i, Garbage, ref save_data);
            saveandrestore.save_float(ref i, HealthCare, ref save_data);
            saveandrestore.save_float(ref i, PoliceDepartment, ref save_data);
            saveandrestore.save_float(ref i, Education, ref save_data);
            saveandrestore.save_float(ref i, Monument, ref save_data);
            saveandrestore.save_float(ref i, FireDepartment, ref save_data);
            saveandrestore.save_float(ref i, PoliceDepartment, ref save_data);
            saveandrestore.save_float(ref i, Policy_cost, ref save_data);
            saveandrestore.save_float(ref i, Disaster, ref save_data);
            saveandrestore.save_float(ref i, citizen_income, ref save_data);
            saveandrestore.save_float(ref i, tourist_income, ref save_data);


            //20*4 = 80
            saveandrestore.save_float(ref i, resident_low_level1_tax_income, ref save_data);
            saveandrestore.save_float(ref i, resident_low_level2_tax_income, ref save_data);
            saveandrestore.save_float(ref i, resident_low_level3_tax_income, ref save_data);
            saveandrestore.save_float(ref i, resident_low_level4_tax_income, ref save_data);
            saveandrestore.save_float(ref i, resident_low_level5_tax_income, ref save_data);
            saveandrestore.save_float(ref i, resident_low_eco_level1_tax_income, ref save_data);
            saveandrestore.save_float(ref i, resident_low_eco_level2_tax_income, ref save_data);
            saveandrestore.save_float(ref i, resident_low_eco_level3_tax_income, ref save_data);
            saveandrestore.save_float(ref i, resident_low_eco_level4_tax_income, ref save_data);
            saveandrestore.save_float(ref i, resident_low_eco_level5_tax_income, ref save_data);
            saveandrestore.save_float(ref i, resident_high_level1_tax_income, ref save_data);
            saveandrestore.save_float(ref i, resident_high_level2_tax_income, ref save_data);
            saveandrestore.save_float(ref i, resident_high_level3_tax_income, ref save_data);
            saveandrestore.save_float(ref i, resident_high_level4_tax_income, ref save_data);
            saveandrestore.save_float(ref i, resident_high_level5_tax_income, ref save_data);
            saveandrestore.save_float(ref i, resident_high_eco_level1_tax_income, ref save_data);
            saveandrestore.save_float(ref i, resident_high_eco_level2_tax_income, ref save_data);
            saveandrestore.save_float(ref i, resident_high_eco_level3_tax_income, ref save_data);
            saveandrestore.save_float(ref i, resident_high_eco_level4_tax_income, ref save_data);
            saveandrestore.save_float(ref i, resident_high_eco_level5_tax_income, ref save_data);


            //40*4 = 160
            saveandrestore.save_float(ref i, commerical_low_level1_income, ref save_data);
            saveandrestore.save_float(ref i, commerical_low_level2_income, ref save_data);
            saveandrestore.save_float(ref i, commerical_low_level3_income, ref save_data);
            saveandrestore.save_float(ref i, commerical_high_level1_income, ref save_data);
            saveandrestore.save_float(ref i, commerical_high_level2_income, ref save_data);
            saveandrestore.save_float(ref i, commerical_high_level3_income, ref save_data);
            saveandrestore.save_float(ref i, commerical_lei_income, ref save_data);
            saveandrestore.save_float(ref i, commerical_tou_income, ref save_data);
            saveandrestore.save_float(ref i, commerical_eco_income, ref save_data);
            saveandrestore.save_float(ref i, industy_forest_income, ref save_data);
            saveandrestore.save_float(ref i, industy_farm_income, ref save_data);
            saveandrestore.save_float(ref i, industy_oil_income, ref save_data);
            saveandrestore.save_float(ref i, industy_ore_income, ref save_data);
            saveandrestore.save_float(ref i, industy_gen_level1_income, ref save_data);
            saveandrestore.save_float(ref i, industy_gen_level2_income, ref save_data);
            saveandrestore.save_float(ref i, industy_gen_level3_income, ref save_data);
            saveandrestore.save_float(ref i, office_gen_level1_income, ref save_data);
            saveandrestore.save_float(ref i, office_gen_level2_income, ref save_data);
            saveandrestore.save_float(ref i, office_gen_level3_income, ref save_data);
            saveandrestore.save_float(ref i, office_high_tech_income, ref save_data);
            saveandrestore.save_float(ref i, resident_low_level1_income, ref save_data);
            saveandrestore.save_float(ref i, resident_low_level2_income, ref save_data);
            saveandrestore.save_float(ref i, resident_low_level3_income, ref save_data);
            saveandrestore.save_float(ref i, resident_low_level4_income, ref save_data);
            saveandrestore.save_float(ref i, resident_low_level5_income, ref save_data);
            saveandrestore.save_float(ref i, resident_low_eco_level1_income, ref save_data);
            saveandrestore.save_float(ref i, resident_low_eco_level2_income, ref save_data);
            saveandrestore.save_float(ref i, resident_low_eco_level3_income, ref save_data);
            saveandrestore.save_float(ref i, resident_low_eco_level4_income, ref save_data);
            saveandrestore.save_float(ref i, resident_low_eco_level5_income, ref save_data);
            saveandrestore.save_float(ref i, resident_high_level1_income, ref save_data);
            saveandrestore.save_float(ref i, resident_high_level2_income, ref save_data);
            saveandrestore.save_float(ref i, resident_high_level3_income, ref save_data);
            saveandrestore.save_float(ref i, resident_high_level4_income, ref save_data);
            saveandrestore.save_float(ref i, resident_high_level5_income, ref save_data);
            saveandrestore.save_float(ref i, resident_high_eco_level1_income, ref save_data);
            saveandrestore.save_float(ref i, resident_high_eco_level2_income, ref save_data);
            saveandrestore.save_float(ref i, resident_high_eco_level3_income, ref save_data);
            saveandrestore.save_float(ref i, resident_high_eco_level4_income, ref save_data);
            saveandrestore.save_float(ref i, resident_high_eco_level5_income, ref save_data);

            //16*4 = 64
            saveandrestore.save_float(ref i, commerical_low_level1_trade_income, ref save_data);
            saveandrestore.save_float(ref i, commerical_low_level2_trade_income, ref save_data);
            saveandrestore.save_float(ref i, commerical_low_level3_trade_income, ref save_data);
            saveandrestore.save_float(ref i, commerical_high_level1_trade_income, ref save_data);
            saveandrestore.save_float(ref i, commerical_high_level2_trade_income, ref save_data);
            saveandrestore.save_float(ref i, commerical_high_level3_trade_income, ref save_data);
            saveandrestore.save_float(ref i, commerical_lei_trade_income, ref save_data);
            saveandrestore.save_float(ref i, commerical_tou_trade_income, ref save_data);
            saveandrestore.save_float(ref i, commerical_eco_trade_income, ref save_data);
            saveandrestore.save_float(ref i, industy_forest_trade_income, ref save_data);
            saveandrestore.save_float(ref i, industy_farm_trade_income, ref save_data);
            saveandrestore.save_float(ref i, industy_oil_trade_income, ref save_data);
            saveandrestore.save_float(ref i, industy_ore_trade_income, ref save_data);
            saveandrestore.save_float(ref i, industy_gen_level1_trade_income, ref save_data);
            saveandrestore.save_float(ref i, industy_gen_level2_trade_income, ref save_data);
            saveandrestore.save_float(ref i, industy_gen_level3_trade_income, ref save_data);


            //19*4*17 = 1292
            saveandrestore.save_ints(ref i, citizen_tax_income_forui, ref save_data);
            saveandrestore.save_ints(ref i, citizen_income_forui, ref save_data);
            saveandrestore.save_ints(ref i, tourist_income_forui, ref save_data);
            saveandrestore.save_ints(ref i, resident_high_landincome_forui, ref save_data);
            saveandrestore.save_ints(ref i, resident_low_landincome_forui, ref save_data);
            saveandrestore.save_ints(ref i, resident_high_eco_landincome_forui, ref save_data);
            saveandrestore.save_ints(ref i, resident_low_eco_landincome_forui, ref save_data);
            saveandrestore.save_ints(ref i, comm_high_landincome_forui, ref save_data);
            saveandrestore.save_ints(ref i, comm_low_landincome_forui, ref save_data);
            saveandrestore.save_ints(ref i, comm_lei_landincome_forui, ref save_data);
            saveandrestore.save_ints(ref i, comm_tou_landincome_forui, ref save_data);
            saveandrestore.save_ints(ref i, comm_eco_landincome_forui, ref save_data);
            saveandrestore.save_ints(ref i, indu_gen_landincome_forui, ref save_data);
            saveandrestore.save_ints(ref i, indu_farmer_landincome_forui, ref save_data);
            saveandrestore.save_ints(ref i, indu_foresty_landincome_forui, ref save_data);
            saveandrestore.save_ints(ref i, indu_oil_landincome_forui, ref save_data);
            saveandrestore.save_ints(ref i, indu_ore_landincome_forui, ref save_data);
            saveandrestore.save_ints(ref i, office_gen_landincome_forui, ref save_data);
            saveandrestore.save_ints(ref i, office_high_tech_landincome_forui, ref save_data);

            //10*17*4 = 680
            saveandrestore.save_ints(ref i, comm_high_tradeincome_forui, ref save_data);
            saveandrestore.save_ints(ref i, comm_low_tradeincome_forui, ref save_data);
            saveandrestore.save_ints(ref i, comm_lei_tradeincome_forui, ref save_data);
            saveandrestore.save_ints(ref i, comm_tou_tradeincome_forui, ref save_data);
            saveandrestore.save_ints(ref i, comm_eco_tradeincome_forui, ref save_data);
            saveandrestore.save_ints(ref i, indu_gen_tradeincome_forui, ref save_data);
            saveandrestore.save_ints(ref i, indu_farmer_tradeincome_forui, ref save_data);
            saveandrestore.save_ints(ref i, indu_foresty_tradeincome_forui, ref save_data);
            saveandrestore.save_ints(ref i, indu_oil_tradeincome_forui, ref save_data);
            saveandrestore.save_ints(ref i, indu_ore_tradeincome_forui, ref save_data);

            //3 * 17 * 4 = 204
            saveandrestore.save_ints(ref i, road_income_forui, ref save_data);
            saveandrestore.save_ints(ref i, cemetery_income_forui, ref save_data);
            saveandrestore.save_ints(ref i, garbage_income_forui, ref save_data);

            //3 * 4 = 12
            saveandrestore.save_float(ref i, road_income, ref save_data);
            saveandrestore.save_float(ref i, cemetery_income, ref save_data);
            saveandrestore.save_float(ref i, garbage_income, ref save_data);
        }

        public int FetchResource(EconomyManager.Resource resource, int amount, ItemClass itemClass)
        {
            //DebugLog.LogToFileOnly("go in FetchResource " + Policy_cost.ToString());
            // if(itemClass.m_layer == ItemClass.Layer.Markers) means mod added employee expense
            //if (itemClass.m_layer == ItemClass.Layer.ShipPaths) means mod added elecity and heat building oil and coal expense
            int temp;
            float coefficient;
            if (resource == EconomyManager.Resource.Maintenance)
            {
                if(itemClass.m_layer == ItemClass.Layer.Markers)
                {
                    coefficient = 16f / (float)comm_data.game_income_expense_multiple;
                }
                else
                {
                    coefficient = (float)comm_data.mantain_and_land_fee_decrease / (float)comm_data.game_income_expense_multiple;
                }
                switch (itemClass.m_service)
                {
                    case ItemClass.Service.Road:
                        Road += (float)amount / coefficient;
                        if (Road > 1)
                        {
                            temp = (int)Road;
                            Road = Road - (int)Road;
                            Singleton<EconomyManager>.instance.FetchResource(resource, temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
                            return amount;
                        }
                        return amount;
                    case ItemClass.Service.Garbage:
                        Garbage += (float)amount / coefficient;
                        if (Garbage > 1)
                        {
                            temp = (int)Garbage;
                            Garbage = Garbage - (int)Garbage;
                            Singleton<EconomyManager>.instance.FetchResource(resource, temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
                            return amount;
                        }
                        return amount;
                    case ItemClass.Service.PoliceDepartment:
                        PoliceDepartment += (float)amount / coefficient;
                        if (PoliceDepartment > 1)
                        {
                            temp = (int)PoliceDepartment;
                            PoliceDepartment = PoliceDepartment - (int)PoliceDepartment;
                            Singleton<EconomyManager>.instance.FetchResource(resource, temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
                            return amount;
                        }
                        return amount;
                    case ItemClass.Service.Beautification:
                        Beautification += (float)amount / coefficient;
                        if (Beautification > 1)
                        {
                            temp = (int)Beautification;
                            Beautification = Beautification - (int)Beautification;
                            Singleton<EconomyManager>.instance.FetchResource(resource, temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
                            return amount;
                        }
                        return amount;
                    case ItemClass.Service.Water:
                        Water += (float)amount / coefficient;
                        if (Water > 1)
                        {
                            temp = (int)Water;
                            Water = Water - (int)Water;
                            Singleton<EconomyManager>.instance.FetchResource(resource, temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
                            return amount;
                        }
                        return amount;
                    case ItemClass.Service.Education:
                        Education += (float)amount / coefficient;
                        if (Education > 1)
                        {
                            temp = (int)Education;
                            Education = Education - (int)Education;
                            Singleton<EconomyManager>.instance.FetchResource(resource, temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
                            return amount;
                        }
                        return amount;
                    case ItemClass.Service.Electricity:
                        Electricity += (float)amount / coefficient;
                        if (Electricity > 1)
                        {
                            temp = (int)Electricity;
                            Electricity = Electricity - (int)Electricity;
                            Singleton<EconomyManager>.instance.FetchResource(resource, temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
                            return amount;
                        }
                        return amount;
                    case ItemClass.Service.FireDepartment:
                        FireDepartment += (float)amount / coefficient;
                        if (FireDepartment > 1)
                        {
                            temp = (int)FireDepartment;
                            FireDepartment = FireDepartment - (int)FireDepartment;
                            Singleton<EconomyManager>.instance.FetchResource(resource, temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
                            return amount;
                        }
                        return amount;
                    case ItemClass.Service.Monument:
                        Monument += amount / coefficient;
                        if (Monument > 1)
                        {
                            temp = (int)Monument;
                            Monument = Monument - (int)Monument;
                            Singleton<EconomyManager>.instance.FetchResource(resource, temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
                            return amount;
                        }
                        return amount;
                    case ItemClass.Service.HealthCare:
                        HealthCare += (float)amount / coefficient;
                        if (HealthCare > 1)
                        {
                            temp = (int)HealthCare;
                            HealthCare = HealthCare - (int)HealthCare;
                            Singleton<EconomyManager>.instance.FetchResource(resource, temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
                            return amount;
                        }
                        return amount;
                    case ItemClass.Service.PublicTransport:
                        PublicTransport += (float)amount / coefficient;
                        if (PublicTransport > 1)
                        {
                            temp = (int)PublicTransport;
                            PublicTransport = PublicTransport - (int)PublicTransport;
                            Singleton<EconomyManager>.instance.FetchResource(resource, temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
                            return amount;
                        }
                        return amount;
                    case ItemClass.Service.Disaster:
                        Disaster += (float)amount / coefficient;
                        if (Disaster > 1)
                        {
                            temp = (int)Disaster;
                            Disaster = Disaster - (int)Disaster;
                            Singleton<EconomyManager>.instance.FetchResource(resource, temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
                            return amount;
                        }
                        return amount;
                    default: break;
                }
            }
            if (resource == EconomyManager.Resource.PolicyCost)
            {
                Policy_cost += (float)amount * (float)comm_data.game_income_expense_multiple / (float)comm_data.mantain_and_land_fee_decrease;
                //DebugLog.LogToFileOnly("go in FetchResource " + Policy_cost.ToString() + " " + amount.ToString());
                if (Policy_cost > 1)
                {
                    temp = (int)Policy_cost;
                    Policy_cost = Policy_cost - (int)Policy_cost;
                    Singleton<EconomyManager>.instance.FetchResource(resource, temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
                    return amount;
                }
                return amount;
            }
                return Singleton<EconomyManager>.instance.FetchResource(resource, amount, itemClass.m_service, itemClass.m_subService, itemClass.m_level);
        }



        //public int AddResource(EconomyManager.Resource resource, int amount, ItemClass itemClass)
        //{
        //int temp;
        //if((resource == EconomyManager.Resource.CitizenIncome) && (itemClass.m_service == ItemClass.Service.Citizen))
        //{
        //    citizen_income += (float)amount / 16;
        //    if (citizen_income > 1)
        //    {
        //        temp = (int)citizen_income;
        //        citizen_income = citizen_income - (int)citizen_income;
        //        Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, (int)temp, ItemClass.Service.None, ItemClass.SubService.None, ItemClass.Level.None);
        //        return Singleton<EconomyManager>.instance.AddResource(resource, (int)temp, itemClass.m_service, itemClass.m_subService, itemClass.m_level, DistrictPolicies.Taxation.None);
        //    }
        //    return 0;
        //}
        //else if (resource == EconomyManager.Resource.CitizenIncome)
        //{
        //    return 0;
        // }
        //else if (resource == EconomyManager.Resource.TourismIncome)
        //{
        //    Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, amount, ItemClass.Service.None, ItemClass.SubService.None, ItemClass.Level.None);
        //    return Singleton<EconomyManager>.instance.AddResource(resource, amount, itemClass.m_service, itemClass.m_subService, itemClass.m_level, DistrictPolicies.Taxation.None);
        //}
        //return Singleton<EconomyManager>.instance.AddResource(resource, amount, itemClass.m_service, itemClass.m_subService, itemClass.m_level, DistrictPolicies.Taxation.None);
        //}

        public int EXAddGovermentIncome(int amount, ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level, int taxRate)
        {
            switch (service)
            {
                case ItemClass.Service.Garbage:
                    garbage_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                    if (garbage_income > 1)
                    {
                        amount = (int)garbage_income;
                        garbage_income = garbage_income - (int)garbage_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                    garbage_income_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.Service.HealthCare:
                    //DebugLog.LogToFileOnly("add cemetery_income pre  amount = " + amount.ToString());
                    cemetery_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                    //DebugLog.LogToFileOnly("add cemetery_income pre  cemetery_income = " + cemetery_income.ToString() + "_taxMultiplier = " + _taxMultiplier.ToString());
                    if (cemetery_income > 1)
                    {
                        amount = (int)cemetery_income;
                        cemetery_income = cemetery_income - (int)cemetery_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                    //DebugLog.LogToFileOnly("add cemetery_income post  amount = " + amount.ToString());
                    cemetery_income_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.Service.Road:
                    road_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                    if (road_income > 1)
                    {
                        amount = (int)road_income;
                        road_income = road_income - (int)road_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                    road_income_forui[comm_data.update_money_count] += amount;
                    break;
                default:
                    amount = 0;
                    DebugLog.LogToFileOnly("find unknown  EXAddGovermentIncome building" + " building servise is" + service + " building subservise is" + subService + " buildlevelandtax is" + level + " " + taxRate);
                    break;
            }
            return amount;
        }



        public int EXAddPersonalTaxIncome(int amount, ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level, int taxRate)
        {
            switch (subService)
            {
                case ItemClass.SubService.ResidentialHigh:
                    if (level == ItemClass.Level.Level1)
                    {
                        resident_high_level1_tax_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_high_level1_tax_income > 1)
                        {
                            amount = (int)resident_high_level1_tax_income;
                            resident_high_level1_tax_income = resident_high_level1_tax_income - (int)resident_high_level1_tax_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level2)
                    {
                        resident_high_level2_tax_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_high_level2_tax_income > 1)
                        {
                            amount = (int)resident_high_level2_tax_income;
                            resident_high_level2_tax_income = resident_high_level2_tax_income - (int)resident_high_level2_tax_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level3)
                    {
                        resident_high_level3_tax_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_high_level3_tax_income > 1)
                        {
                            amount = (int)resident_high_level3_tax_income;
                            resident_high_level3_tax_income = resident_high_level3_tax_income - (int)resident_high_level3_tax_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level4)
                    {
                        resident_high_level4_tax_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_high_level4_tax_income > 1)
                        {
                            amount = (int)resident_high_level4_tax_income;
                            resident_high_level4_tax_income = resident_high_level4_tax_income - (int)resident_high_level4_tax_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level5)
                    {
                        resident_high_level5_tax_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_high_level5_tax_income > 1)
                        {
                            amount = (int)resident_high_level5_tax_income;
                            resident_high_level5_tax_income = resident_high_level5_tax_income - (int)resident_high_level5_tax_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    break;
                case ItemClass.SubService.ResidentialLow:
                    if (level == ItemClass.Level.Level1)
                    {
                        resident_low_level1_tax_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_low_level1_tax_income > 1)
                        {
                            amount = (int)resident_low_level1_tax_income;
                            resident_low_level1_tax_income = resident_low_level1_tax_income - (int)resident_low_level1_tax_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level2)
                    {
                        resident_low_level2_tax_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_low_level2_tax_income > 1)
                        {
                            amount = (int)resident_low_level2_tax_income;
                            resident_low_level2_tax_income = resident_low_level2_tax_income - (int)resident_low_level2_tax_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level3)
                    {
                        resident_low_level3_tax_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_low_level3_tax_income > 1)
                        {
                            amount = (int)resident_low_level3_tax_income;
                            resident_low_level3_tax_income = resident_low_level3_tax_income - (int)resident_low_level3_tax_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level4)
                    {
                        resident_low_level4_tax_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_low_level4_tax_income > 1)
                        {
                            amount = (int)resident_low_level4_tax_income;
                            resident_low_level4_tax_income = resident_low_level4_tax_income - (int)resident_low_level4_tax_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level5)
                    {
                        resident_low_level5_tax_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_low_level5_tax_income > 1)
                        {
                            amount = (int)resident_low_level5_tax_income;
                            resident_low_level5_tax_income = resident_low_level5_tax_income - (int)resident_low_level5_tax_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    break;
                case ItemClass.SubService.ResidentialHighEco:
                    if (level == ItemClass.Level.Level1)
                    {
                        resident_high_eco_level1_tax_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_high_eco_level1_tax_income > 1)
                        {
                            amount = (int)resident_high_eco_level1_tax_income;
                            resident_high_eco_level1_tax_income = resident_high_eco_level1_tax_income - (int)resident_high_eco_level1_tax_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level2)
                    {
                        resident_high_eco_level2_tax_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_high_eco_level2_tax_income > 1)
                        {
                            amount = (int)resident_high_eco_level2_tax_income;
                            resident_high_eco_level2_tax_income = resident_high_eco_level2_tax_income - (int)resident_high_eco_level2_tax_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level3)
                    {
                        resident_high_eco_level3_tax_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_high_eco_level3_tax_income > 1)
                        {
                            amount = (int)resident_high_eco_level3_tax_income;
                            resident_high_eco_level3_tax_income = resident_high_eco_level3_tax_income - (int)resident_high_eco_level3_tax_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level4)
                    {
                        resident_high_eco_level4_tax_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_high_eco_level4_tax_income > 1)
                        {
                            amount = (int)resident_high_eco_level4_tax_income;
                            resident_high_eco_level4_tax_income = resident_high_eco_level4_tax_income - (int)resident_high_eco_level4_tax_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level5)
                    {
                        resident_high_eco_level5_tax_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_high_eco_level5_tax_income > 1)
                        {
                            amount = (int)resident_high_eco_level5_tax_income;
                            resident_high_eco_level5_tax_income = resident_high_eco_level5_tax_income - (int)resident_high_eco_level5_tax_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    break;
                case ItemClass.SubService.ResidentialLowEco:
                    if (level == ItemClass.Level.Level1)
                    {
                        resident_low_eco_level1_tax_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_low_eco_level1_tax_income > 1)
                        {
                            amount = (int)resident_low_eco_level1_tax_income;
                            resident_low_eco_level1_tax_income = resident_low_eco_level1_tax_income - (int)resident_low_eco_level1_tax_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level2)
                    {
                        resident_low_eco_level2_tax_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_low_eco_level2_tax_income > 1)
                        {
                            amount = (int)resident_low_eco_level2_tax_income;
                            resident_low_eco_level2_tax_income = resident_low_eco_level2_tax_income - (int)resident_low_eco_level2_tax_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level3)
                    {
                        resident_low_eco_level3_tax_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_low_eco_level3_tax_income > 1)
                        {
                            amount = (int)resident_low_eco_level3_tax_income;
                            resident_low_eco_level3_tax_income = resident_low_eco_level3_tax_income - (int)resident_low_eco_level3_tax_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level4)
                    {
                        resident_low_eco_level4_tax_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_low_eco_level4_tax_income > 1)
                        {
                            amount = (int)resident_low_eco_level4_tax_income;
                            resident_low_eco_level4_tax_income = resident_low_eco_level4_tax_income - (int)resident_low_eco_level4_tax_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level5)
                    {
                        resident_low_eco_level5_tax_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_low_eco_level5_tax_income > 1)
                        {
                            amount = (int)resident_low_eco_level5_tax_income;
                            resident_low_eco_level5_tax_income = resident_low_eco_level5_tax_income - (int)resident_low_eco_level5_tax_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    break;
                default:
                    DebugLog.LogToFileOnly("find unknown  EXAddPrivateLandIncome building" + " building servise is" + service + " building subservise is" + subService + " buildlevelandtax is" + level + " " + taxRate);
                    break;
            }
            citizen_tax_income_forui[comm_data.update_money_count] = citizen_tax_income_forui[comm_data.update_money_count] + amount;
            //DebugLog.LogToFileOnly("find citizen tax income amout = " + amount.ToString() + "update_money_count = " + comm_data.update_money_count.ToString() + "taxmultiplex = " + _taxMultiplier.ToString());
            return amount;
        }


        public int EXAddTourismIncome(int amount, ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level, int taxRate)
        {
            if (taxRate == 114)
            {
                taxRate = 100;
                citizen_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                if (citizen_income > 1)
                {
                    amount = (int)citizen_income;
                    citizen_income = citizen_income - (int)citizen_income;
                }
                else
                {
                    amount = 0;
                }
                citizen_income_forui[comm_data.update_money_count] += amount;
            }
            else
            {
                taxRate = 100;
                tourist_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                if (tourist_income > 1)
                {
                    amount = (int)tourist_income;
                    tourist_income = tourist_income - (int)tourist_income;
                }
                else
                {
                    amount = 0;
                }
                tourist_income_forui[comm_data.update_money_count] += amount;
            }
            return amount;
        }


        public int EXAddPrivateTradeIncome(int amount, ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level, int taxRate)
        {
            switch (subService)
            {
                case ItemClass.SubService.IndustrialFarming:
                    industy_farm_trade_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                    if (industy_farm_trade_income > 1)
                    {
                        amount = (int)industy_farm_trade_income;
                        industy_farm_trade_income = industy_farm_trade_income - (int)industy_farm_trade_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                    indu_farmer_tradeincome_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.SubService.IndustrialForestry:
                    industy_forest_trade_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                    if (industy_forest_trade_income > 1)
                    {
                        amount = (int)industy_forest_trade_income;
                        industy_forest_trade_income = industy_forest_trade_income - (int)industy_forest_trade_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                    indu_foresty_tradeincome_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.SubService.IndustrialOil:
                    industy_oil_trade_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                    if (industy_oil_trade_income > 1)
                    {
                        amount = (int)industy_oil_trade_income;
                        industy_oil_trade_income = industy_oil_trade_income - (int)industy_oil_trade_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                    indu_oil_tradeincome_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.SubService.IndustrialOre:
                    industy_ore_trade_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                    if (industy_ore_trade_income > 1)
                    {
                        amount = (int)industy_ore_trade_income;
                        industy_ore_trade_income = industy_ore_trade_income - (int)industy_ore_trade_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                    indu_ore_tradeincome_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.SubService.IndustrialGeneric:
                    if (level == ItemClass.Level.Level1)
                    {
                        industy_gen_level1_trade_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (industy_gen_level1_trade_income > 1)
                        {
                            amount = (int)industy_gen_level1_trade_income;
                            industy_gen_level1_trade_income = industy_gen_level1_trade_income - (int)industy_gen_level1_trade_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level2)
                    {
                        industy_gen_level2_trade_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (industy_gen_level2_trade_income > 1)
                        {
                            amount = (int)industy_gen_level2_trade_income;
                            industy_gen_level2_trade_income = industy_gen_level2_trade_income - (int)industy_gen_level2_trade_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level3)
                    {
                        industy_gen_level3_trade_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (industy_gen_level3_trade_income > 1)
                        {
                            amount = (int)industy_gen_level3_trade_income;
                            industy_gen_level3_trade_income = industy_gen_level3_trade_income - (int)industy_gen_level3_trade_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    indu_gen_tradeincome_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialHigh:
                    if (level == ItemClass.Level.Level1)
                    {
                        commerical_high_level1_trade_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (commerical_high_level1_trade_income > 1)
                        {
                            amount = (int)commerical_high_level1_trade_income;
                            commerical_high_level1_trade_income = commerical_high_level1_trade_income - (int)commerical_high_level1_trade_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level2)
                    {
                        commerical_high_level2_trade_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (commerical_high_level2_trade_income > 1)
                        {
                            amount = (int)commerical_high_level2_trade_income;
                            commerical_high_level2_trade_income = commerical_high_level2_trade_income - (int)commerical_high_level2_trade_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level3)
                    {
                        commerical_high_level3_trade_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (commerical_high_level3_trade_income > 1)
                        {
                            amount = (int)commerical_high_level3_trade_income;
                            commerical_high_level3_trade_income = commerical_high_level3_trade_income - (int)commerical_high_level3_trade_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    comm_high_tradeincome_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialLow:
                    if (level == ItemClass.Level.Level1)
                    {
                        commerical_low_level1_trade_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (commerical_low_level1_trade_income > 1)
                        {
                            amount = (int)commerical_low_level1_trade_income;
                            commerical_low_level1_trade_income = commerical_low_level1_trade_income - (int)commerical_low_level1_trade_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level2)
                    {
                        commerical_low_level2_trade_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (commerical_low_level2_trade_income > 1)
                        {
                            amount = (int)commerical_low_level2_trade_income;
                            commerical_low_level2_trade_income = commerical_low_level2_trade_income - (int)commerical_low_level2_trade_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level3)
                    {
                        commerical_low_level3_trade_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (commerical_low_level3_trade_income > 1)
                        {
                            amount = (int)commerical_low_level3_trade_income;
                            commerical_low_level3_trade_income = commerical_low_level3_trade_income - (int)commerical_low_level3_trade_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    comm_low_tradeincome_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialLeisure:
                    commerical_lei_trade_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                    if (commerical_lei_trade_income > 1)
                    {
                        amount = (int)commerical_lei_trade_income;
                        commerical_lei_trade_income = commerical_lei_trade_income - (int)commerical_lei_trade_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                    comm_lei_tradeincome_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialTourist:
                    commerical_tou_trade_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                    if (commerical_tou_trade_income > 1)
                    {
                        amount = (int)commerical_tou_trade_income;
                        commerical_tou_trade_income = commerical_tou_trade_income - (int)commerical_tou_trade_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                    comm_tou_tradeincome_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialEco:
                    commerical_eco_trade_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                    if (commerical_eco_trade_income > 1)
                    {
                        amount = (int)commerical_eco_trade_income;
                        commerical_eco_trade_income = commerical_eco_trade_income - (int)commerical_eco_trade_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                    comm_eco_tradeincome_forui[comm_data.update_money_count] += amount;
                    break;
                default:
                    amount = 0;
                    DebugLog.LogToFileOnly("find unknown  EXAddPrivateTradeIncome building" + " building servise is" + service + " building subservise is" + subService + " buildlevelandtax is" + level + " " + taxRate);
                    break;
            }
            return amount;
        }

        public int EXAddPrivateLandIncome(int amount, ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level, int taxRate)
        {
            switch (subService)
            {
                case ItemClass.SubService.IndustrialFarming:
                    industy_farm_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                    if(industy_farm_income > 1)
                    {
                        amount = (int)industy_farm_income;
                        industy_farm_income = industy_farm_income - (int)industy_farm_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                    indu_farmer_landincome_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.SubService.IndustrialForestry:
                    industy_forest_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                    if (industy_forest_income > 1)
                    {
                        amount = (int)industy_forest_income;
                        industy_forest_income = industy_forest_income - (int)industy_forest_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                    indu_foresty_landincome_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.SubService.IndustrialOil:
                    industy_oil_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                    if (industy_oil_income > 1)
                    {
                        amount = (int)industy_oil_income;
                        industy_oil_income = industy_oil_income - (int)industy_oil_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                    indu_oil_landincome_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.SubService.IndustrialOre:
                    industy_ore_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                    if (industy_ore_income > 1)
                    {
                        amount = (int)industy_ore_income;
                        industy_ore_income = industy_ore_income - (int)industy_ore_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                    indu_ore_landincome_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.SubService.IndustrialGeneric:
                    if (level == ItemClass.Level.Level1)
                    {
                        industy_gen_level1_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (industy_gen_level1_income > 1)
                        {
                            amount = (int)industy_gen_level1_income;
                            industy_gen_level1_income = industy_gen_level1_income - (int)industy_gen_level1_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level2)
                    {
                        industy_gen_level2_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (industy_gen_level2_income > 1)
                        {
                            amount = (int)industy_gen_level2_income;
                            industy_gen_level2_income = industy_gen_level2_income - (int)industy_gen_level2_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level3)
                    {
                        industy_gen_level3_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (industy_gen_level3_income > 1)
                        {
                            amount = (int)industy_gen_level3_income;
                            industy_gen_level3_income = industy_gen_level3_income - (int)industy_gen_level3_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    indu_gen_landincome_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialHigh:
                    if (level == ItemClass.Level.Level1)
                    {
                        commerical_high_level1_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (commerical_high_level1_income > 1)
                        {
                            amount = (int)commerical_high_level1_income;
                            commerical_high_level1_income = commerical_high_level1_income - (int)commerical_high_level1_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level2)
                    {
                        commerical_high_level2_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (commerical_high_level2_income > 1)
                        {
                            amount = (int)commerical_high_level2_income;
                            commerical_high_level2_income = commerical_high_level2_income - (int)commerical_high_level2_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level3)
                    {
                        commerical_high_level3_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (commerical_high_level3_income > 1)
                        {
                            amount = (int)commerical_high_level3_income;
                            commerical_high_level3_income = commerical_high_level3_income - (int)commerical_high_level3_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    comm_high_landincome_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialLow:
                    if (level == ItemClass.Level.Level1)
                    {
                        commerical_low_level1_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (commerical_low_level1_income > 1)
                        {
                            amount = (int)commerical_low_level1_income;
                            commerical_low_level1_income = commerical_low_level1_income - (int)commerical_low_level1_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level2)
                    {
                        commerical_low_level2_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (commerical_low_level2_income > 1)
                        {
                            amount = (int)commerical_low_level2_income;
                            commerical_low_level2_income = commerical_low_level2_income - (int)commerical_low_level2_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level3)
                    {
                        commerical_low_level3_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (commerical_low_level3_income > 1)
                        {
                            amount = (int)commerical_low_level3_income;
                            commerical_low_level3_income = commerical_low_level3_income - (int)commerical_low_level3_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    comm_low_landincome_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialLeisure:
                    commerical_lei_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                    if (commerical_lei_income > 1)
                    {
                        amount = (int)commerical_lei_income;
                        commerical_lei_income = commerical_lei_income - (int)commerical_lei_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                    comm_lei_landincome_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialTourist:
                    commerical_tou_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                    if (commerical_tou_income > 1)
                    {
                        amount = (int)commerical_tou_income;
                        commerical_tou_income = commerical_tou_income - (int)commerical_tou_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                    comm_tou_landincome_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialEco:
                    commerical_eco_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                    if (commerical_eco_income > 1)
                    {
                        amount = (int)commerical_eco_income;
                        commerical_eco_income = commerical_eco_income - (int)commerical_eco_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                    comm_eco_landincome_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.SubService.ResidentialHigh:
                    if (level == ItemClass.Level.Level1)
                    {
                        resident_high_level1_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_high_level1_income > 1)
                        {
                            amount = (int)resident_high_level1_income;
                            resident_high_level1_income = resident_high_level1_income - (int)resident_high_level1_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level2)
                    {
                        resident_high_level2_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_high_level2_income > 1)
                        {
                            amount = (int)resident_high_level2_income;
                            resident_high_level2_income = resident_high_level2_income - (int)resident_high_level2_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level3)
                    {
                        resident_high_level3_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_high_level3_income > 1)
                        {
                            amount = (int)resident_high_level3_income;
                            resident_high_level3_income = resident_high_level3_income - (int)resident_high_level3_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level4)
                    {
                        resident_high_level4_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_high_level4_income > 1)
                        {
                            amount = (int)resident_high_level4_income;
                            resident_high_level4_income = resident_high_level4_income - (int)resident_high_level4_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level5)
                    {
                        resident_high_level5_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_high_level5_income > 1)
                        {
                            amount = (int)resident_high_level5_income;
                            resident_high_level5_income = resident_high_level5_income - (int)resident_high_level5_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    resident_high_landincome_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.SubService.ResidentialLow:
                    if (level == ItemClass.Level.Level1)
                    {
                        resident_low_level1_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        //DebugLog.LogToFileOnly("find resident1");
                        if (resident_low_level1_income > 1)
                        {
                            amount = (int)resident_low_level1_income;
                            resident_low_level1_income = resident_low_level1_income - (int)resident_low_level1_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level2)
                    {
                        resident_low_level2_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_low_level2_income > 1)
                        {
                            amount = (int)resident_low_level2_income;
                            resident_low_level2_income = resident_low_level2_income - (int)resident_low_level2_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level3)
                    {
                        resident_low_level3_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_low_level3_income > 1)
                        {
                            amount = (int)resident_low_level3_income;
                            resident_low_level3_income = resident_low_level3_income - (int)resident_low_level3_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level4)
                    {
                        resident_low_level4_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_low_level4_income > 1)
                        {
                            amount = (int)resident_low_level4_income;
                            resident_low_level4_income = resident_low_level4_income - (int)resident_low_level4_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level5)
                    {
                        resident_low_level5_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_low_level5_income > 1)
                        {
                            amount = (int)resident_low_level5_income;
                            resident_low_level5_income = resident_low_level5_income - (int)resident_low_level5_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    resident_low_landincome_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.SubService.ResidentialHighEco:
                    if (level == ItemClass.Level.Level1)
                    {
                        resident_high_eco_level1_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_high_eco_level1_income > 1)
                        {
                            amount = (int)resident_high_eco_level1_income;
                            resident_high_eco_level1_income = resident_high_eco_level1_income - (int)resident_high_eco_level1_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level2)
                    {
                        resident_high_eco_level2_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_high_eco_level2_income > 1)
                        {
                            amount = (int)resident_high_eco_level2_income;
                            resident_high_eco_level2_income = resident_high_eco_level2_income - (int)resident_high_eco_level2_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level3)
                    {
                        resident_high_eco_level3_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_high_eco_level3_income > 1)
                        {
                            amount = (int)resident_high_eco_level3_income;
                            resident_high_eco_level3_income = resident_high_eco_level3_income - (int)resident_high_eco_level3_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level4)
                    {
                        resident_high_eco_level4_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_high_eco_level4_income > 1)
                        {
                            amount = (int)resident_high_eco_level4_income;
                            resident_high_eco_level4_income = resident_high_eco_level4_income - (int)resident_high_eco_level4_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level5)
                    {
                        resident_high_eco_level5_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_high_eco_level5_income > 1)
                        {
                            amount = (int)resident_high_eco_level5_income;
                            resident_high_eco_level5_income = resident_high_eco_level5_income - (int)resident_high_eco_level5_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    resident_high_eco_landincome_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.SubService.ResidentialLowEco:
                    if (level == ItemClass.Level.Level1)
                    {
                        resident_low_eco_level1_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_low_eco_level1_income > 1)
                        {
                            amount = (int)resident_low_eco_level1_income;
                            resident_low_eco_level1_income = resident_low_eco_level1_income - (int)resident_low_eco_level1_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level2)
                    {
                        resident_low_eco_level2_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_low_eco_level2_income > 1)
                        {
                            amount = (int)resident_low_eco_level2_income;
                            resident_low_eco_level2_income = resident_low_eco_level2_income - (int)resident_low_eco_level2_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level3)
                    {
                        resident_low_eco_level3_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_low_eco_level3_income > 1)
                        {
                            amount = (int)resident_low_eco_level3_income;
                            resident_low_eco_level3_income = resident_low_eco_level3_income - (int)resident_low_eco_level3_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level4)
                    {
                        resident_low_eco_level4_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_low_eco_level4_income > 1)
                        {
                            amount = (int)resident_low_eco_level4_income;
                            resident_low_eco_level4_income = resident_low_eco_level4_income - (int)resident_low_eco_level4_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    else if (level == ItemClass.Level.Level5)
                    {
                        resident_low_eco_level5_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                        if (resident_low_eco_level5_income > 1)
                        {
                            amount = (int)resident_low_eco_level5_income;
                            resident_low_eco_level5_income = resident_low_eco_level5_income - (int)resident_low_eco_level5_income;
                        }
                        else
                        {
                            amount = 0;
                        }
                    }
                    resident_low_eco_landincome_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.SubService.OfficeGeneric:
                        if (level == ItemClass.Level.Level1)
                        {
                            office_gen_level1_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                            if (office_gen_level1_income > 1)
                            {
                                amount = (int)office_gen_level1_income;
                                office_gen_level1_income = office_gen_level1_income - (int)office_gen_level1_income;
                            }
                            else
                            {
                                amount = 0;
                            }
                        }
                        else if (level == ItemClass.Level.Level2)
                        {
                            office_gen_level2_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                            if (office_gen_level2_income > 1)
                            {
                                amount = (int)office_gen_level2_income;
                                office_gen_level2_income = office_gen_level2_income - (int)office_gen_level2_income;
                            }
                            else
                            {
                                amount = 0;
                            }
                        }
                        else if (level == ItemClass.Level.Level3)
                        {
                            office_gen_level3_income += (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                            if (office_gen_level1_income > 1)
                            {
                                amount = (int)office_gen_level3_income;
                                office_gen_level3_income = office_gen_level3_income - (int)office_gen_level3_income;
                            }
                            else
                            {
                                amount = 0;
                            }
                        }
                    office_gen_landincome_forui[comm_data.update_money_count] += amount;
                    break;
                case ItemClass.SubService.OfficeHightech:
                    office_high_tech_income+= (float)((double)(amount * taxRate * ((float)_taxMultiplier / 1000000f)));
                    if (office_high_tech_income > 1)
                    {
                        amount = (int)office_high_tech_income;
                        office_high_tech_income = office_high_tech_income - (int)office_high_tech_income;
                    }
                    else
                    {
                        amount = 0;
                    }
                    office_high_tech_landincome_forui[comm_data.update_money_count] += amount;
                    break;
                default:
                    amount = 0;
                    DebugLog.LogToFileOnly("find unknown  EXAddPrivateLandIncome building" + " building servise is" + service + " building subservise is" + subService + " buildlevelandtax is" + level + " " + taxRate);
                    break;
            }
            return amount;
        }

        public int AddPrivateIncome(int amount, ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level, int taxRate)
        {
            if (!_init)
            {
                _init = true;
                Init();
            }
            if (taxRate == 115)
            {
                //115 means goverment income
                taxRate = 100;
                Singleton<EconomyManager>.instance.m_EconomyWrapper.OnAddResource(EconomyManager.Resource.PrivateIncome, ref amount, service, subService, level);
                amount = EXAddGovermentIncome(amount, service, subService, level, taxRate);
                service = ItemClass.Service.Industrial;
                subService = ItemClass.SubService.IndustrialGeneric;
                level = ItemClass.Level.Level3;
                amount = amount * comm_data.game_income_expense_multiple;
                int num = ClassIndex(service, subService, level);
                if (num != -1)
                {
                    _income[num * 17 + 16] += (long)amount;
                }
                _cashAmount = (long)cashAmount.GetValue(Singleton<EconomyManager>.instance);
                cashAmount.SetValue(Singleton<EconomyManager>.instance, (_cashAmount + (long)amount));
                _cashDelta = (long)cashDelta.GetValue(Singleton<EconomyManager>.instance);
                cashDelta.SetValue(Singleton<EconomyManager>.instance, (_cashDelta + (long)amount));
            }
            else if ((taxRate == 113) || (taxRate == 114))
            {
                //113 means tourist tourism income // 114 means resident tourism income
                //taxRate = 100;
                Singleton<EconomyManager>.instance.m_EconomyWrapper.OnAddResource(EconomyManager.Resource.PrivateIncome, ref amount, service, subService, level);
                amount = EXAddTourismIncome(amount, service, subService, level, taxRate);
                amount = amount * comm_data.game_income_expense_multiple;
                int num = ClassIndex(service, subService, level);
                if (num != -1)
                {
                    _income[num * 17 + 16] += (long)amount;
                }
                _cashAmount = (long)cashAmount.GetValue(Singleton<EconomyManager>.instance);
                cashAmount.SetValue(Singleton<EconomyManager>.instance, (_cashAmount + (long)amount));
                _cashDelta = (long)cashDelta.GetValue(Singleton<EconomyManager>.instance);
                cashDelta.SetValue(Singleton<EconomyManager>.instance, (_cashDelta + (long)amount));
            }
            else if (taxRate == 112)
            {
                //112 means personal income tax income
                taxRate = 100;
                Singleton<EconomyManager>.instance.m_EconomyWrapper.OnAddResource(EconomyManager.Resource.PrivateIncome, ref amount, service, subService, level);
                amount = EXAddPersonalTaxIncome(amount, service, subService, level, taxRate);
                amount = amount* comm_data.game_income_expense_multiple;
                int num = ClassIndex(service, subService, level);
                if (num != -1)
                {
                    _income[num * 17 + 16] += (long)amount;
                }
                _cashAmount = (long)cashAmount.GetValue(Singleton<EconomyManager>.instance);
                cashAmount.SetValue(Singleton<EconomyManager>.instance, (_cashAmount + (long)amount));
                _cashDelta = (long)cashDelta.GetValue(Singleton<EconomyManager>.instance);
                cashDelta.SetValue(Singleton<EconomyManager>.instance, (_cashDelta + (long)amount));
            }
            else if (taxRate == 111)
            {
                //111 means trade income
                taxRate = 100;
                Singleton<EconomyManager>.instance.m_EconomyWrapper.OnAddResource(EconomyManager.Resource.PrivateIncome, ref amount, service, subService, level);
                amount = EXAddPrivateTradeIncome(amount, service, subService, level, taxRate);
                amount = amount * comm_data.game_income_expense_multiple;
                int num = ClassIndex(service, subService, level);
                if (num != -1)
                {
                    _income[num * 17 + 16] += (long)amount;
                }
                _cashAmount = (long)cashAmount.GetValue(Singleton<EconomyManager>.instance);
                cashAmount.SetValue(Singleton<EconomyManager>.instance, (_cashAmount + (long)amount));
                _cashDelta = (long)cashDelta.GetValue(Singleton<EconomyManager>.instance);
                cashDelta.SetValue(Singleton<EconomyManager>.instance, (_cashDelta + (long)amount));
            }
            else if (taxRate >= 100)
            {
                taxRate = taxRate / 100;
                Singleton<EconomyManager>.instance.m_EconomyWrapper.OnAddResource(EconomyManager.Resource.PrivateIncome, ref amount, service, subService, level);
                amount = EXAddPrivateLandIncome(amount, service, subService, level, taxRate);
                amount = amount * comm_data.game_income_expense_multiple;
                int num = ClassIndex(service, subService, level);
                if (num != -1)
                {
                    _income[num * 17 + 16] += (long)amount;
                }
                _cashAmount =(long)cashAmount.GetValue(Singleton<EconomyManager>.instance);
                cashAmount.SetValue(Singleton<EconomyManager>.instance, (_cashAmount + (long)amount));
                _cashDelta = (long)cashDelta.GetValue(Singleton<EconomyManager>.instance);
                cashDelta.SetValue(Singleton<EconomyManager>.instance, (_cashDelta + (long)amount));
            }
            else
            {
                Singleton<EconomyManager>.instance.m_EconomyWrapper.OnAddResource(EconomyManager.Resource.PrivateIncome, ref amount, service, subService, level);
                amount = (int)(((long)amount * (long)taxRate * (long)_taxMultiplier + 999999L) / 1000000L);
                //amount = amount * 10;
                //all income * 10 , other wise is too small for game
                //all maintain fee / 10, otherwise it is too high for city
                //that is , for in game maintain fee and policy cost, it is 1/100 unit compared with enonomic element of real city mode
                //int num = ClassIndex(service, subService, level);
                //if (num != -1)
                //{
                //    _income[num * 17 + 16] += (long)amount;
                //}
                //_cashAmount += (long)amount;
                //_cashDelta += (long)amount;
            }
            //DebugLog.LogToFileOnly("cashamout = " + _cashAmount.ToString());
            return amount;
        }

        private static int ClassIndex(ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level)
        {
            int privateServiceIndex = ItemClass.GetPrivateServiceIndex(service);
            if (privateServiceIndex != -1)
            {
                return PrivateClassIndex(service, subService, level);
            }
            return PublicClassIndex(service, subService) + 120;
        }

        // EconomyManager
        private static int PrivateClassIndex(ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level)
        {
            int privateServiceIndex = ItemClass.GetPrivateServiceIndex(service);
            int privateSubServiceIndex = ItemClass.GetPrivateSubServiceIndex(subService);
            if (privateServiceIndex == -1)
            {
                return -1;
            }
            int num;
            if (privateSubServiceIndex != -1)
            {
                num = 8 + privateSubServiceIndex;
            }
            else
            {
                num = privateServiceIndex;
            }
            num *= 5;
            if (level != ItemClass.Level.None)
            {
                num = (int)(num + level);
            }
            return num;
        }

        private static int PublicClassIndex(ItemClass.Service service, ItemClass.SubService subService)
        {
            int publicServiceIndex = ItemClass.GetPublicServiceIndex(service);
            int publicSubServiceIndex = ItemClass.GetPublicSubServiceIndex(subService);
            if (publicServiceIndex == -1)
            {
                return -1;
            }
            int result;
            if (publicSubServiceIndex != -1)
            {
                result = 12 + publicSubServiceIndex;
            }
            else
            {
                result = publicServiceIndex;
            }
            return result;
        }

        public static void Init()
        {
            //DebugLog.Log("Init fake transfer manager");
            try
            {
                var inst = Singleton<EconomyManager>.instance;
                var income = typeof(EconomyManager).GetField("m_income", BindingFlags.NonPublic | BindingFlags.Instance);
                var taxMultiplier = typeof(EconomyManager).GetField("m_taxMultiplier", BindingFlags.NonPublic | BindingFlags.Instance);
                cashDelta = typeof(EconomyManager).GetField("m_cashDelta", BindingFlags.NonPublic | BindingFlags.Instance);
                cashAmount = typeof(EconomyManager).GetField("m_cashAmount", BindingFlags.NonPublic | BindingFlags.Instance);
                if (inst == null)
                {
                    DebugLog.LogToFileOnly("No instance of EconomyManager found!");
                    return;
                }
                _income = income.GetValue(inst) as long[];
                //_taxMultiplier = (int)taxMultiplier.GetValue(inst);
                _taxMultiplier = 10000;
                _cashDelta = (long)cashDelta.GetValue(inst);
                _cashAmount = (long)cashAmount.GetValue(inst);
                if (_income == null)
                {
                    DebugLog.LogToFileOnly("EconomyManager Arrays are null");
                }
            }
            catch (Exception ex)
            {
                DebugLog.LogToFileOnly("EconomyManager Exception: " + ex.Message);
            }
        }
        private static FieldInfo cashAmount;
        private static FieldInfo cashDelta;
        private static int _taxMultiplier;
        private static long _cashDelta;
        private static long _cashAmount;
        private static long[] _income;
        private static bool _init;
    }
}
