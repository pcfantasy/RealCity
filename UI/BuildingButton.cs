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
            base.normalBgSprite = "ToolbarIconGroup1Nomarl";
            base.hoveredBgSprite = "ToolbarIconGroup1Hovered";
            base.focusedBgSprite = "ToolbarIconGroup1Focused";
            base.pressedBgSprite = "ToolbarIconGroup1Pressed";
            base.playAudioEvents = true;
            base.name = "BButton";
            UISprite internalSprite = base.AddUIComponent<UISprite>();
            internalSprite.atlas = SpriteUtilities.GetAtlas(Loader.m_atlasName);
            internalSprite.spriteName = "BuildingButton";
            internalSprite.relativePosition = new Vector3(0, 0);
            internalSprite.width = 40f;
            internalSprite.height = 40f;
            base.size = new Vector2(40f, 40f);
            base.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                BuildingButton.BuildingUIToggle();
            };
        }

        public override void Update()
        {
            MainDataStore.last_buildingid = WorldInfoPanel.GetCurrentInstanceID().Building;
            if ((Singleton<BuildingManager>.instance.m_buildings.m_buffer[MainDataStore.last_buildingid].Info.m_class.m_service != ItemClass.Service.Residential) && Loader.isGuiRunning)
            {
                base.Show();
            }
            else
            {
                base.Hide();
            }
            base.Update();
        }
    }
}
