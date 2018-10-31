using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCity
{
    public class Language
    {
        public static string[] OptionUI_English =
        {
            "Language",                                                                                      //0
            "Language_Select",                                                                               //1
            "Smart Public Transport(more cars and maintenance in peek, less in trough)",                     //2
            "(click me)"                                                                                     //3
        };

        public static string[] BuildingUI_English = {
            "Building Money(unit:1/100₡)",                           //0
            "Material Buffer",                                       //2
            "Production Buffer",                                     //4
            "Average Employfee",                                     //8
            "Building Landrent",                                     //10
            "Family Money(unit:1/100₡)",                             //14
            "(profit-sharing)",                                      //16
            "Accumulating Money(unit:₡)",                            //17
            "Average Buy Pice:",                                     //18
            "Average Sell Pice:",                                    //19
            "Family Salary:",                                        //20
            "Material/Production:",                                  //21
            "Sell tax",                                              //22
            "BuyToSell Profit",                                      //23
            "(exclude visit income)",                                //24
            "Family status:",                                        //25
            "Food stored",                                           //27
            "Lumber stored",                                         //28
            "Coal stored",                                           //29
            "Petrol stored",                                         //30
            "Food",                                                  //31
            "Coal",                                                  //32
            "Lumber",                                                //33
            "Petrol",                                                //34
            "Grain or Meat",                                         //35
            "Ore",                                                   //36
            "Log",                                                   //37
            "Oil",                                                   //38
            "PreGoods",                                              //39
            "Goods",                                                 //40
            "Price:" ,                                               //41
            "AnimalProducts",
            "Flours",
            "Paper",
            "PlanedTimber",
            "Petroleum",
            "Plastics",
            "Glass",
            "Metals",
            "LuxuryProducts",
            "Family Goods",
            "(Family Needs Goods)",
        };


        public static string[] PoliticsMessage_English = {
        "Parliament Hall",                                      //0
        "Parliament Seats",                                     //1
        "Communist:",                                           //2
        "Green ",                                               //3
        "Socialist ",                                           //4
        "Liberal ",                                             //5
        "National ",                                            //6
        "Goverment:",                                           //7
        "(Left union)",                                         //8
        "(Wide left union)",                                    //9
        "(Right union)",                                        //10
        "(All union)",                                          //11
        "Polls",                                                //12
        "Next Vote",                                            //13
        "Next Meeting",                                         //14
        "Current Meeting Item ",                                //15
        "riseResidentTax",                                      //16
        "fallResidentTax",                                      //17
        "riseBenefit",                                          //18
        "fallBenefit",                                          //19
        "riseCommericalTax",                                    //20
        "fallCommericalTax",                                    //21
        "riseIndustrialTax",                                    //22
        "fallIndustrialTax",                                    //23
        "allowOutsideGarbage",                                  //24
        "notAllowOutsideGarbage",                               //25
        "VoteResult ",                                          //26
        "Yes",                                                  //27
        "No",                                                   //28
        "NoAttend",                                             //29
        "Current Policy:",                                      //30
        "Allow Outside Garbage:",                               //31
        "Benefit:",                                             //32
        "ResidentSalaryTax:",                                             //32
        "CommericalTradeTax:",                                             //32
        "IndustrialTradeTax:",                                             //32
        };

        public static string[] EconomicUI_English =
        {
            "Economic Data",                                         //0
            "1、Citizen Status(unit: 1/100₡)",                       //1
            "citizenCount",                                          //2
            "familyCount",                                           //3
            "salaryPerFamily",                                       //4
            "citizenTaxPerFamily",                                   //5
            "expensePerFamily",                                      //6
            "averageTransFee",                                       //7
            "highSalaryCount",                                       //8
            "mediumSalaryCount",                                     //9
            "lowSalaryCount",                                        //10
            "WealthHighCount",                                       //11
            "WealthMediumCount",                                     //12
            "WealthLowCount",                                        //13                      
            "2、Building Status",                                    //14
            "profit(I&C)Building",                                   //15
            "externalInvestments(for office)",                       //16
            "tip1",                                                  //17
            "tip2",                                                  //18
            "tip3",                                                  //19
            "tip4",                                                  //20
            "tip5",                                                  //21
            "tip6",                                                  //22
        };


        public static string[] TipAndChirperMessage_English = {                              //0
        "Better use TMPE and disable vehicle spawn",                                         //1
        "Can use Favorite Cims mod to watch every citizens and their family",                //2
        "Please Build City Resource in Roal Panel(need asset)",                              //3
        "City Lack of Food, Leads to no movingin people and Citizens may get sick",          //4
        "City Lack of Lumber or Coal, Leads to no RICO demand",                              //5
        "City Lack of Petrol, goverment will use less car",                                  //6
        };


        public static string[] RealCityUI_English =
        {
            "1、City resident salary-tax income(include food fee)",
            "citizen salary-tax income",
            "2、City tourism income",
            "from resident",
            "from tourist",
            "3、City land tax income",
            "residentHighLandIncome",
            "residentLowLandIncome",
            "residentHighEcoLandIncome",
            "residentLowEcoLandIncome",
            "commericalHighLandIncome",
            "commericalLowLandIncome",
            "commericalEcoLandIncome",
            "commericalTourismLandIncome",
            "commericalLeisureLandIncome",
            "industrialGeneralLandIncome",
            "industrialFarmingLandIncome",
            "industrialForestyLandIncome",
            "industrialOilLandIncome",
            "industrialOreLandIncome",
            "officeGeneralLandIncome",
            "officeHighTech_LandIncome",
            "4、City trade tax income",
            "commericalHighTradeIncome",
            "commericalLowTradeIncome",
            "commericalEcoTradeIncome",
            "commericalTourismTradeIncome",
            "commericalLeisureTradeIncome",
            "industrialGeneralTradeIncome",
            "industrialFarmingTradeIncome",
            "industrialForestyTradeIncome",
            "industrialOilTradeIncome",
            "industrialOreTradeIncome",
            "5、City Public transport income",
            "Bus",
            "Tram",
            "Train",
            "Ship",
            "Plane",
            "Metro",
            "Taxi",
            "Cablecar",
            "Monorail",
            "6、City Player Building income",
            "Road",
            "Garbage",
            "School",
            "PlayerIndustry",
            "7、City all total income",
            "City income data(unit:₡)",
            "City status",
            "If using real time,tell mod if it is weekend to disable Smart Public Transport function",
        };




        public static string[] BuildingUI_Chinese = {
            "公司现金流(单位:1/100克朗)",                      //0
            "原料仓库储存量",                                  //1
            "产品仓库储存量",                                  //2
            "员工平均工资(按居民刷新周期算)",                  //3
            "土地费支出",                                      //4
            "居民家庭存款(单位:1/100克朗)",                    //5
            "分红制(当次)",                                    //6
            "累积赚或亏的钱(单位:克朗)",                       //7
            "原料买入平均价格:",                               //8
            "产品卖出平均价格:",                               //9
            "家庭总工资:",                                     //10
            "原料产品消耗比:",                                 //11
            "卖出收入交易税",                                  //12
            "买入卖出利润率",                                  //13
            "(不包括商业访问收入)",                            //14 
            "家庭状况",                                        //15
            "存储的食物",                                      //16
            "存储的木材",                                      //17
            "存储的矿物",                                      //18
            "存储的汽油",                                      //19
            "食物",                                            //20
            "矿物",                                            //21
            "木材",                                            //22
            "汽油",                                            //23
            "谷物或肉",                                        //24
            "原矿石",                                          //25
            "原木",                                            //26
            "原油",                                            //27
            "库存商品",                                        //28
            "货架商品",                                        //29
            "价格:" ,                                          //30
            "动物制品",                                        //31
            "面粉",                                            //32
            "纸",                                              //33
            "木板木材",                                        //34
            "石油制品",                                        //35
            "塑料",                                            //36
            "玻璃",                                            //37
            "金属",                                            //38
            "奢侈消费品",                                      //39
            "家里货物量",
            "(家里需要购物)",
        };

    public static string[] OptionUI_Chinese =
        {
            "语言",                                                   //0
            "语言选择",                                               //1
            "智能公共交通(早晚高峰车多维护费多,深夜钱少维护费少)",    //2
            "(点我)",                                                 //3
        };

        public static string[] EconomicUI_Chinese =
        {
            "经济数据",                                    //0
            "1、居民情况(钱单位:1/100克朗)",               //1
            "居民人数",                                    //2
            "家庭户数",                                    //3
            "每户居民收入",                                //4
            "居民平均纳税",                                //5
            "每户固定支出",                                //6
            "平均公交费用",                                //7
            "收支优秀的户数",                              //8
            "收支尚可的户数",                              //9
            "收支糟糕的户数",                              //10
            "财富稳定性高",                                //11
            "财富稳定性中",                                //12
            "财富稳定性低",                                //13
            "2、建筑情况",                                 //14
            "赚钱的公司数目",                              //15
            "可用于投资办公的钱",                          //16
            "提示1",                                       //19
            "提示2",                                       //20
            "提示3",                                       //21
            "提示4",                                       //22
            "提示5",                                       //23
            "提示6",                                       //24
        };


        public static string[] TipAndChirperMessage_Chinese = {
        "最好用TMPE并且禁止车辆消失",                                                        //0
        "可以用Favorite Cims mod看每个居民和他们家庭的情况",                                 //1
        "请建立城市资源大厦(需要特殊资产,道路界面)",                                         //2
        "城市缺少食物,会导致没外来人员而且居民会生病",                                       //3
        "城市缺少木材和矿物品,没法建房子,所以没任何建筑需求",                                //4
        "城市缺少汽油,政府部门会少出车",                                                     //5
        };


        public static string[] PoliticsMessage_Chinese = {
        "议会大厅",                                      //0
        "议会席位",                                      //1
        "和谐党(你懂的) ",                               //2
        "绿党 ",                                         //3
        "社会党 ",                                       //4
        "自由党 ",                                       //5
        "国家党 ",                                       //6
        "执政情况:",                                     //7
        "(左派联合)",                                    //8
        "(左派大联合)",                                  //9
        "(右翼联盟)",                                    //10
        "(全民团结政府)",                                 //11
        "民调:",                                          //12
        "下次选举时间",                                    //13
        "下次会议时间",                                    //14
        "目前审理的议题",                                  //15
        "升高居民税",                                   //16
        "降低居民税",                                   //17
        "增加福利",                                   //18
        "降低福利",                                   //19
        "增加商业税",                                   //20
        "降低商业税",                                   //21
        "增加工业税",                                   //22
        "降低工业税",                                   //23
        "允许外来垃圾",                                   //24
        "禁止外来垃圾",                                   //25
        "投票结果",                                     //26
        "赞成",                                       //27
        "反对",                                       //28
        "弃权",                                       //29
        "目前政策",                                   //30
        "允许外来垃圾",                               //31
        "福利值",                                     //32
        "居民个人所得税:",                                             //32
        "商业交易税:",                                             //32
        "工业交易税:",                                             //32
        };

        public static string[] RealCityUI_Chinese =
        {
            "1、城市居民所得税收入(包含食品费)",       //0
            "居民所得税收入",                          //1
            "2、城市旅游收入",                         //2
            "来自本地游客",                            //3
            "来自外地游客",                            //4
            "3、城市地税收入",                         //5
            "高密度居民区地税收入",
            "低密度居民区地税收入",
            "高密度生态居民区地税收入",
            "低密度生态居民区地税收入",
            "高密度商业地税收入",
            "低密度商业地税收入",
            "生态商业地税收入",
            "旅游商业地税收入",
            "娱乐商业地税收入",
            "一般工业地税收入",
            "农业地税收入",
            "林业地税收入",
            "石油业地税收入",
            "矿业地税收入",
            "一般办公区地税收入",
            "高科技办公区地税收入",
            "4、城市交易税收入",
            "高密度商业交易税收入",
            "低密度商业交易税收入",
            "生态商业交易税收入",
            "旅游商业交易税收入",
            "娱乐商业交易税收入",
            "一般工业交易税收入",
            "农业交易税收入",
            "林业交易税收入",
            "石油业交易税收入",
            "矿业交易税收入",
            "5、城市公共交通收入",
            "公交车",
            "有轨电车",
            "火车",
            "渡船",
            "飞机",
            "地铁",
            "出租车",
            "缆车",
            "单轨",
            "6、政府建筑收入",
            "道路",
            "垃圾",
            "学校",
            "国营工业",                                                           //47
            "7、城市总收入",                                                      //48
            "城市收入情况(单位:克朗)",                                            //49
            "城市情况",                                                           //50
            "如果用realtime,告诉MOD现在是不是周末,可以关闭智能交通控制功能",      //51
        };


        public static string[] BuildingUI = new string[BuildingUI_English.Length];
        public static string[] OptionUI = new string[OptionUI_English.Length];
        public static string[] RealCityUI1 = new string[RealCityUI_Chinese.Length];
        public static string[] TipAndChirperMessage = new string[TipAndChirperMessage_Chinese.Length];
        public static string[] EconomicUI = new string[EconomicUI_Chinese.Length];
        public static string[] PoliticsMessage = new string[PoliticsMessage_Chinese.Length];

        public static byte currentLanguage = 255; 

        public static void LanguageSwitch (byte language)
        {
            if (language == 1)
            {
                for (int i = 0; i < BuildingUI_English.Length; i++)
                {
                    BuildingUI[i] = BuildingUI_Chinese[i];
                }
                for (int i = 0; i < OptionUI_English.Length; i++)
                {
                    OptionUI[i] = OptionUI_Chinese[i];
                }
                for (int i = 0; i < EconomicUI_English.Length; i++)
                {
                    EconomicUI[i] = EconomicUI_Chinese[i];
                }
                for (int i = 0; i < TipAndChirperMessage_English.Length; i++)
                {
                    TipAndChirperMessage[i] = TipAndChirperMessage_Chinese[i];
                }
                for (int i = 0; i < RealCityUI_English.Length; i++)
                {
                    RealCityUI1[i] = RealCityUI_Chinese[i];
                }
                for (int i = 0; i < PoliticsMessage_English.Length; i++)
                {
                    PoliticsMessage[i] = PoliticsMessage_Chinese[i];
                }
                currentLanguage = 1;
                EcnomicUI.WIDTH = 600;
                RealCityUI.WIDTH = 650;
                PoliticsUI.WIDTH = 750;
            }
            else if (language == 0)
            {
                for (int i = 0; i < BuildingUI_English.Length; i++)
                {
                    BuildingUI[i] = BuildingUI_English[i];
                }
                for (int i = 0; i < OptionUI_English.Length; i++)
                {
                    OptionUI[i] = OptionUI_English[i];
                }
                for (int i = 0; i < EconomicUI_English.Length; i++)
                {
                    EconomicUI[i] = EconomicUI_English[i];
                }
                for (int i = 0; i < TipAndChirperMessage_English.Length; i++)
                {
                    TipAndChirperMessage[i] = TipAndChirperMessage_English[i];
                }
                for (int i = 0; i < RealCityUI_English.Length; i++)
                {
                    RealCityUI1[i] = RealCityUI_English[i];
                }
                for (int i = 0; i < PoliticsMessage_English.Length; i++)
                {
                    PoliticsMessage[i] = PoliticsMessage_English[i];
                }
                currentLanguage = 0;
                EcnomicUI.WIDTH = 700;
                RealCityUI.WIDTH = 750;
                PoliticsUI.WIDTH = 800;
            }
            else
            {
                DebugLog.LogToFileOnly("unknow language!! use English");
                for (int i = 0; i < BuildingUI_English.Length; i++)
                {
                    BuildingUI[i] = BuildingUI_English[i];
                }
                for (int i = 0; i < OptionUI_English.Length; i++)
                {
                    OptionUI[i] = OptionUI_English[i];
                }
                for (int i = 0; i < EconomicUI_English.Length; i++)
                {
                    EconomicUI[i] = EconomicUI_English[i];
                }
                for (int i = 0; i < TipAndChirperMessage_English.Length; i++)
                {
                    TipAndChirperMessage[i] = TipAndChirperMessage_English[i];
                }
                for (int i = 0; i < RealCityUI_English.Length; i++)
                {
                    RealCityUI1[i] = RealCityUI_English[i];
                }
                for (int i = 0; i < PoliticsMessage_English.Length; i++)
                {
                    PoliticsMessage[i] = PoliticsMessage_English[i];
                }
                currentLanguage = 0;
                EcnomicUI.WIDTH = 700;
                RealCityUI.WIDTH = 750;
                PoliticsUI.WIDTH = 800;
            }

            MainDataStore.last_language = currentLanguage;
        }
    }
}
