using RealCity.Util.Politic.Bill;
using System.Linq;

namespace RealCity.Util.Politic
{
	/// <summary>
	/// 会议
	/// </summary>
	public class GovernmentalMeeting : IGovernmentalMeeting
	{
		private IGovernment gov;
		private IParty[] parties;

		public IBill Bill { get; }
		public AbstractVoteResult VoteResult { get; private set; }

		public GovernmentalMeeting(IGovernment gov, IBill bill)
		{
			this.gov = gov;
			this.parties = gov.Parties;
			this.Bill = bill;
		}

		public void Start()
		{
			AbstractVoteResult r = new VoteResult();

			int seatCount = this.gov.AllSeatCount;
			int resiTaxOffset = 10 - (Politics.residentTax);
			int commTaxOffset = 10 - (Politics.commercialTax);
			int induTaxOffset = 10 - (Politics.industryTax);
			int beneOffset = 10 - (Politics.benefitOffset / 5);
			int moneyOffset = 0; // money offset
			int citizenOffset = 0; // citizen offset
			int industrialBuildingOffset = 0; //industrial building offset
			int commercialBuildingOffset = 0; //commercial building offset

			VoteOffset(ref moneyOffset, ref citizenOffset, ref industrialBuildingOffset, ref commercialBuildingOffset);

			r.AppendChange(
				+this.parties.Sum(p => p.BillAttitudes[this.Bill].Agree),
				+this.parties.Sum(p => p.BillAttitudes[this.Bill].Disagree),
				+this.parties.Sum(p => p.BillAttitudes[this.Bill].Neutral)
			);

			// offset the value of agree by the class of IBill
			// bad codes
			if (this.Bill is RiseResidentTaxBill)
			{
				//agree += resiTaxOffset * this.parties.Length;
				//agree -= moneyOffset;
				//agree -= citizenOffset;
				//disagree -= resiTaxOffset * this.parties.Length;
				r.AppendChange(
					+(resiTaxOffset * this.parties.Length - moneyOffset - citizenOffset),
					-(resiTaxOffset * this.parties.Length)
					);
			}
			else if (this.Bill is ReduceResidentTaxBill)
			{
				//agree -= resiTaxOffset * this.parties.Length;
				//agree += moneyOffset;
				//agree += citizenOffset;
				//disagree += resiTaxOffset * this.parties.Length;
				r.AppendChange(
					-(resiTaxOffset * this.parties.Length - moneyOffset - citizenOffset),
					+(resiTaxOffset * this.parties.Length)
					);
			}
			else if (this.Bill is RiseCommercialTaxBill)
			{
				//agree += commTaxOffset * this.parties.Length;
				//agree -= moneyOffset;
				//agree += commercialBuildingOffset;
				//disagree -= commTaxOffset * this.parties.Length;
				r.AppendChange(
					+(commTaxOffset * this.parties.Length - moneyOffset + commercialBuildingOffset),
					-(resiTaxOffset * this.parties.Length)
					);
			}
			else if (this.Bill is ReduceCommercialTaxBill)
			{
				//agree -= commTaxOffset * this.parties.Length;
				//agree += moneyOffset;
				//agree -= commercialBuildingOffset;
				//disagree += commTaxOffset * this.parties.Length;
				r.AppendChange(
					-(commTaxOffset * this.parties.Length - moneyOffset + commercialBuildingOffset),
					+(resiTaxOffset * this.parties.Length)
					);
			}
			else if (this.Bill is RiseIndustryTaxBill)
			{
				//agree += induTaxOffset * this.parties.Length;
				//agree -= moneyOffset;
				//agree += industrialBuildingOffset;
				//disagree -= induTaxOffset * this.parties.Length;
				r.AppendChange(
					+(induTaxOffset * this.parties.Length - moneyOffset + industrialBuildingOffset),
					-(induTaxOffset * this.parties.Length)
					);
			}
			else if (this.Bill is ReduceIndustryTaxBill)
			{
				//agree -= induTaxOffset * this.parties.Length;
				//agree += moneyOffset;
				//agree -= industrialBuildingOffset;
				//disagree += induTaxOffset * this.parties.Length;
				r.AppendChange(
					-(induTaxOffset * this.parties.Length - moneyOffset + industrialBuildingOffset),
					+(induTaxOffset * this.parties.Length)
					);
			}
			else if (this.Bill is RiseBenefitBill)
			{
				//agree += beneOffset * this.parties.Length;
				//agree += moneyOffset;
				//disagree -= beneOffset * this.parties.Length;
				r.AppendChange(
					+(beneOffset * this.parties.Length + moneyOffset),
					-(beneOffset * this.parties.Length)
					);
			}
			else if (this.Bill is ReduceBenefitBill)
			{
				//agree -= beneOffset * this.parties.Length;
				//agree -= moneyOffset;
				//disagree += beneOffset * this.parties.Length;
				r.AppendChange(
					-(beneOffset * this.parties.Length + moneyOffset),
					+(beneOffset * this.parties.Length)
					);
			}
			this.VoteResult = r;
		}

		private void VoteOffset(ref int moneyOffset, ref int citizenOffset, ref int buildingOffset, ref int commBuildingOffset)
		{
			//moenyOffset
			//FieldInfo cashAmountField = typeof(EconomyManager).GetField("m_cashAmount", BindingFlags.NonPublic | BindingFlags.Instance);
			//long cashAmount = (long)cashAmountField.GetValue(Singleton<EconomyManager>.instance);
			long cashAmount = EconomyManager.instance.GetPrivateField<long>("m_cashAmount");
			if (cashAmount < 0)
			{
				moneyOffset = -4000;
			}
			else if (cashAmount > 24000000)
			{ // 2.4e7
				moneyOffset = 4000;
			}
			else
			{
				moneyOffset = -4000 + (int)(cashAmount / 3000); // equals 0 when cashAmount = 1.2e7
			}


			//citizenOffset
			int citizenOffsetBySalary = 0;
			if (MainDataStore.familyCount > 0)
			{
				//citizenOffsetBySalary is the salary of its family excluding the tax and expense.
				//citizenOffsetBySalary是家庭工资减去赋税和开销的值
				citizenOffsetBySalary = (int)(MainDataStore.citizenSalaryPerFamily - (MainDataStore.citizenSalaryTaxTotal / MainDataStore.familyCount) - MainDataStore.citizenExpensePerFamily);
			}
			if (citizenOffsetBySalary < 100)
			{
				citizenOffset = 500;
			}
			else if (citizenOffsetBySalary > 300)
			{
				citizenOffset = -500;
			}
			else
			{
				citizenOffset = 1000 - 5 * citizenOffsetBySalary;
			}


			//buildingOffset
			buildingOffset = 0;
			// if have industrial buildings
			if (RealCityEconomyExtension.industrialEarnMoneyCount + RealCityEconomyExtension.industrialLackMoneyCount > 0)
			{
				buildingOffset = (
					(int)(100f * (RealCityEconomyExtension.industrialEarnMoneyCount - RealCityEconomyExtension.industrialLackMoneyCount)
					/ (RealCityEconomyExtension.industrialEarnMoneyCount + RealCityEconomyExtension.industrialLackMoneyCount))
					) << 4;
				if (buildingOffset > 1500)
				{
					buildingOffset = 1500;
				}

				if (buildingOffset < -1500)
				{
					buildingOffset = -1500;
				}
			}

			commBuildingOffset = 0;
			// if have commercial buildings
			if (RealCityEconomyExtension.commercialEarnMoneyCount + RealCityEconomyExtension.commercialLackMoneyCount > 0)
			{
				commBuildingOffset = ((int)(100f * (RealCityEconomyExtension.commercialEarnMoneyCount - RealCityEconomyExtension.commercialLackMoneyCount) / (RealCityEconomyExtension.commercialEarnMoneyCount + RealCityEconomyExtension.commercialLackMoneyCount))) << 4;
				if (commBuildingOffset > 1500)
				{
					commBuildingOffset = 1500;
				}

				if (commBuildingOffset < -1500)
				{
					commBuildingOffset = -1500;
				}
			}
		}
	}
}
