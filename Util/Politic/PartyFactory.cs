using System.Collections.Generic;

namespace RealCity.Util.Politic
{
	/// <summary>
	/// 政党工厂
	/// </summary>
	public class PartyFactory
	{
		//TODO: Too much similar codes, need extract same parts

		/// <summary>
		/// 下一个政党的Id
		/// </summary>
		public ushort NextPartyId { get; private set; } = 0;

		public IParty MakeCParty() {
			var billAttitudeMap = new Dictionary<IBill, AbstractVoteResult>();
			AbstractVoteResult raiseResidentTax = new VoteResult(55, 40, 5);
			AbstractVoteResult reduceResidentTax = new VoteResult(40, 55, 5);
			AbstractVoteResult raiseCommercialTax = new VoteResult(80, 20, 0);
			AbstractVoteResult reduceCommercialTax = new VoteResult(20, 80, 0);
			AbstractVoteResult raiseIndustryTax = new VoteResult(20, 70, 10);
			AbstractVoteResult reduceIndustryTax = new VoteResult(70, 20, 10);
			AbstractVoteResult raiseBenefitOffset = new VoteResult(40, 50, 10);
			AbstractVoteResult reduceBenefitOffset = new VoteResult(50, 40, 10);

			billAttitudeMap.Add(Bills.RiseResidentTax, raiseResidentTax);
			billAttitudeMap.Add(Bills.ReduceResidentTax, reduceResidentTax);
			billAttitudeMap.Add(Bills.RiseCommercialTax, raiseCommercialTax);
			billAttitudeMap.Add(Bills.ReduceCommercialTax, reduceCommercialTax);
			billAttitudeMap.Add(Bills.RiseIndustryTax, raiseIndustryTax);
			billAttitudeMap.Add(Bills.ReduceIndustryTax, reduceIndustryTax);
			billAttitudeMap.Add(Bills.RiseBenefit, raiseBenefitOffset);
			billAttitudeMap.Add(Bills.ReduceResidentTax, reduceBenefitOffset);

			IParty party = new Party(
				"Communist",
				this.NextPartyId++,
				new PartyInterestData(
					new byte[4] { 30, 20, 10, 5 },
					new byte[15] { 35, 0, 20, 10, 0, 0, 35, 50, 30, 15, 25, 10, 5, 0, 0, },
					new byte[3] { 35, 10, 0 },
					new byte[3] { 15, 10, 5 },
					new byte[2] { 10, 5 }
				),
				billAttitudeMap
			);
			return party;
		}

		public IParty MakeGParty() {
			var billAttitudeMap = new Dictionary<IBill, AbstractVoteResult>();
			AbstractVoteResult raiseResidentTax = new VoteResult(10, 80, 10);
			AbstractVoteResult reduceResidentTax = new VoteResult(80, 10, 10);
			AbstractVoteResult raiseCommercialTax = new VoteResult(40, 50, 10);
			AbstractVoteResult reduceCommercialTax = new VoteResult(50, 40, 10);
			AbstractVoteResult raiseIndustryTax = new VoteResult(50, 50, 0);
			AbstractVoteResult reduceIndustryTax = new VoteResult(50, 50, 0);
			AbstractVoteResult raiseBenefitOffset = new VoteResult(70, 20, 10);
			AbstractVoteResult reduceBenefitOffset = new VoteResult(20, 70, 10);

			billAttitudeMap.Add(Bills.RiseResidentTax, raiseResidentTax);
			billAttitudeMap.Add(Bills.ReduceResidentTax, reduceResidentTax);
			billAttitudeMap.Add(Bills.RiseCommercialTax, raiseCommercialTax);
			billAttitudeMap.Add(Bills.ReduceCommercialTax, reduceCommercialTax);
			billAttitudeMap.Add(Bills.RiseIndustryTax, raiseIndustryTax);
			billAttitudeMap.Add(Bills.ReduceIndustryTax, reduceIndustryTax);
			billAttitudeMap.Add(Bills.RiseBenefit, raiseBenefitOffset);
			billAttitudeMap.Add(Bills.ReduceResidentTax, reduceBenefitOffset);

			IParty party = new Party(
				"Green",
				this.NextPartyId++,
				new PartyInterestData(
					new byte[4] { 0, 10, 20, 25 },
					new byte[15] { 0, 20, 10, 15, 20, 30, 10, 0, 5, 10, 5, 30, 35, 40, 50, },
					new byte[3] { 0, 10, 30 },
					new byte[3] { 20, 15, 10 },
					new byte[2] { 10, 15 }
				),
				billAttitudeMap
			);
			return party;
		}

		public IParty MakeSParty() {
			var billAttitudeMap = new Dictionary<IBill, AbstractVoteResult>();
			AbstractVoteResult raiseResidentTax = new VoteResult(30, 70, 0);
			AbstractVoteResult reduceResidentTax = new VoteResult(70, 30, 0);
			AbstractVoteResult raiseCommercialTax = new VoteResult(55, 40, 5);
			AbstractVoteResult reduceCommercialTax = new VoteResult(40, 55, 5);
			AbstractVoteResult raiseIndustryTax = new VoteResult(60, 30, 10);
			AbstractVoteResult reduceIndustryTax = new VoteResult(30, 60, 10);
			AbstractVoteResult raiseBenefitOffset = new VoteResult(90, 10, 0);
			AbstractVoteResult reduceBenefitOffset = new VoteResult(10, 90, 0);

			billAttitudeMap.Add(Bills.RiseResidentTax, raiseResidentTax);
			billAttitudeMap.Add(Bills.ReduceResidentTax, reduceResidentTax);
			billAttitudeMap.Add(Bills.RiseCommercialTax, raiseCommercialTax);
			billAttitudeMap.Add(Bills.ReduceCommercialTax, reduceCommercialTax);
			billAttitudeMap.Add(Bills.RiseIndustryTax, raiseIndustryTax);
			billAttitudeMap.Add(Bills.ReduceIndustryTax, reduceIndustryTax);
			billAttitudeMap.Add(Bills.RiseBenefit, raiseBenefitOffset);
			billAttitudeMap.Add(Bills.ReduceResidentTax, reduceBenefitOffset);

			IParty party = new Party(
				"Socialist",
				this.NextPartyId++,
				new PartyInterestData(
					new byte[4] { 10, 25, 30, 40 },
					new byte[15] { 35, 0, 20, 10, 0, 0, 35, 50, 30, 15, 25, 10, 5, 0, 0, },
					new byte[3] { 25, 35, 15 },
					new byte[3] { 30, 25, 20 },
					new byte[2] { 35, 40 }
				),
				billAttitudeMap
			);
			return party;
		}

		public IParty MakeLParty() {
			var billAttitudeMap = new Dictionary<IBill, AbstractVoteResult>();
			AbstractVoteResult raiseResidentTax = new VoteResult(70, 30, 0);
			AbstractVoteResult reduceResidentTax = new VoteResult(30, 70, 0);
			AbstractVoteResult raiseCommercialTax = new VoteResult(10, 90, 0);
			AbstractVoteResult reduceCommercialTax = new VoteResult(90, 10, 0);
			AbstractVoteResult raiseIndustryTax = new VoteResult(70, 30, 0);
			AbstractVoteResult reduceIndustryTax = new VoteResult(30, 70, 0);
			AbstractVoteResult raiseBenefitOffset = new VoteResult(10, 90, 0);
			AbstractVoteResult reduceBenefitOffset = new VoteResult(90, 10, 0);

			billAttitudeMap.Add(Bills.RiseResidentTax, raiseResidentTax);
			billAttitudeMap.Add(Bills.ReduceResidentTax, reduceResidentTax);
			billAttitudeMap.Add(Bills.RiseCommercialTax, raiseCommercialTax);
			billAttitudeMap.Add(Bills.ReduceCommercialTax, reduceCommercialTax);
			billAttitudeMap.Add(Bills.RiseIndustryTax, raiseIndustryTax);
			billAttitudeMap.Add(Bills.ReduceIndustryTax, reduceIndustryTax);
			billAttitudeMap.Add(Bills.RiseBenefit, raiseBenefitOffset);
			billAttitudeMap.Add(Bills.ReduceResidentTax, reduceBenefitOffset);

			IParty party = new Party(
				"Liberal",
				this.NextPartyId++,
				new PartyInterestData(
					new byte[4] { 10, 20, 30, 25 },
					new byte[15] { 35, 0, 20, 10, 0, 0, 35, 50, 30, 15, 25, 10, 5, 0, 0, },
					new byte[3] { 10, 35, 40 },
					new byte[3] { 20, 30, 40 },
					new byte[2] { 35, 35 }
				),
				billAttitudeMap
			);
			return party;
		}

		public IParty MakeNParty() {
			var billAttitudeMap = new Dictionary<IBill, AbstractVoteResult>();
			AbstractVoteResult raiseResidentTax = new VoteResult(35, 55, 10);
			AbstractVoteResult reduceResidentTax = new VoteResult(55, 35, 10);
			AbstractVoteResult raiseCommercialTax = new VoteResult(45, 45, 100);
			AbstractVoteResult reduceCommercialTax = new VoteResult(45, 45, 10);
			AbstractVoteResult raiseIndustryTax = new VoteResult(30, 70, 0);
			AbstractVoteResult reduceIndustryTax = new VoteResult(70, 30, 0);
			AbstractVoteResult raiseBenefitOffset = new VoteResult(30, 60, 10);
			AbstractVoteResult reduceBenefitOffset = new VoteResult(60, 30, 10);

			billAttitudeMap.Add(Bills.RiseResidentTax, raiseResidentTax);
			billAttitudeMap.Add(Bills.ReduceResidentTax, reduceResidentTax);
			billAttitudeMap.Add(Bills.RiseCommercialTax, raiseCommercialTax);
			billAttitudeMap.Add(Bills.ReduceCommercialTax, reduceCommercialTax);
			billAttitudeMap.Add(Bills.RiseIndustryTax, raiseIndustryTax);
			billAttitudeMap.Add(Bills.ReduceIndustryTax, reduceIndustryTax);
			billAttitudeMap.Add(Bills.RiseBenefit, raiseBenefitOffset);
			billAttitudeMap.Add(Bills.ReduceResidentTax, reduceBenefitOffset);

			IParty party = new Party(
				"National",
				this.NextPartyId++,
				new PartyInterestData(
					new byte[4] { 50, 25, 10, 5 },
					new byte[15] { 35, 0, 20, 10, 0, 0, 35, 50, 30, 15, 25, 10, 5, 0, 0, },
					new byte[3] { 30, 10, 15 },
					new byte[3] { 15, 20, 25 },
					new byte[2] { 10, 5 }
				),
				billAttitudeMap
			);
			return party;
		}
	}
}
