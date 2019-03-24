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
            if (Loader.m_atlasLoadedBuildingButton)
            {
                UISprite internalSprite = PBButton.AddUIComponent<UISprite>();
                internalSprite.atlas = SpriteUtilities.GetAtlas(Loader.m_atlasNameBuildingButton);
                internalSprite.spriteName = "BuildingButton";
                internalSprite.relativePosition = new Vector3(0, 0);
                internalSprite.width = 40f;
                internalSprite.height = 35f;
                base.width = 40f;
                base.height = 35f;
                PBButton.size = new Vector2(40f, 35f);
            }
            else
            {
                PBButton.text = Localization.Get("REALCITY_UI");
                PBButton.textScale = 0.9f;
                base.width = 150f;
                base.height = 15f;
                PBButton.size = new Vector2(150f, 15f);
            }
            PBButton.relativePosition = new Vector3(0f, 0f);
            PBButton.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                PlayerBuildingButton.PlayerBuildingUIToggle();
            };
        }

        public override void Update()
        {
            if (Loader.isGuiRunning)
            {
                base.Show();
                if (!Loader.m_atlasLoadedBuildingButton)
                {
                    PBButton.text = Localization.Get("REALCITY_UI");
                }
            }
            base.Update();
        }
    }
}
