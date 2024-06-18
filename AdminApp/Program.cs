using AdminApp.Models.ViewModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddMvc();
builder.Services.AddScoped<TurnoViewModel1>();
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
