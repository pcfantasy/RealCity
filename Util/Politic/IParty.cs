﻿using System.Collections.Generic;

namespace RealCity.Util.Politic
{
	public interface IParty
	{
		PartyInterestData GetPartyInterestData();
		void AddWinChance(ushort val);
		void ResetWinChance();
		string Name { get; }
		ushort WinChance { get; }
		ushort Ticket { get; }
		ushort SeatCount { get; }
		ushort Id { get; }
		PartyType PartyType { get; }
		IDictionary<IBill, AbstractVoteResult> BillAttitudes { get; }
		AbstractVoteResult GetBillAttitude(IBill bill);
	}
	public enum PartyType
	{
		Communist,
		Green,
		Socialist,
		Liberal,
		National,
	}
}
