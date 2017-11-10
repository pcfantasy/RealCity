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
            "Accept Outside Police Patrol Demand(not supported yet)",
            "Accept Outside First Aid Help Demand(not supported yet)",
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
            "landrent"};







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
            "土地费支出"};

        public static string[] OptionUI_Chinese =
        {
            "语言",
            "语言选择",
            "外部连接",
            "接受外部垃圾处理的请求",
            "接受外部遗体处理的请求",
            "接受外部治安巡逻的请求(暂不支持)",
            "接受外部急救支援的请求(暂不支持)",
        };

        public static string[] BuildingUI = new string[BuildingUI_English.Length];
        public static string[] OptionUI = new string[OptionUI_English.Length];

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
                current_language = 0;
            }

            comm_data.last_language = current_language;
        }
    }
}
