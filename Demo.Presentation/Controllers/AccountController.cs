using Demo.DataAccess.Models.IdentityModel;
using Demo.Presentation.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Demo.Presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager) : Controller
    {
        //register
        #region register
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]

        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(model: viewModel);
            var User = new ApplicationUser()
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
            UserName = viewModel.UserName,
                Email = viewModel.Email,
            };
            var Result = _userManager.CreateAsync( User,viewModel.Password).Result;
            if (Result.Succeeded)
                return RedirectToAction( "Login");
            else
            {
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError( string.Empty,  error.Description);
                    
                }
                return View(model: viewModel);
            }
        }
                #endregion
                //login
                //sign out

            }
}
