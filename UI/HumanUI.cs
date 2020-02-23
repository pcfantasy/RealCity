using ColossalFramework.UI;
using UnityEngine;
using ColossalFramework;
using System;
using RealCity.CustomAI;
using RealCity.Util;
using RealCity.CustomData;

namespace RealCity.UI
{
    public class HumanUI : UIPanel
    {
        public static readonly string cacheName = "HumanUI";
        private static readonly float SPACING = 15f;
        private static readonly float SPACING22 = 22f;
        public CitizenWorldInfoPanel baseBuildingWindow;
        public static bool refeshOnce = false;
        private UILabel familyMoney;
        private UILabel familySalary;
        private UILabel familyGoods;

        public override void Update()
        {
            RefreshDisplayData();
            base.Update();
        }

        public override void Awake()
        {
            base.Awake();
            DoOnStartup();
        }

        public override void Start()
        {
            base.Start();
            canFocus = true;
            isInteractive = true;
            isVisible = true;
            opacity = 1f;
            cachedName = cacheName;
            RefreshDisplayData();
        }

        private void DoOnStartup()
        {
            ShowOnGui();            
        }

        private void ShowOnGui()
        { 
            familyMoney = AddUIComponent<UILabel>();
            familyMoney.text = Localization.Get("FAMILY_MONEY");
            familyMoney.relativePosition = new Vector3(SPACING, 50f);
            familyMoney.autoSize = true;

            familySalary = AddUIComponent<UILabel>();
            familySalary.text = Localization.Get("FAMILY_SALARY");
            familySalary.relativePosition = new Vector3(SPACING, familyMoney.relativePosition.y + SPACING22);
            familySalary.autoSize = true;

            familyGoods = AddUIComponent<UILabel>();
            familyGoods.text = Localization.Get("FAMILY_GOODS");
            familyGoods.relativePosition = new Vector3(SPACING, familySalary.relativePosition.y + SPACING22);
            familyGoods.autoSize = true;
        }

        private void RefreshDisplayData()
        {
            uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
            uint num2 = currentFrameIndex & 255u;

            if (refeshOnce || (CitizenData.lastCitizenID != WorldInfoPanel.GetCurrentInstanceID().Citizen))
            {
                if (isVisible)
                {
                    CitizenData.lastCitizenID = WorldInfoPanel.GetCurrentInstanceID().Citizen;
                    CitizenManager instance3 = Singleton<CitizenManager>.instance;
                    ushort homeBuilding = instance3.m_citizens.m_buffer[(int)((UIntPtr)CitizenData.lastCitizenID)].m_homeBuilding;
                    BuildingManager instance2 = Singleton<BuildingManager>.instance;
                    uint homeId = instance3.m_citizens.m_buffer[CitizenData.lastCitizenID].GetContainingUnit(CitizenData.lastCitizenID, instance2.m_buildings.m_buffer[homeBuilding].m_citizenUnits, CitizenUnit.Flags.Home);
                    familyMoney.text = string.Format(Localization.Get("FAMILY_MONEY") + " [{0}]" , CitizenUnitData.familyMoney[homeId]);
                    familySalary.text = string.Format(Localization.Get("FAMILY_SALARY") + " [{0}]", CaculateFamilySalary(homeId));

                    if ((instance3.m_citizens.m_buffer[CitizenData.lastCitizenID].m_flags & Citizen.Flags.NeedGoods) != 0)
                    {
                        familyGoods.text = string.Format(Localization.Get("FAMILY_GOODS") + " [{0}] " + Localization.Get("FAMILY_NEED_GOODS"), instance3.m_units.m_buffer[homeId].m_goods.ToString());
                    }
                    else
                    {
                        familyGoods.text = string.Format(Localization.Get("FAMILY_GOODS") + " [{0}]", instance3.m_units.m_buffer[homeId].m_goods.ToString());
                    }

                    refeshOnce = false;
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
