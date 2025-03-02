using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessingWorkerService.Repositories.UnityOfWork
{
    public class DbSession : IDisposable
    {

        private Guid _id;
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction
        {
            get; set;
        }

        public DbSession()
        {
            _id = Guid.NewGuid();
            Connection = new SqliteConnection("Data Source=C:\\Users\\santo\\source\\repos\\SimplePayment\\database.db;");
            Connection.Open();
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}
