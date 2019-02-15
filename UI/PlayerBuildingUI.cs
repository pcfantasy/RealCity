using System.Collections.Generic;
using ColossalFramework.UI;
using UnityEngine;
using ColossalFramework;
using System;
using System.Reflection;
using System.Text.RegularExpressions;
using RealCity.CustomAI;
using RealCity.Util;

namespace RealCity.UI
{
    public class PlayerBuildingUI : UIPanel
    {
        public static readonly string cacheName = "PlayerBuildingUI";

        private static readonly float SPACING = 15f;

        private static readonly float SPACING22 = 22f;

        private Dictionary<string, UILabel> _valuesControlContainer = new Dictionary<string, UILabel>(16);

        public CityServiceWorldInfoPanel baseBuildingWindow;

        public static bool refeshOnce = false;

        //1、citizen tax income
        private UILabel Food;
        private UILabel Lumber;
        private UILabel Coal;
        private UILabel Petrol;
        //private UILabel alivevisitcount;

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
            base.Hide();
        }

        private void DoOnStartup()
        {
            this.ShowOnGui();
            base.Hide();          
        }


        private void ShowOnGui()
        {
            this.Food = base.AddUIComponent<UILabel>();
            this.Food.text = Language.BuildingUI[16];
            this.Food.relativePosition = new Vector3(SPACING, 10f);
            this.Food.autoSize = true;

            this.Lumber = base.AddUIComponent<UILabel>();
            this.Lumber.text = Language.BuildingUI[17];
            this.Lumber.relativePosition = new Vector3(SPACING, this.Food.relativePosition.y + SPACING22);
            this.Lumber.autoSize = true;

            this.Coal = base.AddUIComponent<UILabel>();
            this.Coal.text = Language.BuildingUI[18];
            this.Coal.relativePosition = new Vector3(SPACING, this.Lumber.relativePosition.y + SPACING22);
            this.Coal.autoSize = true;

            this.Petrol = base.AddUIComponent<UILabel>();
            this.Petrol.text = Language.BuildingUI[19];
            this.Petrol.relativePosition = new Vector3(SPACING, this.Coal.relativePosition.y + SPACING22);
            this.Petrol.autoSize = true;
        }

        private void RefreshDisplayData()
        {
            uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
            uint num2 = currentFrameIndex & 255u;
     
            if (PlayerBuildingUI.refeshOnce || (MainDataStore.last_buildingid != WorldInfoPanel.GetCurrentInstanceID().Building))
            {
                if (base.isVisible)
                {
                    MainDataStore.last_buildingid = WorldInfoPanel.GetCurrentInstanceID().Building;
                    Building buildingData = Singleton<BuildingManager>.instance.m_buildings.m_buffer[MainDataStore.last_buildingid];

                    int aliveWorkCount = 0;
                    int totalWorkCount = 0;
                    Citizen.BehaviourData behaviour = default(Citizen.BehaviourData);
                    BuildingUI.GetWorkBehaviour((ushort)MainDataStore.last_buildingid, ref buildingData, ref behaviour, ref aliveWorkCount, ref totalWorkCount);
                    int allWorkCount = RealCityResidentAI.TotalWorkCount((ushort)MainDataStore.last_buildingid, buildingData, true, false);
                    this.Food.text = Language.BuildingUI[44];
                    this.Lumber.text = Language.BuildingUI[45] + totalWorkCount.ToString() + "/" + allWorkCount.ToString();
                    this.Coal.text = "";
                    this.Petrol.text = "";
                    PlayerBuildingUI.refeshOnce = false;
                    //this.BringToFront();
                }
                else
                {
                    this.Hide();
                }
            }

        }

    }
}
