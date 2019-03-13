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
            }
            else
            {
                Loader.guiPanel4.Hide();
            }
        }

        public override void Start()
        {
            UIView aView = UIView.GetAView();
            base.name = "PlayerBuildingUIPanel";
            base.width = 200f;
            base.height = 25f;
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 + 150f), 5f);
            this.BringToFront();
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
        }

        public override void Update()
        {
            if (Loader.isGuiRunning)
            {
                if (Loader.isTransportLinesManagerRunning)
                {
                    if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[MainDataStore.last_buildingid].Info.m_class.m_service == ItemClass.Service.PublicTransport)
                    {
                        base.Hide();
                        Loader.guiPanel4.Show();
                    }
                    else
                    {
                        base.Show();
                    }
                }
                PBButton.text = Language.OptionUI[4];
            }
            base.Update();
        }
    }
}
