using ColossalFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCity
{
    public class RealCityProcessingFacilityAI : IndustryBuildingAI
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
                int budget = base.GetBudget(buildingID, ref data);
                int num2 = this.GetMaintenanceCost() / 100;
                num2 = num * budget / 100 * num2;
                int num3 = num2;
                DistrictManager instance = Singleton<DistrictManager>.instance;
                byte b = instance.GetPark(data.m_position);
                if (b != 0)
                {
                    if (!instance.m_parks.m_buffer[(int)b].IsIndustry)
                    {
                        b = 0;
                    }
                    else if (this.m_industryType == DistrictPark.ParkType.Industry || this.m_industryType != instance.m_parks.m_buffer[(int)b].m_parkType)
                    {
                        b = 0;
                    }
                }
                DistrictPolicies.Park parkPolicies = instance.m_parks.m_buffer[(int)b].m_parkPolicies;
                if ((parkPolicies & DistrictPolicies.Park.ImprovedLogistics) != DistrictPolicies.Park.None)
                {
                    num3 += num2 / 10;
                }
                if ((parkPolicies & DistrictPolicies.Park.AdvancedAutomation) != DistrictPolicies.Park.None)
                {
                    num3 += num2 / 10;
                }
                //new added here
                float tempNum = CaculateEmployeeOutcome(buildingID, data);
                //DebugLog.LogToFileOnly("RealCityProcessingFacilityAI GetResourceRate facility " + tempNum.ToString());
                if (budget < 100)
                {
                    budget = (budget * budget + 99) / 100;
                }
                else if (budget > 150)
                {
                    budget = 125;
                }
                else if (budget > 100)
                {
                    budget -= (100 - budget) * (100 - budget) / 100;
                }
                num3 = (int)((float)(num3 / MainDataStore.game_expense_divide) + tempNum * budget);
                //DebugLog.LogToFileOnly("RealCityProcessingFacilityAI GetResourceRate facility post " + num3.ToString());
                //new added end
                return -num3;
            }
            return base.GetResourceRate(buildingID, ref data, resource);
        }

        public float CaculateEmployeeOutcome(ushort buildingID , Building building)
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
            int allWorkCount = RealCityResidentAI.TotalWorkCount((ushort)buildingID, building, true, false);
            if (totalWorkerCount > allWorkCount)
            {
                //DebugLog.LogToFileOnly("error, find totalWorkCount > allWorkCount building = " + building.Info.m_buildingAI.ToString());
                allWorkCount = RealCityResidentAI.TotalWorkCount((ushort)buildingID, building, true, true);
            }

            if ((aliveWorkerCount == 0) && (allWorkCount != 0))
            {
                num1 = MainDataStore.goverment_education3 * allWorkCount;
            }

            float idex = (totalWorkerCount != 0) ? (allWorkCount / totalWorkerCount) : 1;
            return num1 * idex / 16f;
        }

    }
}
