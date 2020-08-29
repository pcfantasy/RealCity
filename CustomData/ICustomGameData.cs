namespace RealCity.CustomData
{
	public interface ICustomGameData
	{
		 void DataInit();
		 void Save(ref byte[] saveData);
		 void Load(ref byte[] saveData);
	}
}