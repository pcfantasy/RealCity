namespace RealCity.Util.Politic
{
	public interface IBill
	{
		string Name { get; }
		void Implement();
		bool IsImplementable();
	}
}
