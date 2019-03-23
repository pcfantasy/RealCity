using ColossalFramework;
using RealCity.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCity.CustomAI
{
    public class RealCityCommonBuildingAI: BuildingAI
    {
        public static void CommonBuildingAIReleaseBuildingPostfix(ushort buildingID)
        {
            MainDataStore.building_money[buildingID] = 0;
            MainDataStore.building_buffer2[buildingID] = 0;
            MainDataStore.building_buffer1[buildingID] = 0;
            MainDataStore.building_buffer3[buildingID] = 0;
            MainDataStore.building_buffer4[buildingID] = 0;
            MainDataStore.isBuildingWorkerUpdated[buildingID] = false;
        }
    }
}
