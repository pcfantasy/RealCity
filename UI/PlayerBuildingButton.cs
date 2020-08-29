using ColossalFramework.UI;
using RealCity.Util;
using UnityEngine;

namespace RealCity.UI
{
    public class PlayerBuildingButton : UIButton
    {
        private UIPanel playerBuildingInfo;
        private PlayerBuildingUI playerBuildingUI;
        private InstanceID BuildingID = InstanceID.Empty;
        public void PlayerBuildingUIToggle() {
            if ((!playerBuildingUI.isVisible) && (BuildingID != InstanceID.Empty)) {
                playerBuildingUI.position = new Vector3(playerBuildingInfo.size.x, playerBuildingInfo.size.y);
                playerBuildingUI.size = new Vector3(playerBuildingInfo.size.x, playerBuildingInfo.size.y);
                PlayerBuildingUI.refeshOnce = true;
                playerBuildingUI.Show();
            } else {
                playerBuildingUI.Hide();
            }
        }

        public override void Start() {
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
            //Setup PlayerBuildingUI
            var buildingWindowGameObject = new GameObject("buildingWindowObject");
            playerBuildingUI = (PlayerBuildingUI)buildingWindowGameObject.AddComponent(typeof(PlayerBuildingUI));
            playerBuildingInfo = UIView.Find<UIPanel>("(Library) CityServiceWorldInfoPanel");
            if (playerBuildingInfo == null) {
                DebugLog.LogToFileOnly("UIPanel not found (update broke the mod!): (Library) CityServiceWorldInfoPanel\nAvailable panels are:\n");
            }
            playerBuildingUI.transform.parent = playerBuildingInfo.transform;
            playerBuildingUI.baseBuildingWindow = playerBuildingInfo.gameObject.transform.GetComponentInChildren<CityServiceWorldInfoPanel>();
            eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam) {
                PlayerBuildingUIToggle();
            };
        }

        public override void Update() {
            if (Loader.isGuiRunning) {
                if (WorldInfoPanel.GetCurrentInstanceID() != InstanceID.Empty) {
                    BuildingID = WorldInfoPanel.GetCurrentInstanceID();
                }
                relativePosition = new Vector3(120, playerBuildingInfo.size.y - height);
                Show();
            }
            base.Update();
        }
    }
}