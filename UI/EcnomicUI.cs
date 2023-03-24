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
        private static readonly float HEIGHT = 400f;
        private static readonly float HEADER = 40f;
        private static readonly float SPACING = 17f;
        private static readonly float SPACING22 = 23f;
        private Dictionary<string, UILabel> _valuesControlContainer = new Dictionary<string, UILabel>(16);
        private UIDragHandle dragHandler;
        private UIButton closeButton;
        private UILabel title;
        //1 Citizen
        private UILabel firstline;
        //1.1 citizen income
        private UILabel citizenCount;
        private UILabel familyCount;
        private UILabel citizenSalaryPerFamily;
        private UILabel citizenSalaryTaxPerFamily;
        //1.2 citizen expense
        private UILabel citizenExpensePerFamily;
        private UILabel citizenAverageTransportFee;
        //1.3 income - expense
        private UILabel level1HighWealth;
        private UILabel level2HighWealth;
        private UILabel level3HighWealth;
        private UILabel wealthHigh;
        private UILabel wealthLow;
        private UILabel wealthMedium;
        //2 Building
        private UILabel secondline;
        private UILabel profitBuildingCount;
        private UILabel externalInvestments;
        //3 Policy
        private UILabel thirdLine;
        private UILabel minimumLivingAllowance;
        private UILabel unfinishedTransitionLost;
        private UILabel outsideGovermentMoney;
        private UILabel outsideTouristMoney;
        //4 Tips
        private UILabel tip1;
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
            dragHandler = AddUIComponent<UIDragHandle>();
            dragHandler.target = this;
            title = AddUIComponent<UILabel>();
            title.text = Localization.Get("ECONOMIC_DATA");
            title.relativePosition = new Vector3(WIDTH / 2f - title.width / 2f - 25f, HEADER / 2f - title.height / 2f);
            title.textAlignment = UIHorizontalAlignment.Center;
            closeButton = AddUIComponent<UIButton>();
            closeButton.normalBgSprite = "buttonclose";
            closeButton.hoveredBgSprite = "buttonclosehover";
            closeButton.pressedBgSprite = "buttonclosepressed";
            closeButton.relativePosition = new Vector3(WIDTH - 35f, 5f, 10f);
            closeButton.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
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
            firstline = AddUIComponent<UILabel>();
            firstline.text = Localization.Get("CITIZEN_STATUS");
            firstline.textScale = 1.1f;
            firstline.relativePosition = new Vector3(SPACING, 50f);
            firstline.autoSize = true;

            //data
            citizenCount = AddUIComponent<UILabel>();
            citizenCount.text = Localization.Get("CITIZEN_COUNT");
            citizenCount.relativePosition = new Vector3(SPACING, firstline.relativePosition.y + SPACING22);
            citizenCount.autoSize = true;

            familyCount = AddUIComponent<UILabel>();
            familyCount.text = Localization.Get("FAMILY_COUNT");
            familyCount.relativePosition = new Vector3(citizenCount.relativePosition.x + 300f, citizenCount.relativePosition.y);
            familyCount.autoSize = true;

            citizenSalaryPerFamily = AddUIComponent<UILabel>();
            citizenSalaryPerFamily.text = Localization.Get("SALARY_PER_FAMILY");
            citizenSalaryPerFamily.relativePosition = new Vector3(familyCount.relativePosition.x + 300f, familyCount.relativePosition.y);
            citizenSalaryPerFamily.autoSize = true;

            citizenSalaryTaxPerFamily = AddUIComponent<UILabel>();
            citizenSalaryTaxPerFamily.text = Localization.Get("CITIZEN_TAX_PER_FAMILY");
            citizenSalaryTaxPerFamily.relativePosition = new Vector3(SPACING, citizenCount.relativePosition.y + SPACING22);
            citizenSalaryTaxPerFamily.autoSize = true;

            citizenExpensePerFamily = AddUIComponent<UILabel>();
            citizenExpensePerFamily.text = Localization.Get("EXPENSE_PER_FAMILY");
            citizenExpensePerFamily.relativePosition = new Vector3(familyCount.relativePosition.x, familyCount.relativePosition.y + SPACING22);
            citizenExpensePerFamily.autoSize = true;

            citizenAverageTransportFee = AddUIComponent<UILabel>();
            citizenAverageTransportFee.text = Localization.Get("AVERAGE_TRANPORT_FEE");
            citizenAverageTransportFee.relativePosition = new Vector3(citizenSalaryPerFamily.relativePosition.x, citizenSalaryPerFamily.relativePosition.y + SPACING22);
            citizenAverageTransportFee.autoSize = true;

            level3HighWealth = AddUIComponent<UILabel>();
            level3HighWealth.text = Localization.Get("LEVEL3_HIGH_WEALTH");
            level3HighWealth.relativePosition = new Vector3(SPACING, citizenSalaryTaxPerFamily.relativePosition.y + SPACING22);
            level3HighWealth.autoSize = true;

            level2HighWealth = AddUIComponent<UILabel>();
            level2HighWealth.text = Localization.Get("LEVEL2_HIGH_WEALTH");
            level2HighWealth.relativePosition = new Vector3(citizenExpensePerFamily.relativePosition.x, citizenExpensePerFamily.relativePosition.y + SPACING22);
            level2HighWealth.autoSize = true;

            level1HighWealth = AddUIComponent<UILabel>();
            level1HighWealth.text = Localization.Get("LEVEL1_HIGH_WEALTH");
            level1HighWealth.relativePosition = new Vector3(citizenAverageTransportFee.relativePosition.x, citizenAverageTransportFee.relativePosition.y + SPACING22);
            level1HighWealth.autoSize = true;

            wealthHigh = AddUIComponent<UILabel>();
            wealthHigh.text = Localization.Get("WEALTH_HIGH_COUNT");
            wealthHigh.relativePosition = new Vector3(SPACING, level3HighWealth.relativePosition.y + SPACING22);
            wealthHigh.autoSize = true;

            wealthMedium = AddUIComponent<UILabel>();
            wealthMedium.text = Localization.Get("WEALTH_MEDIUM_COUNT");
            wealthMedium.relativePosition = new Vector3(level2HighWealth.relativePosition.x, level2HighWealth.relativePosition.y + SPACING22);
            wealthMedium.autoSize = true;

            wealthLow = AddUIComponent<UILabel>();
            wealthLow.text = Localization.Get("WEALTH_LOW_COUNT");
            wealthLow.relativePosition = new Vector3(level1HighWealth.relativePosition.x, level1HighWealth.relativePosition.y + SPACING22);
            wealthLow.autoSize = true;

            //building
            secondline = AddUIComponent<UILabel>();
            secondline.text = Localization.Get("BUILDING_STATUS");
            secondline.textScale = 1.1f;
            secondline.relativePosition = new Vector3(SPACING, wealthHigh.relativePosition.y + SPACING22 + 10f);
            secondline.autoSize = true;

            profitBuildingCount = AddUIComponent<UILabel>();
            profitBuildingCount.text = Localization.Get("PROFIT_BUIDLING_COUNT");
            profitBuildingCount.relativePosition = new Vector3(SPACING, secondline.relativePosition.y + SPACING22);
            profitBuildingCount.autoSize = true;

            externalInvestments = AddUIComponent<UILabel>();
            externalInvestments.text = Localization.Get("EXTERNAL_INVESTMENTS");
            externalInvestments.relativePosition = new Vector3(profitBuildingCount.relativePosition.x + 450f, profitBuildingCount.relativePosition.y);
            externalInvestments.autoSize = true;

            //policy
            thirdLine = AddUIComponent<UILabel>();
            thirdLine.text = Localization.Get("POLICY_COST");
            thirdLine.textScale = 1.1f;
            thirdLine.relativePosition = new Vector3(SPACING, profitBuildingCount.relativePosition.y + SPACING22 + 10f);
            thirdLine.autoSize = true;

            minimumLivingAllowance = AddUIComponent<UILabel>();
            minimumLivingAllowance.text = Localization.Get("LIVING_ALLOWANCE");
            minimumLivingAllowance.relativePosition = new Vector3(SPACING, thirdLine.relativePosition.y + SPACING22);
            minimumLivingAllowance.autoSize = true;

            unfinishedTransitionLost = AddUIComponent<UILabel>();
            unfinishedTransitionLost.text = Localization.Get("UNFINISHED_DEAL_LOST");
            unfinishedTransitionLost.relativePosition = new Vector3(minimumLivingAllowance.relativePosition.x + 450f, minimumLivingAllowance.relativePosition.y);
            unfinishedTransitionLost.autoSize = true;

            outsideGovermentMoney = AddUIComponent<UILabel>();
            outsideGovermentMoney.text = Localization.Get("OUTSIDE_GOV_MONEY");
            outsideGovermentMoney.relativePosition = new Vector3(SPACING, minimumLivingAllowance.relativePosition.y + SPACING22);
            outsideGovermentMoney.autoSize = true;

            outsideTouristMoney = AddUIComponent<UILabel>();
            outsideTouristMoney.text = Localization.Get("OUTSIDE_TOU_MONEY");
            outsideTouristMoney.relativePosition = new Vector3(outsideGovermentMoney.relativePosition.x + 450f, outsideGovermentMoney.relativePosition.y);
            outsideTouristMoney.autoSize = true;

            tip1 = AddUIComponent<UILabel>();
            tip1.text = Localization.Get("TIP1");
            tip1.relativePosition = new Vector3(SPACING, outsideGovermentMoney.relativePosition.y + SPACING22 + 10f);
            tip1.autoSize = true;;
        }

        private void RefreshDisplayData()
        {
            if (refeshOnce)
            {
                if (isVisible)
                {
                    //Citizen
                    title.text = Localization.Get("ECONOMIC_DATA");
                    firstline.text = Localization.Get("CITIZEN_STATUS");
                    citizenCount.text = string.Format(Localization.Get("CITIZEN_COUNT") + " [{0}]", MainDataStore.citizenCount);
                    familyCount.text = string.Format(Localization.Get("FAMILY_COUNT") + " [{0}]", MainDataStore.familyCount);
                    citizenSalaryPerFamily.text = string.Format(Localization.Get("SALARY_PER_FAMILY") + " [{0}]", MainDataStore.citizenSalaryPerFamily);

                    if (MainDataStore.familyCount != 0)
                    {
                        citizenSalaryTaxPerFamily.text = string.Format(Localization.Get("CITIZEN_TAX_PER_FAMILY") + " [{0}]", MainDataStore.citizenSalaryTaxTotal / MainDataStore.familyCount);
                    }
                    else
                    {
                        citizenSalaryTaxPerFamily.text = string.Format(Localization.Get("CITIZEN_TAX_PER_FAMILY"));
                    }
                    citizenExpensePerFamily.text = string.Format(Localization.Get("EXPENSE_PER_FAMILY") + " [{0}]", MainDataStore.citizenExpensePerFamily);
                    citizenAverageTransportFee.text = string.Format(Localization.Get("AVERAGE_TRANPORT_FEE") + " [{0}]", MainDataStore.citizenAverageTransportFee);

                    level3HighWealth.text = string.Format(Localization.Get("LEVEL3_HIGH_WEALTH") + " [{0}]", MainDataStore.level3HighWealth);
                    level2HighWealth.text = string.Format(Localization.Get("LEVEL2_HIGH_WEALTH") + " [{0}]", MainDataStore.level2HighWealth);
                    level1HighWealth.text = string.Format(Localization.Get("LEVEL1_HIGH_WEALTH") + " [{0}]", MainDataStore.level1HighWealth);
                    wealthHigh.text = string.Format(Localization.Get("WEALTH_HIGH_COUNT") + " [{0}]", MainDataStore.familyWeightStableHigh);
                    wealthMedium.text = string.Format(Localization.Get("WEALTH_MEDIUM_COUNT") + " [{0}]", MainDataStore.familyCount - MainDataStore.familyWeightStableHigh - MainDataStore.familyWeightStableLow);
                    wealthLow.text = string.Format(Localization.Get("WEALTH_LOW_COUNT") + " [{0}]", MainDataStore.familyWeightStableLow);
                    //Building
                    secondline.text = Localization.Get("BUILDING_STATUS");
                    profitBuildingCount.text = string.Format(Localization.Get("PROFIT_BUIDLING_COUNT") + " [{0}]", RealCityPrivateBuildingAI.profitBuildingCountFinal);
                    externalInvestments.text = string.Format(Localization.Get("EXTERNAL_INVESTMENTS") + " [{0}]", RealCityPrivateBuildingAI.profitBuildingMoneyFinal);
                    //Policy
                    thirdLine.text = Localization.Get("POLICY_COST");
                    minimumLivingAllowance.text = string.Format(Localization.Get("LIVING_ALLOWANCE") + " [{0}]", (MainDataStore.minimumLivingAllowanceFinal / 100));
                    unfinishedTransitionLost.text = string.Format(Localization.Get("UNFINISHED_DEAL_LOST") + " [{0}]", MainDataStore.unfinishedTransitionLostFinal);
                    outsideGovermentMoney.text = string.Format(Localization.Get("OUTSIDE_GOV_MONEY") + " [{0}]", (MainDataStore.outsideGovermentMoney / 100));
                    outsideTouristMoney.text = string.Format(Localization.Get("OUTSIDE_TOU_MONEY") + " [{0}]", (MainDataStore.outsideTouristMoney / 100));
                    //Tip
                    tip1.text = string.Format(Localization.Get("TIP1") + "  " + Localization.Get("USE_TMPE_TIP"));

                    refeshOnce = false;
                }
            }
        }
    }
}
