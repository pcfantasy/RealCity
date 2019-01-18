using ColossalFramework;
using ColossalFramework.UI;
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
        public static int fixEmptyCitizenCount = 0;
        public static int fixEmptyBuildingCount = 0;

        public static bool updateOnce = false;
        public static byte partyTrend = 0;
        public static ushort partyTrendStrength = 0;

        public static byte citizenStatus = 0;
        public static ushort industrialLackMoneyCount = 0;
        public static ushort industrialEarnMoneyCount = 0;

        public static ushort commericalLackMoneyCount = 0;
        public static ushort commericalEarnMoneyCount = 0;

        public static byte isStateOwnedCount = 0;

        public static string tip1_message_forgui = "";
        public static string tip2_message_forgui = "";
        public static string tip3_message_forgui = "";
        public static string tip4_message_forgui = "";
        public static string tip5_message_forgui = "";
        public static string tip6_message_forgui = "";


        public override long OnUpdateMoneyAmount(long internalMoneyAmount)
        {
            if (Loader.CurrentLoadMode == LoadMode.LoadGame || Loader.CurrentLoadMode == LoadMode.NewGame)
            {
                MainDataStore.current_time = Singleton<SimulationManager>.instance.m_currentDayTimeHour;
                uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
                uint num2 = currentFrameIndex & 255u;
                if ((num2 == 255u) && (MainDataStore.current_time != MainDataStore.prev_time))
                {
                    GenerateTips();

                    MainDataStore.landPrice = Singleton<DistrictManager>.instance.m_districts.m_buffer[0].GetLandValue() / 10f;
                    if (MainDataStore.landPrice < 1f)
                    {
                        MainDataStore.landPrice = 1f;
                    } else if (MainDataStore.landPrice > 100f)
                    {
                        MainDataStore.landPrice = 100f;
                    }
                    MainDataStore.game_expense_divide = (int)((float)100 / MainDataStore.landPrice);
                    BuildingStatus();

                    if (MainDataStore.update_money_count == 16)
                    {
                        Politics.parliamentCount--;
                        //Politics.parliamentMeetingCount--;
                        if (Politics.parliamentCount < 0)
                        {
                            Politics.parliamentCount = 40;
                        }

                        MainDataStore.resettlementFinal = MainDataStore.resettlement;
                        MainDataStore.minimumLivingAllowanceFinal = MainDataStore.minimumLivingAllowance;
                        MainDataStore.resettlement = 0;
                        MainDataStore.minimumLivingAllowance = 0;
                        //DebugLog.LogToFileOnly("fixEmptyCitizenCount = " + fixEmptyCitizenCount.ToString());
                        //fixEmptyCitizenCount = 0;
                        CitizenStatus();
                    }

                    //DebugLog.LogToFileOnly("fixEmptyBuildingCount = " + fixEmptyBuildingCount.ToString());
                    //fixEmptyBuildingCount = 0;

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
                PlayerBuildingUI.refeshOnce = true;
                BuildingUI.refeshOnce = true;
                HumanUI.refeshOnce = true;
                TouristUI.refeshOnce = true;
                MainDataStore.is_updated = true;
            }
            return internalMoneyAmount;
        }

        public void GenerateTips()
        {
            tip1_message_forgui = Language.TipAndChirperMessage[0];

            tip2_message_forgui = Language.TipAndChirperMessage[1];

            if (!Loader.isFuelAlarmRunning && !Loader.isRealConstructionRunning)
            {
                tip3_message_forgui = Language.TipAndChirperMessage[2];
                Loader.guiPanel.Show();
            }
            else
            {
                tip3_message_forgui = "";
            }

            tip4_message_forgui = "";

            tip5_message_forgui = "";

            tip6_message_forgui = "";
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




        public static bool IsSpecialBuilding(ushort id)
        {
            BuildingManager instance = Singleton<BuildingManager>.instance;
            if (Loader.isFuelAlarmRunning)
            {
                if (FuelAlarm.FuelAlarmThreading.IsGasBuilding(id))
                {
                    return true;
                }
            }

            if (Loader.isRealConstructionRunning)
            {
                if (RealConstruction.RealConstructionThreading.IsSpecialBuilding(id))
                {
                    return true;
                }
            }

            return false;
        }

        public void CitizenStatus()
        {
            System.Random rand = new System.Random();
            partyTrend = (byte)rand.Next(5);
            partyTrendStrength = (byte)rand.Next(300);

            if (Politics.parliamentCount == 0)
            {
                GetSeats(false);
                GetSeats(true);
                //Politics.parliamentMeetingCount = 1;
                CreateGoverment();
            }
            else
            {
                GetSeats(true);
                CreateGoverment();
                HoldMeeting();
            }
        }

        public void HoldMeeting()
        {
            int temp = Politics.cPartySeats + Politics.gPartySeats + Politics.sPartySeats + Politics.lPartySeats + Politics.nPartySeats;
            if (temp == 99)
            {
                System.Random rand = new System.Random();
                switch (rand.Next(10))
                {
                    case 0:
                        if (Politics.residentTax >= 20)
                        {
                            Politics.currentIdx = 1;
                        }
                        else
                        {
                            Politics.currentIdx = 0;
                        }
                        break;
                    case 1:
                        if (Politics.residentTax <= 1)
                        {
                            Politics.currentIdx = 0;
                        }
                        else
                        {
                            Politics.currentIdx = 1;
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
                    case 4:
                        if (Politics.commericalTax >= 20)
                        {
                            Politics.currentIdx = 5;
                        }
                        else
                        {
                            Politics.currentIdx = 4;
                        }
                        break;
                    case 5:
                        if (Politics.commericalTax <= 1)
                        {
                            Politics.currentIdx = 4;
                        }
                        else
                        {
                            Politics.currentIdx = 5;
                        }
                        break;
                    case 6:
                        if (Politics.industryTax >= 20)
                        {
                            Politics.currentIdx = 7;
                        }
                        else
                        {
                            Politics.currentIdx = 6;
                        }
                        break;
                    case 7:
                        if (Politics.industryTax <= 1)
                        {
                            Politics.currentIdx = 6;
                        }
                        else
                        {
                            Politics.currentIdx = 7;
                        }
                        break;
                    case 8:
                        if (Politics.garbageCount >= 10)
                        {
                            Politics.currentIdx = 9;
                        }
                        else
                        {
                            Politics.currentIdx = 8;
                        }
                        break;
                    case 9:
                        if (Politics.garbageCount <= 0)
                        {
                            Politics.currentIdx = 8;
                        }
                        else
                        {
                            Politics.currentIdx = 9;
                        }
                        break;
                    default: Politics.currentIdx = 10; break;
                }
                VoteResult(Politics.currentIdx);
            }
        }


        public void VoteOffset(ref int idex, ref int MoneyOffset, ref int citizenOffset, ref int buildingOffset, ref int commBuildingOffset)
        {
            //MoneyOffset;
            MoneyOffset = 0;
            FieldInfo cashAmount;
            cashAmount = typeof(EconomyManager).GetField("m_cashAmount", BindingFlags.NonPublic | BindingFlags.Instance);
            long _cashAmount = (long)cashAmount.GetValue(Singleton<EconomyManager>.instance);
            if (_cashAmount < 0)
            {
                MoneyOffset = -2000;
                System.Random rand = new System.Random();
                if (rand.Next(10) < 8)
                {
                    switch (rand.Next(15))
                    {
                        case 0:
                        case 1:
                        case 2:
                            if (Politics.residentTax < 20)
                            {
                                idex = 0;
                            }else if (Politics.benefitOffset > 0)
                            {
                                idex = 3;
                            }
                            else if (Politics.industryTax < 20)
                            {
                                idex = 6;
                            }
                            else if (Politics.garbageCount < 10)
                            {
                                idex = 8;
                            } else if (Politics.commericalTax < 20)
                            {
                                idex = 4;
                            }
                            break;
                        case 3:
                        case 4:
                        case 5:
                            if (Politics.benefitOffset > 0)
                            {
                                idex = 3;
                            }
                            else if (Politics.industryTax < 20)
                            {
                                idex = 6;
                            }
                            else if (Politics.garbageCount < 10)
                            {
                                idex = 8;
                            }
                            else if (Politics.commericalTax < 20)
                            {
                                idex = 4;
                            }
                            else if(Politics.residentTax < 20)
                            {
                                idex = 0;
                            }
                            break;
                        case 6:
                        case 7:
                        case 8:
                            if (Politics.commericalTax < 20)
                            {
                                idex = 4;
                            } else if (Politics.benefitOffset > 0)
                            {
                                idex = 3;
                            }
                            else if (Politics.industryTax < 20)
                            {
                                idex = 6;
                            }
                            else if (Politics.garbageCount < 10)
                            {
                                idex = 8;
                            }
                            else if (Politics.residentTax < 20)
                            {
                                idex = 0;
                            }
                            else if (Politics.commericalTax < 20)
                            {
                                idex = 4;
                            }
                            break;
                        case 9:
                        case 10:
                        case 11:
                            if (Politics.industryTax < 20)
                            {
                                idex = 6;
                            }
                            else if (Politics.garbageCount < 10)
                            {
                                idex = 8;
                            }
                            else if (Politics.commericalTax < 20)
                            {
                                idex = 4;
                            }
                            else if (Politics.benefitOffset > 0)
                            {
                                idex = 3;
                            } else if (Politics.residentTax < 20)
                            {
                                idex = 0;
                            }
                            break;
                        case 12:
                        case 13:
                        case 14:
                            if (Politics.garbageCount < 10)
                            {
                                idex = 8;
                            }
                            else if (Politics.benefitOffset > 0)
                            {
                                idex = 3;
                            }
                            else if (Politics.industryTax < 20)
                            {
                                idex = 6;
                            }
                            else if (Politics.residentTax < 20)
                            {
                                idex = 0;
                            }
                            else if (Politics.commericalTax < 20)
                            {
                                idex = 4;
                            }
                            break;
                    }
                }
                Politics.currentIdx = (byte)idex;
            }
            else if (_cashAmount > 8000000)
            {
                MoneyOffset = 2000;
            }
            else
            {
                MoneyOffset = -2000 + (int)(_cashAmount / 2000);
            }

            //citizenOffset

            citizenOffset = 0;

            int temp = 0;
            if (MainDataStore.family_count > 0)
            {
                temp = (int)(MainDataStore.citizen_salary_per_family - (MainDataStore.citizen_salary_tax_total / MainDataStore.family_count) - MainDataStore.citizen_expense_per_family);
            }

            if (temp < 40)
            {
                citizenOffset = 500;
            }
            else if (temp > 90)
            {
                citizenOffset = -500;
            }
            else
            {
                citizenOffset = 1300 - 20 * temp;
            }

            //buildingOffset
            buildingOffset = 0;
            if (industrialEarnMoneyCount + industrialLackMoneyCount > 0)
            {
                buildingOffset = ((int)(100f * (float)(industrialEarnMoneyCount - industrialLackMoneyCount) / (float)(industrialEarnMoneyCount + industrialLackMoneyCount))) << 4;
                if (buildingOffset > 1500)
                {
                    buildingOffset = 1500;
                }

                if (buildingOffset < -1500)
                {
                    buildingOffset = -1500;
                }
            }

            commBuildingOffset = 0;
            if (commericalEarnMoneyCount + commericalLackMoneyCount > 0)
            {
                commBuildingOffset = ((int)(100f * (float)(commericalEarnMoneyCount - commericalLackMoneyCount) / (float)(commericalEarnMoneyCount + commericalLackMoneyCount))) << 4;
                if (commBuildingOffset > 1500)
                {
                    commBuildingOffset = 1500;
                }

                if (commBuildingOffset < -1500)
                {
                    commBuildingOffset = -1500;
                }
            }


            ///DebugLog.LogToFileOnly("commBuildingOffset = " + commBuildingOffset.ToString());
            ////DebugLog.LogToFileOnly("buildingOffset = " + buildingOffset.ToString());
            industrialEarnMoneyCount = 0;
            industrialLackMoneyCount = 0;
            commericalEarnMoneyCount = 0;
            commericalLackMoneyCount = 0;

        }

        public void VoteResult(int idex)
        {
            int temp = Politics.cPartySeats + Politics.gPartySeats + Politics.sPartySeats + Politics.lPartySeats + Politics.nPartySeats;
            int yes = 0;
            int no = 0;
            int noAttend = 0;
            int residentTax = 10 - (int)(Politics.residentTax);
            int benefitOffset = 10 - (int)(Politics.benefitOffset*2);
            int commericalTax = 10 - (int)(Politics.commericalTax);
            int industryTax = 10 - (int)(Politics.industryTax);
            int garbageCount = 10 - (int)(Politics.garbageCount * 2);

            int temp3 = 0; // money offset
            int temp4 = 0; // citizen offset
            int temp5 = 0; //building offset
            int temp6 = 0; //commbuilding offset

            VoteOffset(ref idex, ref temp3, ref temp4, ref temp5, ref temp6);




            if (temp == 99)
            {
                switch (idex)
                {
                    case 0:
                        yes += Politics.cPartySeats * (Politics.riseSalaryTax[0, 0] + residentTax);
                        yes += Politics.gPartySeats * (Politics.riseSalaryTax[1, 0] + residentTax);
                        yes += Politics.sPartySeats * (Politics.riseSalaryTax[2, 0] + residentTax);
                        yes += Politics.lPartySeats * (Politics.riseSalaryTax[3, 0] + residentTax);
                        yes += Politics.nPartySeats * (Politics.riseSalaryTax[4, 0] + residentTax);
                        no += Politics.cPartySeats * (Politics.riseSalaryTax[0, 1] - residentTax);
                        no += Politics.gPartySeats * (Politics.riseSalaryTax[1, 1] - residentTax);
                        no += Politics.sPartySeats * (Politics.riseSalaryTax[2, 1] - residentTax);
                        no += Politics.lPartySeats * (Politics.riseSalaryTax[3, 1] - residentTax);
                        no += Politics.nPartySeats * (Politics.riseSalaryTax[4, 1] - residentTax);
                        noAttend += Politics.cPartySeats * Politics.riseSalaryTax[0, 2];
                        noAttend += Politics.gPartySeats * Politics.riseSalaryTax[1, 2];
                        noAttend += Politics.sPartySeats * Politics.riseSalaryTax[2, 2];
                        noAttend += Politics.lPartySeats * Politics.riseSalaryTax[3, 2];
                        noAttend += Politics.nPartySeats * Politics.riseSalaryTax[4, 2];
                        yes -= temp3;
                        yes -= temp4;
                        break;
                    case 1:
                        yes += Politics.cPartySeats * (Politics.fallSalaryTax[0, 0] - residentTax);
                        yes += Politics.gPartySeats * (Politics.fallSalaryTax[1, 0] - residentTax);
                        yes += Politics.sPartySeats * (Politics.fallSalaryTax[2, 0] - residentTax);
                        yes += Politics.lPartySeats * (Politics.fallSalaryTax[3, 0] - residentTax);
                        yes += Politics.nPartySeats * (Politics.fallSalaryTax[4, 0] - residentTax);
                        no += Politics.cPartySeats * (Politics.fallSalaryTax[0, 1] + residentTax);
                        no += Politics.gPartySeats * (Politics.fallSalaryTax[1, 1] + residentTax);
                        no += Politics.sPartySeats * (Politics.fallSalaryTax[2, 1] + residentTax);
                        no += Politics.lPartySeats * (Politics.fallSalaryTax[3, 1] + residentTax);
                        no += Politics.nPartySeats * (Politics.fallSalaryTax[4, 1] + residentTax);
                        noAttend += Politics.cPartySeats * Politics.fallSalaryTax[0, 2];
                        noAttend += Politics.gPartySeats * Politics.fallSalaryTax[1, 2];
                        noAttend += Politics.sPartySeats * Politics.fallSalaryTax[2, 2];
                        noAttend += Politics.lPartySeats * Politics.fallSalaryTax[3, 2];
                        noAttend += Politics.nPartySeats * Politics.fallSalaryTax[4, 2];
                        yes += temp3;
                        yes += temp4;
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
                    case 4:
                        yes += Politics.cPartySeats * (Politics.riseCommericalTax[0, 0] + commericalTax);
                        yes += Politics.gPartySeats * (Politics.riseCommericalTax[1, 0] + commericalTax);
                        yes += Politics.sPartySeats * (Politics.riseCommericalTax[2, 0] + commericalTax);
                        yes += Politics.lPartySeats * (Politics.riseCommericalTax[3, 0] + commericalTax);
                        yes += Politics.nPartySeats * (Politics.riseCommericalTax[4, 0] + commericalTax);
                        no += Politics.cPartySeats * (Politics.riseCommericalTax[0, 1] - commericalTax);
                        no += Politics.gPartySeats * (Politics.riseCommericalTax[1, 1] - commericalTax);
                        no += Politics.sPartySeats * (Politics.riseCommericalTax[2, 1] - commericalTax);
                        no += Politics.lPartySeats * (Politics.riseCommericalTax[3, 1] - commericalTax);
                        no += Politics.nPartySeats * (Politics.riseCommericalTax[4, 1] - commericalTax);
                        noAttend += Politics.cPartySeats * Politics.riseCommericalTax[0, 2];
                        noAttend += Politics.gPartySeats * Politics.riseCommericalTax[1, 2];
                        noAttend += Politics.sPartySeats * Politics.riseCommericalTax[2, 2];
                        noAttend += Politics.lPartySeats * Politics.riseCommericalTax[3, 2];
                        noAttend += Politics.nPartySeats * Politics.riseCommericalTax[4, 2];
                        yes -= temp3;
                        yes += temp6;
                        break;
                    case 5:
                        yes += Politics.cPartySeats * (Politics.fallCommericalTax[0, 0] - commericalTax);
                        yes += Politics.gPartySeats * (Politics.fallCommericalTax[1, 0] - commericalTax);
                        yes += Politics.sPartySeats * (Politics.fallCommericalTax[2, 0] - commericalTax);
                        yes += Politics.lPartySeats * (Politics.fallCommericalTax[3, 0] - commericalTax);
                        yes += Politics.nPartySeats * (Politics.fallCommericalTax[4, 0] - commericalTax);
                        no += Politics.cPartySeats * (Politics.fallCommericalTax[0, 1] + commericalTax);
                        no += Politics.gPartySeats * (Politics.fallCommericalTax[1, 1] + commericalTax);
                        no += Politics.sPartySeats * (Politics.fallCommericalTax[2, 1] + commericalTax);
                        no += Politics.lPartySeats * (Politics.fallCommericalTax[3, 1] + commericalTax);
                        no += Politics.nPartySeats * (Politics.fallCommericalTax[4, 1] + commericalTax);
                        noAttend += Politics.cPartySeats * Politics.fallCommericalTax[0, 2];
                        noAttend += Politics.gPartySeats * Politics.fallCommericalTax[1, 2];
                        noAttend += Politics.sPartySeats * Politics.fallCommericalTax[2, 2];
                        noAttend += Politics.lPartySeats * Politics.fallCommericalTax[3, 2];
                        noAttend += Politics.nPartySeats * Politics.fallCommericalTax[4, 2];
                        yes += temp3;
                        yes -= temp6;
                        break;
                    case 6:
                        yes += Politics.cPartySeats * (Politics.riseIndustryTax[0, 0] + industryTax);
                        yes += Politics.gPartySeats * (Politics.riseIndustryTax[1, 0] + industryTax);
                        yes += Politics.sPartySeats * (Politics.riseIndustryTax[2, 0] + industryTax);
                        yes += Politics.lPartySeats * (Politics.riseIndustryTax[3, 0] + industryTax);
                        yes += Politics.nPartySeats * (Politics.riseIndustryTax[4, 0] + industryTax);
                        no += Politics.cPartySeats * (Politics.riseIndustryTax[0, 1] - industryTax);
                        no += Politics.gPartySeats * (Politics.riseIndustryTax[1, 1] - industryTax);
                        no += Politics.sPartySeats * (Politics.riseIndustryTax[2, 1] - industryTax);
                        no += Politics.lPartySeats * (Politics.riseIndustryTax[3, 1] - industryTax);
                        no += Politics.nPartySeats * (Politics.riseIndustryTax[4, 1] - industryTax);
                        noAttend += Politics.cPartySeats * Politics.riseIndustryTax[0, 2];
                        noAttend += Politics.gPartySeats * Politics.riseIndustryTax[1, 2];
                        noAttend += Politics.sPartySeats * Politics.riseIndustryTax[2, 2];
                        noAttend += Politics.lPartySeats * Politics.riseIndustryTax[3, 2];
                        noAttend += Politics.nPartySeats * Politics.riseIndustryTax[4, 2];
                        yes -= temp3;
                        yes += temp5;
                        break;
                    case 7:
                        yes += Politics.cPartySeats * (Politics.fallIndustryTax[0, 0] - industryTax);
                        yes += Politics.gPartySeats * (Politics.fallIndustryTax[1, 0] - industryTax);
                        yes += Politics.sPartySeats * (Politics.fallIndustryTax[2, 0] - industryTax);
                        yes += Politics.lPartySeats * (Politics.fallIndustryTax[3, 0] - industryTax);
                        yes += Politics.nPartySeats * (Politics.fallIndustryTax[4, 0] - industryTax);
                        no += Politics.cPartySeats * (Politics.fallIndustryTax[0, 1] + industryTax);
                        no += Politics.gPartySeats * (Politics.fallIndustryTax[1, 1] + industryTax);
                        no += Politics.sPartySeats * (Politics.fallIndustryTax[2, 1] + industryTax);
                        no += Politics.lPartySeats * (Politics.fallIndustryTax[3, 1] + industryTax);
                        no += Politics.nPartySeats * (Politics.fallIndustryTax[4, 1] + industryTax);
                        noAttend += Politics.cPartySeats * Politics.fallIndustryTax[0, 2];
                        noAttend += Politics.gPartySeats * Politics.fallIndustryTax[1, 2];
                        noAttend += Politics.sPartySeats * Politics.fallIndustryTax[2, 2];
                        noAttend += Politics.lPartySeats * Politics.fallIndustryTax[3, 2];
                        noAttend += Politics.nPartySeats * Politics.fallIndustryTax[4, 2];
                        yes += temp3;
                        yes -= temp5;
                        break;
                    case 8:
                        yes += Politics.cPartySeats * (Politics.riseGarbage[0, 0] + garbageCount);
                        yes += Politics.gPartySeats * (Politics.riseGarbage[1, 0] + garbageCount);
                        yes += Politics.sPartySeats * (Politics.riseGarbage[2, 0] + garbageCount);
                        yes += Politics.lPartySeats * (Politics.riseGarbage[3, 0] + garbageCount);
                        yes += Politics.nPartySeats * (Politics.riseGarbage[4, 0] + garbageCount);
                        no += Politics.cPartySeats * (Politics.riseGarbage[0, 1] - garbageCount);
                        no += Politics.gPartySeats * (Politics.riseGarbage[1, 1] - garbageCount);
                        no += Politics.sPartySeats * (Politics.riseGarbage[2, 1] - garbageCount);
                        no += Politics.lPartySeats * (Politics.riseGarbage[3, 1] - garbageCount);
                        no += Politics.nPartySeats * (Politics.riseGarbage[4, 1] - garbageCount);
                        noAttend += Politics.cPartySeats * Politics.riseGarbage[0, 2];
                        noAttend += Politics.gPartySeats * Politics.riseGarbage[1, 2];
                        noAttend += Politics.sPartySeats * Politics.riseGarbage[2, 2];
                        noAttend += Politics.lPartySeats * Politics.riseGarbage[3, 2];
                        noAttend += Politics.nPartySeats * Politics.riseGarbage[4, 2];
                        yes -= temp3;
                        break;
                    case 9:
                        yes += Politics.cPartySeats * (Politics.fallGarbage[0, 0] - garbageCount);
                        yes += Politics.gPartySeats * (Politics.fallGarbage[1, 0] - garbageCount);
                        yes += Politics.sPartySeats * (Politics.fallGarbage[2, 0] - garbageCount);
                        yes += Politics.lPartySeats * (Politics.fallGarbage[3, 0] - garbageCount);
                        yes += Politics.nPartySeats * (Politics.fallGarbage[4, 0] - garbageCount);
                        no += Politics.cPartySeats * (Politics.fallGarbage[0, 1] + garbageCount);
                        no += Politics.gPartySeats * (Politics.fallGarbage[1, 1] + garbageCount);
                        no += Politics.sPartySeats * (Politics.fallGarbage[2, 1] + garbageCount);
                        no += Politics.lPartySeats * (Politics.fallGarbage[3, 1] + garbageCount);
                        no += Politics.nPartySeats * (Politics.fallGarbage[4, 1] + garbageCount);
                        noAttend += Politics.cPartySeats * Politics.fallGarbage[0, 2];
                        noAttend += Politics.gPartySeats * Politics.fallGarbage[1, 2];
                        noAttend += Politics.sPartySeats * Politics.fallGarbage[2, 2];
                        noAttend += Politics.lPartySeats * Politics.fallGarbage[3, 2];
                        noAttend += Politics.nPartySeats * Politics.fallGarbage[4, 2];
                        yes += temp3;
                        break;
                }


                if (yes < 0)
                {
                    yes = 0;
                }

                if (no < 0)
                {
                    no = 0;
                }

                if (noAttend < 0)
                {
                    noAttend = 0;
                }

                int temp1 = yes + no + noAttend;


                yes = (int)((yes * 99) / temp1);
                no = (int)((no * 99) / temp1);
                noAttend = (int)((noAttend * 99) / temp1);

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
                            Politics.residentTax += 1;
                            break;
                        case 1:
                            Politics.residentTax -= 1;
                            break;
                        case 2:
                            Politics.benefitOffset += 1; break;
                        case 3:
                            Politics.benefitOffset -= 1; break;
                        case 4:
                            Politics.commericalTax += 1;
                            break;
                        case 5:
                            Politics.commericalTax -= 1;
                            break;
                        case 6:
                            Politics.industryTax += 1;
                            break;
                        case 7:
                            Politics.industryTax -= 1;
                            break;
                        case 8:
                            Politics.garbageCount++; break;
                        case 9:
                            Politics.garbageCount--; break;

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
