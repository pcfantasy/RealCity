using ICities;
using System.IO;
using RealCity.Util;
using ColossalFramework.UI;
using UnityEngine;

namespace RealCity
{
    public class RealCity : IUserMod
    {
        public static bool IsEnabled = false;
        public static bool debugMode = false;
        public static bool reduceVehicle = false;
        public static bool removeStuck = false;

        public string Name
        {
            get { return "Real City Revised"; }
        }

        public string Description
        {
            get { return "Make your city reality, Combine CS and SimCity in game playing"; }
        }

        public void OnEnabled()
        {
            IsEnabled = true;
            FileStream fs = File.Create("RealCity.txt");
            fs.Close();
            Loader.HarmonyInitDetour();
            if (UIView.GetAView() != null)
            {
                OnGameIntroLoaded();
            }
            else
            {
                LoadingManager.instance.m_introLoaded += OnGameIntroLoaded;
            }
        }

        public void OnDisabled()
        {
            Loader.HarmonyRevertDetour();
            IsEnabled = false;
            LoadingManager.instance.m_introLoaded -= OnGameIntroLoaded;
        }

        private static void OnGameIntroLoaded()
        {
            ModsCompatibilityChecker mcc = new ModsCompatibilityChecker();
            mcc.PerformModCheck();
        }

        public static void SaveSetting()
        {
            //save langugae
            FileStream fs = File.Create("RealCity_setting.txt");
            StreamWriter streamWriter = new StreamWriter(fs);
            streamWriter.WriteLine(debugMode);
            streamWriter.WriteLine(reduceVehicle);
            streamWriter.WriteLine(removeStuck);
            streamWriter.Flush();
            fs.Close();
        }

        public static void LoadSetting()
        {
            if (File.Exists("RealCity_setting.txt"))
            {
                FileStream fs = new FileStream("RealCity_setting.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string strLine = sr.ReadLine();

                if (strLine == "True")
                {
                    debugMode = true;
                }
                else
                {
                    debugMode = false;
                }

                strLine = sr.ReadLine();

                if (strLine == "True")
                {
                    reduceVehicle = true;
                }
                else
                {
                    reduceVehicle = false;
                }

                strLine = sr.ReadLine();

                if (strLine == "True")
                {
                    removeStuck = true;
                }
                else
                {
                    removeStuck = false;
                }

                sr.Close();
                fs.Close();
            }
        }

        public void OnSettingsUI(UIHelperBase helper)
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
            group.AddCheckbox(Localization.Get("SHOW_LACK_OF_RESOURCE"), debugMode, (index) => debugModeEnable(index));
            group.AddCheckbox(Localization.Get("REDUCE_CARGO_ENABLE"), reduceVehicle, (index) => reduceVehicleEnable(index));
            group.AddButton(Localization.Get("RESET_VALUE"), Loader.InitData);

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

        public void debugModeEnable(bool index)
        {
            debugMode = index;
            SaveSetting();
        }

        public void reduceVehicleEnable(bool index)
        {
            reduceVehicle = index;
            SaveSetting();
        }
    }
}

