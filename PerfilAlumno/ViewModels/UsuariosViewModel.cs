using PerfilAlumno.Models;
using PerfilAlumno.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace PerfilAlumno.ViewModels
{
    public class UsuariosViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;

        private string _mensajeEstado = "Presione el botón para cargar los usuarios.";

        public ObservableCollection<UsuarioApi> Usuarios { get; } = new();

        public string MensajeEstado
        {
            get => _mensajeEstado;
            set
            {
                _mensajeEstado = value;
                OnPropertyChanged();
            }
        }

        public ICommand CargarUsuariosCommand { get; }
        public ICommand VerDetalleUsuarioCommand { get; }

        public UsuariosViewModel()
        {
            _httpClient = new HttpClient();

            CargarUsuariosCommand =
                new Command(async () => await CargarUsuariosAsync());

            VerDetalleUsuarioCommand =
                new Command<UsuarioApi>(async usuario =>
                    await VerDetalleUsuario(usuario));
        }

        private async Task CargarUsuariosAsync()
        {
            try
            {
                MensajeEstado = "Cargando usuarios...";

                var response = await _httpClient.GetAsync(
                    "https://jsonplaceholder.typicode.com/users");

                if (!response.IsSuccessStatusCode)
                {
                    MensajeEstado = ObtenerMensajeErrorHttp(response.StatusCode);
                    return;
                }

                var usuarios =
                    await response.Content.ReadFromJsonAsync<List<UsuarioApi>>();

                Usuarios.Clear();

                if (usuarios != null)
                {
                    foreach (var usuario in usuarios)
                    {
                        Usuarios.Add(usuario);
                    }
                }

                MensajeEstado = "HTTP 200: Usuarios cargados correctamente.";
            }
            catch (HttpRequestException)
            {
                MensajeEstado =
                    "Error de conexión. Verifique su conexión a Internet.";
            }
            catch (TaskCanceledException)
            {
                MensajeEstado =
                    "La solicitud tardó demasiado tiempo.";
            }
            catch (Exception)
            {
                MensajeEstado =
                    "Ocurrió un error inesperado al cargar los usuarios.";
            }
        }

        private string ObtenerMensajeErrorHttp(HttpStatusCode statusCode)
        {
            return statusCode switch
            {
                HttpStatusCode.BadRequest =>
                    "Error HTTP 400: solicitud incorrecta.",

                HttpStatusCode.NotFound =>
                    "Error HTTP 404: recurso no encontrado.",

                HttpStatusCode.InternalServerError =>
                    "Error HTTP 500: error interno del servidor.",

                _ =>
                    $"Error HTTP {(int)statusCode}: no se pudieron obtener los datos."
            };
        }

        private async Task VerDetalleUsuario(UsuarioApi usuario)
        {
            if (usuario == null)
                return;

            await Shell.Current.GoToAsync(
                $"{nameof(DetalleUsuarioPage)}" +
                $"?nombre={Uri.EscapeDataString(usuario.Name)}" +
                $"&email={Uri.EscapeDataString(usuario.Email)}" +
                $"&telefono={Uri.EscapeDataString(usuario.Phone)}");
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