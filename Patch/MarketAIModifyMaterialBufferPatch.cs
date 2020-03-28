using ColossalFramework;
using Harmony;
using System;
using System.Reflection;
using UnityEngine;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class MarketAIModifyMaterialBufferPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(MarketAI).GetMethod("ModifyMaterialBuffer", BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(TransferManager.TransferReason), typeof(int).MakeByRefType() }, null);
        }

        public static bool Prefix(ref Building data, TransferManager.TransferReason material, ref int amountDelta)
        {
            switch (material)
            {
                case TransferManager.TransferReason.ShoppingB:
                case TransferManager.TransferReason.ShoppingC:
                case TransferManager.TransferReason.ShoppingD:
                case TransferManager.TransferReason.ShoppingE:
                case TransferManager.TransferReason.ShoppingF:
                case TransferManager.TransferReason.ShoppingG:
                case TransferManager.TransferReason.ShoppingH:
                    {
                        int customBuffer2 = data.m_customBuffer2;
                        amountDelta = Mathf.Clamp(amountDelta, -customBuffer2, 0);
                        data.m_customBuffer2 = (ushort)(customBuffer2 + amountDelta);
                        data.m_outgoingProblemTimer = 0;
                        data.m_education1 = (byte)Mathf.Clamp(data.m_education1 + (-amountDelta + 99) / 100, 0, 255);
                        int priceInt = 0;
                        IndustryBuildingGetResourcePricePatch.Prefix(ref priceInt, material, data.Info.m_class.m_service);
                        var m_goodsSellPrice = priceInt / 100;
                        int num = (-amountDelta * m_goodsSellPrice + 50) / 100;
                        if (num != 0)
                        {
                            Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.ResourcePrice, num, data.Info.m_class);
                        }
                        return false;
                    }
                default: return true;
            }
        }
    }
}
