using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessingWorkerService.Repositories.UnityOfWork
{
    public interface IUnityOfWork : IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
    public class UnityOfWork : IUnityOfWork
    {

        private readonly DbSession _session;

        public UnityOfWork(DbSession session)
        {
            _session = session;
        }
        public void BeginTransaction()
        {
            _session.Transaction = _session.Connection.BeginTransaction();
        }

        public void Commit()
        {
            _session.Transaction.Commit();
            Dispose();
        }

        public void Dispose()
        {
            _session?.Transaction.Dispose();
        }

        public void Rollback()
        {
            _session.Transaction.Rollback();
            Dispose();
        }
    }
}
