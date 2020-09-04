namespace RealCity.Util.Politic
{
	/// <summary>
	/// 议案（抽象的）
	/// </summary>
	public abstract class AbstractBill : IBill
	{
		protected int effectVal;
		public AbstractBill(int effectVal) {
			this.effectVal = effectVal;
		}
		public abstract void Implement();
		public abstract bool IsImplementable();
	}
}
