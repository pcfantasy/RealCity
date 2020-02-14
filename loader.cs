using ColossalFramework.UI;
using ICities;
using UnityEngine;
using System.IO;
using ColossalFramework;
using System.Reflection;
using System;
using System.Collections.Generic;
using RealCity.CustomAI;
using RealCity.UI;
using RealCity.Util;
using RealCity.CustomManager;
using ColossalFramework.Plugins;
using RealCity.RebalancedIndustries;
using RealCity.CustomData;
using RealCity.Patch;

namespace RealCity
{
    public class Loader : LoadingExtensionBase
    {
        public class Detour
        {
            public MethodInfo OriginalMethod;
            public MethodInfo CustomMethod;
            public RedirectCallsState Redirect;

            public Detour(MethodInfo originalMethod, MethodInfo customMethod)
            {
                OriginalMethod = originalMethod;
                CustomMethod = customMethod;
                Redirect = RedirectionHelper.RedirectCalls(originalMethod, customMethod);
            }
        }

        public static List<Detour> Detours { get; set; }
        public static bool DetourInited = false;
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
        public static bool isRealConstructionRunning = false;
        public static bool isRealGasStationRunning = false;
        public static bool isAdvancedJunctionRuleRunning = false;
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

        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);
            Detours = new List<Detour>();
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            CurrentLoadMode = mode;
            if (RealCity.IsEnabled)
            {
                if (mode == LoadMode.LoadGame || mode == LoadMode.NewGame)
                {
                    isRealConstructionRunning = CheckRealConstructionIsLoaded();
                    isRealGasStationRunning = CheckRealGasStationIsLoaded();
                    isAdvancedJunctionRuleRunning = CheckAdvancedJunctionRuleIsLoaded();
                    isTransportLinesManagerRunning = CheckTransportLinesManagerIsLoaded();
                    //refresh OptionsMainPanel
                    MethodInfo method = typeof(OptionsMainPanel).GetMethod("OnLocaleChanged", BindingFlags.Instance | BindingFlags.NonPublic);
                    method.Invoke(UIView.library.Get<OptionsMainPanel>("OptionsPanel"), new object[0]);
                    SetupGui();
                    InitDetour();
                    HarmonyInitDetour();
                    RealCity.LoadSetting();
                    RealCityThreading.isFirstTime = true;
                    DebugLog.LogToFileOnly("OnLevelLoaded");
                    if (mode == LoadMode.NewGame)
                    {
                        InitData();
                        DebugLog.LogToFileOnly("InitData");
                    }
                }
            }
        }

        public static void InitData()
        {
            MainDataStore.data_init();
            CustomTransportLine.DataInit();
            RealCityEconomyManager.dataInit();
            RealCityEconomyManager.saveData = new byte[2856];
            RealCityPrivateBuildingAI.saveData = new byte[316];
            RealCityResidentAI.saveData = new byte[140];
            MainDataStore.saveData = new byte[3932402];
            MainDataStore.saveData1 = new byte[4194304];
            MainDataStore.saveData2 = new byte[1048576];
            MainDataStore.saveDataForMoreVehicle = new byte[147456];
            Politics.saveData = new byte[103];
            CustomTransportLine.saveData = new byte[512];
            System.Random rand = new System.Random();
            RealCityEconomyExtension.partyTrend = (byte)rand.Next(5);
            RealCityEconomyExtension.partyTrendStrength = (byte)rand.Next(300);

            for (int i = 0; i < Singleton<VehicleManager>.instance.m_vehicles.m_size; i++)
            {
                VehicleManagerReleaseVehicleImplementationPatch.watingPathTime[i] = 0;
                VehicleManagerReleaseVehicleImplementationPatch.stuckTime[i] = 0;
            }

            for (int j = 0; j < 65536; j++)
            {
                HumanAISimulationStepPatch.watingPathTime[j] = 0;
            }
        }

        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();
            if (CurrentLoadMode == LoadMode.LoadGame || CurrentLoadMode == LoadMode.NewGame)
            {
                if (RealCity.IsEnabled && isGuiRunning)
                {
                    RemoveGui();
                }
                if (RealCity.IsEnabled && DetourInited)
                {
                    RealCityThreading.isFirstTime = true;
                    RevertDetours();
                    HarmonyRevertDetour();
                }
            }
        }

        public override void OnReleased()
        {
            base.OnReleased();
        }

        private static void LoadSprites()
        {
            if (SpriteUtilities.GetAtlas(m_atlasName) != null) return;
            var modPath = PluginManager.instance.FindPluginInfo(Assembly.GetExecutingAssembly()).modPath;
            m_atlasLoaded = SpriteUtilities.InitialiseAtlas(Path.Combine(modPath, "Icon/RealCity.png"), m_atlasName);
            if (m_atlasLoaded)
            {
                var spriteSuccess = true;
                spriteSuccess = SpriteUtilities.AddSpriteToAtlas(new Rect(new Vector2(382, 0), new Vector2(191, 191)), "EcButton", m_atlasName)
                             && SpriteUtilities.AddSpriteToAtlas(new Rect(new Vector2(0, 0), new Vector2(191, 191)), "Blank", m_atlasName)
                             && SpriteUtilities.AddSpriteToAtlas(new Rect(new Vector2(191, 0), new Vector2(191, 191)), "BuildingButton", m_atlasName)
                             && SpriteUtilities.AddSpriteToAtlas(new Rect(new Vector2(573, 0), new Vector2(191, 191)), "Politics", m_atlasName)
                             && SpriteUtilities.AddSpriteToAtlas(new Rect(new Vector2(764, 0), new Vector2(191, 191)), "RcButton", m_atlasName)
                             && spriteSuccess;
                if (!spriteSuccess) DebugLog.LogToFileOnly("Some sprites haven't been loaded. This is abnormal; you should probably report this to the mod creator.");
            }
            else DebugLog.LogToFileOnly("The texture atlas (provides custom icons) has not loaded. All icons have reverted to text prompts.");
        }

        public static void SetupGui()
        {
            LoadSprites();
            if (m_atlasLoaded)
            {
                parentGuiView = null;
                parentGuiView = UIView.GetAView();
                if (ecnomicUI == null)
                {
                    ecnomicUI = (EcnomicUI)parentGuiView.AddUIComponent(typeof(EcnomicUI));
                }

                if (realCityUI == null)
                {
                    realCityUI = (RealCityUI)parentGuiView.AddUIComponent(typeof(RealCityUI));
                }

                if (politicsUI == null)
                {
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
        public static void SetupPBLUIGui()
        {
            PBLWindowGameObject = new GameObject("PBLWindowGameObject");
            PBLUI = (PBLUI)PBLWindowGameObject.AddComponent(typeof(PBLUI));
            PBLInfo = UIView.Find<UIPanel>("(Library) PublicTransportWorldInfoPanel");
            if (PBLInfo == null)
            {
                DebugLog.LogToFileOnly("UIPanel not found (update broke the mod!): (Library) PublicTransportWorldInfoPanel\nAvailable panels are:\n");
            }
            else
            {
                PBLUI.transform.parent = PBLInfo.transform;
                PBLUI.size = new Vector3(150, 100);
                PBLUI.baseBuildingWindow = PBLInfo.gameObject.transform.GetComponentInChildren<PublicTransportWorldInfoPanel>();
                UILabel UILabel = PBLUI.baseBuildingWindow.Find<UILabel>("ModelLabel");
                PBLUI.position = new Vector3(UILabel.relativePosition.x + 50f, PBLInfo.size.y - (UILabel.relativePosition.y + 160f));
                PBLInfo.eventVisibilityChanged += PBLInfo_eventVisibilityChanged;
            }
        }
        public static void PBLInfo_eventVisibilityChanged(UIComponent component, bool value)
        {
            PBLUI.isEnabled = value;
            if (value)
            {
                //initialize PBL ui again
                PBLUI.transform.parent = PBLInfo.transform;
                PBLUI.size = new Vector3(150, 100);
                PBLUI.baseBuildingWindow = PBLInfo.gameObject.transform.GetComponentInChildren<PublicTransportWorldInfoPanel>();
                UILabel UILabel = PBLUI.baseBuildingWindow.Find<UILabel>("ModelLabel");
                //DebugLog.LogToFileOnly(UILabel.relativePosition.x.ToString() + "    " +  UILabel.relativePosition.y.ToString());
                PBLUI.position = new Vector3(UILabel.relativePosition.x + 50f, PBLInfo.size.y - (UILabel.relativePosition.y + 160f));
                PBLUI.refeshOnce = true;
                PBLUI.Show();
            }
            else
            {
                PBLUI.Hide();
            }
        }
        public static void SetupHumanGui()
        {
            HumanWindowGameObject = new GameObject("HumanWindowGameObject");
            humanUI = (HumanUI)HumanWindowGameObject.AddComponent(typeof(HumanUI));
            HumanInfo = UIView.Find<UIPanel>("(Library) CitizenWorldInfoPanel");
            if (HumanInfo == null)
            {
                DebugLog.LogToFileOnly("UIPanel not found (update broke the mod!): (Library) CitizenWorldInfoPanel\nAvailable panels are:\n");
            }
            humanUI.transform.parent = HumanInfo.transform;
            humanUI.size = new Vector3(HumanInfo.size.x, HumanInfo.size.y);
            humanUI.baseBuildingWindow = HumanInfo.gameObject.transform.GetComponentInChildren<CitizenWorldInfoPanel>();
            humanUI.position = new Vector3(HumanInfo.size.x, HumanInfo.size.y);
            HumanInfo.eventVisibilityChanged += HumanInfo_eventVisibilityChanged;
        }

        public static void SetupTouristGui()
        {
            TouristWindowGameObject = new GameObject("TouristWindowGameObject");
            touristUI = (TouristUI)TouristWindowGameObject.AddComponent(typeof(TouristUI));
            TouristInfo = UIView.Find<UIPanel>("(Library) TouristWorldInfoPanel");
            if (TouristInfo == null)
            {
                DebugLog.LogToFileOnly("UIPanel not found (update broke the mod!): (Library) TouristWorldInfoPanel\nAvailable panels are:\n");
            }
            touristUI.transform.parent = TouristInfo.transform;
            touristUI.size = new Vector3(TouristInfo.size.x, TouristInfo.size.y);
            touristUI.baseBuildingWindow = TouristInfo.gameObject.transform.GetComponentInChildren<TouristWorldInfoPanel>();
            touristUI.position = new Vector3(TouristInfo.size.x, TouristInfo.size.y);
            TouristInfo.eventVisibilityChanged += TouristInfo_eventVisibilityChanged;
        }

        public static void SetupEcnomicButton()
        {
            if (EcButton == null)
            {
                EcButton = (parentGuiView.AddUIComponent(typeof(EcnomicButton)) as EcnomicButton);
            }
            EcButton.Show();
        }

        public static void SetupCityButton()
        {
            if (RcButton == null)
            {
                RcButton = (parentGuiView.AddUIComponent(typeof(RealCityButton)) as RealCityButton);
            }
            RcButton.Show();
        }

        public static void SetupBuildingButton()
        {
            var buildingInfo = UIView.Find<UIPanel>("(Library) ZonedBuildingWorldInfoPanel");
            if (BButton == null)
            {
                BButton = (buildingInfo.AddUIComponent(typeof(BuildingButton)) as BuildingButton);
            }
            BButton.width = 40f;
            BButton.height = 35f;
            BButton.relativePosition = new Vector3(120, buildingInfo.size.y - BButton.height);
            BButton.Show();
        }

        public static void SetupPlayerBuildingButton()
        {
            var playerBuildingInfo = UIView.Find<UIPanel>("(Library) CityServiceWorldInfoPanel");
            if (PBButton == null)
            {
                PBButton = (playerBuildingInfo.AddUIComponent(typeof(PlayerBuildingButton)) as PlayerBuildingButton);
            }
            PBButton.width = 40f;
            PBButton.height = 35f;
            PBButton.relativePosition = new Vector3(120, playerBuildingInfo.size.y - PBButton.height);
            PBButton.Show();
        }

        public static void SetupPLButton()
        {
            if (PlButton == null)
            {
                PlButton = (parentGuiView.AddUIComponent(typeof(PoliticsButton)) as PoliticsButton);
            }
            PlButton.Show();
        }

        public static void HumanInfo_eventVisibilityChanged(UIComponent component, bool value)
        {
            humanUI.isEnabled = value;
            if (value)
            {
                //initialize human ui again
                humanUI.transform.parent = HumanInfo.transform;
                humanUI.size = new Vector3(HumanInfo.size.x, HumanInfo.size.y);
                humanUI.baseBuildingWindow = HumanInfo.gameObject.transform.GetComponentInChildren<CitizenWorldInfoPanel>();
                humanUI.position = new Vector3(HumanInfo.size.x, HumanInfo.size.y);
                HumanUI.refeshOnce = true;
                humanUI.Show();
            }
            else
            {
                humanUI.Hide();
            }
        }

        public static void TouristInfo_eventVisibilityChanged(UIComponent component, bool value)
        {
            touristUI.isEnabled = value;
            if (value)
            {
                //initialize human ui again
                touristUI.transform.parent = TouristInfo.transform;
                touristUI.size = new Vector3(TouristInfo.size.x, HumanInfo.size.y);
                touristUI.baseBuildingWindow = TouristInfo.gameObject.transform.GetComponentInChildren<TouristWorldInfoPanel>();
                touristUI.position = new Vector3(TouristInfo.size.x, TouristInfo.size.y);
                TouristUI.refeshOnce = true;
                touristUI.Show();
            }
            else
            {
                touristUI.Hide();
            }
        }

        public static void RemoveGui()
        {
            isGuiRunning = false;
            if (parentGuiView != null)
            {
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

            if (BButton != null)
            {
                UnityEngine.Object.Destroy(BButton);
                BButton = null;
            }

            if (PBButton != null)
            {
                UnityEngine.Object.Destroy(PBButton);
                PBButton = null;
            }

            if (buildingWindowGameObject != null)
            {
                UnityEngine.Object.Destroy(buildingWindowGameObject);
            }
            //remove HumanUI
            if (humanUI != null)
            {
                if (humanUI.parent != null)
                {
                    humanUI.parent.eventVisibilityChanged -= HumanInfo_eventVisibilityChanged;
                }
            }
            if (HumanWindowGameObject != null)
            {
                UnityEngine.Object.Destroy(HumanWindowGameObject);
            }
            //remove TouristUI
            if (touristUI != null)
            {
                if (touristUI.parent != null)
                {
                    touristUI.parent.eventVisibilityChanged -= TouristInfo_eventVisibilityChanged;
                }
            }

            if (TouristWindowGameObject != null)
            {
                UnityEngine.Object.Destroy(TouristWindowGameObject);
            }

            if (!isTransportLinesManagerRunning)
            {
                if (PBLUI != null)
                {
                    if (PBLUI.parent != null)
                    {
                        PBLUI.parent.eventVisibilityChanged -= PBLInfo_eventVisibilityChanged;
                    }
                }

                if (PBLWindowGameObject != null)
                {
                    UnityEngine.Object.Destroy(PBLWindowGameObject);
                }
                PBLUI._initialized = false;
            }
        }

        private bool Check3rdPartyModLoaded(string namespaceStr, bool printAll = false)
        {
            bool result = false;
            FieldInfo field = typeof(LoadingWrapper).GetField("m_LoadingExtensions", BindingFlags.Instance | BindingFlags.NonPublic);
            List<ILoadingExtension> list = (List<ILoadingExtension>)field.GetValue(Singleton<LoadingManager>.instance.m_LoadingWrapper);
            if (list != null)
            {
                foreach (ILoadingExtension current in list)
                {
                    if (printAll)
                    {
                        DebugLog.LogToFileOnly(string.Format("Detected extension: {0} in namespace {1}", current.GetType().Name, current.GetType().Namespace));
                    }
                    if (current.GetType().Namespace != null)
                    {
                        string value = current.GetType().Namespace.ToString();
                        if (namespaceStr.Equals(value))
                        {
                            DebugLog.LogToFileOnly(string.Format("The mod '{0}' has been detected.", namespaceStr));
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        private bool CheckRealConstructionIsLoaded()
        {
            return Check3rdPartyModLoaded("RealConstruction", false);
        }

        public void HarmonyInitDetour()
        {
            if (!HarmonyDetourInited)
            {
                DebugLog.LogToFileOnly("Init harmony detours");
                HarmonyDetours.Apply();
                HarmonyDetourInited = true;
            }
        }

        public void HarmonyRevertDetour()
        {
            if (HarmonyDetourInited)
            {
                DebugLog.LogToFileOnly("Revert harmony detours");
                HarmonyDetours.DeApply();
                HarmonyDetourFailed = true;
                HarmonyDetourInited = false;
            }
        }

        public void InitDetour()
        {
            if (!DetourInited)
            {
                DebugLog.LogToFileOnly("Init detours");
                bool detourFailed = false;

                //1
                DebugLog.LogToFileOnly("Detour TransferManager::StartTransfer calls");
                try
                {
                    Detours.Add(new Detour(typeof(TransferManager).GetMethod("StartTransfer", BindingFlags.NonPublic | BindingFlags.Instance),
                                           typeof(RealCityTransferManager).GetMethod("StartTransfer", BindingFlags.NonPublic | BindingFlags.Instance)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour TransferManager::StartTransfer");
                    detourFailed = true;
                }

                //2
                DebugLog.LogToFileOnly("Detour IndustrialBuildingAI::ModifyMaterialBuffer calls");
                try
                {
                    Detours.Add(new Detour(typeof(IndustrialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null),
                                           typeof(RealCityIndustrialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour IndustrialBuildingAI::ModifyMaterialBuffer");
                    detourFailed = true;
                }

                //3
                DebugLog.LogToFileOnly("Detour IndustrialExtractorAI::ModifyMaterialBuffer calls");
                try
                {
                    Detours.Add(new Detour(typeof(IndustrialExtractorAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null),
                                           typeof(RealCityIndustrialExtractorAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour IndustrialExtractorAI::ModifyMaterialBuffer");
                    detourFailed = true;
                }

                //4
                DebugLog.LogToFileOnly("Detour CommercialBuildingAI::ModifyMaterialBuffer calls");
                try
                {
                    Detours.Add(new Detour(typeof(CommercialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null),
                                           typeof(RealCityCommercialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour CommercialBuildingAI::ModifyMaterialBuffer");
                    detourFailed = true;
                }

                //5
                DebugLog.LogToFileOnly("Detour ProcessingFacilityAI::GetResourceRate calls");
                try
                {
                    Detours.Add(new Detour(typeof(ProcessingFacilityAI).GetMethod("GetResourceRate", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(EconomyManager.Resource) }, null),
                                           typeof(RealCityProcessingFacilityAI).GetMethod("GetResourceRate", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(EconomyManager.Resource) }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour ProcessingFacilityAI::GetResourceRate");
                    detourFailed = true;
                }

                //6
                DebugLog.LogToFileOnly("Detour OfficeBuildingAI::GetOutgoingTransferReason calls");
                try
                {
                    Detours.Add(new Detour(typeof(OfficeBuildingAI).GetMethod("GetOutgoingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance),
                                           typeof(RealCityOfficeBuildingAI).GetMethod("GetOutgoingTransferReason", BindingFlags.Public | BindingFlags.Instance)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour OfficeBuildingAI::GetOutgoingTransferReason");
                    detourFailed = true;
                }

                //7
                if (!isRealGasStationRunning)
                {
                    DebugLog.LogToFileOnly("Detour PassengerCarAI::ArriveAtTarget calls");
                    try
                    {
                        Detours.Add(new Detour(typeof(PassengerCarAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null),
                                               typeof(RealCityPassengerCarAI).GetMethod("CustomArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null)));
                    }
                    catch (Exception)
                    {
                        DebugLog.LogToFileOnly("Could not detour PassengerCarAI::ArriveAtTarget");
                        detourFailed = true;
                    }

                    //8
                    DebugLog.LogToFileOnly("Detour CargoTruckAI::ArriveAtTarget calls");
                    try
                    {
                        Detours.Add(new Detour(typeof(CargoTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null),
                                               typeof(RealCityCargoTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null)));
                    }
                    catch (Exception)
                    {
                        DebugLog.LogToFileOnly("Could not detour CargoTruckAI::ArriveAtTarget");
                        detourFailed = true;
                    }
                }

                //9
                DebugLog.LogToFileOnly("Detour CargoTruckAI::SetSource calls");
                try
                {
                    Detours.Add(new Detour(typeof(CargoTruckAI).GetMethod("SetSource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(ushort) }, null),
                                           typeof(RealCityCargoTruckAI).GetMethod("SetSource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(ushort) }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour CargoTruckAI::SetSource");
                    detourFailed = true;
                }

                //10
                DebugLog.LogToFileOnly("Detour TaxiAI::UnloadPassengers calls");
                try
                {
                    Detours.Add(new Detour(typeof(TaxiAI).GetMethod("UnloadPassengers", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(TransportPassengerData).MakeByRefType() }, null),
                                           typeof(RealCityTaxiAI).GetMethod("UnloadPassengers", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(TransportPassengerData).MakeByRefType() }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour TaxiAI::UnloadPassengers");
                    detourFailed = true;
                }

                //11
                DebugLog.LogToFileOnly("Detour ExtractingFacilityAI::GetResourceRate calls");
                try
                {
                    Detours.Add(new Detour(typeof(ExtractingFacilityAI).GetMethod("GetResourceRate", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(EconomyManager.Resource) }, null),
                                           typeof(RealCityExtractingFacilityAI).GetMethod("GetResourceRate", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(EconomyManager.Resource) }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour ExtractingFacilityAI::GetResourceRate");
                    detourFailed = true;
                }

                //12
                DebugLog.LogToFileOnly("Detour PlayerBuildingAI::GetResourceRate calls");
                try
                {
                    Detours.Add(new Detour(typeof(PlayerBuildingAI).GetMethod("GetResourceRate", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(EconomyManager.Resource) }, null),
                                           typeof(RealCityPlayerBuildingAI).GetMethod("GetResourceRate", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(EconomyManager.Resource) }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour PlayerBuildingAI::GetResourceRate");
                    detourFailed = true;
                }

                //13
                DebugLog.LogToFileOnly("Detour BuildingManager::FindBuilding calls");
                try
                {
                    Detours.Add(new Detour(typeof(BuildingManager).GetMethod("FindBuilding", BindingFlags.Public | BindingFlags.Instance),
                                           typeof(RealCityBuildingManager).GetMethod("CustomFindBuilding", BindingFlags.Public | BindingFlags.Instance)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour BuildingManager::FindBuilding");
                    detourFailed = true;
                }

                //14
                DebugLog.LogToFileOnly("Detour IndustryBuildingAI::GetResourcePrice calls");
                try
                {
                    Detours.Add(new Detour(typeof(IndustryBuildingAI).GetMethod("GetResourcePrice", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static),
                                           typeof(RealCityIndustryBuildingAI).GetMethod("CustomGetResourcePrice", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour IndustryBuildingAI::GetResourcePrice ");
                    detourFailed = true;
                }

                //15
                DebugLog.LogToFileOnly("Detour TollBoothAI::EnterBuildingSegment calls");
                try
                {
                    Detours.Add(new Detour(typeof(TollBoothAI).GetMethod("EnterBuildingSegment", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(ushort), typeof(byte), typeof(InstanceID) }, null),
                                           typeof(RealCityTollBooth).GetMethod("EnterBuildingSegment", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(ushort), typeof(byte), typeof(InstanceID) }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour TollBoothAI::EnterBuildingSegment");
                    detourFailed = true;
                }

                //16
                DebugLog.LogToFileOnly("Detour HumanAI::EnterParkArea calls");
                try
                {
                    Detours.Add(new Detour(typeof(HumanAI).GetMethod("EnterParkArea", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(byte), typeof(ushort) }, null),
                                           typeof(RealCityHumanAI).GetMethod("EnterParkArea", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(byte), typeof(ushort) }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour HumanAI::EnterParkArea");
                    detourFailed = true;
                }

                //17
                DebugLog.LogToFileOnly("Detour LandfillSiteAI::GetGarbageRate calls");
                try
                {
                    Detours.Add(new Detour(typeof(LandfillSiteAI).GetMethod("GetGarbageRate", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType()}, null),
                                           typeof(LandfillSiteAIGetGarbageRate).GetMethod("CustomGetGarbageRate", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType()}, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour LandfillSiteAI::GetGarbageRate");
                    detourFailed = true;
                }

                //18
                //private void CalculateArenasExpenses(EconomyPanel.ArenaIndex arenaIndex, ref long expenses)
                DebugLog.LogToFileOnly("Detour EconomyPanel.IncomeExpensesPoll::CalculateArenasExpenses calls");
                try
                {
                    Detours.Add(new Detour(typeof(EconomyPanel).GetNestedType("IncomeExpensesPoll", BindingFlags.NonPublic).GetMethod("CalculateArenasExpenses", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(EconomyPanel.ArenaIndex), typeof(long).MakeByRefType() }, null),
                                           typeof(RealCityEconomyPanel).GetMethod("CalculateArenasExpenses", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(EconomyPanel.ArenaIndex), typeof(long).MakeByRefType() }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour EconomyPanel.IncomeExpensesPoll::CalculateArenasExpenses");
                    detourFailed = true;
                }

                //19
                //public static uint CalculatePolicyExpenses(ref DistrictPark district)
                DebugLog.LogToFileOnly("Detour DistrictPark::CalculatePolicyExpenses calls");
                try
                {
                    Detours.Add(new Detour(typeof(DistrictPark).GetMethod("CalculatePolicyExpenses", BindingFlags.Public | BindingFlags.Instance),
                                           typeof(CustomDistrictPark).GetMethod("CalculatePolicyExpenses", BindingFlags.Public | BindingFlags.Static, null, new Type[] {typeof(DistrictPark).MakeByRefType() }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour Detour DistrictPark::CalculatePolicyExpenses");
                    detourFailed = true;
                }

                //20
                //public static void CalculateVarsityExpenses(ref DistrictPark district, out ulong upkeep, out int coaching, out int cheerleading, out int policies, out ulong total)
                DebugLog.LogToFileOnly("Detour DistrictPark:CalculateVarsityExpenses calls");
                try
                {
                    Detours.Add(new Detour(typeof(DistrictPark).GetMethod("CalculateVarsityExpenses", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ulong).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(ulong).MakeByRefType() }, null),
                                           typeof(CustomDistrictPark).GetMethod("CalculateVarsityExpenses", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(DistrictPark).MakeByRefType(), typeof(ulong).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(ulong).MakeByRefType() }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not DistrictPark:CalculateVarsityExpenses");
                    detourFailed = true;
                }

                if (detourFailed)
                {
                    DebugLog.LogToFileOnly("Detours failed");
                }
                else
                {
                    DebugLog.LogToFileOnly("Detours successful");
                }

                DetourInited = true;
            }
        }

        public void RevertDetours()
        {
            if (DetourInited)
            {
                DebugLog.LogToFileOnly("Revert detours");
                Detours.Reverse();
                foreach (Detour d in Detours)
                {
                    RedirectionHelper.RevertRedirect(d.OriginalMethod, d.Redirect);
                }
                DetourInited = false;
                Detours.Clear();
                DebugLog.LogToFileOnly("Reverting detours finished.");
            }
        }

        private bool CheckRealGasStationIsLoaded()
        {
            return Check3rdPartyModLoaded("RealGasStation", true);
        }

        private bool CheckAdvancedJunctionRuleIsLoaded()
        {
            return Check3rdPartyModLoaded("AdvancedJunctionRule", false);
        }

        private bool CheckTransportLinesManagerIsLoaded()
        {
            return Check3rdPartyModLoaded("Klyte.TransportLinesManager", true);
        }
    }
}
