namespace RealCity.Util
{
    public class MainDataStore
    {
        public static byte[] watingPathTime = new byte[65536];
        public static ushort[] stuckTime = new ushort[65536];
        public const int gameExpenseDivide = 100;
        public const float playerIndustryBuildingProductionSpeedDiv = 1f;
        public const int reduceCargoDiv = 2;
        public const int reduceCargoDivShift = 1;
        //start from V6, goverment salary is floating now
        public static byte govermentEducation0Salary = 50;
        public static byte govermentEducation1Salary = 55;
        public static byte govermentEducation2Salary = 65;
        public static byte govermentEducation3Salary = 80;
        public const byte govermentEducation0SalaryFixed = 50;
        public const byte govermentEducation1SalaryFixed = 55;
        public const byte govermentEducation2SalaryFixed = 65;
        public const byte govermentEducation3SalaryFixed = 80;
        public static int citizenCount = 0;
        public static int familyCount = 0;
        public static int citizenSalaryPerFamily = 0;
        public static long citizenSalaryTotal = 0;
        public static long citizenSalaryTaxTotal = 0;
        public const ushort residentLowLevel1Rent = 300;
        public const ushort residentLowLevel2Rent = 420;
        public const ushort residentLowLevel3Rent = 570;
        public const ushort residentLowLevel4Rent = 750;
        public const ushort residentLowLevel5Rent = 1050;
        public const ushort residentHighLevel1Rent = 240;
        public const ushort residentHighLevel2Rent = 330;
        public const ushort residentHighLevel3Rent = 450;
        public const ushort residentHighLevel4Rent = 600;
        public const ushort residentHighLevel5Rent = 780;
        public static long citizenExpensePerFamily = 0;
        public static long citizenExpense = 0;
        public static ushort[] vehicleTransferTime = new ushort[65536];
        public static bool[] isVehicleCharged = new bool[65536];
        public static uint totalCitizenDrivingTime = 0;
        public static uint totalCitizenDrivingTimeFinal = 0;
        public static int unfinishedTransitionLost = 0;
        public static long publicTransportFee = 0;
        public static long allTransportFee = 0;
        public static byte citizenAverageTransportFee = 0;
        //1.3 income-expense
        public static float[] family_money = new float[524288];
        public static ushort[] familyGoods = new ushort[524288];

        public static uint family_profit_money_num = 0;
        public static uint family_loss_money_num = 0;
        public static uint family_very_profit_money_num = 0;
        public static uint family_weight_stable_high = 0;
        public static uint family_weight_stable_low = 0;
        //2 Building
        //2.1 building expense
        public const byte comm_high_level1 = 20;
        public const byte comm_high_level2 = 25;
        public const byte comm_high_level3 = 30;
        public const byte comm_low_level1 = 30;
        public const byte comm_low_level2 = 35;
        public const byte comm_low_level3 = 40;
        public const byte comm_tourist = 100;
        public const byte comm_leisure = 50;
        public const byte comm_eco = 45;
        public const byte indu_gen_level1 = 10;
        public const byte indu_gen_level2 = 15;
        public const byte indu_gen_level3 = 20;
        public const byte indu_forest = 10;
        public const byte indu_farm = 15;
        public const byte indu_oil = 20;
        public const byte indu_ore = 20;
        public const byte office_gen_levell = 190;
        public const byte office_gen_level2 = 210;
        public const byte office_gen_level3 = 240;
        public const byte office_high_tech = 255;
        //2.3 buildingMoney 
        public static float[] building_money = new float[49152];
        public static float[] building_money_threat = new float[49152];

        //3 Govement expense
        public static int reserved2 = 0; //old gameExpenseDivide no need to restore now.
        public static int minimumLivingAllowance = 0;
        public static int unfinishedTransitionLostFinal = 0; //old resettlement, RealConstruction Mod replace this function
        public static int minimumLivingAllowanceFinal = 0;
        public static int reserved3 = 0; //old resettlementFinal, RealConstruction Mod replace this function
        public static int PoliceDepartment = 0;
        public static int Education = 0;
        public static int Monument = 0;
        public static int FireDepartment = 0;
        public static int Disaster = 0;
        public static int PublicTransport_bus = 0;
        public static int PublicTransport_tram = 0;
        public static int PublicTransport_ship = 0;
        public static int PublicTransport_train = 0;
        public static int PublicTransport_plane = 0;
        public static int PublicTransport_metro = 0;
        public static int PublicTransport_taxi = 0;
        public static int PublicTransport_cablecar = 0;
        //4 outside connection
        public static byte reserved4 = 0; //no use now
        //other in-game variable
        public static byte update_money_count = 0;
        public static bool reserved10 = false;//no use now
        public static float current_time = 0f;
        public static float prev_time = 0f;
        public static ushort last_buildingid = 0;
        public static ushort lastLineID = 0;
        public static uint last_citizenid = 0;
        public static long reserved5 = 0;//no use now
        public static byte lastLanguage = 255;
        public static bool reserved6 = false;//no use now
        public static bool reserved7 = false;//no use now
        public static ushort reserved8 = 0;//no use now
        public static ushort reserved9 = 0;//no use now
        public static bool reserved32 = false;
        public static bool reserved11 = false;//no use now
        public static bool reserved12 = false;//no use now
        public static bool reserved13 = false;
        public static bool reserved14 = false;
        public static bool reserved15 = false;
        public static bool reserved16 = false;
        public static bool reserved17 = false;
        public static bool reserved18 = false;
        public static bool reserved19 = false;
        public static bool reserved20 = false;
        public static int reserved21 = 0;
        public static int reserved22 = 0;
        public static int reserved23 = 0;
        public static int reserved24 = 0;
        public static ushort reserved25 = 0;
        public static ushort reserved26 = 0;
        public static int[] building_buffer1 = new int[49152];
        public static ushort[] building_buffer2 = new ushort[49152];
        public static ushort[] building_buffer3 = new ushort[49152];
        public static ushort[] building_buffer4 = new ushort[49152];
        public static long cashAmount = 0;
        public static long cashDelta = 0;
        public static bool reserved27 = false;
        public static bool[] isBuildingWorkerUpdated = new bool[49152];
        public static bool[] isBuildingReleased = new bool[49152];
        public static byte[] saveData = new byte[3932402];
        public static float[] citizenMoney = new float[1048576];
        public static byte[] saveData1 = new byte[4194304];
        public static bool[] isCitizenFirstMovingIn = new bool[1048576];
        public static byte[] saveData2 = new byte[1048576];
        public static byte[] saveDataForMoreVehicle = new byte[147456];

        public static void data_init()
        {
            for (int i = 0; i < building_buffer1.Length; i++)
            {
                building_money[i] = 0;
                building_buffer2[i] = 0;
                building_buffer1[i] = 0;
                building_buffer3[i] = 0;
                building_buffer4[i] = 0;
                isBuildingWorkerUpdated[i] = false;
                isBuildingReleased[i] = false;
            }

            for (int i = 0; i < isVehicleCharged.Length; i++)
            {
                vehicleTransferTime[i] = 0;
                isVehicleCharged[i] = false;
            }
            for (int i = 0; i < family_money.Length; i++)
            {
                family_money[i] = 0f;
                familyGoods[i] = 0;
            }
            for (int i = 0; i < citizenMoney.Length; i++)
            {
                citizenMoney[i] = 0f;
                isCitizenFirstMovingIn[i] = false;
            }
        }

        public static void save()
        {
            int i = 0;
            SaveAndRestore.save_long(ref i, citizenExpensePerFamily, ref saveData);
            SaveAndRestore.save_long(ref i, citizenExpense, ref saveData);
            ushort[] vehicleTransferTimeLegacy = new ushort[16384];
            bool[] isVehicleChargedLegacy = new bool[16384];
            //for legacy, other 49152 will save in other place
            for (int j = 0; j < 16384; j++)
            {
                vehicleTransferTimeLegacy[j] = vehicleTransferTime[j];
                isVehicleChargedLegacy[j] = isVehicleCharged[j];
            }
            SaveAndRestore.save_ushorts(ref i, vehicleTransferTimeLegacy, ref saveData);
            SaveAndRestore.save_bools(ref i, isVehicleChargedLegacy, ref saveData);
            SaveAndRestore.save_uint(ref i, totalCitizenDrivingTime, ref saveData);
            SaveAndRestore.save_uint(ref i, totalCitizenDrivingTimeFinal, ref saveData);
            SaveAndRestore.save_int(ref i, unfinishedTransitionLost, ref saveData);
            SaveAndRestore.save_long(ref i, publicTransportFee, ref saveData);
            SaveAndRestore.save_long(ref i, allTransportFee, ref saveData);
            SaveAndRestore.save_floats(ref i, family_money, ref saveData);
            SaveAndRestore.save_ushorts(ref i, familyGoods, ref saveData);
            SaveAndRestore.save_uint(ref i, family_profit_money_num, ref saveData);
            SaveAndRestore.save_uint(ref i, family_loss_money_num, ref saveData);
            SaveAndRestore.save_uint(ref i, family_very_profit_money_num, ref saveData);
            SaveAndRestore.save_uint(ref i, family_weight_stable_high, ref saveData);
            SaveAndRestore.save_uint(ref i, family_weight_stable_low, ref saveData);
            SaveAndRestore.save_floats(ref i, building_money, ref saveData);
            SaveAndRestore.save_int(ref i, reserved2, ref saveData);
            SaveAndRestore.save_int(ref i, minimumLivingAllowance, ref saveData);
            SaveAndRestore.save_int(ref i, unfinishedTransitionLostFinal, ref saveData);
            SaveAndRestore.save_int(ref i, minimumLivingAllowanceFinal, ref saveData);
            SaveAndRestore.save_int(ref i, reserved3, ref saveData);
            SaveAndRestore.save_byte(ref i, govermentEducation0Salary, ref saveData);
            SaveAndRestore.save_byte(ref i, govermentEducation1Salary, ref saveData);
            SaveAndRestore.save_byte(ref i, govermentEducation2Salary, ref saveData);
            SaveAndRestore.save_byte(ref i, govermentEducation3Salary, ref saveData);
            SaveAndRestore.save_int(ref i, PoliceDepartment, ref saveData);
            SaveAndRestore.save_int(ref i, Education, ref saveData);
            SaveAndRestore.save_int(ref i, Monument, ref saveData);
            SaveAndRestore.save_int(ref i, FireDepartment, ref saveData);
            SaveAndRestore.save_int(ref i, PublicTransport_bus, ref saveData);
            SaveAndRestore.save_int(ref i, PublicTransport_tram, ref saveData);
            SaveAndRestore.save_int(ref i, PublicTransport_ship, ref saveData);
            SaveAndRestore.save_int(ref i, PublicTransport_plane, ref saveData);
            SaveAndRestore.save_int(ref i, PublicTransport_metro, ref saveData);
            SaveAndRestore.save_int(ref i, PublicTransport_train, ref saveData);
            SaveAndRestore.save_int(ref i, PublicTransport_taxi, ref saveData);
            SaveAndRestore.save_int(ref i, PublicTransport_cablecar, ref saveData);
            SaveAndRestore.save_int(ref i, Disaster, ref saveData);
            SaveAndRestore.save_byte(ref i, reserved4, ref saveData);
            SaveAndRestore.save_byte(ref i, update_money_count, ref saveData);
            SaveAndRestore.save_bool(ref i, reserved6, ref saveData);
            SaveAndRestore.save_float(ref i, current_time, ref saveData);
            SaveAndRestore.save_float(ref i, prev_time, ref saveData);
            SaveAndRestore.save_int(ref i, citizenCount, ref saveData);
            SaveAndRestore.save_int(ref i, familyCount, ref saveData);
            SaveAndRestore.save_int(ref i, citizenSalaryPerFamily, ref saveData);
            SaveAndRestore.save_long(ref i, citizenSalaryTotal, ref saveData);
            SaveAndRestore.save_long(ref i, citizenSalaryTaxTotal, ref saveData);
            SaveAndRestore.save_long(ref i, reserved5, ref saveData);
            byte tmp = 0;
            SaveAndRestore.save_byte(ref i, tmp, ref saveData);
            SaveAndRestore.save_bool(ref i, reserved7, ref saveData);
            SaveAndRestore.save_bool(ref i, reserved10, ref saveData);
            SaveAndRestore.save_bool(ref i, reserved11, ref saveData);
            SaveAndRestore.save_bool(ref i, reserved12, ref saveData);
            SaveAndRestore.save_bool(ref i, reserved13, ref saveData);
            SaveAndRestore.save_bool(ref i, reserved14, ref saveData);
            SaveAndRestore.save_bool(ref i, reserved15, ref saveData);
            SaveAndRestore.save_bool(ref i, reserved16, ref saveData);
            SaveAndRestore.save_bool(ref i, reserved17, ref saveData);
            SaveAndRestore.save_bool(ref i, reserved18, ref saveData);
            SaveAndRestore.save_int(ref i, reserved21, ref saveData);
            SaveAndRestore.save_int(ref i, reserved22, ref saveData);
            SaveAndRestore.save_int(ref i, reserved23, ref saveData);
            SaveAndRestore.save_int(ref i, reserved24, ref saveData);
            SaveAndRestore.save_ushort(ref i, reserved25, ref saveData);
            SaveAndRestore.save_ushort(ref i, reserved26, ref saveData);
            SaveAndRestore.save_ushort(ref i, reserved8, ref saveData);
            SaveAndRestore.save_ushort(ref i, reserved9, ref saveData);
            SaveAndRestore.save_bool(ref i, reserved32, ref saveData);
            SaveAndRestore.save_bool(ref i, reserved11, ref saveData);
            SaveAndRestore.save_bool(ref i, reserved12, ref saveData);
            SaveAndRestore.save_ints(ref i, building_buffer1, ref saveData);
            SaveAndRestore.save_ushorts(ref i, building_buffer2, ref saveData);
            SaveAndRestore.save_ushorts(ref i, building_buffer3, ref saveData);
            SaveAndRestore.save_ushorts(ref i, building_buffer4, ref saveData);
            SaveAndRestore.save_long(ref i, cashAmount, ref saveData);
            SaveAndRestore.save_long(ref i, cashDelta, ref saveData);
            SaveAndRestore.save_bool(ref i, reserved27, ref saveData);
            SaveAndRestore.save_bools(ref i, isBuildingWorkerUpdated, ref saveData);
            DebugLog.LogToFileOnly("(save)saveData in comm_data is " + i.ToString());
            i = 0;
            SaveAndRestore.save_floats(ref i, citizenMoney, ref saveData1);
            i = 0;
            SaveAndRestore.save_bools(ref i, isCitizenFirstMovingIn, ref saveData2);
            i = 0;
            ushort[] vehicleTransferTimeMoreVehicle = new ushort[49152];
            bool[] isVehicleChargedMoreVehicle = new bool[49152];
            for (int j = 0; j < 49152; j++)
            {
                vehicleTransferTimeMoreVehicle[j] = vehicleTransferTime[j + 16384];
                isVehicleChargedMoreVehicle[j] = isVehicleCharged[j + 16384];
            }
            SaveAndRestore.save_ushorts(ref i, vehicleTransferTimeMoreVehicle, ref saveDataForMoreVehicle);
            SaveAndRestore.save_bools(ref i, isVehicleChargedMoreVehicle, ref saveDataForMoreVehicle);
        }

        public static void load()
        {
            int i = 0;
            citizenExpensePerFamily = SaveAndRestore.load_long(ref i, saveData);
            citizenExpense = SaveAndRestore.load_long(ref i, saveData);
            //for legacy, other 49152 will loadedin other place
            ushort[] vehicleTransferTimeLegacy = new ushort[16384];
            bool[] isVehicleChargedLegacy = new bool[16384];
            vehicleTransferTimeLegacy = SaveAndRestore.load_ushorts(ref i, saveData, vehicleTransferTimeLegacy.Length);
            isVehicleChargedLegacy = SaveAndRestore.load_bools(ref i, saveData, isVehicleChargedLegacy.Length);
            for (int j = 0; j < 16384; j++)
            {
                vehicleTransferTime[j] = vehicleTransferTimeLegacy[j];
                isVehicleCharged[j] = isVehicleChargedLegacy[j];
            }
            totalCitizenDrivingTime = SaveAndRestore.load_uint(ref i, saveData);
            totalCitizenDrivingTimeFinal = SaveAndRestore.load_uint(ref i, saveData);
            unfinishedTransitionLost = SaveAndRestore.load_int(ref i, saveData);
            publicTransportFee = SaveAndRestore.load_long(ref i, saveData);
            allTransportFee = SaveAndRestore.load_long(ref i, saveData);
            family_money = SaveAndRestore.load_floats(ref i, saveData, family_money.Length);
            familyGoods = SaveAndRestore.load_ushorts(ref i, saveData, familyGoods.Length);
            family_profit_money_num = SaveAndRestore.load_uint(ref i, saveData);
            family_loss_money_num = SaveAndRestore.load_uint(ref i, saveData);
            family_very_profit_money_num = SaveAndRestore.load_uint(ref i, saveData);
            family_weight_stable_high = SaveAndRestore.load_uint(ref i, saveData);
            family_weight_stable_low = SaveAndRestore.load_uint(ref i, saveData);
            building_money = SaveAndRestore.load_floats(ref i, saveData, building_money.Length);
            SaveAndRestore.load_int(ref i, saveData);
            minimumLivingAllowance = SaveAndRestore.load_int(ref i, saveData);
            unfinishedTransitionLostFinal = SaveAndRestore.load_int(ref i, saveData);
            minimumLivingAllowanceFinal = SaveAndRestore.load_int(ref i, saveData);
            SaveAndRestore.load_int(ref i, saveData);
            govermentEducation0Salary = SaveAndRestore.load_byte(ref i, saveData);
            govermentEducation1Salary = SaveAndRestore.load_byte(ref i, saveData);
            govermentEducation2Salary = SaveAndRestore.load_byte(ref i, saveData);
            govermentEducation3Salary = SaveAndRestore.load_byte(ref i, saveData);
            PoliceDepartment = SaveAndRestore.load_int(ref i, saveData);
            Education = SaveAndRestore.load_int(ref i, saveData);
            Monument = SaveAndRestore.load_int(ref i, saveData);
            FireDepartment = SaveAndRestore.load_int(ref i, saveData);
            PublicTransport_bus = SaveAndRestore.load_int(ref i, saveData);
            PublicTransport_tram = SaveAndRestore.load_int(ref i, saveData);
            PublicTransport_ship = SaveAndRestore.load_int(ref i, saveData);
            PublicTransport_plane = SaveAndRestore.load_int(ref i, saveData);
            PublicTransport_metro = SaveAndRestore.load_int(ref i, saveData);
            PublicTransport_train = SaveAndRestore.load_int(ref i, saveData);
            PublicTransport_taxi = SaveAndRestore.load_int(ref i, saveData);
            PublicTransport_cablecar = SaveAndRestore.load_int(ref i, saveData);
            Disaster = SaveAndRestore.load_int(ref i, saveData);
            SaveAndRestore.load_byte(ref i, saveData);
            update_money_count = SaveAndRestore.load_byte(ref i, saveData);
            SaveAndRestore.load_bool(ref i, saveData);
            current_time = SaveAndRestore.load_float(ref i, saveData);
            prev_time = SaveAndRestore.load_float(ref i, saveData);
            citizenCount = SaveAndRestore.load_int(ref i, saveData);
            familyCount = SaveAndRestore.load_int(ref i, saveData);
            citizenSalaryPerFamily = SaveAndRestore.load_int(ref i, saveData);
            citizenSalaryTotal = SaveAndRestore.load_long(ref i, saveData);
            citizenSalaryTaxTotal = SaveAndRestore.load_long(ref i, saveData);
            SaveAndRestore.load_long(ref i, saveData);
            SaveAndRestore.load_byte(ref i, saveData);
            SaveAndRestore.load_bool(ref i, saveData);
            SaveAndRestore.load_bool(ref i, saveData);
            SaveAndRestore.load_bool(ref i, saveData);
            SaveAndRestore.load_bool(ref i, saveData);
            SaveAndRestore.load_bool(ref i, saveData);
            SaveAndRestore.load_bool(ref i, saveData);
            SaveAndRestore.load_bool(ref i, saveData);
            SaveAndRestore.load_bool(ref i, saveData);
            SaveAndRestore.load_bool(ref i, saveData);
            SaveAndRestore.load_bool(ref i, saveData);
            SaveAndRestore.load_int(ref i, saveData);
            SaveAndRestore.load_int(ref i, saveData);
            SaveAndRestore.load_int(ref i, saveData);
            SaveAndRestore.load_int(ref i, saveData);
            SaveAndRestore.load_ushort(ref i, saveData);
            SaveAndRestore.load_ushort(ref i, saveData);
            SaveAndRestore.load_ushort(ref i, saveData);
            SaveAndRestore.load_ushort(ref i, saveData);
            reserved32 = SaveAndRestore.load_bool(ref i, saveData);
            SaveAndRestore.load_bool(ref i, saveData);
            SaveAndRestore.load_bool(ref i, saveData);
            building_buffer1 = SaveAndRestore.load_ints(ref i, saveData, building_buffer1.Length);
            building_buffer2 = SaveAndRestore.load_ushorts(ref i, saveData, building_buffer2.Length);
            building_buffer3 = SaveAndRestore.load_ushorts(ref i, saveData, building_buffer3.Length);
            building_buffer4 = SaveAndRestore.load_ushorts(ref i, saveData, building_buffer4.Length);
            cashAmount = SaveAndRestore.load_long(ref i, saveData);
            cashDelta = SaveAndRestore.load_long(ref i, saveData);
            SaveAndRestore.load_bool(ref i, saveData);
            isBuildingWorkerUpdated = SaveAndRestore.load_bools(ref i, saveData, isBuildingWorkerUpdated.Length);
            DebugLog.LogToFileOnly("saveData in MainDataStore is " + i.ToString());
        }

        public static void load1()
        {
            int i = 0;
            citizenMoney = SaveAndRestore.load_floats(ref i, saveData1, citizenMoney.Length);
            DebugLog.LogToFileOnly("saveData1 in MainDataStore is " + i.ToString());
        }

        public static void load2()
        {
            int i = 0;
            isCitizenFirstMovingIn = SaveAndRestore.load_bools(ref i, saveData2, isCitizenFirstMovingIn.Length);
            DebugLog.LogToFileOnly("saveData2 in MainDataStore is " + i.ToString());
        }

        public static void loadForMoreVehicle()
        {
            int i = 0;
            ushort[] vehicleTransferTimeMoreVehicle = new ushort[49152];
            bool[] isVehicleChargedMoreVehicle = new bool[49152];
            vehicleTransferTimeMoreVehicle = SaveAndRestore.load_ushorts(ref i, saveDataForMoreVehicle, vehicleTransferTimeMoreVehicle.Length);
            isVehicleChargedMoreVehicle = SaveAndRestore.load_bools(ref i, saveDataForMoreVehicle, isVehicleChargedMoreVehicle.Length);
            for (int j = 0; j < 49152; j++)
            {
                vehicleTransferTime[j + 16384] = vehicleTransferTimeMoreVehicle[j];
                isVehicleCharged[j + 16384] = isVehicleChargedMoreVehicle[j];
            }
            DebugLog.LogToFileOnly("saveData3 in MainDataStore is " + i.ToString());
        }
    }
}
