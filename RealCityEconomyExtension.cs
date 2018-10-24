using ColossalFramework;
using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RealCity
{
    public class RealCityEconomyExtension : EconomyExtensionBase
    {
        public static int foodStillNeeded = 0;
        public static int lumberStillNeeded = 0;
        public static int coalStillNeeded = 0;
        public static int petrolStillNeeded = 0;
        public static bool updateOnce = false;
        public static byte partyTrend = 0;
        public static ushort partyTrendStrength = 0;

        public static byte citizenStatus = 0;
        public static byte isBuildingNoMaterialCount = 0;
        public static byte isBuildingNoBuyerCount = 0;

        public static byte isStateOwnedCount = 0;

        public static string tip1_message_forgui = "";
        public static string tip2_message_forgui = "";
        public static string tip3_message_forgui = "";
        public static string tip4_message_forgui = "";
        public static string tip5_message_forgui = "";
        public static string tip6_message_forgui = "";
        //public static string tip7_message_forgui = "";
        //public static string tip8_message_forgui = "";
        ////public static string tip9_message_forgui = "";
        //public static string tip10_message_forgui = "";


        public override long OnUpdateMoneyAmount(long internalMoneyAmount)
        {
            if (Loader.CurrentLoadMode == LoadMode.LoadGame || Loader.CurrentLoadMode == LoadMode.NewGame)
            {
                MainDataStore.current_time = Singleton<SimulationManager>.instance.m_currentDayTimeHour;
                uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
                uint num2 = currentFrameIndex & 255u;
                if ((num2 == 255u) && (MainDataStore.current_time != MainDataStore.prev_time))
                {


                    //citizen_status();
                    GenerateTips();

                    MainDataStore.landPrice = Singleton<DistrictManager>.instance.m_districts.m_buffer[0].GetLandValue() / 10f;
                    if (MainDataStore.landPrice < 1f)
                    {
                        MainDataStore.landPrice = 1f;
                    }
                    MainDataStore.game_expense_divide = (byte)((float)100 / MainDataStore.landPrice);
                    MainDataStore.isCoalsGetted = false;
                    MainDataStore.isFoodsGetted = false;
                    MainDataStore.isPetrolsGetted = false;
                    MainDataStore.isLumbersGetted = false;
                    MainDataStore.allFoodsFinal = MainDataStore.allFoods;
                    MainDataStore.allLumbersFinal = MainDataStore.allLumbers;
                    MainDataStore.allCoalsFinal = MainDataStore.allCoals;
                    MainDataStore.allPetrolsFinal = MainDataStore.allPetrols;
                    MainDataStore.allVehiclesFinal = MainDataStore.allVehicles;


                    MainDataStore.isCoalsGettedFinal = (coalStillNeeded <= 0) && (MainDataStore.allCoals != 0);
                    MainDataStore.isFoodsGettedFinal = (foodStillNeeded <= 0) && (MainDataStore.allFoods != 0);
                    MainDataStore.isPetrolsGettedFinal = (petrolStillNeeded <= 0) && (MainDataStore.allPetrols != 0);
                    MainDataStore.isLumbersGettedFinal = (lumberStillNeeded <= 0) && (MainDataStore.allLumbers != 0);
                    foodStillNeeded = (MainDataStore.citizen_count > 16) ? (MainDataStore.citizen_count >> 4) : 0;
                    lumberStillNeeded = (RealCityPrivateBuildingAI.allBuildingsFinal > 8) ? (RealCityPrivateBuildingAI.allBuildingsFinal >> 3) : 0;
                    coalStillNeeded = (RealCityPrivateBuildingAI.allBuildingsFinal > 16) ? (RealCityPrivateBuildingAI.allBuildingsFinal >> 4) : 0;
                    petrolStillNeeded = MainDataStore.allVehiclesFinal;
                    MainDataStore.allVehicles = 0;
                    MainDataStore.allFoods = 0;
                    MainDataStore.allLumbers = 0;
                    MainDataStore.allPetrols = 0;
                    MainDataStore.allCoals = 0;
                    BuildingStatus();

                    if (MainDataStore.update_money_count == 16)
                    {
                        Politics.parliamentCount--;
                        Politics.parliamentMeetingCount--;
                        if (Politics.parliamentCount < 0)
                        {
                            Politics.parliamentCount = 20;
                        }
                        if (Politics.parliamentMeetingCount < 0)
                        {
                            Politics.parliamentMeetingCount = 1;
                        }
                        CitizenStatus();
                    }


                    CaculateCitizenTransportFee();
                    MainDataStore.update_money_count++;
                    if (MainDataStore.update_money_count == 17)
                    {
                        MainDataStore.update_money_count = 0;
                    }
                    RealCityEconomyManager.clean_current(MainDataStore.update_money_count);


                    MainDataStore.prev_time = MainDataStore.current_time;
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
                MainDataStore.is_updated = true;
            }
            return internalMoneyAmount;
        }

        public void GenerateTips()
        {
            tip1_message_forgui = Language.TipAndChirperMessage[5];

            tip2_message_forgui = Language.TipAndChirperMessage[7];

            if (!MainDataStore.haveCityResourceDepartmentFinal)
            {
                tip3_message_forgui = Language.TipAndChirperMessage[8];
                Loader.guiPanel.Show();
            }
            else
            {
                tip3_message_forgui = "";
            }

            if (!MainDataStore.isFoodsGettedFinal)
            {
                tip4_message_forgui = Language.TipAndChirperMessage[9];
            }
            else
            {
                tip4_message_forgui = "";
            }

            if (!MainDataStore.isLumbersGettedFinal || !MainDataStore.isCoalsGettedFinal)
            {
                tip5_message_forgui = Language.TipAndChirperMessage[10];
            }
            else
            {
                tip5_message_forgui = "";
            }

            if (!MainDataStore.isPetrolsGettedFinal)
            {
                tip6_message_forgui = Language.TipAndChirperMessage[11];
            }
            else
            {
                tip6_message_forgui = "";
            }

            //if (!MainDataStore.isHellMode)
            //{
            //    tip7_message_forgui = Language.OptionUI[3];
            //}
            //else
            //{
            //    tip7_message_forgui = "";
            //}
        }

        public void CaculateCitizenTransportFee()
        {
            ItemClass temp = new ItemClass();
            long temp1 = 0L;
            long temp2 = 0L;
            MainDataStore.public_transport_fee = 0L;
            temp.m_service = ItemClass.Service.PublicTransport;
            temp.m_subService = ItemClass.SubService.PublicTransportBus;
            Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
            RealCityUI.bus_income = (double)temp2 / 100f;
            MainDataStore.public_transport_fee = MainDataStore.public_transport_fee + temp2;

            temp1 = 0L;
            temp2 = 0L;
            temp.m_subService = ItemClass.SubService.PublicTransportTram;
            Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
            RealCityUI.tram_income = (double)temp2 / 100f;
            MainDataStore.public_transport_fee = MainDataStore.public_transport_fee + temp2;

            temp1 = 0L;
            temp2 = 0L;
            temp.m_subService = ItemClass.SubService.PublicTransportMetro;
            Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
            RealCityUI.metro_income = (double)temp2 / 100f;
            MainDataStore.public_transport_fee = MainDataStore.public_transport_fee + temp2;

            temp1 = 0L;
            temp2 = 0L;
            temp.m_subService = ItemClass.SubService.PublicTransportTrain;
            Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
            RealCityUI.train_income = (double)temp2 / 100f;
            MainDataStore.public_transport_fee = MainDataStore.public_transport_fee + temp2;

            temp1 = 0L;
            temp2 = 0L;
            temp.m_subService = ItemClass.SubService.PublicTransportTaxi;
            Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
            RealCityUI.taxi_income = (double)temp2 / 100f;
            MainDataStore.public_transport_fee = MainDataStore.public_transport_fee + temp2;

            temp1 = 0L;
            temp2 = 0L;
            temp.m_service = ItemClass.Service.PublicTransport;
            temp.m_subService = ItemClass.SubService.PublicTransportPlane;
            Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
            RealCityUI.plane_income = (double)temp2 / 100f;
            MainDataStore.public_transport_fee = MainDataStore.public_transport_fee + temp2;

            temp1 = 0L;
            temp2 = 0L;
            temp.m_subService = ItemClass.SubService.PublicTransportShip;
            Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
            RealCityUI.ship_income = (double)temp2 / 100f;
            MainDataStore.public_transport_fee = MainDataStore.public_transport_fee + temp2;

            temp1 = 0L;
            temp2 = 0L;
            temp.m_subService = ItemClass.SubService.PublicTransportMonorail;
            Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
            RealCityUI.monorail_income = (double)temp2 / 100f;
            MainDataStore.public_transport_fee = MainDataStore.public_transport_fee + temp2;

            temp1 = 0L;
            temp2 = 0L;
            temp.m_subService = ItemClass.SubService.PublicTransportCableCar;
            Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
            RealCityUI.cablecar_income = (double)temp2 / 100f;
            MainDataStore.public_transport_fee = MainDataStore.public_transport_fee + temp2;

            //add vehicle transport_fee
            MainDataStore.temp_total_citizen_vehical_time_last = MainDataStore.temp_total_citizen_vehical_time;
            MainDataStore.temp_total_citizen_vehical_time = 0;

            //assume that 1 time will cost 5fen car oil money
            MainDataStore.all_transport_fee = MainDataStore.public_transport_fee + MainDataStore.temp_total_citizen_vehical_time_last;

            if (MainDataStore.family_count > 0)
            {
                MainDataStore.citizen_average_transport_fee = (byte)(MainDataStore.all_transport_fee / MainDataStore.family_count);
            }
        }


        public void BuildingStatus()
        {
            BuildingManager instance = Singleton<BuildingManager>.instance;
            updateOnce = false;
            RealCityOutsideConnectionAI.haveGarbageBuildingFinal = RealCityOutsideConnectionAI.haveGarbageBuilding;
            MainDataStore.haveCityResourceDepartmentFinal = MainDataStore.haveCityResourceDepartment;
            RealCityOutsideConnectionAI.haveGarbageBuilding = false;
            MainDataStore.haveCityResourceDepartment = false;

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
            partyTrendStrength = (byte)rand.Next(600);

            if (Politics.parliamentCount == 0)
            {
                GetSeats(false);
                GetSeats(true);
                Politics.parliamentMeetingCount = 1;
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


        public void VoteOffset(ref int idex, ref int adviseOffset, ref int MoneyOffset, ref int citizenOffset, ref int buildingOffset)
        {
            //adviseOffset
            adviseOffset = 0;
            if (Politics.tryFallLandTax)
            {
                adviseOffset = ((Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_finalHappiness - 80)) > 0 ? ((int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_finalHappiness - 80) << 7 : 0;
                idex = 13;
                Politics.currentIdx = 13;
            }
            else if (Politics.tryFallImportTax)
            {
                adviseOffset = ((Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_finalHappiness - 80)) > 0 ? ((int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_finalHappiness - 80) << 7 : 0;
                idex = 7;
                Politics.currentIdx = 7;
            }
            else if (Politics.tryFallTradeTax)
            {
                adviseOffset = ((Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_finalHappiness - 80)) > 0 ? ((int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_finalHappiness - 80) << 7 : 0;
                idex = 5;
                Politics.currentIdx = 5;
            }
            else if (Politics.tryRiseImportTax)
            {
                adviseOffset = ((Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_finalHappiness - 80)) > 0 ? ((int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_finalHappiness - 80) << 7 : 0;
                idex = 6;
                Politics.currentIdx = 6;
            }
            else if (Politics.tryRiseTradeTax)
            {
                adviseOffset = ((Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_finalHappiness - 80)) > 0 ? ((int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_finalHappiness - 80) << 7 : 0;
                idex = 4;
                Politics.currentIdx = 4;
            }



            //MoneyOffset;
            MoneyOffset = 0;
            FieldInfo cashAmount;
            cashAmount = typeof(EconomyManager).GetField("m_cashAmount", BindingFlags.NonPublic | BindingFlags.Instance);
            long _cashAmount = (long)cashAmount.GetValue(Singleton<EconomyManager>.instance);
            if (_cashAmount < 0)
            {
                MoneyOffset = -1000;
            }
            else if (_cashAmount > 6000000)
            {
                MoneyOffset = 2000;
            }
            else
            {
                MoneyOffset = -1000 + (int)(_cashAmount / 2000);
            }

            //citizenOffset

            citizenOffset = 0;

            int temp = 0;
            if (MainDataStore.family_count > 0)
            {
                temp = (int)(MainDataStore.citizen_salary_per_family - (MainDataStore.citizen_salary_tax_total / MainDataStore.family_count) - MainDataStore.citizen_expense_per_family);
            }

            if (temp < 30)
            {
                citizenOffset = 1500;
            }
            else if (temp > 60)
            {
                citizenOffset = -1500;
            }
            else
            {
                citizenOffset = 1500 - 100 * temp;
            }

            //buildingOffset
            buildingOffset = 0;
            if (isBuildingNoBuyerCount + isBuildingNoMaterialCount > 0)
            {
                buildingOffset = (isBuildingNoBuyerCount + isBuildingNoMaterialCount) << 4;
                MoneyOffset = 0;
                if (buildingOffset > 2000)
                {
                    buildingOffset = 2000;
                }
                if (Politics.tryFallImportTax || Politics.tryFallLandTax || Politics.tryFallTradeTax || Politics.tryRiseImportTax || Politics.tryRiseTradeTax)
                {
                }
                else
                {
                    idex = 7;
                    Politics.currentIdx = 7;
                }
            }


            //DebugLog.LogToFileOnly("isBuildingNoBuyerCount = " + isBuildingNoBuyerCount.ToString());
            //DebugLog.LogToFileOnly("isBuildingNoMaterialCount = " + isBuildingNoMaterialCount.ToString());
            isBuildingNoBuyerCount = 0;
            isBuildingNoMaterialCount = 0;

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


            int temp2 = 0; // suggest offset
            int temp3 = 0; // money offset
            int temp4 = 0; // citizen offset
            int temp5 = 0; //building offset

            VoteOffset(ref idex, ref temp2, ref temp3, ref temp4, ref temp5);


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
                        yes -= temp4;
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
                        yes += temp4;
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
                        yes -= temp5;
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
                        yes += temp5;
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
                        yes -= temp4;
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
                        yes += temp4;
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
}
