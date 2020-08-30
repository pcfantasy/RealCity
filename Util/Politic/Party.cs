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
		private List<IBill> bills;
		public ushort WinChance { get; private set; } = default;
		public ushort Ticket { get; } = default;
		public ushort SeatCount { get; } = default;

		public Party(PartyInterestData interestData, List<IBill> bills) {
			this.interestData = interestData;
			this.bills = bills;
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