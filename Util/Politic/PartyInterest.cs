using ColossalFramework;
using RealCity.CustomData;
using RealCity.Util.Politic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealCity.Util.Politic
{
	/// <summary>
	/// 政党兴趣度
	/// </summary>
	class PartyInterest
	{
		private IParty party;
		private PartyInterestData partyInterestData;
		private Citizen citizen;
		private uint citizenId;
		private uint homeId;

		private ushort val = 0;
		public PartyInterest(IParty party, ref Citizen citizen, uint citizenId, uint homeId) {
			this.party = party;
			this.partyInterestData = party.GetPartyInterestData();
			this.citizen = citizen;
			this.citizenId = citizenId;
			this.homeId = homeId;
		}
		/// <summary>
		/// 计算市民对政党的兴趣度
		/// </summary>
		public void CalcInterest() {
			this.val += GetFromEducationLevel(this.citizen.EducationLevel);
			this.val += GetFromSubService(Singleton<BuildingManager>.instance.m_buildings.m_buffer[citizen.m_workBuilding].Info.m_class.m_subService);
			this.val += GetFromFamilyMoney(CitizenUnitData.familyMoney[this.homeId]);
			this.val += GetFromAgeGroup(Citizen.GetAgeGroup(this.citizen.Age));
			this.val += GetFromGender(Citizen.GetGender(this.citizenId));
		}
		/// <summary>
		/// 增加政党胜算
		/// </summary>
		public void AddPartyChance() {
			this.party.AddWinChance(this.val);
			//啊这...不加会出bug吗
			//this.val = 0;
		}
		/// <summary>
		/// 以教育背景计算对政党的兴趣度
		/// </summary>
		/// <param name="education"></param>
		/// <returns></returns>
		private ushort GetFromEducationLevel(Citizen.Education education) {
			return this.partyInterestData.EducationLevel[(int)education];
		}
		/// <summary>
		/// 以行业计算对政党的兴趣度
		/// </summary>
		/// <param name="subService"></param>
		/// <returns></returns>
		private ushort GetFromSubService(ItemClass.SubService subService) {
			return this.partyInterestData.SubService[(int)subService];
		}
		/// <summary>
		/// 以家庭财富计算对政党的兴趣度
		/// </summary>
		/// <param name="familyMoney"></param>
		/// <returns></returns>
		private ushort GetFromFamilyMoney(float familyMoney) {
			int i;
			if (familyMoney < 5000) {
				i = 0;
			} else if (familyMoney >= 20000) {
				i = 2;
			} else {
				i = 1;
			}
			return this.partyInterestData.FamilyMoney[i];
		}
		/// <summary>
		/// 以年龄计算对政党的兴趣度
		/// </summary>
		/// <param name="ageGroup"></param>
		/// <returns></returns>
		private ushort GetFromAgeGroup(Citizen.AgeGroup ageGroup) {
			return this.partyInterestData.Age[(int)ageGroup];
		}
		/// <summary>
		/// 以性别计算对政党的兴趣度
		/// </summary>
		/// <param name="gender"></param>
		/// <returns></returns>
		private ushort GetFromGender(Citizen.Gender gender) {
			return this.partyInterestData.Gender[(int)gender];
		}
	}
}
