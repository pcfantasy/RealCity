using System;
using ColossalFramework;
using UnityEngine;
using RealCity.Util;
using Harmony;
using System.Reflection;
using RealCity.CustomData;
using RealCity.CustomAI;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class ResidentAISimulationStepPatch1
    {
        public static MethodBase TargetMethod()
        {
            return typeof(ResidentAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(uint), typeof(CitizenUnit).MakeByRefType() }, null);
        }

        public static void ProcessCitizen(uint homeID, ref CitizenUnit data, bool isPre)
        {
            if (isPre)
            {
                CitizenUnitData.familyMoney[homeID] = 0;
                if (data.m_citizen0 != 0)
                {
                    Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen0];
                    if ((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None)
                    {
                        if (citizenData.Dead == false)
                        {
                            RealCityResidentAI.citizenCount++;
                            CitizenUnitData.familyMoney[homeID] += CitizenData.citizenMoney[data.m_citizen0];
                        }
                    }
                }
                if (data.m_citizen1 != 0)
                {
                    Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen1];
                    if ((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None)
                    {
                        if (citizenData.Dead == false)
                        {
                            RealCityResidentAI.citizenCount++;
                            CitizenUnitData.familyMoney[homeID] += CitizenData.citizenMoney[data.m_citizen1];
                        }
                    }
                }
                if (data.m_citizen2 != 0)
                {
                    Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen2];
                    if ((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None)
                    {
                        if (citizenData.Dead == false)
                        {
                            RealCityResidentAI.citizenCount++;
                            CitizenUnitData.familyMoney[homeID] += CitizenData.citizenMoney[data.m_citizen2];
                        }
                    }
                }
                if (data.m_citizen3 != 0)
                {
                    Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen3];
                    if ((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None)
                    {
                        if (citizenData.Dead == false)
                        {
                            RealCityResidentAI.citizenCount++;
                            CitizenUnitData.familyMoney[homeID] += CitizenData.citizenMoney[data.m_citizen3];
                        }
                    }
                }
                if (data.m_citizen4 != 0)
                {
                    Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen4];
                    if ((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None)
                    {
                        if (citizenData.Dead == false)
                        {
                            RealCityResidentAI.citizenCount++;
                            CitizenUnitData.familyMoney[homeID] += CitizenData.citizenMoney[data.m_citizen4];
                        }
                    }
                }
            }
            else
            {
                if (CitizenUnitData.familyMoney[homeID] < 5000)
                {
                    RealCityResidentAI.familyWeightStableLow++;
                }
                else if (CitizenUnitData.familyMoney[homeID] >= 20000)
                {
                    RealCityResidentAI.familyWeightStableHigh++;
                }

                int temp = 0;
                if (data.m_citizen0 != 0)
                {
                    Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen0];
                    if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                    {
                        temp++;
#if FASTRUN
#else
                        GetVoteChance(data.m_citizen0, citizenData, homeID);
#endif
                    }
                }
                if (data.m_citizen1 != 0)
                {
                    Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen1];
                    if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                    {
#if FASTRUN
#else
                        GetVoteChance(data.m_citizen1, citizenData, homeID);
#endif
                        temp++;
                    }
                }
                if (data.m_citizen2 != 0)
                {
                    Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen2];
                    if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                    {
#if FASTRUN
#else
                        GetVoteChance(data.m_citizen2, citizenData, homeID);
#endif
                        temp++;
                    }
                }
                if (data.m_citizen3 != 0)
                {
                    Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen3];
                    if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                    {
#if FASTRUN
#else
                        GetVoteChance(data.m_citizen3, citizenData, homeID);
#endif
                        temp++;
                    }
                }
                if (data.m_citizen4 != 0)
                {
                    Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen4];
                    if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                    {
#if FASTRUN
#else
                        GetVoteChance(data.m_citizen4, citizenData, homeID);
#endif
                        temp++;
                    }
                }

                if (temp != 0)
                {
                    if (data.m_citizen0 != 0)
                    {
                        Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen0];
                        if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                        {
                            CitizenData.citizenMoney[data.m_citizen0] = CitizenUnitData.familyMoney[homeID] / temp;
                        }
                    }
                    if (data.m_citizen1 != 0)
                    {
                        Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen1];
                        if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                        {
                            CitizenData.citizenMoney[data.m_citizen1] = CitizenUnitData.familyMoney[homeID] / temp;
                        }
                    }
                    if (data.m_citizen2 != 0)
                    {
                        Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen2];
                        if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                        {
                            CitizenData.citizenMoney[data.m_citizen2] = CitizenUnitData.familyMoney[homeID] / temp;
                        }
                    }
                    if (data.m_citizen3 != 0)
                    {
                        Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen3];
                        if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                        {
                            CitizenData.citizenMoney[data.m_citizen3] = CitizenUnitData.familyMoney[homeID] / temp;
                        }
                    }
                    if (data.m_citizen4 != 0)
                    {
                        Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen4];
                        if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                        {
                            CitizenData.citizenMoney[data.m_citizen4] = CitizenUnitData.familyMoney[homeID] / temp;
                        }
                    }
                }
            }

        }

        public static void ProcessFamily(uint homeID, ref CitizenUnit data)
        {
            if (RealCityResidentAI.preCitizenId > homeID)
            {
                //DebugLog.LogToFileOnly("Another period started");
                MainDataStore.familyCount = RealCityResidentAI.familyCount;
                MainDataStore.citizenCount = RealCityResidentAI.citizenCount;
                MainDataStore.profitFamilyNum = RealCityResidentAI.familyProfitMoneyCount;
                MainDataStore.veryProfitFamilyNum = RealCityResidentAI.familyVeryProfitMoneyCount;
                MainDataStore.lossFamilyNum = RealCityResidentAI.familyLossMoneyCount;
                if (RealCityResidentAI.familyCount != 0)
                {
                    MainDataStore.citizenSalaryPerFamily = ((RealCityResidentAI.citizenSalaryCount / RealCityResidentAI.familyCount));
                    MainDataStore.citizenExpensePerFamily = ((RealCityResidentAI.citizenExpenseCount / RealCityResidentAI.familyCount));
                }
                MainDataStore.citizenExpense = RealCityResidentAI.citizenExpenseCount;
                MainDataStore.citizenSalaryTaxTotal = RealCityResidentAI.citizenSalaryTaxTotal;
                MainDataStore.citizenSalaryTotal = RealCityResidentAI.citizenSalaryCount;
                if (MainDataStore.familyCount < MainDataStore.familyWeightStableHigh)
                {
                    MainDataStore.familyWeightStableHigh = (uint)MainDataStore.familyCount;
                }
                else
                {
                    MainDataStore.familyWeightStableHigh = RealCityResidentAI.familyWeightStableHigh;
                }
                if (MainDataStore.familyCount < MainDataStore.familyWeightStableLow)
                {
                    MainDataStore.familyWeightStableLow = (uint)MainDataStore.familyCount;
                }
                else
                {
                    MainDataStore.familyWeightStableLow = RealCityResidentAI.familyWeightStableLow;
                }

                MainDataStore.totalFamilyGoodDemand = RealCityResidentAI.totalFamilyGoodDemand;

                RealCityPrivateBuildingAI.profitBuildingMoneyFinal = RealCityPrivateBuildingAI.profitBuildingMoney;

                RealCityResidentAI.familyVeryProfitMoneyCount = 0;
                RealCityResidentAI.familyProfitMoneyCount = 0;
                RealCityResidentAI.familyLossMoneyCount = 0;
                RealCityResidentAI.familyCount = 0;
                RealCityResidentAI.citizenCount = 0;
                RealCityResidentAI.citizenSalaryCount = 0;
                RealCityResidentAI.citizenExpenseCount = 0;
                RealCityResidentAI.citizenSalaryTaxTotal = 0;
                RealCityResidentAI.tempCitizenSalaryTaxTotal = 0f;
                RealCityResidentAI.familyWeightStableHigh = 0;
                RealCityResidentAI.familyWeightStableLow = 0;
                RealCityResidentAI.totalFamilyGoodDemand = 0;
                RealCityPrivateBuildingAI.profitBuildingMoney = 0;
            }
            RealCityResidentAI.preCitizenId = homeID;
            RealCityResidentAI.familyCount++;

            if (homeID > 524288)
            {
                DebugLog.LogToFileOnly("Error: citizen ID greater than 524288");
            }

            //DebugLog.LogToFileOnly($"ProcessCitizen pre family {homeID} moneny {CitizenUnitData.familyMoney[homeID]}");
            //ProcessCitizen pre, gather all citizenMoney to familyMoney
            ProcessCitizen(homeID, ref data, true);
            //DebugLog.LogToFileOnly($"ProcessCitizen post family {homeID} moneny {CitizenUnitData.familyMoney[homeID]}");
            //1.We calculate citizen income
            int familySalaryCurrent = 0;
            familySalaryCurrent += RealCityResidentAI.ProcessCitizenSalary(data.m_citizen0, false);
            familySalaryCurrent += RealCityResidentAI.ProcessCitizenSalary(data.m_citizen1, false);
            familySalaryCurrent += RealCityResidentAI.ProcessCitizenSalary(data.m_citizen2, false);
            familySalaryCurrent += RealCityResidentAI.ProcessCitizenSalary(data.m_citizen3, false);
            familySalaryCurrent += RealCityResidentAI.ProcessCitizenSalary(data.m_citizen4, false);
            RealCityResidentAI.citizenSalaryCount = RealCityResidentAI.citizenSalaryCount + familySalaryCurrent;
            if (familySalaryCurrent < 0)
            {
                DebugLog.LogToFileOnly("familySalaryCurrent< 0 in ResidentAI");
                familySalaryCurrent = 0;
            }

            //2.We calculate salary tax
            float tax = (float)Politics.residentTax * familySalaryCurrent / 100f;
            RealCityResidentAI.tempCitizenSalaryTaxTotal = RealCityResidentAI.tempCitizenSalaryTaxTotal + (int)tax;
            RealCityResidentAI.citizenSalaryTaxTotal = (int)RealCityResidentAI.tempCitizenSalaryTaxTotal;
            ProcessCitizenIncomeTax(homeID, tax);

            //3. We calculate expense
            int educationFee = 0;
            int expenseRate = 0;
            CitizenManager instance = Singleton<CitizenManager>.instance;
            if (data.m_citizen4 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen4)].Dead)
            {
                educationFee += GetExpenseRate(data.m_citizen4, out expenseRate);
            }
            if (data.m_citizen3 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen3)].Dead)
            {
                educationFee += GetExpenseRate(data.m_citizen3, out expenseRate);
            }
            if (data.m_citizen2 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen2)].Dead)
            {
                educationFee += GetExpenseRate(data.m_citizen2, out expenseRate);
            }
            if (data.m_citizen1 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen1)].Dead)
            {
                educationFee += GetExpenseRate(data.m_citizen1, out expenseRate);
            }
            if (data.m_citizen0 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen0)].Dead)
            {
                educationFee += GetExpenseRate(data.m_citizen0, out expenseRate);
            }
            ProcessCitizenHouseRent(homeID, expenseRate);
            //campus DLC added.
            expenseRate = UniqueFacultyAI.IncreaseByBonus(UniqueFacultyAI.FacultyBonus.Economics, expenseRate);
            RealCityResidentAI.citizenExpenseCount += (educationFee + expenseRate);

            //4. income - expense
            float incomeMinusExpense = familySalaryCurrent - tax - educationFee - expenseRate;
            CitizenUnitData.familyMoney[homeID] += incomeMinusExpense;

            //5. Process citizen status
            if (incomeMinusExpense <= 0)
            {
                RealCityResidentAI.familyLossMoneyCount++;
            }
            else if (incomeMinusExpense > 100)
            {
                RealCityResidentAI.familyVeryProfitMoneyCount++;
            }
            else
            {
                RealCityResidentAI.familyProfitMoneyCount++;
            }

            //6. Limit familyMoney
            if (CitizenUnitData.familyMoney[homeID] > 3200000f)
            {
                CitizenUnitData.familyMoney[homeID] = 3200000f;
            }

            if (CitizenUnitData.familyMoney[homeID] < -3200000f)
            {
                CitizenUnitData.familyMoney[homeID] = -3200000f;
            }

            //8. Caculate minimumLivingAllowance and benefitOffset
            if (CitizenUnitData.familyMoney[homeID] < (-Politics.benefitOffset))
            {
                int num = (int)(-CitizenUnitData.familyMoney[homeID]);
                CitizenUnitData.familyMoney[homeID] += num;
                MainDataStore.minimumLivingAllowance += num;
                Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)17, num, ItemClass.Service.Residential, ItemClass.SubService.None, ItemClass.Level.Level1);
            }
            else
            {
                if (Politics.benefitOffset > 0)
                {
                    CitizenUnitData.familyMoney[homeID] += Politics.benefitOffset;
                    MainDataStore.minimumLivingAllowance += Politics.benefitOffset;
                    Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)17, Politics.benefitOffset, ItemClass.Service.Residential, ItemClass.SubService.None, ItemClass.Level.Level1);
                }
            }

            //ProcessCitizen post, split all familyMoney to CitizenMoney
            ProcessCitizen(homeID, ref data, false);
        }


        public static void ProcessCitizenIncomeTax(uint homeID, float tax)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            ushort building = instance.m_units.m_buffer[(int)((UIntPtr)homeID)].m_building;
            Building buildingdata = Singleton<BuildingManager>.instance.m_buildings.m_buffer[building];
            Singleton<EconomyManager>.instance.AddPrivateIncome((int)(tax), buildingdata.Info.m_class.m_service, buildingdata.Info.m_class.m_subService, buildingdata.Info.m_class.m_level, 112333);
        }

        public static void ProcessCitizenHouseRent(uint homeID, int expenserate)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            ushort building = instance.m_units.m_buffer[(int)((UIntPtr)homeID)].m_building;
            Building buildingdata = Singleton<BuildingManager>.instance.m_buildings.m_buffer[building];
            Singleton<EconomyManager>.instance.AddPrivateIncome(expenserate * 100, buildingdata.Info.m_class.m_service, buildingdata.Info.m_class.m_subService, buildingdata.Info.m_class.m_level, 100);
        }

        // ResidentAI
        public static void Postfix(uint homeID, ref CitizenUnit data)
        {
            if ((Singleton<BuildingManager>.instance.m_buildings.m_buffer[data.m_building].m_flags & (Building.Flags.Completed | Building.Flags.Upgrading)) != Building.Flags.None)
            {
                ProcessFamily(homeID, ref data);
            }
            data.m_goods = (ushort)COMath.Clamp((int)(data.m_goods + 20), 0, 60000);

            float reducedGoods;
            if (CitizenUnitData.familyMoney[homeID] < 1000)
                reducedGoods = data.m_goods * 0.01f;
            else if (CitizenUnitData.familyMoney[homeID] < 5000)
                reducedGoods = data.m_goods * 0.05f;
            else if (CitizenUnitData.familyMoney[homeID] < 10000)
                reducedGoods = data.m_goods * 0.1f;
            else if (CitizenUnitData.familyMoney[homeID] < 20000)
                reducedGoods = data.m_goods * 0.15f;
            else
                reducedGoods = data.m_goods * 0.2f;

            data.m_goods = (ushort)COMath.Clamp((int)(data.m_goods - reducedGoods), 0, 60000);
        }

        public static int GetExpenseRate(uint citizenID, out int incomeAccumulation)
        {
            BuildingManager instance1 = Singleton<BuildingManager>.instance;
            CitizenManager instance2 = Singleton<CitizenManager>.instance;
            ItemClass @class = instance1.m_buildings.m_buffer[instance2.m_citizens.m_buffer[citizenID].m_homeBuilding].Info.m_class;
            incomeAccumulation = 0;
            DistrictManager instance = Singleton<DistrictManager>.instance;
            if (instance2.m_citizens.m_buffer[citizenID].m_homeBuilding != 0)
            {
                byte district = instance.GetDistrict(instance1.m_buildings.m_buffer[instance2.m_citizens.m_buffer[citizenID].m_homeBuilding].m_position);
                DistrictPolicies.Taxation taxationPolicies = instance.m_districts.m_buffer[district].m_taxationPolicies;
                if (@class.m_subService == ItemClass.SubService.ResidentialLow)
                {
                    switch (@class.m_level)
                    {
                        case ItemClass.Level.Level1:
                            incomeAccumulation = MainDataStore.residentLowLevel1Rent;
                            break;
                        case ItemClass.Level.Level2:
                            incomeAccumulation = MainDataStore.residentLowLevel2Rent;
                            break;
                        case ItemClass.Level.Level3:
                            incomeAccumulation = MainDataStore.residentLowLevel3Rent;
                            break;
                        case ItemClass.Level.Level4:
                            incomeAccumulation = MainDataStore.residentLowLevel4Rent;
                            break;
                        case ItemClass.Level.Level5:
                            incomeAccumulation = MainDataStore.residentLowLevel5Rent;
                            break;
                    }
                }
                else if (@class.m_subService == ItemClass.SubService.ResidentialLowEco)
                {
                    switch (@class.m_level)
                    {
                        case ItemClass.Level.Level1:
                            incomeAccumulation = MainDataStore.residentLowLevel1Rent << 1;
                            break;
                        case ItemClass.Level.Level2:
                            incomeAccumulation = MainDataStore.residentLowLevel2Rent << 1;
                            break;
                        case ItemClass.Level.Level3:
                            incomeAccumulation = MainDataStore.residentLowLevel3Rent << 1;
                            break;
                        case ItemClass.Level.Level4:
                            incomeAccumulation = MainDataStore.residentLowLevel4Rent << 1;
                            break;
                        case ItemClass.Level.Level5:
                            incomeAccumulation = MainDataStore.residentLowLevel5Rent << 1;
                            break;
                    }
                }
                else if (@class.m_subService == ItemClass.SubService.ResidentialHigh)
                {
                    switch (@class.m_level)
                    {
                        case ItemClass.Level.Level1:
                            incomeAccumulation = MainDataStore.residentHighLevel1Rent;
                            break;
                        case ItemClass.Level.Level2:
                            incomeAccumulation = MainDataStore.residentHighLevel2Rent;
                            break;
                        case ItemClass.Level.Level3:
                            incomeAccumulation = MainDataStore.residentHighLevel3Rent;
                            break;
                        case ItemClass.Level.Level4:
                            incomeAccumulation = MainDataStore.residentHighLevel4Rent;
                            break;
                        case ItemClass.Level.Level5:
                            incomeAccumulation = MainDataStore.residentHighLevel5Rent;
                            break;
                    }
                }
                else
                {
                    switch (@class.m_level)
                    {
                        case ItemClass.Level.Level1:
                            incomeAccumulation = MainDataStore.residentHighLevel1Rent << 1;
                            break;
                        case ItemClass.Level.Level2:
                            incomeAccumulation = MainDataStore.residentHighLevel2Rent << 1;
                            break;
                        case ItemClass.Level.Level3:
                            incomeAccumulation = MainDataStore.residentHighLevel3Rent << 1;
                            break;
                        case ItemClass.Level.Level4:
                            incomeAccumulation = MainDataStore.residentHighLevel4Rent << 1;
                            break;
                        case ItemClass.Level.Level5:
                            incomeAccumulation = MainDataStore.residentHighLevel5Rent << 1;
                            break;
                    }
                }
                int num2;
                num2 = Singleton<EconomyManager>.instance.GetTaxRate(@class, taxationPolicies);
                incomeAccumulation = (int)(num2 * incomeAccumulation * ((float)(instance.m_districts.m_buffer[district].GetLandValue() + 50) / 10000));
            }

            int educationFee = 0;
            if ((Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenID].m_flags & Citizen.Flags.Student) != Citizen.Flags.None)
            {
                //Only university will cost money
                bool isCampusDLC = false;
                //Campus DLC cost 100
                ushort visitBuilding = Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenID].m_visitBuilding;
                if (visitBuilding != 0u)
                {
                    Building buildingData = Singleton<BuildingManager>.instance.m_buildings.m_buffer[visitBuilding];
                    if (buildingData.Info.m_class.m_service == ItemClass.Service.PlayerEducation)
                    {
                        educationFee = 100;
                        isCampusDLC = true;
                    }
                }

                if (!isCampusDLC && (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenID].m_flags.IsFlagSet(Citizen.Flags.Education2)))
                {
                    educationFee = 20;
                    Singleton<EconomyManager>.instance.AddPrivateIncome(20, ItemClass.Service.Education, ItemClass.SubService.None, ItemClass.Level.Level3, 115333);
                }
            }
            return educationFee;
        }

        public static void GetVoteTickets()
        {
            System.Random rand = new System.Random();
            if (Politics.cPartyChance + Politics.gPartyChance + Politics.sPartyChance + Politics.lPartyChance + Politics.nPartyChance != (800 + RealCityEconomyExtension.partyTrendStrength))
            {
                if (rand.Next(64) <= 1)
                {
                    DebugLog.LogToFileOnly("Error: Chance is not equal 800 " + (Politics.cPartyChance + Politics.gPartyChance + Politics.sPartyChance + Politics.lPartyChance + Politics.nPartyChance).ToString());
                }
            }

            if (RealCityEconomyExtension.voteRandom < Politics.cPartyChance)
            {
                Politics.cPartyTickets++;
            }
            else if (RealCityEconomyExtension.voteRandom < Politics.cPartyChance + Politics.gPartyChance)
            {
                Politics.gPartyTickets++;
            }
            else if (RealCityEconomyExtension.voteRandom < Politics.cPartyChance + Politics.gPartyChance + Politics.sPartyChance)
            {
                Politics.sPartyTickets++;
            }
            else if (RealCityEconomyExtension.voteRandom < Politics.cPartyChance + Politics.gPartyChance + Politics.sPartyChance + Politics.lPartyChance)
            {
                Politics.lPartyTickets++;
            }
            else
            {
                Politics.nPartyTickets++;
            }
        }

        public static void GetVoteChance(uint citizenID, Citizen citizen, uint homeID)
        {
            if ((int)Citizen.GetAgeGroup(citizen.m_age) >= 2)
            {
                if (Politics.parliamentCount == 1)
                {
                    Politics.cPartyChance = 0;
                    Politics.gPartyChance = 0;
                    Politics.sPartyChance = 0;
                    Politics.lPartyChance = 0;
                    Politics.nPartyChance = 0;

                    Politics.cPartyChance += (ushort)(Politics.education[(int)citizen.EducationLevel, 0] << 1);
                    Politics.gPartyChance += (ushort)(Politics.education[(int)citizen.EducationLevel, 1] << 1);
                    Politics.sPartyChance += (ushort)(Politics.education[(int)citizen.EducationLevel, 2] << 1);
                    Politics.lPartyChance += (ushort)(Politics.education[(int)citizen.EducationLevel, 3] << 1);
                    Politics.nPartyChance += (ushort)(Politics.education[(int)citizen.EducationLevel, 4] << 1);

                    int idex = 14;
                    switch (Singleton<BuildingManager>.instance.m_buildings.m_buffer[citizen.m_workBuilding].Info.m_class.m_service)
                    {
                        case ItemClass.Service.Beautification:
                        case ItemClass.Service.Road:
                        case ItemClass.Service.Water:
                        case ItemClass.Service.FireDepartment:
                        case ItemClass.Service.PoliceDepartment:
                        case ItemClass.Service.HealthCare:
                        case ItemClass.Service.Garbage:
                        case ItemClass.Service.PublicTransport:
                        case ItemClass.Service.Disaster:
                        case ItemClass.Service.Education:
                        case ItemClass.Service.Electricity:
                        case ItemClass.Service.Monument:
                            idex = 0; break;
                    }

                    switch (Singleton<BuildingManager>.instance.m_buildings.m_buffer[citizen.m_workBuilding].Info.m_class.m_subService)
                    {
                        case ItemClass.SubService.CommercialLow:
                        case ItemClass.SubService.CommercialHigh:
                            if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[citizen.m_workBuilding].Info.m_class.m_level == ItemClass.Level.Level1)
                            {
                                idex = 1;
                            }
                            else if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[citizen.m_workBuilding].Info.m_class.m_level == ItemClass.Level.Level2)
                            {
                                idex = 2;
                            }
                            else if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[citizen.m_workBuilding].Info.m_class.m_level == ItemClass.Level.Level3)
                            {
                                idex = 3;
                            }
                            break;
                        case ItemClass.SubService.CommercialTourist:
                        case ItemClass.SubService.CommercialLeisure:
                            idex = 4; break;
                        case ItemClass.SubService.CommercialEco:
                            idex = 5; break;
                        case ItemClass.SubService.IndustrialGeneric:
                            if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[citizen.m_workBuilding].Info.m_class.m_level == ItemClass.Level.Level1)
                            {
                                idex = 6;
                            }
                            else if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[citizen.m_workBuilding].Info.m_class.m_level == ItemClass.Level.Level2)
                            {
                                idex = 7;
                            }
                            else if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[citizen.m_workBuilding].Info.m_class.m_level == ItemClass.Level.Level3)
                            {
                                idex = 8;
                            }
                            break;
                        case ItemClass.SubService.IndustrialFarming:
                        case ItemClass.SubService.IndustrialForestry:
                        case ItemClass.SubService.IndustrialOil:
                        case ItemClass.SubService.IndustrialOre:
                            idex = 9; break;
                        case ItemClass.SubService.OfficeGeneric:
                            if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[citizen.m_workBuilding].Info.m_class.m_level == ItemClass.Level.Level1)
                            {
                                idex = 10;
                            }
                            else if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[citizen.m_workBuilding].Info.m_class.m_level == ItemClass.Level.Level2)
                            {
                                idex = 11;
                            }
                            else if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[citizen.m_workBuilding].Info.m_class.m_level == ItemClass.Level.Level3)
                            {
                                idex = 12;
                            }
                            break;
                        case ItemClass.SubService.OfficeHightech:
                            idex = 13; break;
                    }

                    if (idex < 0 || idex > 14)
                    {
                        DebugLog.LogToFileOnly("Error workplace idex" + idex.ToString());
                    }


                    Politics.cPartyChance += (ushort)(Politics.workplace[idex, 0] << 1);
                    Politics.gPartyChance += (ushort)(Politics.workplace[idex, 1] << 1);
                    Politics.sPartyChance += (ushort)(Politics.workplace[idex, 2] << 1);
                    Politics.lPartyChance += (ushort)(Politics.workplace[idex, 3] << 1);
                    Politics.nPartyChance += (ushort)(Politics.workplace[idex, 4] << 1);

                    if (CitizenUnitData.familyMoney[homeID] < 5000)
                    {
                        idex = 0;
                    }
                    else if (CitizenUnitData.familyMoney[homeID] >= 20000)
                    {
                        idex = 2;
                    }
                    else
                    {
                        idex = 1;
                    }

                    if (idex < 0 || idex > 3)
                    {
                        DebugLog.LogToFileOnly("Error: Invaid money idex = " + idex.ToString());
                    }
                    Politics.cPartyChance += (ushort)(Politics.money[idex, 0] << 1);
                    Politics.gPartyChance += (ushort)(Politics.money[idex, 1] << 1);
                    Politics.sPartyChance += (ushort)(Politics.money[idex, 2] << 1);
                    Politics.lPartyChance += (ushort)(Politics.money[idex, 3] << 1);
                    Politics.nPartyChance += (ushort)(Politics.money[idex, 4] << 1);

                    int temp = 0;

                    temp = (int)Citizen.GetAgeGroup(citizen.m_age) - 2;

                    if (temp < 0)
                    {
                        DebugLog.LogToFileOnly(temp.ToString() + Citizen.GetAgeGroup(citizen.m_age).ToString());
                    }

                    Politics.cPartyChance += Politics.age[temp, 0];
                    Politics.gPartyChance += Politics.age[temp, 1];
                    Politics.sPartyChance += Politics.age[temp, 2];
                    Politics.lPartyChance += Politics.age[temp, 3];
                    Politics.nPartyChance += Politics.age[temp, 4];

                    temp = (int)Citizen.GetGender(citizenID);


                    Politics.cPartyChance += Politics.gender[temp, 0];
                    Politics.gPartyChance += Politics.gender[temp, 1];
                    Politics.sPartyChance += Politics.gender[temp, 2];
                    Politics.lPartyChance += Politics.gender[temp, 3];
                    Politics.nPartyChance += Politics.gender[temp, 4];

                    if (RealCityEconomyExtension.partyTrend == 0)
                    {
                        Politics.cPartyChance += RealCityEconomyExtension.partyTrendStrength;
                    }
                    else if (RealCityEconomyExtension.partyTrend == 1)
                    {
                        Politics.gPartyChance += RealCityEconomyExtension.partyTrendStrength;
                    }
                    else if (RealCityEconomyExtension.partyTrend == 2)
                    {
                        Politics.sPartyChance += RealCityEconomyExtension.partyTrendStrength;
                    }
                    else if (RealCityEconomyExtension.partyTrend == 3)
                    {
                        Politics.lPartyChance += RealCityEconomyExtension.partyTrendStrength;
                    }
                    else if (RealCityEconomyExtension.partyTrend == 4)
                    {
                        Politics.nPartyChance += RealCityEconomyExtension.partyTrendStrength;
                    }
                    else
                    {
                        DebugLog.LogToFileOnly("Error: Invalid partyTrend = " + RealCityEconomyExtension.partyTrend.ToString());
                    }

                    GetVoteTickets();
                }
            }
        }
    }
}
