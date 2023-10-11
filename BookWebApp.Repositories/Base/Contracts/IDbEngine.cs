using System;
using System.Data;

namespace BookWebApp.Repositories.Base.Contracts
{
    public interface IDbEngine : IDisposable
    {
        IDbConnection Connection { get; }

        IDbTransaction Transaction { get; }

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
    }
}
