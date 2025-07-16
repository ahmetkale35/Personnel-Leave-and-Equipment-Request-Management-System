using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonelUygulaması.Models;
using System.Security.Claims;

namespace PersonelUygulaması.Controllers
{
    [Authorize]
    public class MainPageController : Controller
    {
        private readonly Context _context;

        public MainPageController()
        {
            _context = new Context();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }

        [Authorize(Roles = "Employee")]
        public IActionResult Employee()
        {
            int userId = GetUserIdFromClaims();
            if (userId == -1)
                return RedirectToAction("Login", "Account");

            var model = PrepareDashboardData(userId);
            SetViewBags(model);

            return View(model.Talepler);
        }

        [Authorize(Roles = "BT")]
        public IActionResult BT()
        {
            int userId = GetUserIdFromClaims();
            if (userId == -1)
                return RedirectToAction("Login", "Account");

            var model = PrepareDashboardData(userId);
            SetViewBags(model);

            return View(model.Talepler);
        }

        // Kullanıcının ID'sini Claims içinden alır
        private int GetUserIdFromClaims()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    return int.Parse(userIdClaim.Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kullanıcı ID alınırken hata: {ex.Message}");
            }
            return -1; // Geçersiz kullanıcı
        }

        // Ortak veri hesaplamalarını yapar
        private (int BekleyenIzin, int OnaylananIzin, int BekleyenEkipman, int OnaylananEkipman, int KalanIzinGunu, List<LeaveRequests> Talepler) PrepareDashboardData(int userId)
        {
            var talepler = _context.LeaveRequests
                .Where(x => x.UserId == userId)
                .ToList();

            int bekleyenIzin = _context.LeaveRequests.Count(x => x.UserId == userId && x.Durum == "Bekliyor");
            int onaylananIzin = _context.LeaveRequests.Count(x => x.UserId == userId && x.Durum == "Onaylandı");

            int bekleyenEkipman = _context.EquipmentRequests.Count(x => x.UserId == userId && x.Durum == "Bekliyor");
            int onaylananEkipman = _context.EquipmentRequests.Count(x => x.UserId == userId && x.Durum == "Onaylandı");

            var onaylananIzinler = _context.LeaveRequests
                .Where(x => x.UserId == userId && x.Durum == "Onaylandı")
                .ToList();

            int toplamGun = onaylananIzinler.Sum(x => (x.BitisTarihi - x.BaslangicTarihi).Days + 1);
            int kullanilanGun = 0;
            DateTime bugun = DateTime.Now.Date;

            foreach (var izin in onaylananIzinler)
            {
                if (izin.BitisTarihi.Date < bugun)
                {
                    kullanilanGun += (izin.BitisTarihi - izin.BaslangicTarihi).Days + 1;
                }
                else if (izin.BaslangicTarihi.Date <= bugun && izin.BitisTarihi.Date >= bugun)
                {
                    kullanilanGun += (bugun - izin.BaslangicTarihi.Date).Days + 1;
                }
            }

            int kalanGun = toplamGun - kullanilanGun;

            return (bekleyenIzin, onaylananIzin, bekleyenEkipman, onaylananEkipman, kalanGun, talepler);
        }

        // ViewBag'leri merkezi olarak ayarlar
        private void SetViewBags((int BekleyenIzin, int OnaylananIzin, int BekleyenEkipman, int OnaylananEkipman, int KalanIzinGunu, List<LeaveRequests> Talepler) model)
        {
            ViewBag.BekleyenSayisi = model.BekleyenIzin + model.BekleyenEkipman;
            ViewBag.OnaylananSayisi = model.OnaylananIzin + model.OnaylananEkipman;
            ViewBag.KalanIzinGunSayisi = model.KalanIzinGunu;
        }
    }
}
