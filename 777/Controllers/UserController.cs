using _777.Core;
using _777.Data;
using _777.Data.Entities;
using _777.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _777.Controllers
{
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
            List<Text> texts = _context.Texts.Include(a => a.User).Where(a => a.UserId == b).ToList();
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
            var TextDetails = _context.Texts.ToList();
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

        public IActionResult Write()
        {
            int b = Convert.ToInt16(_userManager.GetUserId(User));
            List<Text> texts = _context.Texts.Include(a => a.User).ToList();
            return View(texts);
            //ok
        }

        [HttpPost]
        public IActionResult Write(string Text)
        {
            Helper.SentimentAnalysis(Text);
            int userId = Convert.ToInt16(_userManager.GetUserId(User));
            Text text = new();
            text.Content = Text;
            text.UserId = userId;
            text.Title = Helper.DateToString(DateTime.Now);
            text.SentimentScore = Helper.SentimentAnalysis(Text);
            
            _context.Texts.Add(text);
            _context.SaveChanges();
            return View();
        }
        public IActionResult TextDetail(int textId)
        {
            return View(_context.Texts.FirstOrDefault(a => a.Id == textId));
        }

        [HttpPost]
        public IActionResult TextDetail(Text text)
        {
            _context.Texts.Update(text);
            _context.SaveChanges();
            return View();
        }
    }
}
