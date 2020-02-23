using ColossalFramework;
using Harmony;
using System;
using System.Reflection;
using UnityEngine;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class TransferManagerStartTransferPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(TransferManager).GetMethod("StartTransfer", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public static bool Prefix(TransferManager.TransferReason material, TransferManager.TransferOffer offerOut, TransferManager.TransferOffer offerIn, int delta)
        {
            bool offerOutActive = offerOut.Active;
            if (offerOutActive && offerOut.Citizen != 0u)
            {
                Array32<Citizen> citizens = Singleton<CitizenManager>.instance.m_citizens;
                uint citizen = offerOut.Citizen;
                CitizenInfo citizenInfo = citizens.m_buffer[citizen].GetCitizenInfo(citizen);
                if (citizenInfo != null)
                {
                    offerIn.Amount = delta;
                    // NON-STOCK CODE START
                    // Remove cotenancy, otherwise we can not calculate family money
                    bool flag = (material == TransferManager.TransferReason.Single0 || material == TransferManager.TransferReason.Single1 || material == TransferManager.TransferReason.Single2 || material == TransferManager.TransferReason.Single3 || material == TransferManager.TransferReason.Single0B || material == TransferManager.TransferReason.Single1B || material == TransferManager.TransferReason.Single2B || material == TransferManager.TransferReason.Single3B);
                    bool flag2 = (citizenInfo.m_citizenAI is ResidentAI) && (Singleton<BuildingManager>.instance.m_buildings.m_buffer[offerIn.Building].Info.m_class.m_service == ItemClass.Service.Residential);
                    if (flag && flag2)
                    {
                        if (material == TransferManager.TransferReason.Single0 || material == TransferManager.TransferReason.Single0B)
                        {
                            material = TransferManager.TransferReason.Family0;
                        }
                        else if (material == TransferManager.TransferReason.Single1 || material == TransferManager.TransferReason.Single1B)
                        {
                            material = TransferManager.TransferReason.Family1;
                        }
                        else if (material == TransferManager.TransferReason.Single2 || material == TransferManager.TransferReason.Single2B)
                        {
                            material = TransferManager.TransferReason.Family2;
                        }
                        else if (material == TransferManager.TransferReason.Single3 || material == TransferManager.TransferReason.Single3B)
                        {
                            material = TransferManager.TransferReason.Family3;
                        }
                        citizenInfo.m_citizenAI.StartTransfer(citizen, ref citizens.m_buffer[citizen], material, offerIn);
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
