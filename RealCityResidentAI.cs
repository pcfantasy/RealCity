﻿using System;
using ColossalFramework;
using UnityEngine;
using ColossalFramework.Math;
using System.Reflection;
using System.Text.RegularExpressions;

namespace RealCity
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

        public static int CitizenSalary(uint citizenId, bool checkOnly)
        {
            int num = 0;
            System.Random rand = new System.Random();
            //Array16<Building> buildings = Singleton<BuildingManager>.instance.m_buildings;
            if (citizenId != 0u)
            {
                Citizen.Flags temp_flag = Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenId].m_flags;
                if ((temp_flag & Citizen.Flags.Student) != Citizen.Flags.None || (temp_flag & Citizen.Flags.Sick) != Citizen.Flags.None)
                {
                    return num;
                }
                int budget = 0;
                int aliveWorkCount = 0;
                int totalWorkCount = 0;
                Citizen.BehaviourData behaviour = default(Citizen.BehaviourData);
                int workBuilding = Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenId].m_workBuilding;

                if (workBuilding != 0u)
                {
                    Building buildingData = Singleton<BuildingManager>.instance.m_buildings.m_buffer[workBuilding];
                    budget = Singleton<EconomyManager>.instance.GetBudget(buildingData.Info.m_class);
                    if (budget < 100)
                    {
                        budget = (budget * budget + 99) / 100;
                    }
                    else if (budget > 150)
                    {
                        budget = 125;
                    }
                    else if (budget > 100)
                    {
                        budget -= (100 - budget) * (100 - budget) / 100;
                    }
                    BuildingUI.GetWorkBehaviour((ushort)workBuilding, ref buildingData, ref behaviour, ref aliveWorkCount, ref totalWorkCount);
                    switch (buildingData.Info.m_class.m_subService)
                    {
                        case ItemClass.SubService.CommercialHigh:
                            if (MainDataStore.building_money[workBuilding] > 0 && totalWorkCount != 0)
                            {
                                switch (buildingData.Info.m_class.m_level)
                                {
                                    case ItemClass.Level.Level1:
                                        num = (int)(MainDataStore.building_money[workBuilding] * 0.2f / totalWorkCount);
                                        break;
                                    case ItemClass.Level.Level2:
                                        num = (int)(MainDataStore.building_money[workBuilding] * 0.4f / totalWorkCount);
                                        break;
                                    case ItemClass.Level.Level3:
                                        num = (int)(MainDataStore.building_money[workBuilding] * 0.7f / totalWorkCount);
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
                                        num = (int)(MainDataStore.building_money[workBuilding] * 0.1f / totalWorkCount);
                                        break;
                                    case ItemClass.Level.Level2:
                                        num = (int)(MainDataStore.building_money[workBuilding] * 0.3f / totalWorkCount);
                                        break;
                                    case ItemClass.Level.Level3:
                                        num = (int)(MainDataStore.building_money[workBuilding] * 0.6f / totalWorkCount);
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
                                        num = (int)(MainDataStore.building_money[workBuilding] * 0.1f / totalWorkCount);
                                        break;
                                    case ItemClass.Level.Level2:
                                        num = (int)(MainDataStore.building_money[workBuilding] * 0.2f / totalWorkCount);
                                        break;
                                    case ItemClass.Level.Level3:
                                        num = (int)(MainDataStore.building_money[workBuilding] * 0.3f / totalWorkCount);
                                        break;
                                }
                            }
                            break; //
                        case ItemClass.SubService.IndustrialFarming:
                            if (MainDataStore.building_money[workBuilding] > 0 && totalWorkCount != 0)
                            {
                                num = (int)(MainDataStore.building_money[workBuilding] * 0.2f / totalWorkCount);
                            }
                            break; //
                        case ItemClass.SubService.IndustrialForestry:
                            if (MainDataStore.building_money[workBuilding] > 0 && totalWorkCount != 0)
                            {
                                num = (int)(MainDataStore.building_money[workBuilding] * 0.2f / totalWorkCount);
                            }
                            break; //
                        case ItemClass.SubService.IndustrialOil:
                            if (MainDataStore.building_money[workBuilding] > 0 && totalWorkCount != 0)
                            {
                                num = (int)(MainDataStore.building_money[workBuilding] * 0.2f / totalWorkCount);
                            }
                            break; //
                        case ItemClass.SubService.IndustrialOre:
                            if (MainDataStore.building_money[workBuilding] > 0 && totalWorkCount != 0)
                            {
                                num = (int)(MainDataStore.building_money[workBuilding] * 0.2f / totalWorkCount);
                            }
                            break;
                        case ItemClass.SubService.CommercialLeisure:
                            if (MainDataStore.building_money[workBuilding] > 0 && totalWorkCount != 0)
                            {
                                num = (int)(MainDataStore.building_money[workBuilding] * 0.7f / totalWorkCount);
                            }
                            break;
                        case ItemClass.SubService.CommercialTourist:
                            if (MainDataStore.building_money[workBuilding] > 0 && totalWorkCount != 0)
                            {
                                num = (int)(MainDataStore.building_money[workBuilding] * 0.9f / totalWorkCount);
                            }
                            break;
                        case ItemClass.SubService.CommercialEco:
                            if (MainDataStore.building_money[workBuilding] > 0 && totalWorkCount != 0)
                            {
                                num = (int)(MainDataStore.building_money[workBuilding] * 0.5f / totalWorkCount);
                            }
                            break;
                        case ItemClass.SubService.PublicTransportBus:
                        case ItemClass.SubService.PublicTransportTram:
                        case ItemClass.SubService.PublicTransportTrain:
                        case ItemClass.SubService.PublicTransportTaxi:
                        case ItemClass.SubService.PublicTransportShip:
                        case ItemClass.SubService.PublicTransportMetro:
                        case ItemClass.SubService.PublicTransportPlane:
                        case ItemClass.SubService.PublicTransportCableCar:
                        case ItemClass.SubService.PublicTransportMonorail:
                        case ItemClass.SubService.PublicTransportTours:
                        case ItemClass.SubService.PublicTransportPost:
                            if (budget == 0)
                            {
                                DebugLog.LogToFileOnly("Error:  PublicTransport Budget = 0");
                            }
                            int allWorkCount = 0;
                            if (rand.Next(100) == 0)
                            {
                                allWorkCount = TotalWorkCount((ushort)workBuilding, buildingData, false, true);
                            }
                            else
                            {
                                allWorkCount = TotalWorkCount((ushort)workBuilding, buildingData, false, false);
                            }
                            if (totalWorkCount > allWorkCount)
                            {
                                Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenId].SetWorkplace(citizenId, 0, 0u);
                                allWorkCount = TotalWorkCount((ushort)workBuilding, buildingData, false, true);
                            }
                            float idex = (totalWorkCount != 0) ? (allWorkCount / totalWorkCount) : 1;
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenId].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + MainDataStore.govermentEducation0Salary; break;
                                case Citizen.Education.OneSchool:
                                    num = num + MainDataStore.govermentEducation1Salary; break;
                                case Citizen.Education.TwoSchools:
                                    num = num + MainDataStore.govermentEducation2Salary; break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + MainDataStore.govermentEducation3Salary; break;
                            }
                            num = (int)(num * budget / 100f);
                            if (!checkOnly)
                            {
                                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)(num * idex * MainDataStore.gameExpenseDivide), Singleton<BuildingManager>.instance.m_buildings.m_buffer[workBuilding].Info.m_class);
                            }
                            break; //
                        default: break;
                    }

                    switch (buildingData.Info.m_class.m_service)
                    {
                        case ItemClass.Service.Office:
                            if (MainDataStore.building_money[workBuilding] > 0 && totalWorkCount != 0)
                            {
                                num = (int)(MainDataStore.building_money[workBuilding] / totalWorkCount);
                            }
                            break;
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
                            if (budget == 0)
                            {
                                DebugLog.LogToFileOnly("Error:  Playerbuilding Budget = 0");
                            }

                            int allWorkCount = 0;
                            if (rand.Next(100) == 0)
                            {
                                allWorkCount = TotalWorkCount((ushort)workBuilding, buildingData, false, true);
                            }
                            else
                            {
                                allWorkCount = TotalWorkCount((ushort)workBuilding, buildingData, false, false);
                            }

                            float idex = (totalWorkCount != 0) ? (allWorkCount / totalWorkCount) : 1;
                            if (totalWorkCount > allWorkCount)
                            {
                                Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenId].SetWorkplace(citizenId, 0, 0u);
                                idex = 1;
                            }
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenId].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + MainDataStore.govermentEducation0Salary; break;
                                case Citizen.Education.OneSchool:
                                    num = num + MainDataStore.govermentEducation1Salary; break;
                                case Citizen.Education.TwoSchools:
                                    num = num + MainDataStore.govermentEducation2Salary; break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + MainDataStore.govermentEducation3Salary; break;
                            }
                            num = (int)(num * budget / 100f);
                            if (!checkOnly)
                            {
                                //DebugLog.LogToFileOnly(Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.ToString() + Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].m_flags.ToString());
                                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)(num * idex * MainDataStore.gameExpenseDivide), Singleton<BuildingManager>.instance.m_buildings.m_buffer[workBuilding].Info.m_class);
                            }
                            break; //
                        default:
                            break;
                    }

                    if (buildingData.Info.m_class.m_service == ItemClass.Service.PlayerIndustry)
                    {
                        switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenId].EducationLevel)
                        {
                            case Citizen.Education.Uneducated:
                                num = num + MainDataStore.govermentEducation0Salary; break;
                            case Citizen.Education.OneSchool:
                                num = num + MainDataStore.govermentEducation1Salary; break;
                            case Citizen.Education.TwoSchools:
                                num = num + MainDataStore.govermentEducation2Salary; break;
                            case Citizen.Education.ThreeSchools:
                                num = num + MainDataStore.govermentEducation3Salary; break;
                        }
                        if (budget == 0)
                        {
                            DebugLog.LogToFileOnly("Error:  PlayerIndustry Budget = 0");
                        }

                        int allWorkCount = 0;
                        if (rand.Next(100) == 0)
                        {
                            allWorkCount = TotalWorkCount((ushort)workBuilding, buildingData, false, true);
                        }
                        else
                        {
                            allWorkCount = TotalWorkCount((ushort)workBuilding, buildingData, false, false);
                        }
                        if (totalWorkCount > allWorkCount)
                        {
                            Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenId].SetWorkplace(citizenId, 0, 0u);
                            //DebugLog.LogToFileOnly("Error, find TotalWorkCount > allWorkCount building = " + buildingData.Info.m_buildingAI.ToString());
                        }
                        float idex = (totalWorkCount != 0) ? (allWorkCount / totalWorkCount) : 1;
                        num = (int)(num * budget / 100f);
                        if (!checkOnly)
                        {
                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)(num * idex * MainDataStore.gameExpenseDivide), Singleton<BuildingManager>.instance.m_buildings.m_buffer[workBuilding].Info.m_class);
                        }
                    }

                    if (buildingData.Info.m_class.m_service == ItemClass.Service.Commercial || buildingData.Info.m_class.m_service == ItemClass.Service.Industrial || buildingData.Info.m_class.m_service == ItemClass.Service.Office)
                    {
                        MainDataStore.building_money[workBuilding] -= num;
                    }
                }
            }
            return num;
        }//public


        public static int TotalWorkCount(ushort buildingID, Building data, bool checkOnly, bool update)
        {
            int num = 0;


            if (MainDataStore.buildingFlag[buildingID] && !update)
            {
                num = MainDataStore.building_buffer1[buildingID];
            }
            else
            {
                if (data.Info.m_buildingAI is LandfillSiteAI)
                {
                    LandfillSiteAI buildingAI = data.Info.m_buildingAI as LandfillSiteAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is ExtractingFacilityAI)
                {
                    ExtractingFacilityAI buildingAI = data.Info.m_buildingAI as ExtractingFacilityAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is ProcessingFacilityAI)
                {
                    ProcessingFacilityAI buildingAI = data.Info.m_buildingAI as ProcessingFacilityAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is PoliceStationAI)
                {
                    PoliceStationAI buildingAI = data.Info.m_buildingAI as PoliceStationAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is FireStationAI)
                {
                    FireStationAI buildingAI = data.Info.m_buildingAI as FireStationAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is HospitalAI)
                {
                    HospitalAI buildingAI = data.Info.m_buildingAI as HospitalAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is CargoStationAI)
                {
                    CargoStationAI buildingAI = data.Info.m_buildingAI as CargoStationAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is TransportStationAI)
                {
                    TransportStationAI buildingAI = data.Info.m_buildingAI as TransportStationAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is CemeteryAI)
                {
                    CemeteryAI buildingAI = data.Info.m_buildingAI as CemeteryAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is MedicalCenterAI)
                {
                    MedicalCenterAI buildingAI = data.Info.m_buildingAI as MedicalCenterAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is MonumentAI)
                {
                    MonumentAI buildingAI = data.Info.m_buildingAI as MonumentAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is DepotAI)
                {
                    DepotAI buildingAI = data.Info.m_buildingAI as DepotAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is HelicopterDepotAI)
                {
                    HelicopterDepotAI buildingAI = data.Info.m_buildingAI as HelicopterDepotAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is MaintenanceDepotAI)
                {
                    MaintenanceDepotAI buildingAI = data.Info.m_buildingAI as MaintenanceDepotAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is FirewatchTowerAI)
                {
                    FirewatchTowerAI buildingAI = data.Info.m_buildingAI as FirewatchTowerAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is DoomsdayVaultAI)
                {
                    DoomsdayVaultAI buildingAI = data.Info.m_buildingAI as DoomsdayVaultAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is DisasterResponseBuildingAI)
                {
                    DisasterResponseBuildingAI buildingAI = data.Info.m_buildingAI as DisasterResponseBuildingAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is HadronColliderAI)
                {
                    HadronColliderAI buildingAI = data.Info.m_buildingAI as HadronColliderAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is SchoolAI)
                {
                    SchoolAI buildingAI = data.Info.m_buildingAI as SchoolAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is PowerPlantAI)
                {
                    PowerPlantAI buildingAI = data.Info.m_buildingAI as PowerPlantAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is SnowDumpAI)
                {
                    SnowDumpAI buildingAI = data.Info.m_buildingAI as SnowDumpAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is WarehouseAI)
                {
                    WarehouseAI buildingAI = data.Info.m_buildingAI as WarehouseAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is WaterFacilityAI)
                {
                    WaterFacilityAI buildingAI = data.Info.m_buildingAI as WaterFacilityAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is SaunaAI)
                {
                    SaunaAI buildingAI = data.Info.m_buildingAI as SaunaAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is PostOfficeAI)
                {
                    PostOfficeAI buildingAI = data.Info.m_buildingAI as PostOfficeAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is RadioMastAI)
                {
                    RadioMastAI buildingAI = data.Info.m_buildingAI as RadioMastAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is SpaceElevatorAI)
                {
                    SpaceElevatorAI buildingAI = data.Info.m_buildingAI as SpaceElevatorAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is SpaceRadarAI)
                {
                    SpaceRadarAI buildingAI = data.Info.m_buildingAI as SpaceRadarAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is MainIndustryBuildingAI)
                {
                    MainIndustryBuildingAI buildingAI = data.Info.m_buildingAI as MainIndustryBuildingAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is AuxiliaryBuildingAI)
                {
                    AuxiliaryBuildingAI buildingAI = data.Info.m_buildingAI as AuxiliaryBuildingAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is ShelterAI)
                {
                    ShelterAI buildingAI = data.Info.m_buildingAI as ShelterAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else if (data.Info.m_buildingAI is HeatingPlantAI)
                {
                    HeatingPlantAI buildingAI = data.Info.m_buildingAI as HeatingPlantAI;
                    num = buildingAI.m_workPlaceCount0 + buildingAI.m_workPlaceCount1 + buildingAI.m_workPlaceCount2 + buildingAI.m_workPlaceCount3;
                }
                else
                {
                    if (!checkOnly)
                    {
                        DebugLog.LogToFileOnly("Error: find unknow building = " + data.Info.m_buildingAI.ToString());
                    }
                }

                MainDataStore.buildingFlag[buildingID] = true;
                MainDataStore.building_buffer1[buildingID] = num;
            }
            return num;
        }
        //public void BugCheck(uint citizen)
        //{
            //DebugLog.LogToFileOnly("Error case1" + Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen].m_flags.ToString() + " " + Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen].m_homeBuilding.ToString());
            //DebugLog.LogToFileOnly("Error case2" + Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen].m_health.ToString());
         //   if ((Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen].m_flags & Citizen.Flags.Created) == Citizen.Flags.None)
         //   {
                //Singleton<CitizenManager>.instance.ReleaseCitizen(citizen);
        //        RealCityEconomyExtension.fixEmptyCitizenCount++;
        //    }
        //}

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
                        MainDataStore.family_money[homeID] += MainDataStore.citizen_money[data.m_citizen0];
                        //BugCheck(data.m_citizen0);
                    }
                }
                if (data.m_citizen1 != 0)
                {
                    Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen1];
                    if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                    {
                        citizenCount++;
                        MainDataStore.family_money[homeID] += MainDataStore.citizen_money[data.m_citizen1];
                        //BugCheck(data.m_citizen1);
                    }
                }
                if (data.m_citizen2 != 0)
                {
                    Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen2];
                    if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                    {
                        citizenCount++;
                        MainDataStore.family_money[homeID] += MainDataStore.citizen_money[data.m_citizen2];
                        //BugCheck(data.m_citizen2);
                    }
                }
                if (data.m_citizen3 != 0)
                {
                    Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen3];
                    if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                    {
                        citizenCount++;
                        MainDataStore.family_money[homeID] += MainDataStore.citizen_money[data.m_citizen3];
                        //BugCheck(data.m_citizen3);
                    }
                }
                if (data.m_citizen4 != 0)
                {
                    Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen4];
                    if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                    {
                        citizenCount++;
                        MainDataStore.family_money[homeID] += MainDataStore.citizen_money[data.m_citizen4];
                        //BugCheck(data.m_citizen4);
                    }
                }
            }
            else
            {

                if (MainDataStore.family_money[homeID] < 5000)
                {
                    familyWeightStableLow = (ushort)(familyWeightStableLow + 1);
                }
                else if (MainDataStore.family_money[homeID] >= 15000)
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
                        GetVoteChance(data.m_citizen0, citizenData, homeID);
                    }
                }
                if (data.m_citizen1 != 0)
                {
                    Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen1];
                    if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                    {
                        GetVoteChance(data.m_citizen1, citizenData, homeID);
                        temp++;
                    }
                }
                if (data.m_citizen2 != 0)
                {
                    Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen2];
                    if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                    {
                        GetVoteChance(data.m_citizen2, citizenData, homeID);
                        temp++;
                    }
                }
                if (data.m_citizen3 != 0)
                {
                    Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen3];
                    if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                    {
                        GetVoteChance(data.m_citizen3, citizenData, homeID);
                        temp++;
                    }
                }
                if (data.m_citizen4 != 0)
                {
                    Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen4];
                    if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                    {
                        GetVoteChance(data.m_citizen4, citizenData, homeID);
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
                            MainDataStore.citizen_money[data.m_citizen0] = MainDataStore.family_money[homeID] / (float)temp;
                        }
                    }
                    if (data.m_citizen1 != 0)
                    {
                        Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen1];
                        if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                        {
                            MainDataStore.citizen_money[data.m_citizen1] = MainDataStore.family_money[homeID] / (float)temp;
                        }
                    }
                    if (data.m_citizen2 != 0)
                    {
                        Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen2];
                        if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                        {
                            MainDataStore.citizen_money[data.m_citizen2] = MainDataStore.family_money[homeID] / (float)temp;
                        }
                    }
                    if (data.m_citizen3 != 0)
                    {
                        Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen3];
                        if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                        {
                            MainDataStore.citizen_money[data.m_citizen3] = MainDataStore.family_money[homeID] / (float)temp;
                        }
                    }
                    if (data.m_citizen4 != 0)
                    {
                        Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[data.m_citizen4];
                        if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false))
                        {
                            MainDataStore.citizen_money[data.m_citizen4] = MainDataStore.family_money[homeID] / (float)temp;
                        }
                    }
                }
            }

        }


        public byte ProcessFamily(uint homeID, ref CitizenUnit data)
        {
            //DebugLog.LogToFileOnly("we go in now, pc_ResidentAI");
            if (preCitizenId > homeID)
            {
                //DebugLog.LogToFileOnly("process once");
                //citizen_process_done = true;
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
            else
            {
                //citizen_process_done = false;
            }

            familyCount++;
            citizenGoodsTemp += data.m_goods;

            if (homeID > 524288)
            {
                DebugLog.LogToFileOnly("Error: citizen ID greater than 524288");
            }

            ProcessCitizen(homeID, ref data, true);

            //here we caculate citizen income
            int tempNum;
            tempNum = CitizenSalary(data.m_citizen0, false);
            //DebugLog.LogToFileOnly("in ResidentAI salary = " + temp_num.ToString());
            tempNum = tempNum + CitizenSalary(data.m_citizen1, false);
            //DebugLog.LogToFileOnly("in ResidentAI salary = " + temp_num.ToString());
            tempNum = tempNum + CitizenSalary(data.m_citizen2, false);
            //DebugLog.LogToFileOnly("in ResidentAI salary = " + temp_num.ToString());
            tempNum = tempNum + CitizenSalary(data.m_citizen3, false);
            //DebugLog.LogToFileOnly("in ResidentAI salary = " + temp_num.ToString());
            tempNum = tempNum + CitizenSalary(data.m_citizen4, false);
            //DebugLog.LogToFileOnly("in ResidentAI salary = " + temp_num.ToString());
            //DebugLog.LogToFileOnly("Citzen " + homeID.ToString() + "salary is " + temp_num.ToString());
            citizenSalaryCount = citizenSalaryCount + tempNum;
            int citizenSalaryCurrent = tempNum;
            tempNum = 0;

            if (data.m_citizen0 != 0u)
            {
                tempNum++;
            }
            if (data.m_citizen1 != 0u)
            {
                tempNum++;
            }

            if (data.m_citizen2 != 0u)
            {
                tempNum++;
            }

            if (data.m_citizen3 != 0u)
            {
                tempNum++;
            }

            if (data.m_citizen4 != 0u)
            {
                tempNum++;
            }
            //caculate tax
            float salaryPerFamilyMember;
            if (tempNum != 0)
            {
                salaryPerFamilyMember = (float)citizenSalaryCurrent / tempNum;
            }
            else
            {
                salaryPerFamilyMember = 0;
                DebugLog.LogToFileOnly("tempNum == 0 in ResidentAI");
            }

            if (citizenSalaryCurrent < 0)
            {
                DebugLog.LogToFileOnly("citizenSalaryCurrent< 0 in ResidentAI");
                citizenSalaryCurrent = 0;
            }


            //tax

            float tax = (float)Politics.residentTax * (float)citizenSalaryCurrent / 100f;

            tempCitizenSalaryTaxTotal = tempCitizenSalaryTaxTotal + (int)tax;
            citizenSalaryTaxTotal = (int)tempCitizenSalaryTaxTotal;
            ProcessCitizenIncomeTax(homeID, tax);
            
            //here we caculate expense
            tempNum = 0;
            int expenserate = 0;
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint num3 = 0u;
            int num4 = 0;
            if (data.m_citizen4 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen4)].Dead)
            {
                num4++;
                num3 = data.m_citizen4;
                expenserate = 0;
                tempNum += GetExpenseRate(homeID, data.m_citizen4, out expenserate);
            }
            if (data.m_citizen3 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen3)].Dead)
            {
                num4++;
                num3 = data.m_citizen3;
                expenserate = 0;
                tempNum += GetExpenseRate(homeID, data.m_citizen3, out expenserate);
            }
            if (data.m_citizen2 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen2)].Dead)
            {
                num4++;
                num3 = data.m_citizen2;
                expenserate = 0;
                tempNum += GetExpenseRate(homeID, data.m_citizen2, out expenserate);
            }
            if (data.m_citizen1 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen1)].Dead)
            {
                num4++;
                num3 = data.m_citizen1;
                expenserate = 0;
                tempNum += GetExpenseRate(homeID, data.m_citizen1, out expenserate);
            }
            if (data.m_citizen0 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen0)].Dead)
            {
                num4++;
                num3 = data.m_citizen0;
                expenserate = 0;
                tempNum += GetExpenseRate(homeID, data.m_citizen0, out expenserate);
            }

            //temp = education&sick   expenserate = house rent(one family)
            ProcessCitizenHouseRent(homeID, expenserate);
            citizenExpenseCount = citizenExpenseCount + tempNum + expenserate;

            //DebugLog.LogToFileOnly("temp_num = " + temp_num.ToString());
            //DebugLog.LogToFileOnly("expenserate = " + expenserate.ToString());

            //income - expense
            tempNum = citizenSalaryCurrent - (int)(tax) - tempNum - expenserate;// - comm_data.citizen_average_transport_fee;
            MainDataStore.family_money[homeID] = (float)(MainDataStore.family_money[homeID] + tempNum);

            //process shopping
            if (MainDataStore.familyGoods[homeID] == 0)
            {
                //first time
            }
            else if (MainDataStore.familyGoods[homeID] < data.m_goods)
            {
                //DebugLog.LogToFileOnly("find shopping num= " + (data.m_goods - MainDataStore.familyGoods[homeID]).ToString());
                //DebugLog.LogToFileOnly("data.m_goods= " + data.m_goods.ToString());
                //DebugLog.LogToFileOnly("familyGoods num= " + MainDataStore.familyGoods[homeID].ToString());
                MainDataStore.family_money[homeID] -= RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Shopping) * (data.m_goods - MainDataStore.familyGoods[homeID]);
            }
            //process citizen status
            if (tempNum <= 20)
            {
                familyLossMoneyCount = (uint)(familyLossMoneyCount + 1);
            }
            else if (tempNum > 70)
            {
                familyVeryProfitMoneyCount = (uint)(familyVeryProfitMoneyCount + 1);
            }
            else
            {
                familyProfitMoneyCount = (uint)(familyProfitMoneyCount + 1);
            }

            if (MainDataStore.family_money[homeID] < -Politics.benefitOffset)
            {
                int num = (int)(-(MainDataStore.family_money[homeID]) + 0.5f + Politics.benefitOffset);
                MainDataStore.family_money[homeID] = 0;
                MainDataStore.minimumLivingAllowance += (num / 100);
                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, num, ItemClass.Service.Residential, ItemClass.SubService.None, ItemClass.Level.Level1);
            } else
            {
                if (Politics.benefitOffset > 0)
                {
                    MainDataStore.family_money[homeID] += Politics.benefitOffset;
                    MainDataStore.minimumLivingAllowance += (Politics.benefitOffset / 100);
                    Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, Politics.benefitOffset, ItemClass.Service.Residential, ItemClass.SubService.None, ItemClass.Level.Level1);
                }
            }


            if (MainDataStore.family_money[homeID] > 32000000f)
            {
                MainDataStore.family_money[homeID] = 32000000f;
            }

            if (MainDataStore.family_money[homeID] < -32000000f)
            {
                MainDataStore.family_money[homeID] = -32000000f;
            }


            preCitizenId = homeID;
            ProcessCitizen(homeID, ref data, false);


            tempNum = tempNum / 10;
            if (MainDataStore.family_money[homeID] > 0)
            {
                tempNum += (int)(MainDataStore.family_money[homeID] / 5000);
            }

            if (tempNum < 1)
            {
                tempNum = 1;
            } else if (tempNum > 20)
            {
                tempNum = 20;
            }
            return (byte)tempNum;
            //return to original game code.
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


        public static void SetNum(ref int[] source, ref int[] array)
        {
            System.Random rd = new System.Random();
            int range = array.Length;
            for (int i = 0; i < array.Length; i++)
            {
                //随机产生一个位置  
                int pos = rd.Next(range);
                //获取该位置的值  
                array[i] = source[pos];
                //改良：将最后一个数赋给被删除的索引所对应的值  
                source[pos] = source[range - 1];
                range--;
            }
        }

        public static ushort FindNotSoCloseBuilding(Vector3 pos, float maxDistance, ItemClass.Service service, ItemClass.SubService subService, Building.Flags flagsRequired, Building.Flags flagsForbidden)
        {
            int num = Mathf.Max((int)((pos.x - maxDistance) / 64f + 135f), 0);
            int num2 = Mathf.Max((int)((pos.z - maxDistance) / 64f + 135f), 0);
            int num3 = Mathf.Min((int)((pos.x + maxDistance) / 64f + 135f), 269);
            int num4 = Mathf.Min((int)((pos.z + maxDistance) / 64f + 135f), 269);
            ushort result = 0;
            float num5 = maxDistance * maxDistance;
            BuildingManager building = Singleton<BuildingManager>.instance;
            SimulationManager instance2 = Singleton<SimulationManager>.instance;
            System.Random rd = new System.Random();
            ushort[] tempBuilding = new ushort[16];
            for (int i = 0; i < 16; i++)
            {
                tempBuilding[i] = 0;
            }

            if ((num4 >= num2) && (num3 >= num))
            {
                int[] source = new int[(num4 - num2 + 1) * (num3 - num + 1)];
                int[] array = new int[(num4 - num2 + 1) * (num3 - num + 1)];
                int idex = 0;
                for (int i = num2; i <= num4; i++)
                {
                    for (int j = num; j <= num3; j++)
                    {
                        source[idex] = i * 270 + j;
                        idex++;
                    }
                }

                SetNum(ref source, ref array);
                idex = 0;

                for (int i = 0; i <= num4 - num2; i++)
                {
                    for (int j = 0; j <= num3 - num; j++)
                    {
                        ushort num6 = building.m_buildingGrid[array[idex]];
                        idex++;
                        int num7 = 0;
                        int tempBuildingIdex = 0;
                        for (int z = 0; z < 16; z++)
                        {
                            tempBuilding[z] = 0;
                        }
                        while (num6 != 0)
                        {
                            BuildingInfo info = building.m_buildings.m_buffer[(int)num6].Info;
                            if ((info.m_class.m_service == service || service == ItemClass.Service.None) && (info.m_class.m_subService == subService || subService == ItemClass.SubService.None))
                            {
                                Building.Flags flags = building.m_buildings.m_buffer[(int)num6].m_flags;
                                if (info.m_class.m_service == ItemClass.Service.Commercial)
                                {
                                    if ((flags & (flagsRequired | flagsForbidden)) == flagsRequired)
                                    {
                                        if (building.m_buildings.m_buffer[(int)num6].m_customBuffer2 > 1000)
                                        {
                                            //float num8 = Vector3.SqrMagnitude(pos - building.m_buildings.m_buffer[(int)num6].m_position);
                                            //result = num6;
                                            //return result;
                                            tempBuilding[tempBuildingIdex] = num6;
                                            tempBuildingIdex++;
                                        }
                                    }
                                }
                            }
                            num6 = building.m_buildings.m_buffer[(int)num6].m_nextGridBuilding;
                            if (++num7 >= 49152)
                            {
                                CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                                break;
                            }
                            if (((num6 == 0) && (tempBuildingIdex != 0)) || (tempBuildingIdex == 16))
                            {
                                //DebugLog.LogToFileOnly("find comm building num = " + tempBuildingIdex.ToString());
                                num6 = tempBuilding[rd.Next(tempBuildingIdex)];
                                return num6;
                            }
                        }
                    }
                }
            }
            return result;
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

            //new add begin
            int temp_num = 0;
            if ((Singleton<BuildingManager>.instance.m_buildings.m_buffer[data.m_building].m_flags & (Building.Flags.Completed | Building.Flags.Upgrading)) != Building.Flags.None)
            {
                temp_num = ProcessFamily(homeID, ref data);
            }


            data.m_goods = (ushort)Mathf.Max(1, (int)(data.m_goods - temp_num)); //here we can adjust demand

            MainDataStore.familyGoods[homeID] = data.m_goods;

            //new add end
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

        public int GetExpenseRate(uint homeid, uint citizen_id, out int incomeAccumulation)
        {
            BuildingManager instance1 = Singleton<BuildingManager>.instance;
            CitizenManager instance2 = Singleton<CitizenManager>.instance;
            ItemClass @class = instance1.m_buildings.m_buffer[instance2.m_citizens.m_buffer[citizen_id].m_homeBuilding].Info.m_class;
            incomeAccumulation = 0;
            DistrictManager instance = Singleton<DistrictManager>.instance;
            if (instance2.m_citizens.m_buffer[citizen_id].m_homeBuilding != 0)
            {
                byte district = instance.GetDistrict(instance1.m_buildings.m_buffer[instance2.m_citizens.m_buffer[citizen_id].m_homeBuilding].m_position);
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

            int temp = 0;
            if ((Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_flags & Citizen.Flags.Student) != Citizen.Flags.None)
            {
                if (MainDataStore.family_money[homeid] > 0 && (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_flags.IsFlagSet(Citizen.Flags.Education2)))
                {
                    temp = temp + 20;
                    Singleton<EconomyManager>.instance.AddPrivateIncome(20, ItemClass.Service.Education, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                }

                if (MainDataStore.family_money[homeid] > 0 && (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_flags.IsFlagSet(Citizen.Flags.Education1)))
                {
                    temp = temp + 10;
                    Singleton<EconomyManager>.instance.AddPrivateIncome(10, ItemClass.Service.Education, ItemClass.SubService.None, ItemClass.Level.Level2, 115);
                }

                if (MainDataStore.family_money[homeid] > 0)
                {
                    if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_flags.IsFlagSet(Citizen.Flags.Education1))
                    {

                    }
                    else if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_flags.IsFlagSet(Citizen.Flags.Education2))
                    {

                    }
                    else if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_flags.IsFlagSet(Citizen.Flags.Education3))
                    {

                    }
                    else
                    {
                        temp = temp + 5;
                        Singleton<EconomyManager>.instance.AddPrivateIncome(5, ItemClass.Service.Education, ItemClass.SubService.None, ItemClass.Level.Level2, 115);
                    }
                }

            }
            return temp;
        }


        public void GetVoteTickets()
        {
            System.Random rand = new System.Random();
            if (Politics.cPartyChance + Politics.gPartyChance + Politics.sPartyChance + Politics.lPartyChance + Politics.nPartyChance != (800 + RealCityEconomyExtension.partyTrendStrength))
            {
                if (rand.Next(64) <= 1)
                {
                    DebugLog.LogToFileOnly("error, change is not equal 800 " + (Politics.cPartyChance + Politics.gPartyChance + Politics.sPartyChance + Politics.lPartyChance + Politics.nPartyChance).ToString());
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

            Politics.cPartySeatsPolls += ((float)Politics.cPartyChance / (800f + RealCityEconomyExtension.partyTrendStrength));
            Politics.gPartySeatsPolls += ((float)Politics.gPartyChance / (800f + RealCityEconomyExtension.partyTrendStrength));
            Politics.sPartySeatsPolls += ((float)Politics.sPartyChance / (800f + RealCityEconomyExtension.partyTrendStrength));
            Politics.lPartySeatsPolls += ((float)Politics.lPartyChance / (800f + RealCityEconomyExtension.partyTrendStrength));
            Politics.nPartySeatsPolls += ((float)Politics.nPartyChance / (800f + RealCityEconomyExtension.partyTrendStrength));
        }

        public void GetVoteChance(uint citizenID, Citizen citizen, uint homeID)
        {
            if ((int)Citizen.GetAgeGroup(citizen.m_age) >= 2)
            {
                System.Random rand = new System.Random();
                if (((Politics.parliamentCount != 1) && (rand.Next(10) == 0)) || (Politics.parliamentCount == 1))
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
                    else if (MainDataStore.family_money[homeID] >= 15000)
                    {
                        idex = 2;
                    }
                    else
                    {
                        idex = 1;
                    }

                    if (idex < 0 || idex > 3)
                    {
                        DebugLog.LogToFileOnly("Error money idex" + idex.ToString());
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
                        DebugLog.LogToFileOnly("Error partyTrend" + RealCityEconomyExtension.partyTrend.ToString());
                    }

                    GetVoteTickets();
                }
            }
        }
    }
}
