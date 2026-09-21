using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PerfilAlumno.Models;
using PerfilAlumno.Views;
using CommunityToolkit.Maui.Alerts;

namespace PerfilAlumno.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private UserProfile perfil;

        public ProfileViewModel()
        {
            perfil = new UserProfile
            {
                Nombre = "Victoria",
                Edad = 37,
                Descripcion = "Estudiante de la Tecnicatura Universitaria en Programación de Sistemas.",
                ImagenPerfil = "dotnet_bot.png"
            };

            GuardarCommand = new Command(Guardar);
            VerDetalleCommand = new Command(async () => await VerDetalle());
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

        private async void Guardar()
        {
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                await Application.Current!.MainPage!.DisplayAlert(
                    "Error",
                    "El nombre no puede estar vacío.",
                    "Aceptar");

                return;
            }

            await Application.Current!.MainPage!.DisplayAlert(
                "Perfil",
                "Los datos fueron guardados correctamente.",
                "Aceptar");
        }

        private async Task VerDetalle()
        {
            // Validamos los parámetros 
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

            // Mostramos una notificación visual indicando
            // que los datos fueron validados correctamente.
            var snackbar = Snackbar.Make(
                "Datos validados correctamente. Abriendo detalle...");

            await snackbar.Show();

            await Shell.Current.GoToAsync(
                $"{nameof(DetallePerfilPage)}" +
                $"?nombre={Uri.EscapeDataString(Nombre)}" +
                $"&edad={Edad}");
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