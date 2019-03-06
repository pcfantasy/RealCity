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

        public static float WIDTH = 850f;

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
        private UILabel garbage;
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
            //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "Go to UI now");
            instance = this;
            base.size = new Vector2(WIDTH, HEIGHT);
            base.backgroundSprite = "MenuPanel";
            this.canFocus = true;
            this.isInteractive = true;
            this.BringToFront();
            //base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 + 150f), 5f);
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 + 50f), 170f);
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
            this.parliamentSeats.relativePosition = new Vector3(SPACING, 50f);
            this.parliamentSeats.autoSize = true;

            //data
            this.communist = base.AddUIComponent<UILabel>();
            this.communist.text = Language.PoliticsMessage[2];
            this.communist.relativePosition = new Vector3(SPACING, this.parliamentSeats.relativePosition.y + SPACING22);
            this.communist.autoSize = true;

            this.green = base.AddUIComponent<UILabel>();
            this.green.text = Language.PoliticsMessage[3];
            this.green.relativePosition = new Vector3(this.communist.relativePosition.x + this.communist.width + SPACING + 70f, this.communist.relativePosition.y);
            this.green.autoSize = true;

            this.socialist = base.AddUIComponent<UILabel>();
            this.socialist.text = Language.PoliticsMessage[4];
            this.socialist.relativePosition = new Vector3(this.green.relativePosition.x + this.green.width + SPACING + 70f, this.green.relativePosition.y);
            this.socialist.autoSize = true;

            this.liberal = base.AddUIComponent<UILabel>();
            this.liberal.text = Language.PoliticsMessage[5];
            this.liberal.relativePosition = new Vector3(this.socialist.relativePosition.x + this.socialist.width + SPACING + 70f, this.socialist.relativePosition.y);
            this.liberal.autoSize = true;

            this.national = base.AddUIComponent<UILabel>();
            this.national.text = Language.PoliticsMessage[6];
            this.national.relativePosition = new Vector3(this.liberal.relativePosition.x + this.liberal.width + SPACING + 70f, this.liberal.relativePosition.y);
            this.national.autoSize = true;

            goverment = base.AddUIComponent<UILabel>();
            goverment.text = Language.PoliticsMessage[7];
            goverment.relativePosition = new Vector3(SPACING, this.communist.relativePosition.y + SPACING22 + 20f);
            goverment.autoSize = true;

            this.nextVote = base.AddUIComponent<UILabel>();
            this.nextVote.text = Language.PoliticsMessage[13];
            this.nextVote.relativePosition = new Vector3(SPACING, this.goverment.relativePosition.y + SPACING22 + 20f);
            this.nextVote.autoSize = true;

            this.currentMeetingItem = base.AddUIComponent<UILabel>();
            this.currentMeetingItem.text = Language.PoliticsMessage[15];
            this.currentMeetingItem.textScale = 1.1f;
            this.currentMeetingItem.relativePosition = new Vector3(SPACING, this.nextVote.relativePosition.y + SPACING22 + 20f);
            this.currentMeetingItem.autoSize = true;

            this.voteResult = base.AddUIComponent<UILabel>();
            this.voteResult.text = Language.PoliticsMessage[26];
            this.voteResult.relativePosition = new Vector3(SPACING, this.currentMeetingItem.relativePosition.y + SPACING22);
            this.voteResult.autoSize = true;

            this.currentPolitics = base.AddUIComponent<UILabel>();
            this.currentPolitics.text = Language.PoliticsMessage[30];
            this.currentPolitics.textScale = 1.1f;
            this.currentPolitics.relativePosition = new Vector3(SPACING, this.voteResult.relativePosition.y + SPACING22 + 20f);
            this.currentPolitics.autoSize = true;

            this.garbage = base.AddUIComponent<UILabel>();
            this.garbage.text = Language.PoliticsMessage[31];
            this.garbage.relativePosition = new Vector3(SPACING, this.currentPolitics.relativePosition.y + SPACING22);
            this.garbage.autoSize = true;

            this.benefit = base.AddUIComponent<UILabel>();
            this.benefit.text = Language.PoliticsMessage[32];
            this.benefit.relativePosition = new Vector3(SPACING, this.garbage.relativePosition.y + SPACING22);
            this.benefit.autoSize = true;

            this.resident = base.AddUIComponent<UILabel>();
            this.resident.text = Language.PoliticsMessage[33];
            this.resident.relativePosition = new Vector3(SPACING, this.benefit.relativePosition.y + SPACING22);
            this.resident.autoSize = true;

            this.commerical = base.AddUIComponent<UILabel>();
            this.commerical.text = Language.PoliticsMessage[34];
            this.commerical.relativePosition = new Vector3(SPACING, this.resident.relativePosition.y + SPACING22);
            this.commerical.autoSize = true;

            this.industrial = base.AddUIComponent<UILabel>();
            this.industrial.text = Language.PoliticsMessage[35];
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
                    //citizen
                    this.m_title.text = Language.PoliticsMessage[0];
                    this.parliamentSeats.text = Language.PoliticsMessage[1];
                    this.communist.text = string.Format(Language.PoliticsMessage[2] + " [{0}]", Politics.cPartySeats);
                    this.green.text = string.Format(Language.PoliticsMessage[3] + " [{0}]", Politics.gPartySeats);
                    this.socialist.text = string.Format(Language.PoliticsMessage[4] + " [{0}]", Politics.sPartySeats);
                    this.liberal.text = string.Format(Language.PoliticsMessage[5] + " [{0}]", Politics.lPartySeats);
                    this.national.text = string.Format(Language.PoliticsMessage[6] + " [{0}]", Politics.nPartySeats);
                    this.nextVote.text = string.Format(Language.PoliticsMessage[13] + " [{0}]", Politics.parliamentCount);

                    if (Politics.currentIdx > 9)
                    {
                        this.currentMeetingItem.text = string.Format(Language.PoliticsMessage[15] + ": N/A");
                    }
                    else
                    {

                        this.currentMeetingItem.text = string.Format(Language.PoliticsMessage[15] + ":" + Language.PoliticsMessage[16 + (int)Politics.currentIdx]);
                    }

                    this.voteResult.text = string.Format(Language.PoliticsMessage[26] + ": " + Language.PoliticsMessage[27] + ":" + Politics.currentYes.ToString() + " " + Language.PoliticsMessage[28] + ":" + Politics.currentNo.ToString() + " " + Language.PoliticsMessage[29] + ":" + Politics.currentNoAttend.ToString());

                    this.currentPolitics.text = string.Format(Language.PoliticsMessage[30]);

                    this.garbage.text = string.Format(Language.PoliticsMessage[31] + Politics.garbageCount.ToString());

                    this.benefit.text = string.Format(Language.PoliticsMessage[32] + Politics.benefitOffset.ToString());

                    this.resident.text = string.Format(Language.PoliticsMessage[33] + Politics.residentTax.ToString() + "%");

                    this.commerical.text = string.Format(Language.PoliticsMessage[34] + Politics.commericalTax.ToString() + "%");

                    this.industrial.text = string.Format(Language.PoliticsMessage[35] + Politics.industryTax.ToString() + "%");

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
