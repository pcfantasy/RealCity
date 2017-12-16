using ColossalFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RealCity
{
    public class pc_TaxiAI:TaxiAI
    {
        private void UnloadPassengers(ushort vehicleID, ref Vehicle data, ref TransportPassengerData passengerData)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            Vector3 lastFramePosition = data.GetLastFramePosition();
            int num = 0;
            uint num2 = data.m_citizenUnits;
            int num3 = 0;
            while (num2 != 0u)
            {
                uint nextUnit = instance.m_units.m_buffer[(int)((UIntPtr)num2)].m_nextUnit;
                for (int i = 0; i < 5; i++)
                {
                    uint citizen = instance.m_units.m_buffer[(int)((UIntPtr)num2)].GetCitizen(i);
                    if (citizen != 0u)
                    {
                        ushort instance2 = instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_instance;
                        if (instance2 != 0)
                        {
                            Vector3 lastFramePosition2 = instance.m_instances.m_buffer[(int)instance2].GetLastFramePosition();
                            CitizenInfo info = instance.m_instances.m_buffer[(int)instance2].Info;
                            info.m_citizenAI.SetCurrentVehicle(instance2, ref instance.m_instances.m_buffer[(int)instance2], 0, 0u, data.m_targetPos0);
                            int num4 = Mathf.RoundToInt((float)this.m_pricePerKilometer * Vector3.Distance(lastFramePosition2, lastFramePosition) * 0.001f);
                            if (num4 != 0)
                            {
                                //DebugLog.LogToFileOnly("UnloadPassengers ticketPrice pre = " + num4.ToString());
                                CitizenManager instance3 = Singleton<CitizenManager>.instance;
                                ushort homeBuilding = instance3.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_homeBuilding;
                                BuildingManager instance4 = Singleton<BuildingManager>.instance;
                                if ((Singleton<CitizenManager>.instance.m_citizens.m_buffer[citizen].m_flags & Citizen.Flags.Tourist) == Citizen.Flags.None)
                                {
                                    if (comm_data.citizen_money[num2] - num4 > 0)
                                    {
                                        comm_data.citizen_money[num2] = (short)(comm_data.citizen_money[num2] - num4 / comm_data.game_maintain_fee_decrease3);
                                    } else
                                    {
                                        num4 = 0;
                                    }
                                }
                                else
                                {
                                    comm_data.tourist_transport_fee_num += num4;
                                    comm_data.tourist_num++;
                                    if (comm_data.tourist_transport_fee_num > 1000000000000000000)
                                    {
                                        comm_data.tourist_transport_fee_num = 1000000000000000000;
                                    }
                                    if (num4 > 5000)
                                    {
                                        num4 = 5000;
                                    }
                                }
                                Singleton<EconomyManager>.instance.AddResource(EconomyManager.Resource.PublicIncome, num4 / comm_data.game_maintain_fee_decrease3, this.m_info.m_class);
                            }
                            num++;
                            if ((instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_flags & Citizen.Flags.Tourist) != Citizen.Flags.None)
                            {
                                passengerData.m_touristPassengers.m_tempCount = passengerData.m_touristPassengers.m_tempCount + 1u;
                            }
                            else
                            {
                                passengerData.m_residentPassengers.m_tempCount = passengerData.m_residentPassengers.m_tempCount + 1u;
                            }
                            switch (Citizen.GetAgeGroup(instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Age))
                            {
                                case Citizen.AgeGroup.Child:
                                    passengerData.m_childPassengers.m_tempCount = passengerData.m_childPassengers.m_tempCount + 1u;
                                    break;
                                case Citizen.AgeGroup.Teen:
                                    passengerData.m_teenPassengers.m_tempCount = passengerData.m_teenPassengers.m_tempCount + 1u;
                                    break;
                                case Citizen.AgeGroup.Young:
                                    passengerData.m_youngPassengers.m_tempCount = passengerData.m_youngPassengers.m_tempCount + 1u;
                                    break;
                                case Citizen.AgeGroup.Adult:
                                    passengerData.m_adultPassengers.m_tempCount = passengerData.m_adultPassengers.m_tempCount + 1u;
                                    break;
                                case Citizen.AgeGroup.Senior:
                                    passengerData.m_seniorPassengers.m_tempCount = passengerData.m_seniorPassengers.m_tempCount + 1u;
                                    break;
                            }
                        }
                    }
                }
                num2 = nextUnit;
                if (++num3 > 524288)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }
            StatisticBase statisticBase = Singleton<StatisticsManager>.instance.Acquire<StatisticArray>(StatisticType.PassengerCount);
            statisticBase.Acquire<StatisticInt32>((int)this.m_transportInfo.m_transportType, 10).Add(num);
        }
    }
}
