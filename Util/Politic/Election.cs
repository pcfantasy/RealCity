using RealCity.Util.Politic.ElectionUtil;
using System;

namespace RealCity.Util.Politic
{
	/// <summary>
	/// 选举
	/// </summary>
	public static class Election
	{
		private static IParty[] Parties { get; set; }
		/// <summary>
		/// 选举信息
		/// </summary>
		public static ElectionInfo Info { get; private set; }


		public static void NextElection() {
			Info = new ElectionInfo(Parties);
		}

		/// <summary>
		/// 是否即将选举
		/// </summary>
		/// <returns></returns>
		public static bool IsOnElection() {
			return Politics.IsOnElection();
		}
	}
}
