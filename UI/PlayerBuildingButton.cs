using ColossalFramework.UI;
using RealCity.Util;
using UnityEngine;

namespace RealCity.UI
{
    public class PlayerBuildingButton : UIButton
    { 
        public static void PlayerBuildingUIToggle()
        {
            if (!Loader.playerBuildingUI.isVisible)
            {
                PlayerBuildingUI.refeshOnce = true;
                MainDataStore.last_buildingid = WorldInfoPanel.GetCurrentInstanceID().Building;
                Loader.playerBuildingUI.Show();
            }
            else
            {
                Loader.playerBuildingUI.Hide();
            }
        }

        public override void Start()
        {
            normalBgSprite = "ToolbarIconGroup1Nomarl";
            hoveredBgSprite = "ToolbarIconGroup1Hovered";
            focusedBgSprite = "ToolbarIconGroup1Focused";
            pressedBgSprite = "ToolbarIconGroup1Pressed";
            playAudioEvents = true;
            name = "PBButton";
            UISprite internalSprite = AddUIComponent<UISprite>();
            internalSprite.atlas = SpriteUtilities.GetAtlas(Loader.m_atlasName);
            internalSprite.spriteName = "BuildingButton";
            internalSprite.relativePosition = new Vector3(0, 0);
            internalSprite.width = 40f;
            internalSprite.height = 40f;
            size = new Vector2(40f, 40f);
            eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                PlayerBuildingUIToggle();
            };
        }

        public override void Update()
        {
            if (Loader.isGuiRunning)
            { 
                relativePosition = new Vector3(120, Loader.playerbuildingInfo.size.y - height);
                Show();
            }
            base.Update();
        }
    }
}
