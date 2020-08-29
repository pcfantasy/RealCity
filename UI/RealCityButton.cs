using ColossalFramework.UI;
using RealCity.Util;
using UnityEngine;

namespace RealCity.UI
{
    public class RealCityButton : UIButton
    {
        private UIComponent RealCityUITrigger_paneltime;
        private UIComponent RealCityUITrigger_chirper;
        private UIComponent RealCityUITrigger_esc;
        private UIComponent RealCityUITrigger_infopanel;
        private UIComponent RealCityUITrigger_bottombars;
        private UIDragHandle m_DragHandler;
        private float tmpX;
        private float tmpY;
        public static void RealCityUIToggle() {
            if (!Loader.realCityUI.isVisible) {
                RealCityUI.refeshOnce = true;
                Loader.realCityUI.Show();
                if (Loader.politicsUI.isVisible) {
                    Loader.politicsUI.Hide();
                }
            } else {
                Loader.realCityUI.Hide();
            }
        }

        public void RealCityUIOff() {
            if (Loader.realCityUI.isVisible && !Loader.realCityUI.containsMouse && !containsMouse && RealCityUITrigger_paneltime != null && !RealCityUITrigger_paneltime.containsMouse) {
                Loader.realCityUI.Hide();
            }
        }

        public override void Start() {
            name = "RcButton";
            relativePosition = new Vector3((Loader.parentGuiView.fixedWidth / 2 + 150f), 35f);
            normalBgSprite = "ToolbarIconGroup1Nomarl";
            hoveredBgSprite = "ToolbarIconGroup1Hovered";
            focusedBgSprite = "ToolbarIconGroup1Focused";
            pressedBgSprite = "ToolbarIconGroup1Pressed";
            playAudioEvents = true;
            UISprite internalSprite = AddUIComponent<UISprite>();
            internalSprite.atlas = SpriteUtilities.GetAtlas(Loader.m_atlasName);
            internalSprite.spriteName = "RcButton";
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
                    RealCityUIToggle();
                }
                tmpX = relativePosition.x;
                tmpY = relativePosition.y;
            };
            RealCityUITrigger_chirper = UIView.Find<UIPanel>("ChirperPanel");
            RealCityUITrigger_esc = UIView.Find<UIButton>("Esc");
            RealCityUITrigger_infopanel = UIView.Find<UIPanel>("InfoPanel");
            RealCityUITrigger_bottombars = UIView.Find<UISlicedSprite>("TSBar");
            RealCityUITrigger_paneltime = UIView.Find<UIPanel>("PanelTime");
            if (RealCityUITrigger_chirper != null && RealCityUITrigger_paneltime != null) {
                RealCityUITrigger_chirper.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam) {
                    RealCityUIOff();
                };
            }
            if (RealCityUITrigger_esc != null && RealCityUITrigger_paneltime != null) {
                RealCityUITrigger_esc.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam) {
                    RealCityUIOff();
                };
            }
            if (RealCityUITrigger_infopanel != null && RealCityUITrigger_paneltime != null) {
                RealCityUITrigger_infopanel.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam) {
                    RealCityUIOff();
                };
            }
            if (RealCityUITrigger_bottombars != null && RealCityUITrigger_paneltime != null) {
                RealCityUITrigger_bottombars.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam) {
                    RealCityUIOff();
                };
            }
        }

        public override void Update() {
            if (Loader.isGuiRunning) {
                if (Loader.realCityUI.isVisible) {
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