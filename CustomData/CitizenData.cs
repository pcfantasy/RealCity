using RealCity.Util;

namespace RealCity.CustomData
{
    public class CitizenData
    {
        public static float[] citizenMoney = new float[1048576];
        public static bool[] isCitizenFirstMovingIn = new bool[1048576];
        public static uint lastCitizenID = 0;

        public static void DataInit()
        {
            for (int i = 0; i < citizenMoney.Length; i++)
            {
                citizenMoney[i] = 0f;
                isCitizenFirstMovingIn[i] = false;
            }
        }

        public static void Save(ref byte[] saveData)
        {
            //5242880
            int i = 0;
            SaveAndRestore.SaveData(ref i, citizenMoney, ref saveData);
            SaveAndRestore.SaveData(ref i, isCitizenFirstMovingIn, ref saveData);

            if (i != saveData.Length)
            {
                DebugLog.LogToFileOnly($"CitizenData Save Error: saveData.Length = {saveData.Length} + i = {i}");
            }
        }

        public static void Load(ref byte[] saveData)
        {
            int i = 0;
            SaveAndRestore.LoadData(ref i, saveData, ref citizenMoney);
            SaveAndRestore.LoadData(ref i, saveData, ref isCitizenFirstMovingIn);

            if (i != saveData.Length)
            {
                DebugLog.LogToFileOnly($"CitizenData Load Error: saveData.Length = {saveData.Length} + i = {i}");
            }
        }
    }
}
