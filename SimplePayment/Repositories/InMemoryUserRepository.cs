using Common.Interfaces;
using Common.Models;
using Common.Models.SQLite;

namespace SimplePayment.Repositories
{
    public interface IUserFullRepository : IUserRepository, IQueryUserRepository 
    { 
    
    }

    public class InMemoryUserRepository : IUserFullRepository
    {
        //DEVELOPMENT ONLY

        private static List<User> _users = new();

        public InMemoryUserRepository() { }

        public async Task<bool> InsertUser(User user)
        {

            _users.Add(user);

            return true;

        }

        public async Task<User?> QueryUserByEmail(string email)
        {

            return _users.Find(x => x.Email == email);

        }

        public async Task<User?> QueryUserByCPF(string cpf)
        {

            return _users.Find(x => x.CPF == cpf);

        }

        public async Task<UserSQLiteDTO?> QueryUserById(int id)
        {

            //return _users.Find(x => x.Id == id);
            throw new NotImplementedException();

        }

        public Task<UserSQLiteDTO?> QuerySingleUserById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
