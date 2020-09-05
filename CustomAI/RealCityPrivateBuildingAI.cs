using ColossalFramework.Math;
using RealCity.Util;
using RealCity.UI;
using ColossalFramework;
using UnityEngine;
using System;

namespace RealCity.CustomAI
{
	public class RealCityPrivateBuildingAI
	{
		public static ushort allBuildings = 0;
		public static uint preBuidlingId = 0;

		public static int allOfficeLevel1WorkCount = 0;
		public static int allOfficeLevel2WorkCount = 0;
		public static int allOfficeLevel3WorkCount = 0;
		public static int allOfficeHighTechWorkCount = 0;

		public static int allOfficeLevel1WorkCountFinal = 0;
		public static int allOfficeLevel2WorkCountFinal = 0;
		public static int allOfficeLevel3WorkCountFinal = 0;
		public static int allOfficeHighTechWorkCountFinal = 0;
		public static long profitBuildingMoney = 0;
		public static long profitBuildingMoneyFinal = 0;
		public static ushort profitBuildingCount = 0;
		public static ushort profitBuildingCountFinal = 0;
		public static ushort allBuildingsFinal = 0;

		public static void Load(ref byte[] saveData) {
			//60
			int i = 0;
			SaveAndRestore.LoadData(ref i, saveData, ref preBuidlingId);

			SaveAndRestore.LoadData(ref i, saveData, ref allBuildings);
			SaveAndRestore.LoadData(ref i, saveData, ref allBuildingsFinal);
			SaveAndRestore.LoadData(ref i, saveData, ref allOfficeLevel1WorkCount);
			SaveAndRestore.LoadData(ref i, saveData, ref allOfficeLevel2WorkCount);
			SaveAndRestore.LoadData(ref i, saveData, ref allOfficeLevel3WorkCount);
			SaveAndRestore.LoadData(ref i, saveData, ref allOfficeHighTechWorkCount);
			SaveAndRestore.LoadData(ref i, saveData, ref allOfficeLevel1WorkCountFinal);
			SaveAndRestore.LoadData(ref i, saveData, ref allOfficeLevel2WorkCountFinal);
			SaveAndRestore.LoadData(ref i, saveData, ref allOfficeLevel3WorkCountFinal);
			SaveAndRestore.LoadData(ref i, saveData, ref allOfficeHighTechWorkCountFinal);

			SaveAndRestore.LoadData(ref i, saveData, ref profitBuildingMoney);
			SaveAndRestore.LoadData(ref i, saveData, ref profitBuildingMoneyFinal);
			SaveAndRestore.LoadData(ref i, saveData, ref profitBuildingCount);
			SaveAndRestore.LoadData(ref i, saveData, ref profitBuildingCountFinal);

			if (i != saveData.Length) {
				DebugLog.LogToFileOnly($"RealCityPrivateBuildingAI Load Error: saveData.Length = {saveData.Length} + i = {i}");
			}
		}

		public static void Save(ref byte[] saveData) {
			//60
			int i = 0;
			SaveAndRestore.SaveData(ref i, preBuidlingId, ref saveData);

			SaveAndRestore.SaveData(ref i, allBuildings, ref saveData);
			SaveAndRestore.SaveData(ref i, allBuildingsFinal, ref saveData);
			SaveAndRestore.SaveData(ref i, allOfficeLevel1WorkCount, ref saveData);
			SaveAndRestore.SaveData(ref i, allOfficeLevel2WorkCount, ref saveData);
			SaveAndRestore.SaveData(ref i, allOfficeLevel3WorkCount, ref saveData);
			SaveAndRestore.SaveData(ref i, allOfficeHighTechWorkCount, ref saveData);
			SaveAndRestore.SaveData(ref i, allOfficeLevel1WorkCountFinal, ref saveData);
			SaveAndRestore.SaveData(ref i, allOfficeLevel2WorkCountFinal, ref saveData);
			SaveAndRestore.SaveData(ref i, allOfficeLevel3WorkCountFinal, ref saveData);
			SaveAndRestore.SaveData(ref i, allOfficeHighTechWorkCountFinal, ref saveData);

			SaveAndRestore.SaveData(ref i, profitBuildingMoney, ref saveData);
			SaveAndRestore.SaveData(ref i, profitBuildingMoneyFinal, ref saveData);
			SaveAndRestore.SaveData(ref i, profitBuildingCount, ref saveData);
			SaveAndRestore.SaveData(ref i, profitBuildingCountFinal, ref saveData);

			if (i != saveData.Length) {
				DebugLog.LogToFileOnly($"RealCityPrivateBuildingAI Save Error: saveData.Length = {saveData.Length} + i = {i}");
			}
		}

		public static TransferManager.TransferReason GetIncomingProductionType(ushort buildingID, Building data) {
			RealCityIndustrialBuildingAI.InitDelegate();
			if (data.Info.m_buildingAI is IndustrialExtractorAI) {
			} else {
				switch (data.Info.m_class.m_subService) {
					case ItemClass.SubService.CommercialHigh:
					case ItemClass.SubService.CommercialLow:
					case ItemClass.SubService.CommercialEco:
					case ItemClass.SubService.CommercialLeisure:
					case ItemClass.SubService.CommercialTourist:
						CommercialBuildingAI commercialBuildingAI = data.Info.m_buildingAI as CommercialBuildingAI;
						return commercialBuildingAI.m_incomingResource;
					case ItemClass.SubService.IndustrialForestry:
					case ItemClass.SubService.IndustrialFarming:
					case ItemClass.SubService.IndustrialOil:
					case ItemClass.SubService.IndustrialOre:
					case ItemClass.SubService.IndustrialGeneric:
						return RealCityIndustrialBuildingAI.GetIncomingTransferReason((IndustrialBuildingAI)(data.Info.m_buildingAI), buildingID);
				}
			}
			return TransferManager.TransferReason.None;
		}

		public static string GetProductionType(bool isSelling, ushort buildingID, Building data) {
			RealCityIndustrialBuildingAI.InitDelegate();
			string material = "";
			if (!isSelling) {
				if (data.Info.m_buildingAI is IndustrialExtractorAI) {
				} else {
					switch (data.Info.m_class.m_subService) {
						case ItemClass.SubService.CommercialHigh:
						case ItemClass.SubService.CommercialLow:
						case ItemClass.SubService.CommercialEco:
						case ItemClass.SubService.CommercialLeisure:
						case ItemClass.SubService.CommercialTourist:
							CommercialBuildingAI commercialBuildingAI = data.Info.m_buildingAI as CommercialBuildingAI;
							switch (commercialBuildingAI.m_incomingResource) {
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
							switch (RealCityIndustrialBuildingAI.GetIncomingTransferReason((IndustrialBuildingAI)(data.Info.m_buildingAI), buildingID)) {
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
							switch (RealCityIndustrialBuildingAI.GetIncomingTransferReason((IndustrialBuildingAI)(data.Info.m_buildingAI), buildingID)) {
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
							switch (RealCityIndustrialBuildingAI.GetSecondaryIncomingTransferReason((IndustrialBuildingAI)(data.Info.m_buildingAI), buildingID)) {
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
			} else {
				if (data.Info.m_buildingAI is IndustrialExtractorAI) {
					switch (data.Info.m_class.m_subService) {
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
				} else {
					switch (data.Info.m_class.m_subService) {
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

		public static float GetPrice(bool isSelling, ushort buildingID, Building data) {
			RealCityIndustrialBuildingAI.InitDelegate();
			float price = 0f;
			if (!isSelling) {
				if (!(data.Info.m_buildingAI is IndustrialExtractorAI)) {
					switch (data.Info.m_class.m_subService) {
						case ItemClass.SubService.CommercialHigh:
						case ItemClass.SubService.CommercialLow:
						case ItemClass.SubService.CommercialEco:
						case ItemClass.SubService.CommercialLeisure:
						case ItemClass.SubService.CommercialTourist:
							CommercialBuildingAI commercialBuildingAI = data.Info.m_buildingAI as CommercialBuildingAI;
							if (commercialBuildingAI.m_incomingResource == TransferManager.TransferReason.Food || commercialBuildingAI.m_incomingResource == TransferManager.TransferReason.Goods) {
								//SecondaryIncoming : FirstIncoming = 1:3
								price = (3f * RealCityIndustryBuildingAI.GetResourcePrice(commercialBuildingAI.m_incomingResource) + (RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.LuxuryProducts))) / 4f;
							} else {
								price = RealCityIndustryBuildingAI.GetResourcePrice(commercialBuildingAI.m_incomingResource);
							}
							break;
						case ItemClass.SubService.IndustrialForestry:
						case ItemClass.SubService.IndustrialFarming:
						case ItemClass.SubService.IndustrialOil:
						case ItemClass.SubService.IndustrialOre:
							TransferManager.TransferReason tempReason = RealCityIndustrialBuildingAI.GetIncomingTransferReason((IndustrialBuildingAI)(data.Info.m_buildingAI), buildingID);
							price = RealCityIndustryBuildingAI.GetResourcePrice(tempReason);
							break;
						case ItemClass.SubService.IndustrialGeneric:
							TransferManager.TransferReason firstReason = RealCityIndustrialBuildingAI.GetIncomingTransferReason((IndustrialBuildingAI)(data.Info.m_buildingAI), buildingID);
							TransferManager.TransferReason secondReason = RealCityIndustrialBuildingAI.GetSecondaryIncomingTransferReason((IndustrialBuildingAI)(data.Info.m_buildingAI), buildingID);
							//SecondaryIncoming : FirstIncoming = 1:3
							price = (3f * RealCityIndustryBuildingAI.GetResourcePrice(firstReason) + (RealCityIndustryBuildingAI.GetResourcePrice(secondReason))) / 4f;
							break;
						default:
							price = 0; break;
					}
				}
			} else {
				if (data.Info.m_buildingAI is IndustrialExtractorAI) {
					switch (data.Info.m_class.m_subService) {
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
				} else {
					switch (data.Info.m_class.m_subService) {
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

		public static int GetTaxRate(Building data) {
			if (data.Info.m_class.m_service == ItemClass.Service.Commercial) {
				return Politics.commercialTax;
			} else if (data.Info.m_class.m_service == ItemClass.Service.Industrial) {
				return Politics.industryTax;
			}

			return 0;
		}

		public static float GetComsumptionDivider(Building data, ushort buildingID) {
			Citizen.BehaviourData behaviourData = default;
			int aliveWorkerCount = 0;
			int totalWorkerCount = 0;
			RealCityCommonBuildingAI.GetWorkBehaviour((CommonBuildingAI)data.Info.m_buildingAI, buildingID, ref data, ref behaviourData, ref aliveWorkerCount, ref totalWorkerCount);
			float comsumptionDivider = aliveWorkerCount / 8f;

			if (comsumptionDivider < 1f) {
				comsumptionDivider = 1f;
			}

			if (data.Info.m_class.m_service == ItemClass.Service.Industrial) {
				RealCityIndustrialBuildingAI.InitDelegate();
				var incomingTransferReason = RealCityIndustrialBuildingAI.GetIncomingTransferReason((IndustrialBuildingAI)data.Info.m_buildingAI, buildingID);
				//petrol related
				if (incomingTransferReason == TransferManager.TransferReason.Petrol) {
					//*2 / 4
					comsumptionDivider /= 2f;
				} else if (incomingTransferReason == TransferManager.TransferReason.Coal) {
					//*1.67 / 4
					comsumptionDivider /= 2.4f;
				} else if (incomingTransferReason == TransferManager.TransferReason.Lumber) {
					//*1.33 / 4
					comsumptionDivider /= 3f;
				} else if (incomingTransferReason == TransferManager.TransferReason.Food) {
					comsumptionDivider /= 4f;
				}
			}

			return comsumptionDivider;
		}

		public static void ProcessAdditionProduct(ushort buildingID, ref Building buildingData, ref ushort[] __state, bool is16Times = true) {
			//add production pre 16times
			byte shift = 0;
			if (is16Times) {
				shift = 4;
			}

			if ((RealCityEconomyExtension.Can16timesUpdate(buildingID)) || (!is16Times)) {
				float comsumptionDivider = GetComsumptionDivider(buildingData, buildingID);
				int deltaCustomBuffer1 = __state[0] - buildingData.m_customBuffer1;
				if (deltaCustomBuffer1 > 0) {
					if (RealCity.reduceVehicle) {
						if (!Singleton<SimulationManager>.instance.m_isNightTime) {
							buildingData.m_customBuffer1 = (ushort)(buildingData.m_customBuffer1 + deltaCustomBuffer1 - ((int)(deltaCustomBuffer1 / (comsumptionDivider * MainDataStore.reduceCargoDiv)) << shift));
						} else {
							buildingData.m_customBuffer1 = (ushort)(buildingData.m_customBuffer1 + deltaCustomBuffer1 - ((int)(deltaCustomBuffer1 / comsumptionDivider) << shift));
						}
					} else {
						if (!Singleton<SimulationManager>.instance.m_isNightTime) {
							buildingData.m_customBuffer1 = (ushort)(buildingData.m_customBuffer1 + deltaCustomBuffer1 - ((int)(deltaCustomBuffer1 / comsumptionDivider) << shift));
						} else {
							buildingData.m_customBuffer1 = (ushort)(buildingData.m_customBuffer1 + deltaCustomBuffer1 - ((int)((deltaCustomBuffer1 << 1) / comsumptionDivider) << shift));
						}
					}
				}

				int deltaCustomBuffer2 = buildingData.m_customBuffer2 - __state[1];
				if (deltaCustomBuffer2 > 0) {
					if (RealCity.reduceVehicle) {
						if (!Singleton<SimulationManager>.instance.m_isNightTime) {
							buildingData.m_customBuffer2 = (ushort)(buildingData.m_customBuffer2 - deltaCustomBuffer2 + ((deltaCustomBuffer2 >> MainDataStore.reduceCargoDivShift) << shift));
						} else {
							//NightTime 2x , reduceVehicle 1/2, so do nothing
							buildingData.m_customBuffer2 = (ushort)(buildingData.m_customBuffer2 - deltaCustomBuffer2 + (((int)(deltaCustomBuffer2 / (float)MainDataStore.reduceCargoDiv) << 1) << shift));
						}
					} else {
						if (!Singleton<SimulationManager>.instance.m_isNightTime) {
							buildingData.m_customBuffer2 = (ushort)(buildingData.m_customBuffer2 - deltaCustomBuffer2 + (deltaCustomBuffer2 << shift));
						} else {
							buildingData.m_customBuffer2 = (ushort)(buildingData.m_customBuffer2 - deltaCustomBuffer2 + ((deltaCustomBuffer2 << MainDataStore.reduceCargoDivShift) << shift));
							//buildingData.m_customBuffer2 = (ushort)(buildingData.m_customBuffer2 + (deltaCustomBuffer2 << 4));
						}
					}
				}
			} else {
				//DebugLog.LogToFileOnly($"Process buildingID outside 16times = {buildingID}");
				buildingData.m_customBuffer1 = (ushort)Mathf.Clamp(__state[0], 0, 64000);
				buildingData.m_customBuffer2 = (ushort)Mathf.Clamp(__state[1], 0, 64000);
			}
		}

		public static void ProcessAdditionProduct(ref Building buildingData, ref ushort[] __state) {
			int deltaCustomBuffer1 = buildingData.m_customBuffer1 - __state[0];
			if (deltaCustomBuffer1 > 0) {
				if (RealCity.reduceVehicle) {
					if (!Singleton<SimulationManager>.instance.m_isNightTime) {
						buildingData.m_customBuffer1 = (ushort)(buildingData.m_customBuffer1 - deltaCustomBuffer1 + (deltaCustomBuffer1 >> MainDataStore.reduceCargoDivShift));
					}
					//else
					//{
					//NightTime 2x , reduceVehicle 1/2, so do nothing
					//buildingData.m_customBuffer1 = (ushort)(buildingData.m_customBuffer1 - deltaCustomBuffer1 + (deltaCustomBuffer1 / (float)MainDataStore.reduceCargoDiv) * 2f);
					//}
				} else {
					if (!Singleton<SimulationManager>.instance.m_isNightTime) {
						//buildingData.m_customBuffer1 = (ushort)(buildingData.m_customBuffer1 - deltaCustomBuffer1 + deltaCustomBuffer1);
					} else {
						//buildingData.m_customBuffer1 = (ushort)(buildingData.m_customBuffer1 - deltaCustomBuffer1 + 2 * deltaCustomBuffer1);
						buildingData.m_customBuffer1 = (ushort)(buildingData.m_customBuffer1 + deltaCustomBuffer1);
					}
				}
			}
		}
	}
}
