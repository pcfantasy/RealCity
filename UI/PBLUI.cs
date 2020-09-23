using ColossalFramework.UI;
using UnityEngine;
using ColossalFramework;
using RealCity.Util;
using RealCity.CustomData;

namespace RealCity.UI
{
    public class PBLUI : UIPanel
    {
        public static readonly string cacheName = "PBLUI";
        public PublicTransportWorldInfoPanel baseBuildingWindow;
        public static bool refeshOnce = false;
        public static bool _initialized = false;
        public static int aTraffic = 0;
        public static int bTraffic = 0;
        private UILabel WeekDayRush;
        private UILabel WeekDayLow;
        private UILabel WeekDayNight;
        private UILabel WeekEndRush;
        private UILabel WeekEndLow;
        private UILabel WeekEndNight;
        private static UIDropDown WeekDayRushDD;
        private static UIDropDown WeekDayLowDD;
        private static UIDropDown WeekDayNightDD;
        private static UIDropDown WeekEndNightDD;
        private static UIDropDown WeekEndRushDD;
        private static UIDropDown WeekEndLowDD;

        public override void Update()
        {
            RefreshDisplayData();
            base.Update();
        }

        public override void Awake()
        {
            base.Awake();
            DoOnStartup();
        }

        public override void Start()
        {
            base.Start();
            canFocus = true;
            isInteractive = true;
            isVisible = true;
            opacity = 1f;
            cachedName = cacheName;
            RefreshDisplayData();
        }

        private void DoOnStartup()
        {
            ShowOnGui();
        }

        private void ShowOnGui()
        {
            //WeekDay
            WeekDayRush = AddUIComponent<UILabel>();
            WeekDayRush.text = Localization.Get("WeekDayRush");
            WeekDayRush.textScale = 0.8f;
            WeekDayRush.relativePosition = new Vector3(0, 0f);
            WeekDayRush.autoSize = true;

            WeekDayRushDD = CreateDropDown(this);
            WeekDayRushDD.items = new string[] { Localization.Get("25%"), Localization.Get("50%"), Localization.Get("75%"), Localization.Get("100%"), Localization.Get("125%"), Localization.Get("150%"), Localization.Get("175%"), Localization.Get("200%") };
            WeekDayRushDD.selectedIndex = TransportLineData.WeekDayRush[TransportLineData.lastLineID];
            WeekDayRushDD.size = new Vector2(70f, 20f);
            WeekDayRushDD.relativePosition = new Vector3(0f, 15f);
            WeekDayRushDD.eventSelectedIndexChanged += delegate (UIComponent c, int sel)
            {
                TransportLineData.WeekDayRush[TransportLineData.lastLineID] = (byte)sel;
            };

            WeekDayLow = AddUIComponent<UILabel>();
            WeekDayLow.text = Localization.Get("WeekDayLow");
            WeekDayLow.textScale = 0.8f;
            WeekDayLow.relativePosition = new Vector3(0, 35f);
            WeekDayLow.autoSize = true;

            WeekDayLowDD = CreateDropDown(this);
            WeekDayLowDD.items = new string[] { Localization.Get("25%"), Localization.Get("50%"), Localization.Get("75%"), Localization.Get("100%"), Localization.Get("125%"), Localization.Get("150%"), Localization.Get("175%"), Localization.Get("200%")};
            WeekDayLowDD.selectedIndex = TransportLineData.WeekDayLow[TransportLineData.lastLineID];
            WeekDayLowDD.eventSelectedIndexChanged += delegate (UIComponent c, int sel)
            {
                TransportLineData.WeekDayLow[TransportLineData.lastLineID] = (byte)sel;
            };
            WeekDayLowDD.size = new Vector2(70f, 20f);
            WeekDayLowDD.relativePosition = new Vector3(0f, 50f);

            WeekDayNight = AddUIComponent<UILabel>();
            WeekDayNight.text = Localization.Get("WeekDayNight");
            WeekDayNight.textScale = 0.8f;
            WeekDayNight.relativePosition = new Vector3(0, 70f);
            WeekDayNight.autoSize = true;

            WeekDayNightDD = CreateDropDown(this);
            WeekDayNightDD.items = new string[] { Localization.Get("25%"), Localization.Get("50%"), Localization.Get("75%"), Localization.Get("100%"), Localization.Get("125%"), Localization.Get("150%"), Localization.Get("175%"), Localization.Get("200%") };
            WeekDayNightDD.selectedIndex = TransportLineData.WeekDayNight[TransportLineData.lastLineID];
            WeekDayNightDD.eventSelectedIndexChanged += delegate (UIComponent c, int sel)
            {
                TransportLineData.WeekDayNight[TransportLineData.lastLineID] = (byte)sel;
            };
            WeekDayNightDD.size = new Vector2(70f, 20f);
            WeekDayNightDD.relativePosition = new Vector3(0f, 85f);

            //WeekEnd
            WeekEndRush = AddUIComponent<UILabel>();
            WeekEndRush.text = Localization.Get("WeekEndRush");
            WeekEndRush.textScale = 0.8f;
            WeekEndRush.relativePosition = new Vector3(0, 105f);
            WeekEndRush.autoSize = true;

            WeekEndRushDD = CreateDropDown(this);
            WeekEndRushDD.items = new string[] { Localization.Get("25%"), Localization.Get("50%"), Localization.Get("75%"), Localization.Get("100%"), Localization.Get("125%"), Localization.Get("150%"), Localization.Get("175%"), Localization.Get("200%") };
            WeekEndRushDD.selectedIndex = TransportLineData.WeekEndRush[TransportLineData.lastLineID];
            WeekEndRushDD.size = new Vector2(70f, 20f);
            WeekEndRushDD.relativePosition = new Vector3(0f, 120f);
            WeekEndRushDD.eventSelectedIndexChanged += delegate (UIComponent c, int sel)
            {
                TransportLineData.WeekEndRush[TransportLineData.lastLineID] = (byte)sel;
            };

            WeekEndLow = AddUIComponent<UILabel>();
            WeekEndLow.text = Localization.Get("WeekEndLow");

            WeekEndLow.relativePosition = new Vector3(0, 140f);
            WeekEndLow.autoSize = true;
            WeekEndLow.textScale = 0.8f;
            WeekEndLowDD = CreateDropDown(this);
            WeekEndLowDD.items = new string[] { Localization.Get("25%"), Localization.Get("50%"), Localization.Get("75%"), Localization.Get("100%"), Localization.Get("125%"), Localization.Get("150%"), Localization.Get("175%"), Localization.Get("200%") };
            WeekEndLowDD.selectedIndex = TransportLineData.WeekEndLow[TransportLineData.lastLineID];
            WeekEndLowDD.eventSelectedIndexChanged += delegate (UIComponent c, int sel)
            {
                TransportLineData.WeekEndLow[TransportLineData.lastLineID] = (byte)sel;
            };
            WeekEndLowDD.size = new Vector2(70f, 20f);
            WeekEndLowDD.relativePosition = new Vector3(0f, 155f);

            WeekEndNight = AddUIComponent<UILabel>();
            WeekEndNight.text = Localization.Get("WeekEndNight");
            WeekEndNight.textScale = 0.8f;
            WeekEndNight.relativePosition = new Vector3(0, 175f);
            WeekEndNight.autoSize = true;

            WeekEndNightDD = CreateDropDown(this);
            WeekEndNightDD.items = new string[] { Localization.Get("25%"), Localization.Get("50%"), Localization.Get("75%"), Localization.Get("100%"), Localization.Get("125%"), Localization.Get("150%"), Localization.Get("175%"), Localization.Get("200%") };
            WeekEndNightDD.selectedIndex = TransportLineData.WeekEndNight[TransportLineData.lastLineID];
            WeekEndNightDD.eventSelectedIndexChanged += delegate (UIComponent c, int sel)
            {
                TransportLineData.WeekEndNight[TransportLineData.lastLineID] = (byte)sel;
            };
            WeekEndNightDD.size = new Vector2(70f, 20f);
            WeekEndNightDD.relativePosition = new Vector3(0f, 190f);
        }

        public UITextureAtlas GetAtlas(string name)
        {
            UITextureAtlas[] array = Resources.FindObjectsOfTypeAll(typeof(UITextureAtlas)) as UITextureAtlas[];
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].name == name)
                {
                    return array[i];
                }
            }
            return null;
        }

        public UIDropDown CreateDropDown(UIComponent parent)
        {
            UIDropDown dropDown = parent.AddUIComponent<UIDropDown>();
            dropDown.atlas = GetAtlas("Ingame");
            dropDown.size = new Vector2(90f, 30f);
            dropDown.listBackground = "GenericPanelLight";
            dropDown.itemHeight = 30;
            dropDown.itemHover = "ListItemHover";
            dropDown.itemHighlight = "ListItemHighlight";
            dropDown.normalBgSprite = "ButtonMenu";
            dropDown.disabledBgSprite = "ButtonMenuDisabled";
            dropDown.hoveredBgSprite = "ButtonMenuHovered";
            dropDown.focusedBgSprite = "ButtonMenu";
            dropDown.listWidth = 90;
            dropDown.listHeight = 500;
            dropDown.foregroundSpriteMode = UIForegroundSpriteMode.Stretch;
            dropDown.popupColor = new Color32(45, 52, 61, byte.MaxValue);
            dropDown.popupTextColor = new Color32(170, 170, 170, byte.MaxValue);
            dropDown.zOrder = 1;
            dropDown.textScale = 0.8f;
            dropDown.verticalAlignment = UIVerticalAlignment.Middle;
            dropDown.horizontalAlignment = UIHorizontalAlignment.Left;
            dropDown.selectedIndex = 0;
            dropDown.textFieldPadding = new RectOffset(8, 0, 8, 0);
            dropDown.itemPadding = new RectOffset(14, 0, 8, 0);
            UIButton button = dropDown.AddUIComponent<UIButton>();
            dropDown.triggerButton = button;
            button.atlas = GetAtlas("Ingame");
            button.text = "";
            button.size = dropDown.size;
            button.relativePosition = new Vector3(0f, 0f);
            button.textVerticalAlignment = UIVerticalAlignment.Middle;
            button.textHorizontalAlignment = UIHorizontalAlignment.Left;
            button.normalFgSprite = "IconDownArrow";
            button.hoveredFgSprite = "IconDownArrowHovered";
            button.pressedFgSprite = "IconDownArrowPressed";
            button.focusedFgSprite = "IconDownArrowFocused";
            button.disabledFgSprite = "IconDownArrowDisabled";
            button.foregroundSpriteMode = UIForegroundSpriteMode.Fill;
            button.horizontalAlignment = UIHorizontalAlignment.Right;
            button.verticalAlignment = UIVerticalAlignment.Middle;
            button.zOrder = 0;
            button.textScale = 0.8f;
            dropDown.eventSizeChanged += delegate (UIComponent c, Vector2 t)
            {
                button.size = t;
                dropDown.listWidth = (int)t.x;
            };
            return dropDown;
        }

        private void RefreshDisplayData()
        {
            if (refeshOnce || (TransportLineData.lastLineID != GetLineID()))
            {
                if (isVisible)
                {
                    TransportLineData.lastLineID = GetLineID();
                    if (WeekDayRush.text != Localization.Get("WeekDayRush"))
                    {
                        WeekDayRushDD.items = new string[] { Localization.Get("25%"), Localization.Get("50%"), Localization.Get("75%"), Localization.Get("100%"), Localization.Get("125%"), Localization.Get("150%"), Localization.Get("175%"), Localization.Get("200%") };
                        WeekDayLowDD.items = new string[] { Localization.Get("25%"), Localization.Get("50%"), Localization.Get("75%"), Localization.Get("100%"), Localization.Get("125%"), Localization.Get("150%"), Localization.Get("175%"), Localization.Get("200%") };
                        WeekDayNightDD.items = new string[] { Localization.Get("25%"), Localization.Get("50%"), Localization.Get("75%"), Localization.Get("100%"), Localization.Get("125%"), Localization.Get("150%"), Localization.Get("175%"), Localization.Get("200%") };
                        WeekEndRushDD.items = new string[] { Localization.Get("25%"), Localization.Get("50%"), Localization.Get("75%"), Localization.Get("100%"), Localization.Get("125%"), Localization.Get("150%"), Localization.Get("175%"), Localization.Get("200%") };
                        WeekEndLowDD.items = new string[] { Localization.Get("25%"), Localization.Get("50%"), Localization.Get("75%"), Localization.Get("100%"), Localization.Get("125%"), Localization.Get("150%"), Localization.Get("175%"), Localization.Get("200%") };
                        WeekEndNightDD.items = new string[] { Localization.Get("25%"), Localization.Get("50%"), Localization.Get("75%"), Localization.Get("100%"), Localization.Get("125%"), Localization.Get("150%"), Localization.Get("175%"), Localization.Get("200%") };
                    }
                    if (WeekDayRushDD.selectedIndex != TransportLineData.WeekDayRush[TransportLineData.lastLineID])
                        WeekDayRushDD.selectedIndex = TransportLineData.WeekDayRush[TransportLineData.lastLineID];
                    if (WeekDayLowDD.selectedIndex != TransportLineData.WeekDayLow[TransportLineData.lastLineID])
                        WeekDayLowDD.selectedIndex = TransportLineData.WeekDayLow[TransportLineData.lastLineID];
                    if (WeekDayNightDD.selectedIndex != TransportLineData.WeekDayNight[TransportLineData.lastLineID])
                        WeekDayNightDD.selectedIndex = TransportLineData.WeekDayNight[TransportLineData.lastLineID];
                    if (WeekEndRushDD.selectedIndex != TransportLineData.WeekEndRush[TransportLineData.lastLineID])
                        WeekEndRushDD.selectedIndex = TransportLineData.WeekEndRush[TransportLineData.lastLineID];
                    if (WeekEndLowDD.selectedIndex != TransportLineData.WeekEndLow[TransportLineData.lastLineID])
                        WeekEndLowDD.selectedIndex = TransportLineData.WeekEndLow[TransportLineData.lastLineID];
                    if (WeekEndNightDD.selectedIndex != TransportLineData.WeekEndNight[TransportLineData.lastLineID])
                        WeekEndNightDD.selectedIndex = TransportLineData.WeekEndNight[TransportLineData.lastLineID];
                    WeekDayRush.text = Localization.Get("WeekDayRush");
                    WeekDayLow.text = Localization.Get("WeekDayLow");
                    WeekDayNight.text = Localization.Get("WeekDayNight");
                    WeekEndRush.text = Localization.Get("WeekEndRush");
                    WeekEndLow.text = Localization.Get("WeekEndLow");
                    WeekEndNight.text = Localization.Get("WeekEndNight");
                    refeshOnce = false;
                }
            }
        }

        private ushort GetLineID()
        {
            if (WorldInfoPanel.GetCurrentInstanceID().Type == InstanceType.TransportLine)
            {
                return WorldInfoPanel.GetCurrentInstanceID().TransportLine;
            }
            if (WorldInfoPanel.GetCurrentInstanceID().Type == InstanceType.Vehicle)
            {
                ushort firstVehicle = Singleton<VehicleManager>.instance.m_vehicles.m_buffer[WorldInfoPanel.GetCurrentInstanceID().Vehicle].GetFirstVehicle(WorldInfoPanel.GetCurrentInstanceID().Vehicle);
                if (firstVehicle != 0)
                {
                    return Singleton<VehicleManager>.instance.m_vehicles.m_buffer[firstVehicle].m_transportLine;
                }
            }
            return 0;
        }
    }
}
