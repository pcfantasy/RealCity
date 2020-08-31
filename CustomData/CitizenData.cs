using ColossalFramework;
using RealCity.Util;

namespace RealCity.CustomData
{
	public class CitizenData : ICustomGameData
	{
		public float[] citizenMoney = new float[1048576];
		public uint lastCitizenID = 0;

		private static CitizenData _inst = null;
		public static CitizenData Instance {
			get {
				if (_inst == null) {
					_inst = new CitizenData();
				}
				return _inst;
			}
		}
		private CitizenData() { }

		public static uint GetCitizenUnit(ushort buildingId) {
			if (buildingId != 0) {
				return Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingId].m_citizenUnits;
			}
			return 0u;
		}

		public void DataInit() {
			//for (int i = 0; i < citizenMoney.Length; i++) {
			//	citizenMoney[i] = 0f;
			//}
			this.citizenMoney.Initialize();
		}

		public void Save(ref byte[] saveData) {
			//4194304
			int i = 0;
			SaveAndRestore.SaveData(ref i, citizenMoney, ref saveData);

			if (i != saveData.Length) {
				DebugLog.LogToFileOnly($"CitizenData Save Error: saveData.Length = {saveData.Length} actually = {i}");
			}
		}

		public void Load(ref byte[] saveData) {
			int i = 0;
			SaveAndRestore.LoadData(ref i, saveData, ref citizenMoney);

			if (i != saveData.Length) {
				DebugLog.LogToFileOnly($"CitizenData Load Error: saveData.Length = {saveData.Length} actually = {i}");
			}
		}
	}
}