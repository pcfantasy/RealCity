using RealCity.Util;

namespace RealCity.CustomManager
{
    public class RealCityEconomyManager
    {

        public static int[] citizenTaxIncomeForUI = new int[17];
        public static int[] citizenIncomeForUI = new int[17];
        public static int[] touristIncomeForUI = new int[17];
        //land income
        public static int[] residentHighLandIncomeForUI = new int[17];
        public static int[] residentLowLandIncomeForUI = new int[17];
        public static int[] residentHighEcoLandIncomeForUI = new int[17];
        public static int[] residentLowEcoLandIncomeForUI = new int[17];
        public static int[] commHighLandIncomeForUI = new int[17];
        public static int[] commLowLandIncomeForUI = new int[17];
        public static int[] commLeiLandIncomeForUI = new int[17];
        public static int[] commTouLandIncomeForUI = new int[17];
        public static int[] commEcoLandIncomeForUI = new int[17];
        public static int[] induGenLandIncomeForUI = new int[17];
        public static int[] induFarmerLandIncomeForUI = new int[17];
        public static int[] induForestyLandIncomeForUI = new int[17];
        public static int[] induOilLandIncomeForUI = new int[17];
        public static int[] induOreLandIncomeForUI = new int[17];
        public static int[] officeGenLandIncomeForUI = new int[17];
        public static int[] officeHighTechLandIncomeForUI = new int[17];
        //trade income
        public static int[] commHighTradeIncomeForUI = new int[17];
        public static int[] commLowTradeIncomeForUI = new int[17];
        public static int[] commLeiTradeIncomeForUI = new int[17];
        public static int[] commTouTradeIncomeForUI = new int[17];
        public static int[] commEcoTradeIncomeForUI = new int[17];
        public static int[] induGenTradeIncomeForUI = new int[17];
        public static int[] induFarmerTradeIncomeForUI = new int[17];
        public static int[] induForestyTradeIncomeForUI = new int[17];
        public static int[] induOilTradeIncomeForUI = new int[17];
        public static int[] induOreTradeIncomeForUI = new int[17];
        //govement income
        public static int[] garbageIncomeForUI = new int[17];
        public static int[] roadIncomeForUI = new int[17];
        public static int[] playerIndustryIncomeForUI = new int[17];
        public static int[] schoolIncomeForUI = new int[17];
        public static int[] policeStationIncomeForUI = new int[17];
        public static int[] healthCareIncomeForUI = new int[17];
        public static int[] fireStationIncomeForUI = new int[17];

        public static void CleanCurrent(int current_idex) {
            citizenTaxIncomeForUI[current_idex] = 0;
            citizenIncomeForUI[current_idex] = 0;
            touristIncomeForUI[current_idex] = 0;
            residentHighLandIncomeForUI[current_idex] = 0;
            residentLowLandIncomeForUI[current_idex] = 0;
            residentHighEcoLandIncomeForUI[current_idex] = 0;
            residentLowEcoLandIncomeForUI[current_idex] = 0;
            commHighLandIncomeForUI[current_idex] = 0;
            commLowLandIncomeForUI[current_idex] = 0;
            commLeiLandIncomeForUI[current_idex] = 0;
            commTouLandIncomeForUI[current_idex] = 0;
            commEcoLandIncomeForUI[current_idex] = 0;
            induGenLandIncomeForUI[current_idex] = 0;
            induFarmerLandIncomeForUI[current_idex] = 0;
            induForestyLandIncomeForUI[current_idex] = 0;
            induOilLandIncomeForUI[current_idex] = 0;
            induOreLandIncomeForUI[current_idex] = 0;
            officeGenLandIncomeForUI[current_idex] = 0;
            officeHighTechLandIncomeForUI[current_idex] = 0;
            commHighTradeIncomeForUI[current_idex] = 0;
            commLowTradeIncomeForUI[current_idex] = 0;
            commLeiTradeIncomeForUI[current_idex] = 0;
            commTouTradeIncomeForUI[current_idex] = 0;
            commEcoTradeIncomeForUI[current_idex] = 0;
            induGenTradeIncomeForUI[current_idex] = 0;
            induFarmerTradeIncomeForUI[current_idex] = 0;
            induForestyTradeIncomeForUI[current_idex] = 0;
            induOilTradeIncomeForUI[current_idex] = 0;
            induOreTradeIncomeForUI[current_idex] = 0;
            garbageIncomeForUI[current_idex] = 0;
            playerIndustryIncomeForUI[current_idex] = 0;
            roadIncomeForUI[current_idex] = 0;
            schoolIncomeForUI[current_idex] = 0;
            policeStationIncomeForUI[current_idex] = 0;
            fireStationIncomeForUI[current_idex] = 0;
            healthCareIncomeForUI[current_idex] = 0;
        }

        public static void DataInit() {
            for (int i = 0; i < citizenTaxIncomeForUI.Length; i++) {
                citizenTaxIncomeForUI[i] = 0;
                citizenIncomeForUI[i] = 0;
                touristIncomeForUI[i] = 0;
                residentHighLandIncomeForUI[i] = 0;
                residentLowLandIncomeForUI[i] = 0;
                residentHighEcoLandIncomeForUI[i] = 0;
                residentLowEcoLandIncomeForUI[i] = 0;
                commHighLandIncomeForUI[i] = 0;
                commLowLandIncomeForUI[i] = 0;
                commLeiLandIncomeForUI[i] = 0;
                commTouLandIncomeForUI[i] = 0;
                commEcoLandIncomeForUI[i] = 0;
                induGenLandIncomeForUI[i] = 0;
                induFarmerLandIncomeForUI[i] = 0;
                induForestyLandIncomeForUI[i] = 0;
                induOilLandIncomeForUI[i] = 0;
                induOreLandIncomeForUI[i] = 0;
                officeGenLandIncomeForUI[i] = 0;
                officeHighTechLandIncomeForUI[i] = 0;
                commHighTradeIncomeForUI[i] = 0;
                commLowTradeIncomeForUI[i] = 0;
                commLeiTradeIncomeForUI[i] = 0;
                commTouTradeIncomeForUI[i] = 0;
                commEcoTradeIncomeForUI[i] = 0;
                induGenTradeIncomeForUI[i] = 0;
                induFarmerTradeIncomeForUI[i] = 0;
                induForestyTradeIncomeForUI[i] = 0;
                induOilTradeIncomeForUI[i] = 0;
                induOreTradeIncomeForUI[i] = 0;
                roadIncomeForUI[i] = 0;
                playerIndustryIncomeForUI[i] = 0;
                garbageIncomeForUI[i] = 0;
                schoolIncomeForUI[i] = 0;
                policeStationIncomeForUI[i] = 0;
                fireStationIncomeForUI[i] = 0;
                healthCareIncomeForUI[i] = 0;
            }
        }

        public static void Load(ref byte[] saveData) {
            int i = 0;
            SaveAndRestore.LoadData(ref i, saveData, ref citizenTaxIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref citizenIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref touristIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref residentHighLandIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref residentLowLandIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref residentHighEcoLandIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref residentLowEcoLandIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref commHighLandIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref commLowLandIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref commLeiLandIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref commTouLandIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref commEcoLandIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref induGenLandIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref induFarmerLandIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref induForestyLandIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref induOilLandIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref induOreLandIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref officeGenLandIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref officeHighTechLandIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref commHighTradeIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref commLowTradeIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref commLeiTradeIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref commTouTradeIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref commEcoTradeIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref induGenTradeIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref induFarmerTradeIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref induForestyTradeIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref induOilTradeIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref induOreTradeIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref roadIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref playerIndustryIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref garbageIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref schoolIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref policeStationIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref healthCareIncomeForUI);
            SaveAndRestore.LoadData(ref i, saveData, ref fireStationIncomeForUI);

            if (i != saveData.Length) {
                DebugLog.LogToFileOnly($"RealCityEconomyManager Load Error: saveData.Length = {saveData.Length} + i = {i}");
            }
        }

        public static void Save(ref byte[] saveData) {
            int i = 0;
            //36 * 4 * 17 = 2448
            SaveAndRestore.SaveData(ref i, citizenTaxIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, citizenIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, touristIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, residentHighLandIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, residentLowLandIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, residentHighEcoLandIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, residentLowEcoLandIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, commHighLandIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, commLowLandIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, commLeiLandIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, commTouLandIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, commEcoLandIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, induGenLandIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, induFarmerLandIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, induForestyLandIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, induOilLandIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, induOreLandIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, officeGenLandIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, officeHighTechLandIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, commHighTradeIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, commLowTradeIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, commLeiTradeIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, commTouTradeIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, commEcoTradeIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, induGenTradeIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, induFarmerTradeIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, induForestyTradeIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, induOilTradeIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, induOreTradeIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, roadIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, playerIndustryIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, garbageIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, schoolIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, policeStationIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, healthCareIncomeForUI, ref saveData);
            SaveAndRestore.SaveData(ref i, fireStationIncomeForUI, ref saveData);

            if (i != saveData.Length) {
                DebugLog.LogToFileOnly($"RealCityEconomyManager Save Error: saveData.Length = {saveData.Length} + i = {i}");
            }
        }
    }
}