using BookWebApp.Repositories.Models.Users;
using BookWebApp.Utilities.Mapper;

namespace BookWebApp.Services.Models.Users
{
    public class UserRegistrationModel : IMapFrom<User>, IMapTo<User>
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string RepeatPassword { get; set; }

        public string Email { get; set; }
    }
}
