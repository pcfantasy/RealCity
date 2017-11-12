using ColossalFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCity
{
    public abstract class pc_BuildingWorldInfoPanel : WorldInfoPanel
    {
        private string GetName()
        {
            if (this.m_InstanceID.Type == InstanceType.Building && this.m_InstanceID.Building != 0)
            {
                comm_data.current_buildingid = this.m_InstanceID.Building;
                return Singleton<BuildingManager>.instance.GetBuildingName(this.m_InstanceID.Building, InstanceID.Empty);
            }
            return string.Empty;
        }
    }
}
