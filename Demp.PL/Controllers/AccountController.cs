using Demo.BLL.Common.Services.EmailSettings;
using Demo.DAL.Entities.Identity;
using Demp.PL.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demp.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSettings _emailSettings;

        public AccountController
            (
               UserManager<ApplicationUser> userManager,
               SignInManager<ApplicationUser> signInManager,
               IEmailSettings emailSettings
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSettings = emailSettings;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var User = await _userManager.FindByNameAsync(registerViewModel.UserName);

            if (User is { })
            {
                ModelState.AddModelError(nameof(registerViewModel.UserName), "This User Name is already exist fot another account");
                return View(registerViewModel);
            }

            User = new ApplicationUser()
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email,
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                IsAgree = registerViewModel.IsAgree,
            };

            var Result = await _userManager.CreateAsync(User , registerViewModel.Password);
            
            if (Result.Succeeded) 
                return RedirectToAction("LogIn");

            foreach (var error in Result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View(registerViewModel);

        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel logInViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var User = await _userManager.FindByEmailAsync(logInViewModel.Email);

            if (User is { })
            {
                var flag = await _userManager.CheckPasswordAsync(User, logInViewModel.Password);

                if (flag)
                {
                    var Result = await _signInManager.PasswordSignInAsync(User, logInViewModel.Password, logInViewModel.RememberMe, false);

                    if (Result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "Your Account Isn't Confirmed Yet");
                    if(Result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "Your Account Is Locked");
                    if(Result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index) , "Home");

                }
            }

            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            return View(logInViewModel);

        }

        [HttpGet]
        public async new Task<IActionResult> SignOut()
        {
           await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn));
           
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordViewModel forgetPasswordViewModel)
        {


            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(forgetPasswordViewModel.Email);

                if (User is not null )
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(User);

                    var resetPassword = Url.Action("ResetPassword", "Account", new { email = forgetPasswordViewModel.Email, token = token }, Request.Scheme);

                    var email = new Email()
                    {
                        To = forgetPasswordViewModel.Email,
                        Body = "Reset Your Password",
                        Subject = resetPassword ?? string.Empty,
                    };

                    _emailSettings.SendEmail(email);
                    return RedirectToAction("CheckYourInbox");
                }

                ModelState.AddModelError(string.Empty, "Invalid Email");
            }


            return View(forgetPasswordViewModel);
        }


        public IActionResult CheckYourInbox()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string email , string token)
        {
            TempData["email"]=email;
                TempData["token"]=token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;

                var User = await _userManager.FindByEmailAsync(email);

                if (User is not null)
                {
                    var Result = await _userManager.ResetPasswordAsync(User, token, resetPasswordViewModel.Password);

                    if (Result.Succeeded)
                        return RedirectToAction(nameof(LogIn));

                    foreach (var error in Result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);

                }
            }

            ModelState.AddModelError(string.Empty, "Invalid Operation Please Try Again");

            return View(resetPasswordViewModel);
        }
    }
}
