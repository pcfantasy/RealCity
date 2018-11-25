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
        public static int language_idex = 0;

        public string Name
        {
            get { return "Real City"; }
        }

        public string Description
        {
            get { return "Make your city reality, Combine CS and SimCity in game playing"; }
        }

        public void OnEnabled()
        {
            RealCity.IsEnabled = true;
            FileStream fs = File.Create("RealCity.txt");
            fs.Close();
            LoadSetting();
            SaveSetting();
            Language.LanguageSwitch((byte)language_idex);
        }

        public void OnDisabled()
        {
            RealCity.IsEnabled = false;
            Language.LanguageSwitch((byte)language_idex);
        }


        public static void SaveSetting()
        {
            //save langugae
            FileStream fs = File.Create("RealCityV3.0_setting.txt");
            StreamWriter streamWriter = new StreamWriter(fs);
            streamWriter.WriteLine(MainDataStore.last_language);
            streamWriter.WriteLine(MainDataStore.isSmartPbtp);
            //streamWriter.WriteLine(MainDataStore.isHellMode);
            streamWriter.Flush();
            fs.Close();
        }

        public static void LoadSetting()
        {
            if (File.Exists("RealCityV3.0_setting.txt"))
            {
                FileStream fs = new FileStream("RealCityV3.0_setting.txt", FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string strLine = sr.ReadLine();

                if (strLine == "1")
                {
                    MainDataStore.last_language = 1;
                }
                else
                {
                    MainDataStore.last_language = 0;
                }

                strLine = sr.ReadLine();

                if (strLine == "False")
                {
                    MainDataStore.isSmartPbtp = false;
                }
                else
                {
                    MainDataStore.isSmartPbtp = true;
                }

                //strLine = sr.ReadLine();

                //if (strLine == "False")
                //{
                //    MainDataStore.isHellMode = false;
                //}
                //else
                //{
                //    MainDataStore.isHellMode = true;
                //}
                sr.Close();
                fs.Close();
            }
        }


        public void OnSettingsUI(UIHelperBase helper)
        {

            LoadSetting();
            Language.LanguageSwitch(MainDataStore.last_language);
            UIHelperBase group = helper.AddGroup(Language.OptionUI[0]);
            group.AddDropdown(Language.OptionUI[1], new string[] { "English", "简体中文" }, MainDataStore.last_language, (index) => GetLanguageIdex(index));
            UIHelperBase group2 = helper.AddGroup(Language.OptionUI[2]);
            group2.AddCheckbox(Language.OptionUI[2], MainDataStore.isSmartPbtp, (index) => IsSmartPbtp(index));
            //UIHelperBase group3 = helper.AddGroup(Language.OptionUI[3]);
            //group3.AddCheckbox(Language.OptionUI[3], MainDataStore.isHellMode, (index) => IsHellMode(index));
            SaveSetting();
        }

        public void GetLanguageIdex(int index)
        {
            language_idex = index;
            Language.LanguageSwitch((byte)language_idex);
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
            MainDataStore.isSmartPbtp = index;
            SaveSetting();
        }

        //public void IsHellMode(bool index)
        //{
        //    MainDataStore.isHellMode = index;
        //    SaveSetting();
        //}
    }
}

