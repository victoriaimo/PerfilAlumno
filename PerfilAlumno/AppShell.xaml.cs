using PerfilAlumno.Views;

namespace PerfilAlumno
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(
                nameof(DetallePerfilPage),
                typeof(DetallePerfilPage));

            Routing.RegisterRoute(
                nameof(UsuariosPage),
                typeof(UsuariosPage));

            Routing.RegisterRoute(
                nameof(DetalleUsuarioPage),
                typeof(DetalleUsuarioPage));
        }
    }
}