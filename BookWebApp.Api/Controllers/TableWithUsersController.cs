using BookWebApp.Api.Models;
using BookWebApp.Services.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace BookWebApp.Api.Controllers
{
    public class TableWithUsersController : BaseController
    {

        public IActionResult Index()
        {
         TableWithUsers model = new TableWithUsers();
            UserViewModel user1 = new UserViewModel();
            user1.Email = "email1@gmail.com";
            user1.Username = "username1";
            model.Users.Add(user1);
            UserViewModel user2 = new UserViewModel();
            user2.Email = "email2@abv.bg";
            user2.Username = "username2";
            model.Users.Add(user2); 
            UserViewModel user3 = new UserViewModel();
            user3.Email = "email3@abv.bg";
            user3.Username = "username3";
            model.Users.Add(user3);
            UserViewModel user4 = new UserViewModel();
            user4.Email = "email4@gmail.com";
            user4.Username = "username4";
            model.Users.Add(user4);
            return View(model);
        }


    }
}
