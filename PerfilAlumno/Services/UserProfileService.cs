using PerfilAlumno.Models;
using PerfilAlumno.Repositories;

namespace PerfilAlumno.Services
{
    public class UserProfileService
    {
        private readonly IUserProfileRepository _repository;

        public UserProfileService(IUserProfileRepository repository)
        {
            _repository = repository;
        }

        public string? ObtenerErrorValidacion(UserProfile perfil)
        {
            if (string.IsNullOrWhiteSpace(perfil.Nombre))
            {
                return "El nombre no puede estar vacío.";
            }

            if (perfil.Edad <= 0)
            {
                return "La edad debe ser mayor a 0.";
            }

            return null;
        }

        public bool ValidarPerfil(UserProfile perfil)
        {
            return ObtenerErrorValidacion(perfil) == null;
        }

        public async Task<bool> GuardarPerfilAsync(UserProfile perfil)
        {
            if (!ValidarPerfil(perfil))
                return false;

            await _repository.GuardarPerfilAsync(perfil);

            return true;
        }

        public async Task<UserProfile?> ObtenerPerfilAsync()
        {
            return await _repository.ObtenerPerfilAsync();
        }
    }
}