using ColossalFramework;
using ColossalFramework.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RealCity
{
    public class RealCityGarbageTruckAI : GarbageTruckAI
    {
        private bool ArriveAtTarget(ushort vehicleID, ref Vehicle data)
        {
            if (data.m_targetBuilding == 0)
            {
                return true;
            }
            int num = 0;
            if ((data.m_flags & Vehicle.Flags.TransferToTarget) != (Vehicle.Flags)0)
            {
                num = (int)data.m_transferSize;
            }
            if ((data.m_flags & Vehicle.Flags.TransferToSource) != (Vehicle.Flags)0)
            {
                num = Mathf.Min(0, (int)data.m_transferSize - this.m_cargoCapacity);
            }
            BuildingManager instance = Singleton<BuildingManager>.instance;
            BuildingInfo info = instance.m_buildings.m_buffer[(int)data.m_targetBuilding].Info;
            info.m_buildingAI.ModifyMaterialBuffer(data.m_targetBuilding, ref instance.m_buildings.m_buffer[(int)data.m_targetBuilding], (TransferManager.TransferReason)data.m_transferType, ref num);
            ProcessGarbageIncomeArriveAtTarget(vehicleID, ref data, num);
            if ((data.m_flags & Vehicle.Flags.TransferToTarget) != (Vehicle.Flags)0)
            {
                data.m_transferSize = (ushort)Mathf.Clamp((int)data.m_transferSize - num, 0, (int)data.m_transferSize);
            }
            if ((data.m_flags & Vehicle.Flags.TransferToSource) != (Vehicle.Flags)0)
            {
                data.m_transferSize += (ushort)Mathf.Max(0, -num);
            }


            //Go back
            if (data.m_sourceBuilding != 0 && (instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].m_flags & Building.Flags.IncomingOutgoing) == Building.Flags.Outgoing)
            {
                BuildingInfo info2 = instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].Info;
                ushort num2 = instance.FindBuilding(instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].m_position, 200f, info2.m_class.m_service, ItemClass.SubService.None, Building.Flags.Incoming, Building.Flags.Outgoing);
                if (num2 != 0)
                {
                    instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].RemoveOwnVehicle(vehicleID, ref data);
                    data.m_sourceBuilding = num2;
                    instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].AddOwnVehicle(vehicleID, ref data);
                }
            }

            //Turn around
            if ((instance.m_buildings.m_buffer[(int)data.m_targetBuilding].m_flags & Building.Flags.IncomingOutgoing) == Building.Flags.Incoming)
            {
                double x = instance.m_buildings.m_buffer[(int)data.m_targetBuilding].m_position.x - instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].m_position.x;
                double z = instance.m_buildings.m_buffer[(int)data.m_targetBuilding].m_position.z - instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].m_position.z;
                x = (x > 0) ? x : -x;
                z = (z > 0) ? z : -z;
                double distance = (x + z) / 2f;
                Singleton<EconomyManager>.instance.AddPrivateIncome((int)(-num * (distance / 2000f)), ItemClass.Service.Garbage, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                //DebugLog.LogToFileOnly("try turn around building = " + instance.m_buildings.m_buffer[(int)data.m_targetBuilding].Info.m_class.ToString());
                ushort num3 = instance.FindBuilding(instance.m_buildings.m_buffer[(int)data.m_targetBuilding].m_position, 200f, info.m_class.m_service, ItemClass.SubService.None, Building.Flags.Outgoing, Building.Flags.Incoming);
                if (num3 != 0)
                {
                    //data.Unspawn(vehicleID);
                    BuildingInfo info3 = instance.m_buildings.m_buffer[(int)num3].Info;
                    //DebugLog.LogToFileOnly("try turn around get outgoing building = " + info3.m_class.ToString());
                    Randomizer randomizer = new Randomizer((int)vehicleID);
                    Vector3 vector;
                    Vector3 vector2;
                    info3.m_buildingAI.CalculateSpawnPosition(num3, ref instance.m_buildings.m_buffer[(int)num3], ref randomizer, this.m_info, out vector, out vector2);
                    Quaternion rotation = Quaternion.identity;
                    Vector3 forward = vector2 - vector;
                    if (forward.sqrMagnitude > 0.01f)
                    {
                        rotation = Quaternion.LookRotation(forward);
                    }
                    data.m_frame0 = new Vehicle.Frame(vector, rotation);
                    data.m_frame1 = data.m_frame0;
                    data.m_frame2 = data.m_frame0;
                    data.m_frame3 = data.m_frame0;
                    data.m_targetPos0 = vector;
                    data.m_targetPos0.w = 2f;
                    data.m_targetPos1 = vector2;
                    data.m_targetPos1.w = 2f;
                    data.m_targetPos2 = data.m_targetPos1;
                    data.m_targetPos3 = data.m_targetPos1;
                    this.FrameDataUpdated(vehicleID, ref data, ref data.m_frame0);
                    this.SetTarget(vehicleID, ref data, 0);
                    return false;
                }
            }
            this.SetTarget(vehicleID, ref data, 0);
            return false;
        }

        private void ProcessGarbageIncomeArriveAtTarget(ushort vehicleID, ref Vehicle data, int num)
        {
            BuildingManager instance = Singleton<BuildingManager>.instance;
            Building building = instance.m_buildings.m_buffer[(int)data.m_sourceBuilding];
            Building building1 = instance.m_buildings.m_buffer[(int)data.m_targetBuilding];
            BuildingInfo info = instance.m_buildings.m_buffer[(int)data.m_targetBuilding].Info;
            //move to outside modify matter.
            /*if ((data.m_flags & Vehicle.Flags.TransferToSource) != (Vehicle.Flags)0)
            {
                //DebugLog.LogToFileOnly("find garbage move in city, num =" + num.ToString() + "transfer_typ = " + data.m_transferType.ToString());
                //info.m_buildingAI.ModifyMaterialBuffer(data.m_targetBuilding, ref instance.m_buildings.m_buffer[(int)data.m_targetBuilding], (TransferManager.TransferReason)data.m_transferType, ref num);
                if (building1.m_flags.IsFlagSet(Building.Flags.Untouchable))
                {
                    //DebugLog.LogToFileOnly("find garbage in city, num =" + num.ToString() + "building = " + building1.Info.m_class.ToString());
                    Singleton<EconomyManager>.instance.AddPrivateIncome((int)(num * -0.1f), ItemClass.Service.Garbage, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                }
            }*/

            if ((data.m_flags & Vehicle.Flags.TransferToTarget) != (Vehicle.Flags)0)
            {
                //DebugLog.LogToFileOnly("find garbage from outside to city, num =" + num.ToString() + "transfer_typ = " + data.m_transferType.ToString());
                //info.m_buildingAI.ModifyMaterialBuffer(data.m_targetBuilding, ref instance.m_buildings.m_buffer[(int)data.m_targetBuilding], (TransferManager.TransferReason)data.m_transferType, ref num);
                if (building.m_flags.IsFlagSet(Building.Flags.Untouchable))
                {
                    if ((data.m_flags & Vehicle.Flags.Importing) != (Vehicle.Flags)0)
                    {
                        double x = instance.m_buildings.m_buffer[(int)data.m_targetBuilding].m_position.x - instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].m_position.x;
                        double z = instance.m_buildings.m_buffer[(int)data.m_targetBuilding].m_position.z - instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].m_position.z;
                        x = (x > 0) ? x : -x;
                        z = (z > 0) ? z : -z;
                        double distance = (x + z) / 2f;
                        Singleton<EconomyManager>.instance.AddPrivateIncome((int)(num * (distance / 2000f)), ItemClass.Service.Garbage, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                    }
                }
            }
        }

    }
}

