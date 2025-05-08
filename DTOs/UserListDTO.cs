namespace MicroserviciosWeb.DTOs
{
    public class UserListDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string NombreCompleto { get; set; }
        public string Telefono { get; set; }
        public string Role { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
    }
}