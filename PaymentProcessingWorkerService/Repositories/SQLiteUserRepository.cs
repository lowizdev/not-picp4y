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
    public class SQLiteUserRepository: IQueryUserRepository
    {
        private readonly DbSession _dbSession;
        public SQLiteUserRepository(DbSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<UserSQLiteDTO?> QuerySingleUserById(string id)
        {

            var connection = _dbSession.Connection;
            
            //connection.Open();

            string sql = @"
                SELECT * FROM users
                WHERE id = :id 
            ";

            var result = await connection.QueryFirstOrDefaultAsync<UserSQLiteDTO>(sql, new { id });

            return result;

            

        }

        public Task<User?> QueryUserByCPF(string cpf)
        {
            throw new NotImplementedException();
        }

        public Task<User?> QueryUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<UserSQLiteDTO?> QueryUserById(int id)
        {
            var connection = _dbSession.Connection;

            //connection.Open();

            string sql = @"
                SELECT * FROM users
                WHERE id = :id 
            ";

            var result = await connection.QueryFirstOrDefaultAsync<UserSQLiteDTO>(sql, new { id });

            return result;
        }
    }
}
