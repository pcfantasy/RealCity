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
            "Smart Public Transport(more cars and maintenance in peek, less in trough)",                    //2
            "Hell mode(little import)",   //3
            "(click me)"                  //4
        };

        public static string[] BuildingUI_English = {
            "Building Money(unit:1/100₡)",                           //0
            "Only show industry and commerical building money",      //1
            "Material Buffer",                                       //2
            "Only show industry and commerical Material Buffer",     //3
            "Production Buffer",                                     //4
            "Only show industry and commerical Production Buffer",   //5
            "aliveworkcount",                                        //6
            "aliveworkcount",                                        //7
            "Average Employfee",                                     //8
            "Average Employfee",                                     //9
            "Building Landrent",                                     //10
            "Building Landrent",                                     //11
            "Building net asset",                                    //12
            "Building net asset",                                    //13
            "Family Money(unit:1/100₡)",                             //14
            "Only show Family money",                                //15
            "(profit-sharing)",                                      //16
            "Accumulating Money(unit:₡)",                            //17
            "Last Buy:",                                            //18
            "Last Sell:",                                           //19
            "Family Salary:",                                        //20
            "Material/Production:",                                  //21
            "Sell tax",                                              //22
            "BuyToSell Profit",                                      //23
            "(exclude visit income)",                                //24
            "Family status:",                                        //25
            "No toll department",                                    //26
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
             "所有权:",                                               //42
             "国营",                                                  //43
             "私营",                                                  //44
        };


        public static string[] PoliticsMessage_English = {
        "Parliament Hall",                                      //0
        "Parliament Seats",                                      //1
        "Communist:",                               //2
        "Green ",                                         //3
        "Socialist ",                                   //4
        "Liberal ",                                        //5
        "National ",                                        //6
        "Goverment:",                                      //7
        "(Left union)",                                      //8
        "(Wide left union)",                                    //9
        "(Right union)",                                      //10
        "(All union)",                                  //11
        "执政风格:",                                      //12
        "SuggestRiseImportTax(not available if importTaxOffset >= 0.4)",  //13
        "SuggestFallImportTax(not available if importTaxOffset <= 0)",   //14
        "SuggestFallLandRent(not available if landRentOffset <= 0)",   //15
        "SuggestRiseTradeTax(not available if tradTaxOffset >= 0.1)", //16
        "SuggestFallTradeTax(not available if tradTaxOffset <= 0)",   //17
        "高所得税 ",                                      //18
        "Polls",
        "No special politics",
        "Next Vote",
        "Next Meeting",                                    //22
        "Current Meeting Item ",                                  //23
        "riseSalaryTax",                                   //24
        "fallSalaryTax",                                   //25
        "riseBenefit",                                   //26
        "fallBenefit",                                   //27
        "riseTradeTax",                                   //28
        "fallTradeTax",                                   //29
        "riseImportTax",                                   //30
        "fallImportTax",                                   //31
        "riseStateOwned",                                   //32
        "fallStateOwned",                                   //33
        "allowOutsideGarbage",                                   //34
        "notAllowOutsideGarbage",                                   //35
        "riseLandRent",                                   //36
        "fallLandRent",                                   //37
        "VoteResult ",                                       //38
        "Yes",                                       //39
        "No",                                       //40
        "NoAttend",                                       //41
        "Current Politics",                                    //42
        "salaryOffset:",                              //43
        "benefitOffset:",                                    //44
        "tradeTaxOffset:",                                  //45
        "importTaxOffset:",                                  //46
        "stateOwnedPercent:",                              //47
        "isAllowOutsideGarbage:",                              //48
        "(compared with mod standard value, only for debug)",                 //49
        "landRentOffset:",                              //50

        };

        public static string[] EconomicUI_English =
        {
            "Economic Data",                                         //0
            "1、Citizen Status(unit: 1/100₡)",                       //1
            "citizenCount",                                         //2
            "total citizen_count",                                   //3
            "familyCount",                                          //4
            "total family_count",                                    //5
            "salaryPerFamily",                                     //6
            "citizen_salary_per_family",                             //7
            "citizenTaxPerFamily",                                //8
            "total_citizen_salary_tax_per_family",                   //9
            "expensePerFamily",                                    //10
            "fixed_expense_per_family",                              //11
            "averageTransFee",                                     //12
            "citizen_average_transport_fee",                         //13
            "publicTransFee",                                      //14
            "public_transport_fee",                                  //15
            "citizenVehicalTime",                                  //16
            "total citizen_vehical_time",                            //17
            "highSalaryCount",                                           //18
            "family_high_salary_num",                                //19
            "mediumSalaryCount",                                         //20
            "total family_medium_salary_num",                        //21
            "lowSalaryCount",                                            //22
            "family_low_salary_num",                                 //23
            "WealthHighCount",                                           //24
            "family_wealth_stable_high_num",                         //25
            "WealthMediumCount",                                         //26
            "family_wealth_stable_high_num",                         //27
            "WealthLowCount",                                            //28
            "family_wealth_stable_low_num",                          //29                         
            "satisfactionsOfGoods",                                //30
            "family satisfactions of goods",                         //31
            "2、Building Status",                                    //32
            "profit(I&C)Building",                              //33
            "profit(I&C)Building",                              //34
            "externalInvestments(for office)",                             //35
            "externalInvestments(for office)",                             //36
            "tip1",                                                  //37
            "tip1",                                                  //38
            "tip2",                                                  //39
            "tip2",                                                  //40
            "tip3",                                                  //41
            "tip3",                                                  //42
            "tip4",                                                  //43
            "tip4",                                                  //44
            "tip5",                                                  //45
            "tip5",                                                  //46
            "tip6",                                                  //47
            "tip6",                                                  //48
            "tip7",                                                  //49
            "tip7",                                                  //50
            "tip8",                                                  //51
            "tip8",                                                  //52
            "tip9",                                                  //53
            "tip9",                                                  //54
            "tip10",                                                 //55
        };


        public static string[] TipAndChirperMessage_English = {
        "",                                                                             //0
        "",    //1
        "",                  //2
        "",                                                                                          //3
        "",                                                     //4
        "Better use TMPE and disable vehicle spawn",                                                 //5
        "",        //6
        "Can use Favorite Cims mod to watch every citizens and their family",                        //7
        "Please Build City Resource in Roal Panel(need asset)",                              //8
        "City Lack of Food, Leads to no movingin people and Citizens may get sick",                  //9
        "City Lack of Lumber or Coal, Leads to no RICO demand",                                      //10
        "City Lack of Petrol, goverment will use less car",                               //11
        };


        public static string[] RealCityUI_English =
        {
            "1、City resident salary-tax income(include food fee)",
            "citizen salary-tax income",
            "total citizen salary tax income",
            "2、City tourism income",
            "from resident",
            "money from resident tourism",
            "from tourist",
            "money from tourist tourism",
            "3、City land tax income",
            "total city land income",
            "residential_high_landincome",
            "residential_high_landincome",
            "resident_low_landincome",
            "resident_low_landincome",
            "resident_high_eco_landincome",
            "resident_high_eco_landincome",
            "resident_low_eco_landincome",
            "resident_low_eco_landincome",
            "commerical_high_landincome",
            "commerical_high_landincome",
            "commerical_low_landincome",
            "commerical_low_landincome",
            "commerical_eco_landincome",
            "commerical_eco_landincome",
            "commerical_tourism_landincome",
            "commerical_tourism_landincome",
            "commerical_leisure_landincome",
            "commerical_leisure_landincome",
            "industrial_general_landincome",
            "industrial_general_landincome",
            "industrial_farming_landincome",
            "industrial_farming_landincome",
            "industrial_foresty_landincome",
            "industrial_foresty_landincome",
            "industrial_oil_landincome",
            "industrial_oil_landincome",
            "industrial_ore_landincome",
            "industrial_ore_landincome",
            "office_general_landincome",
            "office_general_landincome",
            "office_high_tech_landincome",
            "office_high_tech_landincome",
            "4、City trade tax income",
            "total city trade tax income",
            "commerical_high_tradeincome",
            "commerical_high_tradeincome",
            "commerical_low_tradeincome",
            "commerical_low_tradeincome",
            "commerical_eco_tradeincome",
            "commerical_eco_tradeincome",
            "commerical_tourism_tradeincome",
            "commerical_tourism_tradeincome",
            "commerical_leisure_tradeincome",
            "commerical_leisure_tradeincome",
            "industrial_general_tradeincome",
            "industrial_general_tradeincome",
            "industrial_farming_tradeincome",
            "industrial_farming_tradeincome",
            "industrial_foresty_tradeincome",
            "industrial_foresty_tradeincome",
            "industrial_oil_tradeincome",
            "industrial_oil_tradeincome",
            "industrial_ore_tradeincome",
            "industrial_ore_tradeincome",
            "5、City Public transport income",
            "Bus",
            "Bus",
            "Tram",
            "Tram",
            "Train",
            "Train",
            "Ship",
            "Ship",
            "Plane",
            "Plane",
            "Metro",
            "Metro",
            "Taxi",
            "Taxi",
            "Cablecar",
            "Cablecar",
            "Monorail",
            "Monorail",
            "6、City Player Building income",
            "Player Building income",
            "Road",
            "Road",
            "Garbage",
            "Garbage",
            "School",
            "School",
            "7、City all total income",
            "City income data(unit:₡)",
            "City status",
            "If using real time,tell mod if it is weekend to disable Smart Public Transport function",
        };




        public static string[] BuildingUI_Chinese = {
            "公司现金流(单位:1/100克朗)",
            "只显示商业和工业建筑的现金流",
            "原料仓库储存量",
            "只显示商业和工业建筑的原料仓库储存量",
            "产品仓库储存量",
            "只显示商业和工业建筑的产品仓库储存量",
            "到达公司员工",
            "到达公司员工",
            "员工平均工资(按居民刷新周期算)",
            "员工平均工资(按居民刷新周期算)",
            "土地费支出",
            "土地费支出",
            "公司净资产",
            "公司净资产",
            "居民家庭存款(单位:1/100克朗)",
            "居民家庭存款",
            "分红制(当次)",
            "累积赚或亏的钱(单位:克朗)",
             "上次原料买入:",
             "上次产品卖出:",
             "家庭总工资:",
             "原料产品消耗比:",
             "卖出收入交易税",
             "买入卖出利润率",
             "(不包括商业访问收入)",
             "家庭状况",
             "无收费站",
             "存储的食物",
             "存储的木材",
             "存储的矿物",
             "存储的汽油",
             "食物",                                                  //31
             "矿物",                                                  //32
             "木材",                                                //33
             "汽油",                                                //34
             "谷物或肉",                                         //35
             "原矿石",                                                   //36
             "原木",                                                   //37
             "原油",                                                   //38
             "库存商品",                                              //39
             "货架商品",                                                 //40
             "价格:" ,                                               //41
             "所有权:",                                               //42
             "国营",                                                  //43
             "私营",                                                  //44
        };

    public static string[] OptionUI_Chinese =
        {
            "语言",
            "语言选择",
            "智能公共交通(早晚高峰车多维护费多,深夜钱少维护费少)",
            "地狱模式(很少的进口支撑,后期要靠本地自给自足)",
            "(点我)",
        };

        public static string[] EconomicUI_Chinese =
        {
            "经济数据",
            "1、居民情况(钱单位:1/100克朗)",
            "居民人数",
            "居民总人数",
            "家庭户数",
            "家庭总户数",
            "每户居民收入",
            "每户居民收入",
            "居民平均纳税",
            "居民平均纳税",
            "每户固定支出",
            "居民每户固定支出",
            "平均公交费用",
            "居民平均公交费用",
            "公共交通费用",
            "公共交通费用",
            "居民驾车时间",
            "居民总驾车时间",
            "收支优秀的户数",
            "收支优秀的户数",
            "收支尚可的户数",
            "收支尚可的户数",
            "收支糟糕的户数",
            "收支糟糕的户数",
            "财富稳定性高",
            "家庭财富稳定性高的户数",
            "财富稳定性中",
            "家庭财富稳定性中的户数",
            "财富稳定性低",
            "家庭财富稳定性低的户数",
            "家庭货物满足度",
            "家庭货物满足度",
            "2、建筑情况",
            "赚钱的公司数目",
            "赚钱的公司数目",
            "可用于投资办公的钱",
            "可用于投资办公的钱",
            "提示1",
            "提示1",
            "提示2",
            "提示2",
            "提示3",
            "提示3",
            "提示4",
            "提示4",
            "提示5",
            "提示5",
            "提示6",
            "提示6",
            "提示7",
            "提示7",
            "提示8",
            "提示8",
            "提示9",
            "提示9",
            "提示10"
        };


        public static string[] TipAndChirperMessage_Chinese = {
        "",
        "",
        "",
        "",
        "",
        "最好用TMPE并且禁止车辆消失",
        "",
        "可以用Favorite Cims mod看每个居民和他们家庭的情况",
        "请建立城市资源大厦(需要特殊资产,道路界面)",                                         //8
        "城市缺少食物,会导致没外来人员而且居民会生病",                                       //9
        "城市缺少木材和矿物品,没法建房子,所以没任何建筑需求",                                //10
        "城市缺少汽油,政府部门会少出车",                                                     //11
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
        "执政风格:",                                      //12
        "建议升高进口税(如果进口税偏差>=0.4点击不可用)",  //13
        "建议降低进口税(如果进口税偏差<=0点击不可用)",   //14
        "建议降低土地税(如果土地税偏差<=0点击不可用)",   //15
        "建议升高交易税(如果交易税偏差>=0.1点击不可用)", //16
        "建议降低交易税(如果交易税偏差<=0点击不可用)",   //17
        "高税收",                                          //18
        "民调:",                                          //19
        "无特殊政策",                                      //20
        "下次选举时间",                                    //21
        "下次会议时间",                                    //22
        "目前审理的议题",                                  //23
        "升高个人所得税",                                   //24
        "降低个人所得税",                                   //25
        "增加福利",                                   //26
        "降低福利",                                   //27
        "增加交易税",                                   //28
        "降低交易税",                                   //29
        "增加进口税",                                   //30
        "降低进口税",                                   //31
        "增加国有企业",                                   //32
        "降低国有企业",                                   //33
        "允许外来垃圾",                                   //34
        "禁止外来垃圾",                                   //35
        "增加土地税",                                   //36
        "降低土地税",                                   //37
        "投票结果",                                     //38
        "赞成",                                       //39
        "反对",                                       //40
        "弃权",                                       //41
        "目前政策",                                    //42
        "个人所得税偏差:",                              //43
        "福利指数:",                                    //44
        "交易税偏差:",                                  //45
        "进口税偏差(值越高外部进出口会减少):",                                  //46
        "国营企业百分比:",                              //47
        "允许外来垃圾:",                              //48
        "(值是相对于原MOD的,debug用)",                 //49
        "土地税偏差(和控制面板税率叠加效果):",                              //50
        };

        public static string[] RealCityUI_Chinese =
        {
            "1、城市居民所得税收入(包含食品费)",
            "居民所得税收入",
            "居民所得税总收入",
            "2、城市旅游收入",
            "来自居民",
            "来自本地游客",
            "来自外地游客",
            "居民总工资",
            "3、城市地税收入",
            "地税总收入",
            "高密度居民区地税收入",
            "高密度居民区地税收入",
            "低密度居民区地税收入",
            "低密度居民区地税收入",
            "高密度生态居民区地税收入",
            "高密度生态居民区地税收入",
            "低密度生态居民区地税收入",
            "低密度生态居民区地税收入",
            "高密度商业地税收入",
            "高密度商业地税收入",
            "低密度商业地税收入",
            "低密度商业地税收入",
            "生态商业地税收入",
            "生态商业地税收入",
            "旅游商业地税收入",
            "旅游商业地税收入",
            "娱乐商业地税收入",
            "娱乐商业地税收入",
            "一般工业地税收入",
            "一般工业地税收入",
            "农业地税收入",
            "农业地税收入",
            "林业地税收入",
            "林业地税收入",
            "石油业地税收入",
            "石油业地税收入",
            "矿业地税收入",
            "矿业地税收入",
            "一般办公区地税收入",
            "一般办公区地税收入",
            "高科技办公区地税收入",
            "高科技办公区地税收入",
            "4、城市交易税收入",
            "城市交易税总收入",
            "高密度商业交易税收入",
            "高密度商业交易税收入",
            "低密度商业交易税收入",
            "低密度商业交易税收入",
            "生态商业交易税收入",
            "生态商业交易税收入",
            "旅游商业交易税收入",
            "旅游商业交易税收入",
            "娱乐商业交易税收入",
            "娱乐商业交易税收入",
            "一般工业交易税收入",
            "一般工业交易税收入",
            "农业交易税收入",
            "农业交易税收入",
            "林业交易税收入",
            "林业交易税收入",
            "石油业交易税收入",
            "石油业交易税收入",
            "矿业交易税收入",
            "矿业交易税收入",
            "5、城市公共交通收入",
            "公交车",
            "公交车",
            "有轨电车",
            "有轨电车",
            "火车",
            "火车",
            "渡船",
            "渡船",
            "飞机",
            "飞机",
            "地铁",
            "地铁",
            "出租车",
            "出租车",
            "缆车",
            "缆车",
            "单轨",
            "单轨",
            "6、政府建筑收入",
            "政府建筑收入",
            "道路",
            "道路",
            "垃圾",
            "垃圾",
            "学校",
            "学校",
            "7、城市总收入",
            "城市收入情况(单位:克朗)",
            "城市情况",
            "如果用realtime,告诉MOD现在是不是周末,可以关闭智能交通控制功能",
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
