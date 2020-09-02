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
			bool[] implementable = new bool[AllBills.Length];
			int i = 0;
			implementable[i++] = Politics.CanRiseResidentTax;
			implementable[i++] = Politics.CanReduceResidentTax;
			implementable[i++] = Politics.CanRiseCommercialTax;
			implementable[i++] = Politics.CanReduceCommercialTax;
			implementable[i++] = Politics.CanRiseIndustryTax;
			implementable[i++] = Politics.CanReduceIndustryTax;
			implementable[i++] = Politics.CanRiseBenefit;
			implementable[i++] = Politics.CanReduceBenefit;

			Random r = new Random();
			for (; implementable[idx] == false; idx = r.Next(AllBills.Length)) ;
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
