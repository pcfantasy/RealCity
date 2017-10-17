using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColossalFramework;
using ColossalFramework.UI;
using ICities;
using UnityEngine;

namespace RealCity
{
    public class saveandrestore : SerializableDataExtensionBase
    {
        static byte[] save_data = new byte[8];
        static byte[] load_data;
        private static ISerializableData _serializableData;
        public static void gather_save_data()
        {
            //save_data[7] = (byte)((comm_data.last_bank_count >> 24) & 0xFF);
            //save_data[6] = (byte)((comm_data.last_bank_count >> 16) & 0xFF);
            //save_data[5] = (byte)((comm_data.last_bank_count >> 8) & 0xFF);
            //save_data[4] = (byte)(comm_data.last_bank_count & 0xFF);
            //save_data[3] = (byte)((comm_data.last_pop >> 24) & 0xFF);
            //save_data[2] = (byte)((comm_data.last_pop >> 16) & 0xFF);
            //save_data[1] = (byte)((comm_data.last_pop >> 8) & 0xFF);
            //save_data[0] = (byte)(comm_data.last_pop & 0xFF);
        }

        public static void get_load_data()
        {
            //comm_data.last_pop = (int)((load_data[0] & 0xFF)
            // | ((load_data[1] & 0xFF) << 8)
            // | ((load_data[2] & 0xFF) << 16)
            // | ((load_data[3] & 0xFF) << 24));

            //comm_data.last_bank_count = (int)((load_data[4] & 0xFF)
            // | ((load_data[5] & 0xFF) << 8)
            // | ((load_data[6] & 0xFF) << 16)
            // | ((load_data[7] & 0xFF) << 24));
        }

        public override void OnCreated(ISerializableData serializableData)
        {
            saveandrestore._serializableData = serializableData;
        }

        public override void OnReleased()
        {
        }

        public override void OnSaveData()
        {
            gather_save_data();
            saveandrestore._serializableData.SaveData("real_city data", save_data);
        }

        public override void OnLoadData()
        {
            init_data();
            load_data = saveandrestore._serializableData.LoadData("real_city data");
            if (load_data == null)
            {
                DebugLog.LogToFileOnly("no save data, please check");
            }
            else
            {
                get_load_data();
            }
        }

        public void init_data()
        {
            for (int i = 0; i < comm_data.building_money.Length; i++)
            {
                comm_data.building_money[i] = 0;
            }
            for (int i = 0; i < comm_data.vehical_transfer_time.Length; i++)
            {
                comm_data.vehical_transfer_time[i] = 0;
            }
            for (int i = 0; i < comm_data.vehical_last_transfer_flag.Length; i++)
            {
                comm_data.vehical_last_transfer_flag[i] = false;
            }
        }
    }
}
