using ColossalFramework;
using RealCity.Util;
using System;

namespace RealCity.CustomAI
{
    public class RealCityBuildingAI
    {
        public static void BuildingAIVisitorEnterPostFix(ushort buildingID, ref Building data, uint citizen)
        {
            ProcessTourismIncome(buildingID, ref data, citizen);
        }

        public static void ProcessTourismIncome(ushort buildingID, ref Building data, uint citizen)
        {
            BuildingManager instance2 = Singleton<BuildingManager>.instance;
            CitizenManager instance = Singleton<CitizenManager>.instance;
            //uint citizen = citizenData.m_citizen;
            BuildingInfo info = data.Info;
            ushort homeBuilding = instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_homeBuilding;
            uint homeId = instance.m_citizens.m_buffer[citizen].GetContainingUnit(citizen, instance2.m_buildings.m_buffer[homeBuilding].m_citizenUnits, CitizenUnit.Flags.Home);

            TransferManager.TransferReason tempTransferRreason = TransferManager.TransferReason.Entertainment;
            Random rand = new Random();
            float consumptionIndex = 0f;
            if (info.m_class.m_service == ItemClass.Service.Commercial)
            {
                if ((instance.m_citizens.m_buffer[citizen].m_flags & Citizen.Flags.Tourist) == Citizen.Flags.None)
                {
                    if (tempTransferRreason == TransferManager.TransferReason.Entertainment)
                    {
                        if ((info.m_class.m_subService == ItemClass.SubService.CommercialLeisure))
                        {
                            consumptionIndex = 0.5f;
                        }
                        else if ((info.m_class.m_subService == ItemClass.SubService.CommercialTourist))
                        {
                            consumptionIndex = 0.4f;
                        }
                        else if ((info.m_class.m_subService == ItemClass.SubService.CommercialEco))
                        {
                            consumptionIndex = 0.1f;
                        }
                        else if ((info.m_class.m_subService == ItemClass.SubService.CommercialHigh))
                        {
                            consumptionIndex = 0.3f;
                        }
                        else
                        {
                            consumptionIndex = 0.2f;
                        }
                    }

                    int consumptionMoney = -(int)(consumptionIndex * MainDataStore.citizenMoney[citizen]);

                    if (consumptionMoney < 0)
                    {
                        info.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, tempTransferRreason, ref consumptionMoney);
                    }
                    else
                    {
                        consumptionMoney = 0;
                    }
                    int num = -100;
                    info.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, TransferManager.TransferReason.Shopping, ref num);
                    MainDataStore.citizenMoney[citizen] = (MainDataStore.citizenMoney[citizen] + consumptionMoney + num * RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Shopping));
                }
                else if ((instance.m_citizens.m_buffer[citizen].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None)
                {
                    int consumptionMoney = rand.Next(400);
                    if (tempTransferRreason == TransferManager.TransferReason.Entertainment)
                    {
                        if (instance.m_citizens.m_buffer[citizen].WealthLevel == Citizen.Wealth.High)
                        {
                            consumptionMoney = consumptionMoney << 4;
                        }
                        if (instance.m_citizens.m_buffer[citizen].WealthLevel == Citizen.Wealth.Medium)
                        {
                            consumptionMoney = consumptionMoney << 2;
                        }
                    }

                    consumptionMoney = -(consumptionMoney);
                    if ((consumptionMoney == -200 || consumptionMoney == -50))
                    {
                        consumptionMoney = consumptionMoney + 1;
                    }
                    info.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, tempTransferRreason, ref consumptionMoney);
                    consumptionMoney = -100;
                    info.m_buildingAI.ModifyMaterialBuffer(buildingID, ref data, TransferManager.TransferReason.Shopping, ref consumptionMoney);
                }
            }

            if (info.m_class.m_service == ItemClass.Service.Monument)
            {
                if ((instance.m_citizens.m_buffer[citizen].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None)
                {
                    int tourism_fee = rand.Next(100) + 1;
                    if (instance.m_citizens.m_buffer[citizen].WealthLevel == Citizen.Wealth.High)
                    {
                        tourism_fee = tourism_fee << 4;
                    }
                    if (instance.m_citizens.m_buffer[citizen].WealthLevel == Citizen.Wealth.Medium)
                    {
                        tourism_fee = tourism_fee << 2;
                    }

                    Singleton<EconomyManager>.instance.AddPrivateIncome(tourism_fee, ItemClass.Service.Commercial, ItemClass.SubService.CommercialTourist, ItemClass.Level.Level1, 113);
                }
                else
                {
                    int tourism_fee = (int)(0.4f * MainDataStore.citizenMoney[citizen]);
                    if (tourism_fee > 0)
                    {
                        MainDataStore.citizenMoney[citizen] = (MainDataStore.citizenMoney[citizen] - tourism_fee);
                        Singleton<EconomyManager>.instance.AddPrivateIncome(tourism_fee, ItemClass.Service.Commercial, ItemClass.SubService.CommercialTourist, ItemClass.Level.Level1, 114);
                    }
                }
            }
        }
    }
}
