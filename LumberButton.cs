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
    public class LumberButton : UIPanel
    {
        private UIButton LButton;

        private ItemClass.Availability CurrentMode;

        public static LumberButton instance;

        private UIDragHandle m_DragHandler;

        public static bool refeshOnce = false;

        public override void Start()
        {
            UIView aView = UIView.GetAView();
            base.name = "LumberPanel";
            base.width = 150f;
            base.height = 70f;
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 - 50f ), 30f);
            this.BringToFront();
            //base.backgroundSprite = "MenuPanel";
            //base.autoLayout = true;
            base.opacity = 1f;
            this.CurrentMode = Singleton<ToolManager>.instance.m_properties.m_mode;
            this.m_DragHandler = base.AddUIComponent<UIDragHandle>();
            this.m_DragHandler.target = this;
            this.LButton = base.AddUIComponent<UIButton>();
            this.LButton.normalBgSprite = "RcButton";
            this.LButton.hoveredBgSprite = "RcButtonHovered";
            this.LButton.focusedBgSprite = "RcButtonFocused";
            this.LButton.pressedBgSprite = "RcButtonPressed";
            this.LButton.playAudioEvents = false;
            this.LButton.name = "LButton";
            this.LButton.tooltipBox = aView.defaultTooltipBox;
            this.LButton.text = Language.BuildingUI[22];
            this.LButton.size = new Vector2(150f, 40f);
            this.LButton.relativePosition = new Vector3(0, 30f);

        }

        public override void Update()
        {
            if (Loader.isGuiRunning)
            {
                if (refeshOnce)
                {
                    this.LButton.text = Language.BuildingUI[22] + ": " + MainDataStore.allLumbersFinal.ToString();
                    refeshOnce = false;
                }
                if (!MainDataStore.isLumbersGettedFinal)
                {
                    this.LButton.textColor = Color.red;
                }
                else
                {
                    this.LButton.textColor = Color.white;
                }
            }
        }
    }
}
