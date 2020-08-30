using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RealCity.Util.Politic
{
	public class BillFactory
	{
		public static IBill RaiseResidentTax = new Bill(() => Politics.residentTax++);
		public static IBill ReduceResidentTax = new Bill(() => Politics.residentTax--);
		public static IBill RaiseCommercialTax = new Bill(() => Politics.commercialTax++);
		public static IBill ReduceCommercialTax = new Bill(() => Politics.commercialTax--);
		public static IBill RaiseIndustryTax = new Bill(() => Politics.industryTax++);
		public static IBill ReduceIndustryTax = new Bill(() => Politics.industryTax--);
		public static IBill RaiseBenefitOffset = new Bill(() => Politics.benefitOffset += 10);
		public static IBill ReduceBenefitOffset = new Bill(() => Politics.benefitOffset -= 10);
		IBill GetAnotherBill(IBill currentBill) {

		}
	}
}