using RealCity.Util;

namespace RealCity.CustomData
{
    public class TransportLineData
    {
        public static byte[] WeekDayPlan = new byte[256];
        public static byte[] WeekEndPlan = new byte[256];
        public static ushort lastLineID = 0;

        public static void DataInit()
        {
            for (int i = 0; i < WeekDayPlan.Length; i++)
            {
                WeekDayPlan[i] = 1;
                WeekEndPlan[i] = 2;
            }
        }

        public static void Save(ref byte[] saveData)
        {
            int i = 0;
            SaveAndRestore.SaveData(ref i, WeekDayPlan, ref saveData);
            SaveAndRestore.SaveData(ref i, WeekEndPlan, ref saveData);

            if (i != saveData.Length)
            {
                DebugLog.LogToFileOnly($"TransportLineData Save Error: saveData.Length = {saveData.Length} actually = {i}");
            }
        }

        public static void Load(ref byte[] saveData)
        {
            int i = 0;
            SaveAndRestore.LoadData(ref i, saveData, ref WeekDayPlan);
            SaveAndRestore.LoadData(ref i, saveData, ref WeekEndPlan);

            if (i != saveData.Length)
            {
                DebugLog.LogToFileOnly($"TransportLineData Load Error: saveData.Length = {saveData.Length} actually = {i}");
            }
        }
    }
}
