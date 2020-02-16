using HarmonyLib;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class VehicleManagerReleaseVehicleImplementationPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(VehicleManager).GetMethod("ReleaseVehicleImplementation", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
        }
        public static void Postfix(ushort vehicle, ref Vehicle data)
        {
            MainDataStore.vehicleTransferTime[vehicle] = 0;
            MainDataStore.isVehicleCharged[vehicle] = false;
            MainDataStore.watingPathTime[vehicle] = 0;
            MainDataStore.stuckTime[vehicle] = 0;
        }
    }
}
