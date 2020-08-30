﻿using RealCity.Attributes;
using RealCity.Util;
using System;

namespace RealCity.CustomData
{
	/// <summary>
	/// 政党兴趣度数据
	/// </summary>
	public class PartyInterestData
	{
		private static byte EducationLevelNum { get; } = 4;
		private static byte SubServiceNum { get; } = 15;
		private static byte FamilyMoneyNum { get; } = 3;
		private static byte AgeNum { get; } = 3;
		private static byte GenderNum { get; } = 2;
		[ArrayLength(4)] public byte[] EducationLevel { get; private set; }
		[ArrayLength(15)] public byte[] SubService { get; private set; }
		[ArrayLength(3)] public byte[] FamilyMoney { get; private set; }
		[ArrayLength(3)] public byte[] Age { get; private set; }
		[ArrayLength(2)] public byte[] Gender { get; private set; }

		/// <summary>
		/// 政党兴趣度数据
		/// </summary>
		/// <param name="edu">长度为4的数组，代表4个学历</param>
		/// <param name="service">长度为15的数组，代表15种行业</param>
		/// <param name="familyMoney">长度为3的数组，代表3种家庭富裕程度</param>
		/// <param name="age">长度为3的数组，代表3种（有投票权的）年龄阶段</param>
		/// <param name="gender">长度为2的数组，代表2种性别</param>
		public PartyInterestData(byte[] edu, byte[] service, byte[] familyMoney, byte[] age, byte[] gender) {
			edu = edu.EnsureLength(EducationLevelNum);
			service = service.EnsureLength(SubServiceNum);
			familyMoney = familyMoney.EnsureLength(FamilyMoneyNum);
			age = age.EnsureLength(AgeNum);
			gender = gender.EnsureLength(GenderNum);
			this.EducationLevel = edu;
			this.SubService = service;
			this.FamilyMoney = familyMoney;
			this.Age = age;
			this.Gender = gender;
		}
	}
}