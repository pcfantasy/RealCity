using System.Collections.Generic;
using ColossalFramework.UI;
using UnityEngine;
using ColossalFramework;
using System;
using System.Reflection;

namespace RealCity
{
    public class HumanUI : UIPanel
    {
        public static readonly string cacheName = "HumanUI";

        private static readonly float SPACING = 15f;

        private static readonly float SPACING22 = 22f;

        private Dictionary<string, UILabel> _valuesControlContainer = new Dictionary<string, UILabel>(16);

        public CitizenWorldInfoPanel baseBuildingWindow;

        public static bool refeshOnce = false;

        private UILabel familyMoney;
        private UILabel familySalary;

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
            this.familyMoney = base.AddUIComponent<UILabel>();
            this.familyMoney.text = Language.BuildingUI[14];
            this.familyMoney.tooltip = Language.BuildingUI[15];
            this.familyMoney.relativePosition = new Vector3(SPACING, 50f);
            this.familyMoney.autoSize = true;
            this.familyMoney.name = "Moreeconomic_Text_0";

            this.familySalary = base.AddUIComponent<UILabel>();
            this.familySalary.text = Language.BuildingUI[20];
            this.familySalary.tooltip = Language.BuildingUI[20];
            this.familySalary.relativePosition = new Vector3(SPACING, this.familyMoney.relativePosition.y + SPACING22);
            this.familySalary.autoSize = true;
            this.familySalary.name = "Moreeconomic_Text_1";
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
                    CitizenManager instance3 = Singleton<CitizenManager>.instance;
                    ushort homeBuilding = instance3.m_citizens.m_buffer[(int)((UIntPtr)MainDataStore.last_citizenid)].m_homeBuilding;
                    BuildingManager instance2 = Singleton<BuildingManager>.instance;
                    uint homeId = instance3.m_citizens.m_buffer[MainDataStore.last_citizenid].GetContainingUnit(MainDataStore.last_citizenid, instance2.m_buildings.m_buffer[(int)homeBuilding].m_citizenUnits, CitizenUnit.Flags.Home);
                    this.familyMoney.text = string.Format(Language.BuildingUI[14] + " [{0}]", MainDataStore.family_money[homeId]);
                    this.familySalary.text = string.Format(Language.BuildingUI[20] + " [{0}]", CaculateFamilySalary(homeId));
                    HumanUI.refeshOnce = false;
                }
            }
        }

        public int CaculateFamilySalary(uint homeid)
        {
            float num = 0f;

            uint temp = Singleton<CitizenManager>.instance.m_units.m_buffer[homeid].m_citizen0;
            if (temp != 0)
            {
                num += RealCityResidentAI.CitizenSalary(temp, true);
            }
            temp = Singleton<CitizenManager>.instance.m_units.m_buffer[homeid].m_citizen1;
            if (temp != 0)
            {
                num += RealCityResidentAI.CitizenSalary(temp, true);
            }
            temp = Singleton<CitizenManager>.instance.m_units.m_buffer[homeid].m_citizen2;
            if (temp != 0)
            {
                num += RealCityResidentAI.CitizenSalary(temp, true);
            }
            temp = Singleton<CitizenManager>.instance.m_units.m_buffer[homeid].m_citizen3;
            if (temp != 0)
            {
                num += RealCityResidentAI.CitizenSalary(temp, true);
            }
            temp = Singleton<CitizenManager>.instance.m_units.m_buffer[homeid].m_citizen4;
            if (temp != 0)
            {
                num += RealCityResidentAI.CitizenSalary(temp, true);
            }
            return (int)num;
        }
    }
}
