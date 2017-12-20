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
    public class CityButton : UIPanel
    {
        private UIButton RcButton;

        private UIComponent RealCityUITrigger_paneltime;

        private UIComponent RealCityUITrigger_chirper;

        private UIComponent RealCityUITrigger_esc;

        private UIComponent RealCityUITrigger_infopanel;

        private UIComponent RealCityUITrigger_bottombars;

        private ItemClass.Availability CurrentMode;

        public static CityButton instance;

        private UIDragHandle m_DragHandler;

        public static void RealCityUIToggle()
        {
            if (!Loader.guiPanel1.isVisible)
            {
                RealCityUI.refesh_onece = true;
                Loader.guiPanel1.Show();
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
            base.width = 100f;
            base.height = 70f;
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 + 150f), 5f);
            this.BringToFront();
            base.backgroundSprite = "MenuPanel";
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
            this.RcButton.text = language.RealCityUI1[111];
            this.RcButton.size = new Vector2(100f, 40f);
            this.RcButton.relativePosition = new Vector3(0, 30f);
            this.RcButton.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                CityButton.RealCityUIToggle();
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
                this.RcButton.tooltip = language.RealCityUI1[111];
                this.RcButton.text = language.RealCityUI1[111];
                if (comm_data.city_bank < -1000000)
                {
                    this.RcButton.textColor = Color.red;
                }
                if (Input.GetMouseButton(2) && Input.GetKeyDown(KeyCode.M))
                {
                    CityButton.RealCityUIToggle();
                }
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
        }
    }
}
