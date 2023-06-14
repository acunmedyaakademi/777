<<<<<<< HEAD
﻿namespace _777.Controllers
{
    public class AccountController
    {
    }
}
=======
﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using _777.Data.Entities;

namespace _777.Controllers
{
    public class AccountController : Controller
    {
        readonly UserManager<UserApp> _userManager;
        readonly SignInManager<UserApp> _signInManager;

        public AccountController(SignInManager<UserApp> signInManager, UserManager<UserApp> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
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
                return Content("burada 404 sayfasına yolla");
            }
            return Content("burada 404 sayfasına yolla");
        }


        [HttpPost]
        public async Task<IActionResult> Register(Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal.RegisterModel.InputModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserApp()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = false

                };

                var result = await _userManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {
                    //await _userManager.AddToRoleAsync(user, "Unknown");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    string UserId = _userManager.GetUserId(User);

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = UserId, code = code },
                            protocol: Request.Scheme);

                    //MailKitService.SendMailPassword(user.Email, HtmlEncoder.Default.Encode(callbackUrl));

                    return RedirectToAction("index", "home");
                }
                return Content("burada 404 sayfasına yolla");


            }
            return Content("burada 404 sayfasına yolla");

        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(string Email)
        {
            if (ModelState.IsValid)
            { //todo: smtp 
                var user = await _userManager.FindByEmailAsync(Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    return RedirectToPage("Identity/Account/ForgotPasswordConfirmation");
                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                        "/Account/ResetPassword",
                        pageHandler: null,
                        values: new { area = "Identity", code },
                        protocol: Request.Scheme);
                //MailKitService.SendMailPassword(Email, HtmlEncoder.Default.Encode(callbackUrl));

                return RedirectToAction("Index", "home");
            }
            return Content("burada 404 sayfasına yolla");
        }


        [HttpPost]
        public IActionResult NewPassword(string email)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "home");
        }

    }
}

>>>>>>> master
