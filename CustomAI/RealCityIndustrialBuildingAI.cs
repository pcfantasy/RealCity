using ColossalFramework;
using ColossalFramework.Math;
using RealCity.Util;
using System;
using UnityEngine;

namespace RealCity.CustomAI
{
    public class RealCityIndustrialBuildingAI
    {
        public delegate TransferManager.TransferReason IndustrialBuildingAIGetIncomingTransferReason(IndustrialBuildingAI IndustrialBuildingAI, ushort buildingID);
        public static IndustrialBuildingAIGetIncomingTransferReason GetIncomingTransferReason;

        public delegate TransferManager.TransferReason IndustrialBuildingAIGetOutgoingTransferReason(IndustrialBuildingAI IndustrialBuildingAI);
        public static IndustrialBuildingAIGetOutgoingTransferReason GetOutgoingTransferReason;

        public delegate TransferManager.TransferReason IndustrialBuildingAIGetSecondaryIncomingTransferReason(IndustrialBuildingAI IndustrialBuildingAI, ushort buildingID);
        public static IndustrialBuildingAIGetSecondaryIncomingTransferReason GetSecondaryIncomingTransferReason;

        public delegate int IndustrialBuildingAIMaxIncomingLoadSize(IndustrialBuildingAI IndustrialBuildingAI);
        public static IndustrialBuildingAIMaxIncomingLoadSize MaxIncomingLoadSize;
        
        public delegate int IndustrialBuildingAIGetConsumptionDivider(IndustrialBuildingAI IndustrialBuildingAI);
        public static IndustrialBuildingAIGetConsumptionDivider GetConsumptionDivider;

        public static void InitDelegate()
        {
            if (GetIncomingTransferReason != null)
                return;
            if (GetOutgoingTransferReason != null)
                return;
            if (GetSecondaryIncomingTransferReason != null)
                return;
            if (MaxIncomingLoadSize != null)
                return;
            if (GetConsumptionDivider != null)
                return;
            GetIncomingTransferReason = FastDelegateFactory.Create<IndustrialBuildingAIGetIncomingTransferReason>(typeof(IndustrialBuildingAI), "GetIncomingTransferReason", instanceMethod: true);
            GetOutgoingTransferReason = FastDelegateFactory.Create<IndustrialBuildingAIGetOutgoingTransferReason>(typeof(IndustrialBuildingAI), "GetOutgoingTransferReason", instanceMethod: true);
            GetSecondaryIncomingTransferReason = FastDelegateFactory.Create<IndustrialBuildingAIGetSecondaryIncomingTransferReason>(typeof(IndustrialBuildingAI), "GetSecondaryIncomingTransferReason", instanceMethod: true);
            MaxIncomingLoadSize = FastDelegateFactory.Create<IndustrialBuildingAIMaxIncomingLoadSize>(typeof(IndustrialBuildingAI), "MaxIncomingLoadSize", instanceMethod: true);
            GetConsumptionDivider = FastDelegateFactory.Create<IndustrialBuildingAIGetConsumptionDivider>(typeof(IndustrialBuildingAI), "GetConsumptionDivider", instanceMethod: true);
        }
    }
}
