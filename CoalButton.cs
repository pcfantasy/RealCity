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
    public class CoalButton : UIPanel
    {
        private UIButton CButton;

        private ItemClass.Availability CurrentMode;

        public static CoalButton instance;

        private UIDragHandle m_DragHandler;

        public static bool refeshOnce = false;

        public override void Start()
        {
            UIView aView = UIView.GetAView();
            base.name = "CoodPanel";
            base.width = 150f;
            base.height = 70f;
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth / 2 + 100f), 0f);
            this.BringToFront();
            //base.backgroundSprite = "MenuPanel";
            //base.autoLayout = true;
            base.opacity = 1f;
            this.CurrentMode = Singleton<ToolManager>.instance.m_properties.m_mode;
            this.m_DragHandler = base.AddUIComponent<UIDragHandle>();
            this.m_DragHandler.target = this;
            this.CButton = base.AddUIComponent<UIButton>();
            this.CButton.normalBgSprite = "RcButton";
            this.CButton.hoveredBgSprite = "RcButtonHovered";
            this.CButton.focusedBgSprite = "RcButtonFocused";
            this.CButton.pressedBgSprite = "RcButtonPressed";
            this.CButton.playAudioEvents = false;
            this.CButton.name = language.BuildingUI[32];
            this.CButton.tooltipBox = aView.defaultTooltipBox;
            this.CButton.text = "Coal";
            this.CButton.size = new Vector2(150f, 40f);
            this.CButton.relativePosition = new Vector3(0, 30f);

        }

        public override void Update()
        {
            if (Loader.isGuiRunning)
            {
                if (refeshOnce)
                {
                    this.CButton.tooltip = language.BuildingUI[32];
                    this.CButton.text = language.BuildingUI[32] + ": " + comm_data.allCoals.ToString();
                    refeshOnce = false;
                }
                if (!comm_data.isCoalsGettedFinal)
                {
                    this.CButton.textColor = Color.red;
                }
                else
                {
                    this.CButton.textColor = Color.white;
                }
            }
        }
    }
}
