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

        public static void InitDelegate()
        {
            if (GetIncomingTransferReason != null)
                return;
            if (MaxIncomingLoadSize != null)
                return;
            if (GetVisitBehaviour != null)
                return;
            GetVisitBehaviour = FastDelegateFactory.Create<CommercialBuildingAIGetVisitBehaviour>(typeof(CommercialBuildingAI), "GetVisitBehaviour", instanceMethod: true);
            GetIncomingTransferReason = FastDelegateFactory.Create<CommercialBuildingAIGetIncomingTransferReason>(typeof(CommercialBuildingAI), "GetIncomingTransferReason", instanceMethod: true);
            MaxIncomingLoadSize = FastDelegateFactory.Create<CommercialBuildingAIMaxIncomingLoadSize>(typeof(CommercialBuildingAI), "MaxIncomingLoadSize", instanceMethod: true);
        }
    }
}
