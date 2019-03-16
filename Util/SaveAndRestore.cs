using System;
using ICities;
using System.IO;
using RealCity.CustomAI;
using RealCity.CustomManager;

namespace RealCity.Util
{
    public class SaveAndRestore : SerializableDataExtensionBase
    {
        //static byte[] saveData;
        //static byte[] load_data;
        private static ISerializableData _serializableData;

        public static void save_long(ref int idex, long item, ref byte[] container)
        {
            int i;
            byte[] temp_data;
            temp_data = BitConverter.GetBytes(item);
            for (i = 0; i < temp_data.Length; i++)
            {
                container[idex + i] = temp_data[i];
            }
            idex = idex + temp_data.Length;
        }

        /*public static void save_double(ref int idex, double item, ref byte[] container)
        {
            int i;
            byte[] temp_data;
            temp_data = BitConverter.GetBytes(item);
            for (i = 0; i < temp_data.Length; i++)
            {
                container[idex + i] = temp_data[i];
            }
            idex = idex + temp_data.Length;
        }*/

        public static void save_float(ref int idex, float item, ref byte[] container)
        {
            int i;
            byte[] temp_data;
            temp_data = BitConverter.GetBytes(item);
            for(i = 0; i < temp_data.Length; i++)
            {
                container[idex + i] = temp_data[i];
            }
            idex = idex + temp_data.Length;
        }

        public static void save_floats(ref int idex, float[] item, ref byte[] container)
        {
            int i; int j;
            byte[] temp_data;
            for (j = 0; j < item.Length; j++)
            {
                temp_data = BitConverter.GetBytes(item[j]);
                for (i = 0; i < temp_data.Length; i++)
                {
                    container[idex + i] = temp_data[i];
                }
                idex = idex + temp_data.Length;
            }
        }

        public static void save_uint(ref int idex, uint item, ref byte[] container)
        {
            int i;
            byte[] temp_data;
            temp_data = BitConverter.GetBytes(item);
            for (i = 0; i < temp_data.Length; i++)
            {
                container[idex + i] = temp_data[i];
            }
            idex = idex + temp_data.Length;
        }

        public static void save_int(ref int idex, int item, ref byte[] container)
        {
            int i;
            byte[] temp_data;
            temp_data = BitConverter.GetBytes(item);
            for (i = 0; i < temp_data.Length; i++)
            {
                container[idex + i] = temp_data[i];
            }
            idex = idex + temp_data.Length;
        }

        public static void save_ints(ref int idex, int[] item, ref byte[] container)
        {
            int i; int j;
            byte[] temp_data;
            for (j = 0; j < item.Length; j++)
            {
                temp_data = BitConverter.GetBytes(item[j]);
                for (i = 0; i < temp_data.Length; i++)
                {
                    container[idex + i] = temp_data[i];
                }
                idex = idex + temp_data.Length;
            }
        }

        public static void save_byte(ref int idex, byte item, ref byte[] container)
        {
            container[idex] = item;
            idex = idex + 1;
        }


        public static void save_bytes(ref int idex, byte[] item, ref byte[] container)
        {
            int j;
            for (j = 0; j < item.Length; j++)
            {
                container[idex + j] = item[j];
            }
            idex = idex + item.Length;
        }

        /*public static void save_shorts(ref int idex, short[] item, ref byte[] container)
        {
            int i; int j;
            byte[] temp_data;
            for (j = 0; j < item.Length; j++)
            {
                temp_data = BitConverter.GetBytes(item[j]);
                for (i = 0; i < temp_data.Length; i++)
                {
                    container[idex + i] = temp_data[i];
                }
                idex = idex + temp_data.Length;
            }
        }*/

        public static void save_short(ref int idex, short item, ref byte[] container)
        {
            int i;
            byte[] temp_data;
            temp_data = BitConverter.GetBytes(item);
            for (i = 0; i < temp_data.Length; i++)
            {
                container[idex + i] = temp_data[i];
            }
            idex = idex + temp_data.Length;
        }

        public static void save_ushort(ref int idex, ushort item, ref byte[] container)
        {
            int i;
            byte[] temp_data;
            temp_data = BitConverter.GetBytes(item);
            for (i = 0; i < temp_data.Length; i++)
            {
                container[idex + i] = temp_data[i];
            }
            idex = idex + temp_data.Length;
        }

        public static void save_ushorts(ref int idex, ushort[] item, ref byte[] container)
        {
            int i; int j;
            byte[] temp_data;
            for (j = 0; j < item.Length; j++)
            {
                temp_data = BitConverter.GetBytes(item[j]);
                for (i = 0; i < temp_data.Length; i++)
                {
                    container[idex + i] = temp_data[i];
                }
                idex = idex + temp_data.Length;
            }
        }

        public static void save_bool(ref int idex, bool item, ref byte[] container)
        {
            int i;
            byte[] temp_data;
            temp_data = BitConverter.GetBytes(item);
            for (i = 0; i < temp_data.Length; i++)
            {
                container[idex + i] = temp_data[i];
            }
            idex = idex + temp_data.Length;
        }

        public static void save_bools(ref int idex, bool[] item, ref byte[] container)
        {
            int i; int j;
            byte[] temp_data;
            for (j = 0; j < item.Length; j++)
            {
                temp_data = BitConverter.GetBytes(item[j]);
                for (i = 0; i < temp_data.Length; i++)
                {
                    container[idex + i] = temp_data[i];
                }
                idex = idex + temp_data.Length;
            }
        }


        public static bool load_bool(ref int idex, byte[] container)
        {
            bool tmp;
            if (idex < container.Length)
            {
                tmp = BitConverter.ToBoolean(container, idex);
            }
            else
            {
                DebugLog.LogToFileOnly("load data is too short, please check" + container.Length.ToString());
                tmp = false;
            }
            idex = idex + 1;
            return tmp;
        }

        public static bool[] load_bools(ref int idex, byte[] container, int length)
        {
            bool[] tmp = new bool[length];
            if (idex < container.Length)
            {
                int i;
                for (i = 0; i < length; i++)
                {
                    tmp[i] = BitConverter.ToBoolean(container, idex);
                    idex = idex + 1;
                }
            }
            else
            {
                int i;
                for (i = 0; i < length; i++)
                {
                    idex = idex + 1;
                }
                DebugLog.LogToFileOnly("load data is too short, please check" + container.Length.ToString());
            }
            return tmp;
        }

        public static int load_int(ref int idex, byte[] container)
        {
            int tmp = 0;
            if (idex < container.Length)
            {
                tmp = BitConverter.ToInt32(container, idex);
            }
            else
            {
                DebugLog.LogToFileOnly("load data is too short, please check" + container.Length.ToString());
            }
            idex = idex + 4;
            return tmp;
        }

        public static int[] load_ints(ref int idex, byte[] container, int length)
        {
            int[] tmp = new int[length];
            if (idex < container.Length)
            {
                int i;
                for (i = 0; i < length; i++)
                {
                    tmp[i] = BitConverter.ToInt32(container, idex);
                    idex = idex + 4;
                }
            }
            else
            {
                int i;
                for (i = 0; i < length; i++)
                {
                    idex = idex + 4;
                }
                DebugLog.LogToFileOnly("load data is too short, please check" + container.Length.ToString());
            }
            return tmp;
        }

        public static float load_float(ref int idex, byte[] container)
        {
            float tmp = 0;
            if (idex < container.Length)
            {
                tmp = BitConverter.ToSingle(container, idex);
            }
            else
            {
                DebugLog.LogToFileOnly("load data is too short, please check" + container.Length.ToString());
            }
            idex = idex + 4;
            return tmp;
        }

        public static float[] load_floats(ref int idex, byte[] container, int length)
        {
            float[] tmp = new float[length];
            int i;
            if (idex < container.Length)
            {
                for (i = 0; i < length; i++)
                {
                    tmp[i] = BitConverter.ToSingle(container, idex);
                    idex = idex + 4;
                }
            }
            else
            {
                for (i = 0; i < length; i++)
                {
                    idex = idex + 4;
                }
                DebugLog.LogToFileOnly("load data is too short, please check" + container.Length.ToString());
            }
            return tmp;
        }

        public static uint load_uint(ref int idex, byte[] container)
        {
            uint tmp = 0;
            if (idex < container.Length)
            {
                tmp = BitConverter.ToUInt32(container, idex);
            }
            else
            {
                DebugLog.LogToFileOnly("load data is too short, please check" + container.Length.ToString());
            }
            idex = idex + 4;
            return tmp;
        }

        public static uint[] load_uints(ref int idex, byte[] container, int length)
        {
            uint[] tmp = new uint[length];
            int i;
            if (idex < container.Length)
            {
                for (i = 0; i < length; i++)
                {
                    tmp[i] = BitConverter.ToUInt32(container, idex);
                    idex = idex + 4;
                }
            }
            else
            {
                for (i = 0; i < length; i++)
                {
                    idex = idex + 4;
                }
                DebugLog.LogToFileOnly("load data is too short, please check" + container.Length.ToString());
            }
            return tmp;
        }

        public static ushort load_ushort(ref int idex, byte[] container)
        {
            ushort tmp = 0;
            if (idex < container.Length)
            {
                tmp = BitConverter.ToUInt16(container, idex);
            }
            else
            {
                DebugLog.LogToFileOnly("load data is too short, please check" + container.Length.ToString());
            }
            idex = idex + 2;
            return tmp;
        }

        public static ushort[] load_ushorts(ref int idex, byte[] container, int length)
        {
            ushort[] tmp = new ushort[length];
            int i;
            if (idex < container.Length)
            {
                for (i = 0; i < length; i++)
                {
                    tmp[i] = BitConverter.ToUInt16(container, idex);
                    idex = idex + 2;
                }
            }
            else
            {
                DebugLog.LogToFileOnly("load data is too short, please check" + container.Length.ToString());
                for (i = 0; i < length; i++)
                {
                    idex = idex + 2;
                }
            }
            return tmp;
        }

        public static short load_short(ref int idex, byte[] container)
        {
            short tmp = 0;
            if (idex < container.Length)
            {
                tmp = BitConverter.ToInt16(container, idex);
            }
            else
            {
                DebugLog.LogToFileOnly("load data is too short, please check" + container.Length.ToString());
            }
            idex = idex + 2;
            return tmp;
        }

        /*public static short[] load_shorts(ref int idex, byte[] container, int length)
        {
            short[] tmp = new short[length];
            int i;
            for (i = 0; i < length; i++)
            {
                tmp[i] = BitConverter.ToInt16(container, idex);
                idex = idex + 2;
            }
            return tmp;
        }*/

        public static long load_long(ref int idex, byte[] container)
        {
            long tmp = 0;
            if (idex < container.Length)
            {
                tmp = BitConverter.ToInt64(container, idex);
            }
            else
            {
                DebugLog.LogToFileOnly("load data is too short, please check" + container.Length.ToString());
            }
            idex = idex + 8;
            return tmp;
        }

        /*public static double load_double(ref int idex, byte[] container)
        {
            double tmp;
            tmp = BitConverter.ToDouble(container, idex);
            idex = idex + 8;
            return tmp;
        }*/

        /*public static long[] load_longs(ref int idex, byte[] container, int length)
        {
            long[] tmp = new long[length];
            int i;
            for (i = 0; i < length; i++)
            {
                tmp[i] = BitConverter.ToInt64(container, idex);
                idex = idex + 8;
            }
            return tmp;
        }*/

        public static byte load_byte(ref int idex, byte[] container)
        {
            byte tmp = 0;
            if (idex < container.Length)
            {
                tmp = container[idex];
            }
            else
            {
                DebugLog.LogToFileOnly("load data is too short, please check" + container.Length.ToString());
            }
            idex = idex + 1;
            return tmp;
        }

        public static byte[] load_bytes(ref int idex, byte[] container, int length)
        {
            byte[] tmp = new byte[length];
            int i;
            if (idex < container.Length)
            {
                for (i = 0; i < length; i++)
                {
                    tmp[i] = container[idex];
                    idex = idex + 1;
                }
            }
            else
            {
                for (i = 0; i < length; i++)
                {
                    idex = idex + 1;
                }
                DebugLog.LogToFileOnly("load data is too short, please check" + container.Length.ToString());
            }
            return tmp;
        }

        public static void gather_saveData()
        {
            RealCityEconomyManager.Save();
            MainDataStore.save();
            RealCityResidentAI.Save();
            RealCityPrivateBuildingAI.save();
            Politics.Save();
        }

        //public static void get_load_data()
        //{
        //    pc_EconomyManager.load();
        //    comm_data.load();
        //    pc_ResidentAI.load();
        //    pc_PrivateBuildingAI.load();
        //}

        public override void OnCreated(ISerializableData serializableData)
        {
            SaveAndRestore._serializableData = serializableData;
        }

        public override void OnReleased()
        {
        }

        public override void OnSaveData()
        {
            if (Loader.CurrentLoadMode == LoadMode.LoadGame || Loader.CurrentLoadMode == LoadMode.NewGame)
            {
                DebugLog.LogToFileOnly("startsave");
                RealCityEconomyManager.saveData = new byte[2844];
                MainDataStore.saveData1 = new byte[4194304];
                MainDataStore.saveData2 = new byte[1048576];
                RealCityPrivateBuildingAI.saveData = new byte[44];
                RealCityResidentAI.saveData = new byte[144];
                MainDataStore.saveData = new byte[3932402];
                Politics.saveData = new byte[103];
                gather_saveData();
                SaveAndRestore._serializableData.SaveData("real_city pc_EconomyManager", RealCityEconomyManager.saveData);
                SaveAndRestore._serializableData.SaveData("real_city comm_data", MainDataStore.saveData);
                SaveAndRestore._serializableData.SaveData("real_city pc_ResidentAI", RealCityResidentAI.saveData);
                SaveAndRestore._serializableData.SaveData("real_city pc_PrivateBuildingAI", RealCityPrivateBuildingAI.saveData);
                SaveAndRestore._serializableData.SaveData("real_city citizenMoney", MainDataStore.saveData1);
                SaveAndRestore._serializableData.SaveData("real_city citizenFlag", MainDataStore.saveData2);
                SaveAndRestore._serializableData.SaveData("real_city politics", Politics.saveData);
            }
        }

        public override void OnLoadData()
        {
            Loader.InitData();
            DebugLog.LogToFileOnly("OnLoadData");
            //DebugLog.LogToFileOnly("comm_data save data length pre is " + comm_data.saveData.Length);
            if (true)
            {
                DebugLog.LogToFileOnly("startload");
                RealCityEconomyManager.saveData = SaveAndRestore._serializableData.LoadData("real_city pc_EconomyManager");
                if (RealCityEconomyManager.saveData == null)
                {
                    DebugLog.LogToFileOnly("no pc_EconomyManager save data, please check");
                }
                else
                {
                    RealCityEconomyManager.Load();
                }

                MainDataStore.saveData = SaveAndRestore._serializableData.LoadData("real_city comm_data");
                if (MainDataStore.saveData == null)
                {
                    DebugLog.LogToFileOnly("no comm_data save data, please check");
                }
                else
                {
                    //DebugLog.LogToFileOnly("comm_data save data length is " + comm_data.saveData.Length);
                    MainDataStore.load();
                }

                RealCityResidentAI.saveData = SaveAndRestore._serializableData.LoadData("real_city pc_ResidentAI");
                if (RealCityResidentAI.saveData == null)
                {
                    DebugLog.LogToFileOnly("no pc_ResidentAI save data, please check");
                }
                else
                {
                    RealCityResidentAI.Load();
                }

                RealCityPrivateBuildingAI.saveData = SaveAndRestore._serializableData.LoadData("real_city pc_PrivateBuildingAI");
                if (RealCityPrivateBuildingAI.saveData == null)
                {
                    DebugLog.LogToFileOnly("no pc_PrivateBuildingAI save data, please check");
                }
                else
                {
                    RealCityPrivateBuildingAI.Load();
                }
                MainDataStore.saveData1 = SaveAndRestore._serializableData.LoadData("real_city citizenMoney");
                if (MainDataStore.saveData1 == null)
                {
                    DebugLog.LogToFileOnly("no comm_data save data1, please check");
                }
                else
                {
                    MainDataStore.load1();
                }
                MainDataStore.saveData2 = SaveAndRestore._serializableData.LoadData("real_city citizenFlag");
                if (MainDataStore.saveData2 == null)
                {
                    DebugLog.LogToFileOnly("no comm_data save data2, please check");
                }
                else
                {
                    MainDataStore.load2();
                }
                Politics.saveData = SaveAndRestore._serializableData.LoadData("real_city politics");
                if (Politics.saveData == null)
                {
                    DebugLog.LogToFileOnly("no Politics.saveData, please check");
                }
                else
                {
                    Politics.Load();
                }
            }

        }
    }
}
