using ColossalFramework;
using Harmony;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.CustomData
{
    [HarmonyPatch]
    public static class CustomTransportLine
    {
        public static byte[] WeekDayPlan = new byte[256];
        public static byte[] WeekEndPlan = new byte[256];
        public static byte[] saveData = new byte[512];
        public static ushort lastLineID = 0;

        public static void DataInit()
        {
            for (int i = 0; i < WeekDayPlan.Length; i++)
            {
                WeekDayPlan[i] = 1;
                WeekEndPlan[i] = 2;
            }
        }

        public static void save()
        {
            int i = 0;
            SaveAndRestore.save_bytes(ref i, WeekDayPlan, ref saveData);
            SaveAndRestore.save_bytes(ref i, WeekEndPlan, ref saveData);
        }

        public static void load()
        {
            int i = 0;
            WeekDayPlan = SaveAndRestore.load_bytes(ref i, saveData, WeekDayPlan.Length);
            WeekEndPlan = SaveAndRestore.load_bytes(ref i, saveData, WeekEndPlan.Length);
        }

        public static MethodBase TargetMethod()
        {
            return typeof(TransportLine).GetMethod("CalculateTargetVehicleCount", BindingFlags.Public | BindingFlags.Instance);
        }

        public static void Postfix(ref TransportLine __instance, ref int __result)
        {
            if (Loader.isTransportLinesManagerRunning) { return; }
            float budget = 1f;
            if (IsWeekend(Singleton<SimulationManager>.instance.m_currentGameTime))
            {
                if (WeekEndPlan[FindLineID(ref __instance)] == 1)
                {
                    if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 8 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 10)
                    {
                        budget = RealCity.morningBudgetWeekDay * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 10 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 17)
                    {
                        budget = RealCity.otherBudgetWeekDay * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 17 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 19)
                    {
                        budget = RealCity.eveningBudgetWeekDay * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 19 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 24)
                    {
                        budget = RealCity.otherBudgetWeekDay * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 0 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 4)
                    {
                        budget = RealCity.deepNightBudgetWeekDay * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 4 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 8)
                    {
                        budget = RealCity.otherBudgetWeekDay * 0.01f;
                    }
                }
                else if (WeekEndPlan[FindLineID(ref __instance)] == 2)
                {
                    if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 8 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 10)
                    {
                        budget = RealCity.morningBudgetWeekEnd * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 10 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 17)
                    {
                        budget = RealCity.otherBudgetWeekEnd * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 17 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 19)
                    {
                        budget = RealCity.eveningBudgetWeekEnd * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 19 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 24)
                    {
                        budget = RealCity.otherBudgetWeekEnd * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 0 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 4)
                    {
                        budget = RealCity.deepNightBudgetWeekEnd * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 4 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 8)
                    {
                        budget = RealCity.otherBudgetWeekEnd * 0.01f;
                    }
                }
                else if (WeekEndPlan[FindLineID(ref __instance)] == 3)
                {
                    if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 8 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 10)
                    {
                        budget = RealCity.morningBudgetMax * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 10 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 17)
                    {
                        budget = RealCity.otherBudgetMax * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 17 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 19)
                    {
                        budget = RealCity.eveningBudgetMax * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 19 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 24)
                    {
                        budget = RealCity.otherBudgetMax * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 0 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 4)
                    {
                        budget = RealCity.deepNightBudgetMax * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 4 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 8)
                    {
                        budget = RealCity.otherBudgetMax * 0.01f;
                    }
                }
                else if (WeekEndPlan[FindLineID(ref __instance)] == 4)
                {
                    if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 8 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 10)
                    {
                        budget = RealCity.morningBudgetMin * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 10 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 17)
                    {
                        budget = RealCity.otherBudgetMin * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 17 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 19)
                    {
                        budget = RealCity.eveningBudgetMin * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 19 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 24)
                    {
                        budget = RealCity.otherBudgetMin * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 0 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 4)
                    {
                        budget = RealCity.deepNightBudgetMin * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 4 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 8)
                    {
                        budget = RealCity.otherBudgetMin * 0.01f;
                    }
                }
            }
            else
            {
                //PlanA
                if (WeekDayPlan[FindLineID(ref __instance)] == 1)
                {
                    if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 8 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 10)
                    {
                        budget = RealCity.morningBudgetWeekDay * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 10 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 17)
                    {
                        budget = RealCity.otherBudgetWeekDay * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 17 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 19)
                    {
                        budget = RealCity.eveningBudgetWeekDay * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 19 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 24)
                    {
                        budget = RealCity.otherBudgetWeekDay * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 0 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 4)
                    {
                        budget = RealCity.deepNightBudgetWeekDay * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 4 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 8)
                    {
                        budget = RealCity.otherBudgetWeekDay * 0.01f;
                    }
                }
                else if (WeekDayPlan[FindLineID(ref __instance)] == 2)
                {
                    if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 8 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 10)
                    {
                        budget = RealCity.morningBudgetWeekEnd * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 10 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 17)
                    {
                        budget = RealCity.otherBudgetWeekEnd * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 17 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 19)
                    {
                        budget = RealCity.eveningBudgetWeekEnd * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 19 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 24)
                    {
                        budget = RealCity.otherBudgetWeekEnd * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 0 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 4)
                    {
                        budget = (RealCity.deepNightBudgetWeekEnd) * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 4 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 8)
                    {
                        budget = RealCity.otherBudgetWeekEnd * 0.01f;
                    }
                }
                else if (WeekDayPlan[FindLineID(ref __instance)] == 3)
                {
                    if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 8 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 10)
                    {
                        budget = RealCity.morningBudgetMax * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 10 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 17)
                    {
                        budget = RealCity.otherBudgetMax * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 17 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 19)
                    {
                        budget = RealCity.eveningBudgetMax * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 19 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 24)
                    {
                        budget = RealCity.otherBudgetMax * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 0 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 4)
                    {
                        budget = RealCity.deepNightBudgetMax * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 4 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 8)
                    {
                        budget = RealCity.otherBudgetMax * 0.01f;
                    }
                }
                else if (WeekDayPlan[FindLineID(ref __instance)] == 4)
                {
                    if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 8 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 10)
                    {
                        budget = (RealCity.morningBudgetMin) * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 10 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 17)
                    {
                        budget = (RealCity.otherBudgetMin) * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 17 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 19)
                    {
                        budget = (RealCity.eveningBudgetMin) * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 19 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 24)
                    {
                        budget = (RealCity.otherBudgetMin) * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 0 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 4)
                    {
                        budget = (RealCity.deepNightBudgetMin) * 0.01f;
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 4 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 8)
                    {
                        budget = (RealCity.otherBudgetMin) * 0.01f;
                    }
                }
            }

            DebugLog.LogToFileOnly($"__result pre is {__result} for line {FindLineID(ref __instance)}");
            __result = (int)(__result * budget);
            DebugLog.LogToFileOnly($"__result post is {__result} for line {FindLineID(ref __instance)}");
        }

        public static bool IsWeekend(this DateTime dateTime)
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
