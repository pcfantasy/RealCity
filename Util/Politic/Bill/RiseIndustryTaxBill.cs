namespace RealCity.Util.Politic.Bill
{
	public class RiseIndustryTaxBill : AbstractBill
	{
		public RiseIndustryTaxBill(int val)
			: base(val) {

		}
		public override void Implement() {
			Politics.industryTax += base.effectVal;
		}
	}
}
