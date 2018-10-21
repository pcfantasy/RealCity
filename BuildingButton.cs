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
        private UIButton BButton;

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
            base.width = 30f;
            base.height = 30f;
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 + 150f), 5f);
            this.BringToFront();
            //base.backgroundSprite = "MenuPanel";
            //base.autoLayout = true;
            base.opacity = 1f;
            this.CurrentMode = Singleton<ToolManager>.instance.m_properties.m_mode;
            this.BButton = base.AddUIComponent<UIButton>();
            this.BButton.normalBgSprite = "BButton";
            this.BButton.hoveredBgSprite = "BButtonHovered";
            this.BButton.focusedBgSprite = "BButtonFocused";
            this.BButton.pressedBgSprite = "BButtonPressed";
            this.BButton.playAudioEvents = true;
            this.BButton.name = "BButton";
            this.BButton.tooltipBox = aView.defaultTooltipBox;
            this.BButton.text = "B";
            this.BButton.size = new Vector2(30f, 30f);
            this.BButton.relativePosition = new Vector3(0, 0f);
            base.AlignTo(this.RefPanel, this.Alignment);
            this.BButton.eventClick += delegate (UIComponent component, UIMouseEventParameter eventParam)
            {
                BuildingButton.BuildingUIToggle();
            };
        }

        public override void Update()
        {
            if (Loader.isGuiRunning)
            {
                this.BButton.text = "B";
            }
        }
    }
}
