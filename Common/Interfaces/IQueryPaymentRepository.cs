using Common.Models.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IQueryPaymentRepository
    {
        Task<PaymentSQLiteDTO?> QuerySinglePaymentById(string paymentIdentifier);
    }
}
