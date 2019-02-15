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
    public class RealCityButton : UIPanel
    {
        private UIButton RcButton;

        private UIComponent RealCityUITrigger_paneltime;

        private UIComponent RealCityUITrigger_chirper;

        private UIComponent RealCityUITrigger_esc;

        private UIComponent RealCityUITrigger_infopanel;

        private UIComponent RealCityUITrigger_bottombars;

        private ItemClass.Availability CurrentMode;

        public static RealCityButton instance;

        private UIDragHandle m_DragHandler;

        public static void RealCityUIToggle()
        {
            if (!Loader.guiPanel1.isVisible)
            {
                RealCityUI.refeshOnce = true;
                Loader.guiPanel1.Show();
                if (Loader.guiPanel5.isVisible)
                {
                    Loader.guiPanel5.Hide();
                }
            }
            else
            {
                Loader.guiPanel1.Hide();
            }
        }

        public void RealCityUIOff()
        {
            if (Loader.guiPanel1.isVisible && !Loader.guiPanel1.containsMouse && !this.RcButton.containsMouse && this.RealCityUITrigger_paneltime != null && !this.RealCityUITrigger_paneltime.containsMouse)
            {
                Loader.guiPanel1.Hide();
            }
        }

        public override void Start()
        {
            UIView aView = UIView.GetAView();
            base.name = "RealCityUIPanel";
            base.width = 200f;
            base.height = 70f;
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 + 50f), 35f);
            this.BringToFront();
            //base.backgroundSprite = "MenuPanel";
            //base.autoLayout = true;
            base.opacity = 1f;
            this.CurrentMode = Singleton<ToolManager>.instance.m_properties.m_mode;
            this.m_DragHandler = base.AddUIComponent<UIDragHandle>();
            this.m_DragHandler.target = this;
            this.RcButton = base.AddUIComponent<UIButton>();
            this.RcButton.normalBgSprite = "RcButton";
            this.RcButton.hoveredBgSprite = "RcButtonHovered";
            this.RcButton.focusedBgSprite = "RcButtonFocused";
            this.RcButton.pressedBgSprite = "RcButtonPressed";
            this.RcButton.playAudioEvents = true;
            this.RcButton.name = "RcButton";
            this.RcButton.tooltipBox = aView.defaultTooltipBox;
            this.RcButton.text = Language.RealCityUI1[50] + Language.OptionUI[3];
            this.RcButton.size = new Vector2(200f, 40f);
            this.RcButton.relativePosition = new Vector3(0, 30f);
            this.RcButton.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
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
                this.RcButton.tooltip = Language.RealCityUI1[50] + Language.OptionUI[3];
                this.RcButton.text = Language.RealCityUI1[50] + Language.OptionUI[3];
                if (Loader.guiPanel1.isVisible)
                {
                    this.RcButton.Focus();
                    base.Hide();
                }
                else
                {
                    this.RcButton.Unfocus();
                    base.Show();
                }
            }
            base.Update();
        }
    }
}
