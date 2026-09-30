using PerfilAlumno.ViewModels;

namespace PerfilAlumno.Views;

public partial class UsuariosPage : ContentPage
{
    public UsuariosPage()
    {
        InitializeComponent();
        BindingContext = new UsuariosViewModel();
    }
}