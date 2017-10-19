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
        static byte[] save_data;
        static byte[] load_data;
        private static ISerializableData _serializableData;
        //in comm_data
        //public static long citizen_outcome_per_family = 0;                        64   -------- 1
        //public static long citizen_outcome = 0;                                   64   -------- 2   
        //public static ushort[] vehical_transfer_time = new ushort[16384];         16   -------- 3
        ////fixed debug check value 120;                                             4   -------- 4  
        //public static bool[] vehical_last_transfer_flag = new bool[16384];         1   -------- 5
        //public static uint temp_total_citizen_vehical_time = 0;//temp use         32   -------- 6
        //public static uint temp_total_citizen_vehical_time_last = 0;//temp use    32   -------- 7
        //public static uint total_citizen_vehical_time = 0;                        32   -------- 8
        //public static long public_transport_fee = 0;                              64   -------- 9
        //public static long all_transport_fee = 0;                                 64   -------- 10
        //public static byte citizen_average_transport_fee = 0;                      4   -------- 11
        //fixed debug check value 110;                                               4   -------- 12
        //in pc_ResidentAI
        //public static uint precitizenid = 0;                                      13
        //public static int family_count = 0;                                       14
        //public static int family_profit_money_num = 0;                            15
        //public static int family_loss_money_num = 0;                              16
        //public static int citizen_salary_count = 0;                               17
        //public static int citizen_outcome_count = 0;                              18
        //public static int citizen_salary_tax_total = 0;                           19
        //public static float temp_citizen_salary_tax_total = 0f;                      
        //public static bool citizen_process_done = false;
        //public static int Road = 0;
        //public static int Electricity = 0;
        //public static int Water = 0;
        //public static int Beautification = 0;
        //public static int Garbage = 0;
        //public static int HealthCare = 0;
        //public static int PoliceDepartment = 0;
        //public static int Education = 0;
        //public static int Monument = 0;
        //public static int FireDepartment = 0;
        //public static int PublicTransport_bus = 0;
        //public static int PublicTransport_tram = 0;
        //public static int PublicTransport_ship = 0;
        //public static int PublicTransport_plane = 0;
        //public static int PublicTransport_metro = 0;
        //public static int PublicTransport_train = 0;
        //public static int PublicTransport_taxi = 0;
        public void save_float(ref int idex, float item)
        {
            int i;
            byte[] temp_data;
            temp_data = BitConverter.GetBytes(item);
            for(i = 0; i < temp_data.Length; i++)
            {
                save_data[idex + i] = temp_data[i];
                idex = idex + 1;
            }
        }

        public void save_uint(ref int idex, uint item)
        {
            int i;
            byte[] temp_data;
            temp_data = BitConverter.GetBytes(item);
            for (i = 0; i < temp_data.Length; i++)
            {
                save_data[idex + i] = temp_data[i];
                idex = idex + 1;
            }
        }

        public void save_int(ref int idex, int item)
        {
            int i;
            byte[] temp_data;
            temp_data = BitConverter.GetBytes(item);
            for (i = 0; i < temp_data.Length; i++)
            {
                save_data[idex + i] = temp_data[i];
                idex = idex + 1;
            }
        }

        public void save_byte(ref int idex, byte item)
        {
            int i;
            byte[] temp_data;
            temp_data = BitConverter.GetBytes(item);
            for (i = 0; i < temp_data.Length; i++)
            {
                save_data[idex + i] = temp_data[i];
                idex = idex + 1;
            }
        }

        public void save_short(ref int idex, short item)
        {
            int i;
            byte[] temp_data;
            temp_data = BitConverter.GetBytes(item);
            for (i = 0; i < temp_data.Length; i++)
            {
                save_data[idex + i] = temp_data[i];
                idex = idex + 1;
            }
        }

        public void save_ushort(ref int idex, ushort item)
        {
            int i;
            byte[] temp_data;
            temp_data = BitConverter.GetBytes(item);
            for (i = 0; i < temp_data.Length; i++)
            {
                save_data[idex + i] = temp_data[i];
                idex = idex + 1;
            }
        }

        public void save_bool(ref int idex, bool item)
        {
            int i;
            byte[] temp_data;
            temp_data = BitConverter.GetBytes(item);
            for (i = 0; i < temp_data.Length; i++)
            {
                save_data[idex + i] = temp_data[i];
                idex = idex + 1;
            }
        }


        public bool load_bool(ref int idex)
        {
            bool tmp;
            tmp = BitConverter.ToBoolean(load_data,idex);
            idex = idex + 1;
            return tmp;
        }

        public static void gather_save_data()
        {
            //BitConverter.ToSingle();
            //BitConverter.GetBytes((float)1);
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
            for (int i = 0; i < comm_data.citizen_money.Length; i++)
            {
                comm_data.citizen_money[i] = 0;
            }
        }
    }
}
