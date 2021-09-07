using ISO8583;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISO8583ToJson.PaymentGateway.Models
{
    public class SaleRequest : ISO8583.Model
    {
        public SaleRequest()
        {
            SetMessageTypeIdentifier(new MessageTypeIndicator(
                MessageTypeIndicator.Version.ISO8583_1987,
                MessageTypeIndicator.MessageClass.Financial,
                MessageTypeIndicator.MessageFunction.Request,
                MessageTypeIndicator.MessageOrigin.Acquirer
                ));
        }


    }
}
