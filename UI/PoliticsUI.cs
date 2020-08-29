using ColossalFramework;
using ColossalFramework.UI;
using UnityEngine;
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
		private UILabel commercial;
		private UILabel industrial;
		public static bool refeshOnce = false;

		public override void Update() {
			RefreshDisplayData();
			base.Update();
		}

		public override void Start() {
			base.Start();
			size = new Vector2(WIDTH, HEIGHT);
			backgroundSprite = "MenuPanel";
			canFocus = true;
			isInteractive = true;
			BringToFront();
			relativePosition = new Vector3((Loader.parentGuiView.fixedWidth / 2 + 50f), 170f);
			opacity = 1f;
			cachedName = cacheName;
			m_DragHandler = AddUIComponent<UIDragHandle>();
			m_DragHandler.target = this;
			m_title = AddUIComponent<UILabel>();
			m_title.text = Localization.Get("PARLIAMENT_HALL");
			m_title.relativePosition = new Vector3(WIDTH / 2f - m_title.width / 2f - 25f, HEADER / 2f - m_title.height / 2f);
			m_title.textAlignment = UIHorizontalAlignment.Center;
			m_closeButton = AddUIComponent<UIButton>();
			m_closeButton.normalBgSprite = "buttonclose";
			m_closeButton.hoveredBgSprite = "buttonclosehover";
			m_closeButton.pressedBgSprite = "buttonclosepressed";
			m_closeButton.relativePosition = new Vector3(WIDTH - 35f, 5f, 10f);
			m_closeButton.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam) {
				Hide();
			};
			Hide(); //dont show in the beginning
			DoOnStartup();
		}

		private void DoOnStartup() {
			ShowOnGui();
			RefreshDisplayData();
		}

		private void ShowOnGui() {
			parliamentSeats = AddUIComponent<UILabel>();
			parliamentSeats.text = Localization.Get("PARLIAMENT_SEATS");
			parliamentSeats.textScale = 1.1f;
			parliamentSeats.relativePosition = new Vector3(SPACING, 50f);
			parliamentSeats.autoSize = true;

			communist = AddUIComponent<UILabel>();
			communist.text = Localization.Get("COMMUNIST");
			communist.relativePosition = new Vector3(SPACING, parliamentSeats.relativePosition.y + SPACING22);
			communist.autoSize = true;

			green = AddUIComponent<UILabel>();
			green.text = Localization.Get("GREEN");
			green.relativePosition = new Vector3(communist.relativePosition.x + 160f, communist.relativePosition.y);
			green.autoSize = true;

			socialist = AddUIComponent<UILabel>();
			socialist.text = Localization.Get("SOCIALIST");
			socialist.relativePosition = new Vector3(green.relativePosition.x + 160f, green.relativePosition.y);
			socialist.autoSize = true;

			liberal = AddUIComponent<UILabel>();
			liberal.text = Localization.Get("LIBERAL");
			liberal.relativePosition = new Vector3(socialist.relativePosition.x + 160f, socialist.relativePosition.y);
			liberal.autoSize = true;

			national = AddUIComponent<UILabel>();
			national.text = Localization.Get("NATIONAL");
			national.relativePosition = new Vector3(liberal.relativePosition.x + 160f, liberal.relativePosition.y);
			national.autoSize = true;

			goverment = AddUIComponent<UILabel>();
			goverment.text = Localization.Get("GOVERMENT");
			goverment.relativePosition = new Vector3(SPACING, communist.relativePosition.y + SPACING22 + 20f);
			goverment.autoSize = true;

			nextVote = AddUIComponent<UILabel>();
			nextVote.text = Localization.Get("NEXT_VOTE");
			nextVote.relativePosition = new Vector3(SPACING, goverment.relativePosition.y + SPACING22 + 20f);
			nextVote.autoSize = true;

			currentMeetingItem = AddUIComponent<UILabel>();
			currentMeetingItem.text = Localization.Get("CURRENT_MEETING_ITEM");
			currentMeetingItem.textScale = 1.1f;
			currentMeetingItem.relativePosition = new Vector3(SPACING, nextVote.relativePosition.y + SPACING22 + 20f);
			currentMeetingItem.autoSize = true;

			voteResult = AddUIComponent<UILabel>();
			voteResult.text = Localization.Get("VOTE_RESULT");
			voteResult.relativePosition = new Vector3(SPACING, currentMeetingItem.relativePosition.y + SPACING22);
			voteResult.autoSize = true;

			currentPolitics = AddUIComponent<UILabel>();
			currentPolitics.text = Localization.Get("CURRENT_POLICY");
			currentPolitics.textScale = 1.1f;
			currentPolitics.relativePosition = new Vector3(SPACING, voteResult.relativePosition.y + SPACING22 + 20f);
			currentPolitics.autoSize = true;

			benefit = AddUIComponent<UILabel>();
			benefit.text = Localization.Get("BENEFIT");
			benefit.relativePosition = new Vector3(SPACING, currentPolitics.relativePosition.y + SPACING22);
			benefit.autoSize = true;

			resident = AddUIComponent<UILabel>();
			resident.text = Localization.Get("RESIDENT_SALARY_TAX");
			resident.relativePosition = new Vector3(SPACING, benefit.relativePosition.y + SPACING22);
			resident.autoSize = true;

			commercial = AddUIComponent<UILabel>();
			commercial.text = Localization.Get("COMMERICAL_TRADE_TAX");
			commercial.relativePosition = new Vector3(SPACING, resident.relativePosition.y + SPACING22);
			commercial.autoSize = true;

			industrial = AddUIComponent<UILabel>();
			industrial.text = Localization.Get("INDUSTRIAL_TRADE_TAX");
			industrial.relativePosition = new Vector3(SPACING, commercial.relativePosition.y + SPACING22);
			industrial.autoSize = true;
		}

		private void RefreshDisplayData() {
			if (refeshOnce) {
				if (isVisible) {
					m_title.text = Localization.Get("PARLIAMENT_HALL");
					parliamentSeats.text = Localization.Get("PARLIAMENT_SEATS");
					communist.text = string.Format(Localization.Get("COMMUNIST") + " [{0}]", Politics.cPartySeats);
					green.text = string.Format(Localization.Get("GREEN") + " [{0}]", Politics.gPartySeats);
					socialist.text = string.Format(Localization.Get("SOCIALIST") + " [{0}]", Politics.sPartySeats);
					liberal.text = string.Format(Localization.Get("LIBERAL") + " [{0}]", Politics.lPartySeats);
					national.text = string.Format(Localization.Get("NATIONAL") + " [{0}]", Politics.nPartySeats);
					nextVote.text = string.Format(Localization.Get("NEXT_VOTE") + " [{0}]", Politics.parliamentCount);

					if (Politics.currentIdx > 7) {
						currentMeetingItem.text = string.Format(Localization.Get("CURRENT_MEETING_ITEM") + ": N/A");
					} else {
						switch (Politics.currentIdx) {
							case 0:
								currentMeetingItem.text = string.Format(Localization.Get("CURRENT_MEETING_ITEM") + ":" + Localization.Get("RISE_RESIDENT_TAX"));
								break;
							case 1:
								currentMeetingItem.text = string.Format(Localization.Get("CURRENT_MEETING_ITEM") + ":" + Localization.Get("FALL_RESIDENT_TAX"));
								break;
							case 2:
								currentMeetingItem.text = string.Format(Localization.Get("CURRENT_MEETING_ITEM") + ":" + Localization.Get("RISE_BENEFIT"));
								break;
							case 3:
								currentMeetingItem.text = string.Format(Localization.Get("CURRENT_MEETING_ITEM") + ":" + Localization.Get("FALL_BENEFIT"));
								break;
							case 4:
								currentMeetingItem.text = string.Format(Localization.Get("CURRENT_MEETING_ITEM") + ":" + Localization.Get("RISE_COMMERIAL_TAX"));
								break;
							case 5:
								currentMeetingItem.text = string.Format(Localization.Get("CURRENT_MEETING_ITEM") + ":" + Localization.Get("FALL_COMMERIAL_TAX"));
								break;
							case 6:
								currentMeetingItem.text = string.Format(Localization.Get("CURRENT_MEETING_ITEM") + ":" + Localization.Get("RISE_INDUSTRIAL_TAX"));
								break;
							case 7:
								currentMeetingItem.text = string.Format(Localization.Get("CURRENT_MEETING_ITEM") + ":" + Localization.Get("FALL_INDUSTRIAL_TAX"));
								break;
						}
					}

					voteResult.text = string.Format(Localization.Get("VOTE_RESULT") + ": " + Localization.Get("YES") + ":" + Politics.currentYes.ToString() + " " + Localization.Get("NO") + ":" + Politics.currentNo.ToString() + " " + Localization.Get("NO_ATTEND") + ":" + Politics.currentNoAttend.ToString());
					currentPolitics.text = string.Format(Localization.Get("CURRENT_POLICY"));
					benefit.text = string.Format(Localization.Get("BENEFIT") + " " + ((int)((Politics.benefitOffset * MainDataStore.govermentSalary) / 100f)).ToString());
					resident.text = string.Format(Localization.Get("RESIDENT_SALARY_TAX") + " " + (Politics.residentTax << 1).ToString() + "%");
					commercial.text = string.Format(Localization.Get("COMMERICAL_TRADE_TAX") + " " + Politics.commercialTax.ToString() + "%");
					industrial.text = string.Format(Localization.Get("INDUSTRIAL_TRADE_TAX") + " " + Politics.industryTax.ToString() + "%");

					if (Politics.case1) {
						goverment.text = string.Format(Localization.Get("GOVERMENT") + " " + Localization.Get("COMMUNIST"));
					} else if (Politics.case2) {
						goverment.text = string.Format(Localization.Get("GOVERMENT") + " " + Localization.Get("GREEN"));
					} else if (Politics.case3) {
						goverment.text = string.Format(Localization.Get("GOVERMENT") + " " + Localization.Get("SOCIALIST"));
					} else if (Politics.case4) {
						goverment.text = string.Format(Localization.Get("GOVERMENT") + " " + Localization.Get("LIBERAL"));
					} else if (Politics.case5) {
						goverment.text = string.Format(Localization.Get("GOVERMENT") + " " + Localization.Get("NATIONAL"));
					} else if (Politics.case6) {
						goverment.text = string.Format(Localization.Get("GOVERMENT") + " " + Localization.Get("GREEN") + " " + Localization.Get("SOCIALIST") + " " + Localization.Get("LEFT_UNION"));
					} else if (Politics.case7) {
						goverment.text = string.Format(Localization.Get("GOVERMENT") + Localization.Get("COMMUNIST") + " " + Localization.Get("GREEN") + " " + Localization.Get("SOCIALIST") + " " + Localization.Get("WIDE_LEFT_UNION"));
					} else if (Politics.case8) {
						goverment.text = string.Format(Localization.Get("GOVERMENT") + Localization.Get("LIBERAL") + " " + Localization.Get("NATIONAL") + " " + Localization.Get("RIGHT_UNION"));
					} else {
						goverment.text = string.Format(Localization.Get("GOVERMENT") + Localization.Get("COMMUNIST") + " " + Localization.Get("GREEN") + " " + Localization.Get("SOCIALIST") + " " + Localization.Get("LIBERAL") + " " + Localization.Get("NATIONAL") + " " + Localization.Get("ALL_UNION"));
					}

					refeshOnce = false;
				}
			}
		}
	}
}
