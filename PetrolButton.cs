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
    public class PetrolButton : UIPanel
    {
        private UIButton PButton;

        private ItemClass.Availability CurrentMode;

        public static PetrolButton instance;

        private UIDragHandle m_DragHandler;

        public static bool refeshOnce = false;

        public override void Start()
        {
            UIView aView = UIView.GetAView();
            base.name = "PetrolButton";
            base.width = 150f;
            base.height = 70f;
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 + 250f), 30f);
            this.BringToFront();
            //base.backgroundSprite = "MenuPanel";
            //base.autoLayout = true;
            base.opacity = 1f;
            this.CurrentMode = Singleton<ToolManager>.instance.m_properties.m_mode;
            this.m_DragHandler = base.AddUIComponent<UIDragHandle>();
            this.m_DragHandler.target = this;
            this.PButton = base.AddUIComponent<UIButton>();
            this.PButton.normalBgSprite = "RcButton";
            this.PButton.hoveredBgSprite = "RcButtonHovered";
            this.PButton.focusedBgSprite = "RcButtonFocused";
            this.PButton.pressedBgSprite = "RcButtonPressed";
            this.PButton.playAudioEvents = false;
            this.PButton.name = "PButton";
            this.PButton.tooltipBox = aView.defaultTooltipBox;
            this.PButton.text = Language.BuildingUI[34];
            this.PButton.size = new Vector2(150f, 40f);
            this.PButton.relativePosition = new Vector3(0, 30f);

        }

        public override void Update()
        {
            if (Loader.isGuiRunning)
            {
                if (refeshOnce)
                {
                    this.PButton.tooltip = Language.BuildingUI[34];
                    this.PButton.text = Language.BuildingUI[34] + ": " + MainDataStore.allPetrolsFinal.ToString();
                    refeshOnce = false;
                }
                if (!MainDataStore.isPetrolsGettedFinal)
                {
                    this.PButton.textColor = Color.red;
                }
                else
                {
                    this.PButton.textColor = Color.white;
                }
            }
        }
    }
}
