using ColossalFramework.UI;
using ICities;
using UnityEngine;
using System.IO;
using ColossalFramework;
using System.Reflection;
using System.Collections.Generic;
using RealCity.UI;
using RealCity.Util;
using RealCity.CustomManager;
using ColossalFramework.Plugins;
using RealCity.CustomData;
using CitiesHarmony.API;

namespace RealCity
{
	public class Loader : LoadingExtensionBase
	{
		public static bool HarmonyDetourInited = false;
		public static bool HarmonyDetourFailed = true;
		public static UIView parentGuiView;
		public static UIPanel HumanInfo;
		public static UIPanel TouristInfo;
		public static EcnomicUI ecnomicUI;
		public static RealCityUI realCityUI;
		public static HumanUI humanUI;
		public static PoliticsUI politicsUI;
		public static TouristUI touristUI;
		public static GameObject buildingWindowGameObject;
		public static GameObject HumanWindowGameObject;
		public static GameObject TouristWindowGameObject;
		public static LoadMode CurrentLoadMode;
		public static bool isGuiRunning = false;
		public static PoliticsButton PlButton;
		public static EcnomicButton EcButton;
		public static RealCityButton RcButton;
		public static BuildingButton BButton;
		public static PlayerBuildingButton PBButton;
		public static string m_atlasName = "RealCity";
		public static bool m_atlasLoaded;
		public static UIPanel PBLInfo;
		public static PBLUI PBLUI;
		public static GameObject PBLWindowGameObject;
		public static bool isTransportLinesManagerRunning = false;
		public static bool isRealTimeRunning = false;

		public override void OnCreated(ILoading loading) {
			HarmonyInitDetour();
			base.OnCreated(loading);
		}

		public override void OnLevelLoaded(LoadMode mode) {
			base.OnLevelLoaded(mode);
			CurrentLoadMode = mode;
			if (RealCity.IsEnabled) {
				if (mode == LoadMode.LoadGame || mode == LoadMode.NewGame) {
					isTransportLinesManagerRunning = CheckTransportLinesManagerIsLoaded();
					DebugLog.LogToFileOnly($"Check TLM running = {isTransportLinesManagerRunning}");
					isRealTimeRunning = CheckRealTimeIsLoaded();
					DebugLog.LogToFileOnly($"Check RealTime running = {isRealTimeRunning}");
					isTransportLinesManagerRunning = isTransportLinesManagerRunning || (!isRealTimeRunning);
					//refresh OptionsMainPanel
					MethodInfo method = typeof(OptionsMainPanel).GetMethod("OnLocaleChanged", BindingFlags.Instance | BindingFlags.NonPublic);
					method.Invoke(UIView.library.Get<OptionsMainPanel>("OptionsPanel"), new object[0]);
					SetupGui();
					HarmonyInitDetour();
					OptionUI.LoadSetting();
					RealCityThreading.isFirstTime = true;
					DebugLog.LogToFileOnly("OnLevelLoaded");
					if (mode == LoadMode.NewGame) {
						InitData();
					}
				} else {
					if (RealCity.IsEnabled) {
						HarmonyRevertDetour();
					}
				}
			}
		}

		public static void InitData() {
			DebugLog.LogToFileOnly("InitData");
			TransportLineData.DataInit();
			VehicleData.DataInit();
			BuildingData.DataInit();
			CitizenUnitData.DataInit();
			CitizenData.DataInit();
			RealCityEconomyManager.DataInit();
			System.Random rand = new System.Random();
			RealCityEconomyExtension.partyTrend = (byte)rand.Next(5);
			RealCityEconomyExtension.partyTrendStrength = (byte)rand.Next(300);

			DebugLog.LogToFileOnly("InitData Done");
		}

		public override void OnLevelUnloading() {
			base.OnLevelUnloading();
			if (CurrentLoadMode == LoadMode.LoadGame || CurrentLoadMode == LoadMode.NewGame) {
				if (RealCity.IsEnabled && isGuiRunning) {
					RemoveGui();
				}
				if (RealCity.IsEnabled) {
					RealCityThreading.isFirstTime = true;
					HarmonyRevertDetour();
				}
			}
		}

		public override void OnReleased() {
			base.OnReleased();
		}

		private static void LoadSprites() {
			if (SpriteUtilities.GetAtlas(m_atlasName) != null) return;
			var modPath = PluginManager.instance.FindPluginInfo(Assembly.GetExecutingAssembly()).modPath;
			m_atlasLoaded = SpriteUtilities.InitialiseAtlas(Path.Combine(modPath, "Icon/RealCity.png"), m_atlasName);
			if (m_atlasLoaded) {
				var spriteSuccess = true;
				spriteSuccess = SpriteUtilities.AddSpriteToAtlas(new Rect(new Vector2(382, 0), new Vector2(191, 191)), "EcButton", m_atlasName)
							 && SpriteUtilities.AddSpriteToAtlas(new Rect(new Vector2(0, 0), new Vector2(191, 191)), "Blank", m_atlasName)
							 && SpriteUtilities.AddSpriteToAtlas(new Rect(new Vector2(191, 0), new Vector2(191, 191)), "BuildingButton", m_atlasName)
							 && SpriteUtilities.AddSpriteToAtlas(new Rect(new Vector2(573, 0), new Vector2(191, 191)), "Politics", m_atlasName)
							 && SpriteUtilities.AddSpriteToAtlas(new Rect(new Vector2(764, 0), new Vector2(191, 191)), "RcButton", m_atlasName)
							 && spriteSuccess;
				if (!spriteSuccess) DebugLog.LogToFileOnly("Some sprites haven't been loaded. This is abnormal; you should probably report this to the mod creator.");
			} else DebugLog.LogToFileOnly("The texture atlas (provides custom icons) has not loaded. All icons have reverted to text prompts.");
		}

		public static void SetupGui() {
			LoadSprites();
			if (m_atlasLoaded) {
				parentGuiView = null;
				parentGuiView = UIView.GetAView();
				if (ecnomicUI == null) {
					ecnomicUI = (EcnomicUI)parentGuiView.AddUIComponent(typeof(EcnomicUI));
				}

				if (realCityUI == null) {
					realCityUI = (RealCityUI)parentGuiView.AddUIComponent(typeof(RealCityUI));
				}

				if (politicsUI == null) {
					politicsUI = (PoliticsUI)parentGuiView.AddUIComponent(typeof(PoliticsUI));
				}

				SetupHumanGui();
				SetupTouristGui();
				SetupEcnomicButton();
				SetupPLButton();
				SetupCityButton();
				SetupBuildingButton();
				SetupPlayerBuildingButton();
				if (!isTransportLinesManagerRunning)
					SetupPBLUIGui();
				isGuiRunning = true;
			}
		}
		public static void SetupPBLUIGui() {
			PBLWindowGameObject = new GameObject("PBLWindowGameObject");
			PBLUI = (PBLUI)PBLWindowGameObject.AddComponent(typeof(PBLUI));
			PBLInfo = UIView.Find<UIPanel>("(Library) PublicTransportWorldInfoPanel");
			if (PBLInfo == null) {
				DebugLog.LogToFileOnly("UIPanel not found (update broke the mod!): (Library) PublicTransportWorldInfoPanel\nAvailable panels are:\n");
			} else {
				PBLUI.transform.parent = PBLInfo.transform;
				PBLUI.size = new Vector3(150, 100);
				PBLUI.baseBuildingWindow = PBLInfo.gameObject.transform.GetComponentInChildren<PublicTransportWorldInfoPanel>();
				UILabel UILabel = PBLUI.baseBuildingWindow.Find<UILabel>("ModelLabel");
				PBLUI.position = new Vector3(UILabel.relativePosition.x + 50f, PBLInfo.size.y - (UILabel.relativePosition.y + 160f));
				PBLInfo.eventVisibilityChanged += PBLInfo_eventVisibilityChanged;
			}
		}
		public static void PBLInfo_eventVisibilityChanged(UIComponent component, bool value) {
			PBLUI.isEnabled = value;
			if (value) {
				//initialize PBL ui again
				PBLUI.transform.parent = PBLInfo.transform;
				PBLUI.size = new Vector3(150, 100);
				PBLUI.baseBuildingWindow = PBLInfo.gameObject.transform.GetComponentInChildren<PublicTransportWorldInfoPanel>();
				UILabel UILabel = PBLUI.baseBuildingWindow.Find<UILabel>("ModelLabel");
				//DebugLog.LogToFileOnly(UILabel.relativePosition.x.ToString() + "    " +  UILabel.relativePosition.y.ToString());
				PBLUI.position = new Vector3(UILabel.relativePosition.x + 50f, PBLInfo.size.y - (UILabel.relativePosition.y + 160f));
				PBLUI.refeshOnce = true;
				PBLUI.Show();
			} else {
				PBLUI.Hide();
			}
		}
		public static void SetupHumanGui() {
			HumanWindowGameObject = new GameObject("HumanWindowGameObject");
			humanUI = (HumanUI)HumanWindowGameObject.AddComponent(typeof(HumanUI));
			HumanInfo = UIView.Find<UIPanel>("(Library) CitizenWorldInfoPanel");
			if (HumanInfo == null) {
				DebugLog.LogToFileOnly("UIPanel not found (update broke the mod!): (Library) CitizenWorldInfoPanel\nAvailable panels are:\n");
			}
			humanUI.transform.parent = HumanInfo.transform;
			humanUI.size = new Vector3(HumanInfo.size.x, HumanInfo.size.y);
			humanUI.baseBuildingWindow = HumanInfo.gameObject.transform.GetComponentInChildren<CitizenWorldInfoPanel>();
			humanUI.position = new Vector3(HumanInfo.size.x, HumanInfo.size.y);
			HumanInfo.eventVisibilityChanged += HumanInfo_eventVisibilityChanged;
		}

		public static void SetupTouristGui() {
			TouristWindowGameObject = new GameObject("TouristWindowGameObject");
			touristUI = (TouristUI)TouristWindowGameObject.AddComponent(typeof(TouristUI));
			TouristInfo = UIView.Find<UIPanel>("(Library) TouristWorldInfoPanel");
			if (TouristInfo == null) {
				DebugLog.LogToFileOnly("UIPanel not found (update broke the mod!): (Library) TouristWorldInfoPanel\nAvailable panels are:\n");
			}
			touristUI.transform.parent = TouristInfo.transform;
			touristUI.size = new Vector3(TouristInfo.size.x, TouristInfo.size.y);
			touristUI.baseBuildingWindow = TouristInfo.gameObject.transform.GetComponentInChildren<TouristWorldInfoPanel>();
			touristUI.position = new Vector3(TouristInfo.size.x, TouristInfo.size.y);
			TouristInfo.eventVisibilityChanged += TouristInfo_eventVisibilityChanged;
		}

		public static void SetupEcnomicButton() {
			if (EcButton == null) {
				EcButton = (parentGuiView.AddUIComponent(typeof(EcnomicButton)) as EcnomicButton);
			}
			EcButton.Show();
		}

		public static void SetupCityButton() {
			if (RcButton == null) {
				RcButton = (parentGuiView.AddUIComponent(typeof(RealCityButton)) as RealCityButton);
			}
			RcButton.Show();
		}

		public static void SetupBuildingButton() {
			var buildingInfo = UIView.Find<UIPanel>("(Library) ZonedBuildingWorldInfoPanel");
			if (BButton == null) {
				BButton = (buildingInfo.AddUIComponent(typeof(BuildingButton)) as BuildingButton);
			}
			BButton.width = 40f;
			BButton.height = 35f;
			BButton.relativePosition = new Vector3(120, buildingInfo.size.y - BButton.height);
			BButton.Show();
		}

		public static void SetupPlayerBuildingButton() {
			var playerBuildingInfo = UIView.Find<UIPanel>("(Library) CityServiceWorldInfoPanel");
			if (PBButton == null) {
				PBButton = (playerBuildingInfo.AddUIComponent(typeof(PlayerBuildingButton)) as PlayerBuildingButton);
			}
			PBButton.width = 40f;
			PBButton.height = 35f;
			PBButton.relativePosition = new Vector3(120, playerBuildingInfo.size.y - PBButton.height);
			PBButton.Show();
		}

		public static void SetupPLButton() {
			if (PlButton == null) {
				PlButton = (parentGuiView.AddUIComponent(typeof(PoliticsButton)) as PoliticsButton);
			}
			PlButton.Show();
		}

		public static void HumanInfo_eventVisibilityChanged(UIComponent component, bool value) {
			humanUI.isEnabled = value;
			if (value) {
				//initialize human ui again
				humanUI.transform.parent = HumanInfo.transform;
				humanUI.size = new Vector3(HumanInfo.size.x, HumanInfo.size.y);
				humanUI.baseBuildingWindow = HumanInfo.gameObject.transform.GetComponentInChildren<CitizenWorldInfoPanel>();
				humanUI.position = new Vector3(HumanInfo.size.x, HumanInfo.size.y);
				HumanUI.refeshOnce = true;
				humanUI.Show();
			} else {
				humanUI.Hide();
			}
		}

		public static void TouristInfo_eventVisibilityChanged(UIComponent component, bool value) {
			touristUI.isEnabled = value;
			if (value) {
				//initialize human ui again
				touristUI.transform.parent = TouristInfo.transform;
				touristUI.size = new Vector3(TouristInfo.size.x, HumanInfo.size.y);
				touristUI.baseBuildingWindow = TouristInfo.gameObject.transform.GetComponentInChildren<TouristWorldInfoPanel>();
				touristUI.position = new Vector3(TouristInfo.size.x, TouristInfo.size.y);
				TouristUI.refeshOnce = true;
				touristUI.Show();
			} else {
				touristUI.Hide();
			}
		}

		public static void RemoveGui() {
			isGuiRunning = false;
			if (parentGuiView != null) {
				parentGuiView = null;
				UnityEngine.Object.Destroy(ecnomicUI);
				UnityEngine.Object.Destroy(realCityUI);
				UnityEngine.Object.Destroy(politicsUI);
				UnityEngine.Object.Destroy(EcButton);
				UnityEngine.Object.Destroy(RcButton);
				UnityEngine.Object.Destroy(PlButton);
				ecnomicUI = null;
				realCityUI = null;
				politicsUI = null;
				EcButton = null;
				RcButton = null;
				PlButton = null;
			}

			if (BButton != null) {
				UnityEngine.Object.Destroy(BButton);
				BButton = null;
			}

			if (PBButton != null) {
				UnityEngine.Object.Destroy(PBButton);
				PBButton = null;
			}

			if (buildingWindowGameObject != null) {
				UnityEngine.Object.Destroy(buildingWindowGameObject);
			}
			//remove HumanUI
			if (humanUI != null) {
				if (humanUI.parent != null) {
					humanUI.parent.eventVisibilityChanged -= HumanInfo_eventVisibilityChanged;
				}
			}
			if (HumanWindowGameObject != null) {
				UnityEngine.Object.Destroy(HumanWindowGameObject);
			}
			//remove TouristUI
			if (touristUI != null) {
				if (touristUI.parent != null) {
					touristUI.parent.eventVisibilityChanged -= TouristInfo_eventVisibilityChanged;
				}
			}

			if (TouristWindowGameObject != null) {
				UnityEngine.Object.Destroy(TouristWindowGameObject);
			}

			if (!isTransportLinesManagerRunning) {
				if (PBLUI != null) {
					if (PBLUI.parent != null) {
						PBLUI.parent.eventVisibilityChanged -= PBLInfo_eventVisibilityChanged;
					}
				}

				if (PBLWindowGameObject != null) {
					UnityEngine.Object.Destroy(PBLWindowGameObject);
				}
				PBLUI._initialized = false;
			}
		}

		private bool Check3rdPartyModLoaded(string namespaceStr, bool printAll = false) {
			bool result = false;
			FieldInfo field = typeof(LoadingWrapper).GetField("m_LoadingExtensions", BindingFlags.Instance | BindingFlags.NonPublic);
			List<ILoadingExtension> list = (List<ILoadingExtension>)field.GetValue(Singleton<LoadingManager>.instance.m_LoadingWrapper);
			if (list != null) {
				foreach (ILoadingExtension current in list) {
					if (printAll) {
						DebugLog.LogToFileOnly(string.Format("Detected extension: {0} in namespace {1}", current.GetType().Name, current.GetType().Namespace));
					}
					if (current.GetType().Namespace != null) {
						string value = current.GetType().Namespace.ToString();
						if (namespaceStr.Equals(value)) {
							DebugLog.LogToFileOnly(string.Format("The mod '{0}' has been detected.", namespaceStr));
							result = true;
							break;
						}
					}
				}
			}
			return result;
		}

		public static void HarmonyInitDetour() {
			if (HarmonyHelper.IsHarmonyInstalled) {
				if (!HarmonyDetourInited) {
					DebugLog.LogToFileOnly("Init harmony detours");
					HarmonyDetours.Apply();
					HarmonyDetourInited = true;
				}
			}
		}

		public static void HarmonyRevertDetour() {
			if (HarmonyHelper.IsHarmonyInstalled) {
				if (HarmonyDetourInited) {
					DebugLog.LogToFileOnly("Revert harmony detours");
					HarmonyDetours.DeApply();
					HarmonyDetourFailed = true;
					HarmonyDetourInited = false;
				}
			}
		}

		private bool CheckTransportLinesManagerIsLoaded() {
			return Check3rdPartyModLoaded("Klyte.TransportLinesManager", false);
		}

		private bool CheckRealTimeIsLoaded() {
			return Check3rdPartyModLoaded("RealTime.Core", false);
		}
	}
}