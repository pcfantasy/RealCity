using Harmony;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class VehicleManagerReleaseVehicleImplementationPatch
    {
        public static byte[] watingPathTime = new byte[65536];
        public static ushort[] stuckTime = new ushort[65536];
        public static MethodBase TargetMethod()
        {
            return typeof(VehicleManager).GetMethod("ReleaseVehicleImplementation", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
        }
        public static void Postfix(ushort vehicle, ref Vehicle data)
        {
            MainDataStore.vehicleTransferTime[vehicle] = 0;
            MainDataStore.isVehicleCharged[vehicle] = false;
            watingPathTime[vehicle] = 0;
            stuckTime[vehicle] = 0;
        }
    }
}
