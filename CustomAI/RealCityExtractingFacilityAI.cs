using ColossalFramework;
using RealCity.Util;

namespace RealCity.CustomAI
{
    public class RealCityExtractingFacilityAI : ExtractingFacilityAI
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
                // NON-STOCK CODE START
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
                /// NON-STOCK CODE END ///
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
            num1 += behaviour.m_educated0Count * MainDataStore.govermentEducation0Salary;
            num1 += behaviour.m_educated1Count * MainDataStore.govermentEducation1Salary;
            num1 += behaviour.m_educated2Count * MainDataStore.govermentEducation2Salary;
            num1 += behaviour.m_educated3Count * MainDataStore.govermentEducation3Salary;
            int allWorkCount = RealCityResidentAI.TotalWorkCount(buildingID, building, true, false);
            if (totalWorkerCount > allWorkCount)
            {
                allWorkCount = RealCityResidentAI.TotalWorkCount(buildingID, building, true, true);
            }

            if ((aliveWorkerCount == 0) && (allWorkCount != 0))
            {
                num1 = MainDataStore.govermentEducation3Salary * allWorkCount * RealCityResidentAI.ProcessSalaryLandPriceAdjust(buildingID);
            }

            float idex = (totalWorkerCount != 0) ? (allWorkCount / totalWorkerCount) : 1;
            return num1 * idex / 16f;
        }

    }
}
