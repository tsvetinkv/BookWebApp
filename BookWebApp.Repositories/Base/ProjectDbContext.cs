using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BookWebApp.Repositories.Models.Roles;
using BookWebApp.Repositories.Models.Users;

namespace BookWebApp.Repositories.Base
{
    public class ProjectDbContext : IdentityDbContext<User, Role, string>
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
            : base(options)
        {
        }
    }
}
