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
        private UILabel maintainFeeTips;
        private UILabel workerStatus;

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
            this.canFocus = true;
            this.isInteractive = true;
            base.isVisible = true;
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
            this.maintainFeeTips = base.AddUIComponent<UILabel>();
            this.maintainFeeTips.text = Localization.Get("MAINTAIN_FEE_TIPS");
            this.maintainFeeTips.relativePosition = new Vector3(SPACING, 10f);
            this.maintainFeeTips.autoSize = true;

            this.workerStatus = base.AddUIComponent<UILabel>();
            this.workerStatus.text = Localization.Get("LOCAL_WORKERS_DIV_TOTAL_WORKERS");
            this.workerStatus.relativePosition = new Vector3(SPACING, this.maintainFeeTips.relativePosition.y + SPACING22);
            this.workerStatus.autoSize = true;
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
                    this.maintainFeeTips.text = Localization.Get("MAINTAIN_FEE_TIPS");
                    this.workerStatus.text = Localization.Get("LOCAL_WORKERS_DIV_TOTAL_WORKERS") + totalWorkCount.ToString() + "/" + allWorkCount.ToString();
                    PlayerBuildingUI.refeshOnce = false;
                }
                else
                {
                    this.Hide();
                }
            }

        }

    }
}
