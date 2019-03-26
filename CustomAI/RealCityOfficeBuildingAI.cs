using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealCity.CustomAI
{
    public class RealCityOfficeBuildingAI
    {
        public static TransferManager.TransferReason OfficeBuildingAIGetOutgoingTransferReasonPreFix()
        {
                return TransferManager.TransferReason.None;
        }
    }
}
