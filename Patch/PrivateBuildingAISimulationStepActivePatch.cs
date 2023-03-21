using ColossalFramework;
using ColossalFramework.Math;
using RealCity.Util;
using RealCity.UI;
using Harmony;
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
            if (RealCityEconomyExtension.Can16timesUpdate(buildingID))
            {
                LimitAndCheckOfficeMoney(buildingData, buildingID);
            }
            ProcessLandFeeNoOffice(buildingData, buildingID);
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
                    break;

                case ItemClass.Service.Commercial:
                case ItemClass.Service.Industrial:
                case ItemClass.Service.Office:
                    float averageBuildingSalary = BuildingUI.CaculateEmployeeOutcome(buildingData, out _);

                    if (MainDataStore.citizenCount > 0.0)
                    {
                        float averageCitySalary = MainDataStore.citizenSalaryTotal / MainDataStore.citizenCount;
                        float salaryFactor = averageBuildingSalary / averageCitySalary;
                        if (salaryFactor > 1.6f)
                            salaryFactor = 1.6f;
                        else if (salaryFactor < 0.0f)
                            salaryFactor = 0.0f;

                        BuildingData.buildingMoneyThreat[buildingID] = (1.0f - salaryFactor / 1.6f);
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

            if (buildingData.m_problems == Notification.Problem1.None)
            {
                //mark no good
                if ((buildingData.Info.m_class.m_service == ItemClass.Service.Commercial) && (RealCity.debugMode))
                {
                    Notification.Problem1 problem = Notification.RemoveProblems(buildingData.m_problems, Notification.Problem1.NoGoods);
                    if (buildingData.m_customBuffer2 < 500)
                    {
                        problem = Notification.AddProblems(problem, Notification.Problem1.NoGoods | Notification.Problem1.MajorProblem);
                    }
                    else if (buildingData.m_customBuffer2 < 1000)
                    {
                        problem = Notification.AddProblems(problem, Notification.Problem1.NoGoods);
                    }
                    else
                    {
                        problem = Notification.Problem1.None;
                    }
                    buildingData.m_problems = problem;
                }
            }
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

        public static void LimitAndCheckOfficeMoney(Building building, ushort buildingID)
        {
            if (BuildingData.buildingMoney[buildingID] > 100000000f)
            {
                BuildingData.buildingMoney[buildingID] = 100000000f;
            }
            else if (BuildingData.buildingMoney[buildingID] < -100000000f)
            {
                BuildingData.buildingMoney[buildingID] = -100000000f;
            }

            if (BuildingData.buildingMoney[buildingID] > 0)
            {
                if (building.Info.m_class.m_service == ItemClass.Service.Industrial || building.Info.m_class.m_service == ItemClass.Service.Commercial)
                {
                    float bossTake = 0;
                    float investToOffice = 0;
                    // boss take and return to office
                    switch (building.Info.m_class.m_subService)
                    {
                        case ItemClass.SubService.IndustrialFarming:
                        case ItemClass.SubService.IndustrialForestry:
                            if (building.Info.m_buildingAI is IndustrialExtractorAI)
                            {
                                bossTake = MainDataStore.bossRatioInduExtractor;
                                investToOffice = MainDataStore.investRatioInduExtractor;
                            }
                            else
                            {
                                bossTake = MainDataStore.bossRatioInduOther;
                                investToOffice = MainDataStore.investRatioInduOther;
                            }
                            break;
                        case ItemClass.SubService.IndustrialOil:
                        case ItemClass.SubService.IndustrialOre:
                            bossTake = MainDataStore.bossRatioInduOther; investToOffice = MainDataStore.investRatioInduOther; break;
                        case ItemClass.SubService.IndustrialGeneric:
                            if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                            {
                                bossTake = MainDataStore.bossRatioInduLevel1;
                                investToOffice = MainDataStore.investRatioInduLevel1;
                            }
                            else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                            {
                                bossTake = MainDataStore.bossRatioInduLevel2;
                                investToOffice = MainDataStore.investRatioInduLevel2;
                            }
                            else
                            {
                                bossTake = MainDataStore.bossRatioInduLevel3;
                                investToOffice = MainDataStore.investRatioInduLevel3;
                            }
                            break;
                        case ItemClass.SubService.CommercialHigh:
                        case ItemClass.SubService.CommercialLow:
                            if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                            {
                                bossTake = MainDataStore.bossRatioCommLevel1;
                                investToOffice = MainDataStore.investRatioCommLevel1;
                            }
                            else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                            {
                                bossTake = MainDataStore.bossRatioCommLevel2;
                                investToOffice = MainDataStore.investRatioCommLevel2;
                            }
                            else
                            {
                                bossTake = MainDataStore.bossRatioCommLevel3;
                                investToOffice = MainDataStore.investRatioCommLevel3;
                            }
                            break;
                        case ItemClass.SubService.CommercialTourist:
                            bossTake = MainDataStore.bossRatioCommTou; investToOffice = MainDataStore.investRatioCommTou; break;
                        case ItemClass.SubService.CommercialLeisure:
                            bossTake = MainDataStore.bossRatioCommOther; investToOffice = MainDataStore.investRatioCommOther; break;
                        case ItemClass.SubService.CommercialEco:
                            bossTake = MainDataStore.bossRatioCommECO; investToOffice = MainDataStore.investRatioCommECO; break;
                    }


                    RealCityPrivateBuildingAI.profitBuildingMoney += (long)(BuildingData.buildingMoney[buildingID] * investToOffice);
                    BuildingData.buildingMoney[buildingID] -= (long)(BuildingData.buildingMoney[buildingID] * bossTake);
                }
            }

            if (building.Info.m_class.m_service == ItemClass.Service.Office)
            {
                Citizen.BehaviourData behaviourData = default(Citizen.BehaviourData);
                int aliveWorkerCount = 0;
                int totalWorkerCount = 0;
                RealCityCommonBuildingAI.InitDelegate();
                RealCityCommonBuildingAI.GetWorkBehaviour((OfficeBuildingAI)building.Info.m_buildingAI, buildingID, ref building, ref behaviourData, ref aliveWorkerCount, ref totalWorkerCount);
                int allOfficeWorker = RealCityPrivateBuildingAI.allOfficeLevel1WorkCountFinal + RealCityPrivateBuildingAI.allOfficeLevel2WorkCountFinal + RealCityPrivateBuildingAI.allOfficeLevel3WorkCountFinal + RealCityPrivateBuildingAI.allOfficeHighTechWorkCountFinal;
                double a = 0;
                double z = 0;
                double c = allOfficeWorker * MainDataStore.citizenCount << 1;
                if (allOfficeWorker != 0 && (c != 0))
                {
                    a = (RealCityPrivateBuildingAI.profitBuildingMoneyFinal / (double)(c));
                }
                if ((MainDataStore.citizenCount != 0))
                {
                    z = Math.Pow((aliveWorkerCount / (float)MainDataStore.citizenCount), a);
                }

                if (building.Info.m_class.m_subService == ItemClass.SubService.OfficeGeneric)
                {
                    if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        if (allOfficeWorker != 0)
                        {
                            BuildingData.buildingMoney[buildingID] = (float)(RealCityPrivateBuildingAI.profitBuildingMoneyFinal * 0.65f * z * (aliveWorkerCount / (float)allOfficeWorker));
                            BuildingData.buildingMoney[buildingID] = (BuildingData.buildingMoney[buildingID] > 0) ? BuildingData.buildingMoney[buildingID] : 0;
                        }
                        else
                        {
                            BuildingData.buildingMoney[buildingID] = 0;
                        }
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        if (allOfficeWorker != 0)
                        {
                            BuildingData.buildingMoney[buildingID] = (float)(RealCityPrivateBuildingAI.profitBuildingMoneyFinal * 0.7f * z * (aliveWorkerCount / (float)allOfficeWorker));
                            BuildingData.buildingMoney[buildingID] = (BuildingData.buildingMoney[buildingID] > 0) ? BuildingData.buildingMoney[buildingID] : 0;
                        }
                        else
                        {
                            BuildingData.buildingMoney[buildingID] = 0;
                        }
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        if (allOfficeWorker != 0)
                        {
                            BuildingData.buildingMoney[buildingID] = (float)(RealCityPrivateBuildingAI.profitBuildingMoneyFinal * 0.8f * z * (aliveWorkerCount / (float)allOfficeWorker));
                            BuildingData.buildingMoney[buildingID] = (BuildingData.buildingMoney[buildingID] > 0) ? BuildingData.buildingMoney[buildingID] : 0;
                        }
                        else
                        {
                            BuildingData.buildingMoney[buildingID] = 0;
                        }
                    }
                }
                else if (building.Info.m_class.m_subService == ItemClass.SubService.OfficeHightech)
                {
                    if (allOfficeWorker != 0)
                    {
                        BuildingData.buildingMoney[buildingID] = (float)(RealCityPrivateBuildingAI.profitBuildingMoneyFinal * 0.75f * z * (aliveWorkerCount / (float)allOfficeWorker));
                        BuildingData.buildingMoney[buildingID] = (BuildingData.buildingMoney[buildingID] > 0) ? BuildingData.buildingMoney[buildingID] : 0;
                    }
                    else
                    {
                        BuildingData.buildingMoney[buildingID] = 0;
                    }
                }

                ProcessLandFeeOffice(building, buildingID, totalWorkerCount);
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
                    RealCityPrivateBuildingAI.allOfficeHighTechWorkCount += aliveWorkerCount;
                    break;
                case ItemClass.SubService.OfficeGeneric:
                    RealCityCommonBuildingAI.GetWorkBehaviour((OfficeBuildingAI)building.Info.m_buildingAI, buildingID, ref building, ref behaviourData, ref aliveWorkerCount, ref totalWorkerCount);
                    if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        RealCityPrivateBuildingAI.allOfficeLevel1WorkCount += aliveWorkerCount;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        RealCityPrivateBuildingAI.allOfficeLevel2WorkCount += aliveWorkerCount;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        RealCityPrivateBuildingAI.allOfficeLevel3WorkCount += aliveWorkerCount;
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
