using ColossalFramework;
using RealCity.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RealCity.CustomManager
{
    public class RealCityBuildingManager
    {
        public void SetNum(ref int[] source, ref int[] array)
        {
            System.Random rd = new System.Random();
            int range = array.Length;
            for (int i = 0; i < array.Length; i++)
            {
                //随机产生一个位置  
                int pos = rd.Next(range);
                //获取该位置的值  
                array[i] = source[pos];
                //改良：将最后一个数赋给被删除的索引所对应的值  
                source[pos] = source[range - 1];
                range--;
            }
        }

        public ushort FindRandomBuilding(Vector3 pos, float maxDistance, ItemClass.Service service, ItemClass.SubService subService, Building.Flags flagsRequired, Building.Flags flagsForbidden)
        {
            int num = Mathf.Max((int)((pos.x - maxDistance) / 64f + 135f), 0);
            int num2 = Mathf.Max((int)((pos.z - maxDistance) / 64f + 135f), 0);
            int num3 = Mathf.Min((int)((pos.x + maxDistance) / 64f + 135f), 269);
            int num4 = Mathf.Min((int)((pos.z + maxDistance) / 64f + 135f), 269);
            ushort result = 0;
            float num5 = maxDistance * maxDistance;
            BuildingManager building = Singleton<BuildingManager>.instance;
            SimulationManager instance2 = Singleton<SimulationManager>.instance;
            System.Random rd = new System.Random();
            ushort[] tempBuilding = new ushort[16];
            for (int i = 0; i < 16; i++)
            {
                tempBuilding[i] = 0;
            }

            if ((num4 >= num2) && (num3 >= num))
            {
                int[] source = new int[(num4 - num2 + 1) * (num3 - num + 1)];
                int[] array = new int[(num4 - num2 + 1) * (num3 - num + 1)];
                int idex = 0;
                for (int i = num2; i <= num4; i++)
                {
                    for (int j = num; j <= num3; j++)
                    {
                        source[idex] = i * 270 + j;
                        idex++;
                    }
                }

                SetNum(ref source, ref array);
                idex = 0;

                for (int i = 0; i <= num4 - num2; i++)
                {
                    for (int j = 0; j <= num3 - num; j++)
                    {
                        ushort num6 = building.m_buildingGrid[array[idex]];
                        idex++;
                        int num7 = 0;
                        int tempBuildingIdex = 0;
                        for (int z = 0; z < 16; z++)
                        {
                            tempBuilding[z] = 0;
                        }
                        while (num6 != 0)
                        {
                            BuildingInfo info = building.m_buildings.m_buffer[(int)num6].Info;
                            if ((info.m_class.m_service == service || service == ItemClass.Service.None) && (info.m_class.m_subService == subService || subService == ItemClass.SubService.None))
                            {
                                Building.Flags flags = building.m_buildings.m_buffer[(int)num6].m_flags;
                                if (info.m_class.m_service == ItemClass.Service.Commercial)
                                {
                                    if ((flags & (flagsRequired | flagsForbidden)) == flagsRequired)
                                    {
                                        if (building.m_buildings.m_buffer[(int)num6].m_customBuffer2 > 1000)
                                        {
                                            //float num8 = Vector3.SqrMagnitude(pos - building.m_buildings.m_buffer[(int)num6].m_position);
                                            //result = num6;
                                            //return result;
                                            tempBuilding[tempBuildingIdex] = num6;
                                            tempBuildingIdex++;
                                        }
                                    }
                                }
                            }
                            num6 = building.m_buildings.m_buffer[(int)num6].m_nextGridBuilding;
                            if (++num7 >= 49152)
                            {
                                CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                                break;
                            }
                            if (((num6 == 0) && (tempBuildingIdex != 0)) || (tempBuildingIdex == 16))
                            {
                                //DebugLog.LogToFileOnly("find comm building num = " + tempBuildingIdex.ToString());
                                num6 = tempBuilding[rd.Next(tempBuildingIdex)];
                                return num6;
                            }
                        }
                    }
                }
            }
            return result;
        }

        public ushort CustomFindBuilding(Vector3 pos, float maxDistance, ItemClass.Service service, ItemClass.SubService subService, Building.Flags flagsRequired, Building.Flags flagsForbidden)
        {
            BuildingManager building = Singleton<BuildingManager>.instance;
            if (maxDistance == 32f)
            {
                maxDistance = 128f;
                return FindRandomBuilding(pos, maxDistance, service, subService, flagsRequired, flagsForbidden);
            }
            int num = Mathf.Max((int)((pos.x - maxDistance) / 64f + 135f), 0);
            int num2 = Mathf.Max((int)((pos.z - maxDistance) / 64f + 135f), 0);
            int num3 = Mathf.Min((int)((pos.x + maxDistance) / 64f + 135f), 269);
            int num4 = Mathf.Min((int)((pos.z + maxDistance) / 64f + 135f), 269);
            ushort result = 0;
            float num5 = maxDistance * maxDistance;
            for (int i = num2; i <= num4; i++)
            {
                for (int j = num; j <= num3; j++)
                {
                    ushort num6 = building.m_buildingGrid[i * 270 + j];
                    int num7 = 0;
                    while (num6 != 0)
                    {
                        BuildingInfo info = building.m_buildings.m_buffer[(int)num6].Info;
                        if ((info.m_class.m_service == service || service == ItemClass.Service.None) && (info.m_class.m_subService == subService || subService == ItemClass.SubService.None))
                        {
                            Building.Flags flags = building.m_buildings.m_buffer[(int)num6].m_flags;
                            if ((flags & (flagsRequired | flagsForbidden)) == flagsRequired)
                            {
                                float num8 = Vector3.SqrMagnitude(pos - building.m_buildings.m_buffer[(int)num6].m_position);
                                if (num8 < num5)
                                {
                                    result = num6;
                                    num5 = num8;
                                }
                            }
                        }
                        num6 = building.m_buildings.m_buffer[(int)num6].m_nextGridBuilding;
                        if (++num7 >= 49152)
                        {
                            CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                            break;
                        }
                    }
                }
            }
            return result;
        }
    }
}
