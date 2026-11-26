namespace PerfilAlumno.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = "";
        public int Edad { get; set; }
        public string Descripcion { get; set; } = "";
        public string ImagenPerfil { get; set; } = "";
    }
}