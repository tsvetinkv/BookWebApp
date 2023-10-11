using BookWebApp.Services.Models.Base;
using BookWebApp.Services.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookWebApp.Services.Contracts
{
    public interface IUserService
    {
        Task<List<UserViewModel>> GetAllAsync();

        Task<UserViewModel> GetAsync(string userId);

        Task<OperationResponse> CreateAsync(UserRegistrationModel model);
    }
}
