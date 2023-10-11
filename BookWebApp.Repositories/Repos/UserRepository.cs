using BookWebApp.Repositories.Base;
using BookWebApp.Repositories.Base.Contracts;
using BookWebApp.Repositories.Models.Users;
using BookWebApp.Repositories.Repos.Contracts;
using System.Threading.Tasks;
using Dapper;
using BookWebApp.Repositories.Base.Models;
using System.Collections.Generic;
using BookWebApp.Repositories.Filters.Users;
using System.Linq;
using System.Text;

namespace BookWebApp.Repositories.Repos
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IDbEngine dbEngine) : base(dbEngine)
        {
        }

        public async Task<IEnumerable<User>> GetAllAsync(UserFilter filter)
        {
            filter ??= new UserFilter();

            var connection = this.DbEngine.Connection;

            var whereClause = new StringBuilder("1 = 1");
            whereClause.Append(filter.Ids.Any() ? " AND u.Id in @ids" : string.Empty);
            whereClause.Append(filter.Usernames.Any() ? $" AND u.UserName = @usernames" : string.Empty);
            whereClause.Append(filter.Emails.Any() ? " AND u.Email in @emails" : string.Empty);

            var query = $@"SELECT u.*
                       FROM AspNetUsers u 
                       WHERE {whereClause}";

            var users = await connection.QueryAsync<User>(query, filter, transaction: this.DbEngine.Transaction);
            return users;
        }

        public async Task<User> GetAsync(string userId)
        {
            var connection = this.DbEngine.Connection;
            return await connection.GetAsync<User>(userId, transaction: this.DbEngine.Transaction);
        }

        public async Task<SaveResult> UpdateAsync(User model)
        {
            var connection = this.DbEngine.Connection;
            int rows = await connection.UpdateAsync(model, transaction: this.DbEngine.Transaction);

            return rows < 1 ? new SaveResult { IsSuccessful = false, ErrorMessage = "Неуспешен запис на акаунт." } : new SaveResult { IsSuccessful = true };
        }
    }
}
