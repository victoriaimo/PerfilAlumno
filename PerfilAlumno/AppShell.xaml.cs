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
        }
    }
}