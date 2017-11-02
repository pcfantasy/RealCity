using System.Collections.Generic;
using ColossalFramework.UI;
using UnityEngine;
using ColossalFramework;

namespace RealCity
{
    public class BuildingUI : UIPanel
    {
        public static readonly string cacheName = "BuildingUI";

        private static readonly float SPACING = 15f;

        private static readonly float SPACING22 = 22f;

        private Dictionary<string, UILabel> _valuesControlContainer = new Dictionary<string, UILabel>(16);

        public ZonedBuildingWorldInfoPanel baseBuildingWindow;

        private UILabel m_HeaderDataText;

        //1、citizen tax income
        private UILabel buildingmoney;
        private UILabel buildingincomebuffer;
        private UILabel buildingoutgoingbuffer;

        public override void Update()
        {
            this.RefreshDisplayData();
            base.Update();
        }

        public override void Start()
        {
            base.Start();
            //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "Go to UI now");
            base.backgroundSprite = "MenuPanel";
            this.canFocus = true;
            this.isInteractive = true;
            this.BringToFront();
            base.opacity = 1f;
            base.cachedName = cacheName;
            this.DoOnStartup();
        }

        private void DoOnStartup()
        {
            this.ShowOnGui();
            this.RefreshDisplayData();
        }


        private void ShowOnGui()
        {
            this.m_HeaderDataText = base.AddUIComponent<UILabel>();
            this.m_HeaderDataText.textScale = 0.825f;
            this.m_HeaderDataText.text = string.Concat(new string[]
            {
                "Object Type    [data]"
            });
            this.m_HeaderDataText.tooltip = "N/A";
            this.m_HeaderDataText.relativePosition = new Vector3(SPACING, 50f);
            this.m_HeaderDataText.autoSize = true;

            this.buildingmoney = base.AddUIComponent<UILabel>();
            this.buildingmoney.text = "Building Money [000000000000000]";
            this.buildingmoney.tooltip = "Only show industry and commerical building money";
            this.buildingmoney.relativePosition = new Vector3(SPACING, this.m_HeaderDataText.relativePosition.y + SPACING22);
            this.buildingmoney.autoSize = true;
            this.buildingmoney.name = "Moreeconomic_Text_0";

            this.buildingincomebuffer = base.AddUIComponent<UILabel>();
            this.buildingincomebuffer.text = "buildingincomebuffer [000000000000000]";
            this.buildingincomebuffer.tooltip = "Only show industry and commerical buildingincomebuffer";
            this.buildingincomebuffer.relativePosition = new Vector3(SPACING, this.buildingmoney.relativePosition.y + SPACING22);
            this.buildingincomebuffer.autoSize = true;
            this.buildingincomebuffer.name = "Moreeconomic_Text_1";

            this.buildingoutgoingbuffer = base.AddUIComponent<UILabel>();
            this.buildingoutgoingbuffer.text = "buildingoutgoingbuffer [000000000000000]";
            this.buildingoutgoingbuffer.tooltip = "Only show industry and commerical buildingincomebuffer";
            this.buildingoutgoingbuffer.relativePosition = new Vector3(SPACING, this.buildingincomebuffer.relativePosition.y + SPACING22);
            this.buildingoutgoingbuffer.autoSize = true;
            this.buildingoutgoingbuffer.name = "Moreeconomic_Text_2";
        }

        private void RefreshDisplayData()
        {
            Building buildingdata = Singleton<BuildingManager>.instance.m_buildings.m_buffer[comm_data.current_buildingid];
            this.buildingmoney.text = string.Format("Building Money [{0}]", comm_data.building_money[comm_data.current_buildingid]);
            this.buildingincomebuffer.text = string.Format("buildingincomebuffer [{0}]", buildingdata.m_customBuffer1);
            this.buildingoutgoingbuffer.text = string.Format("buildingoutgoingbuffer [{0}]", buildingdata.m_customBuffer2);
        }

    }
}
