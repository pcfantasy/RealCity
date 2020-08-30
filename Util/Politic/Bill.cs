using System;

namespace RealCity.Util.Politic
{
	public class Bill : IBill
	{
		private Action billContent;
		public Bill(Action content) {
			this.billContent = content;
		}
		public void Implement() {
			this.billContent.Invoke();
		}
	}
}