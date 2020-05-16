using ColossalFramework.UI;
using ICities;
using RealCity.Util;
using System.IO;
using UnityEngine;

namespace RealCity.UI
{
    public class OptionUI : MonoBehaviour
    {
        public static int morningBudgetWeekDay = 200;
        public static int eveningBudgetWeekDay = 200;
        public static int deepNightBudgetWeekDay = 20;
        public static int otherBudgetWeekDay = 80;
        public static int morningBudgetWeekEnd = 100;
        public static int eveningBudgetWeekEnd = 100;
        public static int deepNightBudgetWeekEnd = 20;
        public static int otherBudgetWeekEnd = 100;

        static UISlider morningBudgetWeekDaySlider;
        static UISlider eveningBudgetWeekDaySlider;
        static UISlider deepNightBudgetWeekDaySlider;
        static UISlider otherBudgetWeekDaySlider;
        static UISlider morningBudgetWeekEndSlider;
        static UISlider eveningBudgetWeekEndSlider;
        static UISlider deepNightBudgetWeekEndSlider;
        static UISlider otherBudgetWeekEndSlider;

        public static int morningBudgetMax = 250;
        public static int eveningBudgetMax = 250;
        public static int deepNightBudgetMax = 50;
        public static int otherBudgetMax = 150;
        public static int morningBudgetMin = 60;
        public static int eveningBudgetMin = 60;
        public static int deepNightBudgetMin = 10;
        public static int otherBudgetMin = 30;

        static UISlider morningBudgetMaxSlider;
        static UISlider eveningBudgetMaxSlider;
        static UISlider deepNightBudgetMaxSlider;
        static UISlider otherBudgetMaxSlider;
        static UISlider morningBudgetMinSlider;
        static UISlider eveningBudgetMinSlider;
        static UISlider deepNightBudgetMinSlider;
        static UISlider otherBudgetMinSlider;

        public static void SaveSetting()
        {
            //save langugae
            FileStream fs = File.Create("RealCity_setting.txt");
            StreamWriter streamWriter = new StreamWriter(fs);
            streamWriter.WriteLine(RealCity.debugMode);
            streamWriter.WriteLine(RealCity.reduceVehicle);
            streamWriter.WriteLine(RealCity.realCityV10);
            streamWriter.WriteLine(morningBudgetWeekDay);
            streamWriter.WriteLine(eveningBudgetWeekDay);
            streamWriter.WriteLine(deepNightBudgetWeekDay);
            streamWriter.WriteLine(otherBudgetWeekDay);
            streamWriter.WriteLine(morningBudgetWeekEnd);
            streamWriter.WriteLine(eveningBudgetWeekEnd);
            streamWriter.WriteLine(deepNightBudgetWeekEnd);
            streamWriter.WriteLine(otherBudgetWeekEnd);
            streamWriter.WriteLine(morningBudgetMax);
            streamWriter.WriteLine(eveningBudgetMax);
            streamWriter.WriteLine(deepNightBudgetMax);
            streamWriter.WriteLine(otherBudgetMax);
            streamWriter.WriteLine(morningBudgetMin);
            streamWriter.WriteLine(eveningBudgetMin);
            streamWriter.WriteLine(deepNightBudgetMin);
            streamWriter.WriteLine(otherBudgetMin);
            streamWriter.Flush();
            fs.Close();
        }

        public static void LoadSetting()
        {
            if (File.Exists("RealCity_setting.txt"))
            {
                FileStream fs = new FileStream("RealCity_setting.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string strLine;

                RealCity.debugMode = (sr.ReadLine() == "True")? true : false;

                strLine = sr.ReadLine();

                if (strLine == "True")
                {
                    RealCity.reduceVehicle = true;
                    MainDataStore.maxGoodPurchase = 500;
                }
                else
                {
                    RealCity.reduceVehicle = false;
                    MainDataStore.maxGoodPurchase = 1000;
                }

                RealCity.realCityV10 = (sr.ReadLine() == "True")? true : true; // force to true

                strLine = sr.ReadLine();
                if (!int.TryParse(strLine, out morningBudgetWeekDay)) morningBudgetWeekDay = 200;
                strLine = sr.ReadLine();
                if (!int.TryParse(strLine, out eveningBudgetWeekDay)) eveningBudgetWeekDay = 200;
                strLine = sr.ReadLine();
                if (!int.TryParse(strLine, out deepNightBudgetWeekDay)) deepNightBudgetWeekDay = 20;
                strLine = sr.ReadLine();
                if (!int.TryParse(strLine, out otherBudgetWeekDay)) otherBudgetWeekDay = 80;
                strLine = sr.ReadLine();
                if (!int.TryParse(strLine, out morningBudgetWeekEnd)) morningBudgetWeekEnd = 100;
                strLine = sr.ReadLine();
                if (!int.TryParse(strLine, out eveningBudgetWeekEnd)) eveningBudgetWeekEnd = 100;
                strLine = sr.ReadLine();
                if (!int.TryParse(strLine, out deepNightBudgetWeekEnd)) deepNightBudgetWeekEnd = 20;
                strLine = sr.ReadLine();
                if (!int.TryParse(strLine, out otherBudgetWeekEnd)) otherBudgetWeekEnd = 100;
                strLine = sr.ReadLine();

                if (!int.TryParse(strLine, out morningBudgetMax)) morningBudgetMax = 250;
                strLine = sr.ReadLine();
                if (!int.TryParse(strLine, out eveningBudgetMax)) eveningBudgetMax = 250;
                strLine = sr.ReadLine();
                if (!int.TryParse(strLine, out deepNightBudgetMax)) deepNightBudgetMax = 50;
                strLine = sr.ReadLine();
                if (!int.TryParse(strLine, out otherBudgetMax)) otherBudgetMax = 150;
                strLine = sr.ReadLine();
                if (!int.TryParse(strLine, out morningBudgetMin)) morningBudgetMin = 60;
                strLine = sr.ReadLine();
                if (!int.TryParse(strLine, out eveningBudgetMin)) eveningBudgetMin = 60;
                strLine = sr.ReadLine();
                if (!int.TryParse(strLine, out deepNightBudgetMin)) deepNightBudgetMin = 10;
                strLine = sr.ReadLine();
                if (!int.TryParse(strLine, out otherBudgetMin)) otherBudgetMin = 30;

                sr.Close();
                fs.Close();
            }
        }

        public static void MakeSettings(UIHelperBase helper)
        {
            LoadSetting();
            UIHelper actualHelper = helper as UIHelper;
            UIComponent container = actualHelper.self as UIComponent;

            UITabstrip tabStrip = container.AddUIComponent<UITabstrip>();
            tabStrip.relativePosition = new Vector3(0, 0);
            tabStrip.size = new Vector2(container.width - 20, 40);

            UITabContainer tabContainer = container.AddUIComponent<UITabContainer>();
            tabContainer.relativePosition = new Vector3(0, 40);
            tabContainer.size = new Vector2(container.width - 20, container.height - tabStrip.height - 20);
            tabStrip.tabPages = tabContainer;

            int tabIndex = 0;
            // Lane_ShortCut

            AddOptionTab(tabStrip, Localization.Get("BASIC_SETTING"));
            tabStrip.selectedIndex = tabIndex;

            UIPanel currentPanel = tabStrip.tabContainer.components[tabIndex] as UIPanel;
            currentPanel.autoLayout = true;
            currentPanel.autoLayoutDirection = LayoutDirection.Vertical;
            currentPanel.autoLayoutPadding.top = 5;
            currentPanel.autoLayoutPadding.left = 10;
            currentPanel.autoLayoutPadding.right = 10;

            UIHelper panelHelper = new UIHelper(currentPanel);

            UIHelperBase group = panelHelper.AddGroup(Localization.Get("BASIC_SETTING"));
            group.AddCheckbox(Localization.Get("SHOW_LACK_OF_RESOURCE"), RealCity.debugMode, (index) => debugModeEnable(index));
            group.AddCheckbox(Localization.Get("REDUCE_CARGO_ENABLE"), RealCity.reduceVehicle, (index) => reduceVehicleEnable(index));
            //group.AddCheckbox(Localization.Get("REALCITY_V10_BETA"), RealCity.realCityV10, (index) => realCityV10Enable(index));
            group.AddButton(Localization.Get("RESET_VALUE"), Loader.InitData);

            if (Loader.isTransportLinesManagerRunning)
            {
                UIHelperBase group1 = panelHelper.AddGroup(Localization.Get("TLMRUNNING"));
            }
            else
            {
                UIHelperBase group1 = panelHelper.AddGroup(Localization.Get("TLMNOTRUNNING"));
            }

            ++tabIndex;

            AddOptionTab(tabStrip, Localization.Get("SPTB"));
            tabStrip.selectedIndex = tabIndex;

            currentPanel = tabStrip.tabContainer.components[tabIndex] as UIPanel;
            currentPanel.autoLayout = true;
            currentPanel.autoLayoutDirection = LayoutDirection.Vertical;
            currentPanel.autoLayoutPadding.top = 5;
            currentPanel.autoLayoutPadding.left = 10;
            currentPanel.autoLayoutPadding.right = 10;

            panelHelper = new UIHelper(currentPanel);
            var generalGroup2 = panelHelper.AddGroup(Localization.Get("SMART_PUBLIC_TRANSPORT_BUDGET_WEEKDAY")) as UIHelper;
            morningBudgetWeekDaySlider = generalGroup2.AddSlider(Localization.Get("WEEKDAY_MORNING_BUDGET") + "(" + morningBudgetWeekDay.ToString() + "%)", 10, 300, 5, morningBudgetWeekDay, onMorningBudgetWeekDayChanged) as UISlider;
            morningBudgetWeekDaySlider.parent.Find<UILabel>("Label").width = 500f;
            eveningBudgetWeekDaySlider = generalGroup2.AddSlider(Localization.Get("WEEKDAY_EVENING_BUDGET") + "(" + eveningBudgetWeekDay.ToString() + "%)", 10, 300, 5, eveningBudgetWeekDay, onEveningBudgetWeekDayChanged) as UISlider;
            eveningBudgetWeekDaySlider.parent.Find<UILabel>("Label").width = 500f;
            deepNightBudgetWeekDaySlider = generalGroup2.AddSlider(Localization.Get("WEEKDAY_DEEPNIGHT_BUDGET") + "(" + deepNightBudgetWeekDay.ToString() + "%)", 10, 300, 5, deepNightBudgetWeekDay, onDeepNightBudgetWeekDayChanged) as UISlider;
            deepNightBudgetWeekDaySlider.parent.Find<UILabel>("Label").width = 500f;
            otherBudgetWeekDaySlider = generalGroup2.AddSlider(Localization.Get("WEEKDAY_OTHER_BUDGET") + "(" + otherBudgetWeekDay.ToString() + "%)", 10, 300, 5, otherBudgetWeekDay, onOtherBudgetWeekDayChanged) as UISlider;
            otherBudgetWeekDaySlider.parent.Find<UILabel>("Label").width = 500f;

            var generalGroup3 = panelHelper.AddGroup(Localization.Get("SMART_PUBLIC_TRANSPORT_BUDGET_WEEKEND")) as UIHelper;
            morningBudgetWeekEndSlider = generalGroup3.AddSlider(Localization.Get("WEEKEND_MORNING_BUDGET") + "(" + morningBudgetWeekEnd.ToString() + "%)", 10, 300, 5, morningBudgetWeekEnd, onMorningBudgetWeekEndChanged) as UISlider;
            morningBudgetWeekEndSlider.parent.Find<UILabel>("Label").width = 500f;
            eveningBudgetWeekEndSlider = generalGroup3.AddSlider(Localization.Get("WEEKEND_EVENING_BUDGET") + "(" + eveningBudgetWeekEnd.ToString() + "%)", 10, 300, 5, eveningBudgetWeekEnd, onEveningBudgetWeekEndChanged) as UISlider;
            eveningBudgetWeekEndSlider.parent.Find<UILabel>("Label").width = 500f;
            deepNightBudgetWeekEndSlider = generalGroup3.AddSlider(Localization.Get("WEEKEND_DEEPNIGHT_BUDGET") + "(" + deepNightBudgetWeekEnd.ToString() + "%)", 10, 300, 5, deepNightBudgetWeekEnd, onDeepNightBudgetWeekEndChanged) as UISlider;
            deepNightBudgetWeekEndSlider.parent.Find<UILabel>("Label").width = 500f;
            otherBudgetWeekEndSlider = generalGroup3.AddSlider(Localization.Get("WEEKEND_OTHER_BUDGET") + "(" + otherBudgetWeekEnd.ToString() + "%)", 10, 300, 5, otherBudgetWeekEnd, onOtherBudgetWeekEndChanged) as UISlider;
            otherBudgetWeekEndSlider.parent.Find<UILabel>("Label").width = 500f;

            ++tabIndex;

            AddOptionTab(tabStrip, Localization.Get("SPTB2"));
            tabStrip.selectedIndex = tabIndex;

            currentPanel = tabStrip.tabContainer.components[tabIndex] as UIPanel;
            currentPanel.autoLayout = true;
            currentPanel.autoLayoutDirection = LayoutDirection.Vertical;
            currentPanel.autoLayoutPadding.top = 5;
            currentPanel.autoLayoutPadding.left = 10;
            currentPanel.autoLayoutPadding.right = 10;

            panelHelper = new UIHelper(currentPanel);
            var generalGroup4 = panelHelper.AddGroup(Localization.Get("SMART_PUBLIC_TRANSPORT_BUDGET_MAX")) as UIHelper;
            morningBudgetMaxSlider = generalGroup4.AddSlider(Localization.Get("MAX_MORNING_BUDGET") + "(" + morningBudgetMax.ToString() + "%)", 10, 300, 5, morningBudgetMax, onMorningBudgetMaxChanged) as UISlider;
            morningBudgetMaxSlider.parent.Find<UILabel>("Label").width = 500f;
            eveningBudgetMaxSlider = generalGroup4.AddSlider(Localization.Get("MAX_EVENING_BUDGET") + "(" + eveningBudgetMax.ToString() + "%)", 10, 300, 5, eveningBudgetMax, onEveningBudgetMaxChanged) as UISlider;
            eveningBudgetMaxSlider.parent.Find<UILabel>("Label").width = 500f;
            deepNightBudgetMaxSlider = generalGroup4.AddSlider(Localization.Get("MAX_DEEPNIGHT_BUDGET") + "(" + deepNightBudgetMax.ToString() + "%)", 10, 300, 5, deepNightBudgetMax, onDeepNightBudgetMaxChanged) as UISlider;
            deepNightBudgetMaxSlider.parent.Find<UILabel>("Label").width = 500f;
            otherBudgetMaxSlider = generalGroup4.AddSlider(Localization.Get("MAX_OTHER_BUDGET") + "(" + otherBudgetMax.ToString() + "%)", 10, 300, 5, otherBudgetMax, onOtherBudgetMaxChanged) as UISlider;
            otherBudgetMaxSlider.parent.Find<UILabel>("Label").width = 500f;

            var generalGroup5 = panelHelper.AddGroup(Localization.Get("SMART_PUBLIC_TRANSPORT_BUDGET_MIN")) as UIHelper;
            morningBudgetMinSlider = generalGroup5.AddSlider(Localization.Get("MIN_MORNING_BUDGET") + "(" + morningBudgetMin.ToString() + "%)", 10, 300, 5, morningBudgetMin, onMorningBudgetMinChanged) as UISlider;
            morningBudgetMinSlider.parent.Find<UILabel>("Label").width = 500f;
            eveningBudgetMinSlider = generalGroup5.AddSlider(Localization.Get("MIN_EVENING_BUDGET") + "(" + eveningBudgetMin.ToString() + "%)", 10, 300, 5, eveningBudgetMin, onEveningBudgetMinChanged) as UISlider;
            eveningBudgetMinSlider.parent.Find<UILabel>("Label").width = 500f;
            deepNightBudgetMinSlider = generalGroup5.AddSlider(Localization.Get("MIN_DEEPNIGHT_BUDGET") + "(" + deepNightBudgetMin.ToString() + "%)", 10, 300, 5, deepNightBudgetMin, onDeepNightBudgetMinChanged) as UISlider;
            deepNightBudgetMinSlider.parent.Find<UILabel>("Label").width = 500f;
            otherBudgetMinSlider = generalGroup5.AddSlider(Localization.Get("MIN_OTHER_BUDGET") + "(" + otherBudgetMin.ToString() + "%)", 10, 300, 5, otherBudgetMin, onOtherBudgetMinChanged) as UISlider;
            otherBudgetMinSlider.parent.Find<UILabel>("Label").width = 500f;

            SaveSetting();
        }

        private static UIButton AddOptionTab(UITabstrip tabStrip, string caption)
        {
            UIButton tabButton = tabStrip.AddTab(caption);

            tabButton.normalBgSprite = "SubBarButtonBase";
            tabButton.disabledBgSprite = "SubBarButtonBaseDisabled";
            tabButton.focusedBgSprite = "SubBarButtonBaseFocused";
            tabButton.hoveredBgSprite = "SubBarButtonBaseHovered";
            tabButton.pressedBgSprite = "SubBarButtonBasePressed";

            tabButton.textPadding = new RectOffset(10, 10, 10, 10);
            tabButton.autoSize = true;
            tabButton.tooltip = caption;

            return tabButton;
        }

        public static void debugModeEnable(bool index)
        {
            RealCity.debugMode = index;
            SaveSetting();
        }

        public static void realCityV10Enable(bool index)
        {
            RealCity.realCityV10 = index;
            SaveSetting();
        }

        public static void reduceVehicleEnable(bool index)
        {
            RealCity.reduceVehicle = index;
            if (RealCity.reduceVehicle)
                MainDataStore.maxGoodPurchase = 500;
            else
                MainDataStore.maxGoodPurchase = 1000;
            SaveSetting();
        }

        private static void onMorningBudgetWeekDayChanged(float newVal)
        {
            morningBudgetWeekDay = (int)newVal;
            morningBudgetWeekDaySlider.tooltip = newVal.ToString();
            morningBudgetWeekDaySlider.parent.Find<UILabel>("Label").text = Localization.Get("WEEKDAY_MORNING_BUDGET") + "(" + morningBudgetWeekDay.ToString() + "%)";
            SaveSetting();
        }

        private static void onEveningBudgetWeekDayChanged(float newVal)
        {
            eveningBudgetWeekDay = (int)newVal;
            eveningBudgetWeekDaySlider.tooltip = newVal.ToString();
            eveningBudgetWeekDaySlider.parent.Find<UILabel>("Label").text = Localization.Get("WEEKDAY_EVENING_BUDGET") + "(" + eveningBudgetWeekDay.ToString() + "%)";
            SaveSetting();
        }

        private static void onDeepNightBudgetWeekDayChanged(float newVal)
        {
            deepNightBudgetWeekDay = (int)newVal;
            deepNightBudgetWeekDaySlider.tooltip = newVal.ToString();
            deepNightBudgetWeekDaySlider.parent.Find<UILabel>("Label").text = Localization.Get("WEEKDAY_DEEPNIGHT_BUDGET") + "(" + deepNightBudgetWeekDay.ToString() + "%)";
            SaveSetting();
        }

        private static void onOtherBudgetWeekDayChanged(float newVal)
        {
            otherBudgetWeekDay = (int)newVal;
            otherBudgetWeekDaySlider.tooltip = newVal.ToString();
            otherBudgetWeekDaySlider.parent.Find<UILabel>("Label").text = Localization.Get("WEEKDAY_OTHER_BUDGET") + "(" + otherBudgetWeekDay.ToString() + "%)";
            SaveSetting();
        }

        private static void onMorningBudgetWeekEndChanged(float newVal)
        {
            morningBudgetWeekEnd = (int)newVal;
            morningBudgetWeekEndSlider.tooltip = newVal.ToString();
            morningBudgetWeekEndSlider.parent.Find<UILabel>("Label").text = Localization.Get("WEEKDAY_MORNING_BUDGET") + "(" + morningBudgetWeekEnd.ToString() + "%)";
            SaveSetting();
        }

        private static void onEveningBudgetWeekEndChanged(float newVal)
        {
            eveningBudgetWeekEnd = (int)newVal;
            eveningBudgetWeekEndSlider.tooltip = newVal.ToString();
            eveningBudgetWeekEndSlider.parent.Find<UILabel>("Label").text = Localization.Get("WEEKEND_EVENING_BUDGET") + "(" + eveningBudgetWeekEnd.ToString() + "%)";
            SaveSetting();
        }

        private static void onDeepNightBudgetWeekEndChanged(float newVal)
        {
            deepNightBudgetWeekEnd = (int)newVal;
            deepNightBudgetWeekEndSlider.tooltip = newVal.ToString();
            deepNightBudgetWeekEndSlider.parent.Find<UILabel>("Label").text = Localization.Get("WEEKEND_DEEPNIGHT_BUDGET") + "(" + deepNightBudgetWeekEnd.ToString() + "%)";
            SaveSetting();
        }

        private static void onOtherBudgetWeekEndChanged(float newVal)
        {
            otherBudgetWeekEnd = (int)newVal;
            otherBudgetWeekEndSlider.tooltip = newVal.ToString();
            otherBudgetWeekEndSlider.parent.Find<UILabel>("Label").text = Localization.Get("WEEKEND_OTHER_BUDGET") + "(" + otherBudgetWeekEnd.ToString() + "%)";
            SaveSetting();
        }

        private static void onMorningBudgetMaxChanged(float newVal)
        {
            morningBudgetMax = (int)newVal;
            morningBudgetMaxSlider.tooltip = newVal.ToString();
            morningBudgetMaxSlider.parent.Find<UILabel>("Label").text = Localization.Get("MAX_MORNING_BUDGET") + "(" + morningBudgetMax.ToString() + "%)";
            SaveSetting();
        }

        private static void onEveningBudgetMaxChanged(float newVal)
        {
            eveningBudgetMax = (int)newVal;
            eveningBudgetMaxSlider.tooltip = newVal.ToString();
            eveningBudgetMaxSlider.parent.Find<UILabel>("Label").text = Localization.Get("MAX_EVENING_BUDGET") + "(" + eveningBudgetMax.ToString() + "%)";
            SaveSetting();
        }

        private static void onDeepNightBudgetMaxChanged(float newVal)
        {
            deepNightBudgetMax = (int)newVal;
            deepNightBudgetMaxSlider.tooltip = newVal.ToString();
            deepNightBudgetMaxSlider.parent.Find<UILabel>("Label").text = Localization.Get("MAX_DEEPNIGHT_BUDGET") + "(" + deepNightBudgetMax.ToString() + "%)";
            SaveSetting();
        }

        private static void onOtherBudgetMaxChanged(float newVal)
        {
            otherBudgetMax = (int)newVal;
            otherBudgetMaxSlider.tooltip = newVal.ToString();
            otherBudgetMaxSlider.parent.Find<UILabel>("Label").text = Localization.Get("MAX_OTHER_BUDGET") + "(" + otherBudgetMax.ToString() + "%)";
            SaveSetting();
        }

        private static void onMorningBudgetMinChanged(float newVal)
        {
            morningBudgetMin = (int)newVal;
            morningBudgetMinSlider.tooltip = newVal.ToString();
            morningBudgetMinSlider.parent.Find<UILabel>("Label").text = Localization.Get("MIN_MORNING_BUDGET") + "(" + morningBudgetMin.ToString() + "%)";
            SaveSetting();
        }

        private static void onEveningBudgetMinChanged(float newVal)
        {
            eveningBudgetMin = (int)newVal;
            eveningBudgetMinSlider.tooltip = newVal.ToString();
            eveningBudgetMinSlider.parent.Find<UILabel>("Label").text = Localization.Get("MIN_EVENING_BUDGET") + "(" + eveningBudgetMin.ToString() + "%)";
            SaveSetting();
        }

        private static void onDeepNightBudgetMinChanged(float newVal)
        {
            deepNightBudgetMin = (int)newVal;
            deepNightBudgetMinSlider.tooltip = newVal.ToString();
            deepNightBudgetMinSlider.parent.Find<UILabel>("Label").text = Localization.Get("MIN_DEEPNIGHT_BUDGET") + "(" + deepNightBudgetMin.ToString() + "%)";
            SaveSetting();
        }

        private static void onOtherBudgetMinChanged(float newVal)
        {
            otherBudgetMin = (int)newVal;
            otherBudgetMinSlider.tooltip = newVal.ToString();
            otherBudgetMinSlider.parent.Find<UILabel>("Label").text = Localization.Get("MIN_OTHER_BUDGET") + "(" + otherBudgetMin.ToString() + "%)";
            SaveSetting();
        }
    }
}
