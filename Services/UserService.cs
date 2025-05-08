using MicroserviciosWeb.Data;
using MicroserviciosWeb.DTOs;
using MicroserviciosWeb.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroserviciosWeb.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserListDTO>> GetAllUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Role) // Incluir datos del rol
                .Select(u => new UserListDTO
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    NombreCompleto = u.NombreCompleto,
                    Telefono = u.Telefono,
                    Role = u.Role.Name,
                    FechaCreacion = u.FechaCreacion,
                    Activo = u.Activo
                })
                .ToListAsync();
        }
    }
}