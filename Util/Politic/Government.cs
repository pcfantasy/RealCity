using RealCity.Util.Politic.ElectionUtil;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RealCity.Util.Politic
{
	/// <summary>
	/// 政府
	/// </summary>
	public class Government : IGovernment
	{
		private const int MinSeatCount = 99;

		public static Government Null = new Government();
		public static Government Instance { get; set; }

		public IGovernmentalMeeting LastMeeting { get; private set; }

		public IBill currentBill = Bills.GetRandomBill();
		public IParty[] Parties { get; private set; }
		public GovernmentType GovernmentType { get; private set; }
		public IParty[] RulingParties { get; private set; }
		// refer RealCityEconomyExtension.cs
		public int[] Seats { get; private set; }
		public int AllSeatCount => this.Seats.Sum();

		public void UpdateSeats(ElectionInfo info)
		{
			int cnt = info.GetAllTickets();
			this.Seats = new int[info.PartiesCount];
			for (int i = 0; i < info.PartiesCount; i++)
			{
				this.Seats[i] = GetSeatCount(info.TicketCounter[i], ref cnt);
			}
			this.FixSeatCount();

			#region old codes
			////int allTickets = Politics.cPartyTickets + Politics.gPartyTickets + Politics.sPartyTickets + Politics.lPartyTickets + Politics.nPartyTickets;
			//int cnt = Politics.GetAllTicket();
			//if (cnt != 0) {
			//	Politics.cPartySeats = (ushort)(99 * Politics.cPartyTickets / cnt);
			//	Politics.gPartySeats = (ushort)(99 * Politics.gPartyTickets / cnt);
			//	Politics.sPartySeats = (ushort)(99 * Politics.sPartyTickets / cnt);
			//	Politics.lPartySeats = (ushort)(99 * Politics.lPartyTickets / cnt);
			//	Politics.nPartySeats = (ushort)(99 * Politics.nPartyTickets / cnt);
			//} else {
			//	Politics.cPartySeats = 0;
			//	Politics.gPartySeats = 0;
			//	Politics.sPartySeats = 0;
			//	Politics.lPartySeats = 0;
			//	Politics.nPartySeats = 0;
			//}
			//Politics.cPartyTickets = 0;
			//Politics.gPartyTickets = 0;
			//Politics.sPartyTickets = 0;
			//Politics.lPartyTickets = 0;
			//Politics.nPartyTickets = 0;

			////allTickets = Politics.cPartySeats + Politics.gPartySeats + Politics.sPartySeats + Politics.lPartySeats + Politics.nPartySeats;
			//cnt = Politics.GetAllSeatCount();
			//if (cnt < 99) {
			//	System.Random rand = new System.Random();
			//	switch (rand.Next(5)) {
			//		case 0:
			//			Politics.cPartySeats += (ushort)(99 - cnt); break;
			//		case 1:
			//			Politics.gPartySeats += (ushort)(99 - cnt); break;
			//		case 2:
			//			Politics.sPartySeats += (ushort)(99 - cnt); break;
			//		case 3:
			//			Politics.lPartySeats += (ushort)(99 - cnt); break;
			//		case 4:
			//			Politics.nPartySeats += (ushort)(99 - cnt); break;
			//	}
			//}
			#endregion
		}

		public void UpdateGovType()
		{
			bool isOk = default;
			int halfSeatCount = (MinSeatCount >> 1) + 1;
			for (int i = 0; i < this.Seats.Length; i++)
			{
				if (this.Seats[i] >= halfSeatCount)
				{
					this.GovernmentType = GovernmentType.Single;
					this.RulingParties = new IParty[] { this.Parties[i] };
					isOk = true;
				}
			}

			if (isOk)
				return;

			// bad codes
			int left, wideLeft, right;
			left = wideLeft = right = default;
			for (int i = 0; i < this.Seats.Length; i++)
			{
				if (this.Parties[i].PartyType == PartyType.Green || this.Parties[i].PartyType == PartyType.Socialist)
				{
					left += this.Seats[i];
				}
				else if (this.Parties[i].PartyType == PartyType.Communist)
				{
					wideLeft += this.Seats[i];
				}
				else if (this.Parties[i].PartyType == PartyType.Liberal || this.Parties[i].PartyType == PartyType.National)
				{
					right += this.Seats[i];
				}
			}
			wideLeft += left;
			if (left >= halfSeatCount)
			{
				this.GovernmentType = GovernmentType.LeftUnion;
				this.RulingParties = this.Parties
					.Where(p => p.PartyType == PartyType.Green || p.PartyType == PartyType.Socialist)
					.ToArray();
			}
			else if (wideLeft >= halfSeatCount)
			{
				this.GovernmentType = GovernmentType.WideLeftUnion;
				this.RulingParties = this.Parties
					.Where(p => p.PartyType == PartyType.Green || p.PartyType == PartyType.Socialist || p.PartyType == PartyType.Communist)
					.ToArray();
			}
			else if (right >= halfSeatCount)
			{
				this.GovernmentType = GovernmentType.RightUnion;
				this.RulingParties = this.Parties
					.Where(p => p.PartyType == PartyType.Liberal || p.PartyType == PartyType.National)
					.ToArray();
			}
			else
			{
				this.GovernmentType = GovernmentType.Grand;
				this.RulingParties = this.Parties;
			}
		}

		/// <summary>
		/// Hold a governmental meeting and decide a <see cref="IBill"/> to implement.
		/// </summary>
		/// <returns></returns>
		public IGovernmentalMeeting HoldMeeting()
		{
			//if(this.currentBill == null) {
			//	this.currentBill = Bills.GetRandomBill();
			//}
			IGovernmentalMeeting v = new GovernmentalMeeting(this, Bills.GetAnotherBill(this.currentBill));
			v.Start();
			if (v.VoteResult.IsApprovable)
			{
				v.Bill.Implement();
			}
			this.LastMeeting = v;
			return v;
		}

		private int GetSeatCount(int ticketCount, ref int ticketSum)
		{
			return (int)(99 * ticketCount / ticketSum);
		}

		/// <summary>
		/// 修正Seat数量至 <see cref="MinSeatCount"/> 个
		/// </summary>
		private void FixSeatCount()
		{
			int missingCount = MinSeatCount - this.AllSeatCount;
			// if have missing seats
			if (missingCount > 0)
			{
				System.Random r = new System.Random();
				int idx = r.Next(this.Seats.Length);
				this.Seats[idx] += missingCount;
			}
		}

		public static void Start()
		{
			Instance = Null as Government;
			Instance.Parties = Politics.Parties;
		}
	}
}
