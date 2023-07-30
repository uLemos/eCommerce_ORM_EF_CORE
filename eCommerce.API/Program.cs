global using eCommerce.Models;
using eCommerce.API.DataBase;
using eCommerce.API.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
//var configuration = builder.Configuration; -> Da pra fazer direto, reduzindo o código.

// Add services to the container.

builder.Services.AddControllers(). AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<eCommerceContext>( //Contexto do EF presente no database.
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("eCommerce"))
);

//builder.Services.AddScoped<IUsuarioRepository>(); //Minha injeção de dependência! SCOPED
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>(); //Minha injeção de dependência! SCOPED -> Com o desacoplamento, podendo direcionar a interface a extrair de uma classe em específico.

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
