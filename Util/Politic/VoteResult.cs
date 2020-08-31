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
		public int Agree { get; }
		/// <summary>
		/// 反对
		/// </summary>
		public int Disagree { get; }
		/// <summary>
		/// 弃权
		/// </summary>
		public int NoVote { get; }
		/// <summary>
		/// 投票结果
		/// </summary>
		/// <param name="agree">同意</param>
		/// <param name="disagree">反对</param>
		/// <param name="noVote">弃权</param>
		public VoteResult(int agree, int disagree, int noVote) {
			this.Agree = agree;
			this.Disagree = disagree;
			this.NoVote = noVote;
		}
	}
}
