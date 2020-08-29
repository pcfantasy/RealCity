using RealCity.CustomData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealCity.Util.Politic
{
	interface IParty
	{
		PartyInterestData GetPartyInterestData();
		void AddWinChance(ushort val);
		ushort WinChance { get; }
	}
}