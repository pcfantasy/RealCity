using ColossalFramework;
using RealCity.Util;

namespace RealCity.CustomData
{
    public class CitizenUnitData
    {
        public static float[] familyMoney;
        public static ushort[] familyGoods;

        public static void DataInit()
        {
            uint numCitizenUnits = Singleton<CitizenManager>.instance.m_units.m_size;
            familyMoney = new float[numCitizenUnits];
            familyGoods = new ushort[numCitizenUnits];

            for (int i = 0; i < familyMoney.Length; i++)
            {
                familyMoney[i] = 0f;
                //default;
                familyGoods[i] = 65535;
            }
        }

        public static void Save(ref byte[] saveData)
        {
            //3145728;
            int i = 0;
            SaveAndRestore.SaveData(ref i, familyMoney, ref saveData);
            SaveAndRestore.SaveData(ref i, familyGoods, ref saveData);

            if (i != saveData.Length)
            {
                DebugLog.LogToFileOnly($"CitizenData Save Error: saveData.Length = {saveData.Length} actually = {i}");
            }
        }

        public static void Load(ref byte[] saveData)
        {
            int i = 0;
            SaveAndRestore.LoadData(ref i, saveData, ref familyMoney);
            SaveAndRestore.LoadData(ref i, saveData, ref familyGoods);

            if (i != saveData.Length)
            {
                DebugLog.LogToFileOnly($"CitizenData Load Error: saveData.Length = {saveData.Length} actually = {i}");
            }
        }
    }
}
