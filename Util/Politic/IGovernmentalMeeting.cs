namespace RealCity.Util.Politic
{
	public interface IGovernmentalMeeting
	{
		IBill Bill { get; }
		AbstractVoteResult VoteResult { get; }
		void Start();
	}
}
