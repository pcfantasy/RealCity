using System;
using ICities;
using RealCity.CustomAI;
using RealCity.CustomData;
using RealCity.CustomManager;

namespace RealCity.Util
{
    public class SaveAndRestore : SerializableDataExtensionBase
    {
        private static ISerializableData _serializableData;

        public static void SaveData(ref int idex, long item, ref byte[] container)
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

        public static void SaveData(ref int idex, float item, ref byte[] container)
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

        public static void SaveData(ref int idex, float[] item, ref byte[] container)
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

        public static void SaveData(ref int idex, uint item, ref byte[] container)
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

        public static void SaveData(ref int idex, int item, ref byte[] container)
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

        public static void SaveData(ref int idex, int[] item, ref byte[] container)
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

        public static void SaveData(ref int idex, byte item, ref byte[] container)
        {
            container[idex] = item;
            idex = idex + 1;
        }


        public static void SaveData(ref int idex, byte[] item, ref byte[] container)
        {
            int j;
            for (j = 0; j < item.Length; j++)
            {
                container[idex + j] = item[j];
            }
            idex = idex + item.Length;
        }

        public static void SaveData(ref int idex, short item, ref byte[] container)
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

        public static void SaveData(ref int idex, ushort item, ref byte[] container)
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

        public static void SaveData(ref int idex, ushort[] item, ref byte[] container)
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

        public static void SaveData(ref int idex, bool item, ref byte[] container)
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

        public static void SaveData(ref int idex, bool[] item, ref byte[] container)
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

        public static void LoadData(ref int idex, byte[] container, ref bool item)
        {
            if (idex < container.Length)
            {
                item = BitConverter.ToBoolean(container, idex);
            }
            else
            {
                DebugLog.LogToFileOnly($"load data is too short, please check {container.Length}");
                item = false;
            }
            idex = idex + 1;
        }

        public static void LoadData(ref int idex, byte[] container, ref bool[] item)
        {
            if (idex < container.Length)
            {
                int i;
                for (i = 0; i < item.Length; i++)
                {
                    item[i] = BitConverter.ToBoolean(container, idex);
                    idex = idex + 1;
                }
            }
            else
            {
                int i;
                for (i = 0; i < item.Length; i++)
                {
                    idex = idex + 1;
                }
                DebugLog.LogToFileOnly($"load data is too short, please check {container.Length}");
            }
        }

        public static void LoadData(ref int idex, byte[] container, ref int item)
        {
            if (idex < container.Length)
            {
                item = BitConverter.ToInt32(container, idex);
            }
            else
            {
                DebugLog.LogToFileOnly($"load data is too short, please check {container.Length}");
                item = 0;
            }
            idex = idex + 4;
        }

        public static void LoadData(ref int idex, byte[] container, ref int[] item)
        {
            if (idex < container.Length)
            {
                int i;
                for (i = 0; i < item.Length; i++)
                {
                    item[i] = BitConverter.ToInt32(container, idex);
                    idex = idex + 4;
                }
            }
            else
            {
                int i;
                for (i = 0; i < item.Length; i++)
                {
                    idex = idex + 4;
                }
                DebugLog.LogToFileOnly($"load data is too short, please check {container.Length}");
            }
        }

        public static void LoadData(ref int idex, byte[] container, ref float item)
        {
            if (idex < container.Length)
            {
                item = BitConverter.ToSingle(container, idex);
            }
            else
            {
                DebugLog.LogToFileOnly($"load data is too short, please check {container.Length}");
                item = 0;
            }
            idex = idex + 4;
        }

        public static void LoadData(ref int idex, byte[] container, ref float[] item)
        {
            int i;
            if (idex < container.Length)
            {
                for (i = 0; i < item.Length; i++)
                {
                    item[i] = BitConverter.ToSingle(container, idex);
                    idex = idex + 4;
                }
            }
            else
            {
                for (i = 0; i < item.Length; i++)
                {
                    idex = idex + 4;
                }
                DebugLog.LogToFileOnly($"load data is too short, please check {container.Length}");
            }
        }

        public static void LoadData(ref int idex, byte[] container, ref uint item)
        {
            if (idex < container.Length)
            {
                item = BitConverter.ToUInt32(container, idex);
            }
            else
            {
                DebugLog.LogToFileOnly($"load data is too short, please check {container.Length}");
                item = 0;
            }
            idex = idex + 4;
        }

        public static void LoadData(ref int idex, byte[] container, ref uint[] item)
        {
            int i;
            if (idex < container.Length)
            {
                for (i = 0; i < item.Length; i++)
                {
                    item[i] = BitConverter.ToUInt32(container, idex);
                    idex = idex + 4;
                }
            }
            else
            {
                for (i = 0; i < item.Length; i++)
                {
                    idex = idex + 4;
                }
                DebugLog.LogToFileOnly($"load data is too short, please check {container.Length}");
            }
        }

        public static void LoadData(ref int idex, byte[] container, ref ushort item)
        {
            if (idex < container.Length)
            {
                item = BitConverter.ToUInt16(container, idex);
            }
            else
            {
                DebugLog.LogToFileOnly($"load data is too short, please check {container.Length}");
                item = 0;
            }
            idex = idex + 2;
        }

        public static void LoadData(ref int idex, byte[] container, ref short item)
        {
            if (idex < container.Length)
            {
                item = BitConverter.ToInt16(container, idex);
            }
            else
            {
                DebugLog.LogToFileOnly($"load data is too short, please check {container.Length}");
                item = 0;
            }
            idex = idex + 2;
        }

        public static void LoadData(ref int idex, byte[] container, ref ushort[] item)
        {
            int i;
            if (idex < container.Length)
            {
                for (i = 0; i < item.Length; i++)
                {
                    item[i] = BitConverter.ToUInt16(container, idex);
                    idex = idex + 2;
                }
            }
            else
            {
                DebugLog.LogToFileOnly($"load data is too short, please check {container.Length}");
                for (i = 0; i < item.Length; i++)
                {
                    idex = idex + 2;
                }
            }
        }

        public static void LoadData(ref int idex, byte[] container, ref long item)
        {
            if (idex < container.Length)
            {
                item = BitConverter.ToInt64(container, idex);
            }
            else
            {
                DebugLog.LogToFileOnly($"load data is too short, please check {container.Length}");
                item = 0;
            }
            idex = idex + 8;
        }

        public static void LoadData(ref int idex, byte[] container, ref byte item)
        {
            if (idex < container.Length)
            {
                item = container[idex];
            }
            else
            {
                DebugLog.LogToFileOnly($"load data is too short, please check {container.Length}");
                item = 0;
            }
            idex = idex + 1;
        }

        public static void LoadData(ref int idex, byte[] container, ref byte[] item)
        {
            int i;
            if (idex < container.Length)
            {
                for (i = 0; i < item.Length; i++)
                {
                    item[i] = container[idex];
                    idex = idex + 1;
                }
            }
            else
            {
                for (i = 0; i < item.Length; i++)
                {
                    idex = idex + 1;
                }
                DebugLog.LogToFileOnly($"load data is too short, please check {container.Length}");
            }
        }

        public override void OnCreated(ISerializableData serializableData)
        {
            _serializableData = serializableData;
        }

        public override void OnReleased()
        {
        }

        public override void OnSaveData()
        {
            if (Loader.CurrentLoadMode == LoadMode.LoadGame || Loader.CurrentLoadMode == LoadMode.NewGame)
            {
                DebugLog.LogToFileOnly("StartSave");
                //1
                var saveData = new byte[2448];
                RealCityEconomyManager.Save(ref saveData);
                _serializableData.SaveData("RealCity RealCityEconomyManager", saveData);

                //2
                saveData = new byte[125];
                MainDataStore.Save(ref saveData);
                _serializableData.SaveData("RealCity MainDataStore", saveData);

                //3
                saveData = new byte[48];
                RealCityResidentAI.Save(ref saveData);
                _serializableData.SaveData("RealCity RealCityResidentAI", saveData);

                //4
                saveData = new byte[60];
                RealCityPrivateBuildingAI.Save(ref saveData);
                _serializableData.SaveData("RealCity RealCityPrivateBuildingAI", saveData);

                //5
                saveData = new byte[4194304];
                CitizenData.Save(ref saveData);
                _serializableData.SaveData("RealCity CitizenData", saveData);

                //6
                saveData = new byte[58];
                Politics.Save(ref saveData);
                _serializableData.SaveData("RealCity Politics", saveData);

                //7
                saveData = new byte[1536];
                TransportLineData.Save(ref saveData);
                _serializableData.SaveData("RealCity TransportLineData", saveData);

                //8
                saveData = new byte[442368];
                BuildingData.Save(ref saveData);
                _serializableData.SaveData("RealCity BuildingData", saveData);

                //9
                saveData = new byte[196608];
                VehicleData.Save(ref saveData);
                _serializableData.SaveData("RealCity VehicleData", saveData);

                //10
                saveData = new byte[3145728];
                CitizenUnitData.Save(ref saveData);
                _serializableData.SaveData("RealCity CitizenUnitData", saveData);
            }
        }

        public override void OnLoadData()
        {
            Loader.InitData();
            DebugLog.LogToFileOnly("StartLoad");
            //1
            var saveData = _serializableData.LoadData("RealCity RealCityEconomyManager");
            if (saveData == null)
                DebugLog.LogToFileOnly("no RealCity RealCityEconomyManager, please check");
            else
                RealCityEconomyManager.Load(ref saveData);

            //2
            saveData = _serializableData.LoadData("RealCity MainDataStore");
            if (saveData == null)
                DebugLog.LogToFileOnly("no RealCity MainDataStore, please check");
            else
                MainDataStore.Load(ref saveData);

            //3
            saveData = _serializableData.LoadData("RealCity RealCityResidentAI");
            if (saveData == null)
                DebugLog.LogToFileOnly("no RealCity RealCityResidentAI, please check");
            else
                RealCityResidentAI.Load(ref saveData);

            //4
            saveData = _serializableData.LoadData("RealCity RealCityPrivateBuildingAI");
            if (saveData == null)
                DebugLog.LogToFileOnly("no RealCity RealCityPrivateBuildingAI, please check");
            else
                RealCityPrivateBuildingAI.Load(ref saveData);

            //5
            saveData = _serializableData.LoadData("RealCity CitizenData");
            if (saveData == null)
                DebugLog.LogToFileOnly("no RealCity CitizenData, please check");
            else
                CitizenData.Load(ref saveData);

            //6
            saveData = _serializableData.LoadData("RealCity Politics");
            if (saveData == null)
                DebugLog.LogToFileOnly("no RealCity Politics, please check");
            else
                Politics.Load(ref saveData);

            //7
            saveData = _serializableData.LoadData("RealCity TransportLineData");
            if (saveData == null)
                DebugLog.LogToFileOnly("no RealCity TransportLineData, please check");
            else
                TransportLineData.Load(ref saveData);

            //8
            saveData = _serializableData.LoadData("RealCity BuildingData");
            if (saveData == null)
                DebugLog.LogToFileOnly("no RealCity BuildingData, please check");
            else
                BuildingData.Load(ref saveData);

            //9
            saveData = _serializableData.LoadData("RealCity VehicleData");
            if (saveData == null)
                DebugLog.LogToFileOnly("no RealCity VehicleData, please check");
            else
                VehicleData.Load(ref saveData);

            //10
            saveData = _serializableData.LoadData("RealCity CitizenUnitData");
            if (saveData == null)
                DebugLog.LogToFileOnly("no RealCity CitizenUnitData, please check");
            else
                CitizenUnitData.Load(ref saveData);
        }
    }
}
