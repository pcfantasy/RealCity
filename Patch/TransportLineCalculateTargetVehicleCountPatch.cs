using ColossalFramework;
using HarmonyLib;
using RealCity.CustomData;
using RealCity.UI;
using System;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class TransportLineCalculateTargetVehicleCountPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(TransportLine).GetMethod("CalculateTargetVehicleCount", BindingFlags.Public | BindingFlags.Instance);
        }

        public static void Postfix(ref TransportLine __instance, ref int __result)
        {
            if (Loader.isTransportLinesManagerRunning) { return; }
            float budget = 1f;
            if (!IsWeekend(Singleton<SimulationManager>.instance.m_currentGameTime))
            {
                if ((Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 8 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 10) || (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 17 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 19))
                {
                    budget = (TransportLineData.WeekDayRush[FindLineID(ref __instance)] + 1) * 0.25f;
                }
                else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 10 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 17)
                {
                    budget = (TransportLineData.WeekDayLow[FindLineID(ref __instance)] + 1) * 0.25f;
                }
                else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 19 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 24)
                {
                    budget = (TransportLineData.WeekDayLow[FindLineID(ref __instance)] + 1) * 0.25f;
                }
                else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 0 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 4)
                {
                    budget = (TransportLineData.WeekDayNight[FindLineID(ref __instance)] + 1) * 0.25f;
                }
                else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 4 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 8)
                {
                    budget = (TransportLineData.WeekDayLow[FindLineID(ref __instance)] + 1) * 0.25f;
                }
            }
            else
            {
                if ((Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 8 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 10) || (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 17 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 19))
                {
                    budget = (TransportLineData.WeekEndRush[FindLineID(ref __instance)] + 1) * 0.25f;
                }
                else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 10 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 17)
                {
                    budget = (TransportLineData.WeekEndLow[FindLineID(ref __instance)] + 1) * 0.25f;
                }
                else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 19 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 24)
                {
                    budget = (TransportLineData.WeekEndLow[FindLineID(ref __instance)] + 1) * 0.25f;
                }
                else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 0 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 4)
                {
                    budget = (TransportLineData.WeekEndNight[FindLineID(ref __instance)] + 1) * 0.25f;
                }
                else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 4 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 8)
                {
                    budget = (TransportLineData.WeekEndLow[FindLineID(ref __instance)] + 1) * 0.25f;
                }
            }

            __result = (int)(__result * budget);
            if (__result <= 0)
                __result = 1;
        }

        public static bool IsWeekend(DateTime dateTime)
        {
            if (dateTime.DayOfWeek != DayOfWeek.Saturday)
            {
                return dateTime.DayOfWeek == DayOfWeek.Sunday;
            }
            return true;
        }

        public static ushort FindLineID(ref TransportLine transportLine)
        {
            for (int i = 0; i < 256; i++)
            {
                if (Singleton<TransportManager>.instance.m_lines.m_buffer[i].m_flags.IsFlagSet(TransportLine.Flags.Created))
                {
                    if (transportLine.m_lineNumber != 0)
                    {
                        if (transportLine.Info.m_transportType == Singleton<TransportManager>.instance.m_lines.m_buffer[i].Info.m_transportType)
                        {
                            if (transportLine.m_lineNumber == Singleton<TransportManager>.instance.m_lines.m_buffer[i].m_lineNumber)
                            {
                                return (ushort)i;
                            }
                        }
                    }
                }
            }
            return 0;
        }
    }
}
