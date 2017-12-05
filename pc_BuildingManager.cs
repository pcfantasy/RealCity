using ColossalFramework;
using ColossalFramework.IO;
using ColossalFramework.Math;
using ColossalFramework.PlatformServices;
using ColossalFramework.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class pc_BuildingManager : BuildingManager
{
    public void ReleaseBuilding(ushort building)
    {
        if ((Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_service == ItemClass.Service.Commercial) || (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_service == ItemClass.Service.Industrial) || (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_service == ItemClass.Service.Office) || (Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_service == ItemClass.Service.Resident))
        {
            Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, 5000, Singleton<BuildingManager>.instance.m_buildings.m_buffer[work_building].Info.m_class.m_service, ItemClass.SubService.None, ItemClass.Level.Level1);
        }

        this.ReleaseBuildingImplementation(building, ref this.m_buildings.m_buffer[(int)building]);
    }

}
