using ColossalFramework.UI;
using RealCity.Util;
using UnityEngine;

namespace RealCity.UI
{
	public class EcnomicButton : UIButton
	{
		private UIComponent MoreeconomicUITrigger_paneltime;
		private UIComponent MoreeconomicUITrigger_chirper;
		private UIComponent MoreeconomicUITrigger_esc;
		private UIComponent MoreeconomicUITrigger_infopanel;
		private UIComponent MoreeconomicUITrigger_bottombars;
		private UIDragHandle m_DragHandler;
		private float tmpX;
		private float tmpY;
		public static void MoreeconomicUIToggle() {
			if (!Loader.ecnomicUI.isVisible) {
				EcnomicUI.refeshOnce = true;
				Loader.ecnomicUI.Show();
			} else {
				Loader.ecnomicUI.Hide();
			}
		}

		public void MoreeconomicUIOff() {
			if (Loader.ecnomicUI.isVisible && !Loader.ecnomicUI.containsMouse && !containsMouse && MoreeconomicUITrigger_paneltime != null && !MoreeconomicUITrigger_paneltime.containsMouse) {
				Loader.ecnomicUI.Hide();
			}
		}

		public override void Start() {
			name = "EcButton";
			relativePosition = new Vector3((Loader.parentGuiView.fixedWidth / 2 - 350f), 35f);
			normalBgSprite = "ToolbarIconGroup1Nomarl";
			hoveredBgSprite = "ToolbarIconGroup1Hovered";
			focusedBgSprite = "ToolbarIconGroup1Focused";
			pressedBgSprite = "ToolbarIconGroup1Pressed";
			playAudioEvents = true;
			UISprite internalSprite = AddUIComponent<UISprite>();
			internalSprite.atlas = SpriteUtilities.GetAtlas(Loader.m_atlasName);
			internalSprite.spriteName = "EcButton";
			internalSprite.relativePosition = new Vector3(0, 0);
			internalSprite.width = 50f;
			internalSprite.height = 50f;
			size = new Vector2(50f, 50f);
			zOrder = 11;
			m_DragHandler = AddUIComponent<UIDragHandle>();
			m_DragHandler.target = this;
			m_DragHandler.relativePosition = Vector2.zero;
			m_DragHandler.width = 50;
			m_DragHandler.height = 50;
			m_DragHandler.zOrder = 10;
			m_DragHandler.Start();
			m_DragHandler.enabled = true;
			tmpX = relativePosition.x;
			tmpY = relativePosition.y;
			eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam) {
				if (tmpX == relativePosition.x && tmpY == relativePosition.y) {
					MoreeconomicUIToggle();
				}
				tmpX = relativePosition.x;
				tmpY = relativePosition.y;
			};
			MoreeconomicUITrigger_chirper = UIView.Find<UIPanel>("ChirperPanel");
			MoreeconomicUITrigger_esc = UIView.Find<UIButton>("Esc");
			MoreeconomicUITrigger_infopanel = UIView.Find<UIPanel>("InfoPanel");
			MoreeconomicUITrigger_bottombars = UIView.Find<UISlicedSprite>("TSBar");
			MoreeconomicUITrigger_paneltime = UIView.Find<UIPanel>("PanelTime");
			if (MoreeconomicUITrigger_chirper != null && MoreeconomicUITrigger_paneltime != null) {
				MoreeconomicUITrigger_chirper.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam) {
					MoreeconomicUIOff();
				};
			}
			if (MoreeconomicUITrigger_esc != null && MoreeconomicUITrigger_paneltime != null) {
				MoreeconomicUITrigger_esc.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam) {
					MoreeconomicUIOff();
				};
			}
			if (MoreeconomicUITrigger_infopanel != null && MoreeconomicUITrigger_paneltime != null) {
				MoreeconomicUITrigger_infopanel.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam) {
					MoreeconomicUIOff();
				};
			}
			if (MoreeconomicUITrigger_bottombars != null && MoreeconomicUITrigger_paneltime != null) {
				MoreeconomicUITrigger_bottombars.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam) {
					MoreeconomicUIOff();
				};
			}
		}

		public override void Update() {
			if (Loader.isGuiRunning) {
				if (Loader.ecnomicUI.isVisible) {
					//Focus();
					Hide();
				} else {
					Unfocus();
					Show();
				}
			}
			base.Update();
		}
	}
}
