using System.Collections.Generic;
using ColossalFramework.UI;
using UnityEngine;
using ColossalFramework;
using System;
using ColossalFramework.Math;
using RealCity.CustomAI;
using RealCity.Util;

namespace RealCity.UI
{
    public class BuildingUI : UIPanel
    {
        public static readonly string cacheName = "BuildingUI";
        private static readonly float SPACING = 15f;
        private static readonly float SPACING22 = 22f;
        private Dictionary<string, UILabel> _valuesControlContainer = new Dictionary<string, UILabel>(16);
        public ZonedBuildingWorldInfoPanel baseBuildingWindow;
        public static bool refeshOnce = false;
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

        public override void Update()
        {
            RefreshDisplayData();
            base.Update();
        }

        public override void Awake()
        {
            base.Awake();
            DoOnStartup();
        }

        public override void Start()
        {
            base.Start();
            canFocus = true;
            isInteractive = true;
            isVisible = true;
            opacity = 1f;
            cachedName = cacheName;
            RefreshDisplayData();
            Hide();
        }

        private void DoOnStartup()
        {
            ShowOnGui();
            Hide();          
        }

        private void ShowOnGui()
        {
            buildingMoney = AddUIComponent<UILabel>();
            buildingMoney.text = Localization.Get("BUILDING_MONEY");
            buildingMoney.relativePosition = new Vector3(SPACING, 50f);
            buildingMoney.autoSize = true;

            buildingIncomeBuffer = AddUIComponent<UILabel>();
            buildingIncomeBuffer.text = Localization.Get("MATERIAL_BUFFER");
            buildingIncomeBuffer.relativePosition = new Vector3(SPACING, buildingMoney.relativePosition.y + SPACING22);
            buildingIncomeBuffer.autoSize = true;

            buildingOutgoingBuffer = AddUIComponent<UILabel>();
            buildingOutgoingBuffer.text = Localization.Get("PRODUCTION_BUFFER");
            buildingOutgoingBuffer.relativePosition = new Vector3(SPACING, buildingIncomeBuffer.relativePosition.y + SPACING22);
            buildingOutgoingBuffer.autoSize = true;

            employFee = AddUIComponent<UILabel>();
            employFee.text = Localization.Get("AVERAGE_EMPLOYFEE");
            employFee.relativePosition = new Vector3(SPACING, buildingOutgoingBuffer.relativePosition.y + SPACING22);
            employFee.autoSize = true;

            landRent = AddUIComponent<UILabel>();
            landRent.text = Localization.Get("BUILDING_LANDRENT");
            landRent.relativePosition = new Vector3(SPACING, employFee.relativePosition.y + SPACING22);
            landRent.autoSize = true;

            buyPrice = AddUIComponent<UILabel>();
            buyPrice.text = Localization.Get("BUY_PRICE");
            buyPrice.relativePosition = new Vector3(SPACING, landRent.relativePosition.y + SPACING22);
            buyPrice.autoSize = true;

            sellPrice = AddUIComponent<UILabel>();
            sellPrice.text = Localization.Get("SELL_PRICE");
            sellPrice.relativePosition = new Vector3(SPACING, buyPrice.relativePosition.y + SPACING22);
            sellPrice.autoSize = true;

            comsuptionDivide = AddUIComponent<UILabel>();
            comsuptionDivide.text = Localization.Get("MATERIAL_DIV_PRODUCTION");
            comsuptionDivide.relativePosition = new Vector3(SPACING, sellPrice.relativePosition.y + SPACING22);
            comsuptionDivide.autoSize = true;

            sellTax = AddUIComponent<UILabel>();
            sellTax.text = Localization.Get("SELL_TAX");
            sellTax.relativePosition = new Vector3(SPACING, comsuptionDivide.relativePosition.y + SPACING22);
            sellTax.autoSize = true;

            profit = AddUIComponent<UILabel>();
            profit.text = Localization.Get("PROFIT");
            profit.relativePosition = new Vector3(SPACING, sellTax.relativePosition.y + SPACING22);
            profit.autoSize = true;

            usedcar = AddUIComponent<UILabel>();
            usedcar.text = Localization.Get("CAR_USED");
            usedcar.relativePosition = new Vector3(SPACING, profit.relativePosition.y + SPACING22);
            usedcar.autoSize = true;
        }

        private void RefreshDisplayData()
        {
            uint currentFrameIndex = Singleton<SimulationManager>.instance.m_currentFrameIndex;
            uint num2 = currentFrameIndex & 255u;
            if (refeshOnce || (MainDataStore.last_buildingid != WorldInfoPanel.GetCurrentInstanceID().Building))
            {
                if (isVisible)
                {
                    MainDataStore.last_buildingid = WorldInfoPanel.GetCurrentInstanceID().Building;
                    Building buildingData = Singleton<BuildingManager>.instance.m_buildings.m_buffer[MainDataStore.last_buildingid];
                    if (buildingData.Info.m_class.m_service == ItemClass.Service.Residential)
                    {
                        Hide();
                    }
                    else
                    {
                        int aliveWorkerCount = 0;
                        int totalWorkerCount = 0;
                        float num = CaculateEmployeeOutcome(buildingData, MainDataStore.last_buildingid, out aliveWorkerCount, out totalWorkerCount);
                        int num1 = CaculateLandFee(buildingData, MainDataStore.last_buildingid);
                        string type = RealCityPrivateBuildingAI.GetProductionType(false, MainDataStore.last_buildingid, buildingData);
                        string type2 = RealCityPrivateBuildingAI.GetProductionType(true, MainDataStore.last_buildingid, buildingData);
                        float price = RealCityPrivateBuildingAI.GetPrice(false, MainDataStore.last_buildingid, buildingData);
                        float price2 = RealCityPrivateBuildingAI.GetPrice(true, MainDataStore.last_buildingid, buildingData);
                        buildingMoney.text = string.Format(Localization.Get("BUILDING_MONEY") + " [{0}]", MainDataStore.building_money[MainDataStore.last_buildingid]);
                        buildingIncomeBuffer.text = string.Format(Localization.Get("MATERIAL_BUFFER") + " [{0}]" + " " + type, buildingData.m_customBuffer1);
                        buildingOutgoingBuffer.text = string.Format(Localization.Get("PRODUCTION_BUFFER") + " [{0}]"+ " " + type2, buildingData.m_customBuffer2);
                        employFee.text = Localization.Get("AVERAGE_EMPLOYFEE") + " " + num.ToString() + " " + Localization.Get("PROFIT_SHARING");
                        landRent.text = string.Format(Localization.Get("BUILDING_LANDRENT") + " [{0:N2}]", num1 / 100f);
                        buyPrice.text = string.Format(Localization.Get("BUY_PRICE") + " " + type  + "[{0:N2}]", price);
                        sellPrice.text = string.Format(Localization.Get("SELL_PRICE") + " " + type2  + " [{0:N2}]", price2);

                        float consumptionDivider = 0f;
                        if (buildingData.Info.m_class.m_subService == ItemClass.SubService.IndustrialGeneric)
                        {
                            consumptionDivider = RealCityPrivateBuildingAI.GetComsumptionDivider(buildingData, MainDataStore.last_buildingid) * 4f;
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
                                consumptionDivider = RealCityPrivateBuildingAI.GetComsumptionDivider(buildingData, MainDataStore.last_buildingid);
                                comsuptionDivide.text = string.Format(Localization.Get("MATERIAL_DIV_PRODUCTION") + " [1:{0:N2}]", consumptionDivider);
                            }
                        }

                        int m_sellTax = RealCityPrivateBuildingAI.GetTaxRate(buildingData, MainDataStore.last_buildingid);
                        if (buildingData.Info.m_buildingAI is IndustrialExtractorAI)
                        {
                            sellTax.text = string.Format(Localization.Get("SELL_TAX") + " [{0}%] " + Localization.Get("INCLUDE_RESOURCE_TAX"), m_sellTax);
                        }
                        else
                        {
                            sellTax.text = string.Format(Localization.Get("SELL_TAX") + " [{0}%]", m_sellTax);
                        }

                        if (consumptionDivider == 0f)
                        {
                            profit.text = string.Format(Localization.Get("SELL_TAX") + " N/A");
                        }
                        else
                        {
                            float temp = (price2 * (1f - m_sellTax / 100f) - (price / consumptionDivider)) / price2;
                            if (buildingData.Info.m_class.m_service == ItemClass.Service.Commercial)
                            {
                                profit.text = string.Format(Localization.Get("PROFIT") + " [{0}%]" + Localization.Get("EXCLUDE_VISIT_INCOME"), (int)(temp * 100f));
                            }
                            else
                            {
                                profit.text = string.Format(Localization.Get("PROFIT") + " [{0}%]", (int)(temp * 100f));
                            }
                        }

                        int usedCar = 0;
                        int num27 = 0;
                        int num28 = 0;
                        int value = 0;
                        int car = 0;
                        if (buildingData.Info.m_class.m_service == ItemClass.Service.Industrial)
                        {
                            int num7 = CalculateProductionCapacity(buildingData, (ItemClass.Level)buildingData.m_level, new Randomizer(MainDataStore.last_buildingid), buildingData.m_width, buildingData.m_length);
                            car = Mathf.Max(1, num7 / 6);

                            TransferManager.TransferReason tempReason = default(TransferManager.TransferReason);
                            if (buildingData.Info.m_buildingAI is IndustrialExtractorAI)
                            {
                                tempReason = IndustrialExtractorGetOutgoingTransferReason(buildingData);
                            }
                            else
                            {
                                tempReason = IndustrialGetOutgoingTransferReason(buildingData);
                            }

                            CalculateOwnVehicles(MainDataStore.last_buildingid, ref buildingData, tempReason, ref usedCar, ref num27, ref num28, ref value);
                            usedcar.text = string.Format(Localization.Get("CAR_USED") + " [{0}/{1}]", usedCar,car);
                        }
                        else
                        {
                            usedcar.text = Localization.Get("CAR_USED") + " 0/0";
                        }

                        BringToFront();
                        refeshOnce = false;
                    }
                }
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
                if (++num2 > Singleton<VehicleManager>.instance.m_vehicles.m_size)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }
        }

        public int CalculateProductionCapacity(Building buildingData ,ItemClass.Level level, Randomizer r, int width, int length)
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

        public static float CaculateEmployeeOutcome(Building building, ushort buildingID, out int aliveWorkerCount, out int totalWorkerCount)
        {
            float num1 = 0;
            Citizen.BehaviourData behaviour = default(Citizen.BehaviourData);
            aliveWorkerCount = 0;
            totalWorkerCount = 0;
            GetWorkBehaviour(buildingID, ref building, ref behaviour, ref aliveWorkerCount, ref totalWorkerCount);

            if (totalWorkerCount > 0)
            {
                if (MainDataStore.building_money[buildingID] > 0)
                {
                    switch (building.Info.m_class.m_service)
                    {
                        case ItemClass.Service.Commercial:
                        case ItemClass.Service.Industrial:
                            num1 = (int)((MainDataStore.building_money[buildingID]) * 0.05f / totalWorkerCount);
                            break;
                        case ItemClass.Service.Office:
                            num1 = (int)(MainDataStore.building_money[buildingID] / totalWorkerCount);
                            break;
                    }
                }
                else
                {
                    num1 = 0;
                }
            }
            return num1;
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

            int num = 0;
            GetLandRent(building, buildingID, out num);
            int num2;
            num2 = Singleton<EconomyManager>.instance.GetTaxRate(building.Info.m_class, taxationPolicies);
            if (((taxationPolicies & DistrictPolicies.Taxation.DontTaxLeisure) != DistrictPolicies.Taxation.None) && (building.Info.m_class.m_subService == ItemClass.SubService.CommercialLeisure))
            {
                num = 0;
            }

            if (MainDataStore.building_money[buildingID] < 0)
            {
                num = 0;
            }
            return num*num2;
        }


        public void GetLandRent(Building building, ushort buildingID, out int incomeAccumulation)
        {
            ItemClass @class = building.Info.m_class;
            incomeAccumulation = 0;
            ItemClass.SubService subService = @class.m_subService;
            switch (subService)
            {
                case ItemClass.SubService.IndustrialFarming:
                    incomeAccumulation = MainDataStore.indu_farm;
                    break;
                case ItemClass.SubService.IndustrialForestry:
                    incomeAccumulation = MainDataStore.indu_forest;
                    break;
                case ItemClass.SubService.IndustrialOil:
                    incomeAccumulation = MainDataStore.indu_oil;
                    break;
                case ItemClass.SubService.IndustrialOre:
                    incomeAccumulation = MainDataStore.indu_ore;
                    break;
                case ItemClass.SubService.IndustrialGeneric:
                    if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = MainDataStore.indu_gen_level1;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = MainDataStore.indu_gen_level2;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = MainDataStore.indu_gen_level3;
                    }
                    break;
                case ItemClass.SubService.CommercialHigh:
                    if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = MainDataStore.comm_high_level1;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = MainDataStore.comm_high_level2;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = MainDataStore.comm_high_level3;
                    }
                    break;
                case ItemClass.SubService.CommercialLow:
                    if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = MainDataStore.comm_low_level1;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = MainDataStore.comm_low_level2;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = MainDataStore.comm_low_level3;
                    }
                    break;
                case ItemClass.SubService.CommercialLeisure:
                    incomeAccumulation = MainDataStore.comm_leisure;
                    break;
                case ItemClass.SubService.CommercialTourist:
                    incomeAccumulation = MainDataStore.comm_tourist;
                    break;
                case ItemClass.SubService.CommercialEco:
                    incomeAccumulation = MainDataStore.comm_eco;
                    break;
                case ItemClass.SubService.OfficeGeneric:
                    if (building.Info.m_class.m_level == ItemClass.Level.Level1)
                    {
                        incomeAccumulation = MainDataStore.office_gen_levell;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level2)
                    {
                        incomeAccumulation = MainDataStore.office_gen_level2;
                    }
                    else if (building.Info.m_class.m_level == ItemClass.Level.Level3)
                    {
                        incomeAccumulation = MainDataStore.office_gen_level3;
                    }
                    break;
                case ItemClass.SubService.OfficeHightech:
                    incomeAccumulation = MainDataStore.office_high_tech;
                    break;
                default: break;
            }
        }
    }
}
