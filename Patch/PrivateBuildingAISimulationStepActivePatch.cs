using ColossalFramework;
using ColossalFramework.Math;
using RealCity.Util;
using RealCity.UI;
using HarmonyLib;
using System.Reflection;
using RealCity.CustomData;
using RealCity.CustomAI;
using UnityEngine;
using System;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class PrivateBuildingAISimulationStepActivePatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(PrivateBuildingAI).GetMethod("SimulationStepActive", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public static void Prefix(ref Building buildingData, ref ushort[] __state)
        {
            __state = new ushort[2];
            __state[0] = buildingData.m_customBuffer1;
            __state[1] = buildingData.m_customBuffer2;
        }

        public static void Postfix(ushort buildingID, ref Building buildingData)
        {
            ProcessLandFeeNoOffice(buildingData, buildingID);
            if (RealCityEconomyExtension.Can16timesUpdate(buildingID))
            {
                CalculateBuildingMoneyAndSalary(buildingData, buildingID);
            }
            LimitCommericalBuildingAccess(buildingID, ref buildingData);
            ProcessBuildingDataFinal(buildingID, ref buildingData);
        }

        public static void ProcessBuildingDataFinal(ushort buildingID, ref Building buildingData)
        {
            if (RealCityPrivateBuildingAI.preBuidlingId > buildingID)
            {
                RealCityPrivateBuildingAI.allOfficeHighTechWorkCountFinal = RealCityPrivateBuildingAI.allOfficeHighTechWorkCount;
                RealCityPrivateBuildingAI.allOfficeLevel1WorkCountFinal = RealCityPrivateBuildingAI.allOfficeLevel1WorkCount;
                RealCityPrivateBuildingAI.allOfficeLevel2WorkCountFinal = RealCityPrivateBuildingAI.allOfficeLevel2WorkCount;
                RealCityPrivateBuildingAI.allOfficeLevel3WorkCountFinal = RealCityPrivateBuildingAI.allOfficeLevel3WorkCount;
                RealCityPrivateBuildingAI.profitBuildingCountFinal = RealCityPrivateBuildingAI.profitBuildingCount;
                BuildingData.commBuildingNumFinal = BuildingData.commBuildingNum;
                BuildingData.commBuildingNum = 0;
                RealCityPrivateBuildingAI.profitBuildingCount = 0;
                RealCityPrivateBuildingAI.allOfficeLevel1WorkCount = 0;
                RealCityPrivateBuildingAI.allOfficeLevel2WorkCount = 0;
                RealCityPrivateBuildingAI.allOfficeLevel3WorkCount = 0;
                RealCityPrivateBuildingAI.allOfficeHighTechWorkCount = 0;
            }
            RealCityPrivateBuildingAI.preBuidlingId = buildingID;
            if (buildingData.Info.m_class.m_service == ItemClass.Service.Residential)
            {
                BuildingData.buildingMoney[buildingID] = 0;
            }

            float building_money = BuildingData.buildingMoney[buildingID];

            if (buildingData.Info.m_class.m_service == ItemClass.Service.Industrial)
            {
                if (building_money < 0)
                    RealCityEconomyExtension.industrialLackMoneyCount++;
                else
                    RealCityEconomyExtension.industrialEarnMoneyCount++;
            }

            if (building_money < 0)
                BuildingData.buildingMoneyThreat[buildingID] = 1.0f;

            switch (buildingData.Info.m_class.m_service)
            {
                case ItemClass.Service.Residential:
                    float familyMoney = GetResidentialBuildingAverageMoney(buildingData);
                    if (familyMoney < (MainDataStore.highWealth >> 1))
                        BuildingData.buildingMoneyThreat[buildingID] = 1.0f - familyMoney / MainDataStore.highWealth;
                    else
                        BuildingData.buildingMoneyThreat[buildingID] = (((MainDataStore.highWealth << 1) - (MainDataStore.highWealth >> 1)) - familyMoney) / (MainDataStore.highWealth << 1);

                    if (BuildingData.buildingMoneyThreat[buildingID] < 0)
                        BuildingData.buildingMoneyThreat[buildingID] = 0;
                    break;
                case ItemClass.Service.Commercial:
                case ItemClass.Service.Industrial:
                case ItemClass.Service.Office:
                    float averageBuildingSalary = BuildingUI.CaculateEmployeeOutcome(buildingData, out _);

                    if (MainDataStore.citizenCount > 0.0)
                    {
                        float averageCitySalary = MainDataStore.citizenSalaryTotal / MainDataStore.citizenCount;
                        float salaryFactor = averageBuildingSalary / averageCitySalary;
                        if (salaryFactor > 3f)
                            salaryFactor = 3f;
                        else if (salaryFactor < 0.0f)
                            salaryFactor = 0.0f;

                        BuildingData.buildingMoneyThreat[buildingID] = (1.0f - salaryFactor / 3f);
                    }
                    else
                        BuildingData.buildingMoneyThreat[buildingID] = 1.0f;

                    break;
            }

            if (buildingData.Info.m_class.m_service == ItemClass.Service.Commercial)
            {
                //get all commercial building for resident.
                if (buildingData.m_customBuffer2 > 2000)
                {
                    BuildingData.commBuildingNum++;
                    BuildingData.commBuildingID[BuildingData.commBuildingNum] = buildingID;
                }
                if (BuildingData.buildingMoney[buildingID] < 0)
                {
                    RealCityEconomyExtension.commercialLackMoneyCount++;
                }
                else
                {
                    RealCityEconomyExtension.commercialEarnMoneyCount++;
                }
            }

            if (buildingData.m_problems == Notification.Problem.None)
            {
                //mark no good
                if ((buildingData.Info.m_class.m_service == ItemClass.Service.Commercial) && (RealCity.debugMode))
                {
                    Notification.Problem problem = Notification.RemoveProblems(buildingData.m_problems, Notification.Problem.NoGoods);
                    if (buildingData.m_customBuffer2 < 500)
                    {
                        problem = Notification.AddProblems(problem, Notification.Problem.NoGoods | Notification.Problem.MajorProblem);
                    }
                    else if (buildingData.m_customBuffer2 < 1000)
                    {
                        problem = Notification.AddProblems(problem, Notification.Problem.NoGoods);
                    }
                    else
                    {
                        problem = Notification.Problem.None;
                    }
                    buildingData.m_problems = problem;
                }
            }
        }

        public static float GetResidentialBuildingAverageMoney(Building buildingData)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint citzenUnit = buildingData.m_citizenUnits;
            int unitCount = 0;
            long totalMoney = 0;
            float averageMoney = 0;
            while (citzenUnit != 0u)
            {
                if ((ushort)(instance.m_units.m_buffer[citzenUnit].m_flags & CitizenUnit.Flags.Home) != 0)
                {
                    if ((instance.m_units.m_buffer[citzenUnit].m_citizen0 != 0) || (instance.m_units.m_buffer[citzenUnit].m_citizen1 != 0) || (instance.m_units.m_buffer[citzenUnit].m_citizen2 != 0) || (instance.m_units.m_buffer[citzenUnit].m_citizen3 != 0) || (instance.m_units.m_buffer[citzenUnit].m_citizen4 != 0))
                    {
                        unitCount++;
                        totalMoney += (long)CitizenUnitData.familyMoney[citzenUnit];
                    }
                }
                citzenUnit = instance.m_units.m_buffer[citzenUnit].m_nextUnit;
                if (++unitCount > 524288)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }

            if (unitCount != 0)
            {
                averageMoney = (float)totalMoney / unitCount;
            }

            return averageMoney;
        }

        public static void LimitCommericalBuildingAccess(ushort buildingID, ref Building buildingData)
        {
            if (buildingData.Info.m_class.m_service == ItemClass.Service.Commercial)
            {
                Citizen.BehaviourData behaviour = default;
                int aliveVisitCount = 0;
                int totalVisitCount = 0;
                RealCityCommercialBuildingAI.InitDelegate();
                RealCityCommercialBuildingAI.GetVisitBehaviour((CommercialBuildingAI)buildingData.Info.m_buildingAI, buildingID, ref buildingData, ref behaviour, ref aliveVisitCount, ref totalVisitCount);
                var amount = buildingData.m_customBuffer2 / MainDataStore.maxGoodPurchase - totalVisitCount + aliveVisitCount;
                var AI = buildingData.Info.m_buildingAI as CommercialBuildingAI;
                var maxcount = AI.CalculateVisitplaceCount((ItemClass.Level)buildingData.m_level, new Randomizer(buildingID), buildingData.m_width, buildingData.m_length);
                if ((amount <= 0) || (maxcount <= totalVisitCount))
                {
                    buildingData.m_flags &= ~Building.Flags.Active;
                }

                if (RealCityEconomyExtension.Can16timesUpdate(buildingID))
                {
                    //Remove citizen which already have goods
                    CitizenManager instance = Singleton<CitizenManager>.instance;
                    uint num = buildingData.m_citizenUnits;
                    int num2 = 0;
                    while (num != 0u)
                    {
                        if ((ushort)(instance.m_units.m_buffer[(int)((UIntPtr)num)].m_flags & CitizenUnit.Flags.Visit) != 0)
                        {
                            var citizenID = instance.m_units.m_buffer[(int)((UIntPtr)num)].m_citizen0;
                            if (citizenID != 0u)
                            {
                                ushort homeBuilding = instance.m_citizens.m_buffer[citizenID].m_homeBuilding;
                                uint citizenUnit = CitizenData.GetCitizenUnit(homeBuilding);
                                uint containingUnit = instance.m_citizens.m_buffer[citizenID].GetContainingUnit((uint)citizenID, citizenUnit, CitizenUnit.Flags.Home);
                                if (CitizenUnitData.familyGoods[containingUnit] > 2000)
                                {
                                    if (!instance.m_citizens.m_buffer[citizenID].m_flags.IsFlagSet(Citizen.Flags.Tourist))
                                    {
                                        BuildingManager instance1 = Singleton<BuildingManager>.instance;
                                        instance.m_citizens.m_buffer[citizenID].RemoveFromUnits(citizenID, instance1.m_buildings.m_buffer[buildingID].m_citizenUnits, CitizenUnit.Flags.Visit);
                                        return;
                                    }
                                }
                            }
                            citizenID = instance.m_units.m_buffer[(int)((UIntPtr)num)].m_citizen1;
                            if (citizenID != 0u)
                            {
                                ushort homeBuilding = instance.m_citizens.m_buffer[citizenID].m_homeBuilding;
                                uint citizenUnit = CitizenData.GetCitizenUnit(homeBuilding);
                                uint containingUnit = instance.m_citizens.m_buffer[citizenID].GetContainingUnit((uint)citizenID, citizenUnit, CitizenUnit.Flags.Home);
                                if (CitizenUnitData.familyGoods[containingUnit] > 2000)
                                {
                                    if (!instance.m_citizens.m_buffer[citizenID].m_flags.IsFlagSet(Citizen.Flags.Tourist))
                                    {
                                        BuildingManager instance1 = Singleton<BuildingManager>.instance;
                                        instance.m_citizens.m_buffer[citizenID].RemoveFromUnits(citizenID, instance1.m_buildings.m_buffer[buildingID].m_citizenUnits, CitizenUnit.Flags.Visit);
                                        return;
                                    }
                                }
                            }
                            citizenID = instance.m_units.m_buffer[(int)((UIntPtr)num)].m_citizen2;
                            if (citizenID != 0u)
                            {
                                ushort homeBuilding = instance.m_citizens.m_buffer[citizenID].m_homeBuilding;
                                uint citizenUnit = CitizenData.GetCitizenUnit(homeBuilding);
                                uint containingUnit = instance.m_citizens.m_buffer[citizenID].GetContainingUnit((uint)citizenID, citizenUnit, CitizenUnit.Flags.Home);
                                if (CitizenUnitData.familyGoods[containingUnit] > 2000)
                                {
                                    if (!instance.m_citizens.m_buffer[citizenID].m_flags.IsFlagSet(Citizen.Flags.Tourist))
                                    {
                                        BuildingManager instance1 = Singleton<BuildingManager>.instance;
                                        instance.m_citizens.m_buffer[citizenID].RemoveFromUnits(citizenID, instance1.m_buildings.m_buffer[buildingID].m_citizenUnits, CitizenUnit.Flags.Visit);
                                        return;
                                    }
                                }
                            }
                            citizenID = instance.m_units.m_buffer[(int)((UIntPtr)num)].m_citizen3;
                            if (citizenID != 0u)
                            {
                                ushort homeBuilding = instance.m_citizens.m_buffer[citizenID].m_homeBuilding;
                                uint citizenUnit = CitizenData.GetCitizenUnit(homeBuilding);
                                uint containingUnit = instance.m_citizens.m_buffer[citizenID].GetContainingUnit((uint)citizenID, citizenUnit, CitizenUnit.Flags.Home);
                                if (CitizenUnitData.familyGoods[containingUnit] > 2000)
                                {
                                    if (!instance.m_citizens.m_buffer[citizenID].m_flags.IsFlagSet(Citizen.Flags.Tourist))
                                    {
                                        BuildingManager instance1 = Singleton<BuildingManager>.instance;
                                        instance.m_citizens.m_buffer[citizenID].RemoveFromUnits(citizenID, instance1.m_buildings.m_buffer[buildingID].m_citizenUnits, CitizenUnit.Flags.Visit);
                                        return;
                                    }
                                }
                            }
                            citizenID = instance.m_units.m_buffer[(int)((UIntPtr)num)].m_citizen4;
                            if (citizenID != 0u)
                            {
                                ushort homeBuilding = instance.m_citizens.m_buffer[citizenID].m_homeBuilding;
                                uint citizenUnit = CitizenData.GetCitizenUnit(homeBuilding);
                                uint containingUnit = instance.m_citizens.m_buffer[citizenID].GetContainingUnit((uint)citizenID, citizenUnit, CitizenUnit.Flags.Home);
                                if (CitizenUnitData.familyGoods[containingUnit] > 2000)
                                {
                                    if (!instance.m_citizens.m_buffer[citizenID].m_flags.IsFlagSet(Citizen.Flags.Tourist))
                                    {
                                        BuildingManager instance1 = Singleton<BuildingManager>.instance;
                                        instance.m_citizens.m_buffer[citizenID].RemoveFromUnits(citizenID, instance1.m_buildings.m_buffer[buildingID].m_citizenUnits, CitizenUnit.Flags.Visit);
                                        return;
                                    }
                                }
                            }
                        }
                        num = instance.m_units.m_buffer[(int)((UIntPtr)num)].m_nextUnit;
                        if (++num2 > 524288)
                        {
                            CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                            break;
                        }
                    }
                }
            }
        }

        public static void CalculateBuildingMoneyAndSalary(Building building, ushort buildingID)
        {
            if (BuildingData.buildingMoney[buildingID] > MainDataStore.maxBuildingMoneyLimit)
            {
                BuildingData.buildingMoney[buildingID] = MainDataStore.maxBuildingMoneyLimit;
            }
            else if (BuildingData.buildingMoney[buildingID] < -MainDataStore.maxBuildingMoneyLimit)
            {
                BuildingData.buildingMoney[buildingID] = -MainDataStore.maxBuildingMoneyLimit;
            }


            if (building.Info.m_class.m_service == ItemClass.Service.Industrial || building.Info.m_class.m_service == ItemClass.Service.Commercial || building.Info.m_class.m_service == ItemClass.Service.Office)
            {
                Citizen.BehaviourData behaviourData = default;
                int aliveWorkerCount = 0;
                int totalWorkerCount = 0;
                RealCityCommonBuildingAI.InitDelegate();
                RealCityCommonBuildingAI.GetWorkBehaviour((CommonBuildingAI)building.Info.m_buildingAI, buildingID, ref building, ref behaviourData, ref aliveWorkerCount, ref totalWorkerCount);
                float bossTake = 0;
                float investToOffice = 0;
                float profitShare = 0;

                switch (building.Info.m_class.m_subService)
                {
                    case ItemClass.SubService.OfficeGeneric:
                    case ItemClass.SubService.OfficeHightech:
                        profitShare = 1f; break;
                    case ItemClass.SubService.IndustrialFarming:
                    case ItemClass.SubService.IndustrialForestry:
                        if (building.Info.m_buildingAI is IndustrialExtractorAI)
                        {
                            bossTake = MainDataStore.bossRatioInduExtractor;
                            investToOffice = MainDataStore.investRatioInduExtractor;
                            profitShare = MainDataStore.profitShareRatioInduExtractor;
                        }
                        else
                        {
                            bossTake = MainDataStore.bossRatioInduOther;
                            investToOffice = MainDataStore.investRatioInduOther;
                            profitShare = MainDataStore.profitShareRatioInduOther;
                        }
                        break;
                    case ItemClass.SubService.IndustrialOil:
                    case ItemClass.SubService.IndustrialOre:
                        bossTake = MainDataStore.bossRatioInduOther;
                        investToOffice = MainDataStore.investRatioInduOther;
                        profitShare = MainDataStore.profitShareRatioInduOther; break;
                    case ItemClass.SubService.IndustrialGeneric:
                        if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                        {
                            bossTake = MainDataStore.bossRatioInduLevel1;
                            investToOffice = MainDataStore.investRatioInduLevel1;
                            profitShare = MainDataStore.profitShareRatioInduLevel1;
                        }
                        else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                        {
                            bossTake = MainDataStore.bossRatioInduLevel2;
                            investToOffice = MainDataStore.investRatioInduLevel2;
                            profitShare = MainDataStore.profitShareRatioInduLevel2;
                        }
                        else
                        {
                            bossTake = MainDataStore.bossRatioInduLevel3;
                            investToOffice = MainDataStore.investRatioInduLevel3;
                            profitShare = MainDataStore.profitShareRatioInduLevel3;
                        }
                        break;
                    case ItemClass.SubService.CommercialHigh:
                    case ItemClass.SubService.CommercialLow:
                        if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                        {
                            bossTake = MainDataStore.bossRatioCommLevel1;
                            investToOffice = MainDataStore.investRatioCommLevel1;
                            profitShare = MainDataStore.profitShareRatioCommLevel1;
                        }
                        else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                        {
                            bossTake = MainDataStore.bossRatioCommLevel2;
                            investToOffice = MainDataStore.investRatioCommLevel2;
                            profitShare = MainDataStore.profitShareRatioCommLevel2;
                        }
                        else
                        {
                            bossTake = MainDataStore.bossRatioCommLevel3;
                            investToOffice = MainDataStore.investRatioCommLevel3;
                            profitShare = MainDataStore.profitShareRatioCommLevel1;
                        }
                        break;
                    case ItemClass.SubService.CommercialTourist:
                        bossTake = MainDataStore.bossRatioCommTou;
                        investToOffice = MainDataStore.investRatioCommTou;
                        profitShare = MainDataStore.profitShareRatioCommTou;
                        break;
                    case ItemClass.SubService.CommercialLeisure:
                        bossTake = MainDataStore.bossRatioCommOther;
                        investToOffice = MainDataStore.investRatioCommOther;
                        profitShare = MainDataStore.profitShareRatioCommOther; break;
                    case ItemClass.SubService.CommercialEco:
                        bossTake = MainDataStore.bossRatioCommECO;
                        investToOffice = MainDataStore.investRatioCommECO;
                        profitShare = MainDataStore.profitShareRatioCommECO; break;
                }
                // boss take and return to office
                if (BuildingData.buildingMoney[buildingID] > 0)
                {
                    //Reduce Boss fee
                    long investToOfficeFee = (long)(BuildingData.buildingMoney[buildingID] * investToOffice);
                    long bossTakeFee = (long)(BuildingData.buildingMoney[buildingID] * bossTake);
                    if (building.Info.m_class.m_service == ItemClass.Service.Commercial)
                    {
                        //Commercial have help tourism
                        MainDataStore.outsideTouristMoney += ((bossTakeFee - investToOfficeFee) * MainDataStore.outsideCompanyProfitRatio * MainDataStore.outsideTouristSalaryProfitRatio);
                    }
                    RealCityPrivateBuildingAI.profitBuildingMoney += investToOfficeFee;
                    BuildingData.buildingMoney[buildingID] -= bossTakeFee;
                }

                if (building.Info.m_class.m_service == ItemClass.Service.Office)
                {
                    float allOfficeWorker = RealCityPrivateBuildingAI.allOfficeLevel1WorkCountFinal + RealCityPrivateBuildingAI.allOfficeLevel2WorkCountFinal + RealCityPrivateBuildingAI.allOfficeLevel3WorkCountFinal + RealCityPrivateBuildingAI.allOfficeHighTechWorkCountFinal;
                    float averageOfficeSalary = 0;
                    if (allOfficeWorker != 0)
                    {
                        averageOfficeSalary = (RealCityPrivateBuildingAI.profitBuildingMoneyFinal / allOfficeWorker);
                    }

                    if (building.Info.m_class.m_subService == ItemClass.SubService.OfficeGeneric)
                    {
                        if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                        {
                            BuildingData.buildingMoney[buildingID] = averageOfficeSalary * totalWorkerCount * 0.6f;
                        }
                        else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                        {
                            BuildingData.buildingMoney[buildingID] = averageOfficeSalary * totalWorkerCount * 0.8f;
                        }
                        else if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                        {
                            BuildingData.buildingMoney[buildingID] = averageOfficeSalary * totalWorkerCount * 1f;
                        }
                    }
                    else if (building.Info.m_class.m_subService == ItemClass.SubService.OfficeHightech)
                    {
                        BuildingData.buildingMoney[buildingID] = averageOfficeSalary * totalWorkerCount * 0.75f;
                    }

                    ProcessLandFeeOffice(building, buildingID, totalWorkerCount);
                }

                //Calculate building salary
                int buildingAsset = (int)(BuildingData.buildingMoney[buildingID] + building.m_customBuffer1 * RealCityIndustryBuildingAI.GetResourcePrice(RealCityPrivateBuildingAI.GetIncomingProductionType(buildingID, building)));
                int salary = 0;
                if ((buildingAsset > 0) && (totalWorkerCount != 0))
                {
                    salary = (int)(buildingAsset * profitShare / totalWorkerCount);
                    switch (building.Info.m_class.m_subService)
                    {
                        case ItemClass.SubService.IndustrialFarming:
                        case ItemClass.SubService.IndustrialForestry:
                        case ItemClass.SubService.IndustrialOil:
                        case ItemClass.SubService.IndustrialOre:
                            salary = Math.Min(salary, MainDataStore.salaryInduOtherMax); break;
                        case ItemClass.SubService.IndustrialGeneric:
                            if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                                salary = Math.Min(salary, MainDataStore.salaryInduLevel1Max);
                            else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                                salary = Math.Min(salary, MainDataStore.salaryInduLevel2Max);
                            else
                                salary = Math.Min(salary, MainDataStore.salaryInduLevel3Max);
                            break;
                        case ItemClass.SubService.CommercialHigh:
                        case ItemClass.SubService.CommercialLow:
                            if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                                salary = Math.Min(salary, MainDataStore.salaryCommLevel1Max);
                            else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                                salary = Math.Min(salary, MainDataStore.salaryCommLevel2Max);
                            else
                                salary = Math.Min(salary, MainDataStore.salaryCommLevel3Max);
                            break;
                        case ItemClass.SubService.CommercialTourist:
                            salary = Math.Min(salary, MainDataStore.salaryCommTouMax); break;
                        case ItemClass.SubService.CommercialLeisure:
                            salary = Math.Min(salary, MainDataStore.salaryCommOtherMax); break;
                        case ItemClass.SubService.CommercialEco:
                            salary = Math.Min(salary, MainDataStore.salaryCommECOMax); break;
                    }
                }
                BuildingData.buildingWorkCount[buildingID] = salary;
            }
            else
            {
                //resident building
                ItemClass @class = building.Info.m_class;
                int incomeAccumulation = 0;
                DistrictManager instance = Singleton<DistrictManager>.instance;
                byte district = instance.GetDistrict(building.m_position);
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
                incomeAccumulation = (int)((num2 * incomeAccumulation) / 100f);
                BuildingData.buildingWorkCount[buildingID] = incomeAccumulation;
            }
        }

        public static void ProcessLandFeeOffice(Building building, ushort buildingID, int totalWorkerCount)
        {
            DistrictManager instance = Singleton<DistrictManager>.instance;
            byte district = instance.GetDistrict(building.m_position);
            DistrictPolicies.Services servicePolicies = instance.m_districts.m_buffer[district].m_servicePolicies;
            DistrictPolicies.Taxation taxationPolicies = instance.m_districts.m_buffer[district].m_taxationPolicies;

            int landFee = totalWorkerCount * 10;
            int taxRate;
            taxRate = Singleton<EconomyManager>.instance.GetTaxRate(building.Info.m_class, taxationPolicies);

            if (instance.IsPolicyLoaded(DistrictPolicies.Policies.ExtraInsulation))
            {
                if ((servicePolicies & DistrictPolicies.Services.ExtraInsulation) != DistrictPolicies.Services.None)
                {
                    landFee = landFee * 95 / 100;
                }
            }
            if ((servicePolicies & DistrictPolicies.Services.Recycling) != DistrictPolicies.Services.None)
            {
                landFee = landFee * 95 / 100;
            }

            if (BuildingData.buildingMoney[buildingID] >= 0)
            {
                BuildingData.buildingMoney[buildingID] = (BuildingData.buildingMoney[buildingID] - (float)(landFee * taxRate) / 100);
                Singleton<EconomyManager>.instance.AddPrivateIncome(landFee, building.Info.m_class.m_service, building.Info.m_class.m_subService, building.Info.m_class.m_level, taxRate * 100);
            }
        }

        public static void ProcessLandFeeNoOffice(Building building, ushort buildingID)
        {
            DistrictManager instance = Singleton<DistrictManager>.instance;
            byte district = instance.GetDistrict(building.m_position);
            DistrictPolicies.Services servicePolicies = instance.m_districts.m_buffer[district].m_servicePolicies;
            DistrictPolicies.Taxation taxationPolicies = instance.m_districts.m_buffer[district].m_taxationPolicies;

            int landFee;
            GetLandRentNoOffice(out landFee, building, buildingID);
            int taxRate;

            taxRate = Singleton<EconomyManager>.instance.GetTaxRate(building.Info.m_class, taxationPolicies);

            if (((taxationPolicies & DistrictPolicies.Taxation.DontTaxLeisure) != DistrictPolicies.Taxation.None) && (building.Info.m_class.m_subService == ItemClass.SubService.CommercialLeisure))
            {
                landFee = 0;
            }

            if (instance.IsPolicyLoaded(DistrictPolicies.Policies.ExtraInsulation))
            {
                if ((servicePolicies & DistrictPolicies.Services.ExtraInsulation) != DistrictPolicies.Services.None)
                {
                    landFee = landFee * 95 / 100;
                }
            }
            if ((servicePolicies & DistrictPolicies.Services.Recycling) != DistrictPolicies.Services.None)
            {
                landFee = landFee * 95 / 100;
            }

            if (BuildingData.buildingMoney[buildingID] <= ((landFee * taxRate) / 100f))
            {
                landFee = 0;
            }
            else
            {
                RealCityPrivateBuildingAI.profitBuildingCount++;
            }

            if (RealCityEconomyExtension.Can16timesUpdate(buildingID))
            {
                Singleton<EconomyManager>.instance.AddPrivateIncome(landFee, building.Info.m_class.m_service, building.Info.m_class.m_subService, building.Info.m_class.m_level, taxRate * 100);
                if ((building.Info.m_class.m_service == ItemClass.Service.Commercial) || (building.Info.m_class.m_service == ItemClass.Service.Industrial))
                {
                    BuildingData.buildingMoney[buildingID] = (BuildingData.buildingMoney[buildingID] - (float)(landFee * taxRate) / 100);
                }
            }
        }

        public static void GetLandRentNoOffice(out int landRent, Building building, ushort buildingID)
        {
            ItemClass @class = building.Info.m_class;
            landRent = 0;
            Citizen.BehaviourData behaviourData = default;
            int aliveWorkerCount = 0;
            int totalWorkerCount = 0;
            ItemClass.SubService subService = @class.m_subService;
            RealCityCommonBuildingAI.InitDelegate();
            switch (subService)
            {
                case ItemClass.SubService.OfficeHightech:
                    RealCityCommonBuildingAI.GetWorkBehaviour((OfficeBuildingAI)building.Info.m_buildingAI, buildingID, ref building, ref behaviourData, ref aliveWorkerCount, ref totalWorkerCount);
                    RealCityPrivateBuildingAI.allOfficeHighTechWorkCount += totalWorkerCount;
                    break;
                case ItemClass.SubService.OfficeGeneric:
                    RealCityCommonBuildingAI.GetWorkBehaviour((OfficeBuildingAI)building.Info.m_buildingAI, buildingID, ref building, ref behaviourData, ref aliveWorkerCount, ref totalWorkerCount);
                    if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        RealCityPrivateBuildingAI.allOfficeLevel1WorkCount += totalWorkerCount;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        RealCityPrivateBuildingAI.allOfficeLevel2WorkCount += totalWorkerCount;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        RealCityPrivateBuildingAI.allOfficeLevel3WorkCount += totalWorkerCount;
                    }
                    break;
                case ItemClass.SubService.IndustrialFarming:
                    landRent = MainDataStore.induFarm;
                    break;
                case ItemClass.SubService.IndustrialForestry:
                    landRent = MainDataStore.induForest;
                    break;
                case ItemClass.SubService.IndustrialOil:
                    landRent = MainDataStore.induOil;
                    break;
                case ItemClass.SubService.IndustrialOre:
                    landRent = MainDataStore.induOre;
                    break;
                case ItemClass.SubService.IndustrialGeneric:
                    if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        landRent = MainDataStore.induGenLevel1;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        landRent = MainDataStore.induGenLevel2;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        landRent = MainDataStore.induGenLevel3;
                    }
                    break;
                case ItemClass.SubService.CommercialHigh:
                    if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        landRent = MainDataStore.commHighLevel1;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        landRent = MainDataStore.commHighLevel2;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        landRent = MainDataStore.commHighLevel3;
                    }
                    break;
                case ItemClass.SubService.CommercialLow:
                    if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        landRent = MainDataStore.commLowLevel1;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        landRent = MainDataStore.commLowLevel2;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        landRent = MainDataStore.commLowLevel3;
                    }
                    break;
                case ItemClass.SubService.CommercialLeisure:
                    landRent = MainDataStore.commLeisure;
                    break;
                case ItemClass.SubService.CommercialTourist:
                    landRent = MainDataStore.commTourist;
                    break;
                case ItemClass.SubService.CommercialEco:
                    landRent = MainDataStore.commEco;
                    break;
                default: break;
            }
        }
    }
}
