using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealCity.Util.Politic
{
	/// <summary>
	/// 选举
	/// </summary>
	public class Election
	{
		private Random r = new Random();
		private IParty[] parties;
		//得票计数器
		private int[] ticketCounter;
		public Election(IParty[] parties) {
			if (parties.Length <= 0)
				throw new ArgumentException();
			this.parties = parties;
			this.ticketCounter = new int[parties.Length];
		}
		/// <summary>
		/// 是否即将选举
		/// </summary>
		/// <returns></returns>
		public static bool IsOnElection() {
			return Politics.IsOnElection();
			//return Politics.nextElectionInterval == 1;
		}
		private void CalcPartiesWinChance() {

		}
		private void VoteTicket() {
			// It's like Roulette Wheel Selection
			// 大转盘，政党的WinChance越大，得票机率就越大

			int i = 0;
			int segment = this.parties[0].WinChance;
			int vote = r.Next(800 + RealCityEconomyExtension.partyTrendStrength) + 1;
			/*
			 * [     Party A     ][  P.B  ][   P.C   ]
			 *                          ^ vote       ^ length is the sum of party.WinChance
			 *                   ^ initial Segment
			 * [     Party A     ][  P.B  ][   P.C   ]
			 *                            ^ S move forward, now vote is smaller than SR, which means P.B gets a ticket
			 */

			while (i < parties.Length) {
				if (vote < segment) {
					this.ticketCounter[i]++;
					break;
				}
				segment += parties[++i].WinChance;
			}

			#region old codes
			//if (vote < Politics.cPartyChance) {
			//	Politics.cPartyTickets++;
			//} else if (vote < Politics.cPartyChance + Politics.gPartyChance) {
			//	Politics.gPartyTickets++;
			//} else if (vote < Politics.cPartyChance + Politics.gPartyChance + Politics.sPartyChance) {
			//	Politics.sPartyTickets++;
			//} else if (vote < Politics.cPartyChance + Politics.gPartyChance + Politics.sPartyChance + Politics.lPartyChance) {
			//	Politics.lPartyTickets++;
			//} else {
			//	Politics.nPartyTickets++;
			//}
			#endregion
		}
	}
}
