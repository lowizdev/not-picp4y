using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.RabbitMQ
{
    public class PaymentCreatedMessage
    {

        public string PaymentIdentifier { get; set; }
        public DateTime? PublishedDate { get; set; }

    }
}
