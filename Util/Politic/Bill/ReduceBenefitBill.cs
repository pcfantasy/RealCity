namespace RealCity.Util.Politic.Bill
{
	public class ReduceBenefitBill : AbstractBill
	{
		public ReduceBenefitBill(int val)
			: base(val) {

		}
		public override void Implement() {
			Politics.benefitOffset -= base.effectVal;
		}

		public override bool IsImplementable() {
			return Politics.CanReduceBenefit;
		}
	}
}
