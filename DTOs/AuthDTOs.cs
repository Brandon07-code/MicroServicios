using System.ComponentModel.DataAnnotations;

namespace MicroserviciosWeb.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; } = string.Empty;
    }

    public class RegisterDTO
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(50, ErrorMessage = "La contraseña debe tener al menos {2} caracteres", MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre completo debe tener al menos {2} y máximo {1} caracteres", MinimumLength = 2)]
        public string NombreCompleto { get; set; } = string.Empty;

        [Phone(ErrorMessage = "El formato del teléfono no es válido")]
        public string? Telefono { get; set; }
        public string Role { get; set; } = "Cliente"; // Por defecto es Cliente
    }

    public class AuthResponseDTO
    {
        public bool Success { get; set; }
        public string? Token { get; set; }
        public string Message { get; set; } = string.Empty;
        public UserDTO? User { get; set; }
    }

    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}