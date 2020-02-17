using RealCity.Util;

namespace RealCity.CustomManager
{
    public class RealCityEconomyManager
    {
       
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
        public static int[] garbage_income_forui = new int[17];
        public static int[] road_income_forui = new int[17];
        public static int[] playerIndustryIncomeForUI = new int[17];
        public static int[] school_income_forui = new int[17];
       //72*3 = 216
        public static int[] policeStationIncomeForUI = new int[17];
        public static int[] healthCareIncomeForUI = new int[17];
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

        public static void DataInit()
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
    }
}
