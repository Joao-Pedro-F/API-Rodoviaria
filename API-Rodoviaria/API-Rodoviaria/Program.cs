using API_Rodoviaria.Application.UseCase.PerfilCasosDeUso.CriarPerfil;
using API_Rodoviaria.Domain.Interfaces;
using API_Rodoviaria.Infrastructure.DataAccess;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioPerfil;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<RodoviariaDbContext>();
builder.Services.AddScoped<ICriarPerfilCasoDeUso, CriarPerfilCasoDeUso>();
builder.Services.AddScoped<IRepositorioPerfil, RepositorioPerfil>();

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
