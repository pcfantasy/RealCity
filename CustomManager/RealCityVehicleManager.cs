using RealCity.Util;

namespace RealCity.CustomManager
{
    public class RealCityVehicleManager
    {
        public static byte[] watingPathTime = new byte[65536];
        public static ushort[] stuckTime = new ushort[65536];
        public static void VehicleManagerReleaseVehicleImplementationPostFix(ushort vehicle, ref Vehicle data)
        {
            //DebugLog.LogToFileOnly("VehicleManagerReleaseVehicleImplementationPostFix vehicle ai is " + data.Info.m_vehicleAI.ToString());
            MainDataStore.vehicleTransferTime[vehicle] = 0;
            MainDataStore.isVehicleCharged[vehicle] = false;
            watingPathTime[vehicle] = 0;
            stuckTime[vehicle] = 0;
        }
    }
}
