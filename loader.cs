using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using UnityEngine;

namespace RealCity
{
    public class Settings
    {
        public bool LogInfoToFile = false;
    }

    public static class ConstantsManager
    {
        public static void LoadSettings()
        {
            Settings Settings;
            Settings = new Settings();
        }
    }

    public class Loader : LoadingExtensionBase
    {
        public static UIView parentGuiView;

        public static MoreeconomicUI guiPanel;

        public static RealCityUI guiPanel1;

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
                if (mode == LoadMode.LoadGame || mode == LoadMode.NewGame || mode == LoadMode.LoadMap || mode == LoadMode.NewMap)
                {
                    ConstantsManager.LoadSettings();
                    Loader.SetupGui();
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
                Loader.RemoveGui();
            }
        }

        public override void OnReleased()
        {
            base.OnReleased();
        }

        public static void SetupGui()
        {
            Loader.parentGuiView = null;
            Loader.parentGuiView = UIView.GetAView();
            if (Loader.guiPanel == null)
            {
                Loader.guiPanel = (MoreeconomicUI)Loader.parentGuiView.AddUIComponent(typeof(MoreeconomicUI));
            }
            if (Loader.guiPanel1 == null)
            {
                Loader.guiPanel1 = (RealCityUI)Loader.parentGuiView.AddUIComponent(typeof(RealCityUI));
            }
            Loader.isGuiRunning = true;
        }

        public static void RemoveGui()
        {
            Loader.isGuiRunning = false;
            if (Loader.parentGuiView != null)
            {
                Loader.parentGuiView = null;
            }
        }
    }
}
