using Common.Interfaces;
using Common.Models;
using Microsoft.Data.Sqlite;
using System.Diagnostics.Eventing.Reader;

namespace SimplePayment.Repositories
{

    public class SQLitePaymentRepository : IPaymentRepository
    {

        public SQLitePaymentRepository()
        {

        }

        public async Task<Payment?> InsertPayment(Payment payment)
        {

            using (var connection = new SqliteConnection("Data Source=C:\\Users\\santo\\source\\repos\\SimplePayment\\database.db;"))
            {
                connection.Open();

                //using var transaction = connection.BeginTransaction();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
                    INSERT INTO payments (paymentidentifier, value, payerid, payeeid, paymentstatus) 
                    VALUES ($paymentidentifier, $value, $payerid, $payeeid, $paymentstatus)
                ";

                command.Parameters.AddWithValue("$paymentidentifier", payment.PaymentIdentifier);
                command.Parameters.AddWithValue("$value", payment.Value);
                command.Parameters.AddWithValue("$payerid", payment.PayerId);
                command.Parameters.AddWithValue("$payeeid", payment.PayeeId);
                command.Parameters.AddWithValue("$paymentstatus", payment.PaymentStatus);

                var result = await command.ExecuteNonQueryAsync();


                //transaction.Commit();

                if (result == 1)
                {

                    return payment;

                }

                return null;


            }

        }

    }
}
