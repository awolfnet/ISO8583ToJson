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
            DateTime transactionUTCDatetime = DateTime.UtcNow;
            DateTime transactionLocalDatetime = DateTime.Now;
            int STAN = 359767;


            SaleRequest saleRequest = new SaleRequest();
            saleRequest.SetProcessingCode(ProcessingCode.SaleRequest, ISO8583.FieldType.n6);
            saleRequest.SetTransmissionDatetime(transactionUTCDatetime, ISO8583.FieldType.n10_datetime);
            saleRequest.SetSystemTraceAuditNumber(STAN, ISO8583.FieldType.n6);
            saleRequest.SetLocalTransactionTime(transactionLocalDatetime, ISO8583.FieldType.n6_time);
            saleRequest.SetLocalTransactionDate(transactionLocalDatetime, ISO8583.FieldType.n4_date);
            saleRequest.SetMerchantCategoryCode(9999, ISO8583.FieldType.n4);
            saleRequest.SetPointofServiceConditionCode(11, ISO8583.FieldType.n2);
            saleRequest.SetCardAcceptorTerminalIdentification("1234");
            saleRequest.SetCardAcceptorIdentificationCode("1234");
            saleRequest.SetCurrencyCode("SGD");
            saleRequest.SetOrderNumber("ORDER2021NUMBER");
            saleRequest.SetTotalAmountOfDebits(100, ISO8583.FieldType.n12);
            saleRequest.SetTransactionDescription($"A cup of orange juice at xxxx from iJooz");

            string payload = saleRequest.ToJson();
            System.Diagnostics.Debug.WriteLine(payload);
            
            string response = "{\"0\":\"0210\",\"1\":\"a23840800ac080060000010001000000\",\"3\":\"000000\",\"7\":\"0908075531\",\"11\":\"552322\",\"12\":\"155422\",\"13\":\"0908\",\"18\":\"5499\",\"25\":\"46\",\"37\":\"100100020210908285320932\",\"39\":\"00\",\"41\":\"10000002\",\"42\":\"110000000001292\",\"49\":\"SGD\",\"62\":\"SG0001T20210908035521664\",\"63\":\"00020101021226540013SG.AIRPAY.WWW0118936009180000006943020469430303UMI52045699530370254041.235802SG5905IJOOZ6009Singapore6106627949622805241004202202109082853630046304264D\",\"88\":\"000000000123\",\"104\":\"A cup of orange juice at SG0001 from iJooz\"}";


            SaleResponse saleResponse= Newtonsoft.Json.JsonConvert.DeserializeObject<SaleResponse>(response);

            string responseCode = saleResponse.GetResponseCode();
        }
    }
}
