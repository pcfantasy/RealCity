using RealCity.Util;

namespace RealCity.CustomData
{
    public static class CustomDistrictPark
	{
		public static uint CalculatePolicyExpenses(ref DistrictPark district)
		{
			uint num = 0u;
			DistrictPolicies.Park parkPolicies = district.m_parkPolicies;
			if ((parkPolicies & DistrictPolicies.Park.StudentHealthcare) != 0)
			{
				num += district.m_studentCount * 5;
			}
			if ((parkPolicies & DistrictPolicies.Park.FreeLunch) != 0)
			{
				num += district.m_studentCount;
			}
			return num / MainDataStore.gameExpenseDivide;
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
