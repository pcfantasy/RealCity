using ColossalFramework;
using ColossalFramework.Math;
using RealCity.Util;

namespace RealCity.CustomAI
{
	public class RealCityMarketAI
	{
		public delegate void MarketAIGetVisitBehaviour(MarketAI MarketAI, ushort buildingID, ref Building buildingData, ref Citizen.BehaviourData behaviour, ref int aliveCount, ref int totalCount);
		public static MarketAIGetVisitBehaviour GetVisitBehaviour;

		public static void InitDelegate() {
			if (GetVisitBehaviour != null)
				return;
			GetVisitBehaviour = FastDelegateFactory.Create<MarketAIGetVisitBehaviour>(typeof(MarketAI), "GetVisitBehaviour", instanceMethod: true);
		}
	}
}