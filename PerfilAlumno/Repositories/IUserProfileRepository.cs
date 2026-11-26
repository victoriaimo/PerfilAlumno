using PerfilAlumno.Models;

namespace PerfilAlumno.Repositories
{
    public interface IUserProfileRepository
    {
        Task<UserProfile?> ObtenerPerfilAsync();
        Task GuardarPerfilAsync(UserProfile perfil);
    }
}