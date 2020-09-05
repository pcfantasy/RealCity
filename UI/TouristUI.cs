using ColossalFramework.UI;
using UnityEngine;
using ColossalFramework;
using RealCity.Util;
using RealCity.CustomData;

namespace RealCity.UI
{
	public class TouristUI : UIPanel
	{
		public static readonly string cacheName = "TouristUI";
		private static readonly float SPACING = 15f;
		public TouristWorldInfoPanel baseBuildingWindow;
		public static bool refeshOnce = false;
		private UILabel TouristMoney;

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
			TouristMoney = AddUIComponent<UILabel>();
			TouristMoney.text = Localization.Get("TOURIST_MONEY");
			TouristMoney.relativePosition = new Vector3(SPACING, 50f);
			TouristMoney.autoSize = true;
		}

		private void RefreshDisplayData() {
			if (refeshOnce || (CitizenData.lastCitizenID != WorldInfoPanel.GetCurrentInstanceID().Citizen)) {
				if (isVisible) {
					CitizenData.lastCitizenID = WorldInfoPanel.GetCurrentInstanceID().Citizen;
					TouristMoney.text = string.Format(Localization.Get("TOURIST_MONEY") + " [{0}]", CitizenData.citizenMoney[CitizenData.lastCitizenID]);
					refeshOnce = false;
				}
			}
		}
	}
}
