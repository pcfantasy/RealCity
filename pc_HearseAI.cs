using ColossalFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RealCity
{
    public class pc_HearseAI:HearseAI
    {
        private bool ArriveAtTarget(ushort vehicleID, ref Vehicle data)
        {
            if (data.m_targetBuilding == 0)
            {
                Singleton<VehicleManager>.instance.ReleaseVehicle(vehicleID);
                return true;
            }
            if (data.m_transferType == 42)
            {
                int num = 0;
                if ((data.m_flags & Vehicle.Flags.TransferToTarget) != (Vehicle.Flags)0)
                {
                    num = (int)data.m_transferSize;
                }
                if ((data.m_flags & Vehicle.Flags.TransferToSource) != (Vehicle.Flags)0)
                {
                    num = Mathf.Min(0, (int)data.m_transferSize - this.m_corpseCapacity);
                }
                BuildingManager instance = Singleton<BuildingManager>.instance;
                BuildingInfo info = instance.m_buildings.m_buffer[(int)data.m_targetBuilding].Info;
                info.m_buildingAI.ModifyMaterialBuffer(data.m_targetBuilding, ref instance.m_buildings.m_buffer[(int)data.m_targetBuilding], (TransferManager.TransferReason)data.m_transferType, ref num);
                process_deadmove_income_arrive_at_target(vehicleID, ref data, num);
                if ((data.m_flags & Vehicle.Flags.TransferToTarget) != (Vehicle.Flags)0)
                {
                    data.m_transferSize = (ushort)Mathf.Clamp((int)data.m_transferSize - num, 0, (int)data.m_transferSize);
                }
                if ((data.m_flags & Vehicle.Flags.TransferToSource) != (Vehicle.Flags)0)
                {
                    data.m_transferSize += (ushort)Mathf.Max(0, -num);
                }
                if (data.m_sourceBuilding != 0 && (instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].m_flags & Building.Flags.IncomingOutgoing) == Building.Flags.Outgoing)
                {
                    BuildingInfo info2 = instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].Info;
                    ushort num2 = instance.FindBuilding(instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].m_position, 200f, info2.m_class.m_service, info2.m_class.m_subService, Building.Flags.Incoming, Building.Flags.Outgoing);
                    if (num2 != 0)
                    {
                        instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].RemoveOwnVehicle(vehicleID, ref data);
                        data.m_sourceBuilding = num2;
                        instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].AddOwnVehicle(vehicleID, ref data);
                    }
                }
            }
            else
            {
                this.LoadDeadCitizens(vehicleID, ref data, data.m_targetBuilding);
                for (int i = 0; i < this.m_driverCount; i++)
                {
                    this.CreateDriver(vehicleID, ref data, Citizen.AgePhase.Senior0);
                }
                data.m_flags |= Vehicle.Flags.Stopped;
            }
            this.SetTarget(vehicleID, ref data, 0);
            return false;
        }

        private void process_deadmove_income_arrive_at_target(ushort vehicleID, ref Vehicle data, int num)
        {
            BuildingManager instance = Singleton<BuildingManager>.instance;
            Building building = instance.m_buildings.m_buffer[(int)data.m_sourceBuilding];
            Building building1 = instance.m_buildings.m_buffer[(int)data.m_targetBuilding];
            BuildingInfo info = instance.m_buildings.m_buffer[(int)data.m_targetBuilding].Info;
            if ((data.m_flags & Vehicle.Flags.TransferToTarget) != (Vehicle.Flags)0)
            {
                //DebugLog.LogToFileOnly("find dead move in city, num =" + num.ToString() + "transfer_typ = " + data.m_transferType.ToString());
                //info.m_buildingAI.ModifyMaterialBuffer(data.m_targetBuilding, ref instance.m_buildings.m_buffer[(int)data.m_targetBuilding], (TransferManager.TransferReason)data.m_transferType, ref num);
                if (building.m_flags.IsFlagSet(Building.Flags.Untouchable))
                {
                    Singleton<EconomyManager>.instance.AddPrivateIncome((int)(num * 100f), ItemClass.Service.HealthCare, ItemClass.SubService.None, ItemClass.Level.Level3, 115);
                }
            }
        }


        private void LoadDeadCitizens(ushort vehicleID, ref Vehicle data, ushort buildingID)
        {
            BuildingManager instance = Singleton<BuildingManager>.instance;
            CitizenManager instance2 = Singleton<CitizenManager>.instance;
            uint num = data.m_citizenUnits;
            int num2 = 0;
            while (num != 0u)
            {
                uint nextUnit = instance2.m_units.m_buffer[(int)((UIntPtr)num)].m_nextUnit;
                for (int i = 0; i < 5; i++)
                {
                    uint citizen = instance2.m_units.m_buffer[(int)((UIntPtr)num)].GetCitizen(i);
                    if (citizen != 0u && instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Dead && instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].CurrentLocation != Citizen.Location.Moving)
                    {
                        ushort instance3 = instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_instance;
                        if (instance3 != 0)
                        {
                            instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_instance = 0;
                            instance2.m_instances.m_buffer[(int)instance3].m_citizen = 0u;
                            instance2.ReleaseCitizenInstance(instance3);
                        }
                        instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen)].CurrentLocation = Citizen.Location.Moving;
                    }
                }
                num = nextUnit;
                if (++num2 > 524288)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }
            int num3 = this.m_corpseCapacity;
            if (data.m_sourceBuilding == 0)
            {
                num3 = (int)data.m_transferSize;
            }
            else if ((instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].m_flags & Building.Flags.Active) == Building.Flags.None)
            {
                num3 = (int)data.m_transferSize;
            }
            else
            {
                BuildingInfo info = instance.m_buildings.m_buffer[(int)data.m_sourceBuilding].Info;
                int num4;
                int num5;
                info.m_buildingAI.GetMaterialAmount(data.m_sourceBuilding, ref instance.m_buildings.m_buffer[(int)data.m_sourceBuilding], TransferManager.TransferReason.Dead, out num4, out num5);
                num3 = Mathf.Min(num3, num5 - num4);
            }
            if ((int)data.m_transferSize < num3)
            {
                num = Singleton<BuildingManager>.instance.m_buildings.m_buffer[(int)buildingID].m_citizenUnits;
                num2 = 0;
                while (num != 0u)
                {
                    uint nextUnit2 = instance2.m_units.m_buffer[(int)((UIntPtr)num)].m_nextUnit;
                    for (int j = 0; j < 5; j++)
                    {
                        uint citizen2 = instance2.m_units.m_buffer[(int)((UIntPtr)num)].GetCitizen(j);
                        if (citizen2 != 0u && instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen2)].Dead && instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen2)].GetBuildingByLocation() == buildingID)
                        {
                            ushort instance4 = instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen2)].m_instance;
                            if (instance4 != 0)
                            {
                                instance2.ReleaseCitizenInstance(instance4);
                            }
                            instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen2)].SetVehicle(citizen2, vehicleID, 0u);
                            instance2.m_citizens.m_buffer[(int)((UIntPtr)citizen2)].CurrentLocation = Citizen.Location.Moving;
                            if ((int)(data.m_transferSize += 1) >= num3)
                            {
                                return;
                            }
                        }
                    }
                    num = nextUnit2;
                    if (++num2 > 524288)
                    {
                        CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                        break;
                    }
                }
            }
        }

        private void CreateDriver(ushort vehicleID, ref Vehicle data, Citizen.AgePhase agePhase)
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


    }
}
