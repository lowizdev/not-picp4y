using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.SQLite
{
    public class PaymentSQLiteDTO
    {
        public string? paymentidentifier { get; set; }
        public decimal value { get; set; } //TODO: SHOULD BE INTEGER REPRESENTING CENTS
        public int payerid { get; set; }
        public int payeeid { get; set; }
        public int paymentstatus { get;  set; }
    }
}
