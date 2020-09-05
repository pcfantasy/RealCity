using RealCity.Patch;
using RealCity.Util;

namespace RealCity.CustomAI
{
	public class RealCityIndustryBuildingAI
	{
		public static float GetResourcePrice(TransferManager.TransferReason material) {
			int priceInt = 0;
			float price;
			switch (material) {
				case TransferManager.TransferReason.Goods:
					price = 3.5f;
					if (RealCity.reduceVehicle) {
						price *= MainDataStore.reduceCargoDiv;
					}
					price *= (UniqueFacultyAI.IncreaseByBonus(UniqueFacultyAI.FacultyBonus.Science, 100) / 100f);
					break;
				case TransferManager.TransferReason.Petrol:
					price = 3f;
					if (RealCity.reduceVehicle) {
						price *= MainDataStore.reduceCargoDiv;
					}
					price *= (UniqueFacultyAI.IncreaseByBonus(UniqueFacultyAI.FacultyBonus.Science, 100) / 100f);
					break;
				case TransferManager.TransferReason.Food:
					price = 1.5f;
					if (RealCity.reduceVehicle) {
						price *= MainDataStore.reduceCargoDiv;
					}
					price *= (UniqueFacultyAI.IncreaseByBonus(UniqueFacultyAI.FacultyBonus.Science, 100) / 100f);
					break;
				case TransferManager.TransferReason.Lumber:
					price = 2f;
					if (RealCity.reduceVehicle) {
						price *= MainDataStore.reduceCargoDiv;
					}
					price *= (UniqueFacultyAI.IncreaseByBonus(UniqueFacultyAI.FacultyBonus.Science, 100) / 100f);
					break;
				case TransferManager.TransferReason.Coal:
					price = 2.5f;
					if (RealCity.reduceVehicle) {
						price *= MainDataStore.reduceCargoDiv;
					}
					price *= (UniqueFacultyAI.IncreaseByBonus(UniqueFacultyAI.FacultyBonus.Science, 100) / 100f);
					break;
				case TransferManager.TransferReason.Shopping:
				case TransferManager.TransferReason.ShoppingB:
				case TransferManager.TransferReason.ShoppingC:
				case TransferManager.TransferReason.ShoppingD:
				case TransferManager.TransferReason.ShoppingE:
				case TransferManager.TransferReason.ShoppingH:
					price = 5f;
					if (RealCity.reduceVehicle) {
						price *= MainDataStore.reduceCargoDiv;
					}
					price *= (UniqueFacultyAI.IncreaseByBonus(UniqueFacultyAI.FacultyBonus.Science, 100) / 100f);
					break;
				case TransferManager.TransferReason.Entertainment:
				case TransferManager.TransferReason.EntertainmentB:
				case TransferManager.TransferReason.EntertainmentC:
				case TransferManager.TransferReason.EntertainmentD:
					price = 1f;
					break;
				default: IndustryBuildingGetResourcePricePatch.Prefix(ref priceInt, material); price = priceInt / 100f; break;
			}

			return price;
		}
	}
}
