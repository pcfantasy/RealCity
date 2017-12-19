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

        public static float WIDTH = 850f;

        private static readonly float HEIGHT = 1000f;

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
        private UILabel cemetery_income_title;
        private UILabel police_income_title;
        private UILabel firestation_income_title;
        private UILabel school_income_title;

        //7、all total income
        private UILabel all_total_income_ui;


        //7、task
        private UILabel task_ui;

        private UILabel infinity_garbage;
        private UILabel infinity_dead;
        private UILabel crasy_transport;
        private UILabel happy_holiday;
        private UILabel is_weekend;

        public static UICheckBox infinity_garbage_Checkbox;
        public static UICheckBox infinity_dead_Checkbox;
        public static UICheckBox crasy_transport_Checkbox;
        public static UICheckBox happy_holiday_Checkbox;
        public static UICheckBox is_weekend_Checkbox;

        private UILabel task_num;
        private UILabel task_time;
        private UILabel cd_num;


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

        public static double police_income_forui;
        public static double firestation_income_forui;
        public static double school_income_forui;

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
        public static bool refesh_onece = false;





        //private static bool isRefreshing = false;

        //private bool CoDisplayRefreshEnabled;

        public override void Update()
        {
            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.R))
            {
                this.ProcessVisibility();
                DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "ctrl+R found");
            }
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
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 - 50), (float)(Loader.parentGuiView.fixedHeight / 2 - 500));
            base.opacity = 1f;
            base.cachedName = cacheName;
            this.CurrentMode = Singleton<ToolManager>.instance.m_properties.m_mode;
            this.m_DragHandler = base.AddUIComponent<UIDragHandle>();
            this.m_DragHandler.target = this;
            this.m_title = base.AddUIComponent<UILabel>();
            this.m_title.text = language.RealCityUI1[98];
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

            //base.StartCoroutine(this.RefreshDisplayDataWrapper());
            this.RefreshDisplayData();
        }

        //private void GetDataNeeded()
        //{
        //this._tmpPopData = comm_data.last_pop;
        //this._tmpdeadcount = comm_data.last_bank_count;
        //}

        private void ShowOnGui()
        {
            //1、citizen tax income
            this.citizen_tax_income_title = base.AddUIComponent<UILabel>();
            this.citizen_tax_income_title.text = language.RealCityUI1[0];
            this.citizen_tax_income_title.tooltip = "N/A";
            this.citizen_tax_income_title.textScale = 1.1f;
            this.citizen_tax_income_title.relativePosition = new Vector3(SPACING, 50f);
            this.citizen_tax_income_title.autoSize = true;
            this.citizen_tax_income_title.name = "Moreeconomic_Text_0";

            this.citizen_tax_income = base.AddUIComponent<UILabel>();
            this.citizen_tax_income.text = language.RealCityUI1[1];
            this.citizen_tax_income.tooltip = language.RealCityUI1[2];
            this.citizen_tax_income.relativePosition = new Vector3(SPACING, this.citizen_tax_income_title.relativePosition.y + SPACING22);
            this.citizen_tax_income.autoSize = true;
            this.citizen_tax_income.name = "Moreeconomic_Text_1";

            //2、City tourism income
            this.city_tourism_income_title = base.AddUIComponent<UILabel>();
            this.city_tourism_income_title.text = language.RealCityUI1[3];
            this.city_tourism_income_title.tooltip = "N/A";
            this.city_tourism_income_title.textScale = 1.1f;
            this.city_tourism_income_title.relativePosition = new Vector3(SPACING, this.citizen_tax_income.relativePosition.y + SPACING22 + 10f);
            this.city_tourism_income_title.autoSize = true;
            this.city_tourism_income_title.name = "Moreeconomic_Text_2";

            this.citizen_income = base.AddUIComponent<UILabel>();
            this.citizen_income.text = language.RealCityUI1[4];
            this.citizen_income.tooltip = language.RealCityUI1[5];
            this.citizen_income.relativePosition = new Vector3(SPACING, this.city_tourism_income_title.relativePosition.y + SPACING22);
            this.citizen_income.autoSize = true;
            this.citizen_income.name = "Moreeconomic_Text_3";

            this.tourist_income = base.AddUIComponent<UILabel>();
            this.tourist_income.text = language.RealCityUI1[6];
            this.tourist_income.tooltip = language.RealCityUI1[7];
            this.tourist_income.relativePosition = new Vector3(this.citizen_income.relativePosition.x + this.citizen_income.width + SPACING + 140f, this.citizen_income.relativePosition.y);
            this.tourist_income.autoSize = true;
            this.tourist_income.name = "Moreeconomic_Text_4";

            //3、City land tax income
            this.land_income_title = base.AddUIComponent<UILabel>();
            this.land_income_title.text = language.RealCityUI1[8];
            this.land_income_title.tooltip = language.RealCityUI1[9];
            this.land_income_title.textScale = 1.1f;
            this.land_income_title.relativePosition = new Vector3(SPACING, this.citizen_income.relativePosition.y + SPACING22 + 10f);
            this.land_income_title.autoSize = true;
            this.land_income_title.name = "Moreeconomic_Text_5";

            this.resident_high_landincome = base.AddUIComponent<UILabel>();
            this.resident_high_landincome.text = language.RealCityUI1[10];
            this.resident_high_landincome.tooltip = language.RealCityUI1[11];
            this.resident_high_landincome.relativePosition = new Vector3(SPACING, this.land_income_title.relativePosition.y + SPACING22);
            this.resident_high_landincome.autoSize = true;
            this.resident_high_landincome.name = "Moreeconomic_Text_6";

            this.resident_low_landincome = base.AddUIComponent<UILabel>();
            this.resident_low_landincome.text = language.RealCityUI1[12];
            this.resident_low_landincome.tooltip = language.RealCityUI1[13];
            this.resident_low_landincome.relativePosition = new Vector3(this.resident_high_landincome.relativePosition.x + this.resident_high_landincome.width + SPACING + 120f, this.resident_high_landincome.relativePosition.y);
            this.resident_low_landincome.autoSize = true;
            this.resident_low_landincome.name = "Moreeconomic_Text_7";

            this.resident_high_eco_landincome = base.AddUIComponent<UILabel>();
            this.resident_high_eco_landincome.text = language.RealCityUI1[14];
            this.resident_high_eco_landincome.tooltip = language.RealCityUI1[15];
            this.resident_high_eco_landincome.relativePosition = new Vector3(SPACING, this.resident_high_landincome.relativePosition.y + SPACING22);
            this.resident_high_eco_landincome.autoSize = true;
            this.resident_high_eco_landincome.name = "Moreeconomic_Text_8";

            this.resident_low_eco_landincome = base.AddUIComponent<UILabel>();
            this.resident_low_eco_landincome.text = language.RealCityUI1[16];
            this.resident_low_eco_landincome.tooltip = language.RealCityUI1[17];
            this.resident_low_eco_landincome.relativePosition = new Vector3(this.resident_low_landincome.relativePosition.x, this.resident_low_landincome.relativePosition.y + SPACING22);
            this.resident_low_eco_landincome.autoSize = true;
            this.resident_low_eco_landincome.name = "Moreeconomic_Text_8";

            this.comm_high_landincome = base.AddUIComponent<UILabel>();
            this.comm_high_landincome.text = language.RealCityUI1[18];
            this.comm_high_landincome.tooltip = language.RealCityUI1[19];
            this.comm_high_landincome.relativePosition = new Vector3(SPACING, this.resident_high_eco_landincome.relativePosition.y + SPACING22);
            this.comm_high_landincome.autoSize = true;
            this.comm_high_landincome.name = "Moreeconomic_Text_9";

            this.comm_low_landincome = base.AddUIComponent<UILabel>();
            this.comm_low_landincome.text = language.RealCityUI1[20];
            this.comm_low_landincome.tooltip = language.RealCityUI1[21];
            this.comm_low_landincome.relativePosition = new Vector3(this.resident_low_eco_landincome.relativePosition.x, this.resident_low_eco_landincome.relativePosition.y + SPACING22);
            this.comm_low_landincome.autoSize = true;
            this.comm_low_landincome.name = "Moreeconomic_Text_10";

            this.comm_eco_landincome = base.AddUIComponent<UILabel>();
            this.comm_eco_landincome.text = language.RealCityUI1[22];
            this.comm_eco_landincome.tooltip = language.RealCityUI1[23];
            this.comm_eco_landincome.relativePosition = new Vector3(SPACING, this.comm_high_landincome.relativePosition.y + SPACING22);
            this.comm_eco_landincome.autoSize = true;
            this.comm_eco_landincome.name = "Moreeconomic_Text_11";

            this.comm_tou_landincome = base.AddUIComponent<UILabel>();
            this.comm_tou_landincome.text = language.RealCityUI1[24];
            this.comm_tou_landincome.tooltip = language.RealCityUI1[25];
            this.comm_tou_landincome.relativePosition = new Vector3(this.comm_low_landincome.relativePosition.x, this.comm_low_landincome.relativePosition.y + SPACING22);
            this.comm_tou_landincome.autoSize = true;
            this.comm_tou_landincome.name = "Moreeconomic_Text_12";

            this.comm_lei_landincome = base.AddUIComponent<UILabel>();
            this.comm_lei_landincome.text = language.RealCityUI1[26];
            this.comm_lei_landincome.tooltip = language.RealCityUI1[27];
            this.comm_lei_landincome.relativePosition = new Vector3(SPACING, this.comm_eco_landincome.relativePosition.y + SPACING22);
            this.comm_lei_landincome.autoSize = true;
            this.comm_lei_landincome.name = "Moreeconomic_Text_13";

            this.indu_gen_landincome = base.AddUIComponent<UILabel>();
            this.indu_gen_landincome.text = language.RealCityUI1[28];
            this.indu_gen_landincome.tooltip = language.RealCityUI1[29];
            this.indu_gen_landincome.relativePosition = new Vector3(this.comm_tou_landincome.relativePosition.x, this.comm_tou_landincome.relativePosition.y + SPACING22);
            this.indu_gen_landincome.autoSize = true;
            this.indu_gen_landincome.name = "Moreeconomic_Text_14";

            this.indu_farmer_landincome = base.AddUIComponent<UILabel>();
            this.indu_farmer_landincome.text = language.RealCityUI1[30];
            this.indu_farmer_landincome.tooltip = language.RealCityUI1[31];
            this.indu_farmer_landincome.relativePosition = new Vector3(SPACING, this.comm_lei_landincome.relativePosition.y + SPACING22);
            this.indu_farmer_landincome.autoSize = true;
            this.indu_farmer_landincome.name = "Moreeconomic_Text_15";

            this.indu_foresty_landincome = base.AddUIComponent<UILabel>();
            this.indu_foresty_landincome.text = language.RealCityUI1[32];
            this.indu_foresty_landincome.tooltip = language.RealCityUI1[33];
            this.indu_foresty_landincome.relativePosition = new Vector3(this.indu_gen_landincome.relativePosition.x, this.indu_gen_landincome.relativePosition.y + SPACING22);
            this.indu_foresty_landincome.autoSize = true;
            this.indu_foresty_landincome.name = "Moreeconomic_Text_16";

            this.indu_oil_landincome = base.AddUIComponent<UILabel>();
            this.indu_oil_landincome.text = language.RealCityUI1[34];
            this.indu_oil_landincome.tooltip = language.RealCityUI1[35];
            this.indu_oil_landincome.relativePosition = new Vector3(SPACING, this.indu_farmer_landincome.relativePosition.y + SPACING22);
            this.indu_oil_landincome.autoSize = true;
            this.indu_oil_landincome.name = "Moreeconomic_Text_17";

            this.indu_ore_landincome = base.AddUIComponent<UILabel>();
            this.indu_ore_landincome.text = language.RealCityUI1[36];
            this.indu_ore_landincome.tooltip = language.RealCityUI1[37];
            this.indu_ore_landincome.relativePosition = new Vector3(this.indu_foresty_landincome.relativePosition.x, this.indu_foresty_landincome.relativePosition.y + SPACING22);
            this.indu_ore_landincome.autoSize = true;
            this.indu_ore_landincome.name = "Moreeconomic_Text_18";

            this.office_gen_landincome = base.AddUIComponent<UILabel>();
            this.office_gen_landincome.text = language.RealCityUI1[38];
            this.office_gen_landincome.tooltip = language.RealCityUI1[39];
            this.office_gen_landincome.relativePosition = new Vector3(SPACING, this.indu_oil_landincome.relativePosition.y + SPACING22);
            this.office_gen_landincome.autoSize = true;
            this.office_gen_landincome.name = "Moreeconomic_Text_19";

            this.office_high_tech_landincome = base.AddUIComponent<UILabel>();
            this.office_high_tech_landincome.text = language.RealCityUI1[40];
            this.office_high_tech_landincome.tooltip = language.RealCityUI1[41];
            this.office_high_tech_landincome.relativePosition = new Vector3(this.indu_ore_landincome.relativePosition.x, this.indu_ore_landincome.relativePosition.y + SPACING22);
            this.office_high_tech_landincome.autoSize = true;
            this.office_high_tech_landincome.name = "Moreeconomic_Text_20";

            //4、City trade tax income
            this.trade_income_title = base.AddUIComponent<UILabel>();
            this.trade_income_title.text = language.RealCityUI1[42];
            this.trade_income_title.tooltip = language.RealCityUI1[43];
            this.trade_income_title.textScale = 1.1f;
            this.trade_income_title.relativePosition = new Vector3(SPACING, this.office_gen_landincome.relativePosition.y + SPACING22 + 10f);
            this.trade_income_title.autoSize = true;
            this.trade_income_title.name = "Moreeconomic_Text_21";

            this.comm_high_tradeincome = base.AddUIComponent<UILabel>();
            this.comm_high_tradeincome.text = language.RealCityUI1[44];
            this.comm_high_tradeincome.tooltip = language.RealCityUI1[45];
            this.comm_high_tradeincome.relativePosition = new Vector3(SPACING, this.trade_income_title.relativePosition.y + SPACING22);
            this.comm_high_tradeincome.autoSize = true;
            this.comm_high_tradeincome.name = "Moreeconomic_Text_22";

            this.comm_low_tradeincome = base.AddUIComponent<UILabel>();
            this.comm_low_tradeincome.text = language.RealCityUI1[46];
            this.comm_low_tradeincome.tooltip = language.RealCityUI1[47];
            this.comm_low_tradeincome.relativePosition = new Vector3(this.comm_high_tradeincome.relativePosition.x + this.comm_high_tradeincome.width + SPACING + 120f, this.comm_high_tradeincome.relativePosition.y);
            this.comm_low_tradeincome.autoSize = true;
            this.comm_low_tradeincome.name = "Moreeconomic_Text_23";

            this.comm_eco_tradeincome = base.AddUIComponent<UILabel>();
            this.comm_eco_tradeincome.text = language.RealCityUI1[48];
            this.comm_eco_tradeincome.tooltip = language.RealCityUI1[49];
            this.comm_eco_tradeincome.relativePosition = new Vector3(SPACING, this.comm_high_tradeincome.relativePosition.y + SPACING22);
            this.comm_eco_tradeincome.autoSize = true;
            this.comm_eco_tradeincome.name = "Moreeconomic_Text_24";

            this.comm_tou_tradeincome = base.AddUIComponent<UILabel>();
            this.comm_tou_tradeincome.text = language.RealCityUI1[50];
            this.comm_tou_tradeincome.tooltip = language.RealCityUI1[51];
            this.comm_tou_tradeincome.relativePosition = new Vector3(this.comm_low_tradeincome.relativePosition.x, this.comm_low_tradeincome.relativePosition.y + SPACING22);
            this.comm_tou_tradeincome.autoSize = true;
            this.comm_tou_tradeincome.name = "Moreeconomic_Text_25";

            this.comm_lei_tradeincome = base.AddUIComponent<UILabel>();
            this.comm_lei_tradeincome.text = language.RealCityUI1[52];
            this.comm_lei_tradeincome.tooltip = language.RealCityUI1[53];
            this.comm_lei_tradeincome.relativePosition = new Vector3(SPACING, this.comm_eco_tradeincome.relativePosition.y + SPACING22);
            this.comm_lei_tradeincome.autoSize = true;
            this.comm_lei_tradeincome.name = "Moreeconomic_Text_26";

            this.indu_gen_tradeincome = base.AddUIComponent<UILabel>();
            this.indu_gen_tradeincome.text = language.RealCityUI1[54];
            this.indu_gen_tradeincome.tooltip = language.RealCityUI1[55];
            this.indu_gen_tradeincome.relativePosition = new Vector3(this.comm_tou_tradeincome.relativePosition.x, this.comm_tou_tradeincome.relativePosition.y + SPACING22);
            this.indu_gen_tradeincome.autoSize = true;
            this.indu_gen_tradeincome.name = "Moreeconomic_Text_27";

            this.indu_farmer_tradeincome = base.AddUIComponent<UILabel>();
            this.indu_farmer_tradeincome.text = language.RealCityUI1[56];
            this.indu_farmer_tradeincome.tooltip = language.RealCityUI1[57];
            this.indu_farmer_tradeincome.relativePosition = new Vector3(SPACING, this.comm_lei_tradeincome.relativePosition.y + SPACING22);
            this.indu_farmer_tradeincome.autoSize = true;
            this.indu_farmer_tradeincome.name = "Moreeconomic_Text_28";

            this.indu_foresty_tradeincome = base.AddUIComponent<UILabel>();
            this.indu_foresty_tradeincome.text = language.RealCityUI1[58];
            this.indu_foresty_tradeincome.tooltip = language.RealCityUI1[59];
            this.indu_foresty_tradeincome.relativePosition = new Vector3(this.indu_gen_tradeincome.relativePosition.x, this.indu_gen_tradeincome.relativePosition.y + SPACING22);
            this.indu_foresty_tradeincome.autoSize = true;
            this.indu_foresty_tradeincome.name = "Moreeconomic_Text_29";

            this.indu_oil_tradeincome = base.AddUIComponent<UILabel>();
            this.indu_oil_tradeincome.text = language.RealCityUI1[60];
            this.indu_oil_tradeincome.tooltip = language.RealCityUI1[61];
            this.indu_oil_tradeincome.relativePosition = new Vector3(SPACING, this.indu_farmer_tradeincome.relativePosition.y + SPACING22);
            this.indu_oil_tradeincome.autoSize = true;
            this.indu_oil_tradeincome.name = "Moreeconomic_Text_30";

            this.indu_ore_tradeincome = base.AddUIComponent<UILabel>();
            this.indu_ore_tradeincome.text = language.RealCityUI1[62];
            this.indu_ore_tradeincome.tooltip = language.RealCityUI1[63];
            this.indu_ore_tradeincome.relativePosition = new Vector3(this.indu_foresty_tradeincome.relativePosition.x, this.indu_foresty_tradeincome.relativePosition.y + SPACING22);
            this.indu_ore_tradeincome.autoSize = true;
            this.indu_ore_tradeincome.name = "Moreeconomic_Text_31";

            //5、Public transport income
            this.public_transport_income_title = base.AddUIComponent<UILabel>();
            this.public_transport_income_title.text = language.RealCityUI1[64];
            this.public_transport_income_title.tooltip = "N/A";
            this.public_transport_income_title.textScale = 1.1f;
            this.public_transport_income_title.relativePosition = new Vector3(SPACING, this.indu_oil_tradeincome.relativePosition.y + SPACING22 + 10f);
            this.public_transport_income_title.autoSize = true;
            this.public_transport_income_title.name = "Moreeconomic_Text_32";

            this.from_bus = base.AddUIComponent<UILabel>();
            this.from_bus.text = language.RealCityUI1[65];
            this.from_bus.tooltip = language.RealCityUI1[66];
            this.from_bus.relativePosition = new Vector3(SPACING, this.public_transport_income_title.relativePosition.y + SPACING22);
            this.from_bus.autoSize = true;
            this.from_bus.name = "Moreeconomic_Text_33";

            this.from_tram = base.AddUIComponent<UILabel>();
            this.from_tram.text = language.RealCityUI1[67];
            this.from_tram.tooltip = language.RealCityUI1[68];
            this.from_tram.relativePosition = new Vector3(this.from_bus.relativePosition.x + this.from_bus.width + SPACING + 120f, this.from_bus.relativePosition.y);
            this.from_tram.autoSize = true;
            this.from_tram.name = "Moreeconomic_Text_33";

            this.from_metro = base.AddUIComponent<UILabel>();
            this.from_metro.text = language.RealCityUI1[69];
            this.from_metro.tooltip = language.RealCityUI1[70];
            this.from_metro.relativePosition = new Vector3(this.from_tram.relativePosition.x + this.from_tram.width + SPACING + 120f, this.from_tram.relativePosition.y);
            this.from_metro.autoSize = true;
            this.from_metro.name = "Moreeconomic_Text_34";

            this.from_train = base.AddUIComponent<UILabel>();
            this.from_train.text = language.RealCityUI1[71];
            this.from_train.tooltip = language.RealCityUI1[72];
            this.from_train.relativePosition = new Vector3(SPACING, this.from_bus.relativePosition.y + SPACING22);
            this.from_train.autoSize = true;
            this.from_train.name = "Moreeconomic_Text_35";

            this.from_ship = base.AddUIComponent<UILabel>();
            this.from_ship.text = language.RealCityUI1[73];
            this.from_ship.tooltip = language.RealCityUI1[74];
            this.from_ship.relativePosition = new Vector3(this.from_tram.relativePosition.x, this.from_tram.relativePosition.y + SPACING22);
            this.from_ship.autoSize = true;
            this.from_ship.name = "Moreeconomic_Text_36";

            this.from_taxi = base.AddUIComponent<UILabel>();
            this.from_taxi.text = language.RealCityUI1[75];
            this.from_taxi.tooltip = language.RealCityUI1[76];
            this.from_taxi.relativePosition = new Vector3(this.from_metro.relativePosition.x, this.from_metro.relativePosition.y + SPACING22);
            this.from_taxi.autoSize = true;
            this.from_taxi.name = "Moreeconomic_Text_37";

            this.from_plane = base.AddUIComponent<UILabel>();
            this.from_plane.text = language.RealCityUI1[77];
            this.from_plane.tooltip = language.RealCityUI1[78];
            this.from_plane.relativePosition = new Vector3(SPACING, this.from_train.relativePosition.y + SPACING22);
            this.from_plane.autoSize = true;
            this.from_plane.name = "Moreeconomic_Text_38";

            this.from_cable_car = base.AddUIComponent<UILabel>();
            this.from_cable_car.text = language.RealCityUI1[79];
            this.from_cable_car.tooltip = language.RealCityUI1[80];
            this.from_cable_car.relativePosition = new Vector3(this.from_ship.relativePosition.x, this.from_ship.relativePosition.y + SPACING22);
            this.from_cable_car.autoSize = true;
            this.from_cable_car.name = "Moreeconomic_Text_39";

            this.from_monorail = base.AddUIComponent<UILabel>();
            this.from_monorail.text = language.RealCityUI1[81];
            this.from_monorail.tooltip = language.RealCityUI1[82];
            this.from_monorail.relativePosition = new Vector3(this.from_taxi.relativePosition.x, this.from_taxi.relativePosition.y + SPACING22);
            this.from_monorail.autoSize = true;
            this.from_monorail.name = "Moreeconomic_Text_40";

            //6、Public transport income
            this.goverment_income_title = base.AddUIComponent<UILabel>();
            this.goverment_income_title.text = language.RealCityUI1[83];
            this.goverment_income_title.tooltip = language.RealCityUI1[84];
            this.goverment_income_title.textScale = 1.1f;
            this.goverment_income_title.relativePosition = new Vector3(SPACING, this.from_plane.relativePosition.y + SPACING22 + 10f);
            this.goverment_income_title.autoSize = true;
            this.goverment_income_title.name = "Moreeconomic_Text_41";

            this.road_income_title = base.AddUIComponent<UILabel>();
            this.road_income_title.text = language.RealCityUI1[85];
            this.road_income_title.tooltip = language.RealCityUI1[86];
            this.road_income_title.relativePosition = new Vector3(SPACING, this.goverment_income_title.relativePosition.y + SPACING22);
            this.road_income_title.autoSize = true;
            this.road_income_title.name = "Moreeconomic_Text_42";

            this.cemetery_income_title = base.AddUIComponent<UILabel>();
            this.cemetery_income_title.text = language.RealCityUI1[87];
            this.cemetery_income_title.tooltip = language.RealCityUI1[88];
            this.cemetery_income_title.relativePosition = new Vector3(this.road_income_title.relativePosition.x + this.road_income_title.width + SPACING + 120f, this.road_income_title.relativePosition.y);
            this.cemetery_income_title.autoSize = true;
            this.cemetery_income_title.name = "Moreeconomic_Text_43";

            this.garbage_income_title = base.AddUIComponent<UILabel>();
            this.garbage_income_title.text = language.RealCityUI1[89];
            this.garbage_income_title.tooltip = language.RealCityUI1[90];
            this.garbage_income_title.relativePosition = new Vector3(this.cemetery_income_title.relativePosition.x + this.cemetery_income_title.width + SPACING + 120f, this.cemetery_income_title.relativePosition.y);
            this.garbage_income_title.autoSize = true;
            this.garbage_income_title.name = "Moreeconomic_Text_44";

            this.police_income_title = base.AddUIComponent<UILabel>();
            this.police_income_title.text = language.RealCityUI1[91];
            this.police_income_title.tooltip = language.RealCityUI1[92];
            this.police_income_title.relativePosition = new Vector3(SPACING, this.road_income_title.relativePosition.y + SPACING22);
            this.police_income_title.autoSize = true;
            this.police_income_title.name = "Moreeconomic_Text_42";

            this.school_income_title = base.AddUIComponent<UILabel>();
            this.school_income_title.text = language.RealCityUI1[93];
            this.school_income_title.tooltip = language.RealCityUI1[94];
            this.school_income_title.relativePosition = new Vector3(this.cemetery_income_title.relativePosition.x, this.cemetery_income_title.relativePosition.y + SPACING22);
            this.school_income_title.autoSize = true;
            this.school_income_title.name = "Moreeconomic_Text_43";

            this.firestation_income_title = base.AddUIComponent<UILabel>();
            this.firestation_income_title.text = language.RealCityUI1[95];
            this.firestation_income_title.tooltip = language.RealCityUI1[96];
            this.firestation_income_title.relativePosition = new Vector3(this.garbage_income_title.relativePosition.x, this.garbage_income_title.relativePosition.y + SPACING22);
            this.firestation_income_title.autoSize = true;
            this.firestation_income_title.name = "Moreeconomic_Text_44";

            //6 all total
            this.all_total_income_ui = base.AddUIComponent<UILabel>();
            this.all_total_income_ui.text = language.RealCityUI1[97];
            this.all_total_income_ui.tooltip = "N/A";
            this.all_total_income_ui.textScale = 1.2f;
            this.all_total_income_ui.relativePosition = new Vector3(SPACING, this.firestation_income_title.relativePosition.y + SPACING22 + 15f);
            this.all_total_income_ui.autoSize = true;
            this.all_total_income_ui.name = "Moreeconomic_Text_45";


            this.task_ui = base.AddUIComponent<UILabel>();
            this.task_ui.text = language.RealCityUI1[99];
            this.task_ui.tooltip = language.RealCityUI1[99];
            this.task_ui.relativePosition = new Vector3(SPACING, this.all_total_income_ui.relativePosition.y + SPACING22 + 20f);
            this.task_ui.autoSize = true;
            this.task_ui.name = "Moreeconomic_Text_46";


            infinity_garbage_Checkbox = base.AddUIComponent<UICheckBox>();
            infinity_garbage_Checkbox.relativePosition = new Vector3(SPACING, this.task_ui.relativePosition.y + 30f);
            this.infinity_garbage = base.AddUIComponent<UILabel>();
            this.infinity_garbage.relativePosition = new Vector3(infinity_garbage_Checkbox.relativePosition.x + infinity_garbage_Checkbox.width + SPACING * 2f, infinity_garbage_Checkbox.relativePosition.y + 5f);
            this.infinity_garbage.tooltip = language.RealCityUI1[100];
            infinity_garbage_Checkbox.height = 16f;
            infinity_garbage_Checkbox.width = 16f;
            infinity_garbage_Checkbox.label = this.infinity_garbage;
            infinity_garbage_Checkbox.text = language.RealCityUI1[100];
            UISprite uISprite = infinity_garbage_Checkbox.AddUIComponent<UISprite>();
            uISprite.height = 20f;
            uISprite.width = 20f;
            uISprite.relativePosition = new Vector3(0f, 0f);
            uISprite.spriteName = "check-unchecked";
            uISprite.isVisible = true;
            UISprite uISprite2 = infinity_garbage_Checkbox.AddUIComponent<UISprite>();
            uISprite2.height = 20f;
            uISprite2.width = 20f;
            uISprite2.relativePosition = new Vector3(0f, 0f);
            uISprite2.spriteName = "check-checked";
            infinity_garbage_Checkbox.checkedBoxObject = uISprite2;
            infinity_garbage_Checkbox.isChecked = comm_data.garbage_task;
            infinity_garbage_Checkbox.isEnabled = true;
            infinity_garbage_Checkbox.isVisible = true;
            infinity_garbage_Checkbox.canFocus = true;
            infinity_garbage_Checkbox.isInteractive = true;
            infinity_garbage_Checkbox.eventCheckChanged += delegate (UIComponent component, bool eventParam)
            {
                infinity_garbage_Checkbox_OnCheckChanged(component, eventParam);
            };


            infinity_dead_Checkbox = base.AddUIComponent<UICheckBox>();
            infinity_dead_Checkbox.relativePosition = new Vector3(SPACING, infinity_garbage_Checkbox.relativePosition.y + 30f);
            this.infinity_dead = base.AddUIComponent<UILabel>();
            this.infinity_dead.relativePosition = new Vector3(infinity_dead_Checkbox.relativePosition.x + infinity_dead_Checkbox.width + SPACING * 2f, infinity_dead_Checkbox.relativePosition.y + 5f);
            this.infinity_dead.tooltip = language.RealCityUI1[101];
            infinity_dead_Checkbox.height = 16f;
            infinity_dead_Checkbox.width = 16f;
            infinity_dead_Checkbox.label = this.infinity_dead;
            infinity_dead_Checkbox.text = language.RealCityUI1[101];
            UISprite uISprite3 = infinity_dead_Checkbox.AddUIComponent<UISprite>();
            uISprite3.height = 20f;
            uISprite3.width = 20f;
            uISprite3.relativePosition = new Vector3(0f, 0f);
            uISprite3.spriteName = "check-unchecked";
            uISprite3.isVisible = true;
            UISprite uISprite4 = infinity_dead_Checkbox.AddUIComponent<UISprite>();
            uISprite4.height = 20f;
            uISprite4.width = 20f;
            uISprite4.relativePosition = new Vector3(0f, 0f);
            uISprite4.spriteName = "check-checked";
            infinity_dead_Checkbox.checkedBoxObject = uISprite4;
            infinity_dead_Checkbox.isChecked = comm_data.dead_task;
            infinity_dead_Checkbox.isEnabled = true;
            infinity_dead_Checkbox.isVisible = true;
            infinity_dead_Checkbox.canFocus = true;
            infinity_dead_Checkbox.isInteractive = true;
            infinity_dead_Checkbox.eventCheckChanged += delegate (UIComponent component, bool eventParam)
            {
                infinity_dead_Checkbox_OnCheckChanged(component, eventParam);
            };


            crasy_transport_Checkbox = base.AddUIComponent<UICheckBox>();
            crasy_transport_Checkbox.relativePosition = new Vector3(SPACING, infinity_dead_Checkbox.relativePosition.y + 30f);
            this.crasy_transport = base.AddUIComponent<UILabel>();
            this.crasy_transport.relativePosition = new Vector3(crasy_transport_Checkbox.relativePosition.x + crasy_transport_Checkbox.width + SPACING * 2f, crasy_transport_Checkbox.relativePosition.y + 5f);
            this.crasy_transport.tooltip = language.RealCityUI1[102];
            crasy_transport_Checkbox.height = 16f;
            crasy_transport_Checkbox.width = 16f;
            crasy_transport_Checkbox.label = this.crasy_transport;
            crasy_transport_Checkbox.text = language.RealCityUI1[102];
            UISprite uISprite5 = crasy_transport_Checkbox.AddUIComponent<UISprite>();
            uISprite5.height = 20f;
            uISprite5.width = 20f;
            uISprite5.relativePosition = new Vector3(0f, 0f);
            uISprite5.spriteName = "check-unchecked";
            uISprite5.isVisible = true;
            UISprite uISprite6 = crasy_transport_Checkbox.AddUIComponent<UISprite>();
            uISprite6.height = 20f;
            uISprite6.width = 20f;
            uISprite6.relativePosition = new Vector3(0f, 0f);
            uISprite6.spriteName = "check-checked";
            crasy_transport_Checkbox.checkedBoxObject = uISprite6;
            crasy_transport_Checkbox.isChecked = comm_data.crasy_task;
            crasy_transport_Checkbox.isEnabled = true;
            crasy_transport_Checkbox.isVisible = true;
            crasy_transport_Checkbox.canFocus = true;
            crasy_transport_Checkbox.isInteractive = true;
            crasy_transport_Checkbox.eventCheckChanged += delegate (UIComponent component, bool eventParam)
            {
                crasy_transport_Checkbox_OnCheckChanged(component, eventParam);
            };


            happy_holiday_Checkbox = base.AddUIComponent<UICheckBox>();
            happy_holiday_Checkbox.relativePosition = new Vector3(SPACING, crasy_transport_Checkbox.relativePosition.y + 30f);
            this.happy_holiday = base.AddUIComponent<UILabel>();
            this.happy_holiday.relativePosition = new Vector3(happy_holiday_Checkbox.relativePosition.x + happy_holiday_Checkbox.width + SPACING * 2f, happy_holiday_Checkbox.relativePosition.y + 5f);
            this.happy_holiday.tooltip = language.RealCityUI1[103];
            happy_holiday_Checkbox.height = 16f;
            happy_holiday_Checkbox.width = 16f;
            happy_holiday_Checkbox.label = this.happy_holiday;
            happy_holiday_Checkbox.text = language.RealCityUI1[103];
            UISprite uISprite7 = happy_holiday_Checkbox.AddUIComponent<UISprite>();
            uISprite7.height = 20f;
            uISprite7.width = 20f;
            uISprite7.relativePosition = new Vector3(0f, 0f);
            uISprite7.spriteName = "check-unchecked";
            uISprite7.isVisible = true;
            UISprite uISprite8 = happy_holiday_Checkbox.AddUIComponent<UISprite>();
            uISprite8.height = 20f;
            uISprite8.width = 20f;
            uISprite8.relativePosition = new Vector3(0f, 0f);
            uISprite8.spriteName = "check-checked";
            happy_holiday_Checkbox.checkedBoxObject = uISprite8;
            happy_holiday_Checkbox.isChecked = comm_data.happy_task;
            happy_holiday_Checkbox.isEnabled = true;
            happy_holiday_Checkbox.isVisible = true;
            happy_holiday_Checkbox.canFocus = true;
            happy_holiday_Checkbox.isInteractive = true;
            happy_holiday_Checkbox.eventCheckChanged += delegate (UIComponent component, bool eventParam)
            {
                happy_holiday_Checkbox_OnCheckChanged(component, eventParam);
            };

            is_weekend_Checkbox = base.AddUIComponent<UICheckBox>();
            is_weekend_Checkbox.relativePosition = new Vector3(SPACING, happy_holiday_Checkbox.relativePosition.y + 30f);
            this.is_weekend = base.AddUIComponent<UILabel>();
            this.is_weekend.relativePosition = new Vector3(is_weekend_Checkbox.relativePosition.x + is_weekend_Checkbox.width + SPACING * 2f, is_weekend_Checkbox.relativePosition.y + 5f);
            this.is_weekend.tooltip = language.RealCityUI1[110];
            is_weekend_Checkbox.height = 16f;
            is_weekend_Checkbox.width = 16f;
            is_weekend_Checkbox.label = this.is_weekend;
            is_weekend_Checkbox.text = language.RealCityUI1[110];
            UISprite uISprite9 = is_weekend_Checkbox.AddUIComponent<UISprite>();
            uISprite9.height = 20f;
            uISprite9.width = 20f;
            uISprite9.relativePosition = new Vector3(0f, 0f);
            uISprite9.spriteName = "check-unchecked";
            uISprite9.isVisible = true;
            UISprite uISprite10 = is_weekend_Checkbox.AddUIComponent<UISprite>();
            uISprite10.height = 20f;
            uISprite10.width = 20f;
            uISprite10.relativePosition = new Vector3(0f, 0f);
            uISprite10.spriteName = "check-checked";
            is_weekend_Checkbox.checkedBoxObject = uISprite10;
            is_weekend_Checkbox.isChecked = comm_data.is_weekend;
            is_weekend_Checkbox.isEnabled = true;
            is_weekend_Checkbox.isVisible = true;
            is_weekend_Checkbox.canFocus = true;
            is_weekend_Checkbox.isInteractive = true;
            is_weekend_Checkbox.eventCheckChanged += delegate (UIComponent component, bool eventParam)
            {
                is_weekend_Checkbox_OnCheckChanged(component, eventParam);
            };


            this.task_time = base.AddUIComponent<UILabel>();
            this.task_time.text = "remaining_time: [0000]";
            this.task_time.tooltip = language.RealCityUI1[105];
            this.task_time.relativePosition = new Vector3(SPACING, is_weekend_Checkbox.relativePosition.y + SPACING22 + 20f);
            this.task_time.autoSize = true;
            this.task_time.name = "Moreeconomic_Text_47";


            this.task_num = base.AddUIComponent<UILabel>();
            this.task_num.text = "remaining_num: [0000]";
            this.task_num.tooltip = language.RealCityUI1[107];
            this.task_num.relativePosition = new Vector3(this.task_time.relativePosition.x + this.task_time.width + SPACING + 60f, this.task_time.relativePosition.y);
            this.task_num.autoSize = true;
            this.task_num.name = "Moreeconomic_Text_48";

            this.cd_num = base.AddUIComponent<UILabel>();
            this.cd_num.text = "cooldown_time: [0000]";
            this.cd_num.tooltip = language.RealCityUI1[109];
            this.cd_num.relativePosition = new Vector3(this.task_num.relativePosition.x + this.task_num.width + SPACING + 60f, this.task_num.relativePosition.y);
            this.cd_num.autoSize = true;
            this.cd_num.name = "Moreeconomic_Text_49";
        }




        public static void infinity_garbage_Checkbox_OnCheckChanged(UIComponent UIComp, bool bValue)
        {
            if (bValue)
            {
                if (comm_data.cd_num < 0)
                {
                    if (comm_data.family_count > 500 && (!comm_data.happy_task) && (!comm_data.crasy_task) && (!comm_data.dead_task) && (!comm_data.garbage_task))
                    {
                        comm_data.garbage_task = true;
                        comm_data.task_num = 200;
                        comm_data.task_time = 1000;
                    }
                }
            }
            else
            {
                comm_data.garbage_task = false;
                if (!(comm_data.happy_task || comm_data.crasy_task || comm_data.dead_task || comm_data.garbage_task))
                {
                    comm_data.task_num = 0;
                    comm_data.task_time = 0;
                }
            }
        }


        public static void infinity_dead_Checkbox_OnCheckChanged(UIComponent UIComp, bool bValue)
        {
            if (bValue)
            {
                if (comm_data.cd_num < 0)
                {
                    if (comm_data.family_count > 1500 && (!comm_data.happy_task) && (!comm_data.crasy_task) && (!comm_data.dead_task) && (!comm_data.garbage_task))
                    {
                        comm_data.dead_task = true;
                        comm_data.task_num = 200;
                        comm_data.task_time = 1000;
                    }
                }
            }
            else
            {
                comm_data.dead_task = false;
                if (!(comm_data.happy_task || comm_data.crasy_task || comm_data.dead_task ||comm_data.garbage_task))
                {
                    comm_data.task_num = 0;
                    comm_data.task_time = 0;
                } 
            }
        }


        public static void crasy_transport_Checkbox_OnCheckChanged(UIComponent UIComp, bool bValue)
        {
            if (bValue)
            {
                if (comm_data.cd_num < 0)
                {
                    if (comm_data.family_count > 1000 && (!comm_data.happy_task) && (!comm_data.crasy_task) && (!comm_data.dead_task) && (!comm_data.garbage_task))
                    {
                        comm_data.crasy_task = true;
                        comm_data.task_num = 4000;
                        comm_data.task_time = 2000;
                    }
                }
            }
            else
            {
                comm_data.crasy_task = false;
                if (!(comm_data.happy_task || comm_data.crasy_task || comm_data.dead_task || comm_data.garbage_task))
                {
                    comm_data.task_num = 0;
                    comm_data.task_time = 0;
                }
            }
        }

        public static void happy_holiday_Checkbox_OnCheckChanged(UIComponent UIComp, bool bValue)
        {
            if (bValue)
            {
                if (comm_data.cd_num < 0)
                {
                    if (comm_data.family_count > 4000 && (!comm_data.happy_task) && (!comm_data.crasy_task) && (!comm_data.dead_task) && (!comm_data.garbage_task))
                    {
                        comm_data.happy_task = true;
                        comm_data.task_num = 1;
                        comm_data.task_time = 3000;
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, 5000000, ItemClass.Service.Beautification, ItemClass.SubService.None, ItemClass.Level.Level1);
                    }
                }
            }
            else
            {
                comm_data.happy_task = false;
                if (!(comm_data.happy_task || comm_data.crasy_task || comm_data.dead_task || comm_data.garbage_task))
                {
                    comm_data.task_num = 0;
                    comm_data.task_time = 0;
                }
            }
        }

        public static void is_weekend_Checkbox_OnCheckChanged(UIComponent UIComp, bool bValue)
        {
            if (bValue)
            {
                comm_data.is_weekend = true;
            }
            else
            {
                comm_data.is_weekend = false;
            }
        }





        /*private IEnumerator RefreshDisplayDataWrapper()
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
        }*/

        private void RefreshDisplayData()
        {
            //EconomyPanel instance = Singleton<EconomyPanel>.instance;
            //isRefreshing = true;
            uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
            uint num2 = currentFrameIndex & 255u;
            if (refesh_onece)
            {
                if (base.isVisible)
                {
                    process_data();
                    //citizen
                    //this.citizen_count.text = string.Format(language.RealCityUI1[0] + " [{0}]", comm_data.citizen_count);
                    this.m_title.text = language.RealCityUI1[98];
                    this.citizen_tax_income_title.text = string.Format(language.RealCityUI1[0] + " [{0}]  [{1:N2}%]", citizen_tax_income_total, citizen_tax_income_percent * 100);
                    this.citizen_tax_income.text = string.Format(language.RealCityUI1[1] + " [{0}]", citizen_tax_income_forui);
                    this.city_tourism_income_title.text = string.Format(language.RealCityUI1[3] + " [{0}]  [{1:N2}%]", city_tourism_income_total, city_tourism_income_percent * 100);
                    this.citizen_income.text = string.Format(language.RealCityUI1[4] + " [{0}]", citizen_income_forui);
                    this.tourist_income.text = string.Format(language.RealCityUI1[6] + " [{0}]", tourist_income_forui);
                    this.land_income_title.text = string.Format(language.RealCityUI1[8] + " [{0}]  [{1:N2}%]", city_land_income_total, city_land_income_percent * 100);
                    this.resident_high_landincome.text = string.Format(language.RealCityUI1[10] + " [{0}]", resident_high_landincome_forui);
                    this.resident_low_landincome.text = string.Format(language.RealCityUI1[12] + " [{0}]", resident_low_landincome_forui);
                    this.resident_high_eco_landincome.text = string.Format(language.RealCityUI1[14] + " [{0}]", resident_high_eco_landincome_forui);
                    this.resident_low_eco_landincome.text = string.Format(language.RealCityUI1[16] + " [{0}]", resident_low_eco_landincome_forui);
                    this.comm_high_landincome.text = string.Format(language.RealCityUI1[18] + " [{0}]", comm_high_landincome_forui);
                    this.comm_low_landincome.text = string.Format(language.RealCityUI1[20] + " [{0}]", comm_low_landincome_forui);
                    this.comm_lei_landincome.text = string.Format(language.RealCityUI1[22] + " [{0}]", comm_lei_landincome_forui);
                    this.comm_tou_landincome.text = string.Format(language.RealCityUI1[24] + " [{0}]", comm_tou_landincome_forui);
                    this.comm_eco_landincome.text = string.Format(language.RealCityUI1[26] + " [{0}]", comm_eco_landincome_forui);
                    this.indu_gen_landincome.text = string.Format(language.RealCityUI1[28] + " [{0}]", indu_gen_landincome_forui);
                    this.indu_farmer_landincome.text = string.Format(language.RealCityUI1[30] + " [{0}]", indu_farmer_landincome_forui);
                    this.indu_foresty_landincome.text = string.Format(language.RealCityUI1[32] + " [{0}]", indu_foresty_landincome_forui);
                    this.indu_oil_landincome.text = string.Format(language.RealCityUI1[34] + " [{0}]", indu_oil_landincome_forui);
                    this.indu_ore_landincome.text = string.Format(language.RealCityUI1[36] + " [{0}]", indu_ore_landincome_forui);
                    this.office_gen_landincome.text = string.Format(language.RealCityUI1[38] + " [{0}]", office_gen_landincome_forui);
                    this.office_high_tech_landincome.text = string.Format(language.RealCityUI1[40] + " [{0}]", office_high_tech_landincome_forui); ;
                    this.trade_income_title.text = string.Format(language.RealCityUI1[42] + " [{0}]  [{1:N2}%]", city_trade_income_total, city_trade_income_percent * 100);
                    this.comm_high_tradeincome.text = string.Format(language.RealCityUI1[44] + " [{0}]", comm_high_tradeincome_forui);
                    this.comm_low_tradeincome.text = string.Format(language.RealCityUI1[46] + " [{0}]", comm_low_tradeincome_forui);
                    this.comm_lei_tradeincome.text = string.Format(language.RealCityUI1[52] + " [{0}]", comm_lei_tradeincome_forui);
                    this.comm_tou_tradeincome.text = string.Format(language.RealCityUI1[50] + " [{0}]", comm_tou_tradeincome_forui);
                    this.comm_eco_tradeincome.text = string.Format(language.RealCityUI1[48] + " [{0}]", comm_eco_tradeincome_forui);
                    this.indu_gen_tradeincome.text = string.Format(language.RealCityUI1[54] + " [{0}]", indu_gen_tradeincome_forui);
                    this.indu_farmer_tradeincome.text = string.Format(language.RealCityUI1[56] + " [{0}]", indu_farmer_tradeincome_forui);
                    this.indu_foresty_tradeincome.text = string.Format(language.RealCityUI1[58] + " [{0}]", indu_foresty_tradeincome_forui);
                    this.indu_oil_tradeincome.text = string.Format(language.RealCityUI1[60] + " [{0}]", indu_oil_tradeincome_forui);
                    this.indu_ore_tradeincome.text = string.Format(language.RealCityUI1[62] + " [{0}]", indu_ore_tradeincome_forui);
                    this.public_transport_income_title.text = string.Format(language.RealCityUI1[64] + " [{0}]  [{1:N2}%]", city_transport_income_total, city_transport_income_percent * 100);
                    this.from_bus.text = string.Format(language.RealCityUI1[65] + " [{0}]", bus_income);
                    this.from_tram.text = string.Format(language.RealCityUI1[67] + " [{0}]", tram_income);
                    this.from_train.text = string.Format(language.RealCityUI1[69] + " [{0}]", train_income);
                    this.from_ship.text = string.Format(language.RealCityUI1[71] + " [{0}]", ship_income);
                    this.from_plane.text = string.Format(language.RealCityUI1[73] + " [{0}]", plane_income);
                    this.from_metro.text = string.Format(language.RealCityUI1[75] + " [{0}]", metro_income);
                    this.from_taxi.text = string.Format(language.RealCityUI1[77] + " [{0}]", taxi_income);
                    this.from_cable_car.text = string.Format(language.RealCityUI1[79] + " [{0}]", cablecar_income);
                    this.from_monorail.text = string.Format(language.RealCityUI1[81] + " [{0}]", monorail_income);
                    this.goverment_income_title.text = string.Format(language.RealCityUI1[83] + " [{0}]  [{1:N2}%]", city_playerbuilding_income_total, city_playerbuilding_income_percent * 100);
                    this.road_income_title.text = string.Format(language.RealCityUI1[85] + " [{0}]", road_income_forui);
                    this.cemetery_income_title.text = string.Format(language.RealCityUI1[87] + " [{0}]", cemetery_income_forui);
                    this.garbage_income_title.text = string.Format(language.RealCityUI1[89] + " [{0}]", garbage_income_forui);
                    this.police_income_title.text = string.Format(language.RealCityUI1[91] + " [{0}]", police_income_forui);
                    this.school_income_title.text = string.Format(language.RealCityUI1[93] + " [{0}]", school_income_forui);
                    this.firestation_income_title.text = string.Format(language.RealCityUI1[95] + " [{0}]", firestation_income_forui);
                    this.all_total_income_ui.text = string.Format(language.RealCityUI1[97] + " [{0}]", all_total_income);

                    this.task_ui.text = language.RealCityUI1[99];

                    infinity_garbage_Checkbox.text = language.RealCityUI1[100];
                    infinity_dead_Checkbox.text = language.RealCityUI1[101];
                    crasy_transport_Checkbox.text = language.RealCityUI1[102];
                    happy_holiday_Checkbox.text = language.RealCityUI1[103];
                    is_weekend_Checkbox.text = language.RealCityUI1[110];

                    this.task_time.text = string.Format(language.RealCityUI1[104] + " [{0}]", comm_data.task_time);
                    this.task_num.text = string.Format(language.RealCityUI1[106] + " [{0}]", comm_data.task_num);
                    this.cd_num.text = string.Format(language.RealCityUI1[108] + " [{0}]", comm_data.cd_num);
                    refesh_onece = false;
                }
            }



            //isRefreshing = false;
        }

        private void ProcessVisibility()
        {
            if (!base.isVisible)
            {
                refesh_onece = true;
                base.Show();
                /*if (!this.CoDisplayRefreshEnabled)
                {
                    base.StartCoroutine(this.RefreshDisplayDataWrapper());
                    return;
                }*/
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
            school_income_forui = 0f;
            police_income_forui = 0f;
            firestation_income_forui = 0f;


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
                police_income_forui += (double)pc_EconomyManager.police_income_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                school_income_forui += (double)pc_EconomyManager.school_income_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
                firestation_income_forui += (double)pc_EconomyManager.firestation_income_forui[i] * (float)comm_data.game_income_expense_multiple / 100f;
            }
            /*DebugLog.LogToFileOnly(pc_EconomyManager.school_income_forui[0].ToString());
            DebugLog.LogToFileOnly(pc_EconomyManager.school_income_forui[1].ToString());
            DebugLog.LogToFileOnly(pc_EconomyManager.school_income_forui[2].ToString());
            DebugLog.LogToFileOnly(pc_EconomyManager.school_income_forui[3].ToString());
            DebugLog.LogToFileOnly(pc_EconomyManager.school_income_forui[4].ToString());
            DebugLog.LogToFileOnly(pc_EconomyManager.school_income_forui[5].ToString());
            DebugLog.LogToFileOnly(pc_EconomyManager.school_income_forui[6].ToString());
            DebugLog.LogToFileOnly(pc_EconomyManager.school_income_forui[7].ToString());
            DebugLog.LogToFileOnly(pc_EconomyManager.school_income_forui[8].ToString());
            DebugLog.LogToFileOnly(pc_EconomyManager.school_income_forui[9].ToString());
            DebugLog.LogToFileOnly(pc_EconomyManager.school_income_forui[10].ToString());
            DebugLog.LogToFileOnly(pc_EconomyManager.school_income_forui[11].ToString());
            DebugLog.LogToFileOnly(pc_EconomyManager.school_income_forui[12].ToString());
            DebugLog.LogToFileOnly(pc_EconomyManager.school_income_forui[13].ToString());
            DebugLog.LogToFileOnly(pc_EconomyManager.school_income_forui[14].ToString());
            DebugLog.LogToFileOnly(pc_EconomyManager.school_income_forui[15].ToString());
            DebugLog.LogToFileOnly(pc_EconomyManager.school_income_forui[16].ToString());*/
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
            firestation_income_forui -= (double)pc_EconomyManager.firestation_income_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            police_income_forui -= (double)pc_EconomyManager.police_income_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;
            school_income_forui -= (double)pc_EconomyManager.school_income_forui[comm_data.update_money_count] * (float)comm_data.game_income_expense_multiple / 100f;

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
            city_playerbuilding_income_total += police_income_forui;
            city_playerbuilding_income_total += firestation_income_forui;
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
