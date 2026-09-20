using PerfilAlumno.ViewModels;

namespace PerfilAlumno.Views;

public partial class DetallePerfilPage : ContentPage
{
    public DetallePerfilPage()
    {
        InitializeComponent();

        BindingContext = new DetallePerfilViewModel();
    }
}