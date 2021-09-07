using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISO8583ToJson.PaymentGateway.Enums
{
    public static class ProcessingCode
    {
        public const int SaleRequest = 0;
        public const int QueryRequest = 300000;
        public const int ReversalRequest = 0;
        public const int VoidRequest = 0;
        public const int BatchSubmitRequest = 0;
    }
}
