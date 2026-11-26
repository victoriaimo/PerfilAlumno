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

        public bool ValidarPerfil(UserProfile perfil)
        {
            return !string.IsNullOrWhiteSpace(perfil.Nombre)
                && perfil.Edad > 0;
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