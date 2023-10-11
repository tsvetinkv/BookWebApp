using Microsoft.AspNetCore.Http;
using BookWebApp.Services.Base.Contracts;
using System.Security.Claims;

namespace BookWebApp.Services.Base
{
    public class UserData : IUserData
    {
        public UserData(IHttpContextAccessor httpContextAccessor)
        {
            this.UserId = httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.Name)?.Value;
        }

        public string UserId { get; set; }
    }
}
