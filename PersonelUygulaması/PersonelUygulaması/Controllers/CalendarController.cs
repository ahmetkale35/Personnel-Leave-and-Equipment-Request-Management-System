using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelUygulaması.Models;
using System.Security.Claims;

namespace PersonelUygulaması.Controllers
{
    public class CalendarController : Controller
    {
        Context c = new Context();

        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        
        [HttpGet]
        public IActionResult GetLeaveEvents()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var leaves = c.LeaveRequests
                .Include(x => x.LeaveType)
                .Where(x => x.UserId == userId)
                .Where(x=> x.Durum == "Onaylandı")
                .Select(x => new
                {
                    id = x.id,
                    title = x.LeaveType.Ad + " - " + x.Aciklama,
                    start = x.BaslangicTarihi.ToString("yyyy-MM-dd"),
                    end = x.BitisTarihi.AddDays(1).ToString("yyyy-MM-dd"),
                    color = "green"
                }).ToList();

            return Json(leaves);
        }
    }

}
