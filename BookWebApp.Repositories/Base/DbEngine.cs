using Microsoft.Data.SqlClient;
using MySqlConnector;
using BookWebApp.Repositories.Base.Contracts;
using System.Data;

namespace BookWebApp.Repositories.Base
{
    public class DbEngine : IDbEngine
    {
        private readonly string sqlConnectionString;

        private SqlConnection connection;
        private IDbTransaction transaction;
        private int transactionCounter;

        public DbEngine(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
            this.GetMySQLConnection();
        }

        public IDbConnection Connection
        {
            get { return this.connection; }
        }

        public IDbTransaction Transaction
        {
            get { return this.transaction; }
        }

        public void BeginTransaction()
        {
            if (this.transactionCounter == 0)
            {
                if (this.connection.State == ConnectionState.Closed)
                {
                    this.connection.Open();
                }

                this.transaction = this.connection.BeginTransaction();
            }

            this.transactionCounter++;
        }

        public void CommitTransaction()
        {
            if (this.transactionCounter == 1)
            {
                try
                {
                    this.transaction.Commit();
                }
                finally
                {
                    this.DisposeTransaction();
                }
            }

            this.transactionCounter--;
        }

        public void RollbackTransaction()
        {
            if (this.transactionCounter == 1)
            {
                try
                {
                    this.transaction.Rollback();
                }
                finally
                {
                    this.DisposeTransaction();
                }
            }

            this.transactionCounter--;
        }

        public void Dispose()
        {
            this.DisposeTransaction();
            this.connection?.Dispose();
            this.connection = null;
        }
        private void DisposeTransaction()
        {
            this.transaction?.Dispose();
            this.transaction = null;
        }

        private SqlConnection GetMySQLConnection()
        {
            this.connection ??= new SqlConnection(this.sqlConnectionString);
            return this.connection;
        }
    }
}
