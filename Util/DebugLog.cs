using ColossalFramework.Plugins;
using System.IO;

namespace RealCity.Util
{
	public static class DebugLog
	{
		public static void LogToFileOnly(string msg)
		{
			using (FileStream fileStream = new FileStream("RealCity.txt", FileMode.Append))
			{
				StreamWriter streamWriter = new StreamWriter(fileStream);
				streamWriter.WriteLine(msg);
				streamWriter.Flush();
			}
		}
	}
}
