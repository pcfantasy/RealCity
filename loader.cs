using ColossalFramework.UI;
using ICities;
using UnityEngine;
using System.IO;
using ColossalFramework;
using System.Reflection;
using System;
using System.Linq;
using ColossalFramework.Math;

namespace RealCity
{
    public class Loader : LoadingExtensionBase
    {
        public static UIView parentGuiView;

        public static UIPanel buildingInfo;

        public static UIPanel playerbuildingInfo;

        public static UIPanel HumanInfo;

        public static MoreeconomicUI guiPanel;

        public static RealCityUI guiPanel1;

        public static BuildingUI guiPanel2;

        public static HumanUI guiPanel3;

        public static PlayerBuildingUI guiPanel4;

        public static GameObject buildingWindowGameObject;

        public static GameObject PlayerbuildingWindowGameObject;

        public static GameObject HumanWindowGameObject;

        public static LoadMode CurrentLoadMode;
        public static bool isGuiRunning = false;

        public static EcnomicButton EcMenuPanel;
        public static CityButton RcMenuPanel;

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
        public static RedirectCallsState state28;
        //public static RedirectCallsState state29;
        public static RedirectCallsState state30;
        public static RedirectCallsState state31;
        public static RedirectCallsState state32;
        public static RedirectCallsState state33;
        public static RedirectCallsState state34;
        public static RedirectCallsState state35;
        public static RedirectCallsState state36;
        public static RedirectCallsState state37;
        public static RedirectCallsState state38;
        public static RedirectCallsState state39;
        public static RedirectCallsState state40;
        public static RedirectCallsState state41;
        public static RedirectCallsState state42;
        public static RedirectCallsState state43;
        public static RedirectCallsState state44;
        public static RedirectCallsState state45;
        public static RedirectCallsState state46;
        //public static RedirectCallsState state47;
        public static RedirectCallsState state48;
        public static RedirectCallsState state49;
        public static RedirectCallsState state50;
        public static RedirectCallsState state51;
        public static RedirectCallsState state52;
        public static RedirectCallsState state53;
        public static RedirectCallsState state54;
        public static RedirectCallsState state55;
        public static RedirectCallsState state56;
        public static RedirectCallsState state57;
        public static RedirectCallsState state58;

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
                    detour();
                    DebugLog.LogToFileOnly("OnLevelLoaded");
                    if (mode == LoadMode.NewGame)
                    {
                        init_data();
                    }
                    //DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, "setup_gui now");
                }
            }
        }

        public void init_data()
        {
            comm_data.data_init();
            pc_EconomyManager.data_init();
        }

        public override void OnLevelUnloading()
        {
            if (Loader.CurrentLoadMode == LoadMode.LoadGame || Loader.CurrentLoadMode == LoadMode.NewGame)
            {
                if (RealCity.IsEnabled & Loader.isGuiRunning)
                {
                    RemoveGui();
                }
                if (RealCity.IsEnabled)
                {
                    revert_detour();
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
            //CTRL+M debug UI
            if (Loader.guiPanel == null)
            {
                Loader.guiPanel = (MoreeconomicUI)Loader.parentGuiView.AddUIComponent(typeof(MoreeconomicUI));
            }
            //CRTL+R game UI
            if (Loader.guiPanel1 == null)
            {
                Loader.guiPanel1 = (RealCityUI)Loader.parentGuiView.AddUIComponent(typeof(RealCityUI));
            }

            //building UI
            //if (Loader.guiPanel2 == null)
            //{
            //    Loader.guiPanel2 = (BuildingUI)Loader.parentGuiView.AddUIComponent(typeof(BuildingUI));
            //}
            SetupBuidingGui();
            SetupHumanGui();
            SetupPlayerBuidingGui();
            SetupEcnomicButton();
            SetupCityButton();

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
            guiPanel4.size = new Vector3(playerbuildingInfo.size.x, HumanInfo.size.y);
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
                RcMenuPanel = (parentGuiView.AddUIComponent(typeof(CityButton)) as CityButton);
            }
            RcMenuPanel.Show();
        }


        public static void buildingInfo_eventVisibilityChanged(UIComponent component, bool value)
        {
            guiPanel2.isEnabled = value;
            if (value)
            {
                Loader.guiPanel2.transform.parent = Loader.buildingInfo.transform;
                Loader.guiPanel2.size = new Vector3(Loader.buildingInfo.size.x, Loader.buildingInfo.size.y);
                Loader.guiPanel2.baseBuildingWindow = Loader.buildingInfo.gameObject.transform.GetComponentInChildren<ZonedBuildingWorldInfoPanel>();
                Loader.guiPanel2.position = new Vector3(Loader.buildingInfo.size.x, Loader.buildingInfo.size.y);
                //DebugLog.LogToFileOnly("select building found!!!!!:\n");
                //comm_data.current_buildingid = 0;
                BuildingUI.refesh_once = true;
                guiPanel2.Show();
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
                PlayerBuildingUI.refesh_once = true;
                guiPanel4.Show();
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
                HumanUI.refesh_once = true;
                guiPanel3.Show();
            }
            else
            {
                guiPanel3.Hide();
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
                Loader.guiPanel = null;
                Loader.guiPanel1 = null;
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
            if (PlayerbuildingWindowGameObject != null)
            {
                UnityEngine.Object.Destroy(PlayerbuildingWindowGameObject);
            }
        }


        public void detour()
        {
            var srcMethod1 = typeof(TransferManager).GetMethod("StartTransfer", BindingFlags.NonPublic | BindingFlags.Instance);
            var destMethod1 = typeof(pc_TransferManager).GetMethod("StartTransfer", BindingFlags.NonPublic | BindingFlags.Instance);
            state1 = RedirectionHelper.RedirectCalls(srcMethod1, destMethod1);

            var srcMethod2 = typeof(IndustrialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            var destMethod2 = typeof(pc_IndustrialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            state2 = RedirectionHelper.RedirectCalls(srcMethod2, destMethod2);

            var srcMethod3 = typeof(IndustrialExtractorAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            var destMethod3 = typeof(pc_IndustrialExtractorAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            state3 = RedirectionHelper.RedirectCalls(srcMethod3, destMethod3);

            var srcMethod4 = typeof(CommercialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            var destMethod4 = typeof(pc_CommercialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            state4 = RedirectionHelper.RedirectCalls(srcMethod4, destMethod4);

            var srcMethod5 = typeof(ResidentAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(CitizenUnit).MakeByRefType() }, null);
            var destMethod5 = typeof(pc_ResidentAI).GetMethod("SimulationStep_1", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(CitizenUnit).MakeByRefType() }, null);
            state5 = RedirectionHelper.RedirectCalls(srcMethod5, destMethod5);

            var srcMethod6 = typeof(HumanAI).GetMethod("ArriveAtDestination", BindingFlags.NonPublic | BindingFlags.Instance);
            var destMethod6 = typeof(pc_HumanAI).GetMethod("ArriveAtDestination_1", BindingFlags.NonPublic | BindingFlags.Instance);
            state6 = RedirectionHelper.RedirectCalls(srcMethod6, destMethod6);

            var srcMethod7 = typeof(EconomyManager).GetMethod("FetchResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass) }, null);
            var destMethod7 = typeof(pc_EconomyManager).GetMethod("FetchResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass) }, null);
            state7 = RedirectionHelper.RedirectCalls(srcMethod7, destMethod7);

            var srcMethod8 = typeof(EconomyManager).GetMethod("AddPrivateIncome", BindingFlags.Public | BindingFlags.Instance);
            var destMethod8 = typeof(pc_EconomyManager).GetMethod("AddPrivateIncome", BindingFlags.Public | BindingFlags.Instance);
            state8 = RedirectionHelper.RedirectCalls(srcMethod8, destMethod8);

            var srcMethod9 = typeof(PrivateBuildingAI).GetMethod("SimulationStepActive", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            var destMethod9 = typeof(pc_PrivateBuildingAI).GetMethod("SimulationStepActive_1", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            state9 = RedirectionHelper.RedirectCalls(srcMethod9, destMethod9);

            var srcMethod10 = typeof(PassengerCarAI).GetMethod("ArriveAtDestination", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var destMethod10 = typeof(pc_PassengerCarAI).GetMethod("ArriveAtDestination_1", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            state10 = RedirectionHelper.RedirectCalls(srcMethod10, destMethod10);

            //var srcMethod11 = typeof(IndustrialExtractorAI).GetMethod("GetLevelUpInfo", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(float).MakeByRefType() }, null);
            //var destMethod11 = typeof(pc_IndustrialExtractorAI).GetMethod("GetLevelUpInfo", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(float).MakeByRefType() }, null);
            //state11 = RedirectionHelper.RedirectCalls(srcMethod11, destMethod11);

            //var srcMethod12 = typeof(CommercialBuildingAI).GetMethod("GetLevelUpInfo", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(float).MakeByRefType() }, null);
            //var destMethod12 = typeof(pc_CommercialBuildingAI).GetMethod("GetLevelUpInfo", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(float).MakeByRefType() }, null);
            //state12 = RedirectionHelper.RedirectCalls(srcMethod12, destMethod12);

            //var srcMethod13 = typeof(IndustrialBuildingAI).GetMethod("GetLevelUpInfo", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(float).MakeByRefType() }, null);
            //var destMethod13 = typeof(pc_IndustrialBuildingAI).GetMethod("GetLevelUpInfo", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(float).MakeByRefType() }, null);
            //state13 = RedirectionHelper.RedirectCalls(srcMethod13, destMethod13);

            var srcMethod14 = typeof(CargoTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var destMethod14 = typeof(pc_CargoTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            state14 = RedirectionHelper.RedirectCalls(srcMethod14, destMethod14);

            var srcMethod15 = typeof(HumanAI).GetMethod("EnterVehicle", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType() }, null);
            var destMethod15 = typeof(pc_HumanAI).GetMethod("EnterVehicle_1", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType() }, null);
            state15 = RedirectionHelper.RedirectCalls(srcMethod15, destMethod15);

            //var srcMethod16 = typeof(ResidentAI).GetMethod("StartPathFind", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType() }, null);
            //var destMethod16 = typeof(pc_ResidentAI_1).GetMethod("StartPathFind", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType() }, null);
            //state16 = RedirectionHelper.RedirectCalls(srcMethod16, destMethod16);

            var srcMethod17 = typeof(OfficeBuildingAI).GetMethod("GetOutgoingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            var destMethod17 = typeof(pc_OfficeBuildingAI).GetMethod("GetOutgoingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            state17 = RedirectionHelper.RedirectCalls(srcMethod17, destMethod17);

            var srcMethod18 = typeof(ZoneManager).GetMethod("CalculateResidentialDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            var destMethod18 = typeof(pc_ZoneManager).GetMethod("CalculateResidentialDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            state18 = RedirectionHelper.RedirectCalls(srcMethod18, destMethod18);

            var srcMethod19 = typeof(ZoneManager).GetMethod("CalculateIncomingResidentDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            var destMethod19 = typeof(pc_ZoneManager).GetMethod("CalculateIncomingResidentDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            state19 = RedirectionHelper.RedirectCalls(srcMethod19, destMethod19);

            var srcMethod20 = typeof(ZoneManager).GetMethod("CalculateCommercialDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            var destMethod20 = typeof(pc_ZoneManager).GetMethod("CalculateCommercialDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            state20 = RedirectionHelper.RedirectCalls(srcMethod20, destMethod20);

            var srcMethod21 = typeof(ZoneManager).GetMethod("CalculateWorkplaceDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            var destMethod21 = typeof(pc_ZoneManager).GetMethod("CalculateWorkplaceDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            state21 = RedirectionHelper.RedirectCalls(srcMethod21, destMethod21);

            var srcMethod22 = typeof(CargoTruckAI).GetMethod("SetSource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(ushort) }, null);
            var destMethod22 = typeof(pc_CargoTruckAI).GetMethod("SetSource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(ushort) }, null);
            state22 = RedirectionHelper.RedirectCalls(srcMethod22, destMethod22);

            var srcMethod23 = typeof(OutsideConnectionAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            var destMethod23 = typeof(pc_OutsideConnectionAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            state23 = RedirectionHelper.RedirectCalls(srcMethod23, destMethod23);

            var srcMethod24 = typeof(OutsideConnectionAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            var destMethod24 = typeof(pc_OutsideConnectionAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            state24 = RedirectionHelper.RedirectCalls(srcMethod24, destMethod24);

            var srcMethod25 = typeof(OutsideConnectionAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            var destMethod25 = typeof(pc_OutsideConnectionAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            state25 = RedirectionHelper.RedirectCalls(srcMethod25, destMethod25);

            var srcMethod26 = typeof(TaxiAI).GetMethod("UnloadPassengers", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(TransportPassengerData).MakeByRefType() }, null);
            var destMethod26 = typeof(pc_TaxiAI).GetMethod("UnloadPassengers", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(TransportPassengerData).MakeByRefType() }, null);
            state26 = RedirectionHelper.RedirectCalls(srcMethod26, destMethod26);

            var srcMethod27 = typeof(HearseAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var destMethod27 = typeof(pc_HearseAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            state27 = RedirectionHelper.RedirectCalls(srcMethod27, destMethod27);

            var srcMethod28 = typeof(GarbageTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var destMethod28 = typeof(pc_GarbageTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            state28 = RedirectionHelper.RedirectCalls(srcMethod28, destMethod28);

            //var srcMethod29 = typeof(BuildingWorldInfoPanel).GetMethod("GetName", BindingFlags.NonPublic | BindingFlags.Instance);
            //var destMethod29 = typeof(pc_BuildingWorldInfoPanel).GetMethod("GetName", BindingFlags.NonPublic | BindingFlags.Instance);
            //state29 = RedirectionHelper.RedirectCalls(srcMethod29, destMethod29);

            var srcMethod30 = typeof(CitizenManager).GetMethod("CreateUnits", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint).MakeByRefType(), typeof(Randomizer).MakeByRefType(), typeof(ushort), typeof(ushort), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }, null);
            var destMethod30 = typeof(pc_CitizenManager).GetMethod("CreateUnits_1", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint).MakeByRefType(), typeof(Randomizer).MakeByRefType(), typeof(ushort), typeof(ushort), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }, null);
            state30 = RedirectionHelper.RedirectCalls(srcMethod30, destMethod30);

            // BuildingManager
            //public ushort FindBuilding(Vector3 pos, float maxDistance, ItemClass.Service service, ItemClass.SubService subService, Building.Flags flagsRequired, Building.Flags flagsForbidden)
            //public override void SimulationStep(ushort instanceID, ref CitizenInstance citizenData, ref CitizenInstance.Frame frameData, bool lodPhysics)
            var srcMethod31 = typeof(ResidentAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(CitizenInstance.Frame).MakeByRefType(), typeof(bool) }, null);
            var destMethod31 = typeof(pc_ResidentAI_1).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(CitizenInstance.Frame).MakeByRefType(), typeof(bool) }, null);
            state31 = RedirectionHelper.RedirectCalls(srcMethod31, destMethod31);

            //public override void SetTarget(ushort vehicleID, ref Vehicle data, ushort targetBuilding)
            //var srcMethod32 = typeof(CargoTruckAI).GetMethod("SetTarget", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(ushort) }, null);
            //var destMethod32 = typeof(pc_CargoTruckAI).GetMethod("SetTarget", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(ushort) }, null);
            //state32 = RedirectionHelper.RedirectCalls(srcMethod32, destMethod32);

            var srcMethod33 = typeof(PoliceCarAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var destMethod33 = typeof(pc_PoliceCarAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            state33 = RedirectionHelper.RedirectCalls(srcMethod33, destMethod33);

            var srcMethod34 = typeof(FireTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var destMethod34 = typeof(pc_FireTruckAI).GetMethod("ArriveAtTarget", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            state34 = RedirectionHelper.RedirectCalls(srcMethod34, destMethod34);

            var srcMethod35 = typeof(AmbulanceAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var destMethod35 = typeof(pc_AmbulanceAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            state35 = RedirectionHelper.RedirectCalls(srcMethod35, destMethod35);

            var srcMethod36 = typeof(FireTruckAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance , null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            var destMethod36 = typeof(pc_FireTruckAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            state36 = RedirectionHelper.RedirectCalls(srcMethod36, destMethod36);

            var srcMethod37 = typeof(AmbulanceAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            var destMethod37 = typeof(pc_AmbulanceAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            state37 = RedirectionHelper.RedirectCalls(srcMethod37, destMethod37);

            var srcMethod38 = typeof(MaintenanceTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var destMethod38 = typeof(pc_MaintenanceTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            state38 = RedirectionHelper.RedirectCalls(srcMethod38, destMethod38);

            var srcMethod39 = typeof(MaintenanceTruckAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            var destMethod39 = typeof(pc_MaintenanceTruckAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            state39 = RedirectionHelper.RedirectCalls(srcMethod39, destMethod39);

            var srcMethod40 = typeof(MaintenanceTruckAI).GetMethod("CheckTargetSegment", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var destMethod40 = typeof(pc_MaintenanceTruckAI).GetMethod("CheckTargetSegment", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            state40 = RedirectionHelper.RedirectCalls(srcMethod40, destMethod40);

            var srcMethod41 = typeof(IndustrialBuildingAI).GetMethod("GetIncomingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            var destMethod41 = typeof(pc_IndustrialBuildingAI).GetMethod("GetIncomingTransferReason", BindingFlags.Public | BindingFlags.Instance);
            state41 = RedirectionHelper.RedirectCalls(srcMethod41, destMethod41);

            var srcMethod42 = typeof(IndustrialBuildingAI).GetMethod("BuildingDeactivated", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            var destMethod42 = typeof(pc_IndustrialBuildingAI).GetMethod("BuildingDeactivated", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            state42 = RedirectionHelper.RedirectCalls(srcMethod42, destMethod42);


            //TransferManager.TransferReason material, ref int count, ref int cargo, ref int capacity, ref int outside
            var srcMethod43 = typeof(CommonBuildingAI).GetMethod("CalculateGuestVehicles", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType(), typeof(int).MakeByRefType() , typeof(int).MakeByRefType() , typeof(int).MakeByRefType() }, null);
            var destMethod43 = typeof(pc_PrivateBuildingAI).GetMethod("CalculateGuestVehicles_1", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType() }, null);
            state43 = RedirectionHelper.RedirectCalls(srcMethod43, destMethod43);

            //var srcMethod44 = typeof(IndustrialBuildingAI).GetMethod("GetConsumptionDivider", BindingFlags.NonPublic | BindingFlags.Instance);
            //var destMethod44 = typeof(pc_IndustrialBuildingAI).GetMethod("GetConsumptionDivider", BindingFlags.NonPublic | BindingFlags.Instance);
            //state44 = RedirectionHelper.RedirectCalls(srcMethod44, destMethod44);

            var srcMethod44 = typeof(IndustrialBuildingAI).GetMethod("SimulationStepActive", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            var destMethod44 = typeof(pc_IndustrialBuildingAI).GetMethod("SimulationStepActive", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            state44 = RedirectionHelper.RedirectCalls(srcMethod44, destMethod44);

            var srcMethod45 = typeof(PlayerBuildingAI).GetMethod("GetMaintenanceCost", BindingFlags.Public | BindingFlags.Instance);
            var destMethod45 = typeof(pc_PlayerBuildingAI).GetMethod("GetMaintenanceCost", BindingFlags.Public | BindingFlags.Instance);
            state45 = RedirectionHelper.RedirectCalls(srcMethod45, destMethod45);

            var srcMethod46 = typeof(CommercialBuildingAI).GetMethod("GetIncomingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            var destMethod46 = typeof(pc_CommercialBuildingAI).GetMethod("GetIncomingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            state46 = RedirectionHelper.RedirectCalls(srcMethod46, destMethod46);

            //var srcMethod47 = typeof(PoliceCarAI).GetMethod("ArriveAtSource", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            //var destMethod47 = typeof(pc_PoliceCarAI).GetMethod("ArriveAtSource", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            //state47 = RedirectionHelper.RedirectCalls(srcMethod47, destMethod47);

            var srcMethod48 = typeof(ResidentAI).GetMethod("SetTarget", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(ushort) }, null);
            var destMethod48 = typeof(pc_ResidentAI).GetMethod("SetTarget", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(ushort) }, null);
            state48 = RedirectionHelper.RedirectCalls(srcMethod48, destMethod48);

            //var srcMethod49 = typeof(BuildingAI).GetMethod("CheckUnlocking", BindingFlags.Public | BindingFlags.Instance);
            //var destMethod49 = typeof(pc_BuildingAI).GetMethod("CheckUnlocking_1", BindingFlags.Public | BindingFlags.Instance);
            //state49 = RedirectionHelper.RedirectCalls(srcMethod49, destMethod49);

            var srcMethod50 = typeof(PlayerBuildingAI).GetMethod("GetProductionRate", BindingFlags.Public | BindingFlags.Static);
            var destMethod50 = typeof(pc_PlayerBuildingAI).GetMethod("GetProductionRate_1", BindingFlags.Public | BindingFlags.Static);
            state50 = RedirectionHelper.RedirectCalls(srcMethod50, destMethod50);

            /*var srcMethod51 = typeof(IndustrialBuildingAI).GetMethod("MaxIncomingLoadSize", BindingFlags.NonPublic | BindingFlags.Instance);
            var destMethod51 = typeof(pc_IndustrialBuildingAI).GetMethod("MaxIncomingLoadSize", BindingFlags.NonPublic | BindingFlags.Instance);
            state51 = RedirectionHelper.RedirectCalls(srcMethod51, destMethod51);

            var srcMethod52 = typeof(CommercialBuildingAI).GetMethod("MaxIncomingLoadSize", BindingFlags.NonPublic | BindingFlags.Instance);
            var destMethod52 = typeof(pc_CommercialBuildingAI).GetMethod("MaxIncomingLoadSize", BindingFlags.NonPublic | BindingFlags.Instance);
            state52 = RedirectionHelper.RedirectCalls(srcMethod52, destMethod52);

            var srcMethod53 = typeof(CommercialBuildingAI).GetMethod("CreateBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            var destMethod53 = typeof(pc_CommercialBuildingAI).GetMethod("CreateBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            state53 = RedirectionHelper.RedirectCalls(srcMethod53, destMethod53);

            var srcMethod54 = typeof(IndustrialBuildingAI).GetMethod("CreateBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            var destMethod54 = typeof(pc_IndustrialBuildingAI).GetMethod("CreateBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            state54 = RedirectionHelper.RedirectCalls(srcMethod54, destMethod54);*/

            var srcMethod55 = typeof(TransferManager).GetMethod("AddOutgoingOffer", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            var destMethod55 = typeof(pc_TransferManager).GetMethod("AddOutgoingOffer", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            state55 = RedirectionHelper.RedirectCalls(srcMethod55, destMethod55);

            var srcMethod56 = typeof(PrivateBuildingAI).GetMethod("ReleaseBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            var destMethod56 = typeof(pc_PrivateBuildingAI).GetMethod("ReleaseBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            state56 = RedirectionHelper.RedirectCalls(srcMethod56, destMethod56);

            var srcMethod57 = typeof(EconomyManager).GetMethod("GetBudget", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ItemClass) }, null);
            var destMethod57 = typeof(pc_EconomyManager).GetMethod("GetBudget", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ItemClass) }, null);
            state57 = RedirectionHelper.RedirectCalls(srcMethod57, destMethod57);

            var srcMethod58 = typeof(EconomyManager).GetMethod("AddResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass) }, null);
            var destMethod58 = typeof(pc_EconomyManager).GetMethod("AddResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass) }, null);
            state58 = RedirectionHelper.RedirectCalls(srcMethod58, destMethod58);
        }

        public void revert_detour()
        {
            var srcMethod1 = typeof(TransferManager).GetMethod("StartTransfer", BindingFlags.NonPublic | BindingFlags.Instance);
            var srcMethod2 = typeof(IndustrialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            var srcMethod3 = typeof(IndustrialExtractorAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            var srcMethod4 = typeof(CommercialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            var srcMethod5 = typeof(ResidentAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(CitizenUnit).MakeByRefType() }, null);
            var srcMethod6 = typeof(HumanAI).GetMethod("ArriveAtDestination", BindingFlags.NonPublic | BindingFlags.Instance);
            var srcMethod7 = typeof(EconomyManager).GetMethod("FetchResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass) }, null);
            var srcMethod8 = typeof(EconomyManager).GetMethod("AddPrivateIncome", BindingFlags.Public | BindingFlags.Instance);
            var srcMethod9 = typeof(PrivateBuildingAI).GetMethod("SimulationStepActive", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            var srcMethod10 = typeof(PassengerCarAI).GetMethod("ArriveAtDestination", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            //var srcMethod11 = typeof(IndustrialExtractorAI).GetMethod("GetLevelUpInfo", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(float).MakeByRefType() }, null);
            //var srcMethod12 = typeof(CommercialBuildingAI).GetMethod("GetLevelUpInfo", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(float).MakeByRefType() }, null);
            //var srcMethod13 = typeof(IndustrialBuildingAI).GetMethod("GetLevelUpInfo", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(float).MakeByRefType() }, null);
            var srcMethod14 = typeof(CargoTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var srcMethod15 = typeof(HumanAI).GetMethod("EnterVehicle", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType() }, null);
            //var srcMethod16 = typeof(ResidentAI).GetMethod("StartPathFind", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType() }, null);
            var srcMethod17 = typeof(OfficeBuildingAI).GetMethod("GetOutgoingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            var srcMethod18 = typeof(ZoneManager).GetMethod("CalculateResidentialDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            var srcMethod19 = typeof(ZoneManager).GetMethod("CalculateIncomingResidentDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            var srcMethod20 = typeof(ZoneManager).GetMethod("CalculateCommercialDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            var srcMethod21 = typeof(ZoneManager).GetMethod("CalculateWorkplaceDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            var srcMethod22 = typeof(CargoTruckAI).GetMethod("SetSource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(ushort) }, null);
            var srcMethod23 = typeof(OutsideConnectionAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            var srcMethod24 = typeof(OutsideConnectionAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            var srcMethod25 = typeof(OutsideConnectionAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            var srcMethod26 = typeof(TaxiAI).GetMethod("UnloadPassengers", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(TransportPassengerData).MakeByRefType() }, null);
            var srcMethod27 = typeof(HearseAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var srcMethod28 = typeof(GarbageTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            //var srcMethod29 = typeof(BuildingWorldInfoPanel).GetMethod("GetName", BindingFlags.NonPublic | BindingFlags.Instance);
            var srcMethod30 = typeof(CitizenManager).GetMethod("CreateUnits", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint).MakeByRefType(), typeof(Randomizer).MakeByRefType(), typeof(ushort), typeof(ushort), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }, null);
            var srcMethod31 = typeof(ResidentAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(CitizenInstance.Frame).MakeByRefType(), typeof(bool) }, null);

            var srcMethod33 = typeof(PoliceCarAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var srcMethod34 = typeof(FireTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var srcMethod35 = typeof(AmbulanceAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var srcMethod36 = typeof(FireTruckAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            var srcMethod37 = typeof(AmbulanceAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            var srcMethod38 = typeof(MaintenanceTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var srcMethod39 = typeof(MaintenanceTruckAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            var srcMethod40 = typeof(MaintenanceTruckAI).GetMethod("CheckTargetSegment", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var srcMethod41 = typeof(IndustrialBuildingAI).GetMethod("GetIncomingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            var srcMethod43 = typeof(CommonBuildingAI).GetMethod("CalculateGuestVehicles", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType() }, null);
            var srcMethod42 = typeof(IndustrialBuildingAI).GetMethod("BuildingDeactivated", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);

            //protected override void SimulationStepActive(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
            var srcMethod44 = typeof(IndustrialBuildingAI).GetMethod("SimulationStepActive", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            var srcMethod45 = typeof(PlayerBuildingAI).GetMethod("GetMaintenanceCost", BindingFlags.Public | BindingFlags.Instance);
            var srcMethod46 = typeof(CommercialBuildingAI).GetMethod("GetIncomingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            var srcMethod48 = typeof(ResidentAI).GetMethod("SetTarget", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(ushort) }, null);
            //var srcMethod47 = typeof(PoliceCarAI).GetMethod("ArriveAtSource", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            //var srcMethod49 = typeof(BuildingAI).GetMethod("CheckUnlocking", BindingFlags.Public | BindingFlags.Instance);
            var srcMethod50 = typeof(PlayerBuildingAI).GetMethod("GetProductionRate", BindingFlags.Public | BindingFlags.Static);

            /*var srcMethod51 = typeof(IndustrialBuildingAI).GetMethod("MaxIncomingLoadSize", BindingFlags.NonPublic | BindingFlags.Instance);
            var srcMethod52 = typeof(CommercialBuildingAI).GetMethod("MaxIncomingLoadSize", BindingFlags.NonPublic | BindingFlags.Instance);
            var srcMethod53 = typeof(CommercialBuildingAI).GetMethod("CreateBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            var srcMethod54 = typeof(IndustrialBuildingAI).GetMethod("CreateBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);*/
            var srcMethod55 = typeof(TransferManager).GetMethod("AddOutgoingOffer", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            var srcMethod56 = typeof(PrivateBuildingAI).GetMethod("ReleaseBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            var srcMethod57 = typeof(EconomyManager).GetMethod("GetBudget", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ItemClass) }, null);
            var srcMethod58 = typeof(EconomyManager).GetMethod("AddResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass) }, null);

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
            //RedirectionHelper.RevertRedirect(srcMethod11, state11);
            //RedirectionHelper.RevertRedirect(srcMethod12, state12);
            //RedirectionHelper.RevertRedirect(srcMethod13, state13);
            RedirectionHelper.RevertRedirect(srcMethod14, state14);
            RedirectionHelper.RevertRedirect(srcMethod15, state15);
            //RedirectionHelper.RevertRedirect(srcMethod16, state16);
            RedirectionHelper.RevertRedirect(srcMethod17, state17);
            RedirectionHelper.RevertRedirect(srcMethod18, state18);
            RedirectionHelper.RevertRedirect(srcMethod19, state19);
            RedirectionHelper.RevertRedirect(srcMethod20, state20);
            RedirectionHelper.RevertRedirect(srcMethod21, state21);
            RedirectionHelper.RevertRedirect(srcMethod22, state22);
            RedirectionHelper.RevertRedirect(srcMethod23, state23);
            RedirectionHelper.RevertRedirect(srcMethod24, state24);
            RedirectionHelper.RevertRedirect(srcMethod25, state25);
            RedirectionHelper.RevertRedirect(srcMethod26, state26);
            RedirectionHelper.RevertRedirect(srcMethod27, state27);
            RedirectionHelper.RevertRedirect(srcMethod28, state28);
            //RedirectionHelper.RevertRedirect(srcMethod29, state29);
            RedirectionHelper.RevertRedirect(srcMethod30, state30);
            RedirectionHelper.RevertRedirect(srcMethod31, state31);
            //RedirectionHelper.RevertRedirect(srcMethod32, state32);
            RedirectionHelper.RevertRedirect(srcMethod33, state33);
            RedirectionHelper.RevertRedirect(srcMethod34, state34);
            RedirectionHelper.RevertRedirect(srcMethod35, state35);
            RedirectionHelper.RevertRedirect(srcMethod36, state36);
            RedirectionHelper.RevertRedirect(srcMethod37, state37);
            RedirectionHelper.RevertRedirect(srcMethod38, state38);
            RedirectionHelper.RevertRedirect(srcMethod39, state39);
            RedirectionHelper.RevertRedirect(srcMethod40, state40);
            RedirectionHelper.RevertRedirect(srcMethod41, state41);
            RedirectionHelper.RevertRedirect(srcMethod42, state42);
            RedirectionHelper.RevertRedirect(srcMethod43, state43);
            RedirectionHelper.RevertRedirect(srcMethod44, state44);
            RedirectionHelper.RevertRedirect(srcMethod45, state45);
            RedirectionHelper.RevertRedirect(srcMethod46, state46);
            //RedirectionHelper.RevertRedirect(srcMethod47, state47);
            RedirectionHelper.RevertRedirect(srcMethod48, state48);
            //RedirectionHelper.RevertRedirect(srcMethod49, state49);
            RedirectionHelper.RevertRedirect(srcMethod50, state50);
            //RedirectionHelper.RevertRedirect(srcMethod51, state51);
            //RedirectionHelper.RevertRedirect(srcMethod52, state52);
            //RedirectionHelper.RevertRedirect(srcMethod53, state53);
            //RedirectionHelper.RevertRedirect(srcMethod54, state54);
            RedirectionHelper.RevertRedirect(srcMethod55, state55);
            RedirectionHelper.RevertRedirect(srcMethod56, state56);
            RedirectionHelper.RevertRedirect(srcMethod57, state57);
            RedirectionHelper.RevertRedirect(srcMethod58, state58);
        }
    }
}
