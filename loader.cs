using ColossalFramework.UI;
using ICities;
using UnityEngine;
using System.IO;
using ColossalFramework;
using System.Reflection;
using System;
using System.Linq;
using ColossalFramework.Math;
using System.Collections.Generic;
using RealCity.CustomAI;
using RealCity.UI;
using RealCity.Util;
using RealCity.CustomManager;

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
                this.OriginalMethod = originalMethod;
                this.CustomMethod = customMethod;
                this.Redirect = RedirectionHelper.RedirectCalls(originalMethod, customMethod);
            }
        }

        public static List<Detour> Detours { get; set; }
        public static bool DetourInited = false;
        public static bool HarmonyDetourInited = false;
        public static UIView parentGuiView;
        public static UIPanel buildingInfo;
        public static UIPanel playerbuildingInfo;
        public static UIPanel HumanInfo;
        public static UIPanel TouristInfo;
        public static EcnomicUI guiPanel;
        public static RealCityUI guiPanel1;
        public static BuildingUI guiPanel2;
        public static HumanUI guiPanel3;
        public static PlayerBuildingUI guiPanel4;
        public static PoliticsUI guiPanel5;
        public static TouristUI guiPanel6;
        public static GameObject buildingWindowGameObject;
        public static GameObject PlayerbuildingWindowGameObject;
        public static GameObject HumanWindowGameObject;
        public static GameObject TouristWindowGameObject;
        public static LoadMode CurrentLoadMode;
        public static bool isGuiRunning = false;
        public static bool isRealConstructionRunning = false;
        public static bool isRealGasStationRunning = false;
        public static bool isTransportLinesManagerRunning = false;
        public static PoliticsButton PLPanel;
        public static EcnomicButton EcMenuPanel;
        public static RealCityButton RcMenuPanel;
        public static BuildingButton BMenuPanel;
        public static PlayerBuildingButton PBMenuPanel;

        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);
            Detours = new List<Detour>();
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            Loader.CurrentLoadMode = mode;
            if (RealCity.IsEnabled)
            {
                if (mode == LoadMode.LoadGame || mode == LoadMode.NewGame)
                {
                    SetupGui();
                    isRealConstructionRunning = CheckRealConstructionIsLoaded();
                    isRealGasStationRunning = CheckRealGasStationIsLoaded();
                    isTransportLinesManagerRunning = CheckTransportLinesManagerIsLoaded();
                    InitDetour();
                    HarmonyInitDetour();
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
            RealCityEconomyManager.dataInit();
            RealCityEconomyManager.saveData = new byte[2844];
            RealCityPrivateBuildingAI.saveData = new byte[316];
            RealCityResidentAI.saveData = new byte[140];
            MainDataStore.saveData = new byte[3932402];
            MainDataStore.saveData1 = new byte[4194304];
            MainDataStore.saveData2 = new byte[1048576];
            Politics.saveData = new byte[103];
            System.Random rand = new System.Random();
            RealCityEconomyExtension.partyTrend = (byte)rand.Next(5);
            RealCityEconomyExtension.partyTrendStrength = (byte)rand.Next(300);
        }

        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();
            if (Loader.CurrentLoadMode == LoadMode.LoadGame || Loader.CurrentLoadMode == LoadMode.NewGame)
            {
                if (RealCity.IsEnabled && Loader.isGuiRunning)
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

        public static void SetupGui()
        {
            Loader.parentGuiView = null;
            Loader.parentGuiView = UIView.GetAView();
            if (Loader.guiPanel == null)
            {
                Loader.guiPanel = (EcnomicUI)Loader.parentGuiView.AddUIComponent(typeof(EcnomicUI));
            }

            if (Loader.guiPanel1 == null)
            {
                Loader.guiPanel1 = (RealCityUI)Loader.parentGuiView.AddUIComponent(typeof(RealCityUI));
            }

            if (Loader.guiPanel5 == null)
            {
                Loader.guiPanel5 = (PoliticsUI)Loader.parentGuiView.AddUIComponent(typeof(PoliticsUI));
            }

            SetupBuidingGui();
            SetupHumanGui();
            SetupTouristGui();
            SetupPlayerBuidingGui();
            SetupEcnomicButton();
            SetupPLButton();
            SetupCityButton();
            SetupBuildingButton();
            SetupPlayerBuildingButton();
            Loader.isGuiRunning = true;
        }

        public static void SetupBuidingGui()
        {
            buildingWindowGameObject = new GameObject("buildingWindowObject");
            guiPanel2 = (BuildingUI)buildingWindowGameObject.AddComponent(typeof(BuildingUI));
            buildingInfo = UIView.Find<UIPanel>("(Library) ZonedBuildingWorldInfoPanel");
            if (buildingInfo == null)
            {
                DebugLog.LogToFileOnly("UIPanel not found (update broke the mod!): (Library) ZonedBuildingWorldInfoPanel\nAvailable panels are:\n");
            }
            guiPanel2.transform.parent = buildingInfo.transform;
            guiPanel2.size = new Vector3(buildingInfo.size.x, buildingInfo.size.y);
            guiPanel2.baseBuildingWindow = buildingInfo.gameObject.transform.GetComponentInChildren<ZonedBuildingWorldInfoPanel>();
            guiPanel2.position = new Vector3(buildingInfo.size.x, buildingInfo.size.y);
            buildingInfo.eventVisibilityChanged += buildingInfo_eventVisibilityChanged;
        }

        public static void SetupHumanGui()
        {
            HumanWindowGameObject = new GameObject("HumanWindowGameObject");
            guiPanel3 = (HumanUI)HumanWindowGameObject.AddComponent(typeof(HumanUI));
            HumanInfo = UIView.Find<UIPanel>("(Library) CitizenWorldInfoPanel");
            if (HumanInfo == null)
            {
                DebugLog.LogToFileOnly("UIPanel not found (update broke the mod!): (Library) CitizenWorldInfoPanel\nAvailable panels are:\n");
            }
            guiPanel3.transform.parent = HumanInfo.transform;
            guiPanel3.size = new Vector3(HumanInfo.size.x, HumanInfo.size.y);
            guiPanel3.baseBuildingWindow = HumanInfo.gameObject.transform.GetComponentInChildren<CitizenWorldInfoPanel>();
            guiPanel3.position = new Vector3(HumanInfo.size.x, HumanInfo.size.y);
            HumanInfo.eventVisibilityChanged += HumanInfo_eventVisibilityChanged;
        }

        public static void SetupTouristGui()
        {
            TouristWindowGameObject = new GameObject("TouristWindowGameObject");
            guiPanel6 = (TouristUI)TouristWindowGameObject.AddComponent(typeof(TouristUI));
            TouristInfo = UIView.Find<UIPanel>("(Library) TouristWorldInfoPanel");
            if (TouristInfo == null)
            {
                DebugLog.LogToFileOnly("UIPanel not found (update broke the mod!): (Library) TouristWorldInfoPanel\nAvailable panels are:\n");
            }
            guiPanel6.transform.parent = TouristInfo.transform;
            guiPanel6.size = new Vector3(TouristInfo.size.x, TouristInfo.size.y);
            guiPanel6.baseBuildingWindow = TouristInfo.gameObject.transform.GetComponentInChildren<TouristWorldInfoPanel>();
            guiPanel6.position = new Vector3(TouristInfo.size.x, TouristInfo.size.y);
            TouristInfo.eventVisibilityChanged += TouristInfo_eventVisibilityChanged;
        }

        public static void SetupPlayerBuidingGui()
        {
            PlayerbuildingWindowGameObject = new GameObject("PlayerbuildingWindowGameObject");
            guiPanel4 = (PlayerBuildingUI)PlayerbuildingWindowGameObject.AddComponent(typeof(PlayerBuildingUI));
            playerbuildingInfo = UIView.Find<UIPanel>("(Library) CityServiceWorldInfoPanel");
            if (playerbuildingInfo == null)
            {
                DebugLog.LogToFileOnly("UIPanel not found (update broke the mod!): (Library) CityServiceWorldInfoPanel\nAvailable panels are:\n");
            }
            guiPanel4.transform.parent = playerbuildingInfo.transform;
            guiPanel4.size = new Vector3(playerbuildingInfo.size.x, playerbuildingInfo.size.y);
            guiPanel4.baseBuildingWindow = playerbuildingInfo.gameObject.transform.GetComponentInChildren<CityServiceWorldInfoPanel>();
            guiPanel4.position = new Vector3(playerbuildingInfo.size.x, playerbuildingInfo.size.y);
            playerbuildingInfo.eventVisibilityChanged += playerbuildingInfo_eventVisibilityChanged;
        }

        public static void SetupEcnomicButton()
        {
            if (EcMenuPanel == null)
            {
                EcMenuPanel = (parentGuiView.AddUIComponent(typeof(EcnomicButton)) as EcnomicButton);
            }
            EcMenuPanel.Show();
        }

        public static void SetupCityButton()
        {
            if (RcMenuPanel == null)
            {
                RcMenuPanel = (parentGuiView.AddUIComponent(typeof(RealCityButton)) as RealCityButton);
            }
            RcMenuPanel.Show();
        }

        public static void SetupBuildingButton()
        {
            if (BMenuPanel == null)
            {
                BMenuPanel = (buildingInfo.AddUIComponent(typeof(BuildingButton)) as BuildingButton);
            }
            BMenuPanel.RefPanel = buildingInfo;
            BMenuPanel.Alignment = UIAlignAnchor.BottomLeft;
            BMenuPanel.Show();
        }

        public static void SetupPlayerBuildingButton()
        {
            if (PBMenuPanel == null)
            {
                PBMenuPanel = (playerbuildingInfo.AddUIComponent(typeof(PlayerBuildingButton)) as PlayerBuildingButton);
            }
            PBMenuPanel.RefPanel = playerbuildingInfo;
            PBMenuPanel.Alignment = UIAlignAnchor.BottomLeft;
            PBMenuPanel.Show();
        }

        public static void SetupPLButton()
        {
            if (PLPanel == null)
            {
                PLPanel = (parentGuiView.AddUIComponent(typeof(PoliticsButton)) as PoliticsButton);
            }
            PLPanel.Show();
        }

        public static void buildingInfo_eventVisibilityChanged(UIComponent component, bool value)
        {
            guiPanel2.isEnabled = value;
            if (value)
            {
                Loader.guiPanel2.transform.parent = Loader.buildingInfo.transform;
                Loader.guiPanel2.size = new Vector3(Loader.buildingInfo.size.x, Loader.buildingInfo.size.y + 50f);
                Loader.guiPanel2.baseBuildingWindow = Loader.buildingInfo.gameObject.transform.GetComponentInChildren<ZonedBuildingWorldInfoPanel>();
                Loader.guiPanel2.position = new Vector3(Loader.buildingInfo.size.x, Loader.buildingInfo.size.y);
            }
            else
            {
                guiPanel2.Hide();
            }
        }

        public static void playerbuildingInfo_eventVisibilityChanged(UIComponent component, bool value)
        {
            guiPanel4.isEnabled = value;
            if (value)
            {
                Loader.guiPanel4.transform.parent = Loader.playerbuildingInfo.transform;
                Loader.guiPanel4.size = new Vector3(Loader.playerbuildingInfo.size.x, Loader.playerbuildingInfo.size.y);
                Loader.guiPanel4.baseBuildingWindow = Loader.playerbuildingInfo.gameObject.transform.GetComponentInChildren<CityServiceWorldInfoPanel>();
                Loader.guiPanel4.position = new Vector3(Loader.playerbuildingInfo.size.x, Loader.playerbuildingInfo.size.y);
            }
            else
            {
                guiPanel4.Hide();
            }
        }

        public static void HumanInfo_eventVisibilityChanged(UIComponent component, bool value)
        {
            guiPanel3.isEnabled = value;
            if (value)
            {
                //initialize human ui again
                Loader.guiPanel3.transform.parent = Loader.HumanInfo.transform;
                Loader.guiPanel3.size = new Vector3(Loader.HumanInfo.size.x, Loader.HumanInfo.size.y);
                Loader.guiPanel3.baseBuildingWindow = Loader.HumanInfo.gameObject.transform.GetComponentInChildren<CitizenWorldInfoPanel>();
                Loader.guiPanel3.position = new Vector3(Loader.HumanInfo.size.x, Loader.HumanInfo.size.y);
                HumanUI.refeshOnce = true;
                guiPanel3.Show();
            }
            else
            {
                guiPanel3.Hide();
            }
        }

        public static void TouristInfo_eventVisibilityChanged(UIComponent component, bool value)
        {
            guiPanel6.isEnabled = value;
            if (value)
            {
                //initialize human ui again
                Loader.guiPanel6.transform.parent = Loader.TouristInfo.transform;
                Loader.guiPanel6.size = new Vector3(Loader.TouristInfo.size.x, Loader.HumanInfo.size.y);
                Loader.guiPanel6.baseBuildingWindow = Loader.TouristInfo.gameObject.transform.GetComponentInChildren<TouristWorldInfoPanel>();
                Loader.guiPanel6.position = new Vector3(Loader.TouristInfo.size.x, Loader.TouristInfo.size.y);
                TouristUI.refeshOnce = true;
                guiPanel6.Show();
            }
            else
            {
                guiPanel6.Hide();
            }
        }


        public static void RemoveGui()
        {
            Loader.isGuiRunning = false;
            if (Loader.parentGuiView != null)
            {
                Loader.parentGuiView = null;
                UnityEngine.Object.Destroy(guiPanel);
                UnityEngine.Object.Destroy(guiPanel1);
                UnityEngine.Object.Destroy(guiPanel5);
                UnityEngine.Object.Destroy(EcMenuPanel);
                UnityEngine.Object.Destroy(RcMenuPanel);
                Loader.guiPanel = null;
                Loader.guiPanel1 = null;
                Loader.guiPanel5 = null;
                Loader.EcMenuPanel = null;
                Loader.RcMenuPanel = null;
            }
            //Because realcity botton in buildingUI is covered by TransportLinesManager
            if (buildingInfo != null)
            {
                UnityEngine.Object.Destroy(BMenuPanel);
                Loader.BMenuPanel = null;
            }

            if (playerbuildingInfo != null)
            {
                UnityEngine.Object.Destroy(PBMenuPanel);
                Loader.PBMenuPanel = null;
            }
            //remove buildingUI
            if (guiPanel2 != null)
            {
                if (guiPanel2.parent != null)
                {
                    guiPanel2.parent.eventVisibilityChanged -= buildingInfo_eventVisibilityChanged;
                }
            }
            if (buildingWindowGameObject != null)
            {
                UnityEngine.Object.Destroy(buildingWindowGameObject);
            }
            //remove HumanUI
            if (guiPanel3 != null)
            {
                if (guiPanel3.parent != null)
                {
                    guiPanel3.parent.eventVisibilityChanged -= HumanInfo_eventVisibilityChanged;
                }
            }
            if (HumanWindowGameObject != null)
            {
                UnityEngine.Object.Destroy(HumanWindowGameObject);
            }
            //remove PlayerbuildingUI
            if (guiPanel4 != null)
            {
                if (guiPanel4.parent != null)
                {
                    guiPanel4.parent.eventVisibilityChanged -= playerbuildingInfo_eventVisibilityChanged;
                }
            }
            //remove TouristUI
            if (guiPanel6 != null)
            {
                if (guiPanel6.parent != null)
                {
                    guiPanel6.parent.eventVisibilityChanged -= TouristInfo_eventVisibilityChanged;
                }
            }

            if (TouristWindowGameObject != null)
            {
                UnityEngine.Object.Destroy(TouristWindowGameObject);
            }

            if (PlayerbuildingWindowGameObject != null)
            {
                UnityEngine.Object.Destroy(PlayerbuildingWindowGameObject);
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
            return this.Check3rdPartyModLoaded("RealConstruction", true);
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
                DebugLog.LogToFileOnly("Detour ResidentAI::SimulationStep calls");
                try
                {
                    Detours.Add(new Detour(typeof(ResidentAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(CitizenUnit).MakeByRefType() }, null),
                                           typeof(RealCityResidentAI).GetMethod("CustomSimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(CitizenUnit).MakeByRefType() }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour ResidentAI::SimulationStep");
                    detourFailed = true;
                }

                //6
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

                //7
                DebugLog.LogToFileOnly("Detour EconomyManager::FetchResource calls");
                try
                {
                    Detours.Add(new Detour(typeof(EconomyManager).GetMethod("FetchResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass) }, null),
                                            typeof(RealCityEconomyManager).GetMethod("FetchResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass) }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour EconomyManager::FetchResource");
                    detourFailed = true;
                }

                //8
                DebugLog.LogToFileOnly("Detour EconomyManager::AddPrivateIncome calls");
                try
                {
                    Detours.Add(new Detour(typeof(EconomyManager).GetMethod("AddPrivateIncome", BindingFlags.Public | BindingFlags.Instance),
                                           typeof(RealCityEconomyManager).GetMethod("AddPrivateIncome", BindingFlags.Public | BindingFlags.Instance)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour EconomyManager::AddPrivateIncome");
                    detourFailed = true;
                }

                //9
                DebugLog.LogToFileOnly("Detour PrivateBuildingAI::SimulationStepActive calls");
                try
                {
                    Detours.Add(new Detour(typeof(PrivateBuildingAI).GetMethod("SimulationStepActive", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null),
                                           typeof(RealCityPrivateBuildingAI).GetMethod("CustomSimulationStepActive", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour PrivateBuildingAI::SimulationStepActive");
                    detourFailed = true;
                }

                //10
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

                //11
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


                //12
                DebugLog.LogToFileOnly("Detour HumanAI::EnterVehicle calls");
                try
                {
                    Detours.Add(new Detour(typeof(HumanAI).GetMethod("EnterVehicle", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType() }, null),
                                           typeof(RealCityHumanAI).GetMethod("CustomEnterVehicle", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType() }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour HumanAI::EnterVehicle");
                    detourFailed = true;
                }

                //13
                DebugLog.LogToFileOnly("Detour OfficeBuildingAI::GetOutgoingTransferReason calls");
                try
                {
                    Detours.Add(new Detour(typeof(OfficeBuildingAI).GetMethod("GetOutgoingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance),
                                           typeof(RealCityOfficeBuildingAI).GetMethod("GetOutgoingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour OfficeBuildingAI::GetOutgoingTransferReason");
                    detourFailed = true;
                }

                //14
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

                //15
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

                //16
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

                //17
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

                //18
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

                //19
                DebugLog.LogToFileOnly("Detour CargoTruckAI::ArriveAtSource calls");
                try
                {
                    Detours.Add(new Detour(typeof(CargoTruckAI).GetMethod("ArriveAtSource", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null),
                                           typeof(RealCityCargoTruckAI).GetMethod("ArriveAtSource", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour CargoTruckAI::ArriveAtSource");
                    detourFailed = true;
                }

                //20
                DebugLog.LogToFileOnly("Detour CommonBuildingAI::CalculateGuestVehicles calls");
                try
                {
                    Detours.Add(new Detour(typeof(CommonBuildingAI).GetMethod("CalculateGuestVehicles", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType() }, null),
                                           typeof(RealCityPrivateBuildingAI).GetMethod("CustomCalculateGuestVehicles", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType() }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour CommonBuildingAI::CalculateGuestVehicles");
                    detourFailed = true;
                }

                //21
                /*DebugLog.LogToFileOnly("Detour CommonBuildingAI::ReleaseBuilding calls");
                try
                {
                    Detours.Add(new Detour(typeof(CommonBuildingAI).GetMethod("ReleaseBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null),
                                           typeof(RealCityCommonBuildingAI).GetMethod("ReleaseBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour CommonBuildingAI::ReleaseBuilding");
                    detourFailed = true;
                }*/
                


                //22
                /*DebugLog.LogToFileOnly("Detour OutsideConnectionAI::ModifyMaterialBuffer calls");
                try
                {
                    Detours.Add(new Detour(typeof(OutsideConnectionAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null),
                                           typeof(RealCityOutsideConnectionAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour OutsideConnectionAI::ModifyMaterialBuffer");
                    detourFailed = true;
                }*/

                //23
                /*DebugLog.LogToFileOnly("Detour EconomyManager::GetBudget calls");
                try
                {
                    Detours.Add(new Detour(typeof(EconomyManager).GetMethod("GetBudget", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ItemClass) }, null),
                                           typeof(RealCityEconomyManager).GetMethod("GetBudget", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ItemClass) }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour EconomyManager::GetBudget");
                    detourFailed = true;
                }*/

                //24
                DebugLog.LogToFileOnly("Detour EconomyManager::AddResource calls");
                try
                {
                    Detours.Add(new Detour(typeof(EconomyManager).GetMethod("AddResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass) }, null),
                                           typeof(RealCityEconomyManager).GetMethod("AddResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass) }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour EconomyManager::AddResource");
                    detourFailed = true;
                }

                //25
                /*DebugLog.LogToFileOnly("Detour CitizenManager::ReleaseCitizenImplementation calls");
                try
                {
                    Detours.Add(new Detour(typeof(CitizenManager).GetMethod("ReleaseCitizenImplementation", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(Citizen).MakeByRefType() }, null),
                                          typeof(RealCityCitizenManager).GetMethod("ReleaseCitizenImplementation", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(Citizen).MakeByRefType() }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour CitizenManager::ReleaseCitizenImplementation");
                    detourFailed = true;
                }*/

                //26
                DebugLog.LogToFileOnly("Detour EconomyManager::AddResource1 calls");
                try
                {
                    Detours.Add(new Detour(typeof(EconomyManager).GetMethod("AddResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass.Service), typeof(ItemClass.SubService), typeof(ItemClass.Level) }, null),
                                           typeof(RealCityEconomyManager).GetMethod("AddResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass.Service), typeof(ItemClass.SubService), typeof(ItemClass.Level) }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour EconomyManager::AddResource1");
                    detourFailed = true;
                }

                //27
                /*DebugLog.LogToFileOnly("Detour OutsideConnectionAI::StartTransfer calls");
                try
                {
                    Detours.Add(new Detour(typeof(OutsideConnectionAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null),
                                           typeof(RealCityOutsideConnectionAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour OutsideConnectionAI::StartTransfer");
                    detourFailed = true;
                }*/

                //28
                /*DebugLog.LogToFileOnly("Detour GarbageTruckAI::ArriveAtTarget calls");
                try
                {
                    Detours.Add(new Detour(typeof(GarbageTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null),
                                           typeof(RealCityGarbageTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour GarbageTruckAI::ArriveAtTarget");
                    detourFailed = true;
                }*/

                //29
                DebugLog.LogToFileOnly("Detour BuildingAI::VisitorEnter calls");
                try
                {
                    Detours.Add(new Detour(typeof(BuildingAI).GetMethod("VisitorEnter", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(uint) }, null),
                                           typeof(RealCityHumanAI).GetMethod("CustomVisitorEnter", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(uint) }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour BuildingAI::VisitorEnter");
                    detourFailed = true;
                }

                //30
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

                //31
                /*DebugLog.LogToFileOnly("Detour CitizenManager::ReleaseUnitCitizen calls");
                try
                {
                    Detours.Add(new Detour(typeof(CitizenManager).GetMethod("ReleaseUnitCitizen", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(CitizenUnit).MakeByRefType(), typeof(uint) }, null),
                                           typeof(RealCityCitizenManager).GetMethod("ReleaseUnitCitizen", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(CitizenUnit).MakeByRefType(), typeof(uint) }, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour CitizenManager::ReleaseUnitCitizen");
                    detourFailed = true;
                }*/

                //32
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

                //33
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
            return this.Check3rdPartyModLoaded("RealGasStation", true);
        }

        private bool CheckTransportLinesManagerIsLoaded()
        {
            return this.Check3rdPartyModLoaded("Klyte.TransportLinesManager", true);
        }
    }
}
