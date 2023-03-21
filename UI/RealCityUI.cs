using ColossalFramework;
using ColossalFramework.UI;
using UnityEngine;
using RealCity.CustomManager;
using RealCity.Util;

namespace RealCity.UI
{
    public class RealCityUI : UIPanel
    {
        public static readonly string cacheName = "RealCityUI";
        private static readonly float WIDTH = 900f;
        private static readonly float HEIGHT = 700f;
        private static readonly float HEADER = 40f;
        private static readonly float SPACING = 15f;
        private static readonly float SPACING22 = 22f;
        private UIDragHandle dragHandler;
        private UIButton closeButton;
        private UILabel title;
        //1、citizen tax income
        private UILabel citizenTaxIncomeTitle;
        private UILabel citizenTaxIncome;
        //2、tourist for both citizen and tourist
        private UILabel cityTourisincomeTitle;
        private UILabel citizenIncome;
        private UILabel touristIncome;
        //3、land income
        private UILabel landIncomeTitle;
        private UILabel residentHighLandIncome;
        private UILabel residentLowLandIncome;
        private UILabel residentHighEcoLandIncome;
        private UILabel residentLowEcoLandIncome;
        private UILabel comHighLandIncome;
        private UILabel comLowLandIncome;
        private UILabel comLeiLandIncome;
        private UILabel comTouLandIncome;
        private UILabel comEcoLandIncome;
        private UILabel induGenLandIncome;
        private UILabel induFarmerLandIncome;
        private UILabel induForestyLandIncome;
        private UILabel induOilLandIncome;
        private UILabel induOreLandIncome;
        private UILabel officeGenLandIncome;
        private UILabel officeHighTechLandIncome;
        //4、trade income
        private UILabel tradeIncomeTitle;
        private UILabel comHighTradeIncome;
        private UILabel comLowTradeIncome;
        private UILabel comLeiTradeIncome;
        private UILabel comTouTradeIncome;
        private UILabel comeco_tradeincome;
        private UILabel induGenTradeIncome;
        private UILabel induFarmerTradeIncome;
        private UILabel induForestyTradeIncome;
        private UILabel induOilTradeIncome;
        private UILabel induOreTradeIncome;
        //5、public transport income
        private UILabel publicTransportIncomeTitle;
        //6、goverment income
        private UILabel govermentIncomeTitle;
        private UILabel roadIncomeTitle;
        private UILabel garbageIncomeTitle;
        private UILabel schoolIncomeTitle;
        private UILabel playerIndustryIncomeTitle;
        private UILabel healthCareIncomeTitle;
        private UILabel fireStationIncomeTitle;
        private UILabel policeStationIncomeTitle;
        //7、all total income
        private UILabel allTotalIncomeUI;
        //used for display
        //citizen tax income
        public double citizenTaxIncomeTotal;
        public double citizenTaxIncomePercent;
        public double citizenTaxIncomeForUI;
        //tourist for both citizen and tourist
        public double cityTourismIncomeTotal;
        public double cityTourismIncomePercent;
        public double citizenIncomeForUI;
        public double touristIncomeForUI;
        //land income
        public double cityLandIncomeTotal;
        public double cityLandIncomePercent;
        public double residentHighLandIncomeForUI;
        public double residentLowLandIncomeForUI;
        public double residentHighEcoLandIncomeForUI;
        public double residentLowEcoLandIncomeForUI;
        public double commHighLandIncomeForUI;
        public double commLowLandIncomeForUI;
        public double commLeiLandIncomeForUI;
        public double commTouLandIncomeForUI;
        public double commEcoLandIncomeForUI;
        public double induGenLandIncomeForUI;
        public double induFarmerLandIncomeForUI;
        public double induForestyLandIncomeForUI;
        public double induOilLandIncomeForUI;
        public double induOreLandIncomeForUI;
        public double officeGenLandIncomeForUI;
        public double officeHighTechLandIncomeForUI;
        //trade income
        public double cityTradeIncomeTotal;
        public double cityTradeIncomePercent;
        public double commHighTradeIncomeForUI;
        public double commLowTradeIncomeForUI;
        public double commLeiTradeIncomeForUI;
        public double commTouTradeIncomeForUI;
        public double commEcoTradeIncomeForUI;
        public double induGenTradeIncomeForUI;
        public double induFarmerTradeIncomeForUI;
        public double induForestyTradeIncomeForUI;
        public double induOilTradeIncomeForUI;
        public double induOreTradeIncomeForUI;
        public double garbageIncomeForUI;
        public double roadIncomeForUI;
        public double playerIndustryIncomeForUI;
        public double schoolIncomeForUI;
        public double healthCareIncomeForUI;
        public double policeStationIncomeForUI;
        public double fireStationIncomeForUI;
        //transport income
        public double cityPlayerbuildingIncomeTotal;
        public double cityPlayerbuildingIncomePercent;
        public double cityTransportIncomeTotal;
        public double cityTransportIncomePercent;
        //all total income
        public double allTotalIncome;
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
            relativePosition = new Vector3((Loader.parentGuiView.fixedWidth / 2 + 20f), 170f);
            opacity = 1f;
            cachedName = cacheName;
            dragHandler = AddUIComponent<UIDragHandle>();
            dragHandler.target = this;
            title = AddUIComponent<UILabel>();
            title.text = Localization.Get("CITY_INCOME_DATA");
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
            //1、citizen tax income
            citizenTaxIncomeTitle = AddUIComponent<UILabel>();
            citizenTaxIncomeTitle.text = Localization.Get("CITY_SALARY_TAX_INCOME_TITLE");
            citizenTaxIncomeTitle.textScale = 1.1f;
            citizenTaxIncomeTitle.relativePosition = new Vector3(SPACING, 50f);
            citizenTaxIncomeTitle.autoSize = true;

            citizenTaxIncome = AddUIComponent<UILabel>();
            citizenTaxIncome.text = Localization.Get("SALARY_TAX_INCOME");
            citizenTaxIncome.relativePosition = new Vector3(SPACING, citizenTaxIncomeTitle.relativePosition.y + SPACING22);
            citizenTaxIncome.autoSize = true;

            //2、City tourism income
            cityTourisincomeTitle = AddUIComponent<UILabel>();
            cityTourisincomeTitle.text = Localization.Get("TOURIST_INCOME_TITLE");
            cityTourisincomeTitle.textScale = 1.1f;
            cityTourisincomeTitle.relativePosition = new Vector3(SPACING, citizenTaxIncome.relativePosition.y + SPACING22 + 10f);
            cityTourisincomeTitle.autoSize = true;

            citizenIncome = AddUIComponent<UILabel>();
            citizenIncome.text = Localization.Get("FROM_RESIDENT");
            citizenIncome.relativePosition = new Vector3(SPACING, cityTourisincomeTitle.relativePosition.y + SPACING22);
            citizenIncome.autoSize = true;

            touristIncome = AddUIComponent<UILabel>();
            touristIncome.text = Localization.Get("FROM_TOURIST");
            touristIncome.relativePosition = new Vector3(citizenIncome.relativePosition.x + 450f, citizenIncome.relativePosition.y);
            touristIncome.autoSize = true;

            //3、City land tax income
            landIncomeTitle = AddUIComponent<UILabel>();
            landIncomeTitle.text = Localization.Get("LAND_TAX_INCOME_TITLE");
            landIncomeTitle.textScale = 1.1f;
            landIncomeTitle.relativePosition = new Vector3(SPACING, citizenIncome.relativePosition.y + SPACING22 + 10f);
            landIncomeTitle.autoSize = true;

            residentHighLandIncome = AddUIComponent<UILabel>();
            residentHighLandIncome.text = Localization.Get("RESIDENT_HIGH_LAND_INCOME");
            residentHighLandIncome.relativePosition = new Vector3(SPACING, landIncomeTitle.relativePosition.y + SPACING22);
            residentHighLandIncome.autoSize = true;

            residentLowLandIncome = AddUIComponent<UILabel>();
            residentLowLandIncome.text = Localization.Get("RESIDENT_LOW_LAND_INCOME");
            residentLowLandIncome.relativePosition = new Vector3(residentHighLandIncome.relativePosition.x + 450f, residentHighLandIncome.relativePosition.y);
            residentLowLandIncome.autoSize = true;

            residentHighEcoLandIncome = AddUIComponent<UILabel>();
            residentHighEcoLandIncome.text = Localization.Get("RESIDENT_HIGH_ECO_LAND_INCOME");
            residentHighEcoLandIncome.relativePosition = new Vector3(SPACING, residentHighLandIncome.relativePosition.y + SPACING22);
            residentHighEcoLandIncome.autoSize = true;

            residentLowEcoLandIncome = AddUIComponent<UILabel>();
            residentLowEcoLandIncome.text = Localization.Get("RESIDENT_LOW_ECO_LAND_INCOME");
            residentLowEcoLandIncome.relativePosition = new Vector3(residentLowLandIncome.relativePosition.x, residentLowLandIncome.relativePosition.y + SPACING22);
            residentLowEcoLandIncome.autoSize = true;

            comHighLandIncome = AddUIComponent<UILabel>();
            comHighLandIncome.text = Localization.Get("COMMERICAL_HIGH_LAND_INCOME");
            comHighLandIncome.relativePosition = new Vector3(SPACING, residentHighEcoLandIncome.relativePosition.y + SPACING22);
            comHighLandIncome.autoSize = true;

            comLowLandIncome = AddUIComponent<UILabel>();
            comLowLandIncome.text = Localization.Get("COMMERICAL_LOW_LAND_INCOME");
            comLowLandIncome.relativePosition = new Vector3(residentLowEcoLandIncome.relativePosition.x, residentLowEcoLandIncome.relativePosition.y + SPACING22);
            comLowLandIncome.autoSize = true;

            comEcoLandIncome = AddUIComponent<UILabel>();
            comEcoLandIncome.text = Localization.Get("COMMERICAL_ECO_LAND_INCOME");
            comEcoLandIncome.relativePosition = new Vector3(SPACING, comHighLandIncome.relativePosition.y + SPACING22);
            comEcoLandIncome.autoSize = true;

            comTouLandIncome = AddUIComponent<UILabel>();
            comTouLandIncome.text = Localization.Get("COMMERICAL_TOURISM_LAND_INCOME");
            comTouLandIncome.relativePosition = new Vector3(comLowLandIncome.relativePosition.x, comLowLandIncome.relativePosition.y + SPACING22);
            comTouLandIncome.autoSize = true;

            comLeiLandIncome = AddUIComponent<UILabel>();
            comLeiLandIncome.text = Localization.Get("COMMERICAL_LEISURE_LAND_INCOME");
            comLeiLandIncome.relativePosition = new Vector3(SPACING, comEcoLandIncome.relativePosition.y + SPACING22);
            comLeiLandIncome.autoSize = true;

            induGenLandIncome = AddUIComponent<UILabel>();
            induGenLandIncome.text = Localization.Get("INDUSTRIAL_GENERAL_LAND_INCOME");
            induGenLandIncome.relativePosition = new Vector3(comTouLandIncome.relativePosition.x, comTouLandIncome.relativePosition.y + SPACING22);
            induGenLandIncome.autoSize = true;

            induFarmerLandIncome = AddUIComponent<UILabel>();
            induFarmerLandIncome.text = Localization.Get("INDUSTRIAL_FARMING_LAND_INCOME");
            induFarmerLandIncome.relativePosition = new Vector3(SPACING, comLeiLandIncome.relativePosition.y + SPACING22);
            induFarmerLandIncome.autoSize = true;

            induForestyLandIncome = AddUIComponent<UILabel>();
            induForestyLandIncome.text = Localization.Get("INDUSTRIAL_FORESTY_LAND_INCOME");
            induForestyLandIncome.relativePosition = new Vector3(induGenLandIncome.relativePosition.x, induGenLandIncome.relativePosition.y + SPACING22);
            induForestyLandIncome.autoSize = true;

            induOilLandIncome = AddUIComponent<UILabel>();
            induOilLandIncome.text = Localization.Get("INDUSTRIAL_OIL_LAND_INCOME");
            induOilLandIncome.relativePosition = new Vector3(SPACING, induFarmerLandIncome.relativePosition.y + SPACING22);
            induOilLandIncome.autoSize = true;

            induOreLandIncome = AddUIComponent<UILabel>();
            induOreLandIncome.text = Localization.Get("INDUSTRIAL_ORE_LAND_INCOME");
            induOreLandIncome.relativePosition = new Vector3(induForestyLandIncome.relativePosition.x, induForestyLandIncome.relativePosition.y + SPACING22);
            induOreLandIncome.autoSize = true;

            officeGenLandIncome = AddUIComponent<UILabel>();
            officeGenLandIncome.text = Localization.Get("OFFICE_GENERAL_LAND_INCOME");
            officeGenLandIncome.relativePosition = new Vector3(SPACING, induOilLandIncome.relativePosition.y + SPACING22);
            officeGenLandIncome.autoSize = true;

            officeHighTechLandIncome = AddUIComponent<UILabel>();
            officeHighTechLandIncome.text = Localization.Get("OFFICE_HIGH_TECH_LAND_INCOME");
            officeHighTechLandIncome.relativePosition = new Vector3(induOreLandIncome.relativePosition.x, induOreLandIncome.relativePosition.y + SPACING22);
            officeHighTechLandIncome.autoSize = true;

            //4、City trade tax income
            tradeIncomeTitle = AddUIComponent<UILabel>();
            tradeIncomeTitle.text = Localization.Get("CITY_TRADE_INCOME_TITLE");
            tradeIncomeTitle.textScale = 1.1f;
            tradeIncomeTitle.relativePosition = new Vector3(SPACING, officeGenLandIncome.relativePosition.y + SPACING22 + 10f);
            tradeIncomeTitle.autoSize = true;

            comHighTradeIncome = AddUIComponent<UILabel>();
            comHighTradeIncome.text = Localization.Get("COMMERICAL_HIGH_TRADE_INCOME");
            comHighTradeIncome.relativePosition = new Vector3(SPACING, tradeIncomeTitle.relativePosition.y + SPACING22);
            comHighTradeIncome.autoSize = true;

            comLowTradeIncome = AddUIComponent<UILabel>();
            comLowTradeIncome.text = Localization.Get("COMMERICAL_LOW_TRADE_INCOME");
            comLowTradeIncome.relativePosition = new Vector3(comHighTradeIncome.relativePosition.x + 450f, comHighTradeIncome.relativePosition.y);
            comLowTradeIncome.autoSize = true;

            comeco_tradeincome = AddUIComponent<UILabel>();
            comeco_tradeincome.text = Localization.Get("COMMERICAL_ECO_TRADE_INCOME");
            comeco_tradeincome.relativePosition = new Vector3(SPACING, comHighTradeIncome.relativePosition.y + SPACING22);
            comeco_tradeincome.autoSize = true;

            comTouTradeIncome = AddUIComponent<UILabel>();
            comTouTradeIncome.text = Localization.Get("COMMERICAL_TOURISM_TRADE_INCOME");
            comTouTradeIncome.relativePosition = new Vector3(comLowTradeIncome.relativePosition.x, comLowTradeIncome.relativePosition.y + SPACING22);
            comTouTradeIncome.autoSize = true;

            comLeiTradeIncome = AddUIComponent<UILabel>();
            comLeiTradeIncome.text = Localization.Get("COMMERICAL_LEISURE_TRADE_INCOME");
            comLeiTradeIncome.relativePosition = new Vector3(SPACING, comeco_tradeincome.relativePosition.y + SPACING22);
            comLeiTradeIncome.autoSize = true;

            induGenTradeIncome = AddUIComponent<UILabel>();
            induGenTradeIncome.text = Localization.Get("INDUSTRIAL_GENERAL_TRADE_INCOME");
            induGenTradeIncome.relativePosition = new Vector3(comTouTradeIncome.relativePosition.x, comTouTradeIncome.relativePosition.y + SPACING22);
            induGenTradeIncome.autoSize = true;

            induFarmerTradeIncome = AddUIComponent<UILabel>();
            induFarmerTradeIncome.text = Localization.Get("INDUSTRIAL_FARMING_TRADE_INCOME");
            induFarmerTradeIncome.relativePosition = new Vector3(SPACING, comLeiTradeIncome.relativePosition.y + SPACING22);
            induFarmerTradeIncome.autoSize = true;

            induForestyTradeIncome = AddUIComponent<UILabel>();
            induForestyTradeIncome.text = Localization.Get("INDUSTRIAL_FORESTY_TRADE_INCOME");
            induForestyTradeIncome.relativePosition = new Vector3(induGenTradeIncome.relativePosition.x, induGenTradeIncome.relativePosition.y + SPACING22);
            induForestyTradeIncome.autoSize = true;

            induOilTradeIncome = AddUIComponent<UILabel>();
            induOilTradeIncome.text = Localization.Get("INDUSTRIAL_OIL_TRADE_INCOME");
            induOilTradeIncome.relativePosition = new Vector3(SPACING, induFarmerTradeIncome.relativePosition.y + SPACING22);
            induOilTradeIncome.autoSize = true;

            induOreTradeIncome = AddUIComponent<UILabel>();
            induOreTradeIncome.text = Localization.Get("INDUSTRIAL_ORE_TRADE_INCOME");
            induOreTradeIncome.relativePosition = new Vector3(induForestyTradeIncome.relativePosition.x, induForestyTradeIncome.relativePosition.y + SPACING22);
            induOreTradeIncome.autoSize = true;

            //5、Public transport income
            publicTransportIncomeTitle = AddUIComponent<UILabel>();
            publicTransportIncomeTitle.text = Localization.Get("CITY_PUBLIC_TRANSPORT_INCOME_TITLE");
            publicTransportIncomeTitle.textScale = 1.1f;
            publicTransportIncomeTitle.relativePosition = new Vector3(SPACING, induOilTradeIncome.relativePosition.y + SPACING22 + 10f);
            publicTransportIncomeTitle.autoSize = true;

            //6、Public transport income
            govermentIncomeTitle = AddUIComponent<UILabel>();
            govermentIncomeTitle.text = Localization.Get("CITY_PLAYER_BUILDING_INCOME_TITLE");
            govermentIncomeTitle.textScale = 1.1f;
            govermentIncomeTitle.relativePosition = new Vector3(SPACING, publicTransportIncomeTitle.relativePosition.y + SPACING22 + 10f);
            govermentIncomeTitle.autoSize = true;

            garbageIncomeTitle = AddUIComponent<UILabel>();
            garbageIncomeTitle.text = Localization.Get("GARBAGE");
            garbageIncomeTitle.relativePosition = new Vector3(SPACING, publicTransportIncomeTitle.relativePosition.y + 2 * SPACING22 + 10f);
            garbageIncomeTitle.autoSize = true;

            schoolIncomeTitle = AddUIComponent<UILabel>();
            schoolIncomeTitle.text = Localization.Get("SCHOOL");
            schoolIncomeTitle.relativePosition = new Vector3(garbageIncomeTitle.relativePosition.x + 300f, garbageIncomeTitle.relativePosition.y);
            schoolIncomeTitle.autoSize = true;

            roadIncomeTitle = AddUIComponent<UILabel>();
            roadIncomeTitle.text = Localization.Get("ROAD");
            roadIncomeTitle.relativePosition = new Vector3(schoolIncomeTitle.relativePosition.x + 300f, schoolIncomeTitle.relativePosition.y);
            roadIncomeTitle.autoSize = true;

            playerIndustryIncomeTitle = AddUIComponent<UILabel>();
            playerIndustryIncomeTitle.text = Localization.Get("PLAYERINDUSTRY");
            playerIndustryIncomeTitle.relativePosition = new Vector3(garbageIncomeTitle.relativePosition.x, garbageIncomeTitle.relativePosition.y + SPACING22);
            playerIndustryIncomeTitle.autoSize = true;

            healthCareIncomeTitle = AddUIComponent<UILabel>();
            healthCareIncomeTitle.text = Localization.Get("HEALTHCARE");
            healthCareIncomeTitle.relativePosition = new Vector3(schoolIncomeTitle.relativePosition.x, schoolIncomeTitle.relativePosition.y + SPACING22);
            healthCareIncomeTitle.autoSize = true;

            fireStationIncomeTitle = AddUIComponent<UILabel>();
            fireStationIncomeTitle.text = Localization.Get("FIRESTATION");
            fireStationIncomeTitle.relativePosition = new Vector3(roadIncomeTitle.relativePosition.x, roadIncomeTitle.relativePosition.y + SPACING22);
            fireStationIncomeTitle.autoSize = true;

            policeStationIncomeTitle = AddUIComponent<UILabel>();
            policeStationIncomeTitle.text = Localization.Get("POLICESTATION");
            policeStationIncomeTitle.relativePosition = new Vector3(playerIndustryIncomeTitle.relativePosition.x, playerIndustryIncomeTitle.relativePosition.y + SPACING22);
            policeStationIncomeTitle.autoSize = true;

            allTotalIncomeUI = AddUIComponent<UILabel>();
            allTotalIncomeUI.text = Localization.Get("CITY_TOTAL_INCOME_TITLE");
            allTotalIncomeUI.textScale = 1.1f;
            allTotalIncomeUI.relativePosition = new Vector3(policeStationIncomeTitle.relativePosition.x, policeStationIncomeTitle.relativePosition.y + SPACING22 + 10f);
            allTotalIncomeUI.autoSize = true;
        }

        private void RefreshDisplayData()
        {
            if (refeshOnce)
            {
                if (isVisible)
                {
                    ProcessData();
                    title.text = Localization.Get("CITY_INCOME_DATA");
                    citizenTaxIncomeTitle.text = string.Format(Localization.Get("CITY_SALARY_TAX_INCOME_TITLE") + " [{0}]  [{1:N2}%]", citizenTaxIncomeTotal, citizenTaxIncomePercent * 100);
                    citizenTaxIncome.text = string.Format(Localization.Get("SALARY_TAX_INCOME") + " [{0}]", citizenTaxIncomeForUI);
                    cityTourisincomeTitle.text = string.Format(Localization.Get("TOURIST_INCOME_TITLE") + " [{0}]  [{1:N2}%]", cityTourismIncomeTotal, cityTourismIncomePercent * 100);
                    citizenIncome.text = string.Format(Localization.Get("FROM_RESIDENT") + " [{0}]", citizenIncomeForUI);
                    touristIncome.text = string.Format(Localization.Get("FROM_TOURIST") + " [{0}]", touristIncomeForUI);
                    landIncomeTitle.text = string.Format(Localization.Get("LAND_TAX_INCOME_TITLE") + " [{0}]  [{1:N2}%]", cityLandIncomeTotal, cityLandIncomePercent * 100);
                    residentHighLandIncome.text = string.Format(Localization.Get("RESIDENT_HIGH_LAND_INCOME") + " [{0}]", residentHighLandIncomeForUI);
                    residentLowLandIncome.text = string.Format(Localization.Get("RESIDENT_LOW_LAND_INCOME") + " [{0}]", residentLowLandIncomeForUI);
                    residentHighEcoLandIncome.text = string.Format(Localization.Get("RESIDENT_HIGH_ECO_LAND_INCOME") + " [{0}]", residentHighEcoLandIncomeForUI);
                    residentLowEcoLandIncome.text = string.Format(Localization.Get("RESIDENT_LOW_ECO_LAND_INCOME") + " [{0}]", residentLowEcoLandIncomeForUI);
                    comHighLandIncome.text = string.Format(Localization.Get("COMMERICAL_HIGH_LAND_INCOME") + " [{0}]", commHighLandIncomeForUI);
                    comLowLandIncome.text = string.Format(Localization.Get("COMMERICAL_LOW_LAND_INCOME") + " [{0}]", commLowLandIncomeForUI);
                    comLeiLandIncome.text = string.Format(Localization.Get("COMMERICAL_LEISURE_LAND_INCOME") + " [{0}]", commLeiLandIncomeForUI);
                    comTouLandIncome.text = string.Format(Localization.Get("COMMERICAL_TOURISM_LAND_INCOME") + " [{0}]", commTouLandIncomeForUI);
                    comEcoLandIncome.text = string.Format(Localization.Get("COMMERICAL_ECO_LAND_INCOME") + " [{0}]", commEcoLandIncomeForUI);
                    induGenLandIncome.text = string.Format(Localization.Get("INDUSTRIAL_GENERAL_LAND_INCOME") + " [{0}]", induGenLandIncomeForUI);
                    induFarmerLandIncome.text = string.Format(Localization.Get("INDUSTRIAL_FARMING_LAND_INCOME") + " [{0}]", induFarmerLandIncomeForUI);
                    induForestyLandIncome.text = string.Format(Localization.Get("INDUSTRIAL_FORESTY_LAND_INCOME") + " [{0}]", induForestyLandIncomeForUI);
                    induOilLandIncome.text = string.Format(Localization.Get("INDUSTRIAL_OIL_LAND_INCOME") + " [{0}]", induOilLandIncomeForUI);
                    induOreLandIncome.text = string.Format(Localization.Get("INDUSTRIAL_ORE_LAND_INCOME") + " [{0}]", induOreLandIncomeForUI);
                    officeGenLandIncome.text = string.Format(Localization.Get("OFFICE_GENERAL_LAND_INCOME") + " [{0}]", officeGenLandIncomeForUI);
                    officeHighTechLandIncome.text = string.Format(Localization.Get("OFFICE_HIGH_TECH_LAND_INCOME") + " [{0}]", officeHighTechLandIncomeForUI); ;
                    tradeIncomeTitle.text = string.Format(Localization.Get("CITY_TRADE_INCOME_TITLE") + " [{0}]  [{1:N2}%]", cityTradeIncomeTotal, cityTradeIncomePercent * 100);
                    comHighTradeIncome.text = string.Format(Localization.Get("COMMERICAL_HIGH_TRADE_INCOME") + " [{0}]", commHighTradeIncomeForUI);
                    comLowTradeIncome.text = string.Format(Localization.Get("COMMERICAL_LOW_TRADE_INCOME") + " [{0}]", commLowTradeIncomeForUI);
                    comLeiTradeIncome.text = string.Format(Localization.Get("COMMERICAL_LEISURE_TRADE_INCOME") + " [{0}]", commLeiTradeIncomeForUI);
                    comTouTradeIncome.text = string.Format(Localization.Get("COMMERICAL_TOURISM_TRADE_INCOME") + " [{0}]", commTouTradeIncomeForUI);
                    comeco_tradeincome.text = string.Format(Localization.Get("COMMERICAL_ECO_TRADE_INCOME") + " [{0}]", commEcoTradeIncomeForUI);
                    induGenTradeIncome.text = string.Format(Localization.Get("INDUSTRIAL_GENERAL_TRADE_INCOME") + " [{0}]", induGenTradeIncomeForUI);
                    induFarmerTradeIncome.text = string.Format(Localization.Get("INDUSTRIAL_FARMING_TRADE_INCOME") + " [{0}]", induFarmerTradeIncomeForUI);
                    induForestyTradeIncome.text = string.Format(Localization.Get("INDUSTRIAL_FORESTY_TRADE_INCOME") + " [{0}]", induForestyTradeIncomeForUI);
                    induOilTradeIncome.text = string.Format(Localization.Get("INDUSTRIAL_OIL_TRADE_INCOME") + " [{0}]", induOilTradeIncomeForUI);
                    induOreTradeIncome.text = string.Format(Localization.Get("INDUSTRIAL_ORE_TRADE_INCOME") + " [{0}]", induOreTradeIncomeForUI);
                    publicTransportIncomeTitle.text = string.Format(Localization.Get("CITY_PUBLIC_TRANSPORT_INCOME_TITLE") + " [{0}]  [{1:N2}%]", cityTransportIncomeTotal, cityTransportIncomePercent * 100);
                    govermentIncomeTitle.text = string.Format(Localization.Get("CITY_PLAYER_BUILDING_INCOME_TITLE") + " [{0}]  [{1:N2}%]", cityPlayerbuildingIncomeTotal, cityPlayerbuildingIncomePercent * 100);
                    roadIncomeTitle.text = string.Format(Localization.Get("ROAD") + " [{0}]", roadIncomeForUI);
                    garbageIncomeTitle.text = string.Format(Localization.Get("GARBAGE") + " [{0}]", garbageIncomeForUI);
                    schoolIncomeTitle.text = string.Format(Localization.Get("SCHOOL") + " [{0}]", schoolIncomeForUI);
                    playerIndustryIncomeTitle.text = string.Format(Localization.Get("PLAYERINDUSTRY") + " [{0}]", playerIndustryIncomeForUI);
                    healthCareIncomeTitle.text = string.Format(Localization.Get("HEALTHCARE") + " [{0}]", healthCareIncomeForUI);
                    fireStationIncomeTitle.text = string.Format(Localization.Get("FIRESTATION") + " [{0}]", fireStationIncomeForUI);
                    policeStationIncomeTitle.text = string.Format(Localization.Get("POLICESTATION") + " [{0}]", policeStationIncomeForUI);
                    allTotalIncomeUI.text = string.Format(Localization.Get("CITY_TOTAL_INCOME_TITLE") + " [{0}]", allTotalIncome);
                    refeshOnce = false;
                }
            }
        }

        private void ProcessData()
        {
            cityPlayerbuildingIncomeTotal = 0;
            cityPlayerbuildingIncomePercent = 0;
            roadIncomeForUI = 0f;
            playerIndustryIncomeForUI = 0f;
            healthCareIncomeForUI = 0f;
            policeStationIncomeForUI = 0f;
            fireStationIncomeForUI = 0f;
            garbageIncomeForUI = 0f;
            schoolIncomeForUI = 0f;
            citizenTaxIncomeForUI = 0;
            citizenIncomeForUI = 0;
            touristIncomeForUI = 0;
            residentHighLandIncomeForUI = 0;
            residentLowLandIncomeForUI = 0;
            residentHighEcoLandIncomeForUI = 0;
            residentLowEcoLandIncomeForUI = 0;
            commHighLandIncomeForUI = 0;
            commLowLandIncomeForUI = 0;
            commLeiLandIncomeForUI = 0;
            commTouLandIncomeForUI = 0;
            commEcoLandIncomeForUI = 0;
            induGenLandIncomeForUI = 0;
            induFarmerLandIncomeForUI = 0;
            induForestyLandIncomeForUI = 0;
            induOilLandIncomeForUI = 0;
            induOreLandIncomeForUI = 0;
            officeGenLandIncomeForUI = 0;
            officeHighTechLandIncomeForUI = 0;
            commHighTradeIncomeForUI = 0;
            commLowTradeIncomeForUI = 0;
            commLeiTradeIncomeForUI = 0;
            commTouTradeIncomeForUI = 0;
            commEcoTradeIncomeForUI = 0;
            induGenTradeIncomeForUI = 0;
            induFarmerTradeIncomeForUI = 0;
            induForestyTradeIncomeForUI = 0;
            induOilTradeIncomeForUI = 0;
            induOreTradeIncomeForUI = 0;
            allTotalIncome = 0;
            citizenTaxIncomeTotal = 0;
            cityLandIncomeTotal = 0;
            cityTourismIncomeTotal = 0;
            cityTradeIncomeTotal = 0;
            cityTransportIncomeTotal = 0;
            citizenTaxIncomePercent = 0f;
            cityLandIncomePercent = 0f;
            cityTourismIncomePercent = 0f;
            cityTradeIncomePercent = 0f;
            cityTransportIncomePercent = 0f;
            int i;

            for (i = 0; i < 17; i++)
            {
                citizenTaxIncomeForUI += (double)RealCityEconomyManager.citizenTaxIncomeForUI[i]  / 100f;
                citizenIncomeForUI+= (double)RealCityEconomyManager.citizenIncomeForUI[i]  / 100f;
                touristIncomeForUI+= (double)RealCityEconomyManager.touristIncomeForUI[i]  / 100f;
                residentHighLandIncomeForUI+= (double)RealCityEconomyManager.residentHighLandIncomeForUI[i]  / 100f;
                residentLowLandIncomeForUI+= (double)RealCityEconomyManager.residentLowLandIncomeForUI[i]  / 100f;
                residentHighEcoLandIncomeForUI+= (double)RealCityEconomyManager.residentHighEcoLandIncomeForUI[i]  / 100f;
                residentLowEcoLandIncomeForUI+= (double)RealCityEconomyManager.residentLowEcoLandIncomeForUI[i]  / 100f;
                commHighLandIncomeForUI+= (double)RealCityEconomyManager.commHighLandIncomeForUI[i]  / 100f;
                commLowLandIncomeForUI+= (double)RealCityEconomyManager.commLowLandIncomeForUI[i]  / 100f;
                commLeiLandIncomeForUI+= (double)RealCityEconomyManager.commLeiLandIncomeForUI[i]  / 100f;
                commTouLandIncomeForUI+= (double)RealCityEconomyManager.commTouLandIncomeForUI[i]  / 100f;
                commEcoLandIncomeForUI+= (double)RealCityEconomyManager.commEcoLandIncomeForUI[i]  / 100f;
                induGenLandIncomeForUI+= (double)RealCityEconomyManager.induGenLandIncomeForUI[i]  / 100f;
                induFarmerLandIncomeForUI+= (double)RealCityEconomyManager.induFarmerLandIncomeForUI[i]  / 100f;
                induForestyLandIncomeForUI+= (double)RealCityEconomyManager.induForestyLandIncomeForUI[i]  / 100f;
                induOilLandIncomeForUI+= (double)RealCityEconomyManager.induOilLandIncomeForUI[i]  / 100f;
                induOreLandIncomeForUI+= (double)RealCityEconomyManager.induOreLandIncomeForUI[i]  / 100f;
                officeGenLandIncomeForUI+= (double)RealCityEconomyManager.officeGenLandIncomeForUI[i]  / 100f;
                officeHighTechLandIncomeForUI+= (double)RealCityEconomyManager.officeHighTechLandIncomeForUI[i]  / 100f;
                commHighTradeIncomeForUI+= (double)RealCityEconomyManager.commHighTradeIncomeForUI[i]  / 100f;
                commLowTradeIncomeForUI+= (double)RealCityEconomyManager.commLowTradeIncomeForUI[i]  / 100f;
                commLeiTradeIncomeForUI+= (double)RealCityEconomyManager.commLeiTradeIncomeForUI[i]  / 100f;
                commTouTradeIncomeForUI+= (double)RealCityEconomyManager.commTouTradeIncomeForUI[i]  / 100f;
                commEcoTradeIncomeForUI+= (double)RealCityEconomyManager.commEcoTradeIncomeForUI[i]  / 100f;
                induGenTradeIncomeForUI+= (double)RealCityEconomyManager.induGenTradeIncomeForUI[i]  / 100f;
                induFarmerTradeIncomeForUI+= (double)RealCityEconomyManager.induFarmerTradeIncomeForUI[i]  / 100f;
                induForestyTradeIncomeForUI+= (double)RealCityEconomyManager.induForestyLandIncomeForUI[i]  / 100f;
                induOilTradeIncomeForUI+= (double)RealCityEconomyManager.induOilTradeIncomeForUI[i]  / 100f;
                induOreTradeIncomeForUI+= (double)RealCityEconomyManager.induOreTradeIncomeForUI[i]  / 100f;
                roadIncomeForUI += (double)RealCityEconomyManager.roadIncomeForUI[i]  / 100f;
                playerIndustryIncomeForUI += (double)RealCityEconomyManager.playerIndustryIncomeForUI[i]  / 100f;
                healthCareIncomeForUI += (double)RealCityEconomyManager.healthCareIncomeForUI[i] / 100f;
                policeStationIncomeForUI += (double)RealCityEconomyManager.policeStationIncomeForUI[i] / 100f;
                fireStationIncomeForUI += (double)RealCityEconomyManager.fireStationIncomeForUI[i] / 100f;
                garbageIncomeForUI += (double)RealCityEconomyManager.garbageIncomeForUI[i]  / 100f;
                schoolIncomeForUI += (double)RealCityEconomyManager.schoolIncomeForUI[i]  / 100f;
            }

            citizenTaxIncomeForUI -= (double)RealCityEconomyManager.citizenTaxIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            citizenIncomeForUI -= (double)RealCityEconomyManager.citizenIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            touristIncomeForUI -= (double)RealCityEconomyManager.touristIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            residentHighLandIncomeForUI -= (double)RealCityEconomyManager.residentHighLandIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            residentLowLandIncomeForUI -= (double)RealCityEconomyManager.residentLowLandIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            residentHighEcoLandIncomeForUI -= (double)RealCityEconomyManager.residentHighEcoLandIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            residentLowEcoLandIncomeForUI -= (double)RealCityEconomyManager.residentLowEcoLandIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            commHighLandIncomeForUI -= (double)RealCityEconomyManager.commHighLandIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            commLowLandIncomeForUI -= (double)RealCityEconomyManager.commLowLandIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            commLeiLandIncomeForUI -= (double)RealCityEconomyManager.commLeiLandIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            commTouLandIncomeForUI -= (double)RealCityEconomyManager.commTouLandIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            commEcoLandIncomeForUI -= (double)RealCityEconomyManager.commEcoLandIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            induGenLandIncomeForUI -= (double)RealCityEconomyManager.induGenLandIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            induFarmerLandIncomeForUI -= (double)RealCityEconomyManager.induFarmerLandIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            induForestyLandIncomeForUI -= (double)RealCityEconomyManager.induForestyLandIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            induOilLandIncomeForUI -= (double)RealCityEconomyManager.induOilLandIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            induOreLandIncomeForUI -= (double)RealCityEconomyManager.induOreLandIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            officeGenLandIncomeForUI -= (double)RealCityEconomyManager.officeGenLandIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            officeHighTechLandIncomeForUI -= (double)RealCityEconomyManager.officeHighTechLandIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            commHighTradeIncomeForUI -= (double)RealCityEconomyManager.commHighTradeIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            commLowTradeIncomeForUI -= (double)RealCityEconomyManager.commLowTradeIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            commLeiTradeIncomeForUI -= (double)RealCityEconomyManager.commLeiTradeIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            commTouTradeIncomeForUI -= (double)RealCityEconomyManager.commTouTradeIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            commEcoTradeIncomeForUI -= (double)RealCityEconomyManager.commEcoTradeIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            induGenTradeIncomeForUI -= (double)RealCityEconomyManager.induGenTradeIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            induFarmerTradeIncomeForUI -= (double)RealCityEconomyManager.induFarmerTradeIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            induForestyTradeIncomeForUI -= (double)RealCityEconomyManager.induForestyLandIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            induOilTradeIncomeForUI -= (double)RealCityEconomyManager.induOilTradeIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            induOreTradeIncomeForUI -= (double)RealCityEconomyManager.induOreTradeIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;

            roadIncomeForUI -= (double)RealCityEconomyManager.roadIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            playerIndustryIncomeForUI -= (double)RealCityEconomyManager.playerIndustryIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            fireStationIncomeForUI -= (double)RealCityEconomyManager.fireStationIncomeForUI[MainDataStore.updateMoneyCount] / 100f;
            healthCareIncomeForUI -= (double)RealCityEconomyManager.healthCareIncomeForUI[MainDataStore.updateMoneyCount] / 100f;
            policeStationIncomeForUI -= (double)RealCityEconomyManager.policeStationIncomeForUI[MainDataStore.updateMoneyCount] / 100f;
            garbageIncomeForUI -= (double)RealCityEconomyManager.garbageIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;
            schoolIncomeForUI -= (double)RealCityEconomyManager.schoolIncomeForUI[MainDataStore.updateMoneyCount]  / 100f;

            citizenTaxIncomeTotal += citizenTaxIncomeForUI;
            cityLandIncomeTotal += residentHighLandIncomeForUI;
            cityLandIncomeTotal += residentLowLandIncomeForUI;
            cityLandIncomeTotal += residentHighEcoLandIncomeForUI;
            cityLandIncomeTotal += residentLowEcoLandIncomeForUI;
            cityLandIncomeTotal += commEcoLandIncomeForUI;
            cityLandIncomeTotal += commLeiLandIncomeForUI;
            cityLandIncomeTotal += commTouLandIncomeForUI;
            cityLandIncomeTotal += commHighLandIncomeForUI;
            cityLandIncomeTotal += commLowLandIncomeForUI;
            cityLandIncomeTotal += induGenLandIncomeForUI;
            cityLandIncomeTotal += induForestyLandIncomeForUI;
            cityLandIncomeTotal += induFarmerLandIncomeForUI;
            cityLandIncomeTotal += induOilLandIncomeForUI;
            cityLandIncomeTotal += induOreLandIncomeForUI;
            cityLandIncomeTotal += officeGenLandIncomeForUI;
            cityLandIncomeTotal += officeHighTechLandIncomeForUI;
            cityTradeIncomeTotal += commEcoTradeIncomeForUI;
            cityTradeIncomeTotal += commLeiTradeIncomeForUI;
            cityTradeIncomeTotal += commTouTradeIncomeForUI;
            cityTradeIncomeTotal += commHighTradeIncomeForUI;
            cityTradeIncomeTotal += commLowTradeIncomeForUI;
            cityTradeIncomeTotal += induGenTradeIncomeForUI;
            cityTradeIncomeTotal += induForestyTradeIncomeForUI;
            cityTradeIncomeTotal += induFarmerTradeIncomeForUI;
            cityTradeIncomeTotal += induOilTradeIncomeForUI;
            cityTradeIncomeTotal += induOreTradeIncomeForUI;
            cityTourismIncomeTotal = citizenIncomeForUI + touristIncomeForUI;
            cityTransportIncomeTotal = (double)MainDataStore.publicTransportFee / 100;
            cityPlayerbuildingIncomeTotal += roadIncomeForUI;
            cityPlayerbuildingIncomeTotal += playerIndustryIncomeForUI;
            cityPlayerbuildingIncomeTotal += healthCareIncomeForUI;
            cityPlayerbuildingIncomeTotal += fireStationIncomeForUI;
            cityPlayerbuildingIncomeTotal += policeStationIncomeForUI;
            cityPlayerbuildingIncomeTotal += garbageIncomeForUI;
            cityPlayerbuildingIncomeTotal += schoolIncomeForUI;
            allTotalIncome = cityPlayerbuildingIncomeTotal + citizenTaxIncomeTotal + cityLandIncomeTotal + cityTourismIncomeTotal + cityTradeIncomeTotal + cityTransportIncomeTotal;

            if (allTotalIncome != 0)
            {
                citizenTaxIncomePercent = citizenTaxIncomeTotal / allTotalIncome;
                cityLandIncomePercent = cityLandIncomeTotal / allTotalIncome;
                cityTourismIncomePercent = cityTourismIncomeTotal / allTotalIncome;
                cityTradeIncomePercent = cityTradeIncomeTotal / allTotalIncome;
                cityTransportIncomePercent = cityTransportIncomeTotal / allTotalIncome;
                cityPlayerbuildingIncomePercent = cityPlayerbuildingIncomeTotal / allTotalIncome;
            }
        }
    }
}
