using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BookWebApp.Repositories.Models.Users;
using BookWebApp.Services.Contracts;
using BookWebApp.Services.Models.Base;
using BookWebApp.Services.Models.Users;
using System.Threading.Tasks;

namespace BookWebApp.Api.Controllers
{
    public class IdentityController : BaseController
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IUserService userService;

        public IdentityController(
            UserManager<User> userManager,
            SignInManager<User> signInManager, 
            IUserService userService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(UserLoginModel model, string returnUrl = null)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var user = await this.userManager.FindByNameAsync(model.Username);

            if (user == null)
            {
                return this.View();
            }

            var logInResult = await this.signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!logInResult.Succeeded)
            {
                return this.View(nameof(this.Login), model);
            }

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToAction("Index", "TableWithUsers");
        }

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            return this.RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Identity/SignUp")]
        public IActionResult CreateAccount()
        {
            return this.View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Identity/SignUp")]
        public async Task<IActionResult> CreateAccount(UserRegistrationModel model)
        {
            if (!this.ModelState.IsValid)
            {
                var invalidModelResult = new OperationResponse
                {
                    IsSuccessful = false,
                };

                if (string.IsNullOrWhiteSpace(model.Username))
                {
                    invalidModelResult.ErrorMessage = "Трябва да въведете потребителско име";
                }
                else if (string.IsNullOrWhiteSpace(model.Password) || string.IsNullOrWhiteSpace(model.RepeatPassword))
                {
                    invalidModelResult.ErrorMessage = "Трябва да въведете парола";
                }
                else if (string.IsNullOrWhiteSpace(model.Email))
                {
                    invalidModelResult.ErrorMessage = "Трябва да въведете имейл адрес";
                }

                this.HandleOperationMessage(invalidModelResult);
                return this.View(model);
            }

            var result = await this.userService.CreateAsync(model);

            if (!result.IsSuccessful)
            {
                this.HandleOperationMessage(result);
                return this.View(model);
            }

            this.HandleOperationMessage(result);
            return this.RedirectToAction(nameof(Login));
        }
    }
}
