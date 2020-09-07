using HarmonyLib;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
	[HarmonyPatch]
	public class DistrictParkCalculateVarsityExpensesPatch
	{
		public static MethodBase TargetMethod()
		{
			return typeof(DistrictPark).GetMethod("CalculateVarsityExpenses", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ulong).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(int).MakeByRefType(), typeof(ulong).MakeByRefType() }, null);
		}

		public static void Postfix(ref ulong upkeep, ref int coaching, ref int cheerleading, ref int policies, ref ulong total)
		{
			upkeep /= MainDataStore.gameExpenseDivide;
			coaching /= MainDataStore.gameExpenseDivide;
			cheerleading /= MainDataStore.gameExpenseDivide;
			policies /= MainDataStore.gameExpenseDivide;
			total /= MainDataStore.gameExpenseDivide;
		}
	}
}
