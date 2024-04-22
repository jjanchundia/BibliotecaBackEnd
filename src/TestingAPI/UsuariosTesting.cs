using Biblioteca.Application.UseCases.Usuarios.Crear;
using Biblioteca.Application.UseCases.Usuarios.Login;
using Biblioteca.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace TestingAPI
{
    public class UsuariosTesting
    {
        [Fact]
        public async Task Handle_CreacionUsuario()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new ApplicationDbContext(options))
            {
                var handler = new CreateUsuarioHandler(dbContext);
                var command = new CreateUsuarioCommand
                {
                    Username = "usuario_prueba",
                    Password = "contraseña_prueba",
                    Role = "rol_prueba"
                };

                // Act
                var result = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.True(result.IsSuccess);

                // Verificamos de que se ha agregado el usuario a la base de datos
                var usuarioAgregado = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == "usuario_prueba");
                Assert.NotNull(usuarioAgregado);
                Assert.Equal("usuario_prueba", usuarioAgregado.Username);
                Assert.Equal("contraseña_prueba", usuarioAgregado.Password);
                Assert.Equal("rol_prueba", usuarioAgregado.Role);
            }
        }

        [Fact]
        public async Task Login_UsuarioAutenticado_ReturnsToken()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var configMock = new Mock<IConfiguration>();

            using (var dbContext = new ApplicationDbContext(options))
            {
                var handler = new LoginHandler(dbContext, configMock.Object);
                var command = new LoginCommand
                {
                    Username = "usuario_prueba",
                    Password = "contraseña_prueba",
                };

                var result = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.True(result.IsSuccess);
                //El resultado de esta prueba da error debido a que la BD es en memoria y al tratar de buscar el usuario y contraseña
                //este no lo encuentra ya que al estar en memoria este se pierde
            }
        }
    }
}