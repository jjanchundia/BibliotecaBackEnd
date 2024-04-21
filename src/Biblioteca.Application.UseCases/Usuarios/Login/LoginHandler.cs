using Biblioteca.Application.Dtos;
using Biblioteca.Domain;
using Biblioteca.Persistence;
using MediatR;

namespace Biblioteca.Application.UseCases.Usuarios.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, Result<UserDto>>
    {
        private readonly ApplicationDbContext _dbcontext;
        public LoginHandler(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Result<UserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = _dbcontext.Users.Where(x => x.Username == request.Username && x.Password == request.Password).FirstOrDefault();

            if (user == null)
            {
                return Result<UserDto>.Failure("No se encontró usuario!");
            }

            return Result<UserDto>.Success(new UserDto
            {
                Id = user.Id,
                Password = user.Password,
                Username = user.Username,
                Role = user.Role
            });
        }
    }
}