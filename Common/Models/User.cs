using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class User
    {
        public int? Id { get; private set; } // TODO: MAKING ID INTEGER TO COMPLY WITH REQUIREMENTS, SHOULD BE UUIDv4
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string CPF { get; private set; }
        public string Password { get; private set; }
        public EUserType UserType { get; private set; }

        public User(int? id, string name, string email, string cpf, string password, EUserType userType)
        {
            Id = id;
            Name = name;
            Email = email;
            CPF = cpf;
            Password = password;
            UserType = userType;

            this.SelfValidate();
        }

        private void SelfValidate() 
        {

            if (String.IsNullOrEmpty(Name)) 
            { 
                throw new Exception("User Name is empty");
            }

            if (String.IsNullOrEmpty(Email))
            {
                throw new Exception("User Email is empty");
            }

            if (String.IsNullOrEmpty(Password))
            {
                throw new Exception("User Password is empty");
            }

        }

    }
}
