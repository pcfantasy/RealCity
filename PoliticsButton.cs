using ColossalFramework;
using ColossalFramework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RealCity
{
    public class PoliticsButton : UIPanel
    {
        private UIButton PLButton;

        private UIComponent MoreeconomicUITrigger_paneltime;

        private UIComponent MoreeconomicUITrigger_chirper;

        private UIComponent MoreeconomicUITrigger_esc;

        private UIComponent MoreeconomicUITrigger_infopanel;

        private UIComponent MoreeconomicUITrigger_bottombars;

        private ItemClass.Availability CurrentMode;

        public static PoliticsButton instance;

        private UIDragHandle m_DragHandler;

        public static void MoreeconomicUIToggle()
        {
            if (!Loader.guiPanel5.isVisible)
            {
                PoliticsUI.refeshOnce = true;
                Loader.guiPanel5.Show();

                if (Loader.guiPanel1.isVisible)
                {
                    Loader.guiPanel1.Hide();
                }
            }
            else
            {
                Loader.guiPanel5.Hide();
            }
        }

        public void MoreeconomicUIOff()
        {
            if (Loader.guiPanel5.isVisible && !Loader.guiPanel5.containsMouse && !this.PLButton.containsMouse && this.MoreeconomicUITrigger_paneltime != null && !this.MoreeconomicUITrigger_paneltime.containsMouse)
            {
                Loader.guiPanel5.Hide();
            }
        }

        public override void Start()
        {
            UIView aView = UIView.GetAView();
            base.name = "PoliticsUIPanel";
            base.width = 200f;
            base.height = 70f;
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 + 600f ), 30f);
            this.BringToFront();
            //base.backgroundSprite = "MenuPanel";
            //base.autoLayout = true;
            base.opacity = 1f;
            this.CurrentMode = Singleton<ToolManager>.instance.m_properties.m_mode;
            this.m_DragHandler = base.AddUIComponent<UIDragHandle>();
            this.m_DragHandler.target = this;
            this.PLButton = base.AddUIComponent<UIButton>();
            this.PLButton.normalBgSprite = "PLButton";
            this.PLButton.hoveredBgSprite = "PLButtonHovered";
            this.PLButton.focusedBgSprite = "PLButtonFocused";
            this.PLButton.pressedBgSprite = "PLButtonPressed";
            this.PLButton.playAudioEvents = true;
            this.PLButton.name = "PLButton";
            this.PLButton.tooltipBox = aView.defaultTooltipBox;
            this.PLButton.text = Language.PoliticsMessage[0] + Language.OptionUI[4];
            this.PLButton.size = new Vector2(200f, 40f);
            this.PLButton.relativePosition = new Vector3(0, 30f);
            this.PLButton.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
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
                this.PLButton.text = Language.PoliticsMessage[0] + Language.OptionUI[4];
                this.PLButton.tooltip = Language.EconomicUI[0];
                if (Politics.parliamentCount < 5)  //time is ok
                {
                    this.PLButton.textColor = Color.red;
                } else
                {
                    this.PLButton.textColor = Color.white;
                }

                if (Loader.guiPanel5.isVisible)
                {
                    this.PLButton.Focus();
                    base.Hide();
                }
                else
                {
                    this.PLButton.Unfocus();
                    base.Show();
                }
            }
        }
    }
}
