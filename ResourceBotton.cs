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
    public class ResourceBotton : UIPanel
    {
        private UIButton FButton;
        private UIButton LButton;
        private UIButton CButton;
        private UIButton PButton;

        private ItemClass.Availability CurrentMode;

        public static ResourceBotton instance;

        private UIDragHandle m_DragHandler;

        public static bool refeshOnce = false;

        public override void Start()
        {
            UIView aView = UIView.GetAView();
            base.name = "FoodPanel";
            base.width = 150f;
            base.height = 70f;
            base.relativePosition = new Vector3((float)(Loader.parentGuiView.fixedWidth - 180f), 40f);
            this.BringToFront();
            //base.backgroundSprite = "MenuPanel";
            //base.autoLayout = true;
            base.opacity = 1f;
            this.CurrentMode = Singleton<ToolManager>.instance.m_properties.m_mode;
            this.m_DragHandler = base.AddUIComponent<UIDragHandle>();
            this.m_DragHandler.target = this;
            this.FButton = base.AddUIComponent<UIButton>();
            this.FButton.playAudioEvents = false;
            this.FButton.name = "FButton";
            this.FButton.tooltipBox = aView.defaultTooltipBox;
            this.FButton.text = Language.BuildingUI[20];
            this.FButton.size = new Vector2(150f, 40f);
            this.FButton.relativePosition = new Vector3(0, 30f);

            this.LButton = base.AddUIComponent<UIButton>();
            this.LButton.playAudioEvents = false;
            this.LButton.name = "LButton";
            this.LButton.tooltipBox = aView.defaultTooltipBox;
            this.LButton.text = Language.BuildingUI[22];
            this.LButton.size = new Vector2(150f, 40f);
            this.LButton.relativePosition = new Vector3(this.FButton.relativePosition.x, this.FButton.relativePosition.y + 20f);

            this.CButton = base.AddUIComponent<UIButton>();
            this.CButton.playAudioEvents = false;
            this.CButton.name = "CButton";
            this.CButton.tooltipBox = aView.defaultTooltipBox;
            this.CButton.text = Language.BuildingUI[21];
            this.CButton.size = new Vector2(150f, 40f);
            this.CButton.relativePosition = new Vector3(this.LButton.relativePosition.x, this.LButton.relativePosition.y + 20f);

            this.PButton = base.AddUIComponent<UIButton>();
            this.PButton.playAudioEvents = false;
            this.PButton.name = "PButton";
            this.PButton.tooltipBox = aView.defaultTooltipBox;
            this.PButton.text = Language.BuildingUI[23];
            this.PButton.size = new Vector2(150f, 40f);
            this.PButton.relativePosition = new Vector3(this.CButton.relativePosition.x, this.CButton.relativePosition.y + 20f);

        }

        public override void Update()
        {
            if (Loader.isGuiRunning)
            {
                if (refeshOnce)
                {
                    this.FButton.text = Language.BuildingUI[20] + ": " + MainDataStore.allFoodsFinal.ToString();
                    this.CButton.text = Language.BuildingUI[21] + ": " + MainDataStore.allCoalsFinal.ToString();
                    this.LButton.text = Language.BuildingUI[22] + ": " + MainDataStore.allLumbersFinal.ToString();
                    this.PButton.text = Language.BuildingUI[23] + ": " + MainDataStore.allPetrolsFinal.ToString();
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

                if (!MainDataStore.isCoalsGettedFinal)
                {
                    this.CButton.textColor = Color.red;
                }
                else
                {
                    this.CButton.textColor = Color.white;
                }

                if (!MainDataStore.isLumbersGettedFinal)
                {
                    this.LButton.textColor = Color.red;
                }
                else
                {
                    this.LButton.textColor = Color.white;
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
