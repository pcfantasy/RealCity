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
        public static void CustomReleaseBuilding(ushort buildingID)
        {
            MainDataStore.building_money[buildingID] = 0;
            MainDataStore.building_buffer2[buildingID] = 0;
            MainDataStore.building_buffer1[buildingID] = 0;
            MainDataStore.building_buffer3[buildingID] = 0;
            MainDataStore.building_buffer4[buildingID] = 0;
            MainDataStore.isBuildingWorkerUpdated[buildingID] = false;
            TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
            offer.Building = buildingID;
            Singleton<TransferManager>.instance.RemoveOutgoingOffer((TransferManager.TransferReason)110, offer);
            Singleton<TransferManager>.instance.RemoveOutgoingOffer((TransferManager.TransferReason)111, offer);
            Singleton<TransferManager>.instance.RemoveOutgoingOffer((TransferManager.TransferReason)112, offer);
        }
    }
}
