using System;

namespace RealCity.Util.Politic
{
	/// <summary>
	/// 议案
	/// </summary>
	public abstract class AbstractBill : IBill
	{
		protected int effectVal;
		public AbstractBill(int effectVal) {
			this.effectVal = effectVal;
		}
		public abstract void Implement();
	}
}