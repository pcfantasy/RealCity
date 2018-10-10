using ColossalFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCity
{
    public class pc_CommonBuildingAI: BuildingAI
    {
        public override void ReleaseBuilding(ushort buildingID, ref Building data)
        {

            int cost = 1000;



            comm_data.building_money[buildingID] = 0;
            comm_data.building_buffer2[buildingID] = 0;
            comm_data.building_buffer1[buildingID] = 0;
            comm_data.building_buffer3[buildingID] = 0;
            comm_data.building_buffer4[buildingID] = 0;

            if ((data.Info.m_class.m_service == ItemClass.Service.Commercial) || (data.Info.m_class.m_service == ItemClass.Service.Industrial) || (data.Info.m_class.m_service == ItemClass.Service.Office) || (data.Info.m_class.m_service == ItemClass.Service.Residential))
            {
                Singleton<EconomyManager>.instance.FetchResource(EconomyManager.Resource.PolicyCost, cost, data.Info.m_class.m_service, ItemClass.SubService.None, ItemClass.Level.Level1);
            }
            this.ManualDeactivation(buildingID, ref data);
            this.BuildingDeactivated(buildingID, ref data);
            base.ReleaseBuilding(buildingID, ref data);
        }

        public override void BuildingDeactivated(ushort buildingID, ref Building data)
        {
            TransferManager.TransferOffer offer = default(TransferManager.TransferOffer);
            offer.Building = buildingID;
            Singleton<TransferManager>.instance.RemoveOutgoingOffer(TransferManager.TransferReason.Garbage, offer);
            Singleton<TransferManager>.instance.RemoveOutgoingOffer(TransferManager.TransferReason.Crime, offer);
            Singleton<TransferManager>.instance.RemoveOutgoingOffer(TransferManager.TransferReason.Sick, offer);
            Singleton<TransferManager>.instance.RemoveOutgoingOffer(TransferManager.TransferReason.Sick2, offer);
            Singleton<TransferManager>.instance.RemoveOutgoingOffer(TransferManager.TransferReason.Dead, offer);
            Singleton<TransferManager>.instance.RemoveOutgoingOffer(TransferManager.TransferReason.Fire, offer);
            Singleton<TransferManager>.instance.RemoveOutgoingOffer(TransferManager.TransferReason.Fire2, offer);
            Singleton<TransferManager>.instance.RemoveOutgoingOffer(TransferManager.TransferReason.ForestFire, offer);
            Singleton<TransferManager>.instance.RemoveOutgoingOffer(TransferManager.TransferReason.Collapsed, offer);
            Singleton<TransferManager>.instance.RemoveOutgoingOffer(TransferManager.TransferReason.Collapsed2, offer);
            Singleton<TransferManager>.instance.RemoveIncomingOffer(TransferManager.TransferReason.Worker0, offer);
            Singleton<TransferManager>.instance.RemoveIncomingOffer(TransferManager.TransferReason.Worker1, offer);
            Singleton<TransferManager>.instance.RemoveIncomingOffer(TransferManager.TransferReason.Worker2, offer);
            Singleton<TransferManager>.instance.RemoveIncomingOffer(TransferManager.TransferReason.Worker3, offer);
            data.m_flags &= ~Building.Flags.Active;
            this.EmptyBuilding(buildingID, ref data, CitizenUnit.Flags.Created, false);
            base.BuildingDeactivated(buildingID, ref data);
        }

        protected void EmptyBuilding(ushort buildingID, ref Building data, CitizenUnit.Flags flags, bool onlyMoving)
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            uint num = data.m_citizenUnits;
            int num2 = 0;
            while (num != 0u)
            {
                if ((ushort)(instance.m_units.m_buffer[(int)((UIntPtr)num)].m_flags & flags) != 0)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        uint citizen = instance.m_units.m_buffer[(int)((UIntPtr)num)].GetCitizen(i);
                        if (citizen != 0u)
                        {
                            ushort instance2 = instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_instance;
                            if (((!onlyMoving && instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].GetBuildingByLocation() == buildingID) || (instance2 != 0 && instance.m_instances.m_buffer[(int)instance2].m_targetBuilding == buildingID)) && !instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Collapsed)
                            {
                                ushort num3 = 0;
                                if (instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_homeBuilding == buildingID)
                                {
                                    num3 = instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_workBuilding;
                                }
                                else if (instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_workBuilding == buildingID)
                                {
                                    num3 = instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_homeBuilding;
                                }
                                else if (instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_visitBuilding == buildingID)
                                {
                                    if (instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Arrested)
                                    {
                                        instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].Arrested = false;
                                        if (instance2 != 0)
                                        {
                                            instance.ReleaseCitizenInstance(instance2);
                                        }
                                    }
                                    instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].SetVisitplace(citizen, 0, 0u);
                                    num3 = instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_homeBuilding;
                                }
                                if (num3 != 0)
                                {
                                    CitizenInfo citizenInfo = instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)].GetCitizenInfo(citizen);
                                    HumanAI humanAI = citizenInfo.m_citizenAI as HumanAI;
                                    if (humanAI != null)
                                    {
                                        Citizen[] expr_242_cp_0 = instance.m_citizens.m_buffer;
                                        UIntPtr expr_242_cp_1 = (UIntPtr)citizen;
                                        expr_242_cp_0[(int)expr_242_cp_1].m_flags = (expr_242_cp_0[(int)expr_242_cp_1].m_flags & ~Citizen.Flags.Evacuating);
                                        humanAI.StartMoving(citizen, ref instance.m_citizens.m_buffer[(int)((UIntPtr)citizen)], buildingID, num3);
                                    }
                                }
                            }
                        }
                    }
                }
                num = instance.m_units.m_buffer[(int)((UIntPtr)num)].m_nextUnit;
                if (++num2 > 524288)
                {
                    CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid list detected!\n" + Environment.StackTrace);
                    break;
                }
            }
        }
    }
}
