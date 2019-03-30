using ColossalFramework;
using ColossalFramework.UI;
using RealCity.Util;
using UnityEngine;

namespace RealCity.UI
{
    public class BuildingButton : UIButton
    {
        public static void BuildingUIToggle()
        {
            if (!Loader.buildingUI.isVisible)
            {
                BuildingUI.refeshOnce = true;
                Loader.buildingUI.Show();
            }
            else
            {
                Loader.buildingUI.Hide();
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
            eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                BuildingUIToggle();
            };
        }

        public override void Update()
        {
            MainDataStore.last_buildingid = WorldInfoPanel.GetCurrentInstanceID().Building;
            if ((Singleton<BuildingManager>.instance.m_buildings.m_buffer[MainDataStore.last_buildingid].Info.m_class.m_service != ItemClass.Service.Residential) && Loader.isGuiRunning)
            {
                relativePosition = new Vector3(120, Loader.buildingInfo.size.y - height);
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
