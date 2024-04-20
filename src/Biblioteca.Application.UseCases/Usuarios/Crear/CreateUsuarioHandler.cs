using Biblioteca.Domain;
using Biblioteca.Persistence;
using MediatR;

namespace Biblioteca.Application.UseCases.Usuarios.Crear
{
    public class CreateUsuarioHandler: IRequestHandler<CreateUsuarioCommand, bool>
    {
        private readonly ApplicationDbContext _dbcontext;
        public CreateUsuarioHandler(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<bool> Handle(CreateUsuarioCommand request, CancellationToken cancellationToken)
        {
            var nuevo = new User()
            {
                Username = request.Username,
                Password = request.Password,
                Role = request.Role
            };

            await _dbcontext.Users.AddAsync(nuevo);
            _dbcontext.SaveChanges();
            return true;
        }
    }
}