using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonelUygulaması.Models;
using System.Security.Claims;
using System.Security.Cryptography;

namespace PersonelUygulaması.Controllers
{
    public class AccountController : Controller
    {
        Context c = new Context();

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Users user)
        {
           SHA256 sha = new SHA256CryptoServiceProvider();
            var ParolaHash = Convert.ToBase64String(sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(user.ParolaHash)));

            var bilgiler = c.Users.FirstOrDefault(x => x.Email == user.Email && x.ParolaHash == ParolaHash);

            if (bilgiler != null)
            {
                var rol = c.Roles.Find(bilgiler.RolId);

                // SESSION verileri ekleniyor
                HttpContext.Session.SetString("AdSoyad", bilgiler.AdSoyad);
                HttpContext.Session.SetInt32("KullaniciId", bilgiler.Id);
                HttpContext.Session.SetString("Rol", rol != null ? rol.Ad : "");

                // CLAIMS ile kimlik doğrulama yapılır
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, bilgiler.Id.ToString()),
                    new Claim(ClaimTypes.Name, bilgiler.AdSoyad),
                    new Claim(ClaimTypes.Email, bilgiler.Email),
                    new Claim(ClaimTypes.Role, rol != null ? rol.Ad : "Unknown")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                // Kullanıcının rolüne göre yönlendirme
                if (rol != null && rol.Ad == "Admin")
                    return RedirectToAction("Admin", "MainPage");

                else if (rol != null && rol.Ad == "Employee")
                    return RedirectToAction("Employee", "MainPage");

                else if (rol != null && rol.Ad == "BT")
                    return RedirectToAction("BT", "MainPage");

                else
                    return RedirectToAction("Index", "Home");
            }

            // Giriş başarısızsa
            ViewBag.ErrorMessage = "Kullanıcı adı veya şifre hatalı.";
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            //SESSION temizleniyor
            HttpContext.Session.Clear();

            // Cookie oturumu sonlandırılıyor
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        // Test amaçlı session verilerini gösteren sayfa
        public IActionResult Sess()
        {
            return View();
        }


        public IActionResult CreateSampleUser()
        {
            if (!c.Users.Any(u => u.Email == "ahmet@personel.com"))
            {
                var user = new Users
                {
                    AdSoyad = "Ahmet Yılmaz",
                    Email = "ahmet@personel.com",
                    ParolaHash = PasswordHelper.HashPassword("123"),
                    RolId = 2
                };

                c.Users.Add(user);
                c.SaveChanges();
                var user1 = new Users
                {
                    AdSoyad = "Ayse Tek",
                    Email = "ayse@personel.com",
                    ParolaHash = PasswordHelper.HashPassword("1234"),
                    RolId = 2
                };

                c.Users.Add(user1);
                c.SaveChanges();
                var user2 = new Users
                {
                    AdSoyad = "Burak Aslan",
                    Email = "burak@bt.com",
                    ParolaHash = PasswordHelper.HashPassword("123"),
                    RolId = 3
                };

                c.Users.Add(user2);
                c.SaveChanges();
                var user3 = new Users
                {
                    AdSoyad = "Ali Yıldız",
                    Email = "ali@admin.com",
                    ParolaHash = PasswordHelper.HashPassword("321"),
                    RolId = 1
                };

                c.Users.Add(user3);
                c.SaveChanges();

                return Content("Admin kullanıcı başarıyla eklendi.");
            }

            return Content("Kullanıcı zaten var.");
        }
    }
}
