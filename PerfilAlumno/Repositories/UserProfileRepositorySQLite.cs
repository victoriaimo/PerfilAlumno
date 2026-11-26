using SQLite;
using PerfilAlumno.Models;

namespace PerfilAlumno.Repositories
{
    public class UserProfileRepositorySQLite : IUserProfileRepository
    {
        private readonly SQLiteAsyncConnection _database;

        public UserProfileRepositorySQLite(string ruta)
        {
            _database = new SQLiteAsyncConnection(ruta);
        }

        public async Task<UserProfile?> ObtenerPerfilAsync()
        {
            await _database.CreateTableAsync<UserProfileEntity>();

            var entidad = await _database
                .Table<UserProfileEntity>()
                .FirstOrDefaultAsync();

            if (entidad == null)
                return null;

            return new UserProfile
            {
                Id = entidad.Id,
                Nombre = entidad.Nombre,
                Edad = entidad.Edad,
                Descripcion = entidad.Descripcion,
                ImagenPerfil = entidad.ImagenPerfil
            };
        }

        public async Task GuardarPerfilAsync(UserProfile perfil)
        {
            await _database.CreateTableAsync<UserProfileEntity>();

            var existente = await _database
                .Table<UserProfileEntity>()
                .FirstOrDefaultAsync();

            var entidad = new UserProfileEntity
            {
                Nombre = perfil.Nombre,
                Edad = perfil.Edad,
                Descripcion = perfil.Descripcion,
                ImagenPerfil = perfil.ImagenPerfil
            };

            if (existente == null)
            {
                await _database.InsertAsync(entidad);
            }
            else
            {
                entidad.Id = existente.Id;
                await _database.UpdateAsync(entidad);
            }

            perfil.Id = entidad.Id;
        }
    }
}