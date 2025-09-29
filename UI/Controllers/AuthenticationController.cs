using Microsoft.AspNetCore.Authentication;
//using Microsoft.Extensions.identity.core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UI.Models;

namespace UI.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly AuthenticationContext  _context;

        public AuthenticationController(AuthenticationContext authenticationContext)
        {
            _context = authenticationContext;
            
        }
        
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {

            if (ModelState.IsValid) 
            { 
                Registration account = new Registration();

                
                
                account.UserName = model.UserName;
                account.Email = model.Email;
                account.Mobile = model.Mobile;
                account.Password = model.Password;

                try 
                {

                    _context.Registrations.Add(account);

                    _context.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = $"{account.UserName} registered successfully. Please Login";
                }
                catch (Exception ex) 
                {
                    ModelState.AddModelError("", "Please enter valid caredentials");
                    return View(model);
                }

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = _context.Registrations.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();
                if (user != null)
                {
                    // sucess, cookie
                    var cliam = new List<Claim>
                     { 
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Name, user.UserName)
                     };

                    var claimsIdentity = new ClaimsIdentity(cliam, CookieAuthenticationDefaults.AuthenticationScheme);
                   // var claimsIdentity = new ClaimsIdentity(cliam, CookiesAuthenticationDefaults.Authentication);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    //HttpContext.SignInAsync(CookiesAuthenticationDefaults.AuthenticationSchenme, new ClaimsPrincipal(claimsIdentity);
                
                    return RedirectToAction("Index", controllerName: "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Credentials are not valid");
                }
            }
            return View(model) ;
        }

        public IActionResult Logout()
        { 
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home"); 
        }

        [HttpGet]
        [AllowAnonymous] 
        public IActionResult ForgotPassword()
        {
            return View();
        
        }

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> ForgotPassword (ForgotPasswordViewModel model)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        var user = await UserManager.FindByEmailAsync(model.Email);
        //        if (user != null && await UserManager.IsEmailConfirmedAsync(user)
        //        {
        //            var token = await UserManager.GeneratePasswordResetTokenAsync(user);

        //            var passwordResetLink = Url.Action("ResetPassword", "Account",
        //                       new { email == model.Email, token = token }, Request.Scheme);

        //            Logger.Log(LogLevel.Warning, passwordResetLink);

        //            return View("ForgotPasswordConfirmation");

        //        }
        //        return View("ForgotPasswordConfirmation");
        //    }
        //    return View(model);
        //}
    }
}
