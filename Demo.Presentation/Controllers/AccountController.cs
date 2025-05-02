using Demo.DataAccess.Models.IdentityModel;
using Demo.Presentation.Utilities;
using Demo.Presentation.ViewModels;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Email = Demo.Presentation.Utilities.Email;

namespace Demo.Presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager) : Controller
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
            var Result = _userManager.CreateAsync(User, viewModel.Password).Result;
            if (Result.Succeeded)
                return RedirectToAction("Login");
            else
            {
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);

                }
                return View(model: viewModel);
            }
        }
        #endregion
        //login
        #region login
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel  viewModel)
        { 
            if (!ModelState.IsValid) return View(viewModel);
            var User = _userManager.FindByEmailAsync(viewModel.Email).Result;
            if (User is not null)
            {

                bool Flag = _userManager.CheckPasswordAsync(User, viewModel.Password).Result;
                if (Flag)
                {
                    var Result = _signInManager.PasswordSignInAsync(User, viewModel.Password, viewModel.RememberMe, false).Result;
                    if (Result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, errorMessage: "Your Account Is not allowed");
                    if (Result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, errorMessage: "Your Account Is Locked out");
                    if (Result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            else
            {

                ModelState.AddModelError(string.Empty, "Invalid Login");
            }
                return View(viewModel);

            }
        #endregion
        //sign out
        #region sign out
        [HttpGet]

        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        //[HttpGet]

        //public IActionResult SignOut()
        //{
        //    signInManager.SignOutAsync().GetAwaiter().GetResult();
        //    return RedirectToAction(actionName: nameof(Login));
        //}

        #endregion
        #region ForgetPassword
        [HttpGet]
        public IActionResult ForgetPassword() => View();

        [HttpPost]
        public IActionResult SendResetPasswordLink(ForgetPasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var User =_userManager. FindByEmailAsync( viewModel.Email).Result;
                if (User is not null)
                {
                    var email = new Email()
                    {
                        To = viewModel.Email,
                        Subject = "Reset Password",
                        Body = "Reset Password Link" // TODO 
                    };
                  
                    // Send Email  

                }
            }
            ModelState.AddModelError(key: string.Empty,  "Invalid Operation");
            return View(viewName: nameof(ForgetPassword),  viewModel);
        }
           
        #endregion

    }

}

