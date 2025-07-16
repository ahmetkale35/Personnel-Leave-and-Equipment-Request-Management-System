using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonelUygulaması.Models;
using System.Security.Claims;

namespace PersonelUygulaması.Controllers
{
    [Authorize]
    public class EquipmentController : Controller
    {
        Context c = new Context();

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.EquipmentList = new SelectList(c.EquipmentItems.ToList(), "Id", "Ad");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EquipmentRequests equipmentRequests)
        {
            equipmentRequests.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            equipmentRequests.Durum = "Bekliyor";
            equipmentRequests.TalepTarihi = DateTime.Now;
            c.EquipmentRequests.Add(equipmentRequests);
            c.SaveChanges();
            return RedirectToAction("MyRequests");
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult MyRequests()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var equipmentRequests = c.EquipmentRequests
                .Where(x => x.UserId == userId)
                .Include(x => x.EquipmentItem)
                .Include(x => x.Onaylayan)
                .ToList();

            return View(equipmentRequests);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancel(int id)
        {
            var talep = c.EquipmentRequests.Find(id);
            if (talep != null && talep.Durum == "Bekliyor")
            {
                talep.Durum = "İptal Edildi";
                c.SaveChanges();
            }
            return RedirectToAction("MyRequests");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAll()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var tumTalepler = c.EquipmentRequests.Where(x => x.UserId == userId);
            c.EquipmentRequests.RemoveRange(tumTalepler);
            c.SaveChanges();
            return RedirectToAction("MyRequests");
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Authorize(Roles = "Admin , BT")]
        public IActionResult Pending()
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var currentUser = c.Users.FirstOrDefault(u => u.Id == currentUserId);

            if (currentUser.RolId == 3) // BT
            {
                var requests = c.EquipmentRequests
                    .Where(x => x.UserId != currentUserId)
                    .Include(x => x.EquipmentItem)
                    .Include(x => x.Onaylayan)
                    .Include(x => x.User)
                    .ToList();
                return View(requests);
            }
            else
            {
                var requests = c.EquipmentRequests
                    .Include(x => x.EquipmentItem)
                    .Include(x => x.Onaylayan)
                    .Include(x => x.User)
                    .ToList();
                return View(requests);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin , BT")]
        public IActionResult Approve(int id)
        {
            var request = c.EquipmentRequests.FirstOrDefault(x => x.Id == id);
            if (request != null && request.Durum == "Bekliyor")
            {
                request.Durum = "Onaylandı";
                request.OnaylayanId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                c.SaveChanges();
            }
            return RedirectToAction("Pending");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin , BT")]
        public IActionResult Reject(int id)
        {
            var request = c.EquipmentRequests.FirstOrDefault(x => x.Id == id);
            if (request != null && request.Durum == "Bekliyor")
            {
                request.Durum = "Reddedildi";
                request.OnaylayanId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                c.SaveChanges();
            }
            return RedirectToAction("Pending");
        }
    }
}
