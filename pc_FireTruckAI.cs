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
    public class pc_FireTruckAI:FireTruckAI
    {
        public bool ArriveAtTarget(ushort vehicleID, ref Vehicle data)
        {
            if (data.m_targetBuilding == 0)
            {
                Singleton<VehicleManager>.instance.ReleaseVehicle(vehicleID);
                return true;
            }
            if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_targetBuilding].m_flags.IsFlagSet(Building.Flags.Untouchable))
            {
                int num = Mathf.Min(0, (int)data.m_transferSize - this.m_fireFightingRate);
                BuildingInfo info = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_targetBuilding].Info;
                info.m_buildingAI.ModifyMaterialBuffer(data.m_targetBuilding, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_targetBuilding], (TransferManager.TransferReason)data.m_transferType, ref num);
                var instance = Singleton<BuildingManager>.instance;
                if ((instance.m_buildings.m_buffer[(int)data.m_targetBuilding].m_flags & Building.Flags.IncomingOutgoing) == Building.Flags.Incoming)
                {
                    //DebugLog.LogToFileOnly("try turn around building = " + instance.m_buildings.m_buffer[(int)data.m_targetBuilding].Info.m_class.ToString());
                    double x = instance.m_buildings.m_buffer[(int)data.m_targetBuilding].m_position.x - instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].m_position.x;
                    double z = instance.m_buildings.m_buffer[(int)data.m_targetBuilding].m_position.z - instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].m_position.z;
                    double distance = (x + z)/2f;
                    comm_data.building_money[data.m_sourceBuilding] += (float)(-num * (distance / 20f));
                    Singleton<EconomyManager>.instance.AddPrivateIncome((int)(-num * (distance * 5f)), ItemClass.Service.FireDepartment, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                    ushort num3 = instance.FindBuilding(instance.m_buildings.m_buffer[(int)data.m_targetBuilding].m_position, 200f, info.m_class.m_service, ItemClass.SubService.None, Building.Flags.Outgoing, Building.Flags.Incoming);
                    if (num3 != 0)
                    {
                        //data.Unspawn(vehicleID);
                        BuildingInfo info3 = instance.m_buildings.m_buffer[(int)num3].Info;
                        //DebugLog.LogToFileOnly("firetruck try turn around get outgoing building = " + info3.m_class.ToString());
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
            }
            else
            {
                if ((data.m_sourceBuilding != 0) && Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].m_flags.IsFlagSet(Building.Flags.Untouchable))
                {
                    var instance1 = Singleton<BuildingManager>.instance;
                    BuildingInfo info2 = instance1.m_buildings.m_buffer[(int)data.m_sourceBuilding].Info;
                    if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].m_flags.IsFlagSet(Building.Flags.Outgoing))
                    {
                        ushort num20 = instance1.FindBuilding(instance1.m_buildings.m_buffer[(int)data.m_sourceBuilding].m_position, 200f, info2.m_class.m_service, ItemClass.SubService.None, Building.Flags.Incoming, Building.Flags.Outgoing);
                        if (num20 != 0)
                        {
                            instance1.m_buildings.m_buffer[(int)data.m_sourceBuilding].RemoveOwnVehicle(vehicleID, ref data);
                            data.m_sourceBuilding = num20;
                            instance1.m_buildings.m_buffer[(int)data.m_sourceBuilding].AddOwnVehicle(vehicleID, ref data);
                        }
                    }
                }

                for (int i = 0; i < this.m_firemanCount; i++)
                {
                    if (i < this.m_hoseCount)
                    {
                        this.CreateFireman(vehicleID, ref data, Citizen.AgePhase.Adult1);
                    }
                    else
                    {
                        this.CreateFireman(vehicleID, ref data, Citizen.AgePhase.Adult0);
                    }
                }
            }
            data.m_flags |= Vehicle.Flags.Stopped;
            return false;
        }

        public void CreateFireman(ushort vehicleID, ref Vehicle data, Citizen.AgePhase agePhase)
        {
            SimulationManager instance = Singleton<SimulationManager>.instance;
            CitizenManager instance2 = Singleton<CitizenManager>.instance;
            CitizenInfo groupCitizenInfo = instance2.GetGroupCitizenInfo(ref instance.m_randomizer, this.m_info.m_class.m_service, Citizen.Gender.Male, Citizen.SubCulture.Generic, agePhase);
            if (groupCitizenInfo != null)
            {
                int family = instance.m_randomizer.Int32(256u);
                uint num = 0u;
                if (instance2.CreateCitizen(out num, 90, family, ref instance.m_randomizer, groupCitizenInfo.m_gender))
                {
                    ushort num2;
                    if (instance2.CreateCitizenInstance(out num2, ref instance.m_randomizer, groupCitizenInfo, num))
                    {
                        Vector3 randomDoorPosition = data.GetRandomDoorPosition(ref instance.m_randomizer, VehicleInfo.DoorType.Exit);
                        groupCitizenInfo.m_citizenAI.SetCurrentVehicle(num2, ref instance2.m_instances.m_buffer[(int)num2], 0, 0u, randomDoorPosition);
                        groupCitizenInfo.m_citizenAI.SetTarget(num2, ref instance2.m_instances.m_buffer[(int)num2], data.m_targetBuilding);
                        instance2.m_citizens.m_buffer[(int)((UIntPtr)num)].SetVehicle(num, vehicleID, 0u);
                    }
                    else
                    {
                        instance2.ReleaseCitizen(num);
                    }
                }
            }
        }

        public override void StartTransfer(ushort vehicleID, ref Vehicle data, TransferManager.TransferReason material, TransferManager.TransferOffer offer)
        {
            if (material == (TransferManager.TransferReason)data.m_transferType)
            {
                if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[offer.Building].m_flags.IsFlagSet(Building.Flags.Untouchable))
                {
                    if (offer.Building !=0)
                    {
                        if ((data.m_flags & Vehicle.Flags.WaitingTarget) != (Vehicle.Flags)0)
                        {
                            this.SetTarget(vehicleID, ref data, offer.Building);
                        }
                    }
                }
                else
                {
                    if ((data.m_flags & (Vehicle.Flags.GoingBack | Vehicle.Flags.WaitingTarget)) != (Vehicle.Flags)0)
                    {
                        this.SetTarget(vehicleID, ref data, offer.Building);
                    }
                }
            }
            else
            {
                //base.StartTransfer(vehicleID, ref data, material, offer);
            }
        }
    }
}
