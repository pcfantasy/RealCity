using System;
using ICities;
using System.IO;

namespace RealCity
{
    public class saveandrestore : SerializableDataExtensionBase
    {
        //static byte[] save_data;
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

        public static void save_double(ref int idex, double item, ref byte[] container)
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

        public static void save_shorts(ref int idex, short[] item, ref byte[] container)
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
            tmp = BitConverter.ToBoolean(container, idex);
            idex = idex + 1;
            return tmp;
        }

        public static bool[] load_bools(ref int idex, byte[] container, int length)
        {
            bool[] tmp = new bool[length];
            int i;
            for (i = 0; i < length; i++)
            {
                tmp[i] = BitConverter.ToBoolean(container, idex);
                idex = idex + 1;
            }
            return tmp;
        }

        public static int load_int(ref int idex, byte[] container)
        {
            int tmp;
            tmp = BitConverter.ToInt32(container, idex);
            idex = idex + 4;
            return tmp;
        }

        public static int[] load_ints(ref int idex, byte[] container, int length)
        {
            int[] tmp = new int[length];
            int i;
            for (i = 0; i < length; i++)
            {
                tmp[i] = BitConverter.ToInt32(container, idex);
                idex = idex + 4;
            }
            return tmp;
        }

        public static float load_float(ref int idex, byte[] container)
        {
            float tmp;
            tmp = BitConverter.ToSingle(container, idex);
            idex = idex + 4;
            return tmp;
        }

        public static float[] load_floats(ref int idex, byte[] container, int length)
        {
            float[] tmp = new float[length];
            int i;
            for (i = 0; i < length; i++)
            {
                tmp[i] = BitConverter.ToSingle(container, idex);
                idex = idex + 4;
            }
            return tmp;
        }

        public static uint load_uint(ref int idex, byte[] container)
        {
            uint tmp;
            tmp = BitConverter.ToUInt32(container, idex);
            idex = idex + 4;
            return tmp;
        }

        public static uint[] load_uints(ref int idex, byte[] container, int length)
        {
            uint[] tmp = new uint[length];
            int i;
            for (i = 0; i < length; i++)
            {
                tmp[i] = BitConverter.ToUInt32(container, idex);
                idex = idex + 4;
            }
            return tmp;
        }

        public static ushort load_ushort(ref int idex, byte[] container)
        {
            ushort tmp;
            tmp = BitConverter.ToUInt16(container, idex);
            idex = idex + 2;
            return tmp;
        }

        public static ushort[] load_ushorts(ref int idex, byte[] container, int length)
        {
            ushort[] tmp = new ushort[length];
            int i;
            for (i = 0; i < length; i++)
            {
                tmp[i] = BitConverter.ToUInt16(container, idex);
                idex = idex + 2;
            }
            return tmp;
        }

        public static short load_short(ref int idex, byte[] container)
        {
            short tmp;
            tmp = BitConverter.ToInt16(container, idex);
            idex = idex + 2;
            return tmp;
        }

        public static short[] load_shorts(ref int idex, byte[] container, int length)
        {
            short[] tmp = new short[length];
            int i;
            for (i = 0; i < length; i++)
            {
                tmp[i] = BitConverter.ToInt16(container, idex);
                idex = idex + 2;
            }
            return tmp;
        }

        public static long load_long(ref int idex, byte[] container)
        {
            long tmp;
            tmp = BitConverter.ToInt64(container, idex);
            idex = idex + 8;
            return tmp;
        }

        public static double load_double(ref int idex, byte[] container)
        {
            double tmp;
            tmp = BitConverter.ToDouble(container, idex);
            idex = idex + 8;
            return tmp;
        }

        public static long[] load_longs(ref int idex, byte[] container, int length)
        {
            long[] tmp = new long[length];
            int i;
            for (i = 0; i < length; i++)
            {
                tmp[i] = BitConverter.ToInt64(container, idex);
                idex = idex + 8;
            }
            return tmp;
        }

        public static byte load_byte(ref int idex, byte[] container)
        {
            byte tmp;
            tmp = container[idex];
            idex = idex + 1;
            return tmp;
        }

        public static byte[] load_bytes(ref int idex, byte[] container, int length)
        {
            byte[] tmp = new byte[length];
            int i;
            for (i = 0; i < length; i++)
            {
                tmp[i] = container[idex];
                idex = idex + 1;
            }
            return tmp;
        }

        public static void gather_save_data()
        {
            pc_EconomyManager.save();
            comm_data.save();
            pc_ResidentAI.save();
            pc_PrivateBuildingAI.save();
            //pc_VehicleAI.save();
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
            saveandrestore._serializableData = serializableData;
        }

        public override void OnReleased()
        {
        }

        public override void OnSaveData()
        {
            if (Loader.CurrentLoadMode == LoadMode.LoadGame || Loader.CurrentLoadMode == LoadMode.NewGame)
            {
                DebugLog.LogToFileOnly("startsave");
                pc_EconomyManager.save_data = new byte[2768];
                comm_data.save_data1 = new byte[4194304];
                comm_data.save_data2 = new byte[49152];
                pc_PrivateBuildingAI.save_data = new byte[316];
                pc_ResidentAI.save_data = new byte[140];
                comm_data.save_data = new byte[3063935];
                gather_save_data();
                saveandrestore._serializableData.SaveData("real_city pc_EconomyManager", pc_EconomyManager.save_data);
                saveandrestore._serializableData.SaveData("real_city comm_data", comm_data.save_data);
                saveandrestore._serializableData.SaveData("real_city pc_ResidentAI", pc_ResidentAI.save_data);
                saveandrestore._serializableData.SaveData("real_city pc_PrivateBuildingAI", pc_PrivateBuildingAI.save_data);
                saveandrestore._serializableData.SaveData("real_city citizen_money", comm_data.save_data1);
                saveandrestore._serializableData.SaveData("real_city building_flag", comm_data.save_data2);
                //saveandrestore._serializableData.SaveData("real_city pc_VehicleAI", pc_VehicleAI.save_data);
                RealCity.SaveSetting();
            }
        }

        public override void OnLoadData()
        {
            Loader.init_data();
            //DebugLog.LogToFileOnly("OnLoadData");
            if (true)
            {
                DebugLog.LogToFileOnly("startload");
                pc_EconomyManager.save_data = saveandrestore._serializableData.LoadData("real_city pc_EconomyManager");
                if (pc_EconomyManager.save_data == null)
                {
                    DebugLog.LogToFileOnly("no pc_EconomyManager save data, please check");
                }
                else
                {
                    pc_EconomyManager.load();
                }

                comm_data.save_data = saveandrestore._serializableData.LoadData("real_city comm_data");
                if (comm_data.save_data == null)
                {
                    DebugLog.LogToFileOnly("no comm_data save data, please check");
                }
                else
                {
                    comm_data.load();
                }

                pc_ResidentAI.save_data = saveandrestore._serializableData.LoadData("real_city pc_ResidentAI");
                if (pc_ResidentAI.save_data == null)
                {
                    DebugLog.LogToFileOnly("no pc_ResidentAI save data, please check");
                }
                else
                {
                    pc_ResidentAI.load();
                }

                pc_PrivateBuildingAI.save_data = saveandrestore._serializableData.LoadData("real_city pc_PrivateBuildingAI");
                if (pc_PrivateBuildingAI.save_data == null)
                {
                    DebugLog.LogToFileOnly("no pc_PrivateBuildingAI save data, please check");
                }
                else
                {
                    pc_PrivateBuildingAI.load();
                }

                comm_data.save_data1 = saveandrestore._serializableData.LoadData("real_city citizen_money");
                if (comm_data.save_data1 == null)
                {
                    DebugLog.LogToFileOnly("no comm_data save data1, please check");                    
                }
                else
                {
                    comm_data.load1();
                }

                comm_data.save_data2 = saveandrestore._serializableData.LoadData("real_city building_flag");
                if (comm_data.save_data2 == null)
                {
                    DebugLog.LogToFileOnly("no comm_data save data2, please check");
                }
                else
                {
                    comm_data.load2();
                }
            }

            RealCity.LoadSetting();

            /*pc_VehicleAI.load_data = saveandrestore._serializableData.LoadData("real_city pc_VehicleAI");
            if (pc_VehicleAI.load_data == null)
            {
                DebugLog.LogToFileOnly("no pc_VehicleAI save data, please check");
            }
            else
            {
                pc_VehicleAI.load();
            }*/
        }

        /*public void init_data()
        {
            comm_data.data_init();
            pc_EconomyManager.data_init();
            pc_EconomyManager.save_data = new byte[2768];
            comm_data.save_data1 = new byte[4194304];
            pc_PrivateBuildingAI.save_data = new byte[316];
            pc_ResidentAI.save_data = new byte[140];
            comm_data.save_data = new byte[3063935];
        }*/
    }
}
