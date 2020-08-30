using RealCity.Attributes;
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
		[ArrayLength(4)] public ushort[] EducationLevel { get; private set; }
		[ArrayLength(15)] public ushort[] SubService { get; private set; }
		[ArrayLength(3)] public ushort[] FamilyMoney { get; private set; }
		[ArrayLength(3)] public ushort[] Age { get; private set; }
		[ArrayLength(2)] public ushort[] Gender { get; private set; }

		/// <summary>
		/// 政党兴趣度数据
		/// </summary>
		/// <param name="edu">长度为4的ushort[]，代表4个学历</param>
		/// <param name="service">长度为15的ushort[]，代表15种行业</param>
		/// <param name="familyMoney">长度为3的ushort[]，代表3种家庭富裕程度</param>
		/// <param name="age">长度为3的ushort[]，代表3种（有投票权的）年龄阶段</param>
		/// <param name="gender">长度为2的ushort[]，代表2种性别</param>
		public PartyInterestData(ushort[] edu, ushort[] service, ushort[] familyMoney, ushort[] age, ushort[] gender) {
			edu = EnsureDataLength(edu, EducationLevelNum);
			service = EnsureDataLength(service, SubServiceNum);
			familyMoney = EnsureDataLength(familyMoney, FamilyMoneyNum);
			age = EnsureDataLength(age, AgeNum);
			gender = EnsureDataLength(gender, GenderNum);
			this.EducationLevel = edu;
			this.SubService = service;
			this.FamilyMoney = familyMoney;
			this.Age = age;
			this.Gender = gender;
		}

		/// <summary>
		/// 修正数据长度
		/// </summary>
		/// <param name="data"></param>
		/// <param name="expectedLength"></param>
		/// <returns></returns>
		private static ushort[] EnsureDataLength(ushort[] data, byte expectedLength) {
			if (data.Length != expectedLength) {
				ushort[] correctData = new ushort[expectedLength];
				if (data.Length < expectedLength) {
					// expected: 5 3 4 5
					// actually: 5 3 4 _
					Array.Copy(data, correctData, data.Length);
					// fill the remaining part
					for (int i = data.Length; i < expectedLength; ++i) {
						data[i] = default;
					}
				} else {
					// throw the useless part
					Array.Copy(data, correctData, expectedLength);
				}
				return correctData;
			}
			return data;
		}
	}
}