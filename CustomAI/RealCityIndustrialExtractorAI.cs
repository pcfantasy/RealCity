using RealCity.Util;

namespace RealCity.CustomAI
{
    public class RealCityIndustrialExtractorAI
    {
        public delegate TransferManager.TransferReason IndustrialExtractorAIGetOutgoingTransferReason(IndustrialExtractorAI IndustrialExtractorAI);
        public static IndustrialExtractorAIGetOutgoingTransferReason GetOutgoingTransferReason;

        public static void InitDelegate()
        {
            if (GetOutgoingTransferReason != null)
                return;

            DebugLog.LogToFileOnly("Try to InitDelegate in RealCityIndustrialExtractorAI");
            GetOutgoingTransferReason = FastDelegateFactory.Create<IndustrialExtractorAIGetOutgoingTransferReason>(typeof(IndustrialExtractorAI), "GetOutgoingTransferReason", instanceMethod: true);
        }
    }
}

