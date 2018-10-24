using System.Collections.Generic;
using ColossalFramework;
using ColossalFramework.UI;
using UnityEngine;
using System.Collections;

namespace RealCity
{
    public class PoliticsUI : UIPanel
    {
        public static readonly string cacheName = "PoliticsUI";

        public static float WIDTH = 850f;

        private static readonly float HEIGHT = 700f;

        private static readonly float HEADER = 40f;

        private static readonly float SPACING = 17f;

        private static readonly float SPACING22 = 23f;

        private ItemClass.Availability CurrentMode;

        public static PoliticsUI instance;

        private Dictionary<string, UILabel> _valuesControlContainer = new Dictionary<string, UILabel>(16);

        private UIDragHandle m_DragHandler;

        private UIButton m_closeButton;

        private UILabel m_title;

        private UILabel parliamentSeats;
        private UILabel communist;
        private UILabel green;
        private UILabel socialist;
        private UILabel liberal;
        private UILabel national;

        private UILabel goverment;

        private UILabel polls;
        private UILabel communistPolls;
        private UILabel greenPolls;
        private UILabel socialistPolls;
        private UILabel liberalPolls;
        private UILabel nationalPolls;
        private UILabel nextVote;
        private UILabel nextMeeting;
        private UILabel currentMeetingItem;
        private UILabel voteResult;
        private UILabel currentPolitics;

        private UILabel salary;
        private UILabel benefit;
        private UILabel trade;
        private UILabel import;
        private UILabel stateOwned;
        private UILabel garbage;
        private UILabel landRent;

        public static UICheckBox riseTradeTax_Checkbox;
        private UILabel riseTradeTax;
        public static UICheckBox fallTradeTax_Checkbox;
        private UILabel fallTradeTax;

        public static UICheckBox riseImportTax_Checkbox;
        private UILabel riseImportTax;
        public static UICheckBox fallImportTax_Checkbox;
        private UILabel fallImportTax;

        public static UICheckBox fallLandTax_Checkbox;
        private UILabel fallLandTax;


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
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 + 50f), 150f);
            base.opacity = 1f;
            base.cachedName = cacheName;
            this.CurrentMode = Singleton<ToolManager>.instance.m_properties.m_mode;
            this.m_DragHandler = base.AddUIComponent<UIDragHandle>();
            this.m_DragHandler.target = this;
            this.m_title = base.AddUIComponent<UILabel>();
            this.m_title.text = Language.PoliticsMessage[0];
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
            this.parliamentSeats = base.AddUIComponent<UILabel>();
            this.parliamentSeats.text = Language.PoliticsMessage[1];
            this.parliamentSeats.textScale = 1.1f;
            this.parliamentSeats.tooltip = "N/A";
            this.parliamentSeats.relativePosition = new Vector3(SPACING, 50f);
            this.parliamentSeats.autoSize = true;

            //data
            this.communist = base.AddUIComponent<UILabel>();
            this.communist.text = Language.PoliticsMessage[2];
            this.communist.tooltip = Language.PoliticsMessage[2];
            this.communist.relativePosition = new Vector3(SPACING, this.parliamentSeats.relativePosition.y + SPACING22);
            this.communist.autoSize = true;
            this.communist.name = "Moreeconomic_Text_0";

            this.green = base.AddUIComponent<UILabel>();
            this.green.text = Language.PoliticsMessage[3];
            this.green.tooltip = Language.PoliticsMessage[3];
            this.green.relativePosition = new Vector3(this.communist.relativePosition.x + this.communist.width + SPACING + 70f, this.communist.relativePosition.y);
            this.green.autoSize = true;
            this.green.name = "Moreeconomic_Text_1";

            this.socialist = base.AddUIComponent<UILabel>();
            this.socialist.text = Language.PoliticsMessage[4];
            this.socialist.tooltip = Language.PoliticsMessage[4];
            this.socialist.relativePosition = new Vector3(this.green.relativePosition.x + this.green.width + SPACING + 70f, this.green.relativePosition.y);
            this.socialist.autoSize = true;
            this.socialist.name = "Moreeconomic_Text_2";

            this.liberal = base.AddUIComponent<UILabel>();
            this.liberal.text = Language.PoliticsMessage[5];
            this.liberal.tooltip = Language.PoliticsMessage[5];
            this.liberal.relativePosition = new Vector3(this.socialist.relativePosition.x + this.socialist.width + SPACING + 70f, this.socialist.relativePosition.y);
            this.liberal.autoSize = true;
            this.liberal.name = "Moreeconomic_Text_2";

            this.national = base.AddUIComponent<UILabel>();
            this.national.text = Language.PoliticsMessage[6];
            this.national.tooltip = Language.PoliticsMessage[6];
            this.national.relativePosition = new Vector3(this.liberal.relativePosition.x + this.liberal.width + SPACING + 70f, this.liberal.relativePosition.y);
            this.national.autoSize = true;
            this.national.name = "Moreeconomic_Text_2";


            goverment = base.AddUIComponent<UILabel>();
            goverment.text = Language.PoliticsMessage[7];
            goverment.tooltip = Language.PoliticsMessage[7];
            goverment.relativePosition = new Vector3(SPACING, this.communist.relativePosition.y + SPACING22 + 20f);
            goverment.autoSize = true;
            goverment.name = "Moreeconomic_Text_0";

            //citizen
            this.polls = base.AddUIComponent<UILabel>();
            this.polls.text = Language.PoliticsMessage[19];
            this.polls.textScale = 1.1f;
            this.polls.tooltip = "N/A";
            this.polls.relativePosition = new Vector3(SPACING, goverment.relativePosition.y + SPACING22 + 20f);
            this.polls.autoSize = true;

            //data
            this.communistPolls = base.AddUIComponent<UILabel>();
            this.communistPolls.text = Language.PoliticsMessage[2];
            this.communistPolls.tooltip = Language.PoliticsMessage[2];
            this.communistPolls.relativePosition = new Vector3(SPACING, this.polls.relativePosition.y + SPACING22);
            this.communistPolls.autoSize = true;
            this.communistPolls.name = "Moreeconomic_Text_0";

            this.greenPolls = base.AddUIComponent<UILabel>();
            this.greenPolls.text = Language.PoliticsMessage[3];
            this.greenPolls.tooltip = Language.PoliticsMessage[3];
            this.greenPolls.relativePosition = new Vector3(this.communistPolls.relativePosition.x + this.communistPolls.width + SPACING + 70f, this.communistPolls.relativePosition.y);
            this.greenPolls.autoSize = true;
            this.greenPolls.name = "Moreeconomic_Text_1";

            this.socialistPolls = base.AddUIComponent<UILabel>();
            this.socialistPolls.text = Language.PoliticsMessage[4];
            this.socialistPolls.tooltip = Language.PoliticsMessage[4];
            this.socialistPolls.relativePosition = new Vector3(this.greenPolls.relativePosition.x + this.greenPolls.width + SPACING + 70f, this.greenPolls.relativePosition.y);
            this.socialistPolls.autoSize = true;
            this.socialistPolls.name = "Moreeconomic_Text_2";

            this.liberalPolls = base.AddUIComponent<UILabel>();
            this.liberalPolls.text = Language.PoliticsMessage[5];
            this.liberalPolls.tooltip = Language.PoliticsMessage[5];
            this.liberalPolls.relativePosition = new Vector3(this.socialistPolls.relativePosition.x + this.socialistPolls.width + SPACING + 70f, this.socialistPolls.relativePosition.y);
            this.liberalPolls.autoSize = true;
            this.liberalPolls.name = "Moreeconomic_Text_2";

            this.nationalPolls = base.AddUIComponent<UILabel>();
            this.nationalPolls.text = Language.PoliticsMessage[6];
            this.nationalPolls.tooltip = Language.PoliticsMessage[6];
            this.nationalPolls.relativePosition = new Vector3(this.liberalPolls.relativePosition.x + this.liberalPolls.width + SPACING + 70f, this.liberalPolls.relativePosition.y);
            this.nationalPolls.autoSize = true;
            this.nationalPolls.name = "Moreeconomic_Text_2";

            this.nextVote = base.AddUIComponent<UILabel>();
            this.nextVote.text = Language.PoliticsMessage[21];
            this.nextVote.tooltip = Language.PoliticsMessage[21];
            this.nextVote.relativePosition = new Vector3(SPACING, this.communistPolls.relativePosition.y + SPACING22 + 20f);
            this.nextVote.autoSize = true;
            this.nextVote.name = "Moreeconomic_Text_0";

            this.nextMeeting = base.AddUIComponent<UILabel>();
            this.nextMeeting.text = Language.PoliticsMessage[22];
            this.nextMeeting.tooltip = Language.PoliticsMessage[22];
            this.nextMeeting.relativePosition = new Vector3(SPACING, this.nextVote.relativePosition.y + SPACING22);
            this.nextMeeting.autoSize = true;
            this.nextMeeting.name = "Moreeconomic_Text_0";

            this.currentMeetingItem = base.AddUIComponent<UILabel>();
            this.currentMeetingItem.text = Language.PoliticsMessage[23];
            this.currentMeetingItem.tooltip = Language.PoliticsMessage[23];
            this.currentMeetingItem.textScale = 1.1f;
            this.currentMeetingItem.relativePosition = new Vector3(SPACING, this.nextMeeting.relativePosition.y + SPACING22 + 20f);
            this.currentMeetingItem.autoSize = true;
            this.currentMeetingItem.name = "Moreeconomic_Text_0";

            this.voteResult = base.AddUIComponent<UILabel>();
            this.voteResult.text = Language.PoliticsMessage[36];
            this.voteResult.tooltip = Language.PoliticsMessage[36];
            this.voteResult.relativePosition = new Vector3(SPACING, this.currentMeetingItem.relativePosition.y + SPACING22);
            this.voteResult.autoSize = true;
            this.voteResult.name = "Moreeconomic_Text_0";

            this.currentPolitics = base.AddUIComponent<UILabel>();
            this.currentPolitics.text = Language.PoliticsMessage[40];
            this.currentPolitics.tooltip = Language.PoliticsMessage[40];
            this.currentPolitics.textScale = 1.1f;
            this.currentPolitics.relativePosition = new Vector3(SPACING, this.voteResult.relativePosition.y + SPACING22 + 20f);
            this.currentPolitics.autoSize = true;
            this.currentPolitics.name = "Moreeconomic_Text_0";

            this.salary = base.AddUIComponent<UILabel>();
            this.salary.text = Language.PoliticsMessage[41];
            this.salary.tooltip = Language.PoliticsMessage[41];
            this.salary.relativePosition = new Vector3(SPACING, this.currentPolitics.relativePosition.y + SPACING22);
            this.salary.autoSize = true;
            this.salary.name = "Moreeconomic_Text_0";

            this.benefit = base.AddUIComponent<UILabel>();
            this.benefit.text = Language.PoliticsMessage[42];
            this.benefit.tooltip = Language.PoliticsMessage[42];
            this.benefit.relativePosition = new Vector3(SPACING, this.salary.relativePosition.y + SPACING22);
            this.benefit.autoSize = true;
            this.benefit.name = "Moreeconomic_Text_0";

            this.trade = base.AddUIComponent<UILabel>();
            this.trade.text = Language.PoliticsMessage[43];
            this.trade.tooltip = Language.PoliticsMessage[43];
            this.trade.relativePosition = new Vector3(SPACING, this.benefit.relativePosition.y + SPACING22);
            this.trade.autoSize = true;
            this.trade.name = "Moreeconomic_Text_0";

            this.import = base.AddUIComponent<UILabel>();
            this.import.text = Language.PoliticsMessage[44];
            this.import.tooltip = Language.PoliticsMessage[44];
            this.import.relativePosition = new Vector3(SPACING, this.trade.relativePosition.y + SPACING22);
            this.import.autoSize = true;
            this.import.name = "Moreeconomic_Text_0";

            this.stateOwned = base.AddUIComponent<UILabel>();
            this.stateOwned.text = Language.PoliticsMessage[45];
            this.stateOwned.tooltip = Language.PoliticsMessage[45];
            this.stateOwned.relativePosition = new Vector3(SPACING, this.import.relativePosition.y + SPACING22);
            this.stateOwned.autoSize = true;
            this.stateOwned.name = "Moreeconomic_Text_0";

            this.garbage = base.AddUIComponent<UILabel>();
            this.garbage.text = Language.PoliticsMessage[46];
            this.garbage.tooltip = Language.PoliticsMessage[46];
            this.garbage.relativePosition = new Vector3(SPACING, this.stateOwned.relativePosition.y + SPACING22);
            this.garbage.autoSize = true;
            this.garbage.name = "Moreeconomic_Text_0";

            this.landRent = base.AddUIComponent<UILabel>();
            this.landRent.text = Language.PoliticsMessage[48];
            this.landRent.tooltip = Language.PoliticsMessage[48];
            this.landRent.relativePosition = new Vector3(SPACING, this.garbage.relativePosition.y + SPACING22);
            this.landRent.autoSize = true;
            this.landRent.name = "Moreeconomic_Text_0";

            riseImportTax_Checkbox = base.AddUIComponent<UICheckBox>();
            riseImportTax_Checkbox.relativePosition = new Vector3(SPACING, landRent.relativePosition.y + 30f);
            this.riseImportTax = base.AddUIComponent<UILabel>();
            this.riseImportTax.relativePosition = new Vector3(riseImportTax_Checkbox.relativePosition.x + riseImportTax_Checkbox.width + SPACING * 2f, riseImportTax_Checkbox.relativePosition.y + 5f);
            this.riseImportTax.tooltip = Language.PoliticsMessage[13];
            riseImportTax_Checkbox.height = 16f;
            riseImportTax_Checkbox.width = 16f;
            riseImportTax_Checkbox.label = this.riseImportTax;
            riseImportTax_Checkbox.text = Language.PoliticsMessage[13];
            UISprite uISprite9 = riseImportTax_Checkbox.AddUIComponent<UISprite>();
            uISprite9.height = 20f;
            uISprite9.width = 20f;
            uISprite9.relativePosition = new Vector3(0f, 0f);
            uISprite9.spriteName = "check-unchecked";
            uISprite9.isVisible = true;
            UISprite uISprite10 = riseImportTax_Checkbox.AddUIComponent<UISprite>();
            uISprite10.height = 20f;
            uISprite10.width = 20f;
            uISprite10.relativePosition = new Vector3(0f, 0f);
            uISprite10.spriteName = "check-checked";
            riseImportTax_Checkbox.checkedBoxObject = uISprite10;
            riseImportTax_Checkbox.isChecked = Politics.tryRiseImportTax;
            riseImportTax_Checkbox.isEnabled = true;
            riseImportTax_Checkbox.isVisible = true;
            riseImportTax_Checkbox.canFocus = true;
            riseImportTax_Checkbox.isInteractive = true;
            riseImportTax_Checkbox.eventCheckChanged += delegate (UIComponent component, bool eventParam)
            {
                riseImportTax_Checkbox_OnCheckChanged(component, eventParam);
            };

            fallImportTax_Checkbox = base.AddUIComponent<UICheckBox>();
            fallImportTax_Checkbox.relativePosition = new Vector3(SPACING, riseImportTax_Checkbox.relativePosition.y + 30f);
            this.fallImportTax = base.AddUIComponent<UILabel>();
            this.fallImportTax.relativePosition = new Vector3(fallImportTax_Checkbox.relativePosition.x + fallImportTax_Checkbox.width + SPACING * 2f, fallImportTax_Checkbox.relativePosition.y + 5f);
            this.fallImportTax.tooltip = Language.PoliticsMessage[14];
            fallImportTax_Checkbox.height = 16f;
            fallImportTax_Checkbox.width = 16f;
            fallImportTax_Checkbox.label = this.fallImportTax;
            fallImportTax_Checkbox.text = Language.PoliticsMessage[14];
            UISprite uISprite0 = fallImportTax_Checkbox.AddUIComponent<UISprite>();
            uISprite0.height = 20f;
            uISprite0.width = 20f;
            uISprite0.relativePosition = new Vector3(0f, 0f);
            uISprite0.spriteName = "check-unchecked";
            uISprite0.isVisible = true;
            UISprite uISprite1 = fallImportTax_Checkbox.AddUIComponent<UISprite>();
            uISprite1.height = 20f;
            uISprite1.width = 20f;
            uISprite1.relativePosition = new Vector3(0f, 0f);
            uISprite1.spriteName = "check-checked";
            fallImportTax_Checkbox.checkedBoxObject = uISprite1;
            fallImportTax_Checkbox.isChecked = Politics.tryFallImportTax;
            fallImportTax_Checkbox.isEnabled = true;
            fallImportTax_Checkbox.isVisible = true;
            fallImportTax_Checkbox.canFocus = true;
            fallImportTax_Checkbox.isInteractive = true;
            fallImportTax_Checkbox.eventCheckChanged += delegate (UIComponent component, bool eventParam)
            {
                fallImportTax_Checkbox_OnCheckChanged(component, eventParam);
            };


            fallLandTax_Checkbox = base.AddUIComponent<UICheckBox>();
            fallLandTax_Checkbox.relativePosition = new Vector3(SPACING, fallImportTax_Checkbox.relativePosition.y + 30f);
            this.fallLandTax = base.AddUIComponent<UILabel>();
            this.fallLandTax.relativePosition = new Vector3(fallLandTax_Checkbox.relativePosition.x + fallLandTax_Checkbox.width + SPACING * 2f, fallLandTax_Checkbox.relativePosition.y + 5f);
            this.fallLandTax.tooltip = Language.PoliticsMessage[15];
            fallLandTax_Checkbox.height = 16f;
            fallLandTax_Checkbox.width = 16f;
            fallLandTax_Checkbox.label = this.fallLandTax;
            fallLandTax_Checkbox.text = Language.PoliticsMessage[15];
            UISprite uISprite2 = fallLandTax_Checkbox.AddUIComponent<UISprite>();
            uISprite2.height = 20f;
            uISprite2.width = 20f;
            uISprite2.relativePosition = new Vector3(0f, 0f);
            uISprite2.spriteName = "check-unchecked";
            uISprite2.isVisible = true;
            UISprite uISprite3 = fallLandTax_Checkbox.AddUIComponent<UISprite>();
            uISprite3.height = 20f;
            uISprite3.width = 20f;
            uISprite3.relativePosition = new Vector3(0f, 0f);
            uISprite3.spriteName = "check-checked";
            fallLandTax_Checkbox.checkedBoxObject = uISprite3;
            fallLandTax_Checkbox.isChecked = Politics.tryFallLandTax;
            fallLandTax_Checkbox.isEnabled = true;
            fallLandTax_Checkbox.isVisible = true;
            fallLandTax_Checkbox.canFocus = true;
            fallLandTax_Checkbox.isInteractive = true;
            fallLandTax_Checkbox.eventCheckChanged += delegate (UIComponent component, bool eventParam)
            {
                fallLandTax_Checkbox_OnCheckChanged(component, eventParam);
            };


            riseTradeTax_Checkbox = base.AddUIComponent<UICheckBox>();
            riseTradeTax_Checkbox.relativePosition = new Vector3(SPACING, fallLandTax_Checkbox.relativePosition.y + 30f);
            this.riseTradeTax = base.AddUIComponent<UILabel>();
            this.riseTradeTax.relativePosition = new Vector3(riseTradeTax_Checkbox.relativePosition.x + riseTradeTax_Checkbox.width + SPACING * 2f, riseTradeTax_Checkbox.relativePosition.y + 5f);
            this.riseTradeTax.tooltip = Language.PoliticsMessage[16];
            riseTradeTax_Checkbox.height = 16f;
            riseTradeTax_Checkbox.width = 16f;
            riseTradeTax_Checkbox.label = this.riseTradeTax;
            riseTradeTax_Checkbox.text = Language.PoliticsMessage[16];
            UISprite uISprite4 = riseTradeTax_Checkbox.AddUIComponent<UISprite>();
            uISprite4.height = 20f;
            uISprite4.width = 20f;
            uISprite4.relativePosition = new Vector3(0f, 0f);
            uISprite4.spriteName = "check-unchecked";
            uISprite4.isVisible = true;
            UISprite uISprite5 = riseTradeTax_Checkbox.AddUIComponent<UISprite>();
            uISprite5.height = 20f;
            uISprite5.width = 20f;
            uISprite5.relativePosition = new Vector3(0f, 0f);
            uISprite5.spriteName = "check-checked";
            riseTradeTax_Checkbox.checkedBoxObject = uISprite5;
            riseTradeTax_Checkbox.isChecked = Politics.tryRiseTradeTax;
            riseTradeTax_Checkbox.isEnabled = true;
            riseTradeTax_Checkbox.isVisible = true;
            riseTradeTax_Checkbox.canFocus = true;
            riseTradeTax_Checkbox.isInteractive = true;
            riseTradeTax_Checkbox.eventCheckChanged += delegate (UIComponent component, bool eventParam)
            {
                riseTradeTax_Checkbox_OnCheckChanged(component, eventParam);
            };

            fallTradeTax_Checkbox = base.AddUIComponent<UICheckBox>();
            fallTradeTax_Checkbox.relativePosition = new Vector3(SPACING, riseTradeTax_Checkbox.relativePosition.y + 30f);
            this.fallTradeTax = base.AddUIComponent<UILabel>();
            this.fallTradeTax.relativePosition = new Vector3(fallTradeTax_Checkbox.relativePosition.x + fallTradeTax_Checkbox.width + SPACING * 2f, fallTradeTax_Checkbox.relativePosition.y + 5f);
            this.fallTradeTax.tooltip = Language.PoliticsMessage[17];
            fallTradeTax_Checkbox.height = 16f;
            fallTradeTax_Checkbox.width = 16f;
            fallTradeTax_Checkbox.label = this.fallTradeTax;
            fallTradeTax_Checkbox.text = Language.PoliticsMessage[17];
            UISprite uISprite6 = fallTradeTax_Checkbox.AddUIComponent<UISprite>();
            uISprite6.height = 20f;
            uISprite6.width = 20f;
            uISprite6.relativePosition = new Vector3(0f, 0f);
            uISprite6.spriteName = "check-unchecked";
            uISprite6.isVisible = true;
            UISprite uISprite7 = fallTradeTax_Checkbox.AddUIComponent<UISprite>();
            uISprite7.height = 20f;
            uISprite7.width = 20f;
            uISprite7.relativePosition = new Vector3(0f, 0f);
            uISprite7.spriteName = "check-checked";
            fallTradeTax_Checkbox.checkedBoxObject = uISprite7;
            fallTradeTax_Checkbox.isChecked = Politics.tryFallTradeTax;
            fallTradeTax_Checkbox.isEnabled = true;
            fallTradeTax_Checkbox.isVisible = true;
            fallTradeTax_Checkbox.canFocus = true;
            fallTradeTax_Checkbox.isInteractive = true;
            fallTradeTax_Checkbox.eventCheckChanged += delegate (UIComponent component, bool eventParam)
            {
                fallTradeTax_Checkbox_OnCheckChanged(component, eventParam);
            };

        }


        public static void riseImportTax_Checkbox_OnCheckChanged(UIComponent UIComp, bool bValue)
        {
            if (bValue)
            {
                Politics.tryRiseImportTax = (Politics.importTaxOffset < 0.39f);
                Politics.tryFallImportTax = false;
                Politics.tryFallLandTax = false;
                Politics.tryRiseTradeTax = false;
                Politics.tryFallTradeTax = false;
                PoliticsUI.fallLandTax_Checkbox.isChecked = Politics.tryFallLandTax;
                PoliticsUI.fallImportTax_Checkbox.isChecked = Politics.tryFallImportTax;
                PoliticsUI.fallTradeTax_Checkbox.isChecked = Politics.tryFallTradeTax;
                PoliticsUI.riseImportTax_Checkbox.isChecked = Politics.tryRiseImportTax;
                PoliticsUI.riseTradeTax_Checkbox.isChecked = Politics.tryRiseTradeTax;
            }
            else
            {
                Politics.tryRiseImportTax = false;
                PoliticsUI.fallLandTax_Checkbox.isChecked = Politics.tryFallLandTax;
                PoliticsUI.fallImportTax_Checkbox.isChecked = Politics.tryFallImportTax;
                PoliticsUI.fallTradeTax_Checkbox.isChecked = Politics.tryFallTradeTax;
                PoliticsUI.riseImportTax_Checkbox.isChecked = Politics.tryRiseImportTax;
                PoliticsUI.riseTradeTax_Checkbox.isChecked = Politics.tryRiseTradeTax;
            }
        }

        public static void fallImportTax_Checkbox_OnCheckChanged(UIComponent UIComp, bool bValue)
        {
            if (bValue)
            {
                Politics.tryRiseImportTax = false;
                Politics.tryFallImportTax = (Politics.importTaxOffset > 0f);
                Politics.tryFallLandTax = false;
                Politics.tryRiseTradeTax = false;
                Politics.tryFallTradeTax = false;
                PoliticsUI.fallLandTax_Checkbox.isChecked = Politics.tryFallLandTax;
                PoliticsUI.fallImportTax_Checkbox.isChecked = Politics.tryFallImportTax;
                PoliticsUI.fallTradeTax_Checkbox.isChecked = Politics.tryFallTradeTax;
                PoliticsUI.riseImportTax_Checkbox.isChecked = Politics.tryRiseImportTax;
                PoliticsUI.riseTradeTax_Checkbox.isChecked = Politics.tryRiseTradeTax;
            }
            else
            {
                Politics.tryFallImportTax = false;
                PoliticsUI.fallLandTax_Checkbox.isChecked = Politics.tryFallLandTax;
                PoliticsUI.fallImportTax_Checkbox.isChecked = Politics.tryFallImportTax;
                PoliticsUI.fallTradeTax_Checkbox.isChecked = Politics.tryFallTradeTax;
                PoliticsUI.riseImportTax_Checkbox.isChecked = Politics.tryRiseImportTax;
                PoliticsUI.riseTradeTax_Checkbox.isChecked = Politics.tryRiseTradeTax;
            }
        }

        public static void fallLandTax_Checkbox_OnCheckChanged(UIComponent UIComp, bool bValue)
        {
            if (bValue)
            {
                Politics.tryRiseImportTax = false;
                Politics.tryFallImportTax = false;
                Politics.tryFallLandTax = (Politics.landRentOffset > 0f);
                Politics.tryRiseTradeTax = false;
                Politics.tryFallTradeTax = false;
                PoliticsUI.fallLandTax_Checkbox.isChecked = Politics.tryFallLandTax;
                PoliticsUI.fallImportTax_Checkbox.isChecked = Politics.tryFallImportTax;
                PoliticsUI.fallTradeTax_Checkbox.isChecked = Politics.tryFallTradeTax;
                PoliticsUI.riseImportTax_Checkbox.isChecked = Politics.tryRiseImportTax;
                PoliticsUI.riseTradeTax_Checkbox.isChecked = Politics.tryRiseTradeTax;
            }
            else
            {
                Politics.tryFallLandTax = false;
                PoliticsUI.fallLandTax_Checkbox.isChecked = Politics.tryFallLandTax;
                PoliticsUI.fallImportTax_Checkbox.isChecked = Politics.tryFallImportTax;
                PoliticsUI.fallTradeTax_Checkbox.isChecked = Politics.tryFallTradeTax;
                PoliticsUI.riseImportTax_Checkbox.isChecked = Politics.tryRiseImportTax;
                PoliticsUI.riseTradeTax_Checkbox.isChecked = Politics.tryRiseTradeTax;
            }
        }

        public static void riseTradeTax_Checkbox_OnCheckChanged(UIComponent UIComp, bool bValue)
        {
            if (bValue)
            {
                Politics.tryRiseImportTax = false;
                Politics.tryFallImportTax = false;
                Politics.tryFallLandTax = false;
                Politics.tryRiseTradeTax = (Politics.tradeTaxOffset < 0.099f);
                Politics.tryFallTradeTax = false;
                PoliticsUI.fallLandTax_Checkbox.isChecked = Politics.tryFallLandTax;
                PoliticsUI.fallImportTax_Checkbox.isChecked = Politics.tryFallImportTax;
                PoliticsUI.fallTradeTax_Checkbox.isChecked = Politics.tryFallTradeTax;
                PoliticsUI.riseImportTax_Checkbox.isChecked = Politics.tryRiseImportTax;
                PoliticsUI.riseTradeTax_Checkbox.isChecked = Politics.tryRiseTradeTax;
            }
            else
            {
                Politics.tryRiseTradeTax = false;
                PoliticsUI.fallLandTax_Checkbox.isChecked = Politics.tryFallLandTax;
                PoliticsUI.fallImportTax_Checkbox.isChecked = Politics.tryFallImportTax;
                PoliticsUI.fallTradeTax_Checkbox.isChecked = Politics.tryFallTradeTax;
                PoliticsUI.riseImportTax_Checkbox.isChecked = Politics.tryRiseImportTax;
                PoliticsUI.riseTradeTax_Checkbox.isChecked = Politics.tryRiseTradeTax;
            }
        }

        public static void fallTradeTax_Checkbox_OnCheckChanged(UIComponent UIComp, bool bValue)
        {
            if (bValue)
            {
                Politics.tryRiseImportTax = false;
                Politics.tryFallImportTax = false;
                Politics.tryFallLandTax = false;
                Politics.tryRiseTradeTax = false;
                Politics.tryFallTradeTax = (Politics.tradeTaxOffset > 0f);
                PoliticsUI.fallLandTax_Checkbox.isChecked = Politics.tryFallLandTax;
                PoliticsUI.fallImportTax_Checkbox.isChecked = Politics.tryFallImportTax;
                PoliticsUI.fallTradeTax_Checkbox.isChecked = Politics.tryFallTradeTax;
                PoliticsUI.riseImportTax_Checkbox.isChecked = Politics.tryRiseImportTax;
                PoliticsUI.riseTradeTax_Checkbox.isChecked = Politics.tryRiseTradeTax;
            }
            else
            {
                Politics.tryFallTradeTax = false;
                PoliticsUI.fallLandTax_Checkbox.isChecked = Politics.tryFallLandTax;
                PoliticsUI.fallImportTax_Checkbox.isChecked = Politics.tryFallImportTax;
                PoliticsUI.fallTradeTax_Checkbox.isChecked = Politics.tryFallTradeTax;
                PoliticsUI.riseImportTax_Checkbox.isChecked = Politics.tryRiseImportTax;
                PoliticsUI.riseTradeTax_Checkbox.isChecked = Politics.tryRiseTradeTax;
            }
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
                    this.m_title.text = Language.PoliticsMessage[0];
                    this.communist.text = string.Format(Language.PoliticsMessage[2] + " [{0}]", Politics.cPartySeats);
                    this.green.text = string.Format(Language.PoliticsMessage[3] + " [{0}]", Politics.gPartySeats);
                    this.socialist.text = string.Format(Language.PoliticsMessage[4] + " [{0}]", Politics.sPartySeats);
                    this.liberal.text = string.Format(Language.PoliticsMessage[5] + " [{0}]", Politics.lPartySeats);
                    this.national.text = string.Format(Language.PoliticsMessage[6] + " [{0}]", Politics.nPartySeats);

                    this.communistPolls.text = string.Format(Language.PoliticsMessage[2] + " [{0:N1}%]", Politics.cPartySeatsPollsFinal);
                    this.greenPolls.text = string.Format(Language.PoliticsMessage[3] + " [{0:N1}%]", Politics.gPartySeatsPollsFinal);
                    this.socialistPolls.text = string.Format(Language.PoliticsMessage[4] + " [{0:N1}%]", Politics.sPartySeatsPollsFinal);
                    this.liberalPolls.text = string.Format(Language.PoliticsMessage[5] + " [{0:N1}%]", Politics.lPartySeatsPollsFinal);
                    this.nationalPolls.text = string.Format(Language.PoliticsMessage[6] + " [{0:N1}%]", Politics.nPartySeatsPollsFinal);

                    this.nextVote.text = string.Format(Language.PoliticsMessage[21] + " [{0}]", Politics.parliamentCount);
                    this.nextMeeting.text = string.Format(Language.PoliticsMessage[22] + " [{0}]", Politics.parliamentMeetingCount);

                    if (Politics.currentIdx > 13)
                    {
                        this.currentMeetingItem.text = string.Format(Language.PoliticsMessage[23] + ": N/A");
                    }
                    else
                    {

                        this.currentMeetingItem.text = string.Format(Language.PoliticsMessage[23] + ":" + Language.PoliticsMessage[24 + (int)Politics.currentIdx]);
                    }

                    this.voteResult.text = string.Format(Language.PoliticsMessage[38] + ": " + Language.PoliticsMessage[39] + ":" + Politics.currentYes.ToString() + " " + Language.PoliticsMessage[40] + ":" + Politics.currentNo.ToString() + " " + Language.PoliticsMessage[41] + ":" + Politics.currentNoAttend.ToString());

                    this.currentPolitics.text = string.Format(Language.PoliticsMessage[42] + Language.PoliticsMessage[49]);

                    this.salary.text = string.Format(Language.PoliticsMessage[43] + Politics.salaryTaxOffset.ToString());
                    this.benefit.text = string.Format(Language.PoliticsMessage[44] + Politics.benefitOffset.ToString());
                    this.trade.text = string.Format(Language.PoliticsMessage[45] + Politics.tradeTaxOffset.ToString());
                    this.import.text = string.Format(Language.PoliticsMessage[46] + Politics.importTaxOffset.ToString());
                    this.stateOwned.text = string.Format(Language.PoliticsMessage[47] + Politics.stateOwnedPercent.ToString());
                    this.garbage.text = string.Format(Language.PoliticsMessage[48] + (!Politics.isOutSideGarbagePermit).ToString());
                    this.landRent.text = string.Format(Language.PoliticsMessage[50] + Politics.landRentOffset.ToString());

                    if (Politics.case1)
                    {
                        //c only
                        this.goverment.text = string.Format(Language.PoliticsMessage[7] + Language.PoliticsMessage[2]);
                    }
                    else if (Politics.case2)
                    {
                        this.goverment.text = string.Format(Language.PoliticsMessage[7] + Language.PoliticsMessage[3]);
                    }
                    else if (Politics.case3)
                    {
                        this.goverment.text = string.Format(Language.PoliticsMessage[7] + Language.PoliticsMessage[4]);
                    }
                    else if (Politics.case4)
                    {
                        this.goverment.text = string.Format(Language.PoliticsMessage[7] + Language.PoliticsMessage[5]);
                    }
                    else if (Politics.case5)
                    {
                        this.goverment.text = string.Format(Language.PoliticsMessage[7]);
                    }
                    else if (Politics.case6)
                    {
                        this.goverment.text = string.Format(Language.PoliticsMessage[7] + Language.PoliticsMessage[4] + Language.PoliticsMessage[3] + Language.PoliticsMessage[8]);
                    }
                    else if (Politics.case7)
                    {
                        this.goverment.text = string.Format(Language.PoliticsMessage[7] + Language.PoliticsMessage[4] + Language.PoliticsMessage[3] + Language.PoliticsMessage[2] + Language.PoliticsMessage[9]);
                    }
                    else if (Politics.case8)
                    {
                        this.goverment.text = string.Format(Language.PoliticsMessage[7] + Language.PoliticsMessage[6] + Language.PoliticsMessage[5] + Language.PoliticsMessage[10]);
                    }
                    else
                    {
                        this.goverment.text = string.Format(Language.PoliticsMessage[7] + Language.PoliticsMessage[2] + Language.PoliticsMessage[3] + Language.PoliticsMessage[4] + Language.PoliticsMessage[5] + Language.PoliticsMessage[6] + Language.PoliticsMessage[11]);
                    }

                    this.polls.text = Language.PoliticsMessage[19];

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
