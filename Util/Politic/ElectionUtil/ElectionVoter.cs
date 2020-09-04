using System;
using System.Linq;

namespace RealCity.Util.Politic.ElectionUtil
{
	/// <summary>
	/// 选民
	/// </summary>
	public class ElectionVoter
	{
		private const int MaxTrandStrength = 300;
		//private const int ChanceSum = 800;

		private Random r = new Random();

		private Citizen citizen;
		private int trandPartyIdx;
		private int trendStrength;

		public uint CitizenId { get; }
		public Citizen Citizen => this.citizen;
		public uint HomeId { get; }
		public ElectionInfo EleInfo { get; }
		public PartyInterestCalc[] Interests{ get; }
		public int[] Chance { get; }
		public ElectionVoter(uint citizenID, ref Citizen citizen, uint homeID,ElectionInfo eleInfo) {
			this.CitizenId = citizenID;
			this.citizen = citizen;
			this.HomeId = homeID;
			this.EleInfo = eleInfo;

			this.Interests = new PartyInterestCalc[this.EleInfo.PartiesCount];
			for (int i = 0; i < this.Interests.Length; i++) {
				this.Interests[i] = new PartyInterestCalc(
					this.EleInfo.Parties[i],
					ref this.citizen,
					this.CitizenId,
					this.HomeId
					);
			}
			this.Chance = new int[this.EleInfo.PartiesCount];
		}
		/// <summary>
		/// Vote to a <see cref="IParty"/> based on its interest.
		/// </summary>
		public void VoteTicket() {
			CalcChance();

			// It's Roulette Wheel Selection
			// 大转盘，政党的Chance越大，得票机率就越大
			int i = 0;
			int segment = this.Chance[0];
			// assert this.Chance.Sum() == 800 ?
			int vote = this.r.Next(this.Chance.Sum() + this.trendStrength);
			/* 
			 *       Party A         P.B       P.C
			 * |<--------------->|<------>|<-------->|
			 *                          ^ vote       ^ length is the sum of party.WinChance
			 *                   ^ initial Segment
			 * |<--------------->|<------>|<-------->|
			 *                            ^ S move forward, now vote is smaller than S, which means P.B gets 1 ticket
			 */

			while (i < this.EleInfo.Parties.Length) {
				if (vote < segment) {
					this.EleInfo.TicketCounter[i]++;
					break;
				}
				segment += this.Chance[0];
			}
		}
		private void CalcChance() {
			for (int i = 0; i < this.EleInfo.PartiesCount; i++) {
				this.Interests[i].Calc();
				this.Chance[i] = this.Interests[i].Val;
			}
			// choose a lucky party as trend party
			this.trandPartyIdx = this.r.Next(this.EleInfo.PartiesCount);
			this.trendStrength = this.r.Next(MaxTrandStrength);
			// add its chance
			this.Chance[trandPartyIdx] += this.trendStrength;
		}
	}
}
