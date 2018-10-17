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
        public static bool isPoliticsOn = false;
        public static bool isStateOwned = false;
        public static bool noImport = false;
        public static bool isOutSideGarbagePermit = false;
        public static bool isHighTax = false;
        public static bool isLowTax = false;
        public static bool isNormalTax = false;
        public static bool isNormalBenefit = false;
        public static bool isHighBenefit = false;

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

        public static bool case1 = false;
        public static bool case2 = false;
        public static bool case3 = false;
        public static bool case4 = false;
        public static bool case5 = false;
        public static bool case6 = false;
        public static bool case7 = false;
        public static bool case8 = false;
        public static bool case9 = false;
        public static bool case10 = false;


        public static byte[,] education = { {30, 0,  10, 10, 50},
                                            {25, 10, 20, 20, 25},
                                            {20, 20, 25, 25, 10},
                                            {15 ,30, 30, 20, 5}};

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
        public static byte[,] workplace = { {5,  20, 35, 35, 5},      //goverment
                                            {20, 10, 40, 30, 0},      //comm level1
                                            {10, 15, 35, 35, 5},      //comm level2
                                            {0, 20, 30, 40, 10},      //comm level3
                                            {0,  30, 25, 45, 0},      //comm tour leisure
                                            {20, 20, 20, 20, 20},     //comm eco
                                            {35, 0,  40, 20, 5},     //indus gen level1
                                            {20, 5,  35, 25, 15},     //indus gen level2
                                            {15, 10, 30, 30, 15},     //indus gen level3
                                            {25, 5, 10,  25, 35},     //9  indus farming foresty oil ore
                                            {10, 30, 30, 30, 0},      //office level1
                                            {5 , 35, 25, 35, 0},      //office level2
                                            {0,  40, 10, 35, 15},     //office level3
                                            {0,  50, 10, 40, 0},      //office high tech
                                            {35, 0,  10, 10, 45}};    //no work

        // money < 2000
        // 7000 > money > 2000
        // money > 7000
        public static byte[,] money =     { {35, 0,  25, 10, 30},
                                            {10, 10, 35, 35, 10},
                                            {0 , 30, 15, 40, 15}};

        //youg
        //adult
        //senior
        public static byte[,] age =       { {20, 20, 30, 15,  15},
                                            {10, 15, 25, 30, 20},
                                            {5, 10, 20,  40, 25}};
        //man
        //woman
        public static byte[,] gender =    { {15, 15, 25, 30, 15},
                                            {10, 20, 30, 35, 5 }};

    }
}
