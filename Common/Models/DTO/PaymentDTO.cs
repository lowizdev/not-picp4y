using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.DTO
{
    public class PaymentDTO
    {
        public decimal Value { get; set; }
        public int Payer { get; set; }
        public int Payee { get; set; }
    }
}
