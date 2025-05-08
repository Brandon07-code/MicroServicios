using Microsoft.EntityFrameworkCore;
using MicroserviciosWeb.Models;

namespace MicroserviciosWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar relaciones
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            // Datos iniciales para roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Administrador" },
                new Role { Id = 2, Name = "Cliente" }
            );

            // Crear un administrador por defecto (con contraseña "Admin123!")
            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@example.com",
                    Password = "Admin123!",
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