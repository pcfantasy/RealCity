using RealCity.Util;

namespace RealCity.CustomData
{
	public class VehicleData
	{
		public static ushort[] vehicleTransferTime = new ushort[65536];
		public static bool[] isVehicleCharged = new bool[65536];

		public static void DataInit()
		{
			//for (int i = 0; i < isVehicleCharged.Length; i++) {
			//	vehicleTransferTime[i] = 0;
			//	isVehicleCharged[i] = false;
			//}
			vehicleTransferTime.Initialize();
			isVehicleCharged.Initialize();
		}

		public static void Save(ref byte[] saveData)
		{
			//3 * 65536 = 196608;
			int i = 0;
			SaveAndRestore.SaveData(ref i, vehicleTransferTime, ref saveData);
			SaveAndRestore.SaveData(ref i, isVehicleCharged, ref saveData);

			if (i != saveData.Length)
			{
				DebugLog.LogToFileOnly($"VehicleData Save Error: saveData.Length = {saveData.Length} actually = {i}");
			}
		}

		public static void Load(ref byte[] saveData)
		{
			int i = 0;
			SaveAndRestore.LoadData(ref i, saveData, ref vehicleTransferTime);
			SaveAndRestore.LoadData(ref i, saveData, ref isVehicleCharged);

			if (i != saveData.Length)
			{
				DebugLog.LogToFileOnly($"VehicleData Load Error: saveData.Length = {saveData.Length} actually = {i}");
			}
		}
	}
}
