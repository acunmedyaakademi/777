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
        public UserController(UserManager<UserApp> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
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
            int b = Convert.ToInt16(_userManager.GetUserId(User));
            List<TextApp> texts = _context.TextApps.Include(a => a.User).ToList();
            return View(texts);
            //ok
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

        [HttpPost]
        public IActionResult TextDetail(TextApp text)
        {
            _context.TextApps.Update(text);
            _context.SaveChanges();
            return View();
        }
    }
}
