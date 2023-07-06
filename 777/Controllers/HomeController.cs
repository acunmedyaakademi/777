using _777.Core;
using _777.Data;
using _777.Data.Entities;
using _777.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace _777.Controllers
{
    public class HomeController : Controller
    {
        readonly ApplicationDbContext _context; 

        public HomeController(ApplicationDbContext context)
        { 
            _context = context;
        }

        public IActionResult Index()
        {
            List<InspireMessage> messages = _context.InspireMessages.Include(a => a.user).ToList();
            _context.SaveChanges();
            return View(messages);
        }
        public IActionResult Privacy()
        {
            Dictionary<string, double> dataDict = new Dictionary<string, double>();
            var textApps = _context.TextApps.Where(a => a.CreatedOn.Month == DateTime.Now.Month && a.UserId == 1).ToList();

            foreach (var item in textApps)
            {
                dataDict[item.Title] = item.SentimentScore;
            }

            return View(dataDict);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}