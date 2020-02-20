using RealCity.Util;

namespace RealCity.CustomManager
{
    public class RealCityEconomyManager
    {
       
        public static int[] citizen_tax_income_forui = new int[17];
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
        public static int[] policeStationIncomeForUI = new int[17];
        public static int[] healthCareIncomeForUI = new int[17];
        public static int[] fireStationIncomeForUI = new int[17];

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
            for (int i = 0; i < 17; i++)
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

        public static void Load(ref byte[] saveData)
        {
            int i = 0;
            SaveAndRestore.LoadData(ref i, saveData, ref citizen_tax_income_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref citizen_income_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref tourist_income_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref resident_high_landincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref resident_low_landincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref resident_high_eco_landincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref resident_low_eco_landincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref comm_high_landincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref comm_low_landincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref comm_lei_landincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref comm_tou_landincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref comm_eco_landincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref indu_gen_landincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref indu_farmer_landincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref indu_foresty_landincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref indu_oil_landincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref indu_ore_landincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref office_gen_landincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref office_high_tech_landincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref comm_high_tradeincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref comm_low_tradeincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref comm_lei_tradeincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref comm_tou_tradeincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref comm_eco_tradeincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref indu_gen_tradeincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref indu_farmer_tradeincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref indu_foresty_tradeincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref indu_oil_tradeincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref indu_ore_tradeincome_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref road_income_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref playerIndustryIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref garbage_income_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref school_income_forui);
            SaveAndRestore.LoadData(ref i, saveData, ref policeStationIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref healthCareIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref fireStationIncomeForUI);

            if (i != saveData.Length)
            {
                DebugLog.LogToFileOnly($"RealCityEconomyManager Load Error: saveData.Length = {saveData.Length} + i = {i}");
            }
        }

        public static void Save(ref byte[] saveData)
        {
            int i = 0;
            //36 * 4 * 17 = 2448
            SaveAndRestore.SaveData(ref i, citizen_tax_income_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, citizen_income_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, tourist_income_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, resident_high_landincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, resident_low_landincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, resident_high_eco_landincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, resident_low_eco_landincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, comm_high_landincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, comm_low_landincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, comm_lei_landincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, comm_tou_landincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, comm_eco_landincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, indu_gen_landincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, indu_farmer_landincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, indu_foresty_landincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, indu_oil_landincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, indu_ore_landincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, office_gen_landincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, office_high_tech_landincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, comm_high_tradeincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, comm_low_tradeincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, comm_lei_tradeincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, comm_tou_tradeincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, comm_eco_tradeincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, indu_gen_tradeincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, indu_farmer_tradeincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, indu_foresty_tradeincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, indu_oil_tradeincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, indu_ore_tradeincome_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, road_income_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, playerIndustryIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, garbage_income_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, school_income_forui, ref saveData);
            SaveAndRestore.SaveData(ref i, policeStationIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, healthCareIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, fireStationIncomeForUI, ref saveData);

            if (i != saveData.Length)
            {
                DebugLog.LogToFileOnly($"RealCityEconomyManager Save Error: saveData.Length = {saveData.Length} + i = {i}");
            }
        }
    }
}
