using System;
using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using System.Reflection;
using System.IO;
using System.Linq;
using ColossalFramework.Math;
using UnityEngine;

namespace RealCity
{

    public class RealCity : IUserMod
    {
        public static bool IsEnabled = false;
        public static bool updateOnce = false;

        public byte tip1_citizen = 0;
        public byte tip2_building = 0;
        public byte tip3_outside = 0;

        public static string tip1_message_forgui = "";
        public static string tip2_message_forgui = "";
        public static string tip3_message_forgui = "";
        public static string tip4_message_forgui = "";
        public static string tip5_message_forgui = "";
        public static string tip6_message_forgui = "";
        public static string tip7_message_forgui = "";
        public static string tip8_message_forgui = "";
        public static string tip9_message_forgui = "";
        public static string tip10_message_forgui = "";

        //public static string tip1_message = "";
        //public static string tip2_message = "";
        //public static string tip3_message = "";

        public static int language_idex = 0;

        public string Name
        {
            get { return "Real City Mod"; }
        }

        public string Description
        {
            get { return "Make your city reality"; }
        }

        public void OnEnabled()
        {
            RealCity.IsEnabled = true;
            FileStream fs = File.Create("RealCity.txt");
            fs.Close();
            LoadSetting();
            SaveSetting();
            language.language_switch((byte)language_idex);
        }

        public void OnDisabled()
        {
            RealCity.IsEnabled = false;
            language.language_switch((byte)language_idex);
        }


        public static void SaveSetting()
        {
            //save langugae
            FileStream fs = File.Create("RealCityV2.0_setting.txt");
            StreamWriter streamWriter = new StreamWriter(fs);
            streamWriter.WriteLine(comm_data.last_language);
            streamWriter.WriteLine(comm_data.isSmartPbtp);
            streamWriter.WriteLine(comm_data.isHellMode);
            streamWriter.Flush();
            fs.Close();
        }

        public static void LoadSetting()
        {
            if (File.Exists("RealCityV2.0_setting.txt"))
            {
                FileStream fs = new FileStream("RealCityV2.0_setting.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string strLine = sr.ReadLine();

                if (strLine == "1")
                {
                    comm_data.last_language = 1;
                }
                else
                {
                    comm_data.last_language = 0;
                }

                strLine = sr.ReadLine();

                if (strLine == "False")
                {
                    comm_data.isSmartPbtp = false;
                }
                else
                {
                    comm_data.isSmartPbtp = true;
                }

                strLine = sr.ReadLine();

                if (strLine == "False")
                {
                    comm_data.isHellMode = false;
                }
                else
                {
                    comm_data.isHellMode = true;
                }
                sr.Close();
                fs.Close();
            }
        }


        public void OnSettingsUI(UIHelperBase helper)
        {

            LoadSetting();
            language.language_switch(comm_data.last_language);
            UIHelperBase group = helper.AddGroup(language.OptionUI[0]);
            group.AddDropdown(language.OptionUI[1], new string[] { "English", "简体中文" }, comm_data.last_language, (index) => GetLanguageIdex(index));
            UIHelperBase group2 = helper.AddGroup(language.OptionUI[2]);
            group2.AddCheckbox(language.OptionUI[2], comm_data.isSmartPbtp, (index) => IsSmartPbtp(index));
            UIHelperBase group3 = helper.AddGroup(language.OptionUI[3]);
            group3.AddCheckbox(language.OptionUI[3], comm_data.isHellMode, (index) => IsHellMode(index));
            SaveSetting();
        }

        public void GetLanguageIdex(int index)
        {
            language_idex = index;
            language.language_switch((byte)language_idex);
            SaveSetting();
            MethodInfo method = typeof(OptionsMainPanel).GetMethod("OnLocaleChanged", BindingFlags.Instance | BindingFlags.NonPublic);
            method.Invoke(UIView.library.Get<OptionsMainPanel>("OptionsPanel"), new object[0]);
            Loader.RemoveGui();
            if (Loader.CurrentLoadMode == LoadMode.LoadGame || Loader.CurrentLoadMode == LoadMode.NewGame)
            {
                if (RealCity.IsEnabled)
                {
                    Loader.SetupGui();
                    //DebugLog.LogToFileOnly("get_current language idex = " + language_idex.ToString());
                }
            }
        }

        public void IsSmartPbtp(bool index)
        {
            comm_data.isSmartPbtp = index;
            SaveSetting();
        }

        public void IsHellMode(bool index)
        {
            comm_data.isHellMode = index;
            SaveSetting();
        }

        public class EconomyExtension : EconomyExtensionBase
        {
            public override long OnUpdateMoneyAmount(long internalMoneyAmount)
            {
                if (Loader.CurrentLoadMode == LoadMode.LoadGame || Loader.CurrentLoadMode == LoadMode.NewGame)
                {
                    comm_data.current_time = Singleton<SimulationManager>.instance.m_currentDayTimeHour;
                    uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
                    uint num2 = currentFrameIndex & 255u;
                    if ((num2 == 255u) && (comm_data.current_time != comm_data.prev_time))
                    {

                        
                        //citizen_status();
                        generate_tips();

                        if (comm_data.update_money_count == 16)
                        {
                            comm_data.isCoalsGetted = false;
                            comm_data.isFoodsGetted = false;
                            comm_data.isPetrolsGetted = false;
                            comm_data.isLumbersGetted = false;
                            comm_data.allFoods = 0;
                            comm_data.allLumbers = 0;
                            comm_data.allPetrols = 0;
                            comm_data.allCoals = 0;
                            building_status();
                            comm_data.isCoalsGettedFinal = comm_data.isCoalsGetted;
                            comm_data.isFoodsGettedFinal = comm_data.isFoodsGetted;
                            comm_data.isPetrolsGettedFinal = comm_data.isPetrolsGetted;
                            comm_data.isLumbersGettedFinal = comm_data.isLumbersGetted;
                        }

                        comm_data.allVehicles = 0;
                        VehicleStatus();
                        comm_data.allVehiclesFinal = comm_data.allVehicles;
                        caculate_citizen_transport_fee();

                        comm_data.update_money_count++;
                        if (comm_data.update_money_count == 17)
                        {
                            comm_data.update_money_count = 0;
                        }
                        pc_EconomyManager.clean_current(comm_data.update_money_count);


                        comm_data.prev_time = comm_data.current_time;
                    }
                    RealCityUI.refesh_onece = true;
                    MoreeconomicUI.refesh_onece = true;
                    PlayerBuildingUI.refesh_once = true;
                    BuildingUI.refeshOnce = true;
                    HumanUI.refeshOnce = true;
                    FoodButton.refeshOnce = true;
                    CoalButton.refeshOnce = true;
                    LumberButton.refeshOnce = true;
                    PetrolButton.refeshOnce = true;
                    comm_data.is_updated = true;
                }
                return internalMoneyAmount;
            }

            public void generate_tips()
            {
                if (comm_data.family_count != 0)
                {
                    if (comm_data.citizen_salary_per_family - comm_data.citizen_expense_per_family - (int)(comm_data.citizen_salary_tax_total / comm_data.family_count) - comm_data.citizen_average_transport_fee < 10)
                    {
                        if (comm_data.citizen_expense_per_family > 40)
                        {
                            tip1_message_forgui = language.TipAndChirperMessage[0];

                        }
                        else if (comm_data.citizen_average_transport_fee > 25)
                        {
                            tip1_message_forgui = language.TipAndChirperMessage[1];
                        }
                        else
                        {
                            tip1_message_forgui = language.TipAndChirperMessage[2];
                        }
                    }
                    else if (comm_data.citizen_salary_per_family < 40)
                    {
                        tip1_message_forgui = language.TipAndChirperMessage[2];
                    }
                    else
                    {
                        tip1_message_forgui = language.TipAndChirperMessage[3];
                    }
                }



                tip2_message_forgui = language.TipAndChirperMessage[4];

                tip3_message_forgui = language.TipAndChirperMessage[5];

                tip4_message_forgui = language.TipAndChirperMessage[6];

                tip5_message_forgui = language.TipAndChirperMessage[7];

                if (!comm_data.have_city_resource_department)
                {
                    tip6_message_forgui = language.TipAndChirperMessage[8];
                }
                else
                {
                    tip6_message_forgui = "";
                }

                if (!comm_data.isFoodsGettedFinal)
                {
                    tip7_message_forgui = language.TipAndChirperMessage[9];
                }
                else
                {
                    tip7_message_forgui = "";
                }

                if (!comm_data.isLumbersGettedFinal || !comm_data.isCoalsGettedFinal)
                {
                    tip8_message_forgui = language.TipAndChirperMessage[10];
                }
                else
                {
                    tip8_message_forgui = "";
                }

                if (!comm_data.isPetrolsGettedFinal)
                {
                    tip9_message_forgui = language.TipAndChirperMessage[11];
                }
                else
                {
                    tip9_message_forgui = "";
                }

                if(!comm_data.isHellMode)
                {
                    tip10_message_forgui = language.OptionUI[3];
                } else
                {
                    tip10_message_forgui = "";
                }


            }


            public void caculate_citizen_transport_fee()
            {
                ItemClass temp = new ItemClass();
                long temp1 = 0L;
                long temp2 = 0L;
                comm_data.public_transport_fee = 0L;
                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportBus;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.bus_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_subService = ItemClass.SubService.PublicTransportTram;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.tram_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_subService = ItemClass.SubService.PublicTransportMetro;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.metro_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_subService = ItemClass.SubService.PublicTransportTrain;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.train_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_subService = ItemClass.SubService.PublicTransportTaxi;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.taxi_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportPlane;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.plane_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_subService = ItemClass.SubService.PublicTransportShip;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.ship_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_subService = ItemClass.SubService.PublicTransportMonorail;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.monorail_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_subService = ItemClass.SubService.PublicTransportCableCar;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.cablecar_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2;

                //add vehicle transport_fee
                comm_data.temp_total_citizen_vehical_time_last = comm_data.temp_total_citizen_vehical_time;
                comm_data.temp_total_citizen_vehical_time = 0;

                //assume that 1 time will cost 5fen car oil money
                comm_data.all_transport_fee = comm_data.public_transport_fee + comm_data.temp_total_citizen_vehical_time_last;

                if (comm_data.family_count > 0)
                {
                    comm_data.citizen_average_transport_fee = (byte)(comm_data.all_transport_fee / comm_data.family_count);
                }
            }


            public void building_status()
            {
                BuildingManager instance = Singleton<BuildingManager>.instance;
                updateOnce = false;
                comm_data.have_toll_station = false;
                for (int i = 0; i < instance.m_buildings.m_buffer.Count<Building>(); i++)
                {
                    if (instance.m_buildings.m_buffer[i].m_flags.IsFlagSet(Building.Flags.Created) && (instance.m_buildings.m_buffer[i].m_productionRate!=0) && !instance.m_buildings.m_buffer[i].m_flags.IsFlagSet(Building.Flags.Deleted) && !instance.m_buildings.m_buffer[i].m_flags.IsFlagSet(Building.Flags.Untouchable))
                    {
                        if (false)
                        {
                        }
                        else if (is_special_building((ushort)i) == 2)
                        {
                            comm_data.have_toll_station = true;
                        }
                        else if (is_special_building((ushort)i) == 3)
                        {
                            comm_data.have_city_resource_department = true;
                            process_city_resource_department_building((ushort)i, instance.m_buildings.m_buffer[i]);
                        }
                    }
                }
                updateOnce = true;
            }


            void process_city_resource_department_building (ushort buildingID, Building buildingData)
            {
                int num27 = 0;
                int num28 = 0;
                int num29 = 0;
                int value = 0;
                int num34 = 0;
                TransferManager.TransferReason incomingTransferReason = default(TransferManager.TransferReason);

                //Foods
                incomingTransferReason = TransferManager.TransferReason.Food;
                if (incomingTransferReason != TransferManager.TransferReason.None)
                {
                    CalculateGuestVehicles(buildingID, ref buildingData, incomingTransferReason, ref num27, ref num28, ref num29, ref value);
                    buildingData.m_tempImport = (byte)Mathf.Clamp(value, (int)buildingData.m_tempImport, 255);
                }

                num34 = 16000 - comm_data.building_buffer3[buildingID] - num29;
                if (num34 >= 0)
                {
                    TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                    offer.Priority = num34 / 500;
                    offer.Building = buildingID;
                    offer.Position = buildingData.m_position;
                    offer.Amount = 1;
                    offer.Active = false;
                    Singleton<TransferManager>.instance.AddIncomingOffer(incomingTransferReason, offer);
                }

                if (comm_data.building_buffer3[buildingID] > 0 && !comm_data.isFoodsGetted)
                {
                    if (comm_data.citizen_count >= 1)
                    {
                        if (comm_data.building_buffer3[buildingID] - comm_data.citizen_count > 0)
                        {
                            comm_data.building_buffer3[buildingID] -= (ushort)(comm_data.citizen_count);
                        } else
                        {
                            comm_data.building_buffer3[buildingID] = 0;
                        }
                    }
                    comm_data.isFoodsGetted = true;
                }
                comm_data.allFoods += comm_data.building_buffer3[buildingID];

                //Petrol
                incomingTransferReason = TransferManager.TransferReason.Petrol;
                num27 = 0;
                num28 = 0;
                num29 = 0;
                value = 0;
                num34 = 0;
                if (incomingTransferReason != TransferManager.TransferReason.None)
                {
                    CalculateGuestVehicles(buildingID, ref buildingData, incomingTransferReason, ref num27, ref num28, ref num29, ref value);
                    buildingData.m_tempImport = (byte)Mathf.Clamp(value, (int)buildingData.m_tempImport, 255);
                }

                num34 = 16000 - comm_data.building_buffer2[buildingID] - num29;
                if (num34 >= 0)
                {
                    TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                    offer.Priority = num34 / 500;
                    offer.Building = buildingID;
                    offer.Position = buildingData.m_position;
                    offer.Amount = 1;
                    offer.Active = false;
                    Singleton<TransferManager>.instance.AddIncomingOffer(incomingTransferReason, offer);
                }

                if (comm_data.building_buffer2[buildingID] > 0 && !comm_data.isPetrolsGetted)
                {
                    if (comm_data.allVehiclesFinal >= 1)
                    {
                        if (comm_data.building_buffer2[buildingID] - comm_data.allVehiclesFinal > 0)
                        {
                            comm_data.building_buffer2[buildingID] -= (ushort)(comm_data.allVehiclesFinal);
                        } else
                        {
                            comm_data.building_buffer2[buildingID] = 0;
                        }
                    }
                    comm_data.isPetrolsGetted = true;
                }
                comm_data.allPetrols += comm_data.building_buffer2[buildingID];

                //Coal
                incomingTransferReason = TransferManager.TransferReason.Coal;
                num27 = 0;
                num28 = 0;
                num29 = 0;
                value = 0;
                num34 = 0;
                if (incomingTransferReason != TransferManager.TransferReason.None)
                {
                    CalculateGuestVehicles(buildingID, ref buildingData, incomingTransferReason, ref num27, ref num28, ref num29, ref value);
                    buildingData.m_tempImport = (byte)Mathf.Clamp(value, (int)buildingData.m_tempImport, 255);
                }

                num34 = 16000 - comm_data.building_buffer1[buildingID] - num29;
                if (num34 >= 0)
                {
                    TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                    offer.Priority = num34 / 500;
                    offer.Building = buildingID;
                    offer.Position = buildingData.m_position;
                    offer.Amount = 1;
                    offer.Active = false;
                    Singleton<TransferManager>.instance.AddIncomingOffer(incomingTransferReason, offer);
                }

                if (comm_data.building_buffer1[buildingID] > 0 && !comm_data.isCoalsGetted)
                {
                    if (pc_PrivateBuildingAI.allBuildingsFinal >= 1)
                    {
                        if (comm_data.building_buffer1[buildingID] - pc_PrivateBuildingAI.allBuildingsFinal > 0)
                        {
                            comm_data.building_buffer1[buildingID] -= (ushort)(pc_PrivateBuildingAI.allBuildingsFinal);
                        } else
                        {
                            comm_data.building_buffer1[buildingID] = 0;
                        }
                    }
                    comm_data.isCoalsGetted = true;
                }
                comm_data.allCoals += comm_data.building_buffer1[buildingID];

                //Lumber
                incomingTransferReason = TransferManager.TransferReason.Lumber;
                num27 = 0;
                num28 = 0;
                num29 = 0;
                value = 0;
                num34 = 0;
                if (incomingTransferReason != TransferManager.TransferReason.None)
                {
                    CalculateGuestVehicles(buildingID, ref buildingData, incomingTransferReason, ref num27, ref num28, ref num29, ref value);
                    buildingData.m_tempImport = (byte)Mathf.Clamp(value, (int)buildingData.m_tempImport, 255);
                }

                num34 = 16000 - comm_data.building_buffer4[buildingID] - num29;
                if (num34 >= 0)
                {
                    TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                    offer.Priority = num34 / 500;
                    offer.Building = buildingID;
                    offer.Position = buildingData.m_position;
                    offer.Amount = 1;
                    offer.Active = false;
                    Singleton<TransferManager>.instance.AddIncomingOffer(incomingTransferReason, offer);
                }

                if (comm_data.building_buffer4[buildingID] > 0 && !comm_data.isLumbersGetted)
                {
                    if (pc_PrivateBuildingAI.allBuildingsFinal >= 1)
                    {
                        if (comm_data.building_buffer4[buildingID] - pc_PrivateBuildingAI.allBuildingsFinal > 0)
                        {
                            comm_data.building_buffer4[buildingID] -= (ushort)(pc_PrivateBuildingAI.allBuildingsFinal);
                        }
                        else
                        {
                            comm_data.building_buffer4[buildingID] = 0;
                        }
                    }
                    comm_data.isLumbersGetted = true;
                }
                comm_data.allLumbers += comm_data.building_buffer4[buildingID];
            }

            protected void CalculateGuestVehicles(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int count, ref int cargo, ref int capacity, ref int outside)
            {
                VehicleManager instance = Singleton<VehicleManager>.instance;
                ushort num = data.m_guestVehicles;
                int num2 = 0;
                while (num != 0)
                {
                    if ((TransferManager.TransferReason)instance.m_vehicles.m_buffer[(int)num].m_transferType == material)
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

            public static byte is_special_building(ushort id)
            {
                BuildingManager instance = Singleton<BuildingManager>.instance;

                //bank
                if (instance.m_buildings.m_buffer[id].Info.m_buildingAI.GetConstructionCost() == 1668000)
                {
                    //DebugLog.LogToFileOnly(instance.m_buildings.m_buffer[id].Info.m_buildingAI.GetMaintenanceCost().ToString());
                    //DebugLog.LogToFileOnly(instance.m_buildings.m_buffer[id].Info.m_buildingAI.GetElectricityConsumption().ToString());
                    //DebugLog.LogToFileOnly(instance.m_buildings.m_buffer[id].Info.m_buildingAI.GetWaterConsumption().ToString());
                    return 1;
                }

                //toll
                if (instance.m_buildings.m_buffer[id].Info.m_buildingAI.GetConstructionCost() == 108600)
                {
                    //DebugLog.LogToFileOnly(instance.m_buildings.m_buffer[id].Info.m_buildingAI.GetMaintenanceCost().ToString());
                    //DebugLog.LogToFileOnly(instance.m_buildings.m_buffer[id].Info.m_buildingAI.GetElectricityConsumption().ToString());
                    //DebugLog.LogToFileOnly(instance.m_buildings.m_buffer[id].Info.m_buildingAI.GetWaterConsumption().ToString());
                    //DebugLog.LogToFileOnly(instance.m_buildings.m_buffer[id].m_flags.ToString());
                    return 2;
                }

                //DebugLog.LogToFileOnly(instance.m_buildings.m_buffer[id].Info.m_buildingAI.GetConstructionCost().ToString());
                //city_resource_department
                if (instance.m_buildings.m_buffer[id].Info.m_buildingAI.GetConstructionCost() == 1008600)
                {
                    //DebugLog.LogToFileOnly(instance.m_buildings.m_buffer[id].Info.m_buildingAI.GetMaintenanceCost().ToString());
                    //DebugLog.LogToFileOnly(instance.m_buildings.m_buffer[id].Info.m_buildingAI.GetElectricityConsumption().ToString());
                    //DebugLog.LogToFileOnly(instance.m_buildings.m_buffer[id].Info.m_buildingAI.GetWaterConsumption().ToString());
                    //DebugLog.LogToFileOnly(instance.m_buildings.m_buffer[id].m_flags.ToString());
                    return 3;
                }

                return 0;
            }

            //public void citizen_status()
            //{
            //    comm_data.citizen_count = (int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_populationData.m_finalCount;
            //}

            //public void caculate_profit()
            //{
            //if (comm_data.update_outside_count > 64)
            //{
            //    comm_data.update_outside_count = 0;
            //}
            //comm_data.update_outside_count++;
            //lumber
            /*if ((pc_PrivateBuildingAI.lumber_from_outside_count_final + pc_PrivateBuildingAI.lumber_to_industy_count_final) != 0)
            {
                pc_PrivateBuildingAI.lumber_import_ratio = (float)pc_PrivateBuildingAI.lumber_from_outside_count_final / (float)(pc_PrivateBuildingAI.lumber_from_outside_count_final + pc_PrivateBuildingAI.lumber_to_industy_count_final);
            }

            if ((pc_PrivateBuildingAI.lumber_to_outside_count_final + pc_PrivateBuildingAI.lumber_to_industy_count_final) != 0)
            {
                pc_PrivateBuildingAI.lumber_export_ratio = (float)pc_PrivateBuildingAI.lumber_to_outside_count_final / (float)(pc_PrivateBuildingAI.lumber_to_outside_count_final + pc_PrivateBuildingAI.lumber_to_industy_count_final);
            }

            //food
            if ((pc_PrivateBuildingAI.food_from_outside_count_final + pc_PrivateBuildingAI.food_to_industy_count_final) != 0)
            {
                pc_PrivateBuildingAI.food_import_ratio = (float)pc_PrivateBuildingAI.food_from_outside_count_final / (float)(pc_PrivateBuildingAI.food_from_outside_count_final + pc_PrivateBuildingAI.food_to_industy_count_final);
            }

            if ((pc_PrivateBuildingAI.food_to_outside_count_final + pc_PrivateBuildingAI.food_to_industy_count_final) != 0)
            {
                pc_PrivateBuildingAI.food_export_ratio = (float)pc_PrivateBuildingAI.food_to_outside_count_final / (float)(pc_PrivateBuildingAI.food_to_outside_count_final + pc_PrivateBuildingAI.food_to_industy_count_final);
            }

            //petrol
            if ((pc_PrivateBuildingAI.Petrol_from_outside_count_final + pc_PrivateBuildingAI.Petrol_to_industy_count_final) != 0)
            {
                pc_PrivateBuildingAI.petrol_import_ratio = (float)pc_PrivateBuildingAI.Petrol_from_outside_count_final / (float)(pc_PrivateBuildingAI.Petrol_from_outside_count_final + pc_PrivateBuildingAI.Petrol_to_industy_count_final);
            }

            if ((pc_PrivateBuildingAI.Petrol_to_outside_count_final + pc_PrivateBuildingAI.Petrol_to_industy_count_final) != 0)
            {
                pc_PrivateBuildingAI.petrol_export_ratio = (float)pc_PrivateBuildingAI.Petrol_to_outside_count_final / (float)(pc_PrivateBuildingAI.Petrol_to_outside_count_final + pc_PrivateBuildingAI.Petrol_to_industy_count_final);
            }

            //coal
            if ((pc_PrivateBuildingAI.coal_from_outside_count_final + pc_PrivateBuildingAI.coal_to_industy_count_final) != 0)
            {
                pc_PrivateBuildingAI.coal_import_ratio = (float)pc_PrivateBuildingAI.coal_from_outside_count_final / (float)(pc_PrivateBuildingAI.coal_from_outside_count_final + pc_PrivateBuildingAI.coal_to_industy_count_final);
            }

            if ((pc_PrivateBuildingAI.coal_to_outside_count_final + pc_PrivateBuildingAI.coal_to_industy_count_final) != 0)
            {
                pc_PrivateBuildingAI.coal_export_ratio = (float)pc_PrivateBuildingAI.coal_to_outside_count_final / (float)(pc_PrivateBuildingAI.coal_to_outside_count_final + pc_PrivateBuildingAI.coal_to_industy_count_final);
            }

            //logs
            if ((pc_PrivateBuildingAI.logs_from_outside_count_final + pc_PrivateBuildingAI.logs_to_industy_count_final) != 0)
            {
                pc_PrivateBuildingAI.log_import_ratio = (float)pc_PrivateBuildingAI.logs_from_outside_count_final / (float)(pc_PrivateBuildingAI.logs_from_outside_count_final + pc_PrivateBuildingAI.logs_to_industy_count_final);
            }

            if ((pc_PrivateBuildingAI.logs_to_outside_count_final + pc_PrivateBuildingAI.logs_to_industy_count_final) != 0)
            {
                pc_PrivateBuildingAI.log_export_ratio = (float)pc_PrivateBuildingAI.logs_to_outside_count_final / (float)(pc_PrivateBuildingAI.logs_to_outside_count_final + pc_PrivateBuildingAI.logs_to_industy_count_final);
            }

            //grain
            if ((pc_PrivateBuildingAI.Grain_from_outside_count_final + pc_PrivateBuildingAI.Grain_to_industy_count_final) != 0)
            {
                pc_PrivateBuildingAI.grain_import_ratio = (float)pc_PrivateBuildingAI.Grain_from_outside_count_final / (float)(pc_PrivateBuildingAI.Grain_from_outside_count_final + pc_PrivateBuildingAI.Grain_to_industy_count_final);
            }

            if ((pc_PrivateBuildingAI.Grain_to_outside_count_final + pc_PrivateBuildingAI.Grain_to_industy_count_final) != 0)
            {
                pc_PrivateBuildingAI.grain_export_ratio = (float)pc_PrivateBuildingAI.Grain_to_outside_count_final / (float)(pc_PrivateBuildingAI.Grain_to_outside_count_final + pc_PrivateBuildingAI.Grain_to_industy_count_final);
            }

            //oil
            if ((pc_PrivateBuildingAI.oil_from_outside_count_final + pc_PrivateBuildingAI.oil_to_industy_count_final) != 0)
            {
                pc_PrivateBuildingAI.oil_import_ratio = (float)pc_PrivateBuildingAI.oil_from_outside_count_final / (float)(pc_PrivateBuildingAI.oil_from_outside_count_final + pc_PrivateBuildingAI.oil_to_industy_count_final);
            }

            if ((pc_PrivateBuildingAI.oil_to_outside_count_final + pc_PrivateBuildingAI.oil_to_industy_count_final) != 0)
            {
                pc_PrivateBuildingAI.oil_export_ratio = (float)pc_PrivateBuildingAI.oil_to_outside_count_final / (float)(pc_PrivateBuildingAI.oil_to_outside_count_final + pc_PrivateBuildingAI.oil_to_industy_count_final);
            }

            //ore
            if ((pc_PrivateBuildingAI.ore_from_outside_count_final + pc_PrivateBuildingAI.ore_to_industy_count_final) != 0)
            {
                pc_PrivateBuildingAI.ore_import_ratio = (float)pc_PrivateBuildingAI.ore_from_outside_count_final / (float)(pc_PrivateBuildingAI.ore_from_outside_count_final + pc_PrivateBuildingAI.ore_to_industy_count_final);
            }

            if ((pc_PrivateBuildingAI.ore_to_outside_count_final + pc_PrivateBuildingAI.ore_to_industy_count_final) != 0)
            {
                pc_PrivateBuildingAI.ore_export_ratio = (float)pc_PrivateBuildingAI.ore_to_outside_count_final / (float)(pc_PrivateBuildingAI.ore_to_outside_count_final + pc_PrivateBuildingAI.ore_to_industy_count_final);
            }

            //good
            if ((pc_PrivateBuildingAI.shop_get_goods_from_local_count_level1_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final + pc_PrivateBuildingAI.shop_get_goods_from_outside_count_final) != 0)
            {
                pc_PrivateBuildingAI.good_import_ratio = (float)pc_PrivateBuildingAI.shop_get_goods_from_outside_count_final / (float)(pc_PrivateBuildingAI.shop_get_goods_from_local_count_level1_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final + pc_PrivateBuildingAI.shop_get_goods_from_outside_count_final);
            }

            if ((pc_PrivateBuildingAI.shop_get_goods_from_local_count_level1_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final + pc_PrivateBuildingAI.industy_goods_to_outside_count_final) != 0)
            {
                pc_PrivateBuildingAI.good_export_ratio = (float)pc_PrivateBuildingAI.industy_goods_to_outside_count_final / (float)(pc_PrivateBuildingAI.shop_get_goods_from_local_count_level1_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final + pc_PrivateBuildingAI.industy_goods_to_outside_count_final);
            }

            if ((pc_PrivateBuildingAI.shop_get_goods_from_local_count_level1_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final + pc_PrivateBuildingAI.shop_get_goods_from_outside_count) != 0)
            {
                pc_PrivateBuildingAI.good_level2_ratio = (float)(pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final) / (float)(pc_PrivateBuildingAI.shop_get_goods_from_local_count_level1_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final + pc_PrivateBuildingAI.shop_get_goods_from_outside_count);
            }

            if ((pc_PrivateBuildingAI.shop_get_goods_from_local_count_level1_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final + pc_PrivateBuildingAI.shop_get_goods_from_outside_count) != 0)
            {
                pc_PrivateBuildingAI.good_level3_ratio = (float)(pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final) / (float)(pc_PrivateBuildingAI.shop_get_goods_from_local_count_level1_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final + pc_PrivateBuildingAI.shop_get_goods_from_outside_count);
            }*/

            //}

            public void VehicleStatus()
            {
                VehicleManager instance = Singleton<VehicleManager>.instance;
                //System.Random rand = new System.Random();
                for (int i = 0; i < instance.m_vehicles.m_buffer.Count<Vehicle>(); i++)
                {
                    Vehicle vehicle = instance.m_vehicles.m_buffer[i];
                    if (vehicle.m_flags.IsFlagSet(Vehicle.Flags.Created) && !vehicle.m_flags.IsFlagSet(Vehicle.Flags.Deleted))
                    {
                        if ((vehicle.Info.m_vehicleType == VehicleInfo.VehicleType.Car) && (vehicle.Info.m_class.m_subService != ItemClass.SubService.PublicTransportTaxi))
                        {
                            if (!vehicle.m_flags.IsFlagSet(Vehicle.Flags.Stopped))
                            {
                                comm_data.vehical_transfer_time[i] = (ushort)(comm_data.vehical_transfer_time[i] + 1);
                                if ((TransferManager.TransferReason)vehicle.m_transferType != TransferManager.TransferReason.DummyCar && (TransferManager.TransferReason)vehicle.m_transferType != TransferManager.TransferReason.DummyPlane && (TransferManager.TransferReason)vehicle.m_transferType != TransferManager.TransferReason.DummyTrain && (TransferManager.TransferReason)vehicle.m_transferType != TransferManager.TransferReason.DummyShip)
                                {
                                    if (vehicle.Info.m_vehicleAI is AmbulanceAI || vehicle.Info.m_vehicleAI is PoliceCarAI || vehicle.Info.m_vehicleAI is DisasterResponseVehicleAI || vehicle.Info.m_vehicleAI is HearseAI)
                                    {
                                        comm_data.allVehicles++;
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)10000, vehicle.Info.m_class);
                                    }

                                    if (vehicle.Info.m_vehicleAI is GarbageTruckAI || vehicle.Info.m_vehicleAI is FireTruckAI || vehicle.Info.m_vehicleAI is MaintenanceTruckAI)
                                    {
                                        comm_data.allVehicles += 2;
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)20000, vehicle.Info.m_class);
                                    }

                                    if (vehicle.Info.m_vehicleAI is SnowTruckAI || vehicle.Info.m_vehicleAI is ParkMaintenanceVehicleAI || vehicle.Info.m_vehicleAI is WaterTruckAI)
                                    {
                                        comm_data.allVehicles += 2;
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)20000, vehicle.Info.m_class);
                                    }

                                    if (vehicle.Info.m_vehicleAI is PassengerShipAI || vehicle.Info.m_vehicleAI is PassengerFerryAI || vehicle.Info.m_vehicleAI is CargoShipAI)
                                    {
                                        comm_data.allVehicles += 4;
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)40000, vehicle.Info.m_class);
                                    }

                                    if (vehicle.Info.m_vehicleAI is PassengerPlaneAI || vehicle.Info.m_vehicleAI is PassengerBlimpAI)
                                    {
                                        comm_data.allVehicles += 8;
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)80000, vehicle.Info.m_class);
                                    }

                                    if (vehicle.Info.m_vehicleAI is PassengerTrainAI || vehicle.Info.m_vehicleAI is CargoTrainAI)
                                    {
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)60000, vehicle.Info.m_class);
                                    }

                                    if (vehicle.Info.m_vehicleAI is PoliceCopterAI || vehicle.Info.m_vehicleAI is FireCopterAI || vehicle.Info.m_vehicleAI is DisasterResponseCopterAI || vehicle.Info.m_vehicleAI is AmbulanceCopterAI)
                                    {
                                        comm_data.allVehicles += 8;
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)80000, vehicle.Info.m_class);
                                    }

                                    if (vehicle.Info.m_vehicleAI is BusAI)
                                    {
                                        comm_data.allVehicles += 2;
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)20000, vehicle.Info.m_class);
                                    }

                                    if (vehicle.Info.m_vehicleAI is CableCarAI || vehicle.Info.m_vehicleAI is TramAI)
                                    {
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)30000, vehicle.Info.m_class);
                                    }
                                }
                            }
                            else
                            {
                                comm_data.vehical_transfer_time[i] = 0;
                            }
                        }
                    }
                    else
                    {
                        comm_data.vehical_transfer_time[i] = 0;
                    }
                }
            }
        }

        public class ThreadingRealCityStatsMod : ThreadingExtensionBase
        {
            public override void OnAfterSimulationFrame()
            {
                base.OnAfterSimulationFrame();
                if (Loader.CurrentLoadMode == LoadMode.LoadGame || Loader.CurrentLoadMode == LoadMode.NewGame)
                {
                    uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
                    int num4 = (int)(currentFrameIndex & 15u);
                    int num5 = num4 * 1024;
                    int num6 = (num4 + 1) * 1024 - 1;
                    //DebugLog.LogToFileOnly("currentFrameIndex num2 = " + currentFrameIndex.ToString());
                    VehicleManager instance = Singleton<VehicleManager>.instance;

                    
                    for (int i = num5; i <= num6; i = i + 1)
                    {
                        if (comm_data.have_toll_station)
                        {
                            Vehicle vehicle = instance.m_vehicles.m_buffer[i];
                            Building building = Singleton<BuildingManager>.instance.m_buildings.m_buffer[vehicle.m_sourceBuilding];
                            Building building1 = Singleton<BuildingManager>.instance.m_buildings.m_buffer[vehicle.m_targetBuilding];
                            if (vehicle.m_flags.IsFlagSet(Vehicle.Flags.Created) && !vehicle.m_flags.IsFlagSet(Vehicle.Flags.Deleted))
                            {
                                ushort num = FindToll(vehicle.GetFramePosition(currentFrameIndex), 16f);
                                if (num != 0)
                                {
                                    bool flag1 = building.m_flags.IsFlagSet(Building.Flags.Untouchable) && building1.m_flags.IsFlagSet(Building.Flags.Untouchable);

                                    if (vehicle.m_sourceBuilding != 0)
                                    {
                                        if (!building.m_flags.IsFlagSet(Building.Flags.Untouchable) && building1.m_flags.IsFlagSet(Building.Flags.Untouchable))
                                        {
                                        }
                                    }

                                    if (flag1 && (!comm_data.vehical_flag[i]))
                                    {
                                        if ((vehicle.Info.m_vehicleAI is PassengerCarAI) || (vehicle.Info.m_vehicleAI is CargoTruckAI))
                                        {
                                            comm_data.vehical_flag[i] = true;
                                            Singleton<EconomyManager>.instance.AddPrivateIncome(100, ItemClass.Service.Road, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                                        }
                                    }
                                }
                            }
                        } // toll station
                    }
                }
            }

            public static ushort FindToll(Vector3 pos, float maxDistance)
            {
                int num = Mathf.Max((int)((pos.x - maxDistance) / 64f + 135f), 0);
                int num2 = Mathf.Max((int)((pos.z - maxDistance) / 64f + 135f), 0);
                int num3 = Mathf.Min((int)((pos.x + maxDistance) / 64f + 135f), 269);
                int num4 = Mathf.Min((int)((pos.z + maxDistance) / 64f + 135f), 269);
                ushort result = 0;
                BuildingManager building = Singleton<BuildingManager>.instance;
                float num5 = maxDistance * maxDistance;
                for (int i = num2; i <= num4; i++)
                {
                    for (int j = num; j <= num3; j++)
                    {
                        ushort num6 = building.m_buildingGrid[i * 270 + j];
                        int num7 = 0;
                        while (num6 != 0)
                        {
                            BuildingInfo info = building.m_buildings.m_buffer[(int)num6].Info;
                            if (RealCity.EconomyExtension.is_special_building((ushort)num6) == 2)
                            {
                                if ((building.m_buildings.m_buffer[(int)num6].m_productionRate!=0) || (!building.m_buildings.m_buffer[(int)num6].m_flags.IsFlagSet(Building.Flags.Deleted)))
                                {
                                    float num8 = Vector3.SqrMagnitude(pos - building.m_buildings.m_buffer[(int)num6].m_position);
                                    if (num8 < num5)
                                    {
                                        result = num6;
                                        num5 = num8;
                                        break;
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
                return result;
            }

        }
    }
}

