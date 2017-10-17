using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using UnityEngine;
using System.Collections;

namespace RealCity
{
    public class MoreeconomicGUI : UIPanel
    {
        public static readonly string cacheName = "RealCityGUI";

        private static readonly float WIDTH = 800f;

        private static readonly float HEIGHT = 500f;

        private static readonly float HEADER = 40f;

        private static readonly float SPACING = 15f;

        private static readonly float SPACING22 = 22f;

        private ItemClass.Availability CurrentMode;

        public static MoreeconomicGUI instance;

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
        private UILabel comm_profit;
        private UILabel indu_profit;
        private UILabel food_profit;
        private UILabel petrol_profit;
        private UILabel coal_profit;
        private UILabel lumber_profit;
        private UILabel oil_profit;
        private UILabel ore_profit;
        private UILabel log_profit;
        private UILabel grain_profit;

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
            MoreeconomicGUI.instance = this;
            base.size = new Vector2(MoreeconomicGUI.WIDTH, MoreeconomicGUI.HEIGHT);
            base.backgroundSprite = "MenuPanel";
            this.canFocus = true;
            this.isInteractive = true;
            this.BringToFront();
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 - 200), (float)(Loader.parentGuiView.fixedHeight / 2 - 350));
            base.opacity = 1f;
            base.cachedName = MoreeconomicGUI.cacheName;
            this.CurrentMode = Singleton<ToolManager>.instance.m_properties.m_mode;
            this.m_DragHandler = base.AddUIComponent<UIDragHandle>();
            this.m_DragHandler.target = this;
            this.m_title = base.AddUIComponent<UILabel>();
            this.m_title.text = "economic Data";
            this.m_title.relativePosition = new Vector3(MoreeconomicGUI.WIDTH / 2f - this.m_title.width / 2f - 25f, MoreeconomicGUI.HEADER / 2f - this.m_title.height / 2f);
            this.m_title.textAlignment = UIHorizontalAlignment.Center;
            this.m_closeButton = base.AddUIComponent<UIButton>();
            this.m_closeButton.normalBgSprite = "buttonclose";
            this.m_closeButton.hoveredBgSprite = "buttonclosehover";
            this.m_closeButton.pressedBgSprite = "buttonclosepressed";
            this.m_closeButton.relativePosition = new Vector3(MoreeconomicGUI.WIDTH - 35f, 5f, 10f);
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
            this.m_HeaderDataText.relativePosition = new Vector3(MoreeconomicGUI.SPACING, 50f);
            this.m_HeaderDataText.autoSize = true;

            //citizen
            this.m_firstline_citizen = base.AddUIComponent<UILabel>();
            this.m_firstline_citizen.text = "citizen status";
            this.m_firstline_citizen.tooltip = "N/A";
            this.m_firstline_citizen.relativePosition = new Vector3(MoreeconomicGUI.SPACING, this.m_HeaderDataText.relativePosition.y + MoreeconomicGUI.SPACING22);
            this.m_firstline_citizen.autoSize = true;

            //data
            this.citizen_count = base.AddUIComponent<UILabel>();
            this.citizen_count.text =               string.Format("citizen_count [0000000]");
            this.citizen_count.tooltip = "total citizen_count";
            this.citizen_count.relativePosition = new Vector3(MoreeconomicGUI.SPACING, this.m_firstline_citizen.relativePosition.y + MoreeconomicGUI.SPACING22);
            this.citizen_count.autoSize = true;
            this.citizen_count.name = "Moreeconomic_Text_0";

            this.family_count = base.AddUIComponent<UILabel>();
            this.family_count.text =                 string.Format("family_count [0000000]");
            this.family_count.tooltip = "total family_count";
            this.family_count.relativePosition = new Vector3(this.citizen_count.relativePosition.x + this.citizen_count.width + MoreeconomicGUI.SPACING, this.citizen_count.relativePosition.y);
            this.family_count.autoSize = true;
            this.family_count.name = "Moreeconomic_Text_1";

            this.citizen_salary_per_family = base.AddUIComponent<UILabel>();
            this.citizen_salary_per_family.text =    string.Format("citizen_salary_per_family [000]");
            this.citizen_salary_per_family.tooltip = "citizen_salary_per_family";
            this.citizen_salary_per_family.relativePosition = new Vector3(this.family_count.relativePosition.x + this.family_count.width + MoreeconomicGUI.SPACING, this.family_count.relativePosition.y);
            this.citizen_salary_per_family.autoSize = true;
            this.citizen_salary_per_family.name = "Moreeconomic_Text_2";

            this.citizen_salary_total = base.AddUIComponent<UILabel>();
            this.citizen_salary_total.text =         string.Format("salary_total [00000000]");
            this.citizen_salary_total.tooltip = "total citizen_salary";
            this.citizen_salary_total.relativePosition = new Vector3(MoreeconomicGUI.SPACING, this.citizen_count.relativePosition.y + MoreeconomicGUI.SPACING22);
            //this.m_money_forest.relativePosition = new Vector3(this.m_money_farmer.relativePosition.x + this.m_money_farmer.width + MoreeconomicGUI.SPACING, this.m_money_farmer.relativePosition.y);
            this.citizen_salary_total.autoSize = true;
            this.citizen_salary_total.name = "Moreeconomic_Text_3";

            this.citizen_salary_tax_total = base.AddUIComponent<UILabel>();
            this.citizen_salary_tax_total.text =     string.Format("citizen_tax_total [000]");
            this.citizen_salary_tax_total.tooltip = "total citizen_salary_tax";
            this.citizen_salary_tax_total.relativePosition = new Vector3(this.citizen_salary_total.relativePosition.x + this.citizen_salary_total.width + MoreeconomicGUI.SPACING, this.citizen_salary_total.relativePosition.y);
            this.citizen_salary_tax_total.autoSize = true;
            this.citizen_salary_tax_total.name = "Moreeconomic_Text_4";

            this.citizen_outcome_per_family = base.AddUIComponent<UILabel>();
            this.citizen_outcome_per_family.text =   string.Format("outcome_per_family [0000000000]");
            this.citizen_outcome_per_family.tooltip = "citizen_outcome_per_family";
            this.citizen_outcome_per_family.relativePosition = new Vector3(this.citizen_salary_tax_total.relativePosition.x + this.citizen_salary_tax_total.width + MoreeconomicGUI.SPACING, this.citizen_salary_tax_total.relativePosition.y);
            this.citizen_outcome_per_family.autoSize = true;
            this.citizen_outcome_per_family.name = "Moreeconomic_Text_5";

            this.citizen_outcome = base.AddUIComponent<UILabel>();
            this.citizen_outcome.text =              string.Format("citizen_outcome [0000]");
            this.citizen_outcome.tooltip = "total citizen_outcome";
            this.citizen_outcome.relativePosition = new Vector3(MoreeconomicGUI.SPACING, this.citizen_salary_total.relativePosition.y + MoreeconomicGUI.SPACING22);
            this.citizen_outcome.autoSize = true;
            this.citizen_outcome.name = "Moreeconomic_Text_6";

            this.public_transport_fee = base.AddUIComponent<UILabel>();
            this.public_transport_fee.text =         string.Format("public_trans_fee [000]");
            this.public_transport_fee.tooltip = "public_transport_fee";
            this.public_transport_fee.relativePosition = new Vector3(this.citizen_outcome.relativePosition.x + this.citizen_outcome.width + MoreeconomicGUI.SPACING + 2f, this.citizen_outcome.relativePosition.y);
            this.public_transport_fee.autoSize = true;
            this.public_transport_fee.name = "Moreeconomic_Text_7";

            this.total_citizen_vehical_time = base.AddUIComponent<UILabel>();
            this.total_citizen_vehical_time.text =   string.Format("citizen_vehical_time [00000000]");
            this.total_citizen_vehical_time.tooltip = "total citizen_vehical_time";
            this.total_citizen_vehical_time.relativePosition = new Vector3(this.public_transport_fee.relativePosition.x + this.public_transport_fee.width + MoreeconomicGUI.SPACING, this.public_transport_fee.relativePosition.y);
            this.total_citizen_vehical_time.autoSize = true;
            this.total_citizen_vehical_time.name = "Moreeconomic_Text_8";

            this.family_profit_money_num = base.AddUIComponent<UILabel>();
            this.family_profit_money_num.text =      string.Format("family_profit_num [000]");
            this.family_profit_money_num.tooltip = "total family_profit_money_num";
            this.family_profit_money_num.relativePosition = new Vector3(MoreeconomicGUI.SPACING, this.citizen_outcome.relativePosition.y + MoreeconomicGUI.SPACING22);
            this.family_profit_money_num.autoSize = true;
            this.family_profit_money_num.name = "Moreeconomic_Text_9";

            this.family_loss_money_num = base.AddUIComponent<UILabel>();
            this.family_loss_money_num.text =        string.Format("family_loss_num [00000]");
            this.family_loss_money_num.tooltip = "family_loss_money_num";
            this.family_loss_money_num.relativePosition = new Vector3(this.family_profit_money_num.relativePosition.x + this.family_profit_money_num.width + MoreeconomicGUI.SPACING, this.family_profit_money_num.relativePosition.y);
            this.family_loss_money_num.autoSize = true;
            this.family_loss_money_num.name = "Moreeconomic_Text_10";

            this.citizen_average_transport_fee = base.AddUIComponent<UILabel>();
            this.citizen_average_transport_fee.text = string.Format("average_transport_fee [000000]");
            this.citizen_average_transport_fee.tooltip = "citizen_average_transport_fee";
            this.citizen_average_transport_fee.relativePosition = new Vector3(this.family_loss_money_num.relativePosition.x + this.family_loss_money_num.width + MoreeconomicGUI.SPACING, this.family_loss_money_num.relativePosition.y);
            //this.m_money_forest.relativePosition = new Vector3(this.m_money_farmer.relativePosition.x + this.m_money_farmer.width + MoreeconomicGUI.SPACING, this.m_money_farmer.relativePosition.y);
            this.citizen_average_transport_fee.autoSize = true;
            this.citizen_average_transport_fee.name = "Moreeconomic_Text_11";







            //building
            this.m_secondline_building = base.AddUIComponent<UILabel>();
            this.m_secondline_building.text = "building status";
            this.m_secondline_building.tooltip = "N/A";
            this.m_secondline_building.relativePosition = new Vector3(MoreeconomicGUI.SPACING, this.citizen_average_transport_fee.relativePosition.y + MoreeconomicGUI.SPACING22);
            this.m_secondline_building.autoSize = true;

            this.comm_profit = base.AddUIComponent<UILabel>();
            this.comm_profit.text =           string.Format("commerical_profit  [00000000]");
            this.comm_profit.tooltip = "commerical_profit";
            this.comm_profit.relativePosition = new Vector3(MoreeconomicGUI.SPACING, this.m_secondline_building.relativePosition.y + MoreeconomicGUI.SPACING22);
            this.comm_profit.autoSize = true;
            this.comm_profit.name = "Moreeconomic_Text_12";

            this.indu_profit = base.AddUIComponent<UILabel>();
            this.indu_profit.text =           string.Format("industry_profit [000000000]");
            this.indu_profit.tooltip = "industry_profit";
            this.indu_profit.relativePosition = new Vector3(this.comm_profit.relativePosition.x + this.comm_profit.width + MoreeconomicGUI.SPACING, this.comm_profit.relativePosition.y);
            this.indu_profit.autoSize = true;
            this.indu_profit.name = "Moreeconomic_Text_13";

            this.food_profit = base.AddUIComponent<UILabel>();
            this.food_profit.text =           string.Format("food_profit [0000000000000]");
            this.food_profit.tooltip = "food_profit";
            this.food_profit.relativePosition = new Vector3(this.indu_profit.relativePosition.x + this.indu_profit.width + MoreeconomicGUI.SPACING, this.indu_profit.relativePosition.y);
            this.food_profit.autoSize = true;
            this.food_profit.name = "Moreeconomic_Text_14";

            this.petrol_profit = base.AddUIComponent<UILabel>();
            this.petrol_profit.text =         string.Format("petrol_profit [00000000000]");
            this.petrol_profit.tooltip = "petrol_profit";
            this.petrol_profit.relativePosition = new Vector3(MoreeconomicGUI.SPACING, this.comm_profit.relativePosition.y + MoreeconomicGUI.SPACING22);
            this.petrol_profit.autoSize = true;
            this.petrol_profit.name = "Moreeconomic_Text_15";

            this.coal_profit = base.AddUIComponent<UILabel>();
            this.coal_profit.text =           string.Format("coal_profit [00000000000000]");
            this.coal_profit.tooltip = "coal_profit";
            this.coal_profit.relativePosition = new Vector3(this.petrol_profit.relativePosition.x + this.petrol_profit.width + MoreeconomicGUI.SPACING, this.petrol_profit.relativePosition.y);
            this.coal_profit.autoSize = true;
            this.coal_profit.name = "Moreeconomic_Text_16";

            this.lumber_profit = base.AddUIComponent<UILabel>();
            this.lumber_profit.text =         string.Format("lumber_profit [000000000000]");
            this.lumber_profit.tooltip = "lumber_profit";
            this.lumber_profit.relativePosition = new Vector3(this.coal_profit.relativePosition.x + this.coal_profit.width + MoreeconomicGUI.SPACING, this.coal_profit.relativePosition.y);
            this.lumber_profit.autoSize = true;
            this.lumber_profit.name = "Moreeconomic_Text_17";

            this.oil_profit = base.AddUIComponent<UILabel>();
            this.oil_profit.text =            string.Format("oil_profit   [000000]");
            this.oil_profit.tooltip = "oil_profit";
            this.oil_profit.relativePosition = new Vector3(MoreeconomicGUI.SPACING, this.petrol_profit.relativePosition.y + MoreeconomicGUI.SPACING22);
            this.oil_profit.autoSize = true;
            this.oil_profit.name = "Moreeconomic_Text_18";

            this.ore_profit = base.AddUIComponent<UILabel>();
            this.ore_profit.text =            string.Format("ore_profit  [0000000]");
            this.ore_profit.tooltip = "ore_profit";
            this.ore_profit.relativePosition = new Vector3(this.oil_profit.relativePosition.x + this.oil_profit.width + MoreeconomicGUI.SPACING, this.oil_profit.relativePosition.y);
            this.ore_profit.autoSize = true;
            this.ore_profit.name = "Moreeconomic_Text_19";

            this.log_profit = base.AddUIComponent<UILabel>();
            this.log_profit.text =            string.Format("log_profit  [0000000]");
            this.log_profit.tooltip = "log_profit";
            this.log_profit.relativePosition = new Vector3(this.ore_profit.relativePosition.x + this.ore_profit.width + MoreeconomicGUI.SPACING, this.ore_profit.relativePosition.y);
            this.log_profit.autoSize = true;
            this.log_profit.name = "Moreeconomic_Text_20";

            this.grain_profit = base.AddUIComponent<UILabel>();
            this.grain_profit.text =          string.Format("grain_profit  [00000]");
            this.grain_profit.tooltip = "grain_profit";
            this.grain_profit.relativePosition = new Vector3(this.log_profit.relativePosition.x + this.log_profit.width + MoreeconomicGUI.SPACING, this.log_profit.relativePosition.y);
            this.grain_profit.autoSize = true;
            this.grain_profit.name = "Moreeconomic_Text_21";

            this.all_comm_building_profit = base.AddUIComponent<UILabel>();
            this.all_comm_building_profit.text =     string.Format("all_comm_building_profit num   [00000]");
            this.all_comm_building_profit.tooltip = "all_comm_building_profit num";
            this.all_comm_building_profit.relativePosition = new Vector3(MoreeconomicGUI.SPACING, this.oil_profit.relativePosition.y + MoreeconomicGUI.SPACING22);
            this.all_comm_building_profit.autoSize = true;
            this.all_comm_building_profit.name = "Moreeconomic_Text_22";

            this.all_comm_building_loss = base.AddUIComponent<UILabel>();
            this.all_comm_building_loss.text =       string.Format("all_comm_building_loss num   [0000000]");
            this.all_comm_building_loss.tooltip = "all_comm_building_loss num";
            this.all_comm_building_loss.relativePosition = new Vector3(this.all_comm_building_profit.relativePosition.x + this.all_comm_building_profit.width + MoreeconomicGUI.SPACING, this.all_comm_building_profit.relativePosition.y);
            this.all_comm_building_loss.autoSize = true;
            this.all_comm_building_loss.name = "Moreeconomic_Text_23";

            this.all_industry_building_profit = base.AddUIComponent<UILabel>();
            this.all_industry_building_profit.text = string.Format("all_indust_building_profit num [00000]");
            this.all_industry_building_profit.tooltip = "all_industry_building_profit";
            this.all_industry_building_profit.relativePosition = new Vector3(MoreeconomicGUI.SPACING, this.all_comm_building_profit.relativePosition.y + MoreeconomicGUI.SPACING22);
            this.all_industry_building_profit.autoSize = true;
            this.all_industry_building_profit.name = "Moreeconomic_Text_24";

            this.all_industry_building_loss = base.AddUIComponent<UILabel>();
            this.all_industry_building_loss.text =   string.Format("all_industry_building_loss num [00000]");
            this.all_industry_building_loss.tooltip = "all_industry_building_loss num";
            this.all_industry_building_loss.relativePosition = new Vector3(this.all_industry_building_profit.relativePosition.x + this.all_industry_building_profit.width + MoreeconomicGUI.SPACING, this.all_industry_building_profit.relativePosition.y);
            this.all_industry_building_loss.autoSize = true;
            this.all_industry_building_loss.name = "Moreeconomic_Text_25";

            this.all_foresty_building_profit = base.AddUIComponent<UILabel>();
            this.all_foresty_building_profit.text =  string.Format("all_foresty_building_profit num [00000]");
            this.all_foresty_building_profit.tooltip = "all_foresty_building_profit num";
            this.all_foresty_building_profit.relativePosition = new Vector3(MoreeconomicGUI.SPACING, this.all_industry_building_profit.relativePosition.y + MoreeconomicGUI.SPACING22);
            this.all_foresty_building_profit.autoSize = true;
            this.all_foresty_building_profit.name = "Moreeconomic_Text_26";

            this.all_foresty_building_loss = base.AddUIComponent<UILabel>();
            this.all_foresty_building_loss.text =    string.Format("all_foresty_building_loss num  [000000]");
            this.all_foresty_building_loss.tooltip = "all_foresty_building_loss num";
            this.all_foresty_building_loss.relativePosition = new Vector3(this.all_foresty_building_profit.relativePosition.x + this.all_foresty_building_profit.width + MoreeconomicGUI.SPACING, this.all_foresty_building_profit.relativePosition.y);
            this.all_foresty_building_loss.autoSize = true;
            this.all_foresty_building_loss.name = "Moreeconomic_Text_27";

            this.all_farmer_building_profit = base.AddUIComponent<UILabel>();
            this.all_farmer_building_profit.text =   string.Format("all_farmer_building_profit num  [00000]");
            this.all_farmer_building_profit.tooltip = "all_farmer_building_profit num";
            this.all_farmer_building_profit.relativePosition = new Vector3(MoreeconomicGUI.SPACING, this.all_foresty_building_profit.relativePosition.y + MoreeconomicGUI.SPACING22);
            this.all_farmer_building_profit.autoSize = true;
            this.all_farmer_building_profit.name = "Moreeconomic_Text_28";

            this.all_farmer_building_loss = base.AddUIComponent<UILabel>();
            this.all_farmer_building_loss.text =     string.Format("all_farming_building_loss num  [000000]");
            this.all_farmer_building_loss.tooltip = "all_farmer_building_loss num";
            this.all_farmer_building_loss.relativePosition = new Vector3(this.all_farmer_building_profit.relativePosition.x + this.all_farmer_building_profit.width + MoreeconomicGUI.SPACING, this.all_farmer_building_profit.relativePosition.y);
            this.all_farmer_building_loss.autoSize = true;
            this.all_farmer_building_loss.name = "Moreeconomic_Text_29";

            this.all_oil_building_profit = base.AddUIComponent<UILabel>();
            this.all_oil_building_profit.text =      string.Format("all_oil_building_profit num  [00000000]");
            this.all_oil_building_profit.tooltip = "all_oil_building_profit num";
            this.all_oil_building_profit.relativePosition = new Vector3(MoreeconomicGUI.SPACING, this.all_farmer_building_profit.relativePosition.y + MoreeconomicGUI.SPACING22);
            this.all_oil_building_profit.autoSize = true;
            this.all_oil_building_profit.name = "Moreeconomic_Text_30";

            this.all_oil_building_loss = base.AddUIComponent<UILabel>();
            this.all_oil_building_loss.text =        string.Format("all_oil_building_loss num   [000000000]");
            this.all_oil_building_loss.tooltip = "all_oil_building_loss num";
            this.all_oil_building_loss.relativePosition = new Vector3(this.all_oil_building_profit.relativePosition.x + this.all_oil_building_profit.width + MoreeconomicGUI.SPACING, this.all_oil_building_profit.relativePosition.y);
            this.all_oil_building_loss.autoSize = true;
            this.all_oil_building_loss.name = "Moreeconomic_Text_31";

            this.all_ore_building_profit = base.AddUIComponent<UILabel>();
            this.all_ore_building_profit.text =      string.Format("all_ore_building_profit num  [0000000]");
            this.all_ore_building_profit.tooltip = "all_ore_building_profit num";
            this.all_ore_building_profit.relativePosition = new Vector3(MoreeconomicGUI.SPACING, this.all_oil_building_profit.relativePosition.y + MoreeconomicGUI.SPACING22);
            this.all_ore_building_profit.autoSize = true;
            this.all_ore_building_profit.name = "Moreeconomic_Text_32";

            this.all_ore_building_loss = base.AddUIComponent<UILabel>();
            this.all_ore_building_loss.text =        string.Format("all_ore_building_loss num  [000000000]");
            this.all_ore_building_loss.tooltip = "all_ore_building_loss num";
            this.all_ore_building_loss.relativePosition = new Vector3(this.all_ore_building_profit.relativePosition.x + this.all_ore_building_profit.width + MoreeconomicGUI.SPACING, this.all_ore_building_profit.relativePosition.y);
            this.all_ore_building_loss.autoSize = true;
            this.all_ore_building_loss.name = "Moreeconomic_Text_33";

            this.all_buildings = base.AddUIComponent<UILabel>();
            this.all_buildings.text =             string.Format("all_buildings num [00000000]");
            this.all_buildings.tooltip = "all_buildings num";
            this.all_buildings.relativePosition = new Vector3(MoreeconomicGUI.SPACING, this.all_ore_building_profit.relativePosition.y + MoreeconomicGUI.SPACING22);
            this.all_buildings.autoSize = true;
            this.all_buildings.name = "Moreeconomic_Text_34";

            this.total_cargo_vehical_time = base.AddUIComponent<UILabel>();
            this.total_cargo_vehical_time.text =  string.Format("cargo_running_time [0000000]");
            this.total_cargo_vehical_time.tooltip = "total_cargo_vehical_time";
            this.total_cargo_vehical_time.relativePosition = new Vector3(this.all_buildings.relativePosition.x + this.all_buildings.width + MoreeconomicGUI.SPACING, this.all_buildings.relativePosition.y);
            this.total_cargo_vehical_time.autoSize = true;
            this.total_cargo_vehical_time.name = "Moreeconomic_Text_35";

            this.total_cargo_transfer_size = base.AddUIComponent<UILabel>();
            this.total_cargo_transfer_size.text = string.Format("cargo_transfer_size [000000]");
            this.total_cargo_transfer_size.tooltip = "total_cargo_transfer_size";
            this.total_cargo_transfer_size.relativePosition = new Vector3(this.total_cargo_vehical_time.relativePosition.x + this.total_cargo_vehical_time.width + MoreeconomicGUI.SPACING, this.total_cargo_vehical_time.relativePosition.y);
            this.total_cargo_transfer_size.autoSize = true;
            this.total_cargo_transfer_size.name = "Moreeconomic_Text_36";

            this.total_train_transfer_size = base.AddUIComponent<UILabel>();
            this.total_train_transfer_size.text = string.Format("train_transfer_size [000000]");
            this.total_train_transfer_size.tooltip = "total_train_transfer_size";
            this.total_train_transfer_size.relativePosition = new Vector3(MoreeconomicGUI.SPACING, this.all_buildings.relativePosition.y + MoreeconomicGUI.SPACING22);
            this.total_train_transfer_size.autoSize = true;
            this.total_train_transfer_size.name = "Moreeconomic_Text_37";

            this.total_ship_transfer_size = base.AddUIComponent<UILabel>();
            this.total_ship_transfer_size.text = string.Format("ship_transfer_size [00000000]");
            this.total_ship_transfer_size.tooltip = "total_ship_transfer_size";
            this.total_ship_transfer_size.relativePosition = new Vector3(this.total_train_transfer_size.relativePosition.x + this.total_train_transfer_size.width + MoreeconomicGUI.SPACING, this.total_train_transfer_size.relativePosition.y);
            this.total_ship_transfer_size.autoSize = true;
            this.total_ship_transfer_size.name = "Moreeconomic_Text_38";

            //this.m_getfromBank = base.AddUIComponent<UIButton>();
            //this.m_getfromBank.size = new Vector2(160f, 24f);
            //this.m_getfromBank.text = "Get 100K from Bank";
            //this.m_getfromBank.tooltip = "Get 100K from Bank, if Bank do not have enough money, this is useless";
            //this.m_getfromBank.textScale = 0.875f;
            //this.m_getfromBank.normalBgSprite = "ButtonMenu";
            //this.m_getfromBank.hoveredBgSprite = "ButtonMenuHovered";
            //this.m_getfromBank.pressedBgSprite = "ButtonMenuPressed";
            //this.m_getfromBank.disabledBgSprite = "ButtonMenuDisabled";
            //this.m_getfromBank.relativePosition = new Vector3(MoreeconomicGUI.SPACING, this.m_deadcount.relativePosition.y + MoreeconomicGUI.SPACING22);
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
            //this.m_add2Bank.relativePosition = this.m_getfromBank.relativePosition + new Vector3(this.m_getfromBank.width + MoreeconomicGUI.SPACING * 2f, 0);
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
                while (!MoreeconomicGUI.isRefreshing && base.isVisible)
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
            MoreeconomicGUI.isRefreshing = true;
            //citizen
            this.citizen_count.text = string.Format("citizen_count [{0}]]", comm_data.citizen_count);
            this.family_count.text = string.Format("family_count [{0}]]", comm_data.family_count);
            this.citizen_salary_per_family.text = string.Format("citizen_salary_per_family [{0}]]", comm_data.citizen_salary_per_family);
            this.citizen_salary_total.text = string.Format("salary_total [{0}]]", comm_data.citizen_salary_total + comm_data.citizen_salary_total);
            this.citizen_salary_tax_total.text = string.Format("citizen_tax_total [{0}]]", comm_data.citizen_salary_tax_total);
            this.citizen_outcome_per_family.text = string.Format("outcome_per_family [{0}]]", comm_data.citizen_outcome_per_family);
            this.citizen_outcome.text = string.Format("citizen_outcome [{0}]]", comm_data.citizen_outcome);
            this.total_citizen_vehical_time.text = string.Format("citizen_vehical_time [{0}]]", comm_data.total_citizen_vehical_time);
            this.public_transport_fee.text = string.Format("public_transport_fee [{0}]]", comm_data.public_transport_fee);
            this.citizen_average_transport_fee.text = string.Format("average_transport_fee [{0}]]", comm_data.citizen_average_transport_fee);
            this.family_profit_money_num.text = string.Format("family_profit_num [{0}]]", comm_data.family_profit_money_num);
            this.family_loss_money_num.text = string.Format("family_loss_num [{0}]]", comm_data.family_loss_money_num);

            //building
            this.comm_profit.text = string.Format("commerical_profit [{0}]]", comm_data.comm_profit);
            this.indu_profit.text = string.Format("industry_profit [{0}]]", comm_data.indu_profit);
            this.food_profit.text = string.Format("food_profit [{0}]]", comm_data.food_profit);
            this.petrol_profit.text = string.Format("petrol_profit [{0}]]", comm_data.petrol_profit);
            this.coal_profit.text = string.Format("coal_profit [{0}]]", comm_data.coal_profit);
            this.lumber_profit.text = string.Format("lumber_profit [{0}]]", comm_data.lumber_profit);
            this.oil_profit.text = string.Format("oil_profit [{0}]]", comm_data.oil_profit);
            this.ore_profit.text = string.Format("ore_profit [{0}]]", comm_data.ore_profit);
            this.log_profit.text = string.Format("log_profit [{0}]]", comm_data.log_profit);
            this.grain_profit.text = string.Format("grain_profit [{0}]]", comm_data.grain_profit);
            this.all_comm_building_profit.text = string.Format("all_comm_building_profit num [{0}]]", comm_data.all_comm_building_profit);
            this.all_comm_building_loss.text = string.Format("all_comm_building_loss num [{0}]]", comm_data.all_comm_building_loss);
            this.all_industry_building_profit.text = string.Format("all_industry_building_profit num [{0}]]", comm_data.all_industry_building_profit);
            this.all_industry_building_loss.text = string.Format("all_industry_building_loss num [{0}]]", comm_data.all_industry_building_loss);
            this.all_foresty_building_profit.text = string.Format("all_foresty_building_profit num [{0}]]", comm_data.all_foresty_building_profit);
            this.all_foresty_building_loss.text = string.Format("all_foresty_building_loss num [{0}]]", comm_data.all_foresty_building_loss);
            this.all_farmer_building_profit.text = string.Format("all_farmer_building_profit num [{0}]]", comm_data.all_farmer_building_profit);
            this.all_farmer_building_loss.text = string.Format("all_farmer_building_loss num [{0}]]", comm_data.all_farmer_building_loss);
            this.all_oil_building_profit.text = string.Format("all_oil_building_profit num [{0}]]", comm_data.all_oil_building_profit);
            this.all_oil_building_loss.text = string.Format("all_oil_building_loss num [{0}]]", comm_data.all_oil_building_loss);
            this.all_ore_building_profit.text = string.Format("all_ore_building_profit num [{0}]]", comm_data.all_ore_building_profit);
            this.all_ore_building_loss.text = string.Format("all_ore_building_loss num [{0}]]", comm_data.all_ore_building_loss);
            this.all_buildings.text = string.Format("all_buildings num [{0}]]", comm_data.all_buildings);
            this.total_cargo_vehical_time.text = string.Format("total_cargo_vehical_time [{0}]]", comm_data.total_cargo_vehical_time);
            this.total_cargo_transfer_size.text = string.Format("total_cargo_transfer_size [{0}K]", (float)(comm_data.total_cargo_transfer_size/1000));
            this.total_train_transfer_size.text = string.Format("total_train_transfer_size [{0}K]", (float)(comm_data.total_train_transfer_size / 1000));
            this.total_ship_transfer_size.text = string.Format("total_ship_transfer_size [{0}K]", (float)(comm_data.total_ship_transfer_size / 1000));
            MoreeconomicGUI.isRefreshing = false;
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
