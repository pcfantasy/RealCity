using System.Collections.Generic;

namespace RealCity.Util.Politic
{
	/// <summary>
	/// 政党
	/// </summary>
	public class Party : IParty
	{
		private PartyInterestData interestData;
		public string Name { get; }
		public ushort WinChance { get; private set; } = default;
		public ushort Ticket { get; } = default;
		public ushort SeatCount { get; } = default;
		public ushort Id { get; }
		public PartyType PartyType { get; }
		public IDictionary<IBill, VoteResult> BillAttitudes { get; }

		/// <summary>
		/// 政党
		/// </summary>
		/// <param name="name">名称</param>
		/// <param name="id">Id</param>
		/// <param name="interestData">民众对政党的兴趣度</param>
		/// <param name="billAttitude">政党对政策的态度</param>
		public Party(string name, ushort id, PartyType type, PartyInterestData interestData, Dictionary<IBill, VoteResult> billAttitude)
		{
			this.Name = name;
			this.Id = id;
			this.PartyType = type;
			this.interestData = interestData;
			this.BillAttitudes = billAttitude;
		}

		public PartyInterestData GetPartyInterestData()
		{
			return this.interestData;
		}

		public void AddWinChance(ushort val)
		{
			this.WinChance += val;
		}

		public void ResetWinChance()
		{
			this.WinChance = default;
		}

		public VoteResult GetBillAttitude(IBill bill)
		{
			if (this.BillAttitudes.ContainsKey(bill))
			{
				return this.BillAttitudes[bill];
			}
			// dont wanna Party know VoteResult...
			return null;
		}

		public override string ToString()
		{
			return Localization.Get(this.PartyType.ToString("G").ToUpper());
		}
	}
}
