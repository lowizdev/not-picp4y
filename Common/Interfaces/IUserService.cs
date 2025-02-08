using Common.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IUsersService
    {
        Task<bool> CreateUser(UserDTO userDTO);
    }
}
