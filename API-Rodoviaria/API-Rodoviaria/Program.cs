using System.Text;
using API_Rodoviaria.Application.UseCase;
using API_Rodoviaria.Application.UseCase.CadeiraCasosDeUso.ListarCadeira;
using API_Rodoviaria.Application.UseCase.OnibusCasoDeUso.CriarOnibus;
using API_Rodoviaria.Application.UseCase.PerfilCasosDeUso.CriarPerfil;
using API_Rodoviaria.Application.UseCase.ReservaCasosDeUso.CriarReserva;
using API_Rodoviaria.Application.UseCase.UsuarioCasosDeUso.CriarUsuario;
using API_Rodoviaria.Application.UseCase.UsuarioCasosDeUso.Login;
using API_Rodoviaria.Application.UseCase.ViagemCasosDeUso.ListarViagens;
using API_Rodoviaria.Controllers.Posts;
using API_Rodoviaria.Domain.Interfaces.IMotorista;
using API_Rodoviaria.Domain.Interfaces.IOnibus;
using API_Rodoviaria.Domain.Interfaces.IPerfil;
using API_Rodoviaria.Domain.Interfaces.IReserva;
using API_Rodoviaria.Domain.Interfaces.IViagem;
using API_Rodoviaria.Infrastructure.DataAccess;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioCadeira;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioMotorista;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioOnibus;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioPerfil;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioReserva;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioUsuario;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioViagem;
using API_Rodoviaria.Infrastructure.Middleware;
using API_Rodoviaria.Infrastructure.Security;
using API_Rodoviaria.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<RodoviariaDbContext>(opcoes =>
opcoes.UseNpgsql(builder.Configuration.GetConnectionString("Padrao")));


var jwt = builder.Configuration.GetSection("Jwt").Get<JwtConfiguracoes>()
?? throw new InvalidOperationException("Configure a seção 'Jwt' no appsettings.json.");
builder.Services.AddSingleton(jwt);
builder.Services
.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(opcoes =>
{
    opcoes.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwt.Emissor,
        ValidAudience = jwt.Audiencia,
        IssuerSigningKey = new
    SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Chave)),
        ClockSkew = TimeSpan.Zero
    };
  
opcoes.Events = new JwtBearerEvents
{
    OnMessageReceived = contexto =>
    {
        if
        (contexto.Request.Cookies.TryGetValue(AuthController.NomeCookie, out var token))
            contexto.Token = token;
        return Task.CompletedTask;
    }
};
});
builder.Services.AddAuthorization();
// ---------- Injeção de dependência ----------
// "Quando alguém pedir a INTERFACE, entregue esta CLASSE."
// Scoped = uma instância nova por requisição HTTP.
builder.Services.AddScoped<IServicoHashSenha, ServicoHashSenha>();
builder.Services.AddScoped<IServicoToken, ServicoToken>();
builder.Services.AddScoped<IRepositorioPerfil, RepositorioPerfil>();
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<IRepositorioMotorista, RepositorioMotorista>();
builder.Services.AddScoped<IRepositorioOnibus, RepositorioOnibus>();
builder.Services.AddScoped<IRepositorioViagem, RepositorioViagem>();
builder.Services.AddScoped<IRepositorioCadeira, RepositorioCadeira>();
builder.Services.AddScoped<IRepositorioReserva, RepositorioReserva>();
builder.Services.AddScoped<ICriarPerfilCasoDeUso, CriarPerfilCasoDeUso>();
builder.Services.AddScoped<ICriarUsuarioCasoDeUso, CriarUsuarioCasoDeUso>();
builder.Services.AddScoped<ILoginCasoDeUso, LoginCasoDeUso>();
builder.Services.AddScoped<ICriarMotoristaCasoDeUso, CriarMotoristaCasoDeUso>();
builder.Services.AddScoped<ICriarOnibusCasoDeUso, CriarOnibusCasoDeUso>();
builder.Services.AddScoped<ICriarReservaCasoDeUso, CriarReservaCasoDeUso>();
builder.Services.AddScoped<IListarViagensCasoDeUso, ListarViagensCasoDeUso>();
builder.Services.AddScoped<IListarCadeirasDisponiveisCasoDeUso,ListarCadeirasDisponiveisCasoDeUso>();
var app = builder.Build();

using (var escopo = app.Services.CreateScope())
{
    var db = escopo.ServiceProvider.GetRequiredService<RodoviariaDbContext>();
    await db.Database.MigrateAsync();
    await SementeInicial.ExecutarAsync(escopo.ServiceProvider,
    app.Configuration);
}
app.UseMiddleware<TratamentoErrosMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();