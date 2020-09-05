using System;

namespace RealCity.CustomData
{
	public interface ICustomGameData
	{
		void DataInit();

		//TODO: let ICustomGameData obey the SRP
		void Save(ref byte[] saveData);
		void Load(ref byte[] saveData);
	}
}
