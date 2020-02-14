using Harmony;

namespace RealCity.Util
{
    public static class HarmonyDetours
    {
        public static HarmonyInstance harmony = null;
        public static void Apply()
        {
            harmony = HarmonyInstance.Create("RealCity");
            harmony.PatchAll();
            Loader.HarmonyDetourFailed = false;
            DebugLog.LogToFileOnly("Harmony patches applied");
        }

        public static void DeApply()
        {
            harmony.UnpatchAll("RealCity");
            DebugLog.LogToFileOnly("Harmony patches DeApplied");
        }
    }
}
