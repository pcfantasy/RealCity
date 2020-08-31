using RealCity.CustomData;
using System.Collections.Generic;

namespace RealCity.Util.Politic
{
	public interface IParty
	{
		PartyInterestData GetPartyInterestData();
		Dictionary<IBill, VoteResult> GetBillAttitude();
		void AddWinChance(ushort val);
		void ResetWinChance();
		ushort WinChance { get; }
		ushort Ticket { get; }
		ushort SeatCount { get; }
	}
}