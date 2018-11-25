using ColossalFramework;
using ColossalFramework.Math;
using System;
using UnityEngine;
using System.Text.RegularExpressions;

namespace RealCity
{

    public class RealCityPrivateBuildingAI : CommonBuildingAI
    {
        //2.1 building income

        //public const float goodPrice = 0.8f;
        //public const float petrolPrice = 3.1f;
        //public const float coalPrice = 2.7f;
        //public const float lumberPrice = 2.1f;
        //public const float foodPrice = 1.7f;
        //public const float oilPrice = 2.6f;
        //public const float orePrice = 2.2f;
        //public const float logPrice = 1.8f;
        //public const float grainPrice = 1.4f;

        //public static float preGoodPrice = (foodPrice + lumberPrice + coalPrice + petrolPrice) / 4f;

        public static ushort allBuildings = 0;
        public static uint preBuidlingId = 0;

        public static ushort allOfficeLevel1BuildingCount = 0;
        public static ushort allOfficeLevel2BuildingCount = 0;
        public static ushort allOfficeLevel3BuildingCount = 0;
        public static ushort allOfficeHighTechBuildingCount = 0;

        public static ushort allOfficeLevel1BuildingCountFinal = 0;
        public static ushort allOfficeLevel2BuildingCountFinal = 0;
        public static ushort allOfficeLevel3BuildingCountFinal = 0;
        public static ushort allOfficeHighTechBuildingCountFinal = 0;
        public static long greaterThan20000ProfitBuildingMoney = 0;
        public static long greaterThan20000ProfitBuildingMoneyFinal = 0;
        public static ushort greaterThan20000ProfitBuildingCount = 0;
        public static ushort greaterThan20000ProfitBuildingCountFinal = 0;
        public static ushort allBuildingsFinal = 0;

        public static byte[] saveData = new byte[44];

        public static void Load()
        {
            int i = 0;
            preBuidlingId = SaveAndRestore.load_uint(ref i, saveData);

            allBuildings = SaveAndRestore.load_ushort(ref i, saveData);
            allBuildingsFinal = SaveAndRestore.load_ushort(ref i, saveData);
            allOfficeLevel1BuildingCount = SaveAndRestore.load_ushort(ref i, saveData);
            allOfficeLevel2BuildingCount = SaveAndRestore.load_ushort(ref i, saveData);
            allOfficeLevel3BuildingCount = SaveAndRestore.load_ushort(ref i, saveData);
            allOfficeHighTechBuildingCount = SaveAndRestore.load_ushort(ref i, saveData);

            allOfficeLevel1BuildingCountFinal = SaveAndRestore.load_ushort(ref i, saveData);
            allOfficeLevel2BuildingCountFinal = SaveAndRestore.load_ushort(ref i, saveData);
            allOfficeLevel3BuildingCountFinal = SaveAndRestore.load_ushort(ref i, saveData);
            allOfficeHighTechBuildingCountFinal = SaveAndRestore.load_ushort(ref i, saveData);

            greaterThan20000ProfitBuildingMoney = SaveAndRestore.load_long(ref i, saveData);
            greaterThan20000ProfitBuildingMoneyFinal = SaveAndRestore.load_long(ref i, saveData);
            greaterThan20000ProfitBuildingCount = SaveAndRestore.load_ushort(ref i, saveData);
            greaterThan20000ProfitBuildingCountFinal = SaveAndRestore.load_ushort(ref i, saveData);

            //office_gen_salary_index = saveandrestore.load_float(ref i, saveData);
            //office_high_tech_salary_index = saveandrestore.load_float(ref i, saveData);

            DebugLog.LogToFileOnly("saveData in private building is " + i.ToString());


        }


        public static void save()
        {
            int i = 0;

            //12*2 + 7*4 = 52
            SaveAndRestore.save_uint(ref i, preBuidlingId, ref saveData);

            //20 + 20
            SaveAndRestore.save_ushort(ref i, allBuildings, ref saveData);
            SaveAndRestore.save_ushort(ref i, allBuildingsFinal, ref saveData);
            SaveAndRestore.save_ushort(ref i, allOfficeLevel1BuildingCount, ref saveData);
            SaveAndRestore.save_ushort(ref i, allOfficeLevel2BuildingCount, ref saveData);
            SaveAndRestore.save_ushort(ref i, allOfficeLevel3BuildingCount, ref saveData);
            SaveAndRestore.save_ushort(ref i, allOfficeHighTechBuildingCount, ref saveData);
            SaveAndRestore.save_ushort(ref i, allOfficeLevel1BuildingCountFinal, ref saveData);
            SaveAndRestore.save_ushort(ref i, allOfficeLevel2BuildingCountFinal, ref saveData);
            SaveAndRestore.save_ushort(ref i, allOfficeLevel3BuildingCountFinal, ref saveData);
            SaveAndRestore.save_ushort(ref i, allOfficeHighTechBuildingCountFinal, ref saveData);

            SaveAndRestore.save_long(ref i, greaterThan20000ProfitBuildingMoney, ref saveData);
            SaveAndRestore.save_long(ref i, greaterThan20000ProfitBuildingMoneyFinal, ref saveData);
            SaveAndRestore.save_ushort(ref i, greaterThan20000ProfitBuildingCount, ref saveData);
            SaveAndRestore.save_ushort(ref i, greaterThan20000ProfitBuildingCountFinal, ref saveData);

        }
        protected void CustomSimulationStepActive(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
        {
            if (buildingID > 49152)
            {
                DebugLog.LogToFileOnly("Error: buildingID greater than 49152");
            }
            base.SimulationStepActive(buildingID, ref buildingData, ref frameData);
            ProcessLandFee(buildingData, buildingID);
            //caculate_employee_expense(buildingData, buildingID);
            LimitAndCheckBuildingMoney(buildingData, buildingID);
            if ((buildingData.m_problems & Notification.Problem.MajorProblem) != Notification.Problem.None)
            {
                if (buildingData.m_fireIntensity == 0)
                {
                    buildingData.m_majorProblemTimer = (byte)Mathf.Min(255, (int)(buildingData.m_majorProblemTimer + 1));
                    if (buildingData.m_majorProblemTimer >= 64 && !Singleton<BuildingManager>.instance.m_abandonmentDisabled)
                    {
                        if ((buildingData.m_flags & Building.Flags.Flooded) != Building.Flags.None)
                        {
                            InstanceID id = default(InstanceID);
                            id.Building = buildingID;
                            Singleton<InstanceManager>.instance.SetGroup(id, null);
                            buildingData.m_flags &= ~Building.Flags.Flooded;
                        }
                        buildingData.m_majorProblemTimer = 192;
                        buildingData.m_flags &= ~Building.Flags.Active;
                        buildingData.m_flags |= Building.Flags.Abandoned;
                        buildingData.m_problems = (Notification.Problem.FatalProblem | (buildingData.m_problems & ~Notification.Problem.MajorProblem));
                        base.RemovePeople(buildingID, ref buildingData, 100);
                        this.BuildingDeactivated(buildingID, ref buildingData);
                        Singleton<BuildingManager>.instance.UpdateBuildingRenderer(buildingID, true);
                    }
                }
            }
            else
            {
                buildingData.m_majorProblemTimer = 0;
            }

            ProcessBuildingDataFinal(buildingID, ref buildingData);
            //LimitCommericalBuildingAccess(buildingID, ref buildingData);
            ProcessAdditionProduct(buildingID, ref buildingData);
        }

        public void ProcessAdditionProduct(ushort buildingID, ref Building buildingData)
        {
            if (buildingData.Info.m_class.m_service == ItemClass.Service.Commercial || buildingData.Info.m_class.m_service == ItemClass.Service.Industrial)
            {
                float temp = GetComsumptionDivider(buildingData, buildingID);
                int deltaCustomBuffer1 = MainDataStore.building_buffer1[buildingID] - buildingData.m_customBuffer1;
                if (deltaCustomBuffer1 > 0)
                {
                    if (deltaCustomBuffer1 > 500)
                    {
                        deltaCustomBuffer1 = 500;
                    }
                    buildingData.m_customBuffer1 = (ushort)(buildingData.m_customBuffer1 + deltaCustomBuffer1 - (int)(deltaCustomBuffer1 / temp));
                }
                MainDataStore.building_buffer1[buildingID] = (ushort)buildingData.m_customBuffer1;

                if (Singleton<SimulationManager>.instance.m_isNightTime)
                {
                    int deltaCustomBuffer2 = buildingData.m_customBuffer2 - MainDataStore.building_buffer2[buildingID];
                    if (deltaCustomBuffer2 > 0)
                    {
                        if (deltaCustomBuffer2 > 500)
                        {
                            deltaCustomBuffer2 = 500;
                        }
                        buildingData.m_customBuffer2 = (ushort)(buildingData.m_customBuffer2 + deltaCustomBuffer2);
                    }
                    MainDataStore.building_buffer2[buildingID] = (ushort)buildingData.m_customBuffer2;
                }
            }
        }


        public static string GetProductionType(bool isSelling, ushort buildingID, Building data)
        {
            string material = "";
            if (!isSelling)
            {
                if (data.Info.m_buildingAI is IndustrialExtractorAI)
                {
                }
                else
                {
                    switch (data.Info.m_class.m_subService)
                    {
                        case ItemClass.SubService.CommercialHigh:
                        case ItemClass.SubService.CommercialLow:
                        case ItemClass.SubService.CommercialEco:
                        case ItemClass.SubService.CommercialLeisure:
                        case ItemClass.SubService.CommercialTourist:
                            CommercialBuildingAI commercialBuildingAI = data.Info.m_buildingAI as CommercialBuildingAI;
                            switch (commercialBuildingAI.m_incomingResource)
                            {
                                case TransferManager.TransferReason.Goods:
                                    material = Language.BuildingUI[28] + Language.BuildingUI[39]; break;
                                case TransferManager.TransferReason.Food:
                                    material = Language.BuildingUI[20] + Language.BuildingUI[39]; break;
                                case TransferManager.TransferReason.Petrol:
                                    material = Language.BuildingUI[23]; break;
                                case TransferManager.TransferReason.Lumber:
                                    material = Language.BuildingUI[22]; break;
                                case TransferManager.TransferReason.Logs:
                                    material = Language.BuildingUI[26]; break;
                                case TransferManager.TransferReason.Oil:
                                    material = Language.BuildingUI[27]; break;
                                case TransferManager.TransferReason.Ore:
                                    material = Language.BuildingUI[25]; break;
                                case TransferManager.TransferReason.Grain:
                                    material = Language.BuildingUI[24]; break;
                                default: break;
                            }
                            break;
                        case ItemClass.SubService.IndustrialForestry:
                        case ItemClass.SubService.IndustrialFarming:
                        case ItemClass.SubService.IndustrialOil:
                        case ItemClass.SubService.IndustrialOre:
                            TransferManager.TransferReason tempReason2 = RealCityIndustrialBuildingAI.GetIncomingTransferReason(buildingID);
                            switch (tempReason2)
                            {
                                case TransferManager.TransferReason.Grain:
                                    material = Language.BuildingUI[24]; break;
                                case TransferManager.TransferReason.Logs:
                                    material = Language.BuildingUI[26]; break;
                                case TransferManager.TransferReason.Ore:
                                    material = Language.BuildingUI[25]; break;
                                case TransferManager.TransferReason.Oil:
                                    material = Language.BuildingUI[27]; break;
                                default: break;
                            }
                            break;
                        case ItemClass.SubService.IndustrialGeneric:
                            TransferManager.TransferReason tempReason = RealCityIndustrialBuildingAI.GetIncomingTransferReason(buildingID);
                            TransferManager.TransferReason tempReason1 = RealCityIndustrialBuildingAI.GetSecondaryIncomingTransferReason(buildingID);
                            switch (tempReason)
                            {
                                case TransferManager.TransferReason.Food:
                                    material = Language.BuildingUI[20]; break;
                                case TransferManager.TransferReason.Lumber:
                                    material = Language.BuildingUI[22]; break;
                                case TransferManager.TransferReason.Petrol:
                                    material = Language.BuildingUI[23]; break;
                                case TransferManager.TransferReason.Coal:
                                    material = Language.BuildingUI[21]; break;
                                default: break;
                            }
                            switch (tempReason1)
                            {
                                case TransferManager.TransferReason.AnimalProducts:
                                    material += Language.BuildingUI[31]; break;
                                case TransferManager.TransferReason.Flours:
                                    material += Language.BuildingUI[32]; break;
                                case TransferManager.TransferReason.Paper:
                                    material += Language.BuildingUI[33]; break;
                                case TransferManager.TransferReason.PlanedTimber:
                                    material += Language.BuildingUI[34]; break;
                                case TransferManager.TransferReason.Petroleum:
                                    material += Language.BuildingUI[35]; break;
                                case TransferManager.TransferReason.Plastics:
                                    material += Language.BuildingUI[36]; break;
                                case TransferManager.TransferReason.Glass:
                                    material += Language.BuildingUI[37]; break;
                                case TransferManager.TransferReason.Metals:
                                    material += Language.BuildingUI[38]; break;
                                default: break;
                            }
                            break;
                        default:
                            material = ""; break;
                    }
                }
            }
            else
            {
                if (data.Info.m_buildingAI is IndustrialExtractorAI)
                {
                    switch (data.Info.m_class.m_subService)
                    {
                        case ItemClass.SubService.IndustrialForestry:
                            material = Language.BuildingUI[26]; break;
                        case ItemClass.SubService.IndustrialFarming:
                            material = Language.BuildingUI[24]; break;
                        case ItemClass.SubService.IndustrialOil:
                            material = Language.BuildingUI[27]; break;
                        case ItemClass.SubService.IndustrialOre:
                            material = Language.BuildingUI[25]; break;
                        default:
                            material = ""; break;
                    }
                }
                else
                {
                    switch (data.Info.m_class.m_subService)
                    {
                        case ItemClass.SubService.IndustrialForestry:
                            material = Language.BuildingUI[22]; break;
                        case ItemClass.SubService.IndustrialFarming:
                            material = Language.BuildingUI[20]; break;
                        case ItemClass.SubService.IndustrialOil:
                            material = Language.BuildingUI[23]; break;
                        case ItemClass.SubService.IndustrialOre:
                            material = Language.BuildingUI[21]; break;
                        case ItemClass.SubService.IndustrialGeneric:
                            material = Language.BuildingUI[28]; break;
                        case ItemClass.SubService.CommercialHigh:
                        case ItemClass.SubService.CommercialLow:
                        case ItemClass.SubService.CommercialEco:
                        case ItemClass.SubService.CommercialLeisure:
                        case ItemClass.SubService.CommercialTourist:
                            material = Language.BuildingUI[29]; break;
                        default:
                            material = ""; break;
                    }
                }
            }
            return material;
        }

        public static float GetPrice(bool isSelling, ushort buildingID, Building data)
        {
            float price = 0f;
            if (!isSelling)
            {
                if (data.Info.m_buildingAI is IndustrialExtractorAI)
                {
                }
                else
                {
                    switch (data.Info.m_class.m_subService)
                    {
                        case ItemClass.SubService.CommercialHigh:
                        case ItemClass.SubService.CommercialLow:
                        case ItemClass.SubService.CommercialEco:
                        case ItemClass.SubService.CommercialLeisure:
                        case ItemClass.SubService.CommercialTourist:
                            CommercialBuildingAI commercialBuildingAI = data.Info.m_buildingAI as CommercialBuildingAI;
                            if (commercialBuildingAI.m_incomingResource == TransferManager.TransferReason.Food || commercialBuildingAI.m_incomingResource == TransferManager.TransferReason.Goods)
                            {
                                price = (3f * RealCityIndustryBuildingAI.GetResourcePrice(commercialBuildingAI.m_incomingResource) + (RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.LuxuryProducts)/ (float)MainDataStore.commericalPriceAdjust)) / 4f;
                            }
                            else
                            {
                                price = RealCityIndustryBuildingAI.GetResourcePrice(commercialBuildingAI.m_incomingResource);
                            }
                            break;
                        case ItemClass.SubService.IndustrialForestry:
                        case ItemClass.SubService.IndustrialFarming:
                        case ItemClass.SubService.IndustrialOil:
                        case ItemClass.SubService.IndustrialOre:
                            TransferManager.TransferReason tempReason = RealCityIndustrialBuildingAI.GetIncomingTransferReason(buildingID);
                            price = RealCityIndustryBuildingAI.GetResourcePrice(tempReason);
                            break;
                        case ItemClass.SubService.IndustrialGeneric:
                            TransferManager.TransferReason tempReason1 = RealCityIndustrialBuildingAI.GetIncomingTransferReason(buildingID);
                            TransferManager.TransferReason tempReason2 = RealCityIndustrialBuildingAI.GetSecondaryIncomingTransferReason(buildingID);
                            price = (3f * RealCityIndustryBuildingAI.GetResourcePrice(tempReason1) + (RealCityIndustryBuildingAI.GetResourcePrice(tempReason2) / (float)MainDataStore.industialPriceAdjust)) / 4f;
                            break;
                        default:
                            price = 0; break;
                    }
                }
            }
            else
            {
                if (data.Info.m_buildingAI is IndustrialExtractorAI)
                {
                    switch (data.Info.m_class.m_subService)
                    {
                        case ItemClass.SubService.IndustrialForestry:
                            price = RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Logs); break;
                        case ItemClass.SubService.IndustrialFarming:
                            price = RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Grain); break;
                        case ItemClass.SubService.IndustrialOil:
                            price = RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Oil); break;
                        case ItemClass.SubService.IndustrialOre:
                            price = RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Ore); break;
                        default:
                            price = 0; break;
                    }
                }
                else
                {
                    switch (data.Info.m_class.m_subService)
                    {
                        case ItemClass.SubService.IndustrialForestry:
                            price = RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Lumber); break;
                        case ItemClass.SubService.IndustrialFarming:
                            price = RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Food); break;
                        case ItemClass.SubService.IndustrialOil:
                            price = RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Petrol); break;
                        case ItemClass.SubService.IndustrialOre:
                            price = RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Coal); break;
                        case ItemClass.SubService.IndustrialGeneric:
                            price = RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Goods); break;
                        case ItemClass.SubService.CommercialHigh:
                        case ItemClass.SubService.CommercialLow:
                        case ItemClass.SubService.CommercialEco:
                        case ItemClass.SubService.CommercialLeisure:
                        case ItemClass.SubService.CommercialTourist:
                            price = RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Shopping); break;
                        default:
                            price = RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.None); break;
                    }
                }
            }

            return price;
        }


        /*public void LimitCommericalBuildingAccess(ushort buildingID, ref Building buildingData)
        {
            if (buildingData.Info.m_class.m_service == ItemClass.Service.Commercial)
            {
                Citizen.BehaviourData behaviour = default(Citizen.BehaviourData);
                int alivevisitCount = 0;
                int totalvisitCount = 0;
                int maxcount = 0;
                GetVisitBehaviour(buildingID, ref buildingData, ref behaviour, ref alivevisitCount, ref totalvisitCount);
                GetVisitNum(buildingID, ref buildingData, ref maxcount);
                if ( maxcount * 5 < totalvisitCount + 1)
                {
                    buildingData.m_flags &= ~Building.Flags.Active;
                }
                else
                {

                }
            }
        }

        protected void GetVisitNum(ushort buildingID, ref Building buildingData, ref int maxcount)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint num = buildingData.m_citizenUnits;
            int num2 = 0;
            while (num != 0u)
            {
                if ((ushort)(instance.m_units.m_buffer[(int)((UIntPtr)num)].m_flags & CitizenUnit.Flags.Visit) != 0)
                {
                    maxcount++;
                }
                num = instance.m_units.m_buffer[(int)((UIntPtr)num)].m_nextUnit;
                if (++num2 > 524288)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }
        }*/


        public void ProcessBuildingDataFinal(ushort buildingID, ref Building buildingData)
        {
            if (preBuidlingId < buildingID)
            {
            }
            else
            {
                allOfficeHighTechBuildingCountFinal = allOfficeHighTechBuildingCount;
                allOfficeLevel1BuildingCountFinal = allOfficeLevel1BuildingCount;
                allOfficeLevel2BuildingCountFinal = allOfficeLevel2BuildingCount;
                allOfficeLevel3BuildingCountFinal = allOfficeLevel3BuildingCount;
                greaterThan20000ProfitBuildingCountFinal = greaterThan20000ProfitBuildingCount;
                greaterThan20000ProfitBuildingMoneyFinal = greaterThan20000ProfitBuildingMoney;
                MainDataStore.Extractor_building_final = MainDataStore.Extractor_building;
                allBuildingsFinal = allBuildings;

                greaterThan20000ProfitBuildingMoney = 0;
                greaterThan20000ProfitBuildingCount = 0;
                MainDataStore.Extractor_building = 0;
                allOfficeLevel1BuildingCount = 0;
                allOfficeLevel2BuildingCount = 0;
                allOfficeLevel3BuildingCount = 0;
                allOfficeHighTechBuildingCount = 0;
                allBuildings = 0;
            }
            preBuidlingId = buildingID;


            allBuildings++;
            if (buildingData.Info.m_class.m_service == ItemClass.Service.Residential)
            {
                MainDataStore.building_money[buildingID] = 0;
            }

            if (buildingData.Info.m_class.m_service == ItemClass.Service.Industrial)
            {
                if (MainDataStore.building_money[buildingID] < 0)
                {
                    RealCityEconomyExtension.industrialLackMoneyCount++;
                }
                else
                {
                    RealCityEconomyExtension.industrialEarnMoneyCount++;
                }
            }

            if (buildingData.Info.m_class.m_service == ItemClass.Service.Commercial)
            {
                if (MainDataStore.building_money[buildingID] < 0)
                {
                    RealCityEconomyExtension.commericalLackMoneyCount++;
                }
                else
                {
                    RealCityEconomyExtension.commericalEarnMoneyCount++;
                }
            }

            if (buildingData.m_problems == Notification.Problem.None)
            {
                //mark no good
                if (buildingData.Info.m_class.m_service == ItemClass.Service.Commercial)
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

        public void LimitAndCheckBuildingMoney(Building building, ushort buildingID)
        {
            if (MainDataStore.building_money[buildingID] > 60000000)
            {
                MainDataStore.building_money[buildingID] = 60000000;
            }
            else if (MainDataStore.building_money[buildingID] < -65000000)
            {
                MainDataStore.building_money[buildingID] = 0;
            }
            else if (MainDataStore.building_money[buildingID] < -60000000)
            {
                MainDataStore.building_money[buildingID] = -60000000;
            }


            if (MainDataStore.building_money[buildingID] > 0)
            {
                if (building.Info.m_class.m_service == ItemClass.Service.Industrial || building.Info.m_class.m_service == ItemClass.Service.Commercial)
                {
                    if (building.Info.m_buildingAI is IndustrialExtractorAI)
                    {

                    }
                    else
                    {
                        greaterThan20000ProfitBuildingCount++;

                        float idex = 0;
                        float idex1 = 0;

                        // outside will to invest
                        if (building.Info.m_class.m_subService != ItemClass.SubService.IndustrialGeneric  && building.Info.m_class.m_subService != ItemClass.SubService.CommercialHigh && building.Info.m_class.m_subService != ItemClass.SubService.CommercialLow)
                        {
                            if (building.Info.m_class.m_subService == ItemClass.SubService.CommercialLeisure || building.Info.m_class.m_subService == ItemClass.SubService.CommercialTourist)
                            {
                                idex = 0.03f;
                            } else
                            {
                                idex = 0.005f;
                            }
                        }
                        else if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                        {
                            idex = 0.01f;
                        }
                        else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                        {
                            idex = 0.015f;
                        }
                        else if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                        {
                            idex = 0.02f;
                        }

                        // Boss will to take 
                        if (building.Info.m_class.m_subService != ItemClass.SubService.IndustrialGeneric && building.Info.m_class.m_subService != ItemClass.SubService.CommercialHigh && building.Info.m_class.m_subService != ItemClass.SubService.CommercialLow)
                        {
                            if (building.Info.m_class.m_subService == ItemClass.SubService.CommercialLeisure || building.Info.m_class.m_subService == ItemClass.SubService.CommercialTourist)
                            {
                                idex1 = 0.01f;
                            }
                            else
                            {
                                idex1 = 0.03f;
                            }
                        }
                        else if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                        {
                            idex1 = 0.02f;
                        }
                        else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                        {
                            idex1 = 0.015f;
                        }
                        else if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                        {
                            idex1 = 0.01f;
                        }

                        greaterThan20000ProfitBuildingMoney += (long)(MainDataStore.building_money[buildingID] * idex);
                        MainDataStore.building_money[buildingID] -= (long)(MainDataStore.building_money[buildingID] * idex1);
                    }
                }
            }

            if (building.Info.m_class.m_service == ItemClass.Service.Office)
            {
                if (building.Info.m_class.m_subService == ItemClass.SubService.OfficeGeneric)
                {
                    if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        if (allOfficeLevel1BuildingCountFinal > 0)
                        {
                            MainDataStore.building_money[buildingID] += greaterThan20000ProfitBuildingMoneyFinal * 0.1f / allOfficeLevel1BuildingCountFinal;
                        }
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        if (allOfficeLevel2BuildingCountFinal > 0)
                        {
                            MainDataStore.building_money[buildingID] += greaterThan20000ProfitBuildingMoneyFinal * 0.2f / allOfficeLevel2BuildingCountFinal;
                        }
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        if (allOfficeLevel3BuildingCountFinal > 0)
                        {
                            MainDataStore.building_money[buildingID] += greaterThan20000ProfitBuildingMoneyFinal * 0.3f / allOfficeLevel3BuildingCountFinal;
                        }
                    }
                }
                else if (building.Info.m_class.m_subService == ItemClass.SubService.OfficeHightech)
                {
                    if (allOfficeHighTechBuildingCountFinal > 0)
                    {
                        MainDataStore.building_money[buildingID] += greaterThan20000ProfitBuildingMoneyFinal * 0.4f / allOfficeHighTechBuildingCountFinal;
                    }
                }
            }
        }

        public void ProcessLandFee(Building building, ushort buildingID)
        {
            DistrictManager instance = Singleton<DistrictManager>.instance;
            byte district = instance.GetDistrict(building.m_position);
            DistrictPolicies.Services servicePolicies = instance.m_districts.m_buffer[(int)district].m_servicePolicies;
            DistrictPolicies.Taxation taxationPolicies = instance.m_districts.m_buffer[(int)district].m_taxationPolicies;
            DistrictPolicies.CityPlanning cityPlanningPolicies = instance.m_districts.m_buffer[(int)district].m_cityPlanningPolicies;

            int num = 0;
            GetLandRent(out num);
            int num2;
            num2 = Singleton<EconomyManager>.instance.GetTaxRate(this.m_info.m_class, taxationPolicies);
            if (MainDataStore.building_money[buildingID] <= 0)
            {
                num2 = 0;
            }
            if (((taxationPolicies & DistrictPolicies.Taxation.DontTaxLeisure) != DistrictPolicies.Taxation.None) && (building.Info.m_class.m_subService == ItemClass.SubService.CommercialLeisure))
            {
                num = 0;
            }
            num = (int)(num * ((float)(instance.m_districts.m_buffer[(int)district].GetLandValue() + 50) / 100));
            //num = num / comm_data.mantain_and_land_fee_decrease;

            //do this to decrase land expense in early game;
            //float idex = (comm_data.mantain_and_land_fee_decrease > 1) ? (comm_data.mantain_and_land_fee_decrease / 2) : 1f;
            if ((building.Info.m_class.m_service == ItemClass.Service.Commercial) || (building.Info.m_class.m_service == ItemClass.Service.Industrial) || (building.Info.m_class.m_service == ItemClass.Service.Office))
            {
                MainDataStore.building_money[buildingID] = (MainDataStore.building_money[buildingID] - (float)(num * num2) / 100);
            }
            if (instance.IsPolicyLoaded(DistrictPolicies.Policies.ExtraInsulation))
            {
                if ((servicePolicies & DistrictPolicies.Services.ExtraInsulation) != DistrictPolicies.Services.None)
                {
                    num = num * 95 / 100;
                }
            }
            if ((servicePolicies & DistrictPolicies.Services.Recycling) != DistrictPolicies.Services.None)
            {
                num = num * 95 / 100;
            }

            Singleton<EconomyManager>.instance.AddPrivateIncome(num, building.Info.m_class.m_service, building.Info.m_class.m_subService, building.Info.m_class.m_level, num2 * 100);
        }

        public void GetLandRent(out int incomeAccumulation)
        {
            ItemClass @class = this.m_info.m_class;
            incomeAccumulation = 0;
            ItemClass.SubService subService = @class.m_subService;
            switch (subService)
            {
                case ItemClass.SubService.OfficeHightech:
                    incomeAccumulation = (int)(MainDataStore.office_high_tech);
                    allOfficeHighTechBuildingCount++;
                    break;
                case ItemClass.SubService.OfficeGeneric:
                    if (this.m_info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = (int)(MainDataStore.office_gen_levell);
                        allOfficeLevel1BuildingCount++;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = (int)(MainDataStore.office_gen_level2);
                        allOfficeLevel2BuildingCount++;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = (int)(MainDataStore.office_gen_level3);
                        allOfficeLevel3BuildingCount++;
                    }
                    break;
                case ItemClass.SubService.IndustrialFarming:
                    incomeAccumulation = MainDataStore.indu_farm;
                    break;
                case ItemClass.SubService.IndustrialForestry:
                    incomeAccumulation = MainDataStore.indu_forest;
                    break;
                case ItemClass.SubService.IndustrialOil:
                    incomeAccumulation = MainDataStore.indu_oil;
                    break;
                case ItemClass.SubService.IndustrialOre:
                    incomeAccumulation = MainDataStore.indu_ore;
                    break;
                case ItemClass.SubService.IndustrialGeneric:
                    if (this.m_info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = MainDataStore.indu_gen_level1;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = MainDataStore.indu_gen_level2;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = MainDataStore.indu_gen_level3;
                    }
                    break;
                case ItemClass.SubService.CommercialHigh:
                    if (this.m_info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = MainDataStore.comm_high_level1;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = MainDataStore.comm_high_level2;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = MainDataStore.comm_high_level3;
                    }
                    break;
                case ItemClass.SubService.CommercialLow:
                    if (this.m_info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = MainDataStore.comm_low_level1;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = MainDataStore.comm_low_level2;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = MainDataStore.comm_low_level3;
                    }
                    break;
                case ItemClass.SubService.CommercialLeisure:
                    incomeAccumulation = MainDataStore.comm_leisure;
                    break;
                case ItemClass.SubService.CommercialTourist:
                    incomeAccumulation = MainDataStore.comm_tourist;
                    break;
                case ItemClass.SubService.CommercialEco:
                    incomeAccumulation = MainDataStore.comm_eco;
                    break;
                default: break;
            }
        }

        public static int GetTaxRate(Building data, ushort buildingID)
        {
            if (data.Info.m_class.m_service == ItemClass.Service.Commercial)
            {
                return Politics.commericalTax;
            }
            else if (data.Info.m_class.m_service == ItemClass.Service.Industrial)
            {
                if (data.Info.m_buildingAI is IndustrialExtractorAI)
                {
                    return Politics.industryTax + 60 ;
                }
                else
                {
                    return Politics.industryTax;
                }
            }

            return 0;
        }

        public static float GetComsumptionDivider(Building data, ushort buildingID)
        {
            if (data.Info.m_class.m_service == ItemClass.Service.Office)
            {
                return 0f;
            }


            Citizen.BehaviourData behaviourData = default(Citizen.BehaviourData);
            int aliveWorkerCount = 0;
            int totalWorkerCount = 0;
            BuildingUI.GetWorkBehaviour(buildingID, ref data, ref behaviourData, ref aliveWorkerCount, ref totalWorkerCount);

            float finalIdex = aliveWorkerCount / 10f;

            if (finalIdex < 1f)
            {
                finalIdex = 1f;
            }

            if (data.Info.m_class.m_subService == ItemClass.SubService.IndustrialGeneric)
            {
                Randomizer randomizer = new Randomizer((int)buildingID);
                int temp = randomizer.Int32(4u);
                //petrol related
                if (temp == 2)
                {
                    finalIdex = finalIdex * 1.5f / 4f;
                } else if (temp == 3)
                {
                    finalIdex = finalIdex * 1.25f / 4f;
                }
                else
                {
                    finalIdex = finalIdex / 4f;
                }
            }

            
            return finalIdex;
        }

        protected void CustomCalculateGuestVehicles(ushort buildingID, ref Building data, TransferManager.TransferReason material1, TransferManager.TransferReason material2, ref int count, ref int cargo, ref int capacity, ref int outside)
        {
            VehicleManager instance = Singleton<VehicleManager>.instance;
            ushort num = data.m_guestVehicles;
            int num2 = 0;
            while (num != 0)
            {
                TransferManager.TransferReason transferType = (TransferManager.TransferReason)instance.m_vehicles.m_buffer[(int)num].m_transferType;
                if (transferType == material1 || transferType == material2)
                {
                    VehicleInfo info = instance.m_vehicles.m_buffer[(int)num].Info;
                    int a;
                    int num3;
                    info.m_vehicleAI.GetSize(num, ref instance.m_vehicles.m_buffer[(int)num], out a, out num3);
                    cargo += Mathf.Min(a, num3);
                    if (transferType == material2 && isHighPriceProduction(buildingID, ref data, material2))
                    {
                        if (material2 == TransferManager.TransferReason.LuxuryProducts)
                        {
                            capacity += (int)(num3 * MainDataStore.commericalPriceAdjust);
                        }
                        else
                        {
                            capacity += (int)(num3 * MainDataStore.industialPriceAdjust);
                        }
                    }
                    else
                    {
                        capacity += num3;
                    }
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

        public bool isHighPriceProduction (ushort buildingID, ref Building data, TransferManager.TransferReason material2)
        {
            if (data.Info.m_class.m_service == ItemClass.Service.Industrial || data.Info.m_class.m_service == ItemClass.Service.Commercial)
            {
                switch (material2)
                {
                    case TransferManager.TransferReason.AnimalProducts:
                    case TransferManager.TransferReason.Flours:
                    case TransferManager.TransferReason.Paper:
                    case TransferManager.TransferReason.PlanedTimber:
                    case TransferManager.TransferReason.Petroleum:
                    case TransferManager.TransferReason.Plastics:
                    case TransferManager.TransferReason.Glass:
                    case TransferManager.TransferReason.Metals:
                    case TransferManager.TransferReason.LuxuryProducts:
                        return true;
                }
            }
            return false;
        }
    }
}
