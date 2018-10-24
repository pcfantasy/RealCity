using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RealCity
{
    public class RealCityZoneManager:ZoneManager
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

        private int CalculateCommercialDemand(ref District districtData)
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
        }

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
            if (!MainDataStore.isFoodsGettedFinal|| !MainDataStore.isCoalsGettedFinal || !MainDataStore.isLumbersGettedFinal)
            {
                originalDemand = 0;
            }
            return originalDemand;
        }

        public int OnCalculateCommercialDemand(ref int originalDemand)
        {
            if (!MainDataStore.isCoalsGettedFinal || !MainDataStore.isLumbersGettedFinal)
            {
                originalDemand = 0;
            }
            return originalDemand;
        }

        public int OnCalculateWorkplaceDemand(ref int originalDemand)
        {
            if (!MainDataStore.isCoalsGettedFinal || !MainDataStore.isLumbersGettedFinal)
            {
                originalDemand = 0;
            }
            return originalDemand;
        }
    }
}
