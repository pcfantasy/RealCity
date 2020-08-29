using ColossalFramework;
using ColossalFramework.Math;
using RealCity.Util;
using System;
using UnityEngine;

namespace RealCity.CustomAI
{
    public class RealCityCommonBuildingAI
    {
        public delegate void CommonBuildingAICalculateGuestVehicles1(CommonBuildingAI CommonBuildingAI, ushort buildingID, ref Building data, TransferManager.TransferReason material1, TransferManager.TransferReason material2, ref int count, ref int cargo, ref int capacity, ref int outside);
        public static CommonBuildingAICalculateGuestVehicles1 CalculateGuestVehicles1;

        public delegate void CommonBuildingAICalculateGuestVehicles(CommonBuildingAI CommonBuildingAI, ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int count, ref int cargo, ref int capacity, ref int outside);
        public static CommonBuildingAICalculateGuestVehicles CalculateGuestVehicles;

        public delegate void CommonBuildingAICalculateOwnVehicles(CommonBuildingAI CommonBuildingAI, ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int count, ref int cargo, ref int capacity, ref int outside);
        public static CommonBuildingAICalculateOwnVehicles CalculateOwnVehicles;

        public delegate void CommonBuildingAIGetWorkBehaviour(CommonBuildingAI CommonBuildingAI, ushort buildingID, ref Building buildingData, ref Citizen.BehaviourData behaviour, ref int aliveCount, ref int totalCount);
        public static CommonBuildingAIGetWorkBehaviour GetWorkBehaviour;

        public static void InitDelegate() {
            if (CalculateOwnVehicles != null)
                return;
            if (GetWorkBehaviour != null)
                return;
            if (CalculateGuestVehicles != null)
                return;
            if (CalculateGuestVehicles1 != null)
                return;
            CalculateOwnVehicles = FastDelegateFactory.Create<CommonBuildingAICalculateOwnVehicles>(typeof(CommonBuildingAI), "CalculateOwnVehicles", instanceMethod: true);
            GetWorkBehaviour = FastDelegateFactory.Create<CommonBuildingAIGetWorkBehaviour>(typeof(CommonBuildingAI), "GetWorkBehaviour", instanceMethod: true);
            CalculateGuestVehicles = FastDelegateFactory.Create<CommonBuildingAICalculateGuestVehicles>(typeof(CommonBuildingAI), "CalculateGuestVehicles", instanceMethod: true);
            CalculateGuestVehicles1 = FastDelegateFactory.Create<CommonBuildingAICalculateGuestVehicles1>(typeof(CommonBuildingAI), "CalculateGuestVehicles", instanceMethod: true);
        }
    }
}