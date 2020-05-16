using ICities;
using System.IO;
using RealCity.Util;
using ColossalFramework.UI;
using CitiesHarmony.API;
using RealCity.UI;

namespace RealCity
{
    public class RealCity : IUserMod
    {
        public static bool IsEnabled = false;
        public static bool debugMode = false;
        public static bool reduceVehicle = false;
        public static bool realCityV10 = true;

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
            HarmonyHelper.DoOnHarmonyReady(() => Loader.HarmonyInitDetour());
            if (UIView.GetAView() != null)
            {
                OnGameIntroLoaded();
            }
            else
            {
                LoadingManager.instance.m_introLoaded += OnGameIntroLoaded;
            }
        }

        public void OnDisabled()
        {
            Loader.HarmonyRevertDetour();
            IsEnabled = false;
            LoadingManager.instance.m_introLoaded -= OnGameIntroLoaded;
        }

        private static void OnGameIntroLoaded()
        {
            ModsCompatibilityChecker mcc = new ModsCompatibilityChecker();
            mcc.PerformModCheck();
        }

        public void OnSettingsUI(UIHelperBase helper)
        {
            OptionUI.MakeSettings(helper);
        }

        public static bool GetRealCityV10()
        {
            return realCityV10;
        }

        public static int GetReduceCargoDiv()
        {
            return MainDataStore.reduceCargoDiv;
        }

        public static float GetOutsideTouristMoney()
        {
            return MainDataStore.outsideTouristMoney;
        }

        public static void SetOutsideTouristMoney(float value)
        {
            MainDataStore.outsideTouristMoney = value;
        }

        public static float GetOutsideGovermentMoney()
        {
            return MainDataStore.outsideGovermentMoney;
        }

        public static void SetOutsideGovermentMoney(float value)
        {
            MainDataStore.outsideGovermentMoney = value;
        }
    }
}

