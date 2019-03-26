using ColossalFramework;
using ColossalFramework.UI;
using RealCity.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static void RealCityUIToggle()
        {
            if (!Loader.realCityUI.isVisible)
            {
                RealCityUI.refeshOnce = true;
                Loader.realCityUI.Show();
                if (Loader.politicsUI.isVisible)
                {
                    Loader.politicsUI.Hide();
                }
            }
            else
            {
                Loader.realCityUI.Hide();
            }
        }

        public void RealCityUIOff()
        {
            if (Loader.realCityUI.isVisible && !Loader.realCityUI.containsMouse && !base.containsMouse && this.RealCityUITrigger_paneltime != null && !this.RealCityUITrigger_paneltime.containsMouse)
            {
                Loader.realCityUI.Hide();
            }
        }

        public override void Start()
        {
            base.name = "RcButton";
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 + 150f), 35f);
            base.normalBgSprite = "ToolbarIconGroup1Nomarl";
            base.hoveredBgSprite = "ToolbarIconGroup1Hovered";
            base.focusedBgSprite = "ToolbarIconGroup1Focused";
            base.pressedBgSprite = "ToolbarIconGroup1Pressed";
            base.playAudioEvents = true;
            UISprite internalSprite = base.AddUIComponent<UISprite>();
            internalSprite.atlas = SpriteUtilities.GetAtlas(Loader.m_atlasName);
            internalSprite.spriteName = "RcButton";
            internalSprite.relativePosition = new Vector3(0, 0);
            internalSprite.width = 50f;
            internalSprite.height = 50f;
            base.size = new Vector2(50f, 50f);
            base.zOrder = 11;
            this.m_DragHandler = base.AddUIComponent<UIDragHandle>();
            this.m_DragHandler.target = this;
            this.m_DragHandler.relativePosition = Vector2.zero;
            this.m_DragHandler.width = 50;
            this.m_DragHandler.height = 50;
            this.m_DragHandler.zOrder = 10;
            this.m_DragHandler.Start();
            this.m_DragHandler.enabled = true;
            base.eventDoubleClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                RealCityButton.RealCityUIToggle();
            };
            this.RealCityUITrigger_chirper = UIView.Find<UIPanel>("ChirperPanel");
            this.RealCityUITrigger_esc = UIView.Find<UIButton>("Esc");
            this.RealCityUITrigger_infopanel = UIView.Find<UIPanel>("InfoPanel");
            this.RealCityUITrigger_bottombars = UIView.Find<UISlicedSprite>("TSBar");
            this.RealCityUITrigger_paneltime = UIView.Find<UIPanel>("PanelTime");
            if (this.RealCityUITrigger_chirper != null && this.RealCityUITrigger_paneltime != null)
            {
                this.RealCityUITrigger_chirper.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    this.RealCityUIOff();
                };
            }
            if (this.RealCityUITrigger_esc != null && this.RealCityUITrigger_paneltime != null)
            {
                this.RealCityUITrigger_esc.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    this.RealCityUIOff();
                };
            }
            if (this.RealCityUITrigger_infopanel != null && this.RealCityUITrigger_paneltime != null)
            {
                this.RealCityUITrigger_infopanel.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    this.RealCityUIOff();
                };
            }
            if (this.RealCityUITrigger_bottombars != null && this.RealCityUITrigger_paneltime != null)
            {
                this.RealCityUITrigger_bottombars.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    this.RealCityUIOff();
                };
            }
        }

        public override void Update()
        {
            if (Loader.isGuiRunning)
            {
                if (Loader.realCityUI.isVisible)
                {
                    base.Focus();
                    base.Hide();
                }
                else
                {
                    base.Unfocus();
                    base.Show();
                }
            }
            base.Update();
        }
    }
}
