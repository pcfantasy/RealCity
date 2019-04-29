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
        private static readonly float HEIGHT = 780f;
        private static readonly float HEADER = 40f;
        private static readonly float SPACING = 15f;
        private static readonly float SPACING22 = 22f;
        private UIDragHandle m_DragHandler;
        private UIButton m_closeButton;
        private UILabel m_title;
        //1、citizen tax income
        private UILabel citizen_tax_income_title;
        private UILabel citizen_tax_income;
        //2、tourist for both citizen and tourist
        private UILabel city_tourism_income_title;
        private UILabel citizen_income;
        private UILabel tourist_income;
        //3、land income
        private UILabel land_income_title;
        private UILabel resident_high_landincome;
        private UILabel resident_low_landincome;
        private UILabel resident_high_eco_landincome;
        private UILabel resident_low_eco_landincome;
        private UILabel comm_high_landincome;
        private UILabel comm_low_landincome;
        private UILabel comm_lei_landincome;
        private UILabel comm_tou_landincome;
        private UILabel comm_eco_landincome;
        private UILabel indu_gen_landincome;
        private UILabel indu_farmer_landincome;
        private UILabel indu_foresty_landincome;
        private UILabel indu_oil_landincome;
        private UILabel indu_ore_landincome;
        private UILabel office_gen_landincome;
        private UILabel office_high_tech_landincome;
        //4、trade income
        private UILabel trade_income_title;
        private UILabel comm_high_tradeincome;
        private UILabel comm_low_tradeincome;
        private UILabel comm_lei_tradeincome;
        private UILabel comm_tou_tradeincome;
        private UILabel comm_eco_tradeincome;
        private UILabel indu_gen_tradeincome;
        private UILabel indu_farmer_tradeincome;
        private UILabel indu_foresty_tradeincome;
        private UILabel indu_oil_tradeincome;
        private UILabel indu_ore_tradeincome;
        //5、public transport income
        private UILabel public_transport_income_title;
        private UILabel from_bus;
        private UILabel from_tram;
        private UILabel from_train;
        private UILabel from_ship;
        private UILabel from_plane;
        private UILabel from_metro;
        private UILabel from_taxi;
        private UILabel from_cable_car;
        private UILabel from_monorail;
        //6、goverment income
        private UILabel goverment_income_title;
        private UILabel road_income_title;
        private UILabel garbage_income_title;
        private UILabel school_income_title;
        private UILabel playerIndustryIncomeTitle;
        private UILabel healthCareIncomeTitle;
        private UILabel fireStationIncomeTitle;
        private UILabel policeStationIncomeTitle;
        //7、all total income
        private UILabel all_total_income_ui;
        //used for display
        //citizen tax income
        public double citizen_tax_income_total;
        public double citizen_tax_income_percent;
        public double citizen_tax_income_forui;
        //tourist for both citizen and tourist
        public double city_tourism_income_total;
        public double city_tourism_income_percent;
        public double citizen_income_forui;
        public double tourist_income_forui;
        //land income
        public double city_land_income_total;
        public double city_land_income_percent;
        public double resident_high_landincome_forui;
        public double resident_low_landincome_forui;
        public double resident_high_eco_landincome_forui;
        public double resident_low_eco_landincome_forui;
        public double comm_high_landincome_forui;
        public double comm_low_landincome_forui;
        public double comm_lei_landincome_forui;
        public double comm_tou_landincome_forui;
        public double comm_eco_landincome_forui;
        public double indu_gen_landincome_forui;
        public double indu_farmer_landincome_forui;
        public double indu_foresty_landincome_forui;
        public double indu_oil_landincome_forui;
        public double indu_ore_landincome_forui;
        public double office_gen_landincome_forui;
        public double office_high_tech_landincome_forui;
        //trade income
        public double city_trade_income_total;
        public double city_trade_income_percent;
        public double comm_high_tradeincome_forui;
        public double comm_low_tradeincome_forui;
        public double comm_lei_tradeincome_forui;
        public double comm_tou_tradeincome_forui;
        public double comm_eco_tradeincome_forui;
        public double indu_gen_tradeincome_forui;
        public double indu_farmer_tradeincome_forui;
        public double indu_foresty_tradeincome_forui;
        public double indu_oil_tradeincome_forui;
        public double indu_ore_tradeincome_forui;
        public double garbage_income_forui;
        public double road_income_forui;
        public double playerIndustryIncomeForUI;
        public double school_income_forui;
        public double healthCareIncomeForUI;
        public double policeStationIncomeForUI;
        public double fireStationIncomeForUI;
        //transport income
        public double city_playerbuilding_income_total;
        public double city_playerbuilding_income_percent;
        public double city_transport_income_total;
        public double city_transport_income_percent;
        public static double bus_income;
        public static double metro_income;
        public static double tram_income;
        public static double train_income;
        public static double plane_income;
        public static double ship_income;
        public static double taxi_income;
        public static double cablecar_income;
        public static double monorail_income;
        //all total income
        public double all_total_income;
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
            m_DragHandler = AddUIComponent<UIDragHandle>();
            m_DragHandler.target = this;
            m_title = AddUIComponent<UILabel>();
            m_title.text = Localization.Get("CITY_INCOME_DATA");
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
            //1、citizen tax income
            citizen_tax_income_title = AddUIComponent<UILabel>();
            citizen_tax_income_title.text = Localization.Get("CITY_SALARY_TAX_INCOME_TITLE");
            citizen_tax_income_title.textScale = 1.1f;
            citizen_tax_income_title.relativePosition = new Vector3(SPACING, 50f);
            citizen_tax_income_title.autoSize = true;

            citizen_tax_income = AddUIComponent<UILabel>();
            citizen_tax_income.text = Localization.Get("SALARY_TAX_INCOME");
            citizen_tax_income.relativePosition = new Vector3(SPACING, citizen_tax_income_title.relativePosition.y + SPACING22);
            citizen_tax_income.autoSize = true;

            //2、City tourism income
            city_tourism_income_title = AddUIComponent<UILabel>();
            city_tourism_income_title.text = Localization.Get("TOURIST_INCOME_TITLE");
            city_tourism_income_title.textScale = 1.1f;
            city_tourism_income_title.relativePosition = new Vector3(SPACING, citizen_tax_income.relativePosition.y + SPACING22 + 10f);
            city_tourism_income_title.autoSize = true;

            citizen_income = AddUIComponent<UILabel>();
            citizen_income.text = Localization.Get("FROM_RESIDENT");
            citizen_income.relativePosition = new Vector3(SPACING, city_tourism_income_title.relativePosition.y + SPACING22);
            citizen_income.autoSize = true;

            tourist_income = AddUIComponent<UILabel>();
            tourist_income.text = Localization.Get("FROM_TOURIST");
            tourist_income.relativePosition = new Vector3(citizen_income.relativePosition.x + 450f, citizen_income.relativePosition.y);
            tourist_income.autoSize = true;

            //3、City land tax income
            land_income_title = AddUIComponent<UILabel>();
            land_income_title.text = Localization.Get("LAND_TAX_INCOME_TITLE");
            land_income_title.textScale = 1.1f;
            land_income_title.relativePosition = new Vector3(SPACING, citizen_income.relativePosition.y + SPACING22 + 10f);
            land_income_title.autoSize = true;

            resident_high_landincome = AddUIComponent<UILabel>();
            resident_high_landincome.text = Localization.Get("RESIDENT_HIGH_LAND_INCOME");
            resident_high_landincome.relativePosition = new Vector3(SPACING, land_income_title.relativePosition.y + SPACING22);
            resident_high_landincome.autoSize = true;

            resident_low_landincome = AddUIComponent<UILabel>();
            resident_low_landincome.text = Localization.Get("RESIDENT_LOW_LAND_INCOME");
            resident_low_landincome.relativePosition = new Vector3(resident_high_landincome.relativePosition.x + 450f, resident_high_landincome.relativePosition.y);
            resident_low_landincome.autoSize = true;

            resident_high_eco_landincome = AddUIComponent<UILabel>();
            resident_high_eco_landincome.text = Localization.Get("RESIDENT_HIGH_ECO_LAND_INCOME");
            resident_high_eco_landincome.relativePosition = new Vector3(SPACING, resident_high_landincome.relativePosition.y + SPACING22);
            resident_high_eco_landincome.autoSize = true;

            resident_low_eco_landincome = AddUIComponent<UILabel>();
            resident_low_eco_landincome.text = Localization.Get("RESIDENT_LOW_ECO_LAND_INCOME");
            resident_low_eco_landincome.relativePosition = new Vector3(resident_low_landincome.relativePosition.x, resident_low_landincome.relativePosition.y + SPACING22);
            resident_low_eco_landincome.autoSize = true;

            comm_high_landincome = AddUIComponent<UILabel>();
            comm_high_landincome.text = Localization.Get("COMMERICAL_HIGH_LAND_INCOME");
            comm_high_landincome.relativePosition = new Vector3(SPACING, resident_high_eco_landincome.relativePosition.y + SPACING22);
            comm_high_landincome.autoSize = true;

            comm_low_landincome = AddUIComponent<UILabel>();
            comm_low_landincome.text = Localization.Get("COMMERICAL_LOW_LAND_INCOME");
            comm_low_landincome.relativePosition = new Vector3(resident_low_eco_landincome.relativePosition.x, resident_low_eco_landincome.relativePosition.y + SPACING22);
            comm_low_landincome.autoSize = true;

            comm_eco_landincome = AddUIComponent<UILabel>();
            comm_eco_landincome.text = Localization.Get("COMMERICAL_ECO_LAND_INCOME");
            comm_eco_landincome.relativePosition = new Vector3(SPACING, comm_high_landincome.relativePosition.y + SPACING22);
            comm_eco_landincome.autoSize = true;

            comm_tou_landincome = AddUIComponent<UILabel>();
            comm_tou_landincome.text = Localization.Get("COMMERICAL_TOURISM_LAND_INCOME");
            comm_tou_landincome.relativePosition = new Vector3(comm_low_landincome.relativePosition.x, comm_low_landincome.relativePosition.y + SPACING22);
            comm_tou_landincome.autoSize = true;

            comm_lei_landincome = AddUIComponent<UILabel>();
            comm_lei_landincome.text = Localization.Get("COMMERICAL_LEISURE_LAND_INCOME");
            comm_lei_landincome.relativePosition = new Vector3(SPACING, comm_eco_landincome.relativePosition.y + SPACING22);
            comm_lei_landincome.autoSize = true;

            indu_gen_landincome = AddUIComponent<UILabel>();
            indu_gen_landincome.text = Localization.Get("INDUSTRIAL_GENERAL_LAND_INCOME");
            indu_gen_landincome.relativePosition = new Vector3(comm_tou_landincome.relativePosition.x, comm_tou_landincome.relativePosition.y + SPACING22);
            indu_gen_landincome.autoSize = true;

            indu_farmer_landincome = AddUIComponent<UILabel>();
            indu_farmer_landincome.text = Localization.Get("INDUSTRIAL_FARMING_LAND_INCOME");
            indu_farmer_landincome.relativePosition = new Vector3(SPACING, comm_lei_landincome.relativePosition.y + SPACING22);
            indu_farmer_landincome.autoSize = true;

            indu_foresty_landincome = AddUIComponent<UILabel>();
            indu_foresty_landincome.text = Localization.Get("INDUSTRIAL_FORESTY_LAND_INCOME");
            indu_foresty_landincome.relativePosition = new Vector3(indu_gen_landincome.relativePosition.x, indu_gen_landincome.relativePosition.y + SPACING22);
            indu_foresty_landincome.autoSize = true;

            indu_oil_landincome = AddUIComponent<UILabel>();
            indu_oil_landincome.text = Localization.Get("INDUSTRIAL_OIL_LAND_INCOME");
            indu_oil_landincome.relativePosition = new Vector3(SPACING, indu_farmer_landincome.relativePosition.y + SPACING22);
            indu_oil_landincome.autoSize = true;

            indu_ore_landincome = AddUIComponent<UILabel>();
            indu_ore_landincome.text = Localization.Get("INDUSTRIAL_ORE_LAND_INCOME");
            indu_ore_landincome.relativePosition = new Vector3(indu_foresty_landincome.relativePosition.x, indu_foresty_landincome.relativePosition.y + SPACING22);
            indu_ore_landincome.autoSize = true;

            office_gen_landincome = AddUIComponent<UILabel>();
            office_gen_landincome.text = Localization.Get("OFFICE_GENERAL_LAND_INCOME");
            office_gen_landincome.relativePosition = new Vector3(SPACING, indu_oil_landincome.relativePosition.y + SPACING22);
            office_gen_landincome.autoSize = true;

            office_high_tech_landincome = AddUIComponent<UILabel>();
            office_high_tech_landincome.text = Localization.Get("OFFICE_HIGH_TECH_LAND_INCOME");
            office_high_tech_landincome.relativePosition = new Vector3(indu_ore_landincome.relativePosition.x, indu_ore_landincome.relativePosition.y + SPACING22);
            office_high_tech_landincome.autoSize = true;

            //4、City trade tax income
            trade_income_title = AddUIComponent<UILabel>();
            trade_income_title.text = Localization.Get("CITY_TRADE_INCOME_TITLE");
            trade_income_title.textScale = 1.1f;
            trade_income_title.relativePosition = new Vector3(SPACING, office_gen_landincome.relativePosition.y + SPACING22 + 10f);
            trade_income_title.autoSize = true;

            comm_high_tradeincome = AddUIComponent<UILabel>();
            comm_high_tradeincome.text = Localization.Get("COMMERICAL_HIGH_TRADE_INCOME");
            comm_high_tradeincome.relativePosition = new Vector3(SPACING, trade_income_title.relativePosition.y + SPACING22);
            comm_high_tradeincome.autoSize = true;

            comm_low_tradeincome = AddUIComponent<UILabel>();
            comm_low_tradeincome.text = Localization.Get("COMMERICAL_LOW_TRADE_INCOME");
            comm_low_tradeincome.relativePosition = new Vector3(comm_high_tradeincome.relativePosition.x + 450f, comm_high_tradeincome.relativePosition.y);
            comm_low_tradeincome.autoSize = true;

            comm_eco_tradeincome = AddUIComponent<UILabel>();
            comm_eco_tradeincome.text = Localization.Get("COMMERICAL_ECO_TRADE_INCOME");
            comm_eco_tradeincome.relativePosition = new Vector3(SPACING, comm_high_tradeincome.relativePosition.y + SPACING22);
            comm_eco_tradeincome.autoSize = true;

            comm_tou_tradeincome = AddUIComponent<UILabel>();
            comm_tou_tradeincome.text = Localization.Get("COMMERICAL_TOURISM_TRADE_INCOME");
            comm_tou_tradeincome.relativePosition = new Vector3(comm_low_tradeincome.relativePosition.x, comm_low_tradeincome.relativePosition.y + SPACING22);
            comm_tou_tradeincome.autoSize = true;

            comm_lei_tradeincome = AddUIComponent<UILabel>();
            comm_lei_tradeincome.text = Localization.Get("COMMERICAL_LEISURE_TRADE_INCOME");
            comm_lei_tradeincome.relativePosition = new Vector3(SPACING, comm_eco_tradeincome.relativePosition.y + SPACING22);
            comm_lei_tradeincome.autoSize = true;

            indu_gen_tradeincome = AddUIComponent<UILabel>();
            indu_gen_tradeincome.text = Localization.Get("INDUSTRIAL_GENERAL_TRADE_INCOME");
            indu_gen_tradeincome.relativePosition = new Vector3(comm_tou_tradeincome.relativePosition.x, comm_tou_tradeincome.relativePosition.y + SPACING22);
            indu_gen_tradeincome.autoSize = true;

            indu_farmer_tradeincome = AddUIComponent<UILabel>();
            indu_farmer_tradeincome.text = Localization.Get("INDUSTRIAL_FARMING_TRADE_INCOME");
            indu_farmer_tradeincome.relativePosition = new Vector3(SPACING, comm_lei_tradeincome.relativePosition.y + SPACING22);
            indu_farmer_tradeincome.autoSize = true;

            indu_foresty_tradeincome = AddUIComponent<UILabel>();
            indu_foresty_tradeincome.text = Localization.Get("INDUSTRIAL_FORESTY_TRADE_INCOME");
            indu_foresty_tradeincome.relativePosition = new Vector3(indu_gen_tradeincome.relativePosition.x, indu_gen_tradeincome.relativePosition.y + SPACING22);
            indu_foresty_tradeincome.autoSize = true;

            indu_oil_tradeincome = AddUIComponent<UILabel>();
            indu_oil_tradeincome.text = Localization.Get("INDUSTRIAL_OIL_TRADE_INCOME");
            indu_oil_tradeincome.relativePosition = new Vector3(SPACING, indu_farmer_tradeincome.relativePosition.y + SPACING22);
            indu_oil_tradeincome.autoSize = true;

            indu_ore_tradeincome = AddUIComponent<UILabel>();
            indu_ore_tradeincome.text = Localization.Get("INDUSTRIAL_ORE_TRADE_INCOME");
            indu_ore_tradeincome.relativePosition = new Vector3(indu_foresty_tradeincome.relativePosition.x, indu_foresty_tradeincome.relativePosition.y + SPACING22);
            indu_ore_tradeincome.autoSize = true;

            //5、Public transport income
            public_transport_income_title = AddUIComponent<UILabel>();
            public_transport_income_title.text = Localization.Get("CITY_PUBLIC_TRANSPORT_INCOME_TITLE");
            public_transport_income_title.textScale = 1.1f;
            public_transport_income_title.relativePosition = new Vector3(SPACING, indu_oil_tradeincome.relativePosition.y + SPACING22 + 10f);
            public_transport_income_title.autoSize = true;

            from_bus = AddUIComponent<UILabel>();
            from_bus.text = Localization.Get("BUS");
            from_bus.relativePosition = new Vector3(SPACING, public_transport_income_title.relativePosition.y + SPACING22);
            from_bus.autoSize = true;

            from_tram = AddUIComponent<UILabel>();
            from_tram.text = Localization.Get("TRAM");
            from_tram.relativePosition = new Vector3(from_bus.relativePosition.x + 300f, from_bus.relativePosition.y);
            from_tram.autoSize = true;

            from_metro = AddUIComponent<UILabel>();
            from_metro.text = Localization.Get("TRAIN");
            from_metro.relativePosition = new Vector3(from_tram.relativePosition.x + 300f, from_tram.relativePosition.y);
            from_metro.autoSize = true;

            from_train = AddUIComponent<UILabel>();
            from_train.text = Localization.Get("SHIP");
            from_train.relativePosition = new Vector3(SPACING, from_bus.relativePosition.y + SPACING22);
            from_train.autoSize = true;

            from_ship = AddUIComponent<UILabel>();
            from_ship.text = Localization.Get("PLANE");
            from_ship.relativePosition = new Vector3(from_tram.relativePosition.x, from_tram.relativePosition.y + SPACING22);
            from_ship.autoSize = true;

            from_taxi = AddUIComponent<UILabel>();
            from_taxi.text = Localization.Get("METRO");
            from_taxi.relativePosition = new Vector3(from_metro.relativePosition.x, from_metro.relativePosition.y + SPACING22);
            from_taxi.autoSize = true;

            from_plane = AddUIComponent<UILabel>();
            from_plane.text = Localization.Get("TAXI");
            from_plane.relativePosition = new Vector3(SPACING, from_train.relativePosition.y + SPACING22);
            from_plane.autoSize = true;

            from_cable_car = AddUIComponent<UILabel>();
            from_cable_car.text = Localization.Get("CABLECAR");
            from_cable_car.relativePosition = new Vector3(from_ship.relativePosition.x, from_ship.relativePosition.y + SPACING22);
            from_cable_car.autoSize = true;

            from_monorail = AddUIComponent<UILabel>();
            from_monorail.text = Localization.Get("MONORAIL");
            from_monorail.relativePosition = new Vector3(from_taxi.relativePosition.x, from_taxi.relativePosition.y + SPACING22);
            from_monorail.autoSize = true;

            //6、Public transport income
            goverment_income_title = AddUIComponent<UILabel>();
            goverment_income_title.text = Localization.Get("CITY_PLAYER_BUILDING_INCOME_TITLE");
            goverment_income_title.textScale = 1.1f;
            goverment_income_title.relativePosition = new Vector3(SPACING, from_plane.relativePosition.y + SPACING22 + 10f);
            goverment_income_title.autoSize = true;

            garbage_income_title = AddUIComponent<UILabel>();
            garbage_income_title.text = Localization.Get("GARBAGE");
            garbage_income_title.relativePosition = new Vector3(SPACING, from_plane.relativePosition.y + 2 * SPACING22 + 10f);
            garbage_income_title.autoSize = true;

            school_income_title = AddUIComponent<UILabel>();
            school_income_title.text = Localization.Get("SCHOOL");
            school_income_title.relativePosition = new Vector3(from_cable_car.relativePosition.x, from_cable_car.relativePosition.y + 2 * SPACING22 + 10f);
            school_income_title.autoSize = true;

            road_income_title = AddUIComponent<UILabel>();
            road_income_title.text = Localization.Get("ROAD");
            road_income_title.relativePosition = new Vector3(from_monorail.relativePosition.x, from_monorail.relativePosition.y + 2 * SPACING22 + 10f);
            road_income_title.autoSize = true;

            playerIndustryIncomeTitle = AddUIComponent<UILabel>();
            playerIndustryIncomeTitle.text = Localization.Get("PLAYERINDUSTRY");
            playerIndustryIncomeTitle.relativePosition = new Vector3(garbage_income_title.relativePosition.x, garbage_income_title.relativePosition.y + SPACING22);
            playerIndustryIncomeTitle.autoSize = true;

            healthCareIncomeTitle = AddUIComponent<UILabel>();
            healthCareIncomeTitle.text = Localization.Get("HEALTHCARE");
            healthCareIncomeTitle.relativePosition = new Vector3(school_income_title.relativePosition.x, school_income_title.relativePosition.y + SPACING22);
            healthCareIncomeTitle.autoSize = true;

            fireStationIncomeTitle = AddUIComponent<UILabel>();
            fireStationIncomeTitle.text = Localization.Get("FIRESTATION");
            fireStationIncomeTitle.relativePosition = new Vector3(road_income_title.relativePosition.x, road_income_title.relativePosition.y + SPACING22);
            fireStationIncomeTitle.autoSize = true;

            policeStationIncomeTitle = AddUIComponent<UILabel>();
            policeStationIncomeTitle.text = Localization.Get("POLICESTATION");
            policeStationIncomeTitle.relativePosition = new Vector3(playerIndustryIncomeTitle.relativePosition.x, playerIndustryIncomeTitle.relativePosition.y + SPACING22);
            policeStationIncomeTitle.autoSize = true;

            all_total_income_ui = AddUIComponent<UILabel>();
            all_total_income_ui.text = Localization.Get("CITY_TOTAL_INCOME_TITLE");
            all_total_income_ui.textScale = 1.1f;
            all_total_income_ui.relativePosition = new Vector3(policeStationIncomeTitle.relativePosition.x, policeStationIncomeTitle.relativePosition.y + SPACING22 + 10f);
            all_total_income_ui.autoSize = true;
        }

        private void RefreshDisplayData()
        {
            uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
            uint num2 = currentFrameIndex & 255u;
            if (refeshOnce)
            {
                if (isVisible)
                {
                    process_data();
                    m_title.text = Localization.Get("CITY_INCOME_DATA");
                    citizen_tax_income_title.text = string.Format(Localization.Get("CITY_SALARY_TAX_INCOME_TITLE") + " [{0}]  [{1:N2}%]", citizen_tax_income_total, citizen_tax_income_percent * 100);
                    citizen_tax_income.text = string.Format(Localization.Get("SALARY_TAX_INCOME") + " [{0}]", citizen_tax_income_forui);
                    city_tourism_income_title.text = string.Format(Localization.Get("TOURIST_INCOME_TITLE") + " [{0}]  [{1:N2}%]", city_tourism_income_total, city_tourism_income_percent * 100);
                    citizen_income.text = string.Format(Localization.Get("FROM_RESIDENT") + " [{0}]", citizen_income_forui);
                    tourist_income.text = string.Format(Localization.Get("FROM_TOURIST") + " [{0}]", tourist_income_forui);
                    land_income_title.text = string.Format(Localization.Get("LAND_TAX_INCOME_TITLE") + " [{0}]  [{1:N2}%]", city_land_income_total, city_land_income_percent * 100);
                    resident_high_landincome.text = string.Format(Localization.Get("RESIDENT_HIGH_LAND_INCOME") + " [{0}]", resident_high_landincome_forui);
                    resident_low_landincome.text = string.Format(Localization.Get("RESIDENT_LOW_LAND_INCOME") + " [{0}]", resident_low_landincome_forui);
                    resident_high_eco_landincome.text = string.Format(Localization.Get("RESIDENT_HIGH_ECO_LAND_INCOME") + " [{0}]", resident_high_eco_landincome_forui);
                    resident_low_eco_landincome.text = string.Format(Localization.Get("RESIDENT_LOW_ECO_LAND_INCOME") + " [{0}]", resident_low_eco_landincome_forui);
                    comm_high_landincome.text = string.Format(Localization.Get("COMMERICAL_HIGH_LAND_INCOME") + " [{0}]", comm_high_landincome_forui);
                    comm_low_landincome.text = string.Format(Localization.Get("COMMERICAL_LOW_LAND_INCOME") + " [{0}]", comm_low_landincome_forui);
                    comm_lei_landincome.text = string.Format(Localization.Get("COMMERICAL_LEISURE_LAND_INCOME") + " [{0}]", comm_lei_landincome_forui);
                    comm_tou_landincome.text = string.Format(Localization.Get("COMMERICAL_TOURISM_LAND_INCOME") + " [{0}]", comm_tou_landincome_forui);
                    comm_eco_landincome.text = string.Format(Localization.Get("COMMERICAL_ECO_LAND_INCOME") + " [{0}]", comm_eco_landincome_forui);
                    indu_gen_landincome.text = string.Format(Localization.Get("INDUSTRIAL_GENERAL_LAND_INCOME") + " [{0}]", indu_gen_landincome_forui);
                    indu_farmer_landincome.text = string.Format(Localization.Get("INDUSTRIAL_FARMING_LAND_INCOME") + " [{0}]", indu_farmer_landincome_forui);
                    indu_foresty_landincome.text = string.Format(Localization.Get("INDUSTRIAL_FORESTY_LAND_INCOME") + " [{0}]", indu_foresty_landincome_forui);
                    indu_oil_landincome.text = string.Format(Localization.Get("INDUSTRIAL_OIL_LAND_INCOME") + " [{0}]", indu_oil_landincome_forui);
                    indu_ore_landincome.text = string.Format(Localization.Get("INDUSTRIAL_ORE_LAND_INCOME") + " [{0}]", indu_ore_landincome_forui);
                    office_gen_landincome.text = string.Format(Localization.Get("OFFICE_GENERAL_LAND_INCOME") + " [{0}]", office_gen_landincome_forui);
                    office_high_tech_landincome.text = string.Format(Localization.Get("OFFICE_HIGH_TECH_LAND_INCOME") + " [{0}]", office_high_tech_landincome_forui); ;
                    trade_income_title.text = string.Format(Localization.Get("CITY_TRADE_INCOME_TITLE") + " [{0}]  [{1:N2}%]", city_trade_income_total, city_trade_income_percent * 100);
                    comm_high_tradeincome.text = string.Format(Localization.Get("COMMERICAL_HIGH_TRADE_INCOME") + " [{0}]", comm_high_tradeincome_forui);
                    comm_low_tradeincome.text = string.Format(Localization.Get("COMMERICAL_LOW_TRADE_INCOME") + " [{0}]", comm_low_tradeincome_forui);
                    comm_lei_tradeincome.text = string.Format(Localization.Get("COMMERICAL_LEISURE_TRADE_INCOME") + " [{0}]", comm_lei_tradeincome_forui);
                    comm_tou_tradeincome.text = string.Format(Localization.Get("COMMERICAL_TOURISM_TRADE_INCOME") + " [{0}]", comm_tou_tradeincome_forui);
                    comm_eco_tradeincome.text = string.Format(Localization.Get("COMMERICAL_ECO_TRADE_INCOME") + " [{0}]", comm_eco_tradeincome_forui);
                    indu_gen_tradeincome.text = string.Format(Localization.Get("INDUSTRIAL_GENERAL_TRADE_INCOME") + " [{0}]", indu_gen_tradeincome_forui);
                    indu_farmer_tradeincome.text = string.Format(Localization.Get("INDUSTRIAL_FARMING_TRADE_INCOME") + " [{0}]", indu_farmer_tradeincome_forui);
                    indu_foresty_tradeincome.text = string.Format(Localization.Get("INDUSTRIAL_FORESTY_TRADE_INCOME") + " [{0}]", indu_foresty_tradeincome_forui);
                    indu_oil_tradeincome.text = string.Format(Localization.Get("INDUSTRIAL_OIL_TRADE_INCOME") + " [{0}]", indu_oil_tradeincome_forui);
                    indu_ore_tradeincome.text = string.Format(Localization.Get("INDUSTRIAL_ORE_TRADE_INCOME") + " [{0}]", indu_ore_tradeincome_forui);
                    public_transport_income_title.text = string.Format(Localization.Get("CITY_PUBLIC_TRANSPORT_INCOME_TITLE") + " [{0}]  [{1:N2}%]", city_transport_income_total, city_transport_income_percent * 100);
                    from_bus.text = string.Format(Localization.Get("BUS") + " [{0}]", bus_income);
                    from_tram.text = string.Format(Localization.Get("TRAM") + " [{0}]", tram_income);
                    from_train.text = string.Format(Localization.Get("TRAIN") + " [{0}]", train_income);
                    from_ship.text = string.Format(Localization.Get("SHIP") + " [{0}]", ship_income);
                    from_plane.text = string.Format(Localization.Get("PLANE") + " [{0}]", plane_income);
                    from_metro.text = string.Format(Localization.Get("METRO") + " [{0}]", metro_income);
                    from_taxi.text = string.Format(Localization.Get("TAXI") + " [{0}]", taxi_income);
                    from_cable_car.text = string.Format(Localization.Get("CABLECAR") + " [{0}]", cablecar_income);
                    from_monorail.text = string.Format(Localization.Get("MONORAIL") + " [{0}]", monorail_income);
                    goverment_income_title.text = string.Format(Localization.Get("CITY_PLAYER_BUILDING_INCOME_TITLE") + " [{0}]  [{1:N2}%]", city_playerbuilding_income_total, city_playerbuilding_income_percent * 100);
                    road_income_title.text = string.Format(Localization.Get("ROAD") + " [{0}]", road_income_forui);
                    garbage_income_title.text = string.Format(Localization.Get("GARBAGE") + " [{0}]", garbage_income_forui);
                    school_income_title.text = string.Format(Localization.Get("SCHOOL") + " [{0}]", school_income_forui);
                    playerIndustryIncomeTitle.text = string.Format(Localization.Get("PLAYERINDUSTRY") + " [{0}]", playerIndustryIncomeForUI);
                    healthCareIncomeTitle.text = string.Format(Localization.Get("HEALTHCARE") + " [{0}]", healthCareIncomeForUI);
                    fireStationIncomeTitle.text = string.Format(Localization.Get("FIRESTATION") + " [{0}]", fireStationIncomeForUI);
                    policeStationIncomeTitle.text = string.Format(Localization.Get("POLICESTATION") + " [{0}]", policeStationIncomeForUI);
                    all_total_income_ui.text = string.Format(Localization.Get("CITY_TOTAL_INCOME_TITLE") + " [{0}]", all_total_income);
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

        private void process_data()
        {
            city_playerbuilding_income_total = 0;
            city_playerbuilding_income_percent = 0;
            road_income_forui = 0f;
            playerIndustryIncomeForUI = 0f;
            healthCareIncomeForUI = 0f;
            policeStationIncomeForUI = 0f;
            fireStationIncomeForUI = 0f;
            garbage_income_forui = 0f;
            school_income_forui = 0f;
            citizen_tax_income_forui = 0;
            citizen_income_forui = 0;
            tourist_income_forui = 0;
            resident_high_landincome_forui = 0;
            resident_low_landincome_forui = 0;
            resident_high_eco_landincome_forui = 0;
            resident_low_eco_landincome_forui = 0;
            comm_high_landincome_forui = 0;
            comm_low_landincome_forui = 0;
            comm_lei_landincome_forui = 0;
            comm_tou_landincome_forui = 0;
            comm_eco_landincome_forui = 0;
            indu_gen_landincome_forui = 0;
            indu_farmer_landincome_forui = 0;
            indu_foresty_landincome_forui = 0;
            indu_oil_landincome_forui = 0;
            indu_ore_landincome_forui = 0;
            office_gen_landincome_forui = 0;
            office_high_tech_landincome_forui = 0;
            comm_high_tradeincome_forui = 0;
            comm_low_tradeincome_forui = 0;
            comm_lei_tradeincome_forui = 0;
            comm_tou_tradeincome_forui = 0;
            comm_eco_tradeincome_forui = 0;
            indu_gen_tradeincome_forui = 0;
            indu_farmer_tradeincome_forui = 0;
            indu_foresty_tradeincome_forui = 0;
            indu_oil_tradeincome_forui = 0;
            indu_ore_tradeincome_forui = 0;
            all_total_income = 0;
            citizen_tax_income_total = 0;
            city_land_income_total = 0;
            city_tourism_income_total = 0;
            city_trade_income_total = 0;
            city_transport_income_total = 0;
            citizen_tax_income_percent = 0f;
            city_land_income_percent = 0f;
            city_tourism_income_percent = 0f;
            city_trade_income_percent = 0f;
            city_transport_income_percent = 0f;
            int i;

            for (i = 0; i < 17; i++)
            {
                citizen_tax_income_forui += (double)RealCityEconomyManager.citizen_tax_income_forui[i]  / 100f;
                citizen_income_forui+= (double)RealCityEconomyManager.citizen_income_forui[i]  / 100f;
                tourist_income_forui+= (double)RealCityEconomyManager.tourist_income_forui[i]  / 100f;
                resident_high_landincome_forui+= (double)RealCityEconomyManager.resident_high_landincome_forui[i]  / 100f;
                resident_low_landincome_forui+= (double)RealCityEconomyManager.resident_low_landincome_forui[i]  / 100f;
                resident_high_eco_landincome_forui+= (double)RealCityEconomyManager.resident_high_eco_landincome_forui[i]  / 100f;
                resident_low_eco_landincome_forui+= (double)RealCityEconomyManager.resident_low_eco_landincome_forui[i]  / 100f;
                comm_high_landincome_forui+= (double)RealCityEconomyManager.comm_high_landincome_forui[i]  / 100f;
                comm_low_landincome_forui+= (double)RealCityEconomyManager.comm_low_landincome_forui[i]  / 100f;
                comm_lei_landincome_forui+= (double)RealCityEconomyManager.comm_lei_landincome_forui[i]  / 100f;
                comm_tou_landincome_forui+= (double)RealCityEconomyManager.comm_tou_landincome_forui[i]  / 100f;
                comm_eco_landincome_forui+= (double)RealCityEconomyManager.comm_eco_landincome_forui[i]  / 100f;
                indu_gen_landincome_forui+= (double)RealCityEconomyManager.indu_gen_landincome_forui[i]  / 100f;
                indu_farmer_landincome_forui+= (double)RealCityEconomyManager.indu_farmer_landincome_forui[i]  / 100f;
                indu_foresty_landincome_forui+= (double)RealCityEconomyManager.indu_foresty_landincome_forui[i]  / 100f;
                indu_oil_landincome_forui+= (double)RealCityEconomyManager.indu_oil_landincome_forui[i]  / 100f;
                indu_ore_landincome_forui+= (double)RealCityEconomyManager.indu_ore_landincome_forui[i]  / 100f;
                office_gen_landincome_forui+= (double)RealCityEconomyManager.office_gen_landincome_forui[i]  / 100f;
                office_high_tech_landincome_forui+= (double)RealCityEconomyManager.office_high_tech_landincome_forui[i]  / 100f;
                comm_high_tradeincome_forui+= (double)RealCityEconomyManager.comm_high_tradeincome_forui[i]  / 100f;
                comm_low_tradeincome_forui+= (double)RealCityEconomyManager.comm_low_tradeincome_forui[i]  / 100f;
                comm_lei_tradeincome_forui+= (double)RealCityEconomyManager.comm_lei_tradeincome_forui[i]  / 100f;
                comm_tou_tradeincome_forui+= (double)RealCityEconomyManager.comm_tou_tradeincome_forui[i]  / 100f;
                comm_eco_tradeincome_forui+= (double)RealCityEconomyManager.comm_eco_tradeincome_forui[i]  / 100f;
                indu_gen_tradeincome_forui+= (double)RealCityEconomyManager.indu_gen_tradeincome_forui[i]  / 100f;
                indu_farmer_tradeincome_forui+= (double)RealCityEconomyManager.indu_farmer_tradeincome_forui[i]  / 100f;
                indu_foresty_tradeincome_forui+= (double)RealCityEconomyManager.indu_foresty_tradeincome_forui[i]  / 100f;
                indu_oil_tradeincome_forui+= (double)RealCityEconomyManager.indu_oil_tradeincome_forui[i]  / 100f;
                indu_ore_tradeincome_forui+= (double)RealCityEconomyManager.indu_ore_tradeincome_forui[i]  / 100f;
                road_income_forui += (double)RealCityEconomyManager.road_income_forui[i]  / 100f;
                playerIndustryIncomeForUI += (double)RealCityEconomyManager.playerIndustryIncomeForUI[i]  / 100f;
                healthCareIncomeForUI += (double)RealCityEconomyManager.healthCareIncomeForUI[i] / 100f;
                policeStationIncomeForUI += (double)RealCityEconomyManager.policeStationIncomeForUI[i] / 100f;
                fireStationIncomeForUI += (double)RealCityEconomyManager.fireStationIncomeForUI[i] / 100f;
                garbage_income_forui += (double)RealCityEconomyManager.garbage_income_forui[i]  / 100f;
                school_income_forui += (double)RealCityEconomyManager.school_income_forui[i]  / 100f;
            }

            citizen_tax_income_forui -= (double)RealCityEconomyManager.citizen_tax_income_forui[MainDataStore.update_money_count]  / 100f;
            citizen_income_forui -= (double)RealCityEconomyManager.citizen_income_forui[MainDataStore.update_money_count]  / 100f;
            tourist_income_forui -= (double)RealCityEconomyManager.tourist_income_forui[MainDataStore.update_money_count]  / 100f;
            resident_high_landincome_forui -= (double)RealCityEconomyManager.resident_high_landincome_forui[MainDataStore.update_money_count]  / 100f;
            resident_low_landincome_forui -= (double)RealCityEconomyManager.resident_low_landincome_forui[MainDataStore.update_money_count]  / 100f;
            resident_high_eco_landincome_forui -= (double)RealCityEconomyManager.resident_high_eco_landincome_forui[MainDataStore.update_money_count]  / 100f;
            resident_low_eco_landincome_forui -= (double)RealCityEconomyManager.resident_low_eco_landincome_forui[MainDataStore.update_money_count]  / 100f;
            comm_high_landincome_forui -= (double)RealCityEconomyManager.comm_high_landincome_forui[MainDataStore.update_money_count]  / 100f;
            comm_low_landincome_forui -= (double)RealCityEconomyManager.comm_low_landincome_forui[MainDataStore.update_money_count]  / 100f;
            comm_lei_landincome_forui -= (double)RealCityEconomyManager.comm_lei_landincome_forui[MainDataStore.update_money_count]  / 100f;
            comm_tou_landincome_forui -= (double)RealCityEconomyManager.comm_tou_landincome_forui[MainDataStore.update_money_count]  / 100f;
            comm_eco_landincome_forui -= (double)RealCityEconomyManager.comm_eco_landincome_forui[MainDataStore.update_money_count]  / 100f;
            indu_gen_landincome_forui -= (double)RealCityEconomyManager.indu_gen_landincome_forui[MainDataStore.update_money_count]  / 100f;
            indu_farmer_landincome_forui -= (double)RealCityEconomyManager.indu_farmer_landincome_forui[MainDataStore.update_money_count]  / 100f;
            indu_foresty_landincome_forui -= (double)RealCityEconomyManager.indu_foresty_landincome_forui[MainDataStore.update_money_count]  / 100f;
            indu_oil_landincome_forui -= (double)RealCityEconomyManager.indu_oil_landincome_forui[MainDataStore.update_money_count]  / 100f;
            indu_ore_landincome_forui -= (double)RealCityEconomyManager.indu_ore_landincome_forui[MainDataStore.update_money_count]  / 100f;
            office_gen_landincome_forui -= (double)RealCityEconomyManager.office_gen_landincome_forui[MainDataStore.update_money_count]  / 100f;
            office_high_tech_landincome_forui -= (double)RealCityEconomyManager.office_high_tech_landincome_forui[MainDataStore.update_money_count]  / 100f;
            comm_high_tradeincome_forui -= (double)RealCityEconomyManager.comm_high_tradeincome_forui[MainDataStore.update_money_count]  / 100f;
            comm_low_tradeincome_forui -= (double)RealCityEconomyManager.comm_low_tradeincome_forui[MainDataStore.update_money_count]  / 100f;
            comm_lei_tradeincome_forui -= (double)RealCityEconomyManager.comm_lei_tradeincome_forui[MainDataStore.update_money_count]  / 100f;
            comm_tou_tradeincome_forui -= (double)RealCityEconomyManager.comm_tou_tradeincome_forui[MainDataStore.update_money_count]  / 100f;
            comm_eco_tradeincome_forui -= (double)RealCityEconomyManager.comm_eco_tradeincome_forui[MainDataStore.update_money_count]  / 100f;
            indu_gen_tradeincome_forui -= (double)RealCityEconomyManager.indu_gen_tradeincome_forui[MainDataStore.update_money_count]  / 100f;
            indu_farmer_tradeincome_forui -= (double)RealCityEconomyManager.indu_farmer_tradeincome_forui[MainDataStore.update_money_count]  / 100f;
            indu_foresty_tradeincome_forui -= (double)RealCityEconomyManager.indu_foresty_tradeincome_forui[MainDataStore.update_money_count]  / 100f;
            indu_oil_tradeincome_forui -= (double)RealCityEconomyManager.indu_oil_tradeincome_forui[MainDataStore.update_money_count]  / 100f;
            indu_ore_tradeincome_forui -= (double)RealCityEconomyManager.indu_ore_tradeincome_forui[MainDataStore.update_money_count]  / 100f;

            road_income_forui -= (double)RealCityEconomyManager.road_income_forui[MainDataStore.update_money_count]  / 100f;
            playerIndustryIncomeForUI -= (double)RealCityEconomyManager.playerIndustryIncomeForUI[MainDataStore.update_money_count]  / 100f;
            fireStationIncomeForUI -= (double)RealCityEconomyManager.fireStationIncomeForUI[MainDataStore.update_money_count] / 100f;
            healthCareIncomeForUI -= (double)RealCityEconomyManager.healthCareIncomeForUI[MainDataStore.update_money_count] / 100f;
            policeStationIncomeForUI -= (double)RealCityEconomyManager.policeStationIncomeForUI[MainDataStore.update_money_count] / 100f;
            garbage_income_forui -= (double)RealCityEconomyManager.garbage_income_forui[MainDataStore.update_money_count]  / 100f;
            school_income_forui -= (double)RealCityEconomyManager.school_income_forui[MainDataStore.update_money_count]  / 100f;

            citizen_tax_income_total += citizen_tax_income_forui;
            city_land_income_total += resident_high_landincome_forui;
            city_land_income_total += resident_low_landincome_forui;
            city_land_income_total += resident_high_eco_landincome_forui;
            city_land_income_total += resident_low_eco_landincome_forui;
            city_land_income_total += comm_eco_landincome_forui;
            city_land_income_total += comm_lei_landincome_forui;
            city_land_income_total += comm_tou_landincome_forui;
            city_land_income_total += comm_high_landincome_forui;
            city_land_income_total += comm_low_landincome_forui;
            city_land_income_total += indu_gen_landincome_forui;
            city_land_income_total += indu_foresty_landincome_forui;
            city_land_income_total += indu_farmer_landincome_forui;
            city_land_income_total += indu_oil_landincome_forui;
            city_land_income_total += indu_ore_landincome_forui;
            city_land_income_total += office_gen_landincome_forui;
            city_land_income_total += office_high_tech_landincome_forui;
            city_trade_income_total += comm_eco_tradeincome_forui;
            city_trade_income_total += comm_lei_tradeincome_forui;
            city_trade_income_total += comm_tou_tradeincome_forui;
            city_trade_income_total += comm_high_tradeincome_forui;
            city_trade_income_total += comm_low_tradeincome_forui;
            city_trade_income_total += indu_gen_tradeincome_forui;
            city_trade_income_total += indu_foresty_tradeincome_forui;
            city_trade_income_total += indu_farmer_tradeincome_forui;
            city_trade_income_total += indu_oil_tradeincome_forui;
            city_trade_income_total += indu_ore_tradeincome_forui;
            city_tourism_income_total = citizen_income_forui + tourist_income_forui;
            city_transport_income_total += bus_income;
            city_transport_income_total += tram_income;
            city_transport_income_total += train_income;
            city_transport_income_total += ship_income;
            city_transport_income_total += taxi_income;
            city_transport_income_total += plane_income;
            city_transport_income_total += metro_income;
            city_transport_income_total += cablecar_income;
            city_transport_income_total += monorail_income;
            city_playerbuilding_income_total += road_income_forui;
            city_playerbuilding_income_total += playerIndustryIncomeForUI;
            city_playerbuilding_income_total += healthCareIncomeForUI;
            city_playerbuilding_income_total += fireStationIncomeForUI;
            city_playerbuilding_income_total += policeStationIncomeForUI;
            city_playerbuilding_income_total += garbage_income_forui;
            city_playerbuilding_income_total += school_income_forui;
            all_total_income = city_playerbuilding_income_total + citizen_tax_income_total + city_land_income_total + city_tourism_income_total + city_trade_income_total + city_transport_income_total;

            if (all_total_income != 0)
            {
                citizen_tax_income_percent = citizen_tax_income_total / all_total_income;
                city_land_income_percent = city_land_income_total / all_total_income;
                city_tourism_income_percent = city_tourism_income_total / all_total_income;
                city_trade_income_percent = city_trade_income_total / all_total_income;
                city_transport_income_percent = city_transport_income_total / all_total_income;
                city_playerbuilding_income_percent = city_playerbuilding_income_total / all_total_income;
            }
        }
    }
}
