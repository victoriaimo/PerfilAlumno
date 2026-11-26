using PerfilAlumno.ViewModels;

namespace PerfilAlumno
{
    public partial class MainPage : ContentPage
    {
        public MainPage(ProfileViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is ProfileViewModel viewModel)
            {
                await viewModel.CargarPerfilAsync();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (BindingContext is ProfileViewModel viewModel)
            {
                viewModel.DetenerAcelerometro();
            }
        }
    }
}