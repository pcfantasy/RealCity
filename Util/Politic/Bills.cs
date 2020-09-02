using System;
using System.Linq;
using RealCity.Util.Politic.Bill;

namespace RealCity.Util.Politic
{
	public static class Bills
	{

		public readonly static IBill RiseResidentTax = new RiseResidentTaxBill(1);
		public readonly static IBill ReduceResidentTax = new ReduceResidentTaxBill(1);

		public readonly static IBill RiseCommercialTax = new RiseCommercialTaxBill(1);
		public readonly static IBill ReduceCommercialTax = new ReduceCommercialTaxBill(1);

		public readonly static IBill RiseIndustryTax = new RiseIndustryTaxBill(1);
		public readonly static IBill ReduceIndustryTax = new ReduceIndustryTaxBill(1);

		public readonly static IBill RiseBenefit = new RiseBenefitBill(10);
		public readonly static IBill ReduceBenefit = new ReduceBenefitBill(10);


		//这里需要null object吗 ？

		private readonly static IBill[] AllBills = {
			RiseResidentTax,
			ReduceResidentTax,
			RiseCommercialTax,
			ReduceCommercialTax,
			RiseIndustryTax,
			ReduceIndustryTax,
			RiseBenefit,
			ReduceBenefit,
			// must pair the Rise and Reduce bills,
			// which is NOT elegant
		};

		private readonly static string[] AllBillsNameStr = {
			nameof(RiseResidentTax),
			nameof(ReduceResidentTax),
			nameof(RiseCommercialTax),
			nameof(ReduceCommercialTax),
			nameof(RiseIndustryTax),
			nameof(ReduceIndustryTax),
			nameof(RiseBenefit),
			nameof(ReduceBenefit),
		};

		public static IBill GetReversedBill(IBill bill) {
			//string revStr;
			//int index = Array.IndexOf(AllBills, bill);
			//// even = rise, odd = reduce
			//if ((index & 1) == 1) {
			//	revStr = AllBillsNameStr[index - 1];
			//} else {
			//	revStr = AllBillsNameStr[index + 1];
			//}
			//var ms = typeof(Bills).GetField(revStr, System.Reflection.BindingFlags.Static);
			//return ms.GetValue(null) as IBill;
			IBill revBill;
			int index = Array.IndexOf(AllBills, bill);
			// even = rise, odd = reduce
			if ((index & 1) == 1) {
				revBill = AllBills[index - 1];
			} else {
				revBill = AllBills[index + 1];
			}
			return revBill;
		}

		public static IBill GetAnotherBill(IBill bill) {
			int idx = Array.IndexOf(AllBills, bill);
			int[] avoidIdxs = new int[AllBills.Length >> 1];
			int i = 0;
			// bad codes
			if (Politics.CanRiseResidentTax == false) {
				avoidIdxs[i] = 0;
			} else if (Politics.CanReduceResidentTax == false) {
				avoidIdxs[i] = 1;
			}
			++i;
			if (Politics.CanRiseCommercialTax == false) {
				avoidIdxs[i] = 2;
			} else if (Politics.CanReduceCommercialTax == false) {
				avoidIdxs[i] = 3;
			}
			++i;
			if (Politics.CanRiseIndustryTax == false) {
				avoidIdxs[i] = 4;
			} else if (Politics.CanReduceIndustryTax == false) {
				avoidIdxs[i] = 5;
			}
			++i;
			if (Politics.CanRiseBenefit == false) {
				avoidIdxs[i] = 6;
			} else if (Politics.CanReduceBenefit == false) {
				avoidIdxs[i] = 7;
			}

			Random r = new Random();
			for (; avoidIdxs.Contains(idx); idx = r.Next(AllBills.Length)) ;
			IBill bi = AllBills[idx];
			return bi;

			#region old codes
			//System.Random rand = new System.Random();

			//// 8 is a id of non-exist bill
			//int avoidIdx0 = 8;
			//int avoidIdx1 = 8;
			//int avoidIdx2 = 8;
			//int avoidIdx3 = 8;
			//if (Politics.residentTax >= 20) {
			//	avoidIdx0 = 0;
			//} else if (Politics.residentTax <= 1) {
			//	avoidIdx0 = 1;
			//}

			//if (Politics.commercialTax >= 20) {
			//	avoidIdx2 = 4;
			//} else if (Politics.commercialTax <= 1) {
			//	avoidIdx2 = 5;
			//}

			//if (Politics.industryTax >= 20) {
			//	avoidIdx3 = 6;
			//} else if (Politics.industryTax <= 1) {
			//	avoidIdx3 = 7;
			//}

			//if (Politics.benefitOffset >= 100) {
			//	avoidIdx1 = 2;
			//} else if (Politics.benefitOffset <= 0) {
			//	avoidIdx1 = 3;
			//}

			//if ((orgIdx == avoidIdx0) || (orgIdx == avoidIdx1) || (orgIdx == avoidIdx2) || (orgIdx == avoidIdx3)) {
			//	while (true) {
			//		byte returnValue = (byte)rand.Next(8);
			//		if (!((returnValue == avoidIdx0) || (returnValue == avoidIdx1) || (returnValue == avoidIdx2) || (returnValue == avoidIdx3))) {
			//			return returnValue;
			//		}
			//	}
			//} else {
			//	return orgIdx;
			//}
			#endregion
		}
	}
}
