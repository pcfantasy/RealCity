using RealCity.CustomData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealCity.Util.Politic
{
	public class Party : IParty
	{
		// maybe some customized Bill in parliament here

		private PartyInterestData interestData;
		private Dictionary<IBill, byte> billInterestData;
		public ushort WinChance { get; private set; } = default;
		public ushort Ticket { get; } = default;
		public ushort SeatCount { get; } = default;

		public Party(PartyInterestData interestData, Dictionary<IBill, byte> billInterestData) {
			this.interestData = interestData;
			this.billInterestData = billInterestData;
		}

		public void AddWinChance(ushort val) {
			this.WinChance += val;
		}

		public PartyInterestData GetPartyInterestData() {
			return this.interestData;
		}

		public void ResetWinChance() {
			this.WinChance = default;
		}
	}
}