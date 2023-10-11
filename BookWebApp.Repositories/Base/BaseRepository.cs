using BookWebApp.Repositories.Base.Contracts;

namespace BookWebApp.Repositories.Base
{
    public abstract class BaseRepository
    {
        protected readonly IDbEngine DbEngine;

        protected BaseRepository(IDbEngine dbEngine)
        {
            this.DbEngine = dbEngine;
        }

        public void BeginTransaction()
        {
            this.DbEngine.BeginTransaction();
        }

        public void CommitTransaction()
        {
            this.DbEngine.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            this.DbEngine.RollbackTransaction();
        }
    }
}
