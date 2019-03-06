using System;
using ColossalFramework;
using UnityEngine;
using ColossalFramework.Math;
using System.Reflection;
using System.Text.RegularExpressions;
using RealCity.Util;
using RealCity.UI;

namespace RealCity.CustomAI
{
    public class RealCityResidentAI : ResidentAI
    {
        public static uint preCitizenId = 0;
        public static int familyCount = 0;
        public static int citizenCount = 0;
        public static uint familyVeryProfitMoneyCount = 0;
        public static uint familyProfitMoneyCount = 0;
        public static uint familyLossMoneyCount = 0;
        public static int citizenSalaryCount = 0;
        public static int citizenExpenseCount = 0;
        public static int citizenSalaryTaxTotal = 0;
        public static float tempCitizenSalaryTaxTotal = 0f;
        //public static bool citizen_process_done = false;
        //govement salary outconme
        public static int Road = 0;
        public static int Electricity = 0;
        public static int Water = 0;
        public static int Beautification = 0;
        public static int Garbage = 0;
        public static int HealthCare = 0;
        public static int PoliceDepartment = 0;
        public static int Education = 0;
        public static int Monument = 0;
        public static int FireDepartment = 0;
        public static int PublicTransport_bus = 0;
        public static int PublicTransport_tram = 0;
        public static int PublicTransport_ship = 0;
        public static int PublicTransport_plane = 0;
        public static int PublicTransport_metro = 0;
        public static int PublicTransport_train = 0;
        public static int PublicTransport_taxi = 0;
        public static int PublicTransport_cablecar = 0;
        public static int PublicTransport_monorail = 0;
        public static int Disaster = 0;

        public static uint familyWeightStableHigh = 0;
        public static uint familyWeightStableLow = 0;

        public static long citizenGoodsTemp = 0;
        public static long citizenGoods = 0;

        public static byte[] saveData = new byte[144];
        //public static byte[] saveData = new byte[140];

        public static void Load()
        {
            int i = 0;
            preCitizenId = SaveAndRestore.load_uint(ref i, saveData);
            familyCount = SaveAndRestore.load_int(ref i, saveData);
            familyVeryProfitMoneyCount = SaveAndRestore.load_uint(ref i, saveData);
            familyProfitMoneyCount = SaveAndRestore.load_uint(ref i, saveData);
            familyLossMoneyCount = SaveAndRestore.load_uint(ref i, saveData);
            citizenSalaryCount = SaveAndRestore.load_int(ref i, saveData);
            citizenExpenseCount = SaveAndRestore.load_int(ref i, saveData);
            citizenSalaryTaxTotal = SaveAndRestore.load_int(ref i, saveData);
            tempCitizenSalaryTaxTotal = SaveAndRestore.load_float(ref i, saveData);

            Road = SaveAndRestore.load_int(ref i, saveData);
            Electricity = SaveAndRestore.load_int(ref i, saveData);
            Water = SaveAndRestore.load_int(ref i, saveData);
            Beautification = SaveAndRestore.load_int(ref i, saveData);
            Garbage = SaveAndRestore.load_int(ref i, saveData);
            HealthCare = SaveAndRestore.load_int(ref i, saveData);
            PoliceDepartment = SaveAndRestore.load_int(ref i, saveData);
            Education = SaveAndRestore.load_int(ref i, saveData);
            Monument = SaveAndRestore.load_int(ref i, saveData);
            FireDepartment = SaveAndRestore.load_int(ref i, saveData);
            PublicTransport_bus = SaveAndRestore.load_int(ref i, saveData);
            PublicTransport_tram = SaveAndRestore.load_int(ref i, saveData);
            PublicTransport_ship = SaveAndRestore.load_int(ref i, saveData);
            PublicTransport_plane = SaveAndRestore.load_int(ref i, saveData);
            PublicTransport_metro = SaveAndRestore.load_int(ref i, saveData);
            PublicTransport_train = SaveAndRestore.load_int(ref i, saveData);
            PublicTransport_taxi = SaveAndRestore.load_int(ref i, saveData);
            PublicTransport_cablecar = SaveAndRestore.load_int(ref i, saveData);
            PublicTransport_monorail = SaveAndRestore.load_int(ref i, saveData);
            Disaster = SaveAndRestore.load_int(ref i, saveData);

            familyWeightStableHigh = SaveAndRestore.load_uint(ref i, saveData);
            familyWeightStableLow = SaveAndRestore.load_uint(ref i, saveData);

            citizenGoods = SaveAndRestore.load_long(ref i, saveData);
            citizenGoodsTemp = SaveAndRestore.load_long(ref i, saveData);
            citizenCount = SaveAndRestore.load_int(ref i, saveData);

            DebugLog.LogToFileOnly("saveData in residentAI is " + i.ToString());
        }

        public static void Save()
        {
            int i = 0;

            //2*4 + 3*4 + 4*4 = 36
            SaveAndRestore.save_uint(ref i, preCitizenId, ref saveData);
            SaveAndRestore.save_int(ref i, familyCount, ref saveData);
            SaveAndRestore.save_uint(ref i, familyVeryProfitMoneyCount, ref saveData);
            SaveAndRestore.save_uint(ref i, familyProfitMoneyCount, ref saveData);
            SaveAndRestore.save_uint(ref i, familyLossMoneyCount, ref saveData);
            SaveAndRestore.save_int(ref i, citizenSalaryCount, ref saveData);
            SaveAndRestore.save_int(ref i, citizenExpenseCount, ref saveData);
            SaveAndRestore.save_int(ref i, citizenSalaryTaxTotal, ref saveData);
            SaveAndRestore.save_float(ref i, tempCitizenSalaryTaxTotal, ref saveData);

            //20 * 4 = 80
            SaveAndRestore.save_int(ref i, Road, ref saveData);
            SaveAndRestore.save_int(ref i, Electricity, ref saveData);
            SaveAndRestore.save_int(ref i, Water, ref saveData);
            SaveAndRestore.save_int(ref i, Beautification, ref saveData);
            SaveAndRestore.save_int(ref i, Garbage, ref saveData);
            SaveAndRestore.save_int(ref i, HealthCare, ref saveData);
            SaveAndRestore.save_int(ref i, PoliceDepartment, ref saveData);
            SaveAndRestore.save_int(ref i, Education, ref saveData);
            SaveAndRestore.save_int(ref i, Monument, ref saveData);
            SaveAndRestore.save_int(ref i, FireDepartment, ref saveData);
            SaveAndRestore.save_int(ref i, PublicTransport_bus, ref saveData);
            SaveAndRestore.save_int(ref i, PublicTransport_tram, ref saveData);
            SaveAndRestore.save_int(ref i, PublicTransport_ship, ref saveData);
            SaveAndRestore.save_int(ref i, PublicTransport_plane, ref saveData);
            SaveAndRestore.save_int(ref i, PublicTransport_metro, ref saveData);
            SaveAndRestore.save_int(ref i, PublicTransport_train, ref saveData);
            SaveAndRestore.save_int(ref i, PublicTransport_taxi, ref saveData);
            SaveAndRestore.save_int(ref i, PublicTransport_cablecar, ref saveData);
            SaveAndRestore.save_int(ref i, PublicTransport_monorail, ref saveData);
            SaveAndRestore.save_int(ref i, Disaster, ref saveData);

            //8
            SaveAndRestore.save_uint(ref i, familyWeightStableHigh, ref saveData);
            SaveAndRestore.save_uint(ref i, familyWeightStableLow, ref saveData);

            //16
            SaveAndRestore.save_long(ref i, citizenGoods, ref saveData);
            SaveAndRestore.save_long(ref i, citizenGoodsTemp, ref saveData);
            SaveAndRestore.save_int(ref i, citizenCount, ref saveData);

            DebugLog.LogToFileOnly("(save)saveData in residentAI is " + i.ToString());
        }

        public static float ProcessSalaryLandPriceAdjust(ushort workBuilding)
        {
            DistrictManager districtMan = Singleton<DistrictManager>.instance;
            ushort district = districtMan.GetDistrict(Singleton<BuildingManager>.instance.m_buildings.m_buffer[workBuilding].m_position);
            float localSalaryIdex = (districtMan.m_districts.m_buffer[district].GetLandValue() + 50f) / 50f;
            float citySalaryIdex = (districtMan.m_districts.m_buffer[0].GetLandValue() + 50f) / 50f;
            return (localSalaryIdex + citySalaryIdex) / 2f;
        }

        public static bool isGoverment(ushort buildingID)
        {
            bool isGoverment = false;
            Building buildingData = Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingID];
            switch (buildingData.Info.m_class.m_service)
            {
                case ItemClass.Service.Disaster:
                case ItemClass.Service.PoliceDepartment:
                case ItemClass.Service.Education:
                case ItemClass.Service.Road:
                case ItemClass.Service.Garbage:
                case ItemClass.Service.HealthCare:
                case ItemClass.Service.Beautification:
                case ItemClass.Service.Monument:
                case ItemClass.Service.Water:
                case ItemClass.Service.Electricity:
                case ItemClass.Service.FireDepartment:
                case ItemClass.Service.PlayerIndustry:
                case ItemClass.Service.PublicTransport:
                    isGoverment = true; break;
            }
            return isGoverment;
        }

        public static int ProcessCitizenSalary(uint citizenId, bool checkOnly)
        {
            int salary = 0;
            System.Random rand = new System.Random();
            if (citizenId != 0u)
            {
                Citizen.Flags tempFlag = Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenId].m_flags;
                if ((tempFlag & Citizen.Flags.Student) != Citizen.Flags.None || (tempFlag & Citizen.Flags.Sick) != Citizen.Flags.None)
                {
                    return salary;
                }
                ushort workBuilding = Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenId].m_workBuilding;
                if (workBuilding != 0u)
                {
                    Building buildingData = Singleton<BuildingManager>.instance.m_buildings.m_buffer[workBuilding];
                    int aliveWorkCount = 0;
                    int totalWorkCount = 0;
                    Citizen.BehaviourData behaviour = default(Citizen.BehaviourData);
                    BuildingUI.GetWorkBehaviour((ushort)workBuilding, ref buildingData, ref behaviour, ref aliveWorkCount, ref totalWorkCount);
                    if (!isGoverment(workBuilding))
                    {
                        switch (buildingData.Info.m_class.m_subService)
                        {
                            case ItemClass.SubService.CommercialHigh:
                                if (MainDataStore.building_money[workBuilding] > 0 && totalWorkCount != 0)
                                {
                                    switch (buildingData.Info.m_class.m_level)
                                    {
                                        case ItemClass.Level.Level1:
                                            salary = (int)(MainDataStore.building_money[workBuilding] * 0.2f / totalWorkCount);
                                            break;
                                        case ItemClass.Level.Level2:
                                            salary = (int)(MainDataStore.building_money[workBuilding] * 0.4f / totalWorkCount);
                                            break;
                                        case ItemClass.Level.Level3:
                                            salary = (int)(MainDataStore.building_money[workBuilding] * 0.7f / totalWorkCount);
                                            break;
                                    }
                                }
                                break; //
                            case ItemClass.SubService.CommercialLow:
                                if (MainDataStore.building_money[workBuilding] > 0 && totalWorkCount != 0)
                                {
                                    switch (buildingData.Info.m_class.m_level)
                                    {
                                        case ItemClass.Level.Level1:
                                            salary = (int)(MainDataStore.building_money[workBuilding] * 0.1f / totalWorkCount);
                                            break;
                                        case ItemClass.Level.Level2:
                                            salary = (int)(MainDataStore.building_money[workBuilding] * 0.3f / totalWorkCount);
                                            break;
                                        case ItemClass.Level.Level3:
                                            salary = (int)(MainDataStore.building_money[workBuilding] * 0.6f / totalWorkCount);
                                            break;
                                    }
                                }
                                break; //
                            case ItemClass.SubService.IndustrialGeneric:
                                if (MainDataStore.building_money[workBuilding] > 0 && totalWorkCount != 0)
                                {
                                    switch (buildingData.Info.m_class.m_level)
                                    {
                                        case ItemClass.Level.Level1:
                                            salary = (int)(MainDataStore.building_money[workBuilding] * 0.1f / totalWorkCount);
                                            break;
                                        case ItemClass.Level.Level2:
                                            salary = (int)(MainDataStore.building_money[workBuilding] * 0.2f / totalWorkCount);
                                            break;
                                        case ItemClass.Level.Level3:
                                            salary = (int)(MainDataStore.building_money[workBuilding] * 0.3f / totalWorkCount);
                                            break;
                                    }
                                }
                                break; //
                            case ItemClass.SubService.IndustrialFarming:
                                if (MainDataStore.building_money[workBuilding] > 0 && totalWorkCount != 0)
                                {
                                    salary = (int)(MainDataStore.building_money[workBuilding] * 0.2f / totalWorkCount);
                                }
                                break; //
                            case ItemClass.SubService.IndustrialForestry:
                                if (MainDataStore.building_money[workBuilding] > 0 && totalWorkCount != 0)
                                {
                                    salary = (int)(MainDataStore.building_money[workBuilding] * 0.2f / totalWorkCount);
                                }
                                break; //
                            case ItemClass.SubService.IndustrialOil:
                                if (MainDataStore.building_money[workBuilding] > 0 && totalWorkCount != 0)
                                {
                                    salary = (int)(MainDataStore.building_money[workBuilding] * 0.2f / totalWorkCount);
                                }
                                break; //
                            case ItemClass.SubService.IndustrialOre:
                                if (MainDataStore.building_money[workBuilding] > 0 && totalWorkCount != 0)
                                {
                                    salary = (int)(MainDataStore.building_money[workBuilding] * 0.2f / totalWorkCount);
                                }
                                break;
                            case ItemClass.SubService.CommercialLeisure:
                                if (MainDataStore.building_money[workBuilding] > 0 && totalWorkCount != 0)
                                {
                                    salary = (int)(MainDataStore.building_money[workBuilding] * 0.7f / totalWorkCount);
                                }
                                break;
                            case ItemClass.SubService.CommercialTourist:
                                if (MainDataStore.building_money[workBuilding] > 0 && totalWorkCount != 0)
                                {
                                    salary = (int)(MainDataStore.building_money[workBuilding] * 0.9f / totalWorkCount);
                                }
                                break;
                            case ItemClass.SubService.CommercialEco:
                                if (MainDataStore.building_money[workBuilding] > 0 && totalWorkCount != 0)
                                {
                                    salary = (int)(MainDataStore.building_money[workBuilding] * 0.5f / totalWorkCount);
                                }
                                break;
                            case ItemClass.SubService.OfficeGeneric:
                            case ItemClass.SubService.OfficeHightech:
                                if (MainDataStore.building_money[workBuilding] > 0 && totalWorkCount != 0)
                                {
                                    salary = (int)(MainDataStore.building_money[workBuilding] / totalWorkCount);
                                }
                                break;
                            default: break;
                        }
                        if (!checkOnly)
                        {
                            MainDataStore.building_money[workBuilding] -= salary;
                        }
                    }
                    else
                    {
                        //Goverment
                        switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenId].EducationLevel)
                        {
                            case Citizen.Education.Uneducated:
                                salary = MainDataStore.govermentEducation0Salary; break;
                            case Citizen.Education.OneSchool:
                                salary = MainDataStore.govermentEducation1Salary; break;
                            case Citizen.Education.TwoSchools:
                                salary = MainDataStore.govermentEducation2Salary; break;
                            case Citizen.Education.ThreeSchools:
                                salary = MainDataStore.govermentEducation3Salary; break;
                        }
                        int allWorkCount = 0;
                        //Update to see if there is building workplace change.
                        //If a building have 10 workers and have 100 workplacecount, we assume that the other 90 vitual workers are from outside
                        //Which will give addition cost
                        allWorkCount = TotalWorkCount((ushort)workBuilding, buildingData, false, false);
                        if (totalWorkCount > allWorkCount)
                        {
                            Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenId].SetWorkplace(citizenId, 0, 0u);
                        }
                        float vitualWorkersRatio = (totalWorkCount != 0) ? (allWorkCount / totalWorkCount) : 1;

                        //Budget offset for Salary
                        int budget = Singleton<EconomyManager>.instance.GetBudget(buildingData.Info.m_class);
                        salary = (int)(salary * budget / 100f);

                        //LandPrice offset for Salary
                        float landPriceOffset = ProcessSalaryLandPriceAdjust(workBuilding);
                        salary = (int)(salary * landPriceOffset);
#if Debug
                        DebugLog.LogToFileOnly("DebugInfo: LandPrice offset for Salary is " + landPriceOffset.ToString());
#endif
                        if (!checkOnly)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)(salary * vitualWorkersRatio * MainDataStore.gameExpenseDivide), Singleton<BuildingManager>.instance.m_buildings.m_buffer[workBuilding].Info.m_class);
                        }
                    }
                }
            }
            return salary;
        }//public


        public static int TotalWorkCount(ushort buildingID, Building data, bool checkOnly, bool update)
        {
            int totalWorkCount = 0;
            //For performance
#if FASTRUN
            update = false;
#endif
            if (MainDataStore.isBuildingWorkerUpdated[buildingID] && !update)
            {
                totalWorkCount = MainDataStore.building_buffer1[buildingID];
            }
            else
            {
                if (data.Info.m_buildingAI is LandfillSiteAI)
                {
                    LandfillSiteAI buildingAI = data.Info.m_buildingAI as LandfillSiteAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is ExtractingFacilityAI)
                {
                    ExtractingFacilityAI buildingAI = data.Info.m_buildingAI as ExtractingFacilityAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is ProcessingFacilityAI)
                {
                    ProcessingFacilityAI buildingAI = data.Info.m_buildingAI as ProcessingFacilityAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is PoliceStationAI)
                {
                    PoliceStationAI buildingAI = data.Info.m_buildingAI as PoliceStationAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is FireStationAI)
                {
                    FireStationAI buildingAI = data.Info.m_buildingAI as FireStationAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is HospitalAI)
                {
                    HospitalAI buildingAI = data.Info.m_buildingAI as HospitalAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is CargoStationAI)
                {
                    CargoStationAI buildingAI = data.Info.m_buildingAI as CargoStationAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is TransportStationAI)
                {
                    TransportStationAI buildingAI = data.Info.m_buildingAI as TransportStationAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is CemeteryAI)
                {
                    CemeteryAI buildingAI = data.Info.m_buildingAI as CemeteryAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is MedicalCenterAI)
                {
                    MedicalCenterAI buildingAI = data.Info.m_buildingAI as MedicalCenterAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is MonumentAI)
                {
                    MonumentAI buildingAI = data.Info.m_buildingAI as MonumentAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is DepotAI)
                {
                    DepotAI buildingAI = data.Info.m_buildingAI as DepotAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is HelicopterDepotAI)
                {
                    HelicopterDepotAI buildingAI = data.Info.m_buildingAI as HelicopterDepotAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is MaintenanceDepotAI)
                {
                    MaintenanceDepotAI buildingAI = data.Info.m_buildingAI as MaintenanceDepotAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is FirewatchTowerAI)
                {
                    FirewatchTowerAI buildingAI = data.Info.m_buildingAI as FirewatchTowerAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is DoomsdayVaultAI)
                {
                    DoomsdayVaultAI buildingAI = data.Info.m_buildingAI as DoomsdayVaultAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is DisasterResponseBuildingAI)
                {
                    DisasterResponseBuildingAI buildingAI = data.Info.m_buildingAI as DisasterResponseBuildingAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is HadronColliderAI)
                {
                    HadronColliderAI buildingAI = data.Info.m_buildingAI as HadronColliderAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is SchoolAI)
                {
                    SchoolAI buildingAI = data.Info.m_buildingAI as SchoolAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is PowerPlantAI)
                {
                    PowerPlantAI buildingAI = data.Info.m_buildingAI as PowerPlantAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is SnowDumpAI)
                {
                    SnowDumpAI buildingAI = data.Info.m_buildingAI as SnowDumpAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is WarehouseAI)
                {
                    WarehouseAI buildingAI = data.Info.m_buildingAI as WarehouseAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is WaterFacilityAI)
                {
                    WaterFacilityAI buildingAI = data.Info.m_buildingAI as WaterFacilityAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is SaunaAI)
                {
                    SaunaAI buildingAI = data.Info.m_buildingAI as SaunaAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is PostOfficeAI)
                {
                    PostOfficeAI buildingAI = data.Info.m_buildingAI as PostOfficeAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is RadioMastAI)
                {
                    RadioMastAI buildingAI = data.Info.m_buildingAI as RadioMastAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is SpaceElevatorAI)
                {
                    SpaceElevatorAI buildingAI = data.Info.m_buildingAI as SpaceElevatorAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is SpaceRadarAI)
                {
                    SpaceRadarAI buildingAI = data.Info.m_buildingAI as SpaceRadarAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is MainIndustryBuildingAI)
                {
                    MainIndustryBuildingAI buildingAI = data.Info.m_buildingAI as MainIndustryBuildingAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is AuxiliaryBuildingAI)
                {
                    AuxiliaryBuildingAI buildingAI = data.Info.m_buildingAI as AuxiliaryBuildingAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is ShelterAI)
                {
                    ShelterAI buildingAI = data.Info.m_buildingAI as ShelterAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is HeatingPlantAI)
                {
                    HeatingPlantAI buildingAI = data.Info.m_buildingAI as HeatingPlantAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else
                {
                    if (!checkOnly)
                    {
                        DebugLog.LogToFileOnly("Error: find unknow building = " + data.Info.m_buildingAI.ToString());
                    }
                }

                MainDataStore.isBuildingWorkerUpdated[buildingID] = true;
                MainDataStore.building_buffer1[buildingID] = totalWorkCount;
            }
            return totalWorkCount;
        }

        public void ProcessCitizen(uint homeID, ref CitizenUnit data, bool isPre)
        {
            if (isPre)
            {
                MainDataStore.family_money[homeID] = 0;
                if (data.m_citizen0 != 0)
                {
                    Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen0];
                    if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                    {
                        citizenCount++;
                        MainDataStore.family_money[homeID] += MainDataStore.citizenMoney[data.m_citizen0];
                    }
                }
                if (data.m_citizen1 != 0)
                {
                    Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen1];
                    if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                    {
                        citizenCount++;
                        MainDataStore.family_money[homeID] += MainDataStore.citizenMoney[data.m_citizen1];
                    }
                }
                if (data.m_citizen2 != 0)
                {
                    Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen2];
                    if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                    {
                        citizenCount++;
                        MainDataStore.family_money[homeID] += MainDataStore.citizenMoney[data.m_citizen2];
                    }
                }
                if (data.m_citizen3 != 0)
                {
                    Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen3];
                    if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                    {
                        citizenCount++;
                        MainDataStore.family_money[homeID] += MainDataStore.citizenMoney[data.m_citizen3];
                    }
                }
                if (data.m_citizen4 != 0)
                {
                    Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen4];
                    if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                    {
                        citizenCount++;
                        MainDataStore.family_money[homeID] += MainDataStore.citizenMoney[data.m_citizen4];
                    }
                }
            }
            else
            {
                if (MainDataStore.family_money[homeID] < 5000)
                {
                    familyWeightStableLow = (ushort)(familyWeightStableLow + 1);
                }
                else if (MainDataStore.family_money[homeID] >= 20000)
                {
                    familyWeightStableHigh = (ushort)(familyWeightStableHigh + 1);
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

                if (temp!=0)
                {
                    if (data.m_citizen0 != 0)
                    {
                        Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen0];
                        if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                        {
                            MainDataStore.citizenMoney[data.m_citizen0] = MainDataStore.family_money[homeID] / (float)temp;
                        }
                    }
                    if (data.m_citizen1 != 0)
                    {
                        Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen1];
                        if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                        {
                            MainDataStore.citizenMoney[data.m_citizen1] = MainDataStore.family_money[homeID] / (float)temp;
                        }
                    }
                    if (data.m_citizen2 != 0)
                    {
                        Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen2];
                        if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                        {
                            MainDataStore.citizenMoney[data.m_citizen2] = MainDataStore.family_money[homeID] / (float)temp;
                        }
                    }
                    if (data.m_citizen3 != 0)
                    {
                        Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen3];
                        if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                        {
                            MainDataStore.citizenMoney[data.m_citizen3] = MainDataStore.family_money[homeID] / (float)temp;
                        }
                    }
                    if (data.m_citizen4 != 0)
                    {
                        Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen4];
                        if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                        {
                            MainDataStore.citizenMoney[data.m_citizen4] = MainDataStore.family_money[homeID] / (float)temp;
                        }
                    }
                }
            }

        }


        public byte ProcessFamily(uint homeID, ref CitizenUnit data)
        {
            if (preCitizenId > homeID)
            {
                MainDataStore.familyCount = familyCount;
                MainDataStore.citizenCount = citizenCount;
                MainDataStore.family_profit_money_num = familyProfitMoneyCount;
                MainDataStore.family_very_profit_money_num = familyVeryProfitMoneyCount;
                MainDataStore.family_loss_money_num = familyLossMoneyCount;
                if (familyCount != 0)
                {
                    MainDataStore.citizenSalaryPerFamily = (int)((citizenSalaryCount / familyCount));
                    MainDataStore.citizenExpensePerFamily = (int)((citizenExpenseCount / familyCount));
                }
                MainDataStore.citizenExpense = citizenExpenseCount;
                MainDataStore.citizenSalaryTaxTotal = citizenSalaryTaxTotal;
                MainDataStore.citizenSalaryTotal = citizenSalaryCount;
                if (MainDataStore.familyCount < MainDataStore.family_weight_stable_high)
                {
                    MainDataStore.family_weight_stable_high = (uint)MainDataStore.familyCount;
                }
                else
                {
                    MainDataStore.family_weight_stable_high = familyWeightStableHigh;
                }
                if (MainDataStore.familyCount < MainDataStore.family_weight_stable_low)
                {
                    MainDataStore.family_weight_stable_low = (uint)MainDataStore.familyCount;
                }
                else
                {
                    MainDataStore.family_weight_stable_low = familyWeightStableLow;
                }
                citizenGoods = citizenGoodsTemp;
                familyVeryProfitMoneyCount = 0;
                familyProfitMoneyCount = 0;
                familyLossMoneyCount = 0;
                familyCount = 0;
                citizenCount = 0;
                citizenSalaryCount = 0;
                citizenExpenseCount = 0;
                citizenSalaryTaxTotal = 0;
                tempCitizenSalaryTaxTotal = 0f;
                familyWeightStableHigh = 0;
                familyWeightStableLow = 0;
                citizenGoodsTemp = 0;
            }
            preCitizenId = homeID;

            familyCount++;
            citizenGoodsTemp += data.m_goods;

            if (homeID > 524288)
            {
                DebugLog.LogToFileOnly("Error: citizen ID greater than 524288");
            }
            //ProcessCitizen pre, gather all citizenMoney to family
            ProcessCitizen(homeID, ref data, true);
            //1.We caculate citizen income
            int familySalaryCurrent = 0;
            familySalaryCurrent += ProcessCitizenSalary(data.m_citizen0, false);
            familySalaryCurrent += ProcessCitizenSalary(data.m_citizen1, false);
            familySalaryCurrent += ProcessCitizenSalary(data.m_citizen2, false);
            familySalaryCurrent += ProcessCitizenSalary(data.m_citizen3, false);
            familySalaryCurrent += ProcessCitizenSalary(data.m_citizen4, false);
            citizenSalaryCount = citizenSalaryCount + familySalaryCurrent;
            if (familySalaryCurrent < 0)
            {
                DebugLog.LogToFileOnly("familySalaryCurrent< 0 in ResidentAI");
                familySalaryCurrent = 0;
            }

            //2.We caculate salary tax
            float tax = (float)Politics.residentTax * (float)familySalaryCurrent / 100f;
            tempCitizenSalaryTaxTotal = tempCitizenSalaryTaxTotal + (int)tax;
            citizenSalaryTaxTotal = (int)tempCitizenSalaryTaxTotal;
            ProcessCitizenIncomeTax(homeID, tax);
            
            //3. We caculate expense
            int educationFee = 0;
            int expenseRate = 0;
            CitizenManager instance = Singleton<CitizenManager>.instance;
            if (data.m_citizen4 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen4)].Dead)
            {
                expenseRate = 0;
                educationFee += GetExpenseRate(homeID, data.m_citizen4, out expenseRate);
            }
            if (data.m_citizen3 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen3)].Dead)
            {
                expenseRate = 0;
                educationFee += GetExpenseRate(homeID, data.m_citizen3, out expenseRate);
            }
            if (data.m_citizen2 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen2)].Dead)
            {
                expenseRate = 0;
                educationFee += GetExpenseRate(homeID, data.m_citizen2, out expenseRate);
            }
            if (data.m_citizen1 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen1)].Dead)
            {
                expenseRate = 0;
                educationFee += GetExpenseRate(homeID, data.m_citizen1, out expenseRate);
            }
            if (data.m_citizen0 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen0)].Dead)
            {
                expenseRate = 0;
                educationFee += GetExpenseRate(homeID, data.m_citizen0, out expenseRate);
            }
            ProcessCitizenHouseRent(homeID, expenseRate);
            citizenExpenseCount += (educationFee + expenseRate);

            //4. income - expense
            float incomeMinusExpense = familySalaryCurrent - tax - educationFee - expenseRate;
            MainDataStore.family_money[homeID] += incomeMinusExpense;

            //5. Process shopping
            if (MainDataStore.familyGoods[homeID] == 0)
            {
                //first time
            }
            else if (MainDataStore.familyGoods[homeID] < data.m_goods)
            {
                MainDataStore.family_money[homeID] -= RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Shopping) * (data.m_goods - MainDataStore.familyGoods[homeID]);
            }
            
            //6. Process citizen status
            if (incomeMinusExpense <= 0)
            {
                familyLossMoneyCount = (uint)(familyLossMoneyCount + 1);
            }
            else if (incomeMinusExpense > 70)
            {
                familyVeryProfitMoneyCount = (uint)(familyVeryProfitMoneyCount + 1);
            }
            else
            {
                familyProfitMoneyCount = (uint)(familyProfitMoneyCount + 1);
            }

            //7. Caculate minimumLivingAllowance and benefitOffset
            if (MainDataStore.family_money[homeID] < -Politics.benefitOffset)
            {
                int num = (int)(-(MainDataStore.family_money[homeID]) + 0.5f + Politics.benefitOffset);
                MainDataStore.family_money[homeID] = 0;
                MainDataStore.minimumLivingAllowance += num;
                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, num, ItemClass.Service.Residential, ItemClass.SubService.None, ItemClass.Level.Level1);
            }
            else
            {
                if (Politics.benefitOffset > 0)
                {
                    MainDataStore.family_money[homeID] += Politics.benefitOffset;
                    MainDataStore.minimumLivingAllowance += Politics.benefitOffset;
                    Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, Politics.benefitOffset, ItemClass.Service.Residential, ItemClass.SubService.None, ItemClass.Level.Level1);
                }
            }

            //8. Limit familyMoney
            if (MainDataStore.family_money[homeID] > 32000000f)
            {
                MainDataStore.family_money[homeID] = 32000000f;
            }

            if (MainDataStore.family_money[homeID] < -32000000f)
            {
                MainDataStore.family_money[homeID] = -32000000f;
            }
            
            //ProcessCitizen post, split all familyMoney to citizen
            ProcessCitizen(homeID, ref data, false);
            //9. Fixed m_goods consuption
            //9.1 based on incomeMinusExpense
            float fixedGoodsConsumption = incomeMinusExpense / 10;
            //9.2 based on familyMoney
            if (MainDataStore.family_money[homeID] > 0)
            {
                fixedGoodsConsumption += (int)(MainDataStore.family_money[homeID] / 5000);
            }
            if (fixedGoodsConsumption < 1)
            {
                fixedGoodsConsumption = 1;
            } else if (fixedGoodsConsumption > 20)
            {
                fixedGoodsConsumption = 20;
            }
            return (byte)fixedGoodsConsumption;
        }


        public void ProcessCitizenIncomeTax(uint homeID, float tax)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            ushort building = instance.m_units.m_buffer[(int)((UIntPtr)homeID)].m_building;
            Building buildingdata = Singleton<BuildingManager>.instance.m_buildings.m_buffer[building];
            Singleton<EconomyManager>.instance.AddPrivateIncome((int)(tax), buildingdata.Info.m_class.m_service, buildingdata.Info.m_class.m_subService, buildingdata.Info.m_class.m_level, 112);
        }

        public void ProcessCitizenHouseRent(uint homeID, int expenserate)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            ushort building = instance.m_units.m_buffer[(int)((UIntPtr)homeID)].m_building;
            Building buildingdata = Singleton<BuildingManager>.instance.m_buildings.m_buffer[building];
            DistrictManager instance2 = Singleton<DistrictManager>.instance;
            byte district = instance2.GetDistrict(buildingdata.m_position);
            Singleton<EconomyManager>.instance.AddPrivateIncome(expenserate*100, buildingdata.Info.m_class.m_service, buildingdata.Info.m_class.m_subService, buildingdata.Info.m_class.m_level, 100);
        }

        // ResidentAI
        public void CustomSimulationStep(uint homeID, ref CitizenUnit data)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            ushort building = instance.m_units.m_buffer[(int)((UIntPtr)homeID)].m_building;
            if (data.m_citizen0 != 0u && data.m_citizen1 != 0u && (data.m_citizen2 == 0u || data.m_citizen3 == 0u || data.m_citizen4 == 0u))
            {
                bool flag = this.CanMakeBabies(data.m_citizen0, ref instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen0)]);
                bool flag2 = this.CanMakeBabies(data.m_citizen1, ref instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen1)]);
                if (flag && flag2 && Singleton<SimulationManager>.instance.m_randomizer.Int32(12u) == 0)
                {
                    int family = (int)instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen0)].m_family;
                    uint num;
                    if (instance.CreateCitizen(out num, 0, family, ref Singleton<SimulationManager>.instance.m_randomizer))
                    {
                        instance.m_citizens.m_buffer[(int)((UIntPtr)num)].SetHome(num, 0, homeID);
                        Citizen[] expr_126_cp_0 = instance.m_citizens.m_buffer;
                        UIntPtr expr_126_cp_1 = (UIntPtr)num;
                        expr_126_cp_0[(int)expr_126_cp_1].m_flags = (expr_126_cp_0[(int)expr_126_cp_1].m_flags | Citizen.Flags.Original);
                        if (building != 0)
                        {
                            DistrictManager instance2 = Singleton<DistrictManager>.instance;
                            Vector3 position = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)building].m_position;
                            byte district = instance2.GetDistrict(position);
                            District[] expr_183_cp_0_cp_0 = instance2.m_districts.m_buffer;
                            byte expr_183_cp_0_cp_1 = district;
                            expr_183_cp_0_cp_0[(int)expr_183_cp_0_cp_1].m_birthData.m_tempCount = expr_183_cp_0_cp_0[(int)expr_183_cp_0_cp_1].m_birthData.m_tempCount + 1u;
                        }
                    }
                }
            }
            if (data.m_citizen0 != 0u && data.m_citizen1 == 0u)
            {
                this.TryFindPartner(data.m_citizen0, ref instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen0)]);
            }
            else if (data.m_citizen1 != 0u && data.m_citizen0 == 0u)
            {
                this.TryFindPartner(data.m_citizen1, ref instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen1)]);
            }
            if (data.m_citizen2 != 0u)
            {
                this.TryMoveAwayFromHome(data.m_citizen2, ref instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen2)]);
            }
            if (data.m_citizen3 != 0u)
            {
                this.TryMoveAwayFromHome(data.m_citizen3, ref instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen3)]);
            }
            if (data.m_citizen4 != 0u)
            {
                this.TryMoveAwayFromHome(data.m_citizen4, ref instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen4)]);
            }

            // NON-STOCK CODE START
            int fixedGoodsConsumption = 0;
            if ((Singleton<BuildingManager>.instance.m_buildings.m_buffer[data.m_building].m_flags & (Building.Flags.Completed | Building.Flags.Upgrading)) != Building.Flags.None)
            {
                fixedGoodsConsumption = ProcessFamily(homeID, ref data);
            }
            data.m_goods = (ushort)Mathf.Max(1, (int)(data.m_goods - fixedGoodsConsumption)); //here we can adjust demand
            MainDataStore.familyGoods[homeID] = data.m_goods;
            // NON-STOCK CODE END
            if (data.m_goods < 200)
            {
                int num2 = Singleton<SimulationManager>.instance.m_randomizer.Int32(5u);
                for (int i = 0; i < 5; i++)
                {
                    uint citizen = data.GetCitizen((num2 + i) % 5);
                    if (citizen != 0u)
                    {
                        Citizen[] expr_2FA_cp_0 = instance.m_citizens.m_buffer;
                        UIntPtr expr_2FA_cp_1 = (UIntPtr)citizen;
                        expr_2FA_cp_0[(int)expr_2FA_cp_1].m_flags = (expr_2FA_cp_0[(int)expr_2FA_cp_1].m_flags | Citizen.Flags.NeedGoods);
                        break;
                    }
                }
            }
            if (building != 0 && (Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)building].m_problems & (Notification.Problem.MajorProblem | Notification.Problem.FatalProblem)) != Notification.Problem.None)
            {
                uint num3 = 0u;
                int num4 = 0;
                if (data.m_citizen4 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen4)].Dead)
                {
                    num4++;
                    num3 = data.m_citizen4;
                }
                if (data.m_citizen3 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen3)].Dead)
                {
                    num4++;
                    num3 = data.m_citizen3;
                }
                if (data.m_citizen2 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen2)].Dead)
                {
                    num4++;
                    num3 = data.m_citizen2;
                }
                if (data.m_citizen1 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen1)].Dead)
                {
                    num4++;
                    num3 = data.m_citizen1;
                }
                if (data.m_citizen0 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen0)].Dead)
                {
                    num4++;
                    num3 = data.m_citizen0;
                }
                if (num3 != 0u)
                {
                    this.TryMoveFamily(num3, ref instance.m_citizens.m_buffer[(int)((UIntPtr)num3)], num4);
                }
            }
        }

        public int GetExpenseRate(uint homeid, uint citizenID, out int incomeAccumulation)
        {
            BuildingManager instance1 = Singleton<BuildingManager>.instance;
            CitizenManager instance2 = Singleton<CitizenManager>.instance;
            ItemClass @class = instance1.m_buildings.m_buffer[instance2.m_citizens.m_buffer[citizenID].m_homeBuilding].Info.m_class;
            incomeAccumulation = 0;
            DistrictManager instance = Singleton<DistrictManager>.instance;
            if (instance2.m_citizens.m_buffer[citizenID].m_homeBuilding != 0)
            {
                byte district = instance.GetDistrict(instance1.m_buildings.m_buffer[instance2.m_citizens.m_buffer[citizenID].m_homeBuilding].m_position);
                DistrictPolicies.Taxation taxationPolicies = instance.m_districts.m_buffer[(int)district].m_taxationPolicies;
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
                if (MainDataStore.family_money[homeid] > 0)
                {
                    incomeAccumulation = (int)(num2 * incomeAccumulation * ((float)(instance.m_districts.m_buffer[(int)district].GetLandValue() + 50) / 10000));
                } else
                {
                    incomeAccumulation = 0;
                }
            }

            int educationFee = 0;
            if ((Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenID].m_flags & Citizen.Flags.Student) != Citizen.Flags.None)
            {
                //Only university will cost money
                if (MainDataStore.family_money[homeid] > 0 && (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenID].m_flags.IsFlagSet(Citizen.Flags.Education2)))
                {
                    educationFee += 20;
                    Singleton<EconomyManager>.instance.AddPrivateIncome(20, ItemClass.Service.Education, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                }
            }
            return educationFee;
        }


        public void GetVoteTickets()
        {
            System.Random rand = new System.Random();
            if (Politics.cPartyChance + Politics.gPartyChance + Politics.sPartyChance + Politics.lPartyChance + Politics.nPartyChance != (800 + RealCityEconomyExtension.partyTrendStrength))
            {
                if (rand.Next(64) <= 1)
                {
                    DebugLog.LogToFileOnly("Error: Chance is not equal 800 " + (Politics.cPartyChance + Politics.gPartyChance + Politics.sPartyChance + Politics.lPartyChance + Politics.nPartyChance).ToString());
                }
            }
            int temp = rand.Next(800 + RealCityEconomyExtension.partyTrendStrength) + 1;
            if (temp < Politics.cPartyChance)
            {
                Politics.cPartyTickets++;
            }
            else if (temp < Politics.cPartyChance + Politics.gPartyChance)
            {
                Politics.gPartyTickets++;
            }
            else if (temp < Politics.cPartyChance + Politics.gPartyChance + Politics.sPartyChance)
            {
                Politics.sPartyTickets++;
            }
            else if (temp < Politics.cPartyChance + Politics.gPartyChance + Politics.sPartyChance + Politics.lPartyChance)
            {
                Politics.lPartyTickets++;
            }
            else
            {
                Politics.nPartyTickets++;
            }
        }

        public void GetVoteChance(uint citizenID, Citizen citizen, uint homeID)
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

                    if (MainDataStore.family_money[homeID] < 5000)
                    {
                        idex = 0;
                    }
                    else if (MainDataStore.family_money[homeID] >= 20000)
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
