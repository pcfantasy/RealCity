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
                MoreeconomicUI.refesh_onece = true;
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
            base.width = 120f;
            base.height = 70f;
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 - 450f ), 0f);
            this.BringToFront();
            //base.backgroundSprite = "MenuPanel";
            //base.autoLayout = true;
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
            this.EcButton.text = language.EconomicUI[0];
            this.EcButton.size = new Vector2(120f, 40f);
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
                this.EcButton.text = language.EconomicUI[0];
                this.EcButton.tooltip = language.EconomicUI[0];
                if (!comm_data.isCoalsGettedFinal || !comm_data.isFoodsGettedFinal || !comm_data.isPetrolsGettedFinal || !comm_data.isLumbersGettedFinal)  //lack of resource
                {
                    this.EcButton.textColor = Color.red;
                } else
                {
                    this.EcButton.textColor = Color.white;
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
        }
    }
}
