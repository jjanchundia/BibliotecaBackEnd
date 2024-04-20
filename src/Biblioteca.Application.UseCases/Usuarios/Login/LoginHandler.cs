using Biblioteca.Application.UseCases.Usuarios.Crear;
using Biblioteca.Domain;
using Biblioteca.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.UseCases.Usuarios.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, bool>
    {
        private readonly ApplicationDbContext _dbcontext;
        public LoginHandler(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<bool> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = _dbcontext.Users.Where(x => x.Username == request.Username && x.Password == request.Password).FirstOrDefault();

            if (user != null)
            {
                return true;
            }
            
            return false;
        }
    }
}