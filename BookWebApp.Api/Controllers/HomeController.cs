using Microsoft.AspNetCore.Mvc;

namespace BookWebApp.Api.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}