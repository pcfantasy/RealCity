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
    public class PlayerBuildingButton : UIPanel
    {
        private UIButton PBButton;

        //private UIComponent PlayerBuildingUITrigger_paneltime;

        //private UIComponent PlayerBuildingUITrigger_chirper;

        //private UIComponent PlayerBuildingUITrigger_esc;

        //private UIComponent PlayerBuildingUITrigger_infopanel;

        //private UIComponent PlayerBuildingUITrigger_bottombars;

        private ItemClass.Availability CurrentMode;

        public static PlayerBuildingButton instance;

        public UIAlignAnchor Alignment;

        public UIPanel RefPanel;

        public static void PlayerBuildingUIToggle()
        {
            if (!Loader.guiPanel4.isVisible)
            {
                PlayerBuildingUI.refesh_once = true;
                comm_data.last_buildingid = WorldInfoPanel.GetCurrentInstanceID().Building;
                if (RealCity.EconomyExtension.is_special_building(comm_data.last_buildingid) == 3)
                {
                    Loader.guiPanel4.Show();
                }
            }
            else
            {
                Loader.guiPanel4.Hide();
            }
        }

        /*public void PlayerBuildingUIOff()
        {
            if (Loader.guiPanel4.isVisible && !Loader.guiPanel4.containsMouse && !this.PBButton.containsMouse && this.PlayerBuildingUITrigger_paneltime != null && !this.PlayerBuildingUITrigger_paneltime.containsMouse)
            {
                Loader.guiPanel4.Hide();
            }
        }*/

        public override void Start()
        {
            UIView aView = UIView.GetAView();
            base.name = "PlayerBuildingUIPanel";
            base.width = 30f;
            base.height = 30f;
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 + 150f), 5f);
            this.BringToFront();
            //base.backgroundSprite = "MenuPanel";
            //base.autoLayout = true;
            base.opacity = 1f;
            this.CurrentMode = Singleton<ToolManager>.instance.m_properties.m_mode;
            this.PBButton = base.AddUIComponent<UIButton>();
            this.PBButton.normalBgSprite = "PBButton";
            this.PBButton.hoveredBgSprite = "PBButtonHovered";
            this.PBButton.focusedBgSprite = "PBButtonFocused";
            this.PBButton.pressedBgSprite = "PBButtonPressed";
            this.PBButton.playAudioEvents = true;
            this.PBButton.name = "PBButton";
            this.PBButton.tooltipBox = aView.defaultTooltipBox;
            this.PBButton.text = "B";
            this.PBButton.size = new Vector2(30f, 30f);
            this.PBButton.relativePosition = new Vector3(0, 0f);
            base.AlignTo(this.RefPanel, this.Alignment);
            this.PBButton.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                PlayerBuildingButton.PlayerBuildingUIToggle();
            };
            /*this.PlayerBuildingUITrigger_chirper = UIView.Find<UIPanel>("ChirperPanel");
            this.PlayerBuildingUITrigger_esc = UIView.Find<UIButton>("Esc");
            this.PlayerBuildingUITrigger_infopanel = UIView.Find<UIPanel>("InfoPanel");
            this.PlayerBuildingUITrigger_bottombars = UIView.Find<UISlicedSprite>("TSBar");
            this.PlayerBuildingUITrigger_paneltime = UIView.Find<UIPanel>("PanelTime");
            if (this.PlayerBuildingUITrigger_chirper != null && this.PlayerBuildingUITrigger_paneltime != null)
            {
                this.PlayerBuildingUITrigger_chirper.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    this.PlayerBuildingUIOff();
                };
            }
            if (this.PlayerBuildingUITrigger_esc != null && this.PlayerBuildingUITrigger_paneltime != null)
            {
                this.PlayerBuildingUITrigger_esc.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    this.PlayerBuildingUIOff();
                };
            }
            if (this.PlayerBuildingUITrigger_infopanel != null && this.PlayerBuildingUITrigger_paneltime != null)
            {
                this.PlayerBuildingUITrigger_infopanel.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    this.PlayerBuildingUIOff();
                };
            }
            if (this.PlayerBuildingUITrigger_bottombars != null && this.PlayerBuildingUITrigger_paneltime != null)
            {
                this.PlayerBuildingUITrigger_bottombars.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
                {
                    this.PlayerBuildingUIOff();
                };
            }*/
        }

        public override void Update()
        {
            if (Loader.isGuiRunning)
            {
                this.PBButton.tooltip = language.RealCityUI1[114];
                this.PBButton.text = "B";
            }
        }
    }
}
