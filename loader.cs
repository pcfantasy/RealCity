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

namespace RealCity
{
    public class Loader : LoadingExtensionBase
    {
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
        public static bool isFuelAlarmRunning = false;


        public static PoliticsButton PLPanel;
        public static EcnomicButton EcMenuPanel;
        public static RealCityButton RcMenuPanel;
        public static BuildingButton BMenuPanel;
        public static PlayerBuildingButton PBMenuPanel;

        public static RedirectCallsState state1;
        public static RedirectCallsState state2;
        public static RedirectCallsState state3;
        public static RedirectCallsState state4;
        public static RedirectCallsState state5;
        public static RedirectCallsState state6;
        public static RedirectCallsState state7;
        public static RedirectCallsState state8;
        public static RedirectCallsState state9;
        public static RedirectCallsState state10;
        public static RedirectCallsState state11;
        public static RedirectCallsState state12;
        public static RedirectCallsState state13;
        public static RedirectCallsState state14;
        public static RedirectCallsState state15;
        public static RedirectCallsState state16;
        public static RedirectCallsState state17;
        public static RedirectCallsState state18;
        public static RedirectCallsState state19;
        public static RedirectCallsState state20;
        public static RedirectCallsState state21;
        public static RedirectCallsState state22;
        public static RedirectCallsState state23;
        public static RedirectCallsState state24;
        public static RedirectCallsState state25;
        public static RedirectCallsState state26;
        public static RedirectCallsState state27;
        //public static RedirectCallsState state28;
        public static RedirectCallsState state29;
        public static RedirectCallsState state30;
        public static RedirectCallsState state31;
        public static RedirectCallsState state32;
        public static RedirectCallsState state33;
        //public static RedirectCallsState state34;
        public static RedirectCallsState state35;
        public static RedirectCallsState state36;
        public static RedirectCallsState state37;
        public static RedirectCallsState state38;
        public static RedirectCallsState state39;
        public static RedirectCallsState state40;
        //public static RedirectCallsState state41;
        //public static RedirectCallsState state42;
        public static RedirectCallsState state43;
        public static RedirectCallsState state44;
        public static RedirectCallsState state45;
        public static RedirectCallsState state46;
        public static RedirectCallsState state47;
        public static RedirectCallsState state48;

        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);

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
                    isFuelAlarmRunning = CheckFuelAlarmIsLoaded();
                    Detour();
                    DebugLog.LogToFileOnly("OnLevelLoaded");
                    if (mode == LoadMode.NewGame)
                    {
                        InitData();
                        DebugLog.LogToFileOnly("InitData");
                    }
                    //DebugLog.LogWarning(language.OptionUI[15]);
                    //DebugLog.LogWarning(language.OptionUI[16]);
                    //DebugLog.LogWarning(language.OptionUI[17]);
                    //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "setup_gui now");
                }
            }
        }

        public static void InitData()
        {
            MainDataStore.data_init();
            RealCityEconomyManager.data_init();
            RealCityEconomyManager.saveData = new byte[2628];
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
                if (RealCity.IsEnabled & Loader.isGuiRunning)
                {
                    RemoveGui();
                }
                if (RealCity.IsEnabled)
                {
                    RevertDetour();
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
            //guiPanel2.size = new Vector3(50,50);
            guiPanel2.baseBuildingWindow = buildingInfo.gameObject.transform.GetComponentInChildren<ZonedBuildingWorldInfoPanel>();
            guiPanel2.position = new Vector3(buildingInfo.size.x, buildingInfo.size.y);
            //guiPanel2.position = new Vector3(0, 0);
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
                //DebugLog.LogToFileOnly("select building found!!!!!:\n");
                //comm_data.current_buildingid = 0;
                //BuildingUI.refesh_once = true;
                //guiPanel2.Show();
            }
            else
            {
                //comm_data.current_buildingid = 0;
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
                //DebugLog.LogToFileOnly("select building found!!!!!:\n");
                //comm_data.current_buildingid = 0;
                //PlayerBuildingUI.refesh_once = true;
                //guiPanel4.Show();
            }
            else
            {
                //comm_data.current_buildingid = 0;
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

        public void Detour()
        {
            var srcMethod1 = typeof(TransferManager).GetMethod("StartTransfer", BindingFlags.NonPublic | BindingFlags.Instance);
            var destMethod1 = typeof(RealCityTransferManager).GetMethod("StartTransfer", BindingFlags.NonPublic | BindingFlags.Instance);
            state1 = RedirectionHelper.RedirectCalls(srcMethod1, destMethod1);

            var srcMethod2 = typeof(IndustrialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            var destMethod2 = typeof(RealCityIndustrialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            state2 = RedirectionHelper.RedirectCalls(srcMethod2, destMethod2);

            var srcMethod3 = typeof(IndustrialExtractorAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            var destMethod3 = typeof(RealCityIndustrialExtractorAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            state3 = RedirectionHelper.RedirectCalls(srcMethod3, destMethod3);

            var srcMethod4 = typeof(CommercialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            var destMethod4 = typeof(RealCityCommercialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            state4 = RedirectionHelper.RedirectCalls(srcMethod4, destMethod4);

            var srcMethod5 = typeof(ResidentAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(CitizenUnit).MakeByRefType() }, null);
            var destMethod5 = typeof(RealCityResidentAI).GetMethod("CustomSimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(CitizenUnit).MakeByRefType() }, null);
            state5 = RedirectionHelper.RedirectCalls(srcMethod5, destMethod5);


            // public override int CustomGetResourceRate(ushort buildingID, ref Building data, EconomyManager.Resource resource)
            var srcMethod6 = typeof(ProcessingFacilityAI).GetMethod("GetResourceRate", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(EconomyManager.Resource) }, null);
            var destMethod6 = typeof(RealCityProcessingFacilityAI).GetMethod("GetResourceRate", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(EconomyManager.Resource) }, null);
            state6 = RedirectionHelper.RedirectCalls(srcMethod6, destMethod6);

            var srcMethod7 = typeof(EconomyManager).GetMethod("FetchResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass) }, null);
            var destMethod7 = typeof(RealCityEconomyManager).GetMethod("FetchResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass) }, null);
            state7 = RedirectionHelper.RedirectCalls(srcMethod7, destMethod7);

            var srcMethod8 = typeof(EconomyManager).GetMethod("AddPrivateIncome", BindingFlags.Public | BindingFlags.Instance);
            var destMethod8 = typeof(RealCityEconomyManager).GetMethod("AddPrivateIncome", BindingFlags.Public | BindingFlags.Instance);
            state8 = RedirectionHelper.RedirectCalls(srcMethod8, destMethod8);

            var srcMethod9 = typeof(PrivateBuildingAI).GetMethod("SimulationStepActive", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            var destMethod9 = typeof(RealCityPrivateBuildingAI).GetMethod("CustomSimulationStepActive", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            state9 = RedirectionHelper.RedirectCalls(srcMethod9, destMethod9);

            var srcMethod10 = typeof(PassengerCarAI).GetMethod("ArriveAtDestination", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var destMethod10 = typeof(RealCityPassengerCarAI).GetMethod("CustomArriveAtDestination", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            state10 = RedirectionHelper.RedirectCalls(srcMethod10, destMethod10);

            var srcMethod11 = typeof(CargoTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var destMethod11 = typeof(RealCityCargoTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            state11 = RedirectionHelper.RedirectCalls(srcMethod11, destMethod11);

            var srcMethod12 = typeof(HumanAI).GetMethod("EnterVehicle", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType() }, null);
            var destMethod12 = typeof(RealCityHumanAI).GetMethod("CustomEnterVehicle", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType() }, null);
            state12 = RedirectionHelper.RedirectCalls(srcMethod12, destMethod12);

            var srcMethod13 = typeof(OfficeBuildingAI).GetMethod("GetOutgoingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            var destMethod13 = typeof(RealCityOfficeBuildingAI).GetMethod("GetOutgoingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            state13 = RedirectionHelper.RedirectCalls(srcMethod13, destMethod13);

            /*var srcMethod14 = typeof(ZoneManager).GetMethod("CalculateResidentialDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            var destMethod14 = typeof(RealCityZoneManager).GetMethod("CalculateResidentialDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            state14 = RedirectionHelper.RedirectCalls(srcMethod14, destMethod14);

            var srcMethod15 = typeof(ZoneManager).GetMethod("CalculateIncomingResidentDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            var destMethod15 = typeof(RealCityZoneManager).GetMethod("CalculateIncomingResidentDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            state15 = RedirectionHelper.RedirectCalls(srcMethod15, destMethod15);

            var srcMethod16 = typeof(ZoneManager).GetMethod("CalculateCommercialDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            var destMethod16 = typeof(RealCityZoneManager).GetMethod("CalculateCommercialDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            state16 = RedirectionHelper.RedirectCalls(srcMethod16, destMethod16);

            var srcMethod17 = typeof(ZoneManager).GetMethod("CalculateWorkplaceDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            var destMethod17 = typeof(RealCityZoneManager).GetMethod("CalculateWorkplaceDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            state17 = RedirectionHelper.RedirectCalls(srcMethod17, destMethod17);*/

            var srcMethod18 = typeof(CargoTruckAI).GetMethod("SetSource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(ushort) }, null);
            var destMethod18 = typeof(RealCityCargoTruckAI).GetMethod("SetSource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(ushort) }, null);
            state18 = RedirectionHelper.RedirectCalls(srcMethod18, destMethod18);

            var srcMethod19 = typeof(TaxiAI).GetMethod("UnloadPassengers", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(TransportPassengerData).MakeByRefType() }, null);
            var destMethod19 = typeof(RealCityTaxiAI).GetMethod("UnloadPassengers", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(TransportPassengerData).MakeByRefType() }, null);
            state19 = RedirectionHelper.RedirectCalls(srcMethod19, destMethod19);

            var srcMethod20 = typeof(ExtractingFacilityAI).GetMethod("GetResourceRate", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(EconomyManager.Resource) }, null);
            var destMethod20 = typeof(RealCityExtractingFacilityAI).GetMethod("GetResourceRate", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(EconomyManager.Resource) }, null);
            state20 = RedirectionHelper.RedirectCalls(srcMethod20, destMethod20);

            var srcMethod21 = typeof(ResidentAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(CitizenInstance.Frame).MakeByRefType(), typeof(bool) }, null);
            var destMethod21 = typeof(RealCityHumanAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(CitizenInstance.Frame).MakeByRefType(), typeof(bool) }, null);
            state21 = RedirectionHelper.RedirectCalls(srcMethod21, destMethod21);

            var srcMethod22 = typeof(CargoTruckAI).GetMethod("ArriveAtSource", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var destMethod22 = typeof(RealCityCargoTruckAI).GetMethod("ArriveAtSource", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            state22 = RedirectionHelper.RedirectCalls(srcMethod22, destMethod22);

            //TransferManager.TransferReason material, ref int count, ref int cargo, ref int capacity, ref int outside
            var srcMethod25 = typeof(CommonBuildingAI).GetMethod("CalculateGuestVehicles", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType() }, null);
            var destMethod25 = typeof(RealCityPrivateBuildingAI).GetMethod("CustomCalculateGuestVehicles", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType() }, null);
            state25 = RedirectionHelper.RedirectCalls(srcMethod25, destMethod25);


            //var srcMethod26 = typeof(CommercialBuildingAI).GetMethod("GetIncomingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            //var destMethod26 = typeof(pc_CommercialBuildingAI).GetMethod("GetIncomingTransferReason", BindingFlags.Public | BindingFlags.Instance);
            //state26 = RedirectionHelper.RedirectCalls(srcMethod26, destMethod26);

            // public override int CustomGetResourceRate(ushort buildingID, ref Building data, EconomyManager.Resource resource)
            var srcMethod27 = typeof(PlayerBuildingAI).GetMethod("GetResourceRate", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(EconomyManager.Resource) }, null);
            var destMethod27 = typeof(RealCityPlayerBuildingAI).GetMethod("GetResourceRate", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(EconomyManager.Resource) }, null);
            state27 = RedirectionHelper.RedirectCalls(srcMethod27, destMethod27);

            //var srcMethod28 = typeof(PlayerBuildingAI).GetMethod("GetProductionRate", BindingFlags.Public | BindingFlags.Static);
            //var destMethod28 = typeof(pc_PlayerBuildingAI).GetMethod("GetProductionRate_1", BindingFlags.Public | BindingFlags.Static);
            //state28 = RedirectionHelper.RedirectCalls(srcMethod28, destMethod28);

            //var srcMethod29 = typeof(TransferManager).GetMethod("AddIncomingOffer", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            //var destMethod29 = typeof(RealCityTransferManager).GetMethod("AddIncomingOffer", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            //state29 = RedirectionHelper.RedirectCalls(srcMethod29, destMethod29);

            var srcMethod30 = typeof(CommonBuildingAI).GetMethod("ReleaseBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            var destMethod30 = typeof(RealCityCommonBuildingAI).GetMethod("ReleaseBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            state30 = RedirectionHelper.RedirectCalls(srcMethod30, destMethod30);

            var srcMethod31 = typeof(EconomyManager).GetMethod("GetBudget", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ItemClass) }, null);
            var destMethod31 = typeof(RealCityEconomyManager).GetMethod("GetBudget", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ItemClass) }, null);
            state31 = RedirectionHelper.RedirectCalls(srcMethod31, destMethod31);

            var srcMethod32 = typeof(EconomyManager).GetMethod("AddResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass) }, null);
            var destMethod32 = typeof(RealCityEconomyManager).GetMethod("AddResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass) }, null);
            state32 = RedirectionHelper.RedirectCalls(srcMethod32, destMethod32);

            var srcMethod33 = typeof(CitizenManager).GetMethod("ReleaseCitizenImplementation", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(Citizen).MakeByRefType() }, null);
            var destMethod33 = typeof(RealCityCitizenManager).GetMethod("ReleaseCitizenImplementation", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(Citizen).MakeByRefType() }, null);
            state33 = RedirectionHelper.RedirectCalls(srcMethod33, destMethod33);

            var srcMethod36 = typeof(EconomyManager).GetMethod("AddResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass.Service), typeof(ItemClass.SubService), typeof(ItemClass.Level) }, null);
            var destMethod36 = typeof(RealCityEconomyManager).GetMethod("AddResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass.Service), typeof(ItemClass.SubService), typeof(ItemClass.Level) }, null);
            state36 = RedirectionHelper.RedirectCalls(srcMethod36, destMethod36);

            //var srcMethod34 = typeof(ResidentAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(Citizen).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            //var destMethod34 = typeof(pc_HumanAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(Citizen).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            //state34 = RedirectionHelper.RedirectCalls(srcMethod34, destMethod34);

            //var srcMethod35 = typeof(VehicleAI).GetMethod("CalculateTargetSpeed", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(float), typeof(float) }, null);
            //var destMethod35 = typeof(pc_VehicleAI).GetMethod("CalculateTargetSpeed_1", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(float), typeof(float) }, null);
            //state35 = RedirectionHelper.RedirectCalls(srcMethod35, destMethod35);

            //var srcMethod36 = typeof(OutsideConnectionAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            //var destMethod36 = typeof(pc_OutsideConnectionAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            //state36 = RedirectionHelper.RedirectCalls(srcMethod36, destMethod36);
            //var srcMethod36 = typeof(OutsideConnectionAI).GetMethod("GetProductionRate", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            //var destMethod36 = typeof(RealCityOutsideConnectionAI).GetMethod("GetProductionRate", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            //state36 = RedirectionHelper.RedirectCalls(srcMethod36, destMethod36);

            //public static void AddConnectionOffers(ushort buildingID, ref Building data, int productionRate, int cargoCapacity, int residentCapacity, int touristFactor0, int touristFactor1, int touristFactor2, TransferManager.TransferReason dummyTrafficReason, int dummyTrafficFactor)
            //var srcMethod37 = typeof(OutsideConnectionAI).GetMethod("AddConnectionOffers", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(TransferManager.TransferReason), typeof(int) }, null);
            //var destMethod37 = typeof(pc_OutsideConnectionAI).GetMethod("AddConnectionOffers", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(TransferManager.TransferReason), typeof(int) }, null);
            //state37 = RedirectionHelper.RedirectCalls(srcMethod37, srcMethod37);

            var srcMethod37 = typeof(OutsideConnectionAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            var destMethod37 = typeof(RealCityOutsideConnectionAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            state37 = RedirectionHelper.RedirectCalls(srcMethod37, destMethod37);

            var srcMethod38 = typeof(OutsideConnectionAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            var destMethod38 = typeof(RealCityOutsideConnectionAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            state38 = RedirectionHelper.RedirectCalls(srcMethod38, destMethod38);

            var srcMethod39 = typeof(GarbageTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var destMethod39 = typeof(RealCityGarbageTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            state39 = RedirectionHelper.RedirectCalls(srcMethod39, destMethod39);

            //public virtual void VisitorEnter(ushort buildingID, ref Building data, uint citizen)
            var srcMethod40 = typeof(BuildingAI).GetMethod("VisitorEnter", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(uint) }, null);
            var destMethod40 = typeof(RealCityHumanAI).GetMethod("CustomVisitorEnter", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(uint) }, null);
            state40 = RedirectionHelper.RedirectCalls(srcMethod40, destMethod40);

            //var srcMethod41 = typeof(CommercialBuildingAI).GetMethod("CreateBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            //var destMethod41 = typeof(RealCityCommercialBuildingAI).GetMethod("CreateBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            //state41 = RedirectionHelper.RedirectCalls(srcMethod41, destMethod41);

            //var srcMethod42 = typeof(IndustrialBuildingAI).GetMethod("CreateBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            //var destMethod42 = typeof(RealCityIndustrialBuildingAI).GetMethod("CreateBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            //state42 = RedirectionHelper.RedirectCalls(srcMethod42, destMethod42);

            var srcMethod43 = typeof(IndustryBuildingAI).GetMethod("GetResourcePrice", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            var destMethod43 = typeof(RealCityIndustryBuildingAI).GetMethod("CustomGetResourcePrice", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            state43 = RedirectionHelper.RedirectCalls(srcMethod43, destMethod43);

            var srcMethod44 = typeof(CitizenManager).GetMethod("ReleaseUnitCitizen", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(CitizenUnit).MakeByRefType(), typeof(uint) }, null);
            var destMethod44 = typeof(RealCityCitizenManager).GetMethod("ReleaseUnitCitizen", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(CitizenUnit).MakeByRefType(), typeof(uint) }, null);
            state44 = RedirectionHelper.RedirectCalls(srcMethod44, destMethod44);

            //var srcMethod45 = typeof(IndustryBuildingAI).GetMethod("ExchangeResource", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            //var destMethod45 = typeof(RealCityIndustryBuildingAI).GetMethod("ExchangeResource", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            //state45 = RedirectionHelper.RedirectCalls(srcMethod45, destMethod45);

            //public override void EnterBuildingSegment(ushort buildingID, ref Building data, ushort segmentID, byte offset, InstanceID itemID)

            var srcMethod46 = typeof(TollBoothAI).GetMethod("EnterBuildingSegment", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(ushort), typeof(byte),typeof(InstanceID) }, null);
            var destMethod46 = typeof(RealCityTollBooth).GetMethod("EnterBuildingSegment", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(ushort), typeof(byte), typeof(InstanceID) }, null);
            state46 = RedirectionHelper.RedirectCalls(srcMethod46, destMethod46);

            //var srcMethod47 = typeof(PassengerCarAI).GetMethod("EnterTollRoad", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(ushort), typeof(ushort), typeof(int) }, null);
            //var destMethod47 = typeof(RealCityPassengerCarAI).GetMethod("EnterTollRoad", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(ushort), typeof(ushort), typeof(int) }, null);
            //state47 = RedirectionHelper.RedirectCalls(srcMethod47, destMethod47);


            //public override void EnterParkArea(ushort instanceID, ref CitizenInstance citizenData, byte park, ushort gateID)

            var srcMethod48 = typeof(HumanAI).GetMethod("EnterParkArea", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(byte), typeof(ushort)}, null);
            var destMethod48 = typeof(RealCityHumanAI).GetMethod("EnterParkArea", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(byte), typeof(ushort)}, null);
            state48 = RedirectionHelper.RedirectCalls(srcMethod48, destMethod48);

            /*if (!RedirectionHelper.IsRedirected(srcMethod1, destMethod1))
            {
                DebugLog.LogToFileOnly("function1 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod2, destMethod2))
            {
                DebugLog.LogToFileOnly("function2 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod3, destMethod3))
            {
                DebugLog.LogToFileOnly("function3 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod4, destMethod4))
            {
                DebugLog.LogToFileOnly("function4 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod5, destMethod5))
            {
                DebugLog.LogToFileOnly("function5 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod7, destMethod7))
            {
                DebugLog.LogToFileOnly("function7 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod8, destMethod8))
            {
                DebugLog.LogToFileOnly("function8 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod9, destMethod9))
            {
                DebugLog.LogToFileOnly("function9 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod10, destMethod10))
            {
                DebugLog.LogToFileOnly("function10 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod11, destMethod11))
            {
                DebugLog.LogToFileOnly("function11 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod12, destMethod12))
            {
                DebugLog.LogToFileOnly("function12 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod13, destMethod13))
            {
                DebugLog.LogToFileOnly("function13 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod14, destMethod14))
            {
                DebugLog.LogToFileOnly("function14 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod15, destMethod15))
            {
                DebugLog.LogToFileOnly("function15 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod16, destMethod16))
            {
                DebugLog.LogToFileOnly("function16 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod17, destMethod17))
            {
                DebugLog.LogToFileOnly("function17 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod18, destMethod18))
            {
                DebugLog.LogToFileOnly("function18 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod19, destMethod19))
            {
                DebugLog.LogToFileOnly("function19 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod21, destMethod21))
            {
                DebugLog.LogToFileOnly("function21 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod22, destMethod22))
            {
                DebugLog.LogToFileOnly("function22 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod25, destMethod25))
            {
                DebugLog.LogToFileOnly("function25 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod30, destMethod30))
            {
                DebugLog.LogToFileOnly("function30 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod33, destMethod33))
            {
                DebugLog.LogToFileOnly("function33 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod36, destMethod36))
            {
                DebugLog.LogToFileOnly("function36 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod37, destMethod37))
            {
                DebugLog.LogToFileOnly("function37 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod38, destMethod38))
            {
                DebugLog.LogToFileOnly("function38 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod39, destMethod39))
            {
                DebugLog.LogToFileOnly("function39 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod40, destMethod40))
            {
                DebugLog.LogToFileOnly("function40 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod41, destMethod41))
            {
                DebugLog.LogToFileOnly("function41 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod42, destMethod42))
            {
                DebugLog.LogToFileOnly("function42 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod43, destMethod43))
            {
                DebugLog.LogToFileOnly("function43 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod44, destMethod44))
            {
                DebugLog.LogToFileOnly("function44 is not available");
            }
            if (!RedirectionHelper.IsRedirected(srcMethod46, destMethod46))
            {
                DebugLog.LogToFileOnly("function46 is not available");
            }
            //if (!RedirectionHelper.IsRedirected(srcMethod47, destMethod47))
            //{
            //     DebugLog.LogToFileOnly("function47 is not available");
            //}

            if (!RedirectionHelper.IsRedirected(srcMethod48, destMethod48))
            {
                DebugLog.LogToFileOnly("function48 is not available");
            }*/
        }

        public void RevertDetour()
        {
            var srcMethod1 = typeof(TransferManager).GetMethod("StartTransfer", BindingFlags.NonPublic | BindingFlags.Instance);
            var srcMethod2 = typeof(IndustrialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            var srcMethod3 = typeof(IndustrialExtractorAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            var srcMethod4 = typeof(CommercialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            var srcMethod5 = typeof(ResidentAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(CitizenUnit).MakeByRefType() }, null);
            var srcMethod6 = typeof(ProcessingFacilityAI).GetMethod("GetResourceRate", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(EconomyManager.Resource) }, null);
            var srcMethod7 = typeof(EconomyManager).GetMethod("FetchResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass) }, null);
            var srcMethod8 = typeof(EconomyManager).GetMethod("AddPrivateIncome", BindingFlags.Public | BindingFlags.Instance);
            var srcMethod9 = typeof(PrivateBuildingAI).GetMethod("SimulationStepActive", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            var srcMethod10 = typeof(PassengerCarAI).GetMethod("ArriveAtDestination", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var srcMethod11 = typeof(CargoTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var srcMethod12 = typeof(HumanAI).GetMethod("EnterVehicle", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType() }, null);
            var srcMethod13 = typeof(OfficeBuildingAI).GetMethod("GetOutgoingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            //var srcMethod14 = typeof(ZoneManager).GetMethod("CalculateResidentialDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            //var srcMethod15 = typeof(ZoneManager).GetMethod("CalculateIncomingResidentDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            //var srcMethod16 = typeof(ZoneManager).GetMethod("CalculateCommercialDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            //var srcMethod17 = typeof(ZoneManager).GetMethod("CalculateWorkplaceDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            var srcMethod18 = typeof(CargoTruckAI).GetMethod("SetSource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(ushort) }, null);
            var srcMethod19 = typeof(TaxiAI).GetMethod("UnloadPassengers", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(TransportPassengerData).MakeByRefType() }, null);
            var srcMethod20 = typeof(ExtractingFacilityAI).GetMethod("GetResourceRate", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(EconomyManager.Resource) }, null);
            var srcMethod21 = typeof(ResidentAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(CitizenInstance.Frame).MakeByRefType(), typeof(bool) }, null);
            var srcMethod22 = typeof(CargoTruckAI).GetMethod("ArriveAtSource", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var srcMethod25 = typeof(CommonBuildingAI).GetMethod("CalculateGuestVehicles", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType() }, null);

            //protected override void SimulationStepActive(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
            //var srcMethod26 = typeof(CommercialBuildingAI).GetMethod("GetIncomingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            //var srcMethod28 = typeof(PlayerBuildingAI).GetMethod("GetProductionRate", BindingFlags.Public | BindingFlags.Static);
            var srcMethod27 = typeof(PlayerBuildingAI).GetMethod("GetResourceRate", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(EconomyManager.Resource) }, null);
            //var srcMethod29 = typeof(TransferManager).GetMethod("AddIncomingOffer", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            var srcMethod30 = typeof(CommonBuildingAI).GetMethod("ReleaseBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            var srcMethod31 = typeof(EconomyManager).GetMethod("GetBudget", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ItemClass) }, null);
            var srcMethod32 = typeof(EconomyManager).GetMethod("AddResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass) }, null);
            var srcMethod33 = typeof(CitizenManager).GetMethod("ReleaseCitizenImplementation", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(Citizen).MakeByRefType() }, null);
            //var srcMethod34 = typeof(ResidentAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(Citizen).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            //var srcMethod35 = typeof(VehicleAI).GetMethod("CalculateTargetSpeed", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(float), typeof(float) }, null);
            var srcMethod36 = typeof(EconomyManager).GetMethod("AddResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass.Service), typeof(ItemClass.SubService), typeof(ItemClass.Level) }, null);
            var srcMethod37 = typeof(OutsideConnectionAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            var srcMethod38 = typeof(OutsideConnectionAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            var srcMethod39 = typeof(GarbageTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var srcMethod40 = typeof(BuildingAI).GetMethod("VisitorEnter", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(uint) }, null);
            //var srcMethod41 = typeof(CommercialBuildingAI).GetMethod("CreateBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            //var srcMethod42 = typeof(IndustrialBuildingAI).GetMethod("CreateBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            var srcMethod43 = typeof(IndustryBuildingAI).GetMethod("GetResourcePrice", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            var srcMethod44 = typeof(CitizenManager).GetMethod("ReleaseUnitCitizen", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(CitizenUnit).MakeByRefType(), typeof(uint) }, null);
            //var srcMethod45 = typeof(IndustryBuildingAI).GetMethod("ExchangeResource", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            var srcMethod46 = typeof(TollBoothAI).GetMethod("EnterBuildingSegment", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(ushort), typeof(byte), typeof(InstanceID) }, null);
            //var srcMethod47 = typeof(PassengerCarAI).GetMethod("EnterTollRoad", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(ushort), typeof(ushort), typeof(int) }, null);
            var srcMethod48 = typeof(HumanAI).GetMethod("EnterParkArea", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(byte), typeof(ushort) }, null);


            RedirectionHelper.RevertRedirect(srcMethod1, state1);
            RedirectionHelper.RevertRedirect(srcMethod2, state2);
            RedirectionHelper.RevertRedirect(srcMethod3, state3);
            RedirectionHelper.RevertRedirect(srcMethod4, state4);
            RedirectionHelper.RevertRedirect(srcMethod5, state5);
            RedirectionHelper.RevertRedirect(srcMethod6, state6);
            RedirectionHelper.RevertRedirect(srcMethod7, state7);
            RedirectionHelper.RevertRedirect(srcMethod8, state8);
            RedirectionHelper.RevertRedirect(srcMethod9, state9);
            RedirectionHelper.RevertRedirect(srcMethod10, state10);
            RedirectionHelper.RevertRedirect(srcMethod11, state11);
            RedirectionHelper.RevertRedirect(srcMethod12, state12);
            RedirectionHelper.RevertRedirect(srcMethod13, state13);
            //RedirectionHelper.RevertRedirect(srcMethod14, state14);
            //RedirectionHelper.RevertRedirect(srcMethod15, state15);
            //RedirectionHelper.RevertRedirect(srcMethod16, state16);
            //RedirectionHelper.RevertRedirect(srcMethod17, state17);
            RedirectionHelper.RevertRedirect(srcMethod18, state18);
            RedirectionHelper.RevertRedirect(srcMethod19, state19);
            RedirectionHelper.RevertRedirect(srcMethod20, state20);
            RedirectionHelper.RevertRedirect(srcMethod21, state21);
            RedirectionHelper.RevertRedirect(srcMethod22, state22);
            RedirectionHelper.RevertRedirect(srcMethod25, state25);
            //RedirectionHelper.RevertRedirect(srcMethod26, state26);
            RedirectionHelper.RevertRedirect(srcMethod27, state27);
            //RedirectionHelper.RevertRedirect(srcMethod28, state28);
            //RedirectionHelper.RevertRedirect(srcMethod29, state29);
            RedirectionHelper.RevertRedirect(srcMethod30, state30);
            RedirectionHelper.RevertRedirect(srcMethod31, state31);
            RedirectionHelper.RevertRedirect(srcMethod32, state32);
            RedirectionHelper.RevertRedirect(srcMethod33, state33);
            //RedirectionHelper.RevertRedirect(srcMethod34, state34);
            //RedirectionHelper.RevertRedirect(srcMethod35, state35);
            RedirectionHelper.RevertRedirect(srcMethod36, state36);
            RedirectionHelper.RevertRedirect(srcMethod37, state37);
            RedirectionHelper.RevertRedirect(srcMethod38, state38);
            RedirectionHelper.RevertRedirect(srcMethod39, state39);
            RedirectionHelper.RevertRedirect(srcMethod40, state40);
            //RedirectionHelper.RevertRedirect(srcMethod41, state41);
            //RedirectionHelper.RevertRedirect(srcMethod42, state42);
            RedirectionHelper.RevertRedirect(srcMethod43, state43);
            RedirectionHelper.RevertRedirect(srcMethod44, state44);
            //RedirectionHelper.RevertRedirect(srcMethod45, state45);
            RedirectionHelper.RevertRedirect(srcMethod46, state46);
            //RedirectionHelper.RevertRedirect(srcMethod47, state47);
            RedirectionHelper.RevertRedirect(srcMethod48, state48);
        }

        private bool CheckFuelAlarmIsLoaded()
        {
            return this.Check3rdPartyModLoaded("FuelAlarm", true);
        }
    }
}
