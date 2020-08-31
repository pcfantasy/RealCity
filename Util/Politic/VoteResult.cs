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
		public byte Agree { get; }
		/// <summary>
		/// 反对
		/// </summary>
		public byte Disagree { get; }
		/// <summary>
		/// 弃权
		/// </summary>
		public byte NoVote { get; }
		/// <summary>
		/// 投票结果
		/// </summary>
		/// <param name="agree">同意</param>
		/// <param name="disagree">反对</param>
		/// <param name="noVote">弃权</param>
		public VoteResult(byte agree, byte disagree, byte noVote) {
			this.Agree = agree;
			this.Disagree = disagree;
			this.NoVote = noVote;
		}
	}
}
