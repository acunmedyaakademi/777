using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using _777.Data.Entities;
using _777.Core;
using _777.Models;
using _777.Data;

namespace _777.Controllers
{
    public class AccountController : Controller
    {
        readonly UserManager<UserApp> _userManager;
        readonly ApplicationDbContext _context;
        readonly SignInManager<UserApp> _signInManager;

        public AccountController(SignInManager<UserApp> signInManager, UserManager<UserApp> userManager, ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login(Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal.LoginModel.InputModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                        return RedirectToAction("index", "home");
                }
                return Content("Email ya da Şifre yanlış");
            }
            return Content("Email ya da Şifre yanlış");
        }


        [HttpPost]
        public async Task<IActionResult> Register(MyRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserApp()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    string UserId = _userManager.GetUserId(User);

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = UserId, code = code },
                    protocol: Request.Scheme);

                    Helper.SendMail(user.Email, HtmlEncoder.Default.Encode(callbackUrl));

                    return RedirectToAction("index", "home");
                }
                return Content("burada 404 sayfasına yolla");
            }
            return Content("burada 404 sayfasına yolla");
        }

        //[HttpPost]
        //public async Task<IActionResult> ResetPassword(string Email)
        //{
        //    if (ModelState.IsValid)
        //    { //todo: smtp 
        //        var user = await _userManager.FindByEmailAsync(Email);
        //        if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
        //            return RedirectToPage("Identity/Account/ForgotPasswordConfirmation");

        //        var code = await _userManager.GeneratePasswordResetTokenAsync(user);
        //        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        //        var callbackUrl = Url.Page(
        //                "/Account/ConfirmEmail",
        //                pageHandler: null,
        //                values: new { area = "Identity", code },
        //                protocol: Request.Scheme);
        //        if (Helper.SendMail(Email, HtmlEncoder.Default.Encode(callbackUrl)))

        //            return RedirectToAction("Index", "home");
        //    }
        //    return Content("burada 404 sayfasına yolla");
        //}


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "home");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return Content("Olmadı");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return Content("Mailiniz Onaylanmıştır");

            return Content("Olmadı");
        }

  
        public IActionResult ChangeUsarname( string Usarname)
        {
            int ıd = Convert.ToInt16(_userManager.GetUserId(User));
           var user = _context.Users.Where(a => a.Id == ıd).FirstOrDefault();
           user.UserName = Usarname;   
            _context.SaveChanges();

            return Content("Burası hesaba gidecek");
        }


        public IActionResult AccountDelete (int id)
        {

            int ıd = Convert.ToInt16(_userManager.GetUserId(User));
            var user = _context.Users.Where(a => a.Id == ıd).FirstOrDefault();
            _context.Remove(user);
            _context.SaveChanges();
            return Content("Burası Hesabı silecek");
        }
        
    }

}

