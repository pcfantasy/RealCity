using System;
using System.Linq;

namespace RealCity.Util.Politic.ElectionUtil
{
	/// <summary>
	/// 选举信息
	/// </summary>
	public class ElectionInfo
	{
		public int PartiesCount => this.Parties.Length;
		public IParty[] Parties { get; }
		/// <summary>
		/// 得票计数器
		/// </summary>
		public int[] TicketCounter { get; }
		/// <summary>
		/// 选举信息
		/// </summary>
		/// <param name="parties">参选政党</param>
		public ElectionInfo(IParty[] parties) {
			if (parties.Length <= 0)
				throw new ArgumentException("No party joins in an election.");
			this.Parties = parties;
			this.TicketCounter = new int[parties.Length];
		}
		public int GetAllTickets() {
			return TicketCounter.Sum();
		}
	}
}
