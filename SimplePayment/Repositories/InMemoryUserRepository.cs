using Common.Interfaces;
using Common.Models;

namespace SimplePayment.Repositories
{
    
    public class InMemoryUserRepository : IUserRepository
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

        public async Task<User?> QueryUserById(int id)
        {

            return _users.Find(x => x.Id == id);

        }

    }
}
