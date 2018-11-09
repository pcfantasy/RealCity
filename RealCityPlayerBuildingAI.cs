using ColossalFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCity
{
    public class RealCityPlayerBuildingAI: CommonBuildingAI
    {
        public override int GetResourceRate(ushort buildingID, ref Building data, EconomyManager.Resource resource)
        {
            if (resource == EconomyManager.Resource.Maintenance)
            {
                int num = (int)data.m_productionRate;
                if ((data.m_flags & Building.Flags.Evacuating) != Building.Flags.None)
                {
                    num = 0;
                }
                int budget = this.GetBudget(buildingID, ref data);
                int num2 = this.GetMaintenanceCost() / 100;
                num2 = num * budget / 100 * num2;
                //new added here
                float tempNum = CaculateEmployeeOutcome(buildingID, data);
                //DebugLog.LogToFileOnly("RealCityPlayerBuildingAI GetResourceRate facility " + tempNum.ToString());
                num2 = (int)((float)(num2 / MainDataStore.game_expense_divide) + tempNum * 100f);
                //DebugLog.LogToFileOnly("RealCityPlayerBuildingAI GetResourceRate facility post " + num2.ToString());
                //new added end
                return -num2;
            }
            return base.GetResourceRate(buildingID, ref data, resource);
        }


        public float CaculateEmployeeOutcome(ushort buildingID, Building building)
        {
            float num1 = 0;
            Citizen.BehaviourData behaviour = default(Citizen.BehaviourData);
            int aliveWorkerCount = 0;
            int totalWorkerCount = 0;
            GetWorkBehaviour(buildingID, ref building, ref behaviour, ref aliveWorkerCount, ref totalWorkerCount);
            num1 += behaviour.m_educated0Count * MainDataStore.goverment_education0;
            num1 += behaviour.m_educated1Count * MainDataStore.goverment_education1;
            num1 += behaviour.m_educated2Count * MainDataStore.goverment_education2;
            num1 += behaviour.m_educated3Count * MainDataStore.goverment_education3;
            return num1 / 16f;
        }

        public int GetBudget(ushort buildingID, ref Building buildingData)
        {
            ushort eventIndex = buildingData.m_eventIndex;
            if (eventIndex != 0)
            {
                EventManager instance = Singleton<EventManager>.instance;
                EventInfo info = instance.m_events.m_buffer[(int)eventIndex].Info;
                return info.m_eventAI.GetBudget(eventIndex, ref instance.m_events.m_buffer[(int)eventIndex]);
            }
            return Singleton<EconomyManager>.instance.GetBudget(this.m_info.m_class);
        }
    }
}
