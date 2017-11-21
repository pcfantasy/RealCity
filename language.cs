﻿using System;
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
            "Resident basic living allowances",
            "Supports for unprofitable company(not supported yet)",
            "Accept Outside Fire inspection Help Demand",
            "Accept Outside Road maintanance Demand"
        };

        public static string[] BuildingUI_English = { "Building Money",
            "Only show industry and commerical building money",
            "buildingincomebuffer",
            "Only show industry and commerical buildingincomebuffer",
            "buildingoutgoingbuffer",
            "Only show industry and commerical buildingincomebuffer",
            "aliveworkcont",
            "aliveworkcont",
            "employfee",
            "employfee",
            "landrent",
            "landrent",
            "net_asset",
            "net_asset",
            "alivevisitcount",
            "alivevisitcount",
        };

        public static string[] EconomicUI_English =
        {
            "Economic Data",
            "1、Citizen Status(unit: fen)",
            "citizen_count",
            "total citizen_count",
            "family_count",
            "total family_count",
            "citizen_salary_per_family",
            "citizen_salary_per_family",
            "salary_total",
            "total citizen_salary",
            "citizen_tax_total",
            "total_citizen_salary_tax",
            "fixed_expense_per_family",
            "citizen_expense_per_family",
            "citizen_expense",
            "total citizen_expense",
            "public_trans_fee",
            "public_transport_fee",
            "citizen_vehical_time",
            "total citizen_vehical_time",
            "family_profit_num",
            "total family_profit_money_num",
            "family_loss_num",
            "family_loss_money_num",
            "family_very_profit_num",
            "family_very_profit_num",
            "wealth_stable_high",
            "family_wealth_stable_high_num",
            "wealth_stable_low",
            "family_wealth_stable_low_num",
            "average_trans_fee",
            "citizen_average_transport_fee",
            "family total goods",
            "family total goods",
            "family satisfactios of goods",
            "family satisfactios of goods",
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
            "all_comm_building_profit num",
            "all_comm_building_profit num",
            "all_comm_building_loss num",
            "all_comm_building_loss num",
            "all_industry_building_profit",
            "all_industry_building_profit",
            "all_industry_building_loss num",
            "all_industry_building_loss num",
            "all_foresty_building_profit num",
            "all_foresty_building_profit num",
            "all_foresty_building_loss num",
            "all_foresty_building_loss num",
            "all_farmer_building_profit num",
            "all_farmer_building_profit num",
            "all_farmer_building_loss num",
            "all_farmer_building_loss num",
            "all_oil_building_profit num",
            "all_oil_building_profit num",
            "all_oil_building_loss num",
            "all_oil_building_loss num",
            "all_ore_building_profit num",
            "all_ore_building_profit num",
            "all_ore_building_loss num",
            "all_ore_building_loss num",
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
            "outside road",
            "outside total road maintenance",
            "outside fire",
            "outside total fire check",
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
        };


        public static string[] TipAndChirperMessage_English = {
        "#RealCity Anyone can help me to pay my house rent? TT",
        "#RealCity Can`t imagine house price in our city!!",
        "#RealCity Just look at the rent, I thought we were in New York.",
        "Citizen are too poor, please decrease house tax",
        "#RealCity More public transport!",
        "#RealCity I ate my breakfast at home, and ate my lunch when I just arrived workplace.",
        "Citizen are too poor, try to develop public transport and deal with traffic congestion",
        "#RealCity Sent thousands of resumes, with nothing in my mailbox, no......",
        "#RealCity I spent off my salary less than one day.",
        "Citizen are too poor, try to provide more jobs and make buildings profit",
        "#RealCity What a nice city, morning everyone",
        "Citizen seems ok",
        "#RealCity Wanna open a shop in our city, money money come on :)",
        "most of commercial buildings are profit,",
        "#RealCity Heared that the shop below my house will close down",
        "#RealCity I think there are too many shops in our city",
        "most of commercial buildings are lossing money,",
        "#RealCity industrialization city, happy with our city",
        "most of industrial buildings are profit.",
        "#RealCity Two months with discount salary, what happened with my workplace",
        "most of industrial buildings are lossing money.",
        "#RealCity Too many trucks through our city with noise but without any benefits!",
        "Can build road maintain buildings to earn road toll of feedthrough truck",
        "#RealCity Road toll, nice idea, but please use the money to improve our life.",
        "Feedthrough trucks will give us road toll now, make traffic smooth to earn money",
        "#RealCity Neighbour city is full of garbage now, wish our city will not like that in the future",
        "Can build landfiller, our neighbour city is full of garbage now",
        "#RealCity Oh, I see a lot of garbage cars moving in, any deals with outside city!",
        "A lot of garbage cars are moving in, take care of your landfiller capacity",
        "#RealCity Do you know what is the best selling of Neighbour city, it is presbyopic glasses haha...",
        "Can building cemetery ,neighbour city aging population is high and lack of cemetery",
        "#RealCity Cemetery price is higher than house in Neighbour city, they try to bury dead to our city",
        "A lot of hearse cars are moving in,take care of your cemetery capacity",
        };


        public static string[] RealCityUI_English =
        {
            "1、City resident salary-tax income",
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
            "Metro",
            "Metro",
            "Train",
            "Train",
            "Ship",
            "Ship",
            "Taxi",
            "Taxi",
            "Plane",
            "Plane",
            "Cablecar",
            "Cablecar",
            "Monorail",
            "Monorail",
            "6、City Player Building income",
            "Player Building income",
            "Road",
            "Road",
            "Cemetery",
            "Cemetery",
            "Garbage",
            "Garbage",
            "police",
            "police",
            "school",
            "school",
            "firestation",
            "firestation",
            "7、City all total income",
        };




        public static string[] BuildingUI_Chinese = { "公司现金流",
            "只显示商业和工业建筑的现金流",
            "公司原料仓库储存量",
            "只显示商业和工业建筑的原料仓库储存量",
            "公司产品仓库储存量",
            "只显示商业和工业建筑的产品仓库储存量",
            "到达公司员工",
            "到达公司员工",
            "员工工资支出",
            "员工工资支出",
            "土地费支出",
            "土地费支出",
            "公司净资产",
            "公司净资产",
            "参观人数",
            "参观人数",};

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
            "居民低保",
            "支援亏损企业(暂不支持)",
            "接受外部消防安全检查的请求",
            "接受外部道路维护的请求",
        };

        public static string[] EconomicUI_Chinese =
        {
            "经济数据",
            "1、居民情况(钱单位:分)",
            "居民人数",
            "居民总人数",
            "家庭户数",
            "家庭总户数",
            "每户居民收入",
            "每户居民收入",
            "总收入",
            "居民总收入",
            "居民总纳税",
            "居民总所得税",
            "每户固定支出",
            "居民每户固定支出",
            "居民支出",
            "居民总支出",
            "公共交通费用",
            "公共交通费用",
            "居民驾车时间",
            "居民总驾车时间",
            "家境尚可的户数",
            "家境尚可的户数",
            "欠债的户数",
            "欠债的户数",
            "家境富裕的户数",
            "家境富裕的户数",
            "财富稳定性高",
            "家庭财富稳定性高的户数",
            "财富稳定性低",
            "家庭财富稳定性低的户数",
            "平均公交费用",
            "居民平均公交费用",
            "家庭总货物",
            "家庭总货物",
            "家庭货物满足度",
            "家庭货物满足度",
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
            "城市外部垃圾",
            "城市外部总垃圾",
            "城市外部遗体",
            "城市外部总遗体",
            "城市外部病人",
            "城市外部总病人",
            "城市外部罪犯",
            "城市外部总罪犯",
            "城市外部道路维修",
            "城市外部道路维修",
            "城市外部火情",
            "城市外部火情",
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
        };


        public static string[] TipAndChirperMessage_Chinese = {
        "#真实城市 谁能帮我付房租吗？/(ㄒoㄒ)/~~",
        "#真实城市 简直不敢想象我们市的房价！！",
        "#真实城市 瞅瞅这房价, 我以为我在纽约租房呢。",
        "居民太穷，请调低税收",
        "#真实城市 需要更多的公共交通！",
        "#真实城市 我在家吃早餐, 刚到公司, 就要吃午饭了。",
        "居民太穷了，需要建立更多公共交通，处理交通拥堵问题。",
        "#真实城市 发了上千封简历，一个回复都没有，啊......",
        "#真实城市 我一天就把我工资花完了。",
        "居民太穷了，请尝试提供更多工作岗位，让更多建筑盈利。",
        "#真实城市 多棒的一座城市啊，各位早安！",
        "居民看起来还行",
        "#真实城市 想在我们市开家店，小钱钱快来啊:)",
        "大部分商业建筑都在盈利。",
        "#真实城市 听说我家楼下那家店铺要倒闭了。",
        "#真实城市 我认为咱们市商店太多了。",
        "大部分商业建筑都在亏损。",
        "#真实城市 工业化的城市，我喜欢。",
        "大部分工业建筑都在盈利",
        "#真实城市 两个月没领到全额工资了，我的单位怎么了",
        "大部分工业建筑都在亏损",
        "#真实城市 市里面太多卡车了，只带来了噪声，却没带来利益。",
        "可以建造一些道路维护站来收取过境卡车的过路费",
        "#真实城市 过路费，好主意，但请将这些钱用来造福人民",
        "过境卡车需要交过路费了，让市内交通保持畅通可以赚钱",
        "#真实城市 邻市已经垃圾成山， 希望我们市将来不会变成那样。",
        "可以建造更多垃圾填埋场来处理邻市的垃圾。",
        "#真实城市 哇，我看见好多垃圾车开进来，这里面肯定有一些交易。",
        "许多垃圾车正在将垃圾运到本市，请控制好垃圾填埋场的存储空间。",
        "#真实城市 你知道邻市销量最好的东西是什么吗？是老花镜，哈哈哈哈",
        "可以建造更多公墓，邻市老年人很多且墓地不足。",
        "#真实城市 邻市的墓地价格比房价还贵，他们都把遗体运到我们市来埋葬。",
        "许多灵柩车正将遗体运到本市，控制好墓地的容量。",
        };


        public static string[] RealCityUI_Chinese =
        {
            "1、城市居民所得税收入",
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
            "地铁",
            "地铁",
            "火车",
            "火车",
            "渡船",
            "渡船",
            "出租车",
            "出租车",
            "飞机",
            "飞机",
            "缆车",
            "缆车",
            "单轨",
            "单轨",
            "6、城市外部收入",
            "城市外部收入",
            "道路",
            "道路",
            "公墓",
            "公墓",
            "垃圾",
            "垃圾",
            "警察局",
            "警察局",
            "学校",
            "学校",
            "消防局",
            "消防局",
            "7、城市总收入",
        };


        public static string[] BuildingUI = new string[BuildingUI_English.Length];
        public static string[] OptionUI = new string[OptionUI_English.Length];
        public static string[] RealCityUI = new string[RealCityUI_Chinese.Length];
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
                    RealCityUI[i] = RealCityUI_Chinese[i];
                }
                current_language = 1;
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
                    RealCityUI[i] = RealCityUI_English[i];
                }
                current_language = 0;
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
                    RealCityUI[i] = RealCityUI_English[i];
                }
                current_language = 0;
            }

            comm_data.last_language = current_language;
        }
    }
}
