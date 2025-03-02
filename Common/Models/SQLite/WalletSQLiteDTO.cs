using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.SQLite
{
    public class WalletSQLiteDTO
    { //USING DTOs TO AVOID IMPEDANCE MISMATCH. COULD ALSO BE USING AN ORM
        public int? id { get; private set; }
        public int userid { get; private set; }
        public decimal value { get; private set; }
    }
}
