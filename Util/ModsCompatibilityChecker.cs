using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ColossalFramework.PlatformServices;
using ColossalFramework.UI;
using RealCity.UI;
using ColossalFramework.Plugins;

namespace RealCity.Util
{
	public class ModsCompatibilityChecker
	{
		//TODO include %APPDATA% mods folder
		private readonly ulong[] userModList;
		private readonly Dictionary<ulong, string> incompatibleModList;

		public ModsCompatibilityChecker() {
			incompatibleModList = LoadIncompatibleModList();
			userModList = GetUserModsList();
		}

		public void PerformModCheck() {
			DebugLog.LogToFileOnly("Performing incompatible mods check");
			Dictionary<ulong, string> incompatibleMods = new Dictionary<ulong, string>();
			for (int i = 0; i < userModList.Length; i++) {
				string incompatibleModName = "";
				if (incompatibleModList.TryGetValue(userModList[i], out incompatibleModName)) {
					incompatibleMods.Add(userModList[i], incompatibleModName);
				}
			}

			if (incompatibleMods.Count > 0) {
				DebugLog.LogToFileOnly("Incompatible mods detected! Count: " + incompatibleMods.Count);
				IncompatibleModsPanel panel = IncompatibleModsPanel.Instance;
				panel.IncompatibleMods = incompatibleMods;
				panel.Initialize();
				UIView.PushModal(panel);
			} else {
				DebugLog.LogToFileOnly("No incompatible mods detected");
			}
		}

		private string TxTPath() {
			var modPath = PluginManager.instance.FindPluginInfo(Assembly.GetExecutingAssembly()).modPath;
			return Path.Combine(modPath, $"Resources/incompatible_mods.txt");
		}

		private Dictionary<ulong, string> LoadIncompatibleModList() {
			Dictionary<ulong, string> incompatibleMods = new Dictionary<ulong, string>();
			string[] lines;
			using (Stream st = File.OpenRead(TxTPath())) {
				using (StreamReader sr = new StreamReader(st)) {
					lines = sr.ReadToEnd().Split(new string[] { "\n", "\r\n" }, StringSplitOptions.None);
				}
			}

			for (int i = 0; i < lines.Length; i++) {
				string[] strings = lines[i].Split(';');
				ulong steamId = 0;
				if (ulong.TryParse(strings[0], out steamId)) {
					incompatibleMods.Add(steamId, strings[1]);
				}
			}

			return incompatibleMods;
		}

		private ulong[] GetUserModsList() {
			PublishedFileId[] ids = ContentManagerPanel.subscribedItemsTable.ToArray();
			return ids.Select(id => id.AsUInt64).ToArray();
		}
	}
}