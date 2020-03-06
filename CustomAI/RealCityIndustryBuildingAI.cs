using RealCity.Util;

namespace RealCity.CustomAI
{
    public class RealCityIndustryBuildingAI: PlayerBuildingAI
    {
        public static int CustomGetResourcePrice(TransferManager.TransferReason material)
        {
            int price;
            switch (material)
            {
                case TransferManager.TransferReason.AnimalProducts:
                    price = 150; break;
                case TransferManager.TransferReason.Flours:
                    price = 150; break;
                case TransferManager.TransferReason.Paper:
                    price = 200; break;
                case TransferManager.TransferReason.PlanedTimber:
                    price = 200; break;
                case TransferManager.TransferReason.Petroleum:
                    price = 300; break;
                case TransferManager.TransferReason.Plastics:
                    price = 300; break;
                case TransferManager.TransferReason.Glass:
                    price = 250; break;
                case TransferManager.TransferReason.Metals:
                    price = 250; break;
                case TransferManager.TransferReason.LuxuryProducts:
                    price = 350; break;
                case TransferManager.TransferReason.Oil:
                    price = 200; break;
                case TransferManager.TransferReason.Ore:
                    price = 160; break;
                case TransferManager.TransferReason.Logs:
                    price = 130; break;
                case TransferManager.TransferReason.Grain:
                    price = 100; break;
                case TransferManager.TransferReason.Goods:
                    return 0;
                case TransferManager.TransferReason.Petrol:
                    return 0;
                case TransferManager.TransferReason.Food:
                    return 0;
                case TransferManager.TransferReason.Lumber:
                    return 0;
                case TransferManager.TransferReason.Coal:
                    return 0;
                case TransferManager.TransferReason.Shopping:
                case TransferManager.TransferReason.ShoppingB:
                case TransferManager.TransferReason.ShoppingC:
                case TransferManager.TransferReason.ShoppingD:
                case TransferManager.TransferReason.ShoppingE:
                case TransferManager.TransferReason.ShoppingH:
                    return 0;
                case TransferManager.TransferReason.Entertainment:
                case TransferManager.TransferReason.EntertainmentB:
                case TransferManager.TransferReason.EntertainmentC:
                case TransferManager.TransferReason.EntertainmentD:
                    return 0;
                default: return 0;
            }

            if (RealCity.reduceVehicle)
            {
                price <<= MainDataStore.reduceCargoDivShift;
            }
            return UniqueFacultyAI.IncreaseByBonus(UniqueFacultyAI.FacultyBonus.Science, price);
        }

        public static float GetResourcePrice(TransferManager.TransferReason material)
        {
            float price = 0;
            switch (material)
            {
                case TransferManager.TransferReason.Goods:
                    price = 3.5f; break;
                case TransferManager.TransferReason.Petrol:
                    price = 3f; break;
                case TransferManager.TransferReason.Food:
                    price = 1.5f; break;
                case TransferManager.TransferReason.Lumber:
                    price = 2f; break;
                case TransferManager.TransferReason.Coal:
                    price = 2.5f; break;
                case TransferManager.TransferReason.Shopping:
                case TransferManager.TransferReason.ShoppingB:
                case TransferManager.TransferReason.ShoppingC:
                case TransferManager.TransferReason.ShoppingD:
                case TransferManager.TransferReason.ShoppingE:
                case TransferManager.TransferReason.ShoppingH:
                    price = 4.5f; break;
                case TransferManager.TransferReason.Entertainment:
                case TransferManager.TransferReason.EntertainmentB:
                case TransferManager.TransferReason.EntertainmentC:
                case TransferManager.TransferReason.EntertainmentD:
                    if (RealCity.reduceVehicle)
                        price = 1f;
                    else
                        price = 0.5f;
                    break;
                default: return CustomGetResourcePrice(material) / 100f;
            }

            if (RealCity.reduceVehicle)
            {
                price *= MainDataStore.reduceCargoDiv;
            }
            return (UniqueFacultyAI.IncreaseByBonus(UniqueFacultyAI.FacultyBonus.Science, 100) / 100f) * price;
        }
    }
}
