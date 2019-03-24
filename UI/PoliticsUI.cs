using System.Collections.Generic;
using ColossalFramework;
using ColossalFramework.UI;
using UnityEngine;
using System.Collections;
using RealCity.Util;

namespace RealCity.UI
{
    public class PoliticsUI : UIPanel
    {
        public static readonly string cacheName = "PoliticsUI";
        private static float WIDTH = 800f;
        private static readonly float HEIGHT = 450f;
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
        private UILabel nextVote;
        private UILabel currentMeetingItem;
        private UILabel voteResult;
        private UILabel currentPolitics;
        private UILabel benefit;
        private UILabel resident;
        private UILabel commerical;
        private UILabel industrial;
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
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 + 50f), 170f);
            base.opacity = 1f;
            base.cachedName = cacheName;
            this.CurrentMode = Singleton<ToolManager>.instance.m_properties.m_mode;
            this.m_DragHandler = base.AddUIComponent<UIDragHandle>();
            this.m_DragHandler.target = this;
            this.m_title = base.AddUIComponent<UILabel>();
            this.m_title.text = Localization.Get("PARLIAMENT_HALL");
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
            this.parliamentSeats = base.AddUIComponent<UILabel>();
            this.parliamentSeats.text = Localization.Get("PARLIAMENT_SEATS");
            this.parliamentSeats.textScale = 1.1f;
            this.parliamentSeats.relativePosition = new Vector3(SPACING, 50f);
            this.parliamentSeats.autoSize = true;

            this.communist = base.AddUIComponent<UILabel>();
            this.communist.text = Localization.Get("COMMUNIST");
            this.communist.relativePosition = new Vector3(SPACING, this.parliamentSeats.relativePosition.y + SPACING22);
            this.communist.autoSize = true;

            this.green = base.AddUIComponent<UILabel>();
            this.green.text = Localization.Get("GREEN");
            this.green.relativePosition = new Vector3(this.communist.relativePosition.x + this.communist.width + SPACING + 70f, this.communist.relativePosition.y);
            this.green.autoSize = true;

            this.socialist = base.AddUIComponent<UILabel>();
            this.socialist.text = Localization.Get("SOCIALIST");
            this.socialist.relativePosition = new Vector3(this.green.relativePosition.x + this.green.width + SPACING + 70f, this.green.relativePosition.y);
            this.socialist.autoSize = true;

            this.liberal = base.AddUIComponent<UILabel>();
            this.liberal.text = Localization.Get("LIBERAL");
            this.liberal.relativePosition = new Vector3(this.socialist.relativePosition.x + this.socialist.width + SPACING + 70f, this.socialist.relativePosition.y);
            this.liberal.autoSize = true;

            this.national = base.AddUIComponent<UILabel>();
            this.national.text = Localization.Get("NATIONAL");
            this.national.relativePosition = new Vector3(this.liberal.relativePosition.x + this.liberal.width + SPACING + 70f, this.liberal.relativePosition.y);
            this.national.autoSize = true;

            this.goverment = base.AddUIComponent<UILabel>();
            this.goverment.text = Localization.Get("GOVERMENT");
            this.goverment.relativePosition = new Vector3(SPACING, this.communist.relativePosition.y + SPACING22 + 20f);
            this.goverment.autoSize = true;

            this.nextVote = base.AddUIComponent<UILabel>();
            this.nextVote.text = Localization.Get("NEXT_VOTE");
            this.nextVote.relativePosition = new Vector3(SPACING, this.goverment.relativePosition.y + SPACING22 + 20f);
            this.nextVote.autoSize = true;

            this.currentMeetingItem = base.AddUIComponent<UILabel>();
            this.currentMeetingItem.text = Localization.Get("CURRENT_MEETING_ITEM");
            this.currentMeetingItem.textScale = 1.1f;
            this.currentMeetingItem.relativePosition = new Vector3(SPACING, this.nextVote.relativePosition.y + SPACING22 + 20f);
            this.currentMeetingItem.autoSize = true;

            this.voteResult = base.AddUIComponent<UILabel>();
            this.voteResult.text = Localization.Get("VOTE_RESULT");
            this.voteResult.relativePosition = new Vector3(SPACING, this.currentMeetingItem.relativePosition.y + SPACING22);
            this.voteResult.autoSize = true;

            this.currentPolitics = base.AddUIComponent<UILabel>();
            this.currentPolitics.text = Localization.Get("CURRENT_POLICY");
            this.currentPolitics.textScale = 1.1f;
            this.currentPolitics.relativePosition = new Vector3(SPACING, this.voteResult.relativePosition.y + SPACING22 + 20f);
            this.currentPolitics.autoSize = true;

            this.benefit = base.AddUIComponent<UILabel>();
            this.benefit.text = Localization.Get("BENEFIT");
            this.benefit.relativePosition = new Vector3(SPACING, this.currentPolitics.relativePosition.y + SPACING22);
            this.benefit.autoSize = true;

            this.resident = base.AddUIComponent<UILabel>();
            this.resident.text = Localization.Get("RESIDENT_SALARY_TAX");
            this.resident.relativePosition = new Vector3(SPACING, this.benefit.relativePosition.y + SPACING22);
            this.resident.autoSize = true;

            this.commerical = base.AddUIComponent<UILabel>();
            this.commerical.text = Localization.Get("COMMERICAL_TRADE_TAX");
            this.commerical.relativePosition = new Vector3(SPACING, this.resident.relativePosition.y + SPACING22);
            this.commerical.autoSize = true;

            this.industrial = base.AddUIComponent<UILabel>();
            this.industrial.text = Localization.Get("INDUSTRIAL_TRADE_TAX");
            this.industrial.relativePosition = new Vector3(SPACING, this.commerical.relativePosition.y + SPACING22);
            this.industrial.autoSize = true;
        }


        private void RefreshDisplayData()
        {
            uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
            uint num2 = currentFrameIndex & 255u;

            if (refeshOnce)
            {
                if (base.isVisible)
                {
                    this.m_title.text = Localization.Get("PARLIAMENT_HALL");
                    this.parliamentSeats.text = Localization.Get("PARLIAMENT_SEATS");
                    this.communist.text = string.Format(Localization.Get("COMMUNIST") + " [{0}]", Politics.cPartySeats);
                    this.green.text = string.Format(Localization.Get("GREEN") + " [{0}]", Politics.gPartySeats);
                    this.socialist.text = string.Format(Localization.Get("SOCIALIST") + " [{0}]", Politics.sPartySeats);
                    this.liberal.text = string.Format(Localization.Get("LIBERAL") + " [{0}]", Politics.lPartySeats);
                    this.national.text = string.Format(Localization.Get("NATIONAL") + " [{0}]", Politics.nPartySeats);
                    this.nextVote.text = string.Format(Localization.Get("NEXT_VOTE") + " [{0}]", Politics.parliamentCount);

                    if (Politics.currentIdx > 7)
                    {
                        this.currentMeetingItem.text = string.Format(Localization.Get("CURRENT_MEETING_ITEM") + ": N/A");
                    }
                    else
                    {
                        switch(Politics.currentIdx)
                        {
                            case 0:
                                this.currentMeetingItem.text = string.Format(Localization.Get("CURRENT_MEETING_ITEM") + ":" + Localization.Get("RISE_RESIDENT_TAX"));
                                break;
                            case 1:
                                this.currentMeetingItem.text = string.Format(Localization.Get("CURRENT_MEETING_ITEM") + ":" + Localization.Get("FALL_RESIDENT_TAX"));
                                break;
                            case 2:
                                this.currentMeetingItem.text = string.Format(Localization.Get("CURRENT_MEETING_ITEM") + ":" + Localization.Get("RISE_BENEFIT"));
                                break;
                            case 3:
                                this.currentMeetingItem.text = string.Format(Localization.Get("CURRENT_MEETING_ITEM") + ":" + Localization.Get("FALL_BENEFIT"));
                                break;
                            case 4:
                                this.currentMeetingItem.text = string.Format(Localization.Get("CURRENT_MEETING_ITEM") + ":" + Localization.Get("RISE_COMMERIAL_TAX"));
                                break;
                            case 5:
                                this.currentMeetingItem.text = string.Format(Localization.Get("CURRENT_MEETING_ITEM") + ":" + Localization.Get("FALL_COMMERIAL_TAX"));
                                break;
                            case 6:
                                this.currentMeetingItem.text = string.Format(Localization.Get("CURRENT_MEETING_ITEM") + ":" + Localization.Get("RISE_INDUSTRIAL_TAX"));
                                break;
                            case 7:
                                this.currentMeetingItem.text = string.Format(Localization.Get("CURRENT_MEETING_ITEM") + ":" + Localization.Get("FALL_INDUSTRIAL_TAX"));
                                break;
                        }
                    }

                    this.voteResult.text = string.Format(Localization.Get("VOTE_RESULT") + ": " + Localization.Get("YES") + ":" + Politics.currentYes.ToString() + " " + Localization.Get("NO") + ":" + Politics.currentNo.ToString() + " " + Localization.Get("NO_ATTEND") + ":" + Politics.currentNoAttend.ToString());
                    this.currentPolitics.text = string.Format(Localization.Get("CURRENT_POLICY"));
                    this.benefit.text = string.Format(Localization.Get("BENEFIT") + " " + Politics.benefitOffset.ToString());
                    this.resident.text = string.Format(Localization.Get("RESIDENT_SALARY_TAX") + " " + Politics.residentTax.ToString() + "%");
                    this.commerical.text = string.Format(Localization.Get("COMMERICAL_TRADE_TAX") + " " + Politics.commericalTax.ToString() + "%");
                    this.industrial.text = string.Format(Localization.Get("INDUSTRIAL_TRADE_TAX") + " " + Politics.industryTax.ToString() + "%");

                    if (Politics.case1)
                    {
                        this.goverment.text = string.Format(Localization.Get("GOVERMENT") + " " + Localization.Get("COMMUNIST"));
                    }
                    else if (Politics.case2)
                    {
                        this.goverment.text = string.Format(Localization.Get("GOVERMENT") + " " + Localization.Get("GREEN"));
                    }
                    else if (Politics.case3)
                    {
                        this.goverment.text = string.Format(Localization.Get("GOVERMENT") + " " + Localization.Get("SOCIALIST"));
                    }
                    else if (Politics.case4)
                    {
                        this.goverment.text = string.Format(Localization.Get("GOVERMENT") + " " + Localization.Get("LIBERAL"));
                    }
                    else if (Politics.case5)
                    {
                        this.goverment.text = string.Format(Localization.Get("GOVERMENT") + " " + Localization.Get("NATIONAL"));
                    }
                    else if (Politics.case6)
                    {
                        this.goverment.text = string.Format(Localization.Get("GOVERMENT") + " " + Localization.Get("GREEN") + " " + Localization.Get("SOCIALIST") + " " + Localization.Get("LEFT_UNION"));
                    }
                    else if (Politics.case7)
                    {
                        this.goverment.text = string.Format(Localization.Get("GOVERMENT") + Localization.Get("COMMUNIST") + " " + Localization.Get("GREEN") + " " + Localization.Get("SOCIALIST") + " " + Localization.Get("WIDE_LEFT_UNION"));
                    }
                    else if (Politics.case8)
                    {
                        this.goverment.text = string.Format(Localization.Get("GOVERMENT") + Localization.Get("LIBERAL") + " " + Localization.Get("NATIONAL") + " " + Localization.Get("RIGHT_UNION"));
                    }
                    else
                    {
                        this.goverment.text = string.Format(Localization.Get("GOVERMENT") + Localization.Get("COMMUNIST") + " " + Localization.Get("GREEN") + " " + Localization.Get("SOCIALIST") + " " + Localization.Get("LIBERAL") + " " + Localization.Get("NATIONAL") + " " + Localization.Get("ALL_UNION"));
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
