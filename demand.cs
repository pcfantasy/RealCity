using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICities;

namespace RealCity
{
    public class SuperDemandBase : DemandExtensionBase
    {
        public override int OnCalculateResidentialDemand(int originalDemand)
        {
            if (comm_data.citizen_count > 100)
            {
                if (comm_data.family_weight_stable_low != 0)
                {
                    if ((comm_data.family_weight_stable_high / comm_data.family_weight_stable_low) > 1)
                    {
                        //do nothing
                    } else
                    {
                        originalDemand = (int)(((long)originalDemand * (long)comm_data.family_weight_stable_high) / (long)comm_data.family_weight_stable_low);
                    }
                }
                else
                {
                    if (comm_data.family_weight_stable_high != 0)
                    {
                        //do nothing
                    }
                    else
                    {
                        DebugLog.LogToFileOnly("should be wrong, citizen count > 0, no weight num");
                    }
                }
            }
            else
            {
                //do nothing
            }
            return originalDemand;
        }

        public override int OnCalculateCommercialDemand(int originalDemand)
        {
            if ((pc_PrivateBuildingAI.all_comm_building_loss_final + pc_PrivateBuildingAI.all_comm_building_profit_final) > 10)
            {
                if (pc_PrivateBuildingAI.all_comm_building_loss_final != 0)
                {
                    if ((pc_PrivateBuildingAI.all_comm_building_profit_final / pc_PrivateBuildingAI.all_comm_building_loss_final) > 1)
                    {
                        //do nothing
                    }
                    else
                    {
                        originalDemand = (int)(((long)originalDemand * (long)pc_PrivateBuildingAI.all_comm_building_profit_final) / (long)pc_PrivateBuildingAI.all_comm_building_loss_final);
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
        }

        public override int OnCalculateWorkplaceDemand(int originalDemand)
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

            if ((profit_building_num + loss_building_num) > 10)
            {
                if (loss_building_num != 0)
                {
                    if ((profit_building_num / loss_building_num) > 1)
                    {
                        //do nothing
                    }
                    else
                    {
                        originalDemand = (int)(((long)originalDemand * (long)profit_building_num) / (long)loss_building_num);
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
