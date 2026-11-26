using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PerfilAlumno.Models;
using PerfilAlumno.Views;
using PerfilAlumno.Services;
using CommunityToolkit.Maui.Alerts;

namespace PerfilAlumno.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private UserProfile perfil;
        private readonly UserProfileService _service;
        private string _datosAcelerometro = "Acelerómetro detenido";

        public ProfileViewModel(UserProfileService service)
        {
            _service = service;

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
            TomarFotoCommand = new Command(async () => await TomarFoto());
            ObtenerUbicacionCommand = new Command(async () => await ObtenerUbicacion());
            IniciarAcelerometroCommand = new Command(async () => await IniciarAcelerometro());
            DetenerAcelerometroCommand = new Command(DetenerAcelerometro);
        }

        public async Task CargarPerfilAsync()
        {
            var perfilGuardado = await _service.ObtenerPerfilAsync();

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

        public string DatosAcelerometro
        {
            get => _datosAcelerometro;
            set
            {
                _datosAcelerometro = value;
                OnPropertyChanged();
            }
        }

        public ICommand GuardarCommand { get; }
        public ICommand VerDetalleCommand { get; }
        public ICommand VerUsuariosCommand { get; }
        public ICommand TomarFotoCommand { get; }
        public ICommand ObtenerUbicacionCommand { get; }
        public ICommand IniciarAcelerometroCommand { get; }
        public ICommand DetenerAcelerometroCommand { get; }

        private async Task IniciarAcelerometro()
        {
            try
            {
                if (!Accelerometer.Default.IsSupported)
                {
                    await Application.Current!.MainPage!.DisplayAlert(
                        "Acelerómetro",
                        "El dispositivo no dispone de acelerómetro.",
                        "Aceptar");

                    return;
                }

                if (!Accelerometer.Default.IsMonitoring)
                {
                    Accelerometer.Default.ReadingChanged += AcelerometroReadingChanged;

                    Accelerometer.Default.Start(SensorSpeed.UI);

                    DatosAcelerometro = "Acelerómetro iniciado";
                }
            }
            catch (Exception)
            {
                Accelerometer.Default.ReadingChanged -= AcelerometroReadingChanged;

                await Application.Current!.MainPage!.DisplayAlert(
                    "Error",
                    "No se pudo iniciar el acelerómetro.",
                    "Aceptar");
            }
        }

        private void AcelerometroReadingChanged(
            object? sender,
            AccelerometerChangedEventArgs e)
        {
            var aceleracion = e.Reading.Acceleration;

            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (Accelerometer.Default.IsMonitoring)
                {
                    DatosAcelerometro =
                        $"X: {aceleracion.X:F2}\n" +
                        $"Y: {aceleracion.Y:F2}\n" +
                        $"Z: {aceleracion.Z:F2}";
                }
            });
        }

        public void DetenerAcelerometro()
        {
            try
            {
                if (Accelerometer.Default.IsMonitoring)
                {
                    Accelerometer.Default.Stop();
                }

                DatosAcelerometro = "Acelerómetro detenido";
            }
            catch (Exception)
            {
                DatosAcelerometro = "No se pudo detener el acelerómetro.";
            }
            finally
            {
                Accelerometer.Default.ReadingChanged -= AcelerometroReadingChanged;
            }
        }

        private async Task ObtenerUbicacion()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                }

                if (status != PermissionStatus.Granted)
                {
                    await Application.Current!.MainPage!.DisplayAlert(
                        "Permiso denegado",
                        "Debe autorizar el acceso a la ubicación.",
                        "Aceptar");

                    return;
                }

                var request = new GeolocationRequest(
                    GeolocationAccuracy.High,
                    TimeSpan.FromSeconds(10));

                var ubicacion = await Geolocation.GetLocationAsync(request);

                if (ubicacion != null)
                {
                    await Application.Current!.MainPage!.DisplayAlert(
                        "Ubicación actual",
                        $"Latitud: {ubicacion.Latitude}\n" +
                        $"Longitud: {ubicacion.Longitude}\n" +
                        $"Precisión: {ubicacion.Accuracy} metros",
                        "Aceptar");
                }
                else
                {
                    await Application.Current!.MainPage!.DisplayAlert(
                        "Ubicación",
                        "No se pudo obtener la ubicación actual.",
                        "Aceptar");
                }
            }
            catch (FeatureNotSupportedException)
            {
                await Application.Current!.MainPage!.DisplayAlert(
                    "GPS",
                    "El GPS no está disponible en este dispositivo.",
                    "Aceptar");
            }
            catch (FeatureNotEnabledException)
            {
                await Application.Current!.MainPage!.DisplayAlert(
                    "GPS",
                    "Debe activar la ubicación del dispositivo.",
                    "Aceptar");
            }
            catch (PermissionException)
            {
                await Application.Current!.MainPage!.DisplayAlert(
                    "Permiso denegado",
                    "No se autorizó el acceso a la ubicación.",
                    "Aceptar");
            }
            catch (Exception)
            {
                await Application.Current!.MainPage!.DisplayAlert(
                    "Error",
                    "No se pudo obtener la ubicación.",
                    "Aceptar");
            }
        }

        private async Task TomarFoto()
        {
            try
            {
                if (!MediaPicker.Default.IsCaptureSupported)
                {
                    await Application.Current!.MainPage!.DisplayAlert(
                        "Cámara",
                        "La cámara no está disponible en este dispositivo.",
                        "Aceptar");

                    return;
                }

                var status = await Permissions.CheckStatusAsync<Permissions.Camera>();

                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.Camera>();
                }

                if (status != PermissionStatus.Granted)
                {
                    await Application.Current!.MainPage!.DisplayAlert(
                        "Permiso denegado",
                        "Debe autorizar el acceso a la cámara.",
                        "Aceptar");

                    return;
                }

                var foto = await MediaPicker.Default.CapturePhotoAsync();

                if (foto != null)
                {
                    var ruta = Path.Combine(
                        FileSystem.AppDataDirectory,
                        Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName));

                    using var origen = await foto.OpenReadAsync();

                    using (var destino = File.OpenWrite(ruta))
                    {
                        await origen.CopyToAsync(destino);
                    }

                    ImagenPerfil = ruta;
                }
            }
            catch (Exception)
            {
                await Application.Current!.MainPage!.DisplayAlert(
                    "Error",
                    "No se pudo capturar la fotografía.",
                    "Aceptar");
            }
        }

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
                if (!await _service.GuardarPerfilAsync(perfil))
                    return;

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