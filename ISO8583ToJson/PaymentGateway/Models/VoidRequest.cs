using ISO8583ToJson.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISO8583ToJson.PaymentGateway.Models
{
    public class VoidRequest : ISO8583
    {
        public VoidRequest()
        {
            SetMessageTypeIdentifier(new MessageTypeIndicator(
                MessageTypeIndicator.Version.ISO8583_1987,
                MessageTypeIndicator.MessageClass.ReversalAndChargeback,
                MessageTypeIndicator.MessageFunction.Notification,
                MessageTypeIndicator.MessageOrigin.Acquirer
            ));

        }
    }
}
