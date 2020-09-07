namespace RealCity.Util.Politic
{
	/// <summary>
	/// 投票结果
	/// </summary>
	public class VoteResult : AbstractVoteResult
	{
		public override bool IsApprovable => this.Agree >= (this.Sum >> 1);

		/// <summary>
		/// 投票结果
		/// </summary>
		public VoteResult()
			: base()
		{

		}
		/// <summary>
		/// 投票结果
		/// </summary>
		/// <param name="agree">同意</param>
		/// <param name="disagree">反对</param>
		/// <param name="neutral">弃权</param>
		public VoteResult(int agree, int disagree, int neutral)
			: base(agree, disagree, neutral)
		{

		}
	}
}
