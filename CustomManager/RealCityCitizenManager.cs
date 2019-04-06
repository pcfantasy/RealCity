using RealCity.CustomAI;
using RealCity.Util;

namespace RealCity.CustomManager
{
    public class RealCityCitizenManager:CitizenManager
    {
        public static void CitizenManagerReleaseCitizenImplementationPostFix(uint citizen)
        {
            MainDataStore.citizenMoney[citizen] = 0;
            MainDataStore.isCitizenFirstMovingIn[citizen] = false;
        }

        public static void CitizenManagerReleaseUnitCitizenPostFix(uint unit)
        {
            MainDataStore.familyGoods[unit] = 0;
            MainDataStore.family_money[unit] = 0;
        }

        public static void CitizenManagerReleaseCitizenInstancePostFix(ushort instance)
        {
            RealCityHumanAI.watingPathTime[instance] = 0;
        }
    }
}
