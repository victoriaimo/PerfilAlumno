using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PerfilAlumno.Models;
using PerfilAlumno.Views;
using PerfilAlumno.Repositories;
using CommunityToolkit.Maui.Alerts;

namespace PerfilAlumno.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private UserProfile perfil;
        private readonly IUserProfileRepository _repository;

        public ProfileViewModel(IUserProfileRepository repository)
        {
            _repository = repository;

            perfil = new UserProfile
            {
                Nombre = "Victoria",
                Edad = 37,
                Descripcion = "Estudiante de la Tecnicatura Universitaria en Programación de Sistemas.",
                ImagenPerfil = "dotnet_bot.png"
            };

            GuardarCommand = new Command(async () => await Guardar());
            VerDetalleCommand = new Command(async () => await VerDetalle());
            VerUsuariosCommand = new Command(async () => await VerUsuarios());
        }

        public async Task CargarPerfilAsync()
        {
            var perfilGuardado = await _repository.ObtenerPerfilAsync();

            if (perfilGuardado != null)
            {
                perfil = perfilGuardado;

                OnPropertyChanged(nameof(Nombre));
                OnPropertyChanged(nameof(Edad));
                OnPropertyChanged(nameof(Descripcion));
                OnPropertyChanged(nameof(ImagenPerfil));
            }
        }

        public string Nombre
        {
            get => perfil.Nombre;
            set
            {
                perfil.Nombre = value;
                OnPropertyChanged();
            }
        }

        public int Edad
        {
            get => perfil.Edad;
            set
            {
                perfil.Edad = value;
                OnPropertyChanged();
            }
        }

        public string Descripcion
        {
            get => perfil.Descripcion;
            set
            {
                perfil.Descripcion = value;
                OnPropertyChanged();
            }
        }

        public string ImagenPerfil
        {
            get => perfil.ImagenPerfil;
            set
            {
                perfil.ImagenPerfil = value;
                OnPropertyChanged();
            }
        }

        public ICommand GuardarCommand { get; }
        public ICommand VerDetalleCommand { get; }
        public ICommand VerUsuariosCommand { get; }

        private async Task Guardar()
        {
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                await Application.Current!.MainPage!.DisplayAlert(
                    "Error",
                    "El nombre no puede estar vacío.",
                    "Aceptar");

                return;
            }

            if (Edad <= 0)
            {
                await Application.Current!.MainPage!.DisplayAlert(
                    "Error",
                    "La edad debe ser mayor a 0.",
                    "Aceptar");

                return;
            }

            try
            {
                await _repository.GuardarPerfilAsync(perfil);

                await Application.Current!.MainPage!.DisplayAlert(
                    "Perfil",
                    "Los datos fueron guardados correctamente.",
                    "Aceptar");
            }
            catch (Exception)
            {
                await Application.Current!.MainPage!.DisplayAlert(
                    "Error",
                    "No se pudo guardar el perfil.",
                    "Aceptar");
            }
        }

        private async Task VerDetalle()
        {
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                await Application.Current!.MainPage!.DisplayAlert(
                    "Error",
                    "Debe ingresar un nombre antes de continuar.",
                    "Aceptar");

                return;
            }

            if (Edad <= 0)
            {
                await Application.Current!.MainPage!.DisplayAlert(
                    "Error",
                    "La edad debe ser mayor a 0.",
                    "Aceptar");

                return;
            }

            var snackbar = Snackbar.Make(
                "Datos validados correctamente. Abriendo detalle...");

            await snackbar.Show();

            await Shell.Current.GoToAsync(
                $"{nameof(DetallePerfilPage)}" +
                $"?nombre={Uri.EscapeDataString(Nombre)}" +
                $"&edad={Edad}");
        }

        private async Task VerUsuarios()
        {
            await Shell.Current.GoToAsync(nameof(UsuariosPage));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}