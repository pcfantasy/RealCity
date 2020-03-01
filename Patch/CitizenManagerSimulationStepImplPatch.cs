using ColossalFramework;
using ColossalFramework.Math;
using Harmony;
using RealCity.CustomData;
using RealCity.Util;
using System;
using System.Reflection;

namespace RealCity.Patch
{
    [HarmonyPatch]
    public class CitizenManagerSimulationStepImplPatch
    {
        public static MethodBase TargetMethod()
        {
            return typeof(CitizenManager).GetMethod("SimulationStepImpl", BindingFlags.NonPublic | BindingFlags.Instance);
        }
		public static void Prefix(int subStep, ref ushort __state)
		{
			__state = 0;
			var instance = Singleton<CitizenManager>.instance;
			if (subStep != 0)
			{
				int num = (int)(Singleton<SimulationManager>.instance.m_currentFrameIndex & 4095u);
				int num2 = num * 256;
				int num3 = (num + 1) * 256 - 1;
				for (int i = num2; i <= num3; i++)
				{
					if ((instance.m_citizens.m_buffer[i].m_flags & Citizen.Flags.Created) != Citizen.Flags.None)
					{
						CitizenInfo citizenInfo = instance.m_citizens.m_buffer[i].GetCitizenInfo((uint)i);
						if (citizenInfo.m_citizenAI is ResidentAI)
						{
							ushort homeBuilding = instance.m_citizens.m_buffer[i].m_homeBuilding;
							uint citizenUnit = CitizenData.GetCitizenUnit(homeBuilding);
							uint containingUnit = instance.m_citizens.m_buffer[i].GetContainingUnit((uint)i, citizenUnit, CitizenUnit.Flags.Home);

							if (containingUnit != 0)
							{
								__state = instance.m_units.m_buffer[containingUnit].m_goods;
								//DebugLog.LogToFileOnly($"m_goods pre is {__state}");
							}
						}
					}
				}
			}
		}

		public static void Postfix(int subStep, ref ushort __state)
        {
			var instance = Singleton<CitizenManager>.instance;
			if (subStep != 0)
			{
				int num = (int)(Singleton<SimulationManager>.instance.m_currentFrameIndex & 4095u);
				int num2 = num * 256;
				int num3 = (num + 1) * 256 - 1;
				for (int i = num2; i <= num3; i++)
				{
					if ((instance.m_citizens.m_buffer[i].m_flags & Citizen.Flags.Created) != Citizen.Flags.None)
					{
						CitizenInfo citizenInfo = instance.m_citizens.m_buffer[i].GetCitizenInfo((uint)i);
						if (citizenInfo.m_citizenAI is ResidentAI)
						{
							ushort homeBuilding = instance.m_citizens.m_buffer[i].m_homeBuilding;
							uint citizenUnit = CitizenData.GetCitizenUnit(homeBuilding);
							uint containingUnit = instance.m_citizens.m_buffer[i].GetContainingUnit((uint)i, citizenUnit, CitizenUnit.Flags.Home);

							if (containingUnit != 0)
							{
								if (CitizenData.citizenCanUpdateGoods[i])
								{
									//DebugLog.LogToFileOnly($"can update m_goods is {instance.m_units.m_buffer[containingUnit].m_goods}, pre is {__state}");
									CitizenData.citizenCanUpdateGoods[i] = false;
								}
								else
								{
									if (instance.m_units.m_buffer[containingUnit].m_goods > __state)
									{
										//DebugLog.LogToFileOnly($"reject update m_goods is {instance.m_units.m_buffer[containingUnit].m_goods}, pre is {__state}");
										instance.m_units.m_buffer[containingUnit].m_goods = __state;
									}
									else
									{
										//DebugLog.LogToFileOnly($"can update m_goods(reduced) is {instance.m_units.m_buffer[containingUnit].m_goods}, pre is {__state}");
									}
								}
							}
						}
					}
				}
			}
		}
    }
}
