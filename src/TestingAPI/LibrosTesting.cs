using Biblioteca.Application.UseCases.Libros.Consultar;
using Biblioteca.Application.UseCases.Libros.ConsultarPorId;
using Biblioteca.Application.UseCases.Libros.Crear;
using Biblioteca.Application.UseCases.Libros.Devolver;
using Biblioteca.Application.UseCases.Libros.Editar;
using Biblioteca.Application.UseCases.Libros.Eliminar;
using Biblioteca.Application.UseCases.Libros.Prestar;
using Biblioteca.Persistence;
using Microsoft.EntityFrameworkCore;

namespace TestingAPI
{
    public class LibrosTesting
    {
        [Fact]
        public async Task Handle_ConsultarLibro()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new ApplicationDbContext(options))
            {
                var handler = new GetAllLibrosHandler(dbContext);
                var command = new GetlAllLibrosQuery();

                // Act
                var result = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.True(result.IsSuccess);
            }
        }

        [Fact]
        public async Task Handle_ConsultarLibroPorId()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new ApplicationDbContext(options))
            {
                var handler = new GetAllLibroByIdHandler(dbContext);
                var command = new GetlAllLibrosQueryById() { LibroId = 1 };

                // Act
                var result = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.True(result.IsSuccess);
            }
        }

        [Fact]
        public async Task Handle_CreacionLibro()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new ApplicationDbContext(options))
            {
                var handler = new CreateLibroHandler(dbContext);
                var command = new CreateLibroCommand
                {
                    Nombre = "LibroTest",
                    Descripcion = "Test",
                };

                // Act
                var result = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.True(result.IsSuccess);

                // Verificamos de que se ha agregado el usuario a la base de datos
                var libroAdd = await dbContext.Libro.FirstOrDefaultAsync(u => u.LibroId == result.Value.LibroId);
                Assert.NotNull(libroAdd);
                Assert.Equal("LibroTest", libroAdd.Nombre);
                Assert.Equal("Test", libroAdd.Descripcion);
            }
        }

        [Fact]
        public async Task Handle_EditarLibro()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new ApplicationDbContext(options))
            {
                var handler = new EditLibroHandler(dbContext);
                var command = new EditLibroCommand
                {
                    LibroId = 1,
                    Nombre = "LibroTest",
                    Descripcion = "Test",
                };

                // Act
                var result = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.True(result.IsSuccess);

                // Verificamos de que se ha agregado el usuario a la base de datos
                var libroEdit = await dbContext.Libro.FirstOrDefaultAsync(u => u.LibroId == result.Value.LibroId);
                Assert.NotNull(libroEdit);
                Assert.Equal("LibroTest", libroEdit.Nombre);
                Assert.Equal("Test", libroEdit.Descripcion);
            }
        }

        [Fact]
        public async Task Handle_PrestarLibro()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new ApplicationDbContext(options))
            {
                var handler = new PrestarHandler(dbContext);
                var command = new PrestarCommand
                {
                    LibroId = 1,
                };

                // Act
                var result = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.True(result.IsSuccess);

                // Verificamos de que se ha agregado el usuario a la base de datos
                var libro = await dbContext.Libro.FirstOrDefaultAsync(u => u.LibroId == command.LibroId);
                Assert.NotNull(libro);
                Assert.Equal("P", libro.Estado);
            }
        }

        [Fact]
        public async Task Handle_DevolverLibro()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new ApplicationDbContext(options))
            {
                var handler = new DevolverLibroHandler(dbContext);
                var command = new DevolverLibroCommand
                {
                    LibroId = 1,
                };

                // Act
                var result = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.True(result.IsSuccess);

                // Verificamos de que se ha agregado el usuario a la base de datos
                var libro = await dbContext.Libro.FirstOrDefaultAsync(u => u.LibroId == command.LibroId);
                Assert.NotNull(libro);
                Assert.Equal("L", libro.Estado);
            }
        }

        [Fact]
        public async Task Handle_EliminarLibro()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new ApplicationDbContext(options))
            {
                var handler = new DeleteLibroHandler(dbContext);
                var command = new DeleteLibroCommand
                {
                    LibroId = 1,
                };

                // Act
                var result = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.True(result.IsSuccess);
            }
        }
    }
}