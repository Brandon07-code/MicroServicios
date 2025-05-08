using Microsoft.EntityFrameworkCore;
using MicroserviciosWeb.Models;

namespace MicroserviciosWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<LoginHistory> LoginHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación Usuario-Rol
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId);

            // Relación LoginHistory-Usuario
            modelBuilder.Entity<LoginHistory>()
                .HasOne(lh => lh.User)
                .WithMany()
                .HasForeignKey(lh => lh.UserId)
                .IsRequired(false);

            // Datos iniciales de roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Administrador" },
                new Role { Id = 2, Name = "Cliente" }
            );

            // Usuario administrador inicial
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@example.com",
                    Password = "Admin123!", // ¡Solo para pruebas!
                    RoleId = 1,
                    NombreCompleto = "Administrador del Sistema",
                    Telefono = "1234567890",
                    Activo = true,
                    FechaCreacion = DateTime.Now
                }
            );
        }
    }
}