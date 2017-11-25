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
    public class pc_PoliceCarAI:PoliceCarAI
    {
        private bool ArriveAtTarget(ushort vehicleID, ref Vehicle data)
        {
            if (data.m_targetBuilding == 0)
            {
                return true;
            }

            if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_targetBuilding].m_flags.IsFlagSet(Building.Flags.Untouchable))
            {
                int num = Mathf.Min(0, (int)data.m_transferSize - this.m_criminalCapacity);
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
                }

                if (this.m_info.m_class.m_level >= ItemClass.Level.Level4)
                {
                    this.ArrestCriminals(vehicleID, ref data, data.m_targetBuilding);
                    data.m_flags |= Vehicle.Flags.Stopped;
                }
                else
                {
                    int num = -this.m_crimeCapacity;
                    BuildingInfo info = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_targetBuilding].Info;
                    info.m_buildingAI.ModifyMaterialBuffer(data.m_targetBuilding, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_targetBuilding], (TransferManager.TransferReason)data.m_transferType, ref num);
                    if ((data.m_flags & Vehicle.Flags.Emergency2) != (Vehicle.Flags)0)
                    {
                        this.ArrestCriminals(vehicleID, ref data, data.m_targetBuilding);
                        for (int i = 0; i < this.m_policeCount; i++)
                        {
                            this.CreatePolice(vehicleID, ref data, Citizen.AgePhase.Adult0);
                        }
                        data.m_flags |= Vehicle.Flags.Stopped;
                    }
                }
            }
            this.SetTarget(vehicleID, ref data, 0);
            return false;
        }

        private void ArrestCriminals(ushort vehicleID, ref Vehicle vehicleData, ushort building)
        {
            if ((int)vehicleData.m_transferSize >= this.m_criminalCapacity)
            {
                return;
            }
            BuildingManager instance = Singleton<BuildingManager>.instance;
            CitizenManager instance2 = Singleton<CitizenManager>.instance;
            uint num = instance.m_buildings.m_buffer[(int)building].m_citizenUnits;
            int num2 = 0;
            while (num != 0u)
            {
                uint nextUnit = instance2.m_units.m_buffer[(int)((UIntPtr)num)].m_nextUnit;
                for (int i = 0; i < 5; i++)
                {
                    uint citizen = instance2.m_units.m_buffer[(int)((UIntPtr)num)].GetCitizen(i);
                    if (citizen != 0u && (instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Criminal || instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Arrested) && !instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Dead && instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].GetBuildingByLocation() == building)
                    {
                        instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].SetVehicle(citizen, vehicleID, 0u);
                        if (instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_vehicle != vehicleID)
                        {
                            vehicleData.m_transferSize = (ushort)this.m_criminalCapacity;
                            return;
                        }
                        instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Arrested = true;
                        //DebugLog.LogToFileOnly("arresting citizenID = " + citizen.ToString());
                        ushort instance3 = instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_instance;
                        if (instance3 != 0)
                        {
                            instance2.ReleaseCitizenInstance(instance3);
                        }
                        instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].CurrentLocation = Citizen.Location.Moving;
                        if ((int)(vehicleData.m_transferSize += 1) >= this.m_criminalCapacity)
                        {
                            return;
                        }
                    }
                }
                num = nextUnit;
                if (++num2 > 524288)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }
        }

        private void CreatePolice(ushort vehicleID, ref Vehicle data, Citizen.AgePhase agePhase)
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

        private bool ArriveAtSource(ushort vehicleID, ref Vehicle data)
        {
            if (data.m_sourceBuilding == 0)
            {
                Singleton<VehicleManager>.instance.ReleaseVehicle(vehicleID);
                return true;
            }
            int transferSize = (int)data.m_transferSize;
            BuildingInfo info = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].Info;
            info.m_buildingAI.ModifyMaterialBuffer(data.m_sourceBuilding, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_sourceBuilding], (TransferManager.TransferReason)data.m_transferType, ref transferSize);
            data.m_transferSize = (ushort)Mathf.Clamp((int)data.m_transferSize - transferSize, 0, (int)data.m_transferSize);
            this.UnloadCriminals(vehicleID, ref data);
            this.RemoveSource(vehicleID, ref data);
            return true;
        }

        private void UnloadCriminals(ushort vehicleID, ref Vehicle data)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint num = data.m_citizenUnits;
            int num2 = 0;
            int num3 = 0;
            while (num != 0u)
            {
                uint nextUnit = instance.m_units.m_buffer[(int)((UIntPtr)num)].m_nextUnit;
                for (int i = 0; i < 5; i++)
                {
                    uint citizen = instance.m_units.m_buffer[(int)((UIntPtr)num)].GetCitizen(i);
                    if (citizen != 0u && instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].CurrentLocation == Citizen.Location.Moving && instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Arrested)
                    {
                        ushort instance2 = instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_instance;
                        if (instance2 != 0)
                        {
                            instance.ReleaseCitizenInstance(instance2);
                        }
                        instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].SetVisitplace(citizen, data.m_sourceBuilding, 0u);
                        if (instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_visitBuilding != 0)
                        {
                            instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].CurrentLocation = Citizen.Location.Visit;
                            //DebugLog.LogToFileOnly("arrest success citizenID = " + citizen.ToString());
                            if (this.m_info.m_class.m_level >= ItemClass.Level.Level4)
                            {
                                this.SpawnPrisoner(vehicleID, ref data, citizen);
                            }
                        }
                        else
                        {
                            DebugLog.LogToFileOnly("arrest fail citizenID = " + citizen.ToString());
                            instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].CurrentLocation = Citizen.Location.Home;
                            instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Arrested = false;
                            num3++;
                        }
                    }
                }
                num = nextUnit;
                if (++num2 > 524288)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }
            data.m_transferSize = 0;
            if (num3 != 0 && data.m_sourceBuilding != 0)
            {
                BuildingManager instance3 = Singleton<BuildingManager>.instance;
                DistrictManager instance4 = Singleton<DistrictManager>.instance;
                byte district = instance4.GetDistrict(instance3.m_buildings.m_buffer[(int)data.m_sourceBuilding].m_position);
                District[] expr_212_cp_0_cp_0 = instance4.m_districts.m_buffer;
                byte expr_212_cp_0_cp_1 = district;
                expr_212_cp_0_cp_0[(int)expr_212_cp_0_cp_1].m_productionData.m_tempCriminalExtra = expr_212_cp_0_cp_0[(int)expr_212_cp_0_cp_1].m_productionData.m_tempCriminalExtra + (uint)num3;
            }
        }

        private void RemoveSource(ushort vehicleID, ref Vehicle data)
        {
            if (data.m_sourceBuilding != 0)
            {
                Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].RemoveOwnVehicle(vehicleID, ref data);
                data.m_sourceBuilding = 0;
            }
        }

        private void SpawnPrisoner(ushort vehicleID, ref Vehicle data, uint citizen)
        {
            if (data.m_sourceBuilding == 0)
            {
                return;
            }
            SimulationManager instance = Singleton<SimulationManager>.instance;
            CitizenManager instance2 = Singleton<CitizenManager>.instance;
            Citizen.Gender gender = Citizen.GetGender(citizen);
            CitizenInfo groupCitizenInfo = instance2.GetGroupCitizenInfo(ref instance.m_randomizer, this.m_info.m_class.m_service, gender, Citizen.SubCulture.Generic, Citizen.AgePhase.Young0);
            ushort num;
            if (groupCitizenInfo != null && instance2.CreateCitizenInstance(out num, ref instance.m_randomizer, groupCitizenInfo, citizen))
            {
                Vector3 randomDoorPosition = data.GetRandomDoorPosition(ref instance.m_randomizer, VehicleInfo.DoorType.Exit);
                groupCitizenInfo.m_citizenAI.SetCurrentVehicle(num, ref instance2.m_instances.m_buffer[(int)num], 0, 0u, randomDoorPosition);
                groupCitizenInfo.m_citizenAI.SetTarget(num, ref instance2.m_instances.m_buffer[(int)num], data.m_sourceBuilding);
            }
        }
    }
}
