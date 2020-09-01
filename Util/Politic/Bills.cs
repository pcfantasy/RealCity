using RealCity.Util.Politic.Bill;

namespace RealCity.Util.Politic
{
	public class Bills
	{
		public static IBill RiseResidentTax = new RiseResidentTaxBill(1);
		public static IBill RiseCommercialTax = new RiseCommercialTaxBill(1);
		public static IBill RiseIndustryTax = new RiseIndustryTaxBill(1);
		public static IBill RiseBenefitOffset = new RiseBenefitBill(10);

		public static IBill ReduceResidentTax => new ReduceResidentTaxBill(1);
		public static IBill ReduceCommercialTax = new ReduceCommercialTaxBill(1);
		public static IBill ReduceIndustryTax = new ReduceIndustryTaxBill(1);
		public static IBill ReduceBenefitOffset = new ReduceBenefitBill(10);

		//这里需要null object吗 ？



		public IBill GetAnotherBill(IBill bill) {

			//需要获得一个逆向的Bill   RiseBill => ReduceBill

			#region old codes
			System.Random rand = new System.Random();

			int avoidIdx0 = 8;
			int avoidIdx1 = 8;
			int avoidIdx2 = 8;
			int avoidIdx3 = 8;
			if (Politics.residentTax >= 20) {
				avoidIdx0 = 0;
			} else if (Politics.residentTax <= 1) {
				avoidIdx0 = 1;
			}

			if (Politics.benefitOffset >= 100) {
				avoidIdx1 = 2;
			} else if (Politics.benefitOffset <= 0) {
				avoidIdx1 = 3;
			}

			if (Politics.commercialTax >= 20) {
				avoidIdx2 = 4;
			} else if (Politics.commercialTax <= 1) {
				avoidIdx2 = 5;
			}

			if (Politics.industryTax >= 20) {
				avoidIdx3 = 6;
			} else if (Politics.industryTax <= 1) {
				avoidIdx3 = 7;
			}

			if ((orgIdx == avoidIdx0) 
				|| (orgIdx == avoidIdx1) 
				|| (orgIdx == avoidIdx2)
				|| (orgIdx == avoidIdx3)) {
				while (true) {
					byte returnValue = (byte)rand.Next(8);
					if (!((returnValue == avoidIdx0) || (returnValue == avoidIdx1) || (returnValue == avoidIdx2) || (returnValue == avoidIdx3))) {
						return returnValue;
					}
				}
			} else {
				return orgIdx;
			}
			#endregion
		}
	}
}