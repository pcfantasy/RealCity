using RealCity.Util.Politic.ElectionUtil;

namespace RealCity.Util.Politic
{
	/// <summary>
	/// 选举
	/// </summary>
	public static class Election
	{
		private static IParty[] Parties { get; set; } = Politics.Parties;

		/// <summary>
		/// 选举信息
		/// </summary>
		public static ElectionInfo CurrentElectionInfo { get; private set; }

		public static void NextElection() {
			CurrentElectionInfo = new ElectionInfo(Parties);
		}

		/// <summary>
		/// Is on election?
		/// </summary>
		/// <returns></returns>
		public static bool IsOnElection() {
			return Politics.IsOnElection();
		}
	}
}
