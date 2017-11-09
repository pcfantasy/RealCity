using ColossalFramework;
using ColossalFramework.IO;
using ICities;
using System;

namespace RealCity
{
    internal class Message : MessageBase
    {
        public string m_message;

        public uint m_senderID;

        public Message(uint senderID, string message)
        {
            this.m_message = message;
            this.m_senderID = senderID;
        }

        public override uint GetSenderID()
        {
            return this.m_senderID;
        }

        public override string GetSenderName()
        {
            CitizenManager instance = Singleton<CitizenManager>.instance;
            return instance.GetCitizenName(this.m_senderID) ?? instance.GetDefaultCitizenName(this.m_senderID);
        }

        public override string GetText()
        {
            return this.m_message;
        }

        public override bool IsSimilarMessage(MessageBase other)
        {
            return false;
        }

        public override void Serialize(DataSerializer s)
        {
            s.WriteSharedString(this.m_message);
            s.WriteUInt32(this.m_senderID);
        }

        public override void Deserialize(DataSerializer s)
        {
            this.m_message = s.ReadSharedString();
            this.m_senderID = s.ReadUInt32();
            MessageManager ms = Singleton<MessageManager>.instance;

            ms.DeleteMessage(new Message(m_senderID,m_message));
        }

        public override void AfterDeserialize(DataSerializer s)
        {
        }
    }
}
