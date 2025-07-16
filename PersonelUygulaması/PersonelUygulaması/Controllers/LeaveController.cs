using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PersonelUygulaması.Models;
using System.Security.Claims;


[Authorize]
public class LeaveController : Controller
{
    Context c = new Context();

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.LeaveTypes = c.LeaveTypes.ToList();
        
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(LeaveRequests model)
    {
        model.UserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        model.Durum = "Bekliyor";
        c.LeaveRequests.Add(model);
        c.SaveChanges();
        return RedirectToAction("MyRequests");
    }

    public IActionResult Cancel(int id)
    {
        var izin = c.LeaveRequests.FirstOrDefault(x => x.id == id);
        if (izin == null || izin.Durum != "Bekliyor")
            return NotFound();

        izin.Durum = "İptal Edildi";
        c.SaveChanges();

        return RedirectToAction("MyRequests");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteAll()
    {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        var userRequests = c.LeaveRequests.Where(x => x.UserId == userId).ToList();

        if (userRequests.Any())
        {
            c.LeaveRequests.RemoveRange(userRequests);
            c.SaveChanges();
        }

        return RedirectToAction("MyRequests");
    }




    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [HttpGet]
    public IActionResult MyRequests()
{
    int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

    var talepler = c.LeaveRequests
        .Where(x => x.UserId == userId)
        .ToList();

    ViewBag.LeaveTypes = c.LeaveTypes.ToList();


    return View(talepler);
}

    [Authorize(Roles = "Admin")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

    public IActionResult Pending()
    {
        var pendingRequests = c.LeaveRequests
        
        .Include(x => x.LeaveType)
        .Include(x => x.User)
        .ToList();
        return View(pendingRequests);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult Approve(int id)
    {
        var request = c.LeaveRequests.FirstOrDefault(x => x.id == id);
        if (request == null || request.Durum != "Bekliyor")
            return NotFound();
        request.Durum = "Onaylandı";
        c.SaveChanges();
        return RedirectToAction("Pending");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult Reject(int id)
    {
        var request = c.LeaveRequests.FirstOrDefault(x => x.id == id);
        if (request == null || request.Durum != "Bekliyor")
            return NotFound();
        request.Durum = "Reddedildi";
        c.SaveChanges();
        return RedirectToAction("Pending");
    }

}
