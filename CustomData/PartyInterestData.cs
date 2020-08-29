using RealCity.Attributes;

namespace RealCity.CustomData
{
	class PartyInterestData
	{
		[ArrayLength(5)]
		public ushort[] EducationLevel { get; private set; }
		//[ArrayLength(14)]
		public ushort[] SubService { get; private set; }
		[ArrayLength(5)]
		public ushort[] FamilyMoney { get; private set; }
		[ArrayLength(3)]
		public ushort[] Age { get; private set; }
		[ArrayLength(2)]
		public ushort[] Gender { get; private set; }

		public PartyInterestData(ushort[] edu, ushort[] serv, ushort[] fam, ushort[] age, ushort[] gender) {
			this.EducationLevel = edu;
			this.SubService = serv;
			this.FamilyMoney = fam;
			this.Age = age;
			this.Gender = gender;
		}
	}
}
