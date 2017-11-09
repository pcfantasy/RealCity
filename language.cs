using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCity
{
    public class language
    {
        public static string[] BuildingUI_English = { "Building Money",
            "Only show industry and commerical building money",
            "buildingincomebuffer",
            "Only show industry and commerical buildingincomebuffer",
            "buildingoutgoingbuffer",
            "Only show industry and commerical buildingincomebuffer"};

        public static string[] BuildingUI_Chinese = { "公司现金流",
            "只显示商业和工业建筑的现金流",
            "公司原料仓库储存量",
            "只显示商业和工业建筑的原料仓库储存量",
            "公司产品仓库储存量",
            "只显示商业和工业建筑的产品仓库储存量"};

        public static string[] BuildingUI = new string[BuildingUI_English.Length];

        public static byte current_language = 255; 

        public static void language_switch (byte language)
        {
            if (language == 1)
            {
                for (int i = 0; i < BuildingUI_English.Length; i++)
                {
                    BuildingUI[i] = BuildingUI_Chinese[i];
                }
                current_language = 1;
            }
            else if (language == 0)
            {
                for (int i = 0; i < BuildingUI_English.Length; i++)
                {
                    BuildingUI[i] = BuildingUI_English[i];
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
                current_language = 0;
            }

            comm_data.last_language = current_language;
        }
    }
}
