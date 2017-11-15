using ColossalFramework.UI;
using ICities;
using UnityEngine;
using System.IO;
using ColossalFramework;
using System.Reflection;

namespace RealCity
{
    public class Loader : LoadingExtensionBase
    {
        public static UIView parentGuiView;

        public static UIPanel buildingInfo;

        public static MoreeconomicUI guiPanel;

        public static RealCityUI guiPanel1;

        private BuildingUI guiPanel2;

        public static GameObject buildingWindowGameObject;

        internal static LoadMode CurrentLoadMode;
        public static bool isGuiRunning = false;

        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);

        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            Loader.CurrentLoadMode = mode;
            if (RealCity.IsEnabled)
            {
                if (mode == LoadMode.LoadGame || mode == LoadMode.NewGame)
                {
                    SetupGui();
                    language.language_switch(comm_data.last_language);
                    if (mode == LoadMode.NewGame)
                    {
                        init_data();
                    }
                    //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "setup_gui now");
                }
            }
        }

        public void init_data()
        {
            comm_data.data_init();
            pc_EconomyManager.data_init();
        }

        public override void OnLevelUnloading()
        {
            if (RealCity.IsEnabled & Loader.isGuiRunning)
            {
                RemoveGui();
            }
        }

        public override void OnReleased()
        {
            base.OnReleased();
        }

        public void SetupGui()
        {
            Loader.parentGuiView = null;
            Loader.parentGuiView = UIView.GetAView();
            //CTRL+M debug UI
            if (Loader.guiPanel == null)
            {
                Loader.guiPanel = (MoreeconomicUI)Loader.parentGuiView.AddUIComponent(typeof(MoreeconomicUI));
            }
            //CRTL+R game UI
            if (Loader.guiPanel1 == null)
            {
                Loader.guiPanel1 = (RealCityUI)Loader.parentGuiView.AddUIComponent(typeof(RealCityUI));
            }

            //building UI
            //if (Loader.guiPanel2 == null)
            //{
            //    Loader.guiPanel2 = (BuildingUI)Loader.parentGuiView.AddUIComponent(typeof(BuildingUI));
            //}
            SetupBuidingGui();

            Loader.isGuiRunning = true;
        }

        public void SetupBuidingGui()
        {
            buildingWindowGameObject = new GameObject("buildingWindowObject");
            this.guiPanel2 = (BuildingUI)buildingWindowGameObject.AddComponent(typeof(BuildingUI));


            buildingInfo = UIView.Find<UIPanel>("(Library) ZonedBuildingWorldInfoPanel");
            if (buildingInfo == null)
            {
                DebugLog.LogToFileOnly("UIPanel not found (update broke the mod!): (Library) ZonedBuildingWorldInfoPanel\nAvailable panels are:\n");
            }
            guiPanel2.transform.parent = buildingInfo.transform;
            guiPanel2.size = new Vector3(buildingInfo.size.x, buildingInfo.size.y);
            //guiPanel2.size = new Vector3(50,50);
            guiPanel2.baseBuildingWindow = buildingInfo.gameObject.transform.GetComponentInChildren<ZonedBuildingWorldInfoPanel>();
            guiPanel2.position = new Vector3(buildingInfo.size.x, buildingInfo.size.y);
            //guiPanel2.position = new Vector3(0, 0);
            buildingInfo.eventVisibilityChanged += buildingInfo_eventVisibilityChanged;

        }

        public void buildingInfo_eventVisibilityChanged(UIComponent component, bool value)
        {
            guiPanel2.isEnabled = value;
            if (value)
            {
                //DebugLog.LogToFileOnly("select building found!!!!!:\n");
                //comm_data.current_buildingid = 0;
                BuildingUI.refesh_once = true;
                guiPanel2.Show();
            }
            else
            {
                comm_data.current_buildingid = 0;
                guiPanel2.Hide();
            }
        }


        public void RemoveGui()
        {
            Loader.isGuiRunning = false;
            if (Loader.parentGuiView != null)
            {
                Loader.parentGuiView = null;
            }

            bool flag2 = guiPanel2 != null;
            if (flag2)
            {
                bool flag3 = guiPanel2.parent != null;
                if (flag3)
                {
                    guiPanel2.parent.eventVisibilityChanged -= buildingInfo_eventVisibilityChanged;
                }
            }
            bool flag4 = buildingWindowGameObject != null;
            if (flag4)
            {
                UnityEngine.Object.Destroy(buildingWindowGameObject);
            }
        }
    }
}
