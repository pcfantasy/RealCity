using ColossalFramework;
using RealCity.Util;
using System;
using UnityEngine;

namespace RealCity.CustomData
{
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

        public static int CalculateTargetVehicleCount(ref TransportLine transportLine)
        {
            TransportInfo info = transportLine.Info;
            float num = transportLine.m_totalLength;
            if (num == 0f && transportLine.m_stops != 0)
            {
                NetManager instance = Singleton<NetManager>.instance;
                ushort stops = transportLine.m_stops;
                ushort num2 = stops;
                int num3 = 0;
                while (num2 != 0)
                {
                    ushort num4 = 0;
                    for (int i = 0; i < 8; i++)
                    {
                        ushort segment = instance.m_nodes.m_buffer[num2].GetSegment(i);
                        if (segment != 0 && instance.m_segments.m_buffer[segment].m_startNode == num2)
                        {
                            num += instance.m_segments.m_buffer[segment].m_averageLength;
                            num4 = instance.m_segments.m_buffer[segment].m_endNode;
                            break;
                        }
                    }
                    num2 = num4;
                    if (num2 == stops)
                    {
                        break;
                    }
                    if (++num3 >= 32768)
                    {
                        CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                        break;
                    }
                }
            }
            int budget = Singleton<EconomyManager>.instance.GetBudget(info.m_class);
            //Non-stock code begin
            //DebugLog.LogToFileOnly("Change budget begin," + Singleton<SimulationManager>.instance.m_currentGameTime.Hour.ToString());
            if (IsWeekend(Singleton<SimulationManager>.instance.m_currentGameTime))
            {
                if (WeekEndPlan[FindLineID(transportLine)] == 1)
                {
                    if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 8 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 10)
                    {
                        budget = (int)((RealCity.morningBudgetWeekDay) * 0.01f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 10 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 17)
                    {
                        budget = (int)((RealCity.otherBudgetWeekDay) * 0.01f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 17 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 19)
                    {
                        budget = (int)((RealCity.eveningBudgetWeekDay) * 0.01f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 19 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 24)
                    {
                        budget = (int)((RealCity.otherBudgetWeekDay) * 0.01f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 0 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 4)
                    {
                        budget = (int)((RealCity.deepNightBudgetWeekDay) * 0.01f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 4 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 8)
                    {
                        budget = (int)((RealCity.otherBudgetWeekDay) * 0.01f * budget);
                    }
                }
                else if (WeekEndPlan[FindLineID(transportLine)] == 2)
                {
                    if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 8 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 10)
                    {
                        budget = (int)((RealCity.morningBudgetWeekEnd) * 0.01f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 10 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 17)
                    {
                        budget = (int)((RealCity.otherBudgetWeekEnd) * 0.01f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 17 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 19)
                    {
                        budget = (int)((RealCity.eveningBudgetWeekEnd) * 0.01f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 19 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 24)
                    {
                        budget = (int)((RealCity.otherBudgetWeekEnd) * 0.01f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 0 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 4)
                    {
                        budget = (int)((RealCity.deepNightBudgetWeekEnd) * 0.01f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 4 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 8)
                    {
                        budget = (int)((RealCity.otherBudgetWeekEnd) * 0.01f * budget);
                    }
                }
            }
            else
            {
                //PlanA
                if (WeekDayPlan[FindLineID(transportLine)] == 1)
                {
                    if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 8 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 10)
                    {
                        budget = (int)((RealCity.morningBudgetWeekDay) * 0.01f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 10 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 17)
                    {
                        budget = (int)((RealCity.otherBudgetWeekDay) * 0.01f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 17 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 19)
                    {
                        budget = (int)((RealCity.eveningBudgetWeekDay) * 0.01f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 19 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 24)
                    {
                        budget = (int)((RealCity.otherBudgetWeekDay) * 0.01f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 0 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 4)
                    {
                        budget = (int)((RealCity.deepNightBudgetWeekDay) * 0.01f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 4 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 8)
                    {
                        budget = (int)((RealCity.otherBudgetWeekDay) * 0.01f * budget);
                    }
                }
                else if (WeekDayPlan[FindLineID(transportLine)] == 2)
                {
                    if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 8 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 10)
                    {
                        budget = (int)((RealCity.morningBudgetWeekEnd) * 0.01f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 10 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 17)
                    {
                        budget = (int)((RealCity.otherBudgetWeekEnd) * 0.01f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 17 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 19)
                    {
                        budget = (int)((RealCity.eveningBudgetWeekEnd) * 0.01f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 19 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 24)
                    {
                        budget = (int)((RealCity.otherBudgetWeekEnd) * 0.01f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 0 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 4)
                    {
                        budget = (int)((RealCity.deepNightBudgetWeekEnd) * 0.01f * budget);
                    }
                    else if (Singleton<SimulationManager>.instance.m_currentGameTime.Hour >= 4 && Singleton<SimulationManager>.instance.m_currentGameTime.Hour < 8)
                    {
                        budget = (int)((RealCity.otherBudgetWeekEnd) * 0.01f * budget);
                    }
                }
            }
            //Non-stock code end
            budget = (budget * transportLine.m_budget + 50) / 100;
            return Mathf.CeilToInt(budget * num / (info.m_defaultVehicleDistance * 100f));
        }

        public static bool IsWeekend(this DateTime dateTime)
        {
            if (dateTime.DayOfWeek != DayOfWeek.Saturday)
            {
                return dateTime.DayOfWeek == DayOfWeek.Sunday;
            }
            return true;
        }

        public static ushort FindLineID(TransportLine transportLine)
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
