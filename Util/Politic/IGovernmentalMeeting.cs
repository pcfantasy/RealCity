namespace RealCity.Util.Politic
{
	public interface IGovernmentalMeeting
	{
		IBill Bill { get; }
		VoteResult VoteResult { get; }
		void Start();
	}
}
