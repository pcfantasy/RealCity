using ColossalFramework;
using RealCity.UI;
using RealCity.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCity.CustomAI
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
                num2 = (int)((float)(num2 / MainDataStore.gameExpenseDivide) + tempNum * budget);
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
            num1 += behaviour.m_educated0Count * MainDataStore.govermentEducation0Salary;
            num1 += behaviour.m_educated1Count * MainDataStore.govermentEducation1Salary;
            num1 += behaviour.m_educated2Count * MainDataStore.govermentEducation2Salary;
            num1 += behaviour.m_educated3Count * MainDataStore.govermentEducation3Salary;
            int allWorkCount = RealCityResidentAI.TotalWorkCount((ushort)buildingID, building, true, false);


            if ((aliveWorkerCount == 0) && (allWorkCount!=0))
            {
                num1 = MainDataStore.govermentEducation3Salary * allWorkCount * RealCityResidentAI.ProcessSalaryLandPriceAdjust(buildingID);
            }

            float idex = (totalWorkerCount != 0) ? (allWorkCount / totalWorkerCount) : 1f;
            if (totalWorkerCount > allWorkCount)
            {
                allWorkCount = RealCityResidentAI.TotalWorkCount((ushort)buildingID, building, true, true);
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
                EventInfo info = instance.m_events.m_buffer[(int)eventIndex].Info;
                return info.m_eventAI.GetBudget(eventIndex, ref instance.m_events.m_buffer[(int)eventIndex]);
            }
            return Singleton<EconomyManager>.instance.GetBudget(this.m_info.m_class);
        }

        public static void PlayerBuildingAISimulationStepPostFix(ushort buildingID, ref Building buildingData, ref Building.Frame frameData)
        {
            ProcessZeroWorker((ushort)buildingID, ref buildingData);
        }

        public static void ProcessZeroWorker(ushort buildingID, ref Building data)
        {
            if (data.m_flags.IsFlagSet(Building.Flags.Completed))
            {
                int aliveWorkCount = 0;
                int totalWorkCount = 0;
                Citizen.BehaviourData behaviour = default(Citizen.BehaviourData);
                BuildingUI.GetWorkBehaviour(buildingID, ref data, ref behaviour, ref aliveWorkCount, ref totalWorkCount);
                System.Random rand = new System.Random();
                int allWorkCount = 0;
                uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
                int num4 = (int)(currentFrameIndex & 4095u);
                if (((num4 >> 4) & 15u) == (buildingID & 15u))
                {
                    allWorkCount = RealCityResidentAI.TotalWorkCount(buildingID, data, true, true);
                }
                else
                {
                    allWorkCount = RealCityResidentAI.TotalWorkCount(buildingID, data, true, false);
                }

                if (totalWorkCount == 0 && allWorkCount != 0)
                {
                    float num1 = (MainDataStore.govermentEducation3Salary / 16) * allWorkCount * RealCityResidentAI.ProcessSalaryLandPriceAdjust(buildingID);
                    Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.Maintenance, (int)(num1 * MainDataStore.gameExpenseDivide), data.Info.m_class);
                }
            }
        }
    }
}
