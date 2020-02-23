﻿using RealCity.Util;

namespace RealCity.CustomData
{
    public class CitizenData
    {
        public static float[] citizenMoney = new float[1048576];
        public static uint lastCitizenID = 0;

        public static void DataInit()
        {
            for (int i = 0; i < citizenMoney.Length; i++)
            {
                citizenMoney[i] = 0f;
            }
        }

        public static void Save(ref byte[] saveData)
        {
            //4194304
            int i = 0;
            SaveAndRestore.SaveData(ref i, citizenMoney, ref saveData);

            if (i != saveData.Length)
            {
                DebugLog.LogToFileOnly($"CitizenData Save Error: saveData.Length = {saveData.Length} + i = {i}");
            }
        }

        public static void Load(ref byte[] saveData)
        {
            int i = 0;
            SaveAndRestore.LoadData(ref i, saveData, ref citizenMoney);

            if (i != saveData.Length)
            {
                DebugLog.LogToFileOnly($"CitizenData Load Error: saveData.Length = {saveData.Length} + i = {i}");
            }
        }
    }
}
