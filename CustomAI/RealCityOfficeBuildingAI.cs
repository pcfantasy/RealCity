using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCity.CustomAI
{
    public class RealCityOfficeBuildingAI:PrivateBuildingAI
    {
        private TransferManager.TransferReason GetOutgoingTransferReason()
        {
            ItemClass.SubService subService = this.m_info.m_class.m_subService;
            //if (subService != ItemClass.SubService.OfficeHightech)
            //{
                return TransferManager.TransferReason.None;
            //}
            //return TransferManager.TransferReason.Goods;
        }
    }
}
