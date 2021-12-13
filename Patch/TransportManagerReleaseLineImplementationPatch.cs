using HarmonyLib;
using RealCity.CustomData;
using System;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public static class TransportManagerReleaseLineImplementationPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(TransportManager).GetMethod("ReleaseLineImplementation", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(TransportLine).MakeByRefType() }, null);
        }
        public static void Prefix(ushort lineID)
        {
            TransportLineData.WeekDayRush[lineID] = 3;
            TransportLineData.WeekDayLow[lineID] = 3;
            TransportLineData.WeekDayNight[lineID] = 3;
            TransportLineData.WeekEndRush[lineID] = 3;
            TransportLineData.WeekEndLow[lineID] = 3;
            TransportLineData.WeekEndNight[lineID] = 3;
        }
    }
}