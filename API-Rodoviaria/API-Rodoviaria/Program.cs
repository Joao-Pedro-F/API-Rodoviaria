using System.Text;
using API_Rodoviaria.Application.UseCase;
using API_Rodoviaria.Controllers;
using API_Rodoviaria.Domain.Constantes;
using API_Rodoviaria.Domain.Interfaces;
using API_Rodoviaria.Domain.Models;
using API_Rodoviaria.Infrastructure.DataAccess;
using API_Rodoviaria.Infrastructure.DataAccess.Repository;
using API_Rodoviaria.Infrastructure.Services;
using API_Rodoviaria.Infrastructure.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using API_Rodoviaria.Infrastructure.Security;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioUsuario;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioMotorista;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioOnibus;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioRota;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioViagem;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioReserva;
using API_Rodoviaria.Domain.Interfaces.IUsuario;
using API_Rodoviaria.Application.UseCase.UsuarioCasosDeUso.CriarUsuario;
using API_Rodoviaria.Application.UseCase.UsuarioCasosDeUso.Login;
using API_Rodoviaria.Application.UseCase.UsuarioCasosDeUso.VerUsuario;
using API_Rodoviaria.Application.UseCase.UsuarioCasosDeUso.AtualizarUsuario;
using API_Rodoviaria.Application.UseCase.UsuarioCasosDeUso.DeletarUsuario;
using API_Rodoviaria.Domain.Interfaces.IMotorista;
using API_Rodoviaria.Application.UseCase.MotoristaCasosDeUso.VerMotorista;
using API_Rodoviaria.Application.UseCase.MotoristaCasosDeUso.AtualizarMotorista;
using API_Rodoviaria.Application.UseCase.MotoristaCasosDeUso.DeletarMotorista;
using API_Rodoviaria.Domain.Interfaces.IOnibus;
using API_Rodoviaria.Application.UseCase.OnibusCasoDeUso.CriarOnibus;
using API_Rodoviaria.Application.UseCase.OnibusCasoDeUso.VerOnibus;
using API_Rodoviaria.Application.UseCase.OnibusCasoDeUso.DeletarOnibus;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// ---------- Banco ----------
builder.Services.AddDbContext<RodoviariaDbContext>(opcoes =>
opcoes.UseNpgsql(builder.Configuration.GetConnectionString("Padrao")));
// ---------- JWT lido do COOKIE ----------
var chaveJwt = builder.Configuration["Jwt:Key"]
?? throw new InvalidOperationException("Configure 'Jwt:Key' no appsettings.json.");
builder.Services
.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(opcoes =>
{
opcoes.TokenValidationParameters = new
TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["Jwt:Issuer"],
    ValidAudience = builder.Configuration["Jwt:Audience"],
    IssuerSigningKey = new
SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveJwt)),
    ClockSkew = TimeSpan.Zero
};
    // Por padrão o JwtBearer lê o header "Authorization: Bearer

// Aqui ensinamos ele a ler também do cookie.
opcoes.Events = new JwtBearerEvents
{
    OnMessageReceived = contexto =>
    {
        if
        (contexto.Request.Cookies.TryGetValue(UsuariosController.NomeCookie, out
        var token))
            contexto.Token = token;
        return Task.CompletedTask;
    }
};
});
builder.Services.AddAuthorization();
// ---------- Injeção de dependência ----------
builder.Services.AddScoped<IServicoHashSenha, ServicoHashSenha>();
builder.Services.AddScoped<IServicoToken, ServicoToken>();
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<IRepositorioMotorista, RepositorioMotorista>
();
builder.Services.AddScoped<IRepositorioOnibus, RepositorioOnibus>();
builder.Services.AddScoped<IRepositorioRota, RepositorioRota>();
builder.Services.AddScoped<IRepositorioViagem, RepositorioViagem>();
builder.Services.AddScoped<IRepositorioReserva, RepositorioReserva>();
// Usuário
builder.Services.AddScoped<ICriarUsuarioCasoDeUso, CriarUsuarioCasoDeUso>
();
builder.Services.AddScoped<ILoginCasoDeUso, LoginCasoDeUso>();
builder.Services.AddScoped<IPaginacaoUsuarioCasoDeUso,PaginacaoUsuarioCasoDeUso>();
builder.Services.AddScoped<IListarUsuarioCasoDeUso,ListarUsuarioCasoDeUso>();
builder.Services.AddScoped < IAtualizarUsuarioCasoDeUso,AtualizarUsuarioCasoDeUso>();
builder.Services.AddScoped<IDeletarUsuarioCasoDeUso,DeletarUsuarioCasoDeUso>();
// Motorista
builder.Services.AddScoped<ICriarMotoristaCasoDeUso,CriarMotoristaCasoDeUso>();
builder.Services.AddScoped<IVerMotoristaPaginadoCasoDeUso,VerMotoristaPaginadoCasoDeUso>();
builder.Services.AddScoped<IVerMotoristaPorIdCasoDeUso,VerMotoristaPorIdCasoDeUso>();
builder.Services.AddScoped<IAtualizarMotoristaCasoDeUso,AtualizarMotoristaCasoDeUso>();
builder.Services.AddScoped<IDeletarMotoristaCasoDeUso,DeletarMotoristaCasoDeUso>();
// Ônibus
builder.Services.AddScoped<ICriarOnibusCasoDeUso, CriarOnibusCasoDeUso>();
builder.Services.AddScoped<IListarOnibusCasoDeUso, ListarOnibusCasoDeUso>
();
builder.Services.AddScoped<IVerOnibusPorIdCasoDeUso,VerOnibusPorIdCasoDeUso>();
builder.Services.AddScoped<IAtualizarOnibusCasoDeUso,IAtualizarOnibusCasoDeUso>();
builder.Services.AddScoped<IDeletarOnibusCasoDeUso, DeletarOnibusCasoDeUso>
();
// Rota
builder.Services.AddScoped<ICriarRotaUseCase, CriarRotaUseCase>();
builder.Services.AddScoped<IListarRotasUseCase, ListarRotasUseCase>();
builder.Services.AddScoped<IObterRotaPorIdUseCase,
ObterRotaPorIdUseCase>();
builder.Services.AddScoped<IAtualizarRotaUseCase, AtualizarRotaUseCase>
();
builder.Services.AddScoped<IDeletarRotaUseCase, DeletarRotaUseCase>();
// Viagem
builder.Services.AddScoped<ICriarViagemUseCase, CriarViagemUseCase>();
builder.Services.AddScoped<IListarViagensUseCase, ListarViagensUseCase>
();
builder.Services.AddScoped<IObterViagemPorIdUseCase,
ObterViagemPorIdUseCase>();
builder.Services.AddScoped<IAtualizarViagemUseCase,
AtualizarViagemUseCase>();
builder.Services.AddScoped<IDeletarViagemUseCase, DeletarViagemUseCase>
();
// Reserva
builder.Services.AddScoped<ICriarReservaUseCase, CriarReservaUseCase>
();
builder.Services.AddScoped < IListarReservasUseCase,
ListarReservasUseCase>();
builder.Services.AddScoped<IObterReservaPorIdUseCase,
ObterReservaPorIdUseCase>();
builder.Services.AddScoped<IAtualizarReservaUseCase,
AtualizarReservaUseCase>();
builder.Services.AddScoped<IDeletarReservaUseCase,
DeletarReservaUseCase>();
var app = builder.Build();
// ---------- Migrations + dados iniciais ----------
using (var escopo = app.Services.CreateScope())
{
    var db =
    escopo.ServiceProvider.GetRequiredService<RodoviariaDbContext>();
    await db.Database.MigrateAsync();
    foreach (var nome in new[] { NomesPerfis.Admin, NomesPerfis.Cliente })
    {
        if (!await db.Perfis.AnyAsync(p => p.Cargo == nome))
            db.Perfis.Add(new Perfil { Cargo = nome });
    }
    await db.SaveChangesAsync();
    var perfilAdmin = await db.Perfis.FirstAsync(p => p.Cargo ==
    Cargos.Admin);
    if (!await db.Usuarios.AnyAsync(u => u.FkPerfil ==
    perfilAdmin.IdPerfil))
    {
        var senhas =
        escopo.ServiceProvider.GetRequiredService<IServicoHashSenha>();
        var admin = app.Configuration.GetSection("AdminInicial");
        db.Usuarios.Add(new Usuario
        {
            Username = admin["Username"] ?? "admin",
            Password = senhas.GerarhashSenha(admin["Password"] ?? "Admin@12345"),
            Email = admin["Email"] ?? "admin@rodoviaria.com",
            Cpf = admin["CPF"] ?? "00000000000",
            Endereco = "Sistema",
            FkPerfil = perfilAdmin.IdPerfil
        });
        await db.SaveChangesAsync();
    }
}
app.UseMiddleware<TTTTT>();
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