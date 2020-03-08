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

        public delegate void IndustrialBuildingAICalculateGuestVehicles1(IndustrialBuildingAI IndustrialBuildingAI, ushort buildingID, ref Building data, TransferManager.TransferReason material1, TransferManager.TransferReason material2, ref int count, ref int cargo, ref int capacity, ref int outside);
        public static IndustrialBuildingAICalculateGuestVehicles1 CalculateGuestVehicles1;

        public delegate void IndustrialBuildingAICalculateGuestVehicles(IndustrialBuildingAI IndustrialBuildingAI, ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int count, ref int cargo, ref int capacity, ref int outside);
        public static IndustrialBuildingAICalculateGuestVehicles CalculateGuestVehicles;

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
            if (CalculateGuestVehicles != null)
                return;
            if (CalculateGuestVehicles1 != null)
                return;
            GetIncomingTransferReason = FastDelegateFactory.Create<IndustrialBuildingAIGetIncomingTransferReason>(typeof(IndustrialBuildingAI), "GetIncomingTransferReason", instanceMethod: true);
            GetOutgoingTransferReason = FastDelegateFactory.Create<IndustrialBuildingAIGetOutgoingTransferReason>(typeof(IndustrialBuildingAI), "GetOutgoingTransferReason", instanceMethod: true);
            GetSecondaryIncomingTransferReason = FastDelegateFactory.Create<IndustrialBuildingAIGetSecondaryIncomingTransferReason>(typeof(IndustrialBuildingAI), "GetSecondaryIncomingTransferReason", instanceMethod: true);
            MaxIncomingLoadSize = FastDelegateFactory.Create<IndustrialBuildingAIMaxIncomingLoadSize>(typeof(IndustrialBuildingAI), "MaxIncomingLoadSize", instanceMethod: true);
            GetConsumptionDivider = FastDelegateFactory.Create<IndustrialBuildingAIGetConsumptionDivider>(typeof(IndustrialBuildingAI), "GetConsumptionDivider", instanceMethod: true);
            CalculateGuestVehicles = FastDelegateFactory.Create<IndustrialBuildingAICalculateGuestVehicles>(typeof(IndustrialBuildingAI), "CalculateGuestVehicles", instanceMethod: true);
            CalculateGuestVehicles1 = FastDelegateFactory.Create<IndustrialBuildingAICalculateGuestVehicles1>(typeof(IndustrialBuildingAI), "CalculateGuestVehicles", instanceMethod: true);
        }
    }
}
