using System.Collections.Generic;

namespace RealCity.Util.Politic
{
	public interface IParty
	{
		PartyInterestData GetPartyInterestData();
		Dictionary<IBill, VoteResult> GetBillAttitude();
		void AddWinChance(ushort val);
		void ResetWinChance();
		string Name { get; }
		ushort WinChance { get; }
		ushort Ticket { get; }
		ushort SeatCount { get; }
		ushort Id { get; }
	}
}