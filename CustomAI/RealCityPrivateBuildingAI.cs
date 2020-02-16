using ColossalFramework.Math;
using RealCity.Util;
using RealCity.UI;

namespace RealCity.CustomAI
{
    public  class RealCityPrivateBuildingAI
    {
        public static ushort allBuildings = 0;
        public static uint preBuidlingId = 0;

        public static ushort allOfficeLevel1BuildingCount = 0;
        public static ushort allOfficeLevel2BuildingCount = 0;
        public static ushort allOfficeLevel3BuildingCount = 0;
        public static ushort allOfficeHighTechBuildingCount = 0;

        public static ushort allOfficeLevel1BuildingCountFinal = 0;
        public static ushort allOfficeLevel2BuildingCountFinal = 0;
        public static ushort allOfficeLevel3BuildingCountFinal = 0;
        public static ushort allOfficeHighTechBuildingCountFinal = 0;
        public static long profitBuildingMoney = 0;
        public static long profitBuildingMoneyFinal = 0;
        public static ushort profitBuildingCount = 0;
        public static ushort profitBuildingCountFinal = 0;
        public static ushort allBuildingsFinal = 0;

        public static byte[] saveData = new byte[44];

        public static void Load()
        {
            int i = 0;
            preBuidlingId = SaveAndRestore.load_uint(ref i, saveData);
            allBuildings = SaveAndRestore.load_ushort(ref i, saveData);
            allBuildingsFinal = SaveAndRestore.load_ushort(ref i, saveData);
            allOfficeLevel1BuildingCount = SaveAndRestore.load_ushort(ref i, saveData);
            allOfficeLevel2BuildingCount = SaveAndRestore.load_ushort(ref i, saveData);
            allOfficeLevel3BuildingCount = SaveAndRestore.load_ushort(ref i, saveData);
            allOfficeHighTechBuildingCount = SaveAndRestore.load_ushort(ref i, saveData);
            allOfficeLevel1BuildingCountFinal = SaveAndRestore.load_ushort(ref i, saveData);
            allOfficeLevel2BuildingCountFinal = SaveAndRestore.load_ushort(ref i, saveData);
            allOfficeLevel3BuildingCountFinal = SaveAndRestore.load_ushort(ref i, saveData);
            allOfficeHighTechBuildingCountFinal = SaveAndRestore.load_ushort(ref i, saveData);
            profitBuildingMoney = SaveAndRestore.load_long(ref i, saveData);
            profitBuildingMoneyFinal = SaveAndRestore.load_long(ref i, saveData);
            profitBuildingCount = SaveAndRestore.load_ushort(ref i, saveData);
            profitBuildingCountFinal = SaveAndRestore.load_ushort(ref i, saveData);
            DebugLog.LogToFileOnly("saveData in private building is " + i.ToString());
        }

        public static void Save()
        {
            int i = 0;

            //12*2 + 7*4 = 52
            SaveAndRestore.save_uint(ref i, preBuidlingId, ref saveData);

            //20 + 20
            SaveAndRestore.save_ushort(ref i, allBuildings, ref saveData);
            SaveAndRestore.save_ushort(ref i, allBuildingsFinal, ref saveData);
            SaveAndRestore.save_ushort(ref i, allOfficeLevel1BuildingCount, ref saveData);
            SaveAndRestore.save_ushort(ref i, allOfficeLevel2BuildingCount, ref saveData);
            SaveAndRestore.save_ushort(ref i, allOfficeLevel3BuildingCount, ref saveData);
            SaveAndRestore.save_ushort(ref i, allOfficeHighTechBuildingCount, ref saveData);
            SaveAndRestore.save_ushort(ref i, allOfficeLevel1BuildingCountFinal, ref saveData);
            SaveAndRestore.save_ushort(ref i, allOfficeLevel2BuildingCountFinal, ref saveData);
            SaveAndRestore.save_ushort(ref i, allOfficeLevel3BuildingCountFinal, ref saveData);
            SaveAndRestore.save_ushort(ref i, allOfficeHighTechBuildingCountFinal, ref saveData);

            SaveAndRestore.save_long(ref i, profitBuildingMoney, ref saveData);
            SaveAndRestore.save_long(ref i, profitBuildingMoneyFinal, ref saveData);
            SaveAndRestore.save_ushort(ref i, profitBuildingCount, ref saveData);
            SaveAndRestore.save_ushort(ref i, profitBuildingCountFinal, ref saveData);
        }

        public static string GetProductionType(bool isSelling, ushort buildingID, Building data)
        {
            string material = "";
            if (!isSelling)
            {
                if (data.Info.m_buildingAI is IndustrialExtractorAI)
                {
                }
                else
                {
                    switch (data.Info.m_class.m_subService)
                    {
                        case ItemClass.SubService.CommercialHigh:
                        case ItemClass.SubService.CommercialLow:
                        case ItemClass.SubService.CommercialEco:
                        case ItemClass.SubService.CommercialLeisure:
                        case ItemClass.SubService.CommercialTourist:
                            CommercialBuildingAI commercialBuildingAI = data.Info.m_buildingAI as CommercialBuildingAI;
                            switch (commercialBuildingAI.m_incomingResource)
                            {
                                case TransferManager.TransferReason.Goods:
                                    material = Localization.Get("PREGOODS") + Localization.Get("LUXURY_PRODUCTS"); break;
                                case TransferManager.TransferReason.Food:
                                    material = Localization.Get("FOOD") + Localization.Get("LUXURY_PRODUCTS"); break;
                                case TransferManager.TransferReason.Petrol:
                                    material = Localization.Get("PETROL"); break;
                                case TransferManager.TransferReason.Lumber:
                                    material = Localization.Get("LUMBER"); break;
                                case TransferManager.TransferReason.Logs:
                                    material = Localization.Get("LOG"); break;
                                case TransferManager.TransferReason.Oil:
                                    material = Localization.Get("OIL"); break;
                                case TransferManager.TransferReason.Ore:
                                    material = Localization.Get("ORE"); break;
                                case TransferManager.TransferReason.Grain:
                                    material = Localization.Get("GRAIN_MEAT"); break;
                                default: break;
                            }
                            break;
                        case ItemClass.SubService.IndustrialForestry:
                        case ItemClass.SubService.IndustrialFarming:
                        case ItemClass.SubService.IndustrialOil:
                        case ItemClass.SubService.IndustrialOre:
                            TransferManager.TransferReason tempReason2 = RealCityIndustrialBuildingAI.GetIncomingTransferReason(buildingID);
                            switch (tempReason2)
                            {
                                case TransferManager.TransferReason.Grain:
                                    material = Localization.Get("GRAIN_MEAT"); break;
                                case TransferManager.TransferReason.Logs:
                                    material = Localization.Get("LOG"); break;
                                case TransferManager.TransferReason.Ore:
                                    material = Localization.Get("ORE"); break;
                                case TransferManager.TransferReason.Oil:
                                    material = Localization.Get("OIL"); break;
                                default: break;
                            }
                            break;
                        case ItemClass.SubService.IndustrialGeneric:
                            TransferManager.TransferReason tempReason = RealCityIndustrialBuildingAI.GetIncomingTransferReason(buildingID);
                            TransferManager.TransferReason tempReason1 = RealCityIndustrialBuildingAI.GetSecondaryIncomingTransferReason(buildingID);
                            switch (tempReason)
                            {
                                case TransferManager.TransferReason.Food:
                                    material = Localization.Get("FOOD"); break;
                                case TransferManager.TransferReason.Lumber:
                                    material = Localization.Get("LUMBER"); break;
                                case TransferManager.TransferReason.Petrol:
                                    material = Localization.Get("PETROL"); break;
                                case TransferManager.TransferReason.Coal:
                                    material = Localization.Get("COAL"); break;
                                default: break;
                            }
                            switch (tempReason1)
                            {
                                case TransferManager.TransferReason.AnimalProducts:
                                    material += Localization.Get("ANIMALPRODUCTS"); break;
                                case TransferManager.TransferReason.Flours:
                                    material += Localization.Get("FLOURS"); break;
                                case TransferManager.TransferReason.Paper:
                                    material += Localization.Get("PAPER"); break;
                                case TransferManager.TransferReason.PlanedTimber:
                                    material += Localization.Get("PLANEDTIMBER"); break;
                                case TransferManager.TransferReason.Petroleum:
                                    material += Localization.Get("PETROLEUM"); break;
                                case TransferManager.TransferReason.Plastics:
                                    material += Localization.Get("PLASTICS"); break;
                                case TransferManager.TransferReason.Glass:
                                    material += Localization.Get("GLASS"); break;
                                case TransferManager.TransferReason.Metals:
                                    material += Localization.Get("METALS"); break;
                                default: break;
                            }
                            break;
                        default:
                            material = ""; break;
                    }
                }
            }
            else
            {
                if (data.Info.m_buildingAI is IndustrialExtractorAI)
                {
                    switch (data.Info.m_class.m_subService)
                    {
                        case ItemClass.SubService.IndustrialForestry:
                            material = Localization.Get("LOG"); break;
                        case ItemClass.SubService.IndustrialFarming:
                            material = Localization.Get("GRAIN_MEAT"); break;
                        case ItemClass.SubService.IndustrialOil:
                            material = Localization.Get("OIL"); break;
                        case ItemClass.SubService.IndustrialOre:
                            material = Localization.Get("ORE"); break;
                        default:
                            material = ""; break;
                    }
                }
                else
                {
                    switch (data.Info.m_class.m_subService)
                    {
                        case ItemClass.SubService.IndustrialForestry:
                            material = Localization.Get("LUMBER"); break;
                        case ItemClass.SubService.IndustrialFarming:
                            material = Localization.Get("FOOD"); break;
                        case ItemClass.SubService.IndustrialOil:
                            material = Localization.Get("PETROL"); break;
                        case ItemClass.SubService.IndustrialOre:
                            material = Localization.Get("COAL"); break;
                        case ItemClass.SubService.IndustrialGeneric:
                            material = Localization.Get("PREGOODS"); break;
                        case ItemClass.SubService.CommercialHigh:
                        case ItemClass.SubService.CommercialLow:
                        case ItemClass.SubService.CommercialEco:
                        case ItemClass.SubService.CommercialLeisure:
                        case ItemClass.SubService.CommercialTourist:
                            material = Localization.Get("GOODS"); break;
                        default:
                            material = ""; break;
                    }
                }
            }
            return material;
        }

        public static float GetPrice(bool isSelling, ushort buildingID, Building data)
        {
            float price = 0f;
            if (!isSelling)
            {
                if (!(data.Info.m_buildingAI is IndustrialExtractorAI))
                {
                    switch (data.Info.m_class.m_subService)
                    {
                        case ItemClass.SubService.CommercialHigh:
                        case ItemClass.SubService.CommercialLow:
                        case ItemClass.SubService.CommercialEco:
                        case ItemClass.SubService.CommercialLeisure:
                        case ItemClass.SubService.CommercialTourist:
                            CommercialBuildingAI commercialBuildingAI = data.Info.m_buildingAI as CommercialBuildingAI;
                            if (commercialBuildingAI.m_incomingResource == TransferManager.TransferReason.Food || commercialBuildingAI.m_incomingResource == TransferManager.TransferReason.Goods)
                            {
                                price = (3f * RealCityIndustryBuildingAI.GetResourcePrice(commercialBuildingAI.m_incomingResource) + (RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.LuxuryProducts))) / 4f;
                            }
                            else
                            {
                                price = RealCityIndustryBuildingAI.GetResourcePrice(commercialBuildingAI.m_incomingResource);
                            }
                            break;
                        case ItemClass.SubService.IndustrialForestry:
                        case ItemClass.SubService.IndustrialFarming:
                        case ItemClass.SubService.IndustrialOil:
                        case ItemClass.SubService.IndustrialOre:
                            TransferManager.TransferReason tempReason = RealCityIndustrialBuildingAI.GetIncomingTransferReason(buildingID);
                            price = RealCityIndustryBuildingAI.GetResourcePrice(tempReason);
                            break;
                        case ItemClass.SubService.IndustrialGeneric:
                            TransferManager.TransferReason tempReason1 = RealCityIndustrialBuildingAI.GetIncomingTransferReason(buildingID);
                            TransferManager.TransferReason tempReason2 = RealCityIndustrialBuildingAI.GetSecondaryIncomingTransferReason(buildingID);
                            price = (3f * RealCityIndustryBuildingAI.GetResourcePrice(tempReason1) + (RealCityIndustryBuildingAI.GetResourcePrice(tempReason2))) / 4f;
                            break;
                        default:
                            price = 0; break;
                    }
                }
            }
            else
            {
                if (data.Info.m_buildingAI is IndustrialExtractorAI)
                {
                    switch (data.Info.m_class.m_subService)
                    {
                        case ItemClass.SubService.IndustrialForestry:
                            price = RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Logs); break;
                        case ItemClass.SubService.IndustrialFarming:
                            price = RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Grain); break;
                        case ItemClass.SubService.IndustrialOil:
                            price = RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Oil); break;
                        case ItemClass.SubService.IndustrialOre:
                            price = RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Ore); break;
                        default:
                            price = 0; break;
                    }
                }
                else
                {
                    switch (data.Info.m_class.m_subService)
                    {
                        case ItemClass.SubService.IndustrialForestry:
                            price = RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Lumber); break;
                        case ItemClass.SubService.IndustrialFarming:
                            price = RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Food); break;
                        case ItemClass.SubService.IndustrialOil:
                            price = RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Petrol); break;
                        case ItemClass.SubService.IndustrialOre:
                            price = RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Coal); break;
                        case ItemClass.SubService.IndustrialGeneric:
                            price = RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Goods); break;
                        case ItemClass.SubService.CommercialHigh:
                        case ItemClass.SubService.CommercialLow:
                        case ItemClass.SubService.CommercialEco:
                        case ItemClass.SubService.CommercialLeisure:
                        case ItemClass.SubService.CommercialTourist:
                            price = RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Shopping); break;
                        default:
                            price = RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.None); break;
                    }
                }
            }
            return price;
        }

        public static int GetTaxRate(Building data, ushort buildingID)
        {
            if (data.Info.m_class.m_service == ItemClass.Service.Commercial)
            {
                return Politics.commericalTax;
            }
            else if (data.Info.m_class.m_service == ItemClass.Service.Industrial)
            {
                if (data.Info.m_buildingAI is IndustrialExtractorAI)
                {
                    return Politics.industryTax + 60 ;
                }
                else
                {
                    return Politics.industryTax;
                }
            }

            return 0;
        }

        public static float GetComsumptionDivider(Building data, ushort buildingID)
        {
            if (data.Info.m_class.m_service == ItemClass.Service.Office)
            {
                return 0f;
            }

            Citizen.BehaviourData behaviourData = default(Citizen.BehaviourData);
            int aliveWorkerCount = 0;
            int totalWorkerCount = 0;
            BuildingUI.GetWorkBehaviour(buildingID, ref data, ref behaviourData, ref aliveWorkerCount, ref totalWorkerCount);
            float finalIdex = aliveWorkerCount / 10f;

            if (finalIdex < 1f)
            {
                finalIdex = 1f;
            }

            if (data.Info.m_class.m_subService == ItemClass.SubService.IndustrialGeneric)
            {
                Randomizer randomizer = new Randomizer(buildingID);
                int temp = randomizer.Int32(4u);
                //petrol related
                if (temp == 2)
                {
                    finalIdex = finalIdex * 1.5f / 4f;
                } else if (temp == 3)
                {
                    finalIdex = finalIdex * 1.25f / 4f;
                }
                else
                {
                    finalIdex = finalIdex / 4f;
                }
            }

            return finalIdex;
        }
    }
}
