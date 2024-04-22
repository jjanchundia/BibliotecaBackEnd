using Biblioteca.Application.Dtos;
using Biblioteca.Domain;
using Biblioteca.Persistence;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Biblioteca.Application.UseCases.Usuarios.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, Result<UserDto>>
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly IConfiguration _config;
        public LoginHandler(ApplicationDbContext dbcontext, IConfiguration config)
        {
            _dbcontext = dbcontext;
            _config = config;
        }

        public async Task<Result<UserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = _dbcontext.Users.Where(x => x.Username == request.Username && x.Password == request.Password).FirstOrDefault();
            string token2 = "";
            if (user == null)
            {
                return Result<UserDto>.Failure("No se encontró usuario!");
            }

            var response = Result<UserDto>.Success(new UserDto
            {
                Id = user.Id,
                Password = user.Password,
                Username = user.Username,
                Role = user.Role
            });

            if (response.IsSuccess)
            {
                // Crear claims basados en el usuario autenticado
                var claims = new[]
                {
                    new Claim(response.Value.Username, response.Value.Password),
                    // Puedes agregar más claims según sea necesario (por ejemplo, roles)
                };

                // Configurar la clave secreta para firmar el token
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expiracion = DateTime.UtcNow.AddHours(24);
                // Configurar la información del token
                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: claims,
                    expires: expiracion,
                    signingCredentials: creds);

                // Devolver el token JWT como resultado de la autenticación exitosa
                //return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                token2 = new JwtSecurityTokenHandler().WriteToken(token).ToString();
            }

            // Devolver un error de no autorizado si las credenciales son incorrectas
            //return Unauthorized();

            return Result<UserDto>.Success(new UserDto
            {
                Id = user.Id,
                Password = user.Password,
                Username = user.Username,
                Role = user.Role, 
                Token = token2
            });
        }
    }
}