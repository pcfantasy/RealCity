using ColossalFramework;
using ColossalFramework.Math;
using RealCity.Util;

namespace RealCity.CustomAI
{
    public class RealCityCommercialBuildingAI
    {
        public delegate TransferManager.TransferReason CommercialBuildingAIGetIncomingTransferReason(CommercialBuildingAI CommercialBuildingAI);
        public static CommercialBuildingAIGetIncomingTransferReason GetIncomingTransferReason;

        public delegate int CommercialBuildingAIMaxIncomingLoadSize(CommercialBuildingAI CommercialBuildingAI);
        public static CommercialBuildingAIMaxIncomingLoadSize MaxIncomingLoadSize;

        public delegate void CommercialBuildingAIGetVisitBehaviour(CommercialBuildingAI CommercialBuildingAI, ushort buildingID, ref Building buildingData, ref Citizen.BehaviourData behaviour, ref int aliveCount, ref int totalCount);
        public static CommercialBuildingAIGetVisitBehaviour GetVisitBehaviour;

        public delegate void CommercialBuildingAICalculateGuestVehicles1(CommercialBuildingAI CommercialBuildingAI, ushort buildingID, ref Building data, TransferManager.TransferReason material1, TransferManager.TransferReason material2, ref int count, ref int cargo, ref int capacity, ref int outside);
        public static CommercialBuildingAICalculateGuestVehicles1 CalculateGuestVehicles1;

        public delegate void CommercialBuildingAICalculateGuestVehicles(CommercialBuildingAI CommercialBuildingAI, ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int count, ref int cargo, ref int capacity, ref int outside);
        public static CommercialBuildingAICalculateGuestVehicles CalculateGuestVehicles;

        public static void InitDelegate()
        {
            if (GetIncomingTransferReason != null)
                return;
            if (MaxIncomingLoadSize != null)
                return;
            if (GetVisitBehaviour != null)
                return;
            if (CalculateGuestVehicles != null)
                return;
            if (CalculateGuestVehicles1 != null)
                return;
            GetVisitBehaviour = FastDelegateFactory.Create<CommercialBuildingAIGetVisitBehaviour>(typeof(CommercialBuildingAI), "GetVisitBehaviour", instanceMethod: true);
            GetIncomingTransferReason = FastDelegateFactory.Create<CommercialBuildingAIGetIncomingTransferReason>(typeof(CommercialBuildingAI), "GetIncomingTransferReason", instanceMethod: true);
            MaxIncomingLoadSize = FastDelegateFactory.Create<CommercialBuildingAIMaxIncomingLoadSize>(typeof(CommercialBuildingAI), "MaxIncomingLoadSize", instanceMethod: true);
            CalculateGuestVehicles = FastDelegateFactory.Create<CommercialBuildingAICalculateGuestVehicles>(typeof(CommercialBuildingAI), "CalculateGuestVehicles", instanceMethod: true);
            CalculateGuestVehicles1 = FastDelegateFactory.Create<CommercialBuildingAICalculateGuestVehicles1>(typeof(CommercialBuildingAI), "CalculateGuestVehicles", instanceMethod: true);
        }
    }
}
