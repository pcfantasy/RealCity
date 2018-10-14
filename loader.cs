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
        public static BuildingButton BMenuPanel;
        public static PlayerBuildingButton PBMenuPanel;

        public static FoodButton FMenuPanel;
        public static CoalButton CMenuPanel;
        public static LumberButton LMenuPanel;
        public static PetrolButton PMenuPanel;

        //public static RedirectCallsState state1;
        public static RedirectCallsState state2;
        public static RedirectCallsState state3;
        public static RedirectCallsState state4;
        public static RedirectCallsState state5;
        //public static RedirectCallsState state6;
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
        //public static RedirectCallsState state27;
        //public static RedirectCallsState state28;
        public static RedirectCallsState state29;
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
            comm_data.data_init();
            pc_EconomyManager.data_init();
            pc_EconomyManager.save_data = new byte[2768];
            pc_PrivateBuildingAI.saveData = new byte[316];
            pc_ResidentAI.saveData = new byte[140];
            comm_data.save_data = new byte[3063935];
            comm_data.save_data1 = new byte[4194304];
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
                Loader.guiPanel = (MoreeconomicUI)Loader.parentGuiView.AddUIComponent(typeof(MoreeconomicUI));
            }

            if (Loader.guiPanel1 == null)
            {
                Loader.guiPanel1 = (RealCityUI)Loader.parentGuiView.AddUIComponent(typeof(RealCityUI));
            }

            SetupBuidingGui();
            SetupHumanGui();
            SetupPlayerBuidingGui();
            SetupEcnomicButton();
            SetupCityButton();
            SetupBuildingButton();
            SetupPlayerBuildingButton();
            SetupFoodButton();
            SetupCoalButton();
            SetupLumberButton();
            SetupPetrolButton();

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


        public static void SetupFoodButton()
        {
            if (FMenuPanel == null)
            {
                FMenuPanel = (parentGuiView.AddUIComponent(typeof(FoodButton)) as FoodButton);
            }
            FMenuPanel.Show();
        }

        public static void SetupCoalButton()
        {
            if (CMenuPanel == null)
            {
                CMenuPanel = (parentGuiView.AddUIComponent(typeof(CoalButton)) as CoalButton);
            }
            CMenuPanel.Show();
        }

        public static void SetupLumberButton()
        {
            if (LMenuPanel == null)
            {
                LMenuPanel = (parentGuiView.AddUIComponent(typeof(LumberButton)) as LumberButton);
            }
            LMenuPanel.Show();
        }

        public static void SetupPetrolButton()
        {
            if (PMenuPanel == null)
            {
                PMenuPanel = (parentGuiView.AddUIComponent(typeof(PetrolButton)) as PetrolButton);
            }
            PMenuPanel.Show();
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


        public void Detour()
        {
            //var srcMethod1 = typeof(TransferManager).GetMethod("StartTransfer", BindingFlags.NonPublic | BindingFlags.Instance);
            //var destMethod1 = typeof(pc_TransferManager).GetMethod("StartTransfer", BindingFlags.NonPublic | BindingFlags.Instance);
            //state1 = RedirectionHelper.RedirectCalls(srcMethod1, destMethod1);

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

            //var srcMethod6 = typeof(HumanAI).GetMethod("ArriveAtDestination", BindingFlags.NonPublic | BindingFlags.Instance);
            //var destMethod6 = typeof(pc_HumanAI).GetMethod("ArriveAtDestination_1", BindingFlags.NonPublic | BindingFlags.Instance);
            //state6 = RedirectionHelper.RedirectCalls(srcMethod6, destMethod6);

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

            var srcMethod11 = typeof(CargoTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var destMethod11 = typeof(pc_CargoTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            state11 = RedirectionHelper.RedirectCalls(srcMethod11, destMethod11);

            var srcMethod12 = typeof(HumanAI).GetMethod("EnterVehicle", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType() }, null);
            var destMethod12 = typeof(pc_HumanAI).GetMethod("EnterVehicle_1", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType() }, null);
            state12 = RedirectionHelper.RedirectCalls(srcMethod12, destMethod12);

            var srcMethod13 = typeof(OfficeBuildingAI).GetMethod("GetOutgoingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            var destMethod13 = typeof(pc_OfficeBuildingAI).GetMethod("GetOutgoingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            state13 = RedirectionHelper.RedirectCalls(srcMethod13, destMethod13);

            var srcMethod14 = typeof(ZoneManager).GetMethod("CalculateResidentialDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            var destMethod14 = typeof(pc_ZoneManager).GetMethod("CalculateResidentialDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            state14 = RedirectionHelper.RedirectCalls(srcMethod14, destMethod14);

            var srcMethod15 = typeof(ZoneManager).GetMethod("CalculateIncomingResidentDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            var destMethod15 = typeof(pc_ZoneManager).GetMethod("CalculateIncomingResidentDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            state15 = RedirectionHelper.RedirectCalls(srcMethod15, destMethod15);

            var srcMethod16 = typeof(ZoneManager).GetMethod("CalculateCommercialDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            var destMethod16 = typeof(pc_ZoneManager).GetMethod("CalculateCommercialDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            state16 = RedirectionHelper.RedirectCalls(srcMethod16, destMethod16);

            var srcMethod17 = typeof(ZoneManager).GetMethod("CalculateWorkplaceDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            var destMethod17 = typeof(pc_ZoneManager).GetMethod("CalculateWorkplaceDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            state17 = RedirectionHelper.RedirectCalls(srcMethod17, destMethod17);

            var srcMethod18 = typeof(CargoTruckAI).GetMethod("SetSource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(ushort) }, null);
            var destMethod18 = typeof(pc_CargoTruckAI).GetMethod("SetSource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(ushort) }, null);
            state18 = RedirectionHelper.RedirectCalls(srcMethod18, destMethod18);

            var srcMethod19 = typeof(TaxiAI).GetMethod("UnloadPassengers", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(TransportPassengerData).MakeByRefType() }, null);
            var destMethod19 = typeof(pc_TaxiAI).GetMethod("UnloadPassengers", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(TransportPassengerData).MakeByRefType() }, null);
            state19 = RedirectionHelper.RedirectCalls(srcMethod19, destMethod19);

            var srcMethod20 = typeof(CitizenManager).GetMethod("CreateUnits", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint).MakeByRefType(), typeof(Randomizer).MakeByRefType(), typeof(ushort), typeof(ushort), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }, null);
            var destMethod20 = typeof(pc_CitizenManager).GetMethod("CreateUnits_1", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint).MakeByRefType(), typeof(Randomizer).MakeByRefType(), typeof(ushort), typeof(ushort), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }, null);
            state20 = RedirectionHelper.RedirectCalls(srcMethod20, destMethod20);

            var srcMethod21 = typeof(ResidentAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(CitizenInstance.Frame).MakeByRefType(), typeof(bool) }, null);
            var destMethod21 = typeof(pc_HumanAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(CitizenInstance.Frame).MakeByRefType(), typeof(bool) }, null);
            state21 = RedirectionHelper.RedirectCalls(srcMethod21, destMethod21);

            var srcMethod22 = typeof(CargoTruckAI).GetMethod("ArriveAtSource", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var destMethod22 = typeof(pc_CargoTruckAI).GetMethod("ArriveAtSource", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            state22 = RedirectionHelper.RedirectCalls(srcMethod22, destMethod22);

            var srcMethod23 = typeof(IndustrialBuildingAI).GetMethod("GetIncomingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            var destMethod23 = typeof(pc_IndustrialBuildingAI).GetMethod("GetIncomingTransferReason", BindingFlags.Public | BindingFlags.Instance);
            state23 = RedirectionHelper.RedirectCalls(srcMethod23, destMethod23);

            var srcMethod24 = typeof(IndustrialBuildingAI).GetMethod("BuildingDeactivated", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            var destMethod24 = typeof(pc_IndustrialBuildingAI).GetMethod("BuildingDeactivated", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            state24 = RedirectionHelper.RedirectCalls(srcMethod24, destMethod24);


            //TransferManager.TransferReason material, ref int count, ref int cargo, ref int capacity, ref int outside
            var srcMethod25 = typeof(CommonBuildingAI).GetMethod("CalculateGuestVehicles", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType() }, null);
            var destMethod25 = typeof(pc_PrivateBuildingAI).GetMethod("CalculateGuestVehicles_1", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType() }, null);
            state25 = RedirectionHelper.RedirectCalls(srcMethod25, destMethod25);


            //var srcMethod26 = typeof(CommercialBuildingAI).GetMethod("GetIncomingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            //var destMethod26 = typeof(pc_CommercialBuildingAI).GetMethod("GetIncomingTransferReason", BindingFlags.Public | BindingFlags.Instance);
            //state26 = RedirectionHelper.RedirectCalls(srcMethod26, destMethod26);

            //var srcMethod27 = typeof(ResidentAI).GetMethod("SetTarget", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(ushort), typeof(bool) }, null);
            //var destMethod27 = typeof(pc_ResidentAI).GetMethod("SetTarget", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(ushort), typeof(bool) }, null);
            //state27 = RedirectionHelper.RedirectCalls(srcMethod27, destMethod27);

            //var srcMethod28 = typeof(PlayerBuildingAI).GetMethod("GetProductionRate", BindingFlags.Public | BindingFlags.Static);
            //var destMethod28 = typeof(pc_PlayerBuildingAI).GetMethod("GetProductionRate_1", BindingFlags.Public | BindingFlags.Static);
            //state28 = RedirectionHelper.RedirectCalls(srcMethod28, destMethod28);

            var srcMethod29 = typeof(TransferManager).GetMethod("AddIncomingOffer", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            var destMethod29 = typeof(pc_TransferManager).GetMethod("AddIncomingOffer", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            state29 = RedirectionHelper.RedirectCalls(srcMethod29, destMethod29);

            var srcMethod30 = typeof(CommonBuildingAI).GetMethod("ReleaseBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            var destMethod30 = typeof(pc_CommonBuildingAI).GetMethod("ReleaseBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            state30 = RedirectionHelper.RedirectCalls(srcMethod30, destMethod30);

            var srcMethod31 = typeof(EconomyManager).GetMethod("GetBudget", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ItemClass) }, null);
            var destMethod31 = typeof(pc_EconomyManager).GetMethod("GetBudget", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ItemClass) }, null);
            state31 = RedirectionHelper.RedirectCalls(srcMethod31, destMethod31);

            var srcMethod32 = typeof(EconomyManager).GetMethod("AddResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass) }, null);
            var destMethod32 = typeof(pc_EconomyManager).GetMethod("AddResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass) }, null);
            state32 = RedirectionHelper.RedirectCalls(srcMethod32, destMethod32);

            var srcMethod33 = typeof(CitizenManager).GetMethod("ReleaseCitizenImplementation", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(Citizen).MakeByRefType() }, null);
            var destMethod33 = typeof(pc_CitizenManager).GetMethod("ReleaseCitizenImplementation", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(Citizen).MakeByRefType() }, null);
            state33 = RedirectionHelper.RedirectCalls(srcMethod33, destMethod33);

            var srcMethod34 = typeof(ResidentAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(Citizen).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            var destMethod34 = typeof(pc_HumanAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(Citizen).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            state34 = RedirectionHelper.RedirectCalls(srcMethod34, destMethod34);

            var srcMethod35 = typeof(VehicleAI).GetMethod("CalculateTargetSpeed", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(float), typeof(float) }, null);
            var destMethod35 = typeof(pc_VehicleAI).GetMethod("CalculateTargetSpeed_1", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(float), typeof(float) }, null);
            state35 = RedirectionHelper.RedirectCalls(srcMethod35, destMethod35);

            var srcMethod36 = typeof(OutsideConnectionAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            var destMethod36 = typeof(pc_OutsideConnectionAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            state36 = RedirectionHelper.RedirectCalls(srcMethod36, destMethod36);

            //public static void AddConnectionOffers(ushort buildingID, ref Building data, int productionRate, int cargoCapacity, int residentCapacity, int touristFactor0, int touristFactor1, int touristFactor2, TransferManager.TransferReason dummyTrafficReason, int dummyTrafficFactor)
            //var srcMethod37 = typeof(OutsideConnectionAI).GetMethod("AddConnectionOffers", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(TransferManager.TransferReason), typeof(int) }, null);
            //var destMethod37 = typeof(pc_OutsideConnectionAI).GetMethod("AddConnectionOffers", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(TransferManager.TransferReason), typeof(int) }, null);
            //state37 = RedirectionHelper.RedirectCalls(srcMethod37, srcMethod37);

            var srcMethod37 = typeof(OutsideConnectionAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            var destMethod37 = typeof(pc_OutsideConnectionAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            state37 = RedirectionHelper.RedirectCalls(srcMethod37, destMethod37);

            var srcMethod38 = typeof(OutsideConnectionAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            var destMethod38 = typeof(pc_OutsideConnectionAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            state38 = RedirectionHelper.RedirectCalls(srcMethod38, destMethod38);

            var srcMethod39 = typeof(GarbageTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var destMethod39 = typeof(pc_GarbageTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            state39 = RedirectionHelper.RedirectCalls(srcMethod39, destMethod39);

            //public virtual void VisitorEnter(ushort buildingID, ref Building data, uint citizen)
            var srcMethod40 = typeof(BuildingAI).GetMethod("VisitorEnter", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(uint) }, null);
            var destMethod40 = typeof(pc_HumanAI).GetMethod("VisitorEnter", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(uint) }, null);
            state40 = RedirectionHelper.RedirectCalls(srcMethod40, destMethod40);

            var srcMethod41 = typeof(CommercialBuildingAI).GetMethod("CreateBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            var destMethod41 = typeof(pc_CommercialBuildingAI).GetMethod("CreateBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            state41 = RedirectionHelper.RedirectCalls(srcMethod41, destMethod41);

            var srcMethod42 = typeof(IndustrialBuildingAI).GetMethod("CreateBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            var destMethod42 = typeof(pc_IndustrialBuildingAI).GetMethod("CreateBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            state42 = RedirectionHelper.RedirectCalls(srcMethod42, destMethod42);
        }

        public void RevertDetour()
        {
            //var srcMethod1 = typeof(TransferManager).GetMethod("StartTransfer", BindingFlags.NonPublic | BindingFlags.Instance);
            var srcMethod2 = typeof(IndustrialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            var srcMethod3 = typeof(IndustrialExtractorAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            var srcMethod4 = typeof(CommercialBuildingAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            var srcMethod5 = typeof(ResidentAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(CitizenUnit).MakeByRefType() }, null);
            //var srcMethod6 = typeof(HumanAI).GetMethod("ArriveAtDestination", BindingFlags.NonPublic | BindingFlags.Instance);
            var srcMethod7 = typeof(EconomyManager).GetMethod("FetchResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass) }, null);
            var srcMethod8 = typeof(EconomyManager).GetMethod("AddPrivateIncome", BindingFlags.Public | BindingFlags.Instance);
            var srcMethod9 = typeof(PrivateBuildingAI).GetMethod("SimulationStepActive", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(Building.Frame).MakeByRefType() }, null);
            var srcMethod10 = typeof(PassengerCarAI).GetMethod("ArriveAtDestination", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var srcMethod11 = typeof(CargoTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var srcMethod12 = typeof(HumanAI).GetMethod("EnterVehicle", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType() }, null);
            var srcMethod13 = typeof(OfficeBuildingAI).GetMethod("GetOutgoingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            var srcMethod14 = typeof(ZoneManager).GetMethod("CalculateResidentialDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            var srcMethod15 = typeof(ZoneManager).GetMethod("CalculateIncomingResidentDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            var srcMethod16 = typeof(ZoneManager).GetMethod("CalculateCommercialDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            var srcMethod17 = typeof(ZoneManager).GetMethod("CalculateWorkplaceDemand", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(District).MakeByRefType() }, null);
            var srcMethod18 = typeof(CargoTruckAI).GetMethod("SetSource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(ushort) }, null);
            var srcMethod19 = typeof(TaxiAI).GetMethod("UnloadPassengers", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(TransportPassengerData).MakeByRefType() }, null);
            var srcMethod20 = typeof(CitizenManager).GetMethod("CreateUnits", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint).MakeByRefType(), typeof(Randomizer).MakeByRefType(), typeof(ushort), typeof(ushort), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) }, null);
            var srcMethod21 = typeof(ResidentAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(CitizenInstance.Frame).MakeByRefType(), typeof(bool) }, null);
            var srcMethod22 = typeof(CargoTruckAI).GetMethod("ArriveAtSource", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var srcMethod23 = typeof(IndustrialBuildingAI).GetMethod("GetIncomingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            var srcMethod24 = typeof(CommonBuildingAI).GetMethod("CalculateGuestVehicles", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType() }, null);
            var srcMethod25 = typeof(IndustrialBuildingAI).GetMethod("BuildingDeactivated", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);

            //protected override void SimulationStepActive(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
            //var srcMethod26 = typeof(CommercialBuildingAI).GetMethod("GetIncomingTransferReason", BindingFlags.NonPublic | BindingFlags.Instance);
            //var srcMethod27 = typeof(ResidentAI).GetMethod("SetTarget", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(ushort), typeof(bool) }, null);
            //var srcMethod28 = typeof(PlayerBuildingAI).GetMethod("GetProductionRate", BindingFlags.Public | BindingFlags.Static);

            var srcMethod29 = typeof(TransferManager).GetMethod("AddIncomingOffer", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            var srcMethod30 = typeof(CommonBuildingAI).GetMethod("ReleaseBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            var srcMethod31 = typeof(EconomyManager).GetMethod("GetBudget", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ItemClass) }, null);
            var srcMethod32 = typeof(EconomyManager).GetMethod("AddResource", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(EconomyManager.Resource), typeof(int), typeof(ItemClass) }, null);
            var srcMethod33 = typeof(CitizenManager).GetMethod("ReleaseCitizenImplementation", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(Citizen).MakeByRefType() }, null);
            var srcMethod34 = typeof(ResidentAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(Citizen).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            var srcMethod35 = typeof(VehicleAI).GetMethod("CalculateTargetSpeed", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType(), typeof(float), typeof(float) }, null);
            var srcMethod36 = typeof(OutsideConnectionAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            var srcMethod37 = typeof(OutsideConnectionAI).GetMethod("StartTransfer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(TransferManager.TransferOffer) }, null);
            var srcMethod38 = typeof(OutsideConnectionAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
            var srcMethod39 = typeof(GarbageTruckAI).GetMethod("ArriveAtTarget", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
            var srcMethod40 = typeof(BuildingAI).GetMethod("VisitorEnter", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(uint) }, null);
            var srcMethod41 = typeof(CommercialBuildingAI).GetMethod("CreateBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);
            var srcMethod42 = typeof(IndustrialBuildingAI).GetMethod("CreateBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType() }, null);

            //RedirectionHelper.RevertRedirect(srcMethod1, state1);
            RedirectionHelper.RevertRedirect(srcMethod2, state2);
            RedirectionHelper.RevertRedirect(srcMethod3, state3);
            RedirectionHelper.RevertRedirect(srcMethod4, state4);
            RedirectionHelper.RevertRedirect(srcMethod5, state5);
            //RedirectionHelper.RevertRedirect(srcMethod6, state6);
            RedirectionHelper.RevertRedirect(srcMethod7, state7);
            RedirectionHelper.RevertRedirect(srcMethod8, state8);
            RedirectionHelper.RevertRedirect(srcMethod9, state9);
            RedirectionHelper.RevertRedirect(srcMethod10, state10);
            RedirectionHelper.RevertRedirect(srcMethod11, state11);
            RedirectionHelper.RevertRedirect(srcMethod12, state12);
            RedirectionHelper.RevertRedirect(srcMethod13, state13);
            RedirectionHelper.RevertRedirect(srcMethod14, state14);
            RedirectionHelper.RevertRedirect(srcMethod15, state15);
            RedirectionHelper.RevertRedirect(srcMethod16, state16);
            RedirectionHelper.RevertRedirect(srcMethod17, state17);
            RedirectionHelper.RevertRedirect(srcMethod18, state18);
            RedirectionHelper.RevertRedirect(srcMethod19, state19);
            RedirectionHelper.RevertRedirect(srcMethod20, state20);
            RedirectionHelper.RevertRedirect(srcMethod21, state21);
            RedirectionHelper.RevertRedirect(srcMethod22, state22);
            RedirectionHelper.RevertRedirect(srcMethod23, state23);
            RedirectionHelper.RevertRedirect(srcMethod24, state24);
            RedirectionHelper.RevertRedirect(srcMethod25, state25);
            //RedirectionHelper.RevertRedirect(srcMethod26, state26);
            //RedirectionHelper.RevertRedirect(srcMethod27, state27);
            //RedirectionHelper.RevertRedirect(srcMethod28, state28);
            RedirectionHelper.RevertRedirect(srcMethod29, state29);
            RedirectionHelper.RevertRedirect(srcMethod30, state30);
            RedirectionHelper.RevertRedirect(srcMethod31, state31);
            RedirectionHelper.RevertRedirect(srcMethod32, state32);
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
        }
    }
}
