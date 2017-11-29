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
    public class pc_AmbulanceAI: AmbulanceAI
    {
        private bool ArriveAtTarget(ushort vehicleID, ref Vehicle data)
        {
            if (data.m_targetBuilding == 0)
            {
                Singleton<VehicleManager>.instance.ReleaseVehicle(vehicleID);
                return true;
            }
            if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_targetBuilding].m_flags.IsFlagSet(Building.Flags.Untouchable))
            {
                int num = Mathf.Min(0, (int)data.m_transferSize - this.m_patientCapacity);
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
                        this.SetTarget(vehicleID, ref data, 0);
                        return false;
                    }
                }
            }            
            else
            {
                CitizenManager instance = Singleton<CitizenManager>.instance;
                uint num = data.m_citizenUnits;
                int num2 = 0;
                int temp_num = 0;
                while (num != 0u)
                {
                    uint nextUnit = instance.m_units.m_buffer[(int)((UIntPtr)num)].m_nextUnit;
                    for (int i = 0; i < 5; i++)
                    {
                        uint citizen = instance.m_units.m_buffer[(int)((UIntPtr)num)].GetCitizen(i);
                        if (citizen != 0u && instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].CurrentLocation != Citizen.Location.Moving)
                        {
                            ushort instance2 = instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_instance;
                            if (instance2 != 0)
                            {
                                instance.ReleaseCitizenInstance(instance2);
                            }
                            instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].CurrentLocation = Citizen.Location.Moving;
                            data.m_transferSize += 1;
                            temp_num++;
                        }
                    }
                    num = nextUnit;
                    if (++num2 > 524288)
                    {
                        CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                        break;
                    }
                }

                if ((data.m_sourceBuilding != 0) && Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].m_flags.IsFlagSet(Building.Flags.Untouchable))
                {
                    var instance1 = Singleton<BuildingManager>.instance;
                    //DebugLog.LogToFileOnly("try turn around get income hospital building = ");
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
                    Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, (int)(temp_num * 100f * comm_data.game_maintain_fee_decrease), this.m_info.m_class);
                }

                for (int j = 0; j < this.m_paramedicCount; j++)
                {
                    //DebugLog.LogToFileOnly("try CreateParamedic");
                    this.CreateParamedic(vehicleID, ref data, Citizen.AgePhase.Adult0);
                }
                data.m_flags |= Vehicle.Flags.Stopped;
                data.m_flags &= ~Vehicle.Flags.Emergency2;
            }
            this.SetTarget(vehicleID, ref data, 0);
            return false;
        }

        private void CreateParamedic(ushort vehicleID, ref Vehicle data, Citizen.AgePhase agePhase)
        {
            SimulationManager instance = Singleton<SimulationManager>.instance;
            CitizenManager instance2 = Singleton<CitizenManager>.instance;
            CitizenInfo groupCitizenInfo = instance2.GetGroupCitizenInfo(ref instance.m_randomizer, this.m_info.m_class.m_service, Citizen.Gender.Female, Citizen.SubCulture.Generic, agePhase);
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
                if ((data.m_flags & Vehicle.Flags.WaitingTarget) != (Vehicle.Flags)0)
                {
                    if (offer.Building != 0)
                    {
                        this.SetTarget(vehicleID, ref data, offer.Building);
                    }
                    else
                    {
                        uint citizen = offer.Citizen;
                        ushort buildingByLocation = Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].GetBuildingByLocation();
                        this.SetTarget(vehicleID, ref data, buildingByLocation);
                        Singleton<CitizenManager>.instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].SetVehicle(citizen, vehicleID, 0u);
                    }
                }
            }
            else
            {
                //VehicleAI.StartTransfer(vehicleID, ref data, material, offer);
            }
        }


    }
}
