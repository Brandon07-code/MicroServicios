using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MicroserviciosWeb.DTOs;
using MicroserviciosWeb.Services;

namespace MicroserviciosWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.Login(loginDto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.Register(registerDto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("test")]
        [Authorize]
        public IActionResult TestAuth()
        {
            return Ok(new { Message = "Autenticación exitosa" });
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Administrador")]
        public IActionResult AdminOnly()
        {
            return Ok(new { Message = "Tienes acceso como administrador" });
        }
        [HttpGet("cliente")]
        [Authorize(Roles = "Cliente")]
        public IActionResult ClienteOnly()
        {
            return Ok(new { Message = "Tienes acceso como cliente" });
        }
    }
}