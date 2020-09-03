namespace RealCity.Util.Politic
{
	/// <summary>
	/// 投票结果（抽象的）
	/// </summary>
	public abstract class AbstractVoteResult  //TODO: consider a better name
	{
		/// <summary>
		/// 同意
		/// </summary>
		public int Agree { get; }
		/// <summary>
		/// 反对
		/// </summary>
		public int Disagree { get; }
		/// <summary>
		/// 弃权
		/// </summary>
		public int Neutral { get; }
		public int Sum => this.Agree + this.Disagree + this.Neutral;

		// define under what circumstances will the result be approvable here
		public abstract bool IsApprovable { get; }
		/// <summary>
		/// 投票结果
		/// </summary>
		/// <param name="agree">同意</param>
		/// <param name="disagree">反对</param>
		/// <param name="neutral">弃权</param>
		public AbstractVoteResult(int agree, int disagree, int neutral) {
			this.Agree = agree;
			this.Disagree = disagree;
			this.Neutral = neutral;
		}
	}
}
