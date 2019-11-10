using ColossalFramework;
using ColossalFramework.UI;
using RealCity.Util;
using UnityEngine;

namespace RealCity.UI
{
    public class BuildingButton : UIButton
    {
        private UIPanel buildingInfo;
        private BuildingUI buildingUI;
        private InstanceID BuildingID = InstanceID.Empty;
        public void BuildingUIToggle()
        {
            if ((!buildingUI.isVisible) && (BuildingID != InstanceID.Empty) && (Singleton<BuildingManager>.instance.m_buildings.m_buffer[BuildingID.Building].Info.m_class.m_service != ItemClass.Service.Residential))
            {
                BuildingUI.refeshOnce = true;
                buildingUI.position = new Vector3(buildingInfo.size.x, buildingInfo.size.y);
                buildingUI.size = new Vector3(buildingInfo.size.x, buildingInfo.size.y);
                buildingUI.Show();
            }
            else
            {
                buildingUI.Hide();
            }
        }

        public override void Start()
        {
            normalBgSprite = "ToolbarIconGroup1Nomarl";
            hoveredBgSprite = "ToolbarIconGroup1Hovered";
            focusedBgSprite = "ToolbarIconGroup1Focused";
            pressedBgSprite = "ToolbarIconGroup1Pressed";
            playAudioEvents = true;
            name = "BButton";
            UISprite internalSprite = AddUIComponent<UISprite>();
            internalSprite.atlas = SpriteUtilities.GetAtlas(Loader.m_atlasName);
            internalSprite.spriteName = "BuildingButton";
            internalSprite.relativePosition = new Vector3(0, 0);
            internalSprite.width = 40f;
            internalSprite.height = 40f;
            size = new Vector2(40f, 40f);

            //Setup BuildingUI
            var buildingWindowGameObject = new GameObject("buildingWindowObject");
            buildingUI = (BuildingUI)buildingWindowGameObject.AddComponent(typeof(BuildingUI));
            buildingInfo = UIView.Find<UIPanel>("(Library) ZonedBuildingWorldInfoPanel");
            if (buildingInfo == null)
            {
                DebugLog.LogToFileOnly("UIPanel not found (update broke the mod!): (Library) ZonedBuildingWorldInfoPanel\nAvailable panels are:\n");
            }
            buildingUI.transform.parent = buildingInfo.transform;
            buildingUI.baseBuildingWindow = buildingInfo.gameObject.transform.GetComponentInChildren<ZonedBuildingWorldInfoPanel>();
            eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                BuildingUIToggle();
            };
        }

        public override void Update()
        {
            var buildingID = WorldInfoPanel.GetCurrentInstanceID().Building;
            if ((Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingID].Info.m_class.m_service != ItemClass.Service.Residential) && Loader.isGuiRunning)
            {
                if (WorldInfoPanel.GetCurrentInstanceID() != InstanceID.Empty)
                {
                    BuildingID = WorldInfoPanel.GetCurrentInstanceID();
                }
                relativePosition = new Vector3(120, buildingInfo.size.y - height);
                Show();
            }
            else
            {
                Hide();
            }
            base.Update();
        }
    }
}
