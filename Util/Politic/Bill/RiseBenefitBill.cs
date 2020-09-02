namespace RealCity.Util.Politic.Bill
{
	public class RiseBenefitBill : AbstractBill
	{
		public RiseBenefitBill(int val)
			: base(val) {

		}
		public override void Implement() {
			Politics.benefitOffset += base.effectVal;
		}

		public override bool IsImplementable() {
			return Politics.CanRiseBenefit;
		}
	}
}
