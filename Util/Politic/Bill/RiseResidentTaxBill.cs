using ColossalFramework;
using System;

namespace RealCity.Util.Politic.Bill
{
	public class RiseResidentTaxBill : AbstractBill
	{
		public override string Name => "RISE_RESIDENT_TAX";

		public RiseResidentTaxBill(int val)
			: base(val)
		{

		}

		public override void Implement()
		{
			Politics.residentTax += base.effectVal;
		}

		public override bool IsImplementable()
		{
			// 20% oppotunity to implement though financial shortage
			if (new Random().Next(5) == 0)
			{
				return Politics.CanRiseResidentTax
					&& Singleton<EconomyManager>.instance.GetPrivateField<long>("m_cashAmount") < 0L;
			}
			else
			{
				return Politics.CanRiseResidentTax;
			}
		}
	}
}
