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
    public class pc_MaintenanceTruckAI : MaintenanceTruckAI
    {
        private bool ArriveAtTarget(ushort vehicleID, ref Vehicle data)
        {
            if (data.m_targetBuilding == 0)
            {
                return true;
            }
            if ((data.m_flags & Vehicle.Flags.Exporting) != (Vehicle.Flags)0)
            {
                data.m_flags &= ~Vehicle.Flags.Exporting;
                int num = Mathf.Min(0, (int)data.m_transferSize - this.m_maintenanceCapacity);
                //DebugLog.LogToFileOnly("this.m_maintenanceCapacity = " + this.m_maintenanceCapacity.ToString());
                BuildingInfo info = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_targetBuilding].Info;
                info.m_buildingAI.ModifyMaterialBuffer(data.m_targetBuilding, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_targetBuilding], (TransferManager.TransferReason)data.m_transferType, ref num);
                var instance = Singleton<BuildingManager>.instance;
                if ((instance.m_buildings.m_buffer[(int)data.m_targetBuilding].m_flags & Building.Flags.IncomingOutgoing) == Building.Flags.Incoming)
                {
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
                        this.RemoveTarget(vehicleID, ref data);
                        this.SetTarget(vehicleID, ref data, 0);
                        return false;
                    }
                }
            }
            else
            {
                this.SetTarget(vehicleID, ref data, 0);
            }
            return false;
        }


        public override void StartTransfer(ushort vehicleID, ref Vehicle data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (material == (TransferManager.TransferReason)data.m_transferType)
            {
                if ((data.m_flags & Vehicle.Flags.WaitingTarget) != (Vehicle.Flags)0)
                {
                    if (offer.Building != 0)
                    {
                        //DebugLog.LogToFileOnly("need to go outside for road maintenance");
                        this.SetTarget_1(vehicleID, ref data, offer.Building);
                        data.m_flags |= Vehicle.Flags.Exporting;
                    }
                    else
                    {
                        this.SetTarget(vehicleID, ref data, offer.NetSegment);
                    }
                }
            }
            else
            {
                base.StartTransfer(vehicleID, ref data, material, offer);
            }
        }

        public void SetTarget_1(ushort vehicleID, ref Vehicle data, ushort targetBuilding)
        {
            this.RemoveTarget(vehicleID, ref data);
            data.m_targetBuilding = targetBuilding;
            data.m_flags &= ~Vehicle.Flags.WaitingTarget;
            data.m_waitCounter = 0;
            Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)targetBuilding].AddGuestVehicle(vehicleID, ref data);
            if (!this.StartPathFind_1(vehicleID, ref data))
            {
                data.Unspawn(vehicleID);
            }
        }

        private void RemoveTarget(ushort vehicleID, ref Vehicle data)
        {
            if (data.m_targetBuilding != 0)
            {
                Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_targetBuilding].RemoveGuestVehicle(vehicleID, ref data);
                data.m_targetBuilding = 0;
            }
        }

        protected bool StartPathFind_1(ushort vehicleID, ref Vehicle vehicleData)
        {
            if ((vehicleData.m_flags & Vehicle.Flags.WaitingTarget) != (Vehicle.Flags)0)
            {
                return true;
            }
            if ((vehicleData.m_flags & Vehicle.Flags.GoingBack) != (Vehicle.Flags)0)
            {
                if (vehicleData.m_sourceBuilding != 0)
                {
                    Vector3 endPos = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)vehicleData.m_sourceBuilding].CalculateSidewalkPosition();
                    return this.StartPathFind(vehicleID, ref vehicleData, vehicleData.m_targetPos3, endPos);
                }
            }
            else if (vehicleData.m_targetBuilding != 0)
            {
                Vector3 endPos2 = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)vehicleData.m_targetBuilding].CalculateSidewalkPosition();
                return this.StartPathFind(vehicleID, ref vehicleData, vehicleData.m_targetPos3, endPos2);
            }
            return false;
        }


        private bool CheckTargetSegment(ushort vehicleID, ref Vehicle vehicleData)
        {
            if ((vehicleData.m_flags & Vehicle.Flags.Exporting) != (Vehicle.Flags)0)
            {
                return true;
            }
            else
            {
                NetManager instance = Singleton<NetManager>.instance;
                return vehicleData.m_targetBuilding < 36864 && (instance.m_segments.m_buffer[(int)vehicleData.m_targetBuilding].m_flags & (NetSegment.Flags.Created | NetSegment.Flags.Deleted)) == NetSegment.Flags.Created && instance.m_segments.m_buffer[(int)vehicleData.m_targetBuilding].m_condition < 192;
            }
        }
    }
}
