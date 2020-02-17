using ColossalFramework;

namespace RealCity.CustomAI
{
    public class RealCityIndustrialExtractorAI : PrivateBuildingAI
    {
        public static TransferManager.TransferReason GetOutgoingTransferReason(ushort buildingID)
        {
            switch (Singleton<BuildingManager>.instance.m_buildings.m_buffer[buildingID].Info.m_class.m_subService)
            {
                case ItemClass.SubService.IndustrialForestry:
                    return TransferManager.TransferReason.Logs;
                case ItemClass.SubService.IndustrialFarming:
                    return TransferManager.TransferReason.Grain;
                case ItemClass.SubService.IndustrialOil:
                    return TransferManager.TransferReason.Oil;
                case ItemClass.SubService.IndustrialOre:
                    return TransferManager.TransferReason.Ore;
                default:
                    return TransferManager.TransferReason.None;
            }
        }

    }
}

