﻿using System.Collections.Generic;
using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.PlatformServices;
using ColossalFramework.Plugins;
using ColossalFramework.UI;
using UnityEngine;
using RealCity.Util;

namespace RealCity.UI
{
	public class IncompatibleModsPanel : UIPanel
	{
		public UILabel title;
		public UIButton closeButton;
		public UISprite warningIcon;
		private static IncompatibleModsPanel _instance;

		public static IncompatibleModsPanel Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = (UIView.GetAView().AddUIComponent(typeof(IncompatibleModsPanel)) as IncompatibleModsPanel);
				}

				return _instance;
			}
		}

		public Dictionary<ulong, string> IncompatibleMods { get; set; }

		public void Initialize()
		{
			DebugLog.LogToFileOnly("IncompatibleModsPanel initialize");
			isVisible = true;

			backgroundSprite = "MenuPanel3";
			color = new Color32(75, 75, 135, 255);
			width = 600;
			height = 440;

			Vector2 resolution = UIView.GetAView().GetScreenResolution();
			relativePosition = new Vector3(resolution.x / 2 - 300, resolution.y / 3);

			warningIcon = AddUIComponent<UISprite>();
			warningIcon.size = new Vector2(40f, 40f);
			warningIcon.spriteName = "IconWarning";
			warningIcon.relativePosition = new Vector3(15, 15);
			warningIcon.zOrder = 0;

			title = AddUIComponent<UILabel>();
			title.autoSize = true;
			title.padding = new RectOffset(10, 10, 15, 15);
			title.relativePosition = new Vector2(60, 12);
			title.text = Localization.Get("INCOMPATIBILITY_CHECK_TIP");

			UIPanel panel = AddUIComponent<UIPanel>();
			panel.relativePosition = new Vector2(20, 70);
			panel.size = new Vector2(565, 320);

			UIScrollablePanel scrollablePanel = panel.AddUIComponent<UIScrollablePanel>();
			scrollablePanel.backgroundSprite = "";
			scrollablePanel.size = new Vector2(550, 340);
			scrollablePanel.relativePosition = new Vector3(0, 0);
			scrollablePanel.clipChildren = true;
			if (IncompatibleMods.Count != 0)
			{
				int acc = 0;
				UIPanel item;
				IncompatibleMods.ForEach((pair) => {
					item = CreateEntry(ref scrollablePanel, pair.Value, pair.Key);
					item.relativePosition = new Vector2(0, acc);
					item.size = new Vector2(560, 50);
					acc += 50;
				});
				item = null;
			}
			scrollablePanel.FitTo(panel);
			scrollablePanel.scrollWheelDirection = UIOrientation.Vertical;
			scrollablePanel.builtinKeyNavigation = true;

			UIScrollbar verticalScroll = panel.AddUIComponent<UIScrollbar>();
			verticalScroll.stepSize = 1;
			verticalScroll.relativePosition = new Vector2(panel.width - 15, 0);
			verticalScroll.orientation = UIOrientation.Vertical;
			verticalScroll.size = new Vector2(20, 320);
			verticalScroll.incrementAmount = 25;
			verticalScroll.scrollEasingType = EasingType.BackEaseOut;

			scrollablePanel.verticalScrollbar = verticalScroll;

			UISlicedSprite track = verticalScroll.AddUIComponent<UISlicedSprite>();
			track.spriteName = "ScrollbarTrack";
			track.relativePosition = Vector3.zero;
			track.size = new Vector2(16, 320);

			verticalScroll.trackObject = track;

			UISlicedSprite thumb = track.AddUIComponent<UISlicedSprite>();
			thumb.spriteName = "ScrollbarThumb";
			thumb.autoSize = true;
			thumb.relativePosition = Vector3.zero;
			verticalScroll.thumbObject = thumb;

			closeButton = AddUIComponent<UIButton>();
			closeButton.eventClick += CloseButtonClick;
			closeButton.relativePosition = new Vector3(width - closeButton.width - 45, 15f);
			closeButton.normalBgSprite = "buttonclose";
			closeButton.hoveredBgSprite = "buttonclosehover";
			closeButton.pressedBgSprite = "buttonclosepressed";

			BringToFront();
		}

		private void CloseButtonClick(UIComponent component, UIMouseEventParameter eventparam)
		{
			closeButton.eventClick -= CloseButtonClick;
			TryPopModal();
			Hide();
		}

		private UIPanel CreateEntry(ref UIScrollablePanel parent, string name, ulong steamId)
		{
			UIPanel panel = parent.AddUIComponent<UIPanel>();
			panel.size = new Vector2(560, 50);
			panel.backgroundSprite = "ContentManagerItemBackground";
			UILabel label = panel.AddUIComponent<UILabel>();
			label.text = name;
			label.textAlignment = UIHorizontalAlignment.Left;
			label.relativePosition = new Vector2(10, 15);
			CreateButton(panel, "Unsubscribe", (int)panel.width - 170, 10, delegate (UIComponent component, UIMouseEventParameter param) { UnsubscribeClick(component, param, steamId); });
			return panel;
		}

		private void UnsubscribeClick(UIComponent component, UIMouseEventParameter eventparam, ulong steamId)
		{
			DebugLog.LogToFileOnly("Trying to unsubscribe workshop item " + steamId);
			component.isEnabled = false;
			if (PlatformService.workshop.Unsubscribe(new PublishedFileId(steamId)))
			{
				IncompatibleMods.Remove(steamId);
				component.parent.Disable();
				component.isVisible = false;
				DebugLog.LogToFileOnly("Workshop item " + steamId + " unsubscribed");
			}
			else
			{
				DebugLog.LogToFileOnly("Failed unsubscribing workshop item " + steamId);
				component.isEnabled = true;
			}
		}

		private UIButton CreateButton(UIComponent parent, string text, int x, int y, MouseEventHandler eventClick)
		{
			var button = parent.AddUIComponent<UIButton>();
			button.textScale = 0.8f;
			button.width = 150f;
			button.height = 30;
			button.normalBgSprite = "ButtonMenu";
			button.disabledBgSprite = "ButtonMenuDisabled";
			button.hoveredBgSprite = "ButtonMenuHovered";
			button.focusedBgSprite = "ButtonMenu";
			button.pressedBgSprite = "ButtonMenuPressed";
			button.textColor = new Color32(255, 255, 255, 255);
			button.playAudioEvents = true;
			button.text = text;
			button.relativePosition = new Vector3(x, y);
			button.eventClick += eventClick;

			return button;
		}

		private new void OnEnable()
		{
			DebugLog.LogToFileOnly("IncompatibleModsPanel enabled");
			PlatformService.workshop.eventUGCQueryCompleted += OnQueryCompleted;
			Singleton<PluginManager>.instance.eventPluginsChanged += OnPluginsChanged;
			Singleton<PluginManager>.instance.eventPluginsStateChanged += OnPluginsChanged;
			LocaleManager.eventLocaleChanged += OnLocaleChanged;
		}

		private void OnQueryCompleted(UGCDetails result, bool ioerror)
		{
			DebugLog.LogToFileOnly("IncompatibleModsPanel.OnQueryCompleted() - " + result.result.ToString("D") + " IO error?:" + ioerror);
		}

		private void OnPluginsChanged()
		{
			DebugLog.LogToFileOnly("IncompatibleModsPanel.OnPluginsChanged() - Plugins changed");
		}

		private new void OnDisable()
		{
			DebugLog.LogToFileOnly("IncompatibleModsPanel disabled");
			PlatformService.workshop.eventUGCQueryCompleted -= OnQueryCompleted;
			Singleton<PluginManager>.instance.eventPluginsChanged -= OnPluginsChanged;
			Singleton<PluginManager>.instance.eventPluginsStateChanged -= OnPluginsChanged;
			LocaleManager.eventLocaleChanged -= OnLocaleChanged;
		}

		protected override void OnKeyDown(UIKeyEventParameter p)
		{
			if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Return))
			{
				TryPopModal();
				p.Use();
				Hide();
			}

			base.OnKeyDown(p);
		}

		private void TryPopModal()
		{
			if (UIView.HasModalInput())
			{
				UIView.PopModal();
				UIComponent component = UIView.GetModalComponent();
				if (component != null)
				{
					UIView.SetFocus(component);
				}
			}
		}
	}
}
