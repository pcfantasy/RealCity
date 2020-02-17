using ColossalFramework;
using RealCity.UI;
using RealCity.Util;
using System;

namespace RealCity.CustomAI
{
    public class RealCityPlayerBuildingAI
    {
        public static float CaculateEmployeeOutcome(ushort buildingID, Building building)
        {
            float num1 = 0;
            Citizen.BehaviourData behaviour = default(Citizen.BehaviourData);
            int aliveWorkerCount = 0;
            int totalWorkerCount = 0;
            BuildingUI.GetWorkBehaviour(buildingID, ref building, ref behaviour, ref aliveWorkerCount, ref totalWorkerCount);
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
