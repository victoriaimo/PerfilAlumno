using SQLite;

namespace PerfilAlumno.Models
{
    public class UserProfile
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Nombre { get; set; } = "";
        public int Edad { get; set; }
        public string Descripcion { get; set; } = "";
        public string ImagenPerfil { get; set; } = "";
    }
}