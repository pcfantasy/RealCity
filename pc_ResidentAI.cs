using System;
using ColossalFramework;
using UnityEngine;
using ColossalFramework.Math;
using System.Reflection;
using System.Text.RegularExpressions;

namespace RealCity
{
    public class pc_ResidentAI : ResidentAI
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
            preCitizenId = saveandrestore.load_uint(ref i, saveData);
            familyCount = saveandrestore.load_int(ref i, saveData);
            familyVeryProfitMoneyCount = saveandrestore.load_uint(ref i, saveData);
            familyProfitMoneyCount = saveandrestore.load_uint(ref i, saveData);
            familyLossMoneyCount = saveandrestore.load_uint(ref i, saveData);
            citizenSalaryCount = saveandrestore.load_int(ref i, saveData);
            citizenExpenseCount = saveandrestore.load_int(ref i, saveData);
            citizenSalaryTaxTotal = saveandrestore.load_int(ref i, saveData);
            tempCitizenSalaryTaxTotal = saveandrestore.load_float(ref i, saveData);

            Road = saveandrestore.load_int(ref i, saveData);
            Electricity = saveandrestore.load_int(ref i, saveData);
            Water = saveandrestore.load_int(ref i, saveData);
            Beautification = saveandrestore.load_int(ref i, saveData);
            Garbage = saveandrestore.load_int(ref i, saveData);
            HealthCare = saveandrestore.load_int(ref i, saveData);
            PoliceDepartment = saveandrestore.load_int(ref i, saveData);
            Education = saveandrestore.load_int(ref i, saveData);
            Monument = saveandrestore.load_int(ref i, saveData);
            FireDepartment = saveandrestore.load_int(ref i, saveData);
            PublicTransport_bus = saveandrestore.load_int(ref i, saveData);
            PublicTransport_tram = saveandrestore.load_int(ref i, saveData);
            PublicTransport_ship = saveandrestore.load_int(ref i, saveData);
            PublicTransport_plane = saveandrestore.load_int(ref i, saveData);
            PublicTransport_metro = saveandrestore.load_int(ref i, saveData);
            PublicTransport_train = saveandrestore.load_int(ref i, saveData);
            PublicTransport_taxi = saveandrestore.load_int(ref i, saveData);
            PublicTransport_cablecar = saveandrestore.load_int(ref i, saveData);
            PublicTransport_monorail = saveandrestore.load_int(ref i, saveData);
            Disaster = saveandrestore.load_int(ref i, saveData);

            familyWeightStableHigh = saveandrestore.load_uint(ref i, saveData);
            familyWeightStableLow = saveandrestore.load_uint(ref i, saveData);

            citizenGoods = saveandrestore.load_long(ref i, saveData);
            citizenGoodsTemp = saveandrestore.load_long(ref i, saveData);
            citizenCount = saveandrestore.load_int(ref i, saveData);

            DebugLog.LogToFileOnly("saveData in residentAI is " + i.ToString());
        }

        public static void Save()
        {
            int i = 0;

            //2*4 + 3*4 + 4*4 = 36
            saveandrestore.save_uint(ref i, preCitizenId, ref saveData);
            saveandrestore.save_int(ref i, familyCount, ref saveData);
            saveandrestore.save_uint(ref i, familyVeryProfitMoneyCount, ref saveData);
            saveandrestore.save_uint(ref i, familyProfitMoneyCount, ref saveData);
            saveandrestore.save_uint(ref i, familyLossMoneyCount, ref saveData);
            saveandrestore.save_int(ref i, citizenSalaryCount, ref saveData);
            saveandrestore.save_int(ref i, citizenExpenseCount, ref saveData);
            saveandrestore.save_int(ref i, citizenSalaryTaxTotal, ref saveData);
            saveandrestore.save_float(ref i, tempCitizenSalaryTaxTotal, ref saveData);

            //20 * 4 = 80
            saveandrestore.save_int(ref i, Road, ref saveData);
            saveandrestore.save_int(ref i, Electricity, ref saveData);
            saveandrestore.save_int(ref i, Water, ref saveData);
            saveandrestore.save_int(ref i, Beautification, ref saveData);
            saveandrestore.save_int(ref i, Garbage, ref saveData);
            saveandrestore.save_int(ref i, HealthCare, ref saveData);
            saveandrestore.save_int(ref i, PoliceDepartment, ref saveData);
            saveandrestore.save_int(ref i, Education, ref saveData);
            saveandrestore.save_int(ref i, Monument, ref saveData);
            saveandrestore.save_int(ref i, FireDepartment, ref saveData);
            saveandrestore.save_int(ref i, PublicTransport_bus, ref saveData);
            saveandrestore.save_int(ref i, PublicTransport_tram, ref saveData);
            saveandrestore.save_int(ref i, PublicTransport_ship, ref saveData);
            saveandrestore.save_int(ref i, PublicTransport_plane, ref saveData);
            saveandrestore.save_int(ref i, PublicTransport_metro, ref saveData);
            saveandrestore.save_int(ref i, PublicTransport_train, ref saveData);
            saveandrestore.save_int(ref i, PublicTransport_taxi, ref saveData);
            saveandrestore.save_int(ref i, PublicTransport_cablecar, ref saveData);
            saveandrestore.save_int(ref i, PublicTransport_monorail, ref saveData);
            saveandrestore.save_int(ref i, Disaster, ref saveData);

            //8
            saveandrestore.save_uint(ref i, familyWeightStableHigh, ref saveData);
            saveandrestore.save_uint(ref i, familyWeightStableLow, ref saveData);

            //16
            saveandrestore.save_long(ref i, citizenGoods, ref saveData);
            saveandrestore.save_long(ref i, citizenGoodsTemp, ref saveData);
            saveandrestore.save_int(ref i, citizenCount, ref saveData);

            DebugLog.LogToFileOnly("(save)save_data in residentAI is " + i.ToString());
        }

        public static int CitizenSalary(uint citizenId, bool checkOnly)
        {
            int num = 0;
            System.Random rand = new System.Random();
            //Array16<Building> buildings = Singleton<BuildingManager>.instance.m_buildings;
            if (citizenId != 0u)
            {
                Citizen.Flags temp_flag = Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenId].m_flags;
                if ((temp_flag & Citizen.Flags.Student) != Citizen.Flags.None)
                {
                    return num;
                }
                int budget = 0;
                int aliveWorkCount = 0;
                int totalWorkCount = 0;
                Citizen.BehaviourData behaviour = default(Citizen.BehaviourData);
                int workBuilding = Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenId].m_workBuilding;
                if (checkOnly)
                {
                    DebugLog.LogToFileOnly("comm_data.building_money[workBuilding]:" + comm_data.building_money[workBuilding].ToString());
                    DebugLog.LogToFileOnly(Singleton<BuildingManager>.instance.m_buildings.m_buffer[workBuilding].Info.m_class.ToString());
                }
                if (workBuilding != 0u)
                {
                    budget = Singleton<EconomyManager>.instance.GetBudget(Singleton<BuildingManager>.instance.m_buildings.m_buffer[workBuilding].Info.m_class);
                    BuildingUI.GetWorkBehaviour((ushort)workBuilding, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[workBuilding], ref behaviour, ref aliveWorkCount, ref totalWorkCount);
                    switch (Singleton<BuildingManager>.instance.m_buildings.m_buffer[workBuilding].Info.m_class.m_subService)
                    {
                        case ItemClass.SubService.CommercialHigh:
                            if (comm_data.building_money[workBuilding] > 0 && totalWorkCount != 0)
                            {
                                switch (Singleton<BuildingManager>.instance.m_buildings.m_buffer[workBuilding].Info.m_class.m_level)
                                {
                                    case ItemClass.Level.Level1:
                                        num = (int)(comm_data.building_money[workBuilding] * 0.2f / totalWorkCount);
                                        break;
                                    case ItemClass.Level.Level2:
                                        num = (int)(comm_data.building_money[workBuilding] * 0.4f / totalWorkCount);
                                        break;
                                    case ItemClass.Level.Level3:
                                        num = (int)(comm_data.building_money[workBuilding] * 0.7f / totalWorkCount);
                                        break;
                                }
                            }
                            break; //
                        case ItemClass.SubService.CommercialLow:
                            if (comm_data.building_money[workBuilding] > 0 && totalWorkCount != 0)
                            {
                                switch (Singleton<BuildingManager>.instance.m_buildings.m_buffer[workBuilding].Info.m_class.m_level)
                                {
                                    case ItemClass.Level.Level1:
                                        num = (int)(comm_data.building_money[workBuilding] * 0.1f / totalWorkCount);
                                        break;
                                    case ItemClass.Level.Level2:
                                        num = (int)(comm_data.building_money[workBuilding] * 0.3f / totalWorkCount);
                                        break;
                                    case ItemClass.Level.Level3:
                                        num = (int)(comm_data.building_money[workBuilding] * 0.6f / totalWorkCount);
                                        break;
                                }
                            }
                            break; //
                        case ItemClass.SubService.IndustrialGeneric:
                            if (comm_data.building_money[workBuilding] > 0 && totalWorkCount != 0)
                            {
                                switch (Singleton<BuildingManager>.instance.m_buildings.m_buffer[workBuilding].Info.m_class.m_level)
                                {
                                    case ItemClass.Level.Level1:
                                        num = (int)(comm_data.building_money[workBuilding] * 0.1f / totalWorkCount);
                                        break;
                                    case ItemClass.Level.Level2:
                                        num = (int)(comm_data.building_money[workBuilding] * 0.2f / totalWorkCount);
                                        break;
                                    case ItemClass.Level.Level3:
                                        num = (int)(comm_data.building_money[workBuilding] * 0.3f / totalWorkCount);
                                        break;
                                }
                            }
                            break; //
                        case ItemClass.SubService.IndustrialFarming:
                            if (comm_data.building_money[workBuilding] > 0 && totalWorkCount != 0)
                            {
                                num = (int)(comm_data.building_money[workBuilding] * 0.2f / totalWorkCount);
                            }
                            break; //
                        case ItemClass.SubService.IndustrialForestry:
                            if (comm_data.building_money[workBuilding] > 0 && totalWorkCount != 0)
                            {
                                num = (int)(comm_data.building_money[workBuilding] * 0.2f / totalWorkCount);
                            }
                            break; //
                        case ItemClass.SubService.IndustrialOil:
                            if (comm_data.building_money[workBuilding] > 0 && totalWorkCount != 0)
                            {
                                num = (int)(comm_data.building_money[workBuilding] * 0.2f / totalWorkCount);
                            }
                            break; //
                        case ItemClass.SubService.IndustrialOre:
                            if (comm_data.building_money[workBuilding] > 0 && totalWorkCount != 0)
                            {
                                num = (int)(comm_data.building_money[workBuilding] * 0.2f / totalWorkCount);
                            }
                            break;
                        case ItemClass.SubService.CommercialLeisure:
                            if (comm_data.building_money[workBuilding] > 0 && totalWorkCount != 0)
                            {
                                num = (int)(comm_data.building_money[workBuilding] * 0.7f / totalWorkCount);
                            }
                            break;
                        case ItemClass.SubService.CommercialTourist:
                            if (comm_data.building_money[workBuilding] > 0 && totalWorkCount != 0)
                            {
                                num = (int)(comm_data.building_money[workBuilding] * 0.9f / totalWorkCount);
                            }
                            break;
                        case ItemClass.SubService.CommercialEco:
                            if (comm_data.building_money[workBuilding] > 0 && totalWorkCount != 0)
                            {
                                num = (int)(comm_data.building_money[workBuilding] * 0.5f / totalWorkCount);
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
                            if (budget == 0)
                            {
                                DebugLog.LogToFileOnly("Error:  playerbuilding budget9 = 0");
                            }
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenId].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.goverment_education0) + rand.Next(1); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.goverment_education1) + rand.Next(2); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.goverment_education2) + rand.Next(3); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.goverment_education3) + rand.Next(4); break;
                            }
                            if (!checkOnly)
                            {
                                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)(num * budget * comm_data.game_expense_divide / 100f), Singleton<BuildingManager>.instance.m_buildings.m_buffer[workBuilding].Info.m_class);
                            }
                            break; //
                        default: break;
                    }

                    switch (Singleton<BuildingManager>.instance.m_buildings.m_buffer[workBuilding].Info.m_class.m_service)
                    {
                        case ItemClass.Service.Office:
                            if (comm_data.building_money[workBuilding] > 0 && totalWorkCount != 0)
                            {
                                num = (int)(comm_data.building_money[workBuilding] / totalWorkCount);
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
                                DebugLog.LogToFileOnly("Error:  playerbuilding budget20 = 0");
                            }
                            switch (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenId].EducationLevel)
                            {
                                case Citizen.Education.Uneducated:
                                    num = num + (int)(comm_data.goverment_education0) + rand.Next(1); break;
                                case Citizen.Education.OneSchool:
                                    num = num + (int)(comm_data.goverment_education1) + rand.Next(2); break;
                                case Citizen.Education.TwoSchools:
                                    num = num + (int)(comm_data.goverment_education2) + rand.Next(3); break;
                                case Citizen.Education.ThreeSchools:
                                    num = num + (int)(comm_data.goverment_education3) + rand.Next(4); break;
                            }
                            if (!checkOnly)
                            {
                                //DebugLog.LogToFileOnly(Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.ToString() + Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].m_flags.ToString());
                                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)(num * budget * comm_data.game_expense_divide / 100f), Singleton<BuildingManager>.instance.m_buildings.m_buffer[workBuilding].Info.m_class);
                            }
                            break; //
                        default:
                            break;
                    }
                    if (!checkOnly)
                    {
                        comm_data.building_money[workBuilding] -= num;
                    }
                    //DebugLog.LogToFileOnly("salary4 is " + num.ToString());
                }
                //DebugLog.LogToFileOnly("salary3 is " + num.ToString());
            }
            //DebugLog.LogToFileOnly("salary2 is " + num.ToString());
            return num;
        }//public


        public void ProcessCitizen(uint homeID, ref CitizenUnit data, bool isPre)
        {
            if (isPre)
            {
                comm_data.family_money[homeID] = 0;
                if (data.m_citizen0 != 0)
                {
                    citizenCount++;
                    comm_data.family_money[homeID] += comm_data.citizen_money[data.m_citizen0];
                }
                if (data.m_citizen1 != 0)
                {
                    citizenCount++;
                    comm_data.family_money[homeID] += comm_data.citizen_money[data.m_citizen1];
                }
                if (data.m_citizen2 != 0)
                {
                    citizenCount++;
                    comm_data.family_money[homeID] += comm_data.citizen_money[data.m_citizen2];
                }
                if (data.m_citizen3 != 0)
                {
                    citizenCount++;
                    comm_data.family_money[homeID] += comm_data.citizen_money[data.m_citizen3];
                }
                if (data.m_citizen4 != 0)
                {
                    citizenCount++;
                    comm_data.family_money[homeID] += comm_data.citizen_money[data.m_citizen4];
                }
            } else
            {
                int temp = 0;
                if (data.m_citizen0 != 0)
                {
                    temp++;
                }
                if (data.m_citizen1 != 0)
                {
                    temp++;
                }
                if (data.m_citizen2 != 0)
                {
                    temp++;
                }
                if (data.m_citizen3 != 0)
                {
                    temp++;
                }
                if (data.m_citizen4 != 0)
                {
                    temp++;
                }

                if (temp!=0)
                {
                    if (data.m_citizen0 != 0)
                    {
                        comm_data.citizen_money[data.m_citizen0] = comm_data.family_money[homeID] / (float)temp;
                    }
                    if (data.m_citizen1 != 0)
                    {
                        comm_data.citizen_money[data.m_citizen1] = comm_data.family_money[homeID] / (float)temp;
                    }
                    if (data.m_citizen2 != 0)
                    {
                        comm_data.citizen_money[data.m_citizen2] = comm_data.family_money[homeID] / (float)temp;
                    }
                    if (data.m_citizen3 != 0)
                    {
                        comm_data.citizen_money[data.m_citizen3] = comm_data.family_money[homeID] / (float)temp;
                    }
                    if (data.m_citizen4 != 0)
                    {
                        comm_data.citizen_money[data.m_citizen4] = comm_data.family_money[homeID] / (float)temp;
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
                comm_data.family_count = familyCount;
                comm_data.citizen_count = citizenCount;
                comm_data.family_profit_money_num = familyProfitMoneyCount;
                comm_data.family_very_profit_money_num = familyVeryProfitMoneyCount;
                comm_data.family_loss_money_num = familyLossMoneyCount;
                if (familyCount != 0)
                {
                    comm_data.citizen_salary_per_family = (int)((citizenSalaryCount / familyCount));
                    comm_data.citizen_expense_per_family = (int)((citizenExpenseCount / familyCount));
                }
                comm_data.citizen_expense = citizenExpenseCount;
                comm_data.citizen_salary_tax_total = citizenSalaryTaxTotal;
                comm_data.citizen_salary_total = citizenSalaryCount;
                if (comm_data.family_count < comm_data.family_weight_stable_high)
                {
                    comm_data.family_weight_stable_high = (uint)comm_data.family_count;
                }
                else
                {
                    comm_data.family_weight_stable_high = familyWeightStableHigh;
                }
                if (comm_data.family_count < comm_data.family_weight_stable_low)
                {
                    comm_data.family_weight_stable_low = (uint)comm_data.family_count;
                }
                else
                {
                    comm_data.family_weight_stable_low = familyWeightStableLow;
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
            else if (preCitizenId < homeID)
            {
                //citizen_process_done = false;
                familyCount++;
                citizenGoodsTemp += data.m_goods;
            }

            if (homeID > 524288)
            {
                DebugLog.LogToFileOnly("Error: citizen ID greater than 524288");
            }

            /*if (comm_data.family_money[homeID] < -39000000f)
            {
                comm_data.family_money[homeID] = 0;
            }*/

            ProcessCitizen(homeID, ref data, true);

            if (comm_data.family_money[homeID] < 5000)
            {
                if (comm_data.family_profit_status[homeID] == 10)
                {
                    comm_data.family_profit_status[homeID] = 25;
                }
            }else if (comm_data.family_money[homeID] > 20000)
            {
                if (comm_data.family_profit_status[homeID] == 10)
                {
                    comm_data.family_profit_status[homeID] = 230;
                }
            } else
            {
                if (comm_data.family_profit_status[homeID] == 10)
                {
                    comm_data.family_profit_status[homeID] = (byte)(comm_data.family_money[homeID] / 100);
                }
            }

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
            float tax = 0;
            //0-10 0% 10-30 5% 30-60 10% 60-100 20% >100 35%

            if (citizenSalaryCurrent < 0)
            {
                DebugLog.LogToFileOnly("citizenSalaryCurrent< 0 in ResidentAI");
                citizenSalaryCurrent = 0;
            }

            if (citizenSalaryCurrent < 10)
            {
                tax = 0;
            }
            else if (citizenSalaryCurrent >= 10 && citizenSalaryCurrent <= 30)
            {
                tax = (citizenSalaryCurrent - 10) * 0.05f;
            }
            else if (citizenSalaryCurrent > 30 && citizenSalaryCurrent <= 60)
            {
                tax = (citizenSalaryCurrent - 30) * 0.1f + 1f;
            }
            else if (citizenSalaryCurrent > 60 && citizenSalaryCurrent <= 100)
            {
                tax = (citizenSalaryCurrent - 60) * 0.2f + 4f;
            }
            else if (citizenSalaryCurrent > 100)
            {
                tax = (citizenSalaryCurrent - 100) * 0.35f + 12f;
            }


            //caculate food tax
            if (tempNum > 0 && comm_data.family_money[homeID] > 0 && comm_data.isFoodsGettedFinal)
            {
                comm_data.family_money[homeID] -= pc_PrivateBuildingAI.foodPrice * tempNum;
                tax += (pc_PrivateBuildingAI.foodPrice * tempNum + 0.5f);
            }

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
            comm_data.family_money[homeID] = (float)(comm_data.family_money[homeID] + tempNum);
            //process citizen status
            System.Random rand = new System.Random();
            if (tempNum <= 20)
            {
                tempNum = rand.Next(10);
                familyLossMoneyCount = (uint)(familyLossMoneyCount + 1);
                //try_move_family to do here;
            }
            else if (tempNum > 70)
            {
                tempNum = rand.Next((int)(tempNum / 2));
                familyVeryProfitMoneyCount = (uint)(familyVeryProfitMoneyCount + 1);
            }
            else
            {
                tempNum = rand.Next((int)(tempNum / 2));
                familyProfitMoneyCount = (uint)(familyProfitMoneyCount + 1);
            }


            tempNum = (tempNum > 40) ? 40 : tempNum;

            comm_data.family_money[homeID] = (float)(comm_data.family_money[homeID] - tempNum);

            if (comm_data.family_money[homeID] < 0)
            {
                int num = (int)(-(comm_data.family_money[homeID]) + 0.5f);
                comm_data.family_money[homeID] = 0;
                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, num, ItemClass.Service.Residential, ItemClass.SubService.None, ItemClass.Level.Level1);
            }


            if (comm_data.family_money[homeID] >= 20000)
            {
                comm_data.family_profit_status[homeID]++;
            }
            else if (comm_data.family_money[homeID] < 5000)
            {
                comm_data.family_profit_status[homeID]--;
            }
            else
            {
                if (comm_data.family_profit_status[homeID] > 128)
                {
                    comm_data.family_profit_status[homeID]--;
                } else
                {
                    comm_data.family_profit_status[homeID]++;
                }
            }



            if (comm_data.family_money[homeID] > 32000000f)
            {
                comm_data.family_money[homeID] = 32000000f;
            }

            if (comm_data.family_money[homeID] < -32000000f)
            {
                comm_data.family_money[homeID] = -32000000f;
            }

            if (comm_data.family_profit_status[homeID] > 250)
            {
                comm_data.family_profit_status[homeID] = 250;
            }
            if (comm_data.family_profit_status[homeID] < 15)
            {
                comm_data.family_profit_status[homeID] = 15;
            }

            if ((comm_data.family_money[homeID] < 5000) && (comm_data.family_profit_status[homeID] <= 25))
            {
                familyWeightStableLow = (ushort)(familyWeightStableLow + 1);
            }
            else if ((comm_data.family_money[homeID] >= 20000) && (comm_data.family_profit_status[homeID] >= 230))
            {
                familyWeightStableHigh = (ushort)(familyWeightStableHigh + 1);
            }

            //DebugLog.LogToFileOnly("comm_data.family_profit_status[" + homeID.ToString() +"] = " + comm_data.family_profit_status[homeID].ToString() + "money = " + comm_data.family_money[homeID].ToString());
            //set other non-exist citizen status to 0
            uint i;
            if (preCitizenId < homeID)
            {
                for (i = (preCitizenId + 1); i < homeID; i++)
                {
                    if (comm_data.family_profit_status[i] != 10)
                    {
                        //comm_data.family_money[i] = rand.Next(comm_data.citizen_salary_per_family + 1) * 200 ;
                        comm_data.family_profit_status[i] = 10;
                    }
                }
            } else
            {
                for (i = (preCitizenId + 1); i < 524288; i++)
                {
                    if (comm_data.family_profit_status[i] != 10)
                    {
                        //comm_data.family_money[i] = rand.Next(comm_data.citizen_salary_per_family + 1) * 200;
                        comm_data.family_profit_status[i] = 10;
                    }
                }

                for (i = 0; i < homeID; i++)
                {
                    if (comm_data.family_profit_status[i] != 10)
                    {
                        comm_data.family_profit_status[i] = 10;
                    }
                }
            }
            if (comm_data.citizen_count == 0)
            {
                comm_data.family_money[homeID] = 0;
                comm_data.family_profit_status[homeID] = 128;
            }
            preCitizenId = homeID;
            ProcessCitizen(homeID, ref data, false);
            return (byte)tempNum;
            //return to original game code.
        }

/*        public void TryMoveAwayFromHome_1(uint citizenID, ref Citizen data)
        {
            if (data.Dead)
            {
                return;
            }
            if (data.m_homeBuilding == 0)
            {
                return;
            }
            Citizen.AgeGroup ageGroup = Citizen.GetAgeGroup(data.Age);
            if (ageGroup == Citizen.AgeGroup.Young || ageGroup == Citizen.AgeGroup.Adult)
            {
                TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                if (ageGroup == Citizen.AgeGroup.Young)
                {
                    offer.Priority = 1;
                }
                else
                {
                    offer.Priority = Singleton<SimulationManager>.instance.m_randomizer.Int32(2, 4);
                }
                offer.Citizen = citizenID;
                offer.Position = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_homeBuilding].m_position;
                offer.Amount = 1;
                offer.Active = true;
                if (Singleton<SimulationManager>.instance.m_randomizer.Int32(2u) == 0)
                {
                    switch (data.EducationLevel)
                    {
                        case Citizen.Education.Uneducated:
                            Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single0, offer);
                            break;
                        case Citizen.Education.OneSchool:
                            Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single1, offer);
                            break;
                        case Citizen.Education.TwoSchools:
                        case Citizen.Education.ThreeSchools:
                            Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single2, offer);
                            break;
                    }
                }
                else
                {
                    switch (data.EducationLevel)
                    {
                        case Citizen.Education.Uneducated:
                            Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single0B, offer);
                            break;
                        case Citizen.Education.OneSchool:
                            Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single1B, offer);
                            break;
                        case Citizen.Education.TwoSchools:
                        case Citizen.Education.ThreeSchools:
                            Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single2B, offer);
                            break;
                    }
                }
            }
        }*/


/*        public void TryMoveFamily_1(uint homeID, uint citizenID, ref Citizen data, int familySize)
        {
            if (data.Dead)
            {
                return;
            }
            if (data.m_homeBuilding == 0)
            {
                return;
            }
            TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
            offer.Priority = Singleton<SimulationManager>.instance.m_randomizer.Int32(1, 7);
            offer.Citizen = citizenID;
            offer.Position = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_homeBuilding].m_position;
            offer.Amount = 1;
            offer.Active = true;
            if (familySize == 1)
            {
                if (Singleton<SimulationManager>.instance.m_randomizer.Int32(2u) == 0)
                {
                    if ((comm_data.family_money[homeID] > 20000) && (comm_data.family_profit_status[homeID] >= 230))
                    {
                        Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single3, offer);
                    }
                    else if ((comm_data.family_money[homeID] < 5000) && (comm_data.family_profit_status[homeID] <= 25))
                    {
                        Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single0, offer);
                    }
                    else if (Singleton<SimulationManager>.instance.m_randomizer.Int32(2u) == 0)
                    {
                        Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single2, offer);
                    }
                    else
                    {
                        Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single1, offer);
                    }
                }
                else
                {
                    if ((comm_data.family_money[homeID] > 20000) && (comm_data.family_profit_status[homeID] >= 230))
                    {
                        Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single3B, offer);
                    }
                    else if ((comm_data.family_money[homeID] < 5000) && (comm_data.family_profit_status[homeID] <= 25))
                    {
                        Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single0B, offer);
                    }
                    else if (Singleton<SimulationManager>.instance.m_randomizer.Int32(2u) == 0)
                    {
                        Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single2B, offer);
                    }
                    else
                    {
                        Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Single1B, offer);
                    }
                }
            }
            else
            {
                if ((comm_data.family_money[homeID] > 20000) && (comm_data.family_profit_status[homeID] >= 230))
                {
                    Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Family3, offer);
                }
                else if ((comm_data.family_money[homeID] < 5000) && (comm_data.family_profit_status[homeID] <= 25))
                {
                    Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Family0, offer);
                }
                else if (Singleton<SimulationManager>.instance.m_randomizer.Int32(2u) == 0)
                {
                    Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Family2, offer);
                }
                else
                {
                    Singleton<TransferManager>.instance.AddOutgoingOffer(TransferManager.TransferReason.Family1, offer);
                }
            }
        }*/

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


        public bool ChanceToDoVitureShopping(uint homeID, ref CitizenUnit data)
        {
            SimulationManager instance2 = Singleton<SimulationManager>.instance;
            BuildingManager expr_18 = Singleton<BuildingManager>.instance;
            Building building = expr_18.m_buildings.m_buffer[(int)data.m_building];
            ushort num = 0;
            
            num = FindNotSoCloseBuilding(building.m_position, 2000f, ItemClass.Service.Commercial, ItemClass.SubService.None, Building.Flags.Created, Building.Flags.Deleted | Building.Flags.Abandoned);
            

            if ((num == 0) || (Singleton<SimulationManager>.instance.m_randomizer.Int32(20) < 10))
            {
                int num2 = Singleton<SimulationManager>.instance.m_randomizer.Int32(5u);
                for (int i = 0; i < 5; i++)
                {
                    uint citizen = data.GetCitizen((num2 + i) % 5);
                    if (citizen != 0u)
                    {
                        if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen].m_workBuilding != 0)
                        {
                            ushort num1 = 0;

                            
                            num1 = FindNotSoCloseBuilding(expr_18.m_buildings.m_buffer[Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen].m_workBuilding].m_position, 2000f, ItemClass.Service.Commercial, ItemClass.SubService.None, Building.Flags.Created, Building.Flags.Deleted | Building.Flags.Abandoned);


                            if (num1 != 0)
                            {
                                num = (ushort)num1;
                                break;
                            }
                        }
                    }
                }
            }
            if (num != 0)
            {
                int num1 = -100;
                expr_18.m_buildings.m_buffer[(int)num].Info.m_buildingAI.ModifyMaterialBuffer(num, ref expr_18.m_buildings.m_buffer[(int)num], TransferManager.TransferReason.Shopping, ref num1);
                ushort temp = (ushort)(-num1);
                data.m_goods += temp;
                //DebugLog.LogToFileOnly("try viture shopping now, temp = " + temp.ToString());
                return true;
            }
            else
            {
                //DebugLog.LogToFileOnly("failed to find a building to shopping");
            }
            return false;
        }

        public static void setNum(ref int[] source, ref int[] array)
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

                setNum(ref source, ref array);
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
                                    if ((info.m_class.m_subService == ItemClass.SubService.CommercialTourist) && (instance2.m_randomizer.Int32(100) < 97))
                                    {

                                    }
                                    else if ((info.m_class.m_subService == ItemClass.SubService.CommercialLeisure) && (instance2.m_randomizer.Int32(100) < 95))
                                    {

                                    }
                                    else
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

        /*public static ushort FindNotSoCloseBuilding(Vector3 pos, float maxDistance, ItemClass.Service service, ItemClass.SubService subService, Building.Flags flagsRequired, Building.Flags flagsForbidden)
        {
            int num = Mathf.Max((int)((pos.x - maxDistance) / 64f + 135f), 0);
            int num2 = Mathf.Max((int)((pos.z - maxDistance) / 64f + 135f), 0);
            int num3 = Mathf.Min((int)((pos.x + maxDistance) / 64f + 135f), 269);
            int num4 = Mathf.Min((int)((pos.z + maxDistance) / 64f + 135f), 269);
            ushort result = 0;
            float num5 = maxDistance * maxDistance;
            BuildingManager building = Singleton<BuildingManager>.instance;
            SimulationManager instance2 = Singleton<SimulationManager>.instance;

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
                for (int i = 0; i <= num4-num2; i++)
                  {
                    if (result != 0)
                    {
                        break;
                    }
                    for (int j = 0; j <= num3-num; j++)
                    {
                        ushort num6 = building.m_buildingGrid[array[idex]];
                        idex++;
                        int num7 = 0;
                        if (result != 0)
                        {
                            break;
                        }
                        while (num6 != 0)
                        {
                            BuildingInfo info = building.m_buildings.m_buffer[(int)num6].Info;
                            if ((info.m_class.m_service == service || service == ItemClass.Service.None) && (info.m_class.m_subService == subService || subService == ItemClass.SubService.None))
                            {
                                Building.Flags flags = building.m_buildings.m_buffer[(int)num6].m_flags;
                                if ((flags & (flagsRequired | flagsForbidden)) == flagsRequired)
                                {
                                    float num8 = Vector3.SqrMagnitude(pos - building.m_buildings.m_buffer[(int)num6].m_position);
                                    //for rush hour
                                    if (info.m_class.m_service == ItemClass.Service.Commercial)
                                    {
                                        if ((info.m_class.m_subService == ItemClass.SubService.CommercialTourist) && (instance2.m_randomizer.Int32(100) < 97))
                                        {

                                        }
                                        else if ((info.m_class.m_subService == ItemClass.SubService.CommercialLeisure) && (instance2.m_randomizer.Int32(100) < 95))
                                        {

                                        }
                                        else if ((maxDistance == 2000f) || (maxDistance == 500f))
                                        {
                                            if (instance2.m_randomizer.Int32(building.m_buildings.m_buffer[(int)num6].m_customBuffer2) > 300)
                                            {
                                                result = num6;
                                                if (instance2.m_randomizer.Int32(6) == 0)
                                                {
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if ((instance2.m_randomizer.Int32(building.m_buildings.m_buffer[(int)num6].m_customBuffer2) > 200))
                                            {
                                                result = num6;
                                                if (instance2.m_randomizer.Int32(6) == 0)
                                                {
                                                    break;
                                                }
                                            }
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
                        }
                    }
                }
            }
            return result;
        }*/

        /*public static void SetNum(ref int[] source, ref int[] array)
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
        }*/

        // ResidentAI
        public void SimulationStep_1(uint homeID, ref CitizenUnit data)
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
            int temp_num = ProcessFamily(homeID, ref data);


            data.m_goods = (ushort)Mathf.Max(0, (int)(data.m_goods - temp_num)); //here we can adjust demand

            if (data.m_goods < 19800)
            {
                SimulationManager instance2 = Singleton<SimulationManager>.instance;
                //float currentDayTimeHour = instance2.m_currentDayTimeHour;
                if (instance2.m_randomizer.Int32((uint)data.m_goods + 1) < temp_num * 1000)
                {
                    ChanceToDoVitureShopping(homeID, ref data);
                    //DebugLog.LogToFileOnly("lack of good, buy it directly" + data.m_goods.ToString());
                }
            }


            //sick
            if (data.m_goods < 19700)
            {
                if (Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.HealthCare))
                {
                    int num2 = Singleton<SimulationManager>.instance.m_randomizer.Int32(5u);
                    for (int i = 0; i < 5; i++)
                    {
                        uint citizen = data.GetCitizen((num2 + i) % 5);
                        if (citizen != 0u)
                        {
                            SimulationManager instance2 = Singleton<SimulationManager>.instance;
                            Citizen[] expr_2FA_cp_0 = instance.m_citizens.m_buffer;
                            if (instance2.m_randomizer.Int32(data.m_goods) < 100)
                            {
                                expr_2FA_cp_0[citizen].Sick = true;
                            }
                            break;
                        }
                    }
                }
            }

            //lack of food
            if (!comm_data.isFoodsGettedFinal)
            {
                if (Singleton<UnlockManager>.instance.Unlocked(ItemClass.Service.HealthCare))
                {
                    int num2 = Singleton<SimulationManager>.instance.m_randomizer.Int32(5u);
                    for (int i = 0; i < 5; i++)
                    {
                        uint citizen = data.GetCitizen((num2 + i) % 5);
                        if (citizen != 0u)
                        {
                            SimulationManager instance2 = Singleton<SimulationManager>.instance;
                            Citizen[] expr_2FA_cp_0 = instance.m_citizens.m_buffer;
                            if (instance2.m_randomizer.Int32(500) < 50)
                            {
                                expr_2FA_cp_0[citizen].Sick = true;
                            }
                            break;
                        }
                    }
                }
            }

            if (data.m_goods < 20000)
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
                            incomeAccumulation = comm_data.resident_low_level1_rent;
                            break;
                        case ItemClass.Level.Level2:
                            incomeAccumulation = comm_data.resident_low_level2_rent;
                            break;
                        case ItemClass.Level.Level3:
                            incomeAccumulation = comm_data.resident_low_level3_rent;
                            break;
                        case ItemClass.Level.Level4:
                            incomeAccumulation = comm_data.resident_low_level4_rent;
                            break;
                        case ItemClass.Level.Level5:
                            incomeAccumulation = comm_data.resident_low_level5_rent;
                            break;
                    }
                }
                else if (@class.m_subService == ItemClass.SubService.ResidentialLowEco)
                {
                    switch (@class.m_level)
                    {
                        case ItemClass.Level.Level1:
                            incomeAccumulation = comm_data.resident_low_eco_level1_rent;
                            break;
                        case ItemClass.Level.Level2:
                            incomeAccumulation = comm_data.resident_low_eco_level2_rent;
                            break;
                        case ItemClass.Level.Level3:
                            incomeAccumulation = comm_data.resident_low_eco_level3_rent;
                            break;
                        case ItemClass.Level.Level4:
                            incomeAccumulation = comm_data.resident_low_eco_level4_rent;
                            break;
                        case ItemClass.Level.Level5:
                            incomeAccumulation = comm_data.resident_low_eco_level5_rent;
                            break;
                    }
                }
                else if (@class.m_subService == ItemClass.SubService.ResidentialHigh)
                {
                    switch (@class.m_level)
                    {
                        case ItemClass.Level.Level1:
                            incomeAccumulation = comm_data.resident_high_level1_rent;
                            break;
                        case ItemClass.Level.Level2:
                            incomeAccumulation = comm_data.resident_high_level2_rent;
                            break;
                        case ItemClass.Level.Level3:
                            incomeAccumulation = comm_data.resident_high_level3_rent;
                            break;
                        case ItemClass.Level.Level4:
                            incomeAccumulation = comm_data.resident_high_level4_rent;
                            break;
                        case ItemClass.Level.Level5:
                            incomeAccumulation = comm_data.resident_high_level5_rent;
                            break;
                    }
                }
                else
                {
                    switch (@class.m_level)
                    {
                        case ItemClass.Level.Level1:
                            incomeAccumulation = comm_data.resident_high_eco_level1_rent;
                            break;
                        case ItemClass.Level.Level2:
                            incomeAccumulation = comm_data.resident_high_eco_level2_rent;
                            break;
                        case ItemClass.Level.Level3:
                            incomeAccumulation = comm_data.resident_high_eco_level3_rent;
                            break;
                        case ItemClass.Level.Level4:
                            incomeAccumulation = comm_data.resident_high_eco_level4_rent;
                            break;
                        case ItemClass.Level.Level5:
                            incomeAccumulation = comm_data.resident_high_eco_level5_rent;
                            break;
                    }
                }
                int num2;
                num2 = Singleton<EconomyManager>.instance.GetTaxRate(@class, taxationPolicies);
                if (comm_data.family_money[homeid] > 0)
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
                if (comm_data.family_money[homeid] > 0 && (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_flags.IsFlagSet(Citizen.Flags.Education2)))
                {
                    temp = temp + 20;
                    Singleton<EconomyManager>.instance.AddPrivateIncome(20, ItemClass.Service.Education, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                }

                if (comm_data.family_money[homeid] > 0 && (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen_id].m_flags.IsFlagSet(Citizen.Flags.Education1)))
                {
                    temp = temp + 10;
                    Singleton<EconomyManager>.instance.AddPrivateIncome(10, ItemClass.Service.Education, ItemClass.SubService.None, ItemClass.Level.Level2, 115);
                }

                if (comm_data.family_money[homeid] > 0)
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

        private bool IsRoadConnection(ushort building)
        {
            if (building != 0)
            {
                BuildingManager instance = Singleton<BuildingManager>.instance;
                if ((instance.m_buildings.m_buffer[(int)building].m_flags & Building.Flags.IncomingOutgoing) != Building.Flags.None && instance.m_buildings.m_buffer[(int)building].Info.m_class.m_service == ItemClass.Service.Road)
                {
                    return true;
                }
            }
            return false;
        }



        public void IsOutsideMovingIn(ushort instanceID, ref CitizenInstance data, ushort targetBuilding)
        {
            int homeBuilding1 = Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].m_homeBuilding;
            uint containingUnit = Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].GetContainingUnit(data.m_citizen, Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)homeBuilding1].m_citizenUnits, CitizenUnit.Flags.Home);
            if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].m_flags.IsFlagSet(Citizen.Flags.MovingIn))
            {
                comm_data.citizen_money[data.m_citizen] = 0;
            }
        }



        /*public override void SetTarget(ushort instanceID, ref CitizenInstance data, ushort targetIndex, bool targetIsNode)
        {
            int dayTimeFrame = (int)Singleton<SimulationManager>.instance.m_dayTimeFrame;
            int dAYTIME_FRAMES = (int)SimulationManager.DAYTIME_FRAMES;
            int num = Mathf.Max(dAYTIME_FRAMES >> 2, Mathf.Abs(dayTimeFrame - (dAYTIME_FRAMES >> 1)));
            if (Singleton<SimulationManager>.instance.m_randomizer.Int32((uint)dAYTIME_FRAMES >> 1) < num)
            {
                data.m_flags &= ~CitizenInstance.Flags.CannotUseTaxi;
            }
            else
            {
                data.m_flags |= CitizenInstance.Flags.CannotUseTaxi;
            }
            data.m_flags &= ~CitizenInstance.Flags.CannotUseTransport;
            if (targetIndex != data.m_targetBuilding || targetIsNode != ((data.m_flags & CitizenInstance.Flags.TargetIsNode) != CitizenInstance.Flags.None))
            {
                if (data.m_targetBuilding != 0)
                {
                    if ((data.m_flags & CitizenInstance.Flags.TargetIsNode) != CitizenInstance.Flags.None)
                    {
                        Singleton<NetManager>.instance.m_nodes.m_buffer[(int)data.m_targetBuilding].RemoveTargetCitizen(instanceID, ref data);
                        ushort num2 = 0;
                        if (targetIsNode)
                        {
                            num2 = Singleton<NetManager>.instance.m_nodes.m_buffer[(int)data.m_targetBuilding].m_transportLine;
                        }
                        if ((data.m_flags & CitizenInstance.Flags.OnTour) != CitizenInstance.Flags.None)
                        {
                            ushort transportLine = Singleton<NetManager>.instance.m_nodes.m_buffer[(int)data.m_targetBuilding].m_transportLine;
                            uint citizen = data.m_citizen;
                            if (transportLine != 0 && transportLine != num2 && citizen != 0u)
                            {
                                TransportManager instance = Singleton<TransportManager>.instance;
                                TransportInfo info = instance.m_lines.m_buffer[(int)transportLine].Info;
                                if (info != null && info.m_vehicleType == VehicleInfo.VehicleType.None)
                                {
                                    data.m_flags &= ~CitizenInstance.Flags.OnTour;
                                }
                            }
                        }
                        if (!targetIsNode)
                        {
                            data.m_flags &= ~CitizenInstance.Flags.TargetIsNode;
                        }
                    }
                    else
                    {
                        Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_targetBuilding].RemoveTargetCitizen(instanceID, ref data);
                    }
                }
                data.m_targetBuilding = targetIndex;
                if (targetIsNode)
                {
                    data.m_flags |= CitizenInstance.Flags.TargetIsNode;
                }
                if (data.m_targetBuilding != 0)
                {
                    if ((data.m_flags & CitizenInstance.Flags.TargetIsNode) != CitizenInstance.Flags.None)
                    {
                        Singleton<NetManager>.instance.m_nodes.m_buffer[(int)data.m_targetBuilding].AddTargetCitizen(instanceID, ref data);
                    }
                    else
                    {
                        Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_targetBuilding].AddTargetCitizen(instanceID, ref data);
                    }
                    data.m_targetSeed = (byte)Singleton<SimulationManager>.instance.m_randomizer.Int32(256u);
                }
            }
            if (((data.m_flags & CitizenInstance.Flags.TargetIsNode) == CitizenInstance.Flags.None && this.IsRoadConnection(targetIndex)) || this.IsRoadConnection(data.m_sourceBuilding))
            {
                data.m_flags |= CitizenInstance.Flags.BorrowCar;
                // new added begin
                IsOutsideMovingIn(instanceID, ref data, targetIndex);
                // new added end
            }
            else
            {
                data.m_flags &= ~CitizenInstance.Flags.BorrowCar;
            }
            if (targetIndex != 0 && (data.m_flags & (CitizenInstance.Flags.Character | CitizenInstance.Flags.TargetIsNode)) == CitizenInstance.Flags.None)
            {
                ushort eventIndex = 0;
                if (data.m_citizen != 0u && Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].m_workBuilding != targetIndex)
                {
                    eventIndex = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)targetIndex].m_eventIndex;
                }
                Color32 eventCitizenColor = Singleton<EventManager>.instance.GetEventCitizenColor(eventIndex, data.m_citizen);
                if (eventCitizenColor.a == 255)
                {
                    data.m_color = eventCitizenColor;
                    data.m_flags |= CitizenInstance.Flags.CustomColor;
                }
            }
            // new added begin
            Citizen.AgePhase temp_agephase = data.Info.m_agePhase;
            ushort temp_parkedVehicle = 0;
            if ((Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].CurrentLocation == Citizen.Location.Home) && (!(targetIndex == (Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].m_workBuilding))))
            {
                data.Info.m_agePhase = CanUseTransport(instanceID, ref data, targetIndex);
            }

            if (data.Info.m_agePhase == Citizen.AgePhase.Child)
            {
                if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].m_parkedVehicle != 0)
                {
                    if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].CurrentLocation == Citizen.Location.Home)
                    {
                        temp_parkedVehicle = Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].m_parkedVehicle;
                        Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].m_parkedVehicle = 0;
                    }
                }
            }

            if (!this.StartPathFind(instanceID, ref data))
            {
                data.Info.m_agePhase = temp_agephase;
                data.Unspawn(instanceID);
                if (temp_parkedVehicle != 0)
                {
                    Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].m_parkedVehicle = temp_parkedVehicle;
                }
            }
            data.Info.m_agePhase = temp_agephase;
            if (temp_parkedVehicle != 0)
            {
                Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].m_parkedVehicle = temp_parkedVehicle;
            }
            // new added end
        }*/


        /* public override void SetTarget(ushort instanceID, ref CitizenInstance data, ushort targetBuilding)
         {
             int dayTimeFrame = (int)Singleton<SimulationManager>.instance.m_dayTimeFrame;
             int dAYTIME_FRAMES = (int)SimulationManager.DAYTIME_FRAMES;
             int num = Mathf.Max(dAYTIME_FRAMES >> 2, Mathf.Abs(dayTimeFrame - (dAYTIME_FRAMES >> 1)));
             if (Singleton<SimulationManager>.instance.m_randomizer.Int32((uint)dAYTIME_FRAMES >> 1) < num)
             {
                 data.m_flags &= ~CitizenInstance.Flags.CannotUseTaxi;
             }
             else
             {
                 data.m_flags |= CitizenInstance.Flags.CannotUseTaxi;
             }
             data.m_flags &= ~CitizenInstance.Flags.CannotUseTransport;



             if (targetBuilding != data.m_targetBuilding)
             {
                 if (data.m_targetBuilding != 0)
                 {
                     Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_targetBuilding].RemoveTargetCitizen(instanceID, ref data);
                 }
                 data.m_targetBuilding = targetBuilding;
                 if (data.m_targetBuilding != 0)
                 {
                     Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_targetBuilding].AddTargetCitizen(instanceID, ref data);
                     data.m_targetSeed = (byte)Singleton<SimulationManager>.instance.m_randomizer.Int32(256u);
                 }
             }
             if (this.IsRoadConnection(targetBuilding) || this.IsRoadConnection(data.m_sourceBuilding))
             {
                 data.m_flags |= CitizenInstance.Flags.BorrowCar;
                 is_outside_movingin(instanceID, ref data, targetBuilding);
             }
             else
             {
                 data.m_flags &= ~CitizenInstance.Flags.BorrowCar;
             }
             if (targetBuilding != 0 && (data.m_flags & CitizenInstance.Flags.Character) == CitizenInstance.Flags.None)
             {
                 ushort eventIndex = 0;
                 if (data.m_citizen != 0u && Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].m_workBuilding != targetBuilding)
                 {
                     eventIndex = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)targetBuilding].m_eventIndex;
                 }
                 Color32 eventCitizenColor = Singleton<EventManager>.instance.GetEventCitizenColor(eventIndex, data.m_citizen);
                 if (eventCitizenColor.a == 255)
                 {
                     data.m_color = eventCitizenColor;
                     data.m_flags |= CitizenInstance.Flags.CustomColor;
                 }
             }

             Citizen.AgePhase temp_agephase = data.Info.m_agePhase;
             ushort temp_parkedVehicle = 0;
             if ((Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].CurrentLocation == Citizen.Location.Home) && (!(targetBuilding == (Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].m_workBuilding))))
             { 
                 data.Info.m_agePhase = canusetransport(instanceID, ref data, targetBuilding);
             }

             if (data.Info.m_agePhase == Citizen.AgePhase.Child)
             {
                 if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].m_parkedVehicle != 0)
                 {
                     if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].CurrentLocation == Citizen.Location.Home)
                     {
                         temp_parkedVehicle = Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].m_parkedVehicle;
                         Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].m_parkedVehicle = 0;
                     }
                 }
             }


             if (!this.StartPathFind(instanceID, ref data))
             {
                 data.Info.m_agePhase = temp_agephase;
                 data.Unspawn(instanceID);
                 if (temp_parkedVehicle != 0)
                 {
                     Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].m_parkedVehicle = temp_parkedVehicle;
                 }
             }
             data.Info.m_agePhase = temp_agephase;
             if (temp_parkedVehicle != 0)
             {
                 Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen)].m_parkedVehicle = temp_parkedVehicle;
             }
         }*/

        /*public Citizen.AgePhase CanUseTransport (ushort instanceID, ref CitizenInstance citizenData, ushort targetBuilding)
        {
            uint citizen = citizenData.m_citizen;
            System.Random rand = new System.Random();
            Citizen.AgePhase tempAgePhase = citizenData.Info.m_agePhase;
            int homeBuilding1 = Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_homeBuilding;
            uint containingUnit = Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].GetContainingUnit(citizen, Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)homeBuilding1].m_citizenUnits, CitizenUnit.Flags.Home);
            int tempMoney = (comm_data.family_money[containingUnit] > 1) ? (int)comm_data.family_money[containingUnit] : 1;
            if (rand.Next(tempMoney) < 4000)
            {
                BuildingManager instance3 = Singleton<BuildingManager>.instance;
                DistrictManager instance4 = Singleton<DistrictManager>.instance;

                byte district = 0;
                if (citizenData.m_sourceBuilding != 0)
                {
                    district = instance4.GetDistrict(instance3.m_buildings.m_buffer[citizenData.m_sourceBuilding].m_position);
                }

                byte district1 = 0;
                if (targetBuilding != 0)
                {
                    district1 = instance4.GetDistrict(instance3.m_buildings.m_buffer[targetBuilding].m_position);
                }
                DistrictPolicies.Services servicePolicies = instance4.m_districts.m_buffer[(int)district].m_servicePolicies;
                DistrictPolicies.Event @event = instance4.m_districts.m_buffer[(int)district].m_eventPolicies & Singleton<EventManager>.instance.GetEventPolicyMask();
                DistrictPolicies.Services servicePolicies1 = instance4.m_districts.m_buffer[(int)district1].m_servicePolicies;
                DistrictPolicies.Event @event1 = instance4.m_districts.m_buffer[(int)district1].m_eventPolicies & Singleton<EventManager>.instance.GetEventPolicyMask();
                citizenData.m_flags = (citizenData.m_flags | CitizenInstance.Flags.CannotUseTaxi);
                citizenData.m_flags = (citizenData.m_flags | CitizenInstance.Flags.CannotUseTransport);
                tempAgePhase = Citizen.AgePhase.Child;
                if ((citizenData.m_sourceBuilding != 0) && (targetBuilding != 0))
                {
                    if (((servicePolicies & DistrictPolicies.Services.FreeTransport) != DistrictPolicies.Services.None) || ((@event & DistrictPolicies.Event.ComeOneComeAll) != DistrictPolicies.Event.None))
                    {
                        if (((servicePolicies1 & DistrictPolicies.Services.FreeTransport) != DistrictPolicies.Services.None) || ((@event1 & DistrictPolicies.Event.ComeOneComeAll) != DistrictPolicies.Event.None))
                        {
                            citizenData.m_flags = (citizenData.m_flags & (~CitizenInstance.Flags.CannotUseTransport));
                            //DebugLog.LogToFileOnly("public transport free, people can use transport now");
                        }
                    }
                }
            }
            else
            {
                BuildingManager instance3 = Singleton<BuildingManager>.instance;
                DistrictManager instance4 = Singleton<DistrictManager>.instance;

                byte district = 0;
                if (citizenData.m_sourceBuilding != 0)
                {
                    district = instance4.GetDistrict(instance3.m_buildings.m_buffer[citizenData.m_sourceBuilding].m_position);
                }

                byte district1 = 0;
                if (targetBuilding != 0)
                {
                    district1 = instance4.GetDistrict(instance3.m_buildings.m_buffer[targetBuilding].m_position);
                }
                DistrictPolicies.Services servicePolicies = instance4.m_districts.m_buffer[(int)district].m_servicePolicies;
                DistrictPolicies.Event @event = instance4.m_districts.m_buffer[(int)district].m_eventPolicies & Singleton<EventManager>.instance.GetEventPolicyMask();
                DistrictPolicies.Services servicePolicies1 = instance4.m_districts.m_buffer[(int)district1].m_servicePolicies;
                DistrictPolicies.Event @event1 = instance4.m_districts.m_buffer[(int)district1].m_eventPolicies & Singleton<EventManager>.instance.GetEventPolicyMask();
                if (rand.Next((int)comm_data.family_money[containingUnit]) < 6000)
                {
                    citizenData.m_flags = (citizenData.m_flags | CitizenInstance.Flags.CannotUseTaxi);
                }

                if (rand.Next((int)comm_data.family_money[containingUnit]) < 5000)
                {
                    tempAgePhase = Citizen.AgePhase.Child; // do not use car
                }                    //citizenData.m_flags = (citizenData.m_flags | CitizenInstance.Flags.CannotUseTransport);
                if ((citizenData.m_sourceBuilding != 0) && (targetBuilding != 0))
                {
                    if (((servicePolicies & DistrictPolicies.Services.FreeTransport) != DistrictPolicies.Services.None) || ((@event & DistrictPolicies.Event.ComeOneComeAll) != DistrictPolicies.Event.None))
                    {
                        if (((servicePolicies1 & DistrictPolicies.Services.FreeTransport) != DistrictPolicies.Services.None) || ((@event1 & DistrictPolicies.Event.ComeOneComeAll) != DistrictPolicies.Event.None))
                        {
                            citizenData.m_flags = (citizenData.m_flags & (~CitizenInstance.Flags.CannotUseTransport));
                            //DebugLog.LogToFileOnly("public transport free, people can use transport now");
                        }
                    }
                }
            }
            return tempAgePhase;
        }*/
    }
}
