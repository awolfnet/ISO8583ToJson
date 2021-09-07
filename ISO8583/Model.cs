using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISO8583
{

    public class Model : Dictionary<String, String>
    {
        private byte[] _bitmap = new byte[129];

        private bool _hasSecondaryBitmap = false;

        private MessageTypeIndicator _messageTypeIndicator;

        public Model()
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



    }


}
