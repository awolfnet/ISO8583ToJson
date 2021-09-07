using System;
using System.Collections.Generic;
using System.Text;

namespace ISO8583ToJson.Models
{

    public class ISO8583 : Dictionary<String, String>
    {
        private byte[] _bitmap = new byte[129];

        private bool _hasSecondaryBitmap = false;

        private MessageTypeIndicator _messageTypeIndicator;

        public ISO8583()
        {

        }

        protected void SetMessageTypeIdentifier(MessageTypeIndicator messageType)
        {
            _messageTypeIndicator = messageType;
            Add(Field.MessageTypeIdentifier.FieldNumber(), _messageTypeIndicator.ToString());
        }

        protected void SetFieldValue(Field fieldName, string value)
        {
            int field = (int)fieldName;
            Add(fieldName.FieldNumber(), value);

            _bitmap[field] = 1;
            if (field > 64)
            {
                _hasSecondaryBitmap = true;
            }
        }

        protected string GetFieldValue(Field field)
        {
            return this[field.FieldNumber()];
        }

        protected bool TryGetFieldValue(Field field, out string value)
        {
            return TryGetValue(field.FieldNumber(), out value);
        }

        public string ToJson()
        {
            string bitmap = GetBitmap().ToLower();
            Add(Field.Bitmap.FieldNumber(), bitmap);
            //Can be replaced with your own json util.
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            return json;
        }

        protected string GetBitmap()
        {
            int bitmapLength = 64;
            if (_hasSecondaryBitmap)
            {
                _bitmap[1] = 1;
                bitmapLength = 128;
            }

            StringBuilder map = new StringBuilder();
            for (int i = 1; i < bitmapLength; i += 4)
            {

                int n = _bitmap[i] * 0b1000 + _bitmap[i + 1] * 0b100 + _bitmap[i + 2] * 0b10 + _bitmap[i + 3];
                map.Append(n.ToString("X"));
            }

            return map.ToString();
        }

        public void SetTransmissionDatetime(DateTime datetime, string format)
        {
            SetFieldValue(Field.TransmissionDatetime, datetime.ToString(format));
        }

        public void SetProcessingCode(int code, string format)
        {
            SetFieldValue(Field.ProcessingCode, code.ToString(format));

        }

        public void SetSystemTraceAuditNumber(int number, string format)
        {
            SetFieldValue(Field.SystemTraceAuditNumber, number.ToString(format));

        }

        public void SetLocalTransactionTime(DateTime time, string format)
        {
            SetFieldValue(Field.LocalTransactionTime, time.ToString(format));
        }

        public void SetLocalTransactionDate(DateTime date, string format)
        {
            SetFieldValue(Field.LocalTransactionDate, date.ToString(format));
        }

        /// <summary>
        /// Merchant category code
        /// https://raw.githubusercontent.com/greggles/mcc-codes/main/mcc_codes.csv
        /// </summary>
        /// <param name="type"></param>
        /// <param name="format"></param>
        public void SetMerchantCategoryCode(int code, string format)
        {
            SetFieldValue(Field.MerchantCategoryCode, code.ToString(format));
        }

        public void SetPointofServiceConditionCode(int code, string format)
        {
            SetFieldValue(Field.PointOfServiceConditionCode, code.ToString(format));
        }

        public void SetCardAcceptorTerminalIdentification(string tid)
        {
            SetFieldValue(Field.CardAcceptorTerminalIdentification, tid);
        }

        public void SetCardAcceptorIdentificationCode(string mid)
        {
            SetFieldValue(Field.CardAcceptorIdentificationCode, mid);
        }

        public void SetAdditionalData(string data)
        {
            SetFieldValue(Field.AdditionalData, data);
        }

        public void SetCurrencyCode(string code)
        {
            SetFieldValue(Field.CurrencyCode, code);
        }

        public void SetAdditionalAmounts(string amounts)
        {
            SetFieldValue(Field.AdditionalAmounts, amounts);
        }

        public void SetOrderNumber(string orderNumber)
        {
            SetFieldValue(Field.OrderNumber, orderNumber);
        }

        public void SetTotalAmountOfDebits(int amount, string format)
        {
            SetFieldValue(Field.TotalAmountOfDebits, amount.ToString(format));

        }

        public void SetTransactionDescription(string description)
        {
            SetFieldValue(Field.TransactionDescription, description);
        }

        public string GetRetrievalReferenceNumber()
        {
            return GetFieldValue(Field.RetrievalReferenceNumber);
        }

        public string GetResponseCode()
        {
            return GetFieldValue(Field.ResponseCode);
        }

        public enum Field
        {
            MessageTypeIdentifier = 0,
            Bitmap = 1,
            ProcessingCode = 3,
            TransmissionDatetime = 7,
            SystemTraceAuditNumber = 11,
            LocalTransactionTime = 12,
            LocalTransactionDate = 13,
            MerchantCategoryCode = 18,
            PointOfServiceConditionCode = 25,
            RetrievalReferenceNumber = 37,
            ResponseCode = 39,
            CardAcceptorTerminalIdentification = 41,
            CardAcceptorIdentificationCode = 42,
            AdditionalData = 48,
            CurrencyCode = 49,
            AdditionalAmounts = 54,
            OrderNumber = 62,
            ReservedField_63 = 63,
            TotalAmountOfDebits = 88,
            TransactionDescription = 104
        }

        public static class FieldType
        {
            /// <summary>
            /// Fixed length field of 2 digits
            /// </summary>
            public const string n2 = "00";

            /// <summary>
            /// Fixed length field of 4 digits
            /// </summary>
            public const string n4 = "0000";

            /// <summary>
            /// Fixed length field of 4 digits Month and Day
            /// </summary>
            public const string n4_date = "MMdd";

            /// <summary>
            /// Fixed length field of 6 digits
            /// </summary>
            public const string n6 = "000000";

            /// <summary>
            /// Fixed length field of 6 digits Hours Minutes and Seconds
            /// </summary>
            public const string n6_time = "HHmmss";

            /// <summary>
            /// Fixed length field of 10 digits date and time
            /// </summary>
            public const string n10_datetime = "MMddHHmmss";

            /// <summary>
            /// Fixed length field of 12 digits
            /// </summary>
            public const string n12 = "000000000000";
        }

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

    public static class FieldExtensions
    {
        public static string FieldNumber(this ISO8583.Field fieldName)
        {
            return ((int)fieldName).ToString();
        }
    }
}
