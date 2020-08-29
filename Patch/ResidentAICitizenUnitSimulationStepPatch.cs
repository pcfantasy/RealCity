using ColossalFramework;
using HarmonyLib;
using RealCity.CustomAI;
using RealCity.CustomData;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
	[HarmonyPatch]
	public class ResidentAICitizenUnitSimulationStepPatch
	{
		public static MethodBase TargetMethod() {
			return typeof(ResidentAI).GetMethod(
				"SimulationStep",
				BindingFlags.Public | BindingFlags.Instance,
				null,
				new Type[] { typeof(uint), typeof(CitizenUnit).MakeByRefType() },
				null);
		}

		/// <summary>
		/// 处理市民
		/// </summary>
		/// <param name="homeID"></param>
		/// <param name="data"></param>
		/// <param name="isPre"></param>
		public static void ProcessCitizen(uint homeID, ref CitizenUnit data, bool isPre) {
			FieldInfo fieldInfo;
			if (isPre) {
				CitizenUnitData.familyMoney[homeID] = 0;
				for (int i = 0; i <= 4; i++) {
					fieldInfo = data.GetType().GetField($"m_citizen{i}");
					uint m_citizenI = (uint)fieldInfo.GetValue(data);
					if (m_citizenI != 0) {
						Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[m_citizenI];
						if ((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) {
							if (citizenData.Dead == false) {
								RealCityResidentAI.citizenCount++;
								CitizenUnitData.familyMoney[homeID] += CitizenData.Instance.citizenMoney[m_citizenI];
							}
						}
					}
				}
			} else {
				//
				if (CitizenUnitData.familyMoney[homeID] < MainDataStore.lowWealth) {
					RealCityResidentAI.familyWeightStableLow++;
				} else if (CitizenUnitData.familyMoney[homeID] >= MainDataStore.highWealth) {
					RealCityResidentAI.familyWeightStableHigh++;
				}

				int temp = 0;
				for (int i = 0; i <= 4; i++) {
					fieldInfo = data.GetType().GetField($"m_citizen{i}");
					uint m_citizenI = (uint)fieldInfo.GetValue(data);

					if (m_citizenI != 0) {
						Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[m_citizenI];
						if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false)) {
							++temp;
#if FASTRUN
#else
							GetVoteChance(m_citizenI, citizenData, homeID);
#endif
						}
					}
				}

				if (temp != 0) {
					for (int i = 0; i <= 4; i++) {
						fieldInfo = data.GetType().GetField($"m_citizen{i}");
						uint m_citizenI = (uint)fieldInfo.GetValue(data);

						if (m_citizenI != 0) {
							Citizen citizenData = Singleton<CitizenManager>.instance.m_citizens.m_buffer[m_citizenI];
							if (((citizenData.m_flags & Citizen.Flags.MovingIn) == Citizen.Flags.None) && (citizenData.Dead == false)) {
								CitizenData.Instance.citizenMoney[m_citizenI] = CitizenUnitData.familyMoney[homeID] / temp;
							}
						}
					}
				}
			}
		}

		public static void ProcessFamily(uint homeID, ref CitizenUnit data) {
			if (RealCityResidentAI.preCitizenId > homeID) {
				//DebugLog.LogToFileOnly("Another period started");
				MainDataStore.familyCount = RealCityResidentAI.familyCount;
				MainDataStore.citizenCount = RealCityResidentAI.citizenCount;
				MainDataStore.level2HighWealth = RealCityResidentAI.level2HighWealth;
				MainDataStore.level3HighWealth = RealCityResidentAI.level3HighWealth;
				MainDataStore.level1HighWealth = RealCityResidentAI.level1HighWealth;
				if (RealCityResidentAI.familyCount != 0) {
					MainDataStore.citizenSalaryPerFamily = ((RealCityResidentAI.citizenSalaryCount / RealCityResidentAI.familyCount));
					MainDataStore.citizenExpensePerFamily = ((RealCityResidentAI.citizenExpenseCount / RealCityResidentAI.familyCount));
				}
				MainDataStore.citizenExpense = RealCityResidentAI.citizenExpenseCount;
				MainDataStore.citizenSalaryTaxTotal = RealCityResidentAI.citizenSalaryTaxTotal;
				MainDataStore.citizenSalaryTotal = RealCityResidentAI.citizenSalaryCount;
				if (MainDataStore.familyCount < MainDataStore.familyWeightStableHigh) {
					MainDataStore.familyWeightStableHigh = (uint)MainDataStore.familyCount;
				} else {
					MainDataStore.familyWeightStableHigh = RealCityResidentAI.familyWeightStableHigh;
				}
				if (MainDataStore.familyCount < MainDataStore.familyWeightStableLow) {
					MainDataStore.familyWeightStableLow = (uint)MainDataStore.familyCount;
				} else {
					MainDataStore.familyWeightStableLow = RealCityResidentAI.familyWeightStableLow;
				}

				RealCityPrivateBuildingAI.profitBuildingMoneyFinal = RealCityPrivateBuildingAI.profitBuildingMoney;

				RealCityResidentAI.level3HighWealth = 0;
				RealCityResidentAI.level2HighWealth = 0;
				RealCityResidentAI.level1HighWealth = 0;
				RealCityResidentAI.familyCount = 0;
				RealCityResidentAI.citizenCount = 0;
				RealCityResidentAI.citizenSalaryCount = 0;
				RealCityResidentAI.citizenExpenseCount = 0;
				RealCityResidentAI.citizenSalaryTaxTotal = 0;
				RealCityResidentAI.tempCitizenSalaryTaxTotal = 0f;
				RealCityResidentAI.familyWeightStableHigh = 0;
				RealCityResidentAI.familyWeightStableLow = 0;
				RealCityPrivateBuildingAI.profitBuildingMoney = 0;
			}

			RealCityResidentAI.preCitizenId = homeID;
			RealCityResidentAI.familyCount++;

			if (homeID > 524288) {
				DebugLog.LogToFileOnly("Error: citizen ID greater than 524288");
			}

			//DebugLog.LogToFileOnly($"ProcessCitizen pre family {homeID} moneny {CitizenUnitData.familyMoney[homeID]}");
			//把家庭成员的财产汇总。gather all citizenMoney to familyMoney
			ProcessCitizen(homeID, ref data, true);
			//DebugLog.LogToFileOnly($"ProcessCitizen post family {homeID} moneny {CitizenUnitData.familyMoney[homeID]}");
			//1.We calculate citizen income
			int familySalaryCurrent = 0;
			familySalaryCurrent += RealCityResidentAI.ProcessCitizenSalary(data.m_citizen0, false);
			familySalaryCurrent += RealCityResidentAI.ProcessCitizenSalary(data.m_citizen1, false);
			familySalaryCurrent += RealCityResidentAI.ProcessCitizenSalary(data.m_citizen2, false);
			familySalaryCurrent += RealCityResidentAI.ProcessCitizenSalary(data.m_citizen3, false);
			familySalaryCurrent += RealCityResidentAI.ProcessCitizenSalary(data.m_citizen4, false);
			RealCityResidentAI.citizenSalaryCount = RealCityResidentAI.citizenSalaryCount + familySalaryCurrent;
			if (familySalaryCurrent < 0) {
				DebugLog.LogToFileOnly("familySalaryCurrent< 0 in ResidentAI");
				familySalaryCurrent = 0;
			}

			//2.We calculate salary tax
			float tax = (float)(Politics.residentTax << 1) * familySalaryCurrent / 100f;
			RealCityResidentAI.tempCitizenSalaryTaxTotal = RealCityResidentAI.tempCitizenSalaryTaxTotal + (int)tax;
			RealCityResidentAI.citizenSalaryTaxTotal = (int)RealCityResidentAI.tempCitizenSalaryTaxTotal;
			ProcessCitizenIncomeTax(homeID, tax);

			//3. We calculate expense
			int educationFee = 0;
			int hospitalFee = 0;
			int expenseRate = 0;
			CitizenManager instance = Singleton<CitizenManager>.instance;
			int tempEducationFee;
			int tempHospitalFee;
			if (data.m_citizen4 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen4)].Dead) {
				GetExpenseRate(data.m_citizen4, out expenseRate, out tempEducationFee, out tempHospitalFee);
				educationFee += tempEducationFee;
				hospitalFee += tempHospitalFee;
			}
			if (data.m_citizen3 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen3)].Dead) {
				GetExpenseRate(data.m_citizen3, out expenseRate, out tempEducationFee, out tempHospitalFee);
				educationFee += tempEducationFee;
				hospitalFee += tempHospitalFee;
			}
			if (data.m_citizen2 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen2)].Dead) {
				GetExpenseRate(data.m_citizen2, out expenseRate, out tempEducationFee, out tempHospitalFee);
				educationFee += tempEducationFee;
				hospitalFee += tempHospitalFee;
			}
			if (data.m_citizen1 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen1)].Dead) {
				GetExpenseRate(data.m_citizen1, out expenseRate, out tempEducationFee, out tempHospitalFee);
				educationFee += tempEducationFee;
				hospitalFee += tempHospitalFee;
			}
			if (data.m_citizen0 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen0)].Dead) {
				GetExpenseRate(data.m_citizen0, out expenseRate, out tempEducationFee, out tempHospitalFee);
				educationFee += tempEducationFee;
				hospitalFee += tempHospitalFee;
			}
			ProcessCitizenHouseRent(homeID, expenseRate);
			//campus DLC added.
			expenseRate = UniqueFacultyAI.IncreaseByBonus(UniqueFacultyAI.FacultyBonus.Economics, expenseRate);
			RealCityResidentAI.citizenExpenseCount += (educationFee + expenseRate + hospitalFee);

			//4. income - expense
			float incomeMinusExpense = familySalaryCurrent - tax - educationFee - expenseRate;
			CitizenUnitData.familyMoney[homeID] += incomeMinusExpense;

			//5. Limit familyMoney
			if (CitizenUnitData.familyMoney[homeID] > 100000000f) {
				CitizenUnitData.familyMoney[homeID] = 100000000f;
			}

			if (CitizenUnitData.familyMoney[homeID] < -100000000f) {
				CitizenUnitData.familyMoney[homeID] = -100000000f;
			}

			//6. Caculate minimumLivingAllowance and benefitOffset
			if (CitizenUnitData.familyMoney[homeID] < (-(Politics.benefitOffset * MainDataStore.govermentSalary) / 100f)) {
				int num = (int)(-CitizenUnitData.familyMoney[homeID]);
				CitizenUnitData.familyMoney[homeID] += num;
				MainDataStore.minimumLivingAllowance += num;
				Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)17, num, ItemClass.Service.Residential, ItemClass.SubService.None, ItemClass.Level.Level1);
			} else {
				if (Politics.benefitOffset > 0) {
					CitizenUnitData.familyMoney[homeID] += ((Politics.benefitOffset * MainDataStore.govermentSalary) / 100f);
					MainDataStore.minimumLivingAllowance += (int)((Politics.benefitOffset * MainDataStore.govermentSalary) / 100f);
					Singleton<EconomyManager>.instance.FetchResource((EconomyManager.Resource)17, (int)((Politics.benefitOffset * MainDataStore.govermentSalary) / 100f), ItemClass.Service.Residential, ItemClass.SubService.None, ItemClass.Level.Level1);
				}
			}

			var canBuyGoodMoney = MainDataStore.maxGoodPurchase * RealCityIndustryBuildingAI.GetResourcePrice(TransferManager.TransferReason.Shopping);
			var familySalaryCurrentTmp = (familySalaryCurrent > canBuyGoodMoney) ? canBuyGoodMoney : familySalaryCurrent;

			//7. Process citizen status
			if ((CitizenUnitData.familyMoney[homeID] / (canBuyGoodMoney + 1000f - familySalaryCurrentTmp)) >= 30) {
				RealCityResidentAI.level3HighWealth++;
			} else if ((CitizenUnitData.familyMoney[homeID] / (canBuyGoodMoney + 1000f - familySalaryCurrentTmp)) >= 20) {
				RealCityResidentAI.level2HighWealth++;
			} else if ((CitizenUnitData.familyMoney[homeID] / (canBuyGoodMoney + 1000f - familySalaryCurrentTmp)) >= 10) {
				RealCityResidentAI.level1HighWealth++;
			}

			//8 reduce goods
			float reducedGoods;
			if (CitizenUnitData.familyMoney[homeID] < canBuyGoodMoney)
				reducedGoods = CitizenUnitData.familyGoods[homeID] / 100f;
			else
				reducedGoods = CitizenUnitData.familyGoods[homeID] / 50f;

			CitizenUnitData.familyGoods[homeID] = (ushort)COMath.Clamp((int)(CitizenUnitData.familyGoods[homeID] - reducedGoods), 0, 60000);
			data.m_goods = (ushort)(CitizenUnitData.familyGoods[homeID] / 10f);

			//9 Buy good from outside and try move family
			if (data.m_goods == 0) {
				if ((CitizenUnitData.familyMoney[homeID] > canBuyGoodMoney) && (familySalaryCurrent > 1)) {
					uint citizenID = 0u;
					int familySize = 0;
					if (data.m_citizen4 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen4)].Dead) {
						familySize++;
						citizenID = data.m_citizen4;
						instance.m_citizens.m_buffer[citizenID].m_flags &= ~Citizen.Flags.NeedGoods;
					}
					if (data.m_citizen3 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen3)].Dead) {
						familySize++;
						citizenID = data.m_citizen3;
						instance.m_citizens.m_buffer[citizenID].m_flags &= ~Citizen.Flags.NeedGoods;
					}
					if (data.m_citizen2 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen2)].Dead) {
						familySize++;
						citizenID = data.m_citizen2;
						instance.m_citizens.m_buffer[citizenID].m_flags &= ~Citizen.Flags.NeedGoods;
					}
					if (data.m_citizen1 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen1)].Dead) {
						familySize++;
						citizenID = data.m_citizen1;
						instance.m_citizens.m_buffer[citizenID].m_flags &= ~Citizen.Flags.NeedGoods;
					}
					if (data.m_citizen0 != 0u && !instance.m_citizens.m_buffer[(int)((UIntPtr)data.m_citizen0)].Dead) {
						familySize++;
						citizenID = data.m_citizen0;
						instance.m_citizens.m_buffer[citizenID].m_flags &= ~Citizen.Flags.NeedGoods;
					}

					Singleton<ResidentAI>.instance.TryMoveFamily(citizenID, ref instance.m_citizens.m_buffer[citizenID], familySize);

					CitizenUnitData.familyGoods[homeID] = 5000;
					data.m_goods = (ushort)(CitizenUnitData.familyGoods[homeID] / 10f);
					CitizenUnitData.familyMoney[homeID] -= canBuyGoodMoney;
					MainDataStore.outsideGovermentMoney += (canBuyGoodMoney * MainDataStore.outsideGovermentProfitRatio);
					MainDataStore.outsideTouristMoney += (canBuyGoodMoney * MainDataStore.outsideCompanyProfitRatio * MainDataStore.outsideTouristSalaryProfitRatio);
				}
			}

			//把家庭财产分配给家庭成员。split all familyMoney to CitizenMoney
			ProcessCitizen(homeID, ref data, false);
		}


		public static void ProcessCitizenIncomeTax(uint homeID, float tax) {
			CitizenManager instance = Singleton<CitizenManager>.instance;
			ushort building = instance.m_units.m_buffer[(int)((UIntPtr)homeID)].m_building;
			Building buildingdata = Singleton<BuildingManager>.instance.m_buildings.m_buffer[building];
			Singleton<EconomyManager>.instance.AddPrivateIncome((int)(tax), buildingdata.Info.m_class.m_service, buildingdata.Info.m_class.m_subService, buildingdata.Info.m_class.m_level, 112333);
		}

		public static void ProcessCitizenHouseRent(uint homeID, int expenserate) {
			CitizenManager instance = Singleton<CitizenManager>.instance;
			ushort building = instance.m_units.m_buffer[(int)((UIntPtr)homeID)].m_building;
			Building buildingdata = Singleton<BuildingManager>.instance.m_buildings.m_buffer[building];
			Singleton<EconomyManager>.instance.AddPrivateIncome(expenserate * 100, buildingdata.Info.m_class.m_service, buildingdata.Info.m_class.m_subService, buildingdata.Info.m_class.m_level, 100);
		}

		public static void Prefix(uint homeID, ref CitizenUnit data) {
			if (CitizenUnitData.familyGoods[homeID] == 65535) {
				//first time
				if (data.m_goods < 6000)
					CitizenUnitData.familyGoods[homeID] = (ushort)(data.m_goods * 10);
				else
					CitizenUnitData.familyGoods[homeID] = 60000;
			}
		}

		// ResidentAI
		public static void Postfix(uint homeID, ref CitizenUnit data) {
			if ((Singleton<BuildingManager>.instance.m_buildings.m_buffer[data.m_building].m_flags & (Building.Flags.Completed | Building.Flags.Upgrading)) != Building.Flags.None) {
				ProcessFamily(homeID, ref data);
			}
		}

		public static void GetExpenseRate(uint citizenID, out int incomeAccumulation, out int educationFee, out int hospitalFee) {
			BuildingManager instance1 = Singleton<BuildingManager>.instance;
			CitizenManager instance2 = Singleton<CitizenManager>.instance;
			var buildingID = Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenID].m_homeBuilding;
			incomeAccumulation = BuildingData.buildingWorkCount[buildingID];

			educationFee = 0;
			hospitalFee = 0;
			if ((Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenID].m_flags & Citizen.Flags.Student) != Citizen.Flags.None) {
				//Only university will cost money
				bool isCampusDLC = false;
				//Campus DLC cost 50
				ushort visitBuilding = Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenID].m_visitBuilding;
				if (visitBuilding != 0u) {
					Building buildingData = Singleton<BuildingManager>.instance.m_buildings.m_buffer[visitBuilding];
					if (buildingData.Info.m_class.m_service == ItemClass.Service.PlayerEducation) {
						var tempEducationFee = (uint)((MainDataStore.govermentSalary) / 100f);
						if (tempEducationFee < 1)
							tempEducationFee = 1;

						educationFee = (int)tempEducationFee * 100;
						isCampusDLC = true;
					}
				}

				if (!isCampusDLC) {
					if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenID].m_flags.IsFlagSet(Citizen.Flags.Education2)) {
						educationFee = MainDataStore.govermentSalary >> 1;
						Singleton<EconomyManager>.instance.AddPrivateIncome(educationFee, ItemClass.Service.Education, ItemClass.SubService.None, ItemClass.Level.Level3, 115333);
					} else if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenID].m_flags.IsFlagSet(Citizen.Flags.Education1)) {
						educationFee = MainDataStore.govermentSalary >> 2;
						Singleton<EconomyManager>.instance.AddPrivateIncome(educationFee, ItemClass.Service.Education, ItemClass.SubService.None, ItemClass.Level.Level2, 115333);
					} else {
						educationFee = MainDataStore.govermentSalary >> 2;
						Singleton<EconomyManager>.instance.AddPrivateIncome(educationFee, ItemClass.Service.Education, ItemClass.SubService.None, ItemClass.Level.Level1, 115333);
					}
				}
			}

			if (Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenID].Sick) {
				ushort visitBuilding = Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenID].m_visitBuilding;
				if (visitBuilding != 0u) {
					Building buildingData = Singleton<BuildingManager>.instance.m_buildings.m_buffer[visitBuilding];
					if (visitBuilding != Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizenID].m_workBuilding) {
						if (buildingData.Info.m_class.m_service == ItemClass.Service.HealthCare) {
							hospitalFee = MainDataStore.govermentSalary >> 1;
							Singleton<EconomyManager>.instance.AddPrivateIncome(hospitalFee, ItemClass.Service.HealthCare, ItemClass.SubService.None, ItemClass.Level.Level2, 115333);
						}
					}
				}
			}
		}

		public static void GetVoteTickets() {
			System.Random rand = new System.Random();
			if (Politics.cPartyChance + Politics.gPartyChance + Politics.sPartyChance + Politics.lPartyChance + Politics.nPartyChance != (800 + RealCityEconomyExtension.partyTrendStrength)) {
				if (rand.Next(64) <= 1) {
					DebugLog.LogToFileOnly($"Error: GetVoteTickets Chance is not equal 800 {(Politics.cPartyChance + Politics.gPartyChance + Politics.sPartyChance + Politics.lPartyChance + Politics.nPartyChance)}");
				}
			}

			int voteRandom = rand.Next(800 + RealCityEconomyExtension.partyTrendStrength) + 1;
			if (voteRandom < Politics.cPartyChance) {
				Politics.cPartyTickets++;
			} else if (voteRandom < Politics.cPartyChance + Politics.gPartyChance) {
				Politics.gPartyTickets++;
			} else if (voteRandom < Politics.cPartyChance + Politics.gPartyChance + Politics.sPartyChance) {
				Politics.sPartyTickets++;
			} else if (voteRandom < Politics.cPartyChance + Politics.gPartyChance + Politics.sPartyChance + Politics.lPartyChance) {
				Politics.lPartyTickets++;
			} else {
				Politics.nPartyTickets++;
			}
		}

		public static void GetVoteChance(uint citizenID, Citizen citizen, uint homeID) {
			//达到最低投票年龄，而且即将选举 if (elder than Vote Age) and (gonna be election)
			if (Politics.IsOverVotingAge(Citizen.GetAgeGroup(citizen.m_age))
				&& Politics.IsOnElection()) {

				//重置机率
				Politics.cPartyChance = 0;
				Politics.gPartyChance = 0;
				Politics.sPartyChance = 0;
				Politics.lPartyChance = 0;
				Politics.nPartyChance = 0;


				Politics.cPartyChance += (ushort)(Politics.education[(int)citizen.EducationLevel, 0] << 1);
				Politics.gPartyChance += (ushort)(Politics.education[(int)citizen.EducationLevel, 1] << 1);
				Politics.sPartyChance += (ushort)(Politics.education[(int)citizen.EducationLevel, 2] << 1);
				Politics.lPartyChance += (ushort)(Politics.education[(int)citizen.EducationLevel, 3] << 1);
				Politics.nPartyChance += (ushort)(Politics.education[(int)citizen.EducationLevel, 4] << 1);


				int choiceIndex = 14;
				//根据工作地点决定投票策略
				if (RealCityResidentAI.IsGoverment(citizen.m_workBuilding)) {
					choiceIndex = 0;
				}
				switch (Singleton<BuildingManager>.instance.m_buildings.m_buffer[citizen.m_workBuilding].Info.m_class.m_subService) {
					case ItemClass.SubService.CommercialLow:
					case ItemClass.SubService.CommercialHigh:
						if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[citizen.m_workBuilding].Info.m_class.m_level == ItemClass.Level.Level1) {
							choiceIndex = 1;
						} else if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[citizen.m_workBuilding].Info.m_class.m_level == ItemClass.Level.Level2) {
							choiceIndex = 2;
						} else if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[citizen.m_workBuilding].Info.m_class.m_level == ItemClass.Level.Level3) {
							choiceIndex = 3;
						}
						break;
					case ItemClass.SubService.CommercialTourist:
					case ItemClass.SubService.CommercialLeisure:
						choiceIndex = 4; break;
					case ItemClass.SubService.CommercialEco:
						choiceIndex = 5; break;
					case ItemClass.SubService.IndustrialGeneric:
						if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[citizen.m_workBuilding].Info.m_class.m_level == ItemClass.Level.Level1) {
							choiceIndex = 6;
						} else if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[citizen.m_workBuilding].Info.m_class.m_level == ItemClass.Level.Level2) {
							choiceIndex = 7;
						} else if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[citizen.m_workBuilding].Info.m_class.m_level == ItemClass.Level.Level3) {
							choiceIndex = 8;
						}
						break;
					case ItemClass.SubService.IndustrialFarming:
					case ItemClass.SubService.IndustrialForestry:
					case ItemClass.SubService.IndustrialOil:
					case ItemClass.SubService.IndustrialOre:
						choiceIndex = 9; break;
					case ItemClass.SubService.OfficeGeneric:
						if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[citizen.m_workBuilding].Info.m_class.m_level == ItemClass.Level.Level1) {
							choiceIndex = 10;
						} else if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[citizen.m_workBuilding].Info.m_class.m_level == ItemClass.Level.Level2) {
							choiceIndex = 11;
						} else if (Singleton<BuildingManager>.instance.m_buildings.m_buffer[citizen.m_workBuilding].Info.m_class.m_level == ItemClass.Level.Level3) {
							choiceIndex = 12;
						}
						break;
					case ItemClass.SubService.OfficeHightech:
						choiceIndex = 13; break;
				}
				if (choiceIndex < 0 || choiceIndex > 14) {
					DebugLog.LogToFileOnly($"Error: GetVoteChance workplace idex {choiceIndex}");
				}
				Politics.cPartyChance += (ushort)(Politics.workplace[choiceIndex, 0] << 1);
				Politics.gPartyChance += (ushort)(Politics.workplace[choiceIndex, 1] << 1);
				Politics.sPartyChance += (ushort)(Politics.workplace[choiceIndex, 2] << 1);
				Politics.lPartyChance += (ushort)(Politics.workplace[choiceIndex, 3] << 1);
				Politics.nPartyChance += (ushort)(Politics.workplace[choiceIndex, 4] << 1);


				if (CitizenUnitData.familyMoney[homeID] < 5000) {
					choiceIndex = 0;
				} else if (CitizenUnitData.familyMoney[homeID] >= 20000) {
					choiceIndex = 2;
				} else {
					choiceIndex = 1;
				}
				if (choiceIndex < 0 || choiceIndex > 3) {
					DebugLog.LogToFileOnly($"Error: GetVoteChance Invaid money idex = {choiceIndex}");
				}
				Politics.cPartyChance += (ushort)(Politics.money[choiceIndex, 0] << 1);
				Politics.gPartyChance += (ushort)(Politics.money[choiceIndex, 1] << 1);
				Politics.sPartyChance += (ushort)(Politics.money[choiceIndex, 2] << 1);
				Politics.lPartyChance += (ushort)(Politics.money[choiceIndex, 3] << 1);
				Politics.nPartyChance += (ushort)(Politics.money[choiceIndex, 4] << 1);


				int temp = (int)Citizen.GetAgeGroup(citizen.m_age) - 2;
				if (temp < 0) {
					DebugLog.LogToFileOnly($"Error: GetVoteChance temp = {temp} < 0, GetAgeGroup = {Citizen.GetAgeGroup(citizen.m_age)}");
				}
				Politics.cPartyChance += Politics.age[temp, 0];
				Politics.gPartyChance += Politics.age[temp, 1];
				Politics.sPartyChance += Politics.age[temp, 2];
				Politics.lPartyChance += Politics.age[temp, 3];
				Politics.nPartyChance += Politics.age[temp, 4];


				temp = (int)Citizen.GetGender(citizenID);
				Politics.cPartyChance += Politics.gender[temp, 0];
				Politics.gPartyChance += Politics.gender[temp, 1];
				Politics.sPartyChance += Politics.gender[temp, 2];
				Politics.lPartyChance += Politics.gender[temp, 3];
				Politics.nPartyChance += Politics.gender[temp, 4];


				if (RealCityEconomyExtension.partyTrend == 0) {
					Politics.cPartyChance += RealCityEconomyExtension.partyTrendStrength;
				} else if (RealCityEconomyExtension.partyTrend == 1) {
					Politics.gPartyChance += RealCityEconomyExtension.partyTrendStrength;
				} else if (RealCityEconomyExtension.partyTrend == 2) {
					Politics.sPartyChance += RealCityEconomyExtension.partyTrendStrength;
				} else if (RealCityEconomyExtension.partyTrend == 3) {
					Politics.lPartyChance += RealCityEconomyExtension.partyTrendStrength;
				} else if (RealCityEconomyExtension.partyTrend == 4) {
					Politics.nPartyChance += RealCityEconomyExtension.partyTrendStrength;
				} else {
					DebugLog.LogToFileOnly($"Error: GetVoteChance Invalid partyTrend = {RealCityEconomyExtension.partyTrend}");
				}

				GetVoteTickets();
			}
		}
	}
}
