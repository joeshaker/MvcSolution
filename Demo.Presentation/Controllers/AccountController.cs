using DataAccess.Data.IdentityModel;
using Demo.Presentation.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Demo.Presentation.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager) : Controller
    {
        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModels registerViewModelcs)
        {

            if (!ModelState.IsValid) return View(registerViewModelcs);
            var User = new ApplicationUser()
            {
                FirstName = registerViewModelcs.FirstName,
                LastName = registerViewModelcs.LastName,
                Email = registerViewModelcs.Email,
                UserName = registerViewModelcs.UserName,
            };

            var Result = _userManager.CreateAsync(User, registerViewModelcs.Password).Result;
            if (Result.Succeeded)
            {

                return RedirectToAction("Login");
            }
            else
            {
                foreach (var err in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
                return View(registerViewModelcs);
            }
        }
        #endregion
        #region Login
        [HttpGet]
        public IActionResult Login() {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(loginViewModel);
            }

            var result = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(loginViewModel);
            }

            // Here you would typically sign the user in using SignInManager
            // For now, we'll just redirect to a success page
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
