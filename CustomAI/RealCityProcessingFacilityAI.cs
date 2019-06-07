using ColossalFramework;
using RealCity.Util;
using System;

namespace RealCity.CustomAI
{
    public class RealCityProcessingFacilityAI : IndustryBuildingAI
    {
        public override int GetResourceRate(ushort buildingID, ref Building data, EconomyManager.Resource resource)
        {
            if (resource == EconomyManager.Resource.Maintenance)
            {
                int num = data.m_productionRate;
                if ((data.m_flags & Building.Flags.Evacuating) != Building.Flags.None)
                {
                    num = 0;
                }
                int budget = GetBudget(buildingID, ref data);
                int num2 = GetMaintenanceCost() / 100;
                num2 = num * budget / 100 * num2;
                int num3 = num2;
                DistrictManager instance = Singleton<DistrictManager>.instance;
                byte b = instance.GetPark(data.m_position);
                if (b != 0)
                {
                    if (!instance.m_parks.m_buffer[b].IsIndustry)
                    {
                        b = 0;
                    }
                    else if (m_industryType == DistrictPark.ParkType.Industry || m_industryType != instance.m_parks.m_buffer[b].m_parkType)
                    {
                        b = 0;
                    }
                }
                DistrictPolicies.Park parkPolicies = instance.m_parks.m_buffer[b].m_parkPolicies;
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

                num3 = (int)((num3 / MainDataStore.gameExpenseDivide) + tempNum * budget);
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
            int budget = Singleton<EconomyManager>.instance.GetBudget(building.Info.m_class);
            int education0Salary = Math.Max((int)((budget * MainDataStore.govermentEducation0SalaryFixed * RealCityResidentAI.ProcessSalaryLandPriceAdjust(buildingID)) / 100), MainDataStore.govermentEducation0Salary);
            int education1Salary = Math.Max((int)((budget * MainDataStore.govermentEducation1SalaryFixed * RealCityResidentAI.ProcessSalaryLandPriceAdjust(buildingID)) / 100), MainDataStore.govermentEducation1Salary);
            int education2Salary = Math.Max((int)((budget * MainDataStore.govermentEducation2SalaryFixed * RealCityResidentAI.ProcessSalaryLandPriceAdjust(buildingID)) / 100), MainDataStore.govermentEducation2Salary);
            int education3Salary = Math.Max((int)((budget * MainDataStore.govermentEducation3SalaryFixed * RealCityResidentAI.ProcessSalaryLandPriceAdjust(buildingID)) / 100), MainDataStore.govermentEducation3Salary);
            num1 += behaviour.m_educated0Count * education0Salary;
            num1 += behaviour.m_educated1Count * education1Salary;
            num1 += behaviour.m_educated2Count * education2Salary;
            num1 += behaviour.m_educated3Count * education3Salary;
            int allWorkCount = RealCityResidentAI.TotalWorkCount(buildingID, building, true, false);
            if (totalWorkerCount > allWorkCount)
            {
                allWorkCount = RealCityResidentAI.TotalWorkCount(buildingID, building, true, true);
            }

            if ((aliveWorkerCount == 0) && (allWorkCount != 0))
            {
                num1 = education3Salary * allWorkCount;
            }

            float idex = (totalWorkerCount != 0) ? (allWorkCount / totalWorkerCount) : 1;
            return num1 * idex / 16f;
        }

    }
}
