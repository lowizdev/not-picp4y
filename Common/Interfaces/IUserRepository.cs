using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> InsertUser(User user);
        Task<User?> QueryUserByCPF(string cpf);
        Task<User?> QueryUserByEmail(string email);
        Task<User?> QueryUserById(int id);
    }
}
