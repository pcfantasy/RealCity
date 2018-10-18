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
            this.m_title.text = language.PoliticsMessage[0];
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
            this.parliamentSeats.text = language.PoliticsMessage[1];
            this.parliamentSeats.textScale = 1.1f;
            this.parliamentSeats.tooltip = "N/A";
            this.parliamentSeats.relativePosition = new Vector3(SPACING, 50f);
            this.parliamentSeats.autoSize = true;

            //data
            this.communist = base.AddUIComponent<UILabel>();
            this.communist.text = language.PoliticsMessage[2];
            this.communist.tooltip = language.PoliticsMessage[2];
            this.communist.relativePosition = new Vector3(SPACING, this.parliamentSeats.relativePosition.y + SPACING22);
            this.communist.autoSize = true;
            this.communist.name = "Moreeconomic_Text_0";

            this.green = base.AddUIComponent<UILabel>();
            this.green.text = language.PoliticsMessage[3];
            this.green.tooltip = language.PoliticsMessage[3];
            this.green.relativePosition = new Vector3(this.communist.relativePosition.x + this.communist.width + SPACING + 70f, this.communist.relativePosition.y);
            this.green.autoSize = true;
            this.green.name = "Moreeconomic_Text_1";

            this.socialist = base.AddUIComponent<UILabel>();
            this.socialist.text = language.PoliticsMessage[4];
            this.socialist.tooltip = language.PoliticsMessage[4];
            this.socialist.relativePosition = new Vector3(this.green.relativePosition.x + this.green.width + SPACING + 70f, this.green.relativePosition.y);
            this.socialist.autoSize = true;
            this.socialist.name = "Moreeconomic_Text_2";

            this.liberal = base.AddUIComponent<UILabel>();
            this.liberal.text = language.PoliticsMessage[5];
            this.liberal.tooltip = language.PoliticsMessage[5];
            this.liberal.relativePosition = new Vector3(this.socialist.relativePosition.x + this.socialist.width + SPACING + 70f, this.socialist.relativePosition.y);
            this.liberal.autoSize = true;
            this.liberal.name = "Moreeconomic_Text_2";

            this.national = base.AddUIComponent<UILabel>();
            this.national.text = language.PoliticsMessage[6];
            this.national.tooltip = language.PoliticsMessage[6];
            this.national.relativePosition = new Vector3(this.liberal.relativePosition.x + this.liberal.width + SPACING + 70f, this.liberal.relativePosition.y);
            this.national.autoSize = true;
            this.national.name = "Moreeconomic_Text_2";


            goverment = base.AddUIComponent<UILabel>();
            goverment.text = language.PoliticsMessage[7];
            goverment.tooltip = language.PoliticsMessage[7];
            goverment.relativePosition = new Vector3(SPACING, this.communist.relativePosition.y + SPACING22 + 20f);
            goverment.autoSize = true;
            goverment.name = "Moreeconomic_Text_0";

            //citizen
            this.polls = base.AddUIComponent<UILabel>();
            this.polls.text = language.PoliticsMessage[19];
            this.polls.textScale = 1.1f;
            this.polls.tooltip = "N/A";
            this.polls.relativePosition = new Vector3(SPACING, goverment.relativePosition.y + SPACING22 + 20f);
            this.polls.autoSize = true;

            //data
            this.communistPolls = base.AddUIComponent<UILabel>();
            this.communistPolls.text = language.PoliticsMessage[2];
            this.communistPolls.tooltip = language.PoliticsMessage[2];
            this.communistPolls.relativePosition = new Vector3(SPACING, this.polls.relativePosition.y + SPACING22);
            this.communistPolls.autoSize = true;
            this.communistPolls.name = "Moreeconomic_Text_0";

            this.greenPolls = base.AddUIComponent<UILabel>();
            this.greenPolls.text = language.PoliticsMessage[3];
            this.greenPolls.tooltip = language.PoliticsMessage[3];
            this.greenPolls.relativePosition = new Vector3(this.communistPolls.relativePosition.x + this.communistPolls.width + SPACING + 70f, this.communistPolls.relativePosition.y);
            this.greenPolls.autoSize = true;
            this.greenPolls.name = "Moreeconomic_Text_1";

            this.socialistPolls = base.AddUIComponent<UILabel>();
            this.socialistPolls.text = language.PoliticsMessage[4];
            this.socialistPolls.tooltip = language.PoliticsMessage[4];
            this.socialistPolls.relativePosition = new Vector3(this.greenPolls.relativePosition.x + this.greenPolls.width + SPACING + 70f, this.greenPolls.relativePosition.y);
            this.socialistPolls.autoSize = true;
            this.socialistPolls.name = "Moreeconomic_Text_2";

            this.liberalPolls = base.AddUIComponent<UILabel>();
            this.liberalPolls.text = language.PoliticsMessage[5];
            this.liberalPolls.tooltip = language.PoliticsMessage[5];
            this.liberalPolls.relativePosition = new Vector3(this.socialistPolls.relativePosition.x + this.socialistPolls.width + SPACING + 70f, this.socialistPolls.relativePosition.y);
            this.liberalPolls.autoSize = true;
            this.liberalPolls.name = "Moreeconomic_Text_2";

            this.nationalPolls = base.AddUIComponent<UILabel>();
            this.nationalPolls.text = language.PoliticsMessage[6];
            this.nationalPolls.tooltip = language.PoliticsMessage[6];
            this.nationalPolls.relativePosition = new Vector3(this.liberalPolls.relativePosition.x + this.liberalPolls.width + SPACING + 70f, this.liberalPolls.relativePosition.y);
            this.nationalPolls.autoSize = true;
            this.nationalPolls.name = "Moreeconomic_Text_2";

            this.nextVote = base.AddUIComponent<UILabel>();
            this.nextVote.text = language.PoliticsMessage[21];
            this.nextVote.tooltip = language.PoliticsMessage[21];
            this.nextVote.relativePosition = new Vector3(SPACING, this.communistPolls.relativePosition.y + SPACING22 + 20f);
            this.nextVote.autoSize = true;
            this.nextVote.name = "Moreeconomic_Text_0";

            this.nextMeeting = base.AddUIComponent<UILabel>();
            this.nextMeeting.text = language.PoliticsMessage[22];
            this.nextMeeting.tooltip = language.PoliticsMessage[22];
            this.nextMeeting.relativePosition = new Vector3(SPACING, this.nextVote.relativePosition.y + SPACING22 + 20f);
            this.nextMeeting.autoSize = true;
            this.nextMeeting.name = "Moreeconomic_Text_0";

            this.currentMeetingItem = base.AddUIComponent<UILabel>();
            this.currentMeetingItem.text = language.PoliticsMessage[23];
            this.currentMeetingItem.tooltip = language.PoliticsMessage[23];
            this.currentMeetingItem.textScale = 1.1f;
            this.currentMeetingItem.relativePosition = new Vector3(SPACING, this.nextMeeting.relativePosition.y + SPACING22 + 20f);
            this.currentMeetingItem.autoSize = true;
            this.currentMeetingItem.name = "Moreeconomic_Text_0";

            this.voteResult = base.AddUIComponent<UILabel>();
            this.voteResult.text = language.PoliticsMessage[36];
            this.voteResult.tooltip = language.PoliticsMessage[36];
            this.voteResult.relativePosition = new Vector3(SPACING, this.currentMeetingItem.relativePosition.y + SPACING22 + 20f);
            this.voteResult.autoSize = true;
            this.voteResult.name = "Moreeconomic_Text_0";

            this.currentPolitics = base.AddUIComponent<UILabel>();
            this.currentPolitics.text = language.PoliticsMessage[40];
            this.currentPolitics.tooltip = language.PoliticsMessage[40];
            this.currentPolitics.textScale = 1.1f;
            this.currentPolitics.relativePosition = new Vector3(SPACING, this.voteResult.relativePosition.y + SPACING22 + 20f);
            this.currentPolitics.autoSize = true;
            this.currentPolitics.name = "Moreeconomic_Text_0";

            this.salary = base.AddUIComponent<UILabel>();
            this.salary.text = language.PoliticsMessage[41];
            this.salary.tooltip = language.PoliticsMessage[41];
            this.salary.relativePosition = new Vector3(SPACING, this.currentPolitics.relativePosition.y + SPACING22);
            this.salary.autoSize = true;
            this.salary.name = "Moreeconomic_Text_0";

            this.benefit = base.AddUIComponent<UILabel>();
            this.benefit.text = language.PoliticsMessage[42];
            this.benefit.tooltip = language.PoliticsMessage[42];
            this.benefit.relativePosition = new Vector3(SPACING, this.salary.relativePosition.y + SPACING22);
            this.benefit.autoSize = true;
            this.benefit.name = "Moreeconomic_Text_0";

            this.trade = base.AddUIComponent<UILabel>();
            this.trade.text = language.PoliticsMessage[43];
            this.trade.tooltip = language.PoliticsMessage[43];
            this.trade.relativePosition = new Vector3(SPACING, this.benefit.relativePosition.y + SPACING22);
            this.trade.autoSize = true;
            this.trade.name = "Moreeconomic_Text_0";

            this.import = base.AddUIComponent<UILabel>();
            this.import.text = language.PoliticsMessage[44];
            this.import.tooltip = language.PoliticsMessage[44];
            this.import.relativePosition = new Vector3(SPACING, this.trade.relativePosition.y + SPACING22);
            this.import.autoSize = true;
            this.import.name = "Moreeconomic_Text_0";

            this.stateOwned = base.AddUIComponent<UILabel>();
            this.stateOwned.text = language.PoliticsMessage[45];
            this.stateOwned.tooltip = language.PoliticsMessage[45];
            this.stateOwned.relativePosition = new Vector3(SPACING, this.import.relativePosition.y + SPACING22);
            this.stateOwned.autoSize = true;
            this.stateOwned.name = "Moreeconomic_Text_0";

            this.garbage = base.AddUIComponent<UILabel>();
            this.garbage.text = language.PoliticsMessage[46];
            this.garbage.tooltip = language.PoliticsMessage[46];
            this.garbage.relativePosition = new Vector3(SPACING, this.stateOwned.relativePosition.y + SPACING22);
            this.garbage.autoSize = true;
            this.garbage.name = "Moreeconomic_Text_0";

            this.landRent = base.AddUIComponent<UILabel>();
            this.landRent.text = language.PoliticsMessage[48];
            this.landRent.tooltip = language.PoliticsMessage[48];
            this.landRent.relativePosition = new Vector3(SPACING, this.garbage.relativePosition.y + SPACING22);
            this.landRent.autoSize = true;
            this.landRent.name = "Moreeconomic_Text_0";

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
                    this.m_title.text = language.PoliticsMessage[0];
                    this.communist.text = string.Format(language.PoliticsMessage[2] + " [{0}]", Politics.cPartySeats);
                    this.green.text = string.Format(language.PoliticsMessage[3] + " [{0}]", Politics.gPartySeats);
                    this.socialist.text = string.Format(language.PoliticsMessage[4] + " [{0}]", Politics.sPartySeats);
                    this.liberal.text = string.Format(language.PoliticsMessage[5] + " [{0}]", Politics.lPartySeats);
                    this.national.text = string.Format(language.PoliticsMessage[6] + " [{0}]", Politics.nPartySeats);

                    this.communistPolls.text = string.Format(language.PoliticsMessage[2] + " [{0:N1}%]", Politics.cPartySeatsPollsFinal);
                    this.greenPolls.text = string.Format(language.PoliticsMessage[3] + " [{0:N1}%]", Politics.gPartySeatsPollsFinal);
                    this.socialistPolls.text = string.Format(language.PoliticsMessage[4] + " [{0:N1}%]", Politics.sPartySeatsPollsFinal);
                    this.liberalPolls.text = string.Format(language.PoliticsMessage[5] + " [{0:N1}%]", Politics.lPartySeatsPollsFinal);
                    this.nationalPolls.text = string.Format(language.PoliticsMessage[6] + " [{0:N1}%]", Politics.nPartySeatsPollsFinal);

                    this.nextVote.text = string.Format(language.PoliticsMessage[21] + " [{0}]", Politics.parliamentCount);
                    this.nextMeeting.text = string.Format(language.PoliticsMessage[22] + " [{0}]", Politics.parliamentMeetingCount);

                    if (Politics.currentIdx > 13)
                    {
                        this.currentMeetingItem.text = string.Format(language.PoliticsMessage[23] + ": N/A");
                    }
                    else
                    {

                        this.currentMeetingItem.text = string.Format(language.PoliticsMessage[23] + ":" + language.PoliticsMessage[24 + (int)Politics.currentIdx]);
                    }

                    this.voteResult.text = string.Format(language.PoliticsMessage[38] + ": " + language.PoliticsMessage[39] + ":" + Politics.currentYes.ToString() + " " + language.PoliticsMessage[40] + ":" + Politics.currentNo.ToString() + " " + language.PoliticsMessage[41] + ":" + Politics.currentNoAttend.ToString());

                    this.currentPolitics.text = string.Format(language.PoliticsMessage[42] + language.PoliticsMessage[49]);

                    this.salary.text = string.Format(language.PoliticsMessage[43] + Politics.salaryTaxOffset.ToString());
                    this.benefit.text = string.Format(language.PoliticsMessage[44] + Politics.benefitOffset.ToString());
                    this.trade.text = string.Format(language.PoliticsMessage[45] + Politics.tradeTaxOffset.ToString());
                    this.import.text = string.Format(language.PoliticsMessage[46] + Politics.importTaxOffset.ToString());
                    this.stateOwned.text = string.Format(language.PoliticsMessage[47] + Politics.stateOwnedPercent.ToString());
                    this.garbage.text = string.Format(language.PoliticsMessage[48] + (!Politics.isOutSideGarbagePermit).ToString());
                    this.landRent.text = string.Format(language.PoliticsMessage[50] + Politics.landRentOffset.ToString());

                    if (Politics.case1)
                    {
                        //c only
                        this.goverment.text = string.Format(language.PoliticsMessage[7] + language.PoliticsMessage[2]);
                    }
                    else if (Politics.case2)
                    {
                        this.goverment.text = string.Format(language.PoliticsMessage[7] + language.PoliticsMessage[3]);
                    }
                    else if (Politics.case3)
                    {
                        this.goverment.text = string.Format(language.PoliticsMessage[7] + language.PoliticsMessage[4]);
                    }
                    else if (Politics.case4)
                    {
                        this.goverment.text = string.Format(language.PoliticsMessage[7] + language.PoliticsMessage[5]);
                    }
                    else if (Politics.case5)
                    {
                        this.goverment.text = string.Format(language.PoliticsMessage[7]);
                    }
                    else if (Politics.case6)
                    {
                        this.goverment.text = string.Format(language.PoliticsMessage[7] + language.PoliticsMessage[4] + language.PoliticsMessage[3] + language.PoliticsMessage[8]);
                    }
                    else if (Politics.case7)
                    {
                        this.goverment.text = string.Format(language.PoliticsMessage[7] + language.PoliticsMessage[4] + language.PoliticsMessage[3] + language.PoliticsMessage[2] + language.PoliticsMessage[9]);
                    }
                    else if (Politics.case8)
                    {
                        this.goverment.text = string.Format(language.PoliticsMessage[7] + language.PoliticsMessage[6] + language.PoliticsMessage[5] + language.PoliticsMessage[10]);
                    }
                    else
                    {
                        this.goverment.text = string.Format(language.PoliticsMessage[7] + language.PoliticsMessage[2] + language.PoliticsMessage[3] + language.PoliticsMessage[4] + language.PoliticsMessage[5] + language.PoliticsMessage[6] + language.PoliticsMessage[11]);
                    }

                    this.polls.text = language.PoliticsMessage[19];

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
