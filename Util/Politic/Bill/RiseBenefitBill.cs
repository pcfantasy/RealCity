using ColossalFramework;
using System;

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
			// 20% oppotunity to implement though financial shortage
			if (new Random().Next(5) == 0) {
				return Politics.CanRiseBenefit
					&& Singleton<EconomyManager>.instance.GetPrivateField<long>("m_cashAmount") < 0L;
			} else {
				return Politics.CanRiseBenefit;
			}
		}
	}
}
