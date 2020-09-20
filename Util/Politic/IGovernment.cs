using RealCity.Util.Politic.ElectionUtil;

namespace RealCity.Util.Politic
{
	public interface IGovernment
	{
		int[] Seats { get; }
		int AllSeatCount { get; }
		IParty[] Parties { get; }
		GovernmentType GovernmentType { get; }
		//IParty[] RulingParties { get; }
		void UpdateSeats(ElectionInfo info);
		void UpdateGovType();
		IGovernmentalMeeting HoldMeeting();
		bool IsVoteResultApprovable(VoteResult meeting);
	}
	public enum GovernmentType
	{
		Single,
		LeftUnion,
		WideLeftUnion,
		RightUnion,
		Grand,
	}
}
