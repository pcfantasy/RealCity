using System.Collections.Generic;
using ColossalFramework;
using ColossalFramework.UI;
using UnityEngine;
using System.Collections;
using System.Reflection;
using RealCity.CustomManager;
using RealCity.Util;

namespace RealCity.UI
{
    public class RealCityUI : UIPanel
    {
        public static readonly string cacheName = "RealCityUI";
        private static float WIDTH = 700f;
        private static readonly float HEIGHT = 780f;
        private static readonly float HEADER = 40f;
        private static readonly float SPACING = 15f;
        private static readonly float SPACING22 = 22f;
        private ItemClass.Availability CurrentMode;
        public static RealCityUI instance;
        private Dictionary<string, UILabel> _valuesControlContainer = new Dictionary<string, UILabel>(16);
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
            this.RefreshDisplayData();
            base.Update();
        }

        public override void Start()
        {
            base.Start();
            instance = this;
            base.size = new Vector2(WIDTH, HEIGHT);
            base.backgroundSprite = "MenuPanel";
            this.canFocus = true;
            this.isInteractive = true;
            this.BringToFront();
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 + 150f), 170f);
            base.opacity = 1f;
            base.cachedName = cacheName;
            this.CurrentMode = Singleton<ToolManager>.instance.m_properties.m_mode;
            this.m_DragHandler = base.AddUIComponent<UIDragHandle>();
            this.m_DragHandler.target = this;
            this.m_title = base.AddUIComponent<UILabel>();
            this.m_title.text = Localization.Get("CITY_INCOME_DATA");
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
            //1、citizen tax income
            this.citizen_tax_income_title = base.AddUIComponent<UILabel>();
            this.citizen_tax_income_title.text = Localization.Get("CITY_SALARY_TAX_INCOME_TITLE");
            this.citizen_tax_income_title.textScale = 1.1f;
            this.citizen_tax_income_title.relativePosition = new Vector3(SPACING, 50f);
            this.citizen_tax_income_title.autoSize = true;

            this.citizen_tax_income = base.AddUIComponent<UILabel>();
            this.citizen_tax_income.text = Localization.Get("SALARY_TAX_INCOME");
            this.citizen_tax_income.relativePosition = new Vector3(SPACING, this.citizen_tax_income_title.relativePosition.y + SPACING22);
            this.citizen_tax_income.autoSize = true;

            //2、City tourism income
            this.city_tourism_income_title = base.AddUIComponent<UILabel>();
            this.city_tourism_income_title.text = Localization.Get("TOURIST_INCOME_TITLE");
            this.city_tourism_income_title.textScale = 1.1f;
            this.city_tourism_income_title.relativePosition = new Vector3(SPACING, this.citizen_tax_income.relativePosition.y + SPACING22 + 10f);
            this.city_tourism_income_title.autoSize = true;

            this.citizen_income = base.AddUIComponent<UILabel>();
            this.citizen_income.text = Localization.Get("FROM_RESIDENT");
            this.citizen_income.relativePosition = new Vector3(SPACING, this.city_tourism_income_title.relativePosition.y + SPACING22);
            this.citizen_income.autoSize = true;

            this.tourist_income = base.AddUIComponent<UILabel>();
            this.tourist_income.text = Localization.Get("FROM_TOURIST");
            this.tourist_income.relativePosition = new Vector3(this.citizen_income.relativePosition.x + this.citizen_income.width + SPACING + 140f, this.citizen_income.relativePosition.y);
            this.tourist_income.autoSize = true;

            //3、City land tax income
            this.land_income_title = base.AddUIComponent<UILabel>();
            this.land_income_title.text = Localization.Get("LAND_TAX_INCOME_TITLE");
            this.land_income_title.textScale = 1.1f;
            this.land_income_title.relativePosition = new Vector3(SPACING, this.citizen_income.relativePosition.y + SPACING22 + 10f);
            this.land_income_title.autoSize = true;

            this.resident_high_landincome = base.AddUIComponent<UILabel>();
            this.resident_high_landincome.text = Localization.Get("RESIDENT_HIGH_LAND_INCOME");
            this.resident_high_landincome.relativePosition = new Vector3(SPACING, this.land_income_title.relativePosition.y + SPACING22);
            this.resident_high_landincome.autoSize = true;

            this.resident_low_landincome = base.AddUIComponent<UILabel>();
            this.resident_low_landincome.text = Localization.Get("RESIDENT_LOW_LAND_INCOME");
            this.resident_low_landincome.relativePosition = new Vector3(this.resident_high_landincome.relativePosition.x + this.resident_high_landincome.width + SPACING + 120f, this.resident_high_landincome.relativePosition.y);
            this.resident_low_landincome.autoSize = true;

            this.resident_high_eco_landincome = base.AddUIComponent<UILabel>();
            this.resident_high_eco_landincome.text = Localization.Get("RESIDENT_HIGH_ECO_LAND_INCOME");
            this.resident_high_eco_landincome.relativePosition = new Vector3(SPACING, this.resident_high_landincome.relativePosition.y + SPACING22);
            this.resident_high_eco_landincome.autoSize = true;

            this.resident_low_eco_landincome = base.AddUIComponent<UILabel>();
            this.resident_low_eco_landincome.text = Localization.Get("RESIDENT_LOW_ECO_LAND_INCOME");
            this.resident_low_eco_landincome.relativePosition = new Vector3(this.resident_low_landincome.relativePosition.x, this.resident_low_landincome.relativePosition.y + SPACING22);
            this.resident_low_eco_landincome.autoSize = true;

            this.comm_high_landincome = base.AddUIComponent<UILabel>();
            this.comm_high_landincome.text = Localization.Get("COMMERICAL_HIGH_LAND_INCOME");
            this.comm_high_landincome.relativePosition = new Vector3(SPACING, this.resident_high_eco_landincome.relativePosition.y + SPACING22);
            this.comm_high_landincome.autoSize = true;

            this.comm_low_landincome = base.AddUIComponent<UILabel>();
            this.comm_low_landincome.text = Localization.Get("COMMERICAL_LOW_LAND_INCOME");
            this.comm_low_landincome.relativePosition = new Vector3(this.resident_low_eco_landincome.relativePosition.x, this.resident_low_eco_landincome.relativePosition.y + SPACING22);
            this.comm_low_landincome.autoSize = true;

            this.comm_eco_landincome = base.AddUIComponent<UILabel>();
            this.comm_eco_landincome.text = Localization.Get("COMMERICAL_ECO_LAND_INCOME");
            this.comm_eco_landincome.relativePosition = new Vector3(SPACING, this.comm_high_landincome.relativePosition.y + SPACING22);
            this.comm_eco_landincome.autoSize = true;

            this.comm_tou_landincome = base.AddUIComponent<UILabel>();
            this.comm_tou_landincome.text = Localization.Get("COMMERICAL_TOURISM_LAND_INCOME");
            this.comm_tou_landincome.relativePosition = new Vector3(this.comm_low_landincome.relativePosition.x, this.comm_low_landincome.relativePosition.y + SPACING22);
            this.comm_tou_landincome.autoSize = true;

            this.comm_lei_landincome = base.AddUIComponent<UILabel>();
            this.comm_lei_landincome.text = Localization.Get("COMMERICAL_LEISURE_LAND_INCOME");
            this.comm_lei_landincome.relativePosition = new Vector3(SPACING, this.comm_eco_landincome.relativePosition.y + SPACING22);
            this.comm_lei_landincome.autoSize = true;

            this.indu_gen_landincome = base.AddUIComponent<UILabel>();
            this.indu_gen_landincome.text = Localization.Get("INDUSTRIAL_GENERAL_LAND_INCOME");
            this.indu_gen_landincome.relativePosition = new Vector3(this.comm_tou_landincome.relativePosition.x, this.comm_tou_landincome.relativePosition.y + SPACING22);
            this.indu_gen_landincome.autoSize = true;

            this.indu_farmer_landincome = base.AddUIComponent<UILabel>();
            this.indu_farmer_landincome.text = Localization.Get("INDUSTRIAL_FARMING_LAND_INCOME");
            this.indu_farmer_landincome.relativePosition = new Vector3(SPACING, this.comm_lei_landincome.relativePosition.y + SPACING22);
            this.indu_farmer_landincome.autoSize = true;

            this.indu_foresty_landincome = base.AddUIComponent<UILabel>();
            this.indu_foresty_landincome.text = Localization.Get("INDUSTRIAL_FORESTY_LAND_INCOME");
            this.indu_foresty_landincome.relativePosition = new Vector3(this.indu_gen_landincome.relativePosition.x, this.indu_gen_landincome.relativePosition.y + SPACING22);
            this.indu_foresty_landincome.autoSize = true;

            this.indu_oil_landincome = base.AddUIComponent<UILabel>();
            this.indu_oil_landincome.text = Localization.Get("INDUSTRIAL_OIL_LAND_INCOME");
            this.indu_oil_landincome.relativePosition = new Vector3(SPACING, this.indu_farmer_landincome.relativePosition.y + SPACING22);
            this.indu_oil_landincome.autoSize = true;

            this.indu_ore_landincome = base.AddUIComponent<UILabel>();
            this.indu_ore_landincome.text = Localization.Get("INDUSTRIAL_ORE_LAND_INCOME");
            this.indu_ore_landincome.relativePosition = new Vector3(this.indu_foresty_landincome.relativePosition.x, this.indu_foresty_landincome.relativePosition.y + SPACING22);
            this.indu_ore_landincome.autoSize = true;

            this.office_gen_landincome = base.AddUIComponent<UILabel>();
            this.office_gen_landincome.text = Localization.Get("OFFICE_GENERAL_LAND_INCOME");
            this.office_gen_landincome.relativePosition = new Vector3(SPACING, this.indu_oil_landincome.relativePosition.y + SPACING22);
            this.office_gen_landincome.autoSize = true;

            this.office_high_tech_landincome = base.AddUIComponent<UILabel>();
            this.office_high_tech_landincome.text = Localization.Get("OFFICE_HIGH_TECH_LAND_INCOME");
            this.office_high_tech_landincome.relativePosition = new Vector3(this.indu_ore_landincome.relativePosition.x, this.indu_ore_landincome.relativePosition.y + SPACING22);
            this.office_high_tech_landincome.autoSize = true;

            //4、City trade tax income
            this.trade_income_title = base.AddUIComponent<UILabel>();
            this.trade_income_title.text = Localization.Get("CITY_TRADE_INCOME_TITLE");
            this.trade_income_title.textScale = 1.1f;
            this.trade_income_title.relativePosition = new Vector3(SPACING, this.office_gen_landincome.relativePosition.y + SPACING22 + 10f);
            this.trade_income_title.autoSize = true;

            this.comm_high_tradeincome = base.AddUIComponent<UILabel>();
            this.comm_high_tradeincome.text = Localization.Get("COMMERICAL_HIGH_TRADE_INCOME");
            this.comm_high_tradeincome.relativePosition = new Vector3(SPACING, this.trade_income_title.relativePosition.y + SPACING22);
            this.comm_high_tradeincome.autoSize = true;

            this.comm_low_tradeincome = base.AddUIComponent<UILabel>();
            this.comm_low_tradeincome.text = Localization.Get("COMMERICAL_LOW_TRADE_INCOME");
            this.comm_low_tradeincome.relativePosition = new Vector3(this.comm_high_tradeincome.relativePosition.x + this.comm_high_tradeincome.width + SPACING + 120f, this.comm_high_tradeincome.relativePosition.y);
            this.comm_low_tradeincome.autoSize = true;

            this.comm_eco_tradeincome = base.AddUIComponent<UILabel>();
            this.comm_eco_tradeincome.text = Localization.Get("COMMERICAL_ECO_TRADE_INCOME");
            this.comm_eco_tradeincome.relativePosition = new Vector3(SPACING, this.comm_high_tradeincome.relativePosition.y + SPACING22);
            this.comm_eco_tradeincome.autoSize = true;

            this.comm_tou_tradeincome = base.AddUIComponent<UILabel>();
            this.comm_tou_tradeincome.text = Localization.Get("COMMERICAL_TOURISM_TRADE_INCOME");
            this.comm_tou_tradeincome.relativePosition = new Vector3(this.comm_low_tradeincome.relativePosition.x, this.comm_low_tradeincome.relativePosition.y + SPACING22);
            this.comm_tou_tradeincome.autoSize = true;

            this.comm_lei_tradeincome = base.AddUIComponent<UILabel>();
            this.comm_lei_tradeincome.text = Localization.Get("COMMERICAL_LEISURE_TRADE_INCOME");
            this.comm_lei_tradeincome.relativePosition = new Vector3(SPACING, this.comm_eco_tradeincome.relativePosition.y + SPACING22);
            this.comm_lei_tradeincome.autoSize = true;

            this.indu_gen_tradeincome = base.AddUIComponent<UILabel>();
            this.indu_gen_tradeincome.text = Localization.Get("INDUSTRIAL_GENERAL_TRADE_INCOME");
            this.indu_gen_tradeincome.relativePosition = new Vector3(this.comm_tou_tradeincome.relativePosition.x, this.comm_tou_tradeincome.relativePosition.y + SPACING22);
            this.indu_gen_tradeincome.autoSize = true;

            this.indu_farmer_tradeincome = base.AddUIComponent<UILabel>();
            this.indu_farmer_tradeincome.text = Localization.Get("INDUSTRIAL_FARMING_TRADE_INCOME");
            this.indu_farmer_tradeincome.relativePosition = new Vector3(SPACING, this.comm_lei_tradeincome.relativePosition.y + SPACING22);
            this.indu_farmer_tradeincome.autoSize = true;

            this.indu_foresty_tradeincome = base.AddUIComponent<UILabel>();
            this.indu_foresty_tradeincome.text = Localization.Get("INDUSTRIAL_FORESTY_TRADE_INCOME");
            this.indu_foresty_tradeincome.relativePosition = new Vector3(this.indu_gen_tradeincome.relativePosition.x, this.indu_gen_tradeincome.relativePosition.y + SPACING22);
            this.indu_foresty_tradeincome.autoSize = true;

            this.indu_oil_tradeincome = base.AddUIComponent<UILabel>();
            this.indu_oil_tradeincome.text = Localization.Get("INDUSTRIAL_OIL_TRADE_INCOME");
            this.indu_oil_tradeincome.relativePosition = new Vector3(SPACING, this.indu_farmer_tradeincome.relativePosition.y + SPACING22);
            this.indu_oil_tradeincome.autoSize = true;

            this.indu_ore_tradeincome = base.AddUIComponent<UILabel>();
            this.indu_ore_tradeincome.text = Localization.Get("INDUSTRIAL_ORE_TRADE_INCOME");
            this.indu_ore_tradeincome.relativePosition = new Vector3(this.indu_foresty_tradeincome.relativePosition.x, this.indu_foresty_tradeincome.relativePosition.y + SPACING22);
            this.indu_ore_tradeincome.autoSize = true;

            //5、Public transport income
            this.public_transport_income_title = base.AddUIComponent<UILabel>();
            this.public_transport_income_title.text = Localization.Get("CITY_PUBLIC_TRANSPORT_INCOME_TITLE");
            this.public_transport_income_title.textScale = 1.1f;
            this.public_transport_income_title.relativePosition = new Vector3(SPACING, this.indu_oil_tradeincome.relativePosition.y + SPACING22 + 10f);
            this.public_transport_income_title.autoSize = true;

            this.from_bus = base.AddUIComponent<UILabel>();
            this.from_bus.text = Localization.Get("BUS");
            this.from_bus.relativePosition = new Vector3(SPACING, this.public_transport_income_title.relativePosition.y + SPACING22);
            this.from_bus.autoSize = true;

            this.from_tram = base.AddUIComponent<UILabel>();
            this.from_tram.text = Localization.Get("TRAM");
            this.from_tram.relativePosition = new Vector3(this.from_bus.relativePosition.x + this.from_bus.width + SPACING + 120f, this.from_bus.relativePosition.y);
            this.from_tram.autoSize = true;

            this.from_metro = base.AddUIComponent<UILabel>();
            this.from_metro.text = Localization.Get("TRAIN");
            this.from_metro.relativePosition = new Vector3(this.from_tram.relativePosition.x + this.from_tram.width + SPACING + 120f, this.from_tram.relativePosition.y);
            this.from_metro.autoSize = true;

            this.from_train = base.AddUIComponent<UILabel>();
            this.from_train.text = Localization.Get("SHIP");
            this.from_train.relativePosition = new Vector3(SPACING, this.from_bus.relativePosition.y + SPACING22);
            this.from_train.autoSize = true;

            this.from_ship = base.AddUIComponent<UILabel>();
            this.from_ship.text = Localization.Get("PLANE");
            this.from_ship.relativePosition = new Vector3(this.from_tram.relativePosition.x, this.from_tram.relativePosition.y + SPACING22);
            this.from_ship.autoSize = true;

            this.from_taxi = base.AddUIComponent<UILabel>();
            this.from_taxi.text = Localization.Get("METRO");
            this.from_taxi.relativePosition = new Vector3(this.from_metro.relativePosition.x, this.from_metro.relativePosition.y + SPACING22);
            this.from_taxi.autoSize = true;

            this.from_plane = base.AddUIComponent<UILabel>();
            this.from_plane.text = Localization.Get("TAXI");
            this.from_plane.relativePosition = new Vector3(SPACING, this.from_train.relativePosition.y + SPACING22);
            this.from_plane.autoSize = true;

            this.from_cable_car = base.AddUIComponent<UILabel>();
            this.from_cable_car.text = Localization.Get("CABLECAR");
            this.from_cable_car.relativePosition = new Vector3(this.from_ship.relativePosition.x, this.from_ship.relativePosition.y + SPACING22);
            this.from_cable_car.autoSize = true;

            this.from_monorail = base.AddUIComponent<UILabel>();
            this.from_monorail.text = Localization.Get("MONORAIL");
            this.from_monorail.relativePosition = new Vector3(this.from_taxi.relativePosition.x, this.from_taxi.relativePosition.y + SPACING22);
            this.from_monorail.autoSize = true;

            //6、Public transport income
            this.goverment_income_title = base.AddUIComponent<UILabel>();
            this.goverment_income_title.text = Localization.Get("CITY_PLAYER_BUILDING_INCOME_TITLE");
            this.goverment_income_title.textScale = 1.1f;
            this.goverment_income_title.relativePosition = new Vector3(SPACING, this.from_plane.relativePosition.y + SPACING22 + 10f);
            this.goverment_income_title.autoSize = true;

            this.garbage_income_title = base.AddUIComponent<UILabel>();
            this.garbage_income_title.text = Localization.Get("GARBAGE");
            this.garbage_income_title.relativePosition = new Vector3(SPACING, this.from_plane.relativePosition.y + 2 * SPACING22 + 10f);
            this.garbage_income_title.autoSize = true;

            this.school_income_title = base.AddUIComponent<UILabel>();
            this.school_income_title.text = Localization.Get("SCHOOL");
            this.school_income_title.relativePosition = new Vector3(this.from_cable_car.relativePosition.x, this.from_cable_car.relativePosition.y + 2 * SPACING22 + 10f);
            this.school_income_title.autoSize = true;

            this.road_income_title = base.AddUIComponent<UILabel>();
            this.road_income_title.text = Localization.Get("ROAD");
            this.road_income_title.relativePosition = new Vector3(this.from_monorail.relativePosition.x, this.from_monorail.relativePosition.y + 2 * SPACING22 + 10f);
            this.road_income_title.autoSize = true;

            this.playerIndustryIncomeTitle = base.AddUIComponent<UILabel>();
            this.playerIndustryIncomeTitle.text = Localization.Get("PLAYERINDUSTRY");
            this.playerIndustryIncomeTitle.relativePosition = new Vector3(this.garbage_income_title.relativePosition.x, this.garbage_income_title.relativePosition.y + SPACING22);
            this.playerIndustryIncomeTitle.autoSize = true;

            this.healthCareIncomeTitle = base.AddUIComponent<UILabel>();
            this.healthCareIncomeTitle.text = Localization.Get("HEALTHCARE");
            this.healthCareIncomeTitle.relativePosition = new Vector3(this.school_income_title.relativePosition.x, this.school_income_title.relativePosition.y + SPACING22);
            this.healthCareIncomeTitle.autoSize = true;

            this.fireStationIncomeTitle = base.AddUIComponent<UILabel>();
            this.fireStationIncomeTitle.text = Localization.Get("FIRESTATION");
            this.fireStationIncomeTitle.relativePosition = new Vector3(this.road_income_title.relativePosition.x, this.road_income_title.relativePosition.y + SPACING22);
            this.fireStationIncomeTitle.autoSize = true;

            this.policeStationIncomeTitle = base.AddUIComponent<UILabel>();
            this.policeStationIncomeTitle.text = Localization.Get("POLICESTATION");
            this.policeStationIncomeTitle.relativePosition = new Vector3(this.playerIndustryIncomeTitle.relativePosition.x, this.playerIndustryIncomeTitle.relativePosition.y + SPACING22);
            this.policeStationIncomeTitle.autoSize = true;

            this.all_total_income_ui = base.AddUIComponent<UILabel>();
            this.all_total_income_ui.text = Localization.Get("CITY_TOTAL_INCOME_TITLE");
            this.all_total_income_ui.textScale = 1.1f;
            this.all_total_income_ui.relativePosition = new Vector3(this.policeStationIncomeTitle.relativePosition.x, this.policeStationIncomeTitle.relativePosition.y + SPACING22 + 10f);
            this.all_total_income_ui.autoSize = true;
        }

        private void RefreshDisplayData()
        {
            uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
            uint num2 = currentFrameIndex & 255u;
            if (refeshOnce)
            {
                if (base.isVisible)
                {
                    process_data();
                    this.m_title.text = Localization.Get("CITY_INCOME_DATA");
                    this.citizen_tax_income_title.text = string.Format(Localization.Get("CITY_INCOME_DATA") + " [{0}]  [{1:N2}%]", citizen_tax_income_total, citizen_tax_income_percent * 100);
                    this.citizen_tax_income.text = string.Format(Localization.Get("SALARY_TAX_INCOME") + " [{0}]", citizen_tax_income_forui);
                    this.city_tourism_income_title.text = string.Format(Localization.Get("TOURIST_INCOME_TITLE") + " [{0}]  [{1:N2}%]", city_tourism_income_total, city_tourism_income_percent * 100);
                    this.citizen_income.text = string.Format(Localization.Get("FROM_RESIDENT") + " [{0}]", citizen_income_forui);
                    this.tourist_income.text = string.Format(Localization.Get("FROM_TOURIST") + " [{0}]", tourist_income_forui);
                    this.land_income_title.text = string.Format(Localization.Get("LAND_TAX_INCOME_TITLE") + " [{0}]  [{1:N2}%]", city_land_income_total, city_land_income_percent * 100);
                    this.resident_high_landincome.text = string.Format(Localization.Get("RESIDENT_HIGH_LAND_INCOME") + " [{0}]", resident_high_landincome_forui);
                    this.resident_low_landincome.text = string.Format(Localization.Get("RESIDENT_LOW_LAND_INCOME") + " [{0}]", resident_low_landincome_forui);
                    this.resident_high_eco_landincome.text = string.Format(Localization.Get("RESIDENT_HIGH_ECO_LAND_INCOME") + " [{0}]", resident_high_eco_landincome_forui);
                    this.resident_low_eco_landincome.text = string.Format(Localization.Get("RESIDENT_LOW_ECO_LAND_INCOME") + " [{0}]", resident_low_eco_landincome_forui);
                    this.comm_high_landincome.text = string.Format(Localization.Get("COMMERICAL_HIGH_LAND_INCOME") + " [{0}]", comm_high_landincome_forui);
                    this.comm_low_landincome.text = string.Format(Localization.Get("COMMERICAL_LEISURE_LAND_INCOME") + " [{0}]", comm_low_landincome_forui);
                    this.comm_lei_landincome.text = string.Format(Localization.Get("COMMERICAL_ECO_LAND_INCOME") + " [{0}]", comm_lei_landincome_forui);
                    this.comm_tou_landincome.text = string.Format(Localization.Get("COMMERICAL_TOURISM_LAND_INCOME") + " [{0}]", comm_tou_landincome_forui);
                    this.comm_eco_landincome.text = string.Format(Localization.Get("COMMERICAL_ECO_LAND_INCOME") + " [{0}]", comm_eco_landincome_forui);
                    this.indu_gen_landincome.text = string.Format(Localization.Get("INDUSTRIAL_GENERAL_LAND_INCOME") + " [{0}]", indu_gen_landincome_forui);
                    this.indu_farmer_landincome.text = string.Format(Localization.Get("INDUSTRIAL_FARMING_LAND_INCOME") + " [{0}]", indu_farmer_landincome_forui);
                    this.indu_foresty_landincome.text = string.Format(Localization.Get("INDUSTRIAL_FORESTY_LAND_INCOME") + " [{0}]", indu_foresty_landincome_forui);
                    this.indu_oil_landincome.text = string.Format(Localization.Get("INDUSTRIAL_OIL_LAND_INCOME") + " [{0}]", indu_oil_landincome_forui);
                    this.indu_ore_landincome.text = string.Format(Localization.Get("INDUSTRIAL_ORE_LAND_INCOME") + " [{0}]", indu_ore_landincome_forui);
                    this.office_gen_landincome.text = string.Format(Localization.Get("OFFICE_GENERAL_LAND_INCOME") + " [{0}]", office_gen_landincome_forui);
                    this.office_high_tech_landincome.text = string.Format(Localization.Get("OFFICE_HIGH_TECH_LAND_INCOME") + " [{0}]", office_high_tech_landincome_forui); ;
                    this.trade_income_title.text = string.Format(Localization.Get("CITY_TRADE_INCOME_TITLE") + " [{0}]  [{1:N2}%]", city_trade_income_total, city_trade_income_percent * 100);
                    this.comm_high_tradeincome.text = string.Format(Localization.Get("COMMERICAL_HIGH_TRADE_INCOME") + " [{0}]", comm_high_tradeincome_forui);
                    this.comm_low_tradeincome.text = string.Format(Localization.Get("COMMERICAL_LOW_TRADE_INCOME") + " [{0}]", comm_low_tradeincome_forui);
                    this.comm_lei_tradeincome.text = string.Format(Localization.Get("COMMERICAL_LEISURE_TRADE_INCOME") + " [{0}]", comm_lei_tradeincome_forui);
                    this.comm_tou_tradeincome.text = string.Format(Localization.Get("COMMERICAL_TOURISM_TRADE_INCOME") + " [{0}]", comm_tou_tradeincome_forui);
                    this.comm_eco_tradeincome.text = string.Format(Localization.Get("COMMERICAL_ECO_TRADE_INCOME") + " [{0}]", comm_eco_tradeincome_forui);
                    this.indu_gen_tradeincome.text = string.Format(Localization.Get("INDUSTRIAL_GENERAL_TRADE_INCOME") + " [{0}]", indu_gen_tradeincome_forui);
                    this.indu_farmer_tradeincome.text = string.Format(Localization.Get("INDUSTRIAL_FARMING_TRADE_INCOME") + " [{0}]", indu_farmer_tradeincome_forui);
                    this.indu_foresty_tradeincome.text = string.Format(Localization.Get("INDUSTRIAL_FORESTY_TRADE_INCOME") + " [{0}]", indu_foresty_tradeincome_forui);
                    this.indu_oil_tradeincome.text = string.Format(Localization.Get("INDUSTRIAL_OIL_TRADE_INCOME") + " [{0}]", indu_oil_tradeincome_forui);
                    this.indu_ore_tradeincome.text = string.Format(Localization.Get("INDUSTRIAL_ORE_TRADE_INCOME") + " [{0}]", indu_ore_tradeincome_forui);
                    this.public_transport_income_title.text = string.Format(Localization.Get("CITY_PUBLIC_TRANSPORT_INCOME_TITLE") + " [{0}]  [{1:N2}%]", city_transport_income_total, city_transport_income_percent * 100);
                    this.from_bus.text = string.Format(Localization.Get("BUS") + " [{0}]", bus_income);
                    this.from_tram.text = string.Format(Localization.Get("TRAM") + " [{0}]", tram_income);
                    this.from_train.text = string.Format(Localization.Get("TRAIN") + " [{0}]", train_income);
                    this.from_ship.text = string.Format(Localization.Get("SHIP") + " [{0}]", ship_income);
                    this.from_plane.text = string.Format(Localization.Get("PLANE") + " [{0}]", plane_income);
                    this.from_metro.text = string.Format(Localization.Get("METRO") + " [{0}]", metro_income);
                    this.from_taxi.text = string.Format(Localization.Get("TAXI") + " [{0}]", taxi_income);
                    this.from_cable_car.text = string.Format(Localization.Get("CABLECAR") + " [{0}]", cablecar_income);
                    this.from_monorail.text = string.Format(Localization.Get("MONORAIL") + " [{0}]", monorail_income);
                    this.goverment_income_title.text = string.Format(Localization.Get("CITY_PLAYER_BUILDING_INCOME_TITLE") + " [{0}]  [{1:N2}%]", city_playerbuilding_income_total, city_playerbuilding_income_percent * 100);
                    this.road_income_title.text = string.Format(Localization.Get("ROAD") + " [{0}]", road_income_forui);
                    this.garbage_income_title.text = string.Format(Localization.Get("GARBAGE") + " [{0}]", garbage_income_forui);
                    this.school_income_title.text = string.Format(Localization.Get("SCHOOL") + " [{0}]", school_income_forui);
                    this.playerIndustryIncomeTitle.text = string.Format(Localization.Get("PLAYERINDUSTRY") + " [{0}]", playerIndustryIncomeForUI);
                    this.healthCareIncomeTitle.text = string.Format(Localization.Get("HEALTHCARE") + " [{0}]", healthCareIncomeForUI);
                    this.fireStationIncomeTitle.text = string.Format(Localization.Get("FIRESTATION") + " [{0}]", fireStationIncomeForUI);
                    this.policeStationIncomeTitle.text = string.Format(Localization.Get("POLICESTATION") + " [{0}]", policeStationIncomeForUI);
                    this.all_total_income_ui.text = string.Format(Localization.Get("CITY_TOTAL_INCOME_TITLE") + " [{0}]", all_total_income);
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
