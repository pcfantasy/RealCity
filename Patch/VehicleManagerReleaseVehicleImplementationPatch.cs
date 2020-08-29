using HarmonyLib;
using RealCity.CustomData;
using System;
using System.Reflection;

namespace RealCity.Patch
{
	[HarmonyPatch]
	public class VehicleManagerReleaseVehicleImplementationPatch
	{
		public static MethodBase TargetMethod() {
			return typeof(VehicleManager).GetMethod("ReleaseVehicleImplementation", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Vehicle).MakeByRefType() }, null);
		}
		public static void Postfix(ushort vehicle) {
			VehicleData.vehicleTransferTime[vehicle] = 0;
			VehicleData.isVehicleCharged[vehicle] = false;
		}
	}
}
