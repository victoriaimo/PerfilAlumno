using PerfilAlumno.Models;
using PerfilAlumno.Repositories;

namespace PerfilAlumno.Tests
{
    public class UserProfileRepositoryTests
    {
        [Fact]
        public async Task GuardarObtenerYActualizarPerfil()
        {
         
            var repositorio = new UserProfileRepositorySQLite(":memory:");

            var perfil = new UserProfile
            {
                Nombre = "Victoria",
                Edad = 37,
                Descripcion = "Estudiante de programación"
            };

            // Act
            await repositorio.GuardarPerfilAsync(perfil);
            var resultado = await repositorio.ObtenerPerfilAsync();

            // Assert
            Assert.NotNull(resultado);
            Assert.True(resultado.Id > 0);
            Assert.Equal(perfil.Nombre, resultado.Nombre);
            Assert.Equal(perfil.Edad, resultado.Edad);
            Assert.Equal(perfil.Descripcion, resultado.Descripcion);

            // Actualización
            perfil.Nombre = "Victoria Actualizada";
            perfil.Edad = 38;

            await repositorio.GuardarPerfilAsync(perfil);
            var actualizado = await repositorio.ObtenerPerfilAsync();

            Assert.NotNull(actualizado);
            Assert.Equal(resultado.Id, actualizado.Id);
            Assert.Equal("Victoria Actualizada", actualizado.Nombre);
            Assert.Equal(38, actualizado.Edad);
        }
    }
}