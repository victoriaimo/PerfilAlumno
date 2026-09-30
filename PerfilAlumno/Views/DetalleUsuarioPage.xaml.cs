using PerfilAlumno.ViewModels;

namespace PerfilAlumno.Views;

public partial class DetalleUsuarioPage : ContentPage
{
    public DetalleUsuarioPage()
    {
        InitializeComponent();
        BindingContext = new DetalleUsuarioViewModel();
    }
}