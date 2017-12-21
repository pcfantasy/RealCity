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
            "Outside Connection",
            "Accept Outside Garbage Deal Demand",
            "Accept Outside Dead Deal Demand",
            "Accept Outside Police Patrol Demand",
            "Accept Outside First Aid Help Demand",
            "Special Policy",
            "Resident endowment & unemployed insurance",
            "Smart Public Transport(more cars and maintenance in peek, less in troughs)",
            "Accept Outside Fire inspection Help Demand",
            "Accept Outside Road maintanance Demand",
            "Accept Outside Fire Help(will not go outside for Fire inspection Help Demand)",            
            "Accept Outside Hospital Help(will not go outside for First Aid Help Demand)",
            "Accept Outside Police Help(will not go outside for Police Patrol Demand)",
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
            "Family status:"
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
            "insurance_account(delta)",
            "city_insurance_account(delta)",
            "2、Building Status",
            "good_export_ratio",
            "good_export_ratio",
            "food_export_ratio",
            "food_export_ratio",
            "petrol_export_ratio",
            "petrol_export_ratio",
            "coal_export_ratio",
            "coal_export_ratio",
            "lumber_export_ratio",
            "lumber_export_ratio",
            "oil_export_ratio",
            "oil_export_ratio",
            "ore_export_ratio",
            "ore_export_ratio",
            "log_export_ratio",
            "log_export_ratio",
            "grain_export_ratio",
            "grain_export_ratio",
            "good_import_ratio",
            "good_import_ratio",
            "food_import_ratio",
            "food_import_ratio",
            "petrol_import_ratio",
            "petrol_import_ratio",
            "coal_import_ratio",
            "coal_import_ratio",
            "lumber_import_ratio",
            "lumber_import_ratio",
            "oil_import_ratio",
            "oil_import_ratio",
            "ore_import_ratio",
            "ore_import_ratio",
            "log_import_ratio",
            "log_import_ratio",
            "grain_import_ratio",
            "grain_import_ratio",
            "commerical_profit num",
            "commerical_profit num",
            "commerical_loss num",
            "commerical_loss num",
            "industrial_profit num",
            "industrial_profit num",
            "industrial_loss num",
            "industrial_loss num",
            "foresty_profit num",
            "foresty_profit num",
            "foresty_loss num",
            "foresty_loss num",
            "farming_profit num",
            "farming_profit num",
            "farming_loss num",
            "farming_loss num",
            "oil_profit num",
            "oil_profit num",
            "oil_loss num",
            "oil_loss num",
            "ore_profit num",
            "ore_profit num",
            "ore_loss num",
            "ore_loss num",
            "very profit company num",
            "very profit company num",
            "external money for office",
            "external money for office",
            "3、Outside Situation",
            "Outside Situation",
            "outside garbage",
            "outside total garbage",
            "outside dead",
            "outside total dead",
            "outside sick",
            "outside total dead",
            "outside crime",
            "outside total crime",
            "outside road condition",
            "outside total road condition",
            "outside fire",
            "outside total fire check",
            "outside hospital compacity",
            "outside hospital compacity",
            "outside ambulance car num",
            "outside ambulance car num",
            "outside jail compacity",
            "outside jail compacity",
            "outside policecar num",
            "outside policecar num",
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
        };


        public static string[] TipAndChirperMessage_English = {
        "Citizen are too poor, please decrease house tax",
        "Citizen are too poor, try to develop public transport and deal with traffic congestion",
        "Citizen are too poor, try to provide more jobs and make buildings profit",
        "Citizen seems ok",
        "Better set rush hour time speed to 0.125x",
        "Better use TMPE and disable vehicle spawn",
        "Better use Citizen Lifecycle Rebalance and set lifespan to 16, see Q/A in workshop",
        "Can use Favorite Cims mod to watch every citizens and their family",
        "In earlier game, can use outside hospital and policestation for help.",
        "industry oil and ore is too many, resident may not want to move in",
        "Current Event:",
        "Lack of goods ",
        "High price of goods ",
        "High demand ",
        "Low demand ",
        "Virus attack ",
        "Refugees ",
        "Rich immigrants ",
        "Hot Money ",
        "Money flowout ",
        "Oil ",
        "Ore ",
        "Grain ",
        "Logs ",
        "Food ",
        "Lumber ",
        "Petrol ",
        "Coal ",
        "Remaining time:",
        "No random event now(NSP yet, coming soon)",
        "City Bank lack of money, No CI demand and outside movingin resident will be poor man"   
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
            "HealthCare",
            "HealthCare",
            "Garbage",
            "Garbage",
            "Police",
            "Police",
            "School",
            "School",
            "Firestation",
            "Firestation",
            "7、City all total income",
            "City income data(unit:₡)",
            "Spercial Task(once at a time)",
            "infinity garbage,receive 200 garbagecars in 1000time,get 90K(cd time 2000,when family>500)",
            "infinity dead,receive 200 hearsecars in 1000time,get 50K(cd time 2500,when family>2000)",
            "crasy traffic,receive 4K feedthrough traffic in 2000time,get 70K(cd time 3000,when family>1000)",
            "remaining_time",
            "task remaining_time",
            "remaining_num",
            "task remaining_num",
            "cooldown time",
            "task cooldown time",
            "If using rush hour, tell mod if it is weekend to disable Smart Public Transport function",
            "City Data",
            "City Bank",
            "Get 1K from Bank",
            "Get 1K from Bank, if Bank have money",
            "Click to show BuildingUI"
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
             "家庭状况"};

    public static string[] OptionUI_Chinese =
        {
            "语言",
            "语言选择",
            "外部连接",
            "接受外部垃圾处理的请求",
            "接受外部遗体处理的请求",
            "接受外部治安巡逻的请求",
            "接受外部急救支援的请求",
            "城市特殊政策",
            "居民养老失业保险",
            "智能公共交通(早晚高峰车多维护费多，深夜钱少维护费少)",
            "接受外部消防安全检查的请求",
            "接受外部道路维护的请求",
            "接受外部消防帮助(就不会去外部消防检查)",
            "接受外部医院服务(就不会去外部参与急救)",
            "接受外部警务帮助(就不会去外部治安巡逻)",
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
            "城市保险账户(变动)",
            "城市保险账户(变动)",
            "2、建筑情况",
            "货物出口比例",
            "货物出口比例",
            "食品出口比例",
            "食品出口比例",
            "汽油出口比例",
            "汽油出口比例",
            "煤炭出口比例",
            "煤炭出口比例",
            "木材出口比例",
            "木材出口比例",
            "原油出口比例",
            "原油出口比例",
            "矿石出口比例",
            "矿石出口比例",
            "原木出口比例",
            "原木出口比例",
            "农产品出口比例",
            "农产品出口比例",
            "货物进口比例",
            "货物进口比例",
            "食品进口比例",
            "食品进口比例",
            "汽油进口比例",
            "汽油进口比例",
            "煤炭进口比例",
            "煤炭进口比例",
            "木材进口比例",
            "木材进口比例",
            "原油进口比例",
            "原油进口比例",
            "矿石进口比例",
            "矿石进口比例",
            "原木进口比例",
            "原木进口比例",
            "农产品进口比例",
            "农产品进口比例",
            "盈利的商铺总数",
            "盈利的商铺总数",
            "亏损的商铺总数",
            "亏损的商铺总数",
            "盈利的工厂总数",
            "盈利的工厂总数",
            "亏损的工厂总数",
            "亏损的工厂总数",
            "盈利的林业总数",
            "盈利的林业总数",
            "亏损的林业总数",
            "亏损的林业总数",
            "盈利的农场总数",
            "盈利的农场总数",
            "亏损的农场总数",
            "亏损的农场总数",
            "盈利的油井总数",
            "盈利的油井总数",
            "亏损的油井总数",
            "亏损的油井总数",
            "盈利的矿井总数",
            "盈利的矿井总数",
            "亏损的矿井总数",
            "亏损的矿井总数",
            "非常赚钱的公司数目",
            "非常赚钱的公司数目",
            "可用于投资办公的钱",
            "可用于投资办公的钱",
            "3、城市外部情况",
            "城市外部情况",
            "外部垃圾",
            "外部总垃圾",
            "外部遗体",
            "外部总遗体",
            "外部病人",
            "外部总病人",
            "外部罪犯",
            "外部总罪犯",
            "外部道路状况",
            "外部道路状况",
            "外部火情",
            "外部火情",
            "外部医院容量",
            "外部医院容量",
            "外部救护车",
            "外部救护车",
            "外部监狱容量",
            "外部监狱容量",
            "外部警车",
            "外部警车",
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
        };


        public static string[] TipAndChirperMessage_Chinese = {
        "居民太穷，请调低税收",
        "居民太穷了，需要建立更多公共交通，处理交通拥堵问题。",
        "居民太穷了，请尝试提供更多工作岗位，让更多建筑盈利。",
        "居民看起来还行",
        "最好把Rush Hour的速度改成x0.125",
        "最好用TMPE并且禁止车辆消失",
        "最好用Citizen Lifecycle Rebalance并把lifespan改成16(看工坊Q/A)",
        "可以用Favorite Cims mod看每个居民和他们家庭的情况",
        "城市发展初期,医院和警务请让外部帮助",
        "石油矿业太多了,居民可能不愿意来城市定居",
        "当前事件:",
        "缺货危机 ",
        "物价飞涨 ",
        "需求旺盛 ",
        "需求萧条 ",
        "病毒袭城 ",
        "难民危机 ",
        "富裕移民 ",
        "热钱流入 ",
        "热钱流出 ",
        "原油 ",
        "矿物 ",
        "谷物 ",
        "原木 ",
        "食品 ",
        "木材 ",
        "汽油 ",
        "煤矿 ",
        "剩余时间:",
        "当前没有随机事件(暂不支持,稍后发布)",
        "城市银行缺钱,无工业商业需求,移民全是穷人"
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
            "医疗关怀",
            "医疗关怀",
            "垃圾",
            "垃圾",
            "警察局",
            "警察局",
            "学校",
            "学校",
            "消防局",
            "消防局",
            "7、城市总收入",
            "城市收入情况(单位:克朗)",
            "特殊任务(1次只接受点击一样)",
            "垃圾围城,1000时间接受200卡车垃圾,可获得9万克朗(冷却时间2000,500家庭有效)",
            "死尸围城,1000时间接受200灵车遗体,可获得5万克朗(冷却时间2500,1500家庭有效)",
            "疯狂运输,2000时间接受4000过境车辆,可获得7万克朗(冷却时间3000,1000家庭有效)",
            "剩余时间",
            "任务剩余时间",
            "剩余数目",
            "任务剩余数目",
            "冷却时间",
            "任务冷却时间",
            "用rush hour的话,告诉本MOD今天是不是节假日,可以关掉智能公交预算功能",
            "城市情况",
            "城市银行",
            "挪用银行1K",
            "如果银行有钱,可以挪用1K",
            "点击显示建筑界面"
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
