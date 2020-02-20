namespace RealCity.Util
{
    public class HarmonyDetours
    {
        public const string Id = "pcfantasy.realcity";
        public static void Apply()
        {
            var harmony = new Harmony.Harmony(Id);
            harmony.PatchAll(typeof(HarmonyDetours).Assembly);
            Loader.HarmonyDetourFailed = false;
            DebugLog.LogToFileOnly("Harmony patches applied");
        }

        public static void DeApply()
        {
            var harmony = new Harmony.Harmony(Id);
            harmony.UnpatchAll(Id);
            DebugLog.LogToFileOnly("Harmony patches DeApplied");
        }
    }
}
