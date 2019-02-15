using ColossalFramework;
using ColossalFramework.Math;
using RealCity.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCity.CustomManager
{
    public class RealCityCitizenManager:CitizenManager
    {
        private void ReleaseCitizenImplementation(uint citizen, ref Citizen data)
        {
            InstanceID id = default(InstanceID);
            // NON-STOCK CODE START
            //TODO: Move this logic to RealCityThreading;
            MainDataStore.citizenMoney[citizen] = 0;
            MainDataStore.isCitizenFirstMovingIn[citizen] = false;
            /// NON-STOCK CODE END ///
            id.Citizen = citizen;
            Singleton<InstanceManager>.instance.ReleaseInstance(id);
            if (data.m_instance != 0)
            {
                this.ReleaseCitizenInstance(data.m_instance);
                data.m_instance = 0;
            }
            data.SetHome(citizen, 0, 0u);
            data.SetWorkplace(citizen, 0, 0u);
            data.SetVisitplace(citizen, 0, 0u);
            data.SetVehicle(citizen, 0, 0u);
            data.SetParkedVehicle(citizen, 0);
            data = default(Citizen);
            this.m_citizens.ReleaseItem(citizen);
            this.m_citizenCount = (int)(this.m_citizens.ItemCount() - 1u);
        }

        private void ReleaseUnitCitizen(uint unit, ref CitizenUnit data, uint citizen)
        {
            if (citizen != 0u)
            {
                // NON-STOCK CODE START
                MainDataStore.familyGoods[unit] = 0;
                MainDataStore.family_money[unit] = 0;
                /// NON-STOCK CODE END ///
                if ((ushort)(data.m_flags & CitizenUnit.Flags.Home) != 0)
                {
                    this.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_homeBuilding = 0;
                }
                if ((ushort)(data.m_flags & (CitizenUnit.Flags.Work | CitizenUnit.Flags.Student)) != 0)
                {
                    this.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_workBuilding = 0;
                }
                if ((ushort)(data.m_flags & CitizenUnit.Flags.Visit) != 0)
                {
                    this.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_visitBuilding = 0;
                }
                if ((ushort)(data.m_flags & CitizenUnit.Flags.Vehicle) != 0)
                {
                    this.m_citizens.m_buffer[(int)((UIntPtr)citizen)].m_vehicle = 0;
                }
            }
        }
    }
}
