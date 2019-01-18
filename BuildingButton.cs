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
    public class BuildingButton : UIPanel
    {
        public static UIButton BButton;

        private ItemClass.Availability CurrentMode;

        public static BuildingButton instance;

        public UIAlignAnchor Alignment;

        public UIPanel RefPanel;

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
            base.width = 200f;
            base.height = 25f;
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 + 150f), 5f);
            this.BringToFront();
            //base.backgroundSprite = "MenuPanel";
            //base.autoLayout = true;
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
            BButton.text = Language.OptionUI[4];
            BButton.textScale = 0.9f;
            BButton.size = new Vector2(200f, 20f);
            BButton.relativePosition = new Vector3(0, 0f);
            base.AlignTo(this.RefPanel, this.Alignment);
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
            }
            else
            {
                base.Hide();
            }
            base.Update();
        }
    }
}
