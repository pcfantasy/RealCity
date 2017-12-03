using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RealCity
{
    public class pc_ZoneManager:ZoneManager
    {
        private int CalculateResidentialDemand(ref District districtData)
        {
            int a = (int)(districtData.m_commercialData.m_finalHomeOrWorkCount + districtData.m_industrialData.m_finalHomeOrWorkCount + districtData.m_officeData.m_finalHomeOrWorkCount + districtData.m_playerData.m_finalHomeOrWorkCount);
            int num = (int)(districtData.m_commercialData.m_finalEmptyCount + districtData.m_industrialData.m_finalEmptyCount + districtData.m_officeData.m_finalEmptyCount + districtData.m_playerData.m_finalEmptyCount);
            int finalHomeOrWorkCount = (int)districtData.m_residentialData.m_finalHomeOrWorkCount;
            int finalEmptyCount = (int)districtData.m_residentialData.m_finalEmptyCount;
            int num2 = (int)(districtData.m_educated0Data.m_finalUnemployed + districtData.m_educated1Data.m_finalUnemployed + districtData.m_educated2Data.m_finalUnemployed + districtData.m_educated3Data.m_finalUnemployed);
            int num3 = (int)(districtData.m_educated0Data.m_finalHomeless + districtData.m_educated1Data.m_finalHomeless + districtData.m_educated2Data.m_finalHomeless + districtData.m_educated3Data.m_finalHomeless);
            int num4 = Mathf.Clamp(100 - finalHomeOrWorkCount, 50, 100);
            num4 += Mathf.Clamp((num * 200 - num2 * 200) / Mathf.Max(a, 100), -50, 50);
            num4 += Mathf.Clamp((num3 * 200 - finalEmptyCount * 200) / Mathf.Max(finalHomeOrWorkCount, 100), -50, 50);
            this.m_DemandWrapper.OnCalculateResidentialDemand(ref num4);
            this.OnCalculateResidentialDemand(ref num4);
            return Mathf.Clamp(num4, 0, 100);
        }

        private int CalculateIncomingResidentDemand(ref District districtData)
        {
            int a = (int)(districtData.m_commercialData.m_finalHomeOrWorkCount + districtData.m_industrialData.m_finalHomeOrWorkCount + districtData.m_officeData.m_finalHomeOrWorkCount + districtData.m_playerData.m_finalHomeOrWorkCount);
            int num = (int)(districtData.m_commercialData.m_finalEmptyCount + districtData.m_industrialData.m_finalEmptyCount + districtData.m_officeData.m_finalEmptyCount + districtData.m_playerData.m_finalEmptyCount);
            int finalHomeOrWorkCount = (int)districtData.m_residentialData.m_finalHomeOrWorkCount;
            int finalEmptyCount = (int)districtData.m_residentialData.m_finalEmptyCount;
            int num2 = (int)(districtData.m_educated0Data.m_finalUnemployed + districtData.m_educated1Data.m_finalUnemployed + districtData.m_educated2Data.m_finalUnemployed + districtData.m_educated3Data.m_finalUnemployed);
            int num3 = (int)(districtData.m_educated0Data.m_finalHomeless + districtData.m_educated1Data.m_finalHomeless + districtData.m_educated2Data.m_finalHomeless + districtData.m_educated3Data.m_finalHomeless);
            int num4 = Mathf.Clamp(100 - finalHomeOrWorkCount, 50, 100);
            num4 += Mathf.Clamp((num * 200 - num2 * 200) / Mathf.Max(a, 100), -50, 50);
            num4 += Mathf.Clamp((finalEmptyCount * 200 - num3 * 200) / Mathf.Max(finalHomeOrWorkCount, 100), -50, 50);
            this.m_DemandWrapper.OnCalculateResidentialDemand(ref num4);
            this.OnCalculateResidentialDemand(ref num4);
            return Mathf.Clamp(num4, 0, 100);
        }

        /*private int CalculateCommercialDemand(ref District districtData)
        {
            int num = (int)(districtData.m_commercialData.m_finalHomeOrWorkCount - districtData.m_commercialData.m_finalEmptyCount);
            int num2 = (int)(districtData.m_residentialData.m_finalHomeOrWorkCount - districtData.m_residentialData.m_finalEmptyCount);
            int finalHomeOrWorkCount = (int)districtData.m_visitorData.m_finalHomeOrWorkCount;
            int finalEmptyCount = (int)districtData.m_visitorData.m_finalEmptyCount;
            int num3 = Mathf.Clamp(num2, 0, 50);
            num = num * 10 * 16 / 100;
            num2 = num2 * 20 / 100;
            num3 += Mathf.Clamp((num2 * 200 - num * 200) / Mathf.Max(num, 100), -50, 50);
            num3 += Mathf.Clamp((finalHomeOrWorkCount * 100 - finalEmptyCount * 300) / Mathf.Max(finalHomeOrWorkCount, 100), -50, 50);
            this.m_DemandWrapper.OnCalculateCommercialDemand(ref num3);
            this.OnCalculateCommercialDemand(ref num3);
            return Mathf.Clamp(num3, 0, 100);
        }*/

        private int CalculateWorkplaceDemand(ref District districtData)
        {
            int value = (int)(districtData.m_residentialData.m_finalHomeOrWorkCount - districtData.m_residentialData.m_finalEmptyCount);
            int a = (int)(districtData.m_commercialData.m_finalHomeOrWorkCount + districtData.m_industrialData.m_finalHomeOrWorkCount + districtData.m_officeData.m_finalHomeOrWorkCount + districtData.m_playerData.m_finalHomeOrWorkCount);
            int num = (int)(districtData.m_commercialData.m_finalEmptyCount + districtData.m_industrialData.m_finalEmptyCount + districtData.m_officeData.m_finalEmptyCount + districtData.m_playerData.m_finalEmptyCount);
            int num2 = (int)(districtData.m_educated0Data.m_finalUnemployed + districtData.m_educated1Data.m_finalUnemployed + districtData.m_educated2Data.m_finalUnemployed + districtData.m_educated3Data.m_finalUnemployed);
            int num3 = Mathf.Clamp(value, 0, 50);
            num3 += Mathf.Clamp((num2 * 200 - num * 200) / Mathf.Max(a, 100), -50, 50);
            this.m_DemandWrapper.OnCalculateWorkplaceDemand(ref num3);
            this.OnCalculateWorkplaceDemand(ref num3);
            return Mathf.Clamp(num3, 0, 100);
        }


        public int OnCalculateResidentialDemand(ref int originalDemand)
        {
            float demand_idex = 1f;

            if ((comm_data.citizen_count > 1000) && (comm_data.family_count != 0))
            {
                int family_minus_oilorebuiling = (int)(comm_data.family_count / 10) - pc_PrivateBuildingAI.all_oil_building_profit_final - pc_PrivateBuildingAI.all_ore_building_profit_final - pc_PrivateBuildingAI.all_oil_building_loss_final - pc_PrivateBuildingAI.all_ore_building_loss_final;
                demand_idex = demand_idex * (100 + family_minus_oilorebuiling) / 100;
                if (demand_idex < 0)
                {
                    demand_idex = 0;
                }
                System.Random rand = new System.Random();
                if (demand_idex == 0)
                {
                    if (rand.Next(8) == 0)
                    {
                        demand_idex = 1;
                    }
                }

                originalDemand = (int)(originalDemand * demand_idex);
            }
            else
            {
                //do nothing
            }
            return originalDemand;
        }

        /*public int OnCalculateCommercialDemand(ref int originalDemand)
        {
            if ((pc_PrivateBuildingAI.all_comm_building_loss_final + pc_PrivateBuildingAI.all_comm_building_profit_final) > 0)
            {
                if (pc_PrivateBuildingAI.all_comm_building_loss_final != 0)
                {
                    if ((pc_PrivateBuildingAI.all_comm_building_profit_final / pc_PrivateBuildingAI.all_comm_building_loss_final) >= 20f)
                    {
                        //do nothing
                    }
                    else
                    {
                        //DebugLog.LogToFileOnly("not enough profit commerical building, demand = 0 now");
                        originalDemand = 0;// (int)(((long)originalDemand * (long)pc_PrivateBuildingAI.all_comm_building_profit_final) / (long)pc_PrivateBuildingAI.all_comm_building_loss_final);
                    }
                }
                else
                {
                    if (pc_PrivateBuildingAI.all_comm_building_profit_final != 0)
                    {
                        //do nothing
                    }
                    else
                    {
                        DebugLog.LogToFileOnly("should be wrong, commerial building > 0, no loss and profit num");
                    }
                }
            }
            else
            {
                //do nothing
            }
            return originalDemand;
        }*/

        public int OnCalculateWorkplaceDemand(ref int originalDemand)
        {
            int profit_building_num = 0;
            int loss_building_num = 0;
            profit_building_num += pc_PrivateBuildingAI.all_farmer_building_profit_final;
            profit_building_num += pc_PrivateBuildingAI.all_foresty_building_profit_final;
            profit_building_num += pc_PrivateBuildingAI.all_oil_building_profit_final;
            profit_building_num += pc_PrivateBuildingAI.all_ore_building_profit_final;
            profit_building_num += pc_PrivateBuildingAI.all_industry_building_profit_final;

            loss_building_num += pc_PrivateBuildingAI.all_farmer_building_loss_final;
            loss_building_num += pc_PrivateBuildingAI.all_foresty_building_loss_final;
            loss_building_num += pc_PrivateBuildingAI.all_oil_building_loss_final;
            loss_building_num += pc_PrivateBuildingAI.all_ore_building_loss_final;
            loss_building_num += pc_PrivateBuildingAI.all_industry_building_loss_final;

            if ((profit_building_num + loss_building_num) > 50)
            {
                if (loss_building_num != 0)
                {
                    if ((profit_building_num / loss_building_num) >= 1f)
                    {
                        //do nothing
                    }
                    else
                    {
                        originalDemand = 0;// (int)(((long)originalDemand * (long)profit_building_num) / (long)loss_building_num);
                    }
                }
                else
                {
                    if (profit_building_num != 0)
                    {
                        //do nothing
                    }
                    else
                    {
                        DebugLog.LogToFileOnly("should be wrong, industy building > 0, no loss and profit num");
                    }
                }
            }
            else
            {
                //do nothing
            }
            return originalDemand;
        }

        //public override int OnUpdateDemand(int lastDemand, int nextDemand, int targetDemand)
        //{
        //    return base.OnUpdateDemand(lastDemand, 2147483647, 2147483647);
        //}
    }
}
