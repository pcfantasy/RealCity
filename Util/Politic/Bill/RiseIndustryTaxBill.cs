using ColossalFramework;
using System;

namespace RealCity.Util.Politic.Bill
{
	public class RiseIndustryTaxBill : AbstractBill
	{
		public override string Name => "RISE_INDUSTRIAL_TAX";

		public RiseIndustryTaxBill(int val)
			: base(val)
		{

		}

		public override void Implement()
		{
			Politics.industryTax += base.effectVal;
		}

		public override bool IsImplementable()
		{
			// 20% oppotunity to implement though financial shortage
			if (new Random().Next(5) == 0)
			{
				return Politics.CanRiseIndustryTax
					&& Singleton<EconomyManager>.instance.GetPrivateField<long>("m_cashAmount") < 0L;
			}
			else
			{
				return Politics.CanRiseIndustryTax;
			}
		}
	}
}
