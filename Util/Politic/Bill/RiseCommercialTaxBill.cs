namespace RealCity.Util.Politic.Bill
{
	public class RiseCommercialTaxBill : AbstractBill
	{
		public RiseCommercialTaxBill(int val)
			: base(val) {

		}
		public override void Implement() {
			Politics.commercialTax += base.effectVal;
		}

		public override bool IsImplementable() {
			return Politics.CanRiseCommercialTax;
		}
	}
}
