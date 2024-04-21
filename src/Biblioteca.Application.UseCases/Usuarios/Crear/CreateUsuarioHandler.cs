using Biblioteca.Application.Dtos;
using Biblioteca.Domain;
using Biblioteca.Persistence;
using MediatR;

namespace Biblioteca.Application.UseCases.Usuarios.Crear
{
    public class CreateUsuarioHandler: IRequestHandler<CreateUsuarioCommand, Result<UserDto>>
    {
        private readonly ApplicationDbContext _dbcontext;
        public CreateUsuarioHandler(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<Result<UserDto>> Handle(CreateUsuarioCommand request, CancellationToken cancellationToken)
        {
            var nuevo = new User()
            {
                Username = request.Username,
                Password = request.Password,
                Role = request.Role
            };

            await _dbcontext.Users.AddAsync(nuevo);
            await _dbcontext.SaveChangesAsync();

            var ultimoIdInsertado = nuevo.Id;

            return Result<UserDto>.Success(new UserDto
            {
                Id = ultimoIdInsertado,
                Username = request.Username,
                Password = request.Password,
                Role = request.Role
            });
        }
    }
}