using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ? Session servisi
builder.Services.AddDistributedMemoryCache(); // Gerekli!
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5); // Oturum s�resi
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// ? Cookie Authentication servisi
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";           // Giri� yap�lmam��sa y�nlendirme
        options.AccessDeniedPath = "/Account/AccessDenied"; // Yetki yoksa y�nlendirme
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);  // Cookie ge�erlilik s�resi
        options.SlidingExpiration = true;               // Kullan�c� aktifse s�re uzar
    });

// ? View'da Session kullan�m� i�in
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

// ?? Middlewares s�ralamas� �nemli:
app.UseSession();          // Session middleware
app.UseAuthentication();   // Giri� kontrol�
app.UseAuthorization();    // Yetki kontrol�

// ? Varsay�lan rota (Login ekran�)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
