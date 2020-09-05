using System;

namespace RealCity.RebalancedIndustries
{
	public class Mod
	{
		public static bool IsIndustriesBuilding(IndustryBuildingAI building) {
			switch (building.m_industryType) {
				case DistrictPark.ParkType.Industry:
				case DistrictPark.ParkType.Farming:
				case DistrictPark.ParkType.Forestry:
				case DistrictPark.ParkType.Ore:
				case DistrictPark.ParkType.Oil:
					return true;
			}
			return false;
		}

		public static ushort CombineBytes(byte large, byte small) {
			return Convert.ToUInt16((large << 8) + small);
		}

		public static void SplitBytes(ushort value, ref byte large, ref byte small) {
			large = (byte)(value >> 8);
			small = (byte)(value & 0xFF);
		}
	}
}
