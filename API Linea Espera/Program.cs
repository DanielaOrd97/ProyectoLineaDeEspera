using API_Linea_Espera.Hubs;
using API_Linea_Espera.Models.Entities;
using API_Linea_Espera.Repositories;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using API_Linea_Espera.Helpers;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSignalR();

# region Validators
builder.Services.AddTransient<API_Linea_Espera.Models.Validators.CajaValidator>();
builder.Services.AddTransient<API_Linea_Espera.Models.Validators.RolValidator>();
builder.Services.AddTransient<API_Linea_Espera.Models.Validators.TurnoValidator>();
builder.Services.AddTransient<API_Linea_Espera.Models.Validators.UsuarioValidator>();
#endregion

var connectionString = builder.Configuration.GetConnectionString("BancoConnectionString");

builder.Services.AddDbContext<WebsitosEquipo2bancoContext>(x => x.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
//builder.Services.AddDbContext<SistemaDeEspera1Context>(x => x.UseMySql("server=localhost;user=root;password=root;database=SistemaDeEspera1", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql")));

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Administrador", policy => policy.RequireRole("Administrador"));
    options.AddPolicy("Operador", policy => policy.RequireRole("Operador"));
});

// Registrar TokenGeneratorJwt como singleton
builder.Services.AddSingleton<TokenGeneratorJwt>();

var app = builder.Build();

app.UseCors(x =>
{
    x.AllowAnyHeader();
    x.AllowAnyMethod();
    x.AllowAnyOrigin();
});

app.UseRouting();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<TurnosHub>("/turnos");

app.Run();
