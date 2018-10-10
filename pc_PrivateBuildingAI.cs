using ColossalFramework;
using ColossalFramework.Math;
using System;
using UnityEngine;
using System.Text.RegularExpressions;

namespace RealCity
{

    public class pc_PrivateBuildingAI : CommonBuildingAI
    {
        //2.1 building income

        public const float goodPrice = 0.8f;
        public const float petrolPrice = 3.84f;
        public const float coalPrice = 2.88f;
        public const float lumberPrice = 1.44f;
        public const float foodPrice = 1.44f;
        public const float oilPrice = 3.2f;
        public const float orePrice = 2.4f;
        public const float logPrice = 1.2f;
        public const float grainPrice = 1.2f;

        public static float preGoodPrice = (foodPrice + lumberPrice + coalPrice + petrolPrice) / 4f;

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
            preBuidlingId = saveandrestore.load_uint(ref i, saveData);

            allBuildings = saveandrestore.load_ushort(ref i, saveData);
            allBuildingsFinal = saveandrestore.load_ushort(ref i, saveData);
            allOfficeLevel1BuildingCount = saveandrestore.load_ushort(ref i, saveData);
            allOfficeLevel2BuildingCount = saveandrestore.load_ushort(ref i, saveData);
            allOfficeLevel3BuildingCount = saveandrestore.load_ushort(ref i, saveData);
            allOfficeHighTechBuildingCount = saveandrestore.load_ushort(ref i, saveData);

            allOfficeLevel1BuildingCountFinal = saveandrestore.load_ushort(ref i, saveData);
            allOfficeLevel2BuildingCountFinal = saveandrestore.load_ushort(ref i, saveData);
            allOfficeLevel3BuildingCountFinal = saveandrestore.load_ushort(ref i, saveData);
            allOfficeHighTechBuildingCountFinal = saveandrestore.load_ushort(ref i, saveData);

            greaterThan20000ProfitBuildingMoney = saveandrestore.load_long(ref i, saveData);
            greaterThan20000ProfitBuildingMoneyFinal = saveandrestore.load_long(ref i, saveData);
            greaterThan20000ProfitBuildingCount = saveandrestore.load_ushort(ref i, saveData);
            greaterThan20000ProfitBuildingCountFinal = saveandrestore.load_ushort(ref i, saveData);

            //office_gen_salary_index = saveandrestore.load_float(ref i, saveData);
            //office_high_tech_salary_index = saveandrestore.load_float(ref i, saveData);

            DebugLog.LogToFileOnly("saveData in private building is " + i.ToString());


        }


        public static void save()
        {
            int i = 0;

            //12*2 + 7*4 = 52
            saveandrestore.save_uint(ref i, preBuidlingId, ref saveData);

            //20 + 20
            saveandrestore.save_ushort(ref i, allBuildings, ref saveData);
            saveandrestore.save_ushort(ref i, allBuildingsFinal, ref saveData);
            saveandrestore.save_ushort(ref i, allOfficeLevel1BuildingCount, ref saveData);
            saveandrestore.save_ushort(ref i, allOfficeLevel2BuildingCount, ref saveData);
            saveandrestore.save_ushort(ref i, allOfficeLevel3BuildingCount, ref saveData);
            saveandrestore.save_ushort(ref i, allOfficeHighTechBuildingCount, ref saveData);
            saveandrestore.save_ushort(ref i, allOfficeLevel1BuildingCountFinal, ref saveData);
            saveandrestore.save_ushort(ref i, allOfficeLevel2BuildingCountFinal, ref saveData);
            saveandrestore.save_ushort(ref i, allOfficeLevel3BuildingCountFinal, ref saveData);
            saveandrestore.save_ushort(ref i, allOfficeHighTechBuildingCountFinal, ref saveData);

            saveandrestore.save_long(ref i, greaterThan20000ProfitBuildingMoney, ref saveData);
            saveandrestore.save_long(ref i, greaterThan20000ProfitBuildingMoneyFinal, ref saveData);
            saveandrestore.save_ushort(ref i, greaterThan20000ProfitBuildingCount, ref saveData);
            saveandrestore.save_ushort(ref i, greaterThan20000ProfitBuildingCountFinal, ref saveData);

        }
        protected void SimulationStepActive_1(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
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
            LimitCommericalBuildingAccess(buildingID, ref buildingData);
            ProcessAdditionProduct(buildingID, ref buildingData);

        }

        public void ProcessAdditionProduct(ushort buildingID, ref Building buildingData)
        {
            if (buildingData.Info.m_class.m_service == ItemClass.Service.Commercial || buildingData.Info.m_class.m_service == ItemClass.Service.Industrial)
            {
                float temp = GetComsumptionDivider(buildingData, buildingID);
                int deltaCustomBuffer1 = comm_data.building_buffer1[buildingID] - buildingData.m_customBuffer1;
                if (deltaCustomBuffer1 > 0)
                {
                    buildingData.m_customBuffer1 = (ushort)(buildingData.m_customBuffer1 + deltaCustomBuffer1 - (int)(deltaCustomBuffer1 / temp));
                }
                comm_data.building_buffer1[buildingID] = (ushort)buildingData.m_customBuffer1;
            }
        }

        public static float GetPrice(bool isSelling, ushort buildingID, Building data, TransferManager.TransferReason forceMaterial)
        {
            TransferManager.TransferReason material = default(TransferManager.TransferReason);
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
                            material = TransferManager.TransferReason.Goods; break;
                        case ItemClass.SubService.IndustrialForestry:
                            material = TransferManager.TransferReason.Logs; break;
                        case ItemClass.SubService.IndustrialFarming:
                            material = TransferManager.TransferReason.Grain; break;
                        case ItemClass.SubService.IndustrialOil:
                            material = TransferManager.TransferReason.Oil; break;
                        case ItemClass.SubService.IndustrialOre:
                            material = TransferManager.TransferReason.Ore; break;
                        case ItemClass.SubService.IndustrialGeneric:
                            {
                                System.Random rand = new System.Random();
                                if (forceMaterial == TransferManager.TransferReason.None)
                                {
                                    switch (rand.Next(4))
                                    {
                                        case 0:
                                            material = TransferManager.TransferReason.Lumber; break;
                                        case 1:
                                            material = TransferManager.TransferReason.Food; break;
                                        case 2:
                                            material = TransferManager.TransferReason.Petrol; break;
                                        case 3:
                                            material = TransferManager.TransferReason.Coal; break;
                                        default:
                                            material = TransferManager.TransferReason.None; break;
                                    }
                                }
                                else
                                {
                                    material = forceMaterial;
                                }
                            }
                            break;
                        default:
                            material = TransferManager.TransferReason.None; break;
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
                            material = TransferManager.TransferReason.Logs; break;
                        case ItemClass.SubService.IndustrialFarming:
                            material = TransferManager.TransferReason.Grain; break;
                        case ItemClass.SubService.IndustrialOil:
                            material = TransferManager.TransferReason.Oil; break;
                        case ItemClass.SubService.IndustrialOre:
                            material = TransferManager.TransferReason.Ore; break;
                        default:
                            material = TransferManager.TransferReason.None; break;
                    }
                }
                else
                {
                    switch (data.Info.m_class.m_subService)
                    {
                        case ItemClass.SubService.IndustrialForestry:
                            material = TransferManager.TransferReason.Lumber; break;
                        case ItemClass.SubService.IndustrialFarming:
                            material = TransferManager.TransferReason.Food; break;
                        case ItemClass.SubService.IndustrialOil:
                            material = TransferManager.TransferReason.Petrol; break;
                        case ItemClass.SubService.IndustrialOre:
                            material = TransferManager.TransferReason.Coal; break;
                        case ItemClass.SubService.IndustrialGeneric:
                            material = TransferManager.TransferReason.Goods; break;
                        default:
                            material = TransferManager.TransferReason.None; break;
                    }
                }
            }




            float price = 0f;
            if (isSelling)
            {
                switch (material)
                {
                    case TransferManager.TransferReason.Goods:
                        price = goodPrice;
                        break;
                    case TransferManager.TransferReason.Lumber:
                        price = lumberPrice; break;
                    case TransferManager.TransferReason.Petrol:
                        price = petrolPrice; break;
                    case TransferManager.TransferReason.Food:
                        price = foodPrice; break;
                    case TransferManager.TransferReason.Coal:
                        price = coalPrice; break;
                    case TransferManager.TransferReason.Grain:
                        price = grainPrice; break;
                    case TransferManager.TransferReason.Oil:
                        price = oilPrice; break;
                    case TransferManager.TransferReason.Logs:
                        price = logPrice; break;
                    case TransferManager.TransferReason.Ore:
                        price = orePrice; break;
                }
            }
            else
            {
                switch (material)
                {
                    case TransferManager.TransferReason.Goods:
                        if (data.Info.m_class.m_service == ItemClass.Service.Commercial)
                        {
                            price = goodPrice;
                        }
                        break;
                    case TransferManager.TransferReason.Logs:
                        price = logPrice; break;
                    case TransferManager.TransferReason.Grain:
                        price = grainPrice; break;
                    case TransferManager.TransferReason.Oil:
                        price = oilPrice; break;
                    case TransferManager.TransferReason.Ore:
                        price = orePrice; break;
                    case TransferManager.TransferReason.Lumber:
                        price = lumberPrice;
                        break;
                    case TransferManager.TransferReason.Coal:
                        price = coalPrice;
                        break;
                    case TransferManager.TransferReason.Food:
                        price = foodPrice;
                        break;
                    case TransferManager.TransferReason.Petrol:
                        price = petrolPrice;
                        break;
                }
            }

            return price;
        }


        public void LimitCommericalBuildingAccess(ushort buildingID, ref Building buildingData)
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
        }


        public void ProcessBuildingDataFinal(ushort buildingID, ref Building buildingData)
        {
            if (preBuidlingId < buildingID)
            {
                allBuildings++;
                if (buildingData.Info.m_class.m_service == ItemClass.Service.Residential)
                {
                    comm_data.building_money[buildingID] = 0;
                }

                if (((buildingData.m_problems & (~Notification.Problem.NoGoods)) == Notification.Problem.None) || ((buildingData.m_problems | (Notification.Problem.NoGoods)) != Notification.Problem.None))
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

                        }
                        buildingData.m_problems = problem;
                    }
                }
            }
            else
            {
                allOfficeHighTechBuildingCountFinal = allOfficeHighTechBuildingCount;
                allOfficeLevel1BuildingCountFinal = allOfficeLevel1BuildingCount;
                allOfficeLevel2BuildingCountFinal = allOfficeLevel2BuildingCount;
                allOfficeLevel3BuildingCountFinal = allOfficeLevel3BuildingCount;
                greaterThan20000ProfitBuildingCountFinal = greaterThan20000ProfitBuildingCount;
                greaterThan20000ProfitBuildingMoneyFinal = greaterThan20000ProfitBuildingMoney;
                comm_data.Extractor_building_final = comm_data.Extractor_building;
                allBuildingsFinal = allBuildings;

                greaterThan20000ProfitBuildingMoney = 0;
                greaterThan20000ProfitBuildingCount = 0;
                comm_data.Extractor_building = 0;
                allOfficeLevel1BuildingCount = 0;
                allOfficeLevel2BuildingCount = 0;
                allOfficeLevel3BuildingCount = 0;
                allOfficeHighTechBuildingCount = 0;
                allBuildings = 0;
            }
            preBuidlingId = buildingID;

        }

        public void LimitAndCheckBuildingMoney(Building building, ushort buildingID)
        {
            if (comm_data.building_money[buildingID] > 60000000)
            {
                comm_data.building_money[buildingID] = 60000000;
            }
            else if (comm_data.building_money[buildingID] < -65000000)
            {
                comm_data.building_money[buildingID] = 0;
            }
            else if (comm_data.building_money[buildingID] < -60000000)
            {
                comm_data.building_money[buildingID] = -60000000;
            }


            if (comm_data.building_money[buildingID] > 0)
            {
                if (building.Info.m_class.m_service == ItemClass.Service.Industrial)
                {
                    if (building.Info.m_buildingAI is IndustrialExtractorAI)
                    {

                    }
                    else
                    {
                        greaterThan20000ProfitBuildingCount++;

                        float idex = 0;

                        if (building.Info.m_class.m_subService != ItemClass.SubService.IndustrialGeneric)
                        {
                            idex = 0.1f;
                        }
                        else if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                        {
                            idex = 0.05f;
                        }
                        else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                        {
                            idex = 0.15f;
                        }
                        else if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                        {
                            idex = 0.3f;
                        }

                        greaterThan20000ProfitBuildingMoney += (long)(comm_data.building_money[buildingID] * idex);
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
                            comm_data.building_money[buildingID] += greaterThan20000ProfitBuildingMoneyFinal * 0.05f / allOfficeLevel1BuildingCountFinal;
                        }
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        if (allOfficeLevel2BuildingCountFinal > 0)
                        {
                            comm_data.building_money[buildingID] += greaterThan20000ProfitBuildingMoneyFinal * 0.15f / allOfficeLevel2BuildingCountFinal;
                        }
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        if (allOfficeLevel3BuildingCountFinal > 0)
                        {
                            comm_data.building_money[buildingID] += greaterThan20000ProfitBuildingMoneyFinal * 0.25f / allOfficeLevel3BuildingCountFinal;
                        }
                    }
                }
                else if (building.Info.m_class.m_subService == ItemClass.SubService.OfficeHightech)
                {
                    if (allOfficeHighTechBuildingCountFinal > 0)
                    {
                        comm_data.building_money[buildingID] += greaterThan20000ProfitBuildingMoneyFinal * 0.35f / allOfficeHighTechBuildingCountFinal;
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
            if (comm_data.building_money[buildingID] < 0)
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
                comm_data.building_money[buildingID] = (comm_data.building_money[buildingID] - (float)(num * num2) / 100);
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
                    incomeAccumulation = (int)(comm_data.office_high_tech);
                    allOfficeHighTechBuildingCount++;
                    break;
                case ItemClass.SubService.OfficeGeneric:
                    if (this.m_info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = (int)(comm_data.office_gen_levell);
                        allOfficeLevel1BuildingCount++;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = (int)(comm_data.office_gen_level2);
                        allOfficeLevel2BuildingCount++;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = (int)(comm_data.office_gen_level3);
                        allOfficeLevel3BuildingCount++;
                    }
                    break;
                case ItemClass.SubService.IndustrialFarming:
                    incomeAccumulation = comm_data.indu_farm;
                    break;
                case ItemClass.SubService.IndustrialForestry:
                    incomeAccumulation = comm_data.indu_forest;
                    break;
                case ItemClass.SubService.IndustrialOil:
                    incomeAccumulation = comm_data.indu_oil;
                    break;
                case ItemClass.SubService.IndustrialOre:
                    incomeAccumulation = comm_data.indu_ore;
                    break;
                case ItemClass.SubService.IndustrialGeneric:
                    if (this.m_info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = comm_data.indu_gen_level1;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = comm_data.indu_gen_level2;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = comm_data.indu_gen_level3;
                    }
                    break;
                case ItemClass.SubService.CommercialHigh:
                    if (this.m_info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = comm_data.comm_high_level1;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = comm_data.comm_high_level2;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = comm_data.comm_high_level3;
                    }
                    break;
                case ItemClass.SubService.CommercialLow:
                    if (this.m_info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = comm_data.comm_low_level1;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = comm_data.comm_low_level2;
                    }
                    else if (this.m_info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = comm_data.comm_low_level3;
                    }
                    break;
                case ItemClass.SubService.CommercialLeisure:
                    incomeAccumulation = comm_data.comm_leisure;
                    break;
                case ItemClass.SubService.CommercialTourist:
                    incomeAccumulation = comm_data.comm_tourist;
                    break;
                case ItemClass.SubService.CommercialEco:
                    incomeAccumulation = comm_data.comm_eco;
                    break;
                default: break;
            }
        }

        protected void CalculateGuestVehicles_1(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int count, ref int cargo, ref int capacity, ref int outside)
        {
            VehicleManager instance = Singleton<VehicleManager>.instance;
            ushort num = data.m_guestVehicles;
            int num2 = 0;            
            while (num != 0)
            {
                if (((TransferManager.TransferReason)instance.m_vehicles.m_buffer[(int)num].m_transferType == material) || IsGeneralIndustry(buildingID, data, material) || IsCommercialBuilding(buildingID, data, material))
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

        public static bool IsGeneralIndustry (ushort buildingID, Building data, TransferManager.TransferReason material)
        {
            if (data.Info.m_class.m_subService == ItemClass.SubService.IndustrialGeneric)
            {
                if ((material == TransferManager.TransferReason.Lumber) || (material == TransferManager.TransferReason.Food) || (material == TransferManager.TransferReason.Coal) || (material == TransferManager.TransferReason.Petrol))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsCommercialBuilding(ushort buildingID, Building data, TransferManager.TransferReason material)
        {
            if (data.Info.m_class.m_service == ItemClass.Service.Commercial)
            {
                if ((material == TransferManager.TransferReason.Lumber) || (material == TransferManager.TransferReason.Food) || (material == TransferManager.TransferReason.Coal) || (material == TransferManager.TransferReason.Petrol))
                {
                    return true;
                }
            }
            return false;
        }

        public static float GetTaxRate(Building data, ushort buildingID)
        {
            float tax = 0f;
            switch (data.Info.m_class.m_subService)
            {
                case ItemClass.SubService.CommercialLow:
                    switch (data.Info.m_class.m_level)
                    {
                        case ItemClass.Level.Level1:
                            tax = 0.05f; break;
                        case ItemClass.Level.Level2:
                            tax = 0.07f; break;
                        case ItemClass.Level.Level3:
                            tax = 0.08f; break;
                        default:
                            tax = 0; break;
                    }
                    break;
                case ItemClass.SubService.CommercialHigh:
                    switch (data.Info.m_class.m_level)
                    {
                        case ItemClass.Level.Level1:
                            tax = 0.06f; break;
                        case ItemClass.Level.Level2:
                            tax = 0.08f; break;
                        case ItemClass.Level.Level3:
                            tax = 0.10f; break;
                        default:
                            tax = 0; break;
                    }
                    break;
                case ItemClass.SubService.IndustrialGeneric:
                    switch (data.Info.m_class.m_level)
                    {
                        case ItemClass.Level.Level1:
                            tax = 0.05f; break;
                        case ItemClass.Level.Level2:
                            tax = 0.07f; break;
                        case ItemClass.Level.Level3:
                            tax = 0.08f; break;
                        default:
                            tax = 0; break;
                    }
                    break;
                case ItemClass.SubService.IndustrialFarming:
                    if (data.Info.m_buildingAI is IndustrialExtractorAI)
                    {
                        tax = 0.7f;
                    }
                    else
                    {
                        tax = 0.1f;
                    }
                    break;
                case ItemClass.SubService.IndustrialForestry:
                    if (data.Info.m_buildingAI is IndustrialExtractorAI)
                    {
                        tax = 0.7f;
                    }
                    else
                    {
                        tax = 0.1f;
                    }
                    break;
                case ItemClass.SubService.IndustrialOil:
                    if (data.Info.m_buildingAI is IndustrialExtractorAI)
                    {
                        tax = 0.95f;
                    }
                    else
                    {
                        tax = 0.1f;
                    }
                    break;
                case ItemClass.SubService.IndustrialOre:
                    if (data.Info.m_buildingAI is IndustrialExtractorAI)
                    {
                        tax = 0.9f;
                    }
                    else
                    {
                        tax = 0.1f;
                    }
                    break;
                case ItemClass.SubService.CommercialEco:
                    tax = 0.1f; break;
                case ItemClass.SubService.CommercialTourist:
                    tax = 0.5f; break;
                case ItemClass.SubService.CommercialLeisure:
                    tax = 0.15f; break;
                default: tax = 0f; break;
            }

            return tax;
        }

        public static float GetComsumptionDivider(Building data, ushort buildingID)
        {
            Citizen.BehaviourData behaviourData = default(Citizen.BehaviourData);
            int aliveWorkerCount = 0;
            int totalWorkerCount = 0;
            BuildingUI.GetWorkBehaviour(buildingID, ref data, ref behaviourData, ref aliveWorkerCount, ref totalWorkerCount);

            float finalIdex = aliveWorkerCount / 10f;

            if (finalIdex < 1f)
            {
                finalIdex = 1f;
            }
            return finalIdex;
        }
    }
}
