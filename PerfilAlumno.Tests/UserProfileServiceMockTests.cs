using Moq;
using PerfilAlumno.Models;
using PerfilAlumno.Repositories;
using PerfilAlumno.Services;

namespace PerfilAlumno.Tests
{
    public class UserProfileServiceMockTests
    {
        [Fact]
        public async Task GuardarPerfilValido_LlamaAlRepositorio()
        {
            // Arrange
            var mockRepo = new Mock<IUserProfileRepository>();

            var perfil = new UserProfile
            {
                Nombre = "Victoria",
                Edad = 37
            };

            mockRepo.Setup(r => r.GuardarPerfilAsync(perfil))
                .Returns(Task.CompletedTask);

            var servicio = new UserProfileService(mockRepo.Object);

            // Act
            var resultado = await servicio.GuardarPerfilAsync(perfil);

            // Assert
            Assert.True(resultado);

            mockRepo.Verify(
                r => r.GuardarPerfilAsync(perfil),
                Times.Once());
        }

        [Fact]
        public async Task GuardarPerfilInvalido_NoLlamaAlRepositorio()
        {
            // Arrange
            var mockRepo = new Mock<IUserProfileRepository>();

            var perfil = new UserProfile
            {
                Nombre = "",
                Edad = 37
            };

            var servicio = new UserProfileService(mockRepo.Object);

            // Act
            var resultado = await servicio.GuardarPerfilAsync(perfil);

            // Assert
            Assert.False(resultado);

            mockRepo.Verify(
                r => r.GuardarPerfilAsync(It.IsAny<UserProfile>()),
                Times.Never());
        }
    }
}