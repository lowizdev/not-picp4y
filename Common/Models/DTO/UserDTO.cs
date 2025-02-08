using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.DTO
{
    public class UserDTO
    {
        public string Name { get;  set; }
        public string Email { get;  set; }
        public string CPF { get;  set; }
        public string Password { get;  set; }
        public EUserType UserType { get;  set; }
    }
}
