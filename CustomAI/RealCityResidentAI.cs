using System;
using ColossalFramework;
using RealCity.Util;
using RealCity.UI;

namespace RealCity.CustomAI
{
    public static class RealCityResidentAI
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
                case ItemClass.Service.PlayerEducation:
                case ItemClass.Service.Museums:
                case ItemClass.Service.VarsitySports:
                    isGoverment = true; break;
            }
            return isGoverment;
        }

        public static int ProcessCitizenSalary(uint citizenId, bool checkOnly)
        {
            int salary = 0;
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
                    BuildingUI.GetWorkBehaviour(workBuilding, ref buildingData, ref behaviour, ref aliveWorkCount, ref totalWorkCount);
                    if (!isGoverment(workBuilding))
                    {
                        switch (buildingData.Info.m_class.m_service)
                        {
                            case ItemClass.Service.Commercial:
                            case ItemClass.Service.Industrial:
                                if (MainDataStore.building_money[workBuilding] > 0 && totalWorkCount != 0)
                                {
                                    salary = (int)(MainDataStore.building_money[workBuilding] * 0.05f / totalWorkCount);
                                    break;
                                }
                                break;
                            case ItemClass.Service.Office:
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
                        int salaryMax = 0;
                        switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenId].EducationLevel)
                        {
                            case Citizen.Education.Uneducated:
                                salaryMax = MainDataStore.govermentEducation0Salary;
                                salary = MainDataStore.govermentEducation0SalaryFixed; break;
                            case Citizen.Education.OneSchool:
                                salaryMax = MainDataStore.govermentEducation1Salary;
                                salary = MainDataStore.govermentEducation1SalaryFixed; break;
                            case Citizen.Education.TwoSchools:
                                salaryMax = MainDataStore.govermentEducation2Salary;
                                salary = MainDataStore.govermentEducation2SalaryFixed; break;
                            case Citizen.Education.ThreeSchools:
                                salaryMax = MainDataStore.govermentEducation3Salary;
                                salary = MainDataStore.govermentEducation3SalaryFixed; break;
                        }
                        int allWorkCount = 0;
                        //Update to see if there is building workplace change.
                        //If a building have 10 workers and have 100 workplacecount, we assume that the other 90 vitual workers are from outside
                        //Which will give addition cost
                        allWorkCount = TotalWorkCount(workBuilding, buildingData, false, false);
                        if (totalWorkCount > allWorkCount)
                        {
                            Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenId].SetWorkplace(citizenId, 0, 0u);
                        }
                        float vitualWorkersRatio = (totalWorkCount != 0) ? (allWorkCount / (float)totalWorkCount) : 1f;

                        //Budget offset for Salary
                        int budget = Singleton<EconomyManager>.instance.GetBudget(buildingData.Info.m_class);
                        salary = (int)(salary * budget / 100f);

                        //LandPrice offset for Salary
                        float landPriceOffset = ProcessSalaryLandPriceAdjust(workBuilding);
                        salary = (int)(salary * landPriceOffset);
                        salary = Math.Max(salary, salaryMax);
#if Debug
                        DebugLog.LogToFileOnly("DebugInfo: LandPrice offset for Salary is " + landPriceOffset.ToString());
#endif
                        if (!checkOnly)
                        {
                            var m_class = Singleton<BuildingManager>.instance.m_buildings.m_buffer[workBuilding].Info.m_class;
                            Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)16, (int)(salary * vitualWorkersRatio), m_class);
                        }
                    }
                }
            }

            return UniqueFacultyAI.IncreaseByBonus(UniqueFacultyAI.FacultyBonus.Science, salary);
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
                else if (data.Info.m_buildingAI is MainCampusBuildingAI)
                {
                    MainCampusBuildingAI buildingAI = data.Info.m_buildingAI as MainCampusBuildingAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is MuseumAI)
                {
                    MuseumAI buildingAI = data.Info.m_buildingAI as MuseumAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is UniqueFactoryAI)
                {
                    UniqueFactoryAI buildingAI = data.Info.m_buildingAI as UniqueFactoryAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is UniqueFacultyAI)
                {
                    UniqueFacultyAI buildingAI = data.Info.m_buildingAI as UniqueFacultyAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is VarsitySportsArenaAI)
                {
                    VarsitySportsArenaAI buildingAI = data.Info.m_buildingAI as VarsitySportsArenaAI;
                    totalWorkCount = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is LibraryAI)
                {
                    LibraryAI buildingAI = data.Info.m_buildingAI as LibraryAI;
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
    }
}
