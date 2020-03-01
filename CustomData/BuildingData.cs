
using RealCity.Util;

namespace RealCity.CustomData
{
    public class BuildingData
    {
        public static float[] buildingMoney = new float[49152];
        public static float[] buildingMoneyThreat = new float[49152];
        public static int[] buildingWorkCount = new int[49152];
        public static bool[] isBuildingWorkerUpdated = new bool[49152];
        public static ushort lastBuildingID = 0;
        public static ushort[] commBuildingID = new ushort[49152];
        public static ushort commBuildingNum = 0;
        public static ushort commBuildingNumFinal = 0;


        public static void DataInit()
        {
            for (int i = 0; i < buildingWorkCount.Length; i++)
            {
                buildingMoney[i] = 0;
                buildingWorkCount[i] = 0;
                isBuildingWorkerUpdated[i] = false;
                commBuildingID[i] = 0;
            }
        }

        public static void Save(ref byte[] saveData)
        {
            //442368
            int i = 0;
            SaveAndRestore.SaveData(ref i, buildingMoney, ref saveData);
            SaveAndRestore.SaveData(ref i, buildingWorkCount, ref saveData);
            SaveAndRestore.SaveData(ref i, isBuildingWorkerUpdated, ref saveData);

            if (i != saveData.Length)
            {
                DebugLog.LogToFileOnly($"BuildingData Save Error: saveData.Length = {saveData.Length} + i = {i}");
            }
        }

        public static void Load(ref byte[] saveData)
        {
            int i = 0;
            SaveAndRestore.LoadData(ref i, saveData, ref buildingMoney);
            SaveAndRestore.LoadData(ref i, saveData, ref buildingWorkCount);
            SaveAndRestore.LoadData(ref i, saveData, ref isBuildingWorkerUpdated);

            if (i != saveData.Length)
            {
                DebugLog.LogToFileOnly($"BuildingData Load Error: saveData.Length = {saveData.Length} + i = {i}");
            }
        }
    }
}
