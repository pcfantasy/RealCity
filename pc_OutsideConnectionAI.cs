using ColossalFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UnityEngine;

namespace RealCity
{
    public class pc_OutsideConnectionAI : BuildingAI
    {
        public static int m_cargoCapacity = 40;

        public static int m_residentCapacity = 1000;

        public static int m_touristFactor0 = 325;

        public static int m_touristFactor1 = 125;

        public static int m_touristFactor2 = 50;

        public static TransferManager.TransferReason m_dummyTrafficReason = TransferManager.TransferReason.None;

        public static int m_dummyTrafficFactor = 1000;

        public override void SimulationStep(ushort buildingID, ref Building data)
        {
            base.SimulationStep(buildingID, ref data);
            if ((Singleton<ToolManager>.instance.m_properties.m_mode & ItemClass.Availability.Game) != ItemClass.Availability.None)
            {
                int budget = Singleton<EconomyManager>.instance.GetBudget(this.m_info.m_class);
                int productionRate = OutsideConnectionAI.GetProductionRate(100, budget);
                System.Random rand = new System.Random();

                if (data.Info.m_class.m_service == ItemClass.Service.Road)
                {
                    m_dummyTrafficReason = TransferManager.TransferReason.DummyCar;
                }
                else if (data.Info.m_class.m_subService == ItemClass.SubService.PublicTransportPlane)
                {
                    m_dummyTrafficReason = TransferManager.TransferReason.DummyPlane;
                }
                else if (data.Info.m_class.m_subService == ItemClass.SubService.PublicTransportShip)
                {
                    m_dummyTrafficReason = TransferManager.TransferReason.DummyShip;
                }
                else if (data.Info.m_class.m_subService == ItemClass.SubService.PublicTransportTrain)
                {
                    m_dummyTrafficReason = TransferManager.TransferReason.DummyTrain;
                }

                m_dummyTrafficFactor = rand.Next(800) + 200;
                if (comm_data.isCoalsGettedFinal == false)
                {
                    m_residentCapacity = 0;
                    m_touristFactor0 = 0;
                    m_touristFactor1 = 0;
                    m_touristFactor2 = 0;
                }
                else
                {
                    m_residentCapacity = 1000;
                    m_touristFactor0 = 325;
                    m_touristFactor1 = 125;
                    m_touristFactor2 = 50;
                }
                OutsideConnectionAI.AddConnectionOffers(buildingID, ref data, productionRate, m_cargoCapacity, m_residentCapacity, m_touristFactor0, m_touristFactor1, m_touristFactor2, m_dummyTrafficReason, m_dummyTrafficFactor);
            }
        }
    }
}