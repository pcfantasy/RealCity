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

        //private UILabel city_insurance_account;

        //2 building   27 element
        private UILabel m_secondline_building; //fixed title
        private UILabel profitBuildingCount;
        private UILabel externalInvestments;

        private UILabel tip1;
        private UILabel tip2;
        private UILabel tip3;
        private UILabel tip4;
        private UILabel tip5;
        private UILabel tip6;


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
            this.m_title.text = Language.EconomicUI[0];
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
            this.m_firstline_citizen.text = Language.EconomicUI[1];
            this.m_firstline_citizen.textScale = 1.1f;
            this.m_firstline_citizen.relativePosition = new Vector3(SPACING, 50f);
            this.m_firstline_citizen.autoSize = true;

            //data
            this.citizen_count = base.AddUIComponent<UILabel>();
            this.citizen_count.text = Language.EconomicUI[2];
            this.citizen_count.relativePosition = new Vector3(SPACING, this.m_firstline_citizen.relativePosition.y + SPACING22);
            this.citizen_count.autoSize = true;
            this.citizen_count.name = "Moreeconomic_Text_0";

            this.family_count = base.AddUIComponent<UILabel>();
            this.family_count.text = Language.EconomicUI[3];
            this.family_count.relativePosition = new Vector3(this.citizen_count.relativePosition.x + this.citizen_count.width + SPACING + 140f, this.citizen_count.relativePosition.y);
            this.family_count.autoSize = true;
            this.family_count.name = "Moreeconomic_Text_1";

            this.citizen_salary_per_family = base.AddUIComponent<UILabel>();
            this.citizen_salary_per_family.text = Language.EconomicUI[4];
            this.citizen_salary_per_family.relativePosition = new Vector3(this.family_count.relativePosition.x + this.family_count.width + SPACING + 110f, this.family_count.relativePosition.y);
            this.citizen_salary_per_family.autoSize = true;
            this.citizen_salary_per_family.name = "Moreeconomic_Text_2";

            this.citizen_salary_tax_per_family = base.AddUIComponent<UILabel>();
            this.citizen_salary_tax_per_family.text = Language.EconomicUI[5];
            this.citizen_salary_tax_per_family.relativePosition = new Vector3(SPACING, this.citizen_count.relativePosition.y + SPACING22);
            this.citizen_salary_tax_per_family.autoSize = true;
            this.citizen_salary_tax_per_family.name = "Moreeconomic_Text_4";

            this.citizen_expense_per_family = base.AddUIComponent<UILabel>();
            this.citizen_expense_per_family.text = Language.EconomicUI[6];
            this.citizen_expense_per_family.relativePosition = new Vector3(this.family_count.relativePosition.x, this.family_count.relativePosition.y + SPACING22);
            this.citizen_expense_per_family.autoSize = true;
            this.citizen_expense_per_family.name = "Moreeconomic_Text_5";

            this.citizen_average_transport_fee = base.AddUIComponent<UILabel>();
            this.citizen_average_transport_fee.text = Language.EconomicUI[7];
            this.citizen_average_transport_fee.relativePosition = new Vector3(this.citizen_salary_per_family.relativePosition.x, this.citizen_salary_per_family.relativePosition.y + SPACING22);
            this.citizen_average_transport_fee.autoSize = true;
            this.citizen_average_transport_fee.name = "Moreeconomic_Text_14";

            this.family_very_profit_num = base.AddUIComponent<UILabel>();
            this.family_very_profit_num.text = Language.EconomicUI[8];
            this.family_very_profit_num.relativePosition = new Vector3(SPACING, this.citizen_salary_tax_per_family.relativePosition.y + SPACING22);
            this.family_very_profit_num.autoSize = true;
            this.family_very_profit_num.name = "Moreeconomic_Text_11";

            this.family_profit_money_num = base.AddUIComponent<UILabel>();
            this.family_profit_money_num.text = Language.EconomicUI[9];
            this.family_profit_money_num.relativePosition = new Vector3(this.citizen_expense_per_family.relativePosition.x, this.citizen_expense_per_family.relativePosition.y + SPACING22);
            this.family_profit_money_num.autoSize = true;
            this.family_profit_money_num.name = "Moreeconomic_Text_9";

            this.family_loss_money_num = base.AddUIComponent<UILabel>();
            this.family_loss_money_num.text = Language.EconomicUI[10];
            this.family_loss_money_num.relativePosition = new Vector3(this.citizen_average_transport_fee.relativePosition.x, this.citizen_average_transport_fee.relativePosition.y + SPACING22);
            this.family_loss_money_num.autoSize = true;
            this.family_loss_money_num.name = "Moreeconomic_Text_10";

            this.family_weight_stable_high = base.AddUIComponent<UILabel>();
            this.family_weight_stable_high.text = Language.EconomicUI[11];
            this.family_weight_stable_high.relativePosition = new Vector3(SPACING, this.family_very_profit_num.relativePosition.y + SPACING22);
            this.family_weight_stable_high.autoSize = true;
            this.family_weight_stable_high.name = "Moreeconomic_Text_12";

            this.family_weight_stable_medium = base.AddUIComponent<UILabel>();
            this.family_weight_stable_medium.text = Language.EconomicUI[12];
            this.family_weight_stable_medium.relativePosition = new Vector3(this.family_profit_money_num.relativePosition.x, this.family_profit_money_num.relativePosition.y + SPACING22);
            this.family_weight_stable_medium.autoSize = true;
            this.family_weight_stable_medium.name = "Moreeconomic_Text_44";

            this.family_weight_stable_low = base.AddUIComponent<UILabel>();
            this.family_weight_stable_low.text = Language.EconomicUI[13];
            this.family_weight_stable_low.relativePosition = new Vector3(this.family_loss_money_num.relativePosition.x, this.family_loss_money_num.relativePosition.y + SPACING22);
            this.family_weight_stable_low.autoSize = true;
            this.family_weight_stable_low.name = "Moreeconomic_Text_13";

            //building
            this.m_secondline_building = base.AddUIComponent<UILabel>();
            this.m_secondline_building.text = Language.EconomicUI[14];
            this.m_secondline_building.textScale = 1.1f;
            this.m_secondline_building.relativePosition = new Vector3(SPACING, this.family_weight_stable_high.relativePosition.y + SPACING22 + 10f);
            this.m_secondline_building.autoSize = true;

            this.profitBuildingCount = base.AddUIComponent<UILabel>();
            this.profitBuildingCount.text = Language.EconomicUI[15];
            this.profitBuildingCount.relativePosition = new Vector3(SPACING, this.m_secondline_building.relativePosition.y + SPACING22);
            this.profitBuildingCount.autoSize = true;
            this.profitBuildingCount.name = "Moreeconomic_Text_42";

            this.externalInvestments = base.AddUIComponent<UILabel>();
            this.externalInvestments.text = Language.EconomicUI[16];
            this.externalInvestments.relativePosition = new Vector3(this.profitBuildingCount.relativePosition.x + 100f + this.profitBuildingCount.width, this.profitBuildingCount.relativePosition.y);
            this.externalInvestments.autoSize = true;
            this.externalInvestments.name = "Moreeconomic_Text_43";

            this.tip1 = base.AddUIComponent<UILabel>();
            this.tip1.text = Language.EconomicUI[17];
            this.tip1.relativePosition = new Vector3(SPACING, this.profitBuildingCount.relativePosition.y + SPACING22 + 10f);
            this.tip1.autoSize = true;
            this.tip1.name = "Moreeconomic_Text_49";

            this.tip2 = base.AddUIComponent<UILabel>();
            this.tip2.text = Language.EconomicUI[18];
            this.tip2.relativePosition = new Vector3(SPACING, this.tip1.relativePosition.y + SPACING22);
            this.tip2.autoSize = true;
            this.tip2.name = "Moreeconomic_Text_50";

            this.tip3 = base.AddUIComponent<UILabel>();
            this.tip3.text = Language.EconomicUI[19];
            this.tip3.relativePosition = new Vector3(SPACING, this.tip2.relativePosition.y + SPACING22);
            this.tip3.autoSize = true;
            this.tip3.name = "Moreeconomic_Text_51";

            this.tip4 = base.AddUIComponent<UILabel>();
            this.tip4.text = Language.EconomicUI[20];
            this.tip4.relativePosition = new Vector3(SPACING, this.tip3.relativePosition.y + SPACING22);
            this.tip4.autoSize = true;
            this.tip4.name = "Moreeconomic_Text_52";

            this.tip5 = base.AddUIComponent<UILabel>();
            this.tip5.text = Language.EconomicUI[21];
            this.tip5.relativePosition = new Vector3(SPACING, this.tip4.relativePosition.y + SPACING22);
            this.tip5.autoSize = true;
            this.tip5.name = "Moreeconomic_Text_53";

            this.tip6 = base.AddUIComponent<UILabel>();
            this.tip6.text = Language.EconomicUI[22];
            this.tip6.relativePosition = new Vector3(SPACING, this.tip5.relativePosition.y + SPACING22);
            this.tip6.autoSize = true;
            this.tip6.name = "Moreeconomic_Text_50";
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
                    this.m_title.text = Language.EconomicUI[0];
                    this.m_firstline_citizen.text = Language.EconomicUI[1];
                    this.citizen_count.text = string.Format(Language.EconomicUI[2] + " [{0}]", MainDataStore.citizen_count);
                    this.family_count.text = string.Format(Language.EconomicUI[3] + " [{0}]", MainDataStore.family_count);
                    this.citizen_salary_per_family.text = string.Format(Language.EconomicUI[4] + " [{0}]", MainDataStore.citizen_salary_per_family);

                    if (MainDataStore.family_count != 0)
                    {
                        this.citizen_salary_tax_per_family.text = string.Format(Language.EconomicUI[5] + " [{0}]", MainDataStore.citizen_salary_tax_total / MainDataStore.family_count);
                    }
                    this.citizen_expense_per_family.text = string.Format(Language.EconomicUI[6] + " [{0}]", MainDataStore.citizen_expense_per_family);
                    this.citizen_average_transport_fee.text = string.Format(Language.EconomicUI[7] + " [{0}]", MainDataStore.citizen_average_transport_fee);

                    //this.public_transport_fee.text = string.Format(language.EconomicUI[16] + " [{0}]", comm_data.public_transport_fee * comm_data.game_income_expense_multiple);
                    //this.total_citizen_vehical_time.text = string.Format(language.EconomicUI[18] + " [{0}]", comm_data.temp_total_citizen_vehical_time_last);
                    this.family_very_profit_num.text = string.Format(Language.EconomicUI[8] + " [{0}]", MainDataStore.family_very_profit_money_num);
                    this.family_profit_money_num.text = string.Format(Language.EconomicUI[9] + " [{0}]", MainDataStore.family_profit_money_num);
                    this.family_loss_money_num.text = string.Format(Language.EconomicUI[10] + " [{0}]", MainDataStore.family_loss_money_num);
                    this.family_weight_stable_high.text = string.Format(Language.EconomicUI[11] + " [{0}]", MainDataStore.family_weight_stable_high);
                    this.family_weight_stable_medium.text = string.Format(Language.EconomicUI[12] + " [{0}]", MainDataStore.family_count - MainDataStore.family_weight_stable_high - MainDataStore.family_weight_stable_low);
                    this.family_weight_stable_low.text = string.Format(Language.EconomicUI[13] + " [{0}]", MainDataStore.family_weight_stable_low);

                    //building
                    this.m_secondline_building.text = Language.EconomicUI[14];
                    this.profitBuildingCount.text = string.Format(Language.EconomicUI[15] + " [{0}]", RealCityPrivateBuildingAI.greaterThan20000ProfitBuildingCountFinal);
                    this.externalInvestments.text = string.Format(Language.EconomicUI[16] + " [{0}]", RealCityPrivateBuildingAI.greaterThan20000ProfitBuildingMoneyFinal);

                    this.tip1.text = string.Format(Language.EconomicUI[17] + "  " + RealCityEconomyExtension.tip1_message_forgui);
                    this.tip2.text = string.Format(Language.EconomicUI[18] + "  " + RealCityEconomyExtension.tip2_message_forgui);
                    this.tip3.text = string.Format(Language.EconomicUI[19] + "  " + RealCityEconomyExtension.tip3_message_forgui);
                    this.tip4.text = string.Format(Language.EconomicUI[20] + "  " + RealCityEconomyExtension.tip4_message_forgui);
                    this.tip5.text = string.Format(Language.EconomicUI[21] + "  " + RealCityEconomyExtension.tip5_message_forgui);
                    this.tip6.text = string.Format(Language.EconomicUI[22] + "  " + RealCityEconomyExtension.tip6_message_forgui);

                    if (!MainDataStore.haveCityResourceDepartmentFinal)
                    {
                        if (this.tip3.textColor == Color.red)
                        {
                            this.tip3.textColor = Color.white;
                        }
                        else
                        {
                            this.tip3.textColor = Color.red;
                        }
                    }

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
