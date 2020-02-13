using System.Collections.Generic;
using ColossalFramework;
using ColossalFramework.UI;
using UnityEngine;
using RealCity.CustomAI;
using RealCity.Util;

namespace RealCity.UI
{
    public class EcnomicUI : UIPanel
    {
        public static readonly string cacheName = "EcnomicUI";
        private static readonly float WIDTH = 900f;
        private static readonly float HEIGHT = 450f;
        private static readonly float HEADER = 40f;
        private static readonly float SPACING = 17f;
        private static readonly float SPACING22 = 23f;
        private Dictionary<string, UILabel> _valuesControlContainer = new Dictionary<string, UILabel>(16);
        private UIDragHandle m_DragHandler;
        private UIButton m_closeButton;
        private UILabel m_title;
        //1 Citizen
        private UILabel m_firstline_citizen;
        //1.1 citizen income
        private UILabel citizen_count;
        private UILabel family_count;
        private UILabel citizen_salary_per_family;
        private UILabel citizen_salary_tax_per_family;
        //1.2 citizen expense
        private UILabel citizen_expense_per_family;
        private UILabel citizen_average_transport_fee;
        //1.3 income - expense
        private UILabel family_profit_money_num;
        private UILabel family_loss_money_num;
        private UILabel family_very_profit_num;
        private UILabel family_weight_stable_high;
        private UILabel family_weight_stable_low;
        private UILabel family_weight_stable_medium;
        //2 Building
        private UILabel m_secondline_building;
        private UILabel profitBuildingCount;
        private UILabel externalInvestments;
        //3 Policy
        private UILabel mThirdLinePolicy;
        private UILabel minimumLivingAllowance;
        private UILabel unfinishedTransitionLost;
        //4 Tips
        private UILabel tip1;
        private UILabel tip2;
        private UILabel tip3;
        private UILabel tip4;
        private UILabel tip5;
        private UILabel tip6;
        public static bool refeshOnce = false;

        public override void Update()
        {
            RefreshDisplayData();
            base.Update();
        }

        public override void Start()
        {
            base.Start();
            size = new Vector2(WIDTH, HEIGHT);
            backgroundSprite = "MenuPanel";
            canFocus = true;
            isInteractive = true;
            BringToFront();
            relativePosition = new Vector3(50f, 170f);
            opacity = 1f;
            cachedName = cacheName;
            m_DragHandler = AddUIComponent<UIDragHandle>();
            m_DragHandler.target = this;
            m_title = AddUIComponent<UILabel>();
            m_title.text = Localization.Get("ECONOMIC_DATA");
            m_title.relativePosition = new Vector3(WIDTH / 2f - m_title.width / 2f - 25f, HEADER / 2f - m_title.height / 2f);
            m_title.textAlignment = UIHorizontalAlignment.Center;
            m_closeButton = AddUIComponent<UIButton>();
            m_closeButton.normalBgSprite = "buttonclose";
            m_closeButton.hoveredBgSprite = "buttonclosehover";
            m_closeButton.pressedBgSprite = "buttonclosepressed";
            m_closeButton.relativePosition = new Vector3(WIDTH - 35f, 5f, 10f);
            m_closeButton.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                Hide();
            };
            Hide(); //dont show in the beginning
            DoOnStartup();
        }

        private void DoOnStartup()
        {
            ShowOnGui();
            RefreshDisplayData();
        }

        private void ShowOnGui()
        {
            //citizen
            m_firstline_citizen = AddUIComponent<UILabel>();
            m_firstline_citizen.text = Localization.Get("CITIZEN_STATUS");
            m_firstline_citizen.textScale = 1.1f;
            m_firstline_citizen.relativePosition = new Vector3(SPACING, 50f);
            m_firstline_citizen.autoSize = true;

            //data
            citizen_count = AddUIComponent<UILabel>();
            citizen_count.text = Localization.Get("CITIZEN_COUNT");
            citizen_count.relativePosition = new Vector3(SPACING, m_firstline_citizen.relativePosition.y + SPACING22);
            citizen_count.autoSize = true;

            family_count = AddUIComponent<UILabel>();
            family_count.text = Localization.Get("FAMILY_COUNT");
            family_count.relativePosition = new Vector3(citizen_count.relativePosition.x + 300f, citizen_count.relativePosition.y);
            family_count.autoSize = true;

            citizen_salary_per_family = AddUIComponent<UILabel>();
            citizen_salary_per_family.text = Localization.Get("SALARY_PER_FAMILY");
            citizen_salary_per_family.relativePosition = new Vector3(family_count.relativePosition.x + 300f, family_count.relativePosition.y);
            citizen_salary_per_family.autoSize = true;

            citizen_salary_tax_per_family = AddUIComponent<UILabel>();
            citizen_salary_tax_per_family.text = Localization.Get("CITIZEN_TAX_PER_FAMILY");
            citizen_salary_tax_per_family.relativePosition = new Vector3(SPACING, citizen_count.relativePosition.y + SPACING22);
            citizen_salary_tax_per_family.autoSize = true;

            citizen_expense_per_family = AddUIComponent<UILabel>();
            citizen_expense_per_family.text = Localization.Get("EXPENSE_PER_FAMILY");
            citizen_expense_per_family.relativePosition = new Vector3(family_count.relativePosition.x, family_count.relativePosition.y + SPACING22);
            citizen_expense_per_family.autoSize = true;

            citizen_average_transport_fee = AddUIComponent<UILabel>();
            citizen_average_transport_fee.text = Localization.Get("AVERAGE_TRANPORT_FEE");
            citizen_average_transport_fee.relativePosition = new Vector3(citizen_salary_per_family.relativePosition.x, citizen_salary_per_family.relativePosition.y + SPACING22);
            citizen_average_transport_fee.autoSize = true;

            family_very_profit_num = AddUIComponent<UILabel>();
            family_very_profit_num.text = Localization.Get("HIGH_SALARY_COUNT");
            family_very_profit_num.relativePosition = new Vector3(SPACING, citizen_salary_tax_per_family.relativePosition.y + SPACING22);
            family_very_profit_num.autoSize = true;

            family_profit_money_num = AddUIComponent<UILabel>();
            family_profit_money_num.text = Localization.Get("MEDIUM_SALARY_COUNT");
            family_profit_money_num.relativePosition = new Vector3(citizen_expense_per_family.relativePosition.x, citizen_expense_per_family.relativePosition.y + SPACING22);
            family_profit_money_num.autoSize = true;

            family_loss_money_num = AddUIComponent<UILabel>();
            family_loss_money_num.text = Localization.Get("LOW_SALARY_COUNT");
            family_loss_money_num.relativePosition = new Vector3(citizen_average_transport_fee.relativePosition.x, citizen_average_transport_fee.relativePosition.y + SPACING22);
            family_loss_money_num.autoSize = true;

            family_weight_stable_high = AddUIComponent<UILabel>();
            family_weight_stable_high.text = Localization.Get("WEALTH_HIGH_COUNT");
            family_weight_stable_high.relativePosition = new Vector3(SPACING, family_very_profit_num.relativePosition.y + SPACING22);
            family_weight_stable_high.autoSize = true;

            family_weight_stable_medium = AddUIComponent<UILabel>();
            family_weight_stable_medium.text = Localization.Get("WEALTH_MEDIUM_COUNT");
            family_weight_stable_medium.relativePosition = new Vector3(family_profit_money_num.relativePosition.x, family_profit_money_num.relativePosition.y + SPACING22);
            family_weight_stable_medium.autoSize = true;

            family_weight_stable_low = AddUIComponent<UILabel>();
            family_weight_stable_low.text = Localization.Get("WEALTH_LOW_COUNT");
            family_weight_stable_low.relativePosition = new Vector3(family_loss_money_num.relativePosition.x, family_loss_money_num.relativePosition.y + SPACING22);
            family_weight_stable_low.autoSize = true;

            //building
            m_secondline_building = AddUIComponent<UILabel>();
            m_secondline_building.text = Localization.Get("BUILDING_STATUS");
            m_secondline_building.textScale = 1.1f;
            m_secondline_building.relativePosition = new Vector3(SPACING, family_weight_stable_high.relativePosition.y + SPACING22 + 10f);
            m_secondline_building.autoSize = true;

            profitBuildingCount = AddUIComponent<UILabel>();
            profitBuildingCount.text = Localization.Get("PROFIT_BUIDLING_COUNT");
            profitBuildingCount.relativePosition = new Vector3(SPACING, m_secondline_building.relativePosition.y + SPACING22);
            profitBuildingCount.autoSize = true;

            externalInvestments = AddUIComponent<UILabel>();
            externalInvestments.text = Localization.Get("EXTERNAL_INVESTMENTS");
            externalInvestments.relativePosition = new Vector3(profitBuildingCount.relativePosition.x + 450f, profitBuildingCount.relativePosition.y);
            externalInvestments.autoSize = true;

            //policy
            mThirdLinePolicy = AddUIComponent<UILabel>();
            mThirdLinePolicy.text = Localization.Get("POLICY_COST");
            mThirdLinePolicy.textScale = 1.1f;
            mThirdLinePolicy.relativePosition = new Vector3(SPACING, profitBuildingCount.relativePosition.y + SPACING22 + 10f);
            mThirdLinePolicy.autoSize = true;

            minimumLivingAllowance = AddUIComponent<UILabel>();
            minimumLivingAllowance.text = Localization.Get("LIVING_ALLOWANCE");
            minimumLivingAllowance.relativePosition = new Vector3(SPACING, mThirdLinePolicy.relativePosition.y + SPACING22);
            minimumLivingAllowance.autoSize = true;

            unfinishedTransitionLost = AddUIComponent<UILabel>();
            unfinishedTransitionLost.text = Localization.Get("UNFINISHED_DEAL_LOST");
            unfinishedTransitionLost.relativePosition = new Vector3(minimumLivingAllowance.relativePosition.x + 450f, minimumLivingAllowance.relativePosition.y);
            unfinishedTransitionLost.autoSize = true;

            tip1 = AddUIComponent<UILabel>();
            tip1.text = Localization.Get("TIP1");
            tip1.relativePosition = new Vector3(SPACING, minimumLivingAllowance.relativePosition.y + SPACING22 + 10f);
            tip1.autoSize = true;;

            tip2 = AddUIComponent<UILabel>();
            tip2.text = Localization.Get("TIP2");
            tip2.relativePosition = new Vector3(SPACING, tip1.relativePosition.y + SPACING22);
            tip2.autoSize = true;

            tip3 = AddUIComponent<UILabel>();
            tip3.text = Localization.Get("TIP3");
            tip3.relativePosition = new Vector3(SPACING, tip2.relativePosition.y + SPACING22);
            tip3.autoSize = true;

            tip4 = AddUIComponent<UILabel>();
            tip4.text = Localization.Get("TIP4");
            tip4.relativePosition = new Vector3(SPACING, tip3.relativePosition.y + SPACING22);
            tip4.autoSize = true;

            tip5 = AddUIComponent<UILabel>();
            tip5.text = Localization.Get("TIP5");
            tip5.relativePosition = new Vector3(SPACING, tip4.relativePosition.y + SPACING22);
            tip5.autoSize = true;

            tip6 = AddUIComponent<UILabel>();
            tip6.text = Localization.Get("TIP6");
            tip6.relativePosition = new Vector3(SPACING, tip5.relativePosition.y + SPACING22);
            tip6.autoSize = true;
        }

        private void RefreshDisplayData()
        {
            uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
            uint num2 = currentFrameIndex & 255u;

            if (refeshOnce)
            {
                if (isVisible)
                {
                    //Citizen
                    m_title.text = Localization.Get("ECONOMIC_DATA");
                    m_firstline_citizen.text = Localization.Get("CITIZEN_STATUS");
                    citizen_count.text = string.Format(Localization.Get("CITIZEN_COUNT") + " [{0}]", MainDataStore.citizenCount);
                    family_count.text = string.Format(Localization.Get("FAMILY_COUNT") + " [{0}]", MainDataStore.familyCount);
                    citizen_salary_per_family.text = string.Format(Localization.Get("SALARY_PER_FAMILY") + " [{0}]", MainDataStore.citizenSalaryPerFamily);

                    if (MainDataStore.familyCount != 0)
                    {
                        citizen_salary_tax_per_family.text = string.Format(Localization.Get("CITIZEN_TAX_PER_FAMILY") + " [{0}]", MainDataStore.citizenSalaryTaxTotal / MainDataStore.familyCount);
                    }
                    else
                    {
                        citizen_salary_tax_per_family.text = string.Format(Localization.Get("CITIZEN_TAX_PER_FAMILY"));
                    }
                    citizen_expense_per_family.text = string.Format(Localization.Get("EXPENSE_PER_FAMILY") + " [{0}]", MainDataStore.citizenExpensePerFamily);
                    citizen_average_transport_fee.text = string.Format(Localization.Get("AVERAGE_TRANPORT_FEE") + " [{0}]", MainDataStore.citizenAverageTransportFee);

                    family_very_profit_num.text = string.Format(Localization.Get("HIGH_SALARY_COUNT") + " [{0}]", MainDataStore.family_very_profit_money_num);
                    family_profit_money_num.text = string.Format(Localization.Get("MEDIUM_SALARY_COUNT") + " [{0}]", MainDataStore.family_profit_money_num);
                    family_loss_money_num.text = string.Format(Localization.Get("LOW_SALARY_COUNT") + " [{0}]", MainDataStore.family_loss_money_num);
                    family_weight_stable_high.text = string.Format(Localization.Get("WEALTH_HIGH_COUNT") + " [{0}]", MainDataStore.family_weight_stable_high);
                    family_weight_stable_medium.text = string.Format(Localization.Get("WEALTH_MEDIUM_COUNT") + " [{0}]", MainDataStore.familyCount - MainDataStore.family_weight_stable_high - MainDataStore.family_weight_stable_low);
                    family_weight_stable_low.text = string.Format(Localization.Get("WEALTH_LOW_COUNT") + " [{0}]", MainDataStore.family_weight_stable_low);
                    //Building
                    m_secondline_building.text = Localization.Get("BUILDING_STATUS");
                    profitBuildingCount.text = string.Format(Localization.Get("PROFIT_BUIDLING_COUNT") + " [{0}]", RealCityPrivateBuildingAI.profitBuildingCountFinal);
                    externalInvestments.text = string.Format(Localization.Get("EXTERNAL_INVESTMENTS") + " [{0}]", RealCityPrivateBuildingAI.profitBuildingMoneyFinal);
                    //Policy
                    mThirdLinePolicy.text = Localization.Get("POLICY_COST");
                    minimumLivingAllowance.text = string.Format(Localization.Get("LIVING_ALLOWANCE") + " [{0}]", (MainDataStore.minimumLivingAllowanceFinal / 100));
                    unfinishedTransitionLost.text = string.Format(Localization.Get("UNFINISHED_DEAL_LOST") + " [{0}]", MainDataStore.unfinishedTransitionLostFinal);
                    //Tip
                    tip1.text = string.Format(Localization.Get("TIP1") + "  " + Localization.Get("USE_TMPE_TIP"));
                    tip2.text = string.Format(Localization.Get("TIP2") + "  " + Localization.Get("STARTUP_TIP"));
                    tip3.text = string.Format(Localization.Get("TIP3") + "  " + Localization.Get("TOURIST_TIP"));
                    tip4.text = string.Format(Localization.Get("TIP4") + "  " + Localization.Get("OFFICE_TIP"));
                    tip5.text = string.Format(Localization.Get("TIP5") + "  " + Localization.Get("UNLOCK_TIP"));
                    tip6.text = string.Format(Localization.Get("TIP6") + "  " + Localization.Get("UG_TIP"));

                    refeshOnce = false;
                }
            }
        }

        private void ProcessVisibility()
        {
            if (!isVisible)
            {
                refeshOnce = true;
                Show();
            }
            else
            {
                Hide();
            }
        }
    }
}
