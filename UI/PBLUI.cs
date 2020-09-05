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
		private UILabel WeekDayPlan;
		private UILabel WeekEndPlan;
		private static UIDropDown WeekDayPlanDD;
		private static UIDropDown WeekEndPlanDD;

		public override void Update() {
			RefreshDisplayData();
			base.Update();
		}

		public override void Awake() {
			base.Awake();
			DoOnStartup();
		}

		public override void Start() {
			base.Start();
			canFocus = true;
			isInteractive = true;
			isVisible = true;
			opacity = 1f;
			cachedName = cacheName;
			RefreshDisplayData();
		}

		private void DoOnStartup() {
			ShowOnGui();
		}

		private void ShowOnGui() {
			WeekDayPlan = AddUIComponent<UILabel>();
			WeekDayPlan.text = Localization.Get("WeekDayPlan");
			WeekDayPlan.relativePosition = new Vector3(0, 0f);
			WeekDayPlan.autoSize = true;

			WeekDayPlanDD = CreateDropDown(this);
			WeekDayPlanDD.items = new string[] { Localization.Get("NoPlan"), Localization.Get("WeekDayPlan"), Localization.Get("WeekEndPlan"), Localization.Get("MaxPlan"), Localization.Get("MinPlan") };
			WeekDayPlanDD.selectedIndex = TransportLineData.WeekDayPlan[TransportLineData.lastLineID];
			WeekDayPlanDD.size = new Vector2(130f, 25f);
			WeekDayPlanDD.relativePosition = new Vector3(0f, 20f);
			WeekDayPlanDD.eventSelectedIndexChanged += delegate (UIComponent c, int sel) {
				TransportLineData.WeekDayPlan[TransportLineData.lastLineID] = (byte)sel;
			};

			WeekEndPlan = AddUIComponent<UILabel>();
			WeekEndPlan.text = Localization.Get("WeekEndPlan");
			WeekEndPlan.relativePosition = new Vector3(0, 50f);
			WeekEndPlan.autoSize = true;

			WeekEndPlanDD = CreateDropDown(this);
			WeekEndPlanDD.items = new string[] { Localization.Get("NoPlan"), Localization.Get("WeekDayPlan"), Localization.Get("WeekEndPlan"), Localization.Get("MaxPlan"), Localization.Get("MinPlan") };
			WeekEndPlanDD.selectedIndex = TransportLineData.WeekEndPlan[TransportLineData.lastLineID];
			WeekEndPlanDD.eventSelectedIndexChanged += delegate (UIComponent c, int sel) {
				TransportLineData.WeekEndPlan[TransportLineData.lastLineID] = (byte)sel;
			};
			WeekEndPlanDD.size = new Vector2(130f, 25f);
			WeekEndPlanDD.relativePosition = new Vector3(0f, 70f);
		}

		public UITextureAtlas GetAtlas(string name) {
			UITextureAtlas[] array = Resources.FindObjectsOfTypeAll(typeof(UITextureAtlas)) as UITextureAtlas[];
			for (int i = 0; i < array.Length; i++) {
				if (array[i].name == name) {
					return array[i];
				}
			}
			return null;
		}

		public UIDropDown CreateDropDown(UIComponent parent) {
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
			dropDown.eventSizeChanged += delegate (UIComponent c, Vector2 t) {
				button.size = t;
				dropDown.listWidth = (int)t.x;
			};
			return dropDown;
		}

		private void RefreshDisplayData() {
			if (refeshOnce || (TransportLineData.lastLineID != GetLineID())) {
				if (isVisible) {
					TransportLineData.lastLineID = GetLineID();
					if (WeekDayPlan.text != Localization.Get("WeekDayPlan")) {
						WeekDayPlanDD.items = new string[] { Localization.Get("NoPlan"), Localization.Get("WeekDayPlan"), Localization.Get("WeekEndPlan"), Localization.Get("MaxPlan"), Localization.Get("MinPlan") };
						WeekEndPlanDD.items = new string[] { Localization.Get("NoPlan"), Localization.Get("WeekDayPlan"), Localization.Get("WeekEndPlan"), Localization.Get("MaxPlan"), Localization.Get("MinPlan") };
					}
					if (WeekDayPlanDD.selectedIndex != TransportLineData.WeekDayPlan[TransportLineData.lastLineID])
						WeekDayPlanDD.selectedIndex = TransportLineData.WeekDayPlan[TransportLineData.lastLineID];
					if (WeekEndPlanDD.selectedIndex != TransportLineData.WeekEndPlan[TransportLineData.lastLineID])
						WeekEndPlanDD.selectedIndex = TransportLineData.WeekEndPlan[TransportLineData.lastLineID];
					WeekDayPlan.text = Localization.Get("WeekDayPlan");
					WeekEndPlan.text = Localization.Get("WeekEndPlan");
					refeshOnce = false;
				}
			}
		}

		private ushort GetLineID() {
			if (WorldInfoPanel.GetCurrentInstanceID().Type == InstanceType.TransportLine) {
				return WorldInfoPanel.GetCurrentInstanceID().TransportLine;
			}
			if (WorldInfoPanel.GetCurrentInstanceID().Type == InstanceType.Vehicle) {
				ushort firstVehicle = Singleton<VehicleManager>.instance.m_vehicles.m_buffer[WorldInfoPanel.GetCurrentInstanceID().Vehicle].GetFirstVehicle(WorldInfoPanel.GetCurrentInstanceID().Vehicle);
				if (firstVehicle != 0) {
					return Singleton<VehicleManager>.instance.m_vehicles.m_buffer[firstVehicle].m_transportLine;
				}
			}
			return 0;
		}
	}
}
