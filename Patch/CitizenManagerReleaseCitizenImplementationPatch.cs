using Harmony;
using RealCity.Util;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class CitizenManagerReleaseCitizenImplementationPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(CitizenManager).GetMethod("ReleaseCitizenImplementation", BindingFlags.NonPublic | BindingFlags.Instance);
        }
        public static void Postfix(uint citizen)
        {
            MainDataStore.citizenMoney[citizen] = 0;
            MainDataStore.isCitizenFirstMovingIn[citizen] = false;
        }
    }
}
