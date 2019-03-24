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
    public class EcnomicButton : UIPanel
    {
        private UIButton EcButton;
        private UIComponent MoreeconomicUITrigger_paneltime;
        private UIComponent MoreeconomicUITrigger_chirper;
        private UIComponent MoreeconomicUITrigger_esc;
        private UIComponent MoreeconomicUITrigger_infopanel;
        private UIComponent MoreeconomicUITrigger_bottombars;
        private ItemClass.Availability CurrentMode;
        public static EcnomicButton instance;
        private UIDragHandle m_DragHandler;

        public static void MoreeconomicUIToggle()
        {
            if (!Loader.guiPanel.isVisible)
            {
                EcnomicUI.refeshOnce = true;
                Loader.guiPanel.Show();
            }
            else
            {
                Loader.guiPanel.Hide();
            }
        }

        public void MoreeconomicUIOff()
        {
            if (Loader.guiPanel.isVisible && !Loader.guiPanel.containsMouse && !this.EcButton.containsMouse && this.MoreeconomicUITrigger_paneltime != null && !this.MoreeconomicUITrigger_paneltime.containsMouse)
            {
                Loader.guiPanel.Hide();
            }
        }

        public override void Start()
        {
            UIView aView = UIView.GetAView();
            base.name = "MoreeconomicUIPanel";
            base.width = 200f;
            base.height = 70f;
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 - 350f ), 35f);
            this.BringToFront();
            base.opacity = 1f;
            this.CurrentMode = Singleton<ToolManager>.instance.m_properties.m_mode;
            this.m_DragHandler = base.AddUIComponent<UIDragHandle>();
            this.m_DragHandler.target = this;
            this.EcButton = base.AddUIComponent<UIButton>();
            this.EcButton.normalBgSprite = "EcButton";
            this.EcButton.hoveredBgSprite = "EcButtonHovered";
            this.EcButton.focusedBgSprite = "EcButtonFocused";
            this.EcButton.pressedBgSprite = "EcButtonPressed";
            this.EcButton.playAudioEvents = true;
            this.EcButton.name = "EcButton";
            this.EcButton.tooltipBox = aView.defaultTooltipBox;
            if (Loader.m_atlasLoadedEcButton)
            {
                UISprite internalSprite = EcButton.AddUIComponent<UISprite>();
                internalSprite.atlas = SpriteUtilities.GetAtlas(Loader.m_atlasNameEcButton);
                internalSprite.spriteName = "EcButton";
                internalSprite.relativePosition = new Vector3(0, 0);
                internalSprite.width = 50f;
                internalSprite.height = 40f;
                base.width = 50f;
                base.height = 70f;
                EcButton.size = new Vector2(50f, 40f);
            }
            else
            {
                base.width = 200f;
                base.height = 70f;
                this.EcButton.text = Localization.Get("ECONOMIC_DATA");
                this.EcButton.size = new Vector2(200f, 40f);
            }
            this.EcButton.relativePosition = new Vector3(0, 30f);
            this.EcButton.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                EcnomicButton.MoreeconomicUIToggle();
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
                if (!Loader.m_atlasLoadedEcButton)
                {
                    this.EcButton.text = Localization.Get("ECONOMIC_DATA");
                }

                if (Loader.guiPanel.isVisible)
                {
                    this.EcButton.Focus();
                    base.Hide();
                }
                else
                {
                    this.EcButton.Unfocus();
                    base.Show();
                }
            }
            base.Update();
        }
    }
}
