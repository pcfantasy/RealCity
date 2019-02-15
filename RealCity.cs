using System;
using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using System.Reflection;
using System.IO;
using System.Linq;
using ColossalFramework.Math;
using UnityEngine;
using RealCity.Util;

namespace RealCity
{

    public class RealCity : IUserMod
    {
        public static bool IsEnabled = false;
        public static int languageIdex = 0;

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
            Language.LanguageSwitch((byte)languageIdex);
        }

        public void OnDisabled()
        {
            RealCity.IsEnabled = false;
            Language.LanguageSwitch((byte)languageIdex);
        }

    }
}

