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
    public class BuildingButton : UIPanel
    {
        private UIButton BButton;

        //private UIComponent BuildingUITrigger_paneltime;

        //private UIComponent BuildingUITrigger_chirper;

        //private UIComponent BuildingUITrigger_esc;

        //private UIComponent BuildingUITrigger_infopanel;

        //private UIComponent BuildingUITrigger_bottombars;

        private ItemClass.Availability CurrentMode;

        public static BuildingButton instance;

        public UIAlignAnchor Alignment;

        public UIPanel RefPanel;

        public static void BuildingUIToggle()
        {
            if (!Loader.guiPanel2.isVisible)
            {
                BuildingUI.refesh_once = true;
                Loader.guiPanel2.Show();
            }
            else
            {
                Loader.guiPanel2.Hide();
            }
        }

        /*public void BuildingUIOff()
        {
            if (Loader.guiPanel2.isVisible && !Loader.guiPanel2.containsMouse && !this.BButton.containsMouse && this.BuildingUITrigger_paneltime != null && !this.BuildingUITrigger_paneltime.containsMouse)
            {
                Loader.guiPanel2.Hide();
            }
        }*/

        public override void Start()
        {
            UIView aView = UIView.GetAView();
            base.name = "BuildingUIPanel";
            base.width = 30f;
            base.height = 30f;
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 + 150f), 5f);
            this.BringToFront();
            //base.backgroundSprite = "MenuPanel";
            //base.autoLayout = true;
            base.opacity = 1f;
            this.CurrentMode = Singleton<ToolManager>.instance.m_properties.m_mode;
            this.BButton = base.AddUIComponent<UIButton>();
            this.BButton.normalBgSprite = "BButton";
            this.BButton.hoveredBgSprite = "BButtonHovered";
            this.BButton.focusedBgSprite = "BButtonFocused";
            this.BButton.pressedBgSprite = "BButtonPressed";
            this.BButton.playAudioEvents = true;
            this.BButton.name = "BButton";
            this.BButton.tooltipBox = aView.defaultTooltipBox;
            this.BButton.text = "B";
            this.BButton.size = new Vector2(30f, 30f);
            this.BButton.relativePosition = new Vector3(0, 0f);
            base.AlignTo(this.RefPanel, this.Alignment);
            this.BButton.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                BuildingButton.BuildingUIToggle();
            };
            /*this.BuildingUITrigger_chirper = UIView.Find<UIPanel>("ChirperPanel");
            this.BuildingUITrigger_esc = UIView.Find<UIButton>("Esc");
            this.BuildingUITrigger_infopanel = UIView.Find<UIPanel>("InfoPanel");
            this.BuildingUITrigger_bottombars = UIView.Find<UISlicedSprite>("TSBar");
            this.BuildingUITrigger_paneltime = UIView.Find<UIPanel>("PanelTime");
            if (this.BuildingUITrigger_chirper != null && this.BuildingUITrigger_paneltime != null)
            {
                this.BuildingUITrigger_chirper.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    this.BuildingUIOff();
                };
            }
            if (this.BuildingUITrigger_esc != null && this.BuildingUITrigger_paneltime != null)
            {
                this.BuildingUITrigger_esc.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    this.BuildingUIOff();
                };
            }
            if (this.BuildingUITrigger_infopanel != null && this.BuildingUITrigger_paneltime != null)
            {
                this.BuildingUITrigger_infopanel.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    this.BuildingUIOff();
                };
            }
            if (this.BuildingUITrigger_bottombars != null && this.BuildingUITrigger_paneltime != null)
            {
                this.BuildingUITrigger_bottombars.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    this.BuildingUIOff();
                };
            }*/
        }

        public override void Update()
        {
            if (Loader.isGuiRunning)
            {
                this.BButton.tooltip = language.RealCityUI1[114];
                this.BButton.text = "B";
            }
        }
    }
}
