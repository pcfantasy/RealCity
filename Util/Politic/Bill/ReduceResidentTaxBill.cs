﻿namespace RealCity.Util.Politic.Bill
{
	public class ReduceResidentTaxBill : AbstractBill
	{
		public override string Name => "FALL_RESIDENT_TAX";

		public ReduceResidentTaxBill(int val)
			: base(val)
		{

		}

		public override void Implement()
		{
			Politics.residentTax -= base.effectVal;
		}

		public override bool IsImplementable()
		{
			return Politics.CanReduceResidentTax;
		}
	}
}
