using ColossalFramework;
using Harmony;
using RealCity.CustomManager;
using RealCity.Util;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class EconomyManagerAddPrivateIncomePatch
    {
        
        public static float citizen_income = 0f;
        public static float tourist_income = 0f;
        public static float resident_tax_income = 0f;
        public static float commerical_low_income = 0f;
        public static float commerical_high_income = 0f;
        public static float commerical_lei_income = 0f;
        public static float commerical_tou_income = 0f;
        public static float commerical_eco_income = 0f;
        public static float industy_forest_income = 0f;
        public static float industy_farm_income = 0f;
        public static float industy_oil_income = 0f;
        public static float industy_ore_income = 0f;
        public static float industy_gen_income = 0f;
        public static float office_gen_income = 0f;
        public static float office_high_tech_income = 0f;
        public static float resident_low_income = 0f;
        public static float resident_low_eco_income = 0f;
        public static float resident_high_income = 0f;
        public static float resident_high_eco_income = 0f;
        public static float commerical_low_trade_income = 0f;
        public static float commerical_high_trade_income = 0f;
        public static float commerical_lei_trade_income = 0f;
        public static float commerical_tou_trade_income = 0f;
        public static float commerical_eco_trade_income = 0f;
        public static float industy_forest_trade_income = 0f;
        public static float industy_farm_trade_income = 0f;
        public static float industy_oil_trade_income = 0f;
        public static float industy_ore_trade_income = 0f;
        public static float industy_gen_trade_income = 0f;
        public static float garbage_income = 0f;
        public static float road_income = 0f;
        public static float playerIndustryIncome = 0f;
        public static float school_income = 0f;
        public static float policeStationIncome = 0f;
        public static float healthCareIncome = 0f;
        public static float fireStationIncome = 0f;

        public static MethodBase TargetMethod()
        {
            return typeof(EconomyManager).GetMethod("AddPrivateIncome", BindingFlags.Public | BindingFlags.Instance);
        }

        public static void EXAddGovermentIncome(ref int amount, ItemClass.Service service)
        {
            switch (service)
            {
                case ItemClass.Service.Garbage:
                    ProcessUnitTax100(ref amount, ref garbage_income);
                    RealCityEconomyManager.garbage_income_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.Service.Education:
                    ProcessUnitTax100(ref amount, ref school_income);
                    RealCityEconomyManager.school_income_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.Service.HealthCare:
                    ProcessUnitTax100(ref amount, ref healthCareIncome);
                    RealCityEconomyManager.healthCareIncomeForUI[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.Service.FireDepartment:
                    ProcessUnitTax100(ref amount, ref fireStationIncome);
                    RealCityEconomyManager.fireStationIncomeForUI[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.Service.PoliceDepartment:
                    ProcessUnitTax100(ref amount, ref policeStationIncome);
                    RealCityEconomyManager.policeStationIncomeForUI[MainDataStore.update_money_count] += amount;
                    break;
                default:
                    amount = 0;
                    DebugLog.LogToFileOnly("Error: Find unknown EXAddGovermentIncome building" + " service is" + service.ToString());
                    break;
            }
        }

        public static void EXAddPersonalTaxIncome(ref int amount, ItemClass.Service service)
        {
            switch (service)
            {
                case ItemClass.Service.Residential:
                    ProcessUnitTax100(ref amount, ref resident_tax_income);
                    break;
                default:
                    DebugLog.LogToFileOnly("Find unknown  EXAddPrivateLandIncome building" + " building service is" + service);
                    break;
            }
            RealCityEconomyManager.citizen_tax_income_forui[MainDataStore.update_money_count] = RealCityEconomyManager.citizen_tax_income_forui[MainDataStore.update_money_count] + amount;
        }

        public static void EXAddTourismIncome(ref int amount, int taxRate)
        {
            if (taxRate == 114333)
            {
                ProcessUnitTax100(ref amount, ref citizen_income);
                RealCityEconomyManager.citizen_income_forui[MainDataStore.update_money_count] += amount;
            }
            else
            {
                ProcessUnitTax100(ref amount, ref tourist_income);
                RealCityEconomyManager.tourist_income_forui[MainDataStore.update_money_count] += amount;
            }
        }

        public static void EXAddPrivateTradeIncome(ref int amount, ItemClass.SubService subService)
        {
            switch (subService)
            {
                case ItemClass.SubService.IndustrialFarming:
                    ProcessUnitTax100(ref amount, ref industy_farm_trade_income);
                    RealCityEconomyManager.indu_farmer_tradeincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.IndustrialForestry:
                    ProcessUnitTax100(ref amount, ref industy_forest_trade_income);
                    RealCityEconomyManager.indu_foresty_tradeincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.IndustrialOil:
                    ProcessUnitTax100(ref amount, ref industy_oil_trade_income);
                    RealCityEconomyManager.indu_oil_tradeincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.IndustrialOre:
                    ProcessUnitTax100(ref amount, ref industy_ore_trade_income);
                    RealCityEconomyManager.indu_ore_tradeincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.IndustrialGeneric:
                    ProcessUnitTax100(ref amount, ref industy_gen_trade_income);
                    RealCityEconomyManager.indu_gen_tradeincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialHigh:
                    ProcessUnitTax100(ref amount, ref commerical_high_trade_income);
                    RealCityEconomyManager.comm_high_tradeincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialLow:
                    ProcessUnitTax100(ref amount, ref commerical_low_trade_income);
                    RealCityEconomyManager.comm_low_tradeincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialLeisure:
                    ProcessUnitTax100(ref amount, ref commerical_lei_trade_income);
                    RealCityEconomyManager.comm_lei_tradeincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialTourist:
                    ProcessUnitTax100(ref amount, ref commerical_tou_trade_income);
                    RealCityEconomyManager.comm_tou_tradeincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialEco:
                    ProcessUnitTax100(ref amount, ref commerical_eco_trade_income);
                    RealCityEconomyManager.comm_eco_tradeincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                default:
                    amount = 0;
                    DebugLog.LogToFileOnly("Find unknown  EXAddPrivateTradeIncome building" + " building subservise is" + subService);
                    break;
            }
        }

        public static void EXAddPrivateLandIncome(ref int amount, ItemClass.SubService subService, int taxRate)
        {
            switch (subService)
            {
                case ItemClass.SubService.IndustrialFarming:
                    ProcessUnit(ref amount, ref industy_farm_income, taxRate);
                    RealCityEconomyManager.indu_farmer_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.IndustrialForestry:
                    ProcessUnit(ref amount, ref industy_forest_income, taxRate);
                    RealCityEconomyManager.indu_foresty_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.IndustrialOil:
                    ProcessUnit(ref amount, ref industy_oil_income, taxRate);
                    RealCityEconomyManager.indu_oil_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.IndustrialOre:
                    ProcessUnit(ref amount, ref industy_ore_income, taxRate);
                    RealCityEconomyManager.indu_ore_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.IndustrialGeneric:
                    ProcessUnit(ref amount, ref industy_gen_income, taxRate);
                    RealCityEconomyManager.indu_gen_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialHigh:
                    ProcessUnit(ref amount, ref commerical_high_income, taxRate);
                    RealCityEconomyManager.comm_high_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialLow:
                    ProcessUnit(ref amount, ref commerical_low_income, taxRate);
                    RealCityEconomyManager.comm_low_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialLeisure:
                    ProcessUnit(ref amount, ref commerical_lei_income, taxRate);
                    RealCityEconomyManager.comm_lei_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialTourist:
                    ProcessUnit(ref amount, ref commerical_tou_income, taxRate);
                    RealCityEconomyManager.comm_tou_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.CommercialEco:
                    ProcessUnit(ref amount, ref commerical_eco_income, taxRate);
                    RealCityEconomyManager.comm_eco_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.ResidentialHigh:
                    ProcessUnit(ref amount, ref resident_high_income, taxRate);
                    RealCityEconomyManager.resident_high_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.ResidentialLow:
                    ProcessUnit(ref amount, ref resident_low_income, taxRate);
                    RealCityEconomyManager.resident_low_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.ResidentialHighEco:
                    ProcessUnit(ref amount, ref resident_high_eco_income, taxRate);
                    RealCityEconomyManager.resident_high_eco_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.ResidentialLowEco:
                    ProcessUnit(ref amount, ref resident_low_eco_income, taxRate);
                    RealCityEconomyManager.resident_low_eco_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.OfficeGeneric:
                    ProcessUnit(ref amount, ref office_gen_income, taxRate);
                    RealCityEconomyManager.office_gen_landincome_forui[MainDataStore.update_money_count] += amount;
                    break;
                case ItemClass.SubService.OfficeHightech:
                    ProcessUnit(ref amount, ref office_high_tech_income, taxRate);
                    RealCityEconomyManager.office_high_tech_landincome_forui[MainDataStore.update_money_count] += amount;
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
                container = container - (int)container;
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
                container = container - (int)container;
            }
            else
            {
                amount = 0;
            }
        }
        public static bool Prefix (int amount, ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level, int taxRate)
        {
            if (amount < 0)
            {
                DebugLog.LogToFileOnly("income < 0 error: class =" + service.ToString() + subService.ToString() + level.ToString());
            }
            if (taxRate == 115333)
            {
                //115333 means playerbuilding income
                //taxRate = 100; no need to send taxRate.
                EXAddGovermentIncome(ref amount, service);
                service = ItemClass.Service.Industrial;
                subService = ItemClass.SubService.IndustrialGeneric;
                level = ItemClass.Level.Level3;
            }
            else if ((taxRate == 113333) || (taxRate == 114333))
            {
                //113333 means tourist tourism income // 114333 means resident tourism income
                EXAddTourismIncome(ref amount, taxRate);
            }
            else if (taxRate == 112333)
            {
                //112333 means personal slary tax income
                //taxRate = 100; no need to send taxRate.
                EXAddPersonalTaxIncome(ref amount, service);
            }
            else if (taxRate == 111333)
            {
                //111333 means trade income
                //taxRate = 100; no need to send taxRate.
                EXAddPrivateTradeIncome(ref amount, subService);
            }
            else if (taxRate >= 100)
            {
                taxRate = UniqueFacultyAI.IncreaseByBonus(UniqueFacultyAI.FacultyBonus.Economics, taxRate);
                taxRate = taxRate / 100;
                EXAddPrivateLandIncome(ref amount, subService, taxRate);
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
