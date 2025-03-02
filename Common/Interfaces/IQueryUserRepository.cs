using Common.Models;
using Common.Models.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IQueryUserRepository
    {
        Task<UserSQLiteDTO?> QuerySingleUserById(string id);
        Task<User?> QueryUserByCPF(string cpf);
        Task<User?> QueryUserByEmail(string email);
        Task<UserSQLiteDTO?> QueryUserById(int id);
    }
}
