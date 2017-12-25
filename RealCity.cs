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
        public static bool update_once = false;

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
            FileStream fs = File.Create("RealCity_setting.txt");
            StreamWriter streamWriter = new StreamWriter(fs);
            streamWriter.WriteLine(comm_data.last_language);
            streamWriter.WriteLine(comm_data.garbage_connection);
            streamWriter.WriteLine(comm_data.dead_connection);
            streamWriter.WriteLine(comm_data.crime_connection);
            streamWriter.WriteLine(comm_data.sick_connection);
            streamWriter.WriteLine(comm_data.is_help_resident);
            streamWriter.WriteLine(comm_data.is_smart_pbtp);
            streamWriter.WriteLine(comm_data.fire_connection);
            streamWriter.WriteLine(comm_data.road_connection);
            streamWriter.WriteLine(comm_data.hospitalhelp);
            streamWriter.WriteLine(comm_data.firehelp);
            streamWriter.WriteLine(comm_data.policehelp);
            streamWriter.Flush();
            fs.Close();
        }

        public static void LoadSetting()
        {
            if (File.Exists("RealCity_setting.txt"))
            {
                FileStream fs = new FileStream("RealCity_setting.txt", FileMode.Open);
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
                    comm_data.garbage_connection = false;
                }
                else
                {
                    comm_data.garbage_connection = true;
                }

                strLine = sr.ReadLine();

                if (strLine == "False")
                {
                    comm_data.dead_connection = false;
                }
                else
                {
                    comm_data.dead_connection = true;
                }

                strLine = sr.ReadLine();

                if (strLine == "False")
                {
                    comm_data.crime_connection = false;
                }
                else
                {
                    comm_data.crime_connection = true;
                }

                strLine = sr.ReadLine();

                if (strLine == "False")
                {
                    comm_data.sick_connection = false;
                }
                else
                {
                    comm_data.sick_connection = true;
                }

                strLine = sr.ReadLine();

                if (strLine == "False")
                {
                    comm_data.is_help_resident = false;
                }
                else
                {
                    comm_data.is_help_resident = true;
                }

                strLine = sr.ReadLine();

                if (strLine == "False")
                {
                    comm_data.is_smart_pbtp = false;
                }
                else
                {
                    comm_data.is_smart_pbtp = true;
                }

                strLine = sr.ReadLine();

                if (strLine == "False")
                {
                    comm_data.fire_connection = false;
                }
                else
                {
                    comm_data.fire_connection = true;
                }

                strLine = sr.ReadLine();

                if (strLine == "False")
                {
                    comm_data.road_connection = false;
                }
                else
                {
                    comm_data.road_connection = true;
                }

                strLine = sr.ReadLine();

                if (strLine == "False")
                {
                    comm_data.hospitalhelp = false;
                }
                else
                {
                    comm_data.hospitalhelp = true;
                }

                strLine = sr.ReadLine();

                if (strLine == "False" || true)
                {
                    comm_data.firehelp = false;
                }
                else
                {
                    comm_data.firehelp = true;
                }

                strLine = sr.ReadLine();

                if (strLine == "False")
                {
                    comm_data.policehelp = false;
                }
                else
                {
                    comm_data.policehelp = true;
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
            group.AddDropdown(language.OptionUI[1], new string[] { "English", "简体中文" }, comm_data.last_language, (index) => get_language_idex(index));

            UIHelperBase group1 = helper.AddGroup(language.OptionUI[2]);
            group1.AddCheckbox(language.OptionUI[3], comm_data.garbage_connection, (index) => get_garbage_connection(index));
            group1.AddCheckbox(language.OptionUI[4], comm_data.dead_connection, (index) => get_dead_connection(index));
            group1.AddCheckbox(language.OptionUI[5], comm_data.crime_connection, (index) => get_crime_connection(index));
            group1.AddCheckbox(language.OptionUI[6], comm_data.sick_connection, (index) => get_sick_connection(index));
            group1.AddCheckbox(language.OptionUI[10], comm_data.fire_connection, (index) => get_fire_connection(index));
            //group1.AddCheckbox(language.OptionUI[11], comm_data.road_connection, (index) => get_road_connection(index));
            //group1.AddCheckbox(language.OptionUI[12], comm_data.firehelp, (index) => outsidefirehelp(index));
            group1.AddCheckbox(language.OptionUI[13], comm_data.hospitalhelp, (index) => outsidehospitalhelp(index));
            group1.AddCheckbox(language.OptionUI[14], comm_data.policehelp, (index) => outsidepolicehelp(index));

            UIHelperBase group2 = helper.AddGroup(language.OptionUI[7]);
            group2.AddCheckbox(language.OptionUI[8], comm_data.is_help_resident, (index) => is_help_resident(index));
            group2.AddCheckbox(language.OptionUI[9], comm_data.is_smart_pbtp, (index) => is_smart_pbtp(index));
            SaveSetting();
        }

        public void get_language_idex(int index)
        {
            language_idex = index;
            language.language_switch((byte)language_idex);
            SaveSetting();
            MethodInfo method = typeof(OptionsMainPanel).GetMethod("OnLocaleChanged", BindingFlags.Instance | BindingFlags.NonPublic);
            method.Invoke(UIView.library.Get<OptionsMainPanel>("OptionsPanel"), new object[0]);
            Loader.RemoveGui();
            Loader.SetupGui();
            //DebugLog.LogToFileOnly("get_current language idex = " + language_idex.ToString());
        }

        public void is_help_resident(bool index)
        {
            comm_data.is_help_resident = index;
            SaveSetting();
        }

        public void is_smart_pbtp(bool index)
        {
            comm_data.is_smart_pbtp = index;
            SaveSetting();
        }

        public void get_garbage_connection(bool index)
        {
            comm_data.garbage_connection = index;
            SaveSetting();
        }

        public void get_dead_connection(bool index)
        {
            comm_data.dead_connection = index;
            SaveSetting();
        }
        public void get_crime_connection(bool index)
        {
            comm_data.crime_connection = index;
            if (comm_data.crime_connection)
            {
                comm_data.policehelp = false;
            }
            SaveSetting();
            MethodInfo method = typeof(OptionsMainPanel).GetMethod("OnLocaleChanged", BindingFlags.Instance | BindingFlags.NonPublic);
            method.Invoke(UIView.library.Get<OptionsMainPanel>("OptionsPanel"), new object[0]);
        }
        public void get_sick_connection(bool index)
        {
            comm_data.sick_connection = index;
            if (comm_data.sick_connection)
            {
                comm_data.hospitalhelp = false;
            }
            SaveSetting();
            MethodInfo method = typeof(OptionsMainPanel).GetMethod("OnLocaleChanged", BindingFlags.Instance | BindingFlags.NonPublic);
            method.Invoke(UIView.library.Get<OptionsMainPanel>("OptionsPanel"), new object[0]);
        }

        public void get_fire_connection(bool index)
        {
            comm_data.fire_connection = index;
            if (comm_data.fire_connection)
            {
                comm_data.firehelp = false;
            }
            SaveSetting();
            MethodInfo method = typeof(OptionsMainPanel).GetMethod("OnLocaleChanged", BindingFlags.Instance | BindingFlags.NonPublic);
            method.Invoke(UIView.library.Get<OptionsMainPanel>("OptionsPanel"), new object[0]);
        }

        public void get_road_connection(bool index)
        {
            comm_data.road_connection = index;
            SaveSetting();
        }

        public void outsidehospitalhelp(bool index)
        {
            comm_data.hospitalhelp = index;
            if (comm_data.hospitalhelp)
            {
                comm_data.sick_connection = false;
            }
            SaveSetting();
            MethodInfo method = typeof(OptionsMainPanel).GetMethod("OnLocaleChanged", BindingFlags.Instance | BindingFlags.NonPublic);
            method.Invoke(UIView.library.Get<OptionsMainPanel>("OptionsPanel"), new object[0]);
        }

        public void outsidefirehelp(bool index)
        {
            comm_data.firehelp = index;
            if (comm_data.firehelp)
            {
                comm_data.fire_connection = false;
            }
            SaveSetting();
            MethodInfo method = typeof(OptionsMainPanel).GetMethod("OnLocaleChanged", BindingFlags.Instance | BindingFlags.NonPublic);
            method.Invoke(UIView.library.Get<OptionsMainPanel>("OptionsPanel"), new object[0]);
        }

        public void outsidepolicehelp(bool index)
        {
            comm_data.policehelp = index;

            if (comm_data.policehelp)
            {
                comm_data.crime_connection = false;
            }
            SaveSetting();
            MethodInfo method = typeof(OptionsMainPanel).GetMethod("OnLocaleChanged", BindingFlags.Instance | BindingFlags.NonPublic);
            method.Invoke(UIView.library.Get<OptionsMainPanel>("OptionsPanel"), new object[0]);
        }

        public class EconomyExtension : EconomyExtensionBase
        {
            public override long OnUpdateMoneyAmount(long internalMoneyAmount)
            {
                //DebugLog.LogToFileOnly(Singleton<SimulationManager>.instance.m_currentDayTimeHour.ToString());
                //here we process income_tax and goverment_salary_expense 
                //to make goverment_salary_expense the same with in game unit
                if (Loader.CurrentLoadMode == LoadMode.LoadGame || Loader.CurrentLoadMode == LoadMode.NewGame)
                {
                    comm_data.current_time = Singleton<SimulationManager>.instance.m_currentDayTimeHour;
                    uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
                    uint num2 = currentFrameIndex & 255u;
                    //DebugLog.LogToFileOnly("OnUpdateMoneyAmount num2 = " + num2.ToString());
                    if ((num2 == 255u) && (comm_data.current_time != comm_data.prev_time))
                    {
                        //DebugLog.LogToFileOnly("process once update money " + comm_data.current_time.ToString() + " " + comm_data.prev_time.ToString());
                        citizen_status();
                        vehicle_status();
                        generate_tips();
                        building_status();
                        caculate_goverment_employee_expense();
                        caculate_profit();
                        caculate_citizen_transport_fee();
                        check_task_status();
                        check_event_status();
                        //change_outside_price();

                        comm_data.update_money_count++;
                        if (comm_data.update_money_count == 17)
                        {
                            comm_data.tourist_num_final = comm_data.tourist_num;
                            comm_data.tourist_transport_fee_num_final = comm_data.tourist_transport_fee_num;
                            comm_data.tourist_num = 0;
                            comm_data.tourist_transport_fee_num = 0;
                            comm_data.update_money_count = 0;
                        }
                        pc_EconomyManager.clean_current(comm_data.update_money_count);


                        comm_data.prev_time = comm_data.current_time;
                        //DebugLog.LogToFileOnly("update_money_count is " + comm_data.update_money_count.ToString());
                    }
                    RealCityUI.refesh_onece = true;
                    MoreeconomicUI.refesh_onece = true;
                    PlayerBuildingUI.refesh_once = true;
                    BuildingUI.refesh_once = true;
                    HumanUI.refesh_once = true;
                    comm_data.is_updated = true;
                }

                return internalMoneyAmount;
            }


            public void change_outside_price()
            {
                System.Random rand = new System.Random();

                if (comm_data.highpricegoods)
                {
                    pc_PrivateBuildingAI.good_import_price = (((float)rand.Next(50)/100f) + 1.5f) * pc_PrivateBuildingAI.good_import_price1;
                } else
                {
                    pc_PrivateBuildingAI.good_import_price = pc_PrivateBuildingAI.good_import_price1;
                }

                if (comm_data.highdemand)
                {
                    if (comm_data.high_oil)
                    {
                        pc_PrivateBuildingAI.oil_export_price = (((float)rand.Next(50) / 100f) + 1.5f) * pc_PrivateBuildingAI.oil_export_price1;
                    }else
                    {
                        pc_PrivateBuildingAI.oil_export_price = pc_PrivateBuildingAI.oil_export_price1;
                    }

                    if (comm_data.high_ore)
                    {
                        pc_PrivateBuildingAI.ore_export_price = (((float)rand.Next(50) / 100f) + 1.5f) * pc_PrivateBuildingAI.ore_export_price1;
                    }
                    else
                    {
                        pc_PrivateBuildingAI.ore_export_price = pc_PrivateBuildingAI.ore_export_price1;
                    }

                    if (comm_data.high_grain)
                    {
                        pc_PrivateBuildingAI.grain_export_price = (((float)rand.Next(50) / 100f) + 1.5f) * pc_PrivateBuildingAI.oil_export_price1;
                    }
                    else
                    {
                        pc_PrivateBuildingAI.grain_export_price = pc_PrivateBuildingAI.grain_export_price1;
                    }

                    if (comm_data.high_logs)
                    {
                        pc_PrivateBuildingAI.log_export_price = (((float)rand.Next(50) / 100f) + 1.5f) * pc_PrivateBuildingAI.log_export_price1;
                    }
                    else
                    {
                        pc_PrivateBuildingAI.log_export_price = pc_PrivateBuildingAI.log_export_price1;
                    }

                    if (comm_data.high_food)
                    {
                        pc_PrivateBuildingAI.food_export_price = (((float)rand.Next(50) / 100f) + 1.5f) * pc_PrivateBuildingAI.food_export_price1;
                    }
                    else
                    {
                        pc_PrivateBuildingAI.food_export_price = pc_PrivateBuildingAI.food_export_price1;
                    }

                    if (comm_data.high_lumber)
                    {
                        pc_PrivateBuildingAI.lumber_export_price = (((float)rand.Next(50) / 100f) + 1.5f) * pc_PrivateBuildingAI.lumber_export_price1;
                    }
                    else
                    {
                        pc_PrivateBuildingAI.lumber_export_price = pc_PrivateBuildingAI.lumber_export_price1;
                    }

                    if (comm_data.high_petrol)
                    {
                        pc_PrivateBuildingAI.petrol_export_price = (((float)rand.Next(50) / 100f) + 1.5f) * pc_PrivateBuildingAI.lumber_export_price1;
                    }
                    else
                    {
                        pc_PrivateBuildingAI.petrol_export_price = pc_PrivateBuildingAI.petrol_export_price1;
                    }

                    if (comm_data.high_coal)
                    {
                        pc_PrivateBuildingAI.coal_export_price = (((float)rand.Next(50) / 100f) + 1.5f) * pc_PrivateBuildingAI.coal_export_price1;
                    }
                    else
                    {
                        pc_PrivateBuildingAI.coal_export_price = pc_PrivateBuildingAI.coal_export_price1;
                    }
                }

                if (comm_data.lowdemand)
                {
                    if (comm_data.high_oil)
                    {
                        pc_PrivateBuildingAI.oil_export_price *= (((float)rand.Next(50) / 100f + 0.5f)) * pc_PrivateBuildingAI.oil_export_price1;
                    }
                    else
                    {
                        pc_PrivateBuildingAI.oil_export_price = pc_PrivateBuildingAI.oil_export_price1;
                    }

                    if (comm_data.high_ore)
                    {
                        pc_PrivateBuildingAI.ore_export_price = (((float)rand.Next(50) / 100f + 0.5f)) * pc_PrivateBuildingAI.ore_export_price1;
                    }
                    else
                    {
                        pc_PrivateBuildingAI.ore_export_price = pc_PrivateBuildingAI.ore_export_price1;
                    }

                    if (comm_data.high_grain)
                    {
                        pc_PrivateBuildingAI.grain_export_price = (((float)rand.Next(50) / 100f + 0.5f)) * pc_PrivateBuildingAI.grain_export_price1;
                    }
                    else
                    {
                        pc_PrivateBuildingAI.grain_export_price = pc_PrivateBuildingAI.grain_export_price1;
                    }

                    if (comm_data.high_logs)
                    {
                        pc_PrivateBuildingAI.log_export_price = (((float)rand.Next(50) / 100f + 0.5f)) * pc_PrivateBuildingAI.log_export_price1;
                    }
                    else
                    {
                        pc_PrivateBuildingAI.log_export_price = pc_PrivateBuildingAI.log_export_price1;
                    }

                    if (comm_data.high_food)
                    {
                        pc_PrivateBuildingAI.food_export_price = (((float)rand.Next(50) / 100f + 0.5f)) * pc_PrivateBuildingAI.food_export_price1;
                    }
                    else
                    {
                        pc_PrivateBuildingAI.food_export_price = pc_PrivateBuildingAI.food_export_price1;
                    }

                    if (comm_data.high_lumber)
                    {
                        pc_PrivateBuildingAI.lumber_export_price = (((float)rand.Next(50) / 100f + 0.5f)) * pc_PrivateBuildingAI.lumber_export_price1;
                    }
                    else
                    {
                        pc_PrivateBuildingAI.lumber_export_price = pc_PrivateBuildingAI.lumber_export_price1;
                    }

                    if (comm_data.high_petrol)
                    {
                        pc_PrivateBuildingAI.petrol_export_price = (((float)rand.Next(50) / 100f + 0.5f)) * pc_PrivateBuildingAI.petrol_export_price1;
                    }
                    else
                    {
                        pc_PrivateBuildingAI.petrol_export_price = pc_PrivateBuildingAI.petrol_export_price1;
                    }

                    if (comm_data.high_coal)
                    {
                        pc_PrivateBuildingAI.coal_export_price = (((float)rand.Next(50) / 100f + 0.5f)) * pc_PrivateBuildingAI.coal_export_price1;
                    }
                    else
                    {
                        pc_PrivateBuildingAI.coal_export_price = pc_PrivateBuildingAI.coal_export_price1;
                    }
                }

                if (!comm_data.is_random_event)
                {
                    pc_PrivateBuildingAI.coal_export_price = pc_PrivateBuildingAI.coal_export_price1;
                    pc_PrivateBuildingAI.petrol_export_price = pc_PrivateBuildingAI.petrol_export_price1;
                    pc_PrivateBuildingAI.lumber_export_price = pc_PrivateBuildingAI.lumber_export_price1;
                    pc_PrivateBuildingAI.food_export_price = pc_PrivateBuildingAI.food_export_price1;
                    pc_PrivateBuildingAI.log_export_price = pc_PrivateBuildingAI.log_export_price1;
                    pc_PrivateBuildingAI.grain_export_price = pc_PrivateBuildingAI.grain_export_price1;
                    pc_PrivateBuildingAI.ore_export_price = pc_PrivateBuildingAI.ore_export_price1;
                    pc_PrivateBuildingAI.oil_export_price = pc_PrivateBuildingAI.oil_export_price1;
                    pc_PrivateBuildingAI.good_import_price = pc_PrivateBuildingAI.good_import_price1;
                }
            }



            public void get_high_low_price(bool is_low)
            {
                System.Random rand = new System.Random();
                if (rand.Next(4) == 0)
                {
                    comm_data.high_coal = true;
                }
                else
                {
                    comm_data.high_coal = false;
                }
                if (rand.Next(4) == 0)
                {
                    comm_data.high_petrol = true;
                }
                else
                {
                    comm_data.high_petrol = false;
                }
                if (rand.Next(4) == 0)
                {
                    comm_data.high_food = true;
                }
                else
                {
                    comm_data.high_food = false;
                }
                if (rand.Next(4) == 0)
                {
                    comm_data.high_lumber = true;
                }
                else
                {
                    comm_data.high_lumber = false;
                }
                if (rand.Next(4) == 0)
                {
                    comm_data.high_oil = true;
                }
                else
                {
                    comm_data.high_oil = false;
                }
                if (rand.Next(4) == 0)
                {
                    comm_data.high_ore = true;
                }
                else
                {
                    comm_data.high_ore = false;
                }
                if (rand.Next(4) == 0)
                {
                    comm_data.high_logs = true;
                }
                else
                {
                    comm_data.high_logs = false;
                }
                if (rand.Next(4) == 0)
                {
                    comm_data.high_grain = true;
                }
                else
                {
                    comm_data.high_grain = false;
                }

                if (!is_low)
                {
                    switch (rand.Next(8))
                    {
                        case 0:
                            comm_data.high_grain = true; break;
                        case 1:
                            comm_data.high_logs = true; break;
                        case 2:
                            comm_data.high_ore = true; break;
                        case 3:
                            comm_data.high_oil = true; break;
                        case 4:
                            comm_data.high_lumber = true; break;
                        case 5:
                            comm_data.high_food = true; break;
                        case 6:
                            comm_data.high_petrol = true; break;
                        case 7:
                            comm_data.high_coal = true; break;
                        default: break;
                    }
                }
                else
                {
                    switch (rand.Next(8))
                    {
                        case 0:
                            comm_data.high_grain = false; break;
                        case 1:
                            comm_data.high_logs = false; break;
                        case 2:
                            comm_data.high_ore = false; break;
                        case 3:
                            comm_data.high_oil = false; break;
                        case 4:
                            comm_data.high_lumber = false; break;
                        case 5:
                            comm_data.high_food = false; break;
                        case 6:
                            comm_data.high_petrol = false; break;
                        case 7:
                            comm_data.high_coal = false; break;
                        default: break;
                    }
                }
            }

            public void check_event_status()
            {
                if(comm_data.event_num <=0)
                {
                    comm_data.is_random_event = false;
                    System.Random rand = new System.Random();
                    comm_data.lackofgoods = false;
                    comm_data.highpricegoods = false;
                    comm_data.refugees = false;
                    comm_data.Rich_immigrants = false;
                    comm_data.Virus_attack = false;
                    comm_data.hot_money = false;
                    comm_data.money_flowout = false;
                    comm_data.lowdemand = false;
                    comm_data.highdemand = false;
                    comm_data.happy_holiday = false;
                    if (rand.Next(1000) < 0)
                    {
                        comm_data.is_random_event = true;
                        comm_data.event_num = 1000;
                        switch (rand.Next(9))
                        {
                            case 0:
                                comm_data.lackofgoods = true;break;
                            case 1:
                                comm_data.highpricegoods = true; break;
                            case 2:
                                comm_data.refugees = true; break;
                            case 3:
                                comm_data.Virus_attack = true; break;
                            case 4:
                                comm_data.Rich_immigrants = true; break;
                            case 5:
                                comm_data.hot_money = true; break;
                            case 6:
                                comm_data.money_flowout = true; break;
                            case 7:
                                comm_data.lowdemand = true; get_high_low_price(true);break;
                            case 8:
                                comm_data.highdemand = true; get_high_low_price(false); break;
                            default:break;
                        }
                    }
                } else
                {
                    comm_data.event_num--;
                }
            }



            public void check_task_status()
            {
                comm_data.task_time--;
                comm_data.cd_num--;
                if (comm_data.cd_num < -2)
                {
                    comm_data.cd_num = -2;
                }
                else if (comm_data.cd_num > 0)
                {
                    comm_data.garbage_task = false;
                    comm_data.dead_task = false;
                    comm_data.crasy_task = false;
                }

                if (comm_data.task_time > 0)
                {
                    if (comm_data.task_num <= 0)
                    {
                        comm_data.task_time = 0;
                        comm_data.task_num = 0;
                        if (comm_data.garbage_task)
                        {
                            comm_data.garbage_task = false;                            
                            comm_data.cd_num = 2000;
                            Singleton<EconomyManager>.instance.AddPrivateIncome(9000000, ItemClass.Service.Garbage, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                        }

                        if (comm_data.dead_task)
                        {
                            comm_data.dead_task = false;
                            comm_data.cd_num = 2500;
                            Singleton<EconomyManager>.instance.AddPrivateIncome(5000000, ItemClass.Service.HealthCare, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                        }

                        if (comm_data.crasy_task)
                        {
                            comm_data.crasy_task = false;
                            comm_data.cd_num = 3000;
                            Singleton<EconomyManager>.instance.AddPrivateIncome(7000000, ItemClass.Service.Road, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                        }
                    }
                }
                else
                {
                    comm_data.task_time = 0;
                    comm_data.task_num = 0;
                    if (comm_data.garbage_task)
                    {
                        comm_data.cd_num = 2000;
                        comm_data.garbage_task = false;
                    }

                    if (comm_data.dead_task)
                    {
                        comm_data.cd_num = 2500;
                        comm_data.dead_task = false;
                    }

                    if (comm_data.crasy_task)
                    {
                        comm_data.cd_num = 3000;
                        comm_data.crasy_task = false;
                    }
                }


                if(!comm_data.garbage_task)
                {
                    RealCityUI.infinity_garbage_Checkbox.isChecked = comm_data.garbage_task;
                }

                if (!comm_data.dead_task)
                {
                    RealCityUI.infinity_dead_Checkbox.isChecked = comm_data.dead_task;
                }

                if (!comm_data.crasy_task)
                {
                    RealCityUI.crasy_transport_Checkbox.isChecked = comm_data.crasy_task;
                }
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

                tip6_message_forgui = language.TipAndChirperMessage[8];

                if ((pc_PrivateBuildingAI.all_oil_building_profit_final + pc_PrivateBuildingAI.all_ore_building_profit_final + pc_PrivateBuildingAI.all_oil_building_loss_final + pc_PrivateBuildingAI.all_ore_building_loss_final - comm_data.family_count / 10) < 150)
                {
                    tip7_message_forgui = "";
                }
                else if (comm_data.family_count != 0)
                {
                    tip7_message_forgui = language.TipAndChirperMessage[9];
                }

                if(comm_data.is_random_event)
                {
                    tip8_message_forgui = language.TipAndChirperMessage[10];
                    if (comm_data.lackofgoods)
                    {
                        tip8_message_forgui += language.TipAndChirperMessage[11];
                    }

                    if (comm_data.highpricegoods)
                    {
                        tip8_message_forgui += language.TipAndChirperMessage[12];
                    }

                    if (comm_data.highdemand)
                    {
                        tip8_message_forgui += language.TipAndChirperMessage[13];
                        if (comm_data.high_oil)
                        {
                            tip8_message_forgui += language.TipAndChirperMessage[20];
                        }
                        if (comm_data.high_ore)
                        {
                            tip8_message_forgui += language.TipAndChirperMessage[21];
                        }
                        if (comm_data.high_grain)
                        {
                            tip8_message_forgui += language.TipAndChirperMessage[22];
                        }
                        if (comm_data.high_logs)
                        {
                            tip8_message_forgui += language.TipAndChirperMessage[23];
                        }
                        if (comm_data.high_food)
                        {
                            tip8_message_forgui += language.TipAndChirperMessage[24];
                        }
                        if (comm_data.high_lumber)
                        {
                            tip8_message_forgui += language.TipAndChirperMessage[25];
                        }
                        if (comm_data.high_petrol)
                        {
                            tip8_message_forgui += language.TipAndChirperMessage[26];
                        }
                        if (comm_data.high_coal)
                        {
                            tip8_message_forgui += language.TipAndChirperMessage[27];
                        }
                    }

                    if (comm_data.lowdemand)
                    {
                        tip8_message_forgui += language.TipAndChirperMessage[14];
                        if (!comm_data.high_oil)
                        {
                            tip8_message_forgui += language.TipAndChirperMessage[20];
                        }
                        if (!comm_data.high_ore)
                        {
                            tip8_message_forgui += language.TipAndChirperMessage[21];
                        }
                        if (!comm_data.high_grain)
                        {
                            tip8_message_forgui += language.TipAndChirperMessage[22];
                        }
                        if (!comm_data.high_logs)
                        {
                            tip8_message_forgui += language.TipAndChirperMessage[23];
                        }
                        if (!comm_data.high_food)
                        {
                            tip8_message_forgui += language.TipAndChirperMessage[24];
                        }
                        if (!comm_data.high_lumber)
                        {
                            tip8_message_forgui += language.TipAndChirperMessage[25];
                        }
                        if (!comm_data.high_petrol)
                        {
                            tip8_message_forgui += language.TipAndChirperMessage[26];
                        }
                        if (!comm_data.high_coal)
                        {
                            tip8_message_forgui += language.TipAndChirperMessage[27];
                        }
                    }

                    if (comm_data.Virus_attack)
                    {
                        tip8_message_forgui += language.TipAndChirperMessage[15];
                    }

                    if (comm_data.refugees)
                    {
                        tip8_message_forgui += language.TipAndChirperMessage[16];
                    }

                    if (comm_data.Rich_immigrants)
                    {
                        tip8_message_forgui += language.TipAndChirperMessage[17];
                    }

                    if (comm_data.hot_money)
                    {
                        tip8_message_forgui += language.TipAndChirperMessage[18];
                    }

                    if (comm_data.money_flowout)
                    {
                        tip8_message_forgui += language.TipAndChirperMessage[19];
                    }

                    tip8_message_forgui += language.TipAndChirperMessage[28] + comm_data.event_num.ToString();
                } else
                {
                    tip8_message_forgui = language.TipAndChirperMessage[29];
                }

                FieldInfo cashAmount;
                cashAmount = typeof(EconomyManager).GetField("m_cashAmount", BindingFlags.NonPublic | BindingFlags.Instance);
                long _cashAmount = (long)cashAmount.GetValue(Singleton<EconomyManager>.instance);

                if (!comm_data.have_bank_pre)
                {
                    comm_data.city_bank = 10;
                }


                if (comm_data.city_bank < -1000000)
                {
                    tip9_message_forgui = language.TipAndChirperMessage[30];
                } else
                {
                    tip9_message_forgui = "";
                }

                if (comm_data.city_bank < 0)
                {
                    if (_cashAmount > (long)(-comm_data.city_bank))
                    {
                        comm_data.city_bank += (int)(-comm_data.city_bank);
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, (int)(-comm_data.city_bank), ItemClass.Service.Beautification, ItemClass.SubService.None, ItemClass.Level.Level1);
                    }
                }
            }

            public void caculate_goverment_employee_expense()
            {
                if (Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_populationData.m_finalCount > 0)
                {
                    ItemClass temp = new ItemClass();
                    temp.m_service = ItemClass.Service.PublicTransport;
                    temp.m_subService = ItemClass.SubService.PublicTransportBus;
                    //use this to diff make in-game mantenance and goverment_salary_expense 
                    temp.m_layer = ItemClass.Layer.Markers;
                    if (comm_data.PublicTransport_bus != 0)
                    {
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.PublicTransport_bus, temp);
                    }

                    temp.m_service = ItemClass.Service.PublicTransport;
                    temp.m_subService = ItemClass.SubService.PublicTransportTram;
                    if (comm_data.PublicTransport_tram != 0)
                    {
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.PublicTransport_tram, temp);
                    }

                    temp.m_service = ItemClass.Service.PublicTransport;
                    temp.m_subService = ItemClass.SubService.PublicTransportTrain;
                    if (comm_data.PublicTransport_train != 0)
                    {
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.PublicTransport_train, temp);
                    }

                    temp.m_service = ItemClass.Service.PublicTransport;
                    temp.m_subService = ItemClass.SubService.PublicTransportMetro;
                    if (comm_data.PublicTransport_metro != 0)
                    {
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.PublicTransport_metro, temp);
                    }

                    temp.m_service = ItemClass.Service.PublicTransport;
                    temp.m_subService = ItemClass.SubService.PublicTransportTaxi;
                    if (comm_data.PublicTransport_taxi != 0)
                    {
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.PublicTransport_taxi, temp);
                    }

                    temp.m_service = ItemClass.Service.PublicTransport;
                    temp.m_subService = ItemClass.SubService.PublicTransportPlane;
                    if (comm_data.PublicTransport_plane != 0)
                    {
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.PublicTransport_plane, temp);
                    }

                    temp.m_service = ItemClass.Service.PublicTransport;
                    temp.m_subService = ItemClass.SubService.PublicTransportShip;
                    if (comm_data.PublicTransport_ship != 0)
                    {
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.PublicTransport_ship, temp);
                    }

                    temp.m_service = ItemClass.Service.PublicTransport;
                    temp.m_subService = ItemClass.SubService.PublicTransportCableCar;
                    if (comm_data.PublicTransport_cablecar != 0)
                    {
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.PublicTransport_cablecar, temp);
                    }

                    temp.m_service = ItemClass.Service.PublicTransport;
                    temp.m_subService = ItemClass.SubService.PublicTransportMonorail;
                    if (comm_data.PublicTransport_monorail != 0)
                    {
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.PublicTransport_monorail, temp);
                    }

                    temp.m_service = ItemClass.Service.Road;
                    temp.m_subService = ItemClass.SubService.None;
                    if (comm_data.Road != 0)
                    {
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.Road, temp);
                    }

                    temp.m_service = ItemClass.Service.Water;
                    if (comm_data.Water != 0)
                    {
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.Water, temp);
                    }

                    temp.m_service = ItemClass.Service.Education;
                    if (comm_data.Education != 0)
                    {
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.Education, temp);
                    }

                    temp.m_service = ItemClass.Service.HealthCare;
                    if (comm_data.HealthCare != 0)
                    {
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.HealthCare, temp);
                    }

                    temp.m_service = ItemClass.Service.FireDepartment;
                    if (comm_data.FireDepartment != 0)
                    {
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.FireDepartment, temp);
                    }

                    temp.m_service = ItemClass.Service.Beautification;
                    if (comm_data.Beautification != 0)
                    {
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.Beautification, temp);
                    }

                    temp.m_service = ItemClass.Service.Garbage;
                    if (comm_data.Garbage != 0)
                    {
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.Garbage, temp);
                    }

                    temp.m_service = ItemClass.Service.Electricity;
                    if (comm_data.Electricity != 0)
                    {
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.Electricity, temp);
                    }

                    temp.m_service = ItemClass.Service.Monument;
                    if (comm_data.Monument != 0)
                    {
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.Monument, temp);
                    }

                    temp.m_service = ItemClass.Service.PoliceDepartment;
                    if (comm_data.PoliceDepartment != 0)
                    {
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.PoliceDepartment, temp);
                    }

                    temp.m_service = ItemClass.Service.Disaster;
                    if (comm_data.Disaster != 0)
                    {
                        Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, comm_data.Disaster, temp);
                    }

                    //discard this, because in 1.9.0-f5, no citizen income and tourist income showed in UI, combin them into residential building income
                    //and show them in CTRL+R UI.
                    //citizen income tax
                    //if (comm_data.citizen_salary_tax_total != 0)
                    //{
                    //    temp = new ItemClass();
                    //    temp.m_service = ItemClass.Service.Citizen;
                    //    Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.CitizenIncome, (int)(comm_data.citizen_salary_tax_total), temp);
                    //}
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
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2 / comm_data.game_income_expense_multiple;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportTram;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.tram_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2 / comm_data.game_income_expense_multiple;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportMetro;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.metro_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2 / comm_data.game_income_expense_multiple;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportTrain;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.train_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2 / comm_data.game_income_expense_multiple;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportTaxi;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.taxi_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2 / comm_data.game_income_expense_multiple;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportPlane;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.plane_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2 / comm_data.game_income_expense_multiple;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportShip;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.ship_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2 / comm_data.game_income_expense_multiple;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportMonorail;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.monorail_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2 / comm_data.game_income_expense_multiple;

                temp1 = 0L;
                temp2 = 0L;
                temp.m_service = ItemClass.Service.PublicTransport;
                temp.m_subService = ItemClass.SubService.PublicTransportCableCar;
                Singleton<EconomyManager>.instance.GetIncomeAndExpenses(temp, out temp2, out temp1);
                RealCityUI.cablecar_income = (double)temp2 / 100f;
                comm_data.public_transport_fee = comm_data.public_transport_fee + temp2 / comm_data.game_income_expense_multiple;

                //add vehicle transport_fee
                comm_data.temp_total_citizen_vehical_time_last = comm_data.temp_total_citizen_vehical_time;
                comm_data.temp_total_citizen_vehical_time = 0;

                //assume that 1 time will cost 5fen car oil money
                comm_data.all_transport_fee = comm_data.public_transport_fee + comm_data.temp_total_citizen_vehical_time_last * 5;

                if (comm_data.family_count > 0)
                {
                    if ((comm_data.all_transport_fee / comm_data.family_count) > 40)
                    {
                        comm_data.citizen_average_transport_fee = 40;
                    }
                    else
                    {
                        comm_data.citizen_average_transport_fee = (byte)(comm_data.all_transport_fee / comm_data.family_count);
                    }
                }
            }


            public void building_status()
            {
                BuildingManager instance = Singleton<BuildingManager>.instance;
                update_once = false;
                pc_OutsideConnectionAI.have_garbage_building = false;
                pc_OutsideConnectionAI.have_cemetry_building = false;
                pc_OutsideConnectionAI.have_police_building = false;
                pc_OutsideConnectionAI.have_fire_building = false;
                pc_OutsideConnectionAI.have_hospital_building = false;
                comm_data.have_bank = false;
                comm_data.have_tax_department = false;
                comm_data.have_toll_station = false;
                checked
                {
                    for (int i = 0; i < instance.m_buildings.m_buffer.Count<Building>(); i++)
                    {
                        if (instance.m_buildings.m_buffer[i].m_flags.IsFlagSet(Building.Flags.Created) && instance.m_buildings.m_buffer[i].m_flags.IsFlagSet(Building.Flags.Active) && !instance.m_buildings.m_buffer[i].m_flags.IsFlagSet(Building.Flags.Deleted) && !instance.m_buildings.m_buffer[i].m_flags.IsFlagSet(Building.Flags.Untouchable))
                        {
                            int result = 0;
                            int budget = 0;
                            if ((instance.m_buildings.m_buffer[i].Info.m_class.m_service == ItemClass.Service.HealthCare) && (instance.m_buildings.m_buffer[i].Info.m_class.m_level == ItemClass.Level.Level2))
                            {
                                result = 0;
                                budget = Singleton<EconomyManager>.instance.GetBudget(instance.m_buildings.m_buffer[i].Info.m_class);
                                result = instance.m_buildings.m_buffer[i].Info.m_buildingAI.GetMaintenanceCost() / 100;
                                comm_data.building_money[i] -= (result / 100f) * (float)(budget * (float)(instance.m_buildings.m_buffer[i].m_productionRate / 10000f));
                                pc_OutsideConnectionAI.have_cemetry_building = true;

                                if (comm_data.update_outside_count == 63)
                                {
                                    comm_data.building_money[i] = 0;
                                }

                                if (comm_data.building_money[i] > 80000000)
                                {
                                    comm_data.building_money[i] = 80000000;
                                }
                                else if (comm_data.building_money[i] < -80000000)
                                {
                                    comm_data.building_money[i] = -80000000;
                                }
                            }

                            if (instance.m_buildings.m_buffer[i].Info.m_class.m_service == ItemClass.Service.Garbage)
                            {
                                result = 0;
                                budget = Singleton<EconomyManager>.instance.GetBudget(instance.m_buildings.m_buffer[i].Info.m_class);
                                result = instance.m_buildings.m_buffer[i].Info.m_buildingAI.GetMaintenanceCost() / 100;
                                comm_data.building_money[i] -= (result / 100f) * (float)(budget * (float)(instance.m_buildings.m_buffer[i].m_productionRate / 10000f));
                                pc_OutsideConnectionAI.have_garbage_building = true;
                                //DebugLog.LogToFileOnly("maintenaince is " + budget.ToString() + " " + result.ToString() + " " + instance.m_buildings.m_buffer[i].m_productionRate.ToString());

                                if (comm_data.building_money[i] > 80000000)
                                {
                                    comm_data.building_money[i] = 80000000;
                                }
                                else if (comm_data.building_money[i] < -80000000)
                                {
                                    comm_data.building_money[i] = -80000000;
                                }

                                if (comm_data.update_outside_count == 63)
                                {
                                    comm_data.building_money[i] = 0;
                                }
                            }

                            if (instance.m_buildings.m_buffer[i].Info.m_class.m_service == ItemClass.Service.PoliceDepartment)
                            {
                                result = 0;
                                budget = Singleton<EconomyManager>.instance.GetBudget(instance.m_buildings.m_buffer[i].Info.m_class);
                                result = instance.m_buildings.m_buffer[i].Info.m_buildingAI.GetMaintenanceCost() / 100;
                                comm_data.building_money[i] -= (result / 100f) * (float)(budget * (float)(instance.m_buildings.m_buffer[i].m_productionRate / 10000f));
                                pc_OutsideConnectionAI.have_police_building = true;

                                if (comm_data.building_money[i] > 80000000)
                                {
                                    comm_data.building_money[i] = 80000000;
                                }
                                else if (comm_data.building_money[i] < -80000000)
                                {
                                    comm_data.building_money[i] = -80000000;
                                }

                                if (comm_data.update_outside_count == 63)
                                {
                                    comm_data.building_money[i] = 0;
                                }
                            }

                            if ((instance.m_buildings.m_buffer[i].Info.m_class.m_service == ItemClass.Service.HealthCare) && (instance.m_buildings.m_buffer[i].Info.m_class.m_level == ItemClass.Level.Level1))
                            {
                                result = 0;
                                budget = Singleton<EconomyManager>.instance.GetBudget(instance.m_buildings.m_buffer[i].Info.m_class);
                                result = instance.m_buildings.m_buffer[i].Info.m_buildingAI.GetMaintenanceCost() / 100;
                                comm_data.building_money[i] -= (result / 100f) * (float)(budget * (float)(instance.m_buildings.m_buffer[i].m_productionRate / 10000f));
                                pc_OutsideConnectionAI.have_hospital_building = true;

                                if (comm_data.building_money[i] > 80000000)
                                {
                                    comm_data.building_money[i] = 80000000;
                                }
                                else if (comm_data.building_money[i] < -80000000)
                                {
                                    comm_data.building_money[i] = -80000000;
                                }

                                if (comm_data.update_outside_count == 63)
                                {
                                    comm_data.building_money[i] = 0;
                                }
                            }


                            if (instance.m_buildings.m_buffer[i].Info.m_class.m_service == ItemClass.Service.FireDepartment)
                            {
                                result = 0;
                                budget = Singleton<EconomyManager>.instance.GetBudget(instance.m_buildings.m_buffer[i].Info.m_class);
                                result = instance.m_buildings.m_buffer[i].Info.m_buildingAI.GetMaintenanceCost() / 100;
                                comm_data.building_money[i] -= (result / 100f) * (float)(budget * (float)(instance.m_buildings.m_buffer[i].m_productionRate / 10000f));
                                pc_OutsideConnectionAI.have_fire_building = true;

                                if (comm_data.building_money[i] > 80000000)
                                {
                                    comm_data.building_money[i] = 80000000;
                                }
                                else if (comm_data.building_money[i] < -80000000)
                                {
                                    comm_data.building_money[i] = -80000000;
                                }

                                if (comm_data.update_outside_count == 63)
                                {
                                    comm_data.building_money[i] = 0;
                                }
                            }

                            if (instance.m_buildings.m_buffer[i].Info.m_class.m_service == ItemClass.Service.Monument)
                            {
                                result = 0;
                                budget = Singleton<EconomyManager>.instance.GetBudget(instance.m_buildings.m_buffer[i].Info.m_class);
                                result = instance.m_buildings.m_buffer[i].Info.m_buildingAI.GetMaintenanceCost() / 100;
                                comm_data.building_money[i] -= (result / 100f) * (float)(budget * (float)(instance.m_buildings.m_buffer[i].m_productionRate / 10000f));

                                if (comm_data.building_money[i] > 80000000)
                                {
                                    comm_data.building_money[i] = 80000000;
                                }
                                else if (comm_data.building_money[i] < -80000000)
                                {
                                    comm_data.building_money[i] = -80000000;
                                }

                                if (comm_data.update_outside_count == 63)
                                {
                                    comm_data.building_money[i] = 0;
                                }
                            }

                            if (instance.m_buildings.m_buffer[i].Info.m_class.m_service == ItemClass.Service.Beautification)
                            {
                                result = 0;
                                budget = Singleton<EconomyManager>.instance.GetBudget(instance.m_buildings.m_buffer[i].Info.m_class);
                                result = instance.m_buildings.m_buffer[i].Info.m_buildingAI.GetMaintenanceCost() / 100;
                                comm_data.building_money[i] -= (result / 100f) * (float)(budget * (float)(instance.m_buildings.m_buffer[i].m_productionRate / 10000f));

                                if (comm_data.building_money[i] > 80000000)
                                {
                                    comm_data.building_money[i] = 80000000;
                                }
                                else if (comm_data.building_money[i] < -80000000)
                                {
                                    comm_data.building_money[i] = -80000000;
                                }

                                if (comm_data.update_outside_count == 63)
                                {
                                    comm_data.building_money[i] = 0;
                                }
                            }

                            if (is_special_building((ushort)i) == 1)
                            {
                                comm_data.have_bank = true;
                            }
                            else if (is_special_building((ushort)i) == 2)
                            {
                                comm_data.have_toll_station = true;
                            }
                            else if (is_special_building((ushort)i) == 3)
                            {
                                comm_data.have_tax_department = true;
                            }
                        }
                    }
                }

                if (comm_data.have_bank && (!comm_data.have_bank_pre))
                {
                    caculate_bank_money(true);
                } else if ((!comm_data.have_bank) && comm_data.have_bank_pre)
                {
                    caculate_bank_money(false);
                }
                else
                {

                }
                comm_data.have_bank_pre = comm_data.have_bank;
                update_once = true;

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
                    return 2;
                }

                //tax
                if (instance.m_buildings.m_buffer[id].Info.m_buildingAI.GetConstructionCost() == 1008600)
                {
                    //DebugLog.LogToFileOnly(instance.m_buildings.m_buffer[id].Info.m_buildingAI.GetMaintenanceCost().ToString());
                    //DebugLog.LogToFileOnly(instance.m_buildings.m_buffer[id].Info.m_buildingAI.GetElectricityConsumption().ToString());
                    //DebugLog.LogToFileOnly(instance.m_buildings.m_buffer[id].Info.m_buildingAI.GetWaterConsumption().ToString());
                    return 3;
                }

                return 0;
            }


            public void caculate_bank_money(bool is_collection)
            {
                long bank_money = 0;
                for (int i = 0; i < Singleton<BuildingManager>.instance.m_buildings.m_buffer.Count<Building>(); i++)
                {
                    if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[i].m_flags.IsFlagSet(Building.Flags.Created) && Singleton<BuildingManager>.instance.m_buildings.m_buffer[i].m_flags.IsFlagSet(Building.Flags.Active) && !Singleton<BuildingManager>.instance.m_buildings.m_buffer[i].m_flags.IsFlagSet(Building.Flags.Deleted) && !Singleton<BuildingManager>.instance.m_buildings.m_buffer[i].m_flags.IsFlagSet(Building.Flags.Untouchable))
                    {
                        if (comm_data.building_money[i] > -65000000)
                        {
                            if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[i].Info.m_class.m_service == ItemClass.Service.Commercial)
                            {
                                bank_money += (long)comm_data.building_money[i];
                            }
                            if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[i].Info.m_class.m_service == ItemClass.Service.Office)
                            {
                                bank_money += (long)comm_data.building_money[i];
                            }
                            if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[i].Info.m_class.m_service == ItemClass.Service.Industrial)
                            {
                                bank_money += (long)comm_data.building_money[i];
                            }
                        }
                    }
                }

                for (int i = 0; i < Singleton<CitizenManager>.instance.m_citizens.m_buffer.Count<Citizen>(); i++)
                {
                    bank_money += (long)comm_data.citizen_money[i];
                }

                if (is_collection)
                {
                    comm_data.city_bank = bank_money;
                } else
                {
                    Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, (int)(bank_money - comm_data.city_bank), ItemClass.Service.Beautification, ItemClass.SubService.None, ItemClass.Level.Level1);
                }
            }

            public void citizen_status()
            {
                comm_data.citizen_count = (int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_populationData.m_finalCount;

                uint medium_citizen = (uint)(comm_data.family_count - comm_data.family_weight_stable_high - comm_data.family_weight_stable_low);
                if (medium_citizen < 0)
                {
                    DebugLog.LogToFileOnly("should be wrong, medium_citizen < 0");
                    medium_citizen = 0;
                }


                //comm_data.mantain_and_land_fee_decrease = (byte)(1000f / (Singleton<DistrictManager>.instance.m_districts.m_buffer[0].GetLandValue() + 10f));
                comm_data.salary_idex = (Singleton<DistrictManager>.instance.m_districts.m_buffer[0].GetLandValue() + 50f) / 120f;
            }

            public void vehicle_status()
            {
                VehicleManager instance = Singleton<VehicleManager>.instance;
                for (int i = 0; i < 16384; i = i + 1)
                {
                    System.Random rand = new System.Random();
                    Vehicle vehicle = instance.m_vehicles.m_buffer[i];
                    if (vehicle.m_flags.IsFlagSet(Vehicle.Flags.Created) && !vehicle.m_flags.IsFlagSet(Vehicle.Flags.Deleted))
                    {
                        if ((vehicle.Info.m_vehicleType == VehicleInfo.VehicleType.Car) && (vehicle.Info.m_class.m_subService != ItemClass.SubService.PublicTransportTaxi))
                        {
                            if (!vehicle.m_flags.IsFlagSet(Vehicle.Flags.Stopped))
                            {
                                comm_data.vehical_transfer_time[i] = (ushort)(comm_data.vehical_transfer_time[i] + 1);
                            }
                            else
                            {
                                comm_data.vehical_transfer_time[i] = 0;
                            }

                            if (vehicle.Info.m_vehicleAI is GarbageTruckAI)
                            {
                                Singleton<NaturalResourceManager>.instance.TryDumpResource(NaturalResourceManager.Resource.Pollution, rand.Next(9), rand.Next(9), vehicle.GetLastFramePosition(), 6f);
                                //DebugLog.LogToFileOnly("try give GarbageTruckAI Pollution");
                            }

                            if (vehicle.Info.m_vehicleAI is CargoTruckAI)
                            {
                                if ((TransferManager.TransferReason)vehicle.m_transferType == TransferManager.TransferReason.Oil)
                                {
                                    Singleton<NaturalResourceManager>.instance.TryDumpResource(NaturalResourceManager.Resource.Pollution, rand.Next(9), rand.Next(9), vehicle.GetLastFramePosition(), 6f);
                                }

                                if ((TransferManager.TransferReason)vehicle.m_transferType == TransferManager.TransferReason.Ore)
                                {
                                    Singleton<NaturalResourceManager>.instance.TryDumpResource(NaturalResourceManager.Resource.Pollution, rand.Next(9), rand.Next(9), vehicle.GetLastFramePosition(), 6f);
                                }
                            }
                        }
                    }
                    else
                    {
                        comm_data.vehical_transfer_time[i] = 0;
                    }
                }
            }

            public void caculate_profit()
            {
                if (comm_data.update_outside_count > 64)
                {
                    comm_data.update_outside_count = 0;
                }
                comm_data.update_outside_count++;
                /*float lumber_export_ratio = 0;
                float lumber_import_ratio = 0;
                float petrol_export_ratio = 0;
                float petrol_import_ratio = 0;
                float coal_export_ratio = 0;
                float coal_import_ratio = 0;
                float food_export_ratio = 0;
                float food_import_ratio = 0;
                float logs_export_ratio = 0;
                float logs_import_ratio = 0;
                float grain_export_ratio = 0;
                float grain_import_ratio = 0;
                float oil_export_ratio = 0;
                float oil_import_ratio = 0;
                float ore_export_ratio = 0;
                float ore_import_ratio = 0;
                float good_export_ratio = 0;
                float good_import_ratio = 0;*/
                //lumber
                if ((pc_PrivateBuildingAI.lumber_from_outside_count_final + pc_PrivateBuildingAI.lumber_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.lumber_import_ratio = (float)pc_PrivateBuildingAI.lumber_from_outside_count_final / (float)(pc_PrivateBuildingAI.lumber_from_outside_count_final + pc_PrivateBuildingAI.lumber_to_industy_count_final);
                }
                else
                {
                    //pc_PrivateBuildingAI.lumber_import_ratio = 1;
                }

                if ((pc_PrivateBuildingAI.lumber_to_outside_count_final + pc_PrivateBuildingAI.lumber_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.lumber_export_ratio = (float)pc_PrivateBuildingAI.lumber_to_outside_count_final / (float)(pc_PrivateBuildingAI.lumber_to_outside_count_final + pc_PrivateBuildingAI.lumber_to_industy_count_final);
                }
                else
                {
                    //pc_PrivateBuildingAI.lumber_export_ratio = 1;
                }
                //food
                if ((pc_PrivateBuildingAI.food_from_outside_count_final + pc_PrivateBuildingAI.food_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.food_import_ratio = (float)pc_PrivateBuildingAI.food_from_outside_count_final / (float)(pc_PrivateBuildingAI.food_from_outside_count_final + pc_PrivateBuildingAI.food_to_industy_count_final);
                }
                else
                {
                    //pc_PrivateBuildingAI.food_import_ratio = 1;
                }

                if ((pc_PrivateBuildingAI.food_to_outside_count_final + pc_PrivateBuildingAI.food_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.food_export_ratio = (float)pc_PrivateBuildingAI.food_to_outside_count_final / (float)(pc_PrivateBuildingAI.food_to_outside_count_final + pc_PrivateBuildingAI.food_to_industy_count_final);
                }
                else
                {
                    //pc_PrivateBuildingAI.food_export_ratio = 1;
                }
                //petrol
                if ((pc_PrivateBuildingAI.Petrol_from_outside_count_final + pc_PrivateBuildingAI.Petrol_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.petrol_import_ratio = (float)pc_PrivateBuildingAI.Petrol_from_outside_count_final / (float)(pc_PrivateBuildingAI.Petrol_from_outside_count_final + pc_PrivateBuildingAI.Petrol_to_industy_count_final);
                }
                else
                {
                    //pc_PrivateBuildingAI.petrol_import_ratio = 1;
                }

                if ((pc_PrivateBuildingAI.Petrol_to_outside_count_final + pc_PrivateBuildingAI.Petrol_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.petrol_export_ratio = (float)pc_PrivateBuildingAI.Petrol_to_outside_count_final / (float)(pc_PrivateBuildingAI.Petrol_to_outside_count_final + pc_PrivateBuildingAI.Petrol_to_industy_count_final);
                }
                else
                {
                    //pc_PrivateBuildingAI.petrol_export_ratio = 1;
                }
                //coal
                if ((pc_PrivateBuildingAI.coal_from_outside_count_final + pc_PrivateBuildingAI.coal_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.coal_import_ratio = (float)pc_PrivateBuildingAI.coal_from_outside_count_final / (float)(pc_PrivateBuildingAI.coal_from_outside_count_final + pc_PrivateBuildingAI.coal_to_industy_count_final);
                }
                else
                {
                    //pc_PrivateBuildingAI.coal_import_ratio = 1f;
                }

                if ((pc_PrivateBuildingAI.coal_to_outside_count_final + pc_PrivateBuildingAI.coal_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.coal_export_ratio = (float)pc_PrivateBuildingAI.coal_to_outside_count_final / (float)(pc_PrivateBuildingAI.coal_to_outside_count_final + pc_PrivateBuildingAI.coal_to_industy_count_final);
                }
                else
                {
                    //pc_PrivateBuildingAI.coal_export_ratio = 1f;
                }
                //logs
                if ((pc_PrivateBuildingAI.logs_from_outside_count_final + pc_PrivateBuildingAI.logs_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.log_import_ratio = (float)pc_PrivateBuildingAI.logs_from_outside_count_final / (float)(pc_PrivateBuildingAI.logs_from_outside_count_final + pc_PrivateBuildingAI.logs_to_industy_count_final);
                }
                else
                {
                    //pc_PrivateBuildingAI.log_import_ratio = 1;
                }

                if ((pc_PrivateBuildingAI.logs_to_outside_count_final + pc_PrivateBuildingAI.logs_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.log_export_ratio = (float)pc_PrivateBuildingAI.logs_to_outside_count_final / (float)(pc_PrivateBuildingAI.logs_to_outside_count_final + pc_PrivateBuildingAI.logs_to_industy_count_final);
                }
                else
                {
                    //pc_PrivateBuildingAI.log_export_ratio = 1;
                }
                //grain
                if ((pc_PrivateBuildingAI.Grain_from_outside_count_final + pc_PrivateBuildingAI.Grain_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.grain_import_ratio = (float)pc_PrivateBuildingAI.Grain_from_outside_count_final / (float)(pc_PrivateBuildingAI.Grain_from_outside_count_final + pc_PrivateBuildingAI.Grain_to_industy_count_final);
                }
                else
                {
                    //pc_PrivateBuildingAI.grain_import_ratio = 1;
                }

                if ((pc_PrivateBuildingAI.Grain_to_outside_count_final + pc_PrivateBuildingAI.Grain_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.grain_export_ratio = (float)pc_PrivateBuildingAI.Grain_to_outside_count_final / (float)(pc_PrivateBuildingAI.Grain_to_outside_count_final + pc_PrivateBuildingAI.Grain_to_industy_count_final);
                }
                else
                {
                    //pc_PrivateBuildingAI.grain_export_ratio = 1;
                }
                //oil
                if ((pc_PrivateBuildingAI.oil_from_outside_count_final + pc_PrivateBuildingAI.oil_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.oil_import_ratio = (float)pc_PrivateBuildingAI.oil_from_outside_count_final / (float)(pc_PrivateBuildingAI.oil_from_outside_count_final + pc_PrivateBuildingAI.oil_to_industy_count_final);
                }
                else
                {
                    //pc_PrivateBuildingAI.oil_import_ratio = 1;
                }

                if ((pc_PrivateBuildingAI.oil_to_outside_count_final + pc_PrivateBuildingAI.oil_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.oil_export_ratio = (float)pc_PrivateBuildingAI.oil_to_outside_count_final / (float)(pc_PrivateBuildingAI.oil_to_outside_count_final + pc_PrivateBuildingAI.oil_to_industy_count_final);
                }
                else
                {
                    //pc_PrivateBuildingAI.oil_export_ratio = 1;
                }
                //ore
                if ((pc_PrivateBuildingAI.ore_from_outside_count_final + pc_PrivateBuildingAI.ore_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.ore_import_ratio = (float)pc_PrivateBuildingAI.ore_from_outside_count_final / (float)(pc_PrivateBuildingAI.ore_from_outside_count_final + pc_PrivateBuildingAI.ore_to_industy_count_final);
                }
                else
                {
                    //pc_PrivateBuildingAI.ore_import_ratio = 1;
                }

                if ((pc_PrivateBuildingAI.ore_to_outside_count_final + pc_PrivateBuildingAI.ore_to_industy_count_final) != 0)
                {
                    pc_PrivateBuildingAI.ore_export_ratio = (float)pc_PrivateBuildingAI.ore_to_outside_count_final / (float)(pc_PrivateBuildingAI.ore_to_outside_count_final + pc_PrivateBuildingAI.ore_to_industy_count_final);
                }
                else
                {
                    //pc_PrivateBuildingAI.ore_export_ratio = 1;
                }
                //good
                if ((pc_PrivateBuildingAI.shop_get_goods_from_local_count_level1_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final + pc_PrivateBuildingAI.shop_get_goods_from_outside_count_final) != 0)
                {
                    pc_PrivateBuildingAI.good_import_ratio = (float)pc_PrivateBuildingAI.shop_get_goods_from_outside_count_final / (float)(pc_PrivateBuildingAI.shop_get_goods_from_local_count_level1_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final + pc_PrivateBuildingAI.shop_get_goods_from_outside_count_final);
                }
                else
                {
                    //pc_PrivateBuildingAI.good_import_ratio = 1;
                }

                if ((pc_PrivateBuildingAI.shop_get_goods_from_local_count_level1_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final + pc_PrivateBuildingAI.industy_goods_to_outside_count_final) != 0)
                {
                    pc_PrivateBuildingAI.good_export_ratio = (float)pc_PrivateBuildingAI.industy_goods_to_outside_count_final / (float)(pc_PrivateBuildingAI.shop_get_goods_from_local_count_level1_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final + pc_PrivateBuildingAI.industy_goods_to_outside_count_final);
                }
                else
                {
                    //pc_PrivateBuildingAI.good_export_ratio = 1;
                }

                if ((pc_PrivateBuildingAI.shop_get_goods_from_local_count_level1_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final + pc_PrivateBuildingAI.shop_get_goods_from_outside_count) != 0)
                {
                    pc_PrivateBuildingAI.good_level2_ratio = (float)(pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final) / (float)(pc_PrivateBuildingAI.shop_get_goods_from_local_count_level1_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final + pc_PrivateBuildingAI.shop_get_goods_from_outside_count);
                }
                else
                {
                    //pc_PrivateBuildingAI.good_level2_ratio = 0f;
                }

                if ((pc_PrivateBuildingAI.shop_get_goods_from_local_count_level1_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final + pc_PrivateBuildingAI.shop_get_goods_from_outside_count) != 0)
                {
                    pc_PrivateBuildingAI.good_level3_ratio = (float)(pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final) / (float)(pc_PrivateBuildingAI.shop_get_goods_from_local_count_level1_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final + pc_PrivateBuildingAI.shop_get_goods_from_outside_count);
                }
                else
                {
                    //pc_PrivateBuildingAI.good_level3_ratio = 0f;
                }
                /*pc_PrivateBuildingAI.comm_profit = 0.2f; //update later
                pc_PrivateBuildingAI.indu_profit = (float)(5f + 2f * (5f - good_export_ratio - food_import_ratio - lumber_import_ratio - petrol_import_ratio - coal_import_ratio))/100f;
                pc_PrivateBuildingAI.food_profit = (float)(5f + 5f * (2f - food_export_ratio - grain_import_ratio))/100f;
                pc_PrivateBuildingAI.lumber_profit = (float)(5f + 5f * (2f - lumber_export_ratio - logs_import_ratio))/100f;
                pc_PrivateBuildingAI.coal_profit = (float)(5f + 5f * (2f - coal_export_ratio - ore_import_ratio))/100f;
                pc_PrivateBuildingAI.petrol_profit = (float)(5f + 5f * (2f - petrol_export_ratio - oil_import_ratio))/100f;

                pc_PrivateBuildingAI.log_profit = (float)(5f + 10f * (1f - logs_export_ratio))/100f;
                pc_PrivateBuildingAI.grain_profit = (float)(5f + 10f * (1f - grain_export_ratio))/100f;
                pc_PrivateBuildingAI.oil_profit = (float)(5f + 10f * (1f - oil_export_ratio))/100f;
                pc_PrivateBuildingAI.ore_profit = (float)(5f + 10f * (1f - ore_export_ratio))/100f;*/

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

                    if (comm_data.have_toll_station)
                    {
                        for (int i = num5; i <= num6; i = i + 1)
                        {
                            Vehicle vehicle = instance.m_vehicles.m_buffer[i];
                            Building building = Singleton<BuildingManager>.instance.m_buildings.m_buffer[vehicle.m_sourceBuilding];
                            Building building1 = Singleton<BuildingManager>.instance.m_buildings.m_buffer[vehicle.m_targetBuilding];
                            if (vehicle.m_flags.IsFlagSet(Vehicle.Flags.Created) && !vehicle.m_flags.IsFlagSet(Vehicle.Flags.Deleted))
                            {
                                ushort num = FindToll(vehicle.GetFramePosition(currentFrameIndex), 32f);
                                if (num != 0)
                                {
                                    if (building.m_flags.IsFlagSet(Building.Flags.Untouchable) && building1.m_flags.IsFlagSet(Building.Flags.Untouchable) && (!comm_data.vehical_flag[i]))
                                    {
                                        if ((vehicle.Info.m_vehicleAI is PassengerCarAI) || (vehicle.Info.m_vehicleAI is CargoTruckAI))
                                        {
                                            comm_data.vehical_flag[i] = true;
                                            Singleton<EconomyManager>.instance.AddPrivateIncome(500, ItemClass.Service.Road, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                                        }
                                    }
                                }
                            }
                        }
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
                                if (building.m_buildings.m_buffer[(int)num6].m_flags.IsFlagSet(Building.Flags.Active) || (!building.m_buildings.m_buffer[(int)num6].m_flags.IsFlagSet(Building.Flags.Deleted)))
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

