using ColossalFramework;
using HarmonyLib;
using RealCity.CustomData;
using System;
using System.Reflection;
using UnityEngine;

namespace RealCity.Patch
{
	[HarmonyPatch]
	public class CommonBuildingAIGetColorPatch
	{
		public static MethodBase TargetMethod() {
			return typeof(CommonBuildingAI).GetMethod(
					"GetColor",
					BindingFlags.Instance | BindingFlags.Public,
					null,
					new[] { typeof(ushort), typeof(Building).MakeByRefType(), typeof(InfoManager.InfoMode) },
					new ParameterModifier[0]);
		}

		public static void Postfix(ushort buildingID, ref Building data, InfoManager.InfoMode infoMode, ref Color __result) {
			if (infoMode == InfoManager.InfoMode.LandValue) {
				ItemClass @class = data.Info.m_class;
				ItemClass.Service service = @class.m_service;
				switch (service) {
					case ItemClass.Service.Residential:
					case ItemClass.Service.Office:
					case ItemClass.Service.Industrial:
					case ItemClass.Service.Commercial:
						if (BuildingData.buildingMoneyThreat[buildingID] < 0.5f)
							__result = Color.Lerp(Color.green, Color.yellow, BuildingData.buildingMoneyThreat[buildingID] * 2.0f);
						else
							__result = Color.Lerp(Color.yellow, Color.red, (BuildingData.buildingMoneyThreat[buildingID] - 0.5f) * 2.0f);
						break;
				}
			}
		}
	}
}
