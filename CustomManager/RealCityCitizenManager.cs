using ColossalFramework;
using ColossalFramework.Math;
using RealCity.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
