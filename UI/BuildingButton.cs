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
    public class BuildingButton : UIPanel
    {
        public static UIButton BButton;
        private ItemClass.Availability CurrentMode;
        public static BuildingButton instance;


        public static void BuildingUIToggle()
        {
            if (!Loader.guiPanel2.isVisible)
            {
                BuildingUI.refeshOnce = true;
                Loader.guiPanel2.Show();
            }
            else
            {
                Loader.guiPanel2.Hide();
            }
        }

        public override void Start()
        {
            UIView aView = UIView.GetAView();
            base.name = "BuildingUIPanel";
            base.opacity = 1f;
            this.CurrentMode = Singleton<ToolManager>.instance.m_properties.m_mode;
            BButton = base.AddUIComponent<UIButton>();
            BButton.normalBgSprite = "BButton";
            BButton.hoveredBgSprite = "BButtonHovered";
            BButton.focusedBgSprite = "BButtonFocused";
            BButton.pressedBgSprite = "BButtonPressed";
            BButton.playAudioEvents = true;
            BButton.name = "BButton";
            BButton.tooltipBox = aView.defaultTooltipBox;
            if (Loader.m_atlasLoadedBuildingButton)
            {
                UISprite internalSprite = BButton.AddUIComponent<UISprite>();
                internalSprite.atlas = SpriteUtilities.GetAtlas(Loader.m_atlasNameBuildingButton);
                internalSprite.spriteName = "BuildingButton";
                internalSprite.relativePosition = new Vector3(0, 0);
                internalSprite.width = 40f;
                internalSprite.height = 35f;
                base.width = 40f;
                base.height = 35f;
                BButton.size = new Vector2(40f, 35f);
            }
            else
            {
                BButton.text = Localization.Get("REALCITY_UI");
                BButton.textScale = 0.9f;
                base.width = 150f;
                base.height = 15f;
                BButton.size = new Vector2(150f, 15f);
            }
            BButton.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
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
                if (!Loader.m_atlasLoadedBuildingButton)
                {
                    BButton.text = Localization.Get("REALCITY_UI");
                }
            }
            else
            {
                base.Hide();
            }
            base.Update();
        }
    }
}
