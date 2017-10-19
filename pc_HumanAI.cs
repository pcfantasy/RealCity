using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.Math;
using ColossalFramework.Threading;
using UnityEngine;


namespace RealCity
{
    public class pc_HumanAI : HumanAI
    {
        protected virtual void ArriveAtDestination_1(ushort instanceID, ref CitizenInstance citizenData, bool success)
        {
            uint citizen = citizenData.m_citizen;
            if (citizen != 0u)
            {
                CitizenManager instance = Singleton<CitizenManager>.instance;
                instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].SetVehicle(citizen, 0, 0u);
                if (success)
                {
                    instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].SetLocationByBuilding(citizen, citizenData.m_targetBuilding);
                }
                if (citizenData.m_targetBuilding != 0 && instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].CurrentLocation == Citizen.Location.Visit)
                {
                    BuildingManager instance2 = Singleton<BuildingManager>.instance;
                    BuildingInfo info = instance2.m_buildings.m_buffer[(int)citizenData.m_targetBuilding].Info;
                    int num = -100;
                    info.m_buildingAI.ModifyMaterialBuffer(citizenData.m_targetBuilding, ref instance2.m_buildings.m_buffer[(int)citizenData.m_targetBuilding], TransferManager.TransferReason.Shopping, ref num);
                    if (info.m_class.m_service == ItemClass.Service.Beautification)
                    {
                        StatisticsManager instance3 = Singleton<StatisticsManager>.instance;
                        StatisticBase statisticBase = instance3.Acquire<StatisticInt32>(StatisticType.ParkVisitCount);
                        statisticBase.Add(1);
                    }
                    ushort eventIndex = instance2.m_buildings.m_buffer[(int)citizenData.m_targetBuilding].m_eventIndex;
                    if (eventIndex != 0)
                    {
                        EventManager instance4 = Singleton<EventManager>.instance;
                        EventInfo info2 = instance4.m_events.m_buffer[(int)eventIndex].Info;
                        info2.m_eventAI.VisitorEnter(eventIndex, ref instance4.m_events.m_buffer[(int)eventIndex], citizenData.m_targetBuilding, citizen);
                    }
                }
            }
            if ((citizenData.m_flags & CitizenInstance.Flags.HangAround) == CitizenInstance.Flags.None || !success)
            {
                this.SetSource(instanceID, ref citizenData, 0);
                this.SetTarget(instanceID, ref citizenData, 0);
                citizenData.Unspawn(instanceID);
            }
        }
    }
}
