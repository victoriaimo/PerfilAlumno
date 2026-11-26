using PerfilAlumno.Models;
using PerfilAlumno.Repositories;
using PerfilAlumno.Services;
using Xunit;

namespace PerfilAlumno.Tests
{
    public class UserProfileServiceTests
    {
        [Fact]
        public async Task GuardarPerfil_ConDatosValidos_DebeGuardar()
        {
            // Arrange
            var repository = new FakeUserProfileRepository();
            var service = new UserProfileService(repository);

            var perfil = new UserProfile
            {
                Nombre = "Victoria",
                Edad = 37
            };

            // Act
            var resultado = await service.GuardarPerfilAsync(perfil);

            // Assert
            Assert.True(resultado);
            Assert.NotNull(await repository.ObtenerPerfilAsync());
        }

        [Fact]
        public async Task GuardarPerfil_SinNombre_NoDebeGuardar()
        {
            // Arrange
            var repository = new FakeUserProfileRepository();
            var service = new UserProfileService(repository);

            var perfil = new UserProfile
            {
                Nombre = "",
                Edad = 37
            };

            // Act
            var resultado = await service.GuardarPerfilAsync(perfil);

            // Assert
            Assert.False(resultado);
            Assert.Null(await repository.ObtenerPerfilAsync());
        }

        [Fact]
        public async Task GuardarPerfil_ConEdadInvalida_NoDebeGuardar()
        {
            // Arrange
            var repository = new FakeUserProfileRepository();
            var service = new UserProfileService(repository);

            var perfil = new UserProfile
            {
                Nombre = "Victoria",
                Edad = 0
            };

            // Act
            var resultado = await service.GuardarPerfilAsync(perfil);

            // Assert
            Assert.False(resultado);
            Assert.Null(await repository.ObtenerPerfilAsync());
        }
    }
}