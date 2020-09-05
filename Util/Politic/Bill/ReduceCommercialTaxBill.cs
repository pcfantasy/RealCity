namespace RealCity.Util.Politic.Bill
{
	public class ReduceCommercialTaxBill : AbstractBill
	{
		public override string Name => "FALL_COMMERIAL_TAX";

		public ReduceCommercialTaxBill(int val)
			: base(val) {

		}

		public override void Implement() {
			Politics.commercialTax -= base.effectVal;
		}

		public override bool IsImplementable() {
			return Politics.CanReduceCommercialTax;
		}
	}
}
