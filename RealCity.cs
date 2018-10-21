using System;
using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using System.Reflection;
using System.IO;
using System.Linq;
using ColossalFramework.Math;
using UnityEngine;

namespace RealCity
{

    public class RealCity : IUserMod
    {
        public static bool IsEnabled = false;
        public static bool updateOnce = false;

        public static int foodStillNeeded = 0;
        public static int lumberStillNeeded = 0;
        public static int coalStillNeeded = 0;
        public static int PetrolStillNeeded = 0;

        public byte tip1_citizen = 0;
        public byte tip2_building = 0;
        public byte tip3_outside = 0;

        public static string tip1_message_forgui = "";
        public static string tip2_message_forgui = "";
        public static string tip3_message_forgui = "";
        public static string tip4_message_forgui = "";
        public static string tip5_message_forgui = "";
        public static string tip6_message_forgui = "";
        public static string tip7_message_forgui = "";
        public static string tip8_message_forgui = "";
        public static string tip9_message_forgui = "";
        public static string tip10_message_forgui = "";

        //public static string tip1_message = "";
        //public static string tip2_message = "";
        //public static string tip3_message = "";

        public static byte partyTrend = 0;
        public static ushort partyTrendStrength = 0;

        public static int language_idex = 0;

        public string Name
        {
            get { return "Real City"; }
        }

        public string Description
        {
            get { return "Make your city reality"; }
        }

        public void OnEnabled()
        {
            RealCity.IsEnabled = true;
            FileStream fs = File.Create("RealCity.txt");
            fs.Close();
            LoadSetting();
            SaveSetting();
            language.LanguageSwitch((byte)language_idex);
        }

        public void OnDisabled()
        {
            RealCity.IsEnabled = false;
            language.LanguageSwitch((byte)language_idex);
        }


        public static void SaveSetting()
        {
            //save langugae
            FileStream fs = File.Create("RealCityV2.0_setting.txt");
            StreamWriter streamWriter = new StreamWriter(fs);
            streamWriter.WriteLine(comm_data.last_language);
            streamWriter.WriteLine(comm_data.isSmartPbtp);
            streamWriter.WriteLine(comm_data.isHellMode);
            streamWriter.Flush();
            fs.Close();
        }

        public static void LoadSetting()
        {
            if (File.Exists("RealCityV2.0_setting.txt"))
            {
                FileStream fs = new FileStream("RealCityV2.0_setting.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string strLine = sr.ReadLine();

                if (strLine == "1")
                {
                    comm_data.last_language = 1;
                }
                else
                {
                    comm_data.last_language = 0;
                }

                strLine = sr.ReadLine();

                if (strLine == "False")
                {
                    comm_data.isSmartPbtp = false;
                }
                else
                {
                    comm_data.isSmartPbtp = true;
                }

                strLine = sr.ReadLine();

                if (strLine == "False")
                {
                    comm_data.isHellMode = false;
                }
                else
                {
                    comm_data.isHellMode = true;
                }
                sr.Close();
                fs.Close();
            }
        }


        public void OnSettingsUI(UIHelperBase helper)
        {

            LoadSetting();
            language.LanguageSwitch(comm_data.last_language);
            UIHelperBase group = helper.AddGroup(language.OptionUI[0]);
            group.AddDropdown(language.OptionUI[1], new string[] { "English", "简体中文" }, comm_data.last_language, (index) => GetLanguageIdex(index));
            UIHelperBase group2 = helper.AddGroup(language.OptionUI[2]);
            group2.AddCheckbox(language.OptionUI[2], comm_data.isSmartPbtp, (index) => IsSmartPbtp(index));
            UIHelperBase group3 = helper.AddGroup(language.OptionUI[3]);
            group3.AddCheckbox(language.OptionUI[3], comm_data.isHellMode, (index) => IsHellMode(index));
            SaveSetting();
        }

        public void GetLanguageIdex(int index)
        {
            language_idex = index;
            language.LanguageSwitch((byte)language_idex);
            SaveSetting();
            MethodInfo method = typeof(OptionsMainPanel).GetMethod("OnLocaleChanged", BindingFlags.Instance | BindingFlags.NonPublic);
            method.Invoke(UIView.library.Get<OptionsMainPanel>("OptionsPanel"), new object[0]);
            Loader.RemoveGui();
            if (Loader.CurrentLoadMode == LoadMode.LoadGame || Loader.CurrentLoadMode == LoadMode.NewGame)
            {
                if (RealCity.IsEnabled)
                {
                    Loader.SetupGui();
                    //DebugLog.LogToFileOnly("get_current language idex = " + language_idex.ToString());
                }
            }
        }

        public void IsSmartPbtp(bool index)
        {
            comm_data.isSmartPbtp = index;
            SaveSetting();
        }

        public void IsHellMode(bool index)
        {
            comm_data.isHellMode = index;
            SaveSetting();
        }

        public class EconomyExtension : EconomyExtensionBase
        {
            public override long OnUpdateMoneyAmount(long internalMoneyAmount)
            {
                if (Loader.CurrentLoadMode == LoadMode.LoadGame || Loader.CurrentLoadMode == LoadMode.NewGame)
                {
                    comm_data.current_time = Singleton<SimulationManager>.instance.m_currentDayTimeHour;
                    uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
                    uint num2 = currentFrameIndex & 255u;
                    if ((num2 == 255u) && (comm_data.current_time != comm_data.prev_time))
                    {


                        //citizen_status();
                        GenerateTips();


                        comm_data.isCoalsGetted = false;
                        comm_data.isFoodsGetted = false;
                        comm_data.isPetrolsGetted = false;
                        comm_data.isLumbersGetted = false;
                        comm_data.allFoodsFinal = comm_data.allFoods;
                        comm_data.allLumbersFinal = comm_data.allLumbers;
                        comm_data.allCoalsFinal = comm_data.allCoals;
                        comm_data.allPetrolsFinal = comm_data.allPetrols;
                        comm_data.allVehiclesFinal = comm_data.allVehicles;


                        comm_data.isCoalsGettedFinal = (coalStillNeeded <= 0) && (comm_data.allCoals != 0);
                        comm_data.isFoodsGettedFinal = (foodStillNeeded <= 0) && (comm_data.allFoods != 0);
                        comm_data.isPetrolsGettedFinal = (PetrolStillNeeded <= 0) && (comm_data.allPetrols != 0);
                        comm_data.isLumbersGettedFinal = (lumberStillNeeded <= 0) && (comm_data.allLumbers != 0);
                        foodStillNeeded = (comm_data.citizen_count > 16) ? (comm_data.citizen_count >> 4) : 0;
                        lumberStillNeeded = (pc_PrivateBuildingAI.allBuildingsFinal > 8) ? (pc_PrivateBuildingAI.allBuildingsFinal >> 3) : 0;
                        coalStillNeeded = (pc_PrivateBuildingAI.allBuildingsFinal > 16) ? (pc_PrivateBuildingAI.allBuildingsFinal >> 4) : 0;
                        PetrolStillNeeded = comm_data.allVehiclesFinal;
                        comm_data.allVehicles = 0;
                        comm_data.allFoods = 0;
                        comm_data.allLumbers = 0;
                        comm_data.allPetrols = 0;
                        comm_data.allCoals = 0;
                        BuildingStatus();

                        if (comm_data.update_money_count == 16)
                        {
                            Politics.parliamentCount--;
                            Politics.parliamentMeetingCount--;
                            if (Politics.parliamentCount < 0)
                            {
                                Politics.parliamentCount = 30;
                            }
                            if (Politics.parliamentMeetingCount < 0)
                            {
                                Politics.parliamentMeetingCount = 2;
                            }
                            CitizenStatus();
                        }


                        CaculateCitizenTransportFee();
                        comm_data.update_money_count++;
                        if (comm_data.update_money_count == 17)
                        {
                            comm_data.update_money_count = 0;
                        }
                        pc_EconomyManager.clean_current(comm_data.update_money_count);


                        comm_data.prev_time = comm_data.current_time;
                    }
                    PoliticsUI.refeshOnce = true;
                    RealCityUI.refeshOnce = true;
                    EcnomicUI.refeshOnce = true;
                    PlayerBuildingUI.refesh_once = true;
                    BuildingUI.refeshOnce = true;
                    HumanUI.refeshOnce = true;
                    FoodButton.refeshOnce = true;
                    CoalButton.refeshOnce = true;
                    LumberButton.refeshOnce = true;
                    PetrolButton.refeshOnce = true;
                    comm_data.is_updated = true;
                }
                return internalMoneyAmount;
            }

            public void GenerateTips()
            {
                tip1_message_forgui = language.TipAndChirperMessage[5];

                tip2_message_forgui = language.TipAndChirperMessage[7];

                if (!comm_data.haveCityResourceDepartmentFinal)
                {
                    tip3_message_forgui = language.TipAndChirperMessage[8];
                }
                else
                {
                    tip3_message_forgui = "";
                }

                if (!comm_data.isFoodsGettedFinal)
                {
                    tip4_message_forgui = language.TipAndChirperMessage[9];
                }
                else
                {
                    tip4_message_forgui = "";
                }

                if (!comm_data.isLumbersGettedFinal || !comm_data.isCoalsGettedFinal)
                {
                    tip5_message_forgui = language.TipAndChirperMessage[10];
                }
                else
                {
                    tip5_message_forgui = "";
                }

                if (!comm_data.isPetrolsGettedFinal)
                {
                    tip6_message_forgui = language.TipAndChirperMessage[11];
                }
                else
                {
                    tip6_message_forgui = "";
                }

                if (!comm_data.isHellMode)
                {
                    tip7_message_forgui = language.OptionUI[3];
                }
                else
                {
                    tip7_message_forgui = "";
                }
            }

            public void CaculateCitizenTransportFee()
            {
                ItemClass temp = new ItemClass();
                long temp1 = 0L;
                long temp2 = 0L;
                comm_data.public_transport_fee = 0L;
                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportBus;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.bus_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_subService = ItemClass.SubService.PublicTransportTram;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.tram_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_subService = ItemClass.SubService.PublicTransportMetro;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.metro_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_subService = ItemClass.SubService.PublicTransportTrain;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.train_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_subService = ItemClass.SubService.PublicTransportTaxi;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.taxi_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportPlane;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.plane_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_subService = ItemClass.SubService.PublicTransportShip;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.ship_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_subService = ItemClass.SubService.PublicTransportMonorail;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.monorail_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_subService = ItemClass.SubService.PublicTransportCableCar;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.cablecar_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                //add vehicle transport_fee
                comm_data.temp_total_citizen_vehical_time_last = comm_data.temp_total_citizen_vehical_time;
                comm_data.temp_total_citizen_vehical_time = 0;

                //assume that 1 time will cost 5fen car oil money
                comm_data.all_transport_fee = comm_data.public_transport_fee + comm_data.temp_total_citizen_vehical_time_last;

                if (comm_data.family_count > 0)
                {
                    comm_data.citizen_average_transport_fee = (byte)(comm_data.all_transport_fee / comm_data.family_count);
                }
            }


            public void BuildingStatus()
            {
                BuildingManager instance = Singleton<BuildingManager>.instance;
                updateOnce = false;
                pc_OutsideConnectionAI.haveGarbageBuildingFinal = pc_OutsideConnectionAI.haveGarbageBuilding;
                comm_data.haveCityResourceDepartmentFinal = comm_data.haveCityResourceDepartment;
                pc_OutsideConnectionAI.haveGarbageBuilding = false;
                comm_data.haveCityResourceDepartment = false;

                updateOnce = true;
            }


            

            public static byte IsSpecialBuilding(ushort id)
            {
                BuildingManager instance = Singleton<BuildingManager>.instance;

                if (instance.m_buildings.m_buffer[id].Info.m_buildingAI.GetConstructionCost() == 208600)
                {
                    return 3;
                }

                return 0;
            }

            public void CitizenStatus()
            {
                System.Random rand = new System.Random();
                partyTrend = (byte)rand.Next(5);
                partyTrendStrength = (byte)rand.Next(800);

                if (Politics.parliamentCount == 0)
                {
                    GetSeats(false);
                    GetSeats(true);
                    Politics.parliamentMeetingCount = 2;
                    CreateGoverment();
                }
                else
                {
                    GetSeats(true);
                    CreateGoverment();
                }

                if (Politics.parliamentMeetingCount <= 0)
                {
                    HoldMeeting();
                }

                PoliticsUI.fallLandTax_Checkbox.isChecked = Politics.tryFallLandTax;
                PoliticsUI.fallImportTax_Checkbox.isChecked = Politics.tryFallImportTax;
                PoliticsUI.fallTradeTax_Checkbox.isChecked = Politics.tryFallTradeTax;
                PoliticsUI.riseImportTax_Checkbox.isChecked = Politics.tryRiseImportTax;
                PoliticsUI.riseTradeTax_Checkbox.isChecked = Politics.tryRiseTradeTax;
            }

            public void HoldMeeting()
            {
                int temp = Politics.cPartySeats + Politics.gPartySeats + Politics.sPartySeats + Politics.lPartySeats + Politics.nPartySeats;
                if (temp == 99)
                {
                    System.Random rand = new System.Random();
                    switch (rand.Next(14))
                    {
                        case 0:
                            if (Politics.salaryTaxOffset >= 0.099f)
                            {
                                Politics.currentIdx = 1;
                            }
                            else
                            {
                                Politics.currentIdx = 0;
                            }
                            break;
                        case 1:
                            if (Politics.salaryTaxOffset <= 0f)
                            {
                                Politics.currentIdx = 0;
                            }
                            else
                            {
                                Politics.currentIdx = 1;
                            }
                            break;
                        case 4:
                            if (Politics.tradeTaxOffset >= 0.099f)
                            {
                                Politics.currentIdx = 5;
                            }
                            else
                            {
                                Politics.currentIdx = 4;
                            }
                            break;
                        case 5:
                            if (Politics.tradeTaxOffset <= 0f)
                            {
                                Politics.currentIdx = 4;
                            }
                            else
                            {
                                Politics.currentIdx = 5;
                            }
                            break;
                        case 2:
                            if (Politics.benefitOffset >= 10)
                            {
                                Politics.currentIdx = 3;
                            }
                            else
                            {
                                Politics.currentIdx = 2;
                            }
                            break;
                        case 3:
                            if (Politics.benefitOffset <= 0)
                            {
                                Politics.currentIdx = 2;
                            }
                            else
                            {
                                Politics.currentIdx = 3;
                            }
                            break;
                        case 6:
                            if (Politics.importTaxOffset >= 0.399f)
                            {
                                Politics.currentIdx = 7;
                                //DebugLog.LogToFileOnly("Politics.importTaxOffset case 6, change to 7 " + Politics.importTaxOffset.ToString());
                            }
                            else
                            {
                                Politics.currentIdx = 6;
                                //DebugLog.LogToFileOnly("Politics.importTaxOffset case 6" + Politics.importTaxOffset.ToString());
                            }
                            break;
                        case 7:
                            if (Politics.importTaxOffset <= 0f)
                            {
                                Politics.currentIdx = 6;
                                //DebugLog.LogToFileOnly("Politics.importTaxOffset case 7 change to 6 " + Politics.importTaxOffset.ToString());
                            }
                            else
                            {
                                Politics.currentIdx = 7;
                                //DebugLog.LogToFileOnly("Politics.importTaxOffset case 7 " + Politics.importTaxOffset.ToString());
                            }
                            break;
                        case 8:
                            if (Politics.stateOwnedPercent >= 50)
                            {
                                Politics.currentIdx = 9;
                            }
                            else
                            {
                                Politics.currentIdx = 8;
                            }
                            break;
                        case 9:
                            if (Politics.stateOwnedPercent <= 0)
                            {
                                Politics.currentIdx = 8;
                            }
                            else
                            {
                                Politics.currentIdx = 9;
                            }
                            break;
                        case 10:
                            if (!Politics.isOutSideGarbagePermit)
                            {
                                Politics.currentIdx = 11;
                            }
                            else
                            {
                                Politics.currentIdx = 10;
                            }
                            break;
                        case 11:
                            if (Politics.isOutSideGarbagePermit)
                            {
                                Politics.currentIdx = 10;
                            }
                            else
                            {
                                Politics.currentIdx = 11;
                            }
                            break;
                        case 12:
                            if (Politics.landRentOffset >= 10)
                            {
                                Politics.currentIdx = 13;
                            }
                            else
                            {
                                Politics.currentIdx = 12;
                            }
                            break;
                        case 13:
                            if (Politics.landRentOffset <= 0)
                            {
                                Politics.currentIdx = 12;
                            }
                            else
                            {
                                Politics.currentIdx = 13;
                            }
                            break;
                        default: Politics.currentIdx = 14; break;
                    }
                    VoteResult(Politics.currentIdx);
                }
            }

            public void VoteResult(int idex)
            {
                int temp = Politics.cPartySeats + Politics.gPartySeats + Politics.sPartySeats + Politics.lPartySeats + Politics.nPartySeats;
                int yes = 0;
                int no = 0;
                int noAttend = 0;
                int salaryTaxOffset = 10 - (int)(Politics.salaryTaxOffset * 200);
                int benefitOffset = 10 - (int)(Politics.benefitOffset * 2);
                int tradeOffset = 10 - (int)(Politics.tradeTaxOffset * 200);
                int importOffset = 10 - (int)(Politics.importTaxOffset * 50);
                int stateOwnedOffset = 10 - (int)(Politics.stateOwnedPercent * 2 / 5);
                int landRentOffset = 10 - (int)(Politics.landRentOffset * 2);


                int temp2 = 0; //((Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_finalHappiness - 80)) > 0 ? ((int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_finalHappiness - 80) << 7 : 0;
                int temp3 = 0;

                FieldInfo cashAmount;
                cashAmount = typeof(EconomyManager).GetField("m_cashAmount", BindingFlags.NonPublic | BindingFlags.Instance);
                long _cashAmount = (long)cashAmount.GetValue(Singleton<EconomyManager>.instance);

                if (_cashAmount < 0)
                {
                    temp3 = -1000;
                }
                else if (_cashAmount > 5000000)
                {
                    temp3 = 1000;
                }
                else if (_cashAmount > 3000000)
                {
                    temp3 = 500;
                }
                else if (_cashAmount < 1000000)
                {
                    temp3 = -500;
                }


                if (Politics.tryFallLandTax)
                {
                    temp2 = ((Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_finalHappiness - 80)) > 0 ? ((int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_finalHappiness - 80) << 7 : 0;
                    idex = 13;
                    Politics.currentIdx = 13;
                }
                else if (Politics.tryFallImportTax)
                {
                    temp2 = ((Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_finalHappiness - 80)) > 0 ? ((int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_finalHappiness - 80) << 7 : 0;
                    idex = 7;
                    Politics.currentIdx = 7;
                }
                else if (Politics.tryFallTradeTax)
                {
                    temp2 = ((Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_finalHappiness - 80)) > 0 ? ((int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_finalHappiness - 80) << 7 : 0;
                    idex = 5;
                    Politics.currentIdx = 5;
                }
                else if (Politics.tryRiseImportTax)
                {
                    temp2 = ((Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_finalHappiness - 80)) > 0 ? ((int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_finalHappiness - 80) << 7 : 0;
                    idex = 6;
                    Politics.currentIdx = 6;
                }
                else if (Politics.tryRiseTradeTax)
                {
                    temp2 = ((Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_finalHappiness - 80)) > 0 ? ((int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_finalHappiness - 80) << 7 : 0;
                    idex = 4;
                    Politics.currentIdx = 4;
                }


                Politics.tryRiseImportTax = false;
                Politics.tryFallImportTax = false;
                Politics.tryFallLandTax = false;
                Politics.tryRiseTradeTax = false;
                Politics.tryFallTradeTax = false;



                if (temp == 99)
                {
                    switch (idex)
                    {
                        case 0:
                            yes += Politics.cPartySeats * (Politics.riseSalaryTax[0, 0] + salaryTaxOffset);
                            yes += Politics.gPartySeats * (Politics.riseSalaryTax[1, 0] + salaryTaxOffset);
                            yes += Politics.sPartySeats * (Politics.riseSalaryTax[2, 0] + salaryTaxOffset);
                            yes += Politics.lPartySeats * (Politics.riseSalaryTax[3, 0] + salaryTaxOffset);
                            yes += Politics.nPartySeats * (Politics.riseSalaryTax[4, 0] + salaryTaxOffset);
                            no += Politics.cPartySeats * (Politics.riseSalaryTax[0, 1] - salaryTaxOffset);
                            no += Politics.gPartySeats * (Politics.riseSalaryTax[1, 1] - salaryTaxOffset);
                            no += Politics.sPartySeats * (Politics.riseSalaryTax[2, 1] - salaryTaxOffset);
                            no += Politics.lPartySeats * (Politics.riseSalaryTax[3, 1] - salaryTaxOffset);
                            no += Politics.nPartySeats * (Politics.riseSalaryTax[4, 1] - salaryTaxOffset);
                            noAttend += Politics.cPartySeats * Politics.riseSalaryTax[0, 2];
                            noAttend += Politics.gPartySeats * Politics.riseSalaryTax[1, 2];
                            noAttend += Politics.sPartySeats * Politics.riseSalaryTax[2, 2];
                            noAttend += Politics.lPartySeats * Politics.riseSalaryTax[3, 2];
                            noAttend += Politics.nPartySeats * Politics.riseSalaryTax[4, 2];
                            yes -= temp3;
                            break;
                        case 1:
                            yes += Politics.cPartySeats * (Politics.fallSalaryTax[0, 0] - salaryTaxOffset);
                            yes += Politics.gPartySeats * (Politics.fallSalaryTax[1, 0] - salaryTaxOffset);
                            yes += Politics.sPartySeats * (Politics.fallSalaryTax[2, 0] - salaryTaxOffset);
                            yes += Politics.lPartySeats * (Politics.fallSalaryTax[3, 0] - salaryTaxOffset);
                            yes += Politics.nPartySeats * (Politics.fallSalaryTax[4, 0] - salaryTaxOffset);
                            no += Politics.cPartySeats * (Politics.fallSalaryTax[0, 1] + salaryTaxOffset);
                            no += Politics.gPartySeats * (Politics.fallSalaryTax[1, 1] + salaryTaxOffset);
                            no += Politics.sPartySeats * (Politics.fallSalaryTax[2, 1] + salaryTaxOffset);
                            no += Politics.lPartySeats * (Politics.fallSalaryTax[3, 1] + salaryTaxOffset);
                            no += Politics.nPartySeats * (Politics.fallSalaryTax[4, 1] + salaryTaxOffset);
                            noAttend += Politics.cPartySeats * Politics.fallSalaryTax[0, 2];
                            noAttend += Politics.gPartySeats * Politics.fallSalaryTax[1, 2];
                            noAttend += Politics.sPartySeats * Politics.fallSalaryTax[2, 2];
                            noAttend += Politics.lPartySeats * Politics.fallSalaryTax[3, 2];
                            noAttend += Politics.nPartySeats * Politics.fallSalaryTax[4, 2];
                            yes += temp3;
                            break;
                        case 4:
                            yes += Politics.cPartySeats * (Politics.riseTradeTax[0, 0] + tradeOffset);
                            yes += Politics.gPartySeats * (Politics.riseTradeTax[1, 0] + tradeOffset);
                            yes += Politics.sPartySeats * (Politics.riseTradeTax[2, 0] + tradeOffset);
                            yes += Politics.lPartySeats * (Politics.riseTradeTax[3, 0] + tradeOffset);
                            yes += Politics.nPartySeats * (Politics.riseTradeTax[4, 0] + tradeOffset);
                            no += Politics.cPartySeats * (Politics.riseTradeTax[0, 1] - tradeOffset);
                            no += Politics.gPartySeats * (Politics.riseTradeTax[1, 1] - tradeOffset);
                            no += Politics.sPartySeats * (Politics.riseTradeTax[2, 1] - tradeOffset);
                            no += Politics.lPartySeats * (Politics.riseTradeTax[3, 1] - tradeOffset);
                            no += Politics.nPartySeats * (Politics.riseTradeTax[4, 1] - tradeOffset);
                            noAttend += Politics.cPartySeats * Politics.riseTradeTax[0, 2];
                            noAttend += Politics.gPartySeats * Politics.riseTradeTax[1, 2];
                            noAttend += Politics.sPartySeats * Politics.riseTradeTax[2, 2];
                            noAttend += Politics.lPartySeats * Politics.riseTradeTax[3, 2];
                            noAttend += Politics.nPartySeats * Politics.riseTradeTax[4, 2];
                            yes -= temp3;
                            break;
                        case 5:
                            yes += Politics.cPartySeats * (Politics.fallTradeTax[0, 0] - tradeOffset);
                            yes += Politics.gPartySeats * (Politics.fallTradeTax[1, 0] - tradeOffset);
                            yes += Politics.sPartySeats * (Politics.fallTradeTax[2, 0] - tradeOffset);
                            yes += Politics.lPartySeats * (Politics.fallTradeTax[3, 0] - tradeOffset);
                            yes += Politics.nPartySeats * (Politics.fallTradeTax[4, 0] - tradeOffset);
                            no += Politics.cPartySeats * (Politics.fallTradeTax[0, 1] + tradeOffset);
                            no += Politics.gPartySeats * (Politics.fallTradeTax[1, 1] + tradeOffset);
                            no += Politics.sPartySeats * (Politics.fallTradeTax[2, 1] + tradeOffset);
                            no += Politics.lPartySeats * (Politics.fallTradeTax[3, 1] + tradeOffset);
                            no += Politics.nPartySeats * (Politics.fallTradeTax[4, 1] + tradeOffset);
                            noAttend += Politics.cPartySeats * Politics.fallTradeTax[0, 2];
                            noAttend += Politics.gPartySeats * Politics.fallTradeTax[1, 2];
                            noAttend += Politics.sPartySeats * Politics.fallTradeTax[2, 2];
                            noAttend += Politics.lPartySeats * Politics.fallTradeTax[3, 2];
                            noAttend += Politics.nPartySeats * Politics.fallTradeTax[4, 2];
                            yes += temp3;
                            break;
                        case 2:
                            yes += Politics.cPartySeats * (Politics.riseBenefit[0, 0] + benefitOffset);
                            yes += Politics.gPartySeats * (Politics.riseBenefit[1, 0] + benefitOffset);
                            yes += Politics.sPartySeats * (Politics.riseBenefit[2, 0] + benefitOffset);
                            yes += Politics.lPartySeats * (Politics.riseBenefit[3, 0] + benefitOffset);
                            yes += Politics.nPartySeats * (Politics.riseBenefit[4, 0] + benefitOffset);
                            no += Politics.cPartySeats * (Politics.riseBenefit[0, 1] - benefitOffset);
                            no += Politics.gPartySeats * (Politics.riseBenefit[1, 1] - benefitOffset);
                            no += Politics.sPartySeats * (Politics.riseBenefit[2, 1] - benefitOffset);
                            no += Politics.lPartySeats * (Politics.riseBenefit[3, 1] - benefitOffset);
                            no += Politics.nPartySeats * (Politics.riseBenefit[4, 1] - benefitOffset);
                            noAttend += Politics.cPartySeats * Politics.riseBenefit[0, 2];
                            noAttend += Politics.gPartySeats * Politics.riseBenefit[1, 2];
                            noAttend += Politics.sPartySeats * Politics.riseBenefit[2, 2];
                            noAttend += Politics.lPartySeats * Politics.riseBenefit[3, 2];
                            noAttend += Politics.nPartySeats * Politics.riseBenefit[4, 2];
                            yes += temp3;
                            break;
                        case 3:
                            yes += Politics.cPartySeats * (Politics.fallBenefit[0, 0] - benefitOffset);
                            yes += Politics.gPartySeats * (Politics.fallBenefit[1, 0] - benefitOffset);
                            yes += Politics.sPartySeats * (Politics.fallBenefit[2, 0] - benefitOffset);
                            yes += Politics.lPartySeats * (Politics.fallBenefit[3, 0] - benefitOffset);
                            yes += Politics.nPartySeats * (Politics.fallBenefit[4, 0] - benefitOffset);
                            no += Politics.cPartySeats * (Politics.fallBenefit[0, 1] + benefitOffset);
                            no += Politics.gPartySeats * (Politics.fallBenefit[1, 1] + benefitOffset);
                            no += Politics.sPartySeats * (Politics.fallBenefit[2, 1] + benefitOffset);
                            no += Politics.lPartySeats * (Politics.fallBenefit[3, 1] + benefitOffset);
                            no += Politics.nPartySeats * (Politics.fallBenefit[4, 1] + benefitOffset);
                            noAttend += Politics.cPartySeats * Politics.fallBenefit[0, 2];
                            noAttend += Politics.gPartySeats * Politics.fallBenefit[1, 2];
                            noAttend += Politics.sPartySeats * Politics.fallBenefit[2, 2];
                            noAttend += Politics.lPartySeats * Politics.fallBenefit[3, 2];
                            noAttend += Politics.nPartySeats * Politics.fallBenefit[4, 2];
                            yes -= temp3;
                            break;
                        case 6:
                            yes += Politics.cPartySeats * (Politics.riseImportTax[0, 0] + importOffset);
                            yes += Politics.gPartySeats * (Politics.riseImportTax[1, 0] + importOffset);
                            yes += Politics.sPartySeats * (Politics.riseImportTax[2, 0] + importOffset);
                            yes += Politics.lPartySeats * (Politics.riseImportTax[3, 0] + importOffset);
                            yes += Politics.nPartySeats * (Politics.riseImportTax[4, 0] + importOffset);
                            no += Politics.cPartySeats * (Politics.riseImportTax[0, 1] - importOffset);
                            no += Politics.gPartySeats * (Politics.riseImportTax[1, 1] - importOffset);
                            no += Politics.sPartySeats * (Politics.riseImportTax[2, 1] - importOffset);
                            no += Politics.lPartySeats * (Politics.riseImportTax[3, 1] - importOffset);
                            no += Politics.nPartySeats * (Politics.riseImportTax[4, 1] - importOffset);
                            noAttend += Politics.cPartySeats * Politics.riseImportTax[0, 2];
                            noAttend += Politics.gPartySeats * Politics.riseImportTax[1, 2];
                            noAttend += Politics.sPartySeats * Politics.riseImportTax[2, 2];
                            noAttend += Politics.lPartySeats * Politics.riseImportTax[3, 2];
                            noAttend += Politics.nPartySeats * Politics.riseImportTax[4, 2];
                            yes -= temp3;
                            break;
                        case 7:
                            yes += Politics.cPartySeats * (Politics.fallImportTax[0, 0] - importOffset);
                            yes += Politics.gPartySeats * (Politics.fallImportTax[1, 0] - importOffset);
                            yes += Politics.sPartySeats * (Politics.fallImportTax[2, 0] - importOffset);
                            yes += Politics.lPartySeats * (Politics.fallImportTax[3, 0] - importOffset);
                            yes += Politics.nPartySeats * (Politics.fallImportTax[4, 0] - importOffset);
                            no += Politics.cPartySeats * (Politics.fallImportTax[0, 1] + importOffset);
                            no += Politics.gPartySeats * (Politics.fallImportTax[1, 1] + importOffset);
                            no += Politics.sPartySeats * (Politics.fallImportTax[2, 1] + importOffset);
                            no += Politics.lPartySeats * (Politics.fallImportTax[3, 1] + importOffset);
                            no += Politics.nPartySeats * (Politics.fallImportTax[4, 1] + importOffset);
                            noAttend += Politics.cPartySeats * Politics.fallImportTax[0, 2];
                            noAttend += Politics.gPartySeats * Politics.fallImportTax[1, 2];
                            noAttend += Politics.sPartySeats * Politics.fallImportTax[2, 2];
                            noAttend += Politics.lPartySeats * Politics.fallImportTax[3, 2];
                            noAttend += Politics.nPartySeats * Politics.fallImportTax[4, 2];
                            yes += temp3;
                            break;
                        case 8:
                            yes += Politics.cPartySeats * (Politics.riseStateOwned[0, 0] + stateOwnedOffset);
                            yes += Politics.gPartySeats * (Politics.riseStateOwned[1, 0] + stateOwnedOffset);
                            yes += Politics.sPartySeats * (Politics.riseStateOwned[2, 0] + stateOwnedOffset);
                            yes += Politics.lPartySeats * (Politics.riseStateOwned[3, 0] + stateOwnedOffset);
                            yes += Politics.nPartySeats * (Politics.riseStateOwned[4, 0] + stateOwnedOffset);
                            no += Politics.cPartySeats * (Politics.riseStateOwned[0, 1] - stateOwnedOffset);
                            no += Politics.gPartySeats * (Politics.riseStateOwned[1, 1] - stateOwnedOffset);
                            no += Politics.sPartySeats * (Politics.riseStateOwned[2, 1] - stateOwnedOffset);
                            no += Politics.lPartySeats * (Politics.riseStateOwned[3, 1] - stateOwnedOffset);
                            no += Politics.nPartySeats * (Politics.riseStateOwned[4, 1] - stateOwnedOffset);
                            noAttend += Politics.cPartySeats * Politics.riseStateOwned[0, 2];
                            noAttend += Politics.gPartySeats * Politics.riseStateOwned[1, 2];
                            noAttend += Politics.sPartySeats * Politics.riseStateOwned[2, 2];
                            noAttend += Politics.lPartySeats * Politics.riseStateOwned[3, 2];
                            noAttend += Politics.nPartySeats * Politics.riseStateOwned[4, 2];
                            yes -= temp3;
                            break;
                        case 9:
                            yes += Politics.cPartySeats * (Politics.fallStateOwned[0, 0] - stateOwnedOffset);
                            yes += Politics.gPartySeats * (Politics.fallStateOwned[1, 0] - stateOwnedOffset);
                            yes += Politics.sPartySeats * (Politics.fallStateOwned[2, 0] - stateOwnedOffset);
                            yes += Politics.lPartySeats * (Politics.fallStateOwned[3, 0] - stateOwnedOffset);
                            yes += Politics.nPartySeats * (Politics.fallStateOwned[4, 0] - stateOwnedOffset);
                            no += Politics.cPartySeats * (Politics.fallStateOwned[0, 1] + stateOwnedOffset);
                            no += Politics.gPartySeats * (Politics.fallStateOwned[1, 1] + stateOwnedOffset);
                            no += Politics.sPartySeats * (Politics.fallStateOwned[2, 1] + stateOwnedOffset);
                            no += Politics.lPartySeats * (Politics.fallStateOwned[3, 1] + stateOwnedOffset);
                            no += Politics.nPartySeats * (Politics.fallStateOwned[4, 1] + stateOwnedOffset);
                            noAttend += Politics.cPartySeats * Politics.fallStateOwned[0, 2];
                            noAttend += Politics.gPartySeats * Politics.fallStateOwned[1, 2];
                            noAttend += Politics.sPartySeats * Politics.fallStateOwned[2, 2];
                            noAttend += Politics.lPartySeats * Politics.fallStateOwned[3, 2];
                            noAttend += Politics.nPartySeats * Politics.fallStateOwned[4, 2];
                            yes += temp3;
                            break;
                        case 10:
                            yes += Politics.cPartySeats * Politics.allowGarbage[0, 0];
                            yes += Politics.gPartySeats * Politics.allowGarbage[1, 0];
                            yes += Politics.sPartySeats * Politics.allowGarbage[2, 0];
                            yes += Politics.lPartySeats * Politics.allowGarbage[3, 0];
                            yes += Politics.nPartySeats * Politics.allowGarbage[4, 0];
                            no += Politics.cPartySeats * Politics.allowGarbage[0, 1];
                            no += Politics.gPartySeats * Politics.allowGarbage[1, 1];
                            no += Politics.sPartySeats * Politics.allowGarbage[2, 1];
                            no += Politics.lPartySeats * Politics.allowGarbage[3, 1];
                            no += Politics.nPartySeats * Politics.allowGarbage[4, 1];
                            noAttend += Politics.cPartySeats * Politics.allowGarbage[0, 2];
                            noAttend += Politics.gPartySeats * Politics.allowGarbage[1, 2];
                            noAttend += Politics.sPartySeats * Politics.allowGarbage[2, 2];
                            noAttend += Politics.lPartySeats * Politics.allowGarbage[3, 2];
                            noAttend += Politics.nPartySeats * Politics.allowGarbage[4, 2];
                            yes -= temp3;
                            break;
                        case 11:
                            yes += Politics.cPartySeats * Politics.notAllowGarbage[0, 0];
                            yes += Politics.gPartySeats * Politics.notAllowGarbage[1, 0];
                            yes += Politics.sPartySeats * Politics.notAllowGarbage[2, 0];
                            yes += Politics.lPartySeats * Politics.notAllowGarbage[3, 0];
                            yes += Politics.nPartySeats * Politics.notAllowGarbage[4, 0];
                            no += Politics.cPartySeats * Politics.notAllowGarbage[0, 1];
                            no += Politics.gPartySeats * Politics.notAllowGarbage[1, 1];
                            no += Politics.sPartySeats * Politics.notAllowGarbage[2, 1];
                            no += Politics.lPartySeats * Politics.notAllowGarbage[3, 1];
                            no += Politics.nPartySeats * Politics.notAllowGarbage[4, 1];
                            noAttend += Politics.cPartySeats * Politics.notAllowGarbage[0, 2];
                            noAttend += Politics.gPartySeats * Politics.notAllowGarbage[1, 2];
                            noAttend += Politics.sPartySeats * Politics.notAllowGarbage[2, 2];
                            noAttend += Politics.lPartySeats * Politics.notAllowGarbage[3, 2];
                            noAttend += Politics.nPartySeats * Politics.notAllowGarbage[4, 2];
                            yes += temp3;
                            break;
                        case 12:
                            yes += Politics.cPartySeats * (Politics.riseLandRent[0, 0] + landRentOffset);
                            yes += Politics.gPartySeats * (Politics.riseLandRent[1, 0] + landRentOffset);
                            yes += Politics.sPartySeats * (Politics.riseLandRent[2, 0] + landRentOffset);
                            yes += Politics.lPartySeats * (Politics.riseLandRent[3, 0] + landRentOffset);
                            yes += Politics.nPartySeats * (Politics.riseLandRent[4, 0] + landRentOffset);
                            no += Politics.cPartySeats * (Politics.riseLandRent[0, 1] - landRentOffset);
                            no += Politics.gPartySeats * (Politics.riseLandRent[1, 1] - landRentOffset);
                            no += Politics.sPartySeats * (Politics.riseLandRent[2, 1] - landRentOffset);
                            no += Politics.lPartySeats * (Politics.riseLandRent[3, 1] - landRentOffset);
                            no += Politics.nPartySeats * (Politics.riseLandRent[4, 1] - landRentOffset);
                            noAttend += Politics.cPartySeats * Politics.riseLandRent[0, 2];
                            noAttend += Politics.gPartySeats * Politics.riseLandRent[1, 2];
                            noAttend += Politics.sPartySeats * Politics.riseLandRent[2, 2];
                            noAttend += Politics.lPartySeats * Politics.riseLandRent[3, 2];
                            noAttend += Politics.nPartySeats * Politics.riseLandRent[4, 2];
                            yes -= temp3;
                            break;
                        case 13:
                            yes += Politics.cPartySeats * (Politics.fallLandRent[0, 0] - landRentOffset);
                            yes += Politics.gPartySeats * (Politics.fallLandRent[1, 0] - landRentOffset);
                            yes += Politics.sPartySeats * (Politics.fallLandRent[2, 0] - landRentOffset);
                            yes += Politics.lPartySeats * (Politics.fallLandRent[3, 0] - landRentOffset);
                            yes += Politics.nPartySeats * (Politics.fallLandRent[4, 0] - landRentOffset);
                            no += Politics.cPartySeats * (Politics.fallLandRent[0, 1] + landRentOffset);
                            no += Politics.gPartySeats * (Politics.fallLandRent[1, 1] + landRentOffset);
                            no += Politics.sPartySeats * (Politics.fallLandRent[2, 1] + landRentOffset);
                            no += Politics.lPartySeats * (Politics.fallLandRent[3, 1] + landRentOffset);
                            no += Politics.nPartySeats * (Politics.fallLandRent[4, 1] + landRentOffset);
                            noAttend += Politics.cPartySeats * Politics.fallLandRent[0, 2];
                            noAttend += Politics.gPartySeats * Politics.fallLandRent[1, 2];
                            noAttend += Politics.sPartySeats * Politics.fallLandRent[2, 2];
                            noAttend += Politics.lPartySeats * Politics.fallLandRent[3, 2];
                            noAttend += Politics.nPartySeats * Politics.fallLandRent[4, 2];
                            yes += temp3;
                            break;
                    }


                    yes += temp2;
                    int temp1 = yes + no + noAttend;


                    yes = (int)(yes * 99f / temp1);
                    no = (int)(no * 99f / temp1);
                    noAttend = (int)(noAttend * 99f / temp1);

                    temp1 = yes + no + noAttend;
                    if (temp1 < 99)
                    {
                        System.Random rand = new System.Random();
                        switch (rand.Next(3))
                        {
                            case 0:
                                yes += 99 - temp1; break;
                            case 1:
                                no += 99 - temp1; break;
                            case 2:
                                noAttend += 99 - temp1; break;
                        }
                    }

                    Politics.currentYes = (byte)yes;
                    Politics.currentNo = (byte)no;
                    Politics.currentNoAttend = (byte)noAttend;

                    if (Politics.currentYes >= 50)
                    {
                        switch (idex)
                        {
                            case 0:
                                Politics.salaryTaxOffset += 0.01f; break;
                            case 1:
                                Politics.salaryTaxOffset -= 0.01f; break;
                            case 4:
                                Politics.tradeTaxOffset += 0.01f; break;
                            case 5:
                                Politics.tradeTaxOffset -= 0.01f; break;
                            case 2:
                                Politics.benefitOffset += 1; break;
                            case 3:
                                Politics.benefitOffset -= 1; break;
                            case 6:
                                Politics.importTaxOffset += 0.04f; break;
                            case 7:
                                Politics.importTaxOffset -= 0.04f; break;
                            case 8:
                                Politics.stateOwnedPercent += 5; break;
                            case 9:
                                Politics.stateOwnedPercent -= 5; break;
                            case 10:
                                Politics.isOutSideGarbagePermit = false; break;
                            case 11:
                                Politics.isOutSideGarbagePermit = true; break;
                            case 12:
                                Politics.landRentOffset += 1; break;
                            case 13:
                                Politics.landRentOffset -= 1; break;
                        }
                    }
                }
            }

            public void CreateGoverment()
            {
                if (Politics.cPartySeats >= 50)
                {
                    //c only
                    Politics.case1 = true;
                    Politics.case2 = false;
                    Politics.case3 = false;
                    Politics.case4 = false;
                    Politics.case5 = false;
                    Politics.case6 = false;
                    Politics.case7 = false;
                    Politics.case8 = false;

                } 
                else if (Politics.gPartySeats >= 50)
                {
                    Politics.case1 = false;
                    Politics.case2 = true;
                    Politics.case3 = false;
                    Politics.case4 = false;
                    Politics.case5 = false;
                    Politics.case6 = false;
                    Politics.case7 = false;
                    Politics.case8 = false;
                }
                else if (Politics.sPartySeats >= 50)
                {
                    Politics.case1 = false;
                    Politics.case2 = false;
                    Politics.case3 = true;
                    Politics.case4 = false;
                    Politics.case5 = false;
                    Politics.case6 = false;
                    Politics.case7 = false;
                    Politics.case8 = false;
                }
                else if (Politics.lPartySeats >= 50)
                {
                    Politics.case1 = false;
                    Politics.case2 = false;
                    Politics.case3 = false;
                    Politics.case4 = true;
                    Politics.case5 = false;
                    Politics.case6 = false;
                    Politics.case7 = false;
                    Politics.case8 = false;
                }
                else if (Politics.nPartySeats >= 50)
                {
                    Politics.case1 = false;
                    Politics.case2 = false;
                    Politics.case3 = false;
                    Politics.case4 = false;
                    Politics.case5 = true;
                    Politics.case6 = false;
                    Politics.case7 = false;
                    Politics.case8 = false;
                }
                else if (Politics.sPartySeats + Politics.gPartySeats >= 50)
                {
                    Politics.case1 = false;
                    Politics.case2 = false;
                    Politics.case3 = false;
                    Politics.case4 = false;
                    Politics.case5 = false;
                    Politics.case6 = true;
                    Politics.case7 = false;
                    Politics.case8 = false;
                }
                else if (Politics.sPartySeats + Politics.gPartySeats + Politics.cPartySeats >= 50)
                {
                    Politics.case1 = false;
                    Politics.case2 = false;
                    Politics.case3 = false;
                    Politics.case4 = false;
                    Politics.case5 = false;
                    Politics.case6 = false;
                    Politics.case7 = true;
                    Politics.case8 = false;
                }
                else if (Politics.nPartySeats + Politics.lPartySeats >= 50)
                {
                    Politics.case1 = false;
                    Politics.case2 = false;
                    Politics.case3 = false;
                    Politics.case4 = false;
                    Politics.case5 = false;
                    Politics.case6 = false;
                    Politics.case7 = false;
                    Politics.case8 = true;
                }
                else
                {
                    Politics.case1 = false;
                    Politics.case2 = false;
                    Politics.case3 = false;
                    Politics.case4 = false;
                    Politics.case5 = false;
                    Politics.case6 = false;
                    Politics.case7 = false;
                    Politics.case8 = false;
                }
            }

            public void GetSeats(bool isPolls)
            {
                if (!isPolls)
                {
                    int temp = Politics.cPartyTickets + Politics.gPartyTickets + Politics.sPartyTickets + Politics.lPartyTickets + Politics.nPartyTickets;
                    if (temp != 0)
                    {
                        Politics.cPartySeats = (ushort)(99 * Politics.cPartyTickets / temp);
                        Politics.gPartySeats = (ushort)(99 * Politics.gPartyTickets / temp);
                        Politics.sPartySeats = (ushort)(99 * Politics.sPartyTickets / temp);
                        Politics.lPartySeats = (ushort)(99 * Politics.lPartyTickets / temp);
                        Politics.nPartySeats = (ushort)(99 * Politics.nPartyTickets / temp);
                    }
                    else
                    {
                        Politics.cPartySeats = 0;
                        Politics.gPartySeats = 0;
                        Politics.sPartySeats = 0;
                        Politics.lPartySeats = 0;
                        Politics.nPartySeats = 0;
                    }
                    Politics.cPartyTickets = 0;
                    Politics.gPartyTickets = 0;
                    Politics.sPartyTickets = 0;
                    Politics.lPartyTickets = 0;
                    Politics.nPartyTickets = 0;

                    temp = Politics.cPartySeats + Politics.gPartySeats + Politics.sPartySeats + Politics.lPartySeats + Politics.nPartySeats;
                    if (temp < 99)
                    {
                        System.Random rand = new System.Random();
                        switch (rand.Next(5))
                        {
                            case 0:
                                Politics.cPartySeats += (ushort)(99 - temp); break;
                            case 1:
                                Politics.gPartySeats += (ushort)(99 - temp); break;
                            case 2:
                                Politics.sPartySeats += (ushort)(99 - temp); break;
                            case 3:
                                Politics.lPartySeats += (ushort)(99 - temp); break;
                            case 4:
                                Politics.nPartySeats += (ushort)(99 - temp); break;
                        }
                    }
                }
                else
                {
                    float temp = Politics.cPartySeatsPolls + Politics.gPartySeatsPolls + Politics.sPartySeatsPolls + Politics.lPartySeatsPolls + Politics.nPartySeatsPolls;
                    if (temp != 0)
                    {
                        Politics.cPartySeatsPollsFinal = (float)(100 * Politics.cPartySeatsPolls / temp);
                        Politics.gPartySeatsPollsFinal = (float)(100 * Politics.gPartySeatsPolls / temp);
                        Politics.sPartySeatsPollsFinal = (float)(100 * Politics.sPartySeatsPolls / temp);
                        Politics.lPartySeatsPollsFinal = (float)(100 * Politics.lPartySeatsPolls / temp);
                        Politics.nPartySeatsPollsFinal = (float)(100 * Politics.nPartySeatsPolls / temp);
                    }
                    else
                    {
                        Politics.cPartySeatsPollsFinal = 0;
                        Politics.gPartySeatsPollsFinal = 0;
                        Politics.sPartySeatsPollsFinal = 0;
                        Politics.lPartySeatsPollsFinal = 0;
                        Politics.nPartySeatsPollsFinal = 0;
                    }

                    Politics.cPartyTickets = 0;
                    Politics.gPartyTickets = 0;
                    Politics.sPartyTickets = 0;
                    Politics.lPartyTickets = 0;
                    Politics.nPartyTickets = 0;
                    Politics.cPartySeatsPolls = 0f;
                    Politics.gPartySeatsPolls = 0f;
                    Politics.sPartySeatsPolls = 0f;
                    Politics.lPartySeatsPolls = 0f;
                    Politics.nPartySeatsPolls = 0f;                    
                }
            }
        }

        public class ThreadingRealCityStatsMod : ThreadingExtensionBase
        {

            public override void OnBeforeSimulationFrame()
            {
                base.OnBeforeSimulationFrame();
                if (Loader.CurrentLoadMode == LoadMode.LoadGame || Loader.CurrentLoadMode == LoadMode.NewGame)
                {
                    uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
                    int num4 = (int)(currentFrameIndex & 255u);
                    int num5 = num4 * 192;
                    int num6 = (num4 + 1) * 192 - 1;
                    //DebugLog.LogToFileOnly("currentFrameIndex num2 = " + currentFrameIndex.ToString());
                    BuildingManager instance = Singleton<BuildingManager>.instance;


                    for (int i = num5; i <= num6; i = i + 1)
                    {
                        if (instance.m_buildings.m_buffer[i].Info.m_buildingAI is OutsideConnectionAI)
                        {
                            //DebugLog.LogToFileOnly("find outside building");

                            if (comm_data.isHellMode)
                            {
                                //DebugLog.LogToFileOnly("hell mode, little import pre = " + instance.m_buildings.m_buffer[i].m_teens.ToString());
                                instance.m_buildings.m_buffer[i].m_teens = 0;
                                //DebugLog.LogToFileOnly("hell mode, little import = " + instance.m_buildings.m_buffer[i].m_teens.ToString());
                            }
                            ProcessOutsideDemand((ushort)i, ref instance.m_buildings.m_buffer[i]);
                            AddGarbageOffers((ushort)i, ref instance.m_buildings.m_buffer[i]);
                        }
                    }
                }
            }

            public void ProcessOutsideDemand(ushort buildingID, ref Building data)
            {
                if (data.Info.m_class.m_service == ItemClass.Service.Road)
                {
                    if (Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.Garbage))
                    {
                        if ((data.m_flags & Building.Flags.IncomingOutgoing) == Building.Flags.Incoming)
                        {
                            data.m_garbageBuffer = (ushort)(data.m_garbageBuffer + 40);
                        }
                        else
                        {
                            data.m_garbageBuffer = (ushort)(data.m_garbageBuffer + 20);
                        }
                    }

                    if (data.m_garbageBuffer > 20000)
                    {
                        data.m_garbageBuffer = 20000;
                    }
                }
                else if (RealCity.updateOnce && (data.m_garbageBuffer != 0))
                {
                    data.m_garbageBuffer = 0;
                    TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                    offer.Building = buildingID;
                    Singleton<TransferManager>.instance.RemoveOutgoingOffer(TransferManager.TransferReason.Garbage, offer);
                    Singleton<TransferManager>.instance.RemoveOutgoingOffer(TransferManager.TransferReason.GarbageMove, offer);
                }
                else
                {
                    data.m_garbageBuffer = 0;
                }
            }


            public void AddGarbageOffers(ushort buildingID, ref Building data)
            {
                TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);

                if (!Politics.isOutSideGarbagePermit && pc_OutsideConnectionAI.haveGarbageBuildingFinal && Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.Garbage))
                {
                    if ((data.m_flags & Building.Flags.IncomingOutgoing) == Building.Flags.Incoming)
                    {
                        int car_valid_path = TickPathfindStatus(ref data.m_education3, ref data.m_adults);
                        SimulationManager instance1 = Singleton<SimulationManager>.instance;
                        if (car_valid_path + instance1.m_randomizer.Int32(256u) >> 8 == 0)
                        {
                            if (instance1.m_randomizer.Int32(128u) == 0)
                            {
                                DebugLog.LogToFileOnly("outside connection is not good for car in for garbageoffers");
                                int num24 = (int)data.m_garbageBuffer;
                                if (num24 >= 200 && Singleton<SimulationManager>.instance.m_randomizer.Int32(5u) == 0 && Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.Garbage))
                                {
                                    int num25 = 0;
                                    int num26 = 0;
                                    int num27 = 0;
                                    int num28 = 0;
                                    this.CalculateGuestVehicles(buildingID, ref data, TransferManager.TransferReason.Garbage, ref num25, ref num26, ref num27, ref num28);
                                    num24 -= num27 - num26;
                                    //DebugLog.LogToFileOnly("caculate num24  = " + num24.ToString() + "num27 = " + num27.ToString() + "num26 = " + num26.ToString());
                                    if (num24 >= 200)
                                    {
                                        offer = default(TransferManager.TransferOffer);
                                        offer.Priority = num24 / 1000;
                                        if (offer.Priority > 7)
                                        {
                                            offer.Priority = 7;
                                        }
                                        offer.Building = buildingID;
                                        offer.Position = data.m_position;
                                        offer.Amount = 1;
                                        offer.Active = false;
                                        Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Garbage, offer);
                                    }
                                }
                            }
                        }
                        else
                        {
                            int num24 = (int)data.m_garbageBuffer;
                            if (num24 >= 200 && Singleton<SimulationManager>.instance.m_randomizer.Int32(5u) == 0 && Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.Garbage))
                            {
                                int num25 = 0;
                                int num26 = 0;
                                int num27 = 0;
                                int num28 = 0;
                                CalculateGuestVehicles(buildingID, ref data, TransferManager.TransferReason.Garbage, ref num25, ref num26, ref num27, ref num28);
                                num24 -= num27 - num26;
                                //DebugLog.LogToFileOnly("caculate num24  = " + num24.ToString() + "num27 = " + num27.ToString() + "num26 = " + num26.ToString());
                                if (num24 >= 200)
                                {
                                    offer = default(TransferManager.TransferOffer);
                                    offer.Priority = num24 / 1000;
                                    if (offer.Priority > 7)
                                    {
                                        offer.Priority = 7;
                                    }
                                    offer.Building = buildingID;
                                    offer.Position = data.m_position;
                                    offer.Amount = 1;
                                    offer.Active = false;
                                    Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Garbage, offer);
                                }
                            }
                        }
                    }
                    else
                    {
                        int car_valid_path = TickPathfindStatus(ref data.m_teens, ref data.m_serviceProblemTimer);
                        SimulationManager instance1 = Singleton<SimulationManager>.instance;
                        if (car_valid_path + instance1.m_randomizer.Int32(256u) >> 8 == 0)
                        {
                            if (instance1.m_randomizer.Int32(32u) == 0)
                            {
                                //DebugLog.LogToFileOnly("outside connection is not good for car out for garbagemoveoffers");
                                if (instance1.m_randomizer.Int32(data.m_garbageBuffer) > 4000)
                                {
                                    offer = default(TransferManager.TransferOffer);
                                    offer.Priority = 1 + data.m_garbageBuffer / 5000;
                                    if (offer.Priority > 7)
                                    {
                                        offer.Priority = 7;
                                    }
                                    offer.Building = buildingID;
                                    offer.Position = data.m_position;
                                    offer.Amount = 1;
                                    offer.Active = true;
                                    Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.GarbageMove, offer);
                                }
                            }
                        }
                        else
                        {
                            int num25 = 0;
                            int num26 = 0;
                            int num27 = 0;
                            int num28 = 0;
                            this.CalculateOwnVehicles(buildingID, ref data, TransferManager.TransferReason.GarbageMove, ref num25, ref num26, ref num27, ref num28);
                            if (num25 < 100)
                            {
                                if (data.m_garbageBuffer > 12000)
                                {
                                    offer = default(TransferManager.TransferOffer);
                                    offer.Priority = 1 + data.m_garbageBuffer / 5000;
                                    if (offer.Priority > 7)
                                    {
                                        offer.Priority = 7;
                                    }
                                    offer.Building = buildingID;
                                    offer.Position = data.m_position;
                                    offer.Amount = 1;
                                    offer.Active = true;
                                    Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.GarbageMove, offer);
                                }
                            }
                        }
                    }
                }
            }


            protected void CalculateOwnVehicles(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int count, ref int cargo, ref int capacity, ref int outside)
            {
                VehicleManager instance = Singleton<VehicleManager>.instance;
                ushort num = data.m_ownVehicles;
                int num2 = 0;
                while (num != 0)
                {
                    if ((TransferManager.TransferReason)instance.m_vehicles.m_buffer[(int)num].m_transferType == material)
                    {
                        VehicleInfo info = instance.m_vehicles.m_buffer[(int)num].Info;
                        int a;
                        int num3;
                        info.m_vehicleAI.GetSize(num, ref instance.m_vehicles.m_buffer[(int)num], out a, out num3);
                        cargo += Mathf.Min(a, num3);
                        capacity += num3;
                        count++;
                        if ((instance.m_vehicles.m_buffer[(int)num].m_flags & (Vehicle.Flags.Importing | Vehicle.Flags.Exporting)) != (Vehicle.Flags)0)
                        {
                            outside++;
                        }
                    }
                    num = instance.m_vehicles.m_buffer[(int)num].m_nextOwnVehicle;
                    if (++num2 > 16384)
                    {
                        CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                        break;
                    }
                }
            }

            // OutsideConnectionAI
            private static int TickPathfindStatus(ref byte success, ref byte failure)
            {
                int result = ((int)success << 8) / Mathf.Max(1, (int)(success + failure));
                if (success > failure)
                {
                    success = (byte)(success + 1 >> 1);
                    failure = (byte)(failure >> 1);
                }
                else
                {
                    success = (byte)(success >> 1);
                    failure = (byte)(failure + 1 >> 1);
                }
                return result;
            }


            public override void OnAfterSimulationFrame()
            {
                base.OnAfterSimulationFrame();
                if (Loader.CurrentLoadMode == LoadMode.LoadGame || Loader.CurrentLoadMode == LoadMode.NewGame)
                {
                    uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
                    int num4 = (int)(currentFrameIndex & 255u);
                    int num5 = num4 * 192;
                    int num6 = (num4 + 1) * 192 - 1;
                    //DebugLog.LogToFileOnly("currentFrameIndex num2 = " + currentFrameIndex.ToString());
                    BuildingManager instance = Singleton<BuildingManager>.instance;


                    for (int i = num5; i <= num6; i = i + 1)
                    {
                        if (instance.m_buildings.m_buffer[i].m_flags.IsFlagSet(Building.Flags.Created) && (instance.m_buildings.m_buffer[i].m_productionRate != 0) && !instance.m_buildings.m_buffer[i].m_flags.IsFlagSet(Building.Flags.Deleted) && !instance.m_buildings.m_buffer[i].m_flags.IsFlagSet(Building.Flags.Untouchable))
                        {
                            if (RealCity.EconomyExtension.IsSpecialBuilding((ushort)i) == 3)
                            {
                                comm_data.haveCityResourceDepartment = true;
                                ProcessCityResourceDepartmentBuilding((ushort)i, instance.m_buildings.m_buffer[i]);
                            }

                            if (instance.m_buildings.m_buffer[i].Info.m_class.m_service == ItemClass.Service.Garbage)
                            {
                                pc_OutsideConnectionAI.haveGarbageBuilding = true;
                            }
                        }
                    }


                    int num7 = (int)(currentFrameIndex & 15u);
                    int num8 = num7 * 1024;
                    int num9 = (num7 + 1) * 1024 - 1;
                    //DebugLog.LogToFileOnly("currentFrameIndex num2 = " + currentFrameIndex.ToString());
                    VehicleManager instance1 = Singleton<VehicleManager>.instance;
                    for (int i = num8; i <= num9; i = i + 1)
                    {
                        VehicleStatus(i, currentFrameIndex);
                    }
                }
            }


            public void VehicleStatus(int i, uint currentFrameIndex)
            {
                VehicleManager instance = Singleton<VehicleManager>.instance;
                //System.Random rand = new System.Random();
                Vehicle vehicle = instance.m_vehicles.m_buffer[i];
                int num4 = (int)(currentFrameIndex & 255u);
                if (vehicle.m_flags.IsFlagSet(Vehicle.Flags.Created) && !vehicle.m_flags.IsFlagSet(Vehicle.Flags.Deleted))
                {
                    if ((vehicle.Info.m_vehicleType == VehicleInfo.VehicleType.Car) && (vehicle.Info.m_class.m_subService != ItemClass.SubService.PublicTransportTaxi))
                    {
                        if (!vehicle.m_flags.IsFlagSet(Vehicle.Flags.Stopped))
                        {
                            comm_data.vehical_transfer_time[i] = (ushort)(comm_data.vehical_transfer_time[i] + 1);
                            if (num4 >= 240)
                            {
                                if ((TransferManager.TransferReason)vehicle.m_transferType != TransferManager.TransferReason.DummyCar && (TransferManager.TransferReason)vehicle.m_transferType != TransferManager.TransferReason.DummyPlane && (TransferManager.TransferReason)vehicle.m_transferType != TransferManager.TransferReason.DummyTrain && (TransferManager.TransferReason)vehicle.m_transferType != TransferManager.TransferReason.DummyShip)
                                {
                                    if (vehicle.Info.m_vehicleAI is PoliceCarAI || vehicle.Info.m_vehicleAI is DisasterResponseVehicleAI || vehicle.Info.m_vehicleAI is HearseAI)
                                    {
                                        comm_data.allVehicles++;
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)3000, vehicle.Info.m_class);
                                    }
                                    else if (vehicle.Info.m_vehicleAI is GarbageTruckAI || vehicle.Info.m_vehicleAI is FireTruckAI || vehicle.Info.m_vehicleAI is MaintenanceTruckAI)
                                    {
                                        if (vehicle.m_flags.IsFlagSet(Vehicle.Flags.Importing))
                                        {
                                            //comm_data.allVehicles += 1;
                                            //Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)6000, vehicle.Info.m_class);
                                        }
                                        else
                                        {
                                            comm_data.allVehicles += 2;
                                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)15000, vehicle.Info.m_class);
                                        }
                                    }
                                    else if (vehicle.Info.m_vehicleAI is BusAI || vehicle.Info.m_vehicleAI is AmbulanceAI || vehicle.Info.m_vehicleAI is SnowTruckAI || vehicle.Info.m_vehicleAI is ParkMaintenanceVehicleAI || vehicle.Info.m_vehicleAI is WaterTruckAI)
                                    {
                                        comm_data.allVehicles += 2;
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)10000, vehicle.Info.m_class);
                                    }
                                    else if (vehicle.Info.m_vehicleAI is PassengerShipAI || vehicle.Info.m_vehicleAI is PassengerFerryAI || vehicle.Info.m_vehicleAI is CargoShipAI)
                                    {
                                        comm_data.allVehicles += 4;
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)25000, vehicle.Info.m_class);
                                    }
                                    else if (vehicle.Info.m_vehicleAI is PassengerPlaneAI || vehicle.Info.m_vehicleAI is PassengerBlimpAI)
                                    {
                                        comm_data.allVehicles += 8;
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)60000, vehicle.Info.m_class);
                                    }
                                    else if (vehicle.Info.m_vehicleAI is PassengerTrainAI || vehicle.Info.m_vehicleAI is CargoTrainAI)
                                    {
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)50000, vehicle.Info.m_class);
                                    }
                                    else if (vehicle.Info.m_vehicleAI is MetroTrainAI)
                                    {
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)40000, vehicle.Info.m_class);
                                    }
                                    else if (vehicle.Info.m_vehicleAI is PoliceCopterAI || vehicle.Info.m_vehicleAI is FireCopterAI || vehicle.Info.m_vehicleAI is DisasterResponseCopterAI || vehicle.Info.m_vehicleAI is AmbulanceCopterAI)
                                    {
                                        comm_data.allVehicles += 8;
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)60000, vehicle.Info.m_class);
                                    }
                                    else if (vehicle.Info.m_vehicleAI is CableCarAI || vehicle.Info.m_vehicleAI is TramAI)
                                    {
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)20000, vehicle.Info.m_class);
                                    }
                                }
                            }
                        }
                        else
                        {
                            comm_data.vehical_transfer_time[i] = 0;
                        }
                    }
                }
                else
                {
                    comm_data.vehical_transfer_time[i] = 0;
                }
            }


            void ProcessCityResourceDepartmentBuilding(ushort buildingID, Building buildingData)
            {
                int num27 = 0;
                int num28 = 0;
                int num29 = 0;
                int value = 0;
                int num34 = 0;
                TransferManager.TransferReason incomingTransferReason = default(TransferManager.TransferReason);

                //Foods
                incomingTransferReason = TransferManager.TransferReason.Food;
                if (incomingTransferReason != TransferManager.TransferReason.None)
                {
                    CalculateGuestVehicles(buildingID, ref buildingData, incomingTransferReason, ref num27, ref num28, ref num29, ref value);
                    buildingData.m_tempImport = (byte)Mathf.Clamp(value, (int)buildingData.m_tempImport, 255);
                }

                num34 = 18000 - comm_data.building_buffer3[buildingID] - num29;
                if (num34 >= 0)
                {
                    TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                    offer.Priority = num34 / 1000;
                    if (offer.Priority > 7)
                    {
                        offer.Priority = 7;
                    }
                    offer.Building = buildingID;
                    offer.Position = buildingData.m_position;
                    offer.Amount = 1;
                    offer.Active = false;
                    Singleton<TransferManager>.instance.AddIncomingOffer(incomingTransferReason, offer);
                }

                if (comm_data.building_buffer3[buildingID] > 0)
                {
                    if (foodStillNeeded >= 1)
                    {
                        if (comm_data.building_buffer3[buildingID] - (foodStillNeeded) > 0)
                        {
                            comm_data.building_buffer3[buildingID] -= (ushort)(foodStillNeeded);
                            foodStillNeeded = 0;
                        }
                        else
                        {
                            foodStillNeeded -= comm_data.building_buffer3[buildingID];
                            comm_data.building_buffer3[buildingID] = 0;
                        }
                    }
                }
                comm_data.allFoods += comm_data.building_buffer3[buildingID];

                //Petrol
                incomingTransferReason = TransferManager.TransferReason.Petrol;
                num27 = 0;
                num28 = 0;
                num29 = 0;
                value = 0;
                num34 = 0;
                if (incomingTransferReason != TransferManager.TransferReason.None)
                {
                    CalculateGuestVehicles(buildingID, ref buildingData, incomingTransferReason, ref num27, ref num28, ref num29, ref value);
                    buildingData.m_tempImport = (byte)Mathf.Clamp(value, (int)buildingData.m_tempImport, 255);
                }

                num34 = 18000 - comm_data.building_buffer2[buildingID] - num29;
                if (num34 >= 0)
                {
                    TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                    offer.Priority = num34 / 1000;
                    if (offer.Priority > 7)
                    {
                        offer.Priority = 7;
                    }
                    offer.Building = buildingID;
                    offer.Position = buildingData.m_position;
                    offer.Amount = 1;
                    offer.Active = false;
                    Singleton<TransferManager>.instance.AddIncomingOffer(incomingTransferReason, offer);
                }

                if (comm_data.building_buffer2[buildingID] > 0)
                {
                    if (PetrolStillNeeded >= 1)
                    {
                        if (comm_data.building_buffer2[buildingID] - PetrolStillNeeded > 0)
                        {
                            comm_data.building_buffer2[buildingID] -= (ushort)(PetrolStillNeeded);
                            PetrolStillNeeded = 0;
                        }
                        else
                        {
                            PetrolStillNeeded -= comm_data.building_buffer2[buildingID];
                            comm_data.building_buffer2[buildingID] = 0;
                        }
                    }
                }
                comm_data.allPetrols += comm_data.building_buffer2[buildingID];

                //Coal
                incomingTransferReason = TransferManager.TransferReason.Coal;
                num27 = 0;
                num28 = 0;
                num29 = 0;
                value = 0;
                num34 = 0;
                if (incomingTransferReason != TransferManager.TransferReason.None)
                {
                    CalculateGuestVehicles(buildingID, ref buildingData, incomingTransferReason, ref num27, ref num28, ref num29, ref value);
                    buildingData.m_tempImport = (byte)Mathf.Clamp(value, (int)buildingData.m_tempImport, 255);
                }

                num34 = 18000 - comm_data.building_buffer1[buildingID] - num29;
                if (num34 >= 0)
                {
                    TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                    offer.Priority = num34 / 1000;
                    if (offer.Priority > 7)
                    {
                        offer.Priority = 7;
                    }
                    offer.Building = buildingID;
                    offer.Position = buildingData.m_position;
                    offer.Amount = 1;
                    offer.Active = false;
                    Singleton<TransferManager>.instance.AddIncomingOffer(incomingTransferReason, offer);
                }

                if (comm_data.building_buffer1[buildingID] > 0)
                {
                    if (coalStillNeeded >= 1)
                    {
                        if (comm_data.building_buffer1[buildingID] - coalStillNeeded > 0)
                        {
                            comm_data.building_buffer1[buildingID] -= (ushort)(coalStillNeeded);
                            coalStillNeeded = 0;
                        }
                        else
                        {
                            coalStillNeeded -= comm_data.building_buffer1[buildingID];
                            comm_data.building_buffer1[buildingID] = 0;
                        }
                    }
                }
                comm_data.allCoals += comm_data.building_buffer1[buildingID];

                //Lumber
                incomingTransferReason = TransferManager.TransferReason.Lumber;
                num27 = 0;
                num28 = 0;
                num29 = 0;
                value = 0;
                num34 = 0;
                if (incomingTransferReason != TransferManager.TransferReason.None)
                {
                    CalculateGuestVehicles(buildingID, ref buildingData, incomingTransferReason, ref num27, ref num28, ref num29, ref value);
                    buildingData.m_tempImport = (byte)Mathf.Clamp(value, (int)buildingData.m_tempImport, 255);
                }

                num34 = 18000 - comm_data.building_buffer4[buildingID] - num29;
                if (num34 >= 0)
                {
                    TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                    offer.Priority = num34 / 1000;
                    if (offer.Priority > 7)
                    {
                        offer.Priority = 7;
                    }
                    offer.Building = buildingID;
                    offer.Position = buildingData.m_position;
                    offer.Amount = 1;
                    offer.Active = false;
                    Singleton<TransferManager>.instance.AddIncomingOffer(incomingTransferReason, offer);
                }

                if (comm_data.building_buffer4[buildingID] > 0)
                {
                    if (lumberStillNeeded >= 1)
                    {
                        if (comm_data.building_buffer4[buildingID] - (lumberStillNeeded) > 0)
                        {
                            comm_data.building_buffer4[buildingID] -= (ushort)(lumberStillNeeded);
                            lumberStillNeeded = 0;
                        }
                        else
                        {
                            lumberStillNeeded -= comm_data.building_buffer4[buildingID];
                            comm_data.building_buffer4[buildingID] = 0;
                        }
                    }
                }
                comm_data.allLumbers += comm_data.building_buffer4[buildingID];
            }

            protected void CalculateGuestVehicles(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int count, ref int cargo, ref int capacity, ref int outside)
            {
                VehicleManager instance = Singleton<VehicleManager>.instance;
                ushort num = data.m_guestVehicles;
                int num2 = 0;
                while (num != 0)
                {
                    if ((TransferManager.TransferReason)instance.m_vehicles.m_buffer[(int)num].m_transferType == material)
                    {
                        VehicleInfo info = instance.m_vehicles.m_buffer[(int)num].Info;
                        int a;
                        int num3;
                        info.m_vehicleAI.GetSize(num, ref instance.m_vehicles.m_buffer[(int)num], out a, out num3);
                        cargo += Mathf.Min(a, num3);
                        capacity += num3;
                        count++;
                        if ((instance.m_vehicles.m_buffer[(int)num].m_flags & (Vehicle.Flags.Importing | Vehicle.Flags.Exporting)) != (Vehicle.Flags)0)
                        {
                            outside++;
                        }
                    }
                    num = instance.m_vehicles.m_buffer[(int)num].m_nextGuestVehicle;
                    if (++num2 > 16384)
                    {
                        CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                        break;
                    }
                }
            }

        }
    }
}

