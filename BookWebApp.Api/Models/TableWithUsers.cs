using BookWebApp.Repositories.Models.Users;
using BookWebApp.Services.Models.Users;
using System.Collections.Generic;

namespace BookWebApp.Api.Models
{
    public class TableWithUsers
    {
        public List<UserViewModel> Users { get; set; } = new List<UserViewModel>();
        

    }
}
