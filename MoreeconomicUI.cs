using System.Collections.Generic;
using ColossalFramework;
using ColossalFramework.UI;
using UnityEngine;
using System.Collections;

namespace RealCity
{
    public class MoreeconomicUI : UIPanel
    {
        public static readonly string cacheName = "MoreeconomicUI";

        private static readonly float WIDTH = 800f;

        private static readonly float HEIGHT = 650f;

        private static readonly float HEADER = 40f;

        private static readonly float SPACING = 17f;

        private static readonly float SPACING22 = 23f;

        private ItemClass.Availability CurrentMode;

        public static MoreeconomicUI instance;

        private Dictionary<string, UILabel> _valuesControlContainer = new Dictionary<string, UILabel>(16);

        private UIDragHandle m_DragHandler;

        private UIButton m_closeButton;

        private UILabel m_title;

        private UILabel m_HeaderDataText;

        //1 citizen total 12 element
        //second line  citizen status
        private UILabel m_firstline_citizen;
        //1.1 citizen income
        //public static int citizen_count = 0;
        //public static int family_count = 0;
        //public static int citizen_salary_per_family = 0;
        //public static long citizen_salary_total = 0;
        //public static long citizen_salary_tax_total = 0;
        private UILabel citizen_count;
        private UILabel family_count;
        private UILabel citizen_salary_per_family;
        private UILabel citizen_salary_total;
        private UILabel citizen_salary_tax_total;


        //1.2 citizen outcome
        //1.2.1 citizen outcome
        //public static long citizen_outcome_per_family = 0;
        //public static long citizen_outcome = 0;
        private UILabel citizen_outcome_per_family;
        private UILabel citizen_outcome;

        //1.2.2 transport fee
        //public static uint total_citizen_vehical_time = 0;
        //public static long public_transport_fee = 0;
        //public static long all_transport_fee = 0;
        //public static byte citizen_average_transport_fee = 0;
        private UILabel total_citizen_vehical_time;
        private UILabel public_transport_fee;
        private UILabel citizen_average_transport_fee;

        //1.3 income - outcome
        //public static int family_profit_money_num = 0;
        //public static int family_loss_money_num = 0;
        private UILabel family_profit_money_num;
        private UILabel family_loss_money_num;
        private UILabel family_very_profit_num;
        private UILabel family_weight_stable_high;
        private UILabel family_weight_stable_low;

        private UILabel resident_consumption_rate;
        private UILabel tourist_consumption_rate;

        //2 building   27 element
        private UILabel m_secondline_building; //fixed title
        //2.1 building income
        //public static float comm_profit = 5;
        //public static float indu_profit = 5;
        //public static float food_profit = 5;
        //public static float petrol_profit = 5;
        //public static float coal_profit = 5;
        //public static float lumber_profit = 5;
        //public static float oil_profit = 5;
        //public static float ore_profit = 5;
        //public static float grain_profit = 5;
        //public static float log_profit = 5;
        private UILabel good_import_ratio;
        private UILabel food_import_ratio;
        private UILabel petrol_import_ratio;
        private UILabel coal_import_ratio;
        private UILabel lumber_import_ratio;
        private UILabel oil_import_ratio;
        private UILabel ore_import_ratio;
        private UILabel log_import_ratio;
        private UILabel grain_import_ratio;

        private UILabel good_export_ratio;
        private UILabel food_export_ratio;
        private UILabel petrol_export_ratio;
        private UILabel coal_export_ratio;
        private UILabel lumber_export_ratio;
        private UILabel oil_export_ratio;
        private UILabel ore_export_ratio;
        private UILabel log_export_ratio;
        private UILabel grain_export_ratio;

        //2.2 building outcome
        //2.3 building money
        //public static ushort all_comm_building_profit = 0;
        //public static ushort all_industry_building_profit = 0;
        //public static ushort all_foresty_building_profit = 0;
        //public static ushort all_farmer_building_profit = 0;
        //public static ushort all_oil_building_profit = 0;
        //public static ushort all_ore_building_profit = 0;
        //public static ushort all_comm_building_loss = 0;
        //public static ushort all_industry_building_loss = 0;
        //public static ushort all_foresty_building_loss = 0;
        //public static ushort all_farmer_building_loss = 0;
        //public static ushort all_oil_building_loss = 0;
        //public static ushort all_ore_building_loss = 0;
        //public static ushort all_buildings = 0;
        //public static uint total_cargo_vehical_time = 0;
        //public static uint total_cargo_transfer_size = 0;
        //public static uint total_train_transfer_size = 0;
        //public static uint total_train_ship_size = 0;
        private UILabel all_comm_building_profit;
        private UILabel all_comm_building_loss;
        private UILabel all_industry_building_profit;
        private UILabel all_industry_building_loss;
        private UILabel all_foresty_building_profit;
        private UILabel all_foresty_building_loss;
        private UILabel all_farmer_building_profit;
        private UILabel all_farmer_building_loss;
        private UILabel all_oil_building_profit;
        private UILabel all_oil_building_loss;
        private UILabel all_ore_building_profit;
        private UILabel all_ore_building_loss;
        private UILabel all_buildings;
        private UILabel total_cargo_vehical_time;
        private UILabel total_cargo_transfer_size;
        private UILabel total_train_transfer_size;
        private UILabel total_ship_transfer_size;

        private UILabel office_gen_salary_index;
        private UILabel office_high_tech_salary_index;


        //3 goverment


        private static bool isRefreshing = false;

        private bool CoDisplayRefreshEnabled;

        public override void Update()
        {
            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.M))
            {
                this.ProcessVisibility();
                DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "ctrl+M found");
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
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 - 800), (float)(Loader.parentGuiView.fixedHeight / 2 - 350));
            base.opacity = 1f;
            base.cachedName = cacheName;
            this.CurrentMode = Singleton<ToolManager>.instance.m_properties.m_mode;
            this.m_DragHandler = base.AddUIComponent<UIDragHandle>();
            this.m_DragHandler.target = this;
            this.m_title = base.AddUIComponent<UILabel>();
            this.m_title.text = "economic Data";
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
            this.GetDataNeeded();
            this.ShowOnGui();
            //this.PopulateControlContainers();

            base.StartCoroutine(this.RefreshDisplayDataWrapper());
            this.RefreshDisplayData();
        }

        private void GetDataNeeded()
        {
            //this._tmpPopData = comm_data.last_pop;
            //this._tmpdeadcount = comm_data.last_bank_count;
        }

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

            //citizen
            this.m_firstline_citizen = base.AddUIComponent<UILabel>();
            this.m_firstline_citizen.text = "1、citizen status";
            this.m_firstline_citizen.tooltip = "N/A";
            this.m_firstline_citizen.relativePosition = new Vector3(SPACING, this.m_HeaderDataText.relativePosition.y + SPACING22);
            this.m_firstline_citizen.autoSize = true;

            //data
            this.citizen_count = base.AddUIComponent<UILabel>();
            this.citizen_count.text =               string.Format("citizen_count [0000000]");
            this.citizen_count.tooltip = "total citizen_count";
            this.citizen_count.relativePosition = new Vector3(SPACING, this.m_firstline_citizen.relativePosition.y + SPACING22);
            this.citizen_count.autoSize = true;
            this.citizen_count.name = "Moreeconomic_Text_0";

            this.family_count = base.AddUIComponent<UILabel>();
            this.family_count.text =                 string.Format("family_count [0000000]");
            this.family_count.tooltip = "total family_count";
            this.family_count.relativePosition = new Vector3(this.citizen_count.relativePosition.x + this.citizen_count.width + SPACING + 30f, this.citizen_count.relativePosition.y);
            this.family_count.autoSize = true;
            this.family_count.name = "Moreeconomic_Text_1";

            this.citizen_salary_per_family = base.AddUIComponent<UILabel>();
            this.citizen_salary_per_family.text =    string.Format("citizen_salary_per_family [000]");
            this.citizen_salary_per_family.tooltip = "citizen_salary_per_family";
            this.citizen_salary_per_family.relativePosition = new Vector3(this.family_count.relativePosition.x + this.family_count.width + SPACING + 30f, this.family_count.relativePosition.y);
            this.citizen_salary_per_family.autoSize = true;
            this.citizen_salary_per_family.name = "Moreeconomic_Text_2";

            this.citizen_salary_total = base.AddUIComponent<UILabel>();
            this.citizen_salary_total.text =         string.Format("salary_total [00000000]");
            this.citizen_salary_total.tooltip = "total citizen_salary";
            this.citizen_salary_total.relativePosition = new Vector3(SPACING, this.citizen_count.relativePosition.y + SPACING22);
            //this.m_money_forest.relativePosition = new Vector3(this.m_money_farmer.relativePosition.x + this.m_money_farmer.width + SPACING, this.m_money_farmer.relativePosition.y);
            this.citizen_salary_total.autoSize = true;
            this.citizen_salary_total.name = "Moreeconomic_Text_3";

            this.citizen_salary_tax_total = base.AddUIComponent<UILabel>();
            this.citizen_salary_tax_total.text =     string.Format("citizen_tax_total [000]");
            this.citizen_salary_tax_total.tooltip = "total citizen_salary_tax";
            this.citizen_salary_tax_total.relativePosition = new Vector3(this.citizen_salary_total.relativePosition.x + this.citizen_salary_total.width + SPACING + 30f, this.citizen_salary_total.relativePosition.y);
            this.citizen_salary_tax_total.autoSize = true;
            this.citizen_salary_tax_total.name = "Moreeconomic_Text_4";

            this.citizen_outcome_per_family = base.AddUIComponent<UILabel>();
            this.citizen_outcome_per_family.text =   string.Format("outcome_per_family [0000000000]");
            this.citizen_outcome_per_family.tooltip = "citizen_outcome_per_family";
            this.citizen_outcome_per_family.relativePosition = new Vector3(this.citizen_salary_tax_total.relativePosition.x + this.citizen_salary_tax_total.width + SPACING + 40f, this.citizen_salary_tax_total.relativePosition.y);
            this.citizen_outcome_per_family.autoSize = true;
            this.citizen_outcome_per_family.name = "Moreeconomic_Text_5";

            this.citizen_outcome = base.AddUIComponent<UILabel>();
            this.citizen_outcome.text =              string.Format("citizen_outcome [0000]");
            this.citizen_outcome.tooltip = "total citizen_outcome";
            this.citizen_outcome.relativePosition = new Vector3(SPACING, this.citizen_salary_total.relativePosition.y + SPACING22);
            this.citizen_outcome.autoSize = true;
            this.citizen_outcome.name = "Moreeconomic_Text_6";

            this.public_transport_fee = base.AddUIComponent<UILabel>();
            this.public_transport_fee.text =         string.Format("public_trans_fee [0000]");
            this.public_transport_fee.tooltip = "public_transport_fee";
            this.public_transport_fee.relativePosition = new Vector3(this.citizen_outcome.relativePosition.x + this.citizen_outcome.width + SPACING + 30f, this.citizen_outcome.relativePosition.y);
            this.public_transport_fee.autoSize = true;
            this.public_transport_fee.name = "Moreeconomic_Text_7";

            this.total_citizen_vehical_time = base.AddUIComponent<UILabel>();
            this.total_citizen_vehical_time.text =   string.Format("citizen_vehical_time [00000000]");
            this.total_citizen_vehical_time.tooltip = "total citizen_vehical_time";
            this.total_citizen_vehical_time.relativePosition = new Vector3(this.public_transport_fee.relativePosition.x + this.public_transport_fee.width + SPACING + 30f, this.public_transport_fee.relativePosition.y);
            this.total_citizen_vehical_time.autoSize = true;
            this.total_citizen_vehical_time.name = "Moreeconomic_Text_8";

            this.family_profit_money_num = base.AddUIComponent<UILabel>();
            this.family_profit_money_num.text =      string.Format("family_profit_num [000]");
            this.family_profit_money_num.tooltip = "total family_profit_money_num";
            this.family_profit_money_num.relativePosition = new Vector3(SPACING, this.citizen_outcome.relativePosition.y + SPACING22);
            this.family_profit_money_num.autoSize = true;
            this.family_profit_money_num.name = "Moreeconomic_Text_9";

            this.family_loss_money_num = base.AddUIComponent<UILabel>();
            this.family_loss_money_num.text =        string.Format("family_loss_num [00000]");
            this.family_loss_money_num.tooltip = "family_loss_money_num";
            this.family_loss_money_num.relativePosition = new Vector3(this.family_profit_money_num.relativePosition.x + this.family_profit_money_num.width + SPACING + 22f, this.family_profit_money_num.relativePosition.y);
            this.family_loss_money_num.autoSize = true;
            this.family_loss_money_num.name = "Moreeconomic_Text_10";

            this.family_very_profit_num = base.AddUIComponent<UILabel>();
            this.family_very_profit_num.text =       string.Format("family_very_profit_num [00000]");
            this.family_very_profit_num.tooltip = "family_very_profit_num";
            this.family_very_profit_num.relativePosition = new Vector3(this.family_loss_money_num.relativePosition.x + this.family_loss_money_num.width + SPACING + 20f, this.family_loss_money_num.relativePosition.y);
            //this.m_money_forest.relativePosition = new Vector3(this.m_money_farmer.relativePosition.x + this.m_money_farmer.width + SPACING, this.m_money_farmer.relativePosition.y);
            this.family_very_profit_num.autoSize = true;
            this.family_very_profit_num.name = "Moreeconomic_Text_11";

            this.family_weight_stable_high = base.AddUIComponent<UILabel>();
            this.family_weight_stable_high.text =    string.Format("wealth_stable_high [00000]");
            this.family_weight_stable_high.tooltip = "family_wealth_stable_high_num";
            this.family_weight_stable_high.relativePosition = new Vector3(SPACING, this.family_profit_money_num.relativePosition.y + SPACING22);
            this.family_weight_stable_high.autoSize = true;
            this.family_weight_stable_high.name = "Moreeconomic_Text_12";

            this.family_weight_stable_low = base.AddUIComponent<UILabel>();
            this.family_weight_stable_low.text =     string.Format("wealth_stable_low [00000]");
            this.family_weight_stable_low.tooltip = "family_wealth_stable_low";
            this.family_weight_stable_low.relativePosition = new Vector3(this.family_weight_stable_high.relativePosition.x + this.family_weight_stable_high.width + SPACING, this.family_weight_stable_high.relativePosition.y);
            //this.m_money_forest.relativePosition = new Vector3(this.m_money_farmer.relativePosition.x + this.m_money_farmer.width + SPACING, this.m_money_farmer.relativePosition.y);
            this.family_weight_stable_low.autoSize = true;
            this.family_weight_stable_low.name = "Moreeconomic_Text_13";

            this.citizen_average_transport_fee = base.AddUIComponent<UILabel>();
            this.citizen_average_transport_fee.text = string.Format("average_transport_fee [000000]");
            this.citizen_average_transport_fee.tooltip = "citizen_average_transport_fee";
            this.citizen_average_transport_fee.relativePosition = new Vector3(this.family_weight_stable_low.relativePosition.x + this.family_weight_stable_low.width + SPACING, this.family_weight_stable_low.relativePosition.y);
            //this.m_money_forest.relativePosition = new Vector3(this.m_money_farmer.relativePosition.x + this.m_money_farmer.width + SPACING, this.m_money_farmer.relativePosition.y);
            this.citizen_average_transport_fee.autoSize = true;
            this.citizen_average_transport_fee.name = "Moreeconomic_Text_14";

            this.resident_consumption_rate = base.AddUIComponent<UILabel>();
            this.resident_consumption_rate.text = string.Format("resident_consumption_rate [000000]");
            this.resident_consumption_rate.tooltip = "resident_consumption_rate";
            this.resident_consumption_rate.relativePosition = new Vector3(SPACING, this.family_weight_stable_high.relativePosition.y + SPACING22);
            //this.m_money_forest.relativePosition = new Vector3(this.m_money_farmer.relativePosition.x + this.m_money_farmer.width + SPACING, this.m_money_farmer.relativePosition.y);
            this.resident_consumption_rate.autoSize = true;
            this.resident_consumption_rate.name = "Moreeconomic_Text_44";

            this.tourist_consumption_rate = base.AddUIComponent<UILabel>();
            this.tourist_consumption_rate.text = string.Format("outside_consumption_rate [000000]");
            this.tourist_consumption_rate.tooltip = "outside_consumption_rate";
            this.tourist_consumption_rate.relativePosition = new Vector3(this.resident_consumption_rate.relativePosition.x + this.resident_consumption_rate.width + SPACING + 20f, this.resident_consumption_rate.relativePosition.y);
            //this.m_money_forest.relativePosition = new Vector3(this.m_money_farmer.relativePosition.x + this.m_money_farmer.width + SPACING, this.m_money_farmer.relativePosition.y);
            this.tourist_consumption_rate.autoSize = true;
            this.tourist_consumption_rate.name = "Moreeconomic_Text_45";







            //building
            this.m_secondline_building = base.AddUIComponent<UILabel>();
            this.m_secondline_building.text = "2、building status";
            this.m_secondline_building.tooltip = "N/A";
            this.m_secondline_building.relativePosition = new Vector3(SPACING, this.resident_consumption_rate.relativePosition.y + SPACING22);
            this.m_secondline_building.autoSize = true;

            this.good_export_ratio = base.AddUIComponent<UILabel>();
            this.good_export_ratio.text =           string.Format("good_export_ratio  [000]");
            this.good_export_ratio.tooltip = "good_export_ratio";
            this.good_export_ratio.relativePosition = new Vector3(SPACING, this.m_secondline_building.relativePosition.y + SPACING22);
            this.good_export_ratio.autoSize = true;
            this.good_export_ratio.name = "Moreeconomic_Text_15";

            this.food_export_ratio = base.AddUIComponent<UILabel>();
            this.food_export_ratio.text =           string.Format("food_export_ratio [0000]");
            this.food_export_ratio.tooltip = "food_export_ratio";
            this.food_export_ratio.relativePosition = new Vector3(this.good_export_ratio.relativePosition.x + this.good_export_ratio.width + SPACING, this.good_export_ratio.relativePosition.y);
            this.food_export_ratio.autoSize = true;
            this.food_export_ratio.name = "Moreeconomic_Text_16";

            this.petrol_export_ratio = base.AddUIComponent<UILabel>();
            this.petrol_export_ratio.text =         string.Format("petrol_export_ratio [000]");
            this.petrol_export_ratio.tooltip = "petrol_export_ratio";
            this.petrol_export_ratio.relativePosition = new Vector3(this.food_export_ratio.relativePosition.x + this.food_export_ratio.width + SPACING, this.food_export_ratio.relativePosition.y);
            this.petrol_export_ratio.autoSize = true;
            this.petrol_export_ratio.name = "Moreeconomic_Text_17";

            this.coal_export_ratio = base.AddUIComponent<UILabel>();
            this.coal_export_ratio.text =           string.Format("coal_export_ratio [00000]");
            this.coal_export_ratio.tooltip = "coal_export_ratio";
            this.coal_export_ratio.relativePosition = new Vector3(SPACING, this.petrol_export_ratio.relativePosition.y + SPACING22);
            this.coal_export_ratio.autoSize = true;
            this.coal_export_ratio.name = "Moreeconomic_Text_18";

            this.lumber_export_ratio = base.AddUIComponent<UILabel>();
            this.lumber_export_ratio.text =         string.Format("lumber_export_ratio [000]");
            this.lumber_export_ratio.tooltip = "lumber_export_ratio";
            this.lumber_export_ratio.relativePosition = new Vector3(this.coal_export_ratio.relativePosition.x + this.coal_export_ratio.width + SPACING, this.coal_export_ratio.relativePosition.y);
            this.lumber_export_ratio.autoSize = true;
            this.lumber_export_ratio.name = "Moreeconomic_Text_19";

            this.oil_export_ratio = base.AddUIComponent<UILabel>();
            this.oil_export_ratio.text =            string.Format("oil_export_ratio [000000]");
            this.oil_export_ratio.tooltip = "oil_export_ratio";
            this.oil_export_ratio.relativePosition = new Vector3(this.lumber_export_ratio.relativePosition.x + this.lumber_export_ratio.width + SPACING, this.lumber_export_ratio.relativePosition.y);
            this.oil_export_ratio.autoSize = true;
            this.oil_export_ratio.name = "Moreeconomic_Text_20";

            this.ore_export_ratio = base.AddUIComponent<UILabel>();
            this.ore_export_ratio.text =            string.Format("ore_export_ratio [000000]");
            this.ore_export_ratio.tooltip = "ore_export_ratio";
            this.ore_export_ratio.relativePosition = new Vector3(SPACING, this.oil_export_ratio.relativePosition.y + SPACING22);
            this.ore_export_ratio.autoSize = true;
            this.ore_export_ratio.name = "Moreeconomic_Text_21";

            this.log_export_ratio = base.AddUIComponent<UILabel>();
            this.log_export_ratio.text =            string.Format("log_export_ratio [000000]");
            this.log_export_ratio.tooltip = "log_export_ratio";
            this.log_export_ratio.relativePosition = new Vector3(this.ore_export_ratio.relativePosition.x + this.ore_export_ratio.width + SPACING, this.ore_export_ratio.relativePosition.y);
            this.log_export_ratio.autoSize = true;
            this.log_export_ratio.name = "Moreeconomic_Text_22";

            this.grain_export_ratio = base.AddUIComponent<UILabel>();
            this.grain_export_ratio.text =          string.Format("grain_export_ratio [0000]");
            this.grain_export_ratio.tooltip = "grain_export_ratio";
            this.grain_export_ratio.relativePosition = new Vector3(this.log_export_ratio.relativePosition.x + this.log_export_ratio.width + SPACING, this.log_export_ratio.relativePosition.y);
            this.grain_export_ratio.autoSize = true;
            this.grain_export_ratio.name = "Moreeconomic_Text_23";

            this.good_import_ratio = base.AddUIComponent<UILabel>();
            this.good_import_ratio.text =           string.Format("good_import_ratio  [0000]");
            this.good_import_ratio.tooltip = "good_import_ratio";
            this.good_import_ratio.relativePosition = new Vector3(SPACING, this.grain_export_ratio.relativePosition.y + SPACING22);
            this.good_import_ratio.autoSize = true;
            this.good_import_ratio.name = "Moreeconomic_Text_65";

            this.food_import_ratio = base.AddUIComponent<UILabel>();
            this.food_import_ratio.text =           string.Format("food_import_ratio [00000]");
            this.food_import_ratio.tooltip = "food_import_ratio";
            this.food_import_ratio.relativePosition = new Vector3(this.good_import_ratio.relativePosition.x + this.good_import_ratio.width + SPACING, this.good_import_ratio.relativePosition.y);
            this.food_import_ratio.autoSize = true;
            this.food_import_ratio.name = "Moreeconomic_Text_66";

            this.petrol_import_ratio = base.AddUIComponent<UILabel>();
            this.petrol_import_ratio.text =         string.Format("petrol_import_ratio [000]");
            this.petrol_import_ratio.tooltip = "petrol_import_ratio";
            this.petrol_import_ratio.relativePosition = new Vector3(this.food_import_ratio.relativePosition.x + this.food_import_ratio.width + SPACING, this.food_import_ratio.relativePosition.y);
            this.petrol_import_ratio.autoSize = true;
            this.petrol_import_ratio.name = "Moreeconomic_Text_67";

            this.coal_import_ratio = base.AddUIComponent<UILabel>();
            this.coal_import_ratio.text =           string.Format("coal_import_ratio [00000]");
            this.coal_import_ratio.tooltip = "coal_import_ratio";
            this.coal_import_ratio.relativePosition = new Vector3(SPACING, this.petrol_import_ratio.relativePosition.y + SPACING22);
            this.coal_import_ratio.autoSize = true;
            this.coal_import_ratio.name = "Moreeconomic_Text_68";

            this.lumber_import_ratio = base.AddUIComponent<UILabel>();
            this.lumber_import_ratio.text =         string.Format("lumber_import_ratio [000]");
            this.lumber_import_ratio.tooltip = "lumber_import_ratio";
            this.lumber_import_ratio.relativePosition = new Vector3(this.coal_import_ratio.relativePosition.x + this.coal_import_ratio.width + SPACING, this.coal_import_ratio.relativePosition.y);
            this.lumber_import_ratio.autoSize = true;
            this.lumber_import_ratio.name = "Moreeconomic_Text_69";

            this.oil_import_ratio = base.AddUIComponent<UILabel>();
            this.oil_import_ratio.text =            string.Format("oil_import_ratio [000000]");
            this.oil_import_ratio.tooltip = "oil_import_ratio";
            this.oil_import_ratio.relativePosition = new Vector3(this.lumber_import_ratio.relativePosition.x + this.lumber_import_ratio.width + SPACING, this.lumber_import_ratio.relativePosition.y);
            this.oil_import_ratio.autoSize = true;
            this.oil_import_ratio.name = "Moreeconomic_Text_70";

            this.ore_import_ratio = base.AddUIComponent<UILabel>();
            this.ore_import_ratio.text =            string.Format("ore_import_ratio [000000]");
            this.ore_import_ratio.tooltip = "ore_import_ratio";
            this.ore_import_ratio.relativePosition = new Vector3(SPACING, this.oil_import_ratio.relativePosition.y + SPACING22);
            this.ore_import_ratio.autoSize = true;
            this.ore_import_ratio.name = "Moreeconomic_Text_71";

            this.log_import_ratio = base.AddUIComponent<UILabel>();
            this.log_import_ratio.text =            string.Format("log_import_ratio [000000]");
            this.log_import_ratio.tooltip = "log_import_ratio";
            this.log_import_ratio.relativePosition = new Vector3(this.ore_import_ratio.relativePosition.x + this.ore_import_ratio.width + SPACING, this.ore_import_ratio.relativePosition.y);
            this.log_import_ratio.autoSize = true;
            this.log_import_ratio.name = "Moreeconomic_Text_72";

            this.grain_import_ratio = base.AddUIComponent<UILabel>();
            this.grain_import_ratio.text =          string.Format("grain_import_ratio [00000]");
            this.grain_import_ratio.tooltip = "grain_import_ratio";
            this.grain_import_ratio.relativePosition = new Vector3(this.log_import_ratio.relativePosition.x + this.log_import_ratio.width + SPACING, this.log_import_ratio.relativePosition.y);
            this.grain_import_ratio.autoSize = true;
            this.grain_import_ratio.name = "Moreeconomic_Text_73";


            this.all_comm_building_profit = base.AddUIComponent<UILabel>();
            this.all_comm_building_profit.text =     string.Format("all_comm_building_profit num   [00000]");
            this.all_comm_building_profit.tooltip = "all_comm_building_profit num";
            this.all_comm_building_profit.relativePosition = new Vector3(SPACING, this.grain_import_ratio.relativePosition.y + SPACING22);
            this.all_comm_building_profit.autoSize = true;
            this.all_comm_building_profit.name = "Moreeconomic_Text_25";

            this.all_comm_building_loss = base.AddUIComponent<UILabel>();
            this.all_comm_building_loss.text =       string.Format("all_comm_building_loss num   [0000000]");
            this.all_comm_building_loss.tooltip = "all_comm_building_loss num";
            this.all_comm_building_loss.relativePosition = new Vector3(this.all_comm_building_profit.relativePosition.x + this.all_comm_building_profit.width + SPACING, this.all_comm_building_profit.relativePosition.y);
            this.all_comm_building_loss.autoSize = true;
            this.all_comm_building_loss.name = "Moreeconomic_Text_26";

            this.all_industry_building_profit = base.AddUIComponent<UILabel>();
            this.all_industry_building_profit.text = string.Format("all_indust_building_profit num [00000]");
            this.all_industry_building_profit.tooltip = "all_industry_building_profit";
            this.all_industry_building_profit.relativePosition = new Vector3(SPACING, this.all_comm_building_profit.relativePosition.y + SPACING22);
            this.all_industry_building_profit.autoSize = true;
            this.all_industry_building_profit.name = "Moreeconomic_Text_27";

            this.all_industry_building_loss = base.AddUIComponent<UILabel>();
            this.all_industry_building_loss.text =   string.Format("all_industry_building_loss num [00000]");
            this.all_industry_building_loss.tooltip = "all_industry_building_loss num";
            this.all_industry_building_loss.relativePosition = new Vector3(this.all_industry_building_profit.relativePosition.x + this.all_industry_building_profit.width + SPACING + 10f , this.all_industry_building_profit.relativePosition.y);
            this.all_industry_building_loss.autoSize = true;
            this.all_industry_building_loss.name = "Moreeconomic_Text_28";

            this.all_foresty_building_profit = base.AddUIComponent<UILabel>();
            this.all_foresty_building_profit.text =  string.Format("all_foresty_building_profit num [00000]");
            this.all_foresty_building_profit.tooltip = "all_foresty_building_profit num";
            this.all_foresty_building_profit.relativePosition = new Vector3(SPACING, this.all_industry_building_profit.relativePosition.y + SPACING22);
            this.all_foresty_building_profit.autoSize = true;
            this.all_foresty_building_profit.name = "Moreeconomic_Text_29";

            this.all_foresty_building_loss = base.AddUIComponent<UILabel>();
            this.all_foresty_building_loss.text =    string.Format("all_foresty_building_loss num  [000000]");
            this.all_foresty_building_loss.tooltip = "all_foresty_building_loss num";
            this.all_foresty_building_loss.relativePosition = new Vector3(this.all_foresty_building_profit.relativePosition.x + this.all_foresty_building_profit.width + SPACING -10f , this.all_foresty_building_profit.relativePosition.y);
            this.all_foresty_building_loss.autoSize = true;
            this.all_foresty_building_loss.name = "Moreeconomic_Text_30";

            this.all_farmer_building_profit = base.AddUIComponent<UILabel>();
            this.all_farmer_building_profit.text =   string.Format("all_farmer_building_profit num  [00000]");
            this.all_farmer_building_profit.tooltip = "all_farmer_building_profit num";
            this.all_farmer_building_profit.relativePosition = new Vector3(SPACING, this.all_foresty_building_profit.relativePosition.y + SPACING22);
            this.all_farmer_building_profit.autoSize = true;
            this.all_farmer_building_profit.name = "Moreeconomic_Text_31";

            this.all_farmer_building_loss = base.AddUIComponent<UILabel>();
            this.all_farmer_building_loss.text =     string.Format("all_farming_building_loss num  [000000]");
            this.all_farmer_building_loss.tooltip = "all_farmer_building_loss num";
            this.all_farmer_building_loss.relativePosition = new Vector3(this.all_farmer_building_profit.relativePosition.x + this.all_farmer_building_profit.width + SPACING - 15f, this.all_farmer_building_profit.relativePosition.y);
            this.all_farmer_building_loss.autoSize = true;
            this.all_farmer_building_loss.name = "Moreeconomic_Text_32";

            this.all_oil_building_profit = base.AddUIComponent<UILabel>();
            this.all_oil_building_profit.text =      string.Format("all_oil_building_profit num  [00000000]");
            this.all_oil_building_profit.tooltip = "all_oil_building_profit num";
            this.all_oil_building_profit.relativePosition = new Vector3(SPACING, this.all_farmer_building_profit.relativePosition.y + SPACING22);
            this.all_oil_building_profit.autoSize = true;
            this.all_oil_building_profit.name = "Moreeconomic_Text_33";

            this.all_oil_building_loss = base.AddUIComponent<UILabel>();
            this.all_oil_building_loss.text =        string.Format("all_oil_building_loss num   [000000000]");
            this.all_oil_building_loss.tooltip = "all_oil_building_loss num";
            this.all_oil_building_loss.relativePosition = new Vector3(this.all_oil_building_profit.relativePosition.x + this.all_oil_building_profit.width + SPACING, this.all_oil_building_profit.relativePosition.y);
            this.all_oil_building_loss.autoSize = true;
            this.all_oil_building_loss.name = "Moreeconomic_Text_34";

            this.all_ore_building_profit = base.AddUIComponent<UILabel>();
            this.all_ore_building_profit.text =      string.Format("all_ore_building_profit num  [0000000]");
            this.all_ore_building_profit.tooltip = "all_ore_building_profit num";
            this.all_ore_building_profit.relativePosition = new Vector3(SPACING, this.all_oil_building_profit.relativePosition.y + SPACING22);
            this.all_ore_building_profit.autoSize = true;
            this.all_ore_building_profit.name = "Moreeconomic_Text_35";

            this.all_ore_building_loss = base.AddUIComponent<UILabel>();
            this.all_ore_building_loss.text =        string.Format("all_ore_building_loss num  [000000000]");
            this.all_ore_building_loss.tooltip = "all_ore_building_loss num";
            this.all_ore_building_loss.relativePosition = new Vector3(this.all_ore_building_profit.relativePosition.x + this.all_ore_building_profit.width + SPACING, this.all_ore_building_profit.relativePosition.y);
            this.all_ore_building_loss.autoSize = true;
            this.all_ore_building_loss.name = "Moreeconomic_Text_36";

            this.all_buildings = base.AddUIComponent<UILabel>();
            this.all_buildings.text =             string.Format("all_buildings num [00000000]");
            this.all_buildings.tooltip = "all_buildings num";
            this.all_buildings.relativePosition = new Vector3(SPACING, this.all_ore_building_profit.relativePosition.y + SPACING22);
            this.all_buildings.autoSize = true;
            this.all_buildings.name = "Moreeconomic_Text_37";

            this.total_cargo_vehical_time = base.AddUIComponent<UILabel>();
            this.total_cargo_vehical_time.text =  string.Format("cargo_running_time [0000000]");
            this.total_cargo_vehical_time.tooltip = "total_cargo_vehical_time";
            this.total_cargo_vehical_time.relativePosition = new Vector3(this.all_buildings.relativePosition.x + this.all_buildings.width + SPACING, this.all_buildings.relativePosition.y);
            this.total_cargo_vehical_time.autoSize = true;
            this.total_cargo_vehical_time.name = "Moreeconomic_Text_38";

            this.total_cargo_transfer_size = base.AddUIComponent<UILabel>();
            this.total_cargo_transfer_size.text = string.Format("cargo_transfer_size [000000]");
            this.total_cargo_transfer_size.tooltip = "total_cargo_transfer_size";
            this.total_cargo_transfer_size.relativePosition = new Vector3(this.total_cargo_vehical_time.relativePosition.x + this.total_cargo_vehical_time.width + SPACING, this.total_cargo_vehical_time.relativePosition.y);
            this.total_cargo_transfer_size.autoSize = true;
            this.total_cargo_transfer_size.name = "Moreeconomic_Text_39";

            this.total_train_transfer_size = base.AddUIComponent<UILabel>();
            this.total_train_transfer_size.text = string.Format("train_transfer_size [000000]");
            this.total_train_transfer_size.tooltip = "total_train_transfer_size";
            this.total_train_transfer_size.relativePosition = new Vector3(SPACING, this.all_buildings.relativePosition.y + SPACING22);
            this.total_train_transfer_size.autoSize = true;
            this.total_train_transfer_size.name = "Moreeconomic_Text_40";

            this.total_ship_transfer_size = base.AddUIComponent<UILabel>();
            this.total_ship_transfer_size.text = string.Format("ship_transfer_size [00000000]");
            this.total_ship_transfer_size.tooltip = "total_ship_transfer_size";
            this.total_ship_transfer_size.relativePosition = new Vector3(this.total_train_transfer_size.relativePosition.x + this.total_train_transfer_size.width + SPACING + 5f, this.total_train_transfer_size.relativePosition.y);
            this.total_ship_transfer_size.autoSize = true;
            this.total_ship_transfer_size.name = "Moreeconomic_Text_41";

            this.office_gen_salary_index = base.AddUIComponent<UILabel>();
            this.office_gen_salary_index.text = string.Format("office_gen_salary_index [0000000000]");
            this.office_gen_salary_index.tooltip = "office_gen_salary_index";
            this.office_gen_salary_index.relativePosition = new Vector3(SPACING, this.total_train_transfer_size.relativePosition.y + SPACING22);
            this.office_gen_salary_index.autoSize = true;
            this.office_gen_salary_index.name = "Moreeconomic_Text_42";

            this.office_high_tech_salary_index = base.AddUIComponent<UILabel>();
            this.office_high_tech_salary_index.text = string.Format("office_high_tech_salary_index [0000000000]");
            this.office_high_tech_salary_index.tooltip = "office_high_tech_salary_index";
            this.office_high_tech_salary_index.relativePosition = new Vector3(this.office_gen_salary_index.relativePosition.x + this.office_gen_salary_index.width + SPACING + 10f, this.office_gen_salary_index.relativePosition.y);
            this.office_high_tech_salary_index.autoSize = true;
            this.office_high_tech_salary_index.name = "Moreeconomic_Text_42";

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
                    this.GetDataNeeded();
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
            //citizen
            this.citizen_count.text = string.Format("citizen_count [{0}]", comm_data.citizen_count);
            this.family_count.text = string.Format("family_count [{0}]", comm_data.family_count);
            this.citizen_salary_per_family.text = string.Format("citizen_salary_per_family [{0}]", comm_data.citizen_salary_per_family);
            this.citizen_salary_total.text = string.Format("salary_total [{0}]", comm_data.citizen_salary_total);
            this.citizen_salary_tax_total.text = string.Format("citizen_tax_total [{0}]", comm_data.citizen_salary_tax_total);
            this.citizen_outcome_per_family.text = string.Format("outcome_per_family [{0}]", comm_data.citizen_outcome_per_family);
            this.citizen_outcome.text = string.Format("citizen_outcome [{0}]", comm_data.citizen_outcome);
            this.total_citizen_vehical_time.text = string.Format("citizen_vehical_time [{0}]", comm_data.temp_total_citizen_vehical_time_last);
            this.public_transport_fee.text = string.Format("public_trans_fee [{0}]", comm_data.public_transport_fee);
            this.citizen_average_transport_fee.text = string.Format("average_transport_fee [{0}]", comm_data.citizen_average_transport_fee);
            this.family_profit_money_num.text = string.Format("family_profit_num [{0}]", comm_data.family_profit_money_num);
            this.family_loss_money_num.text = string.Format("family_loss_num [{0}]", comm_data.family_loss_money_num);
            this.family_very_profit_num.text = string.Format("family_very_profit_num [{0}]", comm_data.family_very_profit_money_num);
            this.family_weight_stable_high.text = string.Format("wealth_stable_high [{0}]", comm_data.family_weight_stable_high);
            this.family_weight_stable_low.text = string.Format("wealth_stable_low [{0}]", comm_data.family_weight_stable_low);
            this.resident_consumption_rate.text = string.Format("resident_consumption_rate [{0:N2}]", comm_data.resident_consumption_rate);
            this.tourist_consumption_rate.text = string.Format("outside_consumption_rate [{0:N2}]", comm_data.outside_consumption_rate);

            //building
            this.good_export_ratio.text = string.Format("good_export_ratio [{0}]]", pc_PrivateBuildingAI.good_export_ratio);
            this.food_export_ratio.text = string.Format("food_export_ratio [{0}]]", pc_PrivateBuildingAI.food_export_ratio);
            this.petrol_export_ratio.text = string.Format("petrol_export_ratio [{0}]]", pc_PrivateBuildingAI.petrol_export_ratio);
            this.coal_export_ratio.text = string.Format("coal_export_ratio [{0}]]", pc_PrivateBuildingAI.coal_export_ratio);
            this.lumber_export_ratio.text = string.Format("lumber_export_ratio [{0}]]", pc_PrivateBuildingAI.lumber_export_ratio);
            this.oil_export_ratio.text = string.Format("oil_export_ratio [{0}]]", pc_PrivateBuildingAI.oil_export_ratio);
            this.ore_export_ratio.text = string.Format("ore_export_ratio [{0}]]", pc_PrivateBuildingAI.ore_export_ratio);
            this.log_export_ratio.text = string.Format("log_export_ratio [{0}]]", pc_PrivateBuildingAI.log_export_ratio);
            this.grain_export_ratio.text = string.Format("grain_export_ratio [{0}]]", pc_PrivateBuildingAI.grain_export_ratio);

            this.good_import_ratio.text = string.Format("good_import_ratio [{0}]]", pc_PrivateBuildingAI.good_import_ratio);
            this.food_import_ratio.text = string.Format("food_import_ratio [{0}]]", pc_PrivateBuildingAI.food_import_ratio);
            this.petrol_import_ratio.text = string.Format("petrol_import_ratio [{0}]]", pc_PrivateBuildingAI.petrol_import_ratio);
            this.coal_import_ratio.text = string.Format("coal_import_ratio [{0}]]", pc_PrivateBuildingAI.coal_import_ratio);
            this.lumber_import_ratio.text = string.Format("lumber_import_ratio [{0}]]", pc_PrivateBuildingAI.lumber_import_ratio);
            this.oil_import_ratio.text = string.Format("oil_import_ratio [{0}]]", pc_PrivateBuildingAI.oil_import_ratio);
            this.ore_import_ratio.text = string.Format("ore_import_ratio [{0}]]", pc_PrivateBuildingAI.ore_import_ratio);
            this.log_import_ratio.text = string.Format("log_import_ratio [{0}]]", pc_PrivateBuildingAI.log_import_ratio);
            this.grain_import_ratio.text = string.Format("grain_import_ratio [{0}]]", pc_PrivateBuildingAI.grain_import_ratio);



            this.all_comm_building_profit.text = string.Format("all_comm_building_profit num [{0}]", pc_PrivateBuildingAI.all_comm_building_profit_final);
            this.all_comm_building_loss.text = string.Format("all_comm_building_loss num [{0}]", pc_PrivateBuildingAI.all_comm_building_loss_final);
            this.all_industry_building_profit.text = string.Format("all_industry_building_profit num [{0}]", pc_PrivateBuildingAI.all_industry_building_profit_final);
            this.all_industry_building_loss.text = string.Format("all_industry_building_loss num [{0}]", pc_PrivateBuildingAI.all_industry_building_loss_final);
            this.all_foresty_building_profit.text = string.Format("all_foresty_building_profit num [{0}]", pc_PrivateBuildingAI.all_foresty_building_profit_final);
            this.all_foresty_building_loss.text = string.Format("all_foresty_building_loss num [{0}]", pc_PrivateBuildingAI.all_foresty_building_loss_final);
            this.all_farmer_building_profit.text = string.Format("all_farmer_building_profit num [{0}]", pc_PrivateBuildingAI.all_farmer_building_profit_final);
            this.all_farmer_building_loss.text = string.Format("all_farmer_building_loss num [{0}]", pc_PrivateBuildingAI.all_farmer_building_loss_final);
            this.all_oil_building_profit.text = string.Format("all_oil_building_profit num [{0}]", pc_PrivateBuildingAI.all_oil_building_profit_final);
            this.all_oil_building_loss.text = string.Format("all_oil_building_loss num [{0}]", pc_PrivateBuildingAI.all_oil_building_loss_final);
            this.all_ore_building_profit.text = string.Format("all_ore_building_profit num [{0}]", pc_PrivateBuildingAI.all_ore_building_profit_final);
            this.all_ore_building_loss.text = string.Format("all_ore_building_loss num [{0}]", pc_PrivateBuildingAI.all_ore_building_loss_final);
            this.all_buildings.text = string.Format("all_buildings num [{0}]", pc_PrivateBuildingAI.all_buildings_final);
            this.total_cargo_vehical_time.text = string.Format("total_cargo_vehical_time [{0}]", pc_PrivateBuildingAI.total_cargo_vehical_time);
            this.total_cargo_transfer_size.text = string.Format("total_cargo_transfer_size [{0}K]", (float)(pc_PrivateBuildingAI.total_cargo_transfer_size/1000));
            this.total_train_transfer_size.text = string.Format("total_train_transfer_size [{0}K]", (float)(pc_PrivateBuildingAI.total_train_transfer_size / 1000));
            this.total_ship_transfer_size.text = string.Format("total_ship_transfer_size [{0}K]", (float)(pc_PrivateBuildingAI.total_ship_transfer_size / 1000));
            this.office_gen_salary_index.text = string.Format("office_gen_salary_index [{0}]", pc_PrivateBuildingAI.office_gen_salary_index);
            this.office_high_tech_salary_index.text = string.Format("office_high_tech_salary_index [{0}]", pc_PrivateBuildingAI.office_high_tech_salary_index);
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
    }
}
