using HarmonyLib;

namespace RealCity.Util
{
    public class HarmonyDetours
    {
        public const string ID = "pcfantasy.realcity";
        public static void Apply()
        {
            Harmony.DEBUG = true;
            var harmony = new Harmony(ID);
            harmony.PatchAll(typeof(HarmonyDetours).Assembly);
            Loader.HarmonyDetourFailed = false;
            DebugLog.LogToFileOnly("Harmony patches applied");
        }

        public static void DeApply()
        {
            var harmony = new Harmony(ID);
            harmony.UnpatchAll(ID);
            DebugLog.LogToFileOnly("Harmony patches DeApplied");
        }
    }
}
