using System.Collections.Generic;
using ColossalFramework;
using ColossalFramework.UI;
using UnityEngine;
using System.Collections;

namespace RealCity
{
    public class EcnomicUI : UIPanel
    {
        public static readonly string cacheName = "EcnomicUI";

        public static float WIDTH = 550f;

        private static readonly float HEIGHT = 500f;

        private static readonly float HEADER = 40f;

        private static readonly float SPACING = 17f;

        private static readonly float SPACING22 = 23f;

        private ItemClass.Availability CurrentMode;

        public static EcnomicUI instance;

        private Dictionary<string, UILabel> _valuesControlContainer = new Dictionary<string, UILabel>(16);

        private UIDragHandle m_DragHandler;

        private UIButton m_closeButton;

        private UILabel m_title;

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


        //private UILabel citizen_salary_total;
        private UILabel citizen_salary_tax_per_family;


        //1.2 citizen expense
        //1.2.1 citizen expense
        //public static long citizen_expense_per_family = 0;
        //public static long citizen_expense = 0;
        private UILabel citizen_expense_per_family;
        //private UILabel citizen_expense;

        //1.2.2 transport fee
        //public static uint total_citizen_vehical_time = 0;
        //public static long public_transport_fee = 0;
        //public static long all_transport_fee = 0;
        //public static byte citizen_average_transport_fee = 0;
        //private UILabel total_citizen_vehical_time;
        //private UILabel public_transport_fee;
        private UILabel citizen_average_transport_fee;

        //1.3 income - expense
        //public static int family_profit_money_num = 0;
        //public static int family_loss_money_num = 0;
        private UILabel family_profit_money_num;
        private UILabel family_loss_money_num;
        private UILabel family_very_profit_num;
        private UILabel family_weight_stable_high;
        private UILabel family_weight_stable_low;

        private UILabel family_weight_stable_medium;
        private UILabel family_satisfactios_of_goods;

        //private UILabel city_insurance_account;

        //2 building   27 element
        private UILabel m_secondline_building; //fixed title
        private UILabel office_gen_salary_index;
        private UILabel office_high_tech_salary_index;

        private UILabel tip1;
        private UILabel tip2;
        private UILabel tip3;
        private UILabel tip4;
        private UILabel tip5;
        private UILabel tip6;
        private UILabel tip7;
        //private UILabel tip8;
        //private UILabel tip9;
        //private UILabel tip10;
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
            base.relativePosition = new Vector3(200f, 150f);
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
            this.ShowOnGui();
            this.RefreshDisplayData();
        }

        private void ShowOnGui()
        {
            //citizen
            this.m_firstline_citizen = base.AddUIComponent<UILabel>();
            this.m_firstline_citizen.text = language.EconomicUI[1];
            this.m_firstline_citizen.textScale = 1.1f;
            this.m_firstline_citizen.tooltip = "N/A";
            this.m_firstline_citizen.relativePosition = new Vector3(SPACING, 50f);
            this.m_firstline_citizen.autoSize = true;

            //data
            this.citizen_count = base.AddUIComponent<UILabel>();
            this.citizen_count.text = language.EconomicUI[2];
            this.citizen_count.tooltip = language.EconomicUI[3];
            this.citizen_count.relativePosition = new Vector3(SPACING, this.m_firstline_citizen.relativePosition.y + SPACING22);
            this.citizen_count.autoSize = true;
            this.citizen_count.name = "Moreeconomic_Text_0";

            this.family_count = base.AddUIComponent<UILabel>();
            this.family_count.text = language.EconomicUI[4];
            this.family_count.tooltip = language.EconomicUI[5];
            this.family_count.relativePosition = new Vector3(this.citizen_count.relativePosition.x + this.citizen_count.width + SPACING + 140f, this.citizen_count.relativePosition.y);
            this.family_count.autoSize = true;
            this.family_count.name = "Moreeconomic_Text_1";

            this.citizen_salary_per_family = base.AddUIComponent<UILabel>();
            this.citizen_salary_per_family.text = language.EconomicUI[6];
            this.citizen_salary_per_family.tooltip = language.EconomicUI[7];
            this.citizen_salary_per_family.relativePosition = new Vector3(this.family_count.relativePosition.x + this.family_count.width + SPACING + 110f, this.family_count.relativePosition.y);
            this.citizen_salary_per_family.autoSize = true;
            this.citizen_salary_per_family.name = "Moreeconomic_Text_2";

            this.citizen_salary_tax_per_family = base.AddUIComponent<UILabel>();
            this.citizen_salary_tax_per_family.text = language.EconomicUI[8];
            this.citizen_salary_tax_per_family.tooltip = language.EconomicUI[9];
            this.citizen_salary_tax_per_family.relativePosition = new Vector3(SPACING, this.citizen_count.relativePosition.y + SPACING22);
            this.citizen_salary_tax_per_family.autoSize = true;
            this.citizen_salary_tax_per_family.name = "Moreeconomic_Text_4";

            this.citizen_expense_per_family = base.AddUIComponent<UILabel>();
            this.citizen_expense_per_family.text = language.EconomicUI[10];
            this.citizen_expense_per_family.tooltip = language.EconomicUI[11];
            this.citizen_expense_per_family.relativePosition = new Vector3(this.family_count.relativePosition.x, this.family_count.relativePosition.y + SPACING22);
            this.citizen_expense_per_family.autoSize = true;
            this.citizen_expense_per_family.name = "Moreeconomic_Text_5";

            this.citizen_average_transport_fee = base.AddUIComponent<UILabel>();
            this.citizen_average_transport_fee.text = language.EconomicUI[12];
            this.citizen_average_transport_fee.tooltip = language.EconomicUI[13];
            this.citizen_average_transport_fee.relativePosition = new Vector3(this.citizen_salary_per_family.relativePosition.x, this.citizen_salary_per_family.relativePosition.y + SPACING22);
            this.citizen_average_transport_fee.autoSize = true;
            this.citizen_average_transport_fee.name = "Moreeconomic_Text_14";

            /*this.public_transport_fee = base.AddUIComponent<UILabel>();
            this.public_transport_fee.text =         string.Format("public_trans_fee [0000]");
            this.public_transport_fee.tooltip = language.EconomicUI[15];
            this.public_transport_fee.relativePosition = new Vector3(SPACING, this.citizen_salary_total.relativePosition.y + SPACING22);
            this.public_transport_fee.autoSize = true;
            this.public_transport_fee.name = "Moreeconomic_Text_7";

            this.total_citizen_vehical_time = base.AddUIComponent<UILabel>();
            this.total_citizen_vehical_time.text =   string.Format("citizen_vehical_time [00000000]");
            this.total_citizen_vehical_time.tooltip = language.EconomicUI[17];
            this.total_citizen_vehical_time.relativePosition = new Vector3(this.public_transport_fee.relativePosition.x + this.public_transport_fee.width + SPACING, this.public_transport_fee.relativePosition.y);
            this.total_citizen_vehical_time.autoSize = true;
            this.total_citizen_vehical_time.name = "Moreeconomic_Text_8";*/

            this.family_very_profit_num = base.AddUIComponent<UILabel>();
            this.family_very_profit_num.text = language.EconomicUI[18];
            this.family_very_profit_num.tooltip = language.EconomicUI[19];
            this.family_very_profit_num.relativePosition = new Vector3(SPACING, this.citizen_salary_tax_per_family.relativePosition.y + SPACING22);
            //this.m_money_forest.relativePosition = new Vector3(this.m_money_farmer.relativePosition.x + this.m_money_farmer.width + SPACING, this.m_money_farmer.relativePosition.y);
            this.family_very_profit_num.autoSize = true;
            this.family_very_profit_num.name = "Moreeconomic_Text_11";

            this.family_profit_money_num = base.AddUIComponent<UILabel>();
            this.family_profit_money_num.text = language.EconomicUI[20];
            this.family_profit_money_num.tooltip = language.EconomicUI[21];
            this.family_profit_money_num.relativePosition = new Vector3(this.citizen_expense_per_family.relativePosition.x, this.citizen_expense_per_family.relativePosition.y + SPACING22);
            this.family_profit_money_num.autoSize = true;
            this.family_profit_money_num.name = "Moreeconomic_Text_9";

            this.family_loss_money_num = base.AddUIComponent<UILabel>();
            this.family_loss_money_num.text = language.EconomicUI[22];
            this.family_loss_money_num.tooltip = language.EconomicUI[23];
            this.family_loss_money_num.relativePosition = new Vector3(this.citizen_average_transport_fee.relativePosition.x, this.citizen_average_transport_fee.relativePosition.y + SPACING22);
            this.family_loss_money_num.autoSize = true;
            this.family_loss_money_num.name = "Moreeconomic_Text_10";

            this.family_weight_stable_high = base.AddUIComponent<UILabel>();
            this.family_weight_stable_high.text = language.EconomicUI[24];
            this.family_weight_stable_high.tooltip = language.EconomicUI[25];
            this.family_weight_stable_high.relativePosition = new Vector3(SPACING, this.family_very_profit_num.relativePosition.y + SPACING22);
            this.family_weight_stable_high.autoSize = true;
            this.family_weight_stable_high.name = "Moreeconomic_Text_12";

            this.family_weight_stable_medium = base.AddUIComponent<UILabel>();
            this.family_weight_stable_medium.text = language.EconomicUI[26];
            this.family_weight_stable_medium.tooltip = language.EconomicUI[27];
            this.family_weight_stable_medium.relativePosition = new Vector3(this.family_profit_money_num.relativePosition.x, this.family_profit_money_num.relativePosition.y + SPACING22);
            this.family_weight_stable_medium.autoSize = true;
            this.family_weight_stable_medium.name = "Moreeconomic_Text_44";

            this.family_weight_stable_low = base.AddUIComponent<UILabel>();
            this.family_weight_stable_low.text = language.EconomicUI[28];
            this.family_weight_stable_low.tooltip = language.EconomicUI[29];
            this.family_weight_stable_low.relativePosition = new Vector3(this.family_loss_money_num.relativePosition.x, this.family_loss_money_num.relativePosition.y + SPACING22);
            //this.m_money_forest.relativePosition = new Vector3(this.m_money_farmer.relativePosition.x + this.m_money_farmer.width + SPACING, this.m_money_farmer.relativePosition.y);
            this.family_weight_stable_low.autoSize = true;
            this.family_weight_stable_low.name = "Moreeconomic_Text_13";

            this.family_satisfactios_of_goods = base.AddUIComponent<UILabel>();
            this.family_satisfactios_of_goods.text = language.EconomicUI[30];
            this.family_satisfactios_of_goods.tooltip = language.EconomicUI[31];
            this.family_satisfactios_of_goods.relativePosition = new Vector3(SPACING, this.family_weight_stable_high.relativePosition.y + SPACING22); ;
            //this.m_money_forest.relativePosition = new Vector3(this.m_money_farmer.relativePosition.x + this.m_money_farmer.width + SPACING, this.m_money_farmer.relativePosition.y);
            this.family_satisfactios_of_goods.autoSize = true;
            this.family_satisfactios_of_goods.name = "Moreeconomic_Text_45";

            //building
            this.m_secondline_building = base.AddUIComponent<UILabel>();
            this.m_secondline_building.text = language.EconomicUI[32];
            this.m_secondline_building.tooltip = "N/A";
            this.m_secondline_building.textScale = 1.1f;
            this.m_secondline_building.relativePosition = new Vector3(SPACING, this.family_satisfactios_of_goods.relativePosition.y + SPACING22 + 10f);
            this.m_secondline_building.autoSize = true;

            this.office_gen_salary_index = base.AddUIComponent<UILabel>();
            this.office_gen_salary_index.text = language.EconomicUI[33];
            this.office_gen_salary_index.tooltip = language.EconomicUI[34];
            this.office_gen_salary_index.relativePosition = new Vector3(SPACING, this.m_secondline_building.relativePosition.y + SPACING22);
            this.office_gen_salary_index.autoSize = true;
            this.office_gen_salary_index.name = "Moreeconomic_Text_42";

            this.office_high_tech_salary_index = base.AddUIComponent<UILabel>();
            this.office_high_tech_salary_index.text = language.EconomicUI[35];
            this.office_high_tech_salary_index.tooltip = language.EconomicUI[36];
            this.office_high_tech_salary_index.relativePosition = new Vector3(this.office_gen_salary_index.relativePosition.x + 100f + this.office_gen_salary_index.width, this.office_gen_salary_index.relativePosition.y);
            this.office_high_tech_salary_index.autoSize = true;
            this.office_high_tech_salary_index.name = "Moreeconomic_Text_43";

            this.tip1 = base.AddUIComponent<UILabel>();
            this.tip1.text = language.EconomicUI[37];
            this.tip1.tooltip = language.EconomicUI[38];
            this.tip1.relativePosition = new Vector3(SPACING, this.office_gen_salary_index.relativePosition.y + SPACING22 + 10f);
            this.tip1.autoSize = true;
            this.tip1.name = "Moreeconomic_Text_49";

            this.tip2 = base.AddUIComponent<UILabel>();
            this.tip2.text = language.EconomicUI[39];
            this.tip2.tooltip = language.EconomicUI[40];
            this.tip2.relativePosition = new Vector3(SPACING, this.tip1.relativePosition.y + SPACING22);
            this.tip2.autoSize = true;
            this.tip2.name = "Moreeconomic_Text_50";

            this.tip3 = base.AddUIComponent<UILabel>();
            this.tip3.text = language.EconomicUI[41];
            this.tip3.tooltip = language.EconomicUI[42];
            this.tip3.relativePosition = new Vector3(SPACING, this.tip2.relativePosition.y + SPACING22);
            this.tip3.autoSize = true;
            this.tip3.name = "Moreeconomic_Text_51";

            this.tip4 = base.AddUIComponent<UILabel>();
            this.tip4.text = language.EconomicUI[43];
            this.tip4.tooltip = language.EconomicUI[44];
            this.tip4.relativePosition = new Vector3(SPACING, this.tip3.relativePosition.y + SPACING22);
            this.tip4.autoSize = true;
            this.tip4.name = "Moreeconomic_Text_52";

            this.tip5 = base.AddUIComponent<UILabel>();
            this.tip5.text = language.EconomicUI[45];
            this.tip5.tooltip = language.EconomicUI[46];
            this.tip5.relativePosition = new Vector3(SPACING, this.tip4.relativePosition.y + SPACING22);
            this.tip5.autoSize = true;
            this.tip5.name = "Moreeconomic_Text_53";

            this.tip6 = base.AddUIComponent<UILabel>();
            this.tip6.text = language.EconomicUI[47];
            this.tip6.tooltip = language.EconomicUI[48];
            this.tip6.relativePosition = new Vector3(SPACING, this.tip5.relativePosition.y + SPACING22);
            this.tip6.autoSize = true;
            this.tip6.name = "Moreeconomic_Text_50";

            this.tip7 = base.AddUIComponent<UILabel>();
            this.tip7.text = language.EconomicUI[49];
            this.tip7.tooltip = language.EconomicUI[50];
            this.tip7.relativePosition = new Vector3(SPACING, this.tip6.relativePosition.y + SPACING22);
            this.tip7.autoSize = true;
            this.tip7.name = "Moreeconomic_Text_51";

            /*this.tip8 = base.AddUIComponent<UILabel>();
            this.tip8.text = language.EconomicUI[51];
            this.tip8.tooltip = language.EconomicUI[52];
            this.tip8.relativePosition = new Vector3(SPACING, this.tip7.relativePosition.y + SPACING22);
            this.tip8.autoSize = true;
            this.tip8.name = "Moreeconomic_Text_52";

            this.tip9 = base.AddUIComponent<UILabel>();
            this.tip9.text = language.EconomicUI[53];
            this.tip9.tooltip = language.EconomicUI[54];
            this.tip9.relativePosition = new Vector3(SPACING, this.tip8.relativePosition.y + SPACING22);
            this.tip9.autoSize = true;
            this.tip9.name = "Moreeconomic_Text_53";

            this.tip10 = base.AddUIComponent<UILabel>();
            this.tip10.text = language.EconomicUI[55];
            this.tip10.tooltip = language.EconomicUI[55];
            this.tip10.relativePosition = new Vector3(SPACING, this.tip9.relativePosition.y + SPACING22);
            this.tip10.autoSize = true;
            this.tip10.name = "Moreeconomic_Text_53";*/
        }


        private void RefreshDisplayData()
        {
            uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
            uint num2 = currentFrameIndex & 255u;

            if (refeshOnce)
            {
                if (base.isVisible)
                {
                    //citizen
                    this.m_title.text = language.EconomicUI[0];
                    this.m_firstline_citizen.text = language.EconomicUI[1];
                    this.citizen_count.text = string.Format(language.EconomicUI[2] + " [{0}]", comm_data.citizen_count);
                    this.family_count.text = string.Format(language.EconomicUI[4] + " [{0}]", comm_data.family_count);
                    this.citizen_salary_per_family.text = string.Format(language.EconomicUI[6] + " [{0}]", comm_data.citizen_salary_per_family);

                    if (comm_data.family_count != 0)
                    {
                        this.citizen_salary_tax_per_family.text = string.Format(language.EconomicUI[8] + " [{0}]", comm_data.citizen_salary_tax_total / comm_data.family_count);
                    }
                    this.citizen_expense_per_family.text = string.Format(language.EconomicUI[10] + " [{0}]", comm_data.citizen_expense_per_family);
                    this.citizen_average_transport_fee.text = string.Format(language.EconomicUI[12] + " [{0}]", comm_data.citizen_average_transport_fee);

                    //this.public_transport_fee.text = string.Format(language.EconomicUI[16] + " [{0}]", comm_data.public_transport_fee * comm_data.game_income_expense_multiple);
                    //this.total_citizen_vehical_time.text = string.Format(language.EconomicUI[18] + " [{0}]", comm_data.temp_total_citizen_vehical_time_last);
                    this.family_very_profit_num.text = string.Format(language.EconomicUI[18] + " [{0}]", comm_data.family_very_profit_money_num);
                    this.family_profit_money_num.text = string.Format(language.EconomicUI[20] + " [{0}]", comm_data.family_profit_money_num);
                    this.family_loss_money_num.text = string.Format(language.EconomicUI[22] + " [{0}]", comm_data.family_loss_money_num);
                    this.family_weight_stable_high.text = string.Format(language.EconomicUI[24] + " [{0}]", comm_data.family_weight_stable_high);
                    this.family_weight_stable_medium.text = string.Format(language.EconomicUI[26] + " [{0}]", comm_data.family_count - comm_data.family_weight_stable_high - comm_data.family_weight_stable_low);
                    this.family_weight_stable_low.text = string.Format(language.EconomicUI[28] + " [{0}]", comm_data.family_weight_stable_low);
                    if (comm_data.family_count != 0)
                    {
                        this.family_satisfactios_of_goods.text = string.Format(language.EconomicUI[30] + " [{0:N3}%]", (float)(pc_ResidentAI.citizenGoods / (comm_data.family_count * 2f)));
                    }
                    else
                    {
                        this.family_satisfactios_of_goods.text = string.Format(language.EconomicUI[30] + " [{0:N3}%]", 0);
                    }

                    //building
                    this.m_secondline_building.text = language.EconomicUI[32];
                    this.office_gen_salary_index.text = string.Format(language.EconomicUI[34] + " [{0}]", pc_PrivateBuildingAI.greaterThan20000ProfitBuildingCountFinal);
                    this.office_high_tech_salary_index.text = string.Format(language.EconomicUI[36] + " [{0}]", pc_PrivateBuildingAI.greaterThan20000ProfitBuildingMoneyFinal);

                    this.tip1.text = string.Format(language.EconomicUI[38] + "  " + RealCity.tip1_message_forgui);
                    this.tip2.text = string.Format(language.EconomicUI[40] + "  " + RealCity.tip2_message_forgui);
                    this.tip3.text = string.Format(language.EconomicUI[42] + "  " + RealCity.tip3_message_forgui);
                    this.tip4.text = string.Format(language.EconomicUI[44] + "  " + RealCity.tip4_message_forgui);
                    this.tip5.text = string.Format(language.EconomicUI[46] + "  " + RealCity.tip5_message_forgui);
                    this.tip6.text = string.Format(language.EconomicUI[48] + "  " + RealCity.tip6_message_forgui);
                    this.tip7.text = string.Format(language.EconomicUI[50] + "  " + RealCity.tip7_message_forgui);
                    /*this.tip8.text = string.Format(language.EconomicUI[52] + "  " + RealCity.tip8_message_forgui);
                    this.tip9.text = string.Format(language.EconomicUI[54] + "  " + RealCity.tip9_message_forgui);
                    this.tip10.text = string.Format(language.EconomicUI[55] + "  " + RealCity.tip10_message_forgui);*/
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
