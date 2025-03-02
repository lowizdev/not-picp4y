using Common.Interfaces;
using Common.Models;
using Common.Models.SQLite;
using Dapper;
using Microsoft.Data.Sqlite;
using PaymentProcessingWorkerService.Repositories.UnityOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessingWorkerService.Repositories
{
    public interface IWalletFullRepository : IQueryWalletRepository, IWalletRepository { }
    public class SQLiteWalletRepository: IWalletFullRepository
    {

        private readonly DbSession _dbSession;

        public SQLiteWalletRepository(DbSession dbSession)
        {
            _dbSession = dbSession;
        }

        public Task<bool> InsertWallet(Wallet wallet)
        {
            throw new NotImplementedException();
        }

        public async Task<WalletSQLiteDTO?> QuerySingleWalletByUserId(int userId)
        {

            //using (var connection = new SqliteConnection("Data Source=../database.db"))
            //{
            //    connection.Open();

            var connection = _dbSession.Connection;
            //using var transaction = connection.BeginTransaction();

            string sql = @"
                SELECT * FROM wallets 
                WHERE userid = :userId 
            "; //TODO: SELECT FOR UPDATE WOULD BE NEEDED IF NOT USING SQLITE

            var result = await connection.QueryFirstOrDefaultAsync<WalletSQLiteDTO>(sql, new { userId });

            return result;

            //}


        }

        public async Task<bool> UpdateWallet(Wallet wallet)
        {
            var connection = _dbSession.Connection;
            //using var transaction = connection.BeginTransaction();

            string sql = @"
                UPDATE wallets 
                SET value = :value
                WHERE userid = :userId 
            "; //TODO: SELECT FOR UPDATE WOULD BE NEEDED IF NOT USING SQLITE

            var result = await connection.ExecuteAsync(sql, new { 
                value = wallet.Value,
                userId = wallet.UserId
            });

            return result == 1;
        }
    }
}
