using ICities;
using System.IO;
using RealCity.Util;

namespace RealCity
{
    public class RealCity : IUserMod
    {
        public static bool IsEnabled = false;
        public static bool debugMode = false;
        public static bool reduceVehicle = false;

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
            IsEnabled = true;
            FileStream fs = File.Create("RealCity.txt");
            fs.Close();
        }

        public void OnDisabled()
        {
            IsEnabled = false;
        }

        public static void SaveSetting()
        {
            //save langugae
            FileStream fs = File.Create("RealCity_setting.txt");
            StreamWriter streamWriter = new StreamWriter(fs);
            streamWriter.WriteLine(debugMode);
            streamWriter.WriteLine(reduceVehicle);
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

                if (strLine == "True")
                {
                    debugMode = true;
                }
                else
                {
                    debugMode = false;
                }

                strLine = sr.ReadLine();

                if (strLine == "True")
                {
                    reduceVehicle = true;
                }
                else
                {
                    reduceVehicle = false;
                }

                sr.Close();
                fs.Close();
            }
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            LoadSetting();
            UIHelperBase group = helper.AddGroup(Localization.Get("DEBUG_MODE"));
            group.AddCheckbox(Localization.Get("SHOW_LACK_OF_RESOURCE"), debugMode, (index) => debugModeEnable(index));
            UIHelperBase group1 = helper.AddGroup(Localization.Get("REDUCE_CARGO_DESCRIPTION"));
            group1.AddCheckbox(Localization.Get("REDUCE_CARGO_ENABLE"), reduceVehicle, (index) => reduceVehicleEnable(index));
            SaveSetting();
        }

        public void debugModeEnable(bool index)
        {
            debugMode = index;
            SaveSetting();
        }

        public void reduceVehicleEnable(bool index)
        {
            reduceVehicle = index;
            SaveSetting();
        }
    }
}

