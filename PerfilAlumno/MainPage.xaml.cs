using PerfilAlumno.ViewModels;

namespace PerfilAlumno
{
    public partial class MainPage : ContentPage
    {
        private readonly ProfileViewModel _viewModel;

        public MainPage(ProfileViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _viewModel.CargarPerfilAsync();
        }
    }
}