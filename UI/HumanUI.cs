using System.Collections.Generic;
using ColossalFramework.UI;
using UnityEngine;
using ColossalFramework;
using System;
using System.Reflection;
using RealCity.CustomAI;
using RealCity.Util;

namespace RealCity.UI
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
        private UILabel familyGoods;

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
            this.familyMoney.text = Localization.Get("FAMILY_MONEY");
            this.familyMoney.relativePosition = new Vector3(SPACING, 50f);
            this.familyMoney.autoSize = true;

            this.familySalary = base.AddUIComponent<UILabel>();
            this.familySalary.text = Localization.Get("FAMILY_SALARY");
            this.familySalary.relativePosition = new Vector3(SPACING, this.familyMoney.relativePosition.y + SPACING22);
            this.familySalary.autoSize = true;

            this.familyGoods = base.AddUIComponent<UILabel>();
            this.familyGoods.text = Localization.Get("FAMILY_GOODS");
            this.familyGoods.relativePosition = new Vector3(SPACING, this.familySalary.relativePosition.y + SPACING22);
            this.familyGoods.autoSize = true;
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
                    this.familyMoney.text = string.Format(Localization.Get("FAMILY_MONEY") + " [{0}]" , MainDataStore.family_money[homeId]);
                    this.familySalary.text = string.Format(Localization.Get("FAMILY_SALARY") + " [{0}]", CaculateFamilySalary(homeId));

                    if ((instance3.m_citizens.m_buffer[MainDataStore.last_citizenid].m_flags & Citizen.Flags.NeedGoods) != 0)
                    {
                        this.familyGoods.text = string.Format(Localization.Get("FAMILY_GOODS") + " [{0}] " + Localization.Get("FAMILY_NEED_GOODS"), instance3.m_units.m_buffer[homeId].m_goods.ToString());
                    }
                    else
                    {
                        this.familyGoods.text = string.Format(Localization.Get("FAMILY_GOODS") + " [{0}]", instance3.m_units.m_buffer[homeId].m_goods.ToString());
                    }

                    HumanUI.refeshOnce = false;
                }
            }
        }

        public int CaculateFamilySalary(uint homeid)
        {
            int totalSalary = 0;

            uint citizenID = Singleton<CitizenManager>.instance.m_units.m_buffer[homeid].m_citizen0;
            if (citizenID != 0)
            {
                totalSalary += RealCityResidentAI.ProcessCitizenSalary(citizenID, true);
            }
            citizenID = Singleton<CitizenManager>.instance.m_units.m_buffer[homeid].m_citizen1;
            if (citizenID != 0)
            {
                totalSalary += RealCityResidentAI.ProcessCitizenSalary(citizenID, true);
            }
            citizenID = Singleton<CitizenManager>.instance.m_units.m_buffer[homeid].m_citizen2;
            if (citizenID != 0)
            {
                totalSalary += RealCityResidentAI.ProcessCitizenSalary(citizenID, true);
            }
            citizenID = Singleton<CitizenManager>.instance.m_units.m_buffer[homeid].m_citizen3;
            if (citizenID != 0)
            {
                totalSalary += RealCityResidentAI.ProcessCitizenSalary(citizenID, true);
            }
            citizenID = Singleton<CitizenManager>.instance.m_units.m_buffer[homeid].m_citizen4;
            if (citizenID != 0)
            {
                totalSalary += RealCityResidentAI.ProcessCitizenSalary(citizenID, true);
            }
            return totalSalary;
        }
    }
}
