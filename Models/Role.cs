using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MicroserviciosWeb.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del rol es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre debe tener al menos {2} y máximo {1} caracteres", MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        // Colección de usuarios con este rol
        public virtual ICollection<ApplicationUser>? Users { get; set; }
    }
}