using Common.Enums;
using Common.Interfaces;
using Common.Models;
using Common.Models.DTO;

namespace SimplePayment.Services
{
    
    public class UsersService : IUsersService
    {
        private readonly IUserRepository _userRepository;
        public UsersService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CreateUser(UserDTO userDTO) //TODO: USE A MORE MEANINGFUL RETURN TYPE 
        {

            bool isValidCPFCNPJ = true;

            if (!isValidCPFCNPJ)
            {
                return false;
            }

            bool existsCPFCNPJ = (await _userRepository.QueryUserByCPF(userDTO.CPF)) != null;

            if (existsCPFCNPJ)
            {
                return false;
            }

            bool existsEmail = (await _userRepository.QueryUserByEmail(userDTO.Email)) != null;

            if (existsEmail)
            {
                return false;
            }

            //TODO: HASH PASSWORD

            User user = new(null, userDTO.Name, userDTO.Email, userDTO.CPF, userDTO.Password, userDTO.UserType);

            bool result = await _userRepository.InsertUser(user);

            return result;

        }
    }
}
