using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ? Session servisi
builder.Services.AddDistributedMemoryCache(); // Gerekli!
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5); // Oturum süresi
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ? Cookie Authentication servisi
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";           // Giriþ yapýlmamýþsa yönlendirme
        options.AccessDeniedPath = "/Account/AccessDenied"; // Yetki yoksa yönlendirme
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);  // Cookie geçerlilik süresi
        options.SlidingExpiration = true;               // Kullanýcý aktifse süre uzar
    });

// ? View'da Session kullanýmý için
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ?? Middlewares sýralamasý önemli:
app.UseSession();          // Session middleware
app.UseAuthentication();   // Giriþ kontrolü
app.UseAuthorization();    // Yetki kontrolü

// ? Varsayýlan rota (Login ekraný)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
