using ColossalFramework.UI;
using RealCity.Util;
using UnityEngine;

namespace RealCity.UI
{
    public class PoliticsButton : UIButton
    {
        private UIComponent MoreeconomicUITrigger_paneltime;
        private UIComponent MoreeconomicUITrigger_chirper;
        private UIComponent MoreeconomicUITrigger_esc;
        private UIComponent MoreeconomicUITrigger_infopanel;
        private UIComponent MoreeconomicUITrigger_bottombars;
        private UIDragHandle m_DragHandler;

        public static void MoreeconomicUIToggle()
        {
            if (!Loader.politicsUI.isVisible)
            {
                PoliticsUI.refeshOnce = true;
                Loader.politicsUI.Show();

                if (Loader.realCityUI.isVisible)
                {
                    Loader.realCityUI.Hide();
                }
            }
            else
            {
                Loader.politicsUI.Hide();
            }
        }

        public void MoreeconomicUIOff()
        {
            if (Loader.politicsUI.isVisible && !Loader.politicsUI.containsMouse && !containsMouse && MoreeconomicUITrigger_paneltime != null && !MoreeconomicUITrigger_paneltime.containsMouse)
            {
                Loader.politicsUI.Hide();
            }
        }

        public override void Start()
        {
            relativePosition = new Vector3((Loader.parentGuiView.fixedWidth / 2 + 350f), 35f);
            normalBgSprite = "ToolbarIconGroup1Nomarl";
            hoveredBgSprite = "ToolbarIconGroup1Hovered";
            focusedBgSprite = "ToolbarIconGroup1Focused";
            pressedBgSprite = "ToolbarIconGroup1Pressed";
            playAudioEvents = true;
            name = "EcButton";
            zOrder = 11;
            UISprite internalSprite = AddUIComponent<UISprite>();
            internalSprite.atlas = SpriteUtilities.GetAtlas(Loader.m_atlasName);
            internalSprite.spriteName = "Politics";
            internalSprite.relativePosition = new Vector3(0, 0);
            internalSprite.width = 50f;
            internalSprite.height = 50f;
            size = new Vector2(50f, 50f);
            m_DragHandler = AddUIComponent<UIDragHandle>();
            m_DragHandler.target = this;
            m_DragHandler.relativePosition = Vector2.zero;
            m_DragHandler.width = 50;
            m_DragHandler.height = 50;
            m_DragHandler.zOrder = 10;
            m_DragHandler.Start();
            m_DragHandler.enabled = true;
            eventDoubleClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                MoreeconomicUIToggle();
            };
            MoreeconomicUITrigger_chirper = UIView.Find<UIPanel>("ChirperPanel");
            MoreeconomicUITrigger_esc = UIView.Find<UIButton>("Esc");
            MoreeconomicUITrigger_infopanel = UIView.Find<UIPanel>("InfoPanel");
            MoreeconomicUITrigger_bottombars = UIView.Find<UISlicedSprite>("TSBar");
            MoreeconomicUITrigger_paneltime = UIView.Find<UIPanel>("PanelTime");
            if (MoreeconomicUITrigger_chirper != null && MoreeconomicUITrigger_paneltime != null)
            {
                MoreeconomicUITrigger_chirper.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    MoreeconomicUIOff();
                };
            }
            if (MoreeconomicUITrigger_esc != null && MoreeconomicUITrigger_paneltime != null)
            {
                MoreeconomicUITrigger_esc.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    MoreeconomicUIOff();
                };
            }
            if (MoreeconomicUITrigger_infopanel != null && MoreeconomicUITrigger_paneltime != null)
            {
                MoreeconomicUITrigger_infopanel.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    MoreeconomicUIOff();
                };
            }
            if (MoreeconomicUITrigger_bottombars != null && MoreeconomicUITrigger_paneltime != null)
            {
                MoreeconomicUITrigger_bottombars.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    MoreeconomicUIOff();
                };
            }
        }

        public override void Update()
        {
            if (Loader.isGuiRunning)
            {
                if (Loader.politicsUI.isVisible)
                {
                    Focus();
                    Hide();
                }
                else
                {
                    Unfocus();
                    Show();
                }
            }
            base.Update();
        }
    }
}
