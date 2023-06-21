using _777.Core;
using _777.Data;
using _777.Data.Entities;
using _777.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _777.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        readonly UserManager<UserApp> _userManager;
        readonly ApplicationDbContext _context;
        readonly SignInManager<UserApp> _signInManager;

        public UserController(UserManager<UserApp> userManager, ApplicationDbContext context, SignInManager<UserApp> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            UserIndexVM VM = new UserIndexVM();
            int b = Convert.ToInt16(_userManager.GetUserId(User));
            List<TextApp> texts = _context.TextApps.Include(a => a.User).Where(a => a.UserId == b).ToList();
            List<InspireMessage> ım = _context.InspireMessages.Include(a => a.user).ToList();
            VM.ınspireMessages = ım;
            VM.Texts = texts;
            return View(VM);
            //ok
        }

        public async Task<IActionResult> Profile()
        {
            ProfileVM VM = new ProfileVM();
            var user = await _userManager.GetUserAsync(User);
            var TextDetails = _context.TextApps.ToList();
            List<TextDetail> detaylar = new();

            foreach (var item in TextDetails)
            {
                TextDetail detay = new();
                detay.Title = item.Title;
                detay.Id = item.Id;
                detaylar.Add(detay);
            }
            VM.Details = detaylar;
            VM.User = user;
            return View(VM);
            //ok
        }

        [HttpPost]
        public IActionResult AddMessage(string message)
        {
            int b = Convert.ToInt16(_userManager.GetUserId(User));
            InspireMessage a = new();
            a.Message = message;
            a.UserId = b;

            _context.InspireMessages.Add(a);
            _context.SaveChanges();
            //bywada oldu mesajı yola
            return RedirectToAction("Profile", "user");
        }

        public IActionResult Write()
        {
            var date = Helper.DateToString(DateTime.Now);
            int b = Convert.ToInt16(_userManager.GetUserId(User));
            TextApp text = _context.TextApps.Where(a => a.Id == Convert.ToInt16(_userManager.GetUserId(User)) & a.Title == date).FirstOrDefault();
            if (text != null)
                return View(text);

            return View();
        }

        [HttpPost]
        public IActionResult Write(string Text)
        {
            Helper.SentimentAnalysis(Text);
            int userId = Convert.ToInt16(_userManager.GetUserId(User));
            TextApp text = new();
            text.Content = Text;
            text.UserId = userId;
            text.Title = Helper.DateToString(DateTime.Now);
            text.SentimentScore = Helper.SentimentAnalysis(Text);

            _context.TextApps.Add(text);
            _context.SaveChanges();
            return View();
        }
        public IActionResult TextDetail(int textId)
        {
            var a = _context.TextApps.First(a => a.Id == textId);

            return View(a);
        }


        public IActionResult TextDetails(string day, string month, string year)
        {
            string title = day + " " + month;
            var a = _context.TextApps.Where(a => a.Title == title & a.CreatedOn.Year == Convert.ToInt16(year)).FirstOrDefault();
            return View(a);
        }

        [HttpPost]
        public IActionResult TextDetail(TextApp text)
        {
            _context.TextApps.Update(text);
            _context.SaveChanges();
            return View();
        }

        [HttpPost]
        public IActionResult DeleteText (int TextId)
        {
            var texts = _context.TextApps.Where(a => a.Id == TextId).FirstOrDefault();
            _context.TextApps.Remove(texts);
            
            return RedirectToAction("Profile");
        }

        public IActionResult ChangePassword()
        {
            ViewBag.Message = TempData["Message"] as string;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {


                TempData["Message"] = "Şifreniz Güncellenememiştir.Tekrar Deneyiniz.";

                return RedirectToAction("ChangePassword", "user");
            }

            if (model.NewPassword != model.ConfirmPassword)
            {
                TempData["Message"] = "Yeni şifre ve şifre tekrarı birbirleriyle eşleşmiyor. Tekrar deneyiniz.";
                return RedirectToAction("ChangePassword", "User");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {


                TempData["Message"] = "Şifreniz Güncellenememiştir.Tekrar Deneyiniz.";

                return RedirectToAction("ChangePassword", "user");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)

            {


                TempData["Message"] = "Şifreniz Güncellenememiştir.Tekrar Deneyiniz.";

                return RedirectToAction("ChangePassword", "user");
            }

            TempData["Message"] = "Şifreniz başarıyla güncellenmiştir.";


            await _signInManager.RefreshSignInAsync(user);
            return RedirectToAction("ChangePassword", "user");


        }


        private bool ContainsLowerCaseLetter(string password)
        {
            return password.Any(c => char.IsLower(c));
        }

        private bool ContainsUpperCaseLetter(string password)
        {
            return password.Any(c => char.IsUpper(c));
        }


    }
}
