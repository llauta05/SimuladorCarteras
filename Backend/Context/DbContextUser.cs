using Backend.Configurations;
using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Context
{
    public class DbContextUser : DbContext
    {
        public DbContextUser(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cartera>().HasMany(c => c.Especies).WithOne(e => e.Cartera);
            modelBuilder.Entity<Especie>().HasOne(c => c.Cartera).WithMany(e => e.Especies).HasForeignKey(p => p.CarteraId);

            modelBuilder.Entity<User>().HasMany(c => c.Carteras).WithOne(e => e.Usuario);
            modelBuilder.Entity<Cartera>().HasOne(a => a.Usuario).WithMany(e => e.Carteras).HasForeignKey(p => p.UsuarioId);

            modelBuilder.Entity<Cartera>().HasMany(c => c.Historicos).WithOne(e => e.Cartera);
            modelBuilder.Entity<Historico>().HasOne(a => a.Cartera).WithMany(e => e.Historicos).HasForeignKey(p => p.CarteraId);

            modelBuilder.ApplyConfiguration(new EspecieConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CarteraConfiguration());
            modelBuilder.ApplyConfiguration(new HisotricoConfiguration()); 

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Especie> Especies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cartera> Carteras { get; set; }
        public DbSet<Historico> Historicos { get; set; }
    }
}
