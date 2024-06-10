using API_Linea_Espera.Hubs;
using API_Linea_Espera.Models.Entities;
using API_Linea_Espera.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddSignalR();



var connectionString = builder.Configuration.GetConnectionString("BancoConnectionString");

builder.Services.AddDbContext<SistemaDeEspera1Context>(x => x.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
//builder.Services.AddDbContext<SistemaDeEspera1Context>(x => x.UseMySql("server=localhost;user=root;password=root;database=SistemaDeEspera1", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql")));

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

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

app.UseAuthorization();

app.MapControllers();
app.MapHub<TurnosHub>("/turnos");

app.Run();
