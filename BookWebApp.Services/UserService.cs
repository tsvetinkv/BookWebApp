using AutoMapper;
using Microsoft.AspNetCore.Identity;
using BookWebApp.Repositories.Filters.Users;
using BookWebApp.Repositories.Models.Users;
using BookWebApp.Repositories.Repos.Contracts;
using BookWebApp.Services.Base;
using BookWebApp.Services.Base.Contracts;
using BookWebApp.Services.Contracts;
using BookWebApp.Services.Models.Base;
using BookWebApp.Services.Models.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookWebApp.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<User> userManager;

        public UserService(IMapper mapper,
                           IUserData userData,
                           IUserRepository userRepository,
                           UserManager<User> userManager)
            : base(mapper, userData)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }

        public async Task<OperationResponse> CreateAsync(UserRegistrationModel model)
        {
            if (model == null)
            {
                return new OperationResponse { IsSuccessful = false, ErrorMessage = "Моделът не е валиден!" };
            }

            var userFilter = new UserFilter
            {
                Usernames = new HashSet<string> { model.Username },
                Emails = new HashSet<string> { model.Email }
            };

            var users = await this.userRepository.GetAllAsync(userFilter);

            if (users.Any())
            {
                return new OperationResponse { IsSuccessful = false, ErrorMessage = "Потребителят съществува!" };
            }

            var modelForCreate = this.Mapper.Map<User>(model);
            var response = await this.userManager.CreateAsync(modelForCreate, model.Password);

            if (!response.Succeeded)
            {
                return new OperationResponse { IsSuccessful = false, ErrorMessage = "Неуспешно създаване на потребителя!" };
            }

            return new OperationResponse { IsSuccessful = true };
        }

        public async Task<List<UserViewModel>> GetAllAsync()
        {
            var response = await this.userRepository.GetAllAsync(null);
            return this.Mapper.Map<List<UserViewModel>>(response);
        }

        public async Task<UserViewModel> GetAsync(string userId)
        {
            var response = await this.userRepository.GetAsync(userId);
            return this.Mapper.Map<UserViewModel>(response);
        }
    }
}
