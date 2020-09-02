using RealCity.CustomData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealCity.Util.Politic
{
	/// <summary>
	/// 政党
	/// </summary>
	public class Party : IParty
	{
		private PartyInterestData interestData;
		private Dictionary<IBill, VoteResult> billAttitude;
		public ushort WinChance { get; private set; } = default;
		public ushort Ticket { get; } = default;
		public ushort SeatCount { get; } = default;
		public ushort Id { get; }

		/// <summary>
		/// 政党
		/// </summary>
		/// <param name="interestData">民众对政党的兴趣度</param>
		/// <param name="billAttitude">政党对政策的态度</param>
		public Party(ushort id,PartyInterestData interestData, Dictionary<IBill, VoteResult> billAttitude) {
			this.Id = id;
			this.interestData = interestData;
			this.billAttitude = billAttitude;
		}

		public PartyInterestData GetPartyInterestData() {
			return this.interestData;
		}

		public Dictionary<IBill, VoteResult> GetBillAttitude() {
			return this.billAttitude;
		}

		public void AddWinChance(ushort val) {
			this.WinChance += val;
		}

		public void ResetWinChance() {
			this.WinChance = default;
		}
	}
}