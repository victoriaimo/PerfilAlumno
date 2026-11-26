using PerfilAlumno.Models;

namespace PerfilAlumno.Repositories
{
    public class FakeUserProfileRepository : IUserProfileRepository
    {
        private UserProfile? _perfil;

        public Task<UserProfile?> ObtenerPerfilAsync()
        {
            return Task.FromResult(_perfil);
        }

        public Task GuardarPerfilAsync(UserProfile perfil)
        {
            _perfil = perfil;

            return Task.CompletedTask;
        }
    }
}