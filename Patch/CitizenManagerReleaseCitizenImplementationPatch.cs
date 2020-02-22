using Harmony;
using RealCity.CustomData;
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
            CitizenData.citizenMoney[citizen] = 0;
        }
    }
}
