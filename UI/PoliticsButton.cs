using ColossalFramework.UI;
using RealCity.Util;
using UnityEngine;

namespace RealCity.UI
{
    public class PoliticsButton : UIButton
    {
        private UIComponent PoliticsUITrigger_paneltime;
        private UIComponent PoliticsUITrigger_chirper;
        private UIComponent PoliticsUITrigger_esc;
        private UIComponent PoliticsUITrigger_infopanel;
        private UIComponent PoliticsUITrigger_bottombars;
        private UIDragHandle m_DragHandler;
        private float tmpX;
        private float tmpY;
        public static void PoliticsUIToggle()
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

        public void PoliticsUIOff()
        {
            if (Loader.politicsUI.isVisible && !Loader.politicsUI.containsMouse && !containsMouse && PoliticsUITrigger_paneltime != null && !PoliticsUITrigger_paneltime.containsMouse)
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
            tmpX = relativePosition.x;
            tmpY = relativePosition.y;
            eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                if (tmpX == relativePosition.x && tmpY == relativePosition.y)
                {
                    PoliticsUIToggle();
                }
                tmpX = relativePosition.x;
                tmpY = relativePosition.y;

            };
            PoliticsUITrigger_chirper = UIView.Find<UIPanel>("ChirperPanel");
            PoliticsUITrigger_esc = UIView.Find<UIButton>("Esc");
            PoliticsUITrigger_infopanel = UIView.Find<UIPanel>("InfoPanel");
            PoliticsUITrigger_bottombars = UIView.Find<UISlicedSprite>("TSBar");
            PoliticsUITrigger_paneltime = UIView.Find<UIPanel>("PanelTime");
            if (PoliticsUITrigger_chirper != null && PoliticsUITrigger_paneltime != null)
            {
                PoliticsUITrigger_chirper.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    PoliticsUIOff();
                };
            }
            if (PoliticsUITrigger_esc != null && PoliticsUITrigger_paneltime != null)
            {
                PoliticsUITrigger_esc.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    PoliticsUIOff();
                };
            }
            if (PoliticsUITrigger_infopanel != null && PoliticsUITrigger_paneltime != null)
            {
                PoliticsUITrigger_infopanel.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    PoliticsUIOff();
                };
            }
            if (PoliticsUITrigger_bottombars != null && PoliticsUITrigger_paneltime != null)
            {
                PoliticsUITrigger_bottombars.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    PoliticsUIOff();
                };
            }
        }

        public override void Update()
        {
            if (Loader.isGuiRunning)
            {
                if (Loader.politicsUI.isVisible)
                {
                    //Focus();
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
