using ColossalFramework;
using RealCity.UI;
using RealCity.Util;
using System;

namespace RealCity.CustomAI
{
    public class RealCityPlayerBuildingAI: CommonBuildingAI
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
                num2 = (int)((num2 / MainDataStore.gameExpenseDivide) + tempNum * budget);
                /// NON-STOCK CODE END ///
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


            if ((aliveWorkerCount == 0) && (allWorkCount!=0))
            {
                num1 = education3Salary * allWorkCount;
            }

            float idex = (totalWorkerCount != 0) ? (allWorkCount / totalWorkerCount) : 1f;
            if (totalWorkerCount > allWorkCount)
            {
                allWorkCount = RealCityResidentAI.TotalWorkCount(buildingID, building, true, true);
                idex = 1f;
            }
            return num1 * idex / 16f;
        }

        public int GetBudget(ushort buildingID, ref Building buildingData)
        {
            ushort eventIndex = buildingData.m_eventIndex;
            if (eventIndex != 0)
            {
                EventManager instance = Singleton<EventManager>.instance;
                EventInfo info = instance.m_events.m_buffer[eventIndex].Info;
                return info.m_eventAI.GetBudget(eventIndex, ref instance.m_events.m_buffer[eventIndex]);
            }
            return Singleton<EconomyManager>.instance.GetBudget(m_info.m_class);
        }

        public static void PlayerBuildingAISimulationStepPostFix(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
        {
            ProcessZeroWorker(buildingID, ref buildingData);
        }

        public static void ProcessZeroWorker(ushort buildingID, ref Building data)
        {
            if (data.m_flags.IsFlagSet(Building.Flags.Completed))
            {
                int aliveWorkCount = 0;
                int totalWorkCount = 0;
                Citizen.BehaviourData behaviour = default(Citizen.BehaviourData);
                BuildingUI.GetWorkBehaviour(buildingID, ref data, ref behaviour, ref aliveWorkCount, ref totalWorkCount);
                Random rand = new Random();
                int allWorkCount = 0;
                uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
                int num4 = (int)(currentFrameIndex & 4095u);
                if (((num4 >> 8) & 15u) == (buildingID & 15u))
                {
                    allWorkCount = RealCityResidentAI.TotalWorkCount(buildingID, data, true, true);
                }
                else
                {
                    allWorkCount = RealCityResidentAI.TotalWorkCount(buildingID, data, true, false);
                }

                if (totalWorkCount == 0 && allWorkCount != 0)
                {
                    int budget = Singleton<EconomyManager>.instance.GetBudget(data.Info.m_class);
                    int education3Salary = Math.Max((int)((budget * MainDataStore.govermentEducation3SalaryFixed * RealCityResidentAI.ProcessSalaryLandPriceAdjust(buildingID)) / 100), MainDataStore.govermentEducation3Salary);
                    float num1 = (education3Salary / 16) * allWorkCount;
                    Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)num1, data.Info.m_class.m_service, data.Info.m_class.m_subService, data.Info.m_class.m_level);
                }
            }
        }
    }
}
