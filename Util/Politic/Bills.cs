using System;
using RealCity.Util.Politic.Bill;

namespace RealCity.Util.Politic
{
	public static class Bills
	{
		private static Random r = new Random();

		public readonly static IBill RiseResidentTax = new RiseResidentTaxBill(1);
		public readonly static IBill ReduceResidentTax = new ReduceResidentTaxBill(1);

		public readonly static IBill RiseCommercialTax = new RiseCommercialTaxBill(1);
		public readonly static IBill ReduceCommercialTax = new ReduceCommercialTaxBill(1);

		public readonly static IBill RiseIndustryTax = new RiseIndustryTaxBill(1);
		public readonly static IBill ReduceIndustryTax = new ReduceIndustryTaxBill(1);

		public readonly static IBill RiseBenefit = new RiseBenefitBill(10);
		public readonly static IBill ReduceBenefit = new ReduceBenefitBill(10);


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

		public static IBill GetRandomBill()
		{
			return AllBills.GetRandomElement(r);
		}

		public static IBill GetReversedBill(IBill bill)
		{
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
			if ((index & 1) == 1)
			{
				revBill = AllBills[index - 1];
			}
			else
			{
				revBill = AllBills[index + 1];
			}
			return revBill;
		}

		public static IBill GetAnotherBill(IBill bill)
		{
			for (; bill.IsImplementable() == false; bill = GetRandomBill()) ;
			return bill;

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
