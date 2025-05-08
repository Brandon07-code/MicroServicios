using MicroserviciosWeb.Data;
using MicroserviciosWeb.DTOs;
using MicroserviciosWeb.Helpers;
using MicroserviciosWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroserviciosWeb.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> Login(LoginDTO loginDto);
        Task<AuthResponseDTO> Register(RegisterDTO registerDto);
    }

    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtHelper _jwtHelper;

        public AuthService(ApplicationDbContext context, JwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

        public async Task<AuthResponseDTO> Login(LoginDTO loginDto)
        {
            try
            {
                // Buscar usuario por nombre de usuario
                var user = await _context.Users
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync(u => u.Username == loginDto.Username);

                if (user == null)
                {
                    return new AuthResponseDTO
                    {
                        Success = false,
                        Message = "Usuario no encontrado"
                    };
                }

                // Verificar contraseña (sin encriptar, proyecto de práctica)
                if (loginDto.Password != user.Password)
                {
                    return new AuthResponseDTO
                    {
                        Success = false,
                        Message = "Contraseña incorrecta"
                    };
                }

                // Verificar si la cuenta está activa
                if (!user.Activo)
                {
                    return new AuthResponseDTO
                    {
                        Success = false,
                        Message = "La cuenta de usuario está desactivada"
                    };
                }
                var loginHistory = new LoginHistory
                {
                    Username = user.Username,
                    UserId = user.Id,
                    FechaCreacion = DateTime.UtcNow
                };

                _context.LoginHistories.Add(loginHistory);
                await _context.SaveChangesAsync();

                // Generar token JWT
                var token = _jwtHelper.GenerateJwtToken(user, user.Role?.Name ?? "Cliente");

                return new AuthResponseDTO
                {
                    Success = true,
                    Token = token,
                    Message = "Inicio de sesión exitoso",
                    User = new UserDTO
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Email = user.Email,
                        NombreCompleto = user.NombreCompleto,
                        Role = user.Role?.Name ?? "Cliente"
                    }
                };
            }
            catch (Exception ex)
            {
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = $"Error al iniciar sesión: {ex.Message}"
                };
            }
        }

        public async Task<AuthResponseDTO> Register(RegisterDTO registerDto)
        {
            try
            {
                // Verificar si el usuario ya existe
                if (await _context.Users.AnyAsync(u => u.Username == registerDto.Username))
                {
                    return new AuthResponseDTO
                    {
                        Success = false,
                        Message = "El nombre de usuario ya está en uso"
                    };
                }

                if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
                {
                    return new AuthResponseDTO
                    {
                        Success = false,
                        Message = "El email ya está registrado"
                    };
                }

                // Determinar el RoleId basado en la solicitud
                int roleId = 2; // Por defecto es Cliente (ID 2)

                // Si se especificó un rol y es "Administrador", asignar el rol correcto
                if (!string.IsNullOrEmpty(registerDto.Role) &&
                    registerDto.Role.Equals("Administrador", StringComparison.OrdinalIgnoreCase))
                {
                    // Asumimos que el rol Administrador tiene ID 1
                    roleId = 1;
                }

                // Crear nuevo usuario con el rol apropiado
                var newUser = new ApplicationUser
                {
                    Username = registerDto.Username,
                    Email = registerDto.Email,
                    Password = registerDto.Password,
                    NombreCompleto = registerDto.NombreCompleto,
                    Telefono = registerDto.Telefono,
                    RoleId = roleId,
                    Activo = true,
                    FechaCreacion = DateTime.Now
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                // Recargar el usuario con su rol
                var userWithRole = await _context.Users
                    .Include(u => u.Role)
                    .FirstAsync(u => u.Id == newUser.Id);

                var token = _jwtHelper.GenerateJwtToken(userWithRole, userWithRole.Role?.Name ?? "Cliente");

                return new AuthResponseDTO
                {
                    Success = true,
                    Token = token,
                    Message = "Registro exitoso",
                    User = new UserDTO
                    {
                        Id = userWithRole.Id,
                        Username = userWithRole.Username,
                        Email = userWithRole.Email,
                        NombreCompleto = userWithRole.NombreCompleto,
                        Role = userWithRole.Role?.Name ?? "Cliente"
                    }
                };
            }
            catch (Exception ex)
            {
                return new AuthResponseDTO
                {
                    Success = false,
                    Message = $"Error al registrar usuario: {ex.Message}"
                };
            }
        }
    }
}