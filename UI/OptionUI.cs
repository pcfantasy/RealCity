using ColossalFramework.UI;
using ICities;
using RealCity.Util;
using System.IO;
using UnityEngine;

namespace RealCity.UI
{
    public class OptionUI : MonoBehaviour
    {

        public static void SaveSetting()
        {
            //save langugae
            FileStream fs = File.Create("RealCity_setting.txt");
            StreamWriter streamWriter = new StreamWriter(fs);
            streamWriter.WriteLine(RealCity.debugMode);
            streamWriter.WriteLine(RealCity.reduceVehicle);
            streamWriter.WriteLine(RealCity.noPassengerCar);
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

                RealCity.noPassengerCar = (sr.ReadLine() == "True")? true : false;
               
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
            group.AddCheckbox(Localization.Get("NO_PASSENGERCAR"), RealCity.noPassengerCar, (index) => noPassengerCarEnable(index));
            group.AddButton(Localization.Get("RESET_VALUE"), Loader.InitData);

            if (Loader.isTransportLinesManagerRunning)
            {
                UIHelperBase group1 = panelHelper.AddGroup(Localization.Get("TLMRUNNING"));
            }
            else
            {
                UIHelperBase group1 = panelHelper.AddGroup(Localization.Get("TLMNOTRUNNING"));
            }

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

        public static void noPassengerCarEnable(bool index)
        {
            RealCity.noPassengerCar = index;
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
    }
}
