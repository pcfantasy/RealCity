using ColossalFramework;
using Harmony;
using RealCity.CustomAI;
using RealCity.CustomData;
using System;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class IndustrialBuildingAICreateBuildingPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(IndustrialBuildingAI).GetMethod("CreateBuilding", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType()}, null);
        }
        public static void Postfix(ushort buildingID, ref Building data)
        {
            RealCityIndustrialBuildingAI.InitDelegate();
            var material = RealCityIndustrialBuildingAI.GetIncomingTransferReason((IndustrialBuildingAI)(data.Info.m_buildingAI), buildingID);
            float initialMaterialFee = data.m_customBuffer1 * RealCityIndustryBuildingAI.GetResourcePrice(material);
            BuildingData.buildingMoney[buildingID] = -initialMaterialFee;
        }
    }
}
