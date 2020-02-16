using HarmonyLib;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class CitizenManagerReleaseCitizenInstanceImplementationPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(CitizenManager).GetMethod("ReleaseCitizenInstanceImplementation", BindingFlags.NonPublic | BindingFlags.Instance);
        }
        public static void Prefix(ushort instance, ref CitizenInstance data)
        {
            HumanAISimulationStepPatch.watingPathTime[instance] = 0;
        }
    }
}
