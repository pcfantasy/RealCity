using System.Collections.Generic;
using ColossalFramework;
using ColossalFramework.UI;
using UnityEngine;
using System.Collections;

namespace RealCity
{
    public class RealCityUI : UIPanel
    {
        public static readonly string cacheName = "RealCityUI";

        private static readonly float WIDTH = 800f;

        private static readonly float HEIGHT = 700f;

        private static readonly float HEADER = 40f;

        private static readonly float SPACING = 15f;

        private static readonly float SPACING22 = 22f;

        private ItemClass.Availability CurrentMode;

        public static RealCityUI instance;

        private Dictionary<string, UILabel> _valuesControlContainer = new Dictionary<string, UILabel>(16);

        private UIDragHandle m_DragHandler;

        private UIButton m_closeButton;

        private UILabel m_title;

        private UILabel m_HeaderDataText;

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
        private UILabel cemetery_income_title;

        //7、all total income
        private UILabel all_total_income_ui;


        //used for display
        //citizen tax income
        public static double citizen_tax_income_total;
        public static double citizen_tax_income_percent;
        public static double citizen_tax_income_forui;

        //tourist for both citizen and tourist
        public static double city_tourism_income_total;
        public static double city_tourism_income_percent;
        public static double citizen_income_forui;
        public static double tourist_income_forui;
        //land income
        public static double city_land_income_total;
        public static double city_land_income_percent;
        public static double resident_high_landincome_forui;
        public static double resident_low_landincome_forui;
        public static double resident_high_eco_landincome_forui;
        public static double resident_low_eco_landincome_forui;
        public static double comm_high_landincome_forui;
        public static double comm_low_landincome_forui;
        public static double comm_lei_landincome_forui;
        public static double comm_tou_landincome_forui;
        public static double comm_eco_landincome_forui;
        public static double indu_gen_landincome_forui;
        public static double indu_farmer_landincome_forui;
        public static double indu_foresty_landincome_forui;
        public static double indu_oil_landincome_forui;
        public static double indu_ore_landincome_forui;
        public static double office_gen_landincome_forui;
        public static double office_high_tech_landincome_forui;

        //trade income
        public static double city_trade_income_total;
        public static double city_trade_income_percent;
        public static double comm_high_tradeincome_forui;
        public static double comm_low_tradeincome_forui;
        public static double comm_lei_tradeincome_forui;
        public static double comm_tou_tradeincome_forui;
        public static double comm_eco_tradeincome_forui;
        public static double indu_gen_tradeincome_forui;
        public static double indu_farmer_tradeincome_forui;
        public static double indu_foresty_tradeincome_forui;
        public static double indu_oil_tradeincome_forui;
        public static double indu_ore_tradeincome_forui;

        public static double garbage_income_forui;
        public static double road_income_forui;
        public static double cemetery_income_forui;

        //transport income
        public static double city_playerbuilding_income_total;
        public static double city_playerbuilding_income_percent;
        public static double city_transport_income_total;
        public static double city_transport_income_percent;
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
        public static double all_total_income;

        private static bool isRefreshing = false;

        private bool CoDisplayRefreshEnabled;

        public override void Update()
        {
            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.R))
            {
                this.ProcessVisibility();
                DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "ctrl+R found");
            }
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
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 + 50), (float)(Loader.parentGuiView.fixedHeight / 2 - 350));
            base.opacity = 1f;
            base.cachedName = cacheName;
            this.CurrentMode = Singleton<ToolManager>.instance.m_properties.m_mode;
            this.m_DragHandler = base.AddUIComponent<UIDragHandle>();
            this.m_DragHandler.target = this;
            this.m_title = base.AddUIComponent<UILabel>();
            this.m_title.text = "City Data";
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
            //this.GetDataNeeded();
            this.ShowOnGui();
            //this.PopulateControlContainers();

            base.StartCoroutine(this.RefreshDisplayDataWrapper());
            this.RefreshDisplayData();
        }

        //private void GetDataNeeded()
        //{
            //this._tmpPopData = comm_data.last_pop;
            //this._tmpdeadcount = comm_data.last_bank_count;
        //}

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

            //1、citizen tax income
            this.citizen_tax_income_title = base.AddUIComponent<UILabel>();
            this.citizen_tax_income_title.text = "1、City resident salary-tax income [000000000000000]";
            this.citizen_tax_income_title.tooltip = "N/A";
            this.citizen_tax_income_title.relativePosition = new Vector3(SPACING, this.m_HeaderDataText.relativePosition.y + SPACING22 + 2f);
            this.citizen_tax_income_title.autoSize = true;
            this.citizen_tax_income_title.name = "Moreeconomic_Text_0";

            this.citizen_tax_income = base.AddUIComponent<UILabel>();
            this.citizen_tax_income.text =               string.Format("citizen salary-tax income [0000000]");
            this.citizen_tax_income.tooltip = "total citizen salary tax income";
            this.citizen_tax_income.relativePosition = new Vector3(SPACING, this.citizen_tax_income_title.relativePosition.y + SPACING22 + 2f);
            this.citizen_tax_income.autoSize = true;
            this.citizen_tax_income.name = "Moreeconomic_Text_1";

            //2、City tourism income
            this.city_tourism_income_title = base.AddUIComponent<UILabel>();
            this.city_tourism_income_title.text = string.Format("2、City tourism income [000000000000000]");
            this.city_tourism_income_title.tooltip = "N/A";
            this.city_tourism_income_title.relativePosition = new Vector3(SPACING, this.citizen_tax_income.relativePosition.y + SPACING22 + 2f);
            this.city_tourism_income_title.autoSize = true;
            this.city_tourism_income_title.name = "Moreeconomic_Text_2";

            this.citizen_income = base.AddUIComponent<UILabel>();
            this.citizen_income.text =    string.Format("from resident [00000]");
            this.citizen_income.tooltip = "money from resident tourist";
            this.citizen_income.relativePosition = new Vector3(SPACING, this.city_tourism_income_title.relativePosition.y + SPACING22 + 2f);
            this.citizen_income.autoSize = true;
            this.citizen_income.name = "Moreeconomic_Text_3";

            this.tourist_income = base.AddUIComponent<UILabel>();
            this.tourist_income.text =    string.Format("from tourist [000000]");
            this.tourist_income.tooltip = "total citizen_salary";
            this.tourist_income.relativePosition = new Vector3(this.citizen_income.relativePosition.x + this.citizen_income.width + SPACING, this.citizen_income.relativePosition.y);
            this.tourist_income.autoSize = true;
            this.tourist_income.name = "Moreeconomic_Text_4";

            //3、City land tax income
            this.land_income_title = base.AddUIComponent<UILabel>();
            this.land_income_title.text =     string.Format("3、City land tax income [0000000000000]");
            this.land_income_title.tooltip = "total city land income";
            this.land_income_title.relativePosition = new Vector3(SPACING, this.citizen_income.relativePosition.y + SPACING22 + 2f);
            this.land_income_title.autoSize = true;
            this.land_income_title.name = "Moreeconomic_Text_5";

            this.resident_high_landincome = base.AddUIComponent<UILabel>();
            this.resident_high_landincome.text =     string.Format("residential_high_landincome [0000000]");
            this.resident_high_landincome.tooltip = "residential_high_landincome";
            this.resident_high_landincome.relativePosition = new Vector3(SPACING, this.land_income_title.relativePosition.y + SPACING22 + 2f);
            this.resident_high_landincome.autoSize = true;
            this.resident_high_landincome.name = "Moreeconomic_Text_6";

            this.resident_low_landincome = base.AddUIComponent<UILabel>();
            this.resident_low_landincome.text =      string.Format("resident_low_landincome [000000000]");
            this.resident_low_landincome.tooltip = "resident_low_landincome";
            this.resident_low_landincome.relativePosition = new Vector3(this.resident_high_landincome.relativePosition.x + this.resident_high_landincome.width + SPACING, this.resident_high_landincome.relativePosition.y);
            this.resident_low_landincome.autoSize = true;
            this.resident_low_landincome.name = "Moreeconomic_Text_7";

            this.resident_high_eco_landincome = base.AddUIComponent<UILabel>();
            this.resident_high_eco_landincome.text = string.Format("resident_high_eco_landincome [00000]");
            this.resident_high_eco_landincome.tooltip = "total resident_high_eco_landincome";
            this.resident_high_eco_landincome.relativePosition = new Vector3(SPACING, this.resident_high_landincome.relativePosition.y + SPACING22);
            this.resident_high_eco_landincome.autoSize = true;
            this.resident_high_eco_landincome.name = "Moreeconomic_Text_8";

            this.resident_low_eco_landincome = base.AddUIComponent<UILabel>();
            this.resident_low_eco_landincome.text =  string.Format("resident_low_eco_landincome [0000000]");
            this.resident_low_eco_landincome.tooltip = "total resident_low_eco_landincome";
            this.resident_low_eco_landincome.relativePosition = new Vector3(this.resident_high_eco_landincome.relativePosition.x + this.resident_high_eco_landincome.width + SPACING, this.resident_high_eco_landincome.relativePosition.y);
            this.resident_low_eco_landincome.autoSize = true;
            this.resident_low_eco_landincome.name = "Moreeconomic_Text_8";

            this.comm_high_landincome = base.AddUIComponent<UILabel>();
            this.comm_high_landincome.text =         string.Format("commerical_high_landincome [00000000]");
            this.comm_high_landincome.tooltip = "total commerical_high_landincome";
            this.comm_high_landincome.relativePosition = new Vector3(SPACING, this.resident_high_eco_landincome.relativePosition.y + SPACING22);
            this.comm_high_landincome.autoSize = true;
            this.comm_high_landincome.name = "Moreeconomic_Text_9";

            this.comm_low_landincome = base.AddUIComponent<UILabel>();
            this.comm_low_landincome.text =          string.Format("commerical_low_landincome [000000000]");
            this.comm_low_landincome.tooltip = "commerical_low_landincome";
            this.comm_low_landincome.relativePosition = new Vector3(this.comm_high_landincome.relativePosition.x + this.comm_high_landincome.width + SPACING, this.comm_high_landincome.relativePosition.y);
            this.comm_low_landincome.autoSize = true;
            this.comm_low_landincome.name = "Moreeconomic_Text_10";

            this.comm_eco_landincome = base.AddUIComponent<UILabel>();
            this.comm_eco_landincome.text =          string.Format("commerical_eco_landincome [000000000]");
            this.comm_eco_landincome.tooltip = "commerical_eco_landincome";
            this.comm_eco_landincome.relativePosition = new Vector3(SPACING, this.comm_high_landincome.relativePosition.y + SPACING22);
            this.comm_eco_landincome.autoSize = true;
            this.comm_eco_landincome.name = "Moreeconomic_Text_11";

            this.comm_tou_landincome = base.AddUIComponent<UILabel>();
            this.comm_tou_landincome.text =       string.Format("commerical_tourism_landincome [00000000]");
            this.comm_tou_landincome.tooltip = "commerical_tourism_landincome";
            this.comm_tou_landincome.relativePosition = new Vector3(this.comm_eco_landincome.relativePosition.x + this.comm_eco_landincome.width + SPACING, this.comm_eco_landincome.relativePosition.y);
            this.comm_tou_landincome.autoSize = true;
            this.comm_tou_landincome.name = "Moreeconomic_Text_12";

            this.comm_lei_landincome = base.AddUIComponent<UILabel>();
            this.comm_lei_landincome.text =       string.Format("commerical_leisure_landincome [000000000]");
            this.comm_lei_landincome.tooltip = "commerical_leisure_landincome";
            this.comm_lei_landincome.relativePosition = new Vector3(SPACING, this.comm_eco_landincome.relativePosition.y + SPACING22);
            this.comm_lei_landincome.autoSize = true;
            this.comm_lei_landincome.name = "Moreeconomic_Text_13";

            this.indu_gen_landincome = base.AddUIComponent<UILabel>();
            this.indu_gen_landincome.text =       string.Format("industrial_general_landincome [000000000]");
            this.indu_gen_landincome.tooltip = "industrial_general_landincome";
            this.indu_gen_landincome.relativePosition = new Vector3(this.comm_lei_landincome.relativePosition.x + this.comm_lei_landincome.width + SPACING, this.comm_lei_landincome.relativePosition.y);
            this.indu_gen_landincome.autoSize = true;
            this.indu_gen_landincome.name = "Moreeconomic_Text_14";

            this.indu_farmer_landincome = base.AddUIComponent<UILabel>();
            this.indu_farmer_landincome.text =    string.Format("industrial_farming_landincome  [00000000]");
            this.indu_farmer_landincome.tooltip = "industrial_farming_landincome";
            this.indu_farmer_landincome.relativePosition = new Vector3(SPACING, this.comm_lei_landincome.relativePosition.y + SPACING22);
            this.indu_farmer_landincome.autoSize = true;
            this.indu_farmer_landincome.name = "Moreeconomic_Text_15";

            this.indu_foresty_landincome = base.AddUIComponent<UILabel>();
            this.indu_foresty_landincome.text =   string.Format("industrial_foresty_landincome [000000000]");
            this.indu_foresty_landincome.tooltip = "industrial_foresty_landincome";
            this.indu_foresty_landincome.relativePosition = new Vector3(this.indu_farmer_landincome.relativePosition.x + this.indu_farmer_landincome.width + SPACING, this.indu_farmer_landincome.relativePosition.y);
            this.indu_foresty_landincome.autoSize = true;
            this.indu_foresty_landincome.name = "Moreeconomic_Text_16";

            this.indu_oil_landincome = base.AddUIComponent<UILabel>();
            this.indu_oil_landincome.text =       string.Format("industrial_oil_landincome [0000000000000]");
            this.indu_oil_landincome.tooltip = "industrial_oil_landincome";
            this.indu_oil_landincome.relativePosition = new Vector3(SPACING, this.indu_farmer_landincome.relativePosition.y + SPACING22);
            this.indu_oil_landincome.autoSize = true;
            this.indu_oil_landincome.name = "Moreeconomic_Text_17";

            this.indu_ore_landincome = base.AddUIComponent<UILabel>();
            this.indu_ore_landincome.text =       string.Format("industrial_ore_landincome [0000000000000]");
            this.indu_ore_landincome.tooltip = "industrial_ore_landincome";
            this.indu_ore_landincome.relativePosition = new Vector3(this.indu_oil_landincome.relativePosition.x + this.indu_oil_landincome.width + SPACING, this.indu_oil_landincome.relativePosition.y);
            this.indu_ore_landincome.autoSize = true;
            this.indu_ore_landincome.name = "Moreeconomic_Text_18";
            
            this.office_gen_landincome = base.AddUIComponent<UILabel>();
            this.office_gen_landincome.text =       string.Format("office_general_landincome [0000000000000]");
            this.office_gen_landincome.tooltip = "office_general_landincome";
            this.office_gen_landincome.relativePosition = new Vector3(SPACING, this.indu_oil_landincome.relativePosition.y + SPACING22);
            this.office_gen_landincome.autoSize = true;
            this.office_gen_landincome.name = "Moreeconomic_Text_19";

            this.office_high_tech_landincome = base.AddUIComponent<UILabel>();
            this.office_high_tech_landincome.text = string.Format("office_high_tech_landincome [00000000000]");
            this.office_high_tech_landincome.tooltip = "office_high_tech_landincome";
            this.office_high_tech_landincome.relativePosition = new Vector3(this.office_gen_landincome.relativePosition.x + this.office_gen_landincome.width + SPACING, this.office_gen_landincome.relativePosition.y);
            this.office_high_tech_landincome.autoSize = true;
            this.office_high_tech_landincome.name = "Moreeconomic_Text_20";

            //4、City trade tax income
            this.trade_income_title = base.AddUIComponent<UILabel>();
            this.trade_income_title.text = string.Format("4、City trade tax income [00000000000000]");
            this.trade_income_title.tooltip = "total city trade tax income";
            this.trade_income_title.relativePosition = new Vector3(SPACING, this.office_gen_landincome.relativePosition.y + SPACING22 + 2f);
            this.trade_income_title.autoSize = true;
            this.trade_income_title.name = "Moreeconomic_Text_21";

            this.comm_high_tradeincome = base.AddUIComponent<UILabel>();
            this.comm_high_tradeincome.text = string.Format("commerical_high_tradeincome [00000000]");
            this.comm_high_tradeincome.tooltip = "total commerical_high_tradeincome";
            this.comm_high_tradeincome.relativePosition = new Vector3(SPACING, this.trade_income_title.relativePosition.y + SPACING22 + 2f);
            this.comm_high_tradeincome.autoSize = true;
            this.comm_high_tradeincome.name = "Moreeconomic_Text_22";

            this.comm_low_tradeincome = base.AddUIComponent<UILabel>();
            this.comm_low_tradeincome.text = string.Format("commerical_low_tradeincome [000000000]");
            this.comm_low_tradeincome.tooltip = "commerical_low_tradeincome";
            this.comm_low_tradeincome.relativePosition = new Vector3(this.comm_high_tradeincome.relativePosition.x + this.comm_high_tradeincome.width + SPACING, this.comm_high_tradeincome.relativePosition.y);
            this.comm_low_tradeincome.autoSize = true;
            this.comm_low_tradeincome.name = "Moreeconomic_Text_23";

            this.comm_eco_tradeincome = base.AddUIComponent<UILabel>();
            this.comm_eco_tradeincome.text = string.Format("commerical_eco_tradeincome [000000000]");
            this.comm_eco_tradeincome.tooltip = "commerical_eco_tradeincome";
            this.comm_eco_tradeincome.relativePosition = new Vector3(SPACING, this.comm_high_tradeincome.relativePosition.y + SPACING22);
            this.comm_eco_tradeincome.autoSize = true;
            this.comm_eco_tradeincome.name = "Moreeconomic_Text_24";

            this.comm_tou_tradeincome = base.AddUIComponent<UILabel>();
            this.comm_tou_tradeincome.text = string.Format("commerical_tourism_tradeincome [00000000]");
            this.comm_tou_tradeincome.tooltip = "commerical_tourism_tradeincome";
            this.comm_tou_tradeincome.relativePosition = new Vector3(this.comm_eco_tradeincome.relativePosition.x + this.comm_eco_tradeincome.width + SPACING, this.comm_eco_tradeincome.relativePosition.y);
            this.comm_tou_tradeincome.autoSize = true;
            this.comm_tou_tradeincome.name = "Moreeconomic_Text_25";

            this.comm_lei_tradeincome = base.AddUIComponent<UILabel>();
            this.comm_lei_tradeincome.text = string.Format("commerical_leisure_tradeincome [000000000]");
            this.comm_lei_tradeincome.tooltip = "commerical_leisure_tradeincome";
            this.comm_lei_tradeincome.relativePosition = new Vector3(SPACING, this.comm_eco_tradeincome.relativePosition.y + SPACING22);
            this.comm_lei_tradeincome.autoSize = true;
            this.comm_lei_tradeincome.name = "Moreeconomic_Text_26";

            this.indu_gen_tradeincome = base.AddUIComponent<UILabel>();
            this.indu_gen_tradeincome.text = string.Format("industrial_general_tradeincome [000000000]");
            this.indu_gen_tradeincome.tooltip = "industrial_general_tradeincome";
            this.indu_gen_tradeincome.relativePosition = new Vector3(this.comm_lei_tradeincome.relativePosition.x + this.comm_lei_tradeincome.width + SPACING, this.comm_lei_tradeincome.relativePosition.y);
            this.indu_gen_tradeincome.autoSize = true;
            this.indu_gen_tradeincome.name = "Moreeconomic_Text_27";

            this.indu_farmer_tradeincome = base.AddUIComponent<UILabel>();
            this.indu_farmer_tradeincome.text = string.Format("industrial_farming_tradeincome  [00000000]");
            this.indu_farmer_tradeincome.tooltip = "industrial_farming_tradeincome";
            this.indu_farmer_tradeincome.relativePosition = new Vector3(SPACING, this.comm_lei_tradeincome.relativePosition.y + SPACING22);
            this.indu_farmer_tradeincome.autoSize = true;
            this.indu_farmer_tradeincome.name = "Moreeconomic_Text_28";

            this.indu_foresty_tradeincome = base.AddUIComponent<UILabel>();
            this.indu_foresty_tradeincome.text = string.Format("industrial_foresty_tradeincome [000000000]");
            this.indu_foresty_tradeincome.tooltip = "industrial_foresty_tradeincome";
            this.indu_foresty_tradeincome.relativePosition = new Vector3(this.indu_farmer_tradeincome.relativePosition.x + this.indu_farmer_tradeincome.width + SPACING, this.indu_farmer_tradeincome.relativePosition.y);
            this.indu_foresty_tradeincome.autoSize = true;
            this.indu_foresty_tradeincome.name = "Moreeconomic_Text_29";

            this.indu_oil_tradeincome = base.AddUIComponent<UILabel>();
            this.indu_oil_tradeincome.text = string.Format("industrial_oil_tradeincome [0000000000000]");
            this.indu_oil_tradeincome.tooltip = "industrial_oil_tradeincome";
            this.indu_oil_tradeincome.relativePosition = new Vector3(SPACING, this.indu_farmer_tradeincome.relativePosition.y + SPACING22);
            this.indu_oil_tradeincome.autoSize = true;
            this.indu_oil_tradeincome.name = "Moreeconomic_Text_30";

            this.indu_ore_tradeincome = base.AddUIComponent<UILabel>();
            this.indu_ore_tradeincome.text = string.Format("industrial_ore_tradeincome [0000000000000]");
            this.indu_ore_tradeincome.tooltip = "industrial_ore_tradeincome";
            this.indu_ore_tradeincome.relativePosition = new Vector3(this.indu_oil_tradeincome.relativePosition.x + this.indu_oil_tradeincome.width + SPACING, this.indu_oil_tradeincome.relativePosition.y);
            this.indu_ore_tradeincome.autoSize = true;
            this.indu_ore_tradeincome.name = "Moreeconomic_Text_31";

            //5、Public transport income
            this.public_transport_income_title = base.AddUIComponent<UILabel>();
            this.public_transport_income_title.text = "5、City Public transport income [0000000000000]";
            this.public_transport_income_title.tooltip = "N/A";
            this.public_transport_income_title.relativePosition = new Vector3(SPACING, this.indu_oil_tradeincome.relativePosition.y + SPACING22 + 2f);
            this.public_transport_income_title.autoSize = true;
            this.public_transport_income_title.name = "Moreeconomic_Text_32";

            this.from_bus = base.AddUIComponent<UILabel>();
            this.from_bus.text =            string.Format("Bus [000000000]");
            this.from_bus.tooltip = "Bus";
            this.from_bus.relativePosition = new Vector3(SPACING, this.public_transport_income_title.relativePosition.y + SPACING22 + 2f);
            this.from_bus.autoSize = true;
            this.from_bus.name = "Moreeconomic_Text_33";

            this.from_tram = base.AddUIComponent<UILabel>();
            this.from_tram.text =           string.Format("Tram [00000000]");
            this.from_tram.tooltip = "Tram";
            this.from_tram.relativePosition = new Vector3(this.from_bus.relativePosition.x + this.from_bus.width + SPACING, this.from_bus.relativePosition.y);
            this.from_tram.autoSize = true;
            this.from_tram.name = "Moreeconomic_Text_33";

            this.from_metro = base.AddUIComponent<UILabel>();
            this.from_metro.text =          string.Format("Metro [0000000]");
            this.from_metro.tooltip = "Metro";
            this.from_metro.relativePosition = new Vector3(this.from_tram.relativePosition.x + this.from_tram.width + SPACING, this.from_tram.relativePosition.y);
            this.from_metro.autoSize = true;
            this.from_metro.name = "Moreeconomic_Text_34";

            this.from_train = base.AddUIComponent<UILabel>();
            this.from_train.text =          string.Format("Train [0000000]");
            this.from_train.tooltip = "Train";
            this.from_train.relativePosition = new Vector3(SPACING, this.from_bus.relativePosition.y + SPACING22);
            this.from_train.autoSize = true;
            this.from_train.name = "Moreeconomic_Text_35";

            this.from_ship = base.AddUIComponent<UILabel>();
            this.from_ship.text =           string.Format("Ship [00000000]");
            this.from_ship.tooltip = "Ship";
            this.from_ship.relativePosition = new Vector3(this.from_train.relativePosition.x + this.from_train.width + SPACING, this.from_train.relativePosition.y);
            this.from_ship.autoSize = true;
            this.from_ship.name = "Moreeconomic_Text_36";

            this.from_taxi = base.AddUIComponent<UILabel>();
            this.from_taxi.text =           string.Format("Taxi [00000000]");
            this.from_taxi.tooltip = "Taxi";
            this.from_taxi.relativePosition = new Vector3(this.from_ship.relativePosition.x + this.from_ship.width + SPACING, this.from_ship.relativePosition.y);
            this.from_taxi.autoSize = true;
            this.from_taxi.name = "Moreeconomic_Text_37";

            this.from_plane = base.AddUIComponent<UILabel>();
            this.from_plane.text =    string.Format("Plane [0000000]");
            this.from_plane.tooltip = "Plane";
            this.from_plane.relativePosition = new Vector3(SPACING, this.from_train.relativePosition.y + SPACING22);
            this.from_plane.autoSize = true;
            this.from_plane.name = "Moreeconomic_Text_38";

            this.from_cable_car = base.AddUIComponent<UILabel>();
            this.from_cable_car.text = string.Format("Cablecar [0000]");
            this.from_cable_car.tooltip = "Cablecar";
            this.from_cable_car.relativePosition = new Vector3(this.from_plane.relativePosition.x + this.from_plane.width + SPACING, this.from_plane.relativePosition.y);
            this.from_cable_car.autoSize = true;
            this.from_cable_car.name = "Moreeconomic_Text_39";

            this.from_monorail = base.AddUIComponent<UILabel>();
            this.from_monorail.text =  string.Format("Monorail [0000]");
            this.from_monorail.tooltip = "Monorail";
            this.from_monorail.relativePosition = new Vector3(this.from_cable_car.relativePosition.x + this.from_cable_car.width + SPACING, this.from_cable_car.relativePosition.y);
            this.from_monorail.autoSize = true;
            this.from_monorail.name = "Moreeconomic_Text_40";

            //6、Public transport income
            this.goverment_income_title = base.AddUIComponent<UILabel>();
            this.goverment_income_title.text = "6、City Player Building income [0000000000000]";
            this.goverment_income_title.tooltip = "N/A";
            this.goverment_income_title.relativePosition = new Vector3(SPACING, this.from_plane.relativePosition.y + SPACING22);
            this.goverment_income_title.autoSize = true;
            this.goverment_income_title.name = "Moreeconomic_Text_41";

            this.road_income_title = base.AddUIComponent<UILabel>();
            this.road_income_title.text = string.Format("Road [0000000]");
            this.road_income_title.tooltip = "Road";
            this.road_income_title.relativePosition = new Vector3(SPACING, this.goverment_income_title.relativePosition.y + SPACING22);
            this.road_income_title.autoSize = true;
            this.road_income_title.name = "Moreeconomic_Text_42";

            this.cemetery_income_title = base.AddUIComponent<UILabel>();
            this.cemetery_income_title.text = string.Format("Cemetery [0000]");
            this.cemetery_income_title.tooltip = "Cemetery";
            this.cemetery_income_title.relativePosition = new Vector3(this.road_income_title.relativePosition.x + this.road_income_title.width + SPACING, this.road_income_title.relativePosition.y);
            this.cemetery_income_title.autoSize = true;
            this.cemetery_income_title.name = "Moreeconomic_Text_43";

            this.garbage_income_title = base.AddUIComponent<UILabel>();
            this.garbage_income_title.text = string.Format("Garbage [0000]");
            this.garbage_income_title.tooltip = "Garbage";
            this.garbage_income_title.relativePosition = new Vector3(this.cemetery_income_title.relativePosition.x + this.cemetery_income_title.width + SPACING, this.cemetery_income_title.relativePosition.y);
            this.garbage_income_title.autoSize = true;
            this.garbage_income_title.name = "Moreeconomic_Text_44";

            //6 all total
            this.all_total_income_ui = base.AddUIComponent<UILabel>();
            this.all_total_income_ui.text = "6、City all total income [0000000000000]";
            this.all_total_income_ui.tooltip = "N/A";
            this.all_total_income_ui.relativePosition = new Vector3(SPACING, this.road_income_title.relativePosition.y + SPACING22 + 20f);
            this.all_total_income_ui.autoSize = true;
            this.all_total_income_ui.name = "Moreeconomic_Text_45";


            //this.m_getfromBank = base.AddUIComponent<UIButton>();
            //this.m_getfromBank.size = new Vector2(160f, 24f);
            //this.m_getfromBank.text = "Get 100K from Bank";
            //this.m_getfromBank.tooltip = "Get 100K from Bank, if Bank do not have enough money, this is useless";
            //this.m_getfromBank.textScale = 0.875f;
            //this.m_getfromBank.normalBgSprite = "ButtonMenu";
            //this.m_getfromBank.hoveredBgSprite = "ButtonMenuHovered";
            //this.m_getfromBank.pressedBgSprite = "ButtonMenuPressed";
            //this.m_getfromBank.disabledBgSprite = "ButtonMenuDisabled";
            //this.m_getfromBank.relativePosition = new Vector3(SPACING, this.m_deadcount.relativePosition.y + SPACING22);
            //this.m_getfromBank.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            //{
            //    //Caculate.GetData(true, false);
            //};

            //this.m_add2Bank = base.AddUIComponent<UIButton>();
            //this.m_add2Bank.size = new Vector2(160f, 24f);
            //this.m_add2Bank.text = "Add 100K to Bank";
            //this.m_add2Bank.tooltip = "Add 100K to Bank, if You do not have enough money, dont do this!!";
            //this.m_add2Bank.textScale = 0.875f;
            //this.m_add2Bank.normalBgSprite = "ButtonMenu";
            //this.m_add2Bank.hoveredBgSprite = "ButtonMenuHovered";
            //this.m_add2Bank.pressedBgSprite = "ButtonMenuPressed";
            //this.m_add2Bank.disabledBgSprite = "ButtonMenuDisabled";
            //this.m_add2Bank.relativePosition = this.m_getfromBank.relativePosition + new Vector3(this.m_getfromBank.width + SPACING * 2f, 0);
            //this.m_add2Bank.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            //{
            //    //Caculate.GetData(false, true);
            //};
        }


        private IEnumerator RefreshDisplayDataWrapper()
        {
            if (this.CoDisplayRefreshEnabled)
            {
                //reture some debug
            }
            else
            {
                while (!isRefreshing && base.isVisible)
                {
                    this.CoDisplayRefreshEnabled = true;
                    //this.GetDataNeeded();
                    this.RefreshDisplayData();
                    yield return new WaitForSeconds(3f);
                }
                this.CoDisplayRefreshEnabled = false;

            }
            yield break;
        }

        private void RefreshDisplayData()
        {
            //EconomyPanel instance = Singleton<EconomyPanel>.instance;
            isRefreshing = true;
            process_data();
            //citizen
            //this.citizen_count.text = string.Format("citizen_count [{0}]", comm_data.citizen_count);
            this.citizen_tax_income_title.text = string.Format("1、City resident salary-tax income [{0}]  [{1:N2}%]", citizen_tax_income_total, citizen_tax_income_percent*100);
            this.citizen_tax_income.text = string.Format("citizen salary-tax income [{0}]", citizen_tax_income_forui);
            this.city_tourism_income_title.text = string.Format("2、City tourism income [{0}]  [{1:N2}%]", city_tourism_income_total, city_tourism_income_percent*100) ;
            this.citizen_income.text = string.Format("from resident [{0}]", citizen_income_forui);
            this.tourist_income.text = string.Format("from tourist [{0}]", tourist_income_forui);
            this.land_income_title.text = string.Format("3、City land tax income [{0}]  [{1:N2}%]", city_land_income_total, city_land_income_percent*100);
            this.resident_high_landincome.text = string.Format("residential_high_landincome [{0}]", resident_high_landincome_forui);
            this.resident_low_landincome.text = string.Format("residential_low_landincome [{0}]", resident_low_landincome_forui);
            this.resident_high_eco_landincome.text = string.Format("residential_high_eco_landincome [{0}]", resident_high_eco_landincome_forui);
            this.resident_low_eco_landincome.text = string.Format("residential_low_eco_landincome [{0}]", resident_low_eco_landincome_forui);
            this.comm_high_landincome.text = string.Format("commerical_high_landincome [{0}]", comm_high_landincome_forui);
            this.comm_low_landincome.text = string.Format("commerical_low_landincome [{0}]", comm_low_landincome_forui);
            this.comm_lei_landincome.text = string.Format("commerical_leisure_landincome [{0}]", comm_lei_landincome_forui);
            this.comm_tou_landincome.text = string.Format("commerical_tourist_landincome [{0}]", comm_tou_landincome_forui);
            this.comm_eco_landincome.text = string.Format("commerical_eco_landincome [{0}]", comm_eco_landincome_forui);
            this.indu_gen_landincome.text = string.Format("industrial_general_landincome [{0}]", indu_gen_landincome_forui);
            this.indu_farmer_landincome.text = string.Format("industrial_farmeing_landincome [{0}]", indu_farmer_landincome_forui);
            this.indu_foresty_landincome.text = string.Format("industrial_forestry_landincome [{0}]", indu_foresty_landincome_forui);
            this.indu_oil_landincome.text = string.Format("industrial_oil_landincome [{0}]", indu_oil_landincome_forui);
            this.indu_ore_landincome.text = string.Format("industrial_ore_landincome [{0}]", indu_ore_landincome_forui);
            this.office_gen_landincome.text = string.Format("office_general_landincome [{0}]", office_gen_landincome_forui);
            this.office_high_tech_landincome.text = string.Format("office_high_tech_landincome [{0}]", office_high_tech_landincome_forui); ;
            this.trade_income_title.text = string.Format("4、City trade tax income [{0}]  [{1:N2}%]", city_trade_income_total, city_trade_income_percent*100);
            this.comm_high_tradeincome.text = string.Format("commerical_high_tradeincome [{0}]", comm_high_tradeincome_forui);
            this.comm_low_tradeincome.text = string.Format("commerical_low_tradeincome [{0}]", comm_low_tradeincome_forui);
            this.comm_lei_tradeincome.text = string.Format("commerical_leisure_tradeincome [{0}]", comm_lei_tradeincome_forui);
            this.comm_tou_tradeincome.text = string.Format("commerical_tourist_tradeincome [{0}]", comm_tou_tradeincome_forui);
            this.comm_eco_tradeincome.text = string.Format("commerical_eco_tradeincome [{0}]", comm_eco_tradeincome_forui);
            this.indu_gen_tradeincome.text = string.Format("industrial_general_tradeincome [{0}]", indu_gen_tradeincome_forui);
            this.indu_farmer_tradeincome.text = string.Format("industrial_farmeing_tradeincome [{0}]", indu_farmer_tradeincome_forui);
            this.indu_foresty_tradeincome.text = string.Format("industrial_forestry_tradeincome [{0}]", indu_foresty_tradeincome_forui);
            this.indu_oil_tradeincome.text = string.Format("industrial_oil_tradeincome [{0}]", indu_oil_tradeincome_forui);
            this.indu_ore_tradeincome.text = string.Format("industrial_ore_tradeincome [{0}]", indu_ore_tradeincome_forui);
            this.public_transport_income_title.text = string.Format("5、City Public transport income [{0}]  [{1:N2}%]", city_transport_income_total, city_transport_income_percent*100);
            this.from_bus.text = string.Format("Bus [{0}]", bus_income);
            this.from_tram.text = string.Format("Tram [{0}]", tram_income);
            this.from_train.text = string.Format("Train [{0}]", train_income);
            this.from_ship.text = string.Format("Ship [{0}]", ship_income);
            this.from_plane.text = string.Format("Plane [{0}]", plane_income);
            this.from_metro.text = string.Format("Metro [{0}]", metro_income);
            this.from_taxi.text = string.Format("Taxi [{0}]", taxi_income);
            this.from_cable_car.text = string.Format("Cablecar [{0}]", cablecar_income);
            this.from_monorail.text = string.Format("Monorail [{0}]", monorail_income);
            this.goverment_income_title.text = string.Format("6、City Player Building income [{0}]  [{1:N2}%]", city_playerbuilding_income_total, city_playerbuilding_income_percent * 100);
            this.road_income_title.text = string.Format("Road [{0}]", road_income_forui);
            this.cemetery_income_title.text = string.Format("Cemetety [{0}]", cemetery_income_forui);
            this.garbage_income_title.text = string.Format("Garbage [{0}]", garbage_income_forui);
            this.all_total_income_ui.text = string.Format("7、City all total income [{0}]", all_total_income);



            isRefreshing = false;
        }

        private void ProcessVisibility()
        {
            if (!base.isVisible)
            {
                base.Show();
                if (!this.CoDisplayRefreshEnabled)
                {
                    base.StartCoroutine(this.RefreshDisplayDataWrapper());
                    return;
                }
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
            cemetery_income_forui = 0f;
            garbage_income_forui = 0f;


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
                citizen_tax_income_forui += (double)pc_EconomyManager.citizen_tax_income_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                citizen_income_forui+= (double)pc_EconomyManager.citizen_income_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                tourist_income_forui+= (double)pc_EconomyManager.tourist_income_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                resident_high_landincome_forui+= (double)pc_EconomyManager.resident_high_landincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                resident_low_landincome_forui+= (double)pc_EconomyManager.resident_low_landincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                resident_high_eco_landincome_forui+= (double)pc_EconomyManager.resident_high_eco_landincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                resident_low_eco_landincome_forui+= (double)pc_EconomyManager.resident_low_eco_landincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                comm_high_landincome_forui+= (double)pc_EconomyManager.comm_high_landincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                comm_low_landincome_forui+= (double)pc_EconomyManager.comm_low_landincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                comm_lei_landincome_forui+= (double)pc_EconomyManager.comm_lei_landincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                comm_tou_landincome_forui+= (double)pc_EconomyManager.comm_tou_landincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                comm_eco_landincome_forui+= (double)pc_EconomyManager.comm_eco_landincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                indu_gen_landincome_forui+= (double)pc_EconomyManager.indu_gen_landincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                indu_farmer_landincome_forui+= (double)pc_EconomyManager.indu_farmer_landincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                indu_foresty_landincome_forui+= (double)pc_EconomyManager.indu_foresty_landincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                indu_oil_landincome_forui+= (double)pc_EconomyManager.indu_oil_landincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                indu_ore_landincome_forui+= (double)pc_EconomyManager.indu_ore_landincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                office_gen_landincome_forui+= (double)pc_EconomyManager.office_gen_landincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                office_high_tech_landincome_forui+= (double)pc_EconomyManager.office_high_tech_landincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                comm_high_tradeincome_forui+= (double)pc_EconomyManager.comm_high_tradeincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                comm_low_tradeincome_forui+= (double)pc_EconomyManager.comm_low_tradeincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                comm_lei_tradeincome_forui+= (double)pc_EconomyManager.comm_lei_tradeincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                comm_tou_tradeincome_forui+= (double)pc_EconomyManager.comm_tou_tradeincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                comm_eco_tradeincome_forui+= (double)pc_EconomyManager.comm_eco_tradeincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                indu_gen_tradeincome_forui+= (double)pc_EconomyManager.indu_gen_tradeincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                indu_farmer_tradeincome_forui+= (double)pc_EconomyManager.indu_farmer_tradeincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                indu_foresty_tradeincome_forui+= (double)pc_EconomyManager.indu_foresty_tradeincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                indu_oil_tradeincome_forui+= (double)pc_EconomyManager.indu_oil_tradeincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                indu_ore_tradeincome_forui+= (double)pc_EconomyManager.indu_ore_tradeincome_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;

                road_income_forui += (double)pc_EconomyManager.road_income_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                cemetery_income_forui += (double)pc_EconomyManager.cemetery_income_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                garbage_income_forui += (double)pc_EconomyManager.garbage_income_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
            }
            //DebugLog.LogToFileOnly(pc_EconomyManager.resident_low_landincome_forui[0].ToString());
            //DebugLog.LogToFileOnly(pc_EconomyManager.resident_low_landincome_forui[1].ToString());
            //DebugLog.LogToFileOnly(pc_EconomyManager.resident_low_landincome_forui[2].ToString());
            //DebugLog.LogToFileOnly(pc_EconomyManager.resident_low_landincome_forui[3].ToString());
            //DebugLog.LogToFileOnly(pc_EconomyManager.resident_low_landincome_forui[4].ToString());
            //DebugLog.LogToFileOnly(pc_EconomyManager.resident_low_landincome_forui[5].ToString());
            //DebugLog.LogToFileOnly(pc_EconomyManager.resident_low_landincome_forui[6].ToString());
            //DebugLog.LogToFileOnly(pc_EconomyManager.resident_low_landincome_forui[7].ToString());
            //DebugLog.LogToFileOnly(pc_EconomyManager.resident_low_landincome_forui[8].ToString());
            //DebugLog.LogToFileOnly(pc_EconomyManager.resident_low_landincome_forui[9].ToString());
            //DebugLog.LogToFileOnly(pc_EconomyManager.resident_low_landincome_forui[10].ToString());
            //DebugLog.LogToFileOnly(pc_EconomyManager.resident_low_landincome_forui[11].ToString());
            //DebugLog.LogToFileOnly(pc_EconomyManager.resident_low_landincome_forui[12].ToString());
            //DebugLog.LogToFileOnly(pc_EconomyManager.resident_low_landincome_forui[13].ToString());
            //DebugLog.LogToFileOnly(pc_EconomyManager.resident_low_landincome_forui[14].ToString());
            //DebugLog.LogToFileOnly(pc_EconomyManager.resident_low_landincome_forui[15].ToString());
            //DebugLog.LogToFileOnly(pc_EconomyManager.resident_low_landincome_forui[16].ToString());
            citizen_tax_income_forui -= (double)pc_EconomyManager.citizen_tax_income_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            citizen_income_forui -= (double)pc_EconomyManager.citizen_income_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            tourist_income_forui -= (double)pc_EconomyManager.tourist_income_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            resident_high_landincome_forui -= (double)pc_EconomyManager.resident_high_landincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            resident_low_landincome_forui -= (double)pc_EconomyManager.resident_low_landincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            resident_high_eco_landincome_forui -= (double)pc_EconomyManager.resident_high_eco_landincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            resident_low_eco_landincome_forui -= (double)pc_EconomyManager.resident_low_eco_landincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            comm_high_landincome_forui -= (double)pc_EconomyManager.comm_high_landincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            comm_low_landincome_forui -= (double)pc_EconomyManager.comm_low_landincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            comm_lei_landincome_forui -= (double)pc_EconomyManager.comm_lei_landincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            comm_tou_landincome_forui -= (double)pc_EconomyManager.comm_tou_landincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            comm_eco_landincome_forui -= (double)pc_EconomyManager.comm_eco_landincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            indu_gen_landincome_forui -= (double)pc_EconomyManager.indu_gen_landincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            indu_farmer_landincome_forui -= (double)pc_EconomyManager.indu_farmer_landincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            indu_foresty_landincome_forui -= (double)pc_EconomyManager.indu_foresty_landincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            indu_oil_landincome_forui -= (double)pc_EconomyManager.indu_oil_landincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            indu_ore_landincome_forui -= (double)pc_EconomyManager.indu_ore_landincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            office_gen_landincome_forui -= (double)pc_EconomyManager.office_gen_landincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            office_high_tech_landincome_forui -= (double)pc_EconomyManager.office_high_tech_landincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            comm_high_tradeincome_forui -= (double)pc_EconomyManager.comm_high_tradeincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            comm_low_tradeincome_forui -= (double)pc_EconomyManager.comm_low_tradeincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            comm_lei_tradeincome_forui -= (double)pc_EconomyManager.comm_lei_tradeincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            comm_tou_tradeincome_forui -= (double)pc_EconomyManager.comm_tou_tradeincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            comm_eco_tradeincome_forui -= (double)pc_EconomyManager.comm_eco_tradeincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            indu_gen_tradeincome_forui -= (double)pc_EconomyManager.indu_gen_tradeincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            indu_farmer_tradeincome_forui -= (double)pc_EconomyManager.indu_farmer_tradeincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            indu_foresty_tradeincome_forui -= (double)pc_EconomyManager.indu_foresty_tradeincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            indu_oil_tradeincome_forui -= (double)pc_EconomyManager.indu_oil_tradeincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            indu_ore_tradeincome_forui -= (double)pc_EconomyManager.indu_ore_tradeincome_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;

            road_income_forui -= (double)pc_EconomyManager.road_income_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            cemetery_income_forui -= (double)pc_EconomyManager.cemetery_income_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            garbage_income_forui -= (double)pc_EconomyManager.garbage_income_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;

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
            city_playerbuilding_income_total += cemetery_income_forui;
            city_playerbuilding_income_total += garbage_income_forui;

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
