using HarmonyLib;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class EconomyManagerPeekResourcePatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(EconomyManager).GetMethod("PeekResource", BindingFlags.Public | BindingFlags.Instance);
        }
        public static void Prefix(EconomyManager.Resource resource, ref int amount)
        {
            if (resource == EconomyManager.Resource.Maintenance)
            {
                amount *= 100;
            }
        }
    }
}
