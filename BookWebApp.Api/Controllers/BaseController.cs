using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookWebApp.Services.Models.Base;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BookWebApp.Api.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        protected string UserId => this.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                                              .FirstOrDefault()?.Value;

        protected void HandleOperationMessage(OperationResponse result)
        {
            if (result == null)
            {
                return;
            }

            if (result.IsSuccessful)
            {
                var currentSuccessmessage = new StringBuilder(this.TempData["TempData-Success"]?.ToString());
                currentSuccessmessage.AppendLine(result.ErrorMessage);

                this.TempData["TempData-Error"] = currentSuccessmessage.ToString();
            }
            else
            {
                var currentErrorMessage = new StringBuilder(this.TempData["TempData-Success"]?.ToString());
                currentErrorMessage.AppendLine(result.ErrorMessage);

                this.TempData["TempData-Error"] = currentErrorMessage.ToString();
            }
        }
    }
}
