using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISO8583
{
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
}
