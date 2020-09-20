namespace RealCity.Util.Politic
{
	/// <summary>
	/// 投票结果
	/// </summary>
	public class VoteResult
	{
		/// <summary>
		/// 同意
		/// </summary>
		public int Agree { get; private set; }
		/// <summary>
		/// 反对
		/// </summary>
		public int Disagree { get; private set; }
		/// <summary>
		/// 弃权
		/// </summary>
		public int Neutral { get; private set; }
		public int Sum => this.Agree + this.Disagree + this.Neutral;

		/// <summary>
		/// 投票结果
		/// </summary>
		public VoteResult()
			: this(default, default, default)
		{
		}
		/// <summary>
		/// 投票结果
		/// </summary>
		/// <param name="agree">同意</param>
		/// <param name="disagree">反对</param>
		/// <param name="neutral">弃权</param>
		public VoteResult(int agree, int disagree, int neutral)
		{
			this.Agree = agree;
			this.Disagree = disagree;
			this.Neutral = neutral;
		}
		public void AppendChange(int dAgree, int dDisagree)
		{
			this.AppendChange(dAgree, dDisagree, 0);
		}
		public void AppendChange(int dAgree, int dDisagree, int dNeutral)
		{
			this.Agree += dAgree;
			this.Disagree += dDisagree;
			this.Neutral += dNeutral;
		}
	}
}
