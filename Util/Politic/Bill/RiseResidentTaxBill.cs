namespace RealCity.Util.Politic.Bill
{
	public class RiseResidentTaxBill : AbstractBill
	{
		public RiseResidentTaxBill(int val)
			: base(val) {

		}
		public override void Implement() {
			Politics.residentTax += base.effectVal;
		}

		public override bool IsImplementable() {
			return Politics.CanRiseResidentTax;
		}
	}
}
