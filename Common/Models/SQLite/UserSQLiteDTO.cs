using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.SQLite
{
    public class UserSQLiteDTO
    {
        public int id { get;  set; } // TODO: MAKING ID INTEGER TO COMPLY WITH REQUIREMENTS, SHOULD BE UUIDv4
        public string name { get;  set; }
        public string email { get;  set; }
        public string cpf { get;  set; }
        public string password { get;  set; }
        public int usertype { get;  set; }
    }
}
