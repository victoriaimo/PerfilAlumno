using SQLite;
using PerfilAlumno.Models;

namespace PerfilAlumno.Repositories
{
    public class UserProfileRepositorySQLite : IUserProfileRepository
    {
        private readonly SQLiteAsyncConnection _database;

        public UserProfileRepositorySQLite()
        {
            var dbPath = Path.Combine(
                FileSystem.AppDataDirectory,
                "perfilalumno.db3");

            _database = new SQLiteAsyncConnection(dbPath);
        }

        public async Task<UserProfile?> ObtenerPerfilAsync()
        {
            await _database.CreateTableAsync<UserProfile>();

            return await _database.Table<UserProfile>()
                .FirstOrDefaultAsync();
        }

        public async Task GuardarPerfilAsync(UserProfile perfil)
        {
            await _database.CreateTableAsync<UserProfile>();

            var existente = await ObtenerPerfilAsync();

            if (existente == null)
            {
                await _database.InsertAsync(perfil);
            }
            else
            {
                perfil.Id = existente.Id;
                await _database.UpdateAsync(perfil);
            }
        }
    }
}