using ISO8583ToJson.PaymentGateway.Enums;
using ISO8583ToJson.PaymentGateway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISO8583ToJson.PaymentGateway.Controllers
{
    public class Unionpay
    {

        public void Sale()
        {
            ISO8583.Model model = new ISO8583.Model();

            DateTime transactionUTCDatetime = DateTime.UtcNow;
            DateTime transactionLocalDatetime = DateTime.Now;
            string nonce = Common.GetNonce();
            int STAN = GetSystemTraceAuditNumber();

            SaleRequest saleRequest = new SaleRequest();
            saleRequest.SetProcessingCode(ProcessingCode.SaleRequest, ISO8583.FieldType.n6);
            saleRequest.SetTransmissionDatetime(transactionUTCDatetime, ISO8583.FieldType.n10_datetime);
            saleRequest.SetSystemTraceAuditNumber(STAN, ISO8583.FieldType.n6);
            saleRequest.SetLocalTransactionTime(transactionLocalDatetime, ISO8583.FieldType.n6_time);
            saleRequest.SetLocalTransactionDate(transactionLocalDatetime, ISO8583.FieldType.n4_date);
            saleRequest.SetMerchantCategoryCode(Configuration.MerchantCategoryCode, ISO8583.FieldType.n4);
            saleRequest.SetPointofServiceConditionCode(Configuration.PointofServiceConditionCode, ISO8583.FieldType.n2);
            saleRequest.SetCardAcceptorTerminalIdentification(Configuration.TerminalIdentification);
            saleRequest.SetCardAcceptorIdentificationCode(Configuration.IdentificationCode);
            saleRequest.SetCurrencyCode(Configuration.CurrencyCode);
            saleRequest.SetOrderNumber(order.PaymentDetail.TransactionID);
            saleRequest.SetTotalAmountOfDebits((int)(order.PaymentDetail.Amount * 100), ISO8583.FieldType.n12);
            saleRequest.SetTransactionDescription($"A cup of orange juice at {order.PaymentDetail.TerminalID} from iJooz");

            string payload = saleRequest.ToJson();
        }
    }
}
