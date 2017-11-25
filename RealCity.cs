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
            streamWriter.WriteLine(comm_data.is_help_company);
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
                    comm_data.is_help_company = false;
                }
                else
                {
                    comm_data.is_help_company = true;
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
            group.AddDropdown(language.OptionUI[1], new string[] { "English", "简体中文"}, comm_data.last_language, (index) => get_language_idex(index));

            UIHelperBase group1 = helper.AddGroup(language.OptionUI[2]);
            group1.AddCheckbox(language.OptionUI[3], comm_data.garbage_connection, (index) => get_garbage_connection(index));
            group1.AddCheckbox(language.OptionUI[4], comm_data.dead_connection, (index) => get_dead_connection(index));
            group1.AddCheckbox(language.OptionUI[5], comm_data.crime_connection, (index) => get_crime_connection(index));
            group1.AddCheckbox(language.OptionUI[6], comm_data.sick_connection, (index) => get_sick_connection(index));
            group1.AddCheckbox(language.OptionUI[10], comm_data.fire_connection, (index) => get_fire_connection(index));
            group1.AddCheckbox(language.OptionUI[11], comm_data.road_connection, (index) => get_road_connection(index));
            //group1.AddCheckbox(language.OptionUI[12], comm_data.firehelp, (index) => outsidefirehelp(index));
            group1.AddCheckbox(language.OptionUI[13], comm_data.hospitalhelp, (index) => outsidehospitalhelp(index));
            group1.AddCheckbox(language.OptionUI[14], comm_data.policehelp, (index) => outsidepolicehelp(index));

            UIHelperBase group2 = helper.AddGroup(language.OptionUI[7]);
            group2.AddCheckbox(language.OptionUI[8], comm_data.is_help_resident, (index) => is_help_resident(index));
            group2.AddCheckbox(language.OptionUI[9], comm_data.is_help_company, (index) => is_help_company(index));
            SaveSetting();
        }

        public void get_language_idex ( int index)
        {
            language_idex = index;
            language.language_switch((byte)language_idex);
            SaveSetting();
            MethodInfo method = typeof(OptionsMainPanel).GetMethod("OnLocaleChanged", BindingFlags.Instance | BindingFlags.NonPublic);
            method.Invoke(UIView.library.Get<OptionsMainPanel>("OptionsPanel"), new object[0]);
            //DebugLog.LogToFileOnly("get_current language idex = " + language_idex.ToString());
        }

        public void is_help_resident(bool index)
        {
            comm_data.is_help_resident = index;
            SaveSetting();
        }

        public void is_help_company(bool index)
        {
            comm_data.is_help_company = index;
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

        public void outsidefirehelp (bool index)
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
                comm_data.current_time = Singleton<SimulationManager>.instance.m_currentDayTimeHour;
                uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
                uint num2 = currentFrameIndex & 255u;
                //DebugLog.LogToFileOnly("OnUpdateMoneyAmount num2 = " + num2.ToString());
                if ((num2 == 255u) && (comm_data.current_time != comm_data.prev_time))
                {
                    //DebugLog.LogToFileOnly("process once update money");
                    citizen_status();
                    vehicle_status();
                    building_status();
                    caculate_goverment_employee_expense();
                    caculate_profit();
                    caculate_citizen_transport_fee();
                    generate_tips();

                    //initialize building ui again

                    Loader.guiPanel2.transform.parent = Loader.buildingInfo.transform;
                    Loader.guiPanel2.size = new Vector3(Loader.buildingInfo.size.x, Loader.buildingInfo.size.y);
                    Loader.guiPanel2.baseBuildingWindow = Loader.buildingInfo.gameObject.transform.GetComponentInChildren<ZonedBuildingWorldInfoPanel>();
                    Loader.guiPanel2.position = new Vector3(Loader.buildingInfo.size.x, Loader.buildingInfo.size.y);

                    //initialize human ui again
                    Loader.guiPanel3.transform.parent = Loader.HumanInfo.transform;
                    Loader.guiPanel3.size = new Vector3(Loader.HumanInfo.size.x, Loader.HumanInfo.size.y);
                    Loader.guiPanel3.baseBuildingWindow = Loader.HumanInfo.gameObject.transform.GetComponentInChildren<CitizenWorldInfoPanel>();
                    Loader.guiPanel3.position = new Vector3(Loader.HumanInfo.size.x, Loader.HumanInfo.size.y);

                    /*if ((language.current_language != comm_data.last_language) || (language.current_language == 255))
                    {
                        language.language_switch((byte)language_idex);
                    }*/
                    comm_data.is_updated = true;
                    comm_data.update_money_count++;
                    if (comm_data.update_money_count == 17)
                    {
                        comm_data.update_money_count = 0;
                    }
                    pc_EconomyManager.clean_current(comm_data.update_money_count);
                    comm_data.prev_time = comm_data.current_time;
                    //DebugLog.LogToFileOnly("update_money_count is " + comm_data.update_money_count.ToString());
                 }
                //if(num2 != 255u)
                //{
                //    comm_data.is_updated = false;
                //}
                return internalMoneyAmount;
            }

            public void generate_tips()
            {
                if (comm_data.family_count != 0)
                {
                    if (comm_data.citizen_salary_per_family - comm_data.citizen_expense_per_family - (int)(comm_data.citizen_salary_tax_total / comm_data.family_count) - comm_data.citizen_average_transport_fee < 10)
                    {
                        if (comm_data.citizen_expense_per_family > 35)
                        {
                            try_say_something(language.TipAndChirperMessage[0]);
                            try_say_something(language.TipAndChirperMessage[1]);
                            try_say_something(language.TipAndChirperMessage[2]);
                            tip1_message_forgui = language.TipAndChirperMessage[3];

                        } else if (comm_data.citizen_average_transport_fee > 25)
                        {
                            try_say_something(language.TipAndChirperMessage[4]);
                            try_say_something(language.TipAndChirperMessage[5]);
                            tip1_message_forgui = language.TipAndChirperMessage[6];
                        } else
                        {
                            try_say_something(language.TipAndChirperMessage[7]);
                            try_say_something(language.TipAndChirperMessage[8]);
                            tip1_message_forgui = language.TipAndChirperMessage[9];
                        }
                    }
                    else if (comm_data.citizen_salary_per_family < 30)
                    {
                        try_say_something(language.TipAndChirperMessage[7]);
                        try_say_something(language.TipAndChirperMessage[8]);
                        tip1_message_forgui = language.TipAndChirperMessage[9];
                    }
                    else
                    {
                        try_say_something(language.TipAndChirperMessage[10]);
                        tip1_message_forgui = language.TipAndChirperMessage[11];
                    }
                }

                if (pc_PrivateBuildingAI.all_comm_building_loss_final + pc_PrivateBuildingAI.all_comm_building_profit_final > 0)
                {
                    if (pc_PrivateBuildingAI.all_comm_building_profit_final >= pc_PrivateBuildingAI.all_comm_building_loss_final)
                    {
                        try_say_something(language.TipAndChirperMessage[12]);
                        tip2_message_forgui = language.TipAndChirperMessage[13];
                    }
                    else
                    {
                        try_say_something(language.TipAndChirperMessage[14]);
                        try_say_something(language.TipAndChirperMessage[15]);
                        tip2_message_forgui = language.TipAndChirperMessage[16];
                    }
                } else
                {
                    tip2_message_forgui = "";
                }

                int profit_building_num = 0;
                int loss_building_num = 0;
                profit_building_num += pc_PrivateBuildingAI.all_farmer_building_profit_final;
                profit_building_num += pc_PrivateBuildingAI.all_foresty_building_profit_final;
                profit_building_num += pc_PrivateBuildingAI.all_oil_building_profit_final;
                profit_building_num += pc_PrivateBuildingAI.all_ore_building_profit_final;
                profit_building_num += pc_PrivateBuildingAI.all_industry_building_profit_final;

                loss_building_num += pc_PrivateBuildingAI.all_farmer_building_loss_final;
                loss_building_num += pc_PrivateBuildingAI.all_foresty_building_loss_final;
                loss_building_num += pc_PrivateBuildingAI.all_oil_building_loss_final;
                loss_building_num += pc_PrivateBuildingAI.all_ore_building_loss_final;
                loss_building_num += pc_PrivateBuildingAI.all_industry_building_loss_final;

                if (profit_building_num + loss_building_num > 0)
                {
                    if (profit_building_num >= loss_building_num)
                    {
                        try_say_something(language.TipAndChirperMessage[17]);
                        tip2_message_forgui += language.TipAndChirperMessage[18];
                    }
                    else
                    {
                        try_say_something(language.TipAndChirperMessage[19]);
                        tip2_message_forgui += language.TipAndChirperMessage[20];
                    }
                }
                else
                {
                    tip2_message_forgui += "";
                }

                if (!pc_OutsideConnectionAI.have_maintain_road_building && (comm_data.road_connection))
                {
                    //try_say_something(language.TipAndChirperMessage[21]);
                    tip3_message_forgui = language.TipAndChirperMessage[22];
                }
                else
                {
                    //try_say_something(language.TipAndChirperMessage[23]);
                    tip3_message_forgui = language.TipAndChirperMessage[24];
                }

                if (!pc_OutsideConnectionAI.have_garbage_building && (comm_data.garbage_connection))
                {
                    try_say_something(language.TipAndChirperMessage[25]);
                    tip4_message_forgui = language.TipAndChirperMessage[26];
                }
                else
                {
                    try_say_something(language.TipAndChirperMessage[27]);
                    tip4_message_forgui = language.TipAndChirperMessage[28];
                }

                if (!pc_OutsideConnectionAI.have_cemetry_building && (comm_data.dead_connection))
                {
                    try_say_something(language.TipAndChirperMessage[29]);
                    tip5_message_forgui = language.TipAndChirperMessage[30];
                }
                else
                {
                    try_say_something(language.TipAndChirperMessage[31]);
                    tip5_message_forgui = language.TipAndChirperMessage[32];
                }

                if (!pc_OutsideConnectionAI.have_hospital_building && (comm_data.sick_connection))
                {
                    try_say_something(language.TipAndChirperMessage[33]);
                    tip6_message_forgui = language.TipAndChirperMessage[34];
                }
                else
                {
                    try_say_something(language.TipAndChirperMessage[35]);
                    tip6_message_forgui = language.TipAndChirperMessage[36];
                }

                if (!pc_OutsideConnectionAI.have_fire_building && (comm_data.fire_connection))
                {
                    try_say_something(language.TipAndChirperMessage[37]);
                    tip7_message_forgui = language.TipAndChirperMessage[38];
                }
                else
                {
                    try_say_something(language.TipAndChirperMessage[39]);
                    tip7_message_forgui = language.TipAndChirperMessage[40];
                }

                if (!pc_OutsideConnectionAI.have_police_building && (comm_data.crime_connection))
                {
                    try_say_something(language.TipAndChirperMessage[41]);
                    tip8_message_forgui = language.TipAndChirperMessage[42];
                }
                else
                {
                    try_say_something(language.TipAndChirperMessage[43]);
                    tip8_message_forgui = language.TipAndChirperMessage[44];
                }


                tip9_message_forgui = language.TipAndChirperMessage[45];
            }

            public void try_say_something(string message)
            {
                System.Random rand = new System.Random();
                if (rand.Next(150) < 2)
                {
                    //DebugLog.LogToFileOnly("try_say_something" + message);
                    MessageManager ms = Singleton<MessageManager>.instance;
                    ms.QueueMessage(new Message(ms.GetRandomResidentID(), message));
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

                //assume that 1 time will cost 3fen car oil money
                comm_data.all_transport_fee = comm_data.public_transport_fee + comm_data.temp_total_citizen_vehical_time_last * 3;

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
                pc_OutsideConnectionAI.have_maintain_road_building = false;
                pc_OutsideConnectionAI.have_garbage_building = false;
                pc_OutsideConnectionAI.have_cemetry_building = false;
                pc_OutsideConnectionAI.have_police_building = false;
                pc_OutsideConnectionAI.have_fire_building = false;
                pc_OutsideConnectionAI.have_hospital_building = false;
                checked
                {
                    for (int i = 0; i < instance.m_buildings.m_buffer.Count<Building>(); i++)
                    {
                        if (instance.m_buildings.m_buffer[i].m_flags.IsFlagSet(Building.Flags.Created) && !instance.m_buildings.m_buffer[i].m_flags.IsFlagSet(Building.Flags.Deleted) && !instance.m_buildings.m_buffer[i].m_flags.IsFlagSet(Building.Flags.Untouchable))
                        {
                            if ((instance.m_buildings.m_buffer[i].Info.m_class.m_service == ItemClass.Service.HealthCare) && (instance.m_buildings.m_buffer[i].Info.m_class.m_level == ItemClass.Level.Level2))
                            {
                                pc_OutsideConnectionAI.have_cemetry_building = true;
                            }

                            if (instance.m_buildings.m_buffer[i].Info.m_class.m_service == ItemClass.Service.Garbage)
                            {
                                pc_OutsideConnectionAI.have_garbage_building = true;
                            }

                            if (instance.m_buildings.m_buffer[i].Info.m_class.m_service == ItemClass.Service.Road)
                            {
                                pc_OutsideConnectionAI.have_maintain_road_building = true;
                            }

                            if (instance.m_buildings.m_buffer[i].Info.m_class.m_service == ItemClass.Service.PoliceDepartment)
                            {
                                pc_OutsideConnectionAI.have_police_building = true;
                            }

                            if ((instance.m_buildings.m_buffer[i].Info.m_class.m_service == ItemClass.Service.HealthCare) && (instance.m_buildings.m_buffer[i].Info.m_class.m_level == ItemClass.Level.Level1))
                            {
                                pc_OutsideConnectionAI.have_hospital_building = true;
                            }


                            if (instance.m_buildings.m_buffer[i].Info.m_class.m_service == ItemClass.Service.FireDepartment)
                            {
                                pc_OutsideConnectionAI.have_fire_building = true;
                            }

                            //if (instance.m_buildings.m_buffer[i].Info.m_class.m_service == ItemClass.Service.Commercial)
                            //{
                                //DebugLog.LogToFileOnly("angle and length" + instance.m_buildings.m_buffer[i].m_angle.ToString() + instance.m_buildings.m_buffer[i].m_length.ToString());
                            //}

                            if (pc_OutsideConnectionAI.have_fire_building && pc_OutsideConnectionAI.have_hospital_building && pc_OutsideConnectionAI.have_garbage_building && pc_OutsideConnectionAI.have_maintain_road_building && pc_OutsideConnectionAI.have_cemetry_building && pc_OutsideConnectionAI.have_police_building)
                            {
                                break;
                            }
                        }
                    }
                }

                if (pc_OutsideConnectionAI.have_cemetry_building)
                {
                    //MessageManager ms = Singleton<MessageManager>.instance;
                    //ms.QueueMessage(new Message(ms.GetRandomResidentID(), "we have cemetry now!"));
                }

                if (pc_OutsideConnectionAI.have_police_building)
                {
                    //DebugLog.LogToFileOnly("we have police now");
                    //MessageManager ms = Singleton<MessageManager>.instance;
                    //ms.QueueMessage(new Message(ms.GetRandomResidentID(), "we have cemetry now!"));
                }

                update_once = true;


                int office_gen_num = pc_PrivateBuildingAI.all_office_level1_building_num_final + pc_PrivateBuildingAI.all_office_level2_building_num_final + pc_PrivateBuildingAI.all_office_level3_building_num_final;
                int profit_building_num = 0;
                int high_educated_data = 0;
                int medium_educated_data = 0;
                int low_educated_data = 0;
                profit_building_num += pc_PrivateBuildingAI.all_farmer_building_profit_final;
                profit_building_num += pc_PrivateBuildingAI.all_foresty_building_profit_final;
                profit_building_num += pc_PrivateBuildingAI.all_oil_building_profit_final;
                profit_building_num += pc_PrivateBuildingAI.all_ore_building_profit_final;
                profit_building_num += pc_PrivateBuildingAI.all_industry_building_profit_final;
                if (office_gen_num != 0)
                {
                    if (comm_data.citizen_count != 0)
                    {
                        high_educated_data = (int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_educated3Data.m_finalCount;
                        medium_educated_data = (int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_educated2Data.m_finalCount;
                        low_educated_data = (int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_educated1Data.m_finalCount;
                        //pc_PrivateBuildingAI.office_gen_salary_index = ((2.5f * high_educated_data + 1.5f * medium_educated_data + 0.5f * low_educated_data) * profit_building_num) / (comm_data.citizen_count * office_gen_num);
                    }
                }

                if (pc_PrivateBuildingAI.all_office_high_tech_building_num_final != 0)
                {
                    if (comm_data.citizen_count != 0)
                    {
                        high_educated_data = (int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_educated3Data.m_finalCount;
                        medium_educated_data = (int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_educated2Data.m_finalCount;
                        low_educated_data = (int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_educated1Data.m_finalCount;
                        //pc_PrivateBuildingAI.office_high_tech_salary_index = ((2.5f * high_educated_data + 1.5f * medium_educated_data + 0.5f * low_educated_data) * pc_PrivateBuildingAI.all_office_level3_building_num_final) / (comm_data.citizen_count * pc_PrivateBuildingAI.all_office_high_tech_building_num_final);
                    }
                }

                //pc_PrivateBuildingAI.office_high_tech_salary_index = (pc_PrivateBuildingAI.office_high_tech_salary_index > 1) ? 1 : pc_PrivateBuildingAI.office_high_tech_salary_index;
                //pc_PrivateBuildingAI.office_high_tech_salary_index = (pc_PrivateBuildingAI.office_high_tech_salary_index < 0.1f) ? 0.1f : pc_PrivateBuildingAI.office_high_tech_salary_index;

                //pc_PrivateBuildingAI.office_gen_salary_index = (pc_PrivateBuildingAI.office_gen_salary_index > 1) ? 1 : pc_PrivateBuildingAI.office_gen_salary_index;
                //pc_PrivateBuildingAI.office_gen_salary_index = (pc_PrivateBuildingAI.office_gen_salary_index < 0.1f) ? 0.1f : pc_PrivateBuildingAI.office_gen_salary_index;
            }


            public void citizen_status()
            {
                comm_data.citizen_count = (int)Singleton<DistrictManager>.instance.m_districts.m_buffer[0].m_populationData.m_finalCount;

                if (comm_data.citizen_count > 100)
                {
                    uint medium_citizen = (uint)(comm_data.family_count - comm_data.family_weight_stable_high - comm_data.family_weight_stable_low);
                    if (medium_citizen < 0)
                    {
                        DebugLog.LogToFileOnly("should be wrong, medium_citizen < 0");
                        medium_citizen = 0;
                    }
                    if (comm_data.family_count != 0)
                    {
                        comm_data.resident_consumption_rate = (float)((float)(2 * comm_data.family_weight_stable_high + medium_citizen / 2) / (float)comm_data.family_count);
                    }
                }
                else
                {
                    //do nothing
                }


                comm_data.mantain_and_land_fee_decrease = (byte)(1000f / (Singleton<DistrictManager>.instance.m_districts.m_buffer[0].GetLandValue() + 10f));
                comm_data.salary_idex = (Singleton<DistrictManager>.instance.m_districts.m_buffer[0].GetLandValue() + 50f) / 120f;

                /*CitizenManager instance = Singleton<CitizenManager>.instance;
                for (int i = 0; i < 524288; i = i + 1)
                {
                    CitizenUnit citizenunit = instance.m_units.m_buffer[i];
                    if (citizenunit.m_flags.IsFlagSet(CitizenUnit.Flags.Created))
                    {
                        int j;
                        for (j = 0; j < 5; j++)
                        {
                            uint citizen = citizenunit.GetCitizen(j);
                            if (citizen != 0u)
                            {
                                CitizenManager instance1 = Singleton<CitizenManager>.instance;
                                CitizenInfo citizenInfo = instance1.m_citizens.m_buffer[(int)((UIntPtr)citizen)].GetCitizenInfo(citizen);
                                //DebugLog.LogToFileOnly("m_citizenAI is " + citizenInfo.m_citizenAI.GetType().ToString());
                                if (citizenInfo.m_class.m_service == ItemClass.Service.Citizen)
                                {
                                    comm_data.citizen_count = comm_data.citizen_count + 1;
                                }
                            }
                        }
                    }
                }*/
            }
            public void vehicle_status()
            {
                VehicleManager instance = Singleton<VehicleManager>.instance;
                for (int i = 0; i < 16384; i = i + 1)
                {
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
                if(comm_data.update_outside_count > 64)
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
                if ((pc_PrivateBuildingAI.shop_get_goods_from_local_count_level1_final + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level2_final  + pc_PrivateBuildingAI.shop_get_goods_from_local_count_level3_final + pc_PrivateBuildingAI.shop_get_goods_from_outside_count_final) != 0)
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
                } else
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
    }
}

