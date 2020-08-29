using ColossalFramework;
using HarmonyLib;
using RealCity.Util;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace RealCity.Patch
{
	[HarmonyPatch]
	public class EconomyPanelIncomeExpensesPollPatch
	{
		public static MethodBase TargetMethod() {
			return typeof(EconomyPanel).GetNestedType("IncomeExpensesPoll", BindingFlags.NonPublic).GetMethod("CalculateArenasExpenses", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(EconomyPanel.ArenaIndex), typeof(long).MakeByRefType() }, null);
		}
		public static void Init() {
			DebugLog.LogToFileOnly("Init fake RealCityEconomyPanel");
			try {
				var inst = Singleton<EconomyPanel>.instance;
				var arenas = typeof(EconomyPanel).GetField("m_arenas", BindingFlags.NonPublic | BindingFlags.Static);
				if (inst == null) {
					DebugLog.LogToFileOnly("No instance of EconomyPanel found!");
					return;
				}
				m_arenas = arenas.GetValue(inst) as List<ushort>[];
				if (m_arenas == null) {
					DebugLog.LogToFileOnly("EconomyPanel Arrays are null");
				}
			}
			catch (Exception ex) {
				DebugLog.LogToFileOnly("EconomyPanel Exception: " + ex.Message);
			}
		}

		public static bool Prefix(EconomyPanel.ArenaIndex arenaIndex, ref long expenses) {
			if (!init) {
				Init();
				init = true;
			}

			for (int i = 0; i < m_arenas[(int)arenaIndex].Count; i++) {
				ushort buildingID = m_arenas[(int)arenaIndex][i];
				var Info = Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingID].Info;
				if (Info != null) {
					Singleton<EconomyManager>.instance.GetIncomeAndExpenses(Info.m_class.m_service, Info.m_class.m_subService, Info.m_class.m_level, out long _, out long expense);
					expenses += expense;
					return false;
				}
			}
			return false;
		}

		public static bool init = false;
		private static List<ushort>[] m_arenas;
	}
}
