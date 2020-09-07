namespace RealCity.Util.Politic.Bill
{
	public class ReduceIndustryTaxBill : AbstractBill
	{
		public override string Name => "FALL_INDUSTRIAL_TAX";

		public ReduceIndustryTaxBill(int val)
			: base(val)
		{

		}

		public override void Implement()
		{
			Politics.industryTax -= base.effectVal;
		}

		public override bool IsImplementable()
		{
			return Politics.CanReduceIndustryTax;
		}
	}
}
