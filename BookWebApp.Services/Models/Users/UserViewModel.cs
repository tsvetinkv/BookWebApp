using BookWebApp.Repositories.Models.Users;
using BookWebApp.Utilities.Mapper;
using System;
using System.Collections.Generic;

namespace BookWebApp.Services.Models.Users
{
    public class UserViewModel : IMapFrom<User>, IMapTo<User>
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? Birthday { get; set; }

    }
}
