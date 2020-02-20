using Harmony;
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
            CitizenUnitData.familyGoods[unit] = 0;
            CitizenUnitData.familyMoney[unit] = 0;
        }
    }
}
