using API_Rodoviaria.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Rodoviaria.Infrastructure.DataAccess;


public class RodoviariaDbContext :DbContext
{
    public RodoviariaDbContext(DbContextOptions<RodoviariaDbContext> options) : base(options)
    {
    }

    public DbSet<Perfil> Perfis => Set<Perfil>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Viagem> Viagens => Set<Viagem>();
    public DbSet<Motorista> Motoristas => Set<Motorista>();
    public DbSet<Rota> Rotas => Set<Rota>();
    public DbSet<Onibus> Onibus => Set<Onibus>();
    public DbSet<Reserva> Reservas => Set<Reserva>();
    public DbSet<Cadeira> Cadeiras => Set<Cadeira>();
    public DbSet<ReservaCadeira> ReservaCadeiras => Set<ReservaCadeira>();


    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Perfil>(e =>
        {
            e.ToTable("Perfil");
            e.HasKey(p => p.Id);
            e.Property(p => p.Cargo).IsRequired().HasMaxLength(50);
            e.HasIndex(p => p.Cargo).IsUnique();

        });
        mb.Entity<Usuario>(e =>
        { 
            e.ToTable("Usuario");
            e.HasKey(u => u.Id);
            e.Property(u => u.Username).IsRequired().HasMaxLength(50);
            e.Property(u => u.Password).IsRequired().HasMaxLength(50);
            e.Property(u => u.Email).IsRequired().HasMaxLength(100);
            e.Property(u => u.Cpf).IsRequired().HasMaxLength(11);
            e.Property(u => u.Endereco).IsRequired().HasMaxLength(200);
            e.HasIndex(u => u.Username).IsUnique();
            e.HasIndex(u => u.Email).IsUnique();
            e.HasIndex(u => u.Cpf).IsUnique();

            e.HasOne(u=> u.Perfil)
             .WithMany(p => p.Usuarios)
             .HasForeignKey(u => u.FkPerfil)
             .OnDelete(DeleteBehavior.Restrict);

        });

        mb.Entity<Motorista>(e => {            
            
            e.ToTable("Motorista");
            e.HasKey(m => m.Id);
            e.Property(m => m.Nome).IsRequired().HasMaxLength(100);
            e.Property(m => m.Cpf).IsRequired().HasMaxLength(11);
            e.Property(m => m.Cnh).IsRequired().HasMaxLength(20);
            e.HasIndex(m => m.Cpf).IsUnique();
            e.HasIndex(m => m.Cnh).IsUnique();
        });

        mb.Entity<Rota>(e => {            
            e.ToTable("Rota");
            e.HasKey(r => r.Id);
            e.Property(r => r.EnderecoInicio).IsRequired().HasMaxLength(200);
            e.Property(r => r.EnderecoFim).IsRequired().HasMaxLength(200);
            
        });

        mb.Entity<Onibus>(e => {            
            e.ToTable("Onibus");
            e.HasKey(o => o.Id);
            e.Property(o => o.Placa).IsRequired().HasMaxLength(10);
            e.Property(o => o.CapacidadeTotal).IsRequired();
            e.HasIndex(o => o.Placa).IsUnique();
        });

        mb.Entity<Cadeira>(e=> {            
            e.ToTable("Cadeira");
            e.HasKey(c => c.Id);
            e.HasOne(c => c.Onibus)
             .WithMany(o => o.Cadeiras)
             .HasForeignKey(c => c.FkOnibus)
             .OnDelete(DeleteBehavior.Restrict);
            e.HasIndex(c=> new { c.FkOnibus, c.Numero }).IsUnique();

        });

        mb.Entity<Viagem>(e => {            
            e.ToTable("Viagem");
            e.HasKey(v => v.Id);
            e.Property(v => v.DataChegada).IsRequired();
            e.Property(v => v.DataSaida).IsRequired();
       
            e.HasOne(v => v.Motorista)
             .WithMany(m => m.Viagens)
             .HasForeignKey(v => v.FkMotorista)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(v => v.Rota)
             .WithMany(r => r.Viagens)
             .HasForeignKey(v => v.FkRota)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(v => v.Onibus)
             .WithMany(o => o.Viagens)
             .HasForeignKey(v => v.FkOnibus)
             .OnDelete(DeleteBehavior.Restrict);
        });

        mb.Entity<Reserva>(e =>
        {
            e.ToTable("Reserva");
            e.HasKey(r => r.Id);

            e.HasOne(r => r.Viagem)
             .WithMany(v => v.Reservas)
             .HasForeignKey(r => r.FkViagem)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(r => r.Usuario)
             .WithMany(u => u.Reservas)
             .HasForeignKey(r => r.FkUsuario)
             .OnDelete(DeleteBehavior.Restrict);



        });   
        
        mb.Entity<ReservaCadeira>(e =>
        {
            e.ToTable("ReservaCadeira");
            e.HasKey(rc => rc.Id);
            e.HasOne(rc => rc.Reserva)
             .WithMany(r => r.ReservaCadeiras)
             .HasForeignKey(rc => rc.FkReserva)
             .OnDelete(DeleteBehavior.Cascade);    

            e.HasOne(rc => rc.Cadeira)
             .WithMany(c => c.ReservaCadeira)
             .HasForeignKey(rc => rc.FkCadeira)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasIndex(rc => new { rc.FkReserva, rc.FkCadeira }).IsUnique();
        });
    }

}

