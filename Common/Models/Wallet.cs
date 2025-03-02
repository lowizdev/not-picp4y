using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Wallet
    {
        public int? Id { get; private set; }
        public int UserId { get; private set; }

        public decimal Value { get; private set; }
        public Wallet(int? id, int userId, decimal value) 
        { 
            Id = id;
            UserId = userId;
            Value = value;
        }

    }
}
