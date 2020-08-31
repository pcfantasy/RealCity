using ColossalFramework;
using ColossalFramework.UI;
using RealCity.CustomData;
using RealCity.Util.Politic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RealCity.Util
{
	public class Politics {
		private static IParty cParty;
		private static IParty gParty;
		private static IParty sParty;
		private static IParty lParty;
		private static IParty nParty;

		private const Citizen.AgeGroup VotingAge = Citizen.AgeGroup.Young;
		public static IParty[] Parties => new IParty[] { cParty, gParty, sParty, lParty, nParty };
		public static void ResetWinChance() {
			Parties.ForEach(p => p.ResetWinChance());
		}
		public static int GetAllSeatCount() {
			return Parties.Sum(p => {
				return p.SeatCount;
			});
		}
		public static int GetAllTicket() {
			return Parties.Sum(p => {
				return p.Ticket;
			});
		}
		public static bool IsOnElection() {
			return nextElectionInterval == 1;
		}

		/// <summary>
		/// 是否达到最低投票年龄
		/// </summary>
		/// <param name="age"></param>
		/// <returns></returns>
		public static bool IsOverVotingAge(Citizen.AgeGroup age) {
			//这地方不一定能解耦合...政治就是和人相关的嘛...
			return age >= VotingAge;
		}

		/// <summary>
		/// 是否达到最低投票年龄
		/// </summary>
		/// <param name="citizen"></param>
		/// <returns></returns>
		public static bool IsOverVotingAge(ref Citizen citizen) {
			return IsOverVotingAge(Citizen.GetAgeGroup(citizen.m_age));
		}

		//学历对选举投票的影响因子
		public static byte[,] education = { {30,  0, 10, 10, 50},
											{20, 10, 25, 20, 25},
											{10, 20, 30, 30, 10},
											{ 5, 25, 40, 25,  5}};

		//工作场所对选举投票的影响因子
		public static byte[,] workplace = { { 0, 20, 40, 40,  0},	//goverment
											{20, 10, 40, 30,  0},	//comm level1
											{10, 15, 35, 35,  5},	//comm level2
											{ 0, 20, 30, 40, 10},	//comm level3
											{ 0, 30, 25, 45,  0},	//comm tour, comm leisure
											{35, 10,  5, 20, 30},	//comm eco
											{50,  0, 25, 10, 15},	//indus gen level1
											{30,  5, 35, 15, 15},	//indus gen level2
											{15, 10, 40, 20, 15},	//indus gen level3
											{25,  5, 20, 20, 30},	//9 indus farming foresty oil ore
											{10, 30, 30, 30,  0},	//office level1
											{ 5, 35, 25, 35,  0},	//office level2
											{ 0, 40, 10, 35, 15},	//office level3
											{ 0, 50, 10, 40,  0},	//office high tech
											{35,  0, 10, 10, 45}};  //no work

		//财富积累对选举投票的影响因子
		// money < 2000
		// 6000 > money > 2000
		// money > 6000
		public static byte[,] money =     { {35,  0, 25, 10, 30},
											{10, 10, 35, 35, 10},
											{ 0, 30, 15, 40, 15}};
		//年龄
		//young
		//adult
		//senior
		public static byte[,] age =       { {15, 20, 30, 20, 15},
											{10, 15, 25, 30, 20},
											{ 5, 10, 20, 40, 25}};
		//man
		//woman
		public static byte[,] gender =    { {10, 10, 35, 35, 10},
											{ 5, 15, 40, 35,  5}};


		//5个政党对一项议案的赞成度
		//riseSalaryTax
		public static byte[,] riseSalaryTax = {
												{55, 40,  5},
												{10, 80, 10},
												{30, 70,  0},
												{70, 30,  0},
												{35, 55, 10},
											  };

		//fallSalaryTax
		public static byte[,] fallSalaryTax = {
												{40, 55,  5},
												{80, 10, 10},
												{70, 30,  0},
												{30, 70,  0},
												{55, 35, 10},
											  };

		//riseCommericalTax
		public static byte[,] riseCommericalTax = {
												{80, 20,  0},
												{40, 50, 10},
												{55, 40,  5},
												{10, 90,  0},
												{45, 45, 10},
											  };

		//fallCommericalTax
		public static byte[,] fallCommericalTax = {
												{20, 80,  0},
												{50, 40, 10},
												{40, 55,  5},
												{90, 10,  0},
												{45, 45, 10},
											  };


		//riseIndustryTax
		public static byte[,] riseIndustryTax = {
												{20, 70, 10},
												{50, 50,  0},
												{60, 30, 10},
												{70, 30,  0},
												{30, 70,  0},
											  };

		//fallIndustryTax
		public static byte[,] fallIndustryTax = {
												{70, 20, 10},
												{50, 50,  0},
												{30, 60, 10},
												{30, 70,  0},
												{70, 30,  0},
											  };

		//riseBenefit
		public static byte[,] riseBenefit = {
												{40, 50, 10},
												{70, 20, 10},
												{90, 10,  0},
												{10, 90,  0},
												{30, 60, 10},
											  };

		//fallBenefit
		public static byte[,] fallBenefit = {
												{50, 40, 10},
												{20, 70, 10},
												{10, 90,  0},
												{90, 10,  0},
												{60, 30, 10},
											  };


		public static ushort cPartyChance = 0;
		public static ushort gPartyChance = 0;
		public static ushort sPartyChance = 0;
		public static ushort lPartyChance = 0;
		public static ushort nPartyChance = 0;

		public static ushort cPartyTickets = 0;
		public static ushort gPartyTickets = 0;
		public static ushort sPartyTickets = 0;
		public static ushort lPartyTickets = 0;
		public static ushort nPartyTickets = 0;

		public static ushort cPartySeats = 0;
		public static ushort gPartySeats = 0;
		public static ushort sPartySeats = 0;
		public static ushort lPartySeats = 0;
		public static ushort nPartySeats = 0;

		//下一次_____倒计时
		public static short nextElectionInterval = 0;

		public static bool case1 = false;
		public static bool case2 = false;
		public static bool case3 = false;
		public static bool case4 = false;
		public static bool case5 = false;
		public static bool case6 = false;
		public static bool case7 = false;
		public static bool case8 = false;

		//当前议案Id
		public static byte currentBillId = 14;
		//赞成票数
		public static byte currentYes = 0;
		//否定票数
		public static byte currentNo = 0;
		//中立/缺席（？）
		public static byte currentNoAttend = 0;

		//居民税(0-20)
		public static int residentTax = 20;
		//商业税(0-20)
		public static int commercialTax = 20;
		//工业税(0-20)
		public static int industryTax = 20;
		//社会福利(0-100)
		public static short benefitOffset = 0;
	

		public static void DataInit() {
			PartyFactory factory = new PartyFactory();
			cParty = factory.MakeCParty();
			gParty = factory.MakeGParty();
			sParty = factory.MakeSParty();
			lParty = factory.MakeLParty();
			nParty = factory.MakeNParty();
		}

		public static void Save(ref byte[] saveData) {
			//58
			int i = 0;

			//30
			SaveAndRestore.SaveData(ref i, cPartyChance, ref saveData);
			SaveAndRestore.SaveData(ref i, gPartyChance, ref saveData);
			SaveAndRestore.SaveData(ref i, sPartyChance, ref saveData);
			SaveAndRestore.SaveData(ref i, lPartyChance, ref saveData);
			SaveAndRestore.SaveData(ref i, nPartyChance, ref saveData);

			SaveAndRestore.SaveData(ref i, cPartyTickets, ref saveData);
			SaveAndRestore.SaveData(ref i, gPartyTickets, ref saveData);
			SaveAndRestore.SaveData(ref i, sPartyTickets, ref saveData);
			SaveAndRestore.SaveData(ref i, lPartyTickets, ref saveData);
			SaveAndRestore.SaveData(ref i, nPartyTickets, ref saveData);

			SaveAndRestore.SaveData(ref i, cPartySeats, ref saveData);
			SaveAndRestore.SaveData(ref i, gPartySeats, ref saveData);
			SaveAndRestore.SaveData(ref i, sPartySeats, ref saveData);
			SaveAndRestore.SaveData(ref i, lPartySeats, ref saveData);
			SaveAndRestore.SaveData(ref i, nPartySeats, ref saveData);

			//14
			SaveAndRestore.SaveData(ref i, nextElectionInterval, ref saveData);
			SaveAndRestore.SaveData(ref i, case1, ref saveData);
			SaveAndRestore.SaveData(ref i, case2, ref saveData);
			SaveAndRestore.SaveData(ref i, case3, ref saveData);
			SaveAndRestore.SaveData(ref i, case4, ref saveData);
			SaveAndRestore.SaveData(ref i, case5, ref saveData);
			SaveAndRestore.SaveData(ref i, case6, ref saveData);
			SaveAndRestore.SaveData(ref i, case7, ref saveData);
			SaveAndRestore.SaveData(ref i, case8, ref saveData);
			SaveAndRestore.SaveData(ref i, currentBillId, ref saveData);
			SaveAndRestore.SaveData(ref i, currentYes, ref saveData);
			SaveAndRestore.SaveData(ref i, currentNo, ref saveData);
			SaveAndRestore.SaveData(ref i, currentNoAttend, ref saveData);

			//14
			SaveAndRestore.SaveData(ref i, residentTax, ref saveData);
			SaveAndRestore.SaveData(ref i, commercialTax, ref saveData);
			SaveAndRestore.SaveData(ref i, industryTax, ref saveData);
			SaveAndRestore.SaveData(ref i, benefitOffset, ref saveData);

			residentTax = COMath.Clamp((int)residentTax, 0, 20);
			commercialTax = COMath.Clamp((int)commercialTax, 0, 20);
			industryTax = COMath.Clamp((int)industryTax, 0, 20);
			benefitOffset = (short)COMath.Clamp((int)benefitOffset, 0, 100);

			if (i != saveData.Length) {
				DebugLog.LogToFileOnly($"Politics Save Error: saveData.Length = {saveData.Length} + i = {i}");
			}
		}

		public static void Load(ref byte[] saveData) {
			int i = 0;


			SaveAndRestore.LoadData(ref i, saveData, ref cPartyChance);
			SaveAndRestore.LoadData(ref i, saveData, ref gPartyChance);
			SaveAndRestore.LoadData(ref i, saveData, ref sPartyChance);
			SaveAndRestore.LoadData(ref i, saveData, ref lPartyChance);
			SaveAndRestore.LoadData(ref i, saveData, ref nPartyChance);

			SaveAndRestore.LoadData(ref i, saveData, ref cPartyTickets);
			SaveAndRestore.LoadData(ref i, saveData, ref gPartyTickets);
			SaveAndRestore.LoadData(ref i, saveData, ref sPartyTickets);
			SaveAndRestore.LoadData(ref i, saveData, ref lPartyTickets);
			SaveAndRestore.LoadData(ref i, saveData, ref nPartyTickets);

			SaveAndRestore.LoadData(ref i, saveData, ref cPartySeats);
			SaveAndRestore.LoadData(ref i, saveData, ref gPartySeats);
			SaveAndRestore.LoadData(ref i, saveData, ref sPartySeats);
			SaveAndRestore.LoadData(ref i, saveData, ref lPartySeats);
			SaveAndRestore.LoadData(ref i, saveData, ref nPartySeats);

			SaveAndRestore.LoadData(ref i, saveData, ref nextElectionInterval);

			SaveAndRestore.LoadData(ref i, saveData, ref case1);
			SaveAndRestore.LoadData(ref i, saveData, ref case2);
			SaveAndRestore.LoadData(ref i, saveData, ref case3);
			SaveAndRestore.LoadData(ref i, saveData, ref case4);
			SaveAndRestore.LoadData(ref i, saveData, ref case5);
			SaveAndRestore.LoadData(ref i, saveData, ref case6);
			SaveAndRestore.LoadData(ref i, saveData, ref case7);
			SaveAndRestore.LoadData(ref i, saveData, ref case8);

			SaveAndRestore.LoadData(ref i, saveData, ref currentBillId);
			SaveAndRestore.LoadData(ref i, saveData, ref currentYes);
			SaveAndRestore.LoadData(ref i, saveData, ref currentNo);
			SaveAndRestore.LoadData(ref i, saveData, ref currentNoAttend);
			SaveAndRestore.LoadData(ref i, saveData, ref residentTax);
			SaveAndRestore.LoadData(ref i, saveData, ref commercialTax);
			SaveAndRestore.LoadData(ref i, saveData, ref industryTax);
			SaveAndRestore.LoadData(ref i, saveData, ref benefitOffset);

			if (i != saveData.Length) {
				DebugLog.LogToFileOnly($"Politics Load Error: saveData.Length = {saveData.Length} + i = {i}");
			}
		}
	}
}