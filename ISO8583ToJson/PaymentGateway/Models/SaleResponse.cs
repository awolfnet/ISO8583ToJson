using ISO8583;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISO8583ToJson.PaymentGateway.Models
{
    public class SaleResponse : ISO8583.Model
    {
        /// <summary>
        /// Get value from field
        /// </summary>
        /// <param name="field">field</param>
        /// <returns></returns>
        public string GetQRCode()
        {
            return GetFieldValue(Field.ReservedField_63);
        }

        public string TryGetRetrievalReferenceNumber()
        {
            string value = string.Empty;
            bool hasRetrievalReferenceNumber = TryGetFieldValue(Field.RetrievalReferenceNumber, out value);
            return value;
        }

    }
}
