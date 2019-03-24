using ColossalFramework;
using ColossalFramework.Globalization;
using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RealCity.CustomAI;
using RealCity.Util;
using RealCity.UI;
using RealCity.CustomManager;
using ColossalFramework.UI;
using System.Reflection;

namespace RealCity
{
    public class RealCityThreading : ThreadingExtensionBase
    {
        public static bool isFirstTime = true;
        public override void OnBeforeSimulationFrame()
        {
            base.OnBeforeSimulationFrame();
            if (Loader.CurrentLoadMode == LoadMode.LoadGame || Loader.CurrentLoadMode == LoadMode.NewGame)
            {
                if (RealCity.IsEnabled)
                {
                    CheckDetour();
                }
            }
        }

        public void DetourAfterLoad()
        {
            //This is for Detour Other Mod method
            DebugLog.LogToFileOnly("Init DetourAfterLoad");
            bool detourFailed = false;

            DebugLog.LogToFileOnly("Detour AdvancedJunctionRule.NewCarAI::VehicleStatusForRealCity calls");
            if (Loader.isAdvancedJunctionRuleRunning)
            {
                try
                {
                    Assembly as1 = Assembly.Load("AdvancedJunctionRule");
                    Loader.Detours.Add(new Loader.Detour(as1.GetType("AdvancedJunctionRule.CustomAI.NewCarAI").GetMethod("VehicleStatusForRealCity", BindingFlags.Instance | BindingFlags.Public, null, new Type[] {
                typeof(ushort),
                typeof(Vehicle).MakeByRefType()}, null),
                typeof(RealCityCarAI).GetMethod("CustomCarAICustomSimulationStepPreFix", BindingFlags.Instance | BindingFlags.Public, null, new Type[] {
                typeof(ushort),
                typeof(Vehicle).MakeByRefType()}, null)));
                }
                catch (Exception)
                {
                    DebugLog.LogToFileOnly("Could not detour AdvancedJunctionRule.NewCarAI::VehicleStatusForRealCity");
                    detourFailed = true;
                }
            }

            if (detourFailed)
            {
                DebugLog.LogToFileOnly("DetourAfterLoad failed");
            }
            else
            {
                DebugLog.LogToFileOnly("DetourAfterLoad successful");
            }
        }

        public void CheckDetour()
        {
            if (isFirstTime && Loader.DetourInited)
            {
                isFirstTime = false;
                DetourAfterLoad();
                DebugLog.LogToFileOnly("ThreadingExtension.OnBeforeSimulationFrame: First frame detected. Checking detours.");
                List<string> list = new List<string>();
                foreach (Loader.Detour current in Loader.Detours)
                {
                    if (!RedirectionHelper.IsRedirected(current.OriginalMethod, current.CustomMethod))
                    {
                        list.Add(string.Format("{0}.{1} with {2} parameters ({3})", new object[]
                        {
                    current.OriginalMethod.DeclaringType.Name,
                    current.OriginalMethod.Name,
                    current.OriginalMethod.GetParameters().Length,
                    current.OriginalMethod.DeclaringType.AssemblyQualifiedName
                        }));
                    }
                }
                DebugLog.LogToFileOnly(string.Format("ThreadingExtension.OnBeforeSimulationFrame: First frame detected. Detours checked. Result: {0} missing detours", list.Count));
                if (list.Count > 0)
                {
                    string error = "RealCity detected an incompatibility with another mod! You can continue playing but it's NOT recommended. RealCity will not work as expected. See RealCity.txt for technical details.";
                    DebugLog.LogToFileOnly(error);
                    string text = "The following methods were overriden by another mod:";
                    foreach (string current2 in list)
                    {
                        text += string.Format("\n\t{0}", current2);
                    }
                    DebugLog.LogToFileOnly(text);
                    UIView.library.ShowModal<ExceptionPanel>("ExceptionPanel").SetMessage("Incompatibility Issue", text, true);
                }
            }
        }
    }
}
