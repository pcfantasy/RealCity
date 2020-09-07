﻿using ColossalFramework;
using RealCity.CustomAI;
using RealCity.CustomData;

namespace RealCity.Util.Politic.ElectionUtil
{
	/// <summary>
	/// 政党兴趣度计算
	/// </summary>
	public class PartyInterestCalc
	{
		private PartyInterestData partyInterestData;
		private Citizen citizen;
		private uint citizenId;
		private uint homeId;

		public IParty Party { get; }
		public ushort Val { get; private set; } = 0;

		/// <summary>
		/// 政党兴趣度计算
		/// </summary>
		/// <param name="party">政党</param>
		/// <param name="citizen">市民</param>
		/// <param name="citizenId">市民Id</param>
		/// <param name="homeId">家庭Id</param>
		public PartyInterestCalc(IParty party, ref Citizen citizen, uint citizenId, uint homeId)
		{
			this.Party = party;
			this.partyInterestData = party.GetPartyInterestData();
			this.citizen = citizen;
			this.citizenId = citizenId;
			this.homeId = homeId;
		}

		/// <summary>
		/// 计算市民对政党的兴趣度
		/// </summary>
		public void Calc()
		{
			this.Val += GetFromEducationLevel(this.citizen.EducationLevel);
			this.Val += GetFromSubService(this.citizen.m_workBuilding);
			this.Val += GetFromFamilyMoney(CitizenUnitData.familyMoney[this.homeId]);
			this.Val += GetFromAgeGroup(Citizen.GetAgeGroup(this.citizen.Age));
			this.Val += GetFromGender(Citizen.GetGender(this.citizenId));
		}

		/// <summary>
		/// 以教育背景计算对政党的兴趣度
		/// </summary>
		/// <param name="education"></param>
		/// <returns></returns>
		private ushort GetFromEducationLevel(Citizen.Education education)
		{
			/*
			 * 根据ResidentAICitizenUnitSimulationStepPatch.GetVoteTickets()方法
			 * 似乎所有政党的WinChance加起来应该是800
			 * 而同一个类里的GetVoteChance()也曾把Politics.education[i,j]的参数乘2
			 * 这地方要问一下作者...
			*/
			ushort val = (ushort)(this.partyInterestData.EducationLevel[(int)education] << 1);
			return val;
		}

		/// <summary>
		/// 以行业计算对政党的兴趣度
		/// </summary>
		/// <param name="workplaceId"></param>
		/// <returns></returns>
		private ushort GetFromSubService(ushort workplaceId)
		{
			//默认市民是没有工作的
			int choiceIndex = 0;
			//自定义行业：在政府工作
			if (RealCityResidentAI.IsGoverment(workplaceId))
			{
				choiceIndex = 1;
			}
			else
			{
				ItemClass workplaceItemClass = Singleton<BuildingManager>.instance
					.m_buildings.m_buffer[workplaceId].Info.m_class;
				//其他游戏内置行业
				switch (workplaceItemClass.m_subService)
				{
					case ItemClass.SubService.CommercialLow:
					case ItemClass.SubService.CommercialHigh:
						if (workplaceItemClass.m_level == ItemClass.Level.Level1)
						{
							choiceIndex = 2;
						}
						else if (workplaceItemClass.m_level == ItemClass.Level.Level2)
						{
							choiceIndex = 3;
						}
						else if (workplaceItemClass.m_level == ItemClass.Level.Level3)
						{
							choiceIndex = 4;
						}
						break;
					case ItemClass.SubService.CommercialTourist:
					case ItemClass.SubService.CommercialLeisure:
						choiceIndex = 5; break;
					case ItemClass.SubService.CommercialEco:
						choiceIndex = 6; break;
					case ItemClass.SubService.IndustrialGeneric:
						if (workplaceItemClass.m_level == ItemClass.Level.Level1)
						{
							choiceIndex = 7;
						}
						else if (workplaceItemClass.m_level == ItemClass.Level.Level2)
						{
							choiceIndex = 8;
						}
						else if (workplaceItemClass.m_level == ItemClass.Level.Level3)
						{
							choiceIndex = 9;
						}
						break;
					case ItemClass.SubService.IndustrialFarming:
					case ItemClass.SubService.IndustrialForestry:
					case ItemClass.SubService.IndustrialOil:
					case ItemClass.SubService.IndustrialOre:
						choiceIndex = 10; break;
					case ItemClass.SubService.OfficeGeneric:
						if (workplaceItemClass.m_level == ItemClass.Level.Level1)
						{
							choiceIndex = 11;
						}
						else if (workplaceItemClass.m_level == ItemClass.Level.Level2)
						{
							choiceIndex = 12;
						}
						else if (workplaceItemClass.m_level == ItemClass.Level.Level3)
						{
							choiceIndex = 13;
						}
						break;
					case ItemClass.SubService.OfficeHightech:
						choiceIndex = 14; break;
				}
			}
			return this.partyInterestData.SubService[choiceIndex];
		}

		/// <summary>
		/// 以家庭财富计算对政党的兴趣度
		/// </summary>
		/// <param name="familyMoney"></param>
		/// <returns></returns>
		private ushort GetFromFamilyMoney(float familyMoney)
		{
			int choiceIndex;
			if (familyMoney < 5000)
			{
				choiceIndex = 0;
			}
			else if (familyMoney >= 20000)
			{
				choiceIndex = 2;
			}
			else
			{
				choiceIndex = 1;
			}
			return this.partyInterestData.FamilyMoney[choiceIndex];
		}

		/// <summary>
		/// 以年龄计算对政党的兴趣度
		/// </summary>
		/// <param name="ageGroup"></param>
		/// <returns></returns>
		private ushort GetFromAgeGroup(Citizen.AgeGroup ageGroup)
		{
			return this.partyInterestData.Age[(int)ageGroup - 2];
		}

		/// <summary>
		/// 以性别计算对政党的兴趣度
		/// </summary>
		/// <param name="gender"></param>
		/// <returns></returns>
		private ushort GetFromGender(Citizen.Gender gender)
		{
			return this.partyInterestData.Gender[(int)gender];
		}
	}
}
