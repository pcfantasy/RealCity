using Harmony;
using RealCity.Util;
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
            MainDataStore.familyGoods[unit] = 0;
            MainDataStore.family_money[unit] = 0;
        }
    }
}
