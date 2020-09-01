namespace RealCity.Util.Politic.Bill
{
	public class ReduceIndustryTaxBill : AbstractBill
	{
		public ReduceIndustryTaxBill(int val)
			: base(val) {

		}
		public override void Implement() {
			Politics.industryTax -= base.effectVal;
		}
	}
}
