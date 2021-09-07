using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISO8583
{
    public class MessageTypeIndicator
    {
        public enum Version
        {
            ISO8583_1987 = 0000,
            ISO8583_1993 = 1000,
            ISO8583_2003 = 2000,

        }

        public enum MessageClass
        {
            Authorization = 100,
            Financial = 200,
            FileActions = 300,
            ReversalAndChargeback = 400,
            Reconciliation = 500,
            Administrative = 600,
            FeeCollection = 700,
            NetworkManagement = 800
        }

        public enum MessageFunction
        {
            Request = 00,
            RequestResponse = 10,
            Advice = 20,
            AdviceResponse = 30,
            Notification = 40,
            NotificationAcknowledgement = 50,
            Instruction = 60,
            InstructionAcknowledgement = 70,

        }

        public enum MessageOrigin
        {
            Acquirer = 0,
            AcquirerRepeat = 1,
            Issuer = 2,
            IssuerRepeat = 3,
            Other = 4,
            OtherRepeat = 5
        }

        protected Version _version;
        protected MessageClass _messageClass;
        protected MessageFunction _messageFunction;
        protected MessageOrigin _messageOrigin;

        public MessageTypeIndicator(Version version, MessageClass messageClass, MessageFunction messageFunction, MessageOrigin messageOrigin)
        {
            _version = version;
            _messageClass = messageClass;
            _messageFunction = messageFunction;
            _messageOrigin = messageOrigin;

        }

        public override string ToString()
        {
            int mit = (int)_version + (int)_messageClass + (int)_messageFunction + (int)_messageOrigin;
            return mit.ToString(FieldType.n4);
        }

    }
}
