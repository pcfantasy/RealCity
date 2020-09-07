using HarmonyLib;
using RealCity.CustomData;
using System.Reflection;

namespace RealCity.Patch
{
	[HarmonyPatch]
	public class CitizenManagerReleaseUnitCitizenPatch
	{
		public static MethodBase TargetMethod()
		{
			return typeof(CitizenManager).GetMethod("ReleaseUnitCitizen", BindingFlags.NonPublic | BindingFlags.Instance);
		}
		public static void Postfix(uint unit)
		{
			CitizenUnitData.familyMoney[unit] = 0;
			//65535 stand for uninitial money
			CitizenUnitData.familyGoods[unit] = 65535;
		}
	}
}
