using PerfilAlumno.ViewModels;

namespace PerfilAlumno
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new ProfileViewModel();
        }
    }
}