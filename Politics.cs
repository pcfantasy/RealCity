using ColossalFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCity
{
    public class Politics
    {
        public static byte[,] education = { {30, 0,  10, 10, 50},
                                            {20, 10, 25, 20, 25},
                                            {10, 20, 30, 30, 10},
                                            {5 , 25, 40, 25, 5}};

        //0  govement
        //1  comm level1
        //2  comm level2
        //3  comm level3
        //4  comm tour comm leisure
        //5  comm eco
        //6  indus gen level1
        //7  indus gen level2
        //8  indus gen level3
        //9  indus farming foresty oil ore
        //10 office level1
        //11 office level2
        //12 office level3
        //13 office high tech
        //14 no work
        public static byte[,] workplace = { {0,  20, 40, 40, 0},      //goverment
                                            {20, 10, 40, 30, 0},      //comm level1
                                            {10, 15, 35, 35, 5},      //comm level2
                                            {0,  20, 30, 40, 10},      //comm level3
                                            {0,  30, 25, 45, 0},      //comm tour leisure
                                            {35, 10, 5,  20, 30},     //comm eco
                                            {50, 0,  25, 10, 15},      //indus gen level1
                                            {30, 5,  35, 15, 15},     //indus gen level2
                                            {15, 10, 40, 20, 15},     //indus gen level3
                                            {25, 5,  20, 20, 30},     //9  indus farming foresty oil ore
                                            {10, 30, 30, 30, 0},      //office level1
                                            {5 , 35, 25, 35, 0},      //office level2
                                            {0,  40, 10, 35, 15},     //office level3
                                            {0,  50, 10, 40, 0},      //office high tech
                                            {35, 0,  10, 10, 45}};    //no work

        // money < 2000
        // 6000 > money > 2000
        // money > 6000
        public static byte[,] money =     { {35, 0,  25, 10, 30},
                                            {10, 10, 35, 35, 10},
                                            {0 , 30, 15, 40, 15}};

        //youg
        //adult
        //senior
        public static byte[,] age =       { {15, 20, 30, 20,  15},
                                            {10, 15, 25, 30,  20},
                                            {5,  10, 20, 40,  25}};
        //man
        //woman
        public static byte[,] gender =    { {10, 10, 35, 35, 10},
                                            {5,  15, 40, 35, 5 }};


        //riseSalaryTax
        public static byte[,] riseSalaryTax = {
                                                {55, 40, 5},
                                                {10, 80, 10},
                                                {30, 70, 0},
                                                {70, 30, 0},
                                                {35, 55, 10},
                                              };

        //fallSalaryTax
        public static byte[,] fallSalaryTax = {
                                                {40, 55, 5},
                                                {80, 10, 10},
                                                {70, 30, 0},
                                                {30, 70, 0},
                                                {55, 35, 10},
                                              };

        //riseSalaryTax
        public static byte[,] riseCommericalTax = {
                                                {80, 20, 0},
                                                {40, 50, 10},
                                                {55, 40, 5},
                                                {10, 90, 0},
                                                {45, 45, 10},
                                              };

        //fallSalaryTax
        public static byte[,] fallCommericalTax = {
                                                {20, 80, 0},
                                                {50, 40, 10},
                                                {40, 55, 5},
                                                {90, 10, 0},
                                                {45, 45, 10},
                                              };

        //riseSalaryTax
        public static byte[,] riseBenefit = {
                                                {40, 50, 10},
                                                {70, 20, 10},
                                                {90, 10, 0},
                                                {10, 90, 0},
                                                {30, 60, 10},
                                              };

        //fallSalaryTax
        public static byte[,] fallBenefit = {
                                                {50, 40, 10},
                                                {20, 70, 10},
                                                {10, 90, 0},
                                                {90, 10, 0},
                                                {60, 30, 10},
                                              };


        //riseSalaryTax
        public static byte[,] riseIndustryTax = {
                                                {20, 70, 10},
                                                {50, 50, 0},
                                                {60, 30, 10},
                                                {70, 30,  0},
                                                {30, 70,  0},
                                              };

        //fallSalaryTax
        public static byte[,] fallIndustryTax = {
                                                {70, 20, 10},
                                                {50, 50, 0},
                                                {30, 60, 10},
                                                {30, 70, 0},
                                                {70, 30, 0},
                                              };


        //riseSalaryTax
        public static byte[,] riseGarbage = {
                                                {90, 10,  0},
                                                {10, 90, 0},
                                                {40, 55, 5},
                                                {50, 45, 5},
                                                {75, 20, 5},
                                              };

        //fallSalaryTax
        public static byte[,] fallGarbage = {
                                                {10, 90, 0},
                                                {90, 10, 0},
                                                {55, 40, 5},
                                                {45, 50, 5},
                                                {20, 75, 5},
                                              };


        public static bool isOutSideGarbagePermit = false;


        public static ushort cPartyChance = 0;
        public static ushort gPartyChance = 0;
        public static ushort sPartyChance = 0;
        public static ushort lPartyChance = 0;
        public static ushort nPartyChance = 0;

        public static ushort cPartyTickets = 0;
        public static ushort gPartyTickets = 0;
        public static ushort sPartyTickets = 0;
        public static ushort lPartyTickets = 0;
        public static ushort nPartyTickets = 0;

        public static ushort cPartySeats = 0;
        public static ushort gPartySeats = 0;
        public static ushort sPartySeats = 0;
        public static ushort lPartySeats = 0;
        public static ushort nPartySeats = 0;

        public static float cPartySeatsPolls = 0;
        public static float gPartySeatsPolls = 0;
        public static float sPartySeatsPolls = 0;
        public static float lPartySeatsPolls = 0;
        public static float nPartySeatsPolls = 0;

        public static float cPartySeatsPollsFinal = 0;
        public static float gPartySeatsPollsFinal = 0;
        public static float sPartySeatsPollsFinal = 0;
        public static float lPartySeatsPollsFinal = 0;
        public static float nPartySeatsPollsFinal = 0;

        public static short parliamentCount = 0;
        //public static short parliamentMeetingCount = 0;

        public static bool case1 = false;
        public static bool case2 = false;
        public static bool case3 = false;
        public static bool case4 = false;
        public static bool case5 = false;
        public static bool case6 = false;
        public static bool case7 = false;
        public static bool case8 = false;

        public static byte currentIdx = 14;
        public static byte currentYes = 0;
        public static byte currentNo = 0;
        public static byte currentNoAttend = 0;
        public static int residentTax = 20;     //(0-20)
        public static int commericalTax = 20;    //(0-20)
        public static int industryTax = 20;    //(0-20)
        public static short benefitOffset = 0;         //(0-10)
        public static short garbageCount = 10;         //(0-10)

        public static byte[] saveData = new byte[103];

        public static void Save()
        {
            int i = 0;

            //1
            SaveAndRestore.save_bool(ref i, isOutSideGarbagePermit, ref saveData);

            //30
            SaveAndRestore.save_ushort(ref i, cPartyChance, ref saveData);
            SaveAndRestore.save_ushort(ref i, gPartyChance, ref saveData);
            SaveAndRestore.save_ushort(ref i, sPartyChance, ref saveData);
            SaveAndRestore.save_ushort(ref i, lPartyChance, ref saveData);
            SaveAndRestore.save_ushort(ref i, nPartyChance, ref saveData);

            SaveAndRestore.save_ushort(ref i, cPartyTickets, ref saveData);
            SaveAndRestore.save_ushort(ref i, gPartyTickets, ref saveData);
            SaveAndRestore.save_ushort(ref i, sPartyTickets, ref saveData);
            SaveAndRestore.save_ushort(ref i, lPartyTickets, ref saveData);
            SaveAndRestore.save_ushort(ref i, nPartyTickets, ref saveData);

            SaveAndRestore.save_ushort(ref i, cPartySeats, ref saveData);
            SaveAndRestore.save_ushort(ref i, gPartySeats, ref saveData);
            SaveAndRestore.save_ushort(ref i, sPartySeats, ref saveData);
            SaveAndRestore.save_ushort(ref i, lPartySeats, ref saveData);
            SaveAndRestore.save_ushort(ref i, nPartySeats, ref saveData);

            //20
            SaveAndRestore.save_float(ref i, cPartySeatsPolls, ref saveData);
            SaveAndRestore.save_float(ref i, gPartySeatsPolls, ref saveData);
            SaveAndRestore.save_float(ref i, sPartySeatsPolls, ref saveData);
            SaveAndRestore.save_float(ref i, lPartySeatsPolls, ref saveData);
            SaveAndRestore.save_float(ref i, nPartySeatsPolls, ref saveData);

            //20
            SaveAndRestore.save_float(ref i, cPartySeatsPollsFinal, ref saveData);
            SaveAndRestore.save_float(ref i, gPartySeatsPollsFinal, ref saveData);
            SaveAndRestore.save_float(ref i, sPartySeatsPollsFinal, ref saveData);
            SaveAndRestore.save_float(ref i, lPartySeatsPollsFinal, ref saveData);
            SaveAndRestore.save_float(ref i, nPartySeatsPollsFinal, ref saveData);

            //4
            SaveAndRestore.save_short(ref i, parliamentCount, ref saveData);
            SaveAndRestore.save_short(ref i, 0, ref saveData);

            //8
            SaveAndRestore.save_bool(ref i, case1, ref saveData);
            SaveAndRestore.save_bool(ref i, case2, ref saveData);
            SaveAndRestore.save_bool(ref i, case3, ref saveData);
            SaveAndRestore.save_bool(ref i, case4, ref saveData);
            SaveAndRestore.save_bool(ref i, case5, ref saveData);
            SaveAndRestore.save_bool(ref i, case6, ref saveData);
            SaveAndRestore.save_bool(ref i, case7, ref saveData);
            SaveAndRestore.save_bool(ref i, case8, ref saveData);

            //4
            SaveAndRestore.save_byte(ref i, currentIdx, ref saveData);
            SaveAndRestore.save_byte(ref i, currentYes, ref saveData);
            SaveAndRestore.save_byte(ref i, currentNo, ref saveData);
            SaveAndRestore.save_byte(ref i, currentNoAttend, ref saveData);

            //12
            SaveAndRestore.save_int(ref i, residentTax, ref saveData);
            SaveAndRestore.save_int(ref i, commericalTax, ref saveData);
            SaveAndRestore.save_int(ref i, industryTax, ref saveData);

            //6
            SaveAndRestore.save_short(ref i, benefitOffset, ref saveData);
            SaveAndRestore.save_short(ref i, garbageCount, ref saveData);

            DebugLog.LogToFileOnly("(save)saveData in politics is " + i.ToString());
        }

        public static void Load()
        {
            int i = 0;

            isOutSideGarbagePermit = SaveAndRestore.load_bool(ref i, saveData);

            cPartyChance = SaveAndRestore.load_ushort(ref i, saveData);
            gPartyChance = SaveAndRestore.load_ushort(ref i, saveData);
            sPartyChance = SaveAndRestore.load_ushort(ref i, saveData);
            lPartyChance = SaveAndRestore.load_ushort(ref i, saveData);
            nPartyChance = SaveAndRestore.load_ushort(ref i, saveData);

            cPartyTickets = SaveAndRestore.load_ushort(ref i, saveData);
            gPartyTickets = SaveAndRestore.load_ushort(ref i, saveData);
            sPartyTickets = SaveAndRestore.load_ushort(ref i, saveData);
            lPartyTickets = SaveAndRestore.load_ushort(ref i, saveData);
            nPartyTickets = SaveAndRestore.load_ushort(ref i, saveData);

            cPartySeats = SaveAndRestore.load_ushort(ref i, saveData);
            gPartySeats = SaveAndRestore.load_ushort(ref i, saveData);
            sPartySeats = SaveAndRestore.load_ushort(ref i, saveData);
            lPartySeats = SaveAndRestore.load_ushort(ref i, saveData);
            nPartySeats = SaveAndRestore.load_ushort(ref i, saveData);

            cPartySeatsPolls = SaveAndRestore.load_float(ref i, saveData);
            gPartySeatsPolls = SaveAndRestore.load_float(ref i, saveData);
            sPartySeatsPolls = SaveAndRestore.load_float(ref i, saveData);
            lPartySeatsPolls = SaveAndRestore.load_float(ref i, saveData);
            nPartySeatsPolls = SaveAndRestore.load_float(ref i, saveData);

            cPartySeatsPollsFinal = SaveAndRestore.load_float(ref i, saveData);
            gPartySeatsPollsFinal = SaveAndRestore.load_float(ref i, saveData);
            sPartySeatsPollsFinal = SaveAndRestore.load_float(ref i, saveData);
            lPartySeatsPollsFinal = SaveAndRestore.load_float(ref i, saveData);
            nPartySeatsPollsFinal = SaveAndRestore.load_float(ref i, saveData);

            parliamentCount = SaveAndRestore.load_short(ref i, saveData);
            short temp = SaveAndRestore.load_short(ref i, saveData);

            case1 = SaveAndRestore.load_bool(ref i, saveData);
            case2 = SaveAndRestore.load_bool(ref i, saveData);
            case3 = SaveAndRestore.load_bool(ref i, saveData);
            case4 = SaveAndRestore.load_bool(ref i, saveData);
            case5 = SaveAndRestore.load_bool(ref i, saveData);
            case6 = SaveAndRestore.load_bool(ref i, saveData);
            case7 = SaveAndRestore.load_bool(ref i, saveData);
            case8 = SaveAndRestore.load_bool(ref i, saveData);

            currentIdx = SaveAndRestore.load_byte(ref i, saveData);
            currentYes = SaveAndRestore.load_byte(ref i, saveData);
            currentNo = SaveAndRestore.load_byte(ref i, saveData);
            currentNoAttend = SaveAndRestore.load_byte(ref i, saveData);

            residentTax = SaveAndRestore.load_int(ref i, saveData);
            commericalTax = SaveAndRestore.load_int(ref i, saveData);
            industryTax = SaveAndRestore.load_int(ref i, saveData);

            benefitOffset = SaveAndRestore.load_short(ref i, saveData);
            garbageCount = SaveAndRestore.load_short(ref i, saveData);
        }
     }
}
