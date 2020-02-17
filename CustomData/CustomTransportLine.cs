using RealCity.Util;

namespace RealCity.CustomData
{
    public class CustomTransportLine
    {
        public static byte[] WeekDayPlan = new byte[256];
        public static byte[] WeekEndPlan = new byte[256];
        public static byte[] saveData = new byte[512];
        public static ushort lastLineID = 0;

        public static void DataInit()
        {
            for (int i = 0; i < WeekDayPlan.Length; i++)
            {
                WeekDayPlan[i] = 1;
                WeekEndPlan[i] = 2;
            }
        }

        public static void Save()
        {
            int i = 0;
            SaveAndRestore.save_bytes(ref i, WeekDayPlan, ref saveData);
            SaveAndRestore.save_bytes(ref i, WeekEndPlan, ref saveData);
        }

        public static void Load()
        {
            int i = 0;
            WeekDayPlan = SaveAndRestore.load_bytes(ref i, saveData, WeekDayPlan.Length);
            WeekEndPlan = SaveAndRestore.load_bytes(ref i, saveData, WeekEndPlan.Length);
        }
    }
}
