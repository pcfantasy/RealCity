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
        private UILabel aliveworkcount;
        private UILabel employfee;
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
            this.BringToFront();
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
            this.buildingmoney.text = language.BuildingUI[17];
            this.buildingmoney.tooltip = language.BuildingUI[17];
            this.buildingmoney.relativePosition = new Vector3(SPACING, this.m_HeaderDataText.relativePosition.y + SPACING22);
            this.buildingmoney.autoSize = true;
            this.buildingmoney.name = "Moreeconomic_Text_0";

            this.aliveworkcount = base.AddUIComponent<UILabel>();
            this.aliveworkcount.text = language.BuildingUI[6];
            this.aliveworkcount.tooltip = language.BuildingUI[7];
            this.aliveworkcount.relativePosition = new Vector3(SPACING, this.buildingmoney.relativePosition.y + SPACING22);
            this.aliveworkcount.autoSize = true;
            this.aliveworkcount.name = "Moreeconomic_Text_3";

            this.employfee = base.AddUIComponent<UILabel>();
            this.employfee.text = language.BuildingUI[8];
            this.employfee.tooltip = language.BuildingUI[9];
            this.employfee.relativePosition = new Vector3(SPACING, this.aliveworkcount.relativePosition.y + SPACING22);
            this.employfee.autoSize = true;
            this.employfee.name = "Moreeconomic_Text_4";


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
     
            if (PlayerBuildingUI.refesh_once  || (comm_data.last_buildingid != WorldInfoPanel.GetCurrentInstanceID().Building))
            {
                //DebugLog.LogToFileOnly("buildingUI try to refreshing");
                if (base.isVisible)
                {
                    comm_data.last_buildingid = WorldInfoPanel.GetCurrentInstanceID().Building;

                    Building buildingdata = Singleton<BuildingManager>.instance.m_buildings.m_buffer[comm_data.last_buildingid];
                    int aliveWorkerCount = 0;
                    int totalWorkerCount = 0;
                    float num = caculate_employee_outcome(buildingdata, comm_data.last_buildingid, out aliveWorkerCount, out totalWorkerCount);
                    this.aliveworkcount.text = string.Format(language.BuildingUI[6] + " [{0}]", aliveWorkerCount);
                    this.employfee.text = string.Format(language.BuildingUI[8] + " [{0:N2}]", (int)num);
                    this.buildingmoney.text = string.Format(language.BuildingUI[17] + " [{0}]", comm_data.building_money[comm_data.last_buildingid]);
                    PlayerBuildingUI.refesh_once = false;
                    this.BringToFront();
                }
            }

        }


        public float caculate_employee_outcome(Building building, ushort buildingID, out int aliveWorkerCount, out int totalWorkerCount)
        {
            float num1 = 0;
            Citizen.BehaviourData behaviour = default(Citizen.BehaviourData);
            aliveWorkerCount = 0;
            totalWorkerCount = 0;
            BuildingUI.GetWorkBehaviour(buildingID, ref building, ref behaviour, ref aliveWorkerCount, ref totalWorkerCount);
            num1 = (int)(behaviour.m_educated0Count * comm_data.road_education0 + behaviour.m_educated1Count * comm_data.road_education1 + behaviour.m_educated2Count * comm_data.road_education2 + behaviour.m_educated3Count * comm_data.road_education3);
                    
            System.Random rand = new System.Random();

            //add random value to match citizen salary
            if (num1 != 0)
            {
                num1 += (behaviour.m_educated0Count * rand.Next(1) + behaviour.m_educated1Count * rand.Next(2) + behaviour.m_educated2Count * rand.Next(3) + behaviour.m_educated3Count * rand.Next(4));
            }
            int budget = Singleton<EconomyManager>.instance.GetBudget(building.Info.m_class);
            num1 = (int)(num1 * budget / 100f);
            if (totalWorkerCount > 0)
            {
                num1 = num1 / totalWorkerCount;
            } else
            {
                num1 = 0;
            }
            return num1;
        }

    }
}
