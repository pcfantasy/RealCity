using ColossalFramework;
using Harmony;
using RealCity.CustomData;
using System;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class TollBoothEnterBuildingSegmentPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(TollBoothAI).GetMethod("EnterBuildingSegment", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(ushort), typeof(byte), typeof(InstanceID) }, null);
        }

        public static bool Prefix(ref Building data, InstanceID itemID)
        {
            bool canCharge = false;
            if ((data.m_flags & Building.Flags.Active) != Building.Flags.None)
            {
                ushort vehicle = itemID.Vehicle;
                if (vehicle != 0)
                {
                    VehicleManager instance = Singleton<VehicleManager>.instance;
                    Vehicle vehicleData = instance.m_vehicles.m_buffer[vehicle];
                    VehicleInfo info = instance.m_vehicles.m_buffer[vehicle].Info;
                    if (vehicleData.m_transferType != 112 && vehicleData.m_transferType != 113)
                    {
                        if (!VehicleData.isVehicleCharged[vehicle])
                        {
                            if (info.m_vehicleAI is CargoTruckAI && (instance.m_vehicles.m_buffer[vehicle].m_flags.IsFlagSet(Vehicle.Flags.DummyTraffic)))
                            {
                                canCharge = true;
                                VehicleData.isVehicleCharged[vehicle] = true;
                            }
                            else if (info.m_vehicleAI is PassengerCarAI)
                            {
                                bool is_tourist = false;
                                bool is_dummy = false;
                                if (instance.m_vehicles.m_buffer[vehicle].m_citizenUnits != 0)
                                {
                                    CitizenManager instance2 = Singleton<CitizenManager>.instance;
                                    if (instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen0 != 0)
                                    {
                                        is_tourist = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen0].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None);
                                        is_dummy = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen0].m_flags & Citizen.Flags.DummyTraffic) != Citizen.Flags.None);
                                    }
                                    if (instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen1 != 0)
                                    {
                                        is_tourist = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen1].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None);
                                        is_dummy = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen1].m_flags & Citizen.Flags.DummyTraffic) != Citizen.Flags.None);
                                    }
                                    if (instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen2 != 0)
                                    {
                                        is_tourist = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen2].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None);
                                        is_dummy = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen2].m_flags & Citizen.Flags.DummyTraffic) != Citizen.Flags.None);

                                    }
                                    if (instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen3 != 0)
                                    {
                                        is_tourist = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen3].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None);
                                        is_dummy = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen3].m_flags & Citizen.Flags.DummyTraffic) != Citizen.Flags.None);
                                    }
                                    if (instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen4 != 0)
                                    {
                                        is_tourist = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen4].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None);
                                        is_dummy = ((instance2.m_citizens.m_buffer[instance2.m_units.m_buffer[instance.m_vehicles.m_buffer[vehicle].m_citizenUnits].m_citizen4].m_flags & Citizen.Flags.DummyTraffic) != Citizen.Flags.None);
                                    }
                                }
                                if (is_dummy || is_tourist)
                                {
                                    VehicleData.isVehicleCharged[vehicle] = true;
                                    canCharge = true;
                                }
                            }
                        }
                    }
                }
            }
            if (!canCharge) { return false; }
            return true;
        }
    }
}
