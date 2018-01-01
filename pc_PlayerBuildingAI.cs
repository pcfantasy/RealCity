using ColossalFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RealCity
{
    public class pc_PlayerBuildingAI : PlayerBuildingAI
    {
        public override int GetMaintenanceCost()
        {
            int result = this.m_maintenanceCost * 100;
            /*Regex r = new Regex("CargoStationAI");
            Regex p = new Regex("CargoHarborAI");
            Match m = r.Match(this.m_info.m_buildingAI.ToString());
            Match n = p.Match(this.m_info.m_buildingAI.ToString());
            if (m.Success || n.Success)
            {
                result = result / 2;
            }*/

            Singleton<EconomyManager>.instance.m_EconomyWrapper.OnGetMaintenanceCost(ref result, this.m_info.m_class.m_service, this.m_info.m_class.m_subService, this.m_info.m_class.m_level);
            return result;
        }


        public static int GetProductionRate_1(int productionRate, int budget)
        {
             return (int)(productionRate * budget * Math.Sqrt(budget) + 9999) / 1000;
        }
    }
}
