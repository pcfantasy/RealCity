using ColossalFramework;
using RealCity.Util;

namespace RealCity.CustomManager
{
    public class RealCityEconomyManager
    {
        
        public static float citizen_income = 0f;
        public static float tourist_income = 0f;
        public static float resident_tax_income = 0f;
        public static float commerical_low_income = 0f;
        public static float commerical_high_income = 0f;
        public static float commerical_lei_income = 0f;
        public static float commerical_tou_income = 0f;
        public static float commerical_eco_income = 0f;
        public static float industy_forest_income = 0f;
        public static float industy_farm_income = 0f;
        public static float industy_oil_income = 0f;
        public static float industy_ore_income = 0f;
        public static float industy_gen_income = 0f;
        public static float office_gen_income = 0f;
        public static float office_high_tech_income = 0f;
        public static float resident_low_income = 0f;
        public static float resident_low_eco_income = 0f;
        public static float resident_high_income = 0f;
        public static float resident_high_eco_income = 0f;
        public static float commerical_low_trade_income = 0f;
        public static float commerical_high_trade_income = 0f;
        public static float commerical_lei_trade_income = 0f;
        public static float commerical_tou_trade_income = 0f;
        public static float commerical_eco_trade_income = 0f;
        public static float industy_forest_trade_income = 0f;
        public static float industy_farm_trade_income = 0f;
        public static float industy_oil_trade_income = 0f;
        public static float industy_ore_trade_income = 0f;
        public static float industy_gen_trade_income = 0f;
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
        public static float playerIndustryIncome = 0f;
        public static int[] garbage_income_forui = new int[17];
        public static int[] road_income_forui = new int[17];
        public static int[] playerIndustryIncomeForUI = new int[17];
        public static float school_income = 0f;
        public static int[] school_income_forui = new int[17];
       //72*3 = 216
        public static float policeStationIncome = 0f;
        public static int[] policeStationIncomeForUI = new int[17];
        public static float healthCareIncome = 0f;
        public static int[] healthCareIncomeForUI = new int[17];
        public static float fireStationIncome = 0f;
        public static int[] fireStationIncomeForUI = new int[17];
        //3*4 = 12
        //2628+216+12 = 2856
        public static byte[] saveData = new byte[2856];

        public static void CleanCurrent(int current_idex)
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
            playerIndustryIncomeForUI[i] = 0;
            road_income_forui[i] = 0;
            school_income_forui[i] = 0;
            policeStationIncomeForUI[i] = 0;
            fireStationIncomeForUI[i] = 0;
            healthCareIncomeForUI[i] = 0;
        }

        public static void dataInit()
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
                playerIndustryIncomeForUI[i] = 0;
                garbage_income_forui[i] = 0;
                school_income_forui[i] = 0;
                policeStationIncomeForUI[i] = 0;
                fireStationIncomeForUI[i] = 0;
                healthCareIncomeForUI[i] = 0;
            }
        }

        public static void Load()
        {
            int i = 0;
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            // 97*4 = 388
            // 35*4*17 = 2768
            citizen_tax_income_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            citizen_income_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            tourist_income_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            resident_high_landincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            resident_low_landincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            resident_high_eco_landincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            resident_low_eco_landincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            comm_high_landincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            comm_low_landincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            comm_lei_landincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            comm_tou_landincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            comm_eco_landincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            indu_gen_landincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            indu_farmer_landincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            indu_foresty_landincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            indu_oil_landincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            indu_ore_landincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            office_gen_landincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            office_high_tech_landincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            comm_high_tradeincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            comm_low_tradeincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            comm_lei_tradeincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            comm_tou_tradeincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            comm_eco_tradeincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            indu_gen_tradeincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            indu_farmer_tradeincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            indu_foresty_tradeincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            indu_oil_tradeincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            indu_ore_tradeincome_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            road_income_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            playerIndustryIncomeForUI = SaveAndRestore.load_ints(ref i, saveData, 17);
            garbage_income_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            school_income_forui = SaveAndRestore.load_ints(ref i, saveData, 17);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            policeStationIncomeForUI = SaveAndRestore.load_ints(ref i, saveData, 17);
            SaveAndRestore.load_float(ref i, saveData);
            healthCareIncomeForUI = SaveAndRestore.load_ints(ref i, saveData, 17);
            SaveAndRestore.load_float(ref i, saveData);
            fireStationIncomeForUI = SaveAndRestore.load_ints(ref i, saveData, 17);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            SaveAndRestore.load_float(ref i, saveData);
            DebugLog.LogToFileOnly("saveData in EM is " + i.ToString());
        }

        public static void Save()
        {
            int i = 0;
            //680+1292+64+160+80+52 = 2382
            //15*4 = 60
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            //20*4 = 80
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            //40*4 = 160
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            //16*4 = 64
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            //19*4*17 = 1292
            SaveAndRestore.save_ints(ref i, citizen_tax_income_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, citizen_income_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, tourist_income_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, resident_high_landincome_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, resident_low_landincome_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, resident_high_eco_landincome_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, resident_low_eco_landincome_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, comm_high_landincome_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, comm_low_landincome_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, comm_lei_landincome_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, comm_tou_landincome_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, comm_eco_landincome_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, indu_gen_landincome_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, indu_farmer_landincome_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, indu_foresty_landincome_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, indu_oil_landincome_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, indu_ore_landincome_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, office_gen_landincome_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, office_high_tech_landincome_forui, ref saveData);
            //10*17*4 = 680
            SaveAndRestore.save_ints(ref i, comm_high_tradeincome_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, comm_low_tradeincome_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, comm_lei_tradeincome_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, comm_tou_tradeincome_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, comm_eco_tradeincome_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, indu_gen_tradeincome_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, indu_farmer_tradeincome_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, indu_foresty_tradeincome_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, indu_oil_tradeincome_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, indu_ore_tradeincome_forui, ref saveData);
            //3 * 17 * 4 = 204
            SaveAndRestore.save_ints(ref i, road_income_forui, ref saveData);
            SaveAndRestore.save_ints(ref i, playerIndustryIncomeForUI, ref saveData);
            SaveAndRestore.save_ints(ref i, garbage_income_forui, ref saveData);
            //3 * 4 = 12
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            //3 * 17 * 4 = 204    - 136
            SaveAndRestore.save_ints(ref i, school_income_forui, ref saveData);
            //3 * 4 = 12   - 8
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_ints(ref i, policeStationIncomeForUI, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_ints(ref i, healthCareIncomeForUI, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_ints(ref i, fireStationIncomeForUI, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);

            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
            SaveAndRestore.save_float(ref i, 0, ref saveData);
        }

        public void EXAddGovermentIncome(ref int amount, ItemClass.Service service)
        {
            switch (service)
            {
                case ItemClass.Service.Garbage:
                    ProcessUnitTax100(ref amount, ref garbage_income);
                    garbage_income_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.Service.Education:
                    ProcessUnitTax100(ref amount, ref school_income);
                    school_income_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.Service.HealthCare:
                    ProcessUnitTax100(ref amount, ref healthCareIncome);
                    healthCareIncomeForUI[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.Service.FireDepartment:
                    ProcessUnitTax100(ref amount, ref fireStationIncome);
                    fireStationIncomeForUI[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.Service.PoliceDepartment:
                    ProcessUnitTax100(ref amount, ref policeStationIncome);
                    policeStationIncomeForUI[MainDataStore.update_money_count] += amount;
                    break;
                default:
                    amount = 0;
                    DebugLog.LogToFileOnly("Error: Find unknown EXAddGovermentIncome building" + " service is" + service.ToString());
                    break;
            }
        }

        public void EXAddPersonalTaxIncome(ref int amount, ItemClass.Service service)
        {
            switch (service)
            {
                case ItemClass.Service.Residential:
                    ProcessUnitTax100(ref amount, ref resident_tax_income);
                    break;
                default:
                    DebugLog.LogToFileOnly("Find unknown  EXAddPrivateLandIncome building" + " building service is" + service);
                    break;
            }
            citizen_tax_income_forui[MainDataStore.update_money_count] = citizen_tax_income_forui[MainDataStore.update_money_count] + amount;
        }

        public void EXAddTourismIncome(ref int amount, int taxRate)
        {
            if (taxRate == 114333)
            {
                ProcessUnitTax100(ref amount, ref citizen_income);
                citizen_income_forui[MainDataStore.update_money_count] += amount;
            }
            else
            {
                ProcessUnitTax100(ref amount, ref tourist_income);
                tourist_income_forui[MainDataStore.update_money_count] += amount;
            }
        }

        public void EXAddPrivateTradeIncome(ref int amount, ItemClass.SubService subService)
        {
            switch (subService)
            {
                case ItemClass.SubService.IndustrialFarming:
                    ProcessUnitTax100(ref amount, ref industy_farm_trade_income);
                    indu_farmer_tradeincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.IndustrialForestry:
                    ProcessUnitTax100(ref amount, ref industy_forest_trade_income);
                    indu_foresty_tradeincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.IndustrialOil:
                    ProcessUnitTax100(ref amount, ref industy_oil_trade_income);
                    indu_oil_tradeincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.IndustrialOre:
                    ProcessUnitTax100(ref amount, ref industy_ore_trade_income);
                    indu_ore_tradeincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.IndustrialGeneric:
                    ProcessUnitTax100(ref amount, ref industy_gen_trade_income);
                    indu_gen_tradeincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialHigh:
                    ProcessUnitTax100(ref amount, ref commerical_high_trade_income);
                    comm_high_tradeincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialLow:
                    ProcessUnitTax100(ref amount, ref commerical_low_trade_income);
                    comm_low_tradeincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialLeisure:
                    ProcessUnitTax100(ref amount, ref commerical_lei_trade_income);
                    comm_lei_tradeincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialTourist:
                    ProcessUnitTax100(ref amount, ref commerical_tou_trade_income);
                    comm_tou_tradeincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialEco:
                    ProcessUnitTax100(ref amount, ref commerical_eco_trade_income);
                    comm_eco_tradeincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                default:
                    amount = 0;
                    DebugLog.LogToFileOnly("Find unknown  EXAddPrivateTradeIncome building" + " building subservise is" + subService);
                    break;
            }
        }

        public void EXAddPrivateLandIncome(ref int amount, ItemClass.SubService subService, int taxRate)
        {
            switch (subService)
            {
                case ItemClass.SubService.IndustrialFarming:
                    ProcessUnit(ref amount, ref industy_farm_income, taxRate);
                    indu_farmer_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.IndustrialForestry:
                    ProcessUnit(ref amount, ref industy_forest_income, taxRate);
                    indu_foresty_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.IndustrialOil:
                    ProcessUnit(ref amount, ref industy_oil_income, taxRate);
                    indu_oil_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.IndustrialOre:
                    ProcessUnit(ref amount, ref industy_ore_income, taxRate);
                    indu_ore_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.IndustrialGeneric:
                    ProcessUnit(ref amount, ref industy_gen_income, taxRate);
                    indu_gen_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialHigh:
                    ProcessUnit(ref amount, ref commerical_high_income, taxRate);
                    comm_high_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialLow:
                    ProcessUnit(ref amount, ref commerical_low_income, taxRate);
                    comm_low_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialLeisure:
                    ProcessUnit(ref amount, ref commerical_lei_income, taxRate);
                    comm_lei_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialTourist:
                    ProcessUnit(ref amount, ref commerical_tou_income, taxRate);
                    comm_tou_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialEco:
                    ProcessUnit(ref amount, ref commerical_eco_income, taxRate);
                    comm_eco_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.ResidentialHigh:
                    ProcessUnit(ref amount, ref resident_high_income, taxRate);
                    resident_high_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.ResidentialLow:
                    ProcessUnit(ref amount, ref resident_low_income, taxRate);
                    resident_low_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.ResidentialHighEco:
                    ProcessUnit(ref amount, ref resident_high_eco_income, taxRate);
                    resident_high_eco_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.ResidentialLowEco:
                    ProcessUnit(ref amount, ref resident_low_eco_income, taxRate);
                    resident_low_eco_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.OfficeGeneric:
                    ProcessUnit(ref amount, ref office_gen_income, taxRate);
                    office_gen_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.OfficeHightech:
                    ProcessUnit(ref amount, ref office_high_tech_income, taxRate);
                    office_high_tech_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                default:
                    amount = 0;
                    DebugLog.LogToFileOnly("find unknown  EXAddPrivateLandIncome building" + " building subService is" + subService + " buildlevelandtax is "  + taxRate);
                    break;
            }
        }

        public void ProcessUnit(ref int amount, ref float container, int taxRate)
        {
            container += (amount * taxRate / 100f);
            if (container > 1)
            {
                amount = (int)container;
                container = container - (int)container;
            }
            else
            {
                amount = 0;
            }
        }

        public void ProcessUnitTax100(ref int amount, ref float container)
        {
            container += amount;
            if (container > 1)
            {
                amount = (int)container;
                container = container - (int)container;
            }
            else
            {
                amount = 0;
            }
        }
        public void AddPrivateIncome(int amount, ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level, int taxRate)
        {
            if (amount < 0)
            {
                DebugLog.LogToFileOnly("income < 0 error: class =" + service.ToString() + subService.ToString() + level.ToString());
            }
            if (taxRate == 115333)
            {
                //115333 means playerbuilding income
                //taxRate = 100; no need to send taxRate.
                EXAddGovermentIncome(ref amount, service);
                service = ItemClass.Service.Industrial;
                subService = ItemClass.SubService.IndustrialGeneric;
                level = ItemClass.Level.Level3;
            }
            else if ((taxRate == 113333) || (taxRate == 114333))
            {
                //113333 means tourist tourism income // 114333 means resident tourism income
                EXAddTourismIncome(ref amount, taxRate);
            }
            else if (taxRate == 112333)
            {
                //112333 means personal slary tax income
                //taxRate = 100; no need to send taxRate.
                EXAddPersonalTaxIncome(ref amount, service);
            }
            else if (taxRate == 111)
            {
                //111 means trade income
                //taxRate = 100; no need to send taxRate.
                EXAddPrivateTradeIncome(ref amount, subService);
            }
            else if (taxRate >= 100)
            {
                taxRate = UniqueFacultyAI.IncreaseByBonus(UniqueFacultyAI.FacultyBonus.Economics, taxRate);
                taxRate = taxRate / 100;
                EXAddPrivateLandIncome(ref amount, subService, taxRate);
            }
            else
            {
                amount = 0;
            }

            Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.ResourcePrice, amount, service, subService, level, DistrictPolicies.Taxation.None);
        }
    }
}
