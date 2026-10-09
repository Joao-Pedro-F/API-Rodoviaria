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
using API_Rodoviaria.Application.UseCase.RotaCasoDeUso;
using API_Rodoviaria.Domain.Interfaces.IRota;
using API_Rodoviaria.Application.UseCase.RotaCasoDeUso.CriarRota;
using API_Rodoviaria.Application.UseCase.RotaCasoDeUso.VerRota;
using API_Rodoviaria.Application.UseCase.RotaCasoDeUso.AtualizarRota;
using API_Rodoviaria.Application.UseCase.RotaCasoDeUso.DeletarRota;
using API_Rodoviaria.Domain.Interfaces.IViagem;
using API_Rodoviaria.Application.UseCase.ViagemCasosDeUso.CriarViagem;
using API_Rodoviaria.Application.UseCase.ViagemCasosDeUso.VerViagens;
using API_Rodoviaria.Application.UseCase.ViagemCasosDeUso.AtualizarViagem;
using API_Rodoviaria.Application.UseCase.ViagemCasosDeUso.DeletarViagem;
using API_Rodoviaria.Application.UseCase.ReservaCasosDeUso.CriarReserva;
using API_Rodoviaria.Domain.Interfaces.IReserva;
using API_Rodoviaria.Application.UseCase.ReservaCasosDeUso.VerReservas;
using API_Rodoviaria.Application.UseCase.ReservaCasosDeUso.AtualizarReserva;
using API_Rodoviaria.Application.UseCase.ReservaCasosDeUso.DeletarReserva;
using API_Rodoviaria.Application.UseCase.OnibusCasoDeUso.AtualizarOnibus;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioPerfil;
using API_Rodoviaria.Infrastructure.DataAccess.Repository.RepositorioCadeira;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// ---------- Banco ----------
builder.Services.AddDbContext<RodoviariaDbContext>(opcoes =>
opcoes.UseNpgsql(builder.Configuration.GetConnectionString("Padrao")));
// ---------- JWT lido do COOKIE ----------
var chaveJwt = builder.Configuration["Jwt:Chave"]
?? throw new InvalidOperationException("Configure 'Jwt:Chave' no appsettings.json.");
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
    ValidIssuer = builder.Configuration["Jwt:Emissor"],
    ValidAudience = builder.Configuration["Jwt:Audiencia"],
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
builder.Services.AddScoped<IRepositorioPerfil, RepositorioPerfil>();
builder.Services.AddScoped<IRepositorioCadeira, RepositorioCadeira>();
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
builder.Services.AddScoped<IAtualizarOnibusCasoDeUso,AtualizarOnibusCasoDeUso>();
builder.Services.AddScoped<IDeletarOnibusCasoDeUso, DeletarOnibusCasoDeUso>
();
// Rota
builder.Services.AddScoped<IRepositorioRota, RepositorioRota>();
builder.Services.AddScoped<ICriarRotaCasoDeUso, CriarRotaCasoDeUso>();
builder.Services.AddScoped<IPaginacaoRotaCasoDeUso,PaginacaoRotaCasoDeUso>();
builder.Services.AddScoped<IObterRotaPorId,ObterRotaPorId>();
builder.Services.AddScoped<IAtualizarRotaCasoDeUso, AtualizarRotaCasoDeUso>
();
builder.Services.AddScoped<IDeletarRotaCasoDeUso, DeletarRotaCasoDeUso>();
// Viagem
builder.Services.AddScoped<ICriarViagemCasoDeUso, CriarViagemCasoDeUso>();
builder.Services.AddScoped<IListarViagensCasoDeUso, ListarViagensCasoDeUso>
();
builder.Services.AddScoped<IListarViagensCasoDeUso, ListarViagensCasoDeUso>();
builder.Services.AddScoped<IAtualizarViagemCasoDeUso,AtualizarViagemCasoDeUso>();   
builder.Services.AddScoped<IDeletarViagemCasoDeUso, DeletarViagemCasoDeUso>
();
// Reserva
builder.Services.AddScoped<ICriarReservaCasoDeUso, CriarReservaCasoDeUso>
();
builder.Services.AddScoped < IListarReservaCasoDeUso,
ListarReservaCasoDeUso>();
builder.Services.AddScoped<IObterReservaPorId,
ObterReservaPorId>();
builder.Services.AddScoped<IAtualizarReservaCasoDeUso,
AtualizarReservaCasoDeUso>();
builder.Services.AddScoped<IDeletarReservaCasoDeUso,
DeletarReservaCasoDeUso>();
// lê seção "Jwt" e regista a instância
var jwtConfig = builder.Configuration.GetSection("Jwt").Get<JwtConfiguracoes>();
builder.Services.AddSingleton(jwtConfig);
// ou com IOptions
builder.Services.Configure<JwtConfiguracoes>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<JwtConfiguracoes>>().Value);

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
    NomesPerfis.Admin);
    if (!await db.Usuarios.AnyAsync(u => u.FkPerfil ==
    perfilAdmin.Id))
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
            FkPerfil = perfilAdmin.Id
        });
        await db.SaveChangesAsync();
    }
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