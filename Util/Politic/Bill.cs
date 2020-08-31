using System;

namespace RealCity.Util.Politic
{
	/// <summary>
	/// 议案
	/// </summary>
	public class Bill : IBill
	{
		private Action content;

		/// <summary>
		/// 议案
		/// </summary>
		/// <param name="content">议案内容</param>
		public Bill(Action content) {
			//Bill就像一个抽象类，它并不知道content是什么，只管调用就行
			this.content = content;
		}

		public void Implement() {
			this.content.Invoke();
		}
		public static bool operator ==(Bill a, Bill b) {
			return a.content == b.content;
		}
		public static bool operator !=(Bill a, Bill b) {
			return !(a == b);
		}
	}
}