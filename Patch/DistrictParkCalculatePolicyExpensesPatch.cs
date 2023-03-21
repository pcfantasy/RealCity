using Harmony;
using RealCity.Util;
using System.Reflection;

namespace RealCity.Patch
{
	[HarmonyPatch]
	public class DistrictParkCalculatePolicyExpensesPatch
	{
		public static MethodBase TargetMethod()
		{
			return typeof(DistrictPark).GetMethod("CalculatePolicyExpenses", BindingFlags.Public | BindingFlags.Instance);
		}
		public static void Postfix(ref uint __result)
		{
			__result /= MainDataStore.gameExpenseDivide;
		}

		public static void CalculateVarsityExpenses(ref DistrictPark district, out ulong upkeep, out int coaching, out int cheerleading, out int policies, out ulong total)
		{
			upkeep = district.FetchArenasUpkeep();
			coaching = district.CalculateCoachingStaffCost() / 100;
			int activeArenasCount = district.GetActiveArenasCount();
			cheerleading = district.m_cheerleadingBudget * activeArenasCount;
			policies = 0;
			DistrictPolicies.Park parkPolicies = district.m_parkPolicies;
			if ((parkPolicies & DistrictPolicies.Park.FreeFanMerchandise) != 0)
			{
				policies += 180 * activeArenasCount;
			}
			if ((parkPolicies & DistrictPolicies.Park.VarsitySportsAds) != 0)
			{
				policies += 200 * activeArenasCount;
			}
			upkeep /= MainDataStore.gameExpenseDivide;
			coaching /= MainDataStore.gameExpenseDivide;
			cheerleading /= MainDataStore.gameExpenseDivide;
			policies /= MainDataStore.gameExpenseDivide;
			total = (ulong)((long)upkeep + coaching + cheerleading + policies);
		}
	}
}
