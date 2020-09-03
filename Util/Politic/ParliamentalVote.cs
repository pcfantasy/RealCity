using ColossalFramework;
using System.Linq;

namespace RealCity.Util.Politic
{
	/// <summary>
	/// 议会投票
	/// </summary>
	public class ParliamentalVote
	{
		private IParty[] parties;


		public IBill Bill { get; }
		public AbstractVoteResult VoteResult { get; private set; }

		public ParliamentalVote(IParty[] parties, IBill bill) {
			this.parties = parties;
			this.Bill = bill;
		}

		public void Implement() {
			this.Bill.Implement();
		}


		public AbstractVoteResult Calc() {
			int agree = default;
			int disagree = default;
			int noVote = default;

			int seatCount = Politics.GetAllSeatCount();

			int residentTax = 10 - (Politics.residentTax);
			int commercialTax = 10 - (Politics.commercialTax);
			int industryTax = 10 - (Politics.industryTax);
			int benefitOffset = 10 - (Politics.benefitOffset / 5);
			int moneyOffset = 0; // money offset
			int citizenOffset = 0; // citizen offset
			int industrialBuildingOffset = 0; //industrial building offset
			int commercialBuildingOffset = 0; //commercial building offset
			VoteOffset(ref billId, ref moneyOffset, ref citizenOffset, ref industrialBuildingOffset, ref commercialBuildingOffset);

			if (seatCount == 99) {
				agree += Politics.Parties.Sum(p => {
					// 
					return p.GetBillAttitude()[this.Bill].Agree;
				});
				agree += (Politics.Parties.Length * residentTax - moneyOffset - citizenOffset);

				disagree += Politics.Parties.Sum(p => {
					return p.GetBillAttitude()[this.Bill].Disagree;
				});
				disagree -= Politics.Parties.Length * residentTax;

				noVote += Politics.Parties.Sum(p => {
					return p.GetBillAttitude()[this.Bill].Neutral;
				});
				noVote -= Politics.Parties.Length * residentTax;
			}
			AbstractVoteResult result = new VoteResult(agree, disagree, noVote);
			this.VoteResult = result;
			return result;
		}

		private void VoteOffset(ref int idex, ref int MoneyOffset, ref int citizenOffset, ref int buildingOffset, ref int commBuildingOffset) {
			//MoneyOffset;
			MoneyOffset = 0;
			//FieldInfo cashAmount;
			//cashAmount = typeof(EconomyManager).GetField("m_cashAmount", BindingFlags.NonPublic | BindingFlags.Instance);
			//long _cashAmount = (long)cashAmount.GetValue(Singleton<EconomyManager>.instance);
			long _cashAmount = Singleton<EconomyManager>.instance.GetPrivateField<long>("m_cashAmount");
			if (_cashAmount < 0) {
				MoneyOffset = -4000;
				System.Random rand = new System.Random();
				if (rand.Next(10) < 8) {
					switch (rand.Next(15)) {
						case 0:
						case 1:
						case 2:
							if (Politics.residentTax < 20) {
								idex = 0;
							} else if (Politics.benefitOffset > 0) {
								idex = 3;
							} else if (Politics.industryTax < 20) {
								idex = 6;
							} else if (Politics.commercialTax < 20) {
								idex = 4;
							}
							break;
						case 3:
						case 4:
						case 5:
							if (Politics.benefitOffset > 0) {
								idex = 3;
							} else if (Politics.industryTax < 20) {
								idex = 6;
							} else if (Politics.commercialTax < 20) {
								idex = 4;
							} else if (Politics.residentTax < 20) {
								idex = 0;
							}
							break;
						case 6:
						case 7:
						case 8:
							if (Politics.commercialTax < 20) {
								idex = 4;
							} else if (Politics.benefitOffset > 0) {
								idex = 3;
							} else if (Politics.industryTax < 20) {
								idex = 6;
							} else if (Politics.residentTax < 20) {
								idex = 0;
							} else if (Politics.commercialTax < 20) {
								idex = 4;
							}
							break;
						case 9:
						case 10:
						case 11:
							if (Politics.industryTax < 20) {
								idex = 6;
							} else if (Politics.commercialTax < 20) {
								idex = 4;
							} else if (Politics.benefitOffset > 0) {
								idex = 3;
							} else if (Politics.residentTax < 20) {
								idex = 0;
							}
							break;
						case 12:
						case 13:
						case 14:
							if (Politics.benefitOffset > 0) {
								idex = 3;
							} else if (Politics.industryTax < 20) {
								idex = 6;
							} else if (Politics.residentTax < 20) {
								idex = 0;
							} else if (Politics.commercialTax < 20) {
								idex = 4;
							}
							break;
					}
				}
				Politics.currentBillId = (byte)idex;
			} else if (_cashAmount > 24000000) {
				MoneyOffset = 4000;
			} else {
				MoneyOffset = -4000 + (int)(_cashAmount / 3000);
			}

			//citizenOffset
			int citizenOffsetBySalary = 0;
			if (MainDataStore.familyCount > 0) {
				citizenOffsetBySalary = (int)(MainDataStore.citizenSalaryPerFamily - (MainDataStore.citizenSalaryTaxTotal / MainDataStore.familyCount) - MainDataStore.citizenExpensePerFamily);
			}

			if (citizenOffsetBySalary < 100) {
				citizenOffset = 500;
			} else if (citizenOffsetBySalary > 300) {
				citizenOffset = -500;
			} else {
				citizenOffset = 1000 - 5 * citizenOffsetBySalary;
			}

			//buildingOffset
			buildingOffset = 0;
			if (industrialEarnMoneyCount + industrialLackMoneyCount > 0) {
				buildingOffset = ((int)(100f * (industrialEarnMoneyCount - industrialLackMoneyCount) / (industrialEarnMoneyCount + industrialLackMoneyCount))) << 4;
				if (buildingOffset > 1500) {
					buildingOffset = 1500;
				}

				if (buildingOffset < -1500) {
					buildingOffset = -1500;
				}
			}

			commBuildingOffset = 0;
			if (commercialEarnMoneyCount + commercialLackMoneyCount > 0) {
				commBuildingOffset = ((int)(100f * (commercialEarnMoneyCount - commercialLackMoneyCount) / (commercialEarnMoneyCount + commercialLackMoneyCount))) << 4;
				if (commBuildingOffset > 1500) {
					commBuildingOffset = 1500;
				}

				if (commBuildingOffset < -1500) {
					commBuildingOffset = -1500;
				}
			}

			industrialEarnMoneyCount = 0;
			industrialLackMoneyCount = 0;
			commercialEarnMoneyCount = 0;
			commercialLackMoneyCount = 0;
		}
	}
}
