using System;
using System.ComponentModel.DataAnnotations;

namespace MicroserviciosWeb.Models
{
    public class ApplicationUser
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; } = string.Empty;

        // Relación con roles
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }

        // Información adicional del usuario
        [StringLength(100, ErrorMessage = "El nombre completo debe tener al menos {2} y máximo {1} caracteres", MinimumLength = 2)]
        public string NombreCompleto { get; set; } = string.Empty;

        [Phone(ErrorMessage = "El formato del teléfono no es válido")]
        public string? Telefono { get; set; }

        public bool Activo { get; set; } = true;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}