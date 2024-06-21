using AdminApp.Models.ViewModels;
using AdminApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddMvc();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Establecer el tiempo de expiración de la sesión
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<TurnoViewModel1>();
builder.Services.AddTransient<Service1>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    x =>
    {
        x.AccessDeniedPath = "/Account/Denied";
        x.LoginPath = "/Account/Login";
        //x.LogoutPath = "/Account/Logout";
        x.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        x.Cookie.Name = "ghcookie";
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
#region Validators
//builder.Services.AddTransient<AdminApp.Models.Validators.LogInValidator>();
//builder.Services.AddTransient<AdminApp.Areas.Administrador.Models.Validators.CajaAdminValidator>();
//builder.Services.AddTransient<AdminApp.Areas.Administrador.Models.Validators.UsuariosAdminValidator>();
#endregion



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

//app.UseEndpoints(endpoints =>
//{
//	_ = endpoints.MapControllerRoute(
//		name: "default",
//		pattern: "{controller=Home}/{action=Index}/{id?}");
//});

//app.MapControllerRoute(
//    name: "areas",
//    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    //_ = endpoints.MapControllerRoute(
    //    name: "default",
    //    pattern: "{controller=Home}/{action=Index}/{id?}");    ///RUTA A LA PANTALLA QUE MUESTRA LA FILA.


    _ = endpoints.MapControllerRoute(
          name: "default",
          pattern: "{controller=Account}/{action=Login}/{id?}");

});

app.Run();
