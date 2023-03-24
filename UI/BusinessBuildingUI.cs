using System;
using ColossalFramework.UI;

using System.Text;
using RealCity.Util;
using UnityEngine;
using ColossalFramework.Math;
using RealCity.CustomAI;
using RealCity.CustomData;
using ColossalFramework;

namespace RealCity.UI
{
    class BusinessBuildingUI
    {
        private UILabel buildingMoney;
        private UILabel buildingIncomeBuffer;
        private UILabel buildingOutgoingBuffer;
        private UILabel employFee;
        private UILabel landRent;
        private UILabel buyPrice;
        private UILabel sellPrice;
        private UILabel comsuptionDivide;
        private UILabel sellTax;
        private UILabel profit;
        private UILabel usedcar;
        private static readonly float SPACING = 15f;
        private static readonly float SPACING22 = 22f;

        public BusinessBuildingUI(UIPanel parent)
        {
            buildingMoney = parent.AddUIComponent<UILabel>();
            buildingIncomeBuffer = parent.AddUIComponent<UILabel>();
            buildingOutgoingBuffer = parent.AddUIComponent<UILabel>();
            employFee = parent.AddUIComponent<UILabel>();
            landRent = parent.AddUIComponent<UILabel>();
            buyPrice = parent.AddUIComponent<UILabel>();
            sellPrice = parent.AddUIComponent<UILabel>();
            comsuptionDivide = parent.AddUIComponent<UILabel>();
            sellTax = parent.AddUIComponent<UILabel>();
            profit = parent.AddUIComponent<UILabel>();
            usedcar = parent.AddUIComponent<UILabel>();

            buildingMoney.text = Localization.Get("BUILDING_MONEY");
            buildingMoney.relativePosition = new Vector3(SPACING, 50f);
            buildingMoney.autoSize = true;

            buildingIncomeBuffer.text = Localization.Get("MATERIAL_BUFFER");
            buildingIncomeBuffer.relativePosition = new Vector3(SPACING, buildingMoney.relativePosition.y + SPACING22);
            buildingIncomeBuffer.autoSize = true;

            buildingOutgoingBuffer.text = Localization.Get("PRODUCTION_BUFFER");
            buildingOutgoingBuffer.relativePosition = new Vector3(SPACING, buildingIncomeBuffer.relativePosition.y + SPACING22);
            buildingOutgoingBuffer.autoSize = true;

            employFee.text = Localization.Get("AVERAGE_EMPLOYFEE");
            employFee.relativePosition = new Vector3(SPACING, buildingOutgoingBuffer.relativePosition.y + SPACING22);
            employFee.autoSize = true;

            landRent.text = Localization.Get("BUILDING_LANDRENT");
            landRent.relativePosition = new Vector3(SPACING, employFee.relativePosition.y + SPACING22);
            landRent.autoSize = true;

            buyPrice.text = Localization.Get("BUY_PRICE");
            buyPrice.relativePosition = new Vector3(SPACING, landRent.relativePosition.y + SPACING22);
            buyPrice.autoSize = true;

            sellPrice.text = Localization.Get("SELL_PRICE");
            sellPrice.relativePosition = new Vector3(SPACING, buyPrice.relativePosition.y + SPACING22);
            sellPrice.autoSize = true;

            comsuptionDivide.text = Localization.Get("MATERIAL_DIV_PRODUCTION");
            comsuptionDivide.relativePosition = new Vector3(SPACING, sellPrice.relativePosition.y + SPACING22);
            comsuptionDivide.autoSize = true;

            sellTax.text = Localization.Get("SELL_TAX");
            sellTax.relativePosition = new Vector3(SPACING, comsuptionDivide.relativePosition.y + SPACING22);
            sellTax.autoSize = true;

            profit.text = Localization.Get("PROFIT");
            profit.relativePosition = new Vector3(SPACING, sellTax.relativePosition.y + SPACING22);
            profit.autoSize = true;

            usedcar.text = Localization.Get("CAR_USED");
            usedcar.relativePosition = new Vector3(SPACING, profit.relativePosition.y + SPACING22);
            usedcar.autoSize = true;
        }


        public void Hide()
        {
            buildingMoney.Hide();
            buildingIncomeBuffer.Hide();
            buildingOutgoingBuffer.Hide();
            employFee.Hide();
            landRent.Hide();
            buyPrice.Hide();
            sellPrice.Hide();
            comsuptionDivide.Hide();
            sellTax.Hide();
            profit.Hide();
            usedcar.Hide();
        }

        public void Show()
        {
            buildingMoney.Show();
            buildingIncomeBuffer.Show();
            buildingOutgoingBuffer.Show();
            employFee.Show();
            landRent.Show();
            buyPrice.Show();
            sellPrice.Show();
            comsuptionDivide.Show();
            sellTax.Show();
            profit.Show();
            usedcar.Show();
        }

        public void Update(Building buildingData)
        {
            float averageEmployeeFee = CaculateEmployeeOutcome(buildingData, out int totalWorkerCount);
            int landRentFee = CaculateLandFee(buildingData, BuildingData.lastBuildingID);
            string incomeType = RealCityPrivateBuildingAI.GetProductionType(false, BuildingData.lastBuildingID, buildingData);
            string outgoingType = RealCityPrivateBuildingAI.GetProductionType(true, BuildingData.lastBuildingID, buildingData);
            float incomePrice = RealCityPrivateBuildingAI.GetPrice(false, BuildingData.lastBuildingID, buildingData);
            float outgoingPrice = RealCityPrivateBuildingAI.GetPrice(true, BuildingData.lastBuildingID, buildingData);
            buildingMoney.text = string.Format(Localization.Get("BUILDING_MONEY") + " [{0}]", BuildingData.buildingMoney[BuildingData.lastBuildingID]);
            buildingIncomeBuffer.text = string.Format(Localization.Get("MATERIAL_BUFFER") + " [{0}]" + " " + incomeType, buildingData.m_customBuffer1);
            buildingOutgoingBuffer.text = string.Format(Localization.Get("PRODUCTION_BUFFER") + " [{0}]" + " " + outgoingType, buildingData.m_customBuffer2);
            employFee.text = Localization.Get("AVERAGE_EMPLOYFEE") + " " + averageEmployeeFee.ToString() + " " + Localization.Get("PROFIT_SHARING");
            landRent.text = string.Format(Localization.Get("BUILDING_LANDRENT") + " [{0:N2}]", landRentFee);
            buyPrice.text = string.Format(Localization.Get("BUY_PRICE") + " " + incomeType + "[{0:N2}]", incomePrice);
            sellPrice.text = string.Format(Localization.Get("SELL_PRICE") + " " + outgoingType + " [{0:N2}]", outgoingPrice);

            float consumptionDivider = 0f;
            if (buildingData.Info.m_class.m_subService == ItemClass.SubService.IndustrialGeneric)
            {
                consumptionDivider = RealCityPrivateBuildingAI.GetComsumptionDivider(buildingData, BuildingData.lastBuildingID) * 4f;
                comsuptionDivide.text = string.Format(Localization.Get("MATERIAL_DIV_PRODUCTION") + " [1:{0:N2}]", consumptionDivider);
            }
            else
            {
                if (buildingData.Info.m_buildingAI is IndustrialExtractorAI)
                {
                    comsuptionDivide.text = string.Format(Localization.Get("MATERIAL_DIV_PRODUCTION") + " N/A");
                }
                else
                {
                    consumptionDivider = RealCityPrivateBuildingAI.GetComsumptionDivider(buildingData, BuildingData.lastBuildingID);
                    comsuptionDivide.text = string.Format(Localization.Get("MATERIAL_DIV_PRODUCTION") + " [1:{0:N2}]", consumptionDivider);
                }
            }

            int m_sellTax = RealCityPrivateBuildingAI.GetTaxRate(buildingData);
            sellTax.text = string.Format(Localization.Get("SELL_TAX") + " [{0}%]", m_sellTax);

            if (consumptionDivider == 0f)
            {
                profit.text = string.Format(Localization.Get("SELL_TAX") + " N/A");
            }
            else
            {
                float profitRatio = (outgoingPrice * (1f - m_sellTax / 100f) - (incomePrice / consumptionDivider)) / outgoingPrice;
                if (buildingData.Info.m_class.m_service == ItemClass.Service.Commercial)
                {
                    profit.text = string.Format(Localization.Get("PROFIT") + " [{0}%]" + Localization.Get("EXCLUDE_VISIT_INCOME"), (int)(profitRatio * 100f));
                }
                else
                {
                    profit.text = string.Format(Localization.Get("PROFIT") + " [{0}%]", (int)(profitRatio * 100f));
                }
            }

            int usedCar = 0;
            int num = 0;
            int num1 = 0;
            int num2 = 0;
            int car = 0;
            if (buildingData.Info.m_class.m_service == ItemClass.Service.Industrial)
            {
                TransferManager.TransferReason tempReason = default(TransferManager.TransferReason);
                if (buildingData.Info.m_buildingAI is IndustrialExtractorAI)
                {
                    RealCityIndustrialExtractorAI.InitDelegate();
                    var industrialExtractorAI = (IndustrialExtractorAI)buildingData.Info.m_buildingAI;
                    int productionCapacity = industrialExtractorAI.CalculateProductionCapacity((ItemClass.Level)buildingData.m_level, new Randomizer(BuildingData.lastBuildingID), buildingData.m_width, buildingData.m_length);
                    car = Mathf.Max(1, productionCapacity / 6);
                    tempReason = RealCityIndustrialExtractorAI.GetOutgoingTransferReason((IndustrialExtractorAI)buildingData.Info.m_buildingAI);
                    RealCityCommonBuildingAI.InitDelegate();
                    RealCityCommonBuildingAI.CalculateOwnVehicles((IndustrialExtractorAI)buildingData.Info.m_buildingAI, BuildingData.lastBuildingID, ref buildingData, tempReason, ref usedCar, ref num, ref num1, ref num2);
                }
                else
                {
                    RealCityIndustrialBuildingAI.InitDelegate();
                    var industrialBuildingAI = (IndustrialBuildingAI)buildingData.Info.m_buildingAI;
                    int productionCapacity = industrialBuildingAI.CalculateProductionCapacity((ItemClass.Level)buildingData.m_level, new Randomizer(BuildingData.lastBuildingID), buildingData.m_width, buildingData.m_length);
                    car = Mathf.Max(1, productionCapacity / 6);
                    tempReason = RealCityIndustrialBuildingAI.GetOutgoingTransferReason((IndustrialBuildingAI)buildingData.Info.m_buildingAI);
                    RealCityCommonBuildingAI.InitDelegate();
                    RealCityCommonBuildingAI.CalculateOwnVehicles((IndustrialBuildingAI)buildingData.Info.m_buildingAI, BuildingData.lastBuildingID, ref buildingData, tempReason, ref usedCar, ref num, ref num1, ref num2);
                }

                usedcar.text = string.Format(Localization.Get("CAR_USED") + " [{0}/{1}]", usedCar, car);
            }
            else if (buildingData.Info.m_class.m_service == ItemClass.Service.Commercial)
            {
                Citizen.BehaviourData behaviour = default;
                int aliveVisitCount = 0;
                int totalVisitCount = 0;
                RealCityCommercialBuildingAI.InitDelegate();
                RealCityCommercialBuildingAI.GetVisitBehaviour((CommercialBuildingAI)(buildingData.Info.m_buildingAI), BuildingData.lastBuildingID, ref buildingData, ref behaviour, ref aliveVisitCount, ref totalVisitCount);
                var amount = buildingData.m_customBuffer2 / MainDataStore.maxGoodPurchase - totalVisitCount + aliveVisitCount;
                var commercialBuildingAI = buildingData.Info.m_buildingAI as CommercialBuildingAI;
                var maxCount = commercialBuildingAI.CalculateVisitplaceCount((ItemClass.Level)buildingData.m_level, new Randomizer(BuildingData.lastBuildingID), buildingData.m_width, buildingData.m_length);
                usedcar.text = string.Format("FORDEBUG" + " [{0}/{1}/{2}/{3}]", aliveVisitCount, totalVisitCount, maxCount, amount);
            }
            else
            {
                usedcar.text = Localization.Get("CAR_USED") + " 0/0";
            }
        }


        public TransferManager.TransferReason IndustrialExtractorGetOutgoingTransferReason(Building data)
        {
            switch (data.Info.m_class.m_subService)
            {
                case ItemClass.SubService.IndustrialForestry:
                    return TransferManager.TransferReason.Logs;
                case ItemClass.SubService.IndustrialFarming:
                    return TransferManager.TransferReason.Grain;
                case ItemClass.SubService.IndustrialOil:
                    return TransferManager.TransferReason.Oil;
                case ItemClass.SubService.IndustrialOre:
                    return TransferManager.TransferReason.Ore;
                default:
                    return TransferManager.TransferReason.None;
            }
        }

        private TransferManager.TransferReason IndustrialGetOutgoingTransferReason(Building data)
        {
            switch (data.Info.m_class.m_subService)
            {
                case ItemClass.SubService.IndustrialForestry:
                    return TransferManager.TransferReason.Lumber;
                case ItemClass.SubService.IndustrialFarming:
                    return TransferManager.TransferReason.Food;
                case ItemClass.SubService.IndustrialOil:
                    return TransferManager.TransferReason.Petrol;
                case ItemClass.SubService.IndustrialOre:
                    return TransferManager.TransferReason.Coal;
                default:
                    return TransferManager.TransferReason.Goods;
            }
        }

        protected void CalculateOwnVehicles(ushort buildingID, ref Building data, TransferManager.TransferReason material, ref int count, ref int cargo, ref int capacity, ref int outside)
        {
            VehicleManager instance = Singleton<VehicleManager>.instance;
            ushort num = data.m_ownVehicles;
            int num2 = 0;
            while (num != 0)
            {
                if ((TransferManager.TransferReason)instance.m_vehicles.m_buffer[num].m_transferType == material)
                {
                    VehicleInfo info = instance.m_vehicles.m_buffer[num].Info;
                    int a;
                    int num3;
                    info.m_vehicleAI.GetSize(num, ref instance.m_vehicles.m_buffer[num], out a, out num3);
                    cargo += Mathf.Min(a, num3);
                    capacity += num3;
                    count++;
                    if ((instance.m_vehicles.m_buffer[num].m_flags & (Vehicle.Flags.Importing | Vehicle.Flags.Exporting)) != 0)
                    {
                        outside++;
                    }
                }
                num = instance.m_vehicles.m_buffer[num].m_nextOwnVehicle;
                if (++num2 > 16384)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }
        }

        public int CalculateProductionCapacity(Building buildingData, ItemClass.Level level, Randomizer r, int width, int length)
        {
            ItemClass @class = buildingData.Info.m_class;
            int num;
            if (@class.m_subService == ItemClass.SubService.IndustrialGeneric)
            {
                if (level == ItemClass.Level.Level1)
                {
                    num = 100;
                }
                else if (level == ItemClass.Level.Level2)
                {
                    num = 140;
                }
                else
                {
                    num = 160;
                }
            }
            else
            {
                num = 100;
            }
            if (num != 0)
            {
                num = Mathf.Max(100, width * length * num + r.Int32(100u)) / 100;
            }
            return num;
        }


        public static float CaculateEmployeeOutcome(Building building, out int totalWorkerCount)
        {
            totalWorkerCount = 0;
            float allSalary = 0;
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint num = building.m_citizenUnits;
            int num2 = 0;
            while (num != 0u)
            {
                if ((ushort)(instance.m_units.m_buffer[(int)((UIntPtr)num)].m_flags & CitizenUnit.Flags.Work) != 0)
                {
                    var citizenID = instance.m_units.m_buffer[(int)((UIntPtr)num)].m_citizen0;
                    if (citizenID != 0u)
                    {
                        totalWorkerCount++;
                        allSalary += RealCityResidentAI.ProcessCitizenSalary(citizenID, true);
                    }
                    citizenID = instance.m_units.m_buffer[(int)((UIntPtr)num)].m_citizen1;
                    if (citizenID != 0u)
                    {
                        totalWorkerCount++;
                        allSalary += RealCityResidentAI.ProcessCitizenSalary(citizenID, true);
                    }
                    citizenID = instance.m_units.m_buffer[(int)((UIntPtr)num)].m_citizen2;
                    if (citizenID != 0u)
                    {
                        totalWorkerCount++;
                        allSalary += RealCityResidentAI.ProcessCitizenSalary(citizenID, true);
                    }
                    citizenID = instance.m_units.m_buffer[(int)((UIntPtr)num)].m_citizen3;
                    if (citizenID != 0u)
                    {
                        totalWorkerCount++;
                        allSalary += RealCityResidentAI.ProcessCitizenSalary(citizenID, true);
                    }
                    citizenID = instance.m_units.m_buffer[(int)((UIntPtr)num)].m_citizen4;
                    if (citizenID != 0u)
                    {
                        totalWorkerCount++;
                        allSalary += RealCityResidentAI.ProcessCitizenSalary(citizenID, true);
                    }
                }
                num = instance.m_units.m_buffer[(int)((UIntPtr)num)].m_nextUnit;
                if (++num2 > 524288)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }

            if (totalWorkerCount == 0)
                return 0;
            else
                return allSalary / totalWorkerCount;
        }

        public static void GetWorkBehaviour(ushort buildingID, ref Building buildingData, ref Citizen.BehaviourData behaviour, ref int aliveCount, ref int totalCount)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint num = buildingData.m_citizenUnits;
            int num2 = 0;
            while (num != 0u)
            {
                if ((ushort)(instance.m_units.m_buffer[(int)((UIntPtr)num)].m_flags & CitizenUnit.Flags.Work) != 0)
                {
                    instance.m_units.m_buffer[(int)((UIntPtr)num)].GetCitizenWorkBehaviour(ref behaviour, ref aliveCount, ref totalCount);
                }
                num = instance.m_units.m_buffer[(int)((UIntPtr)num)].m_nextUnit;
                if (++num2 > 524288)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }
        }


        public int CaculateLandFee(Building building, ushort buildingID)
        {
            DistrictManager instance = Singleton<DistrictManager>.instance;
            byte district = instance.GetDistrict(building.m_position);
            DistrictPolicies.Services servicePolicies = instance.m_districts.m_buffer[district].m_servicePolicies;
            DistrictPolicies.Taxation taxationPolicies = instance.m_districts.m_buffer[district].m_taxationPolicies;
            DistrictPolicies.CityPlanning cityPlanningPolicies = instance.m_districts.m_buffer[district].m_cityPlanningPolicies;

            GetLandRent(building, out int landFee);
            int taxRate;
            taxRate = Singleton<EconomyManager>.instance.GetTaxRate(building.Info.m_class, taxationPolicies);

            if (instance.IsPolicyLoaded(DistrictPolicies.Policies.ExtraInsulation))
            {
                if ((servicePolicies & DistrictPolicies.Services.ExtraInsulation) != DistrictPolicies.Services.None)
                {
                    taxRate = taxRate * 95 / 100;
                }
            }
            if ((servicePolicies & DistrictPolicies.Services.Recycling) != DistrictPolicies.Services.None)
            {
                taxRate = taxRate * 95 / 100;
            }

            if (((taxationPolicies & DistrictPolicies.Taxation.DontTaxLeisure) != DistrictPolicies.Taxation.None) && (building.Info.m_class.m_subService == ItemClass.SubService.CommercialLeisure))
            {
                landFee = 0;
            }

            if (BuildingData.buildingMoney[buildingID] > 0)
            {
                if ((building.Info.m_class.m_service == ItemClass.Service.Commercial) || (building.Info.m_class.m_service == ItemClass.Service.Industrial))
                {
                    if (BuildingData.buildingMoney[buildingID] > (taxRate * landFee / 100f))
                        return (int)(taxRate * landFee / 100f);
                    else
                        return 0;
                }
                else if (building.Info.m_class.m_service == ItemClass.Service.Office)
                {
                    Citizen.BehaviourData behaviourData = default;
                    int aliveWorkerCount = 0;
                    int totalWorkerCount = 0;
                    RealCityCommonBuildingAI.InitDelegate();
                    RealCityCommonBuildingAI.GetWorkBehaviour((OfficeBuildingAI)building.Info.m_buildingAI, buildingID, ref building, ref behaviourData, ref aliveWorkerCount, ref totalWorkerCount);
                    return (int)(totalWorkerCount * taxRate / 10f);
                }
            }

            return 0;
        }

        public void GetLandRent(Building building, out int incomeAccumulation)
        {
            ItemClass @class = building.Info.m_class;
            incomeAccumulation = 0;
            ItemClass.SubService subService = @class.m_subService;
            switch (subService)
            {
                case ItemClass.SubService.IndustrialFarming:
                    incomeAccumulation = MainDataStore.induFarm;
                    break;
                case ItemClass.SubService.IndustrialForestry:
                    incomeAccumulation = MainDataStore.induForest;
                    break;
                case ItemClass.SubService.IndustrialOil:
                    incomeAccumulation = MainDataStore.induOil;
                    break;
                case ItemClass.SubService.IndustrialOre:
                    incomeAccumulation = MainDataStore.induOre;
                    break;
                case ItemClass.SubService.IndustrialGeneric:
                    if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = MainDataStore.induGenLevel1;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = MainDataStore.induGenLevel2;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = MainDataStore.induGenLevel3;
                    }
                    break;
                case ItemClass.SubService.CommercialHigh:
                    if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = MainDataStore.commHighLevel1;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = MainDataStore.commHighLevel2;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = MainDataStore.commHighLevel3;
                    }
                    break;
                case ItemClass.SubService.CommercialLow:
                    if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = MainDataStore.commLowLevel1;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = MainDataStore.commLowLevel2;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = MainDataStore.commLowLevel3;
                    }
                    break;
                case ItemClass.SubService.CommercialLeisure:
                    incomeAccumulation = MainDataStore.commLeisure;
                    break;
                case ItemClass.SubService.CommercialTourist:
                    incomeAccumulation = MainDataStore.commTourist;
                    break;
                case ItemClass.SubService.CommercialEco:
                    incomeAccumulation = MainDataStore.commEco;
                    break;
                default: break;
            }
        }
    }
}
