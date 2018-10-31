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
    public class FoodButton : UIPanel
    {
        private UIButton FButton;

        private ItemClass.Availability CurrentMode;

        public static FoodButton instance;

        private UIDragHandle m_DragHandler;

        public static bool refeshOnce = false;

        public override void Start()
        {
            UIView aView = UIView.GetAView();
            base.name = "FoodPanel";
            base.width = 150f;
            base.height = 70f;
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 - 200f), 30f);
            this.BringToFront();
            //base.backgroundSprite = "MenuPanel";
            //base.autoLayout = true;
            base.opacity = 1f;
            this.CurrentMode = Singleton<ToolManager>.instance.m_properties.m_mode;
            this.m_DragHandler = base.AddUIComponent<UIDragHandle>();
            this.m_DragHandler.target = this;
            this.FButton = base.AddUIComponent<UIButton>();
            this.FButton.normalBgSprite = "RcButton";
            this.FButton.hoveredBgSprite = "RcButtonHovered";
            this.FButton.focusedBgSprite = "RcButtonFocused";
            this.FButton.pressedBgSprite = "RcButtonPressed";
            this.FButton.playAudioEvents = false;
            this.FButton.name = "FButton";
            this.FButton.tooltipBox = aView.defaultTooltipBox;
            this.FButton.text = Language.BuildingUI[20];
            this.FButton.size = new Vector2(150f, 40f);
            this.FButton.relativePosition = new Vector3(0, 30f);

        }

        public override void Update()
        {
            if (Loader.isGuiRunning)
            {
                if (refeshOnce)
                {
                    this.FButton.text = Language.BuildingUI[20] + ": " + MainDataStore.allFoodsFinal.ToString();
                    refeshOnce = false;
                }
                if (!MainDataStore.isFoodsGettedFinal)
                {
                    this.FButton.textColor = Color.red;
                }
                else
                {
                    this.FButton.textColor = Color.white;
                }
            }
        }
    }
}
