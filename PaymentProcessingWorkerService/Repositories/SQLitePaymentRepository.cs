using Common.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Common.Models.SQLite;
using Common.Interfaces;
using PaymentProcessingWorkerService.Repositories.UnityOfWork;

namespace PaymentProcessingWorkerService.Repositories
{
    public class SQLitePaymentRepository : IQueryPaymentRepository
    {
        private readonly DbSession _dbSession;
        public SQLitePaymentRepository(DbSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<PaymentSQLiteDTO?> QuerySinglePaymentById(string paymentIdentifier)
        {

            var connection = _dbSession.Connection;

            //using var transaction = connection.BeginTransaction();

            string sql = @"
                SELECT * FROM payments
                WHERE paymentidentifier = :paymentIdentifier 
            ";

            var result = await connection.QueryFirstOrDefaultAsync<PaymentSQLiteDTO>(sql, new { paymentIdentifier });
            //command.CommandText =
            //    ;

            //command.Parameters.AddWithValue("$paymentidentifier", paymentIdentifier);

            //var result = await command.ExecuteReaderAsync();

            return result;

        }
    }
}
