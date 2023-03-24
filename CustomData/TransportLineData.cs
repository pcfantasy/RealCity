using RealCity.Util;

namespace RealCity.CustomData
{
    public class TransportLineData
    {
        public static byte[] WeekDayRush = new byte[256];
        public static byte[] WeekDayLow = new byte[256];
        public static byte[] WeekDayNight = new byte[256];
        public static byte[] WeekEndRush = new byte[256];
        public static byte[] WeekEndLow = new byte[256];
        public static byte[] WeekEndNight = new byte[256];
        public static ushort lastLineID = 0;

        public static void DataInit()
        {
            for (int i = 0; i < WeekDayRush.Length; i++)
            {
                WeekDayRush[i] = 3;
                WeekDayLow[i] = 3;
                WeekDayNight[i] = 3;
                WeekEndRush[i] = 3;
                WeekEndLow[i] = 3;
                WeekEndNight[i] = 3;
            }
        }

        public static void Save(ref byte[] saveData)
        {
            int i = 0;
            SaveAndRestore.SaveData(ref i, WeekDayRush, ref saveData);
            SaveAndRestore.SaveData(ref i, WeekDayLow, ref saveData);
            SaveAndRestore.SaveData(ref i, WeekDayNight, ref saveData);
            SaveAndRestore.SaveData(ref i, WeekEndRush, ref saveData);
            SaveAndRestore.SaveData(ref i, WeekEndLow, ref saveData);
            SaveAndRestore.SaveData(ref i, WeekEndNight, ref saveData);

            if (i != saveData.Length)
            {
                DebugLog.LogToFileOnly($"TransportLineData Save Error: saveData.Length = {saveData.Length} actually = {i}");
            }
        }

        public static void Load(ref byte[] saveData)
        {
            int i = 0;
            SaveAndRestore.LoadData(ref i, saveData, ref WeekDayRush);
            SaveAndRestore.LoadData(ref i, saveData, ref WeekDayLow);
            SaveAndRestore.LoadData(ref i, saveData, ref WeekDayNight);
            SaveAndRestore.LoadData(ref i, saveData, ref WeekEndRush);
            SaveAndRestore.LoadData(ref i, saveData, ref WeekEndLow);
            SaveAndRestore.LoadData(ref i, saveData, ref WeekEndNight);

            if (i != saveData.Length)
            {
                DebugLog.LogToFileOnly($"TransportLineData Load Error: saveData.Length = {saveData.Length} actually = {i}");
            }
        }
    }
}
