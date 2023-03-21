using Harmony;
using RealCity.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class IndustryBuildingGetResourcePricePatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(IndustryBuildingAI).GetMethod("GetResourcePrice", BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
        }
        public static bool Prefix(ref int __result, TransferManager.TransferReason material, ItemClass.Service sourceService = ItemClass.Service.None)
        {
            bool canRisePrice = true;
            switch (material)
            {
                case TransferManager.TransferReason.AnimalProducts:
                    __result = 150; break;
                case TransferManager.TransferReason.Flours:
                    __result = 150; break;
                case TransferManager.TransferReason.Paper:
                    __result = 200; break;
                case TransferManager.TransferReason.PlanedTimber:
                    __result = 200; break;
                case TransferManager.TransferReason.Petroleum:
                    __result = 300; break;
                case TransferManager.TransferReason.Plastics:
                    __result = 300; break;
                case TransferManager.TransferReason.Glass:
                    __result = 250; break;
                case TransferManager.TransferReason.Metals:
                    __result = 250; break;
                case TransferManager.TransferReason.LuxuryProducts:
                    __result = 350; break;
                case TransferManager.TransferReason.Oil:
                    __result = 200; break;
                case TransferManager.TransferReason.Ore:
                    __result = 160; break;
                case TransferManager.TransferReason.Logs:
                    __result = 130; break;
                case TransferManager.TransferReason.Grain:
                    __result = 100; break;
                case TransferManager.TransferReason.Goods:
                    if (sourceService == ItemClass.Service.Fishing)
                    {
                        __result = 500;
                    }
                    else
                    {
                        __result = 0;
                    }
                    break;
                case TransferManager.TransferReason.Fish:
                    __result = 150; canRisePrice = false; break;
                case TransferManager.TransferReason.Petrol:
                    __result = 0; break;
                case TransferManager.TransferReason.Food:
                    __result = 0; break;
                case TransferManager.TransferReason.Lumber:
                    __result = 0; break;
                case TransferManager.TransferReason.Coal:
                    __result = 0; break;
                case TransferManager.TransferReason.Shopping:
                case TransferManager.TransferReason.ShoppingB:
                case TransferManager.TransferReason.ShoppingC:
                case TransferManager.TransferReason.ShoppingD:
                case TransferManager.TransferReason.ShoppingE:
                case TransferManager.TransferReason.ShoppingH:
                    if (sourceService == ItemClass.Service.Fishing)
                    {
                        __result = 200;
                        canRisePrice = false;
                    }
                    else
                    {
                        __result = 0;
                    }
                    break;
                case TransferManager.TransferReason.Entertainment:
                case TransferManager.TransferReason.EntertainmentB:
                case TransferManager.TransferReason.EntertainmentC:
                case TransferManager.TransferReason.EntertainmentD:
                    __result = 0; break;
                default: __result = 0; break;
            }

            if (RealCity.reduceVehicle)
            {
                if (canRisePrice)
                    __result <<= MainDataStore.reduceCargoDivShift;
            }

            __result = UniqueFacultyAI.IncreaseByBonus(UniqueFacultyAI.FacultyBonus.Science, __result);
            return false;
        }
    }
}
