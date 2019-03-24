using System.Collections.Generic;
using ColossalFramework;
using ColossalFramework.UI;
using UnityEngine;
using System.Collections;
using RealCity.CustomAI;
using RealCity.Util;

namespace RealCity.UI
{
    public class EcnomicUI : UIPanel
    {
        public static readonly string cacheName = "EcnomicUI";
        private static float WIDTH = 650f;
        private static readonly float HEIGHT = 450f;
        private static readonly float HEADER = 40f;
        private static readonly float SPACING = 17f;
        private static readonly float SPACING22 = 23f;
        private ItemClass.Availability CurrentMode;
        public static EcnomicUI instance;
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
            this.RefreshDisplayData();
            base.Update();
        }

        public override void Start()
        {
            base.Start();
            //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "Go to UI now");
            instance = this;
            base.size = new Vector2(WIDTH, HEIGHT);
            base.backgroundSprite = "MenuPanel";
            this.canFocus = true;
            this.isInteractive = true;
            this.BringToFront();
            //base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 + 150f), 5f);
            base.relativePosition = new Vector3(200f, 170f);
            base.opacity = 1f;
            base.cachedName = cacheName;
            this.CurrentMode = Singleton<ToolManager>.instance.m_properties.m_mode;
            this.m_DragHandler = base.AddUIComponent<UIDragHandle>();
            this.m_DragHandler.target = this;
            this.m_title = base.AddUIComponent<UILabel>();
            this.m_title.text = Localization.Get("ECONOMIC_DATA");
            this.m_title.relativePosition = new Vector3(WIDTH / 2f - this.m_title.width / 2f - 25f, HEADER / 2f - this.m_title.height / 2f);
            this.m_title.textAlignment = UIHorizontalAlignment.Center;
            this.m_closeButton = base.AddUIComponent<UIButton>();
            this.m_closeButton.normalBgSprite = "buttonclose";
            this.m_closeButton.hoveredBgSprite = "buttonclosehover";
            this.m_closeButton.pressedBgSprite = "buttonclosepressed";
            this.m_closeButton.relativePosition = new Vector3(WIDTH - 35f, 5f, 10f);
            this.m_closeButton.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                base.Hide();
            };
            base.Hide(); //dont show in the beginning
            this.DoOnStartup();
        }

        private void DoOnStartup()
        {
            this.ShowOnGui();
            this.RefreshDisplayData();
        }

        private void ShowOnGui()
        {
            //citizen
            this.m_firstline_citizen = base.AddUIComponent<UILabel>();
            this.m_firstline_citizen.text = Localization.Get("CITIZEN_STATUS");
            this.m_firstline_citizen.textScale = 1.1f;
            this.m_firstline_citizen.relativePosition = new Vector3(SPACING, 50f);
            this.m_firstline_citizen.autoSize = true;

            //data
            this.citizen_count = base.AddUIComponent<UILabel>();
            this.citizen_count.text = Localization.Get("CITIZEN_COUNT");
            this.citizen_count.relativePosition = new Vector3(SPACING, this.m_firstline_citizen.relativePosition.y + SPACING22);
            this.citizen_count.autoSize = true;

            this.family_count = base.AddUIComponent<UILabel>();
            this.family_count.text = Localization.Get("FAMILY_COUNT");
            this.family_count.relativePosition = new Vector3(this.citizen_count.relativePosition.x + this.citizen_count.width + SPACING + 140f, this.citizen_count.relativePosition.y);
            this.family_count.autoSize = true;

            this.citizen_salary_per_family = base.AddUIComponent<UILabel>();
            this.citizen_salary_per_family.text = Localization.Get("SALARY_PER_FAMILY");
            this.citizen_salary_per_family.relativePosition = new Vector3(this.family_count.relativePosition.x + this.family_count.width + SPACING + 110f, this.family_count.relativePosition.y);
            this.citizen_salary_per_family.autoSize = true;

            this.citizen_salary_tax_per_family = base.AddUIComponent<UILabel>();
            this.citizen_salary_tax_per_family.text = Localization.Get("CITIZEN_TAX_PER_FAMILY");
            this.citizen_salary_tax_per_family.relativePosition = new Vector3(SPACING, this.citizen_count.relativePosition.y + SPACING22);
            this.citizen_salary_tax_per_family.autoSize = true;

            this.citizen_expense_per_family = base.AddUIComponent<UILabel>();
            this.citizen_expense_per_family.text = Localization.Get("EXPENSE_PER_FAMILY");
            this.citizen_expense_per_family.relativePosition = new Vector3(this.family_count.relativePosition.x, this.family_count.relativePosition.y + SPACING22);
            this.citizen_expense_per_family.autoSize = true;

            this.citizen_average_transport_fee = base.AddUIComponent<UILabel>();
            this.citizen_average_transport_fee.text = Localization.Get("AVERAGE_TRANPORT_FEE");
            this.citizen_average_transport_fee.relativePosition = new Vector3(this.citizen_salary_per_family.relativePosition.x, this.citizen_salary_per_family.relativePosition.y + SPACING22);
            this.citizen_average_transport_fee.autoSize = true;

            this.family_very_profit_num = base.AddUIComponent<UILabel>();
            this.family_very_profit_num.text = Localization.Get("HIGH_SALARY_COUNT");
            this.family_very_profit_num.relativePosition = new Vector3(SPACING, this.citizen_salary_tax_per_family.relativePosition.y + SPACING22);
            this.family_very_profit_num.autoSize = true;

            this.family_profit_money_num = base.AddUIComponent<UILabel>();
            this.family_profit_money_num.text = Localization.Get("MEDIUM_SALARY_COUNT");
            this.family_profit_money_num.relativePosition = new Vector3(this.citizen_expense_per_family.relativePosition.x, this.citizen_expense_per_family.relativePosition.y + SPACING22);
            this.family_profit_money_num.autoSize = true;

            this.family_loss_money_num = base.AddUIComponent<UILabel>();
            this.family_loss_money_num.text = Localization.Get("LOW_SALARY_COUNT");
            this.family_loss_money_num.relativePosition = new Vector3(this.citizen_average_transport_fee.relativePosition.x, this.citizen_average_transport_fee.relativePosition.y + SPACING22);
            this.family_loss_money_num.autoSize = true;

            this.family_weight_stable_high = base.AddUIComponent<UILabel>();
            this.family_weight_stable_high.text = Localization.Get("WEALTH_HIGH_COUNT");
            this.family_weight_stable_high.relativePosition = new Vector3(SPACING, this.family_very_profit_num.relativePosition.y + SPACING22);
            this.family_weight_stable_high.autoSize = true;

            this.family_weight_stable_medium = base.AddUIComponent<UILabel>();
            this.family_weight_stable_medium.text = Localization.Get("WEALTH_MEDIUM_COUNT");
            this.family_weight_stable_medium.relativePosition = new Vector3(this.family_profit_money_num.relativePosition.x, this.family_profit_money_num.relativePosition.y + SPACING22);
            this.family_weight_stable_medium.autoSize = true;

            this.family_weight_stable_low = base.AddUIComponent<UILabel>();
            this.family_weight_stable_low.text = Localization.Get("WEALTH_LOW_COUNT");
            this.family_weight_stable_low.relativePosition = new Vector3(this.family_loss_money_num.relativePosition.x, this.family_loss_money_num.relativePosition.y + SPACING22);
            this.family_weight_stable_low.autoSize = true;

            //building
            this.m_secondline_building = base.AddUIComponent<UILabel>();
            this.m_secondline_building.text = Localization.Get("BUILDING_STATUS");
            this.m_secondline_building.textScale = 1.1f;
            this.m_secondline_building.relativePosition = new Vector3(SPACING, this.family_weight_stable_high.relativePosition.y + SPACING22 + 10f);
            this.m_secondline_building.autoSize = true;

            this.profitBuildingCount = base.AddUIComponent<UILabel>();
            this.profitBuildingCount.text = Localization.Get("PROFIT_BUIDLING_COUNT");
            this.profitBuildingCount.relativePosition = new Vector3(SPACING, this.m_secondline_building.relativePosition.y + SPACING22);
            this.profitBuildingCount.autoSize = true;

            this.externalInvestments = base.AddUIComponent<UILabel>();
            this.externalInvestments.text = Localization.Get("EXTERNAL_INVESTMENTS");
            this.externalInvestments.relativePosition = new Vector3(this.profitBuildingCount.relativePosition.x + 100f + this.profitBuildingCount.width, this.profitBuildingCount.relativePosition.y);
            this.externalInvestments.autoSize = true;

            //policy
            this.mThirdLinePolicy = base.AddUIComponent<UILabel>();
            this.mThirdLinePolicy.text = Localization.Get("POLICY_COST");
            this.mThirdLinePolicy.textScale = 1.1f;
            this.mThirdLinePolicy.relativePosition = new Vector3(SPACING, this.profitBuildingCount.relativePosition.y + SPACING22 + 10f);
            this.mThirdLinePolicy.autoSize = true;

            this.minimumLivingAllowance = base.AddUIComponent<UILabel>();
            this.minimumLivingAllowance.text = Localization.Get("LIVING_ALLOWANCE");
            this.minimumLivingAllowance.relativePosition = new Vector3(SPACING, this.mThirdLinePolicy.relativePosition.y + SPACING22);
            this.minimumLivingAllowance.autoSize = true;

            this.tip1 = base.AddUIComponent<UILabel>();
            this.tip1.text = Localization.Get("TIP1");
            this.tip1.relativePosition = new Vector3(SPACING, this.minimumLivingAllowance.relativePosition.y + SPACING22 + 10f);
            this.tip1.autoSize = true;;

            this.tip2 = base.AddUIComponent<UILabel>();
            this.tip2.text = Localization.Get("TIP2");
            this.tip2.relativePosition = new Vector3(SPACING, this.tip1.relativePosition.y + SPACING22);
            this.tip2.autoSize = true;

            this.tip3 = base.AddUIComponent<UILabel>();
            this.tip3.text = Localization.Get("TIP3");
            this.tip3.relativePosition = new Vector3(SPACING, this.tip2.relativePosition.y + SPACING22);
            this.tip3.autoSize = true;

            this.tip4 = base.AddUIComponent<UILabel>();
            this.tip4.text = Localization.Get("TIP4");
            this.tip4.relativePosition = new Vector3(SPACING, this.tip3.relativePosition.y + SPACING22);
            this.tip4.autoSize = true;

            this.tip5 = base.AddUIComponent<UILabel>();
            this.tip5.text = Localization.Get("TIP5");
            this.tip5.relativePosition = new Vector3(SPACING, this.tip4.relativePosition.y + SPACING22);
            this.tip5.autoSize = true;

            this.tip6 = base.AddUIComponent<UILabel>();
            this.tip6.text = Localization.Get("TIP6");
            this.tip6.relativePosition = new Vector3(SPACING, this.tip5.relativePosition.y + SPACING22);
            this.tip6.autoSize = true;
        }


        private void RefreshDisplayData()
        {
            uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
            uint num2 = currentFrameIndex & 255u;

            if (refeshOnce)
            {
                if (base.isVisible)
                {
                    //Citizen
                    this.m_title.text = Localization.Get("ECONOMIC_DATA");
                    this.m_firstline_citizen.text = Localization.Get("CITIZEN_STATUS");
                    this.citizen_count.text = string.Format(Localization.Get("CITIZEN_COUNT") + " [{0}]", MainDataStore.citizenCount);
                    this.family_count.text = string.Format(Localization.Get("FAMILY_COUNT") + " [{0}]", MainDataStore.familyCount);
                    this.citizen_salary_per_family.text = string.Format(Localization.Get("SALARY_PER_FAMILY") + " [{0}]", MainDataStore.citizenSalaryPerFamily);

                    if (MainDataStore.familyCount != 0)
                    {
                        this.citizen_salary_tax_per_family.text = string.Format(Localization.Get("CITIZEN_TAX_PER_FAMILY") + " [{0}]", MainDataStore.citizenSalaryTaxTotal / MainDataStore.familyCount);
                    }
                    this.citizen_expense_per_family.text = string.Format(Localization.Get("EXPENSE_PER_FAMILY") + " [{0}]", MainDataStore.citizenExpensePerFamily);
                    this.citizen_average_transport_fee.text = string.Format(Localization.Get("AVERAGE_TRANPORT_FEE") + " [{0}]", MainDataStore.citizenAverageTransportFee);

                    this.family_very_profit_num.text = string.Format(Localization.Get("HIGH_SALARY_COUNT") + " [{0}]", MainDataStore.family_very_profit_money_num);
                    this.family_profit_money_num.text = string.Format(Localization.Get("MEDIUM_SALARY_COUNT") + " [{0}]", MainDataStore.family_profit_money_num);
                    this.family_loss_money_num.text = string.Format(Localization.Get("LOW_SALARY_COUNT") + " [{0}]", MainDataStore.family_loss_money_num);
                    this.family_weight_stable_high.text = string.Format(Localization.Get("WEALTH_HIGH_COUNT") + " [{0}]", MainDataStore.family_weight_stable_high);
                    this.family_weight_stable_medium.text = string.Format(Localization.Get("WEALTH_MEDIUM_COUNT") + " [{0}]", MainDataStore.familyCount - MainDataStore.family_weight_stable_high - MainDataStore.family_weight_stable_low);
                    this.family_weight_stable_low.text = string.Format(Localization.Get("WEALTH_LOW_COUNT") + " [{0}]", MainDataStore.family_weight_stable_low);
                    //Building
                    this.m_secondline_building.text = Localization.Get("BUILDING_STATUS");
                    this.profitBuildingCount.text = string.Format(Localization.Get("PROFIT_BUIDLING_COUNT") + " [{0}]", RealCityPrivateBuildingAI.greaterThan20000ProfitBuildingCountFinal);
                    this.externalInvestments.text = string.Format(Localization.Get("EXTERNAL_INVESTMENTS") + " [{0}]", RealCityPrivateBuildingAI.greaterThan20000ProfitBuildingMoneyFinal);
                    //Policy
                    this.mThirdLinePolicy.text = Localization.Get("POLICY_COST");
                    this.minimumLivingAllowance.text = string.Format(Localization.Get("LIVING_ALLOWANCE") + " [{0}]", (MainDataStore.minimumLivingAllowanceFinal / 100));
                    //Tip
                    this.tip1.text = string.Format(Localization.Get("TIP1") + "  " + Localization.Get("USE_TMPE_TIP"));
                    this.tip2.text = string.Format(Localization.Get("TIP2") + "  " + Localization.Get("STARTUP_TIP"));
                    this.tip3.text = string.Format(Localization.Get("TIP3") + "  " + Localization.Get("TOURIST_TIP"));
                    this.tip4.text = string.Format(Localization.Get("TIP4") + "  " + Localization.Get("OFFICE_TIP"));
                    this.tip5.text = string.Format(Localization.Get("TIP5") + "  " + Localization.Get("UNLOCK_TIP"));
                    this.tip6.text = string.Format(Localization.Get("TIP6") + "  " + Localization.Get("UG_TIP"));

                    refeshOnce = false;
                }
            }
        }

        private void ProcessVisibility()
        {
            if (!base.isVisible)
            {
                refeshOnce = true;
                base.Show();
            }
            else
            {
                base.Hide();
            }
        }
    }
}
