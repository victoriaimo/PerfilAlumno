using PerfilAlumno.ViewModels;

namespace PerfilAlumno.Views;

public partial class UsuariosPage : ContentPage
{
    public UsuariosPage(UsuariosViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}