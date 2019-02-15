using System.Collections.Generic;
using ColossalFramework.UI;
using UnityEngine;
using ColossalFramework;
using System;
using System.Reflection;
using RealCity.Util;

namespace RealCity.UI
{
    public class TouristUI : UIPanel
    {
        public static readonly string cacheName = "TouristUI";

        private static readonly float SPACING = 15f;

        private Dictionary<string, UILabel> _valuesControlContainer = new Dictionary<string, UILabel>(16);

        public TouristWorldInfoPanel baseBuildingWindow;

        public static bool refeshOnce = false;

        private UILabel TouristMoney;

        public override void Update()
        {
            this.RefreshDisplayData();
            base.Update();
        }

        public override void Awake()
        {
            base.Awake();
            this.DoOnStartup();
        }

        public override void Start()
        {
            base.Start();
            //base.backgroundSprite = "MenuPanel";
            this.canFocus = true;
            this.isInteractive = true;
            base.isVisible = true;
            //this.BringToFront();
            base.opacity = 1f;
            base.cachedName = cacheName;
            this.RefreshDisplayData();
        }

        private void DoOnStartup()
        {
            this.ShowOnGui();            
        }


        private void ShowOnGui()
        { 
            this.TouristMoney = base.AddUIComponent<UILabel>();
            this.TouristMoney.text = Language.BuildingUI[46];
            this.TouristMoney.relativePosition = new Vector3(SPACING, 50f);
            this.TouristMoney.autoSize = true;
        }

        private void RefreshDisplayData()
        {
            uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
            uint num2 = currentFrameIndex & 255u;

            if (refeshOnce || (MainDataStore.last_citizenid != WorldInfoPanel.GetCurrentInstanceID().Citizen))
            {
                if (base.isVisible)
                {
                    MainDataStore.last_citizenid = WorldInfoPanel.GetCurrentInstanceID().Citizen;
                    this.TouristMoney.text = string.Format(Language.BuildingUI[46] + " [{0}]", MainDataStore.citizenMoney[MainDataStore.last_citizenid]);
                    TouristUI.refeshOnce = false;
                }
            }
        }
    }
}
