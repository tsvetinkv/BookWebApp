using BookWebApp.Repositories.Base.Models;
using BookWebApp.Repositories.Filters.Users;
using BookWebApp.Repositories.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookWebApp.Repositories.Repos.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync(UserFilter filter);

        Task<User> GetAsync(string userId);

        Task<SaveResult> UpdateAsync(User model);
    }
}
