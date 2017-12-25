using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RealCity
{
    class pc_VehicleAI: VehicleAI
    {
        // VehicleAI
        protected float CalculateTargetSpeed_1(ushort vehicleID, ref Vehicle data, float speedLimit, float curve)
        {
            float a = 1000f / (1f + curve * 1000f / this.m_info.m_turning) + 2f;
            float b = 8f * speedLimit;


            ushort num = 0;
            if (comm_data.have_toll_station)
            {
                num = RealCity.ThreadingRealCityStatsMod.FindToll(data.GetLastFramePosition(), 32f);
            }


            if (num != 0)
            {
                return 1f;
            }
            else
            {
                return Mathf.Min(Mathf.Min(a, b), this.m_info.m_maxSpeed);
            }
        }
    }
}
