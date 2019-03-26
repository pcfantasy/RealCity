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
            if (Loader.politicsUI.isVisible && !Loader.politicsUI.containsMouse && !base.containsMouse && this.MoreeconomicUITrigger_paneltime != null && !this.MoreeconomicUITrigger_paneltime.containsMouse)
            {
                Loader.politicsUI.Hide();
            }
        }

        public override void Start()
        {
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 + 350f), 35f);
            base.normalBgSprite = "ToolbarIconGroup1Nomarl";
            base.hoveredBgSprite = "ToolbarIconGroup1Hovered";
            base.focusedBgSprite = "ToolbarIconGroup1Focused";
            base.pressedBgSprite = "ToolbarIconGroup1Pressed";
            base.playAudioEvents = true;
            base.name = "EcButton";
            base.zOrder = 11;
            UISprite internalSprite = base.AddUIComponent<UISprite>();
            internalSprite.atlas = SpriteUtilities.GetAtlas(Loader.m_atlasName);
            internalSprite.spriteName = "Politics";
            internalSprite.relativePosition = new Vector3(0, 0);
            internalSprite.width = 50f;
            internalSprite.height = 50f;
            base.size = new Vector2(50f, 50f);
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
                PoliticsButton.MoreeconomicUIToggle();
            };
            this.MoreeconomicUITrigger_chirper = UIView.Find<UIPanel>("ChirperPanel");
            this.MoreeconomicUITrigger_esc = UIView.Find<UIButton>("Esc");
            this.MoreeconomicUITrigger_infopanel = UIView.Find<UIPanel>("InfoPanel");
            this.MoreeconomicUITrigger_bottombars = UIView.Find<UISlicedSprite>("TSBar");
            this.MoreeconomicUITrigger_paneltime = UIView.Find<UIPanel>("PanelTime");
            if (this.MoreeconomicUITrigger_chirper != null && this.MoreeconomicUITrigger_paneltime != null)
            {
                this.MoreeconomicUITrigger_chirper.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    this.MoreeconomicUIOff();
                };
            }
            if (this.MoreeconomicUITrigger_esc != null && this.MoreeconomicUITrigger_paneltime != null)
            {
                this.MoreeconomicUITrigger_esc.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    this.MoreeconomicUIOff();
                };
            }
            if (this.MoreeconomicUITrigger_infopanel != null && this.MoreeconomicUITrigger_paneltime != null)
            {
                this.MoreeconomicUITrigger_infopanel.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    this.MoreeconomicUIOff();
                };
            }
            if (this.MoreeconomicUITrigger_bottombars != null && this.MoreeconomicUITrigger_paneltime != null)
            {
                this.MoreeconomicUITrigger_bottombars.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    this.MoreeconomicUIOff();
                };
            }
        }

        public override void Update()
        {
            if (Loader.isGuiRunning)
            {
                if (Loader.politicsUI.isVisible)
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
