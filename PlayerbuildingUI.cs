using System.Collections.Generic;
using ColossalFramework.UI;
using UnityEngine;
using ColossalFramework;
using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace RealCity
{
    public class PlayerBuildingUI : UIPanel
    {
        public static readonly string cacheName = "PlayerBuildingUI";

        private static readonly float SPACING = 15f;

        private static readonly float SPACING22 = 22f;

        private Dictionary<string, UILabel> _valuesControlContainer = new Dictionary<string, UILabel>(16);

        public CityServiceWorldInfoPanel baseBuildingWindow;

        private UILabel m_HeaderDataText;

        public static bool refesh_once = false;

        //1、citizen tax income
        private UILabel buildingmoney;
        //private UILabel alivevisitcount;

        public override void Update()
        {
            this.RefreshDisplayData();
            base.Update();
        }

        public override void Awake()
        {
            base.Awake();
            //DebugLog.LogToFileOnly("buildingUI start now");
            //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "Go to UI now");
            this.DoOnStartup();
        }

        public override void Start()
        {
            base.Start();
            //DebugLog.LogToFileOnly("buildingUI start now");
            //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "Go to UI now");
            base.backgroundSprite = "MenuPanel";
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
            this.m_HeaderDataText = base.AddUIComponent<UILabel>();
            this.m_HeaderDataText.textScale = 0.825f;
            this.m_HeaderDataText.text = string.Concat(new string[]
            {
                "Object Type    [data]"
            });
            this.m_HeaderDataText.tooltip = "N/A";
            this.m_HeaderDataText.relativePosition = new Vector3(SPACING, 50f);
            this.m_HeaderDataText.autoSize = true;

            this.buildingmoney = base.AddUIComponent<UILabel>();
            this.buildingmoney.text = "Accumulating Money [000000000000000]";
            this.buildingmoney.tooltip = language.BuildingUI[17];
            this.buildingmoney.relativePosition = new Vector3(SPACING, this.m_HeaderDataText.relativePosition.y + SPACING22);
            this.buildingmoney.autoSize = true;
            this.buildingmoney.name = "Moreeconomic_Text_0";


            /*this.alivevisitcount = base.AddUIComponent<UILabel>();
            this.alivevisitcount.text = "alivevisitcount [000000000000000]";
            this.alivevisitcount.tooltip = language.BuildingUI[15];
            this.alivevisitcount.relativePosition = new Vector3(SPACING, this.net_asset.relativePosition.y + SPACING22);
            this.alivevisitcount.autoSize = true;
            this.alivevisitcount.name = "Moreeconomic_Text_3";*/
        }

        private void RefreshDisplayData()
        {
            uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
            uint num2 = currentFrameIndex & 255u;
     
            if (((num2 == 255u) && (comm_data.current_time != comm_data.prev_time)) || PlayerBuildingUI.refesh_once || (comm_data.last_buildingid != WorldInfoPanel.GetCurrentInstanceID().Building))
            {
                //DebugLog.LogToFileOnly("buildingUI try to refreshing");
                comm_data.last_buildingid = WorldInfoPanel.GetCurrentInstanceID().Building;    
                this.buildingmoney.text = string.Format(language.BuildingUI[17] + " [{0}]", comm_data.building_money[comm_data.last_buildingid]);
                PlayerBuildingUI.refesh_once = false;
            }

        }

    }
}
