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

        public static int foodStillNeeded = 0;
        public static int lumberStillNeeded = 0;
        public static int coalStillNeeded = 0;
        public static int PetrolStillNeeded = 0;

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
            language.LanguageSwitch((byte)language_idex);
        }

        public void OnDisabled()
        {
            RealCity.IsEnabled = false;
            language.LanguageSwitch((byte)language_idex);
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
            language.LanguageSwitch(comm_data.last_language);
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
            language.LanguageSwitch((byte)language_idex);
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
                        GenerateTips();

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
                            foodStillNeeded = comm_data.citizen_count;
                            lumberStillNeeded = (pc_PrivateBuildingAI.allBuildingsFinal << 1);
                            coalStillNeeded = (pc_PrivateBuildingAI.allBuildingsFinal);
                            PetrolStillNeeded = comm_data.allVehiclesFinal;
                            BuildingStatus();
                            comm_data.isCoalsGettedFinal = (coalStillNeeded <= 0);
                            comm_data.isFoodsGettedFinal = (foodStillNeeded <= 0); ;
                            comm_data.isPetrolsGettedFinal = (PetrolStillNeeded <= 0);
                            comm_data.isLumbersGettedFinal = (lumberStillNeeded <= 0);

                            CitizenStatus();
                            Politics.parliamentCount--;
                            if (Politics.parliamentCount < 0)
                            {
                                Politics.parliamentCount = 4;
                            }
                        }

                        comm_data.allVehicles = 0;
                        VehicleStatus();
                        comm_data.allVehiclesFinal = comm_data.allVehicles;
                        CaculateCitizenTransportFee();
                        comm_data.update_money_count++;
                        if (comm_data.update_money_count == 17)
                        {
                            comm_data.update_money_count = 0;
                        }
                        pc_EconomyManager.clean_current(comm_data.update_money_count);


                        comm_data.prev_time = comm_data.current_time;
                    }
                    PoliticsUI.refesh_onece = true;
                    RealCityUI.refesh_onece = true;
                    EcnomicUI.refesh_onece = true;
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

            public void GenerateTips()
            {
                tip1_message_forgui = language.TipAndChirperMessage[5];

                tip2_message_forgui = language.TipAndChirperMessage[7];

                if (!comm_data.have_city_resource_department)
                {
                    tip3_message_forgui = language.TipAndChirperMessage[8];
                }
                else
                {
                    tip3_message_forgui = "";
                }

                if (!comm_data.isFoodsGettedFinal)
                {
                    tip4_message_forgui = language.TipAndChirperMessage[9];
                }
                else
                {
                    tip4_message_forgui = "";
                }

                if (!comm_data.isLumbersGettedFinal || !comm_data.isCoalsGettedFinal)
                {
                    tip5_message_forgui = language.TipAndChirperMessage[10];
                }
                else
                {
                    tip5_message_forgui = "";
                }

                if (!comm_data.isPetrolsGettedFinal)
                {
                    tip6_message_forgui = language.TipAndChirperMessage[11];
                }
                else
                {
                    tip6_message_forgui = "";
                }

                if (!comm_data.isHellMode)
                {
                    tip7_message_forgui = language.OptionUI[3];
                }
                else
                {
                    tip7_message_forgui = "";
                }
            }

            public void CaculateCitizenTransportFee()
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


            public void BuildingStatus()
            {
                BuildingManager instance = Singleton<BuildingManager>.instance;
                updateOnce = false;
                comm_data.have_toll_station = false;
                pc_OutsideConnectionAI.have_garbage_building = false;
                for (int i = 0; i < instance.m_buildings.m_buffer.Count<Building>(); i++)
                {
                    if (instance.m_buildings.m_buffer[i].m_flags.IsFlagSet(Building.Flags.Created) && (instance.m_buildings.m_buffer[i].m_productionRate != 0) && !instance.m_buildings.m_buffer[i].m_flags.IsFlagSet(Building.Flags.Deleted) && !instance.m_buildings.m_buffer[i].m_flags.IsFlagSet(Building.Flags.Untouchable))
                    {
                        if (false)
                        {
                        }
                        else if (IsSpecialBuilding((ushort)i) == 2)
                        {
                            comm_data.have_toll_station = true;
                        }
                        else if (IsSpecialBuilding((ushort)i) == 3)
                        {
                            comm_data.have_city_resource_department = true;
                            ProcessCityResourceDepartmentBuilding((ushort)i, instance.m_buildings.m_buffer[i]);
                        }

                        if (instance.m_buildings.m_buffer[i].Info.m_class.m_service == ItemClass.Service.Garbage)
                        {
                            pc_OutsideConnectionAI.have_garbage_building = true;
                        }
                    }
                }
                updateOnce = true;
            }


            void ProcessCityResourceDepartmentBuilding (ushort buildingID, Building buildingData)
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

                num34 = 18000 - comm_data.building_buffer3[buildingID] - num29;
                if (num34 >= 0)
                {
                    TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                    offer.Priority = num34 / 1000;
                    if (offer.Priority > 7)
                    {
                        offer.Priority = 7;
                    }
                    offer.Building = buildingID;
                    offer.Position = buildingData.m_position;
                    offer.Amount = 1;
                    offer.Active = false;
                    Singleton<TransferManager>.instance.AddIncomingOffer(incomingTransferReason, offer);
                }

                if (comm_data.building_buffer3[buildingID] > 0 && !comm_data.isFoodsGetted)
                {
                    if (foodStillNeeded >= 1)
                    {
                        if (comm_data.building_buffer3[buildingID] - (foodStillNeeded) > 0)
                        {
                            comm_data.building_buffer3[buildingID] -= (ushort)(foodStillNeeded);
                            foodStillNeeded = 0;
                        } else
                        {
                            foodStillNeeded -= comm_data.building_buffer3[buildingID];
                            comm_data.building_buffer3[buildingID] = 0;
                        }
                    }
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

                num34 = 18000 - comm_data.building_buffer2[buildingID] - num29;
                if (num34 >= 0)
                {
                    TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                    offer.Priority = num34 / 1000;
                    if (offer.Priority > 7)
                    {
                        offer.Priority = 7;
                    }
                    offer.Building = buildingID;
                    offer.Position = buildingData.m_position;
                    offer.Amount = 1;
                    offer.Active = false;
                    Singleton<TransferManager>.instance.AddIncomingOffer(incomingTransferReason, offer);
                }

                if (comm_data.building_buffer2[buildingID] > 0 && !comm_data.isPetrolsGetted)
                {
                    if (PetrolStillNeeded >= 1)
                    {
                        if (comm_data.building_buffer2[buildingID] - PetrolStillNeeded > 0)
                        {
                            PetrolStillNeeded = 0;
                            comm_data.building_buffer2[buildingID] -= (ushort)(PetrolStillNeeded);
                        } else
                        {
                            PetrolStillNeeded -= comm_data.building_buffer2[buildingID];
                            comm_data.building_buffer2[buildingID] = 0;
                        }
                    }
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

                num34 = 18000 - comm_data.building_buffer1[buildingID] - num29;
                if (num34 >= 0)
                {
                    TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                    offer.Priority = num34 / 1000;
                    if (offer.Priority > 7)
                    {
                        offer.Priority = 7;
                    }
                    offer.Building = buildingID;
                    offer.Position = buildingData.m_position;
                    offer.Amount = 1;
                    offer.Active = false;
                    Singleton<TransferManager>.instance.AddIncomingOffer(incomingTransferReason, offer);
                }

                if (comm_data.building_buffer1[buildingID] > 0 && !comm_data.isCoalsGetted)
                {
                    if (coalStillNeeded >= 1)
                    {
                        if (comm_data.building_buffer1[buildingID] - coalStillNeeded > 0)
                        {
                            comm_data.building_buffer1[buildingID] -= (ushort)(coalStillNeeded);
                            coalStillNeeded = 0;
                        } else
                        {
                            coalStillNeeded -= comm_data.building_buffer1[buildingID];
                            comm_data.building_buffer1[buildingID] = 0;
                        }
                    }
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

                num34 = 18000 - comm_data.building_buffer4[buildingID] - num29;
                if (num34 >= 0)
                {
                    TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
                    offer.Priority = num34 / 1000;
                    if (offer.Priority > 7)
                    {
                        offer.Priority = 7;
                    }
                    offer.Building = buildingID;
                    offer.Position = buildingData.m_position;
                    offer.Amount = 1;
                    offer.Active = false;
                    Singleton<TransferManager>.instance.AddIncomingOffer(incomingTransferReason, offer);
                }

                if (comm_data.building_buffer4[buildingID] > 0 && !comm_data.isLumbersGetted)
                {
                    if (lumberStillNeeded >= 1)
                    {
                        if (comm_data.building_buffer4[buildingID] - (lumberStillNeeded) > 0)
                        {
                            comm_data.building_buffer4[buildingID] -= (ushort)(lumberStillNeeded);
                            lumberStillNeeded = 0;
                        }
                        else
                        {
                            lumberStillNeeded -= comm_data.building_buffer4[buildingID];
                            comm_data.building_buffer4[buildingID] = 0;
                        }
                    }
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

            public static byte IsSpecialBuilding(ushort id)
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
                if (instance.m_buildings.m_buffer[id].Info.m_buildingAI.GetConstructionCost() == 208600)
                {
                    //DebugLog.LogToFileOnly(instance.m_buildings.m_buffer[id].Info.m_buildingAI.GetMaintenanceCost().ToString());
                    //DebugLog.LogToFileOnly(instance.m_buildings.m_buffer[id].Info.m_buildingAI.GetElectricityConsumption().ToString());
                    //DebugLog.LogToFileOnly(instance.m_buildings.m_buffer[id].Info.m_buildingAI.GetWaterConsumption().ToString());
                    //DebugLog.LogToFileOnly(instance.m_buildings.m_buffer[id].m_flags.ToString());
                    return 3;
                }

                return 0;
            }

            public void CitizenStatus()
            {
                if (Politics.parliamentCount == 2)
                {
                    GetSeats(false);
                    //DebugLog.LogToFileOnly("cPartySeats = " + Politics.cPartySeats.ToString());
                    //DebugLog.LogToFileOnly("gPartySeats = " + Politics.gPartySeats.ToString());
                    //DebugLog.LogToFileOnly("sPartySeats = " + Politics.sPartySeats.ToString());
                    //DebugLog.LogToFileOnly("lPartySeats = " + Politics.lPartySeats.ToString());
                    //DebugLog.LogToFileOnly("nPartySeats = " + Politics.nPartySeats.ToString());
                    CreateGoverment();
                } else
                {
                    GetSeats(true);
                    CreateGoverment();
                }


            }

            public void CreateGoverment()
            {
                if (Politics.cPartySeats >= 50)
                {
                    //c only
                    Politics.case1 = true;
                    Politics.case2 = false;
                    Politics.case3 = false;
                    Politics.case4 = false;
                    Politics.case5 = false;
                    Politics.case6 = false;
                    Politics.case7 = false;
                    Politics.case8 = false;

                } 
                else if (Politics.gPartySeats >= 50)
                {
                    Politics.case1 = false;
                    Politics.case2 = true;
                    Politics.case3 = false;
                    Politics.case4 = false;
                    Politics.case5 = false;
                    Politics.case6 = false;
                    Politics.case7 = false;
                    Politics.case8 = false;
                }
                else if (Politics.sPartySeats >= 50)
                {
                    Politics.case1 = false;
                    Politics.case2 = false;
                    Politics.case3 = true;
                    Politics.case4 = false;
                    Politics.case5 = false;
                    Politics.case6 = false;
                    Politics.case7 = false;
                    Politics.case8 = false;
                }
                else if (Politics.lPartySeats >= 50)
                {
                    Politics.case1 = false;
                    Politics.case2 = false;
                    Politics.case3 = false;
                    Politics.case4 = true;
                    Politics.case5 = false;
                    Politics.case6 = false;
                    Politics.case7 = false;
                    Politics.case8 = false;
                }
                else if (Politics.nPartySeats >= 50)
                {
                    Politics.case1 = false;
                    Politics.case2 = false;
                    Politics.case3 = false;
                    Politics.case4 = false;
                    Politics.case5 = true;
                    Politics.case6 = false;
                    Politics.case7 = false;
                    Politics.case8 = false;
                }
                else if (Politics.sPartySeats + Politics.gPartySeats >= 50)
                {
                    Politics.case1 = false;
                    Politics.case2 = false;
                    Politics.case3 = false;
                    Politics.case4 = false;
                    Politics.case5 = false;
                    Politics.case6 = true;
                    Politics.case7 = false;
                    Politics.case8 = false;
                }
                else if (Politics.sPartySeats + Politics.gPartySeats + Politics.cPartySeats >= 50)
                {
                    Politics.case1 = false;
                    Politics.case2 = false;
                    Politics.case3 = false;
                    Politics.case4 = false;
                    Politics.case5 = false;
                    Politics.case6 = false;
                    Politics.case7 = true;
                    Politics.case8 = false;
                }
                else if (Politics.nPartySeats + Politics.lPartySeats >= 50)
                {
                    Politics.case1 = false;
                    Politics.case2 = false;
                    Politics.case3 = false;
                    Politics.case4 = false;
                    Politics.case5 = false;
                    Politics.case6 = false;
                    Politics.case7 = false;
                    Politics.case8 = true;
                }
                else
                {
                    Politics.case1 = false;
                    Politics.case2 = false;
                    Politics.case3 = false;
                    Politics.case4 = false;
                    Politics.case5 = false;
                    Politics.case6 = false;
                    Politics.case7 = false;
                    Politics.case8 = false;
                }
            }

            public void GetSeats(bool isPolls)
            {
                if (!isPolls)
                {
                    int temp = Politics.cPartyTickets + Politics.gPartyTickets + Politics.sPartyTickets + Politics.lPartyTickets + Politics.nPartyTickets;
                    if (temp != 0)
                    {
                        Politics.cPartySeats = (ushort)(99 * Politics.cPartyTickets / temp);
                        Politics.gPartySeats = (ushort)(99 * Politics.gPartyTickets / temp);
                        Politics.sPartySeats = (ushort)(99 * Politics.sPartyTickets / temp);
                        Politics.lPartySeats = (ushort)(99 * Politics.lPartyTickets / temp);
                        Politics.nPartySeats = (ushort)(99 * Politics.nPartyTickets / temp);
                    }
                    else
                    {
                        Politics.cPartySeats = 0;
                        Politics.gPartySeats = 0;
                        Politics.sPartySeats = 0;
                        Politics.lPartySeats = 0;
                        Politics.nPartySeats = 0;
                    }
                    Politics.cPartyTickets = 0;
                    Politics.gPartyTickets = 0;
                    Politics.sPartyTickets = 0;
                    Politics.lPartyTickets = 0;
                    Politics.nPartyTickets = 0;

                    temp = Politics.cPartySeats + Politics.gPartySeats + Politics.sPartySeats + Politics.lPartySeats + Politics.nPartySeats;
                    if (temp < 99)
                    {
                        System.Random rand = new System.Random();
                        switch (rand.Next(5))
                        {
                            case 0:
                                Politics.cPartySeats += (ushort)(99 - temp); break;
                            case 1:
                                Politics.gPartySeats += (ushort)(99 - temp); break;
                            case 2:
                                Politics.sPartySeats += (ushort)(99 - temp); break;
                            case 3:
                                Politics.lPartySeats += (ushort)(99 - temp); break;
                            case 4:
                                Politics.nPartySeats += (ushort)(99 - temp); break;
                        }
                    }
                }
                else
                {
                    float temp = Politics.cPartySeatsPolls + Politics.gPartySeatsPolls + Politics.sPartySeatsPolls + Politics.lPartySeatsPolls + Politics.nPartySeatsPolls;
                    if (temp != 0)
                    {
                        Politics.cPartySeatsPollsFinal = (float)(100 * Politics.cPartySeatsPolls / temp);
                        Politics.gPartySeatsPollsFinal = (float)(100 * Politics.gPartySeatsPolls / temp);
                        Politics.sPartySeatsPollsFinal = (float)(100 * Politics.sPartySeatsPolls / temp);
                        Politics.lPartySeatsPollsFinal = (float)(100 * Politics.lPartySeatsPolls / temp);
                        Politics.nPartySeatsPollsFinal = (float)(100 * Politics.nPartySeatsPolls / temp);
                    }
                    else
                    {
                        Politics.cPartySeatsPollsFinal = 0;
                        Politics.gPartySeatsPollsFinal = 0;
                        Politics.sPartySeatsPollsFinal = 0;
                        Politics.lPartySeatsPollsFinal = 0;
                        Politics.nPartySeatsPollsFinal = 0;
                    }

                    Politics.cPartyTickets = 0;
                    Politics.gPartyTickets = 0;
                    Politics.sPartyTickets = 0;
                    Politics.lPartyTickets = 0;
                    Politics.nPartyTickets = 0;
                    Politics.cPartySeatsPolls = 0f;
                    Politics.gPartySeatsPolls = 0f;
                    Politics.sPartySeatsPolls = 0f;
                    Politics.lPartySeatsPolls = 0f;
                    Politics.nPartySeatsPolls = 0f;                    
                }
            }

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
                                    if ( vehicle.Info.m_vehicleAI is PoliceCarAI || vehicle.Info.m_vehicleAI is DisasterResponseVehicleAI || vehicle.Info.m_vehicleAI is HearseAI)
                                    {
                                        comm_data.allVehicles++;
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)3000, vehicle.Info.m_class);
                                    }
                                    else if (vehicle.Info.m_vehicleAI is GarbageTruckAI || vehicle.Info.m_vehicleAI is FireTruckAI || vehicle.Info.m_vehicleAI is MaintenanceTruckAI)
                                    {
                                        if (vehicle.m_flags.IsFlagSet(Vehicle.Flags.Importing))
                                        {
                                            //comm_data.allVehicles += 1;
                                            //Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)6000, vehicle.Info.m_class);
                                        }
                                        else
                                        {
                                            comm_data.allVehicles += 2;
                                            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)15000, vehicle.Info.m_class);
                                        }
                                    }
                                    else if (vehicle.Info.m_vehicleAI is BusAI || vehicle.Info.m_vehicleAI is AmbulanceAI || vehicle.Info.m_vehicleAI is SnowTruckAI || vehicle.Info.m_vehicleAI is ParkMaintenanceVehicleAI || vehicle.Info.m_vehicleAI is WaterTruckAI)
                                    {
                                        comm_data.allVehicles += 2;
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)10000, vehicle.Info.m_class);
                                    }
                                    else if (vehicle.Info.m_vehicleAI is PassengerShipAI || vehicle.Info.m_vehicleAI is PassengerFerryAI || vehicle.Info.m_vehicleAI is CargoShipAI)
                                    {
                                        comm_data.allVehicles += 4;
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)25000, vehicle.Info.m_class);
                                    }
                                    else if (vehicle.Info.m_vehicleAI is PassengerPlaneAI || vehicle.Info.m_vehicleAI is PassengerBlimpAI)
                                    {
                                        comm_data.allVehicles += 8;
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)60000, vehicle.Info.m_class);
                                    }
                                    else if (vehicle.Info.m_vehicleAI is PassengerTrainAI || vehicle.Info.m_vehicleAI is CargoTrainAI)
                                    {
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)50000, vehicle.Info.m_class);
                                    }
                                    else if (vehicle.Info.m_vehicleAI is MetroTrainAI)
                                    {
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)40000, vehicle.Info.m_class);
                                    }
                                    else if (vehicle.Info.m_vehicleAI is PoliceCopterAI || vehicle.Info.m_vehicleAI is FireCopterAI || vehicle.Info.m_vehicleAI is DisasterResponseCopterAI || vehicle.Info.m_vehicleAI is AmbulanceCopterAI)
                                    {
                                        comm_data.allVehicles += 8;
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)60000, vehicle.Info.m_class);
                                    }
                                    else if (vehicle.Info.m_vehicleAI is CableCarAI || vehicle.Info.m_vehicleAI is TramAI)
                                    {
                                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)20000, vehicle.Info.m_class);
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
                                                Singleton<EconomyManager>.instance.AddPrivateIncome(1000, ItemClass.Service.Road, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
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
                            if (RealCity.EconomyExtension.IsSpecialBuilding((ushort)num6) == 2)
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

