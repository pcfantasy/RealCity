using ColossalFramework;
using Harmony;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public static class HumanAISimulationStepPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(HumanAI).GetMethod("SimulationStep", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(CitizenInstance).MakeByRefType(), typeof(CitizenInstance.Frame).MakeByRefType(), typeof(bool) }, null);
        }
        public static byte[] watingPathTime = new byte[65536];
        public static void Postfix(ushort instanceID, ref CitizenInstance citizenData, ref CitizenInstance.Frame frameData, bool lodPhysics)
        {
            if (RealCity.removeStuck)
            {
                if (citizenData.m_flags.IsFlagSet(CitizenInstance.Flags.WaitingPath))
                {
                    if (citizenData.m_path != 0)
                    {
                        watingPathTime[instanceID]++;
                    }
                    if (watingPathTime[instanceID] > 192)
                    {
                        ushort building = 0;
                        building = citizenData.m_sourceBuilding;
                        var buildingData = Singleton<BuildingManager>.instance.m_buildings.m_buffer[building];
                        DebugLog.LogToFileOnly("DebugInfo: Stuck citizen target building m_class is " + buildingData.Info.m_class.ToString());
                        DebugLog.LogToFileOnly("DebugInfo: Stuck citizen target name is " + buildingData.Info.name.ToString());
                        DebugLog.LogToFileOnly("DebugInfo: Stuck citizen flag is " + citizenData.m_flags.ToString());
                        watingPathTime[instanceID] = 0;
                        Singleton<PathManager>.instance.ReleasePath(citizenData.m_path);
                        citizenData.m_path = 0u;
                        citizenData.m_flags = (citizenData.m_flags & ~(CitizenInstance.Flags.WaitingPath | CitizenInstance.Flags.WaitingTransport | CitizenInstance.Flags.EnteringVehicle | CitizenInstance.Flags.BoredOfWaiting | CitizenInstance.Flags.WaitingTaxi));
                    }
                }
                else
                {
                    watingPathTime[instanceID] = 0;
                }
            }
        }
    }
}
