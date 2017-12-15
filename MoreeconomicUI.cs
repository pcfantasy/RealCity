﻿using System.Collections.Generic;
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

        private static readonly float HEIGHT = 950f;

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


        //1.2 citizen expense
        //1.2.1 citizen expense
        //public static long citizen_expense_per_family = 0;
        //public static long citizen_expense = 0;
        private UILabel citizen_expense_per_family;
        private UILabel citizen_expense;

        //1.2.2 transport fee
        //public static uint total_citizen_vehical_time = 0;
        //public static long public_transport_fee = 0;
        //public static long all_transport_fee = 0;
        //public static byte citizen_average_transport_fee = 0;
        private UILabel total_citizen_vehical_time;
        private UILabel public_transport_fee;
        private UILabel citizen_average_transport_fee;

        //1.3 income - expense
        //public static int family_profit_money_num = 0;
        //public static int family_loss_money_num = 0;
        private UILabel family_profit_money_num;
        private UILabel family_loss_money_num;
        private UILabel family_very_profit_num;
        private UILabel family_weight_stable_high;
        private UILabel family_weight_stable_low;

        private UILabel resident_consumption_rate;
        private UILabel tourist_consumption_rate;

        private UILabel city_insurance_account;

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

        //2.2 building expense
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

        private UILabel office_gen_salary_index;
        private UILabel office_high_tech_salary_index;

        //3.outside
        private UILabel m_thirdline_outside; //fixed title
        private UILabel m_outside_garbage;
        private UILabel m_outside_dead;
        private UILabel m_outside_crime;
        private UILabel m_outside_sick;
        private UILabel m_outside_road;
        private UILabel m_outside_firestation;

        private UILabel m_hospital;
        private UILabel m_ambulance;
        private UILabel m_policestation;
        private UILabel m_policecar;

        //private UILabel m_firetruck;

        private UILabel tip1;
        private UILabel tip2;
        private UILabel tip3;
        private UILabel tip4;
        private UILabel tip5;
        private UILabel tip6;
        private UILabel tip7;
        private UILabel tip8;
        private UILabel tip9;
        private UILabel tip10;


        public override void Update()
        {
            if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.M))
            {
                this.ProcessVisibility();
                DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "ctrl+M found");
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
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 - 900), (float)(Loader.parentGuiView.fixedHeight / 2 - 500));
            base.opacity = 1f;
            base.cachedName = cacheName;
            this.CurrentMode = Singleton<ToolManager>.instance.m_properties.m_mode;
            this.m_DragHandler = base.AddUIComponent<UIDragHandle>();
            this.m_DragHandler.target = this;
            this.m_title = base.AddUIComponent<UILabel>();
            this.m_title.text = language.EconomicUI[0];
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

            //base.StartCoroutine(this.RefreshDisplayDataWrapper());
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
                " "
            });
            this.m_HeaderDataText.tooltip = "N/A";
            this.m_HeaderDataText.relativePosition = new Vector3(SPACING, 50f);
            this.m_HeaderDataText.autoSize = true;

            //citizen
            this.m_firstline_citizen = base.AddUIComponent<UILabel>();
            this.m_firstline_citizen.text = language.EconomicUI[1];
            this.m_firstline_citizen.tooltip = "N/A";
            this.m_firstline_citizen.relativePosition = new Vector3(SPACING, this.m_HeaderDataText.relativePosition.y + SPACING22);
            this.m_firstline_citizen.autoSize = true;

            //data
            this.citizen_count = base.AddUIComponent<UILabel>();
            this.citizen_count.text =               string.Format("citizen_count [0000000]");
            this.citizen_count.tooltip = language.EconomicUI[3];
            this.citizen_count.relativePosition = new Vector3(SPACING, this.m_firstline_citizen.relativePosition.y + SPACING22);
            this.citizen_count.autoSize = true;
            this.citizen_count.name = "Moreeconomic_Text_0";

            this.family_count = base.AddUIComponent<UILabel>();
            this.family_count.text =                 string.Format("family_count [0000000]");
            this.family_count.tooltip = language.EconomicUI[5];
            this.family_count.relativePosition = new Vector3(this.citizen_count.relativePosition.x + this.citizen_count.width + SPACING + 30f, this.citizen_count.relativePosition.y);
            this.family_count.autoSize = true;
            this.family_count.name = "Moreeconomic_Text_1";

            this.citizen_salary_per_family = base.AddUIComponent<UILabel>();
            this.citizen_salary_per_family.text =    string.Format("citizen_salary_per_family [000]");
            this.citizen_salary_per_family.tooltip = language.EconomicUI[7];
            this.citizen_salary_per_family.relativePosition = new Vector3(this.family_count.relativePosition.x + this.family_count.width + SPACING + 30f, this.family_count.relativePosition.y);
            this.citizen_salary_per_family.autoSize = true;
            this.citizen_salary_per_family.name = "Moreeconomic_Text_2";

            this.citizen_salary_total = base.AddUIComponent<UILabel>();
            this.citizen_salary_total.text =         string.Format("salary_total [00000000]");
            this.citizen_salary_total.tooltip = language.EconomicUI[9];
            this.citizen_salary_total.relativePosition = new Vector3(SPACING, this.citizen_count.relativePosition.y + SPACING22);
            //this.m_money_forest.relativePosition = new Vector3(this.m_money_farmer.relativePosition.x + this.m_money_farmer.width + SPACING, this.m_money_farmer.relativePosition.y);
            this.citizen_salary_total.autoSize = true;
            this.citizen_salary_total.name = "Moreeconomic_Text_3";

            this.citizen_salary_tax_total = base.AddUIComponent<UILabel>();
            this.citizen_salary_tax_total.text =     string.Format("citizen_tax_total [000]");
            this.citizen_salary_tax_total.tooltip = language.EconomicUI[11];
            this.citizen_salary_tax_total.relativePosition = new Vector3(this.citizen_salary_total.relativePosition.x + this.citizen_salary_total.width + SPACING + 30f, this.citizen_salary_total.relativePosition.y);
            this.citizen_salary_tax_total.autoSize = true;
            this.citizen_salary_tax_total.name = "Moreeconomic_Text_4";

            this.citizen_expense_per_family = base.AddUIComponent<UILabel>();
            this.citizen_expense_per_family.text =   string.Format("expense_per_family [0000000000]");
            this.citizen_expense_per_family.tooltip = language.EconomicUI[13];
            this.citizen_expense_per_family.relativePosition = new Vector3(this.citizen_salary_tax_total.relativePosition.x + this.citizen_salary_tax_total.width + SPACING + 40f, this.citizen_salary_tax_total.relativePosition.y);
            this.citizen_expense_per_family.autoSize = true;
            this.citizen_expense_per_family.name = "Moreeconomic_Text_5";

            this.citizen_expense = base.AddUIComponent<UILabel>();
            this.citizen_expense.text =              string.Format("citizen_expense [0000]");
            this.citizen_expense.tooltip = language.EconomicUI[15];
            this.citizen_expense.relativePosition = new Vector3(SPACING, this.citizen_salary_total.relativePosition.y + SPACING22);
            this.citizen_expense.autoSize = true;
            this.citizen_expense.name = "Moreeconomic_Text_6";

            this.public_transport_fee = base.AddUIComponent<UILabel>();
            this.public_transport_fee.text =         string.Format("public_trans_fee [0000]");
            this.public_transport_fee.tooltip = language.EconomicUI[17];
            this.public_transport_fee.relativePosition = new Vector3(this.citizen_expense.relativePosition.x + this.citizen_expense.width + SPACING + 30f, this.citizen_expense.relativePosition.y);
            this.public_transport_fee.autoSize = true;
            this.public_transport_fee.name = "Moreeconomic_Text_7";

            this.total_citizen_vehical_time = base.AddUIComponent<UILabel>();
            this.total_citizen_vehical_time.text =   string.Format("citizen_vehical_time [00000000]");
            this.total_citizen_vehical_time.tooltip = language.EconomicUI[19];
            this.total_citizen_vehical_time.relativePosition = new Vector3(this.public_transport_fee.relativePosition.x + this.public_transport_fee.width + SPACING + 30f, this.public_transport_fee.relativePosition.y);
            this.total_citizen_vehical_time.autoSize = true;
            this.total_citizen_vehical_time.name = "Moreeconomic_Text_8";

            this.family_profit_money_num = base.AddUIComponent<UILabel>();
            this.family_profit_money_num.text =      string.Format("family_profit_num [000]");
            this.family_profit_money_num.tooltip = language.EconomicUI[21];
            this.family_profit_money_num.relativePosition = new Vector3(SPACING, this.citizen_expense.relativePosition.y + SPACING22);
            this.family_profit_money_num.autoSize = true;
            this.family_profit_money_num.name = "Moreeconomic_Text_9";

            this.family_loss_money_num = base.AddUIComponent<UILabel>();
            this.family_loss_money_num.text =        string.Format("family_loss_num [00000]");
            this.family_loss_money_num.tooltip = language.EconomicUI[23];
            this.family_loss_money_num.relativePosition = new Vector3(this.family_profit_money_num.relativePosition.x + this.family_profit_money_num.width + SPACING + 22f, this.family_profit_money_num.relativePosition.y);
            this.family_loss_money_num.autoSize = true;
            this.family_loss_money_num.name = "Moreeconomic_Text_10";

            this.family_very_profit_num = base.AddUIComponent<UILabel>();
            this.family_very_profit_num.text =       string.Format("family_very_profit_num [00000]");
            this.family_very_profit_num.tooltip = language.EconomicUI[25];
            this.family_very_profit_num.relativePosition = new Vector3(this.family_loss_money_num.relativePosition.x + this.family_loss_money_num.width + SPACING + 20f, this.family_loss_money_num.relativePosition.y);
            //this.m_money_forest.relativePosition = new Vector3(this.m_money_farmer.relativePosition.x + this.m_money_farmer.width + SPACING, this.m_money_farmer.relativePosition.y);
            this.family_very_profit_num.autoSize = true;
            this.family_very_profit_num.name = "Moreeconomic_Text_11";

            this.family_weight_stable_high = base.AddUIComponent<UILabel>();
            this.family_weight_stable_high.text =    string.Format("wealth_stable_high [00000]");
            this.family_weight_stable_high.tooltip = language.EconomicUI[27];
            this.family_weight_stable_high.relativePosition = new Vector3(SPACING, this.family_profit_money_num.relativePosition.y + SPACING22);
            this.family_weight_stable_high.autoSize = true;
            this.family_weight_stable_high.name = "Moreeconomic_Text_12";

            this.family_weight_stable_low = base.AddUIComponent<UILabel>();
            this.family_weight_stable_low.text =     string.Format("wealth_stable_low [00000]");
            this.family_weight_stable_low.tooltip = language.EconomicUI[29];
            this.family_weight_stable_low.relativePosition = new Vector3(this.family_weight_stable_high.relativePosition.x + this.family_weight_stable_high.width + SPACING, this.family_weight_stable_high.relativePosition.y);
            //this.m_money_forest.relativePosition = new Vector3(this.m_money_farmer.relativePosition.x + this.m_money_farmer.width + SPACING, this.m_money_farmer.relativePosition.y);
            this.family_weight_stable_low.autoSize = true;
            this.family_weight_stable_low.name = "Moreeconomic_Text_13";

            this.citizen_average_transport_fee = base.AddUIComponent<UILabel>();
            this.citizen_average_transport_fee.text = string.Format("average_transport_fee [000000]");
            this.citizen_average_transport_fee.tooltip = language.EconomicUI[31];
            this.citizen_average_transport_fee.relativePosition = new Vector3(this.family_weight_stable_low.relativePosition.x + this.family_weight_stable_low.width + SPACING, this.family_weight_stable_low.relativePosition.y);
            //this.m_money_forest.relativePosition = new Vector3(this.m_money_farmer.relativePosition.x + this.m_money_farmer.width + SPACING, this.m_money_farmer.relativePosition.y);
            this.citizen_average_transport_fee.autoSize = true;
            this.citizen_average_transport_fee.name = "Moreeconomic_Text_14";

            this.resident_consumption_rate = base.AddUIComponent<UILabel>();
            this.resident_consumption_rate.text = string.Format("resident_consumption_rate [000000]");
            this.resident_consumption_rate.tooltip = language.EconomicUI[33];
            this.resident_consumption_rate.relativePosition = new Vector3(SPACING, this.family_weight_stable_high.relativePosition.y + SPACING22);
            //this.m_money_forest.relativePosition = new Vector3(this.m_money_farmer.relativePosition.x + this.m_money_farmer.width + SPACING, this.m_money_farmer.relativePosition.y);
            this.resident_consumption_rate.autoSize = true;
            this.resident_consumption_rate.name = "Moreeconomic_Text_44";

            this.tourist_consumption_rate = base.AddUIComponent<UILabel>();
            this.tourist_consumption_rate.text = string.Format("outside_consumption_rate [000000]");
            this.tourist_consumption_rate.tooltip = language.EconomicUI[35];
            this.tourist_consumption_rate.relativePosition = new Vector3(this.resident_consumption_rate.relativePosition.x + this.resident_consumption_rate.width + SPACING + 20f, this.resident_consumption_rate.relativePosition.y);
            //this.m_money_forest.relativePosition = new Vector3(this.m_money_farmer.relativePosition.x + this.m_money_farmer.width + SPACING, this.m_money_farmer.relativePosition.y);
            this.tourist_consumption_rate.autoSize = true;
            this.tourist_consumption_rate.name = "Moreeconomic_Text_45";

            this.city_insurance_account = base.AddUIComponent<UILabel>();
            this.city_insurance_account.text = string.Format("city_insurance_account [000000]");
            this.city_insurance_account.tooltip = language.EconomicUI[144];
            this.city_insurance_account.relativePosition = new Vector3(SPACING, this.resident_consumption_rate.relativePosition.y + SPACING22);
            //this.m_money_forest.relativePosition = new Vector3(this.m_money_farmer.relativePosition.x + this.m_money_farmer.width + SPACING, this.m_money_farmer.relativePosition.y);
            this.city_insurance_account.autoSize = true;
            this.city_insurance_account.name = "Moreeconomic_Text_46";







            //building
            this.m_secondline_building = base.AddUIComponent<UILabel>();
            this.m_secondline_building.text = language.EconomicUI[36];
            this.m_secondline_building.tooltip = "N/A";
            this.m_secondline_building.relativePosition = new Vector3(SPACING, this.city_insurance_account.relativePosition.y + SPACING22);
            this.m_secondline_building.autoSize = true;

            this.good_export_ratio = base.AddUIComponent<UILabel>();
            this.good_export_ratio.text =           string.Format("good_export_ratio  [000]");
            this.good_export_ratio.tooltip = language.EconomicUI[38];
            this.good_export_ratio.relativePosition = new Vector3(SPACING, this.m_secondline_building.relativePosition.y + SPACING22);
            this.good_export_ratio.autoSize = true;
            this.good_export_ratio.name = "Moreeconomic_Text_15";

            this.food_export_ratio = base.AddUIComponent<UILabel>();
            this.food_export_ratio.text =           string.Format("food_export_ratio [0000]");
            this.food_export_ratio.tooltip = language.EconomicUI[40];
            this.food_export_ratio.relativePosition = new Vector3(this.good_export_ratio.relativePosition.x + this.good_export_ratio.width + SPACING + 10f, this.good_export_ratio.relativePosition.y);
            this.food_export_ratio.autoSize = true;
            this.food_export_ratio.name = "Moreeconomic_Text_16";

            this.petrol_export_ratio = base.AddUIComponent<UILabel>();
            this.petrol_export_ratio.text =         string.Format("petrol_export_ratio [000]");
            this.petrol_export_ratio.tooltip = language.EconomicUI[42];
            this.petrol_export_ratio.relativePosition = new Vector3(this.food_export_ratio.relativePosition.x + this.food_export_ratio.width + SPACING + 10f, this.food_export_ratio.relativePosition.y);
            this.petrol_export_ratio.autoSize = true;
            this.petrol_export_ratio.name = "Moreeconomic_Text_17";

            this.coal_export_ratio = base.AddUIComponent<UILabel>();
            this.coal_export_ratio.text =           string.Format("coal_export_ratio [00000]");
            this.coal_export_ratio.tooltip = language.EconomicUI[44];
            this.coal_export_ratio.relativePosition = new Vector3(SPACING, this.petrol_export_ratio.relativePosition.y + SPACING22);
            this.coal_export_ratio.autoSize = true;
            this.coal_export_ratio.name = "Moreeconomic_Text_18";

            this.lumber_export_ratio = base.AddUIComponent<UILabel>();
            this.lumber_export_ratio.text =         string.Format("lumber_export_ratio [000]");
            this.lumber_export_ratio.tooltip = language.EconomicUI[46];
            this.lumber_export_ratio.relativePosition = new Vector3(this.coal_export_ratio.relativePosition.x + this.coal_export_ratio.width + SPACING, this.coal_export_ratio.relativePosition.y);
            this.lumber_export_ratio.autoSize = true;
            this.lumber_export_ratio.name = "Moreeconomic_Text_19";

            this.oil_export_ratio = base.AddUIComponent<UILabel>();
            this.oil_export_ratio.text =            string.Format("oil_export_ratio [000000]");
            this.oil_export_ratio.tooltip = language.EconomicUI[48];
            this.oil_export_ratio.relativePosition = new Vector3(this.lumber_export_ratio.relativePosition.x + this.lumber_export_ratio.width + SPACING, this.lumber_export_ratio.relativePosition.y);
            this.oil_export_ratio.autoSize = true;
            this.oil_export_ratio.name = "Moreeconomic_Text_20";

            this.ore_export_ratio = base.AddUIComponent<UILabel>();
            this.ore_export_ratio.text =            string.Format("ore_export_ratio [000000]");
            this.ore_export_ratio.tooltip = language.EconomicUI[50];
            this.ore_export_ratio.relativePosition = new Vector3(SPACING, this.oil_export_ratio.relativePosition.y + SPACING22);
            this.ore_export_ratio.autoSize = true;
            this.ore_export_ratio.name = "Moreeconomic_Text_21";

            this.log_export_ratio = base.AddUIComponent<UILabel>();
            this.log_export_ratio.text =            string.Format("log_export_ratio [000000]");
            this.log_export_ratio.tooltip = language.EconomicUI[52];
            this.log_export_ratio.relativePosition = new Vector3(this.ore_export_ratio.relativePosition.x + this.ore_export_ratio.width + SPACING, this.ore_export_ratio.relativePosition.y);
            this.log_export_ratio.autoSize = true;
            this.log_export_ratio.name = "Moreeconomic_Text_22";

            this.grain_export_ratio = base.AddUIComponent<UILabel>();
            this.grain_export_ratio.text =          string.Format("grain_export_ratio [0000]");
            this.grain_export_ratio.tooltip = language.EconomicUI[54];
            this.grain_export_ratio.relativePosition = new Vector3(this.log_export_ratio.relativePosition.x + this.log_export_ratio.width + SPACING, this.log_export_ratio.relativePosition.y);
            this.grain_export_ratio.autoSize = true;
            this.grain_export_ratio.name = "Moreeconomic_Text_23";

            this.good_import_ratio = base.AddUIComponent<UILabel>();
            this.good_import_ratio.text =           string.Format("good_import_ratio  [0000]");
            this.good_import_ratio.tooltip = language.EconomicUI[56];
            this.good_import_ratio.relativePosition = new Vector3(SPACING, this.grain_export_ratio.relativePosition.y + SPACING22);
            this.good_import_ratio.autoSize = true;
            this.good_import_ratio.name = "Moreeconomic_Text_65";

            this.food_import_ratio = base.AddUIComponent<UILabel>();
            this.food_import_ratio.text =           string.Format("food_import_ratio [00000]");
            this.food_import_ratio.tooltip = language.EconomicUI[58];
            this.food_import_ratio.relativePosition = new Vector3(this.good_import_ratio.relativePosition.x + this.good_import_ratio.width + SPACING, this.good_import_ratio.relativePosition.y);
            this.food_import_ratio.autoSize = true;
            this.food_import_ratio.name = "Moreeconomic_Text_66";

            this.petrol_import_ratio = base.AddUIComponent<UILabel>();
            this.petrol_import_ratio.text =         string.Format("petrol_import_ratio [000]");
            this.petrol_import_ratio.tooltip = language.EconomicUI[60];
            this.petrol_import_ratio.relativePosition = new Vector3(this.food_import_ratio.relativePosition.x + this.food_import_ratio.width + SPACING, this.food_import_ratio.relativePosition.y);
            this.petrol_import_ratio.autoSize = true;
            this.petrol_import_ratio.name = "Moreeconomic_Text_67";

            this.coal_import_ratio = base.AddUIComponent<UILabel>();
            this.coal_import_ratio.text =           string.Format("coal_import_ratio [00000]");
            this.coal_import_ratio.tooltip = language.EconomicUI[62];
            this.coal_import_ratio.relativePosition = new Vector3(SPACING, this.petrol_import_ratio.relativePosition.y + SPACING22);
            this.coal_import_ratio.autoSize = true;
            this.coal_import_ratio.name = "Moreeconomic_Text_68";

            this.lumber_import_ratio = base.AddUIComponent<UILabel>();
            this.lumber_import_ratio.text =         string.Format("lumber_import_ratio [000]");
            this.lumber_import_ratio.tooltip = language.EconomicUI[64];
            this.lumber_import_ratio.relativePosition = new Vector3(this.coal_import_ratio.relativePosition.x + this.coal_import_ratio.width + SPACING, this.coal_import_ratio.relativePosition.y);
            this.lumber_import_ratio.autoSize = true;
            this.lumber_import_ratio.name = "Moreeconomic_Text_69";

            this.oil_import_ratio = base.AddUIComponent<UILabel>();
            this.oil_import_ratio.text =            string.Format("oil_import_ratio [000000]");
            this.oil_import_ratio.tooltip = language.EconomicUI[66];
            this.oil_import_ratio.relativePosition = new Vector3(this.lumber_import_ratio.relativePosition.x + this.lumber_import_ratio.width + SPACING, this.lumber_import_ratio.relativePosition.y);
            this.oil_import_ratio.autoSize = true;
            this.oil_import_ratio.name = "Moreeconomic_Text_70";

            this.ore_import_ratio = base.AddUIComponent<UILabel>();
            this.ore_import_ratio.text =            string.Format("ore_import_ratio [000000]");
            this.ore_import_ratio.tooltip = language.EconomicUI[68];
            this.ore_import_ratio.relativePosition = new Vector3(SPACING, this.oil_import_ratio.relativePosition.y + SPACING22);
            this.ore_import_ratio.autoSize = true;
            this.ore_import_ratio.name = "Moreeconomic_Text_71";

            this.log_import_ratio = base.AddUIComponent<UILabel>();
            this.log_import_ratio.text =            string.Format("log_import_ratio [000000]");
            this.log_import_ratio.tooltip = language.EconomicUI[70];
            this.log_import_ratio.relativePosition = new Vector3(this.ore_import_ratio.relativePosition.x + this.ore_import_ratio.width + SPACING, this.ore_import_ratio.relativePosition.y);
            this.log_import_ratio.autoSize = true;
            this.log_import_ratio.name = "Moreeconomic_Text_72";

            this.grain_import_ratio = base.AddUIComponent<UILabel>();
            this.grain_import_ratio.text =          string.Format("grain_import_ratio [00000]");
            this.grain_import_ratio.tooltip = language.EconomicUI[72];
            this.grain_import_ratio.relativePosition = new Vector3(this.log_import_ratio.relativePosition.x + this.log_import_ratio.width + SPACING, this.log_import_ratio.relativePosition.y);
            this.grain_import_ratio.autoSize = true;
            this.grain_import_ratio.name = "Moreeconomic_Text_73";


            this.all_comm_building_profit = base.AddUIComponent<UILabel>();
            this.all_comm_building_profit.text =     string.Format("all_comm_building_profit num   [00000]");
            this.all_comm_building_profit.tooltip = language.EconomicUI[74];
            this.all_comm_building_profit.relativePosition = new Vector3(SPACING, this.grain_import_ratio.relativePosition.y + SPACING22);
            this.all_comm_building_profit.autoSize = true;
            this.all_comm_building_profit.name = "Moreeconomic_Text_25";

            this.all_comm_building_loss = base.AddUIComponent<UILabel>();
            this.all_comm_building_loss.text =       string.Format("all_comm_building_loss num   [0000000]");
            this.all_comm_building_loss.tooltip = language.EconomicUI[76];
            this.all_comm_building_loss.relativePosition = new Vector3(this.all_comm_building_profit.relativePosition.x + this.all_comm_building_profit.width + SPACING, this.all_comm_building_profit.relativePosition.y);
            this.all_comm_building_loss.autoSize = true;
            this.all_comm_building_loss.name = "Moreeconomic_Text_26";

            this.all_industry_building_profit = base.AddUIComponent<UILabel>();
            this.all_industry_building_profit.text = string.Format("all_indust_building_profit num [00000]");
            this.all_industry_building_profit.tooltip = language.EconomicUI[78];
            this.all_industry_building_profit.relativePosition = new Vector3(SPACING, this.all_comm_building_profit.relativePosition.y + SPACING22);
            this.all_industry_building_profit.autoSize = true;
            this.all_industry_building_profit.name = "Moreeconomic_Text_27";

            this.all_industry_building_loss = base.AddUIComponent<UILabel>();
            this.all_industry_building_loss.text =   string.Format("all_industry_building_loss num [00000]");
            this.all_industry_building_loss.tooltip = language.EconomicUI[80];
            this.all_industry_building_loss.relativePosition = new Vector3(this.all_industry_building_profit.relativePosition.x + this.all_industry_building_profit.width + SPACING + 10f , this.all_industry_building_profit.relativePosition.y);
            this.all_industry_building_loss.autoSize = true;
            this.all_industry_building_loss.name = "Moreeconomic_Text_28";

            this.all_foresty_building_profit = base.AddUIComponent<UILabel>();
            this.all_foresty_building_profit.text =  string.Format("all_foresty_building_profit num [00000]");
            this.all_foresty_building_profit.tooltip = language.EconomicUI[82];
            this.all_foresty_building_profit.relativePosition = new Vector3(SPACING, this.all_industry_building_profit.relativePosition.y + SPACING22);
            this.all_foresty_building_profit.autoSize = true;
            this.all_foresty_building_profit.name = "Moreeconomic_Text_29";

            this.all_foresty_building_loss = base.AddUIComponent<UILabel>();
            this.all_foresty_building_loss.text =    string.Format("all_foresty_building_loss num  [000000]");
            this.all_foresty_building_loss.tooltip = language.EconomicUI[84];
            this.all_foresty_building_loss.relativePosition = new Vector3(this.all_foresty_building_profit.relativePosition.x + this.all_foresty_building_profit.width + SPACING -10f , this.all_foresty_building_profit.relativePosition.y);
            this.all_foresty_building_loss.autoSize = true;
            this.all_foresty_building_loss.name = "Moreeconomic_Text_30";

            this.all_farmer_building_profit = base.AddUIComponent<UILabel>();
            this.all_farmer_building_profit.text =   string.Format("all_farmer_building_profit num  [00000]");
            this.all_farmer_building_profit.tooltip = language.EconomicUI[86];
            this.all_farmer_building_profit.relativePosition = new Vector3(SPACING, this.all_foresty_building_profit.relativePosition.y + SPACING22);
            this.all_farmer_building_profit.autoSize = true;
            this.all_farmer_building_profit.name = "Moreeconomic_Text_31";

            this.all_farmer_building_loss = base.AddUIComponent<UILabel>();
            this.all_farmer_building_loss.text =     string.Format("all_farming_building_loss num  [000000]");
            this.all_farmer_building_loss.tooltip = language.EconomicUI[88];
            this.all_farmer_building_loss.relativePosition = new Vector3(this.all_farmer_building_profit.relativePosition.x + this.all_farmer_building_profit.width + SPACING - 15f, this.all_farmer_building_profit.relativePosition.y);
            this.all_farmer_building_loss.autoSize = true;
            this.all_farmer_building_loss.name = "Moreeconomic_Text_32";

            this.all_oil_building_profit = base.AddUIComponent<UILabel>();
            this.all_oil_building_profit.text =      string.Format("all_oil_building_profit num  [00000000]");
            this.all_oil_building_profit.tooltip = language.EconomicUI[90];
            this.all_oil_building_profit.relativePosition = new Vector3(SPACING, this.all_farmer_building_profit.relativePosition.y + SPACING22);
            this.all_oil_building_profit.autoSize = true;
            this.all_oil_building_profit.name = "Moreeconomic_Text_33";

            this.all_oil_building_loss = base.AddUIComponent<UILabel>();
            this.all_oil_building_loss.text =        string.Format("all_oil_building_loss num   [000000000]");
            this.all_oil_building_loss.tooltip = language.EconomicUI[92];
            this.all_oil_building_loss.relativePosition = new Vector3(this.all_oil_building_profit.relativePosition.x + this.all_oil_building_profit.width + SPACING, this.all_oil_building_profit.relativePosition.y);
            this.all_oil_building_loss.autoSize = true;
            this.all_oil_building_loss.name = "Moreeconomic_Text_34";

            this.all_ore_building_profit = base.AddUIComponent<UILabel>();
            this.all_ore_building_profit.text =      string.Format("all_ore_building_profit num  [0000000]");
            this.all_ore_building_profit.tooltip = language.EconomicUI[94];
            this.all_ore_building_profit.relativePosition = new Vector3(SPACING, this.all_oil_building_profit.relativePosition.y + SPACING22);
            this.all_ore_building_profit.autoSize = true;
            this.all_ore_building_profit.name = "Moreeconomic_Text_35";

            this.all_ore_building_loss = base.AddUIComponent<UILabel>();
            this.all_ore_building_loss.text =        string.Format("all_ore_building_loss num  [000000000]");
            this.all_ore_building_loss.tooltip = language.EconomicUI[96];
            this.all_ore_building_loss.relativePosition = new Vector3(this.all_ore_building_profit.relativePosition.x + this.all_ore_building_profit.width + SPACING, this.all_ore_building_profit.relativePosition.y);
            this.all_ore_building_loss.autoSize = true;
            this.all_ore_building_loss.name = "Moreeconomic_Text_36";

            this.office_gen_salary_index = base.AddUIComponent<UILabel>();
            this.office_gen_salary_index.text = string.Format("office_gen_salary_index [0000000000]");
            this.office_gen_salary_index.tooltip = language.EconomicUI[98];
            this.office_gen_salary_index.relativePosition = new Vector3(SPACING, this.all_ore_building_profit.relativePosition.y + SPACING22);
            this.office_gen_salary_index.autoSize = true;
            this.office_gen_salary_index.name = "Moreeconomic_Text_42";

            this.office_high_tech_salary_index = base.AddUIComponent<UILabel>();
            this.office_high_tech_salary_index.text = string.Format("office_high_tech_salary_index [0000000000]");
            this.office_high_tech_salary_index.tooltip = language.EconomicUI[100];
            this.office_high_tech_salary_index.relativePosition = new Vector3(this.office_gen_salary_index.relativePosition.x + this.office_gen_salary_index.width + SPACING + 10f, this.office_gen_salary_index.relativePosition.y);
            this.office_high_tech_salary_index.autoSize = true;
            this.office_high_tech_salary_index.name = "Moreeconomic_Text_43";

            this.m_thirdline_outside = base.AddUIComponent<UILabel>();
            this.m_thirdline_outside.text = string.Format(language.EconomicUI[101]);
            this.m_thirdline_outside.tooltip = language.EconomicUI[102];
            this.m_thirdline_outside.relativePosition = new Vector3(SPACING, this.office_gen_salary_index.relativePosition.y + SPACING22);
            this.m_thirdline_outside.autoSize = true;
            this.m_thirdline_outside.name = "Moreeconomic_Text_44";

            this.m_outside_garbage = base.AddUIComponent<UILabel>();
            this.m_outside_garbage.text = string.Format("outside garbage [00000]");
            this.m_outside_garbage.tooltip = language.EconomicUI[104];
            this.m_outside_garbage.relativePosition = new Vector3(SPACING, this.m_thirdline_outside.relativePosition.y + SPACING22);
            this.m_outside_garbage.autoSize = true;
            this.m_outside_garbage.name = "Moreeconomic_Text_45";

            this.m_outside_dead = base.AddUIComponent<UILabel>();
            this.m_outside_dead.text =    string.Format("outside dead [00000]");
            this.m_outside_dead.tooltip = language.EconomicUI[106];
            this.m_outside_dead.relativePosition = new Vector3(this.m_outside_garbage.relativePosition.x + this.m_outside_garbage.width + SPACING, this.m_outside_garbage.relativePosition.y);
            this.m_outside_dead.autoSize = true;
            this.m_outside_dead.name = "Moreeconomic_Text_46";

            this.m_outside_crime = base.AddUIComponent<UILabel>();
            this.m_outside_crime.text = string.Format("outside crime [00000]");
            this.m_outside_crime.tooltip = language.EconomicUI[108];
            this.m_outside_crime.relativePosition = new Vector3(this.m_outside_dead.relativePosition.x + this.m_outside_dead.width + SPACING, this.m_outside_dead.relativePosition.y);
            this.m_outside_crime.autoSize = true;
            this.m_outside_crime.name = "Moreeconomic_Text_47";

            this.m_outside_sick = base.AddUIComponent<UILabel>();
            this.m_outside_sick.text = string.Format("outside sick [00000]");
            this.m_outside_sick.tooltip = language.EconomicUI[110];
            this.m_outside_sick.relativePosition = new Vector3(this.m_outside_crime.relativePosition.x + this.m_outside_crime.width + SPACING, this.m_outside_crime.relativePosition.y);
            this.m_outside_sick.autoSize = true;
            this.m_outside_sick.name = "Moreeconomic_Text_48";

            this.m_outside_road = base.AddUIComponent<UILabel>();
            this.m_outside_road.text = string.Format("outside road [00000]");
            this.m_outside_road.tooltip = language.EconomicUI[112];
            this.m_outside_road.relativePosition = new Vector3(SPACING, this.m_outside_garbage.relativePosition.y + SPACING22);
            this.m_outside_road.autoSize = true;
            this.m_outside_road.name = "Moreeconomic_Text_48";

            this.m_outside_firestation = base.AddUIComponent<UILabel>();
            this.m_outside_firestation.text = string.Format("outside fire [00000]");
            this.m_outside_firestation.tooltip = language.EconomicUI[114];
            this.m_outside_firestation.relativePosition = new Vector3(this.m_outside_road.relativePosition.x + this.m_outside_road.width + SPACING + 140f, this.m_outside_road.relativePosition.y);
            this.m_outside_firestation.autoSize = true;
            this.m_outside_firestation.name = "Moreeconomic_Text_48";

            this.m_hospital = base.AddUIComponent<UILabel>();
            this.m_hospital.text = string.Format("outside hospital capacity [00000]");
            this.m_hospital.tooltip = language.EconomicUI[116];
            this.m_hospital.relativePosition = new Vector3(SPACING, this.m_outside_road.relativePosition.y + SPACING22);
            this.m_hospital.autoSize = true;
            this.m_hospital.name = "Moreeconomic_Text_48";

            this.m_ambulance = base.AddUIComponent<UILabel>();
            this.m_ambulance.text = string.Format("outside ambulance [00000]");
            this.m_ambulance.tooltip = language.EconomicUI[118];
            this.m_ambulance.relativePosition = new Vector3(this.m_hospital.relativePosition.x + this.m_hospital.width + SPACING + 40f, this.m_hospital.relativePosition.y);
            this.m_ambulance.autoSize = true;
            this.m_ambulance.name = "Moreeconomic_Text_48";

            this.m_policestation = base.AddUIComponent<UILabel>();
            this.m_policestation.text = string.Format("outside policestation capacity [00000]");
            this.m_policestation.tooltip = language.EconomicUI[120];
            this.m_policestation.relativePosition = new Vector3(SPACING, this.m_hospital.relativePosition.y + SPACING22);
            this.m_policestation.autoSize = true;
            this.m_policestation.name = "Moreeconomic_Text_48";

            this.m_policecar = base.AddUIComponent<UILabel>();
            this.m_policecar.text = string.Format("outside policecar num [00000]");
            this.m_policecar.tooltip = language.EconomicUI[122];
            this.m_policecar.relativePosition = new Vector3(this.m_policestation.relativePosition.x + this.m_policestation.width + SPACING, this.m_policestation.relativePosition.y);
            this.m_policecar.autoSize = true;
            this.m_policecar.name = "Moreeconomic_Text_48";

            //this.m_firetruck = base.AddUIComponent<UILabel>();
            //this.m_firetruck.text = string.Format("m_firetruck [00000]");
            //this.m_firetruck.tooltip = language.EconomicUI[124];
            //this.m_firetruck.relativePosition = new Vector3(this.m_policecar.relativePosition.x + this.m_policecar.width + SPACING, this.m_policecar.relativePosition.y);
            //this.m_firetruck.autoSize = true;
            //this.m_firetruck.name = "Moreeconomic_Text_48";

            this.tip1 = base.AddUIComponent<UILabel>();
            this.tip1.text = string.Format("tip1: [0000000000]");
            this.tip1.tooltip = language.EconomicUI[126];
            this.tip1.relativePosition = new Vector3(SPACING, this.m_policestation.relativePosition.y + SPACING22 + 10f);
            this.tip1.autoSize = true;
            this.tip1.name = "Moreeconomic_Text_49";

            this.tip2 = base.AddUIComponent<UILabel>();
            this.tip2.text = string.Format("tip2: [0000000000]");
            this.tip2.tooltip = language.EconomicUI[128];
            this.tip2.relativePosition = new Vector3(SPACING, this.tip1.relativePosition.y + SPACING22);
            this.tip2.autoSize = true;
            this.tip2.name = "Moreeconomic_Text_50";

            this.tip3 = base.AddUIComponent<UILabel>();
            this.tip3.text = string.Format("tip3: [0000000000]");
            this.tip3.tooltip = language.EconomicUI[130];
            this.tip3.relativePosition = new Vector3(SPACING, this.tip2.relativePosition.y + SPACING22);
            this.tip3.autoSize = true;
            this.tip3.name = "Moreeconomic_Text_51";

            this.tip4 = base.AddUIComponent<UILabel>();
            this.tip4.text = string.Format("tip4: [0000000000]");
            this.tip4.tooltip = language.EconomicUI[132];
            this.tip4.relativePosition = new Vector3(SPACING, this.tip3.relativePosition.y + SPACING22);
            this.tip4.autoSize = true;
            this.tip4.name = "Moreeconomic_Text_52";

            this.tip5 = base.AddUIComponent<UILabel>();
            this.tip5.text = string.Format("tip5: [0000000000]");
            this.tip5.tooltip = language.EconomicUI[134];
            this.tip5.relativePosition = new Vector3(SPACING, this.tip4.relativePosition.y + SPACING22);
            this.tip5.autoSize = true;
            this.tip5.name = "Moreeconomic_Text_53";

            this.tip6 = base.AddUIComponent<UILabel>();
            this.tip6.text = string.Format("tip6: [0000000000]");
            this.tip6.tooltip = language.EconomicUI[136];
            this.tip6.relativePosition = new Vector3(SPACING, this.tip5.relativePosition.y + SPACING22);
            this.tip6.autoSize = true;
            this.tip6.name = "Moreeconomic_Text_50";

            this.tip7 = base.AddUIComponent<UILabel>();
            this.tip7.text = string.Format("tip7: [0000000000]");
            this.tip7.tooltip = language.EconomicUI[138];
            this.tip7.relativePosition = new Vector3(SPACING, this.tip6.relativePosition.y + SPACING22);
            this.tip7.autoSize = true;
            this.tip7.name = "Moreeconomic_Text_51";

            this.tip8 = base.AddUIComponent<UILabel>();
            this.tip8.text = string.Format("tip8: [0000000000]");
            this.tip8.tooltip = language.EconomicUI[140];
            this.tip8.relativePosition = new Vector3(SPACING, this.tip7.relativePosition.y + SPACING22);
            this.tip8.autoSize = true;
            this.tip8.name = "Moreeconomic_Text_52";

            this.tip9 = base.AddUIComponent<UILabel>();
            this.tip9.text = string.Format("tip9: [0000000000]");
            this.tip9.tooltip = language.EconomicUI[142];
            this.tip9.relativePosition = new Vector3(SPACING, this.tip8.relativePosition.y + SPACING22);
            this.tip9.autoSize = true;
            this.tip9.name = "Moreeconomic_Text_53";

            this.tip10 = base.AddUIComponent<UILabel>();
            this.tip10.text = string.Format("tip10: [0000000000]");
            this.tip10.tooltip = language.EconomicUI[146];
            this.tip10.relativePosition = new Vector3(SPACING, this.tip9.relativePosition.y + SPACING22);
            this.tip10.autoSize = true;
            this.tip10.name = "Moreeconomic_Text_53";

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
                    this.GetDataNeeded();
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
            uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
            uint num2 = currentFrameIndex & 255u;

            if ((num2 == 255u) && (comm_data.current_time != comm_data.prev_time))
            {
                //citizen
                this.m_title.text = language.EconomicUI[0];
                this.m_firstline_citizen.text = language.EconomicUI[1];
                this.citizen_count.text = string.Format(language.EconomicUI[2] + " [{0}]", comm_data.citizen_count);
                this.family_count.text = string.Format(language.EconomicUI[4] + " [{0}]", comm_data.family_count);
                this.citizen_salary_per_family.text = string.Format(language.EconomicUI[6] + " [{0}]", comm_data.citizen_salary_per_family);
                this.citizen_salary_total.text = string.Format(language.EconomicUI[8] + " [{0}]", comm_data.citizen_salary_total);
                this.citizen_salary_tax_total.text = string.Format(language.EconomicUI[10] + " [{0}]", comm_data.citizen_salary_tax_total * comm_data.game_income_expense_multiple);
                this.citizen_expense_per_family.text = string.Format(language.EconomicUI[12] + " [{0}]", comm_data.citizen_expense_per_family);
                this.citizen_expense.text = string.Format(language.EconomicUI[14] + " [{0}]", comm_data.citizen_expense * comm_data.game_income_expense_multiple);
                this.public_transport_fee.text = string.Format(language.EconomicUI[16] + " [{0}]", comm_data.public_transport_fee * comm_data.game_income_expense_multiple);
                this.total_citizen_vehical_time.text = string.Format(language.EconomicUI[18] + " [{0}]", comm_data.temp_total_citizen_vehical_time_last);
                this.family_profit_money_num.text = string.Format(language.EconomicUI[20] + " [{0}]", comm_data.family_profit_money_num);
                this.family_loss_money_num.text = string.Format(language.EconomicUI[22] + " [{0}]", comm_data.family_loss_money_num);
                this.family_very_profit_num.text = string.Format(language.EconomicUI[24] + " [{0}]", comm_data.family_very_profit_money_num);
                this.family_weight_stable_high.text = string.Format(language.EconomicUI[26] + " [{0}]", comm_data.family_weight_stable_high);
                this.family_weight_stable_low.text = string.Format(language.EconomicUI[28] + " [{0}]", comm_data.family_weight_stable_low);
                this.citizen_average_transport_fee.text = string.Format(language.EconomicUI[30] + " [{0}]", comm_data.citizen_average_transport_fee);
                this.resident_consumption_rate.text = string.Format(language.EconomicUI[32] + " [{0}]", pc_ResidentAI.citizen_goods);
                if (comm_data.family_count != 0)
                {
                    this.tourist_consumption_rate.text = string.Format(language.EconomicUI[34] + " [{0:N3}%]", (float)(pc_ResidentAI.citizen_goods / (comm_data.family_count * 200f)));
                }
                else
                {
                    this.tourist_consumption_rate.text = string.Format(language.EconomicUI[34] + " [{0:N3}%]", 0);
                }

                //building
                this.m_secondline_building.text = language.EconomicUI[36];
                this.good_export_ratio.text = string.Format(language.EconomicUI[37] + " [{0:N2}]", pc_PrivateBuildingAI.good_export_ratio);
                this.food_export_ratio.text = string.Format(language.EconomicUI[39] + " [{0:N2}]", pc_PrivateBuildingAI.food_export_ratio);
                this.petrol_export_ratio.text = string.Format(language.EconomicUI[41] + " [{0:N2}]", pc_PrivateBuildingAI.petrol_export_ratio);
                this.coal_export_ratio.text = string.Format(language.EconomicUI[43] + " [{0:N2}]", pc_PrivateBuildingAI.coal_export_ratio);
                this.lumber_export_ratio.text = string.Format(language.EconomicUI[45] + " [{0:N2}]", pc_PrivateBuildingAI.lumber_export_ratio);
                this.oil_export_ratio.text = string.Format(language.EconomicUI[47] + " [{0:N2}]", pc_PrivateBuildingAI.oil_export_ratio);
                this.ore_export_ratio.text = string.Format(language.EconomicUI[49] + " [{0:N2}]", pc_PrivateBuildingAI.ore_export_ratio);
                this.log_export_ratio.text = string.Format(language.EconomicUI[51] + " [{0:N2}]", pc_PrivateBuildingAI.log_export_ratio);
                this.grain_export_ratio.text = string.Format(language.EconomicUI[53] + " [{0:N2}]", pc_PrivateBuildingAI.grain_export_ratio);

                this.good_import_ratio.text = string.Format(language.EconomicUI[55] + " [{0:N2}]", pc_PrivateBuildingAI.good_import_ratio);
                this.food_import_ratio.text = string.Format(language.EconomicUI[57] + " [{0:N2}]", pc_PrivateBuildingAI.food_import_ratio);
                this.petrol_import_ratio.text = string.Format(language.EconomicUI[59] + " [{0:N2}]", pc_PrivateBuildingAI.petrol_import_ratio);
                this.coal_import_ratio.text = string.Format(language.EconomicUI[61] + " [{0:N2}]", pc_PrivateBuildingAI.coal_import_ratio);
                this.lumber_import_ratio.text = string.Format(language.EconomicUI[63] + " [{0:N2}]", pc_PrivateBuildingAI.lumber_import_ratio);
                this.oil_import_ratio.text = string.Format(language.EconomicUI[65] + " [{0:N2}]", pc_PrivateBuildingAI.oil_import_ratio);
                this.ore_import_ratio.text = string.Format(language.EconomicUI[67] + " [{0:N2}]", pc_PrivateBuildingAI.ore_import_ratio);
                this.log_import_ratio.text = string.Format(language.EconomicUI[69] + " [{0:N2}]", pc_PrivateBuildingAI.log_import_ratio);
                this.grain_import_ratio.text = string.Format(language.EconomicUI[71] + " [{0:N2}]", pc_PrivateBuildingAI.grain_import_ratio);



                this.all_comm_building_profit.text = string.Format(language.EconomicUI[73] + " [{0}]", pc_PrivateBuildingAI.all_comm_building_profit_final);
                this.all_comm_building_loss.text = string.Format(language.EconomicUI[75] + " [{0}]", pc_PrivateBuildingAI.all_comm_building_loss_final);
                this.all_industry_building_profit.text = string.Format(language.EconomicUI[77] + " [{0}]", pc_PrivateBuildingAI.all_industry_building_profit_final);
                this.all_industry_building_loss.text = string.Format(language.EconomicUI[79] + " [{0}]", pc_PrivateBuildingAI.all_industry_building_loss_final);
                this.all_foresty_building_profit.text = string.Format(language.EconomicUI[81] + " [{0}]", pc_PrivateBuildingAI.all_foresty_building_profit_final);
                this.all_foresty_building_loss.text = string.Format(language.EconomicUI[83] + " [{0}]", pc_PrivateBuildingAI.all_foresty_building_loss_final);
                this.all_farmer_building_profit.text = string.Format(language.EconomicUI[85] + " [{0}]", pc_PrivateBuildingAI.all_farmer_building_profit_final);
                this.all_farmer_building_loss.text = string.Format(language.EconomicUI[87] + " [{0}]", pc_PrivateBuildingAI.all_farmer_building_loss_final);
                this.all_oil_building_profit.text = string.Format(language.EconomicUI[89] + " [{0}]", pc_PrivateBuildingAI.all_oil_building_profit_final);
                this.all_oil_building_loss.text = string.Format(language.EconomicUI[91] + " [{0}]", pc_PrivateBuildingAI.all_oil_building_loss_final);
                this.all_ore_building_profit.text = string.Format(language.EconomicUI[93] + " [{0}]", pc_PrivateBuildingAI.all_ore_building_profit_final);
                this.all_ore_building_loss.text = string.Format(language.EconomicUI[95] + " [{0}]", pc_PrivateBuildingAI.all_ore_building_loss_final);
                this.office_gen_salary_index.text = string.Format(language.EconomicUI[97] + " [{0}]", pc_PrivateBuildingAI.greater_than_20000_profit_building_num_final);
                this.office_high_tech_salary_index.text = string.Format(language.EconomicUI[99] + " [{0}]", pc_PrivateBuildingAI.greater_than_20000_profit_building_money_final);

                this.m_thirdline_outside.text = string.Format(language.EconomicUI[101]);
                this.m_outside_garbage.text = string.Format(language.EconomicUI[103] + " [{0}]", comm_data.outside_garbage_count);
                this.m_outside_dead.text = string.Format(language.EconomicUI[105] + " [{0}]", comm_data.outside_dead_count);
                this.m_outside_sick.text = string.Format(language.EconomicUI[107] + " [{0}]", comm_data.outside_sick_count);
                this.m_outside_crime.text = string.Format(language.EconomicUI[109] + " [{0}]", comm_data.outside_crime_count);
                if (comm_data.outside_road_num_final != 0)
                {
                    this.m_outside_road.text = string.Format(language.EconomicUI[111] + " [{0:N2}%]", (100f - (float)comm_data.outside_road_count / (comm_data.outside_road_num_final * 650f)));
                }
                else
                {
                    this.m_outside_road.text = string.Format(language.EconomicUI[111] + " [{0:N2}%]", 0);
                }
                this.m_outside_firestation.text = string.Format(language.EconomicUI[113] + " [{0}]", comm_data.outside_firestation_count);

                this.m_hospital.text = string.Format(language.EconomicUI[115] + " [{0}/{1}]", comm_data.outside_patient ,comm_data.outside_road_num_final * pc_OutsideConnectionAI.m_patientCapacity);
                this.m_ambulance.text = string.Format(language.EconomicUI[117] + " [{0}/{1}]", comm_data.outside_ambulance_car , comm_data.outside_road_num_final * pc_OutsideConnectionAI.m_ambulanceCount);
                this.m_policestation.text = string.Format(language.EconomicUI[119] + " [{0}/{1}]", comm_data.outside_crime, comm_data.outside_road_num_final * pc_OutsideConnectionAI.m_jailCapacity);
                this.m_policecar.text = string.Format(language.EconomicUI[121] + " [{0}/{1}]", comm_data.outside_police_car, comm_data.outside_road_num_final * pc_OutsideConnectionAI.m_policeCarCount);

                //this.m_firetruck.text = string.Format(language.EconomicUI[123] + " [{0}/{1}]", comm_data.firetruck, comm_data.outside_road_num_final * pc_OutsideConnectionAI.m_fireTruckCount);

                this.tip1.text = string.Format(language.EconomicUI[125] + "  " + RealCity.tip1_message_forgui);
                this.tip2.text = string.Format(language.EconomicUI[127] + "  " + RealCity.tip2_message_forgui);
                this.tip3.text = string.Format(language.EconomicUI[129] + "  " + RealCity.tip3_message_forgui);
                this.tip4.text = string.Format(language.EconomicUI[131] + "  " + RealCity.tip4_message_forgui);
                this.tip5.text = string.Format(language.EconomicUI[133] + "  " + RealCity.tip5_message_forgui);
                this.tip6.text = string.Format(language.EconomicUI[135] + "  " + RealCity.tip6_message_forgui);
                this.tip7.text = string.Format(language.EconomicUI[137] + "  " + RealCity.tip7_message_forgui);
                this.tip8.text = string.Format(language.EconomicUI[139] + "  " + RealCity.tip8_message_forgui);
                this.tip9.text = string.Format(language.EconomicUI[141] + "  " + RealCity.tip9_message_forgui);
                this.tip10.text = string.Format(language.EconomicUI[145] + "  " + RealCity.tip10_message_forgui);

                this.city_insurance_account.text = string.Format(language.EconomicUI[143] + " [{0:N2}]",comm_data.city_insurance_account_final);
            }
        }

        private void ProcessVisibility()
        {
            if (!base.isVisible)
            {
                base.Show();
                //if (!this.CoDisplayRefreshEnabled)
                //{
                //    base.StartCoroutine(this.RefreshDisplayDataWrapper());
                //    return;
                //}
            }
            else
            {
                base.Hide();
            }
        }
    }
}
