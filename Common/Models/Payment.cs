using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Payment
    {
        public string? PaymentIdentifier { get; set; }
        public decimal Value { get; private set; } //TODO: SHOULD BE INTEGER REPRESENTING CENTS
        public int PayerId { get; private set; }
        public int PayeeId { get; private set; }
        public EPaymentStatus PaymentStatus { get; private set; }
        //public DateTime? Created { get; private set; } //TODO: INCLUDE DATE IN PAYMENT

        public Payment(string? id, decimal value, int payerId, int payeeId, EPaymentStatus paymentStatus)
        {

            PaymentIdentifier = id;
            Value = value;
            PayerId = payerId;
            PayeeId = payeeId;
            PaymentStatus = paymentStatus;

        }
    }
}
