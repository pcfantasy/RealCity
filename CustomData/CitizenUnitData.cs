using RealCity.Util;

namespace RealCity.CustomData
{
    public class CitizenUnitData
    {
        public static float[] familyMoney = new float[524288];

        public static void DataInit()
        {
            for (int i = 0; i < familyMoney.Length; i++)
            {
                familyMoney[i] = 0f;
            }
        }

        public static void Save(ref byte[] saveData)
        {
            //2097152;
            int i = 0;
            SaveAndRestore.SaveData(ref i, familyMoney, ref saveData);

            if (i != saveData.Length)
            {
                DebugLog.LogToFileOnly($"CitizenData Save Error: saveData.Length = {saveData.Length} + i = {i}");
            }
        }

        public static void Load(ref byte[] saveData)
        {
            int i = 0;
            SaveAndRestore.LoadData(ref i, saveData, ref familyMoney);

            if (i != saveData.Length)
            {
                DebugLog.LogToFileOnly($"CitizenData Load Error: saveData.Length = {saveData.Length} + i = {i}");
            }
        }
    }
}
