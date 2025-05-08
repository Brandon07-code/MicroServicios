namespace MicroserviciosWeb.Models
{
    public class LoginHistory
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}