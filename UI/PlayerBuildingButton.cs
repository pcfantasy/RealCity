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
    public class PlayerBuildingButton : UIPanel
    {
        public static UIButton PBButton;

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
                PlayerBuildingUI.refeshOnce = true;
                MainDataStore.last_buildingid = WorldInfoPanel.GetCurrentInstanceID().Building;
                Loader.guiPanel4.Show();
                /*if (Loader.isRealConstructionRunning)
                {
                    RealConstruction.PlayerBuildingUI.refeshOnce = true;
                    RealConstruction.MainDataStore.last_buildingid = WorldInfoPanel.GetCurrentInstanceID().Building;
                    RealConstruction.Loader.guiPanel4.Show();
                }*/
            }
            else
            {
                Loader.guiPanel4.Hide();
                /*if (Loader.isRealConstructionRunning)
                {
                    RealConstruction.Loader.guiPanel4.Hide();
                }*/
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
            base.width = 200f;
            base.height = 25f;
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 + 150f), 5f);
            this.BringToFront();
            //base.backgroundSprite = "MenuPanel";
            //base.autoLayout = true;
            base.opacity = 1f;
            this.CurrentMode = Singleton<ToolManager>.instance.m_properties.m_mode;
            PBButton = base.AddUIComponent<UIButton>();
            PBButton.normalBgSprite = "PBButton";
            PBButton.hoveredBgSprite = "PBButtonHovered";
            PBButton.focusedBgSprite = "PBButtonFocused";
            PBButton.pressedBgSprite = "PBButtonPressed";
            PBButton.playAudioEvents = true;
            PBButton.name = "PBButton";
            PBButton.tooltipBox = aView.defaultTooltipBox;
            PBButton.text = Language.OptionUI[4];
            PBButton.textScale = 0.9f;
            PBButton.size = new Vector2(200f, 20f);
            PBButton.relativePosition = new Vector3(0, 0f);
            base.AlignTo(this.RefPanel, this.Alignment);
            PBButton.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
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
                PBButton.text = Language.OptionUI[4];
            }
            base.Update();
        }
    }
}
