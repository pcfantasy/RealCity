using ColossalFramework;
using RealCity.Util;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace RealCity.CustomAI
{
    public class RealCityEconomyPanel
    {
        public static void Init()
        {
            DebugLog.LogToFileOnly("Init fake RealCityEconomyPanel");
            try
            {
                var inst = Singleton<EconomyPanel>.instance;
                var arenas = typeof(EconomyPanel).GetField("m_arenas", BindingFlags.NonPublic | BindingFlags.Static);
                if (inst == null)
                {
                    DebugLog.LogToFileOnly("No instance of EconomyPanel found!");
                    return;
                }
                m_arenas = arenas.GetValue(inst) as List<ushort>[];
                if (m_arenas == null)
                {
                    DebugLog.LogToFileOnly("EconomyPanel Arrays are null");
                }
            }
            catch (Exception ex)
            {
                DebugLog.LogToFileOnly("EconomyPanel Exception: " + ex.Message);
            }
        }

        private void CalculateArenasExpenses(EconomyPanel.ArenaIndex arenaIndex, ref long expenses)
        {
            if (!init)
            {
                Init();
                init = true;
            }

            for (int i = 0; i < m_arenas[(int)arenaIndex].Count; i++)
            {
                ushort num = m_arenas[(int)arenaIndex][i];
                var Info = Singleton<BuildingManager>.instance.m_buildings.m_buffer[num].Info;
                if (Info != null)
                {
                    long num3 = 0L;
                    long num4 = 0L;
                    Singleton<EconomyManager>.instance.GetIncomeAndExpenses(Info.m_class.m_service, Info.m_class.m_subService, Info.m_class.m_level, out num3, out num4);
                    expenses += num4;
                    return;
                }
            }
        }

        public static bool init = false;
        private static List<ushort>[] m_arenas;
    }
}
