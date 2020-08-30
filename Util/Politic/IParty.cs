using RealCity.CustomData;

namespace RealCity.Util.Politic
{
	public interface IParty
	{
		PartyInterestData GetPartyInterestData();
		void AddWinChance(ushort val);
		void ResetWinChance();
		ushort WinChance { get; }
		ushort Ticket { get; }
		ushort SeatCount { get; }
	}
}