using ColossalFramework;
using HarmonyLib;
using RealCity.CustomManager;
using RealCity.Util;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class EconomyManagerAddPrivateIncomePatch
    {
        
        public static float citizenIncome = 0f;
        public static float touristIncome = 0f;
        public static float residentTaxIncome = 0f;
        public static float commercialLowIncome = 0f;
        public static float commercialHighIncome = 0f;
        public static float commercialLeiIncome = 0f;
        public static float commercialLTouIncome = 0f;
        public static float commercialLEcoIncome = 0f;
        public static float industyForestIncome = 0f;
        public static float industyFarmIncome = 0f;
        public static float industyOilIncome = 0f;
        public static float industyOreIncome = 0f;
        public static float industyGenIncome = 0f;
        public static float officeGenIncome = 0f;
        public static float officeHighTechIncome = 0f;
        public static float residentLowIncome = 0f;
        public static float residentLowEcoIncome = 0f;
        public static float residentHighIncome = 0f;
        public static float residentHighEcoIncome = 0f;
        public static float commercialLowTradeIncome = 0f;
        public static float commercialHighTradeIncome = 0f;
        public static float commercialLeiTradeIncome = 0f;
        public static float commercialTouTradeIncome = 0f;
        public static float commercialEcoTradeIncome = 0f;
        public static float industyForestTradeIncome = 0f;
        public static float industyFarmTradeIncome = 0f;
        public static float industyOilTradeIncome = 0f;
        public static float industyOreTradeIncome = 0f;
        public static float industyGenTradeIncome = 0f;
        public static float garbageIncome = 0f;
        public static float roadIncome = 0f;
        public static float playerIndustryIncome = 0f;
        public static float schoolIncome = 0f;
        public static float policeStationIncome = 0f;
        public static float healthCareIncome = 0f;
        public static float fireStationIncome = 0f;

        public static MethodBase TargetMethod()
        {
            return typeof(EconomyManager).GetMethod("AddPrivateIncome", BindingFlags.Public | BindingFlags.Instance);
        }

        public static void CustomAddGovermentIncome(ref int amount, ItemClass.Service service)
        {
            switch (service)
            {
                case ItemClass.Service.Garbage:
                    ProcessUnitTax100(ref amount, ref garbageIncome);
                    RealCityEconomyManager.garbageIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.Service.Education:
                    ProcessUnitTax100(ref amount, ref schoolIncome);
                    RealCityEconomyManager.schoolIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.Service.HealthCare:
                    ProcessUnitTax100(ref amount, ref healthCareIncome);
                    RealCityEconomyManager.healthCareIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.Service.FireDepartment:
                    ProcessUnitTax100(ref amount, ref fireStationIncome);
                    RealCityEconomyManager.fireStationIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.Service.PoliceDepartment:
                    ProcessUnitTax100(ref amount, ref policeStationIncome);
                    RealCityEconomyManager.policeStationIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                default:
                    amount = 0;
                    DebugLog.LogToFileOnly("Error: Find unknown EXAddGovermentIncome building" + " service is" + service.ToString());
                    break;
            }
        }

        public static void CustomAddPersonalTaxIncome(ref int amount, ItemClass.Service service)
        {
            switch (service)
            {
                case ItemClass.Service.Residential:
                    ProcessUnitTax100(ref amount, ref residentTaxIncome);
                    break;
                default:
                    DebugLog.LogToFileOnly("Find unknown  EXAddPrivateLandIncome building" + " building service is" + service);
                    break;
            }
            RealCityEconomyManager.citizenTaxIncomeForUI[MainDataStore.updateMoneyCount] = RealCityEconomyManager.citizenTaxIncomeForUI[MainDataStore.updateMoneyCount] + amount;
        }

        public static void CustomAddTourismIncome(ref int amount, int taxRate)
        {
            if (taxRate == 114333)
            {
                ProcessUnitTax100(ref amount, ref citizenIncome);
                RealCityEconomyManager.citizenIncomeForUI[MainDataStore.updateMoneyCount] += amount;
            }
            else
            {
                ProcessUnitTax100(ref amount, ref touristIncome);
                RealCityEconomyManager.touristIncomeForUI[MainDataStore.updateMoneyCount] += amount;
            }
        }

        public static void CustomAddPrivateTradeIncome(ref int amount, ItemClass.SubService subService)
        {
            switch (subService)
            {
                case ItemClass.SubService.IndustrialFarming:
                    ProcessUnitTax100(ref amount, ref industyFarmTradeIncome);
                    RealCityEconomyManager.induFarmerTradeIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.SubService.IndustrialForestry:
                    ProcessUnitTax100(ref amount, ref industyForestTradeIncome);
                    RealCityEconomyManager.induForestyTradeIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.SubService.IndustrialOil:
                    ProcessUnitTax100(ref amount, ref industyOilTradeIncome);
                    RealCityEconomyManager.induOilTradeIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.SubService.IndustrialOre:
                    ProcessUnitTax100(ref amount, ref industyOreTradeIncome);
                    RealCityEconomyManager.induOreTradeIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.SubService.IndustrialGeneric:
                    ProcessUnitTax100(ref amount, ref industyGenTradeIncome);
                    RealCityEconomyManager.induGenTradeIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.SubService.CommercialHigh:
                    ProcessUnitTax100(ref amount, ref commercialHighTradeIncome);
                    RealCityEconomyManager.commHighTradeIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.SubService.CommercialLow:
                    ProcessUnitTax100(ref amount, ref commercialLowTradeIncome);
                    RealCityEconomyManager.commLowTradeIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.SubService.CommercialLeisure:
                    ProcessUnitTax100(ref amount, ref commercialLeiTradeIncome);
                    RealCityEconomyManager.commLeiTradeIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.SubService.CommercialTourist:
                    ProcessUnitTax100(ref amount, ref commercialTouTradeIncome);
                    RealCityEconomyManager.commTouTradeIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.SubService.CommercialEco:
                    ProcessUnitTax100(ref amount, ref commercialEcoTradeIncome);
                    RealCityEconomyManager.commEcoTradeIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                default:
                    amount = 0;
                    DebugLog.LogToFileOnly("Find unknown  EXAddPrivateTradeIncome building" + " building subservise is" + subService);
                    break;
            }
        }

        public static void CustomAddPrivateLandIncome(ref int amount, ItemClass.SubService subService, int taxRate)
        {
            switch (subService)
            {
                case ItemClass.SubService.IndustrialFarming:
                    ProcessUnit(ref amount, ref industyFarmIncome, taxRate);
                    RealCityEconomyManager.induFarmerLandIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.SubService.IndustrialForestry:
                    ProcessUnit(ref amount, ref industyForestIncome, taxRate);
                    RealCityEconomyManager.induForestyLandIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.SubService.IndustrialOil:
                    ProcessUnit(ref amount, ref industyOilIncome, taxRate);
                    RealCityEconomyManager.induOilLandIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.SubService.IndustrialOre:
                    ProcessUnit(ref amount, ref industyOreIncome, taxRate);
                    RealCityEconomyManager.induOreLandIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.SubService.IndustrialGeneric:
                    ProcessUnit(ref amount, ref industyGenIncome, taxRate);
                    RealCityEconomyManager.induGenLandIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.SubService.CommercialHigh:
                    ProcessUnit(ref amount, ref commercialHighIncome, taxRate);
                    RealCityEconomyManager.commHighLandIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.SubService.CommercialLow:
                    ProcessUnit(ref amount, ref commercialLowIncome, taxRate);
                    RealCityEconomyManager.commLowLandIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.SubService.CommercialLeisure:
                    ProcessUnit(ref amount, ref commercialLeiIncome, taxRate);
                    RealCityEconomyManager.commLeiLandIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.SubService.CommercialTourist:
                    ProcessUnit(ref amount, ref commercialLTouIncome, taxRate);
                    RealCityEconomyManager.commTouLandIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.SubService.CommercialEco:
                    ProcessUnit(ref amount, ref commercialLEcoIncome, taxRate);
                    RealCityEconomyManager.commEcoLandIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.SubService.ResidentialHigh:
                    ProcessUnit(ref amount, ref residentHighIncome, taxRate);
                    RealCityEconomyManager.residentHighLandIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.SubService.ResidentialLow:
                    ProcessUnit(ref amount, ref residentLowIncome, taxRate);
                    RealCityEconomyManager.residentLowLandIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.SubService.ResidentialHighEco:
                    ProcessUnit(ref amount, ref residentHighEcoIncome, taxRate);
                    RealCityEconomyManager.residentHighEcoLandIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.SubService.ResidentialLowEco:
                    ProcessUnit(ref amount, ref residentLowEcoIncome, taxRate);
                    RealCityEconomyManager.residentLowEcoLandIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.SubService.OfficeGeneric:
                    ProcessUnit(ref amount, ref officeGenIncome, taxRate);
                    RealCityEconomyManager.officeGenLandIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                case ItemClass.SubService.OfficeHightech:
                    ProcessUnit(ref amount, ref officeHighTechIncome, taxRate);
                    RealCityEconomyManager.officeHighTechLandIncomeForUI[MainDataStore.updateMoneyCount] += amount;
                    break;
                default:
                    amount = 0;
                    DebugLog.LogToFileOnly("find unknown  EXAddPrivateLandIncome building" + " building subService is" + subService + " buildlevelandtax is "  + taxRate);
                    break;
            }
        }

        public static void ProcessUnit(ref int amount, ref float container, int taxRate)
        {
            container += (amount * taxRate / 100f);
            if (container > 1)
            {
                amount = (int)container;
                container -= (int)container;
            }
            else
            {
                amount = 0;
            }
        }

        public static void ProcessUnitTax100(ref int amount, ref float container)
        {
            container += amount;
            if (container > 1)
            {
                amount = (int)container;
                container -= (int)container;
            }
            else
            {
                amount = 0;
            }
        }
        public static bool Prefix(int amount, ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level, int taxRate)
        {
            if (amount < 0)
            {
                DebugLog.LogToFileOnly($"Error: EconomyManagerAddPrivateIncomePatch amount < 0 {service} {subService} {level}");
                amount = 0;
            }
            if (taxRate == 115333)
            {
                //115333 means playerbuilding income
                //taxRate = 100; no need to send taxRate.
                CustomAddGovermentIncome(ref amount, service);
                service = ItemClass.Service.Industrial;
                subService = ItemClass.SubService.IndustrialGeneric;
                level = ItemClass.Level.Level3;
            }
            else if ((taxRate == 113333) || (taxRate == 114333))
            {
                //113333 means tourist tourism income // 114333 means resident tourism income
                CustomAddTourismIncome(ref amount, taxRate);
            }
            else if (taxRate == 112333)
            {
                //112333 means personal slary tax income
                //taxRate = 100; no need to send taxRate.
                CustomAddPersonalTaxIncome(ref amount, service);
            }
            else if (taxRate == 111333)
            {
                //111333 means trade income
                //taxRate = 100; no need to send taxRate.
                CustomAddPrivateTradeIncome(ref amount, subService);
            }
            else if (taxRate >= 100)
            {
                taxRate = UniqueFacultyAI.IncreaseByBonus(UniqueFacultyAI.FacultyBonus.Economics, taxRate);
                taxRate /= 100;
                CustomAddPrivateLandIncome(ref amount, subService, taxRate);
            }
            else
            {
                amount = 0;
            }

            Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.ResourcePrice, amount, service, subService, level, DistrictPolicies.Taxation.None);
            return false;
        }
    }
}
