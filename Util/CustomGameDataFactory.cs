using RealCity.CustomData;

namespace RealCity.Util
{
	public class CustomGameDataFactory
	{
		public BuildingData MakeBuildingData() {
			ICustomGameData d = new BuildingData();
			d.DataInit();
			return d as BuildingData;
		}
		public CitizenData MakeCitizenData() {
			ICustomGameData d = new CitizenData();
			d.DataInit();
			return d as CitizenData;
		}
		public CitizenUnitData MakeCitizenUnitData() {
			ICustomGameData d = new CitizenUnitData();
			d.DataInit();
			return d as CitizenUnitData;
		}
		public TransportLineData MakeTransportLineData() {
			ICustomGameData d = new TransportLineData();
			d.DataInit();
			return d as TransportLineData;
		}
		public VehicleData MakeVehicleData() {
			ICustomGameData d = new VehicleData();
			d.DataInit();
			return d as VehicleData;
		}
	}
}
