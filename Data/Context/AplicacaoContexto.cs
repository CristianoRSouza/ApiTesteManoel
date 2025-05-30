using LojaManoelApi.Data.Entities;
using LojaManoelApi.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LojaManoelApi.Data.Context
{
    public class AplicacaoContexto : DbContext
    {
        public AplicacaoContexto(DbContextOptions<AplicacaoContexto> options) : base(options)
        {
        }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Pedido> Produtos { get; set; }
        public DbSet<Dimensao> Dimensoes { get; set; }
        public DbSet<Caixa> Caixas { get; set; }
        public DbSet<Papel> Papeis { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AplicacaoContexto).Assembly);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Papel>().HasData(
             new Papel { Id = 1, PapelToken = RoleType.Admin.ToString() },
             new Papel { Id = 2, PapelToken = RoleType.Manager.ToString() },
             new Papel { Id = 3, PapelToken = RoleType.Client.ToString() }
            );

        }

        
    }
}