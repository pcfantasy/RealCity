using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCity
{
    public class language
    {
        public static string[] OptionUI_English =
        {
            "Language",
            "Language_Select",
            "Smart Public Transport(more cars and maintenance in peek, less in troughs)",
        };

        public static string[] BuildingUI_English = {
            "Building Money(unit:1/100₡)",
            "Only show industry and commerical building money",
            "Material Buffer",
            "Only show industry and commerical Material Buffer",
            "Production Buffer",
            "Only show industry and commerical Production Buffer",
            "aliveworkcount",
            "aliveworkcount",
            "average employfee(resident time-period)",
            "average employfee(resident time-period)",
            "Building landrent",
            "Building landrent",
            "Building net asset",
            "Building net asset",
            "Family Money(unit:1/100₡)",
            "Only show Family money",
            "(profit-sharing)",
            "Accumulating Money(unit:₡)",
            "Buy price:",
            "Sell price:",
            "Family Salary:",
            "Material/Production:",
            "Sell tax",
            "BuyToSell Profit",
            "(exclude visit income)",
            "Family status:",
            "No toll department",
            "Food stored",
            "Lumber stored",
            "Coal stored",
            "Petrol stored",
        };

        public static string[] EconomicUI_English =
        {
            "Economic Data",
            "1、Citizen Status(unit: 1/100₡)",
            "citizen_count",    //1
            "total citizen_count",
            "family_count",    //2
            "total family_count",
            "salary_per_family",
            "citizen_salary_per_family",
            "citizen_tax_per_family",   //1
            "total_citizen_salary_tax_per_family",
            "expense_per_family",    //2
            "fixed_expense_per_family",
            "average_trans_fee",
            "citizen_average_transport_fee",
            "public_trans_fee",
            "public_transport_fee",
            "citizen_vehical_time",
            "total citizen_vehical_time",
            "high_salary",    //1
            "family_high_salary_num",
            "medium_salary",    //2
            "total family_medium_salary_num",
            "low_salary",
            "family_low_salary_num",
            "wealth_high",    //1
            "family_wealth_stable_high_num",
            "wealth_medium",    //2
            "family_wealth_stable_high_num",
            "wealth_low",
            "family_wealth_stable_low_num",
            "satisfactions of goods",
            "family satisfactions of goods",
            "2、Building Status",
            "very profit I&C building",
            "very profit I&C building",
            "external money for office",
            "external money for office",
            "tip1",
            "tip1",
            "tip2",
            "tip2",
            "tip3",
            "tip3",
            "tip4",
            "tip4",
            "tip5",
            "tip5",
            "tip6",
            "tip6",
            "tip7",
            "tip7",
            "tip8",
            "tip8",
            "tip9",
            "tip9",
            "tip10"
        };


        public static string[] TipAndChirperMessage_English = {
        "Citizen are too poor, please decrease house tax",
        "Citizen are too poor, try to develop public transport and deal with traffic congestion",
        "Citizen are too poor, try to provide more jobs and make buildings profit",
        "Citizen seems ok",
        "Better set real time time speed to slowest",
        "Better use TMPE and disable vehicle spawn",
        "Better use Citizen Lifecycle Rebalance and set lifespan to 16, see Q/A in workshop",
        "Can use Favorite Cims mod to watch every citizens and their family",
        };


        public static string[] RealCityUI_English =
        {
            "1、City resident salary-tax income(include all insurance)",
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
             "原料买入价格:",
             "产品卖出价格:",
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
             "存储的汽油",};

    public static string[] OptionUI_Chinese =
        {
            "语言",
            "语言选择",
            "智能公共交通(早晚高峰车多维护费多,深夜钱少维护费少)",
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
            "非常赚钱的公司数目",
            "非常赚钱的公司数目",
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
        "居民太穷，请调低税收",
        "居民太穷了，需要建立更多公共交通，处理交通拥堵问题。",
        "居民太穷了，请尝试提供更多工作岗位，让更多建筑盈利。",
        "居民看起来还行",
        "最好把RealTime的时间速度改成最慢",
        "最好用TMPE并且禁止车辆消失",
        "最好用Citizen Lifecycle Rebalance并把lifespan改成16(看工坊Q/A)",
        "可以用Favorite Cims mod看每个居民和他们家庭的情况",
        };


        public static string[] RealCityUI_Chinese =
        {
            "1、城市居民所得税收入(包含养老失业医疗保险)",
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
            "城市情况"
        };


        public static string[] BuildingUI = new string[BuildingUI_English.Length];
        public static string[] OptionUI = new string[OptionUI_English.Length];
        public static string[] RealCityUI1 = new string[RealCityUI_Chinese.Length];
        public static string[] TipAndChirperMessage = new string[TipAndChirperMessage_Chinese.Length];
        public static string[] EconomicUI = new string[EconomicUI_Chinese.Length];

        public static byte current_language = 255; 

        public static void language_switch (byte language)
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
                current_language = 1;
                MoreeconomicUI.WIDTH = 650;
                RealCityUI.WIDTH = 700;
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
                current_language = 0;
                MoreeconomicUI.WIDTH = 800;
                RealCityUI.WIDTH = 850;
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
                current_language = 0;
                MoreeconomicUI.WIDTH = 800;
                RealCityUI.WIDTH = 850;
            }

            comm_data.last_language = current_language;
        }
    }
}
