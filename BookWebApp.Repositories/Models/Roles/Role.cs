using Microsoft.AspNetCore.Identity;
using BookWebApp.Repositories.Base.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookWebApp.Repositories.Models.Roles
{
    [Table("AspNetRoles")]
    public class Role : IdentityRole, IDataModel<string>
    {
    }
}
