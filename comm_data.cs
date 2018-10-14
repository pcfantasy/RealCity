namespace RealCity
{
    public class comm_data
    {

        public const byte game_expense_divide = 100;
        //1.  citizen
        //1.1 citizen salary
        //Goverment
        public const byte goverment_education0 = 40;
        public const byte goverment_education1 = 45;
        public const byte goverment_education2 = 55;
        public const byte goverment_education3 = 70;

        public static int citizen_count = 0;
        public static int family_count = 0;
        public static int citizen_salary_per_family = 0;
        public static long citizen_salary_total = 0;
        public static long citizen_salary_tax_total = 0;

        //1.2 citizen expense
        public const ushort resident_low_level1_rent = 300;
        public const ushort resident_low_level2_rent = 420;
        public const ushort resident_low_level3_rent = 570;
        public const ushort resident_low_level4_rent = 750;
        public const ushort resident_low_level5_rent = 1050;
        public const ushort resident_low_eco_level1_rent = 330;
        public const ushort resident_low_eco_level2_rent = 450;
        public const ushort resident_low_eco_level3_rent = 600;
        public const ushort resident_low_eco_level4_rent = 780;
        public const ushort resident_low_eco_level5_rent = 1080;
        public const ushort resident_high_level1_rent = 240;
        public const ushort resident_high_level2_rent = 330;
        public const ushort resident_high_level3_rent = 450;
        public const ushort resident_high_level4_rent = 600;
        public const ushort resident_high_level5_rent = 780;
        public const ushort resident_high_eco_level1_rent = 270;
        public const ushort resident_high_eco_level2_rent = 360;
        public const ushort resident_high_eco_level3_rent = 480;
        public const ushort resident_high_eco_level4_rent = 630;
        public const ushort resident_high_eco_level5_rent = 810;
        //1.2.1 citizen expense
        public static long citizen_expense_per_family = 0;
        public static long citizen_expense = 0;
        //1.2.2 transport fee  position.x unit(0.9m),in game, max distance (in x) is 18000m
        public static ushort[] vehical_transfer_time = new ushort[16384];
        public static bool[] vehical_flag = new bool[16384];
        public static uint temp_total_citizen_vehical_time = 0;//temp use
        public static uint temp_total_citizen_vehical_time_last = 0;//temp use
        public static uint total_citizen_vehical_time = 0;
        public static long public_transport_fee = 0;
        public static long all_transport_fee = 0;
        public static byte citizen_average_transport_fee = 0;

        //1.3 income-expense
        public static float[] family_money = new float[524288];
        public static byte[] family_profit_status = new byte[524288];
        public static uint family_profit_money_num = 0;
        public static uint family_loss_money_num = 0;
        public static uint family_very_profit_money_num = 0;
        public static uint family_weight_stable_high = 0;
        public static uint family_weight_stable_low = 0;


        //2 building
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


        //2.3 process building 
        public static float[] building_money = new float[49152];
        //move to buildingAI



        //3 govement expense
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
        public static byte outside_situation_index = 0;
        //other in-game variable
        public static byte update_money_count = 0;
        public static bool is_updated = false;
        public static float current_time = 0f;
        public static float prev_time = 0f;
        public static ushort last_buildingid = 0;
        public static uint last_citizenid = 0;

        public static long temp_public_transport_fee = 0;
        public static byte last_language = 255;

        public static bool isSmartPbtp = false;
        public static bool isHellMode = false;

        public static ushort Extractor_building = 0;
        public static ushort Extractor_building_final = 0;

        //public static ushort tourist_num = 0;
        //public static ushort tourist_num_final = 0;

        //public static long tourist_transport_fee_num = 0;
        //public static long tourist_transport_fee_num_final = 0;

        public static bool is_weekend = false;
        public static bool have_toll_station = false;
        public static bool have_city_resource_department = false;

        public static bool isFoodsGetted = false;
        public static bool isCoalsGetted = false;
        public static bool isLumbersGetted = false;
        public static bool isPetrolsGetted = false;
        public static bool isFoodsGettedFinal = false;
        public static bool isCoalsGettedFinal = false;
        public static bool isLumbersGettedFinal = false;
        public static bool isPetrolsGettedFinal = false;
        public static int allFoods = 0;
        public static int allLumbers = 0;
        public static int allPetrols = 0;
        public static int allCoals = 0;
        public static ushort allVehicles = 0;
        public static ushort allVehiclesFinal = 0;

        // reserved some for futher used
        public static ushort[] building_buffer1 = new ushort[49152];
        public static ushort[] building_buffer2 = new ushort[49152];
        public static ushort[] building_buffer3 = new ushort[49152];
        public static ushort[] building_buffer4 = new ushort[49152];

        //public static byte[] save_data = new byte[2867364];
        public static byte[] save_data = new byte[3260641];
        public static float[] citizen_money = new float[1048576];
        public static byte[] save_data1 = new byte[4194304];
        //public static byte[] save_data = new byte[3063935];


        public static void data_init()
        {
            for (int i = 0; i < comm_data.building_money.Length; i++)
            {
                building_money[i] = 0;
                building_buffer2[i] = 0;
            }
            for (int i = 0; i < comm_data.building_buffer1.Length; i++)
            {
                building_buffer1[i] = 0;
                building_buffer3[i] = 0;
                building_buffer4[i] = 0;
            }
            for (int i = 0; i < comm_data.vehical_transfer_time.Length; i++)
            {
                vehical_transfer_time[i] = 0;
            }
            for (int i = 0; i < comm_data.vehical_flag.Length; i++)
            {
                vehical_flag[i] = false;
            }
            for (int i = 0; i < comm_data.family_money.Length; i++)
            {
                family_money[i] = 0f;
                family_profit_status[i] = 128;
            }
            for (int i = 0; i < comm_data.citizen_money.Length; i++)
            {
                citizen_money[i] = 0f;
            }
        }

        public static void save()
        {
            int i = 0;

            // 2*8 + 2*16384 + 16384 + 3*4 + 2*8 = 49196
            saveandrestore.save_long(ref i, citizen_expense_per_family, ref save_data);
            saveandrestore.save_long(ref i, citizen_expense, ref save_data);
            saveandrestore.save_ushorts(ref i, vehical_transfer_time, ref save_data);
            saveandrestore.save_bools(ref i, vehical_flag, ref save_data);
            saveandrestore.save_uint(ref i, temp_total_citizen_vehical_time, ref save_data);
            saveandrestore.save_uint(ref i, temp_total_citizen_vehical_time_last, ref save_data);
            saveandrestore.save_uint(ref i, total_citizen_vehical_time, ref save_data);
            saveandrestore.save_long(ref i, public_transport_fee, ref save_data);
            saveandrestore.save_long(ref i, all_transport_fee, ref save_data);

            // (4+1)*524288 = 2621440       // 2670636
            saveandrestore.save_floats(ref i, family_money, ref save_data);
            //saveandrestore.save_bytes(ref i, citizen_very_profit_time_num, ref save_data);
            saveandrestore.save_bytes(ref i, family_profit_status, ref save_data);
            //saveandrestore.save_bytes(ref i, citizen_loss_time_num, ref save_data);

            //5*4 = 20      //2670656
            saveandrestore.save_uint(ref i, family_profit_money_num, ref save_data);
            saveandrestore.save_uint(ref i, family_loss_money_num, ref save_data);
            saveandrestore.save_uint(ref i, family_very_profit_money_num, ref save_data);
            saveandrestore.save_uint(ref i, family_weight_stable_high, ref save_data);
            saveandrestore.save_uint(ref i, family_weight_stable_low, ref save_data);

            //49152*2 = 196608  // 2867264
            saveandrestore.save_floats(ref i, building_money, ref save_data);

            //20*4 = 80    //2867344
            saveandrestore.save_int(ref i, Road, ref save_data);
            saveandrestore.save_int(ref i, Electricity, ref save_data);
            saveandrestore.save_int(ref i, Water, ref save_data);
            saveandrestore.save_int(ref i, Beautification, ref save_data);
            saveandrestore.save_int(ref i, Garbage, ref save_data);
            saveandrestore.save_int(ref i, HealthCare, ref save_data);
            saveandrestore.save_int(ref i, PoliceDepartment, ref save_data);
            saveandrestore.save_int(ref i, Education, ref save_data);
            saveandrestore.save_int(ref i, Monument, ref save_data);
            saveandrestore.save_int(ref i, FireDepartment, ref save_data);
            saveandrestore.save_int(ref i, PublicTransport_bus, ref save_data);
            saveandrestore.save_int(ref i, PublicTransport_tram, ref save_data);
            saveandrestore.save_int(ref i, PublicTransport_ship, ref save_data);
            saveandrestore.save_int(ref i, PublicTransport_plane, ref save_data);
            saveandrestore.save_int(ref i, PublicTransport_metro, ref save_data);
            saveandrestore.save_int(ref i, PublicTransport_train, ref save_data);
            saveandrestore.save_int(ref i, PublicTransport_taxi, ref save_data);
            saveandrestore.save_int(ref i, PublicTransport_cablecar, ref save_data);
            saveandrestore.save_int(ref i, Disaster, ref save_data);

            //11      //2867355
            saveandrestore.save_byte(ref i, outside_situation_index, ref save_data);
            saveandrestore.save_byte(ref i, update_money_count, ref save_data);
            saveandrestore.save_bool(ref i, is_updated, ref save_data);
            saveandrestore.save_float(ref i, current_time, ref save_data);
            saveandrestore.save_float(ref i, prev_time, ref save_data);

            //28       //2867397
            saveandrestore.save_int(ref i, citizen_count, ref save_data);
            saveandrestore.save_int(ref i, family_count, ref save_data);
            saveandrestore.save_int(ref i, citizen_salary_per_family, ref save_data);
            saveandrestore.save_long(ref i, citizen_salary_total, ref save_data);
            saveandrestore.save_long(ref i, citizen_salary_tax_total, ref save_data);

            //9        //2867406
            saveandrestore.save_long(ref i, temp_public_transport_fee, ref save_data);
            saveandrestore.save_byte(ref i, last_language, ref save_data);

            //32 + 2 + 7 + 16 + 4 = 61     //2867467

            saveandrestore.save_bool(ref i, isSmartPbtp, ref save_data);
            saveandrestore.save_bool(ref i, isHellMode, ref save_data);
            saveandrestore.save_bool(ref i, isCoalsGetted, ref save_data);
            saveandrestore.save_bool(ref i, isFoodsGetted, ref save_data);
            saveandrestore.save_bool(ref i, isLumbersGetted, ref save_data);
            saveandrestore.save_bool(ref i, isPetrolsGetted, ref save_data);
            saveandrestore.save_bool(ref i, isCoalsGettedFinal, ref save_data);
            saveandrestore.save_bool(ref i, isFoodsGettedFinal, ref save_data);
            saveandrestore.save_bool(ref i, isLumbersGettedFinal, ref save_data);
            saveandrestore.save_bool(ref i, isPetrolsGettedFinal, ref save_data);
            saveandrestore.save_int(ref i, allCoals, ref save_data);
            saveandrestore.save_int(ref i, allFoods, ref save_data);
            saveandrestore.save_int(ref i, allLumbers, ref save_data);
            saveandrestore.save_int(ref i, allPetrols, ref save_data);
            saveandrestore.save_ushort(ref i, allVehicles, ref save_data);
            saveandrestore.save_ushort(ref i, allVehiclesFinal, ref save_data);


            // 16 + 4 + 4 + 16 + 1 + 12 + 26 = 79    //2867546
            saveandrestore.save_ushort(ref i, Extractor_building, ref save_data);
            saveandrestore.save_ushort(ref i, Extractor_building_final, ref save_data);

            //saveandrestore.save_ushort(ref i, tourist_num, ref save_data);
            //saveandrestore.save_ushort(ref i, tourist_num_final, ref save_data);

            //saveandrestore.save_long(ref i, tourist_transport_fee_num, ref save_data);
            //saveandrestore.save_long(ref i, tourist_transport_fee_num_final, ref save_data);


            saveandrestore.save_bool(ref i, is_weekend, ref save_data);



            // 4 + 8 + 5 + 2 + 8 =27
            saveandrestore.save_bool(ref i, have_toll_station, ref save_data);
            saveandrestore.save_bool(ref i, have_city_resource_department, ref save_data);
            saveandrestore.save_ushorts(ref i, building_buffer1, ref save_data);
            saveandrestore.save_ushorts(ref i, building_buffer2, ref save_data);
            saveandrestore.save_ushorts(ref i, building_buffer3, ref save_data);
            saveandrestore.save_ushorts(ref i, building_buffer4, ref save_data);


            DebugLog.LogToFileOnly("(save)save_data in comm_data is " + i.ToString());

            i = 0;
            saveandrestore.save_floats(ref i, citizen_money, ref save_data1);
        }

        public static void load()
        {
            int i = 0;

            citizen_expense_per_family = saveandrestore.load_long(ref i, save_data);
            citizen_expense = saveandrestore.load_long(ref i, save_data);
            vehical_transfer_time = saveandrestore.load_ushorts(ref i, save_data, vehical_transfer_time.Length);
            vehical_flag = saveandrestore.load_bools(ref i, save_data, vehical_flag.Length);
            temp_total_citizen_vehical_time = saveandrestore.load_uint(ref i, save_data);
            temp_total_citizen_vehical_time_last = saveandrestore.load_uint(ref i, save_data);
            total_citizen_vehical_time = saveandrestore.load_uint(ref i, save_data);
            public_transport_fee = saveandrestore.load_long(ref i, save_data);
            all_transport_fee = saveandrestore.load_long(ref i, save_data);
            family_money = saveandrestore.load_floats(ref i, save_data, family_money.Length);
            //citizen_very_profit_time_num = saveandrestore.load_bytes(ref i, save_data, citizen_very_profit_time_num.Length);
            //citizen_loss_time_num = saveandrestore.load_bytes(ref i, save_data, citizen_loss_time_num.Length);
            family_profit_status = saveandrestore.load_bytes(ref i, save_data, family_profit_status.Length);
            family_profit_money_num = saveandrestore.load_uint(ref i, save_data);
            family_loss_money_num = saveandrestore.load_uint(ref i, save_data);
            family_very_profit_money_num = saveandrestore.load_uint(ref i, save_data);
            family_weight_stable_high = saveandrestore.load_uint(ref i, save_data);
            family_weight_stable_low = saveandrestore.load_uint(ref i, save_data);

            building_money = saveandrestore.load_floats(ref i, save_data, building_money.Length);

            Road = saveandrestore.load_int(ref i, save_data);
            Electricity = saveandrestore.load_int(ref i, save_data);
            Water = saveandrestore.load_int(ref i, save_data);
            Beautification = saveandrestore.load_int(ref i, save_data);
            Garbage = saveandrestore.load_int(ref i, save_data);
            HealthCare = saveandrestore.load_int(ref i, save_data);
            PoliceDepartment = saveandrestore.load_int(ref i, save_data);
            Education = saveandrestore.load_int(ref i, save_data);
            Monument = saveandrestore.load_int(ref i, save_data);
            FireDepartment = saveandrestore.load_int(ref i, save_data);
            PublicTransport_bus = saveandrestore.load_int(ref i, save_data);
            PublicTransport_tram = saveandrestore.load_int(ref i, save_data);
            PublicTransport_ship = saveandrestore.load_int(ref i, save_data);
            PublicTransport_plane = saveandrestore.load_int(ref i, save_data);
            PublicTransport_metro = saveandrestore.load_int(ref i, save_data);
            PublicTransport_train = saveandrestore.load_int(ref i, save_data);
            PublicTransport_taxi = saveandrestore.load_int(ref i, save_data);
            PublicTransport_cablecar = saveandrestore.load_int(ref i, save_data);
            Disaster = saveandrestore.load_int(ref i, save_data);

            outside_situation_index = saveandrestore.load_byte(ref i, save_data);
            update_money_count = saveandrestore.load_byte(ref i, save_data);
            is_updated = saveandrestore.load_bool(ref i, save_data);
            is_updated = false;

            current_time = saveandrestore.load_float(ref i, save_data);
            prev_time = saveandrestore.load_float(ref i, save_data);

            citizen_count = saveandrestore.load_int(ref i, save_data);
            family_count = saveandrestore.load_int(ref i, save_data);
            citizen_salary_per_family = saveandrestore.load_int(ref i, save_data);
            citizen_salary_total = saveandrestore.load_long(ref i, save_data);
            citizen_salary_tax_total = saveandrestore.load_long(ref i, save_data);
            temp_public_transport_fee = saveandrestore.load_long(ref i, save_data);
            last_language = saveandrestore.load_byte(ref i, save_data);

            isSmartPbtp = saveandrestore.load_bool(ref i, save_data);
            isHellMode = saveandrestore.load_bool(ref i, save_data);
            isCoalsGetted = saveandrestore.load_bool(ref i, save_data);
            isFoodsGetted = saveandrestore.load_bool(ref i, save_data);
            isLumbersGetted = saveandrestore.load_bool(ref i, save_data);
            isPetrolsGetted = saveandrestore.load_bool(ref i, save_data);
            isCoalsGettedFinal = saveandrestore.load_bool(ref i, save_data);
            isFoodsGettedFinal = saveandrestore.load_bool(ref i, save_data);
            isLumbersGettedFinal = saveandrestore.load_bool(ref i, save_data);
            isPetrolsGettedFinal = saveandrestore.load_bool(ref i, save_data);
            allCoals = saveandrestore.load_int(ref i, save_data);
            allFoods = saveandrestore.load_int(ref i, save_data);
            allLumbers = saveandrestore.load_int(ref i, save_data);
            allPetrols = saveandrestore.load_int(ref i, save_data);
            allVehicles = saveandrestore.load_ushort(ref i, save_data);
            allVehiclesFinal = saveandrestore.load_ushort(ref i, save_data);


            Extractor_building = saveandrestore.load_ushort(ref i, save_data);
            Extractor_building_final = saveandrestore.load_ushort(ref i, save_data);

            //tourist_num = saveandrestore.load_ushort(ref i, save_data);
            //tourist_num_final = saveandrestore.load_ushort(ref i, save_data);

            //tourist_transport_fee_num = saveandrestore.load_long(ref i, save_data);
            //tourist_transport_fee_num_final = saveandrestore.load_long(ref i, save_data);
            is_weekend = saveandrestore.load_bool(ref i, save_data);
            have_toll_station = saveandrestore.load_bool(ref i, save_data);
            have_city_resource_department = saveandrestore.load_bool(ref i, save_data);
            building_buffer1 = saveandrestore.load_ushorts(ref i, save_data, building_buffer1.Length);
            building_buffer2 = saveandrestore.load_ushorts(ref i, save_data, building_buffer2.Length);
            building_buffer3 = saveandrestore.load_ushorts(ref i, save_data, building_buffer3.Length);
            building_buffer4 = saveandrestore.load_ushorts(ref i, save_data, building_buffer4.Length);

            DebugLog.LogToFileOnly("save_data in comm_data is " + i.ToString());
        }

        public static void load1()
        {
            int i = 0;

            i = 0;
            citizen_money = saveandrestore.load_floats(ref i, save_data1, citizen_money.Length);

            DebugLog.LogToFileOnly("save_data1 in comm_data is " + i.ToString());
        }
    }
}
